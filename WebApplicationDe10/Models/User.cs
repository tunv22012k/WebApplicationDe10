﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationDe10.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}