using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using WebApplicationDe10.Helpers;
using WebApplicationDe10.Models;

namespace WebApplicationDe10.Services.Admin
{
    public class UserService
    {
        public bool CreateUSer(QuanTriVien user)
        {
            try
            {
                bool isSuccess = false;

                // Mã hóa mật khẩu hoặc xử lý khác nếu cần
                string hashedPassword = HashPassword(user.MatKhauQuanTriVien);

                // Tạo kết nối với cơ sở dữ liệu và lưu thông tin người dùng
                using (var connection = new SqlConnection(DatabaseHelper.connectionString))
                {
                    string query = @"
                        INSERT INTO QuanTriVien 
                        (
                            TenQuanTriVien, 
                            Email,
                            MatKhauQuanTriVien,
                            NgayTao,
                            NgayCapNhat
                        ) 
                        VALUES 
                        (
                            @TenQuanTriVien,
                            @Email,
                            @MatKhauQuanTriVien,
                            @NgayTao,
                            @NgayCapNhat
                        )
                    ";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@TenQuanTriVien", user.TenQuanTriVien);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@MatKhauQuanTriVien", hashedPassword);
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

        private string HashPassword(string password)
        {
            // Hàm mã hóa mật khẩu
            // Ví dụ: Sử dụng SHA256 hoặc bất kỳ phương pháp nào phù hợp với bạn
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}