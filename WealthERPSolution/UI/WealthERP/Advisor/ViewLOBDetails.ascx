<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewLOBDetails.ascx.cs"
    Inherits="WealthERP.Advisor.ViewLOBDetails" %>

<table class="TableBackground" style="width: 46%;">
    <tr>
        <td colspan="2">
        </td>
    </tr>
    <tr>
        <td class="HeaderCell" colspan="2">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="View LOB Details"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="LinkButtons" OnClick="LinkButton1_Click">Edit</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="margin-left: 100px">
            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Intermediary Name:"
                Width="200px"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblOrgname" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Identifier:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblIdentifier" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="margin-left: 120px">
            <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Business type:"
                Width="200px"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblBtype" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="margin-left: 120px">
            <asp:Label ID="lblLicenseNo" runat="server" CssClass="FieldName" Text="License Number:"
                Width="200px"></asp:Label>
        </td>
        <td class="rightField" width="200">
            <asp:Label ID="lblLicenseNumber" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="margin-left: 120px">
            <asp:Label ID="lblValidity" runat="server" CssClass="FieldName" Text="Validity:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblValiditydate" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
</table>
