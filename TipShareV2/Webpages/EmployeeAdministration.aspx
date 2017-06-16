<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeAdministration.aspx.cs" Inherits="TipShareV2.Webpages.EmployeeAdministration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Administration</title>
    <link href="../Styles/StyleSheet.css" rel="stylesheet" />
</head>
<body>
    <h1>Employee Administration</h1>

    <form id="form1" runat="server">
        <div>

        <asp:Button ID="btnLogout" Text="Logout" OnClick="btnLogout_Click" runat="server" />
            

            <div>
            </div>
            
            <asp:GridView ID="gvEmployee" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                OnRowUpdating="gvEmployee_RowUpdating" 
                DataKeyNames="EmployeeID,StatusDate,UserID,DateCreated,CreatedBy" 
                DataSourceID="sdsEmployee" ShowFooter="False" OnSelectedIndexChanged="gvEmployee_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:TemplateField HeaderText="Employee ID" InsertVisible="False" SortExpression="EmployeeID">
                        <ItemTemplate>
                            <asp:Label ID="lblEmployeeID" runat="server" Text='<%# Bind("EmployeeID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UserID" SortExpression="UserID" Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtUserID" runat="server" Text='<%# Bind("UserID") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="TxtUserID" runat="server" Text='<%# Bind("UserID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="First Name" SortExpression="FirstName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Last Name" SortExpression="LastName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Employee Status" SortExpression="EmployeeStatus">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlEmployeeStatus" runat="server" 
                                SelectedValue='<%# Bind("EmployeeStatus") %>'>
                                <asp:ListItem Text="Select Status" />
                                <asp:ListItem Text="Active" />
                                <asp:ListItem Text="Inactive" />
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAddEmployeeStatus" runat="server" Text='<%# Bind("EmployeeStatus") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="StatusDate" SortExpression="StatusDate" Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtStatusDate" runat="server" Text='<%# Bind("StatusDate") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblStatusDate" runat="server" Text='<%# Bind("StatusDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DateCreated" SortExpression="DateCreated" Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("DateCreated") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("DateCreated") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CreatedBy") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("CreatedBy") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LastUpdatedDate" SortExpression="LastUpdatedDate" Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("LastUpdatedDate") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("LastUpdatedDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LastUpdatedBy" SortExpression="LastUpdatedBy" Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("LastUpdatedBy") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("LastUpdatedBy") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <br />
            <h3>Enter New Employee</h3>
                <asp:Label ID="lblNewEEFirstName" Text="First Name" runat="server" />
                <asp:TextBox ID="txtNewEEFirstName" Text="" runat="server" />  
                <asp:Label ID="lblNewEELastName" Text="Last Name" runat="server" />    
                <asp:TextBox ID="txtNewEELastName" Text="" runat="server" />
                <asp:Label ID="lblNewEEStatus" Text="Status" runat="server" />
                <asp:DropDownList ID="ddlNewEEStatus" InitialValue="Select Employee Status" runat="server">
                    <asp:ListItem Text="Select Employee Status" />
                    <asp:ListItem Text="Active" />
                    <asp:ListItem Text="Inactive" />
                </asp:DropDownList>  
            <asp:Button ID="btnAddNewEE" Text="Save" OnClick="btnAddEmployee_Click" runat="server" />
            <br />
            <br />
            <asp:SqlDataSource ID="sdsEmployee" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                SelectCommand="SELECT * FROM [Employee]" DeleteCommand="DELETE FROM [Employee] WHERE [EmployeeID] = @EmployeeID" 
                InsertCommand="INSERT INTO [Employee] ([UserID], [FirstName], [LastName], [EmployeeStatus], [StatusDate], [DateCreated], [CreatedBy], [LastUpdatedDate], [LastUpdatedBy]) VALUES (@UserID, @FirstName, @LastName, @EmployeeStatus, @StatusDate, @DateCreated, @CreatedBy, @LastUpdatedDate, @LastUpdatedBy)" 
                UpdateCommand="UPDATE [Employee] SET [UserID] = @UserID, [FirstName] = @FirstName, [LastName] = @LastName, [EmployeeStatus] = @EmployeeStatus, [StatusDate] = @StatusDate, [DateCreated] = @DateCreated, [CreatedBy] = @CreatedBy, [LastUpdatedDate] = @LastUpdatedDate, [LastUpdatedBy] = @LastUpdatedBy WHERE [EmployeeID] = @EmployeeID" >
                <DeleteParameters>
                    <asp:Parameter Name="EmployeeID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="FirstName" Type="String" />
                    <asp:Parameter Name="LastName" Type="String" />
                    <asp:Parameter Name="EmployeeStatus" Type="String" />
                    <asp:Parameter Name="StatusDate" Type="DateTime" />
                    <asp:Parameter Name="DateCreated" Type="DateTime" />
                    <asp:Parameter Name="CreatedBy" Type="String" />
                    <asp:Parameter Name="LastUpdatedDate" Type="DateTime" />
                    <asp:Parameter Name="LastUpdatedBy" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="FirstName" Type="String" />
                    <asp:Parameter Name="LastName" Type="String" />
                    <asp:Parameter Name="EmployeeStatus" Type="String" />
                    <asp:Parameter Name="StatusDate" Type="DateTime" />
                    <asp:Parameter Name="DateCreated" Type="DateTime" />
                    <asp:Parameter Name="CreatedBy" Type="String" />
                    <asp:Parameter Name="LastUpdatedDate" Type="DateTime" />
                    <asp:Parameter Name="LastUpdatedBy" Type="String" />
                    <asp:Parameter Name="EmployeeID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
           
           <a href="TipAllocation.aspx">Tip Allocation</a>

        </div>

        
    </form>
</body>
</html>
