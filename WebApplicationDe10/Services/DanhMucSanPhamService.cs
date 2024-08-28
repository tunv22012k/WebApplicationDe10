using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using WebApplicationDe10.Helpers;
using WebApplicationDe10.Models;

namespace WebApplicationDe10.Services
{
    public class DanhMucSanPhamService
    {
        public class ProductData
        {
            public List<SanPham> ListSanPham { get; set; }
            public DanhMucSanPham DanhMucSanPham { get; set; }
        }
        public ProductData GetListSanPham(int MaDanhMuc)
        {
            var productData = new ProductData
            {
                ListSanPham = new List<SanPham>(),
                DanhMucSanPham = new DanhMucSanPham()
            };

            using (var connection = new SqlConnection(DatabaseHelper.connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT 
                        *
                    FROM 
                        SanPham
                    WHERE
                        MaDanhMuc = @MaDanhMuc
                    ORDER BY NgayTao DESC
                ";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@MaDanhMuc", MaDanhMuc);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productData.ListSanPham.Add(new SanPham
                        {
                            MaSanPham       = (int)reader["MaSanPham"],
                            TenSanPham      = reader["TenSanPham"].ToString(),
                            MaDanhMuc       = (int)reader["MaDanhMuc"],
                            MaThuongHieu    = (int)reader["MaThuongHieu"],
                            GiaSanPham      = Convert.ToDecimal(reader["GiaSanPham"]),
                            MoTa            = reader["MoTa"].ToString(),
                            URLHinhAnh      = reader["URLHinhAnh"].ToString(),
                            SoLuongTonKho   = (int)reader["SoLuongTonKho"],
                            ThoiGianBaoHanh = (int)reader["ThoiGianBaoHanh"],
                            NgayRaMat       = Convert.ToDateTime(reader["NgayRaMat"]),
                            NgayTao         = Convert.ToDateTime(reader["NgayTao"]),
                            NgayCapNhat     = reader["NgayCapNhat"] != DBNull.Value ? Convert.ToDateTime(reader["NgayCapNhat"]) : (DateTime?)null,
                        });
                    }
                }

                string query2 = @"
                    SELECT 
                        *
                    FROM 
                        DanhMucSanPham
                    WHERE
                        MaDanhMuc = @MaDanhMuc
                ";

                SqlCommand cmd2 = new SqlCommand(query2, connection);
                cmd2.Parameters.AddWithValue("@MaDanhMuc", MaDanhMuc);

                using (var reader = cmd2.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        productData.DanhMucSanPham.MaDanhMuc    = (int)reader["MaDanhMuc"];
                        productData.DanhMucSanPham.TenDanhMuc   = reader["TenDanhMuc"].ToString();
                        productData.DanhMucSanPham.MoTa         = reader["MoTa"].ToString();
                        productData.DanhMucSanPham.NgayTao      = Convert.ToDateTime(reader["NgayTao"]);
                        productData.DanhMucSanPham.NgayCapNhat  = reader["NgayCapNhat"] != DBNull.Value ? Convert.ToDateTime(reader["NgayCapNhat"]) : (DateTime?)null;
                    }
                }
            }

            return productData;
        }
    }
}