namespace WebApiProjectRECOVERY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BookList
    {
        public int id { get; set; }

        public int id_book { get; set; }

        public int id_class { get; set; }

    }

    public partial class BookListIn
    {
        public int id_book { get; set; }

        public int id_class { get; set; }
    }
}
