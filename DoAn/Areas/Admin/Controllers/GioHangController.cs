﻿using System;
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
        public List<Cart> GetGioHang()
		{
			List<Cart> lstGioHang = Session["GioHang"] as List<Cart>;
            if (lstGioHang == null)
			{
                lstGioHang = new List<Cart>();
                Session["GioHang"] = lstGioHang;
			}
            return lstGioHang;
		}

        // Add to cart
        public ActionResult AddToCart( int id, string url)
		{
            List<Cart> lstGioHang = GetGioHang();
            Cart sp = lstGioHang.Find(i => i.iSanPham == id);
            if(sp == null)
			{
                sp = new Cart(id);
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
            List<Cart> lstGioHang = Session["GioHang"] as List<Cart>;
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
            List<Cart> lstGioHang = Session["GioHang"] as List<Cart>;
            if (lstGioHang != null)
            {
                total = lstGioHang.Sum(s => s.tThanhTien);
            }

            Session["Total"] = total; // Cập nhật tổng tiền trong Session

            return total;
        }

        // update
        public ActionResult UpdateCart(int iSanPham, FormCollection f)
        {
            List<Cart> lstGioHang = GetGioHang();
            Cart sp = lstGioHang.SingleOrDefault(n => n.iSanPham == iSanPham);
            if (sp != null)
            {
                sp.qSanPham = int.Parse(f["quantity"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        // delete one product
        public ActionResult DeleteOneProduct(int iSanPham)
        {
            List<Cart> lstGioHang = GetGioHang();
            Cart sp = lstGioHang.SingleOrDefault(n => n.iSanPham == iSanPham);
            if(sp != null)
            {
                lstGioHang.RemoveAll(n => n.iSanPham == iSanPham);
               
            }
            return RedirectToAction("GioHang");
        }


        // GET: Admin/GioHang
        public ActionResult GioHang()
        {
            List<Cart> lstGioHang = GetGioHang();
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