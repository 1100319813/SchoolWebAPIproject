using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolWebAPIproject.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
    }

    public class BookIn
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
    }
}