<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CurrentUser.aspx.cs" Inherits="SchoolWebAPI_Client_Project.CurrentUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<style type="text/css">
    .mydatagrid
{
width: 80%;
min-width: 80%;
position: absolute;
top: 194px; 
left: -490px;
background-color: #646464
}
.header
{
background-color: #646464;
font-family: Arial;
color: White;
border: none 0px transparent;
height: 25px;
text-align: center;
font-size: 16px;
}

.rows
{
background-color: #fff;
font-family: Arial;
font-size: 14px;
color: #000;
min-height: 10px;
max-height: 25px;
text-align: left;
}
.rows:hover
{
background-color: #ff8000;
font-family: Arial;
text-align: left;
}
.selectedrow
{
background-color: #ff8000;
font-family: Arial;
font-weight: bold;
text-align: left;
}
.mydatagrid a /* FOR THE PAGING ICONS */
{
padding: 5px 5px 5px 5px;
color: #fff;
text-decoration: none;
font-weight: bold;
}

.mydatagrid a:hover /** FOR THE PAGING ICONS HOVER STYLES**/
{
background-color: #000;
color: #fff;
}
.mydatagrid span /* FOR THE PAGING ICONS CURRENT PAGE INDICATOR */
{
background-color: #ffffff;
padding: 5px 5px 5px 5px;
}
.pager
{
background-color: #646464;
font-family: Arial;
color: White;
height: 30px;
text-align: left;
}

.mydatagrid td
{
padding: 5px;
}
.mydatagrid th
{
padding: 5px;
}</style>
<body>
    <asp:Label 
            ID="Title" 
            runat="server" 
            Text="Школьная библиотека"
            Font-Weight="Bold"
            Font-Names="Arial"
            Font-Size="XX-Large"
            style="position:absolute; float:left; margin-left:676px; top: 33px; left: -121px; height: 41px;" 
            /> 
    <div class="sidenav">
    <form id="form1" runat="server">
        <br />

        <asp:Button ID="Button1" runat="server" OnClick="LogOut_Click" BorderColor="#ff8000" ForeColor="#ff8000" Width="137px" Height="40px" style="margin-left: 18px; font-weight: bold; border-radius: 2px 2px 2px 2px; border: 2px solid #ff8000;" Text="Выйти" />
        <asp:Label ID="Hello_Label" runat="server" Font-Names="Arial" ForeColor="Green" Text="Hello" style="font-weight: bold; position:absolute; margin-left: 20px; margin-top: 0px; top: 34px; left: 165px; width: 354px; height: 38px;"></asp:Label>
        
        <br />
        <br />
        
        <asp:Button 
           ID="SearchAll" 
            runat="server" 
            Text="Поиск" 
            BorderColor="#ff8000"
            ForeColor="#ff8000"
            Font-Size="10pt"
            Font-Names="Arial"
            OnClick="SearchAll_Click" style="height: 33px; font-weight: bold; border-radius: 2px 2px 2px 2px; border: 2px solid #ff8000; margin-left: 850px; margin-top: 0px;" Width="103px"
            />        

        <br />
        <asp:Label 
            ID="Label1" 
            runat="server" 
            Text="Автор"
            Font-Names="Arial"
            style="font-weight: bold; position:absolute; float:left; margin-left:10px; top: 98px; left: 5px; width: 67px; height: 18px;" Font-Size="10pt" 
            />
        <asp:TextBox 
            ID="AuthorEnter" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Font-Names="Arial"
            style="border-radius: 2px 2px 2px 2px; border: 2px solid #000000; position:absolute; float:left; margin-left:10px; top: 96px; left: 80px; width: 158px;" Font-Size="10pt"  
            />

        <br />
        <asp:Button 
           ID="ShowUserBooks" 
            runat="server" 
            Text="Показать мои книги" 
            BorderColor="#ff8000"
            ForeColor="#ff8000"
            Height="50"
            Font-Names="Arial"
            Font-Size="10pt"
            style="font-weight: bold; border-radius: 2px 2px 2px 2px; border: 2px solid #ff8000; position:absolute; float:left; margin-left:400px; top: 131px; left: -19px; width: 317px;" 
            OnClick="ShowCurrentUserBooks_Click"
            />
        <br />
        <br />
        <br />
        <asp:Label ID="Error_Label" Font-Names="Arial" Font-Size="14pt" runat="server" ForeColor="Red" Width="800px" Height="28px" style="font-weight: bold;"></asp:Label>

        <br />
        <asp:Label 
            ID="Label2" 
            runat="server" 
            Text="Название"
            Font-Names="Arial"
            style="font-weight: bold; position:absolute; float:left; margin-left:10px; top: 96px; left: 272px;" Font-Size="10pt" 
            />
        <asp:TextBox 
            ID="TitleEnter" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Font-Names="Arial"
            style="border-radius: 2px 2px 2px 2px; border: 2px solid #000000; position:absolute; float:left; margin-left:2px; top: 97px; left: 365px; width: 196px;" Font-Size="10pt" 
            />
        <br />
        <br />
        <br />
        <br />
        <asp:Label 
            ID="Label3" 
            runat="server" 
            Text="ISBN: "
            Font-Names="Arial"
            style="font-weight: bold; position:absolute; float:left; margin-left:10px; top: 98px; left: 583px; width: 105px;" Font-Size="10pt" 
            />
        <asp:TextBox 
            ID="ISBN_Enter" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Font-Names="Arial"
            style="border-radius: 2px 2px 2px 2px; border: 2px solid #000000; position:absolute; float:left; margin-left:4px; top: 94px; left: 640px; width: 185px;" Font-Size="10pt" 
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
            Text="Показать всю библиотеку" 
            BorderColor="#ff8000"
            ForeColor="#ff8000"
            Height="50"
            Font-Names="Arial"
            Font-Size="10pt"
            style="font-weight: bold; border-radius: 2px 2px 2px 2px; border: 2px solid #ff8000; position:absolute; float:left; margin-left:10px; top: 131px; left: 26px; width: 307px;" 
            OnClick="ResetLibrary_Click"
            />
        <br />
        <br />
        <br />
        <br />

        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        </div>
       
