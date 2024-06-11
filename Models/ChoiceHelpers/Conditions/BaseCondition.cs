namespace WebApplication1.Models.ChoiceHelpers.Conditions
{
    public abstract class BaseCondition: BaseEntity, IComparable
    {
        public ConditionsSet ConditionsSet { get; set; }
        public int ConditionsSetId {  get; set; }
        public int priority {  get; set; }
//Allow or Forbid? Какой тип условия: разрешительное или запретительное?
//Разрешительное(Allow): всё, попадающее под условие, можно использовать. иначе - запретить.
        public bool isAllowing { get; set; }

        public abstract bool OilSuitsCondition(MotorOil oil);

        public int CompareTo(object? other)
        {
            if(other == null) return 0;
            else if (other is BaseCondition)
            {
                BaseCondition _other = other as BaseCondition;
                return priority.CompareTo(_other.priority);
            }
            else
                return 0;
        }
        public BaseCondition()
        {
            priority = 1;
            isAllowing = true;
        }
    }
}
