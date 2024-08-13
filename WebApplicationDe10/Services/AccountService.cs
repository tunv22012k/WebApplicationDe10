using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using WebApplicationDe10.Models;

namespace WebApplicationDe10.Services
{
    public class AccountService
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // Phương thức để xác thực người dùng
        public User Authenticate(string email, string password)
        {
            string hashedPassword = HashPassword(password);

            using (var connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        userId, 
                        email,
                        username,
                        password, 
                        role
                    FROM 
                        users
                    WHERE 
                        email = @email 
                        AND password = @password
                ";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", hashedPassword);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new User
                    {
                        userId      = (int)reader["userId"],
                        email       = reader["email"].ToString(),
                        username    = reader["username"].ToString(),
                        role        = reader["role"].ToString()
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