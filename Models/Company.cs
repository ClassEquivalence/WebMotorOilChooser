namespace WebApplication1.Models
{
    public class Company : BaseEntity
    {
        public string? Name { get; set; }
        public List<Store>? Stores { get; set; }
    }
}
