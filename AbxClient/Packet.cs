
namespace AbxClientHandle.pkt
{
    internal class Packet
    {
        public string Symbol { get; set; }
        public char BuySellIndicator { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Sequence { get; set; }

        public Packet(string symbol, char buySellIndicator, int quantity, int price, int Sequence)
        {
            this.Symbol = symbol;
            this.BuySellIndicator = buySellIndicator;
            this.Quantity = quantity;
            this.Price = price;
            this.Sequence = Sequence;

        }
    }
}