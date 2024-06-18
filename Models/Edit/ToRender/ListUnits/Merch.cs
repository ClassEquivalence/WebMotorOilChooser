using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.ToRender;

namespace WebApplication1.Models.Edit.ToRender.ListUnits
{
    public class Merch : RenderBase
    {
        public List<MotorOil> MotorOils { get; set; }
        public List<Store> Stores { get; set; }
        public MotorOilMerch Merchandise { get; set; }
        public Merch(ApplicationContext db, int id)
        {
            Stores = db.Stores.Include(c => c.Company).ToList();
            MotorOils = db.MotorOils.Include(sa => sa.SAEViscosity).ToList();
            Merchandise = db.MotorOilMerches.Where(me => me.id == id).ToList()[0];
        }
        public Merch(ApplicationContext db)
        {
            Stores = db.Stores.Include(c => c.Company).ToList();
            MotorOils = db.MotorOils.Include(sa => sa.SAEViscosity).ToList();
            Merchandise = new();
            Merchandise.Store = Stores[0];
            Merchandise.MotorOil = MotorOils[0];
            db.Add(Merchandise);
            db.SaveChanges();
        }

        public Merch(int companyId, ApplicationContext db)
        {
            Stores = db.Stores.Include(c => c.Company).
                Where(s=>s.CompanyId==companyId).ToList();
            Merchandise = new();
            Merchandise.Store = Stores[0];
            MotorOils = db.MotorOils.Include(sa => sa.SAEViscosity).
                Where(mo => mo.OwnerCompanyId == Merchandise.Store.CompanyId)
                .ToList();
            Merchandise.MotorOil = MotorOils[0];
            db.Add(Merchandise);
            db.SaveChanges();
        }
    }
}
