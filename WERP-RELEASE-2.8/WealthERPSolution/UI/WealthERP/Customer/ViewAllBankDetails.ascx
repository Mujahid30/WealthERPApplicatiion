<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewAllBankDetails.ascx.cs"
    Inherits="WealthERP.Customer.ViewAllBankDetails" %>

<%--<style type="text/css">
    .style1
    {
        width: 162px;
    }
    .style3
    {
        width: 143px;
    }
    .style8
    {
        width: 170px;
    }
    .style9
    {
        width: 162px;
        height: 16px;
    }
    .style10
    {
        height: 16px;
    }
    .style12
    {
        width: 133px;
    }
    .style13
    {
        width: 251px;
    }
</style>--%>
<table class="TableBackground">
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label27" runat="server" Text="Account Type" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style8">
            <asp:DropDownList ID="ddlAccountType" runat="server" Width="180px">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem>Savings</asp:ListItem>
                <asp:ListItem>Current</asp:ListItem>
                <asp:ListItem>NRE</asp:ListItem>
                <asp:ListItem>NRO</asp:ListItem>
                <asp:ListItem>F.C.N.R</asp:ListItem>
                <asp:ListItem>O.D.</asp:ListItem>
                <asp:ListItem>C.C.</asp:ListItem>
                <asp:ListItem>Others</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="style3">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label28" runat="server" Text="Account Number"></asp:Label>
        </td>
        <td class="style8">
            <asp:TextBox ID="txtAccountNumber" runat="server" Width="180px"></asp:TextBox>
        </td>
        <td class="style3">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label29" runat="server" Text="Mode of Operation"></asp:Label>
        </td>
        <td class="style8">
            <asp:DropDownList ID="ddlModeOfOperation" runat="server" Width="180px">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem>Self Only</asp:ListItem>
                <asp:ListItem>Either or Survivor</asp:ListItem>
                <asp:ListItem>Former or Survivor</asp:ListItem>
                <asp:ListItem>Anyone or Survivor</asp:ListItem>
                <asp:ListItem>Jointly</asp:ListItem>
                <asp:ListItem>Other</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="style3">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label30" runat="server" Text="Bank Name"></asp:Label>
        </td>
        <td class="style8">
            <asp:TextBox ID="txtBankName" runat="server" Width="180px"></asp:TextBox>
        </td>
        <td class="style3">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label31" runat="server" Text="Branch Name"></asp:Label>
        </td>
        <td class="style8">
            <asp:TextBox ID="txtBranchName" runat="server" Width="180px"></asp:TextBox>
        </td>
        <td class="style3">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1">
            &nbsp;
        </td>
        <td class="style8">
            &nbsp;
        </td>
        <td class="style3">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label26" runat="server" Text="Branch Details"></asp:Label>
        </td>
        <td class="style8">
            &nbsp;
        </td>
        <td class="style3">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label19" runat="server" Text="Line1"></asp:Label>
        </td>
        <td class="style8">
            <asp:TextBox ID="txtBankAdrLine1" runat="server" Width="180px"></asp:TextBox>
        </td>
        <td class="style3">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label20" runat="server" Text="Line2"></asp:Label>
        </td>
        <td class="style8">
            <asp:TextBox ID="txtBankAdrLine2" runat="server" Width="180px"></asp:TextBox>
        </td>
        <td class="style3">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label21" runat="server" Text="Line3"></asp:Label>
        </td>
        <td class="style8">
            <asp:TextBox ID="txtBankAdrLine3" runat="server" Width="180px"></asp:TextBox>
        </td>
        <td class="style3">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label22" runat="server" Text="City"></asp:Label>
        </td>
        <td class="style8">
            <asp:TextBox ID="txtBankAdrCity" runat="server" Width="180px"></asp:TextBox>
        </td>
        <td class="style3">
            <asp:Label ID="Label23" runat="server" Text="State"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlBankAdrState" runat="server" Width="180px">
                <asp:ListItem>Karnataka</asp:ListItem>
                <asp:ListItem>Andhra</asp:ListItem>
                <asp:ListItem>Tamil Nadu</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label24" runat="server" Text="Pin Code"></asp:Label>
        </td>
        <td class="style8">
            <asp:TextBox ID="txtBankAdrPinCode" runat="server" Width="180px"></asp:TextBox>
        </td>
        <td class="style3">
            <asp:Label ID="Label25" runat="server" Text="Country"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlBankAdrCountry" runat="server" Width="180px">
                <asp:ListItem>India</asp:ListItem>
                <asp:ListItem>USA</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label32" runat="server" Text="MICR"></asp:Label>
        </td>
        <td class="style8">
            <asp:TextBox ID="txtMicr" runat="server" Width="180px"></asp:TextBox>
        </td>
        <td class="style3">
            <asp:Label ID="Label33" runat="server" Text="IFSC"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtIfsc" runat="server" Width="180px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style9" colspan="4">
            <table style="width: 549px">
                <tr>
                    <td class="style13">
                    </td>
                    <td class="style12">
                        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="PCGButton"
                            onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_ViewAllBankDetails_btnBack', 'S');"
                            onmouseout="javascript:ChangeButtonCss('out', 'ctrl_ViewAllBankDetails_btnBack', 'S');" />
                    </td>
                    <td>
                        <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Update" CssClass="PCGButton"
                            onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_ViewAllBankDetails_btnEdit', 'S');"
                            onmouseout="javascript:ChangeButtonCss('out', 'ctrl_ViewAllBankDetails_btnEdit', 'S');" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
