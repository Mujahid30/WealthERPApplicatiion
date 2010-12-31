<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetTheme.ascx.cs" Inherits="WealthERP.General.SetTheme" %>
<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderTextBig" colspan="2">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Advisor Theme"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblSelect" runat="server" Text="Select Theme :" CssClass="FieldName"></asp:Label>
            <asp:DropDownList runat="server" ID="ddlTheme" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlTheme_SelectedIndexChanged">
                <asp:ListItem Value="0">Select a Theme</asp:ListItem>
                <asp:ListItem Value="Maroon">Maroon</asp:ListItem>
                <asp:ListItem Value="Purple">Purple</asp:ListItem>
                <asp:ListItem Value="Blue">Blue</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
