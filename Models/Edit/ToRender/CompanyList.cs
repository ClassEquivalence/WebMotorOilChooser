namespace WebApplication1.Models.Edit.ToRender
{
    public class CompanyList
    {
        public List<Company> Companies { get; set; }
        public CompanyList(ApplicationContext db) 
        {
            Companies = db.Companies.ToList();
        }
    }
}
