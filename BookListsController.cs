using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProjectRECOVERY.Models;

namespace WebApiProjectRECOVERY.Controllers
{
    public class BookListsController : ApiController
    {
            // Get All class
            public IEnumerable<BookList> Get()
            {
                using (LibraryDBModel db = new LibraryDBModel())
                {
                    List<BookList> listbooks = db.BookLists.ToList();
                    return listbooks;
                }
            }

            // GET by class ID
            [Route("Api/BookLists/Get/{ID}")]
            public BookList Get(int ID)
            {
                using (LibraryDBModel db = new LibraryDBModel())
                {
                    return db.BookLists.Where(x => x.id == ID).FirstOrDefault();
                }
            }

            // GET
            [Route("Api/BookLists/GetBookLists")]
            public IEnumerable<BookList> GetBookLists(int ID_Book, int ID_Class)
            {
                string idbook = (ID_Book.ToString() == null) ? "" : ID_Book.ToString();
                string idclass = (ID_Class.ToString() == null) ? "" : ID_Class.ToString();
                using (LibraryDBModel db = new LibraryDBModel())
                {
                    List<BookList> a = db.BookLists.Where(y => y.id_book.ToString().Contains(idbook) && y.id_class.ToString().Contains(idclass)).ToList();
                    return a;
                }
            }

            // POST
            public BookListIn Post([FromBody] BookListIn newbooklist)
            {
                BookList newbooklistwithid = new BookList();

                newbooklistwithid.id_book = newbooklist.id_book;
                newbooklistwithid.id_class = newbooklist.id_class;

                using (LibraryDBModel db = new LibraryDBModel())
                {
                    db.BookLists.Add(newbooklistwithid);
                    db.SaveChanges();
                    return newbooklist;
                }
            }

            // DELETE
            [Route("Api/BookLists/Delete/{id}")]
            public void Delete(int id)
            {
                using (LibraryDBModel db = new LibraryDBModel())
                {
                    db.BookLists.Remove(db.BookLists.FirstOrDefault(e => e.id == id));
                    db.SaveChanges();
                }
            }

            // PUT/Update
            public BookList Put([FromBody] BookList updatedbooklist)
            {
                BookList curbooklist;
                using (LibraryDBModel db = new LibraryDBModel())
                {
                    curbooklist = db.BookLists.Where(x => x.id == updatedbooklist.id).FirstOrDefault();
                    curbooklist.id_book = updatedbooklist.id_book;
                    curbooklist.id_class = updatedbooklist.id_class;
                    db.SaveChanges();
                }
                return curbooklist;
            }
        }
}
