<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditCustomerBankAccount.ascx.cs"
    Inherits="WealthERP.Customer.EditCustomerBankAccount" %>

<style type="text/css">
    .style1
    {
        width: 162px;
    }
    .style8
    {
        width: 170px;
    }
    .style3
    {
        width: 143px;
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
    .style13
    {
        width: 176px;
    }
    .style12
    {
        width: 69px;
    }
</style>
<table cssclass="TableBackground">
    <tr>
        <td class="style1">
            &nbsp;
        </td>
        <td colspan="2">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1" align="left">
            <asp:Label ID="Label27" runat="server" CssClass="FieldName" Text="Account Type"></asp:Label>
        </td>
        <td class="style8" align="left">
            <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="cmbField" Width="180px">
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
        <td class="style3" align="left">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1" align="left">
            <asp:Label ID="Label28" runat="server" CssClass="FieldName" Text="Account Number"></asp:Label>
        </td>
        <td class="style8" align="left">
            <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="style3" align="left">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1" align="left">
            <asp:Label ID="Label29" runat="server" Text="Mode of Operation" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style8" align="left">
            <asp:DropDownList ID="ddlModeOfOperation" CssClass="cmbField" runat="server">
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
        <td class="style1" align="left">
            <asp:Label ID="Label30" runat="server" Text="Bank Name" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style8" align="left">
            <asp:TextBox ID="txtBankName" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="style3">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1" align="left">
            <asp:Label ID="Label31" runat="server" Text="Branch Name" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style8" align="left">
            <asp:TextBox ID="txtBranchName" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="style3">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1" align="left">
            &nbsp;
        </td>
        <td class="style8" align="left">
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
        <td class="style1" align="left">
            <asp:Label ID="Label26" runat="server" Text="Branch Details" CssClass="FieldName"></asp:Label>
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
        <td class="style1" align="left">
            <asp:Label ID="Label19" runat="server" Text="Line1" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style8" align="left">
            <asp:TextBox ID="txtBankAdrLine1" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="style3">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1" align="left">
            <asp:Label ID="Label20" runat="server" Text="Line2" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style8" align="left">
            <asp:TextBox ID="txtBankAdrLine2" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="style3" align="left">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1" align="left">
            <asp:Label ID="Label21" runat="server" Text="Line3" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style8" align="left">
            <asp:TextBox ID="txtBankAdrLine3" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="style3">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1" align="left">
            <asp:Label ID="Label22" runat="server" CssClass="FieldName" Text="City"></asp:Label>
        </td>
        <td class="style8" align="left">
            <asp:TextBox ID="txtBankAdrCity" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="style3" align="left">
            <asp:Label ID="Label23" runat="server" CssClass="FieldName" Text="State"></asp:Label>
        </td>
        <td align="left">
            <asp:DropDownList ID="ddlBankAdrState" runat="server" CssClass="txtField">
                <asp:ListItem>Karnataka</asp:ListItem>
                <asp:ListItem>Andhra</asp:ListItem>
                <asp:ListItem>Tamil Nadu</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="style1" align="left">
            <asp:Label ID="Label24" runat="server" Text="Pin Code" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style8" align="left">
            <asp:TextBox ID="txtBankAdrPinCode" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="style3" align="left">
            <asp:Label ID="Label25" runat="server" Text="Country" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:DropDownList ID="ddlBankAdrCountry" runat="server" CssClass="cmbField">
                <asp:ListItem>India</asp:ListItem>
                <asp:ListItem>USA</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="style1" align="left">
            <asp:Label ID="Label32" runat="server" Text="MICR" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style8" align="left">
            <asp:TextBox ID="txtMicr" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="style3" align="left">
            <asp:Label ID="Label33" runat="server" Text="IFSC" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:TextBox ID="txtIfsc" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style9">
        </td>
        <td class="style10" colspan="2" align="left">
        </td>
        <table style="width: 549px">
            <tr>
                <td class="style13" align="left">
                </td>
                <td class="style12" align="left">
                    &nbsp;
                </td>
                <td align="left">
                    <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Save" CssClass="PCGButton"
                        onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_EditCustomerBankAccount_btnEdit', 'S');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_EditCustomerBankAccount_btnEdit', 'S');" />
                </td>
            </tr>
        </table>
        <td class="style11">
        </td>
    </tr>
</table>
