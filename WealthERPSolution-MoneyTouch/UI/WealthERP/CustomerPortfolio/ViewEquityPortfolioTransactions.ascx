<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewEquityPortfolioTransactions.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.ViewEquityTransactions" %>

<table style="width: 100%;">
    <%--<tr>
        <td>
            &nbsp;
        </td>
        <td align="left" class="HeaderTextBig">
            Equity Portfolio Transactions Valuation View
        </td>
        <td>
            &nbsp;
        </td>
    </tr>--%>
    <tr>
        <td colspan="3">
            <asp:LinkButton ID="lnkBack" CssClass="LinkButtons" runat="server" Text="Back" OnClick="lnkBack_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width:10%">
            <asp:Label ID="lblScripLabel" runat="server" Text="Scrip:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:Label ID="lblScrip" runat="server" Text="" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCustomerLabel" runat="server" Text="Customer:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:Label ID="lblCustomer" runat="server" Text="" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label1" runat="server" Text="Account:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:Label ID="lblAccount" runat="server" Text="" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:GridView ID="gvEquityPortfolio" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" EnableViewState="false" AllowPaging="True" CssClass="GridViewStyle"
                ShowFooter="True" OnPageIndexChanging="gvEquityPortfolio_PageIndexChanging" OnSorting="gvEquityPortfolio_Sorting"
                OnDataBound="gvEquityPortfolio_DataBound">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:BoundField DataField="Date" HeaderText="Date(dd/mm/yyyy)" 
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="Trade Type" HeaderText="Trade Type" 
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="Buy/Sell" HeaderText="Buy/Sell" 
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="Buy Quantity" HeaderText="Buy Qty" 
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="Buy Price" HeaderText="Buy Price(Rs)" 
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="Sell Quantity" HeaderText="Sell Qty" 
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="Sell Price" HeaderText="Sell Price(Rs)" 
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="Cost Of Acquisition" HeaderText="Cost Of Acquisition(Rs)"
                         ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="Realized Sales Value" HeaderText="Realized Sales Value(Rs)"
                         ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="Cost Of Sales" HeaderText="Cost Of Sales(Rs)" 
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="Net Cost" HeaderText="Net Cost(Rs)" 
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="Net Holdings" HeaderText="Net Holdings(Rs)" 
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="Average Price" HeaderText="Avg Price(Rs)" 
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="Profit/Loss" HeaderText="Profit/Loss(Rs)" 
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
</table>
