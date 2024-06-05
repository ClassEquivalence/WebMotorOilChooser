namespace WebApplication1.Models.Edit.ToAccept.ListUnits
{
    public class Merch
    {
        public int merchId {  get; set; }
        public int oilchoose {  get; set; }
        public int storechoose {  get; set; }
        public double price { get; set; }
        public double stockcount { get; set; }

        public void ToMotorOilMerch(MotorOilMerch m)
        {
            m.MotorOilId = oilchoose;
            m.StoreId = storechoose;
            m.Price = price;
            m.StockCount = stockcount;
        }
    }
}
