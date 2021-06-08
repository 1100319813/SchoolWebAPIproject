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
    public partial class Index : System.Web.UI.Page
    {
        string command = "https://localhost:44357/api/books";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvSchoolLibrary.DataSource = PopulateDataTable(command);
                gvSchoolLibrary.DataBind();


            }

        }

        private DataTable PopulateDataTable(string command)
        {
            DataTable dtable = new DataTable();
            dtable.Clear();
            dtable.Columns.Add("Id");
            dtable.Columns.Add("Author");
            dtable.Columns.Add("Title");
            dtable.Columns.Add("ISBN");
            dtable.Columns.Add("Pages");

            System.Net.WebRequest req = System.Net.WebRequest.Create(command);
            req.Method = "GET";
            req.ContentType = "application/json; charset=utf-8";

            System.Net.WebResponse resp = req.GetResponse();
            System.IO.Stream stream = resp.GetResponseStream();

            StreamReader re = new StreamReader(stream);

            String json = re.ReadToEnd();

            List<Book> mybooks = JsonConvert.DeserializeObject<List<Book>>(json);

            foreach (Book book in mybooks)
            {
                DataRow drow = dtable.NewRow();
                drow["Id"] = book.ID;
                drow["Author"] = book.Author;
                drow["Title"] = book.Title;
                drow["ISBN"] = book.ISBN;
                drow["Pages"] = book.Pages;
                dtable.Rows.Add(drow);
            }

            return dtable;
        }

        protected void SearchAuthor_Click(object sender, EventArgs e)
        {
            command = "https://localhost:44357/api/books/GetAllAuthors/" + AuthorEnter.Text;
            gvSchoolLibrary.DataSource = PopulateDataTable(command);
            gvSchoolLibrary.DataBind();
        }

        protected void SearchTitle_Click(object sender, EventArgs e)
        {
            command = "https://localhost:44357/api/books/GetAllTitles/" + TitleEnter.Text;
            gvSchoolLibrary.DataSource = PopulateDataTable(command);
            gvSchoolLibrary.DataBind();
        }

        protected void SearchISBN_Click(object sender, EventArgs e)
        {
            command = "https://localhost:44357/api/books/GetAllISBN/" + ISBN_Enter.Text;
            gvSchoolLibrary.DataSource = PopulateDataTable(command);
            gvSchoolLibrary.DataBind();
        }

        protected void ResetLibrary_Click(object sender, EventArgs e)
        {
            command = "https://localhost:44357/api/books";
            gvSchoolLibrary.DataSource = PopulateDataTable(command);
            gvSchoolLibrary.DataBind();
        }

        protected void gvSchoolLibrary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSchoolLibrary.PageIndex = e.NewPageIndex;
            gvSchoolLibrary.DataSource = PopulateDataTable(command);
            gvSchoolLibrary.DataBind();
        }

        protected void SearchAnything_Click(object sender, EventArgs e)
        {
            command = "https://localhost:44357/api/books/GetAnyBook/" + AnythingEnter.Text;
            gvSchoolLibrary.DataSource = PopulateDataTable(command);
            gvSchoolLibrary.DataBind();
        }

        protected void AllParameterSearch_Click(object sender, EventArgs e)
        {
            command = "https://localhost:44357/api/books/GetAllAuthors/" + EnterAuthor2.Text;
            gvSchoolLibrary.DataSource = PopulateDataTable(command);
            gvSchoolLibrary.DataBind();
            command = "https://localhost:44357/api/books/GetAllTitles/" + EnterTitle2.Text;
            gvSchoolLibrary.DataSource = PopulateDataTable(command);
            gvSchoolLibrary.DataBind();
            command = "https://localhost:44357/api/books/GetAllISBN/" + Enter_ISBN_2.Text;
            gvSchoolLibrary.DataSource = PopulateDataTable(command);
            gvSchoolLibrary.DataBind();
                     
        }
    }
}