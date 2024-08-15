using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using WebApplicationDe10.Helpers;
using WebApplicationDe10.Models;

namespace WebApplicationDe10.Services.Admin
{
    public class HomeService
    {
        public List<User> GetAllUsers(User paramsUser)
        {
            /*string query = "SELECT * FROM users";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            List<User> users = new List<User>();

            foreach (DataRow row in dt.Rows)
            {
                User user = new User();
                user.userId = (int)row["userId"];
                user.email = row["email"].ToString();
                user.username = row["username"].ToString();
                user.role = row["role"].ToString();
                users.Add(user);
            }

            return users;*/

            List<User> users = new List<User>();

            using (var connection = new SqlConnection(DatabaseHelper.connectionString))
            {
                string query = @"
                    SELECT 
                        *
                    FROM 
                        users
                ";

                if (!string.IsNullOrEmpty(paramsUser.email) || !string.IsNullOrEmpty(paramsUser.role))
                {
                    bool isAnd = false;
                    query += " WHERE";

                    if (!string.IsNullOrEmpty(paramsUser.email))
                    {
                        query += " email LIKE @email";
                        isAnd = true;
                    }

                    if (!string.IsNullOrEmpty(paramsUser.role))
                    {
                        if (isAnd)
                        {
                            query += " AND";
                        }
                        query += " role LIKE @role";
                        isAnd = true;
                    }
                }

                SqlCommand cmd = new SqlCommand(query, connection);

                if (!string.IsNullOrEmpty(paramsUser.email))
                {
                    cmd.Parameters.AddWithValue("@email", "%" + paramsUser.email + "%");
                }
                if (!string.IsNullOrEmpty(paramsUser.role))
                {
                    cmd.Parameters.AddWithValue("@role", "%" + paramsUser.role + "%");
                }

                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            userId = (int)reader["userId"],
                            email = reader["email"].ToString(),
                            username = reader["username"].ToString(),
                            role = reader["role"].ToString()
                        });
                    }
                }
            }

            return users;
        }
    }
}