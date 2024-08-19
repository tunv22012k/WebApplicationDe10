using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using WebApplicationDe10.Helpers;
using WebApplicationDe10.Models;

namespace WebApplicationDe10.Services
{
    public class AccountService
    {
        // Phương thức để xác thực người dùng
        public QuanTriVien Authenticate(string email, string password)
        {
            string hashedPassword = HashPassword(password);

            using (var connection = new SqlConnection(DatabaseHelper.connectionString))
            {
                string query = @"
                    SELECT 
                        MaQuanTriVien, 
                        TenQuanTriVien,
                        MatKhauQuanTriVien,
                        Email
                    FROM 
                        QuanTriVien
                    WHERE 
                        Email = @Email 
                        AND MatKhauQuanTriVien = @MatKhauQuanTriVien
                ";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@MatKhauQuanTriVien", hashedPassword);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new QuanTriVien
                    {
                        MaQuanTriVien   = (int)reader["MaQuanTriVien"],
                        Email           = reader["Email"].ToString(),
                        TenQuanTriVien  = reader["TenQuanTriVien"].ToString()
                    };
                }
            }

            return null;
        }

        // Phương thức để hash mật khẩu
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}