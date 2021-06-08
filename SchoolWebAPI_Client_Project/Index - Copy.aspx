<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SchoolWebAPI_Client_Project.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>School Library</title>
</head>
<style type="text/css">
    .mydatagrid
{
width: 80%;
border: solid 2px black;
min-width: 80%;
position: absolute;
top: 194px; 
left: -490px;
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
text-align: left;
border: none 0px transparent;
}
.rows:hover
{
background-color: #ff8000;
font-family: Arial;
color: #fff;
text-align: left;
}
.selectedrow
{
background-color: #ff8000;
font-family: Arial;
color: #fff;
font-weight: bold;
text-align: left;
}
.mydatagrid a /* FOR THE PAGING ICONS */
{
background-color: Transparent;
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
background-color: #c9c9c9;
color: #000;
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
            Font-Names="Arial"
            Font-Size="X-Large"
            Font-Weight="Bold"
            style="position:absolute; float:left; margin-left:476px; top: 33px; left: -121px; height: 41px;" 
            /> 
    <div class="sidenav">
    <form id="form1" runat="server">
        <br />

        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="margin-left: 16px" Text="Log out" Width="115px" />

        <br />
        <br />
        <br />
        <asp:Label 
            ID="Label1" 
            runat="server" 
            Text="Автор"
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:10px; top: 96px; left: 22px; width: 65px;" Font-Size="10pt" 
            />
        <asp:TextBox 
            ID="AuthorEnter" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:10px; top: 96px; left: 91px; width: 159px;" Font-Size="10pt" OnTextChanged="AuthorEnter_TextChanged" 
            />
        <asp:Button 
           ID="SearchAll" 
            runat="server" 
            Text="Поиск" 
            BorderColor="Gray"
            ForeColor="Black"
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:330px; top: 142px; left: 388px; height: 31px; width: 312px;" 
            OnClick="SearchAll_Click" Font-Size="10pt"
            />

        <br />
        <br />
        <br />
        <br />
        <asp:Label 
            ID="Label2" 
            runat="server" 
            Text="Название"
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:10px; top: 96px; left: 272px;" Font-Size="10pt" 
            />
        <asp:TextBox 
            ID="TitleEnter" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:2px; top: 97px; left: 378px; width: 188px;" Font-Size="10pt" 
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
            style="position:absolute; float:left; margin-left:10px; top: 98px; left: 583px; width: 105px;" Font-Size="10pt" 
            />
        <asp:TextBox 
            ID="ISBN_Enter" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Font-Names="Arial"
            style="position:absolute; float:left; margin-left:4px; top: 94px; left: 705px; width: 188px;" Font-Size="10pt" 
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
            BorderColor="Gray"
            ForeColor="Black"
            Height="50"
            Font-Names="Arial"
            Font-Size="10pt"
            style="position:absolute; float:left; margin-left:10px; top: 131px; left: 26px; width: 307px;" 
            OnClick="ResetLibrary_Click"
            />
        <br />
        <br />
        <br />
        <br />

        <asp:Button 
           ID="ShowUsers" 
            runat="server" 
            Text="Показать всех пользователей" 
            BorderColor="Gray"
            ForeColor="Black"
            Height="50"
            Font-Names="Arial"
            Font-Size="10pt"
            style="position:absolute; float:left; margin-left:10px; margin-top:11px; top: 122px; left: 353px; width: 313px;" 
            OnClick="ShowUsers_Click"
            />
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
            Width="775"
            style="position:absolute; float:left; margin-left:500px; margin-top:14px; height: auto"     
            AllowPaging="true"
            PageSize="10"
            AutoGenerateColumns="false"
            ShowFooter="true"
            OnRowEditing="gvSchoolLibrary_RowEditing"
            OnRowUpdating="gvSchoolLibrary_RowUpdating"
            OnRowCancelingEdit="gvSchoolLibrary_RowCancelingEdit"
            OnPageIndexChanging="gvSchoolLibrary_PageIndexChanging"
            OnRowDeleting="gvSchoolLibrary_RowDeleting"
            AllowSorting="true"
            OnSorting="gvSchoolLibrary_Sorting"
            CssClass="mydatagrid" 
            PagerStyle-CssClass="pager"
            HeaderStyle-CssClass="header" 
            RowStyle-CssClass="rows"
            >            
            <Columns>

                <asp:TemplateField HeaderText="Id" SortExpression="Id">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("Id") %>'></asp:Label>  
                    </ItemTemplate>  

                    <FooterTemplate>
                        <asp:Label ID="lbl_ID_New" runat="server" Text='<%#Eval("Id") %>'></asp:Label>  
                    </FooterTemplate>

                </asp:TemplateField>  

                <asp:TemplateField HeaderText="Автор" SortExpression="Автор">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Author" runat="server" Text='<%#Eval("Автор") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txt_Author" runat="server" Text='<%#Eval("Автор") %>'></asp:TextBox>  
                    </EditItemTemplate> 
                    
                    <FooterTemplate>
                        <asp:Textbox ID="txt_Author_New" runat="server" Text='<%#Eval("Автор") %>'></asp:Textbox>  
                    </FooterTemplate>

                </asp:TemplateField>  

                <asp:TemplateField HeaderText="Название" SortExpression="Название">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Title" runat="server" Text='<%#Eval("Название") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txt_Title" runat="server" Text='<%#Eval("Название") %>'></asp:TextBox>  
                    </EditItemTemplate>  

                    <FooterTemplate>
                        <asp:Textbox ID="txt_Title_New" runat="server" Text='<%#Eval("Название") %>'></asp:Textbox>  
                    </FooterTemplate>

                </asp:TemplateField>  

                <asp:TemplateField HeaderText="ISBN" SortExpression="ISBN">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_ISBN" runat="server" Text='<%#Eval("ISBN") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txt_ISBN" runat="server" Text='<%#Eval("ISBN") %>'></asp:TextBox>  
                    </EditItemTemplate>  

                    <FooterTemplate>
                        <asp:Textbox ID="txt_ISBN_New" runat="server" Text='<%#Eval("ISBN") %>'></asp:Textbox>  
                    </FooterTemplate>

                </asp:TemplateField>  

                <asp:TemplateField HeaderText="Количество страниц" SortExpression="Количество страниц">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Pages" runat="server" Text='<%#Eval("Количество страниц") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txt_Pages" runat="server" Text='<%#Eval("Количество страниц") %>'></asp:TextBox>  
                    </EditItemTemplate>
                    
                    <FooterTemplate>
                        <asp:Textbox ID="txt_Pages_New" runat="server" Text='<%#Eval("Количество страниц") %>'></asp:Textbox>  
                    </FooterTemplate>

                </asp:TemplateField> 
                

                <asp:TemplateField HeaderText="Пользователь">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Users" runat="server" Text='<%#Eval("Пользователь") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txt_Users" runat="server" Text='<%#Eval("Пользователь") %>'></asp:TextBox>  
                    </EditItemTemplate>
                </asp:TemplateField> 
                    

                 <asp:TemplateField>  
                    <ItemTemplate>  
                        <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />
                        
                    </ItemTemplate>  
                       
                    <EditItemTemplate>  
                        <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update"/>  
                        
                    </EditItemTemplate>

                    <FooterTemplate>  
                        <asp:Button ID="btn_Add" runat="server" Text="Add" CommandName="Add" OnClick="btn_Add_Click"/>  
                    </FooterTemplate>

                </asp:TemplateField> 

                <asp:TemplateField>
                    <ItemTemplate>  
                        
                        <asp:Button ID="btn_Delete" runat="server" Text="Delete" CommandName="Delete" OnRowDataBound="OnRowDataBound"/>
                    </ItemTemplate>  
                       
                    <EditItemTemplate>  
                         
                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel"/>  
                    </EditItemTemplate>

                    <FooterTemplate>  
                        <asp:Button ID="btn_Clean" runat="server" Text="Clean" CommandName="Clean" />  
                    </FooterTemplate>
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