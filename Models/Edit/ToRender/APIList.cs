using Npgsql.NameTranslation;
using WebApplication1.Models.MotorOilStats;

namespace WebApplication1.Models.Edit.ToRender
{
    public class APIList
    {
        public List<APIQualityClass> qualityClasses;
        public APIList(ApplicationContext db)
        {
            qualityClasses = db.APIQualityClasses.ToList();
        }
    }
}
