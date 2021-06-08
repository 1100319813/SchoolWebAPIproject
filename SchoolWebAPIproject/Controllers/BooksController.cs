using SchoolWebAPIproject.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolWebAPIproject.Controllers
{
    public class BooksController : ApiController
    {
        // Get All books
        public IEnumerable<Book> Get()
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                List<Book> listBook = db.Books.ToList();
                return listBook;
            }
        }
        // GET all books by UserID
        [Route("Api/Books/GetByUserID/{UserID}")]
        public IEnumerable<Book> GetByUserID(int UserID)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return (db.Books.Where(x => x.UserId == UserID)).ToList(); 
            }
        }
        // GET by book ID
        [Route("Api/Books/Get/{ID}")]
        public Book Get(int ID)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return db.Books.Where(x => x.ID == ID).FirstOrDefault();
            }
        }
        // GET
        [Route("Api/Books/GetBooks")]
        public IEnumerable<Book> GetBooks(string Author, string Title, string ISBN)
        {
            string author = (Author == null) ? "" : Author;
            string title = (Title == null) ? "" : Title;
            string isbn = (ISBN == null) ? "" : ISBN;
            using (LibraryDBModel db = new LibraryDBModel())
            {
                List<Book> a = db.Books.Where(y => y.Author.Contains(author) && y.Title.Contains(title) && y.ISBN.Contains(isbn)).ToList();
                return a;
            }
        }
        // POST
        public BookIn Post([FromBody] BookIn newbook)
        {
            Book newbookwithid = new Book();

            newbookwithid.Author = newbook.Author;
            newbookwithid.Title = newbook.Title;
            newbookwithid.ISBN = newbook.ISBN;
            newbookwithid.Pages = newbook.Pages;

            using (LibraryDBModel db = new LibraryDBModel())
            {
                db.Books.Add(newbookwithid);
                db.SaveChanges();
                return newbook;
            }
        }
        // DELETE
        [Route("Api/Books/Delete/{id}")]
        public void DeleteUsingISBN(int id)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                db.Books.Remove(db.Books.FirstOrDefault(e => e.ID == id));
                db.SaveChanges();
            }
        }
        // PUT/Update
        public Book Put([FromBody] Book updatedbook)
        {
            Book curbook;
            using (LibraryDBModel db = new LibraryDBModel())
            {
                curbook = db.Books.Where(x => x.ID == updatedbook.ID).FirstOrDefault();
                curbook.ISBN = updatedbook.ISBN;
                curbook.Title = updatedbook.Title;
                curbook.Author = updatedbook.Author;
                curbook.Pages = updatedbook.Pages;
                curbook.CheckedIn = updatedbook.CheckedIn;
                curbook.CheckedOut = updatedbook.CheckedOut;
                curbook.UserId = updatedbook.UserId;
                db.SaveChanges();
            }
            return curbook;
        }
    }
}
