using WebApplication1.Models.ToRender;
using WebApplication1.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models.Edit.ToRender
{
    public class CompanyList : RenderBase
    {
        public List<Company> Companies { get; set; }
        public List<User> Users { get; set; }
        public CompanyList(ApplicationContext db) 
        {
            Companies = db.Companies.Include(c=>c.Stores).ToList();
            Users = db.Users.Include(u=>u.Role).ThenInclude(r=>r.Permission).ToList();
            
        }
    }
}
