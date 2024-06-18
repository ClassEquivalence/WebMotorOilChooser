using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Models.ToRender;
using WebApplication1.Services;
using WebApplication1.Services.Auth;
using WebApplication1.TestUtils;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ApplicationContext db;
        OilsAndFilters OilsAndFilters;
        ChoosebyCar choosebyCar;
        UsersService _us;
        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            db = context;
            OilsAndFilters = new OilsAndFilters();
            choosebyCar = new ChoosebyCar();
            _us = new(new PasswordHasher(), db);
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(OilChooseCar));
        }
        /*
        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult Test()
        {
            OilsAndFilters.PrepareData(db);
            OilsAndFilters.User = null;
            return View(OilsAndFilters);
        }
        */

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Ajax запрос
        [HttpPost]
        public IActionResult Filters(Models.FormsData.OilFiltersFormData FormData)
        {
            if(Request.Form.Keys.Contains("Car"))
            {
                choosebyCar.PrepareData(db, FormData, Request.Form["Car"]);
                return PartialView(choosebyCar);
            }
            else
            {
                OilsAndFilters.PrepareData(db, FormData);
                //_logger.LogInformation(FormData.ToString());
                return PartialView(OilsAndFilters);
            }
        }

        public IActionResult OilDetails(int id)
        {
            SpecificOilMerch renderModel = new SpecificOilMerch();
            if (renderModel.FormData(db, id))
                return View(renderModel);
            else
                return new NotFoundResult();
        }
        public IActionResult OilChooseCar()
        {
            choosebyCar.PrepareData(db);
            return View(choosebyCar);
        }
        
        public IActionResult initSomeTestDb()
        {
            TestDBMaker tdb = new TestDBMaker(_us);
            tdb.db = db;
            tdb.initTestDb();
            return new NoContentResult();
        }
        

        public IActionResult UserNameShow()
        {
            RenderBase rb = new();
            rb.User = _us.GetUserBySessionId(Request.
                    Cookies[Models.Users.User.SessionIdCookieName]);
            return PartialView(rb);
        }

        public IActionResult Guide()
        {
            return View();
        }
    }
}
