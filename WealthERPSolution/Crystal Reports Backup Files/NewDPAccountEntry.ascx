<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewDPAccountEntry.ascx.cs" Inherits="WealthERP.CustomerPortfolio.NewDPAccountEntry" %>

<style type="text/css">
    .style1
    {
    }
    .style2
    {
        width: 246px;
    }
    .style3
    {
        width: 124px;
    }
</style>
<table style="width:100%;">
    <tr>
        <td class="style3">
            &nbsp;</td>
        <td colspan="3">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblNewDPAcc" runat="server" Text="New DP Account Entry Form" 
                CssClass="HeaderTextSmall"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style3">
            &nbsp;</td>
        <td class="style2">
            <asp:Label ID="LlblAssetClass" runat="server" Text="Asset Class" 
                CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtAssetClass" runat="server" CssClass="txtField" 
                Enabled="False">Equity</asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            &nbsp;</td>
        <td class="style2">
            <asp:Label ID="LablblDPNo" runat="server" Text="DP No." CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtDPNo" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
            &nbsp;</td>
        <td class="style1" colspan="3">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                CssClass="ButtonField" onclick="btnSubmit_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="ButbtnAddTran" runat="server" Text="Add transaction" 
                CssClass="ButtonField" onclick="ButbtnAddTran_Click" />
        </td>
    </tr>
</table>
