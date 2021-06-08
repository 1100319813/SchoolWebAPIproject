using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolWebAPIproject.Models
{
    public class User
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public bool AdminPrivilege { get; set; }
        public bool Active { get; set; }
    }

    public class UserIn
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public bool AdminPrivilege { get; set; }
        public bool Active { get; set; }
    }
}