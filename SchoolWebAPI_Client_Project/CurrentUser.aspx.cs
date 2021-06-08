using Newtonsoft.Json;
using RestSharp;
using SchoolWebAPIproject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolWebAPI_Client_Project
{
    public partial class CurrentUser : System.Web.UI.Page
    {
        static IDictionary<int, string> dictUsers;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Session["fullname"] as string))
                {
                    string command = "https://localhost:44357/api/books/GetByUserID/" + Session["UserID"].ToString();
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
        private DataTable PopulateDataTable(string command)
        {
            DataTable dtable = new DataTable();
            dtable.Clear();
            dtable.Columns.Add("Id");
            dtable.Columns.Add("Автор");
            dtable.Columns.Add("Название");
            dtable.Columns.Add("ISBN");
            dtable.Columns.Add("Страницы");
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
                    drow["Взято"] = book.CheckedOut.ToString("yyyy-MM-dd");
                    drow["Вернуть"] = book.CheckedIn.ToString("yyyy-MM-dd");
                    dtable.Rows.Add(drow);
                }
            }

            return dtable;
        }
        protected void LogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("LoginPage.aspx");
        }
        protected void SearchAuthor_Click(object sender, EventArgs e)
        {
            string command = "https://localhost:44357/api/books/GetAllAuthors/" + AuthorEnter.Text;
            Session["Datatbl"] = gvSchoolLibrary.DataSource = PopulateDataTable(command);
            gvSchoolLibrary.DataBind();
        }

        protected void SearchTitle_Click(object sender, EventArgs e)
        {
            string command = "https://localhost:44357/api/books/GetAllTitles/" + TitleEnter.Text;
            Session["Datatbl"] = gvSchoolLibrary.DataSource = PopulateDataTable(command);
            gvSchoolLibrary.DataBind();
        }

        protected void SearchISBN_Click(object sender, EventArgs e)
        {
            string command = "https://localhost:44357/api/books/GetAllISBN/" + ISBN_Enter.Text;
            Session["Datatbl"] = gvSchoolLibrary.DataSource = PopulateDataTable(command);
            gvSchoolLibrary.DataBind();
        }

        protected void ResetLibrary_Click(object sender, EventArgs e)
        {
            string command = "https://localhost:44357/api/books";
            Session["Datatbl"] = gvSchoolLibrary.DataSource = PopulateDataTable(command);
            gvSchoolLibrary.DataBind();
        }
        protected void ShowCurrentUserBooks_Click(object sender, EventArgs e)
        {
            string command = "https://localhost:44357/api/books/GetByUserID/" + Session["UserID"].ToString();
            Session["Datatbl"] = gvSchoolLibrary.DataSource = PopulateDataTable(command);
            gvSchoolLibrary.DataBind();
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

        protected void gvSchoolLibrary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                if ((e.Row.FindControl("lbl_CheckedIn")) != null)
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