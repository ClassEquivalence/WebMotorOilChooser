using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.ChoiceHelpers;
using WebApplication1.Models.FormsData;

namespace WebApplication1.Models.ToRender
{
    public class ChoosebyMotor: OilsAndFilters
    {
        public List<MotorType> MotorsList { get; set; }
        public override void PrepareData(ApplicationContext db)
        {
            base.PrepareData(db);
            MotorsList = db.MotorTypes.Include(cs => cs.conditionsSet).ThenInclude(cond => cond.APIQualityConditions)
                .Include(cs => cs.conditionsSet).ThenInclude(cond => cond.SAEViscosityConditions)
                .Include(cs => cs.conditionsSet).ThenInclude(cond => cond.OilTypeConditions).ToList();
        }
        public void PrepareData(ApplicationContext db, OilFiltersFormData filters, string motorName)
        {
            base.PrepareData(db, filters);
            MotorType? motor = null;
            foreach(MotorType m in MotorsList)
            {
                if (m.Name == motorName)
                {
                    motor = m;
                    break;
                }
            }
            if (motor == null)
                throw new Exception("Motor not found(tried finding it by Name)");


            List<MotorOilMerch> newList = new List<MotorOilMerch>();
            MotorOil oil;
            foreach (MotorOilMerch merch in MotorOilMerches)
            {
                if (filters.OilSuitsFilter(merch) && motor.conditionsSet.OilAcceptable(merch.MotorOil))
                    newList.Add(merch);
            }
            MotorOilMerches = newList;
        }
    }
}
