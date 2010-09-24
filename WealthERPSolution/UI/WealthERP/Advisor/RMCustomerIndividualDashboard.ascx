<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMCustomerIndividualDashboard.ascx.cs"
    Inherits="WealthERP.Advisor.RMCustomerDashboard" %>
<style type="text/css">
    .style1
    {
        width: 351px;
    }
</style>
<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell" colspan="2">
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
        <td width="50%">
            <asp:Label ID="lblBankDetails" runat="server" Text="Bank Details" Class="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td width="50%" valign="top">
            <table style="width: 100%; height: 100%; margin-top: 0px">
                <tr>
                    <td class="leftField" width="25%">
                        <asp:Label ID="Label3" runat="server" Text="Name:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:Label ID="lblName" runat="server" Text="Label" CssClass="Field"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="leftField" valign="top">
                        <asp:Label ID="Label5" runat="server" Text="Address:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:Label ID="lblAddress" runat="server" Text="Label" CssClass="Field"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" runat="server" valign="top">
            <asp:Label runat="server" CssClass="FieldName" Text="No details to display.." ID="lblBankDetailsMsg"></asp:Label>
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
            <br />
            <table>
                <tr>
                    <td align="right">
                        <asp:LinkButton ID="lnkMoreBankDetails" runat="server" Text=">>More" ForeColor="Blue"
                            Font-Size="X-Small" Font-Bold="true" OnClick="lnkMoreBankDetails_Click"></asp:LinkButton>
                    </td>
                </tr>
            </table>
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
        <td width="50%" valign="top">
            <table style="width: 100%; height: 100%; margin-top: 0px">
                <tr>
                    <td class="leftField" width="25%">
                        <asp:Label ID="Label4" runat="server" Text="Phone Number:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:Label ID="lblPhone" runat="server" Text="Label" CssClass="Field"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="leftField" width="25%">
                        <asp:Label ID="Label6" runat="server" Text="Email:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:Label ID="lblEmail" runat="server" Text="Label" CssClass="Field"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" id="tdFamilyDetailsGrid" runat="server" valign="top">
            <asp:GridView ID="gvFamilyMembers" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" EnableViewState="false" CssClass="GridViewStyle" DataKeyNames="CustomerId">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Member Name">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkCustomerName" runat="server" CssClass="GridViewCmbField" OnClick="lnkCustomerNameFamilyGrid_Click"
                                Text='<%# Eval("Name") %>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Relationship" HeaderText="Relationship" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
</table>
