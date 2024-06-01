using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.ChoiceHelpers;

namespace WebApplication1.Models.Edit.ToRender
{
    public class MotorList
    {
        public List<MotorType> MotorTypes { get; set; }
        public List<ConditionsSet> Conditions { get; set; }
        public MotorList(ApplicationContext db) 
        { 
            MotorTypes = db.MotorTypes.Include(cs=>cs.conditionsSet).ToList();
            Conditions = db.ConditionsSets.ToList();
        }
    }
}
