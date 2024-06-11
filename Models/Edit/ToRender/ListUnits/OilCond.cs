using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.ChoiceHelpers;
using WebApplication1.Models.ChoiceHelpers.Conditions;
using WebApplication1.Models.MotorOilStats;

namespace WebApplication1.Models.Edit.ToRender.ListUnits
{
    public class OilCond
    {
        public int condSetId;
        public OilTypeCondition OilTypeCondition { get; set; }
        public List<MotorOil> Oils { get; set; }
        public OilCond(ApplicationContext db, int condSetId)
        {
            Oils = db.MotorOils.Include(sae => sae.SAEViscosity).
                Include(api=>api.APIQualityClass).ToList();
            this.condSetId = condSetId;
            OilTypeCondition = new();
            OilTypeCondition.MotorOil = Oils[0];
            OilTypeCondition.ConditionsSetId = condSetId;
            db.OilTypeConditions.Add(OilTypeCondition);
            db.SaveChanges();
        }
    }
}

