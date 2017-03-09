<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserLogout.ascx.cs"
    Inherits="WealthERP.General.UserLogout" %>

<table>
    <tr>
        <td>
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Please " CssClass="FieldName"></asp:Label>
            <asp:LinkButton ID="LinkButton1" CssClass="FieldName" runat="server" OnClientClick="javascript:loadcontrol('Userlogin', 'none'); return false;">click here</asp:LinkButton>
            <asp:Label ID="Label2" runat="server" Text=" to login again." CssClass="FieldName"></asp:Label>
        </td>
    </tr>
</table>
