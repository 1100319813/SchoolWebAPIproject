using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProjectRECOVERY.Models;

namespace WebApiProjectRECOVERY.Controllers
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

        // GET by book ID
        [Route("Api/Books/Get/{ID}")]
        public Book Get(int ID)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return db.Books.Where(x => x.id == ID).FirstOrDefault();
            }
        }

        // GET
        [Route("Api/Books/GetBooks")]
        public IEnumerable<Book> GetBooks(string Author, string Title, string ISBN, string Subject, string Grade)
        {
            string author = (Author == null) ? "" : Author;
            string title = (Title == null) ? "" : Title;
            string isbn = (ISBN == null) ? "" : ISBN;
            string subject = (Subject == null) ? "" : Subject;
            string grade = (Grade == null) ? "" : Grade;
            using (LibraryDBModel db = new LibraryDBModel())
            {
                List<Book> a = db.Books.Where(y => y.author.Contains(author) && y.title.Contains(title) && y.isbn.Contains(isbn) && y.subject.Contains(subject) && y.grade.Contains(grade)).ToList();
                return a;
            }
        }
        // POST
        public BookIn Post([FromBody] BookIn newbook)
        {
            Book newbookwithid = new Book();

            newbookwithid.author = newbook.author;
            newbookwithid.title = newbook.title;
            newbookwithid.isbn = newbook.isbn;
            newbookwithid.subject = newbook.subject;
            newbookwithid.grade = newbook.grade;

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
                db.Books.Remove(db.Books.FirstOrDefault(e => e.id == id));
                db.SaveChanges();
            }
        }
        // PUT/Update
        public Book Put([FromBody] Book updatedbook)
        {
            Book curbook;
            using (LibraryDBModel db = new LibraryDBModel())
            {
                curbook = db.Books.Where(x => x.id == updatedbook.id).FirstOrDefault();
                curbook.title = updatedbook.title;
                curbook.author = updatedbook.author;
                curbook.isbn = updatedbook.isbn;
                curbook.subject = updatedbook.subject;
                curbook.grade = updatedbook.grade;
                db.SaveChanges();
            }
            return curbook;
        }
    }
}
