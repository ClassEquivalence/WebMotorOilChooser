using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Models.ToRender;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ApplicationContext db;
        OilsAndFilters OilsAndFilters;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            db = context;
            OilsAndFilters = new OilsAndFilters();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Test()
        {
            OilsAndFilters.PrepareData(db);
            return View(OilsAndFilters);
        }

        [HttpPost]
        public async Task<IActionResult> TForm(MotorOil motoroil)
        {
            db.MotorOils.Add(motoroil);
            await db.SaveChangesAsync();
            return RedirectToAction("Test");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Ajax запрос
        [HttpPost]
        public IActionResult Filters(Models.FormsData.OilFiltersFormData FormData)
        {
            //_logger.LogInformation(FormData.Name);
            /*
            //Models.FormsData.OilFiltersFormData FormData = new Models.FormsData.OilFiltersFormData(Request.Form);
            _logger.LogInformation("sent");
            foreach (string key in Request.Form.Keys)
            {
                _logger.LogInformation("Key: "+ key + " Type: " + Request.Form[key].GetType() + " Content: " + Request.Form[key]);
            }
            */
            OilsAndFilters.PrepareData(db, FormData);
            //_logger.LogInformation(FormData.ToString());
            return PartialView(OilsAndFilters);
        }

        public IActionResult OilDetails(int id)
        {
            SpecificOilMerch renderModel = new SpecificOilMerch();
            if (renderModel.FormData(db, id))
                return View(renderModel);
            else
                return new NotFoundResult();
        }
    }
}
