using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.ToRender;

namespace WebApplication1.Models.Edit.ToRender
{
    //merch, oils, apis, saes, companies, cars
    //motors, conds
    public class MerchList : RenderBase
    {
        public List<MotorOil> MotorOils { get; set;}
        public List<Store> Stores { get; set;}
        public List<MotorOilMerch> MotorOilMerches { get; set;}
        public MerchList(ApplicationContext db) 
        {
            MotorOilMerches = db.MotorOilMerches.Include(s => s.Store).ThenInclude(c => c.Company).Include(mo => mo.MotorOil).ToList();
            Stores = db.Stores.Include(c => c.Company).ToList();
            MotorOils = db.MotorOils.Include(sa=>sa.SAEViscosity).ToList();
        }
        public MerchList(ApplicationContext db, int OwnerId)
        {
            MotorOilMerches = db.MotorOilMerches.Include(s => s.Store).ThenInclude(c => c.Company).Include(mo => mo.MotorOil).
                Where(m=>m.Store.Company.id==OwnerId).ToList();
            Stores = db.Stores.Include(c => c.Company).
                Where(s=>s.Company.id==OwnerId).ToList();
            MotorOils = db.MotorOils.Include(sa => sa.SAEViscosity).
                Where(mo=>mo.OwnerCompanyId==OwnerId).ToList();
        }
    }
}
