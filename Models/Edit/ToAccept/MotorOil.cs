using WebApplication1.Models.MotorOilStats;
using System.Drawing.Imaging;
using System.Drawing;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models.Edit.ToAccept
{

    public class MotorOil
    {
        const int width = 320;
        const int height = 400;
        public string OilName { get; set; }
        public string Producer {  get; set; }
        public int APIClass {  get; set; }
        public decimal Volume {  get; set; }
        public IFormFile oilImgInput {  get; set; }
        public int SAEHot {  get; set; }
        public int SAECold {  get; set; }
        public int oilId {  get; set; }

        static private string ImgPath = "DataStorage/Images/OilPictures/";
        static private string ImgPrefix = "Img";
        static private string ImgExtension = ".jpeg";
        public string GetImgNamePath() { return "/" + ImgPath + ImgPrefix + oilId + ImgExtension; }

        public Models.MotorOil toMotorOil(ApplicationContext db)
        {
            List<APIQualityClass> apil = db.APIQualityClasses.ToList();
            Models.MotorOil? mo;

            if (oilId == -1)
            {
                mo = new();
                mo.Name = OilName;
                mo.Producer = Producer;
                mo.APIQualityClass = db.APIQualityClasses.Where(id => id.id == APIClass).ToList()[0];
                mo.SAEViscosity = new SAEViscosity();
                mo.SAEViscosity.OnHot = SAEHot;
                mo.SAEViscosity.OnCold = SAECold;
                mo.Volume = Volume;
                db.MotorOils.Add(mo);
            }
            else
            {
                mo = (from moils in db.MotorOils.Include(s => s.SAEViscosity)
                                where moils.id == oilId
                                select moils).ToList()[0];

                mo.Name = OilName;
                mo.Producer = Producer;
                mo.APIQualityClass = db.APIQualityClasses.Where(id => id.id == APIClass).ToList()[0];
                mo.SAEViscosity.OnHot = SAEHot;
                mo.SAEViscosity.OnCold = SAECold;
                mo.Volume = Volume;
            }
            db.SaveChanges();
            return mo;
        }
    }
}
