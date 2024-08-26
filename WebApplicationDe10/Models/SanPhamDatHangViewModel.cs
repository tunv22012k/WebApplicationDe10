using System;
using System.Collections.Generic;
using System.Web;

namespace WebApplicationDe10.Models
{
    public class SanPhamDatHangViewModel
    {
        public int MaSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaSanPham { get; set; }
        public decimal TongTien { get; set; }
    }
}