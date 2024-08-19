using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationDe10.Models
{
    public class QuanTriVien
    {
        public int MaQuanTriVien { get; set; }
        public string TenQuanTriVien { get; set; }
        public string MatKhauQuanTriVien { get; set; }
        public string Email { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayCapNhat { get; set; }
    }
}