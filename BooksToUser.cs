namespace WebApiProjectRECOVERY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BooksToUser")]
    public partial class BooksToUser
    {
        public int id { get; set; }

        public int id_book { get; set; }

        public int id_user { get; set; }

        public DateTime checkout_date { get; set; }

        public DateTime return_date { get; set; }

    }

    public partial class BooksToUserIn
    {
        public int id_book { get; set; }

        public int id_user { get; set; }

        public DateTime checkout_date { get; set; }

        public DateTime return_date { get; set; }
    }
}
