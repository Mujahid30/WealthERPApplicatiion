<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserLoanSchemeView.ascx.cs"
    Inherits="WealthERP.Loans.AdviserLoanSchemeView" %>
<%@ Register Src="../General/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<table width="100%"><tr><td align="center">
<div class="warning-msg" id="EditDisabledMessage" runat="server" visible="false">
    <asp:Label ID="lblEditMessageDisabled" runat="server"></asp:Label>
</div>
</td></tr></table>
<table width="100%">
    <tr>
        <td>
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" CssClass="HeaderTextBig" Text="Loan Schemes"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:LinkButton ID="lnkAddNew" CssClass="LinkButtons" runat="server" OnClick="lnkAddNew_Click">Add New Scheme</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvAdviserLoanSchemeView" runat="server" AutoGenerateColumns="False"
                CellPadding="4" DataKeyNames="SchemeId" AllowSorting="True" CssClass="GridViewStyle"
                ShowFooter="true" EmptyDataText="No Schemes available.">
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <RowStyle CssClass="RowStyle" />
                <EmptyDataRowStyle ForeColor="White" HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" CssClass="GridViewCmbField"
                                OnSelectedIndexChanged="ddlAction_OnSelectedIndexChange">
                                <asp:ListItem Text="Select" Value="Select" />
                                <asp:ListItem Text="View" Value="View" />
                                <asp:ListItem Text="Edit" Value="Edit" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SchemeId" HeaderText="Scheme Id" />
                    <asp:BoundField DataField="SchemeName" HeaderText="Scheme Name" />
                    <asp:BoundField DataField="LoanType" HeaderText="Loan Type" />
                    <asp:BoundField DataField="LoanPartner" HeaderText="Loan Partner" />
                    <asp:BoundField DataField="BorrowerType" HeaderText="Borrower Type" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>
            <div id="DivPager" runat="server" style="display: none">
                <uc1:Pager ID="mypager" runat="server" />
            </div>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
