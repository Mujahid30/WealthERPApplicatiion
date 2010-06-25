<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserSettings.ascx.cs"
    Inherits="WealthERP.General.UserSettings" %>
<table id="MessageForSuperAdmin" width="100%" runat="server" visible="false">
    <tr>
        <td align="center">
            <div class="success-msg" id="SuccessMessage" runat="server" visible="false" align="center">
                SuperAdmin have nothing to do in this page.
                <asp:LinkButton ID="IFFNavigator" runat="server" Text="Click" OnClick="IFFNavigator_OnClick"></asp:LinkButton> here to navigate to SuperAdmin Main Page.                
            </div>
        </td>
    </tr>
</table>
