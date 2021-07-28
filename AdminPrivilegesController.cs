using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProjectRECOVERY.Models;

namespace WebApiProjectRECOVERY.Controllers
{
    public class AdminPrivilegesController : ApiController
    {
        public IEnumerable<AdminPrivilege> Get()
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                List<AdminPrivilege> listpriv = db.AdminPrivileges.ToList();
                return listpriv;
            }
        }

        // GET by class ID
        [Route("Api/AdminPrivileges/Get/{ID}")]
        public AdminPrivilege Get(int ID)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                return db.AdminPrivileges.Where(x => x.id == ID).FirstOrDefault();
            }
        }

        // GET
        [Route("Api/AdminPrivileges/GetAdminPrivileges")]
        public IEnumerable<AdminPrivilege> GetAdminPrivileges(int ID_Book, int To_Buy, int Price, int Quantity, string Paid, string Arrived, string Note)
        {
            string idbook = (ID_Book.ToString() == null) ? "" : ID_Book.ToString();
            string tobuy = (To_Buy.ToString() == null) ? "" : To_Buy.ToString();
            string price = (Price.ToString() == null) ? "" : Price.ToString();
            string quantity = (Quantity.ToString() == null) ? "" : Quantity.ToString();
            string note = (Note.ToString() == null) ? "" : Note.ToString();

            if (Paid != null)
            {
                if (Arrived != null)
                {
                    if (Paid == "True" && Arrived == "False")
                    {
                        using (LibraryDBModel db = new LibraryDBModel())
                        {
                            List<AdminPrivilege> a = db.AdminPrivileges.Where(y => y.id_book.ToString().Contains(idbook) && y.to_buy.ToString().Contains(tobuy) && y.price.ToString().Contains(price) && y.quantity.ToString().Contains(quantity) && y.paid == true && y.Note.ToString().Contains(note)).ToList();
                            return a;
                        }
                    }

                    else if (Arrived == "True" && Paid == "False")
                    {
                        using (LibraryDBModel db = new LibraryDBModel())
                        {
                            List<AdminPrivilege> a = db.AdminPrivileges.Where(y => y.id_book.ToString().Contains(idbook) && y.to_buy.ToString().Contains(tobuy) && y.price.ToString().Contains(price) && y.quantity.ToString().Contains(quantity) && y.arrived == true && y.Note.ToString().Contains(note)).ToList();
                            return a;
                        }
                    }

                    else if (Arrived == "True" && Paid == "True")
                    {
                        using (LibraryDBModel db = new LibraryDBModel())
                        {
                            List<AdminPrivilege> a = db.AdminPrivileges.Where(y => y.id_book.ToString().Contains(idbook) && y.to_buy.ToString().Contains(tobuy) && y.price.ToString().Contains(price) && y.quantity.ToString().Contains(quantity) && y.arrived == true && y.paid == true && y.Note.ToString().Contains(note)).ToList();
                            return a;
                        }
                    }
                }
            }

            using (LibraryDBModel db = new LibraryDBModel())
            {
                List<AdminPrivilege> a = db.AdminPrivileges.Where(y => y.id_book.ToString().Contains(idbook) && y.to_buy.ToString().Contains(tobuy) && y.price.ToString().Contains(price) && y.quantity.ToString().Contains(quantity) && y.Note.ToString().Contains(note)).ToList();
                return a;
            }

        }

        // POST
        public AdminPrivilegeIn Post([FromBody] AdminPrivilegeIn newadminprivelege)
        {
            AdminPrivilege newadminprivewithid = new AdminPrivilege();

            newadminprivewithid.id_book = newadminprivelege.id_book;
            newadminprivewithid.to_buy = newadminprivelege.to_buy;
            newadminprivewithid.price = newadminprivelege.price;
            newadminprivewithid.paid = newadminprivelege.paid;
            newadminprivewithid.arrived = newadminprivelege.arrived;
            newadminprivewithid.quantity = newadminprivelege.quantity;
            newadminprivewithid.Note = newadminprivelege.Note;

            using (LibraryDBModel db = new LibraryDBModel())
            {
                db.AdminPrivileges.Add(newadminprivewithid);
                db.SaveChanges();
                return newadminprivelege;
            }
        }

        // DELETE
        [Route("Api/AdminPrivileges/Delete/{id}")]
        public void Delete(int id)
        {
            using (LibraryDBModel db = new LibraryDBModel())
            {
                db.AdminPrivileges.Remove(db.AdminPrivileges.FirstOrDefault(e => e.id == id));
                db.SaveChanges();
            }
        }

        // PUT/Update
        public AdminPrivilege Put([FromBody] AdminPrivilege updatedadminprivilege)
        {
            AdminPrivilege curadminprivilege;
            using (LibraryDBModel db = new LibraryDBModel())
            {
                curadminprivilege = db.AdminPrivileges.Where(x => x.id == updatedadminprivilege.id).FirstOrDefault();
                curadminprivilege.id_book = updatedadminprivilege.id_book;
                curadminprivilege.to_buy = updatedadminprivilege.to_buy;
                curadminprivilege.price = updatedadminprivilege.price;
                curadminprivilege.paid = updatedadminprivilege.paid;
                curadminprivilege.arrived = updatedadminprivilege.arrived;
                curadminprivilege.quantity = updatedadminprivilege.quantity;
                curadminprivilege.Note = updatedadminprivilege.Note;
                db.SaveChanges();
            }
            return curadminprivilege;
        }
    }
}
