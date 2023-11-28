using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;

namespace DoAn.Controllers
{
    public class UserController : Controller
    {
        QLPizzaDataContext db = new QLPizzaDataContext();

        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        public ActionResult Dangnhap(FormCollection collection)
        {
            var staiKhoan = collection["taiKhoan"];
            var smatKhau = collection["matKhau"];
            if (String.IsNullOrEmpty(staiKhoan))
            {
                ViewData["Err1"] = "Nhập lại tài khoản";
            }
            else if (String.IsNullOrEmpty(smatKhau))
            {
                ViewData["Err2"] = "Phải nhập mật khẩu";
            }
            else
            {
                KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.taiKhoan == staiKhoan && n.matKhau == smatKhau);
                if (kh != null)
                {
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.LoginStatus = 0;
                }
            }
            return View();
        }

        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KhachHang kh)
        {
            var sHoTen = collection["HoTen"];
            var staiKhoan = collection["taiKhoan"];
            var smatKhau = collection["matKhau"];
            var sMatKhauNhapLai = collection["MatKhauNL"];
            var sDiachi = collection["Diachi"];
            var sDienThoai = collection["DienThoai"];

            if (String.IsNullOrEmpty(staiKhoan))
            {
                ViewData["err1"] = "Tên đăng không được rỗng";
            }
            else if (String.IsNullOrEmpty(smatKhau))
            {
                ViewData["err2"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(sMatKhauNhapLai))
            {
                ViewData["err3"] = "Phải nhập lại mật khẩu";
            }
            else if (smatKhau != sMatKhauNhapLai)
            {
                ViewData["err3"] = "Mật khẩu nhập lại không khớp";
            }
            else if (String.IsNullOrEmpty(sHoTen))
            {
                ViewData["err4"] = "Họ tên không được rỗng";
            }
            else if (String.IsNullOrEmpty(sDienThoai))
            {
                ViewData["err6"] = "Số điện thoại không được rỗng";
            }
            else if (db.KhachHangs.SingleOrDefault(n => n.taiKhoan == staiKhoan) != null)
            {
                ViewBag.ThongBao = "Tên đăng nhập đã tồn tại";
            }
            else
            {
                kh.hoTen = sHoTen;
                kh.taiKhoan = staiKhoan;
                kh.matKhau = smatKhau;
                kh.diaChi = sDiachi;
                kh.soDT = sDienThoai;
                db.KhachHangs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("Dangnhap");

            }
            return this.DangKy();

        }

        public ActionResult Dangxuat()
        {
            return View();
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}