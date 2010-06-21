<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerNonIndividualDashboard.ascx.cs"
    Inherits="WealthERP.Customer.CustomerNonIndividualDashboard" %>

<asp:Label ID="Label1" runat="server" Text="RM Customer Dashboard"></asp:Label>
<table class="TableBackground">
    <tr>
        <td colspan="4">
            <asp:Label ID="Label2" runat="server" Text="Profile" CssClass="HeaderTextSmall"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label3" runat="server" Text="Company Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="3">
            <asp:Label ID="lblName" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftAddressField">
            <asp:Label ID="Label5" runat="server" Text="Address:" CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="3">
            <asp:Label ID="lblAddress" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label4" runat="server" Text="Phone Number:" CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="3">
            <asp:Label ID="lblPhone" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblTaxStatus" runat="server" Text="Tax Status" CssClass="HeaderTextSmall"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblTaxStatusValue" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
</table>
