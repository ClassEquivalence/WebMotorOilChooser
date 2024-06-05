using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using WebApplication1.Models;
using WebApplication1.Models.Edit.ToAccept.ListUnits;
using WebApplication1.Models.Edit.ToRender;
using WebApplication1.Models.ToRender;
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
        public EditController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            db = context;
            _us = new(new PasswordHasher(), db);
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
        public IActionResult PutMerch(Merch merch)
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

        public IActionResult StoreList()
        {
            return View();
        }
        public IActionResult CompanyList()
        {
            return View();
        }
        public IActionResult UsersList()
        {
            return View();
        }
        public IActionResult CarMotorList() 
        {
            return View();
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
    }
}
