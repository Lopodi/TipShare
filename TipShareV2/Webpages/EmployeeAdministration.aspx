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
                OnRowUpdating="gvEmployee_RowUpdating" DataKeyNames="EmployeeID,StatusDate,UserID" DataSourceID="sdsEmployee" ShowFooter="True" OnSelectedIndexChanged="gvEmployee_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:TemplateField HeaderText="EmployeeID" InsertVisible="False" SortExpression="EmployeeID">
                        <ItemTemplate>
                            <asp:Label ID="lblEmployeeID" runat="server" Text='<%# Bind("EmployeeID") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="btnAddEmployee" OnClick="btnAddEmployee_Click" Text="Add Employee" runat="server" />
                        </FooterTemplate> 
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UserID" SortExpression="UserID" Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtUserID" runat="server" Text='<%# Bind("UserID") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="TxtUserID" runat="server" Text='<%# Bind("UserID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FirstName" SortExpression="FirstName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                        </ItemTemplate>
                        <footertemplate>
                            <asp:TextBox ID="txtAddFirstName" runat="server" />
                        </footertemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LastName" SortExpression="LastName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                        </ItemTemplate>
                        <footertemplate>
                            <asp:TextBox ID="txtAddLirstName" runat="server" />
                        </footertemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EmployeeStatus" SortExpression="EmployeeStatus">
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
                        <footertemplate>
                            <asp:DropDownList ID="ddlAddEmployeeStatus" runat="server"> 
                                <asp:ListItem Text="Select Status" />
                                <asp:ListItem Text="Active" />
                                <asp:ListItem Text="Inactive" />
                                <asp:ListItem Text="Pending Approval" />
                            </asp:DropDownList>
                        </footertemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="StatusDate" SortExpression="StatusDate" Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtStatusDate" runat="server" Text='<%# Bind("StatusDate") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblStatusDate" runat="server" Text='<%# Bind("StatusDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsEmployee" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                SelectCommand="SELECT * FROM [Employee]" DeleteCommand="DELETE FROM [Employee] 
                WHERE [EmployeeID] = @EmployeeID" 
                InsertCommand="INSERT INTO [Employee] ([UserID], [FirstName], [LastName], [EmployeeStatus], 
                [StatusDate]) VALUES (@UserID, @FirstName, @LastName, @EmployeeStatus, @StatusDate)" 
                UpdateCommand="UPDATE [Employee] SET [UserID] = @UserID, [FirstName] = @FirstName, 
                [LastName] = @LastName, [EmployeeStatus] = @EmployeeStatus, [StatusDate] = @StatusDate 
                WHERE [EmployeeID] = @EmployeeID" >
                <DeleteParameters>
                    <asp:Parameter Name="EmployeeID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="FirstName" Type="String" />
                    <asp:Parameter Name="LastName" Type="String" />
                    <asp:Parameter Name="EmployeeStatus" Type="String" />
                    <asp:Parameter Name="StatusDate" Type="DateTime" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="FirstName" Type="String" />
                    <asp:Parameter Name="LastName" Type="String" />
                    <asp:Parameter Name="EmployeeStatus" Type="String" />
                    <asp:Parameter Name="StatusDate" Type="DateTime" />
                    <asp:Parameter Name="EmployeeID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
           
           <a href="TipAllocation.aspx">Tip Allocation</a>

        </div>

        
    </form>
</body>
</html>
