using System;

namespace WebApplicationDe10.Models
{
    public class Quan_Tri_Vien
    {
        public int Ma_Quan_Tri_Vien { get; set; }
        public string Ten_Quan_Tri_Vien { get; set; }
        public string Mat_Khau_Quan_Tri_Vien { get; set; }
        public string Email { get; set; }
        public DateTime Ngay_Tao { get; set; }
        public DateTime Ngay_Cap_Nhat { get; set; }
    }
}