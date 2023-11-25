using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;

namespace DoAn.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        QLPizzaDataContext data = new QLPizzaDataContext();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Table()
        {
            var listTable = data.Bans.ToList();
            return View(listTable);
        }

        // product
        public ActionResult Product()
		{
            var listPro = data.SanPhams.ToList();
            return PartialView(listPro);
		}


        //Ton kho
        [HttpGet]
        public ActionResult Inventory()
		{
            var listInven = data.TonKhos.ToList();
            return PartialView("Inventory",listInven);
		}

        public ActionResult Total()
		{
            var sl = (int)data.SanPhams.Sum(i => i.soLuong);
            var g = (decimal)data.SanPhams.FirstOrDefault().gia;
            var total = sl * g;
            string format = FormatDecimal(total);
            ViewBag.Total = format;
            return PartialView("Total");
		}

        private string FormatDecimal(decimal value)
        {
            return value.ToString("0.##");
        }
    }
}