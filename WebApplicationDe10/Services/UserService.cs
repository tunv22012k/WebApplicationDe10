using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebApplicationDe10.Helpers;
using WebApplicationDe10.Models;

namespace WebApplicationDe10.Services
{
    public class UserService
    {
        public List<User> GetAllUsers()
        {
            string query = "SELECT * FROM users";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            List<User> users = new List<User>();

            foreach (DataRow row in dt.Rows)
            {
                User user = new User();
                user.Id             = (int)row["Id"];
                user.UserName       = row["userName"].ToString();
                user.PassWord       = row["passWord"].ToString();
                user.Email          = row["Email"].ToString();
                user.Address        = row["address"].ToString();
                user.PhoneNumber    = row["phoneNumber"].ToString();
                users.Add(user);
            }

            return users;
        }

        // public Product GetProductById(int id)
        // {
        //     string query = "SELECT * FROM Products WHERE ProductId = @ProductId";
        //     SqlParameter[] parameters = new SqlParameter[]
        //     {
        //         new SqlParameter("@ProductId", id)
        //     };
        // 
        //     DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
        // 
        //     if (dt.Rows.Count > 0)
        //     {
        //         DataRow row = dt.Rows[0];
        //         Product product = new Product
        //         {
        //             ProductId = (int)row["ProductId"],
        //             Name = row["Name"].ToString(),
        //             Price = (decimal)row["Price"]
        //             // Gán các giá trị khác cho product
        //         };
        //         return product;
        //     }
        // 
        //     return null; // Hoặc xử lý nếu không tìm thấy sản phẩm
        // }
        // 
        // public bool AddProduct(Product product)
        // {
        //     string query = "INSERT INTO Products (Name, Price) VALUES (@Name, @Price)";
        //     SqlParameter[] parameters = new SqlParameter[]
        //     {
        //         new SqlParameter("@Name", product.Name),
        //         new SqlParameter("@Price", product.Price)
        //     };
        // 
        //     int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
        //     return rowsAffected > 0;
        // }
        // 
        // public bool UpdateProduct(Product product)
        // {
        //     string query = "UPDATE Products SET Name = @Name, Price = @Price WHERE ProductId = @ProductId";
        //     SqlParameter[] parameters = new SqlParameter[]
        //     {
        //         new SqlParameter("@Name", product.Name),
        //         new SqlParameter("@Price", product.Price),
        //         new SqlParameter("@ProductId", product.ProductId)
        //     };
        // 
        //     int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
        //     return rowsAffected > 0;
        // }
        // 
        // public bool DeleteProduct(int id)
        // {
        //     string query = "DELETE FROM Products WHERE ProductId = @ProductId";
        //     SqlParameter[] parameters = new SqlParameter[]
        //     {
        //         new SqlParameter("@ProductId", id)
        //     };
        // 
        //     int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
        //     return rowsAffected > 0;
        // }
    }
}