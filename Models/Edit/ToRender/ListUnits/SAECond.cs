using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.ChoiceHelpers;
using WebApplication1.Models.ChoiceHelpers.Conditions;
using WebApplication1.Models.MotorOilStats;

namespace WebApplication1.Models.Edit.ToRender.ListUnits
{
    public class SAECond
    {
        public int condSetId;
        public SAEViscosityCondition SAEViscosityCondition { get; set; }
        public SAECond(ApplicationContext db, int condSetId)
        {
            this.condSetId = condSetId;
            SAEViscosityCondition = new();
            SAEViscosityCondition.ConditionsSetId = condSetId;
            db.SAEViscosityConditions.Add(SAEViscosityCondition);
            db.SaveChanges();
        }
    }
}
