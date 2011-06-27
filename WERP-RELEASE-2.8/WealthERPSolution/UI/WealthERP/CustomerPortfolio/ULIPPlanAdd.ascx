<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ULIPPlanAdd.ascx.cs" Inherits="WealthERP.CustomerPortfolio.ULIPPlanAdd" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<style type="text/css">
    .GridViewStyle
{
    font-family: Arial, Sans-Serif;
    font-size:small;
    table-layout: auto;
    border-collapse: collapse;
    border: #1d1d1d 5px solid;
    margin-left: 0px;
}
.HeaderStyle, .PagerStyle /*Common Styles*/
{
    background-image: url('../CSS/Images/HeaderGlassBlack.jpg');
    background-position:center;
    background-repeat:repeat-x;
    background-color:#1d1d1d;
}
.HeaderStyle th
{
    padding: 5px;
    color: #ffffff;
}
.HeaderStyle a
{
    text-decoration:none;
    color:#ffffff;
    display:block;
    text-align:left;
    font-weight:normal;
}
.PagerStyle a
{
    color:#ffffff;
    text-decoration:none;
    padding:2px 10px 2px 10px;
    border-top:solid 1px #777777;
    border-right:solid 1px #333333;
    border-bottom:solid 1px #333333;
    border-left:solid 1px #777777;
}
    .style1
    {
        width: 532px;
    }
</style>

<table style="width:100%;">
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td colspan="2">
            <asp:Label ID="Label3" runat="server" CssClass="HeaderTextSmall" 
                Text="ULIP Plans and Allocations"></asp:Label>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="Label4" runat="server" CssClass="FieldName" 
                Text="Insurance Issuer Code"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlInsuranceIssuerCode" runat="server" 
                CssClass="cmbField" AutoPostBack="True" 
                onselectedindexchanged="ddlInsuranceIssuerCode_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="ULIP Plan"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlUlipPlans" runat="server" CssClass="cmbField" 
                AutoPostBack="True" onselectedindexchanged="ddlUlipPlans_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    </table>
<table style="width:100%;">
    <tr>
        <td class="style1">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblAllocation" runat="server" CssClass="FieldName" 
                Text="Allocation Percentage"></asp:Label>
                    </td>
        <td>
            <asp:Label ID="lblUnits" runat="server" CssClass="FieldName" Text="Units"></asp:Label>
                    </td>
        <td>
            <asp:Label ID="lblPurchasePrice" runat="server" CssClass="FieldName" 
                Text="Purchase Price"></asp:Label>
                    </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </td>
    </tr>
    <tr>
        <td class="style1">
            
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            
            <asp:Button ID="btnSubmit" runat="server" onclick="Button1_Click" Text="Submit" 
                CssClass="ButtonField" />
            
            <br />
           
            <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
           
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>

