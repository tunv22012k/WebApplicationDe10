using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationDe10.Models
{
    public class DanhMucSanPham
    {
        public int MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayCapNhat { get; set; }
    }
}