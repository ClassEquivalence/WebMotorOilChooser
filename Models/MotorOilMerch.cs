namespace WebApplication1.Models
{
    public class MotorOilMerch : BaseEntity
    {
        public MotorOil? MotorOil { get; set; }
        public int MotorOilId { get; set; }
        public Store? Store { get; set; }
        public int StoreId { get; set; }
        public double Price { get; set; }
        public double StockCount { get; set; }
    }
}
