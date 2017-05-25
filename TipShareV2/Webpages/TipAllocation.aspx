<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TipAllocation.aspx.cs" Inherits="TipShareV2.Webpages.TipAllocation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Styles/StyleSheet.css" rel="stylesheet" />
    <title>Tip Allocation</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1> Daily Totals <asp:Button ID="btnLogout" Text="Logout" OnClick="btnLogout_Click" 
                runat="server" /></h1>
            
            <h2> Lunch Shift </h2>
            <h3> Enter Tips </h3>

            <asp:Button ID="btnShiftDate" Text="Select Date" OnClick="btnShiftDate_Click" runat="server" />
            <asp:Calendar ID="cldShiftDate" visible="false" runat="server"
                OnSelectionChanged="cldShiftDate_SelectionChanged" />
           
            <br />               
            <br />

            <asp:Panel runat="server" ID="pnlLunchServer"> 
                <asp:Panel ID="pnlLunchServerLine" runat="server"> 

                <asp:Label ID="lblLunchServer" Text="Server" runat="server" />
                <asp:DropDownList ID="ddlLunchServer" runat="server" AutoPostBack="True" 
                    DataSourceID="sdsEmployee" DataTextField="Name" DataValueField="EmployeeID"> </asp:DropDownList>
              <%--  <asp:RequiredFieldValidator ID="rfvLunchServer" ErrorMessage="Server is a required field" 
                    ControlToValidate="ddlLunchServer" runat="server" />--%>

                <asp:Label ID="lblLunchGrossSales" Text="Gross Sales" runat="server" />
                <asp:TextBox ID="txtLunchGrossSales" runat="server" />
            <%--    <asp:RequiredFieldValidator ID="rfvLunchGrossSales" ErrorMessage="Gross sales is a required field" 
                    ControlToValidate="txtLunchGrossSales" runat="server" />--%>

                <asp:Label ID="lblLunchGrossTips" Text="Gross Tips Earned" runat="server" />
                <asp:TextBox ID="txtLunchGrossTips" runat="server" />
                <%--<asp:RequiredFieldValidator ID="rfvLunchGrossTips" ErrorMessage="Gross tips is a required field" 
                    ControlToValidate="txtLunchGrossTips" runat="server" />--%>

                <asp:Label ID="lblLunchTipAlloc" Text="Tip Allocation %" runat="server" />
                <asp:TextBox ID="txtLunchTipAlloc" runat="server" />  
                <%--<asp:RequiredFieldValidator ID="rfvLunchTipAlloc" ErrorMessage="Tip Allocation % is a required field" 
                    ControlToValidate="txtLunchTipAlloc" runat="server" /> --%>

                <asp:Label ID="lblLunchTipPool" Text="Tip Pool Allocation" runat="server" />
                <asp:Label ID="lblLunchTipPoolCalc" Text=" " runat="server" />
                                

                <asp:Button ID="btnSaveLunch" Text="Save" runat="server" OnClick="btnSaveLunch_Click" />
                    <br />
                <asp:PlaceHolder ID="phNewLunchServer" runat="server"></asp:PlaceHolder>

                <%--<asp:ValidationSummary ValidationGroup="LunchServer" ID="vsLunchServerError" runat="server" />--%>
                  </asp:Panel>  
                <asp:GridView ID="gvLunchTipAlloc" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="GratuityID,EmployeeID,UserID" DataSourceID="SqlDsGratuity">
                    <Columns>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:BoundField DataField="EmployeeName" HeaderText="Select Server" ReadOnly="True" SortExpression="EmployeeName" />
                        <asp:BoundField DataField="GrossSales" HeaderText="Gross Sales Earned" SortExpression="GrossSales" />
                        <asp:BoundField DataField="TipsEarned" HeaderText="Tips Earned" SortExpression="TipsEarned" />
                        <asp:BoundField DataField="TipPercentContributed" HeaderText="Percent Contributed" SortExpression="TipPercentContributed" />
                        <asp:BoundField DataField="TipsAllocated" HeaderText="Tips Allocated" ReadOnly="True" SortExpression="TipsAllocated" />
                        <asp:BoundField DataField="TipPercentAllocated" HeaderText="TipPercentAllocated" SortExpression="TipPercentAllocated" Visible="False" />
                        <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" SortExpression="EmployeeID" Visible="False" />
                        <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" Visible="False" />
                        <asp:BoundField DataField="HoursWorked" HeaderText="HoursWorked" SortExpression="HoursWorked" Visible="False" />
                        <asp:BoundField DataField="ShiftDate" HeaderText="ShiftDate" SortExpression="ShiftDate" Visible="False" />
                        <asp:BoundField DataField="Shift" HeaderText="Shift" SortExpression="Shift" Visible="False" />
                        <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="False" />
                        <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdateDate" SortExpression="LastUpdateDate" Visible="False" />
                        <asp:BoundField DataField="LastCreatedBy" HeaderText="LastCreatedBy" SortExpression="LastCreatedBy" Visible="False" />
                        <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" Visible="False" />
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblLunchServerError" runat="server" />
            </asp:Panel>
            <asp:SqlDataSource ID="SqlDsGratuity" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" DeleteCommand="DELETE FROM [Gratuity] WHERE [GratuityID] = @GratuityID" InsertCommand="INSERT INTO [Gratuity] ([EmployeeID], [UserID], [GrossSales], [TipsEarned], [TipsAllocated], [TipPercentAllocated], [TipPercentContributed], [HoursWorked], [DateCreated], [Shift], [ShiftDate], [CreatedBy], [LastUpdateDate], [LastCreatedBy]) VALUES (@EmployeeID, @UserID, @GrossSales, @TipsEarned, @TipsAllocated, @TipPercentAllocated, @TipPercentContributed, @HoursWorked, @DateCreated, @Shift, @ShiftDate, @CreatedBy, @LastUpdateDate, @LastCreatedBy)" SelectCommand="SELECT Gratuity.GratuityID, Gratuity.EmployeeID, Gratuity.UserID, Gratuity.GrossSales, Gratuity.TipsEarned, Gratuity.TipsAllocated, Gratuity.TipPercentAllocated, Gratuity.TipPercentContributed, Gratuity.HoursWorked, Gratuity.Shift, Gratuity.ShiftDate, Gratuity.CreatedBy, Gratuity.LastUpdateDate, Gratuity.LastCreatedBy, Employee.FirstName + ' ' + Employee.LastName AS EmployeeName, Gratuity.DateCreated FROM Gratuity INNER JOIN Employee ON Gratuity.EmployeeID = Employee.EmployeeID" UpdateCommand="UPDATE [Gratuity] SET [EmployeeID] = @EmployeeID, [UserID] = @UserID, [GrossSales] = @GrossSales, [TipsEarned] = @TipsEarned, [TipsAllocated] = @TipsAllocated, [TipPercentAllocated] = @TipPercentAllocated, [TipPercentContributed] = @TipPercentContributed, [HoursWorked] = @HoursWorked, [DateCreated] = @DateCreated, [Shift] = @Shift, [ShiftDate] = @ShiftDate, [CreatedBy] = @CreatedBy, [LastUpdateDate] = @LastUpdateDate, [LastCreatedBy] = @LastCreatedBy WHERE [GratuityID] = @GratuityID">
                <DeleteParameters>
                    <asp:Parameter Name="GratuityID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="EmployeeID" Type="Int32" />
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="GrossSales" Type="Decimal" />
                    <asp:Parameter Name="TipsEarned" Type="Decimal" />
                    <asp:Parameter Name="TipsAllocated" Type="Decimal" />
                    <asp:Parameter Name="TipPercentAllocated" Type="Decimal" />
                    <asp:Parameter Name="TipPercentContributed" Type="Decimal" />
                    <asp:Parameter Name="HoursWorked" Type="Int32" />
                    <asp:Parameter Name="DateCreated" Type="DateTime" />
                    <asp:Parameter Name="Shift" Type="String" />
                    <asp:Parameter DbType="Date" Name="ShiftDate" />
                    <asp:Parameter Name="CreatedBy" Type="String" />
                    <asp:Parameter Name="LastUpdateDate" Type="DateTime" />
                    <asp:Parameter Name="LastCreatedBy" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="EmployeeID" Type="Int32" />
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="GrossSales" Type="Decimal" />
                    <asp:Parameter Name="TipsEarned" Type="Decimal" />
                    <asp:Parameter Name="TipsAllocated" Type="Decimal" />
                    <asp:Parameter Name="TipPercentAllocated" Type="Decimal" />
                    <asp:Parameter Name="TipPercentContributed" Type="Decimal" />
                    <asp:Parameter Name="HoursWorked" Type="Int32" />
                    <asp:Parameter Name="DateCreated" Type="DateTime" />
                    <asp:Parameter Name="Shift" Type="String" />
                    <asp:Parameter DbType="Date" Name="ShiftDate" />
                    <asp:Parameter Name="CreatedBy" Type="String" />
                    <asp:Parameter Name="LastUpdateDate" Type="DateTime" />
                    <asp:Parameter Name="LastCreatedBy" Type="String" />
                    <asp:Parameter Name="GratuityID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <br />
            <br />

            <asp:Button ID="btnAddLunchServer" OnClick="btnAddNewLunchServer_Click" Text="Add Server" runat="server" />
            
            <br />
            <br />

            <h3> Allocate Tips </h3>

            <asp:Panel runat="server" ID="pnlLunchSupport">

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

            </asp:Panel>

            <br />
            <br />

            <asp:Button ID="btnAddLunchSupport" Text="Add Support Staff" runat="server" />
            
            <br />
            <br />

           <!-- <h1> Dinner Shift </h1>
            <asp:GridView ID="gvDinnerTips" runat="server">   </asp:GridView>
            <asp:GridView ID="gvDinnerAllocateTips" runat="server">   </asp:GridView> !-->

            
            <a href="EmployeeAdministration.aspx">Employee Administration</a>
            <a href="UserAdministration.aspx">User Administration</a>
         </div>

         

           <asp:SqlDataSource ID="sdsEmployee" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
           SelectCommand="SELECT EmployeeID, FirstName + ' ' + LastName AS [Name] 
           FROM Employee
           WHERE EmployeeStatus='Active'" /> 

        <asp:Menu ID="Menu" runat="server"></asp:Menu>

    </form>
</body>
</html>

