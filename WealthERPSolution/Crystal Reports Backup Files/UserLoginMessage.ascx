<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserLoginMessage.ascx.cs"
    Inherits="WealthERP.Advisor.UserLoginMessage" %>

<table style="width: 100%;">
    <tr>
        <td colspan="4">
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblMsg" runat="server" CssClass="FieldName" Text="Your user account has been successfully created !"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblRegistrationEmail" runat="server" Text="You will shortly receive an email with your user account details."
                CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="Label2" runat="server" Text="Type in your username and password(check email) to login to the Advisor Dashboard . This Dashboard allows you to check or update your account ."
                CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
        </td>
        
    </tr>
   
    <tr>
        <td>
        </td>
        <td style="margin-left: 40px">
        </td>
        <td>
        </td>
        <td>
            <asp:LinkButton ID="lnkUserLogin" runat="server" CssClass="LinkButtons" OnClick="lnkUserLogin_Click">Login</asp:LinkButton>
        </td>
    </tr>
</table>
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
