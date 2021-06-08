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
        [Route("Api/User/GetActive")]
        public IEnumerable<User> GetActive()
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                List<User> listUser = db.Users.Where(y => y.Active == true).ToList();
                return listUser;
            }
        }
        // get based on ID
        public User Get(int ID)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return db.Users.Where(x => x.ID == ID).FirstOrDefault();
            }
        }
        [Route("Api/User/GetUsers")]
        public IEnumerable<User> GetUsers(string LastName, string FirstName, string MiddleName, string Email, string Phone, string Active)
        {
            string lastname = (LastName == null) ? "" : LastName;
            string firstname = (FirstName == null) ? "" : FirstName;
            string mname = (MiddleName == null) ? "" : MiddleName;
            string email = (Email == null) ? "" : Email;
            string phone = (Phone == null) ? "" : Phone;

            if (Active != null)
            {
                if (Active == "True")
                {
                    using (LibraryDBModel db = new LibraryDBModel())
                    {
                        List<User> a = db.Users.Where(y => y.LastName.Contains(lastname) &&
                                                           y.FirstName.Contains(firstname) &&
                                                           y.MiddleName.Contains(mname) &&
                                                           y.Email.Contains(email) &&
                                                           y.Phone.Contains(phone) &&
                                                           y.Active == true).ToList();
                        return a;
                    }
                }
            }

            using (LibraryDBModel db = new LibraryDBModel())
            {
                List<User> a = db.Users.Where(y => y.LastName.Contains(lastname) &&
                                                    y.FirstName.Contains(firstname) &&
                                                    y.MiddleName.Contains(mname) &&
                                                    y.Email.Contains(email) &&
                                                    y.Phone.Contains(phone)).ToList();
                return a;
            }
        }

        [Route("Api/User/GetLogin")]
        public User GetUsers(string username)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return db.Users.Where(x => x.UserLogin.CompareTo(username) == 0).FirstOrDefault();
            }
        }
        [Route("Api/User/Delete/{id}")]
        public void DeleteUsingId(int id)
        {
            User curuser;
            using (LibraryDBModel db = new LibraryDBModel())
            {
                curuser = db.Users.Where(x => x.ID == id).FirstOrDefault();
                curuser.Active = false;
                db.SaveChanges();
            }
        }
        //
        public UserIn Post([FromBody] UserIn newuser)
        {
            User newuserwithid = new User();

            newuserwithid.FirstName = newuser.FirstName;
            newuserwithid.LastName = newuser.LastName;
            newuserwithid.MiddleName = newuser.MiddleName;
            newuserwithid.Email = newuser.Email;
            newuserwithid.UserLogin = newuser.UserLogin;
            newuserwithid.UserPassword = newuser.UserPassword;
            newuserwithid.Phone = newuser.Phone;
            newuserwithid.AdminPrivilege = newuser.AdminPrivilege;
            newuserwithid.Active = newuser.Active;

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
                curuser = db.Users.Where(x => x.ID == updateduser.ID).FirstOrDefault();
                curuser.LastName = updateduser.LastName;
                curuser.FirstName = updateduser.FirstName;
                curuser.MiddleName = updateduser.MiddleName;
                curuser.Email = updateduser.Email;
                curuser.Phone = updateduser.Phone;
                curuser.UserLogin = updateduser.UserLogin;
                curuser.UserPassword = updateduser.UserPassword;
                curuser.AdminPrivilege = updateduser.AdminPrivilege;
                curuser.Active = updateduser.Active;
                db.SaveChanges();
            }
            return curuser;
        }        
    }
}
