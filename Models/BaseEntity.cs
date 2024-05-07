using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class BaseEntity
    {
        [Key]
        public int id { get; set; }
    }
}
