using WebApplication1.Models.MotorOilStats;
using System.Drawing;

namespace WebApplication1.Models
{
    public class MotorOil : BaseEntity
    {
        public string? Name { get; set; }
        public string? Producer { get; set; }
        //класс качества, по классификации API.
        public APIQualityClass? APIQualityClass { get; set; }
        public int APIQualityClassId { get; set; }
        public SAEViscosity? SAEViscosity { get; set; } //вязкость
        public decimal Volume { get; set; } //объём масла

        static private string ImgPath = "DataStorage/Images/OilPictures/";
        static private string ImgPrefix = "Img";
        static private string ImgExtension = ".jpeg";
        static private string wwwrootpath = "C:/Visual Studio/WebApplication1/wwwroot";
        public string GetImgNamePath() { return "/" + ImgPath + ImgPrefix + id + ImgExtension; }
        public void SaveImg(IFormFile ImgFile)
        {
            var filestream = System.IO.File.Create(wwwrootpath + GetImgNamePath());
            ImgFile.CopyTo(filestream);
            filestream.Flush();
            filestream.Close();
        }
        public MotorOil()
        {
            APIQualityClass = new();
            SAEViscosity = new();
        }

        public override string ToString()
        {
            return Name + " " + SAEViscosity?.ToString() + " " + Volume +
                "(л) " + APIQualityClass?.Name;
        }
    }
}
