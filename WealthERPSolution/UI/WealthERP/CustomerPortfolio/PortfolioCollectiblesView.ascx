<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioCollectiblesView.ascx.cs" Inherits="WealthERP.CustomerPortfolio.PortfolioCollectiblesView" %>



<style type="text/css">


.HeaderTextBig
{
	font-family:Verdana,tahoma;
	font-weight:bold;
	font-size:small;
	color:#16518A;
}

.FieldName
{
	font-family:Verdana,tahoma;
	font-weight:bold;
	font-size:x-small;
	color:#16518A;
	
}

.FieldName
{
	font-family:Verdana,tahoma;
	font-weight:bold;
	font-size:x-small;
	color:#16518A;
	
}

    .style1
    {
        width: 342px;
    }

</style>
<table style="width:100%;">
    <tr>
        <td colspan="2">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" 
                Text="Collectibles "></asp:Label>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style1">
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" class="style1">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" 
                Text="Asset Instrument Category"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="lblInsCategory" runat="server" Text="Label"></asp:Label>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" class="style1">
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" 
                Text="Asset Particulars"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="lblAssetParticulars" runat="server" Text="Label"></asp:Label>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" class="style1">
            <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="Purchase Date"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="lblPurchaseDate" runat="server" Text="Label"></asp:Label>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" class="style1">
            <asp:Label ID="Label7" runat="server" CssClass="FieldName" 
                Text="Purchase Value"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="lblPurchaseValue" runat="server" Text="Label"></asp:Label>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" class="style1">
            <asp:Label ID="Label9" runat="server" CssClass="FieldName" Text="Current Value"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="lblCurrentValue" runat="server" Text="Remar"></asp:Label>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" class="style1">
            <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="Remarks"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="lblRemarks" runat="server" Text="Label"></asp:Label>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style1">
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style1">
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
