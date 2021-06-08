<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SchoolWebAPI_Client_Project.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>School Library</title>
</head>
<style type="text/css">
    .GridPosition
    {
    	position:absolute;
    	left:100px;	
    	width:200px;
     }
</style>
<body>
    <form id="form1" runat="server">
        <asp:Label 
            ID="Title" 
            runat="server" 
            Text="School Library"
            Font-Names="Arial"
            Font-Size="X-Large"
            Font-Weight="Bold"
            style="position:absolute; float:left; margin-left:300px;" 
            />
        <br />
        <br />
        <br />
        <br />
        <asp:Label 
            ID="Label1" 
            runat="server" 
            Text="Enter Author: "
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:94px;" 
            />
        <asp:TextBox 
            ID="AuthorEnter" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Width="300"
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:204px;" 
            />
        <asp:Button 
           ID="SearchAuthor" 
            runat="server" 
            Text="Search" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Width="100"
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:550px;" 
            OnClick="SearchAuthor_Click"
            />

        <asp:Label 
            ID="Label4" 
            runat="server" 
            Text="Search Anything: "
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:704px;" 
            />
        <asp:TextBox 
            ID="AnythingEnter" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Width="300"
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:870px;" 
            />
        <asp:Button 
           ID="SearchAnything" 
            runat="server" 
            Text="Search" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Width="100"
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:1200px;" 
            OnClick="SearchAnything_Click"
            />

        <br />
        <br />
        <br />
        <br />
        <asp:Label 
            ID="Label2" 
            runat="server" 
            Text="Enter Title: "
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:94px;" 
            />
        <asp:TextBox 
            ID="TitleEnter" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Width="300"
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:204px;" 
            />
        <asp:Button 
           ID="SearchTitle" 
            runat="server" 
            Text="Search" 
            BorderColor="Gray"
            ForeColor="Black"
            Width="100"
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:550px;" 
            OnClick="SearchTitle_Click"
            />
        <br />
        <br />
        <br />
        <br />
        <asp:Label 
            ID="Label3" 
            runat="server" 
            Text="Enter ISBN: "
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:94px;" 
            />
        <asp:TextBox 
            ID="ISBN_Enter" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Width="300"
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:204px;" 
            />
        <asp:Button 
           ID="SearchISBN" 
            runat="server" 
            Text="Search" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Width="100"
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:550px;" 
            OnClick="SearchISBN_Click"
            />
        <br />
        <br />
        <br />
        <br />
        <br />

        <asp:Label 
            ID="Label5" 
            runat="server" 
            Text="Enter Author: "
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:94px;" 
            />
        <asp:TextBox 
            ID="EnterAuthor2" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Width="150"
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:204px;" 
            />

        <asp:Label 
            ID="Label6" 
            runat="server" 
            Text="Enter Title: "
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:404px;" 
            />
        <asp:TextBox 
            ID="EnterTitle2" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Width="150"
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:504px;" 
            />

        <asp:Label 
            ID="Label7" 
            runat="server" 
            Text="Enter ISBN: "
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:704px;" 
            />
        <asp:TextBox 
            ID="Enter_ISBN_2" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Width="150"
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:804px;" 
            />


        <asp:Button 
           ID="AllParameterSearch" 
            runat="server" 
            Text="Search" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Width="100"
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:1004px;" 
            OnClick="AllParameterSearch_Click"
            />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />

        <asp:Button 
           ID="ResetLibrary" 
            runat="server" 
            Text="Show all library data" 
            BorderColor="Gray"
            ForeColor="Black"
            Width="575"
            Height="50"
            Font-Names="Arial"
            Font-Size="Medium"
            style="position:absolute; float:left; margin-left:94px;" 
            OnClick="ResetLibrary_Click"
            />
        <br />
        <br />
        <br />
        <br />
        <asp:GridView 
            ID="gvSchoolLibrary" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Width="575"
            Font-Names="Arial"
            CssClass="GridPosition"
            AllowPaging="true"
            OnPageIndexChanging="gvSchoolLibrary_PageIndexChanging"
            PageSize="10"
            >
            <HeaderStyle BackColor="BlanchedAlmond" Height="30" />
            <RowStyle BackColor="Lightgray" />
            <AlternatingRowStyle BackColor= "WhiteSmoke"/>
        </asp:GridView>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </form>
</body>
</html>