using System;

namespace WebApplicationDe10.Models
{
    public class SanPham
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int MaDanhMuc { get; set; }
        public int MaThuongHieu { get; set; }
        public decimal GiaSanPham { get; set; }
        public string MoTa { get; set; }
        public string URLHinhAnh { get; set; }
        public int SoLuongTonKho { get; set; }
        public int ThoiGianBaoHanh { get; set; }
        public DateTime NgayRaMat { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayCapNhat { get; set; }
    }
}