using System.Collections;
using WebApplication1.Models.ChoiceHelpers.Conditions;

namespace WebApplication1.Models.ChoiceHelpers
{
    public class ConditionsSet: BaseEntity
    {
        public List<APIQualityCondition> APIQualityConditions { get; set; }
        public List<OilTypeCondition> OilTypeConditions { get; set; }
        public List<SAEViscosityCondition> SAEViscosityConditions { get; set; }
        public List<CarType> carTypes { get; set; }

        //Это не свойство, поэтому в БД оно не попадёт.
        public List<BaseCondition> BaseConditions;
        public void PrepareConditionList()
        {
            BaseConditions = new List<BaseCondition>();
            BaseConditions.Concat(SAEViscosityConditions);
            BaseConditions.Concat(OilTypeConditions);
            BaseConditions.Concat(APIQualityConditions);
            BaseConditions.Sort();
        }
        public bool OilAcceptable(MotorOil oil)
        {
            return OilSuitsSpecificConds(oil, APIQualityConditions.ToArray())
                && OilSuitsSpecificConds(oil, OilTypeConditions.ToArray())
                && OilSuitsSpecificConds(oil, SAEViscosityConditions.ToArray());
        }
        bool OilSuitsSpecificConds(MotorOil oil, BaseCondition[] condsList)
        {
            if (condsList == null || condsList.Length==0)
                return true;
            //else...
            condsList.Order();
            bool isFirst = true, suits = new bool();
            foreach (BaseCondition cond in condsList)
            {
                if (isFirst)
                {
                    suits = !cond.isAllowing;
                    isFirst = false;
                }

                if (cond.isAllowing && cond.OilSuitsCondition(oil))
                    suits = true;
                else if (!cond.isAllowing && cond.OilSuitsCondition(oil))
                    suits = false;
            }
            return suits;
        }
    }
}
