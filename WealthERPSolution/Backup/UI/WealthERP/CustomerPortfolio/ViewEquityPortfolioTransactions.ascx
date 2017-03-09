<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewEquityPortfolioTransactions.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.ViewEquityTransactions" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<div class="divPageHeading">
    <table cellspacing="0" cellpadding="3" width="100%">
        <tr>
            <td align="left">
                <asp:Label ID="lblOrderList" runat="server" CssClass="HeaderTextBig" Text="Equity Transaction Details   "></asp:Label>
            </td>
            <td align="right">
                <asp:LinkButton ID="lnkBack" CssClass="LinkButtons" runat="server" Text="Back" OnClick="lnkBack_Click"></asp:LinkButton>
            </td>
            <td align="right" style="width: 10px">
                <asp:ImageButton ID="btnExportFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                    OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
            </td>
        </tr>
    </table>
</div>
<br />
<asp:Panel runat="server" ScrollBars="Horizontal">
    <table style="width: 100%;">
        <tr>
            <td class="leftField" style="width: 10%">
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
        </tr>
        <tr>
        </tr>
        <tr>
        </tr>
        <tr>
            <td colspan="3">
                <telerik:RadGrid ID="gvEquityPortfolio" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" Width="1050px" AllowFilteringByColumn="true"
                    AllowAutomaticInserts="false" ExportSettings-FileName="Equity Portfolio Details"
                    OnNeedDataSource="gvEquityPortfolio_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                        CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-Width="125px" HeaderText="Date (dd/mm/yyyy)"
                                DataField="Date" UniqueName="Date" SortExpression="Date" AutoPostBackOnFilter="true"
                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Trade Type" DataField="Trade Type" UniqueName="Trade Type"
                                SortExpression="Trade Type" AutoPostBackOnFilter="true" AllowFiltering="false"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Buy/Sell" DataField="Buy/Sell" UniqueName="Buy/Sell"
                                SortExpression="Buy/Sell" AutoPostBackOnFilter="true" AllowFiltering="false"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Buy Qty" DataField="Buy Quantity" UniqueName="Buy Quantity"
                                SortExpression="Buy Quantity" AutoPostBackOnFilter="true" AllowFiltering="false"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Buy Price" DataField="Buy Price" UniqueName="Sell Price"
                                SortExpression="Sell Price" AutoPostBackOnFilter="true" AllowFiltering="false"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Sell Qty" DataField="Sell Quantity" UniqueName="Sell Price"
                                SortExpression="Sell Price" AutoPostBackOnFilter="true" AllowFiltering="false"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Sell Price  " DataField="Sell Price" UniqueName="Sell Price"
                                SortExpression="Sell Price" AutoPostBackOnFilter="true" AllowFiltering="false"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Cost Of Acquisition  " DataField="Cost Of Acquisition"
                                UniqueName="Cost Of Acquisition" SortExpression="Cost Of Acquisition" AutoPostBackOnFilter="true"
                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Realized Sales Value  " DataField="Realized Sales Value"
                                UniqueName="Realized Sales Value" SortExpression="Realized Sales Value" AutoPostBackOnFilter="true"
                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Cost Of Sales  " DataField="Cost Of Sales" UniqueName="Cost Of Sales"
                                SortExpression="Cost Of Sales" AutoPostBackOnFilter="true" AllowFiltering="false"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Net Cost  " DataField="Net Cost" UniqueName="Net Cost"
                                SortExpression="Net Cost" AutoPostBackOnFilter="true" AllowFiltering="false"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="No. Of Shares " DataField="Net Holdings" UniqueName="Net Holdings"
                                SortExpression="Net Holdings" AutoPostBackOnFilter="true" AllowFiltering="false"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Average Price  " DataField="Average Price" UniqueName="Average Price"
                                SortExpression="Average Price" AutoPostBackOnFilter="true" AllowFiltering="false"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Profit/Loss" DataField="Profit/Loss" UniqueName="Profit/Loss"
                                SortExpression="Profit/Loss" AutoPostBackOnFilter="true" AllowFiltering="false"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        <Resizing AllowColumnResize="true" />
                    </ClientSettings>
                </telerik:RadGrid>
                <%-- <asp:GridView ID="gvEquityPortfolio" runat="server" AllowSorting="True" AutoGenerateColumns="False"
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
                        <asp:BoundField DataField="Date" HeaderText="Date(dd/mm/yyyy)" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="Trade Type" HeaderText="Trade Type" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="Buy/Sell" HeaderText="Buy/Sell" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="Buy Quantity" HeaderText="Buy Qty" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="Buy Price" HeaderText="Buy Price " ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="Sell Quantity" HeaderText="Sell Qty" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="Sell Price" HeaderText="Sell Price " ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="Cost Of Acquisition" HeaderText="Cost Of Acquisition "
                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="Realized Sales Value" HeaderText="Realized Sales Value "
                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="Cost Of Sales" HeaderText="Cost Of Sales " ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="Net Cost" HeaderText="Net Cost " ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="Net Holdings" HeaderText="Net Holdings " ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="Average Price" HeaderText="Avg Price " ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="Profit/Loss" HeaderText="Profit/Loss " ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-Wrap="false" />
                    </Columns>
                </asp:GridView>--%>
            </td>
        </tr>
    </table>
</asp:Panel>
