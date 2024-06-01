﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using WebApplication1.Models;
using WebApplication1.Models.Edit.ToRender;

namespace WebApplication1.Controllers
{
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
            return View();
        }
        public IActionResult EditOil(int id)
        {
            if (id != -1)
            {
                var oil = db.MotorOils.Include(qc => qc.APIQualityClass).Where(mo => mo.id == id).ToList()[0];
                var dto = OilToEditOil(oil);
                dto.editOilState = "true";
                return View(dto);
            }
            else
            {
                MotorOil oil = new();
                var dto = OilToEditOil(oil);
                dto.editOilState = "false";
                return View(dto);
            }
        }
        public IActionResult EditedAddedOil(Models.Edit.ToAccept.MotorOil motorOil)
        {
            Bitmap bmp = motorOil.NormalizeImg();
            var mo = motorOil.toMotorOil(db);
            mo.AsyncSaveImg(bmp);
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

        protected EditOil OilToEditOil(MotorOil oil)
        {
            EditOil eo = new EditOil();
            eo.imgPath = oil.GetImgNamePath();
            eo.OilName = oil.Name;
            eo.Producer = oil.Producer;
            eo.QualityClass = oil.APIQualityClass;
            eo.QualityClasses = db.APIQualityClasses.ToList();
            eo.Volume = oil.Volume;
            eo.editOilState = "true";
            return eo;
        }
    }
}
