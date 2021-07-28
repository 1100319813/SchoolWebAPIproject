namespace WebApiProjectRECOVERY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Book
    {
        public int id { get; set; }

        public string title { get; set; }

        public string author { get; set; }

        public string isbn { get; set; }

        public string subject { get; set; }

        public string grade { get; set; }

    }

    public partial class BookIn
    {
        public string title { get; set; }

        public string author { get; set; }

        public string isbn { get; set; }

        public string subject { get; set; }

        public string grade { get; set; }
    }
}
