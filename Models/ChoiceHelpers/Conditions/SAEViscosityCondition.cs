namespace WebApplication1.Models.ChoiceHelpers.Conditions
{
    public class SAEViscosityCondition: BaseCondition
    {
        public int minCold { get; set; }
        public int maxCold { get; set; }
        public int minHot { get; set; }
        public int maxHot { get; set; }
    }
}
