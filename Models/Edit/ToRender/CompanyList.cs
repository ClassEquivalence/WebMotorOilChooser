using WebApplication1.Models.ToRender;

namespace WebApplication1.Models.Edit.ToRender
{
    public class CompanyList : RenderBase
    {
        public List<Company> Companies { get; set; }
        public CompanyList(ApplicationContext db) 
        {
            Companies = db.Companies.ToList();
        }
    }
}
