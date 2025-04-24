using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using AbxClientHandle.pkt;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AbxClientHandle
{
    class AbxClient
    {
        TcpClient tcpClient;
        NetworkStream stream;
        public bool IsSuccess = false;
        List<Packet> listPacket;
        public AbxClient()
        {
           InitConnection();
        }
        public void InitConnection()
        {
            stream?.Close();
            tcpClient?.Close();
            try
            {
                tcpClient = new TcpClient("localhost", 3000);
                stream = tcpClient.GetStream();
                IsSuccess = true;
                Console.WriteLine("Connected to the server");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Could not connect to server: {ex.Message}");
            }
        }
        public byte[] CreateStreamRequest() { return new byte[]{1, 0};  }
        public byte[] ReciveResponse(byte resendSeq) { return new byte[] {2, resendSeq}; }
        public void sendRequest(byte[] requestData)
        {
            try
            {
                stream.Write(requestData, 0, requestData.Length);
                Console.WriteLine("Request sent to server.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"[ERROR] Failed to send data: {ex.Message}");
            }
        }

        public void ReceiveAllPkt()
        {   try
            {
                byte[] buffer = new byte[17];
                listPacket = new List<Packet>();
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    listPacket.Add(HandleResponse(buffer));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Unexpected error in ReceiveAllPkt: {ex.Message}");
            }
        }
        public  Packet HandleResponse(byte[] buffer)
        {
            string symbol;
            char buySellIndicator;
            int quantity, price, packetSequence;
            symbol = Encoding.ASCII.GetString(buffer, 0, 4);
            buySellIndicator = (char)buffer[4];
            quantity = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 5));
            price = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 9));
            packetSequence = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 13));
            Console.WriteLine($"Symbol : {symbol}, Buy/sell: {buySellIndicator}, quantity: {quantity}, price: {price}, seq: {packetSequence}");
            return new Packet(symbol, buySellIndicator, quantity, price, packetSequence);
        }
        public  void HandleMissingSeq()
        {
            InitConnection();
            if (!IsSuccess) return;
            List<int> seqs = listPacket.Select(p => p.Sequence).ToList();
            if (seqs.Max() == seqs.Count)
            {
                Console.WriteLine("[INFO] No missing sequences found.");
                return;
            }
            List<int> misSeq = Enumerable.Range(1, seqs.Max()).Except(seqs).ToList();
            foreach (int seq in misSeq)
            {
                try
                {
                    byte[] buffer = new byte[17];
                    sendRequest(ReciveResponse((byte)seq));
                    stream.Read(buffer, 0, buffer.Length);
                    listPacket.Add(HandleResponse(buffer));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Failed to handle missing sequence {seq}: {ex.Message}");
                }
            }

        }
        
        

        public void jsonConvert(String file)
        {
            try
            {

                var opt = new JsonSerializerOptions() { WriteIndented = true };
                string strJson = JsonSerializer.Serialize(listPacket, opt);
                File.WriteAllText(file, strJson);
                Console.WriteLine($"Data saved to {file}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to convert to JSON: {ex.Message}");
            }
        }
        public void CloseConnection()
        {
            stream?.Close();
            tcpClient?.Close();
        }
    }       
     
}