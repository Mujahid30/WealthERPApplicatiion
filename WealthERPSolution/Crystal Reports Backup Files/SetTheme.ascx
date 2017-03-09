<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetTheme.ascx.cs" Inherits="WealthERP.General.SetTheme" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.bxslider.js" type="text/javascript"></script>



       

          <table width="100%">
                <tr>
                    <td colspan="6">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                           Theme
                        </div>
                    </td>
                </tr>
            </table>
            
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



