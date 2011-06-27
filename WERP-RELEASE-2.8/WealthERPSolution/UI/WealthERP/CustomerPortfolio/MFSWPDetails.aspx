<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MFSWPDetails.aspx.cs" Inherits="WealthERP.CustomerPortfolio.MFSWPDetails" %>


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
            width: 145px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:50%;" align="center">
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ID="Label1" runat="server" Text="SWP Details" 
                        CssClass="HeaderTextSmall"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" class="style2">
                    <asp:Label ID="Label2" runat="server" Text="Start Date" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="style2">
                    <asp:Label ID="Label3" runat="server" Text="Period" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPeriod" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="style2">
                    <asp:Label ID="Label4" runat="server" Text="End Date" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="style2">
                    <asp:Label ID="Label5" runat="server" Text="Frequency" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFrequency" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="style2">
                    <asp:Label ID="Label6" runat="server" Text="Amount" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="style2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" class="style1" colspan="2">
                    <asp:Label ID="Label7" runat="server" CssClass="FieldName" 
                        Text="Note: All the SWPs from future will be created by the System Automatically"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" colspan="2">
                    <asp:Label ID="Label8" runat="server" CssClass="FieldName" 
                        Text="Please Enter Details for First Transaction only"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" class="style1" colspan="2">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="ButtonField" 
                        Text="Submit" onclick="btnSubmit_Click" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>