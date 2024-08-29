using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplicationDe10.Helpers;
using WebApplicationDe10.Models;

namespace WebApplicationDe10.Services
{
    public class ChiTietSanPhamService
    {
        public class ProductData
        {
            public SanPham sanPham { get; set; }
            public List<ThongSoKyThuatSP> thongSoKyThuatSP { get; set; }
        }

        public ProductData GetProductById(int MaSanPham)
        {
            var productData = new ProductData
            {
                sanPham             = new SanPham(),
                thongSoKyThuatSP    = new List<ThongSoKyThuatSP>()
            };

            using (var connection = new SqlConnection(DatabaseHelper.connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT 
                        sp.MaSanPham,
                        sp.TenSanPham,
                        sp.MaDanhMuc,
                        sp.MaThuongHieu,
                        sp.GiaSanPham,
                        sp.MoTa,
                        sp.URLHinhAnh,
                        sp.SoLuongTonKho,
                        sp.ThoiGianBaoHanh,
                        sp.NgayRaMat,
                        sp.NgayTao,
                        sp.NgayCapNhat,
                        dm.TenDanhMuc,
                        th.TenThuongHieu
                    FROM 
                        SanPham sp
                    INNER JOIN DanhMucSanPham dm 
                        ON sp.MaDanhMuc = dm.MaDanhMuc
                    INNER JOIN ThuongHieu th 
                        ON sp.MaThuongHieu = th.MaThuongHieu
                    WHERE
                        MaSanPham = @MaSanPham
                ";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@MaSanPham", MaSanPham);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        productData.sanPham.MaSanPham       = (int)reader["MaSanPham"];
                        productData.sanPham.TenSanPham      = reader["TenSanPham"].ToString();
                        productData.sanPham.MaDanhMuc       = (int)reader["MaDanhMuc"];
                        productData.sanPham.MaThuongHieu    = (int)reader["MaThuongHieu"];
                        productData.sanPham.GiaSanPham      = Convert.ToDecimal(reader["GiaSanPham"]);
                        productData.sanPham.MoTa            = reader["MoTa"].ToString();
                        productData.sanPham.URLHinhAnh      = reader["URLHinhAnh"].ToString();
                        productData.sanPham.SoLuongTonKho   = (int)reader["SoLuongTonKho"];
                        productData.sanPham.ThoiGianBaoHanh = (int)reader["ThoiGianBaoHanh"];
                        productData.sanPham.NgayRaMat       = Convert.ToDateTime(reader["NgayRaMat"]);
                        productData.sanPham.NgayTao         = Convert.ToDateTime(reader["NgayTao"]);
                        productData.sanPham.NgayCapNhat     = reader["NgayCapNhat"] != DBNull.Value ? Convert.ToDateTime(reader["NgayCapNhat"]) : (DateTime?)null;
                        productData.sanPham.TenDanhMuc      = reader["TenDanhMuc"].ToString();
                        productData.sanPham.TenThuongHieu   = reader["TenThuongHieu"].ToString();
                        productData.sanPham.HinhAnhSanPham  = GetHinhAnhSanPham((int)reader["MaSanPham"]);
                    }
                }

                string query2 = @"
                    SELECT 
                        *
                    FROM 
                        ThongSoKyThuatSP
                    WHERE
                        MaSanPham = @MaSanPham
                ";

                SqlCommand cmd2 = new SqlCommand(query2, connection);
                cmd2.Parameters.AddWithValue("@MaSanPham", MaSanPham);

                using (var reader = cmd2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productData.thongSoKyThuatSP.Add(new ThongSoKyThuatSP
                        {
                            MaTSKTSP        = (int)reader["MaTSKTSP"],
                            TenThongSo      = reader["TenThongSo"].ToString(),
                            GiaTriThongSo   = reader["GiaTriThongSo"].ToString(),
                        });
                    }
                }
            }

            return productData;
        }

        private List<HinhAnhSanPham> GetHinhAnhSanPham(int maSanPham)
        {
            List<HinhAnhSanPham> hinhAnhSanPhamList = new List<HinhAnhSanPham>();

            using (var connection = new SqlConnection(DatabaseHelper.connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT 
                        *
                    FROM 
                        HinhAnhSanPham 
                    WHERE 
                        MaSanPham = @MaSanPham
                ";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var hinhAnhSanPham = new HinhAnhSanPham
                            {
                                MaHinhAnh   = (int)reader["MaHinhAnh"],
                                MaSanPham   = (int)reader["MaSanPham"],
                                URLHinhAnh  = reader["URLHinhAnh"].ToString(),
                            };

                            hinhAnhSanPhamList.Add(hinhAnhSanPham);
                        }
                    }
                }
            }

            return hinhAnhSanPhamList;
        }
    }
}