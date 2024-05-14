namespace WebApplication1.Models.ChoiceHelpers
{
    public class CarType: BaseEntity
    {
        ConditionsSet conditionsSet {  get; set; }
        public int conditionsSetId {  get; set; }
        public string Name {  get; set; }
        MotorType? motorType { get; set; }
        public int? motorTypeId { get; set; }
    }
}
