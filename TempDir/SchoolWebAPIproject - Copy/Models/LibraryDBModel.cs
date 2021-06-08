namespace SchoolWebAPIproject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;


    public partial class LibraryDBModel : DbContext
    {
        public LibraryDBModel()
            : base("name=LibraryDBModel")
        {
        }

        public virtual DbSet<Book> Books { get; set;}

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }

}
