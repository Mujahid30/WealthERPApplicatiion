<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquityProrate.aspx.cs" Inherits="WealthERP.CustomerPortfolio.EquityProrate" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
    <div>
    
        <table style="width:100%;">
            <tr>
                <td colspan="3" align="left">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server" Text="Prorate Charges" 
                        CssClass="HeaderTextSmall"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3" align="left">
                    <asp:Label ID="Label2" runat="server" Text="Service Tax" CssClass="FieldName"></asp:Label>
                </td>
                <td class="style2" align="left">
                    <asp:TextBox ID="txtServiceTax" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
                <td align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3" align="left">
                    <asp:Label ID="Label3" runat="server" Text="STT" CssClass="FieldName"></asp:Label>
                </td>
                <td class="style2" align="left">
                    <asp:TextBox ID="txtSTT" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
                <td align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3" align="left">
                    <asp:Label ID="Label4" runat="server" Text="Additional Charges" 
                        CssClass="FieldName"></asp:Label>
                </td>
                <td class="style2" align="left">
                    <asp:TextBox ID="txtAdditionalCharge" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
                <td align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1" colspan="3" align="left">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="ButtonField" 
                        onclick="btnSubmit_Click" />
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="3" align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1" colspan="3" align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1" colspan="3" align="left">
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
