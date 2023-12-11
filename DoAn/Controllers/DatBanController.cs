using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;

namespace DoAn.Controllers
{
    public class DatBanController : Controller
    {
        QLPizzaDataContext db = new QLPizzaDataContext();

        [HttpGet]
        public ActionResult DatBan()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return Redirect("~/User/DangNhap?id=2");
            }
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            return View();
        }

        [HttpPost]
        public ActionResult DatBanHang(FormCollection f)
        {
           
            return RedirectToAction("XacNhanDatBan", "DatBan");
        }

        public ActionResult XacNhanDatBan()
        {
            return View();
        }

        // GET: DatBan
        public ActionResult Index()
        {
            return View();
        }
    }
}