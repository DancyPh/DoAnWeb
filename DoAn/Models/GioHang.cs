using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn.Models
{
    public class Giohang
    {
        QLPizzaDataContext db = new QLPizzaDataContext();

        public int imaSanPham { get; set; }
        public string stenSanPham { get; set; }
        public string shinhanh { get; set; }
        public double dgia { get; set; }
        public int isoLuong { get; set; }
        public double dthanhTien
        {
            get { return isoLuong * dgia; }
        }

        public Giohang(int ms)
        {
            imaSanPham = ms;
            SanPham s = db.SanPhams.Single(n => n.maSanPham == imaSanPham);
            stenSanPham = s.tenSanPham;
            shinhanh = s.hinhanh;
            dgia = double.Parse(s.gia.ToString());
            isoLuong = 1;
        }
    }
}