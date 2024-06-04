using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.MotorOilStats;

namespace WebApplication1.Models.Edit.ToRender
{
    public class OilList
    {
        public List<MotorOil> MotorOils { get; set;}
        public OilList(ApplicationContext db)
        {
            MotorOils = db.MotorOils.Include(s=>s.SAEViscosity).Include(api=>api.APIQualityClass).ToList();
        }
    }
}
