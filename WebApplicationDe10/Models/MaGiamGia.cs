using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationDe10.Models
{
    public class MaGiamGia
    {
        public int MaPhieuGiamGia { get; set; }
        public string MaGiamGiaSP { get; set; }
        public decimal SoTienGiamGia { get; set; }
        public DateTime NgayHetHang { get; set; }
        public bool TrangThaiKichHoat { get; set; }
    }
}