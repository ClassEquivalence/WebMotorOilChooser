using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.FormsData;

namespace WebApplication1.Models.ToRender
{
    public class OilsAndFilters
    {
        /*
         Фильтры:
        По названию – соответствие строки
        По производителю – выбор нескольких галочек/поиск по списку
        По классу качества – выбор нескольких галочек
        По вязкости – выбор двух диапазонов/выбор галочек?
        По объёму – выбор 
        По компании-продавцу
        По цене
        По наличию(надо ли?)
        public DbSet<MotorOil> MotorOils { get; set; } = null!;
        public DbSet<Company> Companies { get; set; }
        public DbSet<MotorOilMerch> MotorOilMerches { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<MotorOilStats.SAEViscosity> SAEViscosities {get;set;}
        public DbSet<MotorOilStats.APIQualityClass> APIQualityClasses { get;set;}
         */
        public List<MotorOilMerch> MotorOilMerches { get; set; } = null!;
        //Название - по соответствию первых символов
        public List<string> Producers { get; set; } = null!;
        public List<MotorOilStats.APIQualityClass> APIQualityClasses { get; set; } = null!;
        public List<MotorOilStats.SAEViscosity> SAEViscosities { get; set; } = null!;
        //Объём - по заданию диапазона
        public List<Company> Companies { get; set; } = null!;
        //Цена-по заданию диапазона
        virtual public void PrepareData(ApplicationContext db)
        {
            List<MotorOil> MotorOils = new List<MotorOil>();
            MotorOilMerches = db.MotorOilMerches.Include(Mome => Mome.Store).ThenInclude(S => S.Company).Include(Mome => Mome.MotorOil).ThenInclude(Moil => Moil.APIQualityClass).
                Include(Mome => Mome.MotorOil).ThenInclude(Moil => Moil.SAEViscosity).ToList();
            Companies = new List<Company>();
            foreach (MotorOilMerch merch in MotorOilMerches)
            {
                if(!MotorOils.Contains(merch.MotorOil))
                    MotorOils.Add(merch.MotorOil);

                if (!Companies.Contains(merch.Store.Company))
                    Companies.Add(merch.Store.Company);
            }
            Producers = new List<string>();
            APIQualityClasses = new List<MotorOilStats.APIQualityClass>();
            SAEViscosities = new List<MotorOilStats.SAEViscosity>();
            foreach (MotorOil oil in MotorOils)
            {
                if(!Producers.Contains(oil.Producer))
                    Producers.Add(oil.Producer);

                if (!APIQualityClasses.Contains(oil.APIQualityClass))
                    APIQualityClasses.Add(oil.APIQualityClass);

                if (!SAEViscosities.Contains(oil.SAEViscosity))
                    SAEViscosities.Add(oil.SAEViscosity);
            }
        }
        virtual public void PrepareData(ApplicationContext db, OilFiltersFormData filters)
        {
            PrepareData(db);
            List<MotorOilMerch> newList = new List<MotorOilMerch>();
            MotorOil oil;
            foreach(MotorOilMerch merch in MotorOilMerches)
            {
                if(filters.OilSuitsFilter(merch))
                    newList.Add( merch );
            }
            MotorOilMerches = newList;
        }
    }
}
