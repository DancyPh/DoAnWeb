using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;

namespace DoAn.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        QLPizzaDataContext data = new QLPizzaDataContext();
        // GET: Admin/Product
        public ActionResult Index()
        {
            var product = data.TonKhos.ToList();
            return View(product);
        }

        // Edit
        public ActionResult Edit(int? id)
		{
            var product = data.TonKhos.FirstOrDefault(p => p.maSanPham == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
		}

        [HttpPost]
        public ActionResult Edit(TonKho pro)
		{
			if (ModelState.IsValid)
            {
                var exist = data.TonKhos.FirstOrDefault(p => p.maSanPham == pro.maSanPham);
                if(exist == null)
				{
                    return HttpNotFound();
				}

                exist.tenSanPham = pro.tenSanPham;
                exist.soLuongCL = pro.soLuongCL;
                exist.SanPham.gia = pro.SanPham.gia;

                data.SubmitChanges();

                return RedirectToAction("Index");
            }

            return View(pro);
		}

    }
}