<%@  Page Language="C#" AutoEventWireup="true" CodeBehind="MoneyBackEpisodeAdd.aspx.cs" Inherits="WealthERP.CustomerPortfolio.MoneyBackEpisodeAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
        }
        .style2
        {
            width: 255px;
        }
        .style3
        {
            width: 166px;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">

<table style="width:100%;">
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextSmall" 
                Text="Add MoneyBack Episode"></asp:Label>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:Label ID="Label2" runat="server" CssClass="HeaderTextSmall" 
                Text="Dates of repayment"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label3" runat="server" CssClass="HeaderTextSmall" 
                Text="% of SA repaid"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:PlaceHolder ID="PlaceHolder" runat="server"></asp:PlaceHolder>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:Button ID="btnSubmit" runat="server" CssClass="ButtonField" 
                onclick="btnSubmit_Click" Text="Submit" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>

</body>
</form>