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

<table>
    <tr runat="server" id="trSchemeRating">
        <td colspan="5" align="center">
            <div id="divSchemeRatingDetails" class="popbox">
                <h2 class="popup-title">
                    SCHEME RATING DETAILS
                </h2>
                <table border="1" cellpadding="1" cellspacing="2" style="border-collapse: collapse;">
                    <tr>
                        <td>
                            <asp:Label ID="lblRatingAsOnPopUp" runat="server" CssClass="readOnlyField"></asp:Label>
                        <a href="#" class="popper" data-popbox="divSchemeRatingDetails">
                            
                        </td>
                        <td>
                            <span class="readOnlyField">RATING</span>
                        </td>
                        <td>
                            <span class="readOnlyField">RETURN</span>
                        </td>
                        <td>
                            <span class="readOnlyField">RISK</span>
                        </td>
                        <td>
                            <span class="readOnlyField">RATING OVERALL</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="readOnlyField">3 YEAR</span>
                        </td>
                        <td>
                            <asp:Image runat="server" ID="imgRating3yr" />
                        </td>
                        <td>
                            <asp:Label ID="lblSchemeRetrun3yr" runat="server" CssClass="readOnlyField"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblSchemeRisk3yr" runat="server" CssClass="readOnlyField"></asp:Label>
                        </td>
                        <td rowspan="3">
                            <asp:Image runat="server" ID="imgRatingDetails" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="readOnlyField">5 YEAR</span>
                        </td>
                        <td>
                            <asp:Image runat="server" ID="imgRating5yr" />
                        </td>
                        <td>
                            <asp:Label ID="lblSchemeRetrun5yr" runat="server" CssClass="readOnlyField"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblSchemeRisk5yr" runat="server" CssClass="readOnlyField"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="readOnlyField">10 YEAR</span>
                        </td>
                        <td>
                            <asp:Image runat="server" ID="imgRating10yr" />
                        </td>
                        <td>
                            <asp:Label ID="lblSchemeRetrun10yr" runat="server" CssClass="readOnlyField"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblSchemeRisk10yr" runat="server" CssClass="readOnlyField"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="popup-overlay">
            </div>
        </td>
    </tr>
</table>
