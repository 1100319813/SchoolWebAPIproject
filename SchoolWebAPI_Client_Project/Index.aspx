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
min-width: 80%;
position: absolute;
top: 244px; 
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
}

.button:hover {  
     cursor: pointer;  
     filter: progid:DXImageTransform.Microsoft.gradient(startColorstr=' #85B6F0',  
          endColorstr='#579AEB');  
     /* for IE */  
     -ms-filter: progid:DXImageTransform.Microsoft.gradient(startColorstr=' #85B6F0',  
          endColorstr='#579AEB');  
     /* for IE 8 and above */  
     background: -webkit-gradient(linear, left top, left bottom, from(#85B6F0),  
          to(#579AEB));  
     /* for webkit browsers */  
     background: -moz-linear-gradient(top, #85B6F0, #579AEB);  
     /* for firefox 3.6+ */  
     background: -o-linear-gradient(top, #85B6F0, #579AEB);  
     /* for Opera */  
} 
</style>

<body style="background-image: url('gabriel-sollmann-Y7d265_7i08-unsplash.jpg')">
    <asp:Label 
            ID="Title" 
            runat="server" 
            Text="Школьная библиотека"
            Font-Names="Arial"
            Font-Size="XX-Large"
            Font-Weight="Bold"
            style="font-weight: bold; position:absolute; float:left; margin-left:676px; top: 33px; left: -121px; height: 41px; width: 448px;" 
            /> 
    <div class="sidenav" >
    <form id="form1" runat="server" style="background-color: #BCD7DC; height: 205px; width: 1302px;">
        <br />

        <asp:Button ID="Button1" runat="server" OnClick="LogOut_Click" OnClientClick="return confirm('Вы уверены, что хотите выйти?');" BorderColor="#ff8000"
            ForeColor="#ff8000"
            Font-Names="Arial" style="margin-left: 16px; font-family: Arial; border-radius: 2px 2px 2px 2px;  
            border: 2px solid #ff8000; cursor: pointer; font-weight: bold" Text="Выйти" Width="137px" Height="40px" />

        <asp:Label ID="Hello_Label" runat="server" Font-Names="Arial" ForeColor="Green" Text="Hello" Width="361px" style="font-weight: bold; position:absolute; margin-left: 20px; margin-top: 0px"></asp:Label>

        <br />

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Timer ID="Timer1" runat="server" OnTick="gettickvalue" Interval="2000"></asp:Timer>
        <asp:UpdatePanel ID="BannerPanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Image ID="Image1" runat="server" ImageUrl="<%#changeableImageUrl %>" style="margin-left: 949px; margin-top: 0px" Height="62px" Width="111px" />
                
            </ContentTemplate>
        </asp:UpdatePanel>  
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Button 
           ID="SearchAll" 
            runat="server" 
            Text="Поиск" 
            BorderColor="#ff8000"
            ForeColor="#ff8000"
            Font-Names="Arial"
            style="font-weight: bold; border-radius: 2px 2px 2px 2px; border: 2px solid #ff8000; cursor: pointer; position:absolute; float:left; margin-left:330px; top: 93px; left: 515px; height: 27px; width: 82px;" 
            OnClick="SearchAll_Click" Font-Size="10pt"
            />
        <asp:Button ID="DeleteMultiple" runat="server" Text="Удалить выбранные книги" OnClientClick="return confirm('Вы уверены, что хотите удалить все выбранные книги?');" OnClick="DeleteMultiple_Click"
        BorderColor="#ff8000"
            ForeColor="#ff8000"
            Font-Names="Arial"
            style="font-weight: bold; border-radius: 2px 2px 2px 2px; border: 2px solid #ff8000; cursor: pointer; position:absolute; float:left; margin-left:480px; top: 93px; left: 625px; height: 27px; width: 200px;" 
            Font-Size="10pt"
        />
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
        <asp:Label 
            ID="Label1" 
            runat="server" 
            Text="Автор"
            Font-Names="Arial"
            style="font-weight: bold; position:absolute; float:left; margin-left:10px; top: 96px; left: 22px; width: 65px;" Font-Size="10pt" 
            />
        <asp:TextBox 
            ID="AuthorEnter" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Font-Names="Arial"
            style="border-radius: 2px 2px 2px 2px; border: 2px solid #000000; position:absolute; float:left; margin-left:10px; top: 96px; left: 81px; width: 173px;" Font-Size="10pt" OnTextChanged="AuthorEnter_TextChanged" 
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
            style="font-weight: bold; position:absolute; float:left; margin-left:10px; top: 96px; left: 272px;" Font-Size="10pt" 
            />
        <asp:TextBox 
            ID="TitleEnter" 
            runat="server"
            BorderColor="Gray"
            ForeColor="Black"
            Font-Names="Arial"
            style="border-radius: 2px 2px 2px 2px; border: 2px solid #000000; position:absolute; float:left; margin-left:2px; top: 97px; left: 364px; width: 208px;" Font-Size="10pt" 
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
            style="border-radius: 2px 2px 2px 2px; border: 2px solid #000000; position:absolute; float:left; margin-left:4px; top: 94px; left: 642px; width: 173px;" Font-Size="10pt" 
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
            Font-Names="Arial"
            Font-Size="10pt"
            BorderColor="#ff8000"
            ForeColor="#ff8000"
            style="font-weight: bold; border-radius: 2px 2px 2px 2px; border: 2px solid #ff8000; position:absolute; float:left; margin-left:10px; top: 135px; left: 26px; width: 307px; height: 50px; right: 1167px;" 
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
            BorderColor="#ff8000"
            ForeColor="#ff8000"
            Height="50"
            Font-Names="Arial"
            Font-Size="10pt"
            style="font-weight: bold; border-radius: 2px 2px 2px 2px; border: 2px solid #ff8000; position:absolute; float:left; margin-left:10px; margin-top:11px; top: 122px; left: 353px; width: 313px;" 
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
        <asp:Button 
           ID="LoadLibrary" 
            runat="server" 
            Text="Загрузить библиотеку" 
            BorderColor="#ff8000"
            ForeColor="#ff8000"
            Font-Names="Arial"
            Font-Size="10pt"
            style="font-weight: bold; border-radius: 2px 2px 2px 2px; border: 2px solid #ff8000; position:absolute; float:left; margin-left:10px; margin-top:11px; top: 128px; left: 1056px; width: 192px; height: 44px;" 
            OnClick="LoadLibrary_Click"
            />
        <br />
        <br />
        <asp:TextBox 
            ID="LoadLibraryFromFile" 
            runat="server" 
            style="border-radius: 2px 2px 2px 2px; border: 2px solid #000; position:absolute; float:left; margin-left:10px; margin-top:11px; top: 134px; left: 683px; width: 352px; height: 29px;"
            />
        </div>
        
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
    <br />
        <br />
    <br />
        <br />
<asp:GridView 
            ID="gvSchoolLibrary" 
            runat="server"
            Width="775"
            style="position:absolute; float:left; margin-left:500px; margin-top:0px; height: auto;"     
            AllowPaging="true"
            PageSize="10"
            AutoGenerateColumns="false"
            ShowFooter="true"
            OnRowEditing="gvSchoolLibrary_RowEditing"
            OnRowUpdating="gvSchoolLibrary_RowUpdating"
            OnRowCancelingEdit="gvSchoolLibrary_RowCancelingEdit"
            OnPageIndexChanging="gvSchoolLibrary_PageIndexChanging"
            OnRowDeleting="gvSchoolLibrary_RowDeleting"
            OnRowDataBound="gvSchoolLibrary_RowDataBound"
            AllowSorting="true"
            OnSorting="gvSchoolLibrary_Sorting"
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
                </asp:TemplateField>  

                <asp:TemplateField HeaderText="Автор" HeaderStyle-HorizontalAlign="Left" SortExpression="Автор">  
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

                <asp:TemplateField HeaderText="Название" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="80px" SortExpression="Название">  
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

                <asp:TemplateField HeaderText="ISBN" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="80px" >  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_ISBN" runat="server" Width="100px" Text='<%#Eval("ISBN") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txt_ISBN" runat="server" Width="100px" Text='<%#Eval("ISBN") %>'></asp:TextBox>  
                    </EditItemTemplate>  
                    <FooterTemplate>
                        <asp:Textbox ID="txt_ISBN_New" runat="server" Width="100px" Text='<%#Eval("ISBN") %>'></asp:Textbox>  
                    </FooterTemplate>

                </asp:TemplateField>  

                <asp:TemplateField HeaderText="Страницы" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="80px" >  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Pages" runat="server" Width="60px" Text='<%#Eval("Страницы") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txt_Pages" runat="server" Width="60px" Text='<%#Eval("Страницы") %>'></asp:TextBox>  
                    </EditItemTemplate>                    
                    <FooterTemplate>
                        <asp:Textbox ID="txt_Pages_New" runat="server" Width="60px" Text='<%#Eval("Страницы") %>'></asp:Textbox>  
                    </FooterTemplate>
                </asp:TemplateField>                 

                <asp:TemplateField HeaderText="Взято" HeaderStyle-HorizontalAlign="Left" SortExpression="Взято">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_CheckedOut" runat="server" Width="80px" Text='<%#Eval("Взято") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:Calendar ID="Calendar1" runat="server" ></asp:Calendar> 
                    </EditItemTemplate>                    
                </asp:TemplateField>                 

                <asp:TemplateField HeaderText="Вернуть" HeaderStyle-HorizontalAlign="Left" SortExpression="Вернуть">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_CheckedIn" runat="server" Width="80px" Text='<%#Eval("Вернуть") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:Calendar ID="Calendar2" runat="server" ></asp:Calendar>
                    </EditItemTemplate>                    
                </asp:TemplateField>                 

                <asp:TemplateField HeaderText="Пользователь" HeaderStyle-HorizontalAlign="Left" >  
                    <ControlStyle Width="230px" />
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Users" runat="server" Text='<%#Eval("Пользователь") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:DropDownList ID="ddl_Users" runat="server" ></asp:DropDownList>  
                    </EditItemTemplate>

                </asp:TemplateField>                     

                <asp:TemplateField>  
                    <ItemTemplate>  
                        <asp:Button ID="btn_Info" runat="server" Text="Info" OnClick="btn_Info_Click"/>                        
                    </ItemTemplate>                         
                    <EditItemTemplate>  
                        <asp:Button ID="btn_Info" runat="server" Text="Info" />                         
                    </EditItemTemplate>
                    <FooterTemplate>  
                        <asp:Button ID="btn_Info" runat="server" Text="Info" OnClick="btn_Info_Click1"/>  
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
                        <asp:Button ID="btn_Add" runat="server" Text="Add" CommandName="Add" OnClick="btn_Add_Click"/>  
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
                        <asp:Button ID="btn_Scan" runat="server" Text="SCAN" CommandName="Search" OnClick="Button2_Click"/>  
                        <asp:Button ID="btn_StopScanning" runat="server" Text="STOP" CommandName="Search" OnClick="Button3_Click"/>  
                    </FooterTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" >  
                    <ItemTemplate>  
                        <asp:CheckBox ID="CheckBox1" runat="server" Width="50px" ></asp:CheckBox>  
                    </ItemTemplate>  
                  <EditItemTemplate>  
                        <asp:CheckBox ID="CheckBox1" runat="server" Width="50px" ></asp:CheckBox> 
                    </EditItemTemplate> 
                    <FooterTemplate>  
                          <asp:Textbox ID="txt_Book_Amount" runat="server" Width="50px" Text='<%#Eval("Select") %>'></asp:Textbox> 
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