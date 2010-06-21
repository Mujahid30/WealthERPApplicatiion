<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMCustomerNonIndividualDashboard.ascx.cs" Inherits="WealthERP.Advisor.RMCustomerNonIndividualDasboard" %>


<style type="text/css">
    .style1
    {
        width: 166px;
    }
    .style2
    {
        width: 653px;
        height: 173px;
    }
    .style3
    {
        width: 172px;
    }
    </style>
<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="RM Customer Non-Individual Dashboard"></asp:Label>
            <hr />
        </td>
    </tr>
</table>


<table class="style2" class="TableBackground">
    <tr>
        <td class="style1" align="left">
            <asp:Label ID="Label3" runat="server" Text="Company Name" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style3" align="left">
            <asp:Label ID="lblName" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style1" align="left">
            <asp:Label ID="Label4" runat="server" Text="Phone Number" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style3" align="left">
            <asp:Label ID="lblPhone" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style1" align="left">
            <asp:Label ID="Label5" runat="server" Text="Email" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style3" align="left">
            <asp:Label ID="lblEmail" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label9" runat="server" Text="PAN Number" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style3">
            <asp:Label ID="lblPanNum" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="lblLeftAddress" runat="server" Text="Address" 
                CssClass="FieldName"></asp:Label>
        </td>
        <td class="style3">
            <asp:Label ID="lblAddress" runat="server" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label11" runat="server" Text="Contact Person" 
                CssClass="FieldName"></asp:Label>
        </td>
        <td class="style3">
            <asp:Label ID="lblContactPerson" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
</table>



