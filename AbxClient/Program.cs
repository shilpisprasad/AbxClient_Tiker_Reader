using System;
using AbxClientHandle;

namespace AbxClientMain
{
    class Client
    {
        static void Main(string[] args)
        {
            String output = "Output.json";
            AbxClient client;
            try
            {
                client = new AbxClient();
                byte[] request = client.CreateStreamRequest();
                client.sendRequest(request);
                client.ReceiveAllPkt();
                client.HandleMissingSeq();
                client.jsonConvert(output);
                client.CloseConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Something went wrong: {ex.Message}");
            }
     
        }

    }
}