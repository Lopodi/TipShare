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

            <asp:Button ID="btnShiftDate" Text="Select Date" runat="server" />
            <asp:Calendar ID="cldShiftDate" visible="true" runat="server"
                OnSelectionChanged="cldShiftDate_SelectionChanged" />

            <h2> Lunch Shift </h2>
            <h3> Enter Tips </h3>
            <br />

            <asp:Panel runat="server" ID="pnlLunchServer"> 
                <asp:Panel ID="pnlLunchServerLine" runat="server"> 

                <asp:Label ID="lblLunchServer" Text="Server" runat="server" />
                <asp:DropDownList ID="ddlLunchServer" runat="server" AutoPostBack="True" 
                    DataSourceID="sdsEmployee" DataTextField="Name" DataValueField="EmployeeID"
                    AppendDataBoundItems="True" Selected="True" Value="0">
                    <asp:ListItem Selected="True" Value="0">Select Server</asp:ListItem>
                </asp:DropDownList>
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


                <%--<asp:ValidationSummary ValidationGroup="LunchServer" ID="vsLunchServerError" runat="server" />--%>
                  </asp:Panel>  
                <asp:GridView ID="gvLunchTipAlloc" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                    DataKeyNames="GratuityID,EmployeeID,UserID" Visible="False">
                    <Columns>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:BoundField DataField="EmployeeName" HeaderText="Select Server" ReadOnly="True" SortExpression="EmployeeName" />
                        <asp:BoundField DataField="GrossSales" HeaderText="Gross Sales Earned" SortExpression="GrossSales" DataFormatString="{0:c}" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TipsEarned" HeaderText="Tips Earned" SortExpression="TipsEarned" DataFormatString="{0:c}" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TipPercentContributed" HeaderText="Percent Contributed" SortExpression="TipPercentContributed" DataFormatString="{0:p}" />
                        <asp:BoundField DataField="TipsAllocated" HeaderText="Tips Allocated" ReadOnly="True" SortExpression="TipsAllocated" DataFormatString="{0:c}" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
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
            
            <br />
            <br />

            <h3> Allocate Tips </h3>

            <asp:Panel runat="server" ID="pnlLunchSupport">

                <br />

                <asp:Label ID="lblLunchTipPoolTotal" Text="Tip Pool" runat="server" />
                <asp:Label ID="lblLunchTipPoolTotalCalc" Text='<%#Bind("Entry") %>' runat="server" />
 
                <br />
                <br />

                <asp:Label ID="lblSupport" Text="Support Staff" runat="server" />
                <asp:DropDownList ID="ddlLunchSupport" runat="server" AutoPostBack="True" 
                    DataSourceID="sdsEmployee" DataTextField="Name" DataValueField="EmployeeID"
                    AppendDataBoundItems="True" Selected="True" Value="0">
                    <asp:ListItem Selected="True" Value="0">Select Support Staff</asp:ListItem>
                    </asp:DropDownList>


                <asp:Label ID="lblLunchSupportHours" Text="Hours Worked" runat="server" />
                <asp:TextBox ID="txtLunchSupportHours" runat="server" /> 

                <asp:Label ID="lblLunchSupportTipAlloc" runat="server" />
                <asp:Label ID="lblLunchSupportTipCalc" runat="server" />

                <asp:Button ID="btnSaveLunchSupportHours" OnClick="SaveLunchHours" Text="Save" runat="server" />

                <br />
                <asp:GridView ID="gvLunchSupportAlloc" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                    DataKeyNames="GratuityID,EmployeeID,UserID" Visible="False">
                    <Columns>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:BoundField DataField="EmployeeName" HeaderText="Select Server" ReadOnly="True" SortExpression="EmployeeName" />
                        <asp:BoundField DataField="HoursWorked" HeaderText="Hours Worked" SortExpression="HoursWorked" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TipsEarnedSupport" ReadOnly="True" HeaderText="Support Staff Tips Earned" SortExpression="TipsEarnedSupport" DataFormatString="{0:c}" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblLunchSupportError" runat="server" />
                <asp:GridView ID="gvLunchSupportTipsEarned" Visible="false" runat="server">
                </asp:GridView>
            </asp:Panel>
            
                <br />

            

            <br />
            <br />

            <asp:Button ID="btnAllocateTips" Text="Allocate Tips" OnClick="btnAllocateTips_Click" runat="server" />
            <br />
            <br />

           <!-- <h1> Dinner Shift </h1>
            <asp:GridView ID="gvDinnerTips" runat="server">   </asp:GridView>
            <asp:GridView ID="gvDinnerAllocateTips" runat="server">   </asp:GridView> !-->

            <h2> Dinner Shift </h2>
            <h3> Enter Tips </h3> 

            <br />               
            <br />

            <asp:Panel runat="server" ID="pnlDinnerServer"> 
                <asp:Panel ID="pnlDinnerAdd" runat="server"> 

                <asp:Label ID="lblDinnerServer" Text="Server" runat="server" />
                <asp:DropDownList ID="ddlDinnerServer" runat="server" AutoPostBack="True" 
                    DataSourceID="sdsEmployee" DataTextField="Name" DataValueField="EmployeeID"
                    AppendDataBoundItems="True" Selected="True" Value="0">
                    <asp:ListItem Selected="True" Value="0">Select Server</asp:ListItem>
                </asp:DropDownList>
              <%--  <asp:RequiredFieldValidator ID="rfvLunchServer" ErrorMessage="Server is a required field" 
                    ControlToValidate="ddlLunchServer" runat="server" />--%>

                <asp:Label ID="lblDinnerGrossSales" Text="Gross Sales" runat="server" />
                <asp:TextBox ID="txtDinnerGrossSales" runat="server" />
            <%--    <asp:RequiredFieldValidator ID="rfvLunchGrossSales" ErrorMessage="Gross sales is a required field" 
                    ControlToValidate="txtLunchGrossSales" runat="server" />--%>

                <asp:Label ID="lblDinnerTipsEarned" Text="Gross Tips Earned" runat="server" />
                <asp:TextBox ID="txtDinnerTipsEarned" runat="server" />
                <%--<asp:RequiredFieldValidator ID="rfvLunchGrossTips" ErrorMessage="Gross tips is a required field" 
                    ControlToValidate="txtLunchGrossTips" runat="server" />--%>

                <asp:Label ID="lblDinnerTipAllocPercent" Text="Tip Allocation %" runat="server" />
                <asp:TextBox ID="txtDinnerTipAllocPercent" runat="server" />  
                <%--<asp:RequiredFieldValidator ID="rfvLunchTipAlloc" ErrorMessage="Tip Allocation % is a required field" 
                    ControlToValidate="txtLunchTipAlloc" runat="server" /> --%>

                <asp:Label ID="lblDinnerTipPoolAlloc" Text="Tip Pool Allocation" runat="server" />
                <asp:Label ID="lblDinnerTipPoolAllocSum" Text=" " runat="server" />
                                

                <asp:Button ID="btnSaveDinner" Text="Save" runat="server" />
                    <%-- OnClick="btnSaveDinner_Click --%>

                    </asp:Panel>
                <asp:GridView ID="gvDinnerTipAlloc" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                    DataKeyNames="GratuityID,EmployeeID,UserID" Visible="False">
                    <Columns>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:BoundField DataField="EmployeeName" HeaderText="Select Server" ReadOnly="True" SortExpression="EmployeeName" />
                        <asp:BoundField DataField="GrossSales" HeaderText="Gross Sales Earned" SortExpression="GrossSales" DataFormatString="{0:c}" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TipsEarned" HeaderText="Tips Earned" SortExpression="TipsEarned" DataFormatString="{0:c}" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TipPercentContributed" HeaderText="Percent Contributed" SortExpression="TipPercentContributed" DataFormatString="{0:p}" />
                        <asp:BoundField DataField="TipsAllocated" HeaderText="Tips Allocated" ReadOnly="True" SortExpression="TipsAllocated" DataFormatString="{0:c}" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
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
                <asp:Label ID="lblDinnerServerError" runat="server" />
            </asp:Panel>
            <br />

            <h3> Allocate Tips </h3>

            <asp:Panel runat="server" ID="pnlDinnerSupport">

                <br />

                <asp:Label ID="lblDinnerTipPool" Text="Tip Pool" runat="server" />
                <asp:Label ID="lblDinnerTipPoolSum" Text='<%#Bind("Entry") %>' runat="server" />
 
                <br />
                <br />

                <asp:Label ID="lblDinnerSupportStaff" Text="Support Staff" runat="server" />
                <asp:DropDownList ID="ddlDinnerSupportStaff" runat="server" AutoPostBack="True" 
                    DataSourceID="sdsEmployee" DataTextField="Name" DataValueField="EmployeeID"
                    AppendDataBoundItems="True" Selected="True" Value="0">
                    <asp:ListItem Selected="True" Value="0">Select Support Staff</asp:ListItem>
                    </asp:DropDownList>


                <asp:Label ID="lblDinnerHoursWorked" Text="Hours Worked" runat="server" />
                <asp:TextBox ID="txtDinnerHoursWorked" runat="server" /> 

                <asp:Label ID="lblDinnerSupportTipsAlloc" runat="server" />
                <asp:Label ID="lblDinnerSupportTipsEarned" runat="server" />

                <asp:Button ID="btnLuncHoursWorked" OnClick="SaveLunchHours" Text="Save" runat="server" />

                <br />
                <asp:GridView ID="gvDinnerSupportAlloc" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
                    DataKeyNames="GratuityID,EmployeeID,UserID" Visible="False">
                    <Columns>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:BoundField DataField="EmployeeName" HeaderText="Select Server" ReadOnly="True" SortExpression="EmployeeName" />
                        <asp:BoundField DataField="HoursWorked" HeaderText="Hours Worked" SortExpression="HoursWorked" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TipsEarnedSupport" ReadOnly="True" HeaderText="Support Staff Tips Earned" SortExpression="TipsEarnedSupport" DataFormatString="{0:c}" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblDinnerSupportError" runat="server" />
            </asp:Panel>
            
                <br />

            

            <asp:Label ID="lblDinnerSupportStaffError" runat="server" Text=""></asp:Label>

            

            <br />
            <br />

            <asp:Button ID="btnDinnerAllocTips" Text="Allocate Tips" OnClick="btnAllocateTips_Click" runat="server" />
            <br />
            <br />
                    </asp:Panel>
                    <br />

            
            <a href="EmployeeAdministration.aspx" id="EmployeeAdminLink">Employee Administration</a>
            <a href="UserAdministration.aspx" id="UserAdminLink">User Administration</a>
         </div>

         

            <asp:SqlDataSource ID="sdsGratuity" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                DeleteCommand="DELETE FROM [Gratuity] WHERE [GratuityID] = @GratuityID" 
                InsertCommand="INSERT INTO Gratuity(EmployeeID, UserID, GrossSales, TipsEarned, TipsAllocated, TipPercentAllocated, TipPercentContributed, HoursWorked, DateCreated, Shift, ShiftDate, CreatedBy, LastUpdateDate, LastCreatedBy, TipsEarnedSupport) VALUES (@EmployeeID, @UserID, @GrossSales, @TipsEarned, @TipsAllocated, @TipPercentAllocated, @TipPercentContributed, @HoursWorked, @DateCreated, @Shift, @ShiftDate, @CreatedBy, @LastUpdateDate, @LastCreatedBy, @TipsEarnedSupport)" SelectCommand="SELECT Gratuity.GratuityID, Gratuity.EmployeeID, Gratuity.UserID, Gratuity.GrossSales, Gratuity.TipsEarned, Gratuity.TipsAllocated, Gratuity.TipPercentAllocated, Gratuity.TipPercentContributed, Gratuity.HoursWorked, Gratuity.Shift, Gratuity.ShiftDate, Gratuity.CreatedBy, Gratuity.LastUpdateDate, Gratuity.LastCreatedBy, Employee.FirstName + ' ' + Employee.LastName AS EmployeeName, Gratuity.DateCreated, Gratuity.TipsEarnedSupport FROM Gratuity INNER JOIN Employee ON Gratuity.EmployeeID = Employee.EmployeeID" 
                UpdateCommand="UPDATE Gratuity SET EmployeeID = @EmployeeID, UserID = @UserID, GrossSales = @GrossSales, TipsEarned = @TipsEarned, TipsAllocated = @TipsAllocated, TipPercentAllocated = @TipPercentAllocated, TipPercentContributed = @TipPercentContributed, HoursWorked = @HoursWorked, DateCreated = @DateCreated, Shift = @Shift, ShiftDate = @ShiftDate, CreatedBy = @CreatedBy, LastUpdateDate = @LastUpdateDate, LastCreatedBy = @LastCreatedBy, TipsEarnedSupport = @TipsEarnedSupport, WHERE (GratuityID = @GratuityID)">
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
                    <asp:Parameter Name="TipsEarnedSupport" Type="Decimal" />
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
                    <asp:Parameter Name="TipsEarnedSupport" Type="Decimal" />
                </UpdateParameters>
            </asp:SqlDataSource>
            
         

           <asp:SqlDataSource ID="sdsEmployee" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
           SelectCommand="SELECT EmployeeID, FirstName + ' ' + LastName AS [Name] 
           FROM Employee
           WHERE EmployeeStatus='Active'" /> 

        <asp:Menu ID="Menu" runat="server"></asp:Menu>

    </form>
</body>
</html>

