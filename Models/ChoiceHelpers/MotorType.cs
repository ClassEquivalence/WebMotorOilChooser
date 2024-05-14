namespace WebApplication1.Models.ChoiceHelpers
{
    public class MotorType: BaseEntity
    {
        public ConditionsSet conditionsSet { get; set; }
        public int conditionsSetId {  get; set; }
        public string Name { get; set; }
    }
}
