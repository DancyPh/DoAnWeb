using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;
using System.Web.Routing;

namespace DoAn.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {

        QLPizzaDataContext data = new QLPizzaDataContext();
        // GET: Admin/Account
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult Login()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Login(NhanVien _userFormPage)
        {
            var acc = data.NhanViens.Where(m => m.taiKhoan == _userFormPage.taiKhoan && m.matKhau == _userFormPage.matKhau).FirstOrDefault();
            if (acc == null)
            {
                ViewBag.LoginStatus = 0;
            }
            else
            {
                Session["taiKhoan"] = acc;
                return RedirectToAction("Table", "Home");
            }

            return View();
        }

        public ActionResult LoginLogout()
		{
            return PartialView("LoginLogout");
		}
    }
}