namespace WebApplication1.Models.ChoiceHelpers.Conditions
{
    public class SAEViscosityCondition: BaseCondition
    {
        public int minCold { get; set; }
        public int maxCold { get; set; }
        public int minHot { get; set; }
        public int maxHot { get; set; }
        public override bool OilSuitsCondition(MotorOil oil)
        {
            if (oil.SAEViscosity.OnCold >= minCold && oil.SAEViscosity.OnCold <= maxCold
                && oil.SAEViscosity.OnHot >= minHot && oil.SAEViscosity.OnHot <= maxHot)
                return true;
            else
                return false;
        }
    }
}
