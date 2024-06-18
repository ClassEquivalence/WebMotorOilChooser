using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.ChoiceHelpers;
using WebApplication1.Models.Edit.ToRender;
using WebApplication1.Models.Edit.ToRender.ListUnits;
using WebApplication1.Models.Users;
using WebApplication1.Services;
using WebApplication1.Services.Auth;

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

            if (db.MotorOils.ToList().Count <= 0)
                return RedirectToAction(nameof(OilList));
            else if (db.Stores.ToList().Count <= 0)
                return RedirectToAction(nameof(CompanyList));

            MerchList model = new MerchList(db);
            return View(model);
        }
        [HttpGet]
        public object CreateMerch()
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditMerch)
                return BadRequest();
            //post, put, delete
            if (db.MotorOils.ToList().Count <= 0)
                return "ДОБАВЬТЕ МАСЛО!";
            else if (db.Stores.ToList().Count <= 0)
                return "ДОБАВЬТЕ ТОРГОВУЮ ТОЧКУ!";

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
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditCompanies)
                return BadRequest();

            var cl = new CompanyList(db);
            return View(cl);
        }
        public IActionResult AddCompany()
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditCompanies)
                return BadRequest();

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
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditCompanies)
                return BadRequest();

            var comp = db.Companies.Where(c => c.id == company.id).ToList()[0];
            comp.Name = company.Name;
            comp.OwnerId = company.OwnerId;
            var User = db.Users.Where(u => u.id == company.OwnerId).ToList()[0];
            var role = db.Role.Where(r => r.Name == "CompanyOwner").ToList()[0];
            User.Role = role;
            db.SaveChanges();
            return NoContent();
        }
        public IActionResult DelCompany(int id)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditCompanies)
                return BadRequest();

            var comp = db.Companies.Where(c => c.id == id).ToList()[0];
            db.Companies.Remove(comp);
            db.SaveChanges();
            return NoContent();
        }
        public IActionResult AddStore(int id)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditStores)
                return BadRequest();

            Store store = new();
            store.CompanyId = id;
            db.Stores.Add(store);
            db.SaveChanges();
            Company.initUsers(db);
            return PartialView("Views/Edit/StorePartial.cshtml", store);
        }
        public IActionResult UpdStore(Store store)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditStores)
                return BadRequest();

            var st = db.Stores.Where(c => c.id == store.id).ToList()[0];
            st.Adress = store.Adress;
            //st.CompanyId = store.CompanyId;
            db.SaveChanges();
            return NoContent();
        }
        public IActionResult DelStore(int id)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditStores)
                return BadRequest();

            var st = db.Stores.Where(c => c.id == id).ToList()[0];
            db.Stores.Remove(st);
            db.SaveChanges();
            return NoContent();
        }

        public IActionResult UserList()
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditUsers)
                return BadRequest();

            var users = db.Users.ToList();
            Models.Users.User.initRoles(db);
            return View(users);
        }

        public IActionResult UpdateUser(User user)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditUsers)
                return BadRequest();


            var User = db.Users.Include(u=>u.Role).Where(u => u.id == user.id).ToList()[0];
            User.UserName = user.UserName;
            User.Login = user.Login;
            var urole = db.Role.Where(r => r.id == user.RoleId).ToList()[0];
            if(User.Role.Name!="CompanyOwner" && urole.Name == "CompanyOwner")
            {
                Company comp = new();
                comp.OwnerId = User.id;
                db.Companies.Add(comp);
            }
            User.RoleId = user.RoleId;
            if(Request.Form.Keys.Contains("Password"))
                User.PasswordHash = _ph.Generate(Request.Form["Password"]);
            db.SaveChanges();
            return RedirectToAction("UserList");
        }

        public IActionResult CarList() 
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditOils)
                return BadRequest();

            var carList = db.CarTypes.ToList();
            CarType.initConditionSets(db);
            return View(carList);
        }
        public IActionResult AddCar()
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditOils)
                return BadRequest();

            CarType car = new();
            CarType.initConditionSets(db);
            if (CarType.conditionsSets.Count <= 0)
                return RedirectToAction("ConditionsList");
            car.conditionsSet = CarType.conditionsSets[0];
            db.CarTypes.Add(car);
            db.SaveChanges();
            return PartialView("CarPartial", car);
        }
        public IActionResult UpdCar(CarType car)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditOils)
                return BadRequest();

            var dbcar = db.CarTypes.Where(c => c.id == car.id).ToList()[0];
            dbcar.Name = car.Name;
            dbcar.conditionsSetId = car.conditionsSetId;
            db.SaveChanges();
            return NoContent();
        }
        public IActionResult DelCar(int id)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditOils)
                return BadRequest();

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
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditOils)
                return BadRequest();

            var cs = db.ConditionsSets.Include(cs=>cs.carTypes).Where(cs => cs.id == id).ToList()[0];
            db.ConditionsSets.Remove(cs);
            foreach(var ct in cs.carTypes)
            {
                db.CarTypes.Remove(ct);
            }
            db.SaveChanges();
            return NoContent();
        }
        public IActionResult delOilCond(int id)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditOils)
                return BadRequest();

            var cs = db.OilTypeConditions.Where(cs => cs.id == id).ToList()[0];
            db.OilTypeConditions.Remove(cs);
            db.SaveChanges();
            return NoContent();
        }
        public IActionResult delSAECond(int id)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditOils)
                return BadRequest();

            var cs = db.SAEViscosityConditions.
                Where(cs => cs.id == id).ToList()[0];
            db.SAEViscosityConditions.Remove(cs);
            db.SaveChanges();
            return NoContent();
        }
        public IActionResult delAPICond(int id)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.CanEditOils)
                return BadRequest();

            var cs = db.APIQualityConditions.
                Where(cs => cs.id == id).ToList()[0];
            db.APIQualityConditions.Remove(cs);
            db.SaveChanges();
            return NoContent();
        }










        //Владелец компании может создать и редактировать компанию, свои масла, торговые точки и товары на продажу.
        public IActionResult coCompanyView()
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.OwnsCompany)
                return BadRequest();

            var companies = db.Companies.Where(c => c.OwnerId == u.id).
                Include(c=>c.Stores).ToList();
            Company comp;
            if (companies.Count > 0)
            {
                comp = companies[0];
                return View(comp);
            }
            else
            {
                return BadRequest();
            }
        }
        public IActionResult coUpdCompany(string Name)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.OwnsCompany)
                return BadRequest();

            var companies = db.Companies.Where(c => c.OwnerId == u.id).
                Include(c => c.Stores).ToList();
            Company comp;
            if (companies.Count > 0)
            {
                comp = companies[0];
                comp.Name = Name;
                db.SaveChanges();
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
        public IActionResult coAddStore()
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.OwnsCompany)
                return BadRequest();

            var companies = db.Companies.Where(c => c.OwnerId == u.id).
                Include(c => c.Stores).ToList();
            Company comp;
            if (companies.Count > 0)
            {
                comp = companies[0];
                Store store = new();
                store.Company = comp;
                db.Stores.Add(store);
                db.SaveChanges();
                return PartialView("coStorePartial", store);
            }
            else
            {
                return BadRequest();
            }
        }
        public IActionResult coDelStore(int id)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.OwnsCompany)
                return BadRequest();

            var stores = db.Stores.Where(s=>s.id==id).
                Include(s => s.Company).ToList();
            if (stores.Count > 0)
            {
                var store = stores[0];
                if (store.Company.OwnerId == u.id)
                {
                    db.Stores.Remove(store);
                    db.SaveChanges();
                    return NoContent();
                }
                else return BadRequest();
            }
            else return BadRequest();
        }
        public IActionResult coUpdStore(Store store)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.OwnsCompany)
                return BadRequest();

            var stores = db.Stores.Where(s => s.id == store.id).
                Include(s => s.Company).ToList();
            if (stores.Count > 0)
            {
                var st = stores[0];
                if (st.Company.OwnerId == u.id)
                {
                    st.Adress = store.Adress;
                    db.SaveChanges();
                    return NoContent();
                }
                else return BadRequest();
            }
            else return BadRequest();

        }


        public IActionResult coOils() 
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.OwnsCompany)
                return BadRequest();
            var companies = db.Companies.Where(c => c.OwnerId == u.id).
                Include(c => c.Stores).ToList();
            Company comp;
            int compId;
            if (companies.Count > 0)
            {
                comp = companies[0];
                compId = comp.id;
                db.SaveChanges();
            }
            else
                return BadRequest();
            OilList oilList = new(db, compId);
            return View("coOilList", oilList);
        }

        public IActionResult coUpdOil(int edit)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.OwnsCompany)
                return BadRequest();

            return RedirectToAction(nameof(coEditOil), new { id = edit });
        }
        public IActionResult coDelOil(int delete)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.OwnsCompany)
                return BadRequest();

            var oil = db.MotorOils.Where(id => id.id == delete).ToList()[0];
            db.MotorOils.Remove(oil);
            db.SaveChanges();
            return RedirectToAction(nameof(coOils));
        }
        public IActionResult coAddOil()
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.OwnsCompany)
                return BadRequest();

            return RedirectToAction(nameof(coEditOil), new { id = -1 });
        }
        public IActionResult coEditOil(int id)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.OwnsCompany)
                return BadRequest();

            var uComp = db.Companies.Where(c => c.OwnerId == u.id).ToList()[0];

            if (id != -1)
            {
                var oil = db.MotorOils.Include(qc => qc.APIQualityClass).Include(sae => sae.SAEViscosity).Where(mo => mo.id == id).ToList()[0];

                if (uComp.id != oil.OwnerCompanyId)
                    return BadRequest();

                var dto = new EditOil(db, oil, true);
                return View(dto);
            }
            else
            {
                MotorOil oil = new();
                oil.APIQualityClass = db.APIQualityClasses.ToList()[0];
                oil.OwnerCompanyId = uComp.id;
                var dto = new EditOil(db, oil, false);
                return View(dto);
            }
        }

        public IActionResult coEditedAddedOil(Models.Edit.ToAccept.MotorOil motorOil)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.OwnsCompany)
                return BadRequest();

            IFormFile f = motorOil.oilImgInput;
            var mo = motorOil.toMotorOil(db);
            var comp = db.Companies.Where(c => c.OwnerId == u.id).ToList()[0];
            mo.OwnerCompanyId = comp.id;
            mo.SaveImg(f);
            db.SaveChanges();
            return RedirectToAction(nameof(coOils));
        }

        public IActionResult coMerches()
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.OwnsCompany)
                return BadRequest();

            var comp = db.Companies.Where(c => c.OwnerId == u.id).ToList()[0];
            if (db.MotorOils.Where(mo => mo.OwnerCompanyId == comp.id).ToList().Count <= 0)
                return RedirectToAction(nameof(coOils));
            else if (db.Stores.Where(s => s.CompanyId == comp.id).ToList().Count <= 0)
                return RedirectToAction(nameof(coCompanyView));

            MerchList merchList = new(db, comp.id);
            return View("coMerchList", merchList);
        }

        public object coCreateMerch()
        {

            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.OwnsCompany)
                return BadRequest();
            //post, put, delete

            var comp = db.Companies.Where(c=>c.OwnerId == u.id).ToList()[0];

            if (db.MotorOils.Where(mo => mo.OwnerCompanyId ==comp.id).ToList().Count <= 0)
                return "ДОБАВЬТЕ МОТОРНОЕ МАСЛО!";
            else if (db.Stores.Where(s=>s.CompanyId==comp.id).ToList().Count <= 0)
                return "ДОБАВЬТЕ ТОРГОВУЮ ТОЧКУ!";

            Models.Edit.ToRender.ListUnits.Merch merch = new(comp.id, db);
            return PartialView("CreateMerch", merch);

            //return NoContent();
        }


        public IActionResult coDeleteMerch(int id)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.OwnsCompany)
                return BadRequest();
            //post, put, delete
            MotorOilMerch m = db.MotorOilMerches.Where(m => m.id == id).ToList()[0];
            var comp = db.Companies.Where(c => c.OwnerId == u.id).ToList()[0];
            var store = db.Stores.Where(s => s.id==m.StoreId).ToList()[0];
            if (store.CompanyId != comp.id)
                return BadRequest();
            else
            {
                db.Remove(m);
                db.SaveChanges();
                return NoContent();
            }
        }
        public IActionResult coPutMerch(Models.Edit.ToAccept.ListUnits.Merch merch)
        {
            var u = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            if (u == null || !u.Role.Permission.OwnsCompany)
                return BadRequest();

            MotorOilMerch m = db.MotorOilMerches.Where(m => m.id == merch.merchId).ToList()[0];
            //post, put, delete
            var comp = db.Companies.Where(c => c.OwnerId == u.id).ToList()[0];
            var store = db.Stores.Where(s => s.id == m.StoreId).ToList()[0];
            if (store.CompanyId != comp.id)
                return BadRequest();
            else
            {
                merch.ToMotorOilMerch(m);
                db.SaveChanges();
                return NoContent();
            }
        }

        public IActionResult AdminPanel()
        {
            return View();
        }
        public IActionResult CompanyControlPanel()
        {
            return View();
        }
    }
}
