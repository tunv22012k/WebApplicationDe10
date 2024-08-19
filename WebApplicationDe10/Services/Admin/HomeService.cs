using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplicationDe10.Helpers;
using WebApplicationDe10.Models;

namespace WebApplicationDe10.Services.Admin
{
    public class HomeService
    {
        public List<QuanTriVien> GetAllUsers(QuanTriVien paramsUser)
        {
            List<QuanTriVien> users = new List<QuanTriVien>();

            using (var connection = new SqlConnection(DatabaseHelper.connectionString))
            {
                string query = @"
                    SELECT 
                        *
                    FROM 
                        QuanTriVien
                ";

                if (!string.IsNullOrEmpty(paramsUser.Email) || !string.IsNullOrEmpty(paramsUser.TenQuanTriVien))
                {
                    bool isAnd = false;
                    query += " WHERE";

                    if (!string.IsNullOrEmpty(paramsUser.Email))
                    {
                        query += " Email LIKE @Email";
                        isAnd = true;
                    }

                    if (!string.IsNullOrEmpty(paramsUser.TenQuanTriVien))
                    {
                        if (isAnd)
                        {
                            query += " AND";
                        }
                        query += " TenQuanTriVien LIKE @TenQuanTriVien";
                        isAnd = true;
                    }
                }

                SqlCommand cmd = new SqlCommand(query, connection);

                if (!string.IsNullOrEmpty(paramsUser.Email))
                {
                    cmd.Parameters.AddWithValue("@Email", "%" + paramsUser.Email + "%");
                }
                if (!string.IsNullOrEmpty(paramsUser.TenQuanTriVien))
                {
                    cmd.Parameters.AddWithValue("@TenQuanTriVien", "%" + paramsUser.TenQuanTriVien + "%");
                }

                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new QuanTriVien
                        {
                            MaQuanTriVien   = (int)reader["MaQuanTriVien"],
                            Email           = reader["Email"].ToString(),
                            TenQuanTriVien  = reader["TenQuanTriVien"].ToString()
                        });
                    }
                }
            }

            return users;
        }

        public bool DeleteUSer(string userId)
        {
            try
            {
                bool isSuccess = false;

                // Tạo kết nối với cơ sở dữ liệu
                using (var connection = new SqlConnection(DatabaseHelper.connectionString))
                {
                    string query = "DELETE FROM QuanTriVien WHERE MaQuanTriVien = @userId";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@userId", int.Parse(userId));

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

        public bool EditUSer(QuanTriVien user)
        {
            try
            {
                bool isSuccess = false;

                // Tạo kết nối với cơ sở dữ liệu
                using (var connection = new SqlConnection(DatabaseHelper.connectionString))
                {
                    string query = @"
                        UPDATE QuanTriVien 
                        SET 
                            TenQuanTriVien = @TenQuanTriVien, 
                            Email = @Email
                        WHERE 
                            MaQuanTriVien = @MaQuanTriVien
                    ";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@TenQuanTriVien", user.TenQuanTriVien);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@MaQuanTriVien", user.MaQuanTriVien);

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