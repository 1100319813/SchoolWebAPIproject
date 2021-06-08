<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsersIndex.aspx.cs" Inherits="SchoolWebAPI_Client_Project.UsersIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>School Library Users</title>
</head>
<style type="text/css">
    .mydatagrid
{
width: 80%;
min-width: 80%;
position: absolute;
top: 278px; 
left: -459px;
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
}
    #form1 {
        height: 273px;
        width: 1304px;
    }
</style>

<body style="background-image: url('gabriel-sollmann-Y7d265_7i08-unsplash.jpg')">
    <asp:Label 
            ID="Title" 
            runat="server" 
            Text="Пользователи библиотеки"
            Font-Weight="Bold"
            Font-Names="Arial"
            Font-Size="XX-Large"
            style="font-weight: bold; position:absolute; float:left; margin-left:676px; top: 33px; left: -121px; height: 41px; width: 600px;"
            /> 



    <div class="sidenav">
    <form id="form1" runat="server" style="background-color: #BCD7DC;">
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Logout_Click" Text="Выйти" OnClientClick="return confirm('Вы уверены, что хотите выйти?');" BorderColor="#ff8000" ForeColor="#ff8000" Width="137px" Height="40px" style="margin-left: 18px; font-weight: bold; border-radius: 2px 2px 2px 2px; border: 2px solid #ff8000; position:absolute; float:left;"/>
        <asp:Label ID="Hello_Label" runat="server" Font-Names="Arial" ForeColor="Green" Text="Hello" style="font-weight: bold; position:absolute; margin-left: 20px; margin-top: 0px; top: 34px; left: 165px; width: 354px; height: 38px;"></asp:Label>

        <br />
        <br />
        <asp:Label 
            ID="Label2" 
            runat="server" 
            Text="Имя: "
            Font-Names="Arial"
            style="font-weight: bold; position:absolute; float:left; margin-left:10px; top: 114px; left: 10px; height: 19px; margin-bottom: 10px;" Font-Size="10pt"
            />

        <asp:Label 
            ID="Label1" 
            runat="server" 
            Text="Фамилия: "
            Font-Names="Arial"
            style="font-weight: bold; position:absolute; float:left; margin-left:10px; top: 111px; left: 343px; height: 23px; width: 104px;" Font-Size="10pt"
            />
        <asp:TextBox 
            ID="LastNameEnter" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Font-Names="Arial"
            style="border-radius: 2px 2px 2px 2px; border: 2px solid #000000; position:absolute; float:left; margin-left:155px; top: 114px; left: 302px; width: 235px; right: 752px;" Font-Size="10pt"
            />

        <asp:Label 
            ID="Label3" 
            runat="server" 
            Text="Отчество: "
            Font-Names="Arial"
            style="font-weight: bold; position:absolute; float:left; margin-left:10px; top: 110px; left: 733px; width: 50px;" Font-Size="10pt"
            />

         <asp:Button ID="DeleteMultiple" runat="server" Text="Удалить выбранных" OnClientClick="return confirm('Вы уверены, что хотите удалить всех выбранных пользователей?');" OnClick="DeleteMultiple_Click"
        BorderColor="#ff8000"
            ForeColor="#ff8000"
            Font-Names="Arial"
            style="font-weight: bold; border-radius: 2px 2px 2px 2px; border: 2px solid #ff8000; cursor: pointer; position:absolute; float:left; margin-left:500px; top: 93px; left: 665px; height: 80px; width: 145px;" 
            Font-Size="9pt"
        />
 
        <asp:Label 
            ID="Label5" 
            runat="server" 
            Text="Телефон: "
            Font-Names="Arial"
            style="font-weight: bold; position:absolute; float:left; margin-left:10px; top: 157px; left: 341px; width: 90px; height: 27px;" Font-Size="10pt"
            />
        <asp:TextBox 
            ID="PhoneEnter" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Font-Names="Arial"
            style="border-radius: 2px 2px 2px 2px; border: 2px solid #000000; position:absolute; float:left; margin-left:155px; top: 158px; left: 305px; width: 235px; height: 19px;" Font-Size="10pt"
            />

        <asp:Label 
            ID="Label4" 
            runat="server" 
            Text="Email: "
            Font-Names="Arial"
            style="font-weight: bold; position:absolute; float:left; margin-left:10px; top: 158px; left: 10px; right: 1431px;" Font-Size="10pt"
            />
        <asp:TextBox 
            ID="EmailEnter" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Font-Names="Arial"
            style="border-radius: 2px 2px 2px 2px; border: 2px solid #000000; position:absolute; float:left; margin-left:155px; top: 158px; left: -68px; width: 236px; right: 1121px;" Font-Size="10pt"
            />

        <asp:Button 
           ID="SearchAll" 
            runat="server" 
            Text="Поиск" 
            Font-Size="10pt"
            BorderColor="#ff8000"
            ForeColor="#ff8000"
            Font-Names="Arial"
            style="font-weight: bold; border-radius: 2px 2px 2px 2px; border: 2px solid #ff8000; position:absolute; float:left; margin-left:390px; top: 152px; left: 356px; height: 33px; width: 404px; right: 302px;" 
            OnClick="SearchAll_Click"
            />
        
       

        <br />
        <asp:TextBox 
            ID="MiddleNameEnter" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Font-Names="Arial"
            style="border-radius: 2px 2px 2px 2px; border: 2px solid #000000; position:absolute; float:left; margin-left:155px; top: 110px; left: 696px; width: 292px; right: 309px;" 
            />
 
        <asp:TextBox 
            ID="FirstNameEnter" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Font-Names="Arial"
            style="border-radius: 2px 2px 2px 2px; border: 2px solid #000000; position:absolute; float:left; margin-left:12px; top: 111px; left: 77px; width: 234px; right: 1129px;" 
            />

        <br />
        <br />
        <br />
        <br />
        <br />
        <br /><br />
        <br />
        <br />
        <br />

        
        
        <asp:Label 
            ID="Error_Label" 
            Font-Names="Arial" 
            Font-Size="14pt" 
            runat="server" 
            ForeColor="Red" 
            Width="800px" 
            Height="28px" 
            style="font-weight: bold;"
            />

        <br />
        <br />

        <br />
        <br />
 
        <asp:Button 
           ID="ResetUsers" 
            runat="server" 
            Text="Показать всех пользователей" 
            BorderColor="#ff8000"
            ForeColor="#ff8000"
            Font-Names="Arial"
            Font-Size="10pt"
            style="font-weight: bold; border-radius: 2px 2px 2px 2px; border: 2px solid #ff8000; position:absolute; float:left; margin-left:50px; top: 206px; left: 327px; width: 273px; height: 49px;" 
            OnClick="ResetUsers_Click"
            />

        <asp:CheckBox ID="OnlyActive" 
            runat="server" 
            Text="Только действующие"
            Font-Size="10pt"
            BorderColor="#ff8000"
            ForeColor="#ff8000"
            Font-Names="Arial"
            style="font-weight: bold; border-radius: 2px 2px 2px 2px; border: 2px solid #ff8000; position:absolute; float:left; margin-left:50px; top: 212px; left: 612px; width: 128px; height: 43px; right: 716px;" 
            />
        

        <asp:Button 
           ID="LoadLibrary" 
            runat="server" 
            Text="Загрузить библиотеку" 
            BorderColor="#ff8000"
            ForeColor="#ff8000"
            Font-Names="Arial"
            Font-Size="10pt"
            style="font-weight: bold; border-radius: 2px 2px 2px 2px; border: 2px solid #ff8000; position:absolute; float:left; margin-left:10px; margin-top:11px; top: 193px; left: 1098px; width: 192px; height: 44px;" 
            OnClick="LoadLibrary_Click"
            />

        <asp:Button 
           ID="ShowLibrary" 
            runat="server" 
            Text="Показать всю библиотеку" 
            BorderColor="#ff8000"
            ForeColor="#ff8000"
            Height="50"
            Font-Names="Arial"
            Font-Size="10pt"
            style="font-weight: bold; border-radius: 2px 2px 2px 2px; border: 2px solid #ff8000; position:absolute; float:left; margin-left:50px; top: 204px; left: 40px; width: 266px;" 
            OnClick="ShowLibrary_Click"
            />
        <br />
        <asp:TextBox 
            ID="LoadLibraryFromFile" 
            runat="server" 
            style="border-radius: 2px 2px 2px 2px; border: 2px solid #000000; position:absolute; float:left; margin-left:10px; margin-top:11px; top: 201px; left: 803px; width: 278px; height: 30px;"
            />

        <br />
        <br />
        <br />
        <br />
        <br />
        <br />

        <br />
        <br />

