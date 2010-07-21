<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMCustomerIndividualDashboard.ascx.cs"
    Inherits="WealthERP.Advisor.RMCustomerDashboard" %>
<style type="text/css">
    .style5
    {
        width: 270px;
    }
</style>
<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Customer Individual Dashboard"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table class="TableBackground" width="100%">
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td width="50%">
            <asp:Label ID="lblPersonalDetails" runat="server" Text="Personal Details" Class="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
        <td width="50%" id="tdBankDetailsHeader" runat="server">
            <asp:Label ID="lblBankDetails" runat="server" Text="Bank Details" Class="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td width="50%">
            <table style="width: 457px; height: 100px; margin-top: 0px">
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label3" runat="server" Text="Name:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:Label ID="lblName" runat="server" Text="Label" CssClass="Field"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label5" runat="server" Text="Address:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:Label ID="lblAddress" runat="server" Text="Label" CssClass="Field"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" id="tdBankDetailsGrid" runat="server">
            <asp:GridView ID="gvBankDetails" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" EnableViewState="false" CssClass="GridViewStyle">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:BoundField DataField="Bank Name" HeaderText="Bank" />
                    <asp:BoundField DataField="Account Type" HeaderText="A/C Type" />
                    <asp:BoundField DataField="Account Number" HeaderText="A/C Number" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td width="50%">
            <asp:Label ID="Label1" runat="server" Text="Contact Details" Class="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
        <td width="50%" id="tdFamilyDetailsHeader" runat="server">
            <asp:Label ID="Label2" runat="server" Text="Family Details" Class="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td width="50%">
            <table style="width: 457px; height: 100px; margin-top: 0px">
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label4" runat="server" Text="Phone Number:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:Label ID="lblPhone" runat="server" Text="Label" CssClass="Field"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label6" runat="server" Text="Email:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:Label ID="lblEmail" runat="server" Text="Label" CssClass="Field"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" id="tdFamilyDetailsGrid" runat="server">
            <asp:GridView ID="gvFamilyMembers" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" EnableViewState="false" CssClass="GridViewStyle" DataKeyNames="AssociationId">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Relationship" HeaderText="Relationship" />
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
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
</table>
