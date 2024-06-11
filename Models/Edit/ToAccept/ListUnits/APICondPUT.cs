using WebApplication1.Models.ChoiceHelpers.Conditions;
using WebApplication1.Models.MotorOilStats;

namespace WebApplication1.Models.Edit.ToRender.ListUnits
{
    public class APICondPUT
    {
        public int priorityAPI { get; set; }
        public bool isAllowingAPI { get; set; }
        public int MinAPIClass { get; set; }
        public int condId { get; set; }
    }
}
