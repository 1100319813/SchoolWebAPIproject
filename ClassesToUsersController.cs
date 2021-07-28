using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProjectRECOVERY.Models;

namespace WebApiProjectRECOVERY.Controllers
{
    public class ClassesToUsersController : ApiController
    {
        public IEnumerable<ClassesToUser> Get()
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                List<ClassesToUser> listclasstouser = db.ClassesToUsers.ToList();
                return listclasstouser;
            }
        }

        // GET by class ID
        [Route("Api/ClassesToUser/Get/{ID}")]
        public ClassesToUser Get(int ID)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return db.ClassesToUsers.Where(x => x.id == ID).FirstOrDefault();
            }
        }

        // GET
        [Route("Api/ClassesToUser/GetClassesToUsers")]
        public IEnumerable<ClassesToUser> GetClassesToUsers(int ID_User, int ID_Class)
        {
            string iduser = (ID_User.ToString() == null) ? "" : ID_User.ToString();
            string idclass = (ID_Class.ToString() == null) ? "" : ID_Class.ToString();

            using (LibraryDBModel db = new LibraryDBModel())
            {
                List<ClassesToUser> a = db.ClassesToUsers.Where(y => y.id_user.ToString().Contains(iduser) && y.id_class.ToString().Contains(idclass)).ToList();
                return a;
            }

        }

        // POST
        public ClassesToUserIn Post([FromBody] ClassesToUserIn newclasstouser)
        {
            ClassesToUser newclasstouserwithid = new ClassesToUser();

            newclasstouserwithid.id_user = newclasstouser.id_user;
            newclasstouserwithid.id_class = newclasstouser.id_class;

            using (LibraryDBModel db = new LibraryDBModel())
            {
                db.ClassesToUsers.Add(newclasstouserwithid);
                db.SaveChanges();
                return newclasstouser;
            }
        }

        // DELETE
        [Route("Api/ClassesToUser/Delete/{id}")]
        public void Delete(int id)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                db.ClassesToUsers.Remove(db.ClassesToUsers.FirstOrDefault(e => e.id == id));
                db.SaveChanges();
            }
        }

        // PUT/Update
        public ClassesToUser Put([FromBody] ClassesToUser updatedclasstouser)
        {
            ClassesToUser curclasstouser;
            using (LibraryDBModel db = new LibraryDBModel())
            {
                curclasstouser = db.ClassesToUsers.Where(x => x.id == updatedclasstouser.id).FirstOrDefault();
                curclasstouser.id_user = updatedclasstouser.id_user;
                curclasstouser.id_class = updatedclasstouser.id_class;
                db.SaveChanges();
            }
            return curclasstouser;
        }
    }
}
