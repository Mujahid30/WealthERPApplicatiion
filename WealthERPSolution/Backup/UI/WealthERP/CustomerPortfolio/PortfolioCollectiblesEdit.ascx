<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioCollectiblesEdit.ascx.cs" Inherits="WealthERP.CustomerPortfolio.PortfolioCollectiblesEdit" %>

<style type="text/css">
    .style1
    {
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
            <asp:TextBox ID="txtAssetParticulars" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" class="style1">
            <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="Purchase Date"></asp:Label>
        </td>
        <td align="left">
         
            <asp:TextBox ID="txtPurchaseDate" runat="server" CssClass="txtField"></asp:TextBox>
&nbsp;&nbsp;&nbsp; <img  src="../CSS/Images/images6.jpg" alt="Pick a Date" 
                style="cursor: pointer; width: 30px;" id="imgPurchaseDate" /></td>
                <script type="text/javascript">
                    Calendar.setup(
                                            {
                                                inputField: "txtPurchaseDate",         // ID of the input field
                                                ifFormat: "%m/%d/%Y",    // the date format
                                                button: "imgPurchaseDate"       // ID of the button
                                            }
                                          ); 
                            </script>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" class="style1">
            <asp:Label ID="Label7" runat="server" CssClass="FieldName" 
                Text="Purchase Value"></asp:Label>
        </td>
        <td align="left">
            <asp:TextBox ID="txtPurchaseValue" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" class="style1">
            <asp:Label ID="Label9" runat="server" CssClass="FieldName" Text="Current Value"></asp:Label>
        </td>
        <td align="left">
            <asp:TextBox ID="txtCurrentValue" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" class="style1">
            <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="Remarks"></asp:Label>
        </td>
        <td align="left">
            <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style1" colspan="2">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            &nbsp;<asp:Button ID="btnSaveChanges" runat="server" CssClass="ButtonField" 
                Height="31px" Text="Save Changes" Width="116px" />
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
</table>

