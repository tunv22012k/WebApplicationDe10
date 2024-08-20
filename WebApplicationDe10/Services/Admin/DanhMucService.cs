using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplicationDe10.Helpers;
using WebApplicationDe10.Models;

namespace WebApplicationDe10.Services.Admin
{
    public class DanhMucService
    {
        public List<DanhMucSanPham> GetAllDanhMuc(DanhMucSanPham paramsDanhMuc)
        {
            List<DanhMucSanPham> danhMuc = new List<DanhMucSanPham>();

            using (var connection = new SqlConnection(DatabaseHelper.connectionString))
            {
                string query = @"
                    SELECT 
                        *
                    FROM 
                        DanhMucSanPham
                ";

                if (!string.IsNullOrEmpty(paramsDanhMuc.TenDanhMuc) || !string.IsNullOrEmpty(paramsDanhMuc.MoTa))
                {
                    bool isAnd = false;
                    query += " WHERE";

                    if (!string.IsNullOrEmpty(paramsDanhMuc.TenDanhMuc))
                    {
                        query += " TenDanhMuc LIKE @TenDanhMuc";
                        isAnd = true;
                    }

                    if (!string.IsNullOrEmpty(paramsDanhMuc.MoTa))
                    {
                        if (isAnd)
                        {
                            query += " AND";
                        }
                        query += " MoTa LIKE @MoTa";
                        isAnd = true;
                    }
                }

                SqlCommand cmd = new SqlCommand(query, connection);

                if (!string.IsNullOrEmpty(paramsDanhMuc.TenDanhMuc))
                {
                    cmd.Parameters.AddWithValue("@TenDanhMuc", "%" + paramsDanhMuc.TenDanhMuc + "%");
                }
                if (!string.IsNullOrEmpty(paramsDanhMuc.MoTa))
                {
                    cmd.Parameters.AddWithValue("@MoTa", "%" + paramsDanhMuc.MoTa + "%");
                }

                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        danhMuc.Add(new DanhMucSanPham
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

            return danhMuc;
        }

        public bool CreateDanhMuc(DanhMucSanPham danhMucSanPham)
        {
            try
            {
                bool isSuccess = false;

                // Tạo kết nối với cơ sở dữ liệu và lưu thông tin người dùng
                using (var connection = new SqlConnection(DatabaseHelper.connectionString))
                {
                    string query = @"
                        INSERT INTO DanhMucSanPham 
                        (
                            TenDanhMuc,
                            MoTa,
                            NgayTao,
                            NgayCapNhat
                        ) 
                        VALUES 
                        (
                            @TenDanhMuc,
                            @MoTa,
                            @NgayTao,
                            @NgayCapNhat
                        )
                    ";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@TenDanhMuc", danhMucSanPham.TenDanhMuc);
                    cmd.Parameters.AddWithValue("@MoTa", danhMucSanPham.MoTa);
                    cmd.Parameters.AddWithValue("@NgayTao", DateTime.Now);
                    cmd.Parameters.AddWithValue("@NgayCapNhat", DateTime.Now);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    connection.Close();

                    // Nếu số hàng bị ảnh hưởng lớn hơn 0, quá trình chèn đã thành công
                    isSuccess = rowsAffected > 0;
                }

                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot open database connection: " + ex.Message);
            }
        }

        public bool DeleteDanhMuc(string MaDanhMuc)
        {
            try
            {
                bool isSuccess = false;

                // Tạo kết nối với cơ sở dữ liệu
                using (var connection = new SqlConnection(DatabaseHelper.connectionString))
                {
                    string query = "DELETE FROM DanhMucSanPham WHERE MaDanhMuc = @MaDanhMuc";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MaDanhMuc", int.Parse(MaDanhMuc));

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    connection.Close();

                    // Nếu số hàng bị ảnh hưởng lớn hơn 0, quá trình chèn đã thành công
                    isSuccess = rowsAffected > 0;
                }

                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot open database connection: " + ex.Message);
            }
        }

        public bool EditDanhMuc(DanhMucSanPham paramsDanhMuc)
        {
            try
            {
                bool isSuccess = false;

                // Tạo kết nối với cơ sở dữ liệu
                using (var connection = new SqlConnection(DatabaseHelper.connectionString))
                {
                    string query = @"
                        UPDATE DanhMucSanPham 
                        SET 
                            TenDanhMuc = @TenDanhMuc, 
                            MoTa = @MoTa,
                            NgayCapNhat = @NgayCapNhat
                        WHERE 
                            MaDanhMuc = @MaDanhMuc
                    ";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@TenDanhMuc", paramsDanhMuc.TenDanhMuc);
                    cmd.Parameters.AddWithValue("@MoTa", paramsDanhMuc.MoTa);
                    cmd.Parameters.AddWithValue("@MaDanhMuc", paramsDanhMuc.MaDanhMuc);
                    cmd.Parameters.AddWithValue("@NgayCapNhat", DateTime.Now);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    connection.Close();

                    // Nếu số hàng bị ảnh hưởng lớn hơn 0, quá trình chèn đã thành công
                    isSuccess = rowsAffected > 0;
                }

                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot open database connection: " + ex.Message);
            }
        }
    }
}