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
    }
}
