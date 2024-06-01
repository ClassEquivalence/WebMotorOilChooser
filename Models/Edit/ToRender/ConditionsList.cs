using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.ChoiceHelpers;
using WebApplication1.Models.ChoiceHelpers.Conditions;

namespace WebApplication1.Models.Edit.ToRender
{
    public class ConditionsList
    {
        public List<ConditionsSet> Conditions { get; set; }
        public List<APIQualityCondition> APIConditions { get; set; }
        public List<OilTypeCondition> OilTypeConditions { get; set; }
        public ConditionsList(ApplicationContext db) 
        {
            APIConditions = db.APIQualityConditions.Include(cs => cs.ConditionsSet).ToList();
            OilTypeConditions = db.OilTypeConditions.Include(cs => cs.ConditionsSet).ToList();
            Conditions = db.ConditionsSets.Include(sae => sae.SAEViscosityConditions).ToList();
        }
    }
}
