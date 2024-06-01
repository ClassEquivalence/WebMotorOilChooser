using WebApplication1.Models.ChoiceHelpers;

namespace WebApplication1.Models.Edit.ToRender
{
    public class CarList
    {
        public List<ConditionsSet> Conditions { get; set; }
        public List<CarType> carTypes { get; set; }
        public CarList(ApplicationContext db) 
        { 
            Conditions = db.ConditionsSets.ToList();
            carTypes = db.CarTypes.ToList();
        }
    }
}
