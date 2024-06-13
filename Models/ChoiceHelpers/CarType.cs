namespace WebApplication1.Models.ChoiceHelpers
{
    public class CarType: BaseEntity
    {
        public ConditionsSet? conditionsSet {  get; set; }
        public int? conditionsSetId {  get; set; }
        public string? Name {  get; set; }

        public static List<ConditionsSet>? conditionsSets;
        public static void initConditionSets(ApplicationContext db)
        {
            conditionsSets = db.ConditionsSets.ToList();
        }
        //public MotorType? motorType { get; set; }
        //public int? motorTypeId { get; set; }
    }
}
