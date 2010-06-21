<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerPortfolio.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.CustomerPortfolio" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<table style="width: 100%;">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="Customer Portfolios"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr id="trPage" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr id="trMessage" runat="server" visible="false">
        <td>
            <asp:Label ID="lblMessage" runat="server" CssClass="Error" Text="No Records Found..."></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvCustomerPortfolio" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ShowFooter="True" CssClass="GridViewStyle" EnableViewState="True"
                DataKeyNames="CP_PortfolioId" AllowSorting="True" OnRowCommand="gvCustomerPortfolio_RowCommand">
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Customer Name" ShowHeader="False">
                        <HeaderTemplate>
                            <asp:Label ID="lblCustNameHeader" runat="server" Text="Customer Name"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtCustNameSearch" Text='<%# hdnNameFilter.Value %>' runat="server"
                                CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_CustomerPortfolio_btnNameSearch');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="EditDetails"
                                Text='<%# Eval("CustomerName") %>' CommandArgument='<%# Eval("CP_PortfolioId") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Portfolio Name">
                        <ItemTemplate>
                            <asp:Label ID="lblPortfolioName" runat="server" Text='<%# Bind("PortfolioName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate>
                            <asp:Label ID="lblType" runat="server" Text='<%# Bind("PortfolioType") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AUM">
                        <ItemTemplate>
                            <asp:Label ID="lblAUM" runat="server" Text='<%# Bind("AUM") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PMS Identifier">
                        <ItemTemplate>
                            <asp:Label ID="lblPMSIdentifier" runat="server" Text='<%# Bind("PMSIdentifier") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Is Main Portfolio?">
                        <ItemTemplate>
                            <asp:Label ID="lblIsMainPortfolio" runat="server" Text='<%# Bind("IsMainPortfolio") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
<table style="width: 100%">
    <tr id="trPager" runat="server">
        <td align="center">
            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>
    </tr>
</table>
<asp:Button ID="btnNameSearch" runat="server" Text="" BorderStyle="None" BackColor="Transparent"
    OnClick="btnNameSearch_Click1" />
<asp:HiddenField ID="hdnNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
