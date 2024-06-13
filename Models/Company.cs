using WebApplication1.Models.Users;

namespace WebApplication1.Models
{
    public class Company : BaseEntity
    {
        public string? Name { get; set; }
        public List<Store>? Stores { get; set; }
        public User? Owner { get; set; }
        public int? OwnerId { get; set; }


        public static List<User>? Users;
        public static void initUsers(ApplicationContext db)
        {
            Users = db.Users.ToList();
        }
    }
}

