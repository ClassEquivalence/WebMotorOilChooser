using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.MotorOilStats;

namespace WebApplication1.Models.ToRender
{
    public class SpecificOilMerch: RenderBase
    {
        /*Отображаем:
 * Название масла MO integrated
 * Вязкость MO->SV
 * Объём MO integrated
 * Производителя MO integrated
 * Класс качества MO
 * Цену integrated
 * Продавца Store->Company 
 * Адрес Store 
 * Остаток integrated
 * 
 */
        public string? OilName { get; set; }
        public string? SAEViscosity { get; set; }
        public decimal? Volume { get; set; }
        public string? Producer {  get; set; }
        public string? APIClass {  get; set; }
        public double Price {  get; set; }
        public string? Company {  get; set; }
        public string? Address {  get; set; }
        public double StockCount {  get; set; }
        public string ImgPath {  get; set; }
        public bool FormData(ApplicationContext db, int oilId)
        {
            var oillist = (from oilMerch in db.MotorOilMerches.Include(mo => mo.MotorOil).ThenInclude(sv => sv.SAEViscosity)
                .Include(mo => mo.MotorOil).ThenInclude(qc => qc.APIQualityClass)
                .Include(st => st.Store).ThenInclude(co => co.Company).ToList()
                            where oilMerch.id == oilId
                            select oilMerch).ToList();

            if (oillist.Count != 1)
                return false;
            var oilmerch = oillist[0];

            var oil = oilmerch.MotorOil;
            OilName = oil.Name;
            SAEViscosity = oil.SAEViscosity.ToString();
            Volume = oil.Volume;
            Producer = oil.Producer;
            APIClass = oil.APIQualityClass.Name;
            Price = oilmerch.Price;
            Company = oilmerch.Store.Company.Name;
            Address = oilmerch.Store.Adress;
            StockCount = oilmerch.StockCount;
            ImgPath = oil.GetImgNamePath();
            return true;
        }
    }
}
