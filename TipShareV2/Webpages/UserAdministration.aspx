<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAdministration.aspx.cs" Inherits="TipShareV2.Webpages.UserAdministration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Styles/StyleSheet.css" rel="stylesheet" />
    <title>User Administration</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnLogout" OnClick="btnLogout_Click" Text="Logout" runat="server" />
            <br />
            <asp:GridView ID="gvUserAdmin" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                DataKeyNames="UserID,UserPassword,FirstName,LastName,Email," DataSourceID="sdsUserAdmin" 
                OnSelectedIndexChanged="gvUserAdmin_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ShowEditButton="True" />
                    <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" ReadOnly="True" />
                    <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" ReadOnly="True" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" ReadOnly="True" />
                    <asp:TemplateField HeaderText="UserStatus" SortExpression="UserStatus">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlUserStatus" Text='<%# Bind("UserStatus") %>' runat="server">
                                <asp:ListItem Text="Select Status" />
                                <asp:ListItem Text="Active" />
                                <asp:ListItem Text="Pending Approval" />
                                <asp:ListItem Text="Inactive" />
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblUserStatus" runat="server" Text='<%# Bind("UserStatus") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="StatusDate" HeaderText="StatusDate" SortExpression="StatusDate" ReadOnly="True" />
                    <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" ReadOnly="True" />
                    <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" ReadOnly="True" />
                    <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdateDate" SortExpression="LastUpdateDate" ReadOnly="True" />
                    <asp:BoundField DataField="LastUpdatedBy" HeaderText="LastUpdatedBy" SortExpression="LastUpdatedBy" ReadOnly="True" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsUserAdmin" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" DeleteCommand="DELETE FROM [User] WHERE [UserID] = @UserID" InsertCommand="INSERT INTO [User] ([FirstName], [LastName], [Email], [UserPassword], [UserStatus], [StatusDate], [DateCreated], [CreatedBy], [LastUpdateDate], [LastUpdatedBy]) VALUES (@FirstName, @LastName, @Email, @UserPassword, @UserStatus, @StatusDate, @DateCreated, @CreatedBy, @LastUpdateDate, @LastUpdatedBy)" SelectCommand="SELECT * FROM [User]" UpdateCommand="UPDATE [User] SET [FirstName] = @FirstName, [LastName] = @LastName, [Email] = @Email, [UserPassword] = @UserPassword, [UserStatus] = @UserStatus, [StatusDate] = @StatusDate, [DateCreated] = @DateCreated, [CreatedBy] = @CreatedBy, [LastUpdateDate] = @LastUpdateDate, [LastUpdatedBy] = @LastUpdatedBy WHERE [UserID] = @UserID">
                <DeleteParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="FirstName" Type="String" />
                    <asp:Parameter Name="LastName" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                    <asp:Parameter Name="UserPassword" Type="String" />
                    <asp:Parameter Name="UserStatus" Type="String" />
                    <asp:Parameter Name="StatusDate" Type="DateTime" />
                    <asp:Parameter Name="DateCreated" Type="DateTime" />
                    <asp:Parameter Name="CreatedBy" Type="String" />
                    <asp:Parameter Name="LastUpdateDate" Type="DateTime" />
                    <asp:Parameter Name="LastUpdatedBy" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="FirstName" Type="String" />
                    <asp:Parameter Name="LastName" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                    <asp:Parameter Name="UserPassword" Type="String" />
                    <asp:Parameter Name="UserStatus" Type="String" />
                    <asp:Parameter Name="StatusDate" Type="DateTime" />
                    <asp:Parameter Name="DateCreated" Type="DateTime" />
                    <asp:Parameter Name="CreatedBy" Type="String" />
                    <asp:Parameter Name="LastUpdateDate" Type="DateTime" />
                    <asp:Parameter Name="LastUpdatedBy" Type="String" />
                    <asp:Parameter Name="UserID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <a href="TipAllocation.aspx">Tip Allocation</a>
        </div>
    </form>
</body>
</html>
