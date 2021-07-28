namespace WebApiProjectRECOVERY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public int id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

        public string password { get; set; }

        public string rights { get; set; }

        public bool isteacher { get; set; }

    }

    public partial class UserIn
    {
        public string name { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

        public string password { get; set; }

        public string rights { get; set; }

        public bool isteacher { get; set; }
    }
}
