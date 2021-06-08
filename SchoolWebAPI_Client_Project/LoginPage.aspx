<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="SchoolWebAPI_Client_Project.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style >
        body {
            background-color #646464;
        }
    </style>
</head>
<body style="background-image: url('gabriel-sollmann-Y7d265_7i08-unsplash.jpg')">
    <form id="form1" runat="server">
        <div style="background-color: #FFFFFF; margin-top: 161px;">
            <table style="margin:auto; border: 5px solid white; width: 430px;">
                <tr>
                    <td>
                        <asp:Label ID="UsernameLbl" runat="server" Text="Username"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="PasswordLbl" runat="server" Text="Password"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                    </td>
                    <td>
                        <asp:Label ID="lblErrorMessage" runat="server" Text="Incorrect User Credentials" ForeColor="red"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
