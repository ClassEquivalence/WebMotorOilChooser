namespace WebApplication1.Models.MotorOilStats
{
    public class SAEViscosity : BaseEntity
    {
        public int OnCold { get; set; }
        public int OnHot { get; set; }
        public MotorOil? MotorOil { get; set; }
        public int MotorOilId { get; set; }
        public override string ToString()
        {
            return OnCold + "w" + OnHot;
        }
        public override bool Equals(object? arg2)
        {
            SAEViscosity? other = arg2 as SAEViscosity;
            if (other == null)
                return base.Equals(arg2);

            else if (OnCold == other.OnCold && OnHot == other.OnHot)
                return true;
            else
                return false;
        }
    }
}
