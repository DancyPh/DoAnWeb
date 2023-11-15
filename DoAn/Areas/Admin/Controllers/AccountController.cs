using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;

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
                ViewBag.LoginSatus = 0;
			}
			else
			{
                return RedirectToAction("Index", "Home");
			}
            return View();
		}
    }
}