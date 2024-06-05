namespace WebApplication1.Models.Users
{
    public class Role: BaseEntity
    {
        public string? Name { get; set; }
        public Permission? Permission {  get; set; }
        public int? PermissionId { get; set;}
    }
}
