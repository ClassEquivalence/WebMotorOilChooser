using WebApplication1.Models.MotorOilStats;

namespace WebApplication1.Models.ChoiceHelpers.Conditions
{
    public class APIQualityCondition: BaseCondition
    {
        //минимально допустимый класс качества
        public APIQualityClass MinAPIQualityClass { get; set; }
    }
}
