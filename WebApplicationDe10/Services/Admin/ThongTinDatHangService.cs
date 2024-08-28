using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplicationDe10.Helpers;
using WebApplicationDe10.Models;

namespace WebApplicationDe10.Services.Admin
{
    public class ThongTinDatHangService
    {
        public List<DonHang> GetAllDonHang(DonHang paramsDonHang)
        {
            List<DonHang> donHang = new List<DonHang>();

            using (var connection = new SqlConnection(DatabaseHelper.connectionString))
            {
                string query = @"
                    SELECT 
                        dh.MaDonHang,
                        dh.MaKhachHang,
                        dh.MaSanPham,
                        dh.MaPhieuGiamGia,
                        dh.SoLuong,
                        dh.GiaDonVi,
                        dh.GiamGia,
                        dh.DiaChiGiaoHang,
                        dh.NgayDatHang,
                        dh.TongTien,
                        dh.TrangThaiDonHang,
                        dh.PhuongThucThanhToan,
                        dh.PhuongThucGiaoHang,
                        dh.NgayTao,
                        dh.NgayCapNhat,
                        kh.TenKhachHang,
                        sp.TenSanPham
                    FROM 
                        DonHang dh
                    INNER JOIN KhachHang kh 
                        ON dh.MaKhachHang = kh.MaKhachHang
                    INNER JOIN SanPham sp 
                        ON dh.MaSanPham = sp.MaSanPham
                ";

                /*if (!string.IsNullOrEmpty(paramsSanPham.TenSanPham) || paramsSanPham.MaDanhMuc > 0 || paramsSanPham.MaThuongHieu > 0)
                {
                    bool isAnd = false;
                    query += " WHERE";

                    if (!string.IsNullOrEmpty(paramsSanPham.TenSanPham))
                    {
                        query += " sp.TenSanPham LIKE @TenSanPham";
                        isAnd = true;
                    }

                    if (paramsSanPham.MaDanhMuc > 0)
                    {
                        if (isAnd)
                        {
                            query += " AND";
                        }
                        query += " sp.MaDanhMuc = @MaDanhMuc";
                        isAnd = true;
                    }

                    if (paramsSanPham.MaThuongHieu > 0)
                    {
                        if (isAnd)
                        {
                            query += " AND";
                        }
                        query += " sp.MaThuongHieu = @MaThuongHieu";
                        isAnd = true;
                    }
                }*/

                SqlCommand cmd = new SqlCommand(query, connection);

                /*if (!string.IsNullOrEmpty(paramsSanPham.TenSanPham))
                {
                    cmd.Parameters.AddWithValue("@TenSanPham", "%" + paramsSanPham.TenSanPham + "%");
                }
                if (paramsSanPham.MaDanhMuc > 0)
                {
                    cmd.Parameters.AddWithValue("@MaDanhMuc", paramsSanPham.MaDanhMuc);
                }
                if (paramsSanPham.MaThuongHieu > 0)
                {
                    cmd.Parameters.AddWithValue("@MaThuongHieu", paramsSanPham.MaThuongHieu);
                }*/

                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        donHang.Add(new DonHang
                        {
                            MaDonHang           = (int)reader["MaDonHang"],
                            MaKhachHang         = (int)reader["MaKhachHang"],
                            MaSanPham           = (int)reader["MaSanPham"],
                            MaPhieuGiamGia      = reader["GiamGia"] != DBNull.Value ? (int)reader["MaPhieuGiamGia"] : (int?)null,
                            SoLuong             = (int)reader["SoLuong"],
                            GiaDonVi            = Convert.ToDecimal(reader["GiaDonVi"]),
                            GiamGia             = reader["GiamGia"] != DBNull.Value ? Convert.ToDecimal(reader["GiamGia"]) : (decimal?)null,
                            DiaChiGiaoHang      = reader["DiaChiGiaoHang"].ToString(),
                            NgayDatHang         = Convert.ToDateTime(reader["NgayDatHang"]),
                            TongTien            = Convert.ToDecimal(reader["TongTien"]),
                            TrangThaiDonHang    = reader["TrangThaiDonHang"].ToString(),
                            PhuongThucThanhToan = reader["PhuongThucThanhToan"].ToString(),
                            PhuongThucGiaoHang  = reader["PhuongThucGiaoHang"].ToString(),
                            NgayTao             = Convert.ToDateTime(reader["NgayTao"]), 
                            NgayCapNhat         = reader["NgayCapNhat"] != DBNull.Value ? Convert.ToDateTime(reader["NgayCapNhat"]): (DateTime?)null,
                            TenKhachHang        = reader["TenKhachHang"].ToString(),
                            TenSanPham          = reader["TenSanPham"].ToString()
                        });
                    }
                }
            }

            return donHang;
        }
    }
}