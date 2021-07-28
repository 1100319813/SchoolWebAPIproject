using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebApiProjectRECOVERY.Models
{
    public partial class LibraryDBModel : DbContext
    {
        public LibraryDBModel()
            : base("name=LibraryDBModel")
        {
        }

        public virtual DbSet<AdminPrivilege> AdminPrivileges { get; set; }
        public virtual DbSet<BookList> BookLists { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BooksToUser> BooksToUsers { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<ClassesToUser> ClassesToUsers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminPrivilege>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.author)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.isbn)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.subject)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.grade)
                .IsUnicode(false);

            modelBuilder.Entity<Class>()
                .Property(e => e.Homeroom)
                .IsUnicode(false);

            modelBuilder.Entity<Class>()
                .Property(e => e.GradeLetter)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.rights)
                .IsUnicode(false);
        }
    }
}
