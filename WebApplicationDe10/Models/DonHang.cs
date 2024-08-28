using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationDe10.Models
{
    public class DonHang
    {
        public int MaDonHang { get; set; }
        public int MaKhachHang { get; set; }
        public int MaSanPham { get; set; }
        public int? MaPhieuGiamGia { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaDonVi { get; set; }
        public decimal? GiamGia { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public DateTime NgayDatHang { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThaiDonHang { get; set; }
        public string PhuongThucThanhToan { get; set; }
        public string PhuongThucGiaoHang { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public string TenKhachHang { get; set; }
        public string TenSanPham { get; set; }
    }
}