<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFNewFolioEntry.ascx.cs" Inherits="WealthERP.CustomerPortfolio.MFNewFolioEntry" %>

<style type="text/css">
    .style1
    {
        width: 1002px;
        height: 242px;
    }
    .style2
    {
    }
    .style3
    {
        width: 179px;
    }
    .style4
    {
        width: 262px;
    }
    .style5
    {
        width: 27px;
    }
</style>
<table class="style1">
    <tr>
        <td class="style4">
            &nbsp;</td>
        <td colspan="2" align="center">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextSmall" 
                Text="New Folio Account Entry Form"></asp:Label>
        </td>
        <td class="style2">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style4">
            &nbsp;</td>
        <td class="style3">
            &nbsp;</td>
        <td class="style5">
            &nbsp;</td>
        <td class="style2">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style4">
            &nbsp;</td>
        <td class="style3" align="left">
            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Asset Class"></asp:Label>
        </td>
        <td class="style5" align="left">
            <asp:TextBox ID="txtAssetClass" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="style2">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style4">
            &nbsp;</td>
        <td class="style3" align="left">
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="AMC Name"></asp:Label>
        </td>
        <td class="style5" align="left">
            <asp:TextBox ID="txtAMCName" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="style2">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style4">
            &nbsp;</td>
        <td class="style3" align="left">
            <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Folio Number"></asp:Label>
        </td>
        <td class="style5" align="left">
            <asp:TextBox ID="txtFolioNum" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="style2">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style4">
            &nbsp;</td>
        <td class="style3" align="left">
            <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Nominee Name"></asp:Label>
        </td>
        <td class="style5" align="left">
            <asp:TextBox ID="txtNomineeName" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="style2">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style4">
            &nbsp;</td>
        <td class="style3">
            &nbsp;</td>
        <td class="style5">
            &nbsp;</td>
        <td class="style2">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style4">
            &nbsp;</td>
        <td class="style2" colspan="2" align="center">
            <asp:Button ID="Button1" runat="server" CssClass="ButtonField" Text="Submit" />
        </td>
        <td class="style2">
            &nbsp;</td>
    </tr>
</table>
