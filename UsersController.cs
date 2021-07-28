using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProjectRECOVERY.Models;

namespace WebApiProjectRECOVERY.Controllers
{
    public class UsersController : ApiController
    {
        // Get All users
        public IEnumerable<User> Get()
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                List<User> listUser = db.Users.ToList();
                return listUser;
            }
        }

        // get based on ID
        public User Get(int ID)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return db.Users.Where(x => x.id == ID).FirstOrDefault();
            }
        }


        [Route("Api/User/GetUsers")]
        public IEnumerable<User> GetUsers(string Name, string Email, string Phone)
        {
            string name = (Name == null) ? "" : Name;
            string email = (Email == null) ? "" : Email;
            string phone = (Phone == null) ? "" : Phone;


            using (LibraryDBModel db = new LibraryDBModel())
            {
                List<User> a = db.Users.Where(y => y.name.Contains(name) &&
                                                   y.email.Contains(email) &&
                                                   y.phone.Contains(phone)).ToList();
                return a;
            }

        }

        [Route("Api/User/Delete/{id}")]
        public void DeleteUsingId(int id)
        {
            User curuser;
            using (LibraryDBModel db = new LibraryDBModel())
            {
                curuser = db.Users.Remove(db.Users.FirstOrDefault(e => e.id == id));
                db.SaveChanges();
            }
        }
        //
        public UserIn Post([FromBody] UserIn newuser)
        {
            User newuserwithid = new User();

            newuserwithid.name = newuser.name;
            newuserwithid.email = newuser.email;
            newuserwithid.phone = newuser.phone;
            newuserwithid.password = newuser.password;
            newuserwithid.rights = newuser.rights;
            newuserwithid.isteacher = newuser.isteacher;

            using (LibraryDBModel db = new LibraryDBModel())
            {
                db.Users.Add(newuserwithid);
                db.SaveChanges();
                return newuser;
            }
        }

        // PUT/Update
        public User Put([FromBody] User updateduser)
        {
            User curuser;
            using (LibraryDBModel db = new LibraryDBModel())
            {
                curuser = db.Users.Where(x => x.id == updateduser.id).FirstOrDefault();
                curuser.name = updateduser.name;
                curuser.email = updateduser.email;
                curuser.phone = updateduser.phone;
                curuser.password = updateduser.password;
                curuser.rights = updateduser.rights;
                curuser.isteacher = updateduser.isteacher;
                db.SaveChanges();
            }
            return curuser;
        }
    }
}
