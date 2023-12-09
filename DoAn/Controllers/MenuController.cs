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

        private List<SanPham> LaySp(int count)
        {
            return db.SanPhams.OrderBy(a => a.soLuong).Take(count).ToList();
        }

        private List<SanPham> LaySpPizza(int count)
        {
            return db.SanPhams.Where(a => a.maLoai == 1).Take(count).ToList();
        }

        private List<SanPham> LaySpNuoc(int count)
        {
            return db.SanPhams.Where(a => a.maLoai == 2).Take(count).ToList();
        }

        [HttpGet]
        public ActionResult fcoMenu1()
        {
            var list = LaySpNuoc(3);
            return View(list);
        }


        [HttpGet]
        public ActionResult fcoMenu()
        {
            var list = LaySpPizza(3);
            return View(list);
        }


        [HttpGet]
        public ActionResult OurSanPham1()
        {
            var list = LaySp(3);
            return View(list);
        }

        [HttpGet]
        public ActionResult OurSanPham()
        {
            var list = LaySpHot(3);
            return View(list);
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