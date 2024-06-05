using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.MotorOilStats;
using WebApplication1.Models.ToRender;

namespace WebApplication1.Models.Edit.ToRender
{
    public class OilList : RenderBase
    {
        public List<MotorOil> MotorOils { get; set;}
        public OilList(ApplicationContext db)
        {
            MotorOils = db.MotorOils.Include(s=>s.SAEViscosity).Include(api=>api.APIQualityClass).ToList();
        }
    }
}
