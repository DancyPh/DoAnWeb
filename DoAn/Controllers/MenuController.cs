using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;

namespace DoAn.Controllers
{
    public class MenuController : Controller
    {
        QLPizzaDataContext db = new QLPizzaDataContext();
        private List<SanPham> LaySpHot(int count)
        {
            return db.SanPhams.OrderByDescending(a => a.soLuong).Take(count).ToList();
        }


        [HttpGet]
        // GET: Menu
        public ActionResult Index()
        {
            var list = LaySpHot(6);
            return View(list);
        }
    }
}