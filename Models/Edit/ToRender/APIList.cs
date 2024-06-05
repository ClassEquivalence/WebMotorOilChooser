using Npgsql.NameTranslation;
using WebApplication1.Models.MotorOilStats;
using WebApplication1.Models.ToRender;

namespace WebApplication1.Models.Edit.ToRender
{
    public class APIList : RenderBase
    {
        public List<APIQualityClass> qualityClasses;
        public APIList(ApplicationContext db)
        {
            qualityClasses = db.APIQualityClasses.ToList();
        }
    }
}
