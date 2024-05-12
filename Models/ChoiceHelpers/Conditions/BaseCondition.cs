namespace WebApplication1.Models.ChoiceHelpers.Conditions
{
    public class BaseCondition: BaseEntity
    {
        public ConditionsSet ConditionsSet { get; set; }
        public int ConditionsSetId {  get; set; }
        public int priority {  get; set; }
//Allow or Forbid? Какой тип условия: разрешительное или запретительное?
//Разрешительное(Allow): всё, попадающее под условие, можно использовать. иначе - запретить.
        public bool isAllowing { get; set; }
    }
}
