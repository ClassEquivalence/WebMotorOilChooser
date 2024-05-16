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
        public List<MotorType> motorTypes { get; set; }

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
            bool suits = true;
            foreach (BaseCondition c in BaseConditions)
            {
                if (c.isAllowing && c.isOilAcceptable(oil))
                    suits = true;
                else if (!c.isAllowing && !c.isOilAcceptable(oil))
                    suits = false;
            }
            return suits;
        }
    }
}
