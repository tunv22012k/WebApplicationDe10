using System.Collections.Generic;

namespace WebApplicationDe10.Models
{
    public class DatHangViewModel
    {
        public string TenKhachHang { get; set; }
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public virtual List<SanPhamDatHangViewModel> SanPhamDatHangViewModel { get; set; }
    }
}