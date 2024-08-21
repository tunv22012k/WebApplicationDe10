using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationDe10.Models
{
    public class ThuongHieu
    {
        public int MaThuongHieu { get; set; }
        public string TenThuongHieu { get; set; }
        public string QuocGia { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
    }
}