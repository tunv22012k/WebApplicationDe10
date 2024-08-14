using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebApplicationDe10.Helpers;
using WebApplicationDe10.Models;

namespace WebApplicationDe10.Services.Admin
{
    public class HomeService
    {
        public List<User> GetAllUsers()
        {
            string query = "SELECT * FROM users";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            List<User> users = new List<User>();

            foreach (DataRow row in dt.Rows)
            {
                User user = new User();
                user.userId     = (int)row["userId"];
                user.email      = row["email"].ToString();
                user.username   = row["username"].ToString();
                user.role       = row["role"].ToString();
                users.Add(user);
            }

            return users;
        }
    }
}