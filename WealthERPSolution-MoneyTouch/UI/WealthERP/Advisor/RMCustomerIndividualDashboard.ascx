<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMCustomerIndividualDashboard.ascx.cs"
    Inherits="WealthERP.Advisor.RMCustomerDashboard" %>

<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Customer Individual Dashboard"></asp:Label>
            <hr />
        </td>
    </tr>
</table>

<table class="TableBackground">
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label3" runat="server" Text="Name:" CssClass="FieldName"></asp:Label>
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
    <tr id="trFamilyMembers" runat="server">
        <td colspan="4">
            <asp:Label ID="lblFamilyMembers" runat="server" Text="Family Members" CssClass="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:GridView ID="gvFamilyMembers" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" DataKeyNames="AssociationId" EnableViewState="false" AllowPaging="True"
                CssClass="GridViewStyle" ShowFooter="true" OnPageIndexChanging="gvFamilyMembers_PageIndexChanging">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name"  />
                    <asp:BoundField DataField="Relationship" HeaderText="Relationship"  />
                    <%--<asp:TemplateField>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlMenu" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged">
                                <asp:ListItem>Select </asp:ListItem>
                                <asp:ListItem Text="Edit" Value="Edit">Edit</asp:ListItem>
                                <asp:ListItem Text="View" Value="View">View</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
