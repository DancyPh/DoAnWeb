using DoAn.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DoAn.Controllers
{
    public class HomeController : Controller
	{
        QLPizzaDataContext data = new QLPizzaDataContext();

        private List<SanPham> LaySpHot(int count)
        {
            return data.SanPhams.OrderByDescending(a => a.soLuong).Take(count).ToList();
        }

        public ActionResult LoginLogout()
        {
            return PartialView("LoginLogoutPartial");
        }

        public ActionResult ChiTietSanPham(int ?id)
        {
            var sp = data.SanPhams.FirstOrDefault(s => s.maSanPham == id);
            if(sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }

        public ActionResult Logout()
        {
            return PartialView("LogoutPartial");
        }

        [HttpGet]
        public ActionResult Index()
		{
            var _pro = LaySpHot(6);
			return View(_pro);
		}

        
	}
}