using RestSharp;
using SchoolWebAPIproject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Excel = Microsoft.Office.Interop.Excel;

namespace SchoolWebAPI_Client_Project
{
    public partial class UsersIndex : System.Web.UI.Page
    {
        string command = "https://localhost:44357/api/users";
        static List<string> TrueFalse;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Session["fullname"] as string))
                {
                    Hello_Label.Text = "Добро Пожаловать, " + Session["fullname"].ToString() + "!";
                    gvUsersLibrary.DataSource = PopulateDataTable(command);
                    Session["DatatblUsers"] = gvUsersLibrary.DataSource = PopulateDataTable(command);
                    gvUsersLibrary.DataBind();
                }
                else
                {
                    Response.Redirect("LoginPage.aspx");
                }
            }
        }

        private object PopulateDataTable(string command)
        {
            DataTable dtable = new DataTable();
            dtable.Clear();
            dtable.Columns.Add("Id");
            dtable.Columns.Add("Фамилия");
            dtable.Columns.Add("Имя");
            dtable.Columns.Add("Отчество");
            dtable.Columns.Add("Email");
            dtable.Columns.Add("Телефон");
            dtable.Columns.Add("UserLogin");
            dtable.Columns.Add("UserPassword");
            dtable.Columns.Add("Администратор");
            dtable.Columns.Add("Active");

            RestClient client = new RestClient(command);
            var request = new RestRequest(Method.GET);
            IRestResponse<List<User>> response = client.Execute<List<User>>(request);
            List<User> myusers = response.Data;
            var count = myusers.Count();
            if (count == 0)
            {
                Error_Label.Text = "Результатов не найдено";
            }

            else
            {
                Error_Label.Text = "";
                foreach (User user in myusers)
                {
                    DataRow drow = dtable.NewRow();
                    drow["Id"] = user.ID;
                    drow["Фамилия"] = user.LastName;
                    drow["Имя"] = user.FirstName;
                    drow["Отчество"] = user.MiddleName;
                    drow["Email"] = user.Email;
                    drow["Телефон"] = user.Phone;
                    drow["UserLogin"] = user.UserLogin;
                    drow["UserPassword"] = user.UserPassword;
                    drow["Администратор"] = user.AdminPrivilege;
                    drow["Active"] = user.Active;
                    dtable.Rows.Add(drow);
                }

                TrueFalse = new List<string>();
                TrueFalse.Add("True");
                TrueFalse.Add("False");
            }
            return dtable;
        }

        protected void SearchAll_Click(object sender, EventArgs e)
        {
            string lastname = ((LastNameEnter as TextBox).Text) == "" ? "" : (LastNameEnter as TextBox).Text;
            string firstname = ((FirstNameEnter as TextBox).Text) == "" ? "" : (FirstNameEnter as TextBox).Text;
            string middlename = ((MiddleNameEnter as TextBox).Text) == "" ? "" : (MiddleNameEnter as TextBox).Text;
            string email = ((EmailEnter as TextBox).Text) == "" ? "" : (EmailEnter as TextBox).Text;
            string phone = ((PhoneEnter as TextBox).Text) == "" ? "" : (PhoneEnter as TextBox).Text;
            string active = OnlyActive.Checked ? "True" : "False";

            command = "https://localhost:44357/api/user/getusers" + "?LastName=" + lastname +
                                                                     "&FirstName=" + firstname +
                                                                     "&MiddleName=" + middlename +
                                                                     "&Email=" + email +
                                                                     "&Phone=" + phone +
                                                                     "&Active=" + active;
            gvUsersLibrary.DataSource = PopulateDataTable(command);
            Session["DatatblUsers"] = gvUsersLibrary.DataSource = PopulateDataTable(command);
            gvUsersLibrary.DataBind();
        }



        protected void ResetUsers_Click(object sender, EventArgs e)
        {
            if (OnlyActive.Checked == true)
                command = "https://localhost:44357/api/user/getactive";
            else
                command = "https://localhost:44357/api/users";

            gvUsersLibrary.DataSource = PopulateDataTable(command);
            Session["DatatblUsers"] = gvUsersLibrary.DataSource = PopulateDataTable(command);
            gvUsersLibrary.DataBind();
        }

        protected void ShowLibrary_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected void btn_Add_User_Click(object sender, EventArgs e)
        {
            UserIn newuserentry = new UserIn();

            newuserentry.LastName = (gvUsersLibrary.FooterRow.FindControl("txt_LastName_New") as TextBox).Text;
            newuserentry.FirstName = (gvUsersLibrary.FooterRow.FindControl("txt_FirstName_New") as TextBox).Text;
            newuserentry.MiddleName = (gvUsersLibrary.FooterRow.FindControl("txt_MiddleName_New") as TextBox).Text;
            newuserentry.Email = (gvUsersLibrary.FooterRow.FindControl("txt_Email_New") as TextBox).Text;
            newuserentry.Phone = (gvUsersLibrary.FooterRow.FindControl("txt_Phone_New") as TextBox).Text;
            newuserentry.UserLogin = (gvUsersLibrary.FooterRow.FindControl("txt_UserLogin_New") as TextBox).Text;
            newuserentry.UserPassword = (gvUsersLibrary.FooterRow.FindControl("txt_UserPassword_New") as TextBox).Text;
            newuserentry.AdminPrivilege = ((gvUsersLibrary.FooterRow.FindControl("ddl_Admin_new") as DropDownList).Text.CompareTo("True") == 0) ? true : false;
            newuserentry.Active = ((gvUsersLibrary.FooterRow.FindControl("ddl_Active_New") as DropDownList).Text.CompareTo("True") == 0) ? true : false;

            if ((hasSpecialChar(newuserentry.LastName) == true) || (hasNums(newuserentry.LastName) == true))
            {
                Error_Label.Text = "Не вводите числа и символы!";
            }

            else if (newuserentry.LastName == "")
            {
                Error_Label.Text = "Вы забыли ввести фамилию!";
            }

            else if ((hasSpecialChar(newuserentry.FirstName) == true) || (hasNums(newuserentry.FirstName) == true))
            {
                Error_Label.Text = "Не вводите числа и символы!";
            }

            else if (newuserentry.FirstName == "")
            {
                Error_Label.Text = "Вы забыли ввести имя!";
            }

            else if ((hasSpecialChar(newuserentry.MiddleName) == true) || (hasNums(newuserentry.MiddleName) == true))
            {
                Error_Label.Text = "Не вводите числа и символы!";
            }

            else if (newuserentry.MiddleName == "")
            {
                Error_Label.Text = "Вы забыли ввести отчество!";
            }

            else if ((IsValidEmail(newuserentry.Email) == false))
            {
                Error_Label.Text = "Неправильная электронная почта!";
            }

            else if (newuserentry.Email == "")
            {
                Error_Label.Text = "Вы забыли ввести электронную почту!";
            }

            else if ((hasSpecialChar(newuserentry.Phone.ToString()) == true) || (hasLetters(newuserentry.Phone.ToString()) == true) || (newuserentry.Phone.ToString().Contains(".")) || (newuserentry.Phone.ToString().Contains("-")) || (newuserentry.Phone.ToString().Contains("?")))
            {
                Error_Label.Text = "Не вводите буквы и символы!";
            }

            else if (newuserentry.Phone == "")
            {
                Error_Label.Text = "Вы забыли ввести номер телефона!";
            }

            else
            {
                Error_Label.Text = "";
                RestClient client = new RestClient("https://localhost:44357/api/");
                var request = new RestRequest("users/", Method.POST);
                request.AddJsonBody(newuserentry);
                client.Execute(request);

                gvUsersLibrary.EditIndex = -1;
                gvUsersLibrary.DataSource = PopulateDataTable(command);
                gvUsersLibrary.DataBind();
            }

           
        }

        protected void gvUsersLibrary_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUsersLibrary.EditIndex = e.NewEditIndex;
            gvUsersLibrary.DataSource = PopulateDataTable(command);
            gvUsersLibrary.DataBind();
        }

        protected void gvUsersLibrary_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            User newuserentry = new User();
            newuserentry.ID = Int16.Parse(((gvUsersLibrary.Rows[e.RowIndex].FindControl("lbl_ID") as Label).Text).ToString());
            newuserentry.LastName = (gvUsersLibrary.Rows[e.RowIndex].FindControl("txt_LastName") as TextBox).Text;
            newuserentry.FirstName = (gvUsersLibrary.Rows[e.RowIndex].FindControl("txt_FirstName") as TextBox).Text;
            newuserentry.MiddleName = (gvUsersLibrary.Rows[e.RowIndex].FindControl("txt_MiddleName") as TextBox).Text;
            newuserentry.Email = (gvUsersLibrary.Rows[e.RowIndex].FindControl("txt_Email") as TextBox).Text;
            newuserentry.Phone = (gvUsersLibrary.Rows[e.RowIndex].FindControl("txt_Phone") as TextBox).Text;
            newuserentry.UserLogin = (gvUsersLibrary.Rows[e.RowIndex].FindControl("txt_UserLogin") as TextBox).Text;
            newuserentry.UserPassword = (gvUsersLibrary.Rows[e.RowIndex].FindControl("txt_UserPassword") as TextBox).Text;
            newuserentry.AdminPrivilege = ((gvUsersLibrary.Rows[e.RowIndex].FindControl("ddl_Admin") as DropDownList).Text.CompareTo("True") == 0) ? true : false;
            newuserentry.Active = ((gvUsersLibrary.Rows[e.RowIndex].FindControl("ddl_Active") as DropDownList).Text.CompareTo("True") == 0) ? true : false;

            if ((hasSpecialChar(newuserentry.LastName) == true) || (hasNums(newuserentry.LastName) == true)) {
                Error_Label.Text = "Не вводите числа и символы!";
            }

            else if (newuserentry.LastName == "")
            {
                Error_Label.Text = "Вы забыли ввести фамилию!";
            }

            else if ((hasSpecialChar(newuserentry.FirstName) == true) || (hasNums(newuserentry.FirstName) == true))
            {
                Error_Label.Text = "Не вводите числа и символы!";
            }

            else if (newuserentry.FirstName == "")
            {
                Error_Label.Text = "Вы забыли ввести имя!";
            }

            else if ((hasSpecialChar(newuserentry.MiddleName) == true) || (hasNums(newuserentry.MiddleName) == true))
            {
                Error_Label.Text = "Не вводите числа и символы!";
            }

            else if (newuserentry.MiddleName == "")
            {
                Error_Label.Text = "Вы забыли ввести отчество!";
            }

            else if ((IsValidEmail(newuserentry.Email) == false))
            {
                Error_Label.Text = "Неправильная электронная почта!";
            }

            else if (newuserentry.Email == "")
            {
                Error_Label.Text = "Вы забыли ввести электронную почту!";
            }

            else if ((hasSpecialChar(newuserentry.Phone.ToString()) == true) || (hasLetters(newuserentry.Phone.ToString()) == true) || (newuserentry.Phone.ToString().Contains(".")) || (newuserentry.Phone.ToString().Contains("-")) || (newuserentry.Phone.ToString().Contains("?")))
            {
                Error_Label.Text = "Не вводите буквы и символы!";
            }

            else if (newuserentry.Phone == "")
            {
                Error_Label.Text = "Вы забыли ввести номер телефона!";
            }

            else {
                Error_Label.Text = "";
                RestClient client = new RestClient("https://localhost:44357/api/");
                var request = new RestRequest("users/", Method.PUT);
                request.AddJsonBody(newuserentry);
                client.Execute(request);

                gvUsersLibrary.EditIndex = -1;
                gvUsersLibrary.DataSource = PopulateDataTable(command);
                gvUsersLibrary.DataBind();
            }

            
        }

        public static bool hasSpecialChar(string input)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{};<>_+~`";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }

        public static bool hasNums(string input)
        {
            string specialChar = @"1234567890";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }

        public static bool hasLetters(string input)
        {
            string specialChar = @"zxcvbnmlkjhgfdsaqwertyuiop";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        protected void gvUsersLibrary_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsersLibrary.EditIndex = -1;
            gvUsersLibrary.DataSource = PopulateDataTable(command);
            gvUsersLibrary.DataBind();
        }

        protected void gvUsersLibrary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsersLibrary.PageIndex = e.NewPageIndex;
            gvUsersLibrary.DataSource = PopulateDataTable(command);
            gvUsersLibrary.DataBind();
        }

        protected void gvUsersLibrary_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = Int16.Parse(((gvUsersLibrary.Rows[e.RowIndex].FindControl("lbl_ID") as Label).Text).ToString());

            RestClient client = new RestClient("https://localhost:44357/api/");
            var request = new RestRequest("user/Delete/" + ID.ToString(), Method.DELETE);
            client.Execute(request);

            gvUsersLibrary.EditIndex = -1;
            gvUsersLibrary.DataSource = PopulateDataTable(command);
            gvUsersLibrary.DataBind();
        }

        protected void gvUsersLibrary_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (dir == SortDirection.Ascending)
            {
                dir = SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                dir = SortDirection.Ascending;
                sortingDirection = "Asc";
            }
            DataTable dt = new DataTable();
            dt = (DataTable)Session["DatatblUsers"];
            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            gvUsersLibrary.DataSource = sortedView;
            gvUsersLibrary.DataBind();

        }

        public SortDirection dir
        {
            get
            {
                if (ViewState["dirState"] == null)
                {
                    ViewState["dirState"] = SortDirection.Ascending;
                }
                return (SortDirection)(ViewState["dirState"]);
            }
            set
            {
                ViewState["dirState"] = value;
            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("LoginPage.aspx");
        }

        protected void gvUsersLibrary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlAdmins = (e.Row.FindControl("ddl_Admin") as DropDownList);
                if (ddlAdmins != null)
                { 
                    ddlAdmins.DataSource = TrueFalse;
                    ddlAdmins.DataBind();
                }
                DropDownList ddlActive = (e.Row.FindControl("ddl_Active") as DropDownList);
                if (ddlActive != null)
                {
                    ddlActive.DataSource = TrueFalse;
                    ddlActive.DataBind();
                }
            }
        }

        protected void gvUsersLibrary_DataBound(object sender, EventArgs e)
        {
            if (gvUsersLibrary != null)
            {
                DropDownList ddl_Active_New = gvUsersLibrary.FooterRow.FindControl("ddl_Active_New") as DropDownList;
                if (ddl_Active_New != null)
                {
                    ddl_Active_New.DataSource = TrueFalse;
                    ddl_Active_New.DataBind();
                }

                DropDownList ddl_Admin_new = gvUsersLibrary.FooterRow.FindControl("ddl_Admin_new") as DropDownList;
                if (ddl_Admin_new != null)
                {
                    ddl_Admin_new.DataSource = TrueFalse;
                    ddl_Admin_new.DataBind();
                }
            }
        }

        protected void LoadLibrary_Click(object sender, EventArgs e)
        {
            string LoadPath = (FindControl("LoadLibraryFromFile") as TextBox).Text;
            if (LoadPath != "")
            {
                try
                {
                    //Create COM Objects. Create a COM object for everything that is referenced
                    Excel.Application xlApp = new Excel.Application();
                    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@LoadLibraryFromFile.Text);
                    Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                    Excel.Range xlRange = xlWorksheet.UsedRange;

                    int rowCount = xlRange.Rows.Count;
                    int colCount = xlRange.Columns.Count;

                    // iterate over the rows and columns and print to the console as it appears in the file
                    //excel is not zero based!!
                    for (int i = 1; i <= rowCount; i++)
                    {
                        UserIn newuserentry = new UserIn();
                        if (xlRange.Cells[i, 1] != null && xlRange.Cells[i, 1].Value2 != null)
                            newuserentry.LastName = xlRange.Cells[i, 1].Value2.ToString();
                        if (xlRange.Cells[i, 2] != null && xlRange.Cells[i, 2].Value2 != null)
                            newuserentry.FirstName = xlRange.Cells[i, 2].Value2.ToString();
                        if (xlRange.Cells[i, 3] != null && xlRange.Cells[i, 3].Value2 != null)
                            newuserentry.MiddleName = xlRange.Cells[i, 3].Value2.ToString();
                        if (xlRange.Cells[i, 4] != null && xlRange.Cells[i, 4].Value2 != null)
                            newuserentry.Email = xlRange.Cells[i, 4].Value2.ToString();
                        if (xlRange.Cells[i, 5] != null && xlRange.Cells[i, 5].Value2 != null)
                            newuserentry.Phone = xlRange.Cells[i, 5].Value2.ToString();
                        if (xlRange.Cells[i, 6] != null && xlRange.Cells[i, 6].Value2 != null)
                            newuserentry.UserLogin = xlRange.Cells[i, 6].Value2.ToString();
                        if (xlRange.Cells[i, 7] != null && xlRange.Cells[i, 7].Value2 != null)
                            newuserentry.UserPassword = xlRange.Cells[i, 7].Value2.ToString();
                        if (xlRange.Cells[i, 8] != null && xlRange.Cells[i, 8].Value2 != null)
                            newuserentry.AdminPrivilege = Int32.Parse(xlRange.Cells[i, 8].Value2.ToString()) == 1 ? true : false;
                        if (xlRange.Cells[i, 9] != null && xlRange.Cells[i, 9].Value2 != null)
                            newuserentry.Active = Int32.Parse(xlRange.Cells[i, 9].Value2.ToString()) == 1 ? true : false;

                        RestClient client = new RestClient("https://localhost:44357/api/");
                        var request = new RestRequest("users/", Method.POST);
                        request.AddJsonBody(newuserentry);
                        client.Execute(request);
                    }
                    gvUsersLibrary.EditIndex = -1;
                    Session["DatatblUsers"] = gvUsersLibrary.DataSource = PopulateDataTable(command);
                    gvUsersLibrary.DataBind();
                }
                catch (Exception ex)
                {
                    Error_Label.Text = "Не могу загрузить базу пользователей! Проверьте имя и директорию!";
                }
            }
            else
            {
                Error_Label.Text = "Укажите имя файла, содержащего базу пользователей.";
            }

        }

        protected void DeleteMultiple_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvrow in gvUsersLibrary.Rows)
            {

                CheckBox chck = gvrow.FindControl("CheckBox1") as CheckBox;
                if (chck.Checked)
                {
                    var Label = gvrow.FindControl("Label1") as Label;

                    int ID = Int16.Parse(((gvrow.FindControl("lbl_ID") as Label).Text).ToString());

                    RestClient client = new RestClient("https://localhost:44357/api/");
                    var request = new RestRequest("user/Delete/" + ID.ToString(), Method.DELETE);
                    client.Execute(request);

                    gvUsersLibrary.EditIndex = -1;
                    Session["DatatblUsers"] = gvUsersLibrary.DataSource = PopulateDataTable(command);
                    gvUsersLibrary.DataBind();


                }
            }
            command = "https://localhost:44357/api/users";
            Session["DatatblUsers"] = gvUsersLibrary.DataSource = PopulateDataTable(command);
            gvUsersLibrary.DataBind();

        }
    }

}