using System.Diagnostics.Contracts;

namespace WebApplication1.Models.MotorOilStats
{
    public class APIQualityClass : BaseEntity
    {
        public int QualityValue {  get; set; }
        public string? Name { get; set; }
        public override string? ToString()
        {
            return Name;
        }

        public static bool operator >= (APIQualityClass a, APIQualityClass b)
        {
            return a.QualityValue >= b.QualityValue ? true : false;
        }

        public static bool operator <= (APIQualityClass a, APIQualityClass b)
        {
            return a.QualityValue <= b.QualityValue ? true : false;
        }
    }
}
