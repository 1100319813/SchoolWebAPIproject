using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProjectRECOVERY.Models;

namespace WebApiProjectRECOVERY.Controllers
{
    public class ClassesController : ApiController
    {
        // Get All class
        public IEnumerable<Class> Get()
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                List<Class> listClass = db.Classes.ToList();
                return listClass;
            }
        }

        // GET by class ID
        [Route("Api/Classes/Get/{ID}")]
        public Class Get(int ID)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return db.Classes.Where(x => x.ID == ID).FirstOrDefault();
            }
        }

        // GET
        [Route("Api/Classes/GetClasses")]
        public IEnumerable<Class> GetClasses(string Homeroom, string GradeLevel, string GradeLetter)
        {
            string homeroom = (Homeroom == null) ? "" : Homeroom;
            string gradelevel = (GradeLevel == null) ? "" : GradeLevel;
            string gradeletter = (GradeLetter == null) ? "" : GradeLetter;
            using (LibraryDBModel db = new LibraryDBModel())
            {
                List<Class> a = db.Classes.Where(y => y.Homeroom.Contains(homeroom) && y.GradeLevel.ToString().Contains(gradelevel) && y.GradeLetter.Contains(gradeletter)).ToList();
                return a;
            }
        }

        // POST
        public ClassIn Post([FromBody] ClassIn newclass)
        {
            Class newclasswithid = new Class();

            newclasswithid.Homeroom = newclass.Homeroom;
            newclasswithid.GradeLevel = newclass.GradeLevel;
            newclasswithid.GradeLetter = newclass.GradeLetter;

            using (LibraryDBModel db = new LibraryDBModel())
            {
                db.Classes.Add(newclasswithid);
                db.SaveChanges();
                return newclass;
            }
        }

        // DELETE
        [Route("Api/Classes/Delete/{id}")]
        public void Delete(int id)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                db.Classes.Remove(db.Classes.FirstOrDefault(e => e.ID == id));
                db.SaveChanges();
            }
        }

        // PUT/Update
        public Class Put([FromBody] Class updatedclass)
        {
            Class curclass;
            using (LibraryDBModel db = new LibraryDBModel())
            {
                curclass = db.Classes.Where(x => x.ID == updatedclass.ID).FirstOrDefault();
                curclass.Homeroom = updatedclass.Homeroom;
                curclass.GradeLevel = updatedclass.GradeLevel;
                curclass.GradeLetter = updatedclass.GradeLetter;
                db.SaveChanges();
            }
            return curclass;
        }
    }
}
