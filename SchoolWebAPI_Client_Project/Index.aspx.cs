using AForge.Video.DirectShow;
using Newtonsoft.Json;
using RestSharp;
using SchoolWebAPIproject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using ZXing;
using Excel = Microsoft.Office.Interop.Excel;

namespace SchoolWebAPI_Client_Project
{
    public partial class Index : System.Web.UI.Page
    {
        static string command = "https://localhost:44357/api/books";
        static IDictionary<int, string> dictUsers;
        static IList<string> listUsers;
        static List<string> listUsersSorted;

        public static string changeableImageUrl;
        public static string barcodeValue;

        static FilterInfoCollection filterInfoCollection;
        static VideoCaptureDevice videoCaptureDevice;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Session["username"] as string))
                {
                    changeableImageUrl = "~/Images/Temp.bmp?" + DateTime.Now.Ticks.ToString();
                    barcodeValue = "";

                    Image1.Visible = false;

                    this.DataBind(); //bind data to the page
                    Timer1.Enabled = false;

                    filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                    foreach( FilterInfo device in filterInfoCollection)
                    {
                        string n = device.Name;
                    }
                    Hello_Label.Text = "Добро Пожаловать, " + Session["fullname"].ToString() + "!";
                    Session["Datatbl"] = gvSchoolLibrary.DataSource = PopulateDataTable(command);
                    gvSchoolLibrary.DataBind();
                }
                else
                {
                    Response.Redirect("LoginPage.aspx");
                }
            }
        }

        protected void Page_UnLoad(object sender, EventArgs e)
        {
            //if (videoCaptureDevice != null)
            //{
            //    if (videoCaptureDevice.IsRunning)
            //    {
            //        videoCaptureDevice.Stop();
            //    }
            //}
        }

        private void PopulateUsersDictionary()
        {
            if (dictUsers == null)
            {
                RestClient client = new RestClient("https://localhost:44357/api/");
                var request = new RestRequest("Users", Method.GET);
                IRestResponse<List<User>> answer = client.Execute<List<User>>(request);
                List<User> myusers = answer.Data;

                dictUsers = new Dictionary<int, string>();
                listUsers = new List<string>();
                listUsersSorted = new List<string>();

                dictUsers.Add(0, "Available");
                listUsers.Add("Available");

                foreach (User usr in myusers)
                {
                    dictUsers.Add(usr.ID, usr.LastName + " " + usr.FirstName + " " + usr.MiddleName);
                    listUsers.Add(usr.LastName + " " + usr.FirstName + " " + usr.MiddleName);
                }
                listUsersSorted = listUsers.OrderBy(q => q).ToList();
            }
        }

        private DataTable PopulateDataTable(string command)
        {
            PopulateUsersDictionary();

            DataTable dtable = new DataTable();
            dtable.Clear();
            dtable.Columns.Add("Id");
            dtable.Columns.Add("Автор");
            dtable.Columns.Add("Название");
            dtable.Columns.Add("ISBN");
            dtable.Columns.Add("Страницы");
            dtable.Columns.Add("Пользователь");
            dtable.Columns.Add("Взято");
            dtable.Columns.Add("Вернуть");

            RestClient client = new RestClient(command);
            var request = new RestRequest(Method.GET);
            IRestResponse<List<Book>> response = client.Execute<List<Book>>(request);
            List<Book> mybooks = response.Data;
            var count = mybooks.Count();
            if (count == 0)
            {
                Error_Label.Text = "Результатов не найдено";
            }
            else
            {
                Error_Label.Text = "";
                foreach (Book book in mybooks)
                {
                    DataRow drow = dtable.NewRow();
                    drow["Id"] = book.ID;
                    drow["Автор"] = book.Author;
                    drow["Название"] = book.Title;
                    drow["ISBN"] = book.ISBN;
                    drow["Страницы"] = book.Pages;
                    drow["Пользователь"] = dictUsers[book.UserId];
                    drow["Взято"] = book.CheckedOut.ToString("yyyy-MM-dd");
                    drow["Вернуть"] = book.CheckedIn.ToString("yyyy-MM-dd");
                    dtable.Rows.Add(drow);
                }
            }

            return dtable;
        }

        protected void ResetLibrary_Click(object sender, EventArgs e)
        {
            command = "https://localhost:44357/api/books";
            Session["Datatbl"] = gvSchoolLibrary.DataSource = PopulateDataTable(command);
            gvSchoolLibrary.DataBind();
        }

        protected void gvSchoolLibrary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSchoolLibrary.PageIndex = e.NewPageIndex;
            Session["Datatbl"] = gvSchoolLibrary.DataSource = PopulateDataTable(command);
            gvSchoolLibrary.DataBind();
        }

        protected void gvSchoolLibrary_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSchoolLibrary.EditIndex = e.NewEditIndex;
            Session["Datatbl"] = gvSchoolLibrary.DataSource = PopulateDataTable(command);
            gvSchoolLibrary.DataBind();

        }

        public static bool hasSpecialChar(string input)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}-;<>_,+~`";
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
            string specialChar = @"zxcvbnmlkjhgfdsaqwertyuiopQWERTYUIOPASDFGHJKLZXCVBNM";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }

        protected void gvSchoolLibrary_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Book newbookentry = new Book();

            newbookentry.ID = Int16.Parse(((gvSchoolLibrary.Rows[e.RowIndex].FindControl("lbl_ID") as Label).Text).ToString());
            newbookentry.Author = (gvSchoolLibrary.Rows[e.RowIndex].FindControl("txt_Author") as TextBox).Text;
            newbookentry.Title = (gvSchoolLibrary.Rows[e.RowIndex].FindControl("txt_Title") as TextBox).Text;
            newbookentry.ISBN = (gvSchoolLibrary.Rows[e.RowIndex].FindControl("txt_ISBN") as TextBox).Text;
            newbookentry.Pages = Int32.Parse(((gvSchoolLibrary.Rows[e.RowIndex].FindControl("txt_Pages") as TextBox).Text).ToString());
            

            DateTime dt = DateTime.Parse((gvSchoolLibrary.Rows[e.RowIndex].FindControl("Calendar2") as Calendar).SelectedDate.ToShortDateString());
            string str = Convert.ToDateTime(dt.ToShortDateString()).ToString("yyyy-MM-dd");
            newbookentry.CheckedIn = DateTime.ParseExact(str, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

            dt = DateTime.Parse((gvSchoolLibrary.Rows[e.RowIndex].FindControl("Calendar1") as Calendar).SelectedDate.ToShortDateString());
            str = Convert.ToDateTime(dt.ToShortDateString()).ToString("yyyy-MM-dd");
            newbookentry.CheckedOut = DateTime.ParseExact(str, "yyyy-MM-dd", null);

            string user = (gvSchoolLibrary.Rows[e.RowIndex].FindControl("ddl_Users") as DropDownList).Text;
            newbookentry.UserId = dictUsers.FirstOrDefault(x => x.Value == user).Key;

            if ((hasNums(newbookentry.Author) == true) || (hasSpecialChar(newbookentry.Author) == true))
            {
                Error_Label.Text = "Не вводите числа и символы!";
            }

            else if ((newbookentry.Author == ""))
            {
                Error_Label.Text = "Вы забыли ввести автора!";
            }

            else if ((newbookentry.Title == ""))
            {
                Error_Label.Text = "Вы забыли ввести название!";
            }

            else if ((hasSpecialChar(newbookentry.ISBN.ToString()) == true) || (hasLetters(newbookentry.ISBN.ToString()) == true) || (newbookentry.ISBN.ToString().Contains(".")) || (newbookentry.ISBN.ToString().Contains("-")) || (newbookentry.ISBN.ToString().Contains("?")))
            {
                Error_Label.Text = "Не вводите буквы и символы!";
            }

            else if ((newbookentry.ISBN == ""))
            {
                Error_Label.Text = "Вы забыли ввести ISBN!";
            }

            else
            {
                Error_Label.Text = "";
                RestClient client = new RestClient("https://localhost:44357/api/");
                var request = new RestRequest("books/", Method.PUT);
                request.AddJsonBody(newbookentry);
                client.Execute(request);

                gvSchoolLibrary.EditIndex = -1;
                Session["Datatbl"] = gvSchoolLibrary.DataSource = PopulateDataTable(command);
                gvSchoolLibrary.DataBind();
            }            
        }

        protected void gvSchoolLibrary_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSchoolLibrary.EditIndex = -1;
            Session["Datatbl"] = gvSchoolLibrary.DataSource = PopulateDataTable(command);
            gvSchoolLibrary.DataBind();
        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            int amt;
            string val = ((gvSchoolLibrary.FooterRow.FindControl("txt_Book_Amount") as TextBox).Text).ToString();
            if (val == "")
            {
                amt = 1;
            }
            else {
                amt = Int32.Parse((gvSchoolLibrary.FooterRow.FindControl("txt_Book_Amount") as TextBox).Text);
            }
            var author = (gvSchoolLibrary.FooterRow.FindControl("txt_Author_New") as TextBox).Text;
            var title = (gvSchoolLibrary.FooterRow.FindControl("txt_Title_New") as TextBox).Text;
            var isbn = (gvSchoolLibrary.FooterRow.FindControl("txt_ISBN_New") as TextBox).Text;
            var page = (gvSchoolLibrary.FooterRow.FindControl("txt_Pages_New") as TextBox).Text;

            for (int i = 0; i < amt; i++) {
                BookIn newbookentry = new BookIn();

                newbookentry.Author = author;
                newbookentry.Title = title;
                newbookentry.ISBN = isbn;
                string pagesstr = page;
                newbookentry.Pages = (pagesstr != "") ? Int32.Parse((pagesstr).ToString()) : 0;

                if ((hasNums(newbookentry.Author) == true) || (hasSpecialChar(newbookentry.Author) == true))
                {
                    Error_Label.Text = "Не вводите числа и символы!";
                }

                else if ((newbookentry.Author == ""))
                {
                    Error_Label.Text = "Вы забыли ввести автора!";
                }

                else if ((newbookentry.Title == ""))
                {
                    Error_Label.Text = "Вы забыли ввести название!";
                }

                else if ((hasSpecialChar(newbookentry.ISBN.ToString()) == true) || (hasLetters(newbookentry.ISBN.ToString()) == true) || (newbookentry.ISBN.ToString().Contains(".")) || (newbookentry.ISBN.ToString().Contains("-")) || (newbookentry.ISBN.ToString().Contains("?")))
                {
                    Error_Label.Text = "Не вводите буквы и символы!";
                }

                else if ((newbookentry.ISBN == ""))
                {
                    Error_Label.Text = "Вы забыли ввести ISBN!";
                }

                else
                {
                    RestClient client = new RestClient("https://localhost:44357/api/");
                    var request = new RestRequest("books/", Method.POST);
                    request.AddJsonBody(newbookentry);
                    client.Execute(request);

                    gvSchoolLibrary.EditIndex = -1;
                    Session["Datatbl"] = gvSchoolLibrary.DataSource = PopulateDataTable(command);
                    gvSchoolLibrary.DataBind();
                }
            }
            
        }

        protected void gvSchoolLibrary_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = Int16.Parse(((gvSchoolLibrary.Rows[e.RowIndex].FindControl("lbl_ID") as Label).Text).ToString());

            RestClient client = new RestClient("https://localhost:44357/api/");
            var request = new RestRequest("books/Delete/" + ID.ToString(), Method.DELETE);
            client.Execute(request);

            gvSchoolLibrary.EditIndex = -1;
            Session["Datatbl"] = gvSchoolLibrary.DataSource = PopulateDataTable(command);
            gvSchoolLibrary.DataBind();
        }

        protected void ShowUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect("UsersIndex.aspx");
        }

        protected void SearchAll_Click(object sender, EventArgs e)
        {
            string title = (TitleEnter as TextBox).Text == "" ? "" : (TitleEnter as TextBox).Text;
            string isbn = (ISBN_Enter as TextBox).Text == "" ? "" : (ISBN_Enter as TextBox).Text;
            string author = (AuthorEnter as TextBox).Text == "" ? "" : (AuthorEnter as TextBox).Text;
            string command = "https://localhost:44357/api/books/getbooks" + "?Author=" + author +
                                                                     "&Title=" + title +
                                                                     "&ISBN=" + isbn;

            Session["Datatbl"] = gvSchoolLibrary.DataSource = PopulateDataTable(command);
            gvSchoolLibrary.DataBind();
        }

        protected void gvSchoolLibrary_Sorting(object sender, GridViewSortEventArgs e)
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
            dt = (DataTable)Session["Datatbl"];
            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            gvSchoolLibrary.DataSource = sortedView;
            gvSchoolLibrary.DataBind();

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

        protected void LogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("LoginPage.aspx");
        }

        protected void AuthorEnter_TextChanged(object sender, EventArgs e)
        {

        }

        protected void gvSchoolLibrary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlUsers = (e.Row.FindControl("ddl_Users") as DropDownList);
                if (ddlUsers != null)
                {
                    ddlUsers.DataSource = listUsersSorted;
                    ddlUsers.DataBind();
                }
            }

            if (e.Row.RowIndex >= 0)
            {
                if ((e.Row.FindControl("lbl_CheckedIn")) != null)
                {
                    if ((e.Row.FindControl("lbl_Users")) != null)
                    {
                        if (((e.Row.FindControl("lbl_Users") as Label).Text) != "Available")
                        {
                            if (DateTime.Parse((e.Row.FindControl("lbl_CheckedIn") as Label).Text) < DateTime.Today)
                            {
                                e.Row.Cells[6].BackColor = System.Drawing.Color.Red;
                                e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            string Isbn = (gvSchoolLibrary.FooterRow.FindControl("txt_ISBN_New") as TextBox).Text;
            string Author = "";
            string Title = "";
            string Pages = "";
            string InfoUrl = "";
            if (Isbn == null) {
                    return;
            }
            else
            {
                if (SearchBookOnInternet(Isbn, out Author, out Title, out Pages, out InfoUrl) == true)
                {
                    (gvSchoolLibrary.FooterRow.FindControl("txt_Author_New") as TextBox).Text = Author;
                    (gvSchoolLibrary.FooterRow.FindControl("txt_Title_New") as TextBox).Text = Title;
                    (gvSchoolLibrary.FooterRow.FindControl("txt_Pages_New") as TextBox).Text = Pages;
                    Error_Label.Text = "";
                }
                else
                {
                    Error_Label.Text = "Не можем найти информацию о книге в googlebook.com!";
                }

            }
        }

        protected void LoadLibrary_Click(object sender, EventArgs e)
        {
            string LoadPath = (FindControl("LoadLibraryFromFile") as TextBox).Text;
            if (LoadPath != "")
            {
                //Create COM Objects. Create a COM object for everything that is referenced
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(LoadLibraryFromFile.Text);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;

                // iterate over the rows and columns and print to the console as it appears in the file
                //excel is not zero based!!
                for (int i = 1; i <= rowCount; i++)
                {
                    BookIn newbookentry = new BookIn();
                    if (xlRange.Cells[i, 1] != null && xlRange.Cells[i, 1].Value2 != null)
                        newbookentry.Title = xlRange.Cells[i, 1].Value2.ToString();
                    if (xlRange.Cells[i, 2] != null && xlRange.Cells[i, 2].Value2 != null)
                        newbookentry.Author = xlRange.Cells[i, 2].Value2.ToString();
                    if (xlRange.Cells[i, 3] != null && xlRange.Cells[i, 3].Value2 != null)
                        newbookentry.ISBN = xlRange.Cells[i, 3].Value2.ToString();
                    if (xlRange.Cells[i, 4] != null && xlRange.Cells[i, 4].Value2 != null)
                        newbookentry.Pages = Int32.Parse(xlRange.Cells[i, 4].Value2.ToString());

                    RestClient client = new RestClient("https://localhost:44357/api/");
                    var request = new RestRequest("books/", Method.POST);
                    request.AddJsonBody(newbookentry);
                    client.Execute(request);
                }
                gvSchoolLibrary.EditIndex = -1;
                Session["Datatbl"] = gvSchoolLibrary.DataSource = PopulateDataTable(command);
                gvSchoolLibrary.DataBind();
            }

        }

        protected bool SearchBookOnInternet(string Isbn, out string Author, out string Title, out string Pages, out string InfoUrl) {
            var url = "https://www.googleapis.com/books/v1/volumes?q=isbn:" + Isbn;
            IRestClient cli = new RestClient(url);
            IRestRequest req = new RestRequest(Method.GET);
            var resp = cli.Execute(req);
            dynamic jsonResponse = JsonConvert.DeserializeObject(resp.Content);

            Author = "";
            Title = "";
            Pages = "";
            InfoUrl = "";

            if (jsonResponse.totalItems != "0")
            {
                if (jsonResponse.items[0].volumeInfo.authors != null)
                    Author = jsonResponse.items[0].volumeInfo.authors[0];
                if (jsonResponse.items[0].volumeInfo.title != null)
                    Title = jsonResponse.items[0].volumeInfo.title; ;
                if (jsonResponse.items[0].volumeInfo.pageCount != null)
                    Pages = jsonResponse.items[0].volumeInfo.pageCount;
                if (jsonResponse.items[0].volumeInfo.infoLink != null)
                    InfoUrl = jsonResponse.items[0].volumeInfo.infoLink.ToString();
                return true;
            }
            else {
 
                return false;
            }

        }

        protected void btn_Info_Click1(object sender, EventArgs e)
        {
            FindISBN((gvSchoolLibrary.FooterRow.FindControl("txt_ISBN_New") as TextBox).Text);
        }

        protected void btn_Info_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Button).NamingContainer;
            var isbn = (gvSchoolLibrary.Rows[row.RowIndex].FindControl("lbl_ISBN") as Label).Text;
            string Author = "";
            string Title = "";
            string Pages = "";
            string InfoUrl = "";
            //FindISBN((gvSchoolLibrary.Rows[row.RowIndex].FindControl("lbl_ISBN") as Label).Text);

            if (isbn == null)
            {
                return;
            }
            else
            {
                if (SearchBookOnInternet(isbn, out Author, out Title, out Pages, out InfoUrl) == true)
                {
                    Error_Label.Text = "";
                    Process.Start("chrome.exe", "-noframemerging " + InfoUrl);
                }
                else
                {
                    Error_Label.Text = "Не можем найти информацию о книге в googlebook.com!";
                }

            }

        }

        protected void FindISBN(string ISBN)
        {
            string Author = "";
            string Title = "";
            string Pages = "";
            string InfoUrl = "";
            if (ISBN == null)
            {
                return;
            }
            else
            {
                if (SearchBookOnInternet(ISBN, out Author, out Title, out Pages, out InfoUrl) == true)
                {

                    (gvSchoolLibrary.FooterRow.FindControl("txt_Author_New") as TextBox).Text = Author;
                    (gvSchoolLibrary.FooterRow.FindControl("txt_Title_New") as TextBox).Text = Title;
                    (gvSchoolLibrary.FooterRow.FindControl("txt_Pages_New") as TextBox).Text = Pages;
                    Error_Label.Text = "";
                    Process.Start("chrome.exe", "-noframemerging " + InfoUrl);
                }
                else
                {
                    Error_Label.Text = "Не можем найти информацию о книге в googlebook.com!";
                }

            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[0].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();
            Timer1.Enabled = true;
            Image1.Visible = true;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            // Stop Scanning
            if (videoCaptureDevice != null)
            {
                if (videoCaptureDevice.IsRunning)
                {
                    videoCaptureDevice.Stop();
                }
            }
            Timer1.Enabled = false;
            Image1.Visible = false;
        }

        protected void VideoCaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            string FileName = "Temp" + ".bmp";

            bitmap.Save(Server.MapPath(@"~\images\" + FileName), ImageFormat.Bmp);

            ImageConverter converter = new ImageConverter();
            byte[] img = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));

            BarcodeReader reader = new BarcodeReader();
            var result = reader.Decode(bitmap);
            if (result != null)
            {
                barcodeValue= result.ToString();
                Console.Beep();
            }

            changeableImageUrl = "~/Images/Temp.bmp?" + DateTime.Now.Ticks.ToString();
            this.DataBind(); //bind data to the page
        }

        protected void gettickvalue(object sender, EventArgs e)
        {
             Image1.ImageUrl = changeableImageUrl;
            (gvSchoolLibrary.FooterRow.FindControl("txt_ISBN_New") as TextBox).Text = barcodeValue;
        }

        protected void DeleteMultiple_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvrow in gvSchoolLibrary.Rows)
            {

                CheckBox chck = gvrow.FindControl("CheckBox1") as CheckBox;
                if (chck.Checked)
                {
                    var Label = gvrow.FindControl("Label1") as Label;

                    int ID = Int16.Parse(((gvrow.FindControl("lbl_ID") as Label).Text).ToString());

                    RestClient client = new RestClient("https://localhost:44357/api/");
                    var request = new RestRequest("books/Delete/" + ID.ToString(), Method.DELETE);
                    client.Execute(request);

                    gvSchoolLibrary.EditIndex = -1;
                    Session["Datatbl"] = gvSchoolLibrary.DataSource = PopulateDataTable(command);
                    gvSchoolLibrary.DataBind();


                }
            }
            command = "https://localhost:44357/api/books";
            Session["Datatbl"] = gvSchoolLibrary.DataSource = PopulateDataTable(command);
            gvSchoolLibrary.DataBind();

        }
    }
}