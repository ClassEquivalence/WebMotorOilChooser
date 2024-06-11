using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.ChoiceHelpers;
using WebApplication1.Models.MotorOilStats;

namespace WebApplication1.Models.Edit.ToRender.ListUnits
{
    public class CondSet
    {
        public ConditionsSet ConditionsSet {  get; set; }
        public List<MotorOil> Oils { get; set; }
        public List<APIQualityClass> APIQualityClasses { get; set; }
        public CondSet(ApplicationContext db)
        {
            ConditionsSet = new();
            APIQualityClasses = db.APIQualityClasses.ToList();
            Oils = db.MotorOils.Include(sae => sae.SAEViscosity).ToList();
            db.ConditionsSets.Add(ConditionsSet);
            db.SaveChanges();
            ConditionsSet = db.ConditionsSets.Include(sae => sae.SAEViscosityConditions).
                Include(api => api.APIQualityConditions).
                Include(oilc => oilc.OilTypeConditions).Where(cs=>cs.id==ConditionsSet.id).ToList()[0];
        }
    }
}
