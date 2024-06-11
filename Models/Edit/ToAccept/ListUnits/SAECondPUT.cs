
namespace WebApplication1.Models.Edit.ToRender.ListUnits
{
    public class SAECondPUT
    {
        public int prioritySAE { get; set; }
        public bool isAllowingSAE { get; set; }
        public int onColdLowerBorderSAE { get; set; }
        public int onColdHigherBorderSAE { get; set; }
        public int onHotLowerBorderSAE { get; set; }
        public int onHotHigherBorderSAE { get; set; }
        public int condId { get; set; }
    }
}
