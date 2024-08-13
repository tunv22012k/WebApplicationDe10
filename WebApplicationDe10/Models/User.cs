using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationDe10.Models
{
    public class User
    {
        public int userId { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
}