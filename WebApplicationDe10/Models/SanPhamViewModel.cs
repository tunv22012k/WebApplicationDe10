using System;
using System.Collections.Generic;
using System.Web;

namespace WebApplicationDe10.Models
{
    public class SanPhamViewModel
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int MaDanhMuc { get; set; }
        public int MaThuongHieu { get; set; }
        public decimal GiaSanPham { get; set; }
        public string MoTa { get; set; }
        public HttpPostedFileBase URLHinhAnh { get; set; }
        public string URLHinhAnhCu { get; set; }
        public int SoLuongTonKho { get; set; }
        public int ThoiGianBaoHanh { get; set; }
        public DateTime NgayRaMat { get; set; }
        public List<ThongSoKyThuatSP> ThongSoKyThuatSP { get; set; }
    }
}