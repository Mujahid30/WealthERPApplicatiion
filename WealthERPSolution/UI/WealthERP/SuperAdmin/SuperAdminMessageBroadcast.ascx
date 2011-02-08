<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SuperAdminMessageBroadcast.ascx.cs" Inherits="WealthERP.SuperAdmin.SuperAdminMessageBroadcast" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<style type="text/css">
    .style1
    {
        width: 463px;
    }
    .style2
    {
        width: 46px;
    }
</style>
<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<script type="text/javascript" src="/Scripts/jquery.js"></script>
<script type="text/javascript" src="/Scripts/flexigrid.js"></script>
<link rel="stylesheet" type="text/css" href="/App_Themes/Purple/flexigrid.css" /> 
<script type="text/javascript">

</script>

<table width="100%">
    <tr>
        <td align="center">
            <div class="success-msg" id="SuccessMessage" runat="server" visible="false" align="center">
                Message sent to all Advisors Successfully...
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Broadcast Message"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:TextBox ID="MessageBox" runat="server" Width="600px" Height="100px" TextMode="MultiLine"
                MaxLength="255"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <table width="100%">
            <tr align="center">
            <td  align="right"><asp:Label ID="lblExpiryDate" Text="Expiry Date:" runat="server" CssClass="FieldName"></asp:Label>
              
            <td align="left">
            <asp:TextBox ID="txtExpiryDate" CssClass="txtField" runat="server" Width="175px"></asp:TextBox>
            <ajaxToolKit:CalendarExtender runat="server" Format="dd/MM/yyyy" TargetControlID="txtExpiryDate"
                ID="calExeActivationDate" Enabled="true">
            </ajaxToolKit:CalendarExtender>
            <ajaxToolKit:TextBoxWatermarkExtender TargetControlID="txtExpiryDate" WatermarkText="dd/mm/yyyy"
                runat="server" ID="wmtxtActivationDate">
            </ajaxToolKit:TextBoxWatermarkExtender>
            <span id="Span9" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtExpiryDate"
                ErrorMessage="Expiry Date Required" CssClass="cvPCG" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            </tr></table>
        </td>
    </tr>
</table>
<div align="center">
    <asp:Button ID="SendMessage" runat="server" Text="Send Info" CssClass="PCGButton"
        OnClick="SendMessage_Click" />
</div>
<br />
<table width="100%">
    <tr>
        <td align="center">
            <div class="information-msg" id="MessageBroadcast" runat="server" visible="false" align="center">
                <br />
                <asp:Label ID="lblSuperAdmnMessage" runat="server"></asp:Label>
                    <br />
            </div>
        </td>
    </tr>
</table>
<div id="cool"></div>