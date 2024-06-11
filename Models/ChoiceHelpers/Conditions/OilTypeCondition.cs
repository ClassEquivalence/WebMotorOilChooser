namespace WebApplication1.Models.ChoiceHelpers.Conditions
{
    public class OilTypeCondition: BaseCondition
    {
        public MotorOil MotorOil { get; set; }
        public int MotorOilId { get; set; }
        public override bool OilSuitsCondition(MotorOil oil)
        {
            return oil == MotorOil ? true : false;
        }

        public OilTypeCondition(): base() { }
    }
}
