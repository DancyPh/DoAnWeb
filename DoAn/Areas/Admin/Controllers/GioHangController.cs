using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;

namespace DoAn.Areas.Admin.Controllers
{
    public class GioHangController : Controller
    {
        QLPizzaDataContext data = new QLPizzaDataContext();

        // Get: GioHang
        public List<GioHang> GetGioHang()
		{
			List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
			{
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
			}
            return lstGioHang;
		}

        // Add to cart
        public ActionResult AddToCart( int id, string url)
		{
            List<GioHang> lstGioHang = GetGioHang();
            GioHang sp = lstGioHang.Find(i => i.iSanPham == id);
            if(sp == null)
			{
                sp = new GioHang(id);
                lstGioHang.Add(sp);
			}
			else
			{
                sp.qSanPham++;
			}
            return Redirect(url);
		}

        // amount
        private int Amount()
		{
            int amount = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
			{
                amount = lstGioHang.Sum(s => s.qSanPham);
			}
            return amount;
		}

        // total
        private double Total()
		{
            double total = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang != null)
			{
                total = lstGioHang.Sum(s => s.pSanPham);
			}
            return total;
		}

        // GET: Admin/GioHang
        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = GetGioHang();
            if(lstGioHang.Count == 0)
			{
                return RedirectToAction("Table", "Home");
			}
            ViewBag.Amount = Amount();
            ViewBag.Total = Total();
            return View(lstGioHang);
        }

        public ActionResult GioHangPartial()
		{
            ViewBag.Amount = Amount();
            ViewBag.Total = Total();
            return PartialView();
		}
    }
}