<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="TipShareV2.Webpages.Reports" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Styles/StyleSheet.css" rel="stylesheet" />
    <title>Reports</title>
</head>
<body>
    <form id="frmReports" runat="server">
        <div>
            <asp:Calendar ID="cldBeginDate" runat="server"></asp:Calendar> 
            <asp:Calendar ID="cldEndDate" runat="server"></asp:Calendar>
            <asp:Button ID="btnRunReport" Text="Run Report" OnClick="btnRunReport_Click" runat="server"/>
            <asp:GridView ID="gvTaxableTips" runat="server" AllowSorting="True">
                
            </asp:GridView>
            <asp:SqlDataSource ID="sdsGratuity" runat="server"></asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsEmployee" runat="server"></asp:SqlDataSource>
            <asp:Button Text="Logout" OnClick="btnLogout_Click" runat="server" />
        </div>
    </form>
</body>
</html>
