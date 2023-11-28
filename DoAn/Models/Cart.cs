using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAn.Models;

namespace DoAn.Models
{
	public class Cart
	{
		QLPizzaDataContext data = new QLPizzaDataContext();

		public int iSanPham { get; set; }
		public string nSanPham { get; set; }
		public string imgSanPham { get; set; }
		public double pSanPham { get; set; }
		public int qSanPham { get; set; }
		public double tThanhTien
		{
			get { return pSanPham * qSanPham; }
		}

		public Cart(int id)
		{
			iSanPham = id;
			SanPham pro = data.SanPhams.Single(i => i.maSanPham == iSanPham);
			nSanPham = pro.tenSanPham;
			imgSanPham = pro.hinhanh;
			pSanPham = double.Parse(pro.gia.ToString());
			qSanPham = 1;
		}
	}

}