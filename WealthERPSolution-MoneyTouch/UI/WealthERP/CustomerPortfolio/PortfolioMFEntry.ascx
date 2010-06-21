<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioMFEntry.ascx.cs" Inherits="WealthERP.CustomerPortfolio.PortfolioMFEntry" %>

<style type="text/css">
    .style1
    {
        width: 161px;
    }
    .style2
    {
        width: 579px;
    }
</style>

<table style="width: 100%; height: 202px;" CssClass="TableBackground">
    <tr>
        <td class="style1">
            &nbsp;</td>
        <td class="style2" align="center">
            <asp:Label ID="Label1" runat="server" Text="Portfolio Transaction Entry Form" CssClass="HeaderTextSmall"></asp:Label>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style1">
            &nbsp;</td>
        <td class="style2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3" align="left">
            <asp:Label ID="Label2" runat="server" Text="Source of Transaction" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style1" align="left">
            <asp:RadioButton ID="rbtnManual" runat="server" 
                GroupName="rbtnTransactionSource" Text="Manual" CssClass="txtField" />
        </td>
        <td class="style2" align="left">
            <asp:RadioButton ID="rbtnUpload" runat="server" 
                GroupName="rbtnTransactionSource" Text="Upload" CssClass="txtField" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3" align="left">
            <asp:Label ID="Label3" runat="server" Text="Input For" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style1" align="left">
            <asp:RadioButton ID="rbtnSingle" runat="server" GroupName="rbtnInput" CssClass="txtField"
                Text="Single Transaction" />
        </td>
        <td class="style2" align="left">
            <asp:RadioButton ID="rbtnMultiple" runat="server" GroupName="rbtnInput" CssClass="txtField"
                Text="Multiple Transaction" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style1">
            &nbsp;</td>
        <td class="style2" align="center">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                onclick="btnSubmit_Click" CssClass="ButtonField" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
</table>

