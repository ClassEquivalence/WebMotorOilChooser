using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using WebApplication1.Models;
using WebApplication1.Models.ChoiceHelpers;
using WebApplication1.Models.Edit.ToAccept.ListUnits;
using WebApplication1.Models.Edit.ToRender;
using WebApplication1.Models.Edit.ToRender.ListUnits;
using WebApplication1.Models.ToRender;
using WebApplication1.Models.Users;
using WebApplication1.Services;
using WebApplication1.Services.Auth;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Formats.Asn1.AsnWriter;

namespace WebApplication1.Controllers
{
    [RequestSizeLimit(100 * 1024 * 1024)]
    public class EditController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ApplicationContext db;
        protected UsersService _us;
        protected PasswordHasher _ph;
        public EditController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            db = context;
            _ph = new();
            _us = new(_ph, db);
            Company.initUsers(db);
        }
        public IActionResult OilList()
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditOils)
                return BadRequest();

            var OilList = new OilList(db);
            return View(OilList);
        }
        public IActionResult EditOilList(int? delete, int? edit, int? add)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditOils)
                return BadRequest();

            _logger.LogInformation("LoggerAtReady! ");
            if (Request.Query.ContainsKey("delete"))
            {
                _logger.LogInformation("delete is...");
                var oil = db.MotorOils.Where(id => id.id == delete).ToList()[0];
                db.MotorOils.Remove(oil);
                db.SaveChanges();
                return RedirectToAction(nameof(OilList));
            }
            else if (Request.Query.ContainsKey("edit"))
            {
                _logger.LogInformation("Edit is " + edit);
                return RedirectToAction(nameof(EditOil), new { id = edit });
            }
            else
            {
                _logger.LogInformation("add is...");
                return RedirectToAction(nameof(EditOil), new { id = -1 });
            }
        }
        public IActionResult EditOil(int id)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditOils)
                return BadRequest();

            if (id != -1)
            {
                var oil = db.MotorOils.Include(qc => qc.APIQualityClass).Include(sae=>sae.SAEViscosity).Where(mo => mo.id == id).ToList()[0];
                var dto = new EditOil(db, oil, true);
                return View(dto);
            }
            else
            {
                MotorOil oil = new();
                var dto = new EditOil(db, oil, false);
                return View(dto);
            }
        }
        public IActionResult EditedAddedOil(Models.Edit.ToAccept.MotorOil motorOil)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditOils)
                return BadRequest();

            IFormFile f = motorOil.oilImgInput;
            var mo = motorOil.toMotorOil(db);
            mo.SaveImg(f);
            return View();
        }
        public IActionResult OilMerchList()
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u==null || !u.Role.Permission.CanEditMerch)
                return BadRequest();

            MerchList model = new MerchList(db);
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateMerch()
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditMerch)
                return BadRequest();
            //post, put, delete
            Models.Edit.ToRender.ListUnits.Merch merch = new(db);
            return PartialView(merch);
        }
        [HttpPut]
        public IActionResult PutMerch(Models.Edit.ToAccept.ListUnits.Merch merch)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditMerch)
                return BadRequest();

            MotorOilMerch m = db.MotorOilMerches.Where(m => m.id == merch.merchId).ToList()[0];
            //post, put, delete
            merch.ToMotorOilMerch(m);
            db.SaveChanges();
            return NoContent();
        }
        [HttpDelete]
        public IActionResult DeleteMerch(int id)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditMerch)
                return BadRequest();
            //post, put, delete
            MotorOilMerch m = db.MotorOilMerches.Where(m => m.id == id).ToList()[0];
            db.Remove(m);
            db.SaveChanges();
            return NoContent();
        }
        
        public IActionResult CompanyList()
        {
            var cl = new CompanyList(db);
            return View(cl);
        }
        public IActionResult AddCompany()
        {
            Company company = new();
            company.Owner = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            db.Companies.Add(company);
            db.SaveChanges();
            Company.initUsers(db);
            return PartialView("Views/Edit/CompanyPartial.cshtml", company);
        }
        public IActionResult UpdCompany(Company company)
        {
            var comp = db.Companies.Where(c => c.id == company.id).ToList()[0];
            comp.Name = company.Name;
            comp.OwnerId = company.OwnerId;
            db.SaveChanges();
            return NoContent();
        }
        public IActionResult DelCompany(int id)
        {
            var comp = db.Companies.Where(c => c.id == id).ToList()[0];
            db.Companies.Remove(comp);
            db.SaveChanges();
            return NoContent();
        }
        public IActionResult AddStore(int id)
        {
            Store store = new();
            store.CompanyId = id;
            db.Stores.Add(store);
            db.SaveChanges();
            Company.initUsers(db);
            return PartialView("Views/Edit/StorePartial.cshtml", store);
        }
        public IActionResult UpdStore(Store store)
        {
            var st = db.Stores.Where(c => c.id == store.id).ToList()[0];
            st.Adress = store.Adress;
            //st.CompanyId = store.CompanyId;
            db.SaveChanges();
            return NoContent();
        }
        public IActionResult DelStore(int id)
        {
            var st = db.Stores.Where(c => c.id == id).ToList()[0];
            db.Stores.Remove(st);
            db.SaveChanges();
            return NoContent();
        }

        public IActionResult UserList()
        {
            var users = db.Users.ToList();
            Models.Users.User.initRoles(db);
            return View(users);
        }

        public IActionResult UpdateUser(User user)
        {
            var u = db.Users.Where(u => u.id == user.id).ToList()[0];
            u.UserName = user.UserName;
            u.Login = user.Login;
            u.RoleId = user.RoleId;
            if(Request.Form.Keys.Contains("Password"))
                u.PasswordHash = _ph.Generate(Request.Form["Password"]);
            db.SaveChanges();
            return RedirectToAction("UserList");
        }

        public IActionResult CarList() 
        {
            var carList = db.CarTypes.ToList();
            CarType.initConditionSets(db);
            return View(carList);
        }
        public IActionResult AddCar()
        {
            CarType car = new();
            CarType.initConditionSets(db);
            car.conditionsSet = CarType.conditionsSets[0];
            db.CarTypes.Add(car);
            db.SaveChanges();
            return PartialView("CarPartial", car);
        }
        public IActionResult UpdCar(CarType car)
        {
            var dbcar = db.CarTypes.Where(c => c.id == car.id).ToList()[0];
            dbcar.Name = car.Name;
            dbcar.conditionsSetId = car.conditionsSetId;
            db.SaveChanges();
            return NoContent();
        }
        public IActionResult DelCar(int id)
        {
            var dbcar = db.CarTypes.Where(c => c.id == id).ToList()[0];
            db.CarTypes.Remove(dbcar);
            db.SaveChanges();
            return NoContent();
        }

        public IActionResult ConditionsList()
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditOils)
                return BadRequest();

            ConditionsList cl = new(db);
            return View(cl);
        }

        public IActionResult addCondSet()
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditOils)
                return BadRequest();

            CondSet cs = new(db);
            return PartialView("Views/Edit/CondPartials/CondSet.cshtml", cs);
        }

        public IActionResult addOilCond(int id)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditOils)
                return BadRequest();

            OilCond oc = new(db, id);
            return PartialView("Views/Edit/CondPartials/addOilCond.cshtml", oc);
        }
        public IActionResult addSAECond(int id)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditOils)
                return BadRequest();

            SAECond sc = new(db, id);
            return PartialView("Views/Edit/CondPartials/addSAECond.cshtml", sc);
        }
        public IActionResult addAPICond(int id)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditOils)
                return BadRequest();

            APICond ac = new(db, id);
            return PartialView("Views/Edit/CondPartials/addAPICond.cshtml", ac);
        }

        public IActionResult putOilCond(OilCondPUT ocp)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditOils)
                return BadRequest();

            var cond = db.OilTypeConditions.Where(otc => otc.id == ocp.condId)
                .ToList()[0];
            cond.priority = ocp.priorityOil;
            cond.MotorOilId = ocp.MotorOil;
            cond.isAllowing = ocp.isAllowingOil;
            db.SaveChanges();
            return NoContent();
        }
        public IActionResult putSAECond(SAECondPUT scp)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditOils)
                return BadRequest();

            var cond = db.SAEViscosityConditions.Where(svc => svc.id == scp.condId)
                .ToList()[0];
            cond.priority = scp.prioritySAE;
            cond.isAllowing = scp.isAllowingSAE;
            cond.minCold = scp.onColdLowerBorderSAE;
            cond.maxCold = scp.onColdHigherBorderSAE;
            cond.minHot = scp.onHotLowerBorderSAE;
            cond.maxHot = scp.onHotHigherBorderSAE;
            db.SaveChanges();
            return NoContent();
        }
        public IActionResult putAPICond(APICondPUT acp)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditOils)
                return BadRequest();

            var cond = db.APIQualityConditions.Where(aqc => aqc.id == acp.condId)
                .ToList()[0];
            cond.priority = acp.priorityAPI;
            cond.isAllowing = acp.isAllowingAPI;
            cond.MinAPIQualityClassId = acp.MinAPIClass;
            db.SaveChanges();
            return NoContent();
        }


        public IActionResult delCondSet(int id)
        {
            var cs = db.ConditionsSets.Where(cs => cs.id == id).ToList()[0];
            db.ConditionsSets.Remove(cs);
            db.SaveChanges();
            return NoContent();
        }
        public IActionResult delOilCond(int id)
        {
            var cs = db.OilTypeConditions.Where(cs => cs.id == id).ToList()[0];
            db.OilTypeConditions.Remove(cs);
            db.SaveChanges();
            return NoContent();
        }
        public IActionResult delSAECond(int id)
        {
            var cs = db.SAEViscosityConditions.
                Where(cs => cs.id == id).ToList()[0];
            db.SAEViscosityConditions.Remove(cs);
            db.SaveChanges();
            return NoContent();
        }
        public IActionResult delAPICond(int id)
        {
            var cs = db.APIQualityConditions.
                Where(cs => cs.id == id).ToList()[0];
            db.APIQualityConditions.Remove(cs);
            db.SaveChanges();
            return NoContent();
        }
    }
}
