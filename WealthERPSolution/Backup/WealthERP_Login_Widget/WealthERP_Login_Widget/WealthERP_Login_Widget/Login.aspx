<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WealthERP_Login_Widget._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
       <style type="text/css">
        #form1
        {
            width: 208px;
        }
        .FieldName
{
    font-family: Verdana,Tahoma;
    font-weight: bold;
    font-size: x-small;
    color: #16518A;
    vertical-align: middle;
    text-align: right;
}
.FieldError
{
    font-family: Verdana,Tahoma;
    font-weight: bold;
    font-size: x-small;
    color:Red;
    vertical-align: middle;
    text-align:left;
}
.Field
{
    font-family: Verdana,Tahoma;
    font-weight: normal;
    font-size: x-small;
    vertical-align: middle;
    color: #16518A;
}
.PCGButton
{
    /*background: url(/Images/PCG_gradient_button1.jpg) no-repeat left top; /*background-repeat: no-repeat;*/
    background-color:Purple;
    background-image: url(/Images/PCG_gradient_button1.jpg);
    font-weight: bolder;
    color: White;
    font-size: x-small;
    font-family: Verdana,Tahoma;
    height: 22px;
    width: 69px;
    cursor: pointer;
}
.HeaderText
{
    font-family: Verdana,Tahoma;
    font-weight: bold;
    font-size: x-small;
    color: #523C7D;/*#16518A;*/
}
.HeaderDateText
{
    font-family: Verdana,Tahoma;
    font-weight: bold;
    font-size: x-small;
    color: #FFFFFF;
}

.HeaderTextBig
{
    font-family: Verdana,Tahoma;
    font-weight: bold;
    font-size: medium; /*color: #993434;*/
    color: #523C7D;
}
        .style1
        {
            width: 194px;
        }
    </style>
</head>
<body style="width:100px;height:auto;">
    <form id="form1" runat="server">
   <table id="tblLogoBlock" runat="server"><tr align="center"><td colspan="2"><img alt="Advisor Logo" id="imgLogo" runat="server" src="~/Images/Money_Touch_360_logo1.png" width="200" />
    </td></tr></table>
    <table width="200px" style="border-style:solid" runat="server" id="tblLoginBlock">
    
    <tr align="center" style="border-bottom-style:solid;border-bottom-width:thin;"><td colspan="2"><asp:Label ID="lblLoginHeader" Text="Login" 
            runat="server" CssClass="HeaderTextBig" ></asp:Label> </td></tr>
    <tr>
    <td><asp:Label ID="lblLoginId" Text="Login Id:" runat="server" CssClass="FieldName" ></asp:Label></td>
    <td><asp:TextBox ID="txtUserName" runat="server" Text="" CssClass="Field"></asp:TextBox></td>
    </tr>
    <tr>
    <td><asp:Label ID="lblPassword" Text="Password:" runat="server" CssClass="FieldName"></asp:Label></td>
    <td><asp:TextBox ID="txtPassword"  TextMode="Password" runat="server" CssClass="Field"></asp:TextBox></td>
    </tr>
    <tr align="center"><td colspan="2"><asp:Button ID="btnLogin" Text="Login" 
            runat="server" onclick="btnLogin_Click" CssClass="PCGButton"></asp:Button></td> </tr>
            
    </table>
    <table style="width: 195px"><tr align="center"><td colspan="2"><asp:Label ID="lblLoginMessage" Text="Invalid LoginId or Password" Visible="false" runat="server"  CssClass="FieldName"></asp:Label></td></tr>
    <tr align="center"><td><asp:LinkButton ID="lnklogout" 
            Text="Login as Different User" runat="server" onclick="lnklogout_Click" CssClass="HeaderText"></asp:LinkButton></td></tr>
    </table>
    </form>
</body>
</html>
