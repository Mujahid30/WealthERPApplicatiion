<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerIndividualDashboard.ascx.cs"
    Inherits="WealthERP.Customer.CustomerDashboard" %>

<table class="TableBackground">
    <tr>
        <td colspan="6">
            <asp:Label ID="Label2" runat="server" Text="Profile" CssClass="HeaderTextSmall"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label3" runat="server" Text="Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="5" class="rightField">
            <asp:Label ID="lblName" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftAddressField">
            <asp:Label ID="Label9" runat="server" Text="Address:" CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="5" class="rightField">
            <asp:Label ID="lblAddress" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label4" runat="server" Text="Phone Number:" CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="5" class="rightField">
            <asp:Label ID="lblPhone" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr id="trFamilyMembers" runat="server">
        <td colspan="6">
            <asp:Label ID="lblFamilyMembers" runat="server" Text="Family Members" CssClass="HeaderTextSmall"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:GridView ID="gvFamilyMembers" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" DataKeyNames="AssociationId" AllowPaging="True" CssClass="GridViewStyle">
                <RowStyle BackColor="#EFF3FB" CssClass="RowStyle" />
                <FooterStyle ForeColor="White" Font-Bold="True" CssClass="FooterStyle" />
                <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" CssClass="SelectedRowStyle" />
                <HeaderStyle ForeColor="White" Font-Bold="True" CssClass="HeaderStyle" />
                <EditRowStyle Font-Size="X-Small" CssClass="EditRowStyle" />
                <AlternatingRowStyle BackColor="White" BorderColor="Black" BorderStyle="None" CssClass="AltRowStyle" />
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Relationship" HeaderText="Relationship" SortExpression="Relationship" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
