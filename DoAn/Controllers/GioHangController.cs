using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;

namespace DoAn.Controllers
{
    public class GioHangController : Controller
    {
        QLPizzaDataContext db = new QLPizzaDataContext();

        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }

        public ActionResult ThemGioHang(int ms, string url)
        {
            List<Giohang> lstgiohang = Laygiohang();
            Giohang sp = lstgiohang.Find(n => n.imaSanPham == ms);
            if (sp == null)
            {
                sp = new Giohang(ms);
                lstgiohang.Add(sp);
            }
            else
            {
                sp.isoLuong++;
            }
            return Redirect(url);
        }

        private int Tongsoluong()
        {
            int iTongsoluong = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongsoluong = lstGiohang.Sum(n => n.isoLuong);
            }
            return iTongsoluong;
        }

        private double Tongtien()
        {
            double dTongtien = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                dTongtien = lstGiohang.Sum(n => n.dthanhTien);
            }
            return dTongtien;
        }

        public ActionResult Giohang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = Tongsoluong();
            ViewBag.Tongtien = Tongtien();
            return View(lstGiohang);
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = Tongsoluong();
            ViewBag.Tongtien = Tongtien();
            return PartialView();
        }

        public ActionResult XoaSPKhoiGioHang(int ?imaSanPham)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sp = lstGiohang.SingleOrDefault(n => n.imaSanPham == imaSanPham);
            if (sp != null)
            {
                lstGiohang.RemoveAll(n => n.imaSanPham == imaSanPham);
                if (lstGiohang.Count == 0)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult CapNhatGioHang(int ?imaSanPham, FormCollection f)
        {
            List<Giohang> lstGioHang = Laygiohang();
            Giohang sp = lstGioHang.SingleOrDefault(n => n.imaSanPham == imaSanPham);
            // Neu ton tai thi cho sua so luong
            if (sp != null)
            {
                sp.isoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaGioHang()
        {
            List<Giohang> lstGioHang = Laygiohang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return Redirect("~/User/DangNhap?id=2");
            }
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "SachOnline");
            }
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = Tongsoluong();
            ViewBag.Tongtien = Tongtien();
            return View(lstGiohang);
        }

        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            bool check = false;
            GiaoHang gh = new GiaoHang();
            DonHang dh = new DonHang();
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            List<Giohang> lstGiohang = Laygiohang();
            dh.maKhachHang = kh.maKhachHang;
            dh.ngayDatHang = DateTime.Now;
            var Ngaygiao = String.Format("{0:MM/dd/yyyy}", f["Ngaygiao"]);
            gh.ngayGiao = DateTime.Parse(Ngaygiao);
            dh.IsConfirmed = check;
            db.DonHangs.InsertOnSubmit(dh);
            db.SubmitChanges();
            foreach (var item in lstGiohang)
            {
                ChiTietDonHang ctdh = new ChiTietDonHang();
                ctdh.maDonHang = dh.maDonHang;
                ctdh.maSanPham = item.imaSanPham;
                ctdh.soLuong = item.isoLuong;
                ctdh.thanhTien = (decimal)item.dthanhTien;
                db.ChiTietDonHangs.InsertOnSubmit(ctdh);
            }
            db.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }

        public ActionResult XacNhanDonHang()
        {
            return View();
        }


        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }
    }
}