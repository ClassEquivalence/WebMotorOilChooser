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
        public string GetImgNamePath() { return "/" + ImgPath + ImgPrefix + id + ImgExtension; }
        void SaveStandartizedImg(Bitmap bmp)
        {
            bmp.Save(GetImgNamePath());
            bmp.Dispose();
        }
        public async void AsyncSaveImg(Bitmap bmp)
        {
            await Task.Run(()=>SaveStandartizedImg(bmp));
        }
    }
}
