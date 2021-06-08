using SchoolWebAPIproject.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolWebAPI_Client_Project
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            RestClient client = new RestClient("https://localhost:44357/api/User/");
            string requestquery = "GetLogin?username=" + txtUserName.Text.Trim();
            var request = new RestRequest(requestquery, Method.GET);
            IRestResponse<User> response = client.Execute<User>(request);

            if (response.Content.CompareTo("null") == 0)
                lblErrorMessage.Visible = true;
            else
            {
                User user = JsonConvert.DeserializeObject<User>(response.Content);
                if (user.UserPassword.CompareTo(txtPassword.Text.Trim()) == 0)
                {
                    Session["username"] = txtUserName.Text.Trim();
                    Session["fullname"] = user.LastName.ToString() + " " + user.FirstName.ToString() + " " + user.MiddleName.ToString();
                    Session["admin"] = user.AdminPrivilege.ToString();
                    Session["UserID"] = user.ID.ToString();
                    if (Session["admin"].ToString().CompareTo("True") == 0)
                        Response.Redirect("index.aspx");
                    else
                        Response.Redirect("currentuser.aspx");
                }
                else
                    lblErrorMessage.Visible = true;
            }
        }
    }
}