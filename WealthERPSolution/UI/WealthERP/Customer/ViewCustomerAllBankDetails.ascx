<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewCustomerAllBankDetails.ascx.cs"
    Inherits="WealthERP.Customer.ViewCustomerAllBankDetails" %>


<table class="TableBackground" style="width: 100%;" >
    <tr>
            <td colspan="4" class="HeaderCell">
                <asp:Label ID="Label34" runat="server" Text="Bank Details" CssClass="HeaderTextBig"></asp:Label>
                <hr />
            </td>
        </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
            <asp:Label ID="Label30" runat="server" Text="Bank Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" width="25%">
            <asp:Label ID="lblBankName" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
        <td class="leftField" width="25%">
            <asp:Label ID="Label31" runat="server" Text="Branch Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" width="25%">
            <asp:Label ID="lblBranchName" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label28" runat="server" Text="Account Number:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblAccNum" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
        <td class="leftField">
            <asp:Label ID="Label27" runat="server" Text="Account Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblAccType" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label29" runat="server" Text="Mode of Operation:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:Label ID="lblModeOfOperation" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style11" colspan="4">
            <asp:Label ID="Label26" runat="server" Text="Branch Details" Font-Bold="True" CssClass="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label19" runat="server" Text="Line1(House No/Building):" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblLine1" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
        <td class="leftField">
            <asp:Label ID="Label20" runat="server" Text="Line2(Street):" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblLine2" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label21" runat="server" Text="Line3(Area):" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblLine3" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
        <td class="leftField">
            <asp:Label ID="Label24" runat="server" Text="Pin Code:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblPinCode" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label22" runat="server" Text="City:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblCity" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
        <td class="leftField">
            <asp:Label ID="Label23" runat="server" Text="State:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblState" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label25" runat="server" Text="Country:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:Label ID="lblCountry" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label33" runat="server" Text="IFSC:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblIfsc" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
        <td class="leftField">
            <asp:Label ID="Label32" runat="server" Text="MICR:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblMicr" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4" class="SubmitCell">
            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete"
                CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_ViewCustomerAllBankDetails_btnDelete', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_ViewCustomerAllBankDetails_btnDelete', 'S');" />
            &nbsp;
            <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="PCGButton"
                onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_ViewCustomerAllBankDetails_btnBack', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_ViewCustomerAllBankDetails_btnBack', 'S');" />
        </td>
    </tr>
</table>
