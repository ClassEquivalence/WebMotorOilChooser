namespace WebApplication1.Models.Users
{
    public class Permission: BaseEntity
    {
        public bool CanEditOils { get; set; }
        public bool CanEditStores { get; set; }
        public bool CanEditMerch { get; set; }
        public bool CanEditCompany { get; set; }
        public bool CanEditUsers { get; set; }
    }
}