<asp:GridView 
            ID="gvUsersLibrary" 
            runat="server"
            style="position:absolute; float:left; margin-left:480px; margin-top:34px; height: auto;"     
            AllowPaging="true"
            PageSize="10"
            AutoGenerateColumns="false"
            ShowFooter="true"
            OnRowEditing="gvUsersLibrary_RowEditing"
            OnRowUpdating="gvUsersLibrary_RowUpdating"
            OnRowCancelingEdit="gvUsersLibrary_RowCancelingEdit"
            OnPageIndexChanging="gvUsersLibrary_PageIndexChanging"
            OnRowDeleting="gvUsersLibrary_RowDeleting"
            OnRowDataBound="gvUsersLibrary_RowDataBound"
            OnDataBound="gvUsersLibrary_DataBound"
            AllowSorting="true"
            OnSorting="gvUsersLibrary_Sorting"
            CssClass="mydatagrid" 
            PagerStyle-CssClass="pager"
            HeaderStyle-CssClass="header" 
            RowStyle-CssClass="rows"
            >
            <Columns>
                <asp:TemplateField HeaderText="Id" >
                    <ItemTemplate>  
                        <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("Id") %>'></asp:Label>  
                    </ItemTemplate>  
                    <FooterTemplate>
                        <asp:Label ID="lbl_ID_New" runat="server" Text='<%#Eval("Id") %>'></asp:Label>  
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Фамилия" HeaderStyle-HorizontalAlign="Left" SortExpression="Фамилия">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_LastName" runat="server" Text='<%#Eval("Фамилия") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txt_LastName" runat="server" Text='<%#Eval("Фамилия") %>'></asp:TextBox>  
                    </EditItemTemplate>                     
                    <FooterTemplate>
                        <asp:Textbox ID="txt_LastName_New" runat="server" Text='<%#Eval("Фамилия") %>'></asp:Textbox>  
                    </FooterTemplate>

                </asp:TemplateField>  

                <asp:TemplateField HeaderText="Имя" HeaderStyle-HorizontalAlign="Left" SortExpression="Имя">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_FirstName" runat="server" Text='<%#Eval("Имя") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txt_FirstName" runat="server" Text='<%#Eval("Имя") %>'></asp:TextBox>  
                    </EditItemTemplate>  
                    <FooterTemplate>
                        <asp:Textbox ID="txt_FirstName_New" runat="server" Text='<%#Eval("Имя") %>'></asp:Textbox>  
                    </FooterTemplate>
                </asp:TemplateField>  

                <asp:TemplateField HeaderText="Отчество" HeaderStyle-HorizontalAlign="Left" SortExpression="Отчество">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_MiddleName" runat="server" Text='<%#Eval("Отчество") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txt_MiddleName" runat="server" Text='<%#Eval("Отчество") %>'></asp:TextBox>  
                    </EditItemTemplate>  
                    <FooterTemplate>
                        <asp:Textbox ID="txt_MiddleName_New" runat="server" Text='<%#Eval("Отчество") %>'></asp:Textbox>  
                    </FooterTemplate>

                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Email" HeaderStyle-HorizontalAlign="Left" SortExpression="Email">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Email" runat="server" Text='<%#Eval("Email") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txt_Email" runat="server" Text='<%#Eval("Email") %>'></asp:TextBox>  
                    </EditItemTemplate>                    
                    <FooterTemplate>
                        <asp:Textbox ID="txt_Email_New" runat="server" Text='<%#Eval("Email") %>'></asp:Textbox>  
                    </FooterTemplate>

                </asp:TemplateField>

                <asp:TemplateField HeaderText="Телефон" HeaderStyle-HorizontalAlign="Left" SortExpression="Телефон">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Phone" runat="server" Width="80px"  Text='<%#Eval("Телефон") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txt_Phone" runat="server" Width="80px" Text='<%#Eval("Телефон") %>'></asp:TextBox>  
                    </EditItemTemplate>                    
                    <FooterTemplate>
                        <asp:Textbox ID="txt_Phone_New" runat="server" Width="80px" Text='<%#Eval("Телефон") %>'></asp:Textbox>  
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="UserLogin" HeaderStyle-HorizontalAlign="Left" SortExpression="UserLogin">  
                    <ControlStyle Width="50px" />
                    <ItemTemplate>  
                        <asp:Label ID="lbl_UserLogin" runat="server" Width="50px" Text='<%#Eval("UserLogin") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txt_UserLogin" runat="server" Width="50px" Text='<%#Eval("UserLogin") %>'></asp:TextBox>  
                    </EditItemTemplate>                    
                    <FooterTemplate>
                        <asp:Textbox ID="txt_UserLogin_New" runat="server" Width="50px" Text='<%#Eval("UserLogin") %>'></asp:Textbox>  
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Password" HeaderStyle-HorizontalAlign="Left" >  
                    <ControlStyle Width="50px" />
                    <ItemTemplate>  
                        <asp:Label ID="lbl_UserPassword" runat="server" Width="50px" Text='<%#Eval("UserPassword") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txt_UserPassword" runat="server" Width="50px" Text='<%#Eval("UserPassword") %>'></asp:TextBox>  
                    </EditItemTemplate>                    
                    <FooterTemplate>
                        <asp:Textbox ID="txt_UserPassword_New" runat="server" Width="50px" Text='<%#Eval("UserPassword") %>'></asp:Textbox>  
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Админ" SortExpression="Администратор">  
                    <ControlStyle Width="50px" />
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Admin" runat="server" Width="50px"  Text='<%#Eval("Администратор") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:DropDownList ID="ddl_Admin" Width="50px" runat="server" ></asp:DropDownList>  
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddl_Admin_new" Width="50px" runat="server" ></asp:DropDownList>  
                    </FooterTemplate>
                </asp:TemplateField> 

                <asp:TemplateField HeaderText="Active"  SortExpression="Active">  
                    <ControlStyle Width="50px" />
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Active" runat="server" Width="50px"  Text='<%#Eval("Active") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:DropDownList ID="ddl_Active" runat="server" Width="50px" ></asp:DropDownList>  
                    </EditItemTemplate>                    
                    <FooterTemplate>
                        <asp:DropDownList ID="ddl_Active_New" runat="server" Width="50px" ></asp:DropDownList>  
                    </FooterTemplate>
                </asp:TemplateField>

               <asp:TemplateField>  
                    <ItemTemplate>  
                        <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />                        
                    </ItemTemplate>                         
                    <EditItemTemplate>  
                        <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update"/>                         
                    </EditItemTemplate>
                    <FooterTemplate>  
                        <asp:Button ID="btn_Add_User" runat="server" Text="Add" CommandName="Add" OnClick="btn_Add_User_Click"/>  
                    </FooterTemplate>
                </asp:TemplateField> 

                <asp:TemplateField>
                    <ItemTemplate>                          
                        <asp:Button ID="btn_Delete" runat="server" Text="Delete" CommandName="Delete" OnRowDataBound="OnRowDataBound" OnClientClick="return confirm('Вы уверены, что хотите удалить?');"/>
                    </ItemTemplate>                         
                    <EditItemTemplate>                          
                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel"/>  
                    </EditItemTemplate>
                    <FooterTemplate>  
                        <asp:Button ID="btn_Clean" runat="server" Text="Clean" CommandName="Clean" />  
                    </FooterTemplate>
                </asp:TemplateField> 

                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" >  
                    <ItemTemplate>  
                        <asp:CheckBox ID="CheckBox1" runat="server" Width="50px" ></asp:CheckBox>  
                    </ItemTemplate>  
                  <EditItemTemplate>  
                        <asp:CheckBox ID="CheckBox1" runat="server" Width="50px" ></asp:CheckBox> 
                    </EditItemTemplate>               
                </asp:TemplateField>

            </Columns>


        </asp:GridView>
 
        <br />
        <br />
        </div>

    </form>
</body>
</html>
