using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplicationDe10.Helpers;
using WebApplicationDe10.Models;

namespace WebApplicationDe10.Services.Admin
{
    public class ThuongHieuService
    {
        public List<ThuongHieu> GetAllDanhMuc(ThuongHieu paramsThuongHieu)
        {
            List<ThuongHieu> thuongHieu = new List<ThuongHieu>();

            using (var connection = new SqlConnection(DatabaseHelper.connectionString))
            {
                string query = @"
                    SELECT 
                        *
                    FROM 
                        ThuongHieu
                ";

                if (!string.IsNullOrEmpty(paramsThuongHieu.TenThuongHieu) || !string.IsNullOrEmpty(paramsThuongHieu.QuocGia))
                {
                    bool isAnd = false;
                    query += " WHERE";

                    if (!string.IsNullOrEmpty(paramsThuongHieu.TenThuongHieu))
                    {
                        query += " TenThuongHieu LIKE @TenThuongHieu";
                        isAnd = true;
                    }

                    if (!string.IsNullOrEmpty(paramsThuongHieu.QuocGia))
                    {
                        if (isAnd)
                        {
                            query += " AND";
                        }
                        query += " QuocGia LIKE @QuocGia";
                        isAnd = true;
                    }
                }

                SqlCommand cmd = new SqlCommand(query, connection);

                if (!string.IsNullOrEmpty(paramsThuongHieu.TenThuongHieu))
                {
                    cmd.Parameters.AddWithValue("@TenThuongHieu", "%" + paramsThuongHieu.TenThuongHieu + "%");
                }
                if (!string.IsNullOrEmpty(paramsThuongHieu.QuocGia))
                {
                    cmd.Parameters.AddWithValue("@QuocGia", "%" + paramsThuongHieu.QuocGia + "%");
                }

                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        thuongHieu.Add(new ThuongHieu
                        {
                            MaThuongHieu    = (int)reader["MaThuongHieu"],
                            TenThuongHieu   = reader["TenThuongHieu"].ToString(),
                            QuocGia         = reader["QuocGia"].ToString(),
                            NgayTao         = Convert.ToDateTime(reader["NgayTao"]),
                            NgayCapNhat     = reader["NgayCapNhat"] != DBNull.Value ? Convert.ToDateTime(reader["NgayCapNhat"]) : (DateTime?)null,
                        });
                    }
                }
            }

            return thuongHieu;
        }

        public bool CreateThuongHieu(ThuongHieu thuongHieu)
        {
            try
            {
                bool isSuccess = false;

                // Tạo kết nối với cơ sở dữ liệu và lưu thông tin người dùng
                using (var connection = new SqlConnection(DatabaseHelper.connectionString))
                {
                    string query = @"
                        INSERT INTO ThuongHieu 
                        (
                            TenThuongHieu,
                            QuocGia,
                            NgayTao,
                            NgayCapNhat
                        ) 
                        VALUES 
                        (
                            @TenThuongHieu,
                            @QuocGia,
                            @NgayTao,
                            @NgayCapNhat
                        )
                    ";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@TenThuongHieu", thuongHieu.TenThuongHieu);
                    cmd.Parameters.AddWithValue("@QuocGia", thuongHieu.QuocGia);
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

        public bool DeleteThuongHieu(string MaThuongHieu)
        {
            try
            {
                bool isSuccess = false;

                // Tạo kết nối với cơ sở dữ liệu
                using (var connection = new SqlConnection(DatabaseHelper.connectionString))
                {
                    // Kiểm tra xem có sản phẩm nào sử dụng MaThuongHieu này không
                    string checkQuery = @"SELECT COUNT(*) FROM SanPham WHERE MaThuongHieu = @MaThuongHieu";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                    checkCmd.Parameters.AddWithValue("@MaThuongHieu", MaThuongHieu);
                    connection.Open();
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        // Nếu có sản phẩm đang sử dụng, không xóa được
                        return false;
                    }

                    string query = "DELETE FROM ThuongHieu WHERE MaThuongHieu = @MaThuongHieu";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MaThuongHieu", int.Parse(MaThuongHieu));

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

        public bool EditThuongHieu(ThuongHieu paramsThuongHieu)
        {
            try
            {
                bool isSuccess = false;

                // Tạo kết nối với cơ sở dữ liệu
                using (var connection = new SqlConnection(DatabaseHelper.connectionString))
                {
                    string query = @"
                        UPDATE ThuongHieu 
                        SET 
                            TenThuongHieu   = @TenThuongHieu, 
                            QuocGia         = @QuocGia,
                            NgayCapNhat     = @NgayCapNhat
                        WHERE 
                            MaThuongHieu    = @MaThuongHieu
                    ";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@TenThuongHieu", paramsThuongHieu.TenThuongHieu);
                    cmd.Parameters.AddWithValue("@QuocGia", paramsThuongHieu.QuocGia);
                    cmd.Parameters.AddWithValue("@MaThuongHieu", paramsThuongHieu.MaThuongHieu);
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