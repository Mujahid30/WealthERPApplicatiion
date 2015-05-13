<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetTheme.ascx.cs" Inherits="WealthERP.General.SetTheme" %>


<table width="100%" class="TableBackground">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Theme
                        </td>
                        <td align="right" style="padding-bottom: 2px;">
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblSelect" runat="server" Text="Select Theme :" CssClass="FieldName"></asp:Label>
            <asp:DropDownList runat="server" ID="ddlTheme" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlTheme_SelectedIndexChanged">
                <asp:ListItem Value="0">Select a Theme</asp:ListItem>
                <asp:ListItem Value="BlackAndWhite">Black & white</asp:ListItem>
                <asp:ListItem Value="Blue">Blue</asp:ListItem>
                <asp:ListItem Value="Desert">Desert</asp:ListItem>
                <asp:ListItem Value="Green">Green</asp:ListItem>
                <asp:ListItem Value="Maroon">Maroon</asp:ListItem>
                <asp:ListItem Value="Purple">Purple</asp:ListItem>
                <asp:ListItem Value="Yellow">Yellow</asp:ListItem>
                <asp:ListItem Value="LightPurple">LightPurple</asp:ListItem>
                <asp:ListItem Value="SBICAP">Light Blue</asp:ListItem>
                <asp:ListItem Value="SBIOnLine">SBI Blue</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>


