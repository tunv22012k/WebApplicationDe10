using System;
using System.Collections.Generic;

namespace WebApplicationDe10.Models
{
    public class DanhMucSanPham
    {
        // data db
        public int MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }

        // data khi join thêm
        public virtual List<SanPham> SanPham { get; set; }
    }
}