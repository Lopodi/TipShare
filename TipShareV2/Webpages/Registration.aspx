<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="TipShareV2.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>User Registration</title>
    <link href="../Styles/StyleSheet.css" rel="stylesheet" />
    
</head>
<body>
    <h1>User Registration</h1>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnlRegister" runat="server">
            
        <asp:Label ID="lblFirstName" Text="First Name:" runat="server" />
        <asp:TextBox ID = "txtFirstName" runat="server" /><br />
        <br />
        <br />

        <asp:Label ID="lblLastName" Text="Last Name:" runat="server" />
        <asp:TextBox ID = "txtLastName" runat= "server" /><br />
        <br />
        <br />

        <asp:Label ID="lblEmail" Text="Enter Email:" runat="server" />
        <asp:TextBox ID="txtEmail" runat="server" /> <br /> 
        <asp:Label ID="lblConfirmEmail" Text="Confirm Email:" runat="server" />
        <asp:TextBox ID="txtConfirmEmail" runat="server" /> <br />  
        <br />
        <br />

        <asp:Label ID="lblPassword" Text="Enter Password:" runat="server" />
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" /><br />
        <asp:Label ID="lblConfirmPassword" Text="Confirm Password:" runat="server" />
        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" /> <br />   
        <br />
        <br />

        <asp:Button ID="btnRegister" Text="Submit" runat="server" OnClick="btnRegister_Click" /><br />
        <br />

        </asp:Panel>

        <asp:Label ID="lblConfirm" runat="server" /> <br />
        <asp:Label ID="lblNullError" Text="" runat="server" /><br /> 
        <asp:Label ID="lblEmailMismatchError" runat="server" /> <br />  
        <asp:Label ID="lblEmailInvalidError" runat="server" /> <br />
        <asp:Label ID="lblPasswordLengthError" runat="server" /> <br />  
        <asp:Label ID="lblPasswordMismatchError" runat="server" /> <br />  
               
        </div>
    </form>
</body>
</html>
