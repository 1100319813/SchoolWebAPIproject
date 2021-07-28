namespace WebApiProjectRECOVERY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    //   id int NOT NULL PRIMARY KEY IDENTITY(1,1),
    //   id_book int NOT NULL,
    //   price int NOT NULL,
    //quantity int NOT NULL,
    //   to_buy int NOT NULL,
    //paid bit NOT NULL,
    //   arrived bit NOT NULL,
    //Note varchar(255)

    public partial class AdminPrivilege
    {
        public int id { get; set; }

        public int id_book { get; set; }

        public int price { get; set; }

        public int quantity { get; set; }

        public int to_buy { get; set; }

        public bool paid { get; set; }

        public bool arrived { get; set; }

        public string Note { get; set; }
    }

    public partial class AdminPrivilegeIn
    {
        public int id_book { get; set; }

        public int price { get; set; }

        public int quantity { get; set; }

        public int to_buy { get; set; }

        public bool paid { get; set; }

        public bool arrived { get; set; }

        public string Note { get; set; }
    }
}
