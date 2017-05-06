<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TipAllocationTest.aspx.cs" Inherits="TipShareV2.Webpages.TipAllocationTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Styles/StyleSheet.css" rel="stylesheet" />
    <title>Tip Allocation</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1> Daily Totals </h1>
            <!-- <asp:GridView ID="gvDailyTotals" runat="server"> </asp:GridView> !-->

            <h2> Lunch Shift </h2>
            <h3> Enter Tips </h3>

            <asp:Label ID="lblLunchServer" Text="Server" runat="server" />
            <asp:DropDownList ID="ddlLunchServer" runat="server" AutoPostBack="True" 
                DataSourceID="sdsEmployee" DataTextField="Name" DataValueField="EmployeeID"> </asp:DropDownList>
                       

            <asp:Label ID="lblLunchGrossSales" Text="Gross Sales" runat="server" />
            <asp:TextBox ID="txtLunchGrossSalesTest" runat="server" />

            <asp:Label ID="lblLunchGrossTips" Text="Gross Tips Earned" runat="server" />
            <asp:TextBox ID="txtLunchGrossTipsTest" runat="server" /> 

            <asp:Label ID="lblLunchTipAlloc" Text="Tip Allocation %" runat="server" />
            <asp:TextBox ID="txtLunchTipAllocTest" runat="server" />  

            <asp:Label ID="lblLunchTipPool" Text="Tip Pool Allocation" runat="server" />
            <asp:Label ID="lblLunchTipPoolCalcTest" runat="server" />


            <asp:Button ID="btnSaveLunch" Text="Save" runat="server" OnClick="btnSaveLunch_Click" />

            <h3> Allocate Tips </h3>

            <asp:Label ID="lblSupport" Text="Support Staff" runat="server" />
            <asp:DropDownList ID="ddlLunchSupport" runat="server" AutoPostBack="True"
                DataSourceID="sdsEmployee" DataTextField="Name" DataValueField="EmployeeID"> </asp:DropDownList>

            <asp:Label ID="lblLunchSupportHours" Text="Hours Worked" runat="server" />
            <asp:TextBox ID="txtLunchSupportHours" runat="server" /> <!--OnTextChanged="txtLunchSupportHours_TextChanged"!-->


            <asp:Label ID="lblLunchSupportTipAlloc" runat="server" />
            <asp:Label ID="lblLunchSupportTipCalc" runat="server" />

            <asp:Label ID="lblLunchTipPoolTotal" Text="Tip Pool" runat="server" />
            <asp:Label ID="lblLunchTipPoolTotalCalc" runat="server" />

            <asp:Button ID="btnSaveLunchSupport" Text="Save" runat="server" />

           <!-- <h1> Dinner Shift </h1>
            <asp:GridView ID="gvDinnerTips" runat="server">   </asp:GridView>
            <asp:GridView ID="gvDinnerAllocateTips" runat="server">   </asp:GridView> !-->

            
           
         </div>

           <asp:SqlDataSource ID="sdsEmployee" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
          SelectCommand="SELECT EmployeeID, FirstName + ' ' + LastName AS [Name] 
FROM Employee
WHERE EmployeeStatus='Active'" /> 

    </form>
</body>
</html>
