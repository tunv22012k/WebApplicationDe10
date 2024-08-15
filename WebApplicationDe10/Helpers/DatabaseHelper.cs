using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace WebApplicationDe10.Helpers
{
    public static class DatabaseHelper
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // Hàm để lấy connection string từ file Web.config
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        // Hàm để mở kết nối tới cơ sở dữ liệu
        public static SqlConnection CommonConnect()
        {
            string connectionString = GetConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần và xử lý ngoại lệ
                throw new Exception("Cannot open database connection: " + ex.Message);
            }

            return connection;
        }

        // Hàm để thực thi câu lệnh SQL và trả về kết quả dưới dạng DataTable
        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = CommonConnect())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable result = new DataTable();
                        adapter.Fill(result);
                        return result;
                    }
                }
            }
        }

        // Hàm để thực thi câu lệnh SQL không trả về dữ liệu (INSERT, UPDATE, DELETE)
        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = CommonConnect())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}