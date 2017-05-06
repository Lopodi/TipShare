<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TipShareV2.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
 <link href="../Styles/StyleSheet.css" rel="stylesheet" />  
</head>
<body>
    
    <h1>Welcome!</h1>
    

    <div>

    <form id="Login" runat="server">
    <!-- <img src="xxxx"/> -->
                
        <br />
        <br /> 
        
       <asp:Label Text=" Enter Email:" runat="server" ID="lblEnterEmail" />
       <asp:TextBox ID="txtEnterEmail" runat="server"></asp:TextBox> 
       <asp:Label ID="lblEmailError" runat="server"></asp:Label><br />
        
        <br />
        <asp:Label Text="Enter Password:" runat="server" ID="lblEnterPassword" />
        <asp:TextBox ID="txtEnterPassword" runat="server" TextMode="Password"></asp:TextBox> 
        <asp:Label ID="lblPasswordError" runat="server"></asp:Label><br /> 
           
        <br />
        <br />
           
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
        <br />
        <br />

    <asp:Label ID="lblLoginError" runat="server" />
        
        <br />
        <br />
        <br />
        
    <a href="Registration.aspx">Not Registered? Click here to create an account</a><br />
    <a href="ResetPassword.aspx">Trouble logging in? Click here to reset your password</a>
    </div>
    </form>   
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [UserID], [Email], [UserPassword], [UserStatus] FROM [User]"></asp:SqlDataSource>
</body>
</html>
