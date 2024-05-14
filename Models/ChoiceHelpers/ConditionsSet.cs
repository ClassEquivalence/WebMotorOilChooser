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
    }
}
