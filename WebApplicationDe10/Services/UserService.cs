﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplicationDe10.Helpers;
using WebApplicationDe10.Models;

namespace WebApplicationDe10.Services
{
    public class UserService
    {
        public class ProductData
        {
            public List<SanPham> ListSanPhamNew { get; set; }
            public List<DanhMucSanPham> ListDanhMucSanPham { get; set; }
        }

        public ProductData GetListDanhMucAndSanPham()
        {
            var productData = new ProductData
            {
                ListSanPhamNew      = new List<SanPham>(),
                ListDanhMucSanPham  = new List<DanhMucSanPham>()
            };

            using (var connection = new SqlConnection(DatabaseHelper.connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT 
                        *
                    FROM 
                        SanPham
                    ORDER BY NgayTao DESC
                ";

                SqlCommand cmd = new SqlCommand(query, connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productData.ListSanPhamNew.Add(new SanPham
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
                ";

                SqlCommand cmd2 = new SqlCommand(query2, connection);

                using (var reader = cmd2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productData.ListDanhMucSanPham.Add(new DanhMucSanPham
                        {
                            MaDanhMuc   = (int)reader["MaDanhMuc"],
                            TenDanhMuc  = reader["TenDanhMuc"].ToString(),
                            MoTa        = reader["MoTa"].ToString(),
                            NgayTao     = Convert.ToDateTime(reader["NgayTao"]),
                            NgayCapNhat = reader["NgayCapNhat"] != DBNull.Value ? Convert.ToDateTime(reader["NgayCapNhat"]) : (DateTime?)null,
                            SanPham     = GetListSanPhamByDanhMuc((int)reader["MaDanhMuc"], productData.ListSanPhamNew)
                        });
                    }
                }
            }

            return productData;
        }

        private List<SanPham> GetListSanPhamByDanhMuc(int MaDanhMuc, List<SanPham> listSanPham)
        {
            List<SanPham> sanPham = new List<SanPham>();

            for (int i = 0; i < listSanPham.Count; i++)
            {
                if (MaDanhMuc == listSanPham[i].MaDanhMuc)
                {
                    sanPham.Add(new SanPham
                    {
                        MaSanPham       = listSanPham[i].MaSanPham,
                        TenSanPham      = listSanPham[i].TenSanPham,
                        MaDanhMuc       = listSanPham[i].MaDanhMuc,
                        MaThuongHieu    = listSanPham[i].MaThuongHieu,
                        GiaSanPham      = listSanPham[i].GiaSanPham,
                        MoTa            = listSanPham[i].MoTa,
                        URLHinhAnh      = listSanPham[i].URLHinhAnh,
                        SoLuongTonKho   = listSanPham[i].SoLuongTonKho,
                        ThoiGianBaoHanh = listSanPham[i].ThoiGianBaoHanh,
                        NgayRaMat       = listSanPham[i].NgayRaMat,
                        NgayTao         = listSanPham[i].NgayTao,
                        NgayCapNhat     = listSanPham[i].NgayCapNhat,
                    });
                }
            }

            return sanPham;
        }
    }
}