using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.ChoiceHelpers;
using WebApplication1.Models.ChoiceHelpers.Conditions;
using WebApplication1.Models.MotorOilStats;
using WebApplication1.Models.ToRender;

namespace WebApplication1.Models.Edit.ToRender
{
    public class ConditionsList : RenderBase
    {
        public List<ConditionsSet> Conditions { get; set; }
        public List<MotorOil> Oils { get; set; }
        public List<APIQualityClass> APIQualityClasses { get; set; }
        public ConditionsList(ApplicationContext db) 
        {
            Conditions = db.ConditionsSets.Include(sae => sae.SAEViscosityConditions).
                Include(api=>api.APIQualityConditions).
                Include(oilc=>oilc.OilTypeConditions).ToList();
            APIQualityClasses = db.APIQualityClasses.ToList();
            Oils = db.MotorOils.Include(sae => sae.SAEViscosity).ToList();
        }
    }
}
