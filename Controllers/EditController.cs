﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using WebApplication1.Models;
using WebApplication1.Models.Edit.ToRender;

namespace WebApplication1.Controllers
{
    [RequestSizeLimit(100 * 1024 * 1024)]
    public class EditController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ApplicationContext db;
        public EditController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            db = context;
        }
        public IActionResult OilList()
        {
            var OilList = new OilList(db);
            return View(OilList);
        }
        public IActionResult EditOilList(int? delete, int? edit, int? add)
        {
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
            IFormFile f = motorOil.oilImgInput;
            var mo = motorOil.toMotorOil(db);
            mo.SaveImg(f);
            return View();
        }
        public IActionResult OilMerchList()
        {
            MerchList model = new MerchList(db);
            return View(model);
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
        public IActionResult ConditionList()
        {
            return View();
        }
    }
}
