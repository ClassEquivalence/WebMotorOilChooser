using WebApplication1.Models.ChoiceHelpers.Conditions;
using WebApplication1.Models.MotorOilStats;

namespace WebApplication1.Models.Edit.ToRender.ListUnits
{
    public class APICond
    {
        public int condSetId;
        public List<APIQualityClass> APIQualityClasses { get; set; }
        public APIQualityCondition APIQualityCondition { get; set; }
        public APICond(ApplicationContext db, int condSetId)
        {
            APIQualityClasses = db.APIQualityClasses.ToList();
            this.condSetId = condSetId;
            APIQualityCondition = new();
            APIQualityCondition.MinAPIQualityClass = APIQualityClasses[0];
            APIQualityCondition.ConditionsSetId = condSetId;
            db.APIQualityConditions.Add(APIQualityCondition);
            db.SaveChanges();
        }
    }
}
