using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationDe10.Models
{
    public class LichSuTrangThaiDonHang
    {
        public int MaLichSuTTDonHang { get; set; }
        public int MaDonHang { get; set; }
        public string TrangThaiDonHang { get; set; }
        public DateTime NgayCapNhat { get; set; }
    }
}