using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;

namespace DoAn.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        QLPizzaDataContext data = new QLPizzaDataContext();
        // GET: Admin/Order
        public ActionResult Index()
        {
            return View();
        }

        

        [HttpGet]
        public ActionResult GetOrders()
        {
            var listOrders = data.DonHangs.ToList();

            foreach (var order in listOrders)
            {
                decimal totalAmount = 0;

                foreach (var chiTietDonHang in order.ChiTietDonHangs)
                {
                    totalAmount += (int)chiTietDonHang.thanhTien;
                }

                order.tongTien = totalAmount;
            }
                data.SubmitChanges();

            return View(listOrders);
        }

        [HttpGet]
        public ActionResult Detail(int? id)
		{
            
            var orderDetail = data.ChiTietDonHangs.Where(o => o.maDonHang == id).ToList();
            if (orderDetail != null)
            {
                var order = data.DonHangs.FirstOrDefault(o => o.maDonHang == id);
                
                if (order.IsConfirmed == false)
				{
                    foreach (var i in orderDetail)
                    {
                        var product = data.SanPhams.FirstOrDefault(p => p.maSanPham == i.maSanPham);
                        if (product != null)
                        {
                            // cập nhật số lượng bán
                            product.soLuong += i.soLuong;
                        }

                        var stock = data.TonKhos.FirstOrDefault(s => s.maSanPham == i.maSanPham);
                        if (stock != null)
                        {
                            // cập nhật số lượng tồn kho
                            stock.soLuongCL -= i.soLuong;
                        }

                    }
                }
                
                data.SubmitChanges();
            }
            return View("Detail", orderDetail);
        }

        [HttpPost]
        public ActionResult Check(int?id)
        {
            var order = data.DonHangs.FirstOrDefault(o => o.maDonHang == id); // Lấy ra đơn hàng cần cập nhật trạng thái
            if (order != null)
            {
                order.IsConfirmed = true; // Cập nhật trạng thái IsConfirmed thành true
                data.SubmitChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            }

            return RedirectToAction("GetOrders", "Order");
        }

        [HttpGet]
        public ActionResult OrderNhanVien()
        {
            var listOrders = data.DonHangs.ToList();
            return View(listOrders);
        }

        [HttpGet]
        public ActionResult GoiMon()
		{
            var product = data.SanPhams.ToList();
            return View(product);
        }

    }
}
            