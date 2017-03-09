<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioEquityEntry.ascx.cs" Inherits="WealthERP.CustomerPortfolio.PortfolioEquityEntry" %>

<style type="text/css">
    .style1
    {
    }
    .style2
    {
        width: 138px;
    }
    .style3
    {
        width: 166px;
    }
</style>
<table style="border: thin groove #C0C0C0; width:100%;" 
    CssClass="TableBackground">
    <tr>
        <td colspan="3" align="left">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextSmall" Text="Portfolio Transaction Entry Form"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style3" align="left">
            <asp:Label ID="Label2" runat="server" CssClass="FieldName"  Text="Source of Transaction "></asp:Label>
        </td>
        <td class="style2" align="left">
            &nbsp;</td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3" align="left">
            &nbsp;</td>
        <td class="style2" align="left">
            <asp:RadioButton ID="rbtnManual" runat="server" CssClass="txtField" 
                GroupName="grpSource" Text="Manual" />
        </td>
        <td align="left">
            <asp:RadioButton ID="rbtnUpload" runat="server" CssClass="txtField" 
                GroupName="grpSource" Text="Upload" />
        </td>
    </tr>
    <tr>
        <td class="style3" align="left">
            <asp:Label ID="Label3" runat="server" Text="Input for" CssClass ="FieldName"></asp:Label>
        </td>
        <td class="style2" align="left">
            &nbsp;</td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3" align="left">
            &nbsp;</td>
        <td class="style2" align="left">
            <asp:RadioButton ID="rbtnSingle" runat="server" CssClass="txtField" 
                GroupName="grpInput" 
                Text="Single" />
        </td>
        <td align="left">
            <asp:RadioButton ID="rbtnMultiple" runat="server" CssClass="txtField" 
                GroupName="grpInput" Text="Multiple" />
        </td>
    </tr>
    <tr>
        <td class="style1" align="left" colspan="3">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSubmit" runat="server" CssClass="ButtonField" 
                Text="Submit" onclick="btnSubmit_Click" />
        </td>
    </tr>
    <tr>
        <td class="style3" align="left">
            &nbsp;</td>
        <td class="style2" align="left">
            &nbsp;</td>
        <td align="left">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3" align="left">
            &nbsp;</td>
        <td class="style2" align="left">
            &nbsp;</td>
        <td align="left">
            &nbsp;</td>
    </tr>
</table>
