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

            <h2> Lunch Shift </h2>
            <h3> Enter Tips </h3>

            <asp:Label ID="lblLunchServerError" Text="" runat="server" />

            <asp:Button ID="btnShiftDate" Text="Select Date" OnClick="btnShiftDate_Click" runat="server" />
            <asp:Calendar ID="cldShiftDate" visible="false" runat="server"
                OnSelectionChanged="cldShiftDate_SelectionChanged" />

            <table>
                <tr>
                    <td>Server</td>
                    <td>Gross Sales</td>
                    <td>Gross Tips Earned</td>
                    <td>Tip Allocation Percent</td>
                    <td>Tip Pool Allocation</td>     
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlLunchServer" runat="server" AutoPostBack="True" 
                DataSourceID="sdsEmployee" DataTextField="Name" DataValueField="EmployeeID"> </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLunchGrossSales" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtLunchGrossTips" runat="server" /> 
                    </td>
                    <td>
                        <asp:TextBox ID="txtLunchTipAlloc" runat="server" />  
                    </td>
                    <td>
                        <asp:Label ID="lblLunchTipPoolCalc" runat="server" />
                    </td>
                    <td>
                         <asp:Button ID="btnSaveLunch" Text="Save" runat="server" OnClick="btnSaveLunch_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:Panel ID="pnlLunchServer" runat="server"> </asp:Panel>
                        <asp:PlaceHolder ID="phLunchServer" runat="server" />
                    </td>
                </tr>
            </table>  
            
              <asp:Button ID="btnAddNewLunchServer" OnClick="btnAddNewLunchServer_Click" Text="Add Server" runat="server" />

            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="EmployeeID" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:TemplateField HeaderText="EmployeeID" InsertVisible="False" SortExpression="EmployeeID">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("EmployeeID") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("EmployeeID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FirstName" SortExpression="FirstName">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("FirstName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LastName" SortExpression="LastName">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" DeleteCommand="DELETE FROM [Employee] WHERE [EmployeeID] = @EmployeeID" InsertCommand="INSERT INTO [Employee] ([UserID], [FirstName], [LastName], [EmployeeStatus], [StatusDate], [DateCreated], [CreatedBy], [LastUpdatedDate], [LastUpdatedBy]) VALUES (@UserID, @FirstName, @LastName, @EmployeeStatus, @StatusDate, @DateCreated, @CreatedBy, @LastUpdatedDate, @LastUpdatedBy)" SelectCommand="SELECT * FROM [Employee]" UpdateCommand="UPDATE [Employee] SET [UserID] = @UserID, [FirstName] = @FirstName, [LastName] = @LastName, [EmployeeStatus] = @EmployeeStatus, [StatusDate] = @StatusDate, [DateCreated] = @DateCreated, [CreatedBy] = @CreatedBy, [LastUpdatedDate] = @LastUpdatedDate, [LastUpdatedBy] = @LastUpdatedBy WHERE [EmployeeID] = @EmployeeID">
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
            <br />

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
