<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserSettings.ascx.cs"
    Inherits="WealthERP.General.UserSettings" %>
<table style="top:250px;Position:relative;left:500px">
    <tr>
        <td>
            <asp:LinkButton ID="lnkChangePassword" CssClass="FieldName" runat="server" OnClientClick="javascript:loadcontrol('ChangePassword', 'none'); return false;" Font-Size="Medium">Change Password</asp:LinkButton>
        </td>
    </tr>
    <tr>
    <td>
    </td>
    </tr>
    <tr>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lnkChangeLoginID" CssClass="FieldName" runat="server" OnClientClick="javascript:loadcontrol('ChangeLoginId', 'none'); return false;" Font-Size="Medium">Change LoginID</asp:LinkButton>
        </td>
    </tr>
</table>
