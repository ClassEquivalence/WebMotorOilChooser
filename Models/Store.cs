namespace WebApplication1.Models
{
    public class Store : BaseEntity
    {
        public Company? Company { get; set; }
        public int CompanyId { get; set; }
        public string? Adress { get; set; }
        public List<MotorOilMerch>? MotorOilMerches { get; set; }
    }
}
