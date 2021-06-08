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

        // get based on ID
        public User Get(int ID)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return db.Users.Where(x => x.ID == ID).FirstOrDefault();
            }
        }


        //
        [Route("Api/User/GetAllFirstNames/{firstname}")]
        public IEnumerable<User> GetAllFirstNames(string FirstName)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return db.Users.Where(y => y.FirstName.Contains(FirstName)).ToList();
            }
        }

        

        //
        [Route("Api/User/GetAllLastNames/{lastname}")]
        public IEnumerable<User> GetAllLastNames(string LastName)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return db.Users.Where(y => y.LastName.Contains(LastName)).ToList();
            }
        }

        

        // 
        [Route("Api/User/GetAllMiddleNames/{middlename}")]
        public IEnumerable<User> GetAllMiddleNames(string MiddleName)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return db.Users.Where(y => y.MiddleName.Contains(MiddleName)).ToList();
            }
        }

        // 
        [Route("Api/User/GetAllEmails/{email}")]
        public IEnumerable<User> GetAllEmails(string Email)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return db.Users.Where(y => y.Email.Contains(Email)).ToList();
            }
        }

        // 
        [Route("Api/User/GetAllPhoneNums/{phonenum}")]
        public IEnumerable<User> GetAllPhoneNums(string Phone)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return db.Users.Where(y => y.Phone.Contains(Phone)).ToList();
            }
        }

        // POST
        public UserIn Post([FromBody] UserIn newuser)
        {
            User newuserwithid = new User();

            newuserwithid.FirstName = newuser.FirstName;
            newuserwithid.LastName = newuser.LastName;
            newuserwithid.MiddleName = newuser.MiddleName;
            newuserwithid.Email = newuser.Email;
            newuserwithid.Phone = newuser.Phone;

            using (LibraryDBModel db = new LibraryDBModel())
            {
                db.Users.Add(newuserwithid);
                db.SaveChanges();
                return newuser;
            }
        }

        // DELETE
        [Route("Api/User/Delete/{id}")]
        public void DeleteUsingISBN(int id)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                db.Users.Remove(db.Users.FirstOrDefault(e => e.ID == id));
                db.SaveChanges();
            }
        }


        // PUT/Update
        public HttpResponseMessage Put(int id, [FromBody] User updatedUser)
        {
            try
            {
                using (LibraryDBModel db = new LibraryDBModel())
                {
                    var entity = db.Users.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "User with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.FirstName = updatedUser.FirstName;
                        entity.LastName = updatedUser.LastName;
                        entity.MiddleName = updatedUser.MiddleName;
                        entity.Email = updatedUser.Email;
                        entity.Phone = updatedUser.Phone;
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
