using System;
using System.Collections.Generic;

namespace WebApplicationDe10.Models
{
    public class SanPham
    {
        // data db
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
        public DateTime? NgayCapNhat { get; set; }

        // data khi join thêm
        public string TenDanhMuc { get; set; }
        public string TenThuongHieu { get; set; }
        public virtual List<ThongSoKyThuatSP> ThongSoKyThuatSPs { get; set; }
        public virtual List<HinhAnhSanPham> HinhAnhSanPham { get; set; }
    }
}