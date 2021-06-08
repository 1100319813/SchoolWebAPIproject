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

        // get based on ID
        public Book Get(int ID)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return db.Books.Where(x => x.ID == ID).FirstOrDefault();
            }
        }

       /* [Route("Api/Books/GetAuthor/{author}")]
        public Book GetAuthor(string Author)
        {
            using (LibraryDBModel db = new LibraryDBModel()) 
            {
                return db.Books.Where(y => y.Author == Author).FirstOrDefault();
            }
        } */

        //Gets all authors
        [Route("Api/Books/GetAllAuthors/{author}")]
        public IEnumerable<Book> GetAllAuthors(string Author)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return db.Books.Where(y => y.Author.Contains(Author)).ToList();
            }
        }

        // Gets any book 
        [Route("Api/Books/GetAnyBook/{search}")]
        public IEnumerable<Book> GetAnyBook(string Search)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {

                IEnumerable<Book> res1 = GetAllAuthors(Search);
                IEnumerable<Book> res2 = GetAllTitles(Search);
                res1 = res1.Concat(res2);
                IEnumerable<Book> res3 = GetAllISBN(Search);
                res1 = res1.Concat(res3);
                return res1;
            }
        }



        /*[Route("Api/Books/GetTitle/{title}")]
        public Book GetTitle(string Title)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return db.Books.Where(y => y.Title == Title).FirstOrDefault();
            }
        } */

        //Gets all titles
        [Route("Api/Books/GetAllTitles/{title}")]
        public IEnumerable<Book> GetAllTitles(string Title)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return db.Books.Where(y => y.Title.Contains(Title)).ToList();
            }
        }

        /*[Route("Api/Books/GetISBN/{ISBN}")]
        public Book GetISBN(string ISBN)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return db.Books.Where(y => y.ISBN == ISBN).FirstOrDefault();
            }
        }*/

        // GET all ISBN
        [Route("Api/Books/GetAllISBN/{ISBN}")]
        public IEnumerable<Book> GetAllISBN(string ISBN)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return db.Books.Where(y => y.ISBN.Contains(ISBN)).ToList();
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
        public HttpResponseMessage Put(int id, [FromBody] Book updatedBook)
        {
            try
            {
                using (LibraryDBModel db = new LibraryDBModel())
                {
                    var entity = db.Books.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Book with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.Author = updatedBook.Author;
                        entity.Title = updatedBook.Title;
                        entity.ISBN = updatedBook.ISBN;
                        entity.Pages = updatedBook.Pages;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
