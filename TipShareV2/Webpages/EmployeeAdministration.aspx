<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeAdministration.aspx.cs" Inherits="TipShareV2.Webpages.EmployeeAdministration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Administration</title>
    <link href="Styles/StyleSheet.css" type ="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnAddEmployee" Text="Add Employee" runat="server" />
            <asp:Button ID="btnSaveEmployeeEdit" Text="Save" runat="server" 
                OnClick="btnSaveEmployeeEdit_Click" />

            <div>
                <asp:Label ID="lblSaveEmployeeAlert" runat="server" />
                <asp:Button ID="btnSaveEmployeeYes" Text="Yes" runat="server" />
                <asp:Button ID="btnSaveEmployeeNo" Text="No" runat="server" />
            </div>
            
            <asp:GridView ID="gvEmployee" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="EmployeeID" DataSourceID="sdsEmployee">
                <Columns>
                    <asp:BoundField DataField="Last Name" HeaderText="Last Name" SortExpression="Last Name" />
                    <asp:BoundField DataField="First Name" HeaderText="First Name" SortExpression="First Name" />
                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                    <asp:BoundField DataField="Status Date" HeaderText="Status Date" SortExpression="Status Date" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsEmployee" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                SelectCommand="SELECT EmployeeID, LastName AS [Last Name], FirstName AS [First Name], 
                EmployeeStatus AS [Status], StatusDate AS [Status Date] 
                FROM Employee
                ORDER BY LastName" />

        </div>
    </form>
</body>
</html>
