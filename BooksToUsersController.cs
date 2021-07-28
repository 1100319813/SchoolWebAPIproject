using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProjectRECOVERY.Models;

namespace WebApiProjectRECOVERY.Controllers
{
    public class BooksToUsersController : ApiController
    {
        public IEnumerable<BooksToUser> Get()
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                List<BooksToUser> listbooktouser = db.BooksToUsers.ToList();
                return listbooktouser;
            }
        }

        // GET by class ID
        [Route("Api/BooksToUsers/Get/{ID}")]
        public BooksToUser Get(int ID)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return db.BooksToUsers.Where(x => x.id == ID).FirstOrDefault();
            }
        }

        // GET
        [Route("Api/BooksToUsers/GetBooksToUsers")]
        public IEnumerable<BooksToUser> GetBooksToUsers(int ID_Book, int ID_User)
        {
            string idbook = (ID_Book.ToString() == null) ? "" : ID_Book.ToString();
            string iduser = (ID_User.ToString() == null) ? "" : ID_User.ToString();

            using (LibraryDBModel db = new LibraryDBModel())
            {
                List<BooksToUser> a = db.BooksToUsers.Where(y => y.id_book.ToString().Contains(idbook) && y.id_user.ToString().Contains(iduser)).ToList();
                return a;
            }

        }

        // POST
        public BooksToUserIn Post([FromBody] BooksToUserIn newbooktouser)
        {
            BooksToUser newbooktouserwithid = new BooksToUser();

            newbooktouserwithid.id_book = newbooktouser.id_book;
            newbooktouserwithid.id_user = newbooktouser.id_user;
            newbooktouserwithid.checkout_date = newbooktouser.checkout_date;
            newbooktouserwithid.return_date = newbooktouser.return_date;

            using (LibraryDBModel db = new LibraryDBModel())
            {
                db.BooksToUsers.Add(newbooktouserwithid);
                db.SaveChanges();
                return newbooktouser;
            }
        }

        // DELETE
        [Route("Api/BooksToUsers/Delete/{id}")]
        public void Delete(int id)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                db.BooksToUsers.Remove(db.BooksToUsers.FirstOrDefault(e => e.id == id));
                db.SaveChanges();
            }
        }

        // PUT/Update
        public BooksToUser Put([FromBody] BooksToUser updatedbooktouser)
        {
            BooksToUser curbooktouser;
            using (LibraryDBModel db = new LibraryDBModel())
            {
                curbooktouser = db.BooksToUsers.Where(x => x.id == updatedbooktouser.id).FirstOrDefault();
                curbooktouser.id_book = updatedbooktouser.id_book;
                curbooktouser.id_user = updatedbooktouser.id_user;
                curbooktouser.checkout_date = updatedbooktouser.checkout_date;
                curbooktouser.return_date = updatedbooktouser.return_date;
                db.SaveChanges();
            }
            return curbooktouser;
        }
    }
}
