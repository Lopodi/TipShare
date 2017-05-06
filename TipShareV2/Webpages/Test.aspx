<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="TipShareV2.Webpages.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 29px;
        }
        .auto-style2 {
            width: 89%;
        }
        .auto-style3 {
            height: 29px;
            width: 156px;
        }
        .auto-style4 {
            width: 156px;
        }
        .auto-style5 {
            height: 29px;
            width: 222px;
        }
        .auto-style6 {
            width: 222px;
        }
        .auto-style7 {
            height: 29px;
            width: 200px;
        }
        .auto-style8 {
            width: 200px;
        }
        .auto-style9 {
            height: 29px;
            width: 199px;
        }
        .auto-style10 {
            width: 199px;
        }
        .auto-style11 {
            width: 156px;
            height: 16px;
        }
        .auto-style12 {
            width: 222px;
            height: 16px;
        }
        .auto-style13 {
            width: 200px;
            height: 16px;
        }
        .auto-style14 {
            width: 199px;
            height: 16px;
        }
        .auto-style15 {
            height: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

        

            <table class="auto-style2">
                <tr>
                    <td class="auto-style3">
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="sdsEmployeeDropDown" DataTextField="Name" DataValueField="EmployeeID">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style5">
                        <asp:TextBox ID="txtGrossSales" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style7">
                        <asp:TextBox ID="txtTipsEarned" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style9">
                        <asp:TextBox ID="txtTipAllocPercent" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblTipPool" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Button" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style11"></td>
                    <td class="auto-style12"></td>
                    <td class="auto-style13"></td>
                    <td class="auto-style14"></td>
                    <td class="auto-style15"></td>
                    <td class="auto-style15">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style6">&nbsp;</td>
                    <td class="auto-style8">&nbsp;</td>
                    <td class="auto-style10">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>

        

        </div>
        <asp:SqlDataSource ID="sdsEmployeeDropDown" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT EmployeeID, FirstName + ' ' + LastName AS [Name]
FROM Employee"></asp:SqlDataSource>
    </form>
</body>
</html>
