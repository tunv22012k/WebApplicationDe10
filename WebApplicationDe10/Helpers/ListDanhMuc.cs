using WebApplicationDe10.Models;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace WebApplicationDe10.Helpers
{
    public static class ListDanhMuc
    {
        public static List<DanhMucSanPham> GetDanhMucSanPham()
        {
            var ListDanhMuc = new List<DanhMucSanPham>();

            using (var connection = new SqlConnection(DatabaseHelper.connectionString))
            {
                connection.Open();

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
                        ListDanhMuc.Add(new DanhMucSanPham
                        {
                            MaDanhMuc   = (int)reader["MaDanhMuc"],
                            TenDanhMuc  = reader["TenDanhMuc"].ToString(),
                            MoTa        = reader["MoTa"].ToString(),
                            NgayTao     = Convert.ToDateTime(reader["NgayTao"]),
                            NgayCapNhat = reader["NgayCapNhat"] != DBNull.Value ? Convert.ToDateTime(reader["NgayCapNhat"]) : (DateTime?)null,
                        });
                    }
                }
            }

            return ListDanhMuc;
        }
    }
}