<asp:GridView 
            ID="gvSchoolLibrary" 
            runat="server"
            style="position:absolute; float:left; margin-left:500px; margin-top:34px; height: auto; width: auto"     
            AllowPaging="true"
            PageSize="10"
            AutoGenerateColumns="false"
            ShowFooter="true"
            AllowSorting="true"
            CssClass="mydatagrid" 
            PagerStyle-CssClass="pager"
            HeaderStyle-CssClass="header" 
            RowStyle-CssClass="rows"
            OnRowDataBound="gvSchoolLibrary_RowDataBound"
            >            
            <Columns>

                <asp:TemplateField HeaderText="Id" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="40px" SortExpression="Id">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("Id") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  

                <asp:TemplateField HeaderText="Автор" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="40px" SortExpression="Автор">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Author" runat="server" Width="200px"  Text='<%#Eval("Автор") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  

                <asp:TemplateField HeaderText="Название" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="80px" SortExpression="Название">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Title" runat="server" Width="200px" Text='<%#Eval("Название") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  

                <asp:TemplateField HeaderText="ISBN" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="80px" SortExpression="ISBN">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_ISBN" runat="server" Width="100px" Text='<%#Eval("ISBN") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  

                <asp:TemplateField HeaderText="Страницы" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="80px" SortExpression="Страницы">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Pages" runat="server" Width="60px" Text='<%#Eval("Страницы") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField> 
                
                <asp:TemplateField HeaderText="Взято" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="60px" SortExpression="Взято">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_CheckedOut" runat="server" Width="80px" Text='<%#Eval("Взято") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>                 

                <asp:TemplateField HeaderText="Вернуть" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="80px" SortExpression="Вернуть">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_CheckedIn" runat="server" Width="80px" Text='<%#Eval("Вернуть") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>                 



           </Columns>

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