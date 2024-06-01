using WebApplication1.Models.MotorOilStats;
using System.Drawing.Imaging;
using System.Drawing;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models.Edit.ToAccept
{

    public class MotorOil
    {
        const int width = 300;
        const int height = 400;
        public string OilName { get; set; }
        public string Producer {  get; set; }
        public string APIClass {  get; set; }
        public decimal Volume {  get; set; }
        public IFormFile Image {  get; set; }
        public int SAEHot {  get; set; }
        public int SAECold {  get; set; }
        public int imgx {  get; set; }
        public int imgy {  get; set; }
        public int oilId {  get; set; }
        public double imgscale {  get; set; }

        static private string ImgPath = "DataStorage/Images/OilPictures/";
        static private string ImgPrefix = "Img";
        static private string ImgExtension = ".jpeg";
        public string GetImgNamePath() { return "/" + ImgPath + ImgPrefix + oilId + ImgExtension; }

        public Bitmap NormalizeImg()
        {
            int framex1, framey1, framewidth, frameheight;
            framex1 = (int)Math.Round(-imgx / imgscale);
            framey1 = (int)Math.Round(-imgy / imgscale);
            framewidth = (int)Math.Round(width / imgscale);
            frameheight = (int)Math.Round(height / imgscale);
            Bitmap bmp = new(Image.OpenReadStream());
            var bmp2 = bmp.Clone(new Rectangle(framex1, framey1, framewidth, frameheight), PixelFormat.DontCare);
            var bmp3 = new Bitmap(bmp2, width, height);
            bmp.Dispose();
            bmp2.Dispose();
            return bmp3;
        }
        public Models.MotorOil toMotorOil(ApplicationContext db)
        {
            List<APIQualityClass> apil = db.APIQualityClasses.ToList();
            Models.MotorOil? mo;

            if (oilId == -1)
            {
                mo = new();
                mo.Name = OilName;
                mo.Producer = Producer;
                foreach (var el in apil)
                {
                    if (el.Name == APIClass)
                        mo.APIQualityClass = el;
                }
                mo.SAEViscosity = new SAEViscosity();
                mo.SAEViscosity.OnHot = SAEHot;
                mo.SAEViscosity.OnCold = SAECold;
                mo.Volume = Volume;
                return mo;
            }
            else
            {
                List<Models.MotorOil> motorOils = db.MotorOils.Include(s=>s.SAEViscosity).ToList();
                mo = null;
                foreach(var el in motorOils)
                {
                    if (el.id == oilId)
                    {
                        mo = el;
                        break;
                    }
                }
                if (mo == null)
                    throw new Exception("Motor oil not found");
                mo.Name = OilName;
                mo.Producer = Producer;
                foreach (var el in apil)
                {
                    if (el.Name == APIClass)
                        mo.APIQualityClass = el;
                }
                mo.SAEViscosity.OnHot = SAEHot;
                mo.SAEViscosity.OnCold = SAECold;
                mo.Volume = Volume;
                return mo;
            }
        }
    }
}
