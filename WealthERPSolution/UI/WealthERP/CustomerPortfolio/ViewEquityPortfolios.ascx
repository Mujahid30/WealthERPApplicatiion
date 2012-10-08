<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewEquityPortfolios.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.ViewEquityPortfolios" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script type="text/javascript" src="../Scripts/tabber.js"></script>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<script language="javascript" type="text/javascript">
    function Print_Click(div, btnID) {
        var ContentToPrint = document.getElementById(div);
        var myWindowToPrint = window.open('', '', 'width=200,height=100,toolbar=0,scrollbars=0,status=0,resizable=0,location=0,directories=0');
        myWindowToPrint.document.write(document.getElementById(div).innerHTML);
        myWindowToPrint.document.close();
        myWindowToPrint.focus();
        myWindowToPrint.print();
        myWindowToPrint.close();

        var btn = document.getElementById(btnID);
        btn.click();
    }
    function AferExportAll(btnID) {
        var btn = document.getElementById(btnID);
        btn.click();
    }
    function GetSelectedTab(selectedTab) {
        alert(selectedTab);
    }
</script>

<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Equity Net Position
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
        </td>
        <td colspan="4" class="rightField">
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td></td>
        <td>
            <asp:Label ID="lblDate" runat="server" CssClass="FieldName">As on Date:</asp:Label>
        </td>
        <td>
          <asp:Label ID="lblPickDate" runat="server"  CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:Button ID="btnGo" runat="server" Text="GO" CssClass="PCGButton" OnClick="btnGo_Click"
                ValidationGroup="vgBtnGo" />
        </td>
    </tr>
</table>
<br />
<br />
<table  width="100%">
    <tr align="center">
        <td>
            <div align="center">
                <asp:Label ID="lblErrorMsg" runat="server" CssClass="failure-msg" Visible="false">
                </asp:Label>
            </div>
        </td>
    </tr>
</table>
<telerik:RadTabStrip ID="RadTabStrip1" runat="server" EnableTheming="True" Skin="Telerik"
    EnableEmbeddedSkins="False" Width="100%" MultiPageID="EQPortfolioTabPages" SelectedIndex="0"
    EnableViewState="true">
    <Tabs>
        <telerik:RadTab runat="server" Text="UnRealized" Value="UnRealized" TabIndex="3">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Realized Delivery" Value="Realized Delivery"
            TabIndex="1">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Realized Speculative" Value="Realized Speculative"
            TabIndex="2">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="All" Value="All" TabIndex="0">
        </telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="EQPortfolioTabPages" runat="server" EnableViewState="true"
    SelectedIndex="0">
    <telerik:RadPageView ID="EQPortfolioUnRealizedTabPage" runat="server">
        <asp:Panel ID="pnlEQPortfolioUnRealized" runat="server">
            <table>
                <tr>
                    <td>
                        <div id="dvEquityPortfolioUnrealized" runat="server">
                            <asp:Label ID="lblMessageUnrealized" Visible="false" Text="No Record Exists" runat="server"
                                CssClass="Field"></asp:Label>
                            <asp:ImageButton ID="imgBtnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btngvEquityPortfolioUnrealizedExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="25px" Width="25px" Visible="false">
                            </asp:ImageButton>
                            <telerik:RadGrid ID="gvEquityPortfolioUnrealized" runat="server" GridLines="None"
                                AutoGenerateColumns="False" PageSize="10" AllowSorting="true" AllowPaging="True"
                                ShowStatusBar="True" ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false"
                                Width="1050px" AllowFilteringByColumn="true" AllowAutomaticInserts="false" ExportSettings-FileName="Equity Net Position Details"
                                OnNeedDataSource="gvEquityPortfolioUnrealized_OnNeedDataSource" OnItemCommand="gvEquityPortfolioUnrealized_ItemCommand">
                                <ExportSettings HideStructureColumns="true">
                                </ExportSettings>
                                <MasterTableView DataKeyNames="Sl.No." Width="100%" AllowMultiColumnSorting="True"
                                    AutoGenerateColumns="false" CommandItemDisplay="None">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderStyle-Width="50px" AllowFiltering="false" UniqueName="action"
                                            DataField="action">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" CommandName="Select" HeaderText="Select" ShowHeader="True"
                                                    Text="Select" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="250px" FooterText="Grand Total" HeaderText="Scrip Name"
                                            DataField="CompanyName" UniqueName="CompanyName" SortExpression="CompanyName"
                                            AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="No. of shares"
                                            DataField="Quantity" UniqueName="Quantity" SortExpression="Quantity" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Average Price"
                                            DataField="AveragePrice" UniqueName="AveragePrice" SortExpression="AveragePrice"
                                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Cost Of Purchase  "
                                            DataField="CostOfPurchase" UniqueName="CostOfPurchase" SortExpression="CostOfPurchase"
                                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Current Price  "
                                            DataField="MarketPrice" UniqueName="MarketPrice" SortExpression="MarketPrice"
                                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Current Value  "
                                            DataField="CurrentValue" UniqueName="CurrentValue" SortExpression="CurrentValue"
                                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="UnRealized P/L  "
                                            DataField="UnRealizedPL" UniqueName="UnRealizedPL" SortExpression="UnRealizedPL"
                                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="50px"
                                            HeaderText="XIRR (%)" DataField="XIRR" UniqueName="XIRR" SortExpression="XIRR"
                                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                    <Resizing AllowColumnResize="true" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="EQPortfolioRealizedDelTabPage" runat="server">
        <asp:Panel ID="pnlEQPortfolioRealizedDel" runat="server">
            <table>
                <tr>
                    <td>
                        <div id="dvEquityPortfolioDelivery" runat="server">
                            <asp:Label ID="lblMessageD" Visible="false" Text="No Record Exists" runat="server"
                                CssClass="Field"></asp:Label>
                            <asp:ImageButton ID="imgBtnExport1" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btngvEquityPortfolioDeliveryExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="25px" Width="25px" Visible="false">
                            </asp:ImageButton>
                            <telerik:RadGrid ID="gvEquityPortfolioDelivery" runat="server" GridLines="None" AutoGenerateColumns="False"
                                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                Skin="Telerik" EnableEmbeddedSkins="false" Width="1050px" AllowFilteringByColumn="true"
                                AllowAutomaticInserts="false" ExportSettings-FileName="Equity Net Position Details"
                                OnNeedDataSource="gvEquityPortfolioDelivery_OnNeedDataSource" OnItemCommand="gvEquityPortfolioDelivery_ItemCommand">
                                <ExportSettings HideStructureColumns="true">
                                </ExportSettings>
                                <MasterTableView DataKeyNames="Sl.No." Width="100%" AllowMultiColumnSorting="True"
                                    AutoGenerateColumns="false" CommandItemDisplay="None">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderStyle-Width="50px" AllowFiltering="false" UniqueName="action"
                                            DataField="action">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" CommandName="Select" HeaderText="Select" ShowHeader="True"
                                                    Text="Select" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn Visible="false" HeaderText="Sl.No." DataField="Sl.No." UniqueName="Sl.No."
                                            SortExpression="Sl.No." AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false"
                                            CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="250px" FooterText="Grand Total" HeaderText="Scrip Name"
                                            DataField="CompanyName" UniqueName="CompanyName" SortExpression="CompanyName"
                                            AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="No. of shares sold"
                                            DataField="SaleQty" UniqueName="SaleQty" SortExpression="SaleQty" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Sales Proceeds  "
                                            DataField="RealizedSalesProceeds" UniqueName="RealizedSalesProceeds" SortExpression="RealizedSalesProceeds"
                                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Cost Of Sales  "
                                            DataField="CostOfSales" UniqueName="CostOfSales" SortExpression="CostOfSales"
                                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Realized P/L "
                                            DataField="RealizedPL" UniqueName="RealizedPL" SortExpression="RealizedPL" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="50px"
                                            HeaderText="XIRR (%)" DataField="XIRR" UniqueName="XIRR" SortExpression="XIRR"
                                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                    <Resizing AllowColumnResize="true" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="EQPortfolioRealizedSpecTabPage" runat="server">
        <asp:Panel ID="pnlEQPortfolioRealizedSpec" runat="server">
            <table>
                <tr>
                    <td>
                        <div id="dvEquityPortfolioSpeculative" runat="server">
                            <asp:Label ID="lblMessageSpeculative" Visible="false" Text="No Record Exists" runat="server"
                                CssClass="Field"></asp:Label>
                            <asp:ImageButton ID="imgBtnExport2" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btngvEquityPortfolioSpeculativeExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="25px" Width="25px" Visible="false">
                            </asp:ImageButton>
                            <telerik:RadGrid ID="gvEquityPortfolioSpeculative" runat="server" GridLines="None"
                                AutoGenerateColumns="False" PageSize="10" AllowSorting="true" AllowPaging="True"
                                ShowStatusBar="True" ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false"
                                Width="1050px" AllowFilteringByColumn="true" AllowAutomaticInserts="false" ExportSettings-FileName="Equity Net Position Details"
                                OnNeedDataSource="gvEquityPortfolioSpeculative_OnNeedDataSource" OnItemCommand="gvEquityPortfolioSpeculative_ItemCommand">
                                <ExportSettings HideStructureColumns="true">
                                </ExportSettings>
                                <MasterTableView DataKeyNames="Sl.No." Width="100%" AllowMultiColumnSorting="True"
                                    AutoGenerateColumns="false" CommandItemDisplay="None">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderStyle-Width="50px" AllowFiltering="false" UniqueName="action"
                                            DataField="action">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" CommandName="Select" HeaderText="Select" ShowHeader="True"
                                                    Text="Select" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn Visible="false" HeaderText="Sl.No." DataField="Sl.No." UniqueName="Sl.No."
                                            SortExpression="Sl.No." AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false"
                                            CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="250px" FooterText="Grand Total" HeaderText="Scrip Name"
                                            DataField="CompanyName" UniqueName="CompanyName" SortExpression="CompanyName"
                                            AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="No of shares"
                                            DataField="SaleQty" UniqueName="SaleQty" SortExpression="SaleQty" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Cost Of Sales  "
                                            DataField="CostOfSales" UniqueName="CostOfSales" SortExpression="CostOfSales"
                                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Realized P/L "
                                            DataField="RealizedPL" UniqueName="RealizedPL" SortExpression="RealizedPL" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="50px"
                                            HeaderText="XIRR (%)" DataField="XIRR" UniqueName="XIRR" SortExpression="XIRR"
                                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                    <Resizing AllowColumnResize="true" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="EQPortfolioAllTabPage" runat="server">
        <asp:Panel ID="pnlEQPortfolioAll" runat="server">
            <table>
                <tr>
                    <td>
                        <div id="dvEquityPortfolio" runat="server">
                            <asp:Label ID="lblMessage" Visible="false" Text="No Record Exists" runat="server"
                                CssClass="Field"></asp:Label>
                            <asp:ImageButton ID="imgBtnExport3" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btngvEquityPortfolioExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="25px" Width="25px" Visible="false">
                            </asp:ImageButton>
                            <telerik:RadGrid ID="gvEquityPortfolio" runat="server" GridLines="None" AutoGenerateColumns="False"
                                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                Skin="Telerik" EnableEmbeddedSkins="false" Width="1050px" AllowFilteringByColumn="true"
                                AllowAutomaticInserts="false" ExportSettings-FileName="Equity Net Position Details"
                                OnNeedDataSource="gvEquityPortfolio_OnNeedDataSource" OnItemCommand="gvEquityPortfolio_ItemCommand">
                                <ExportSettings HideStructureColumns="true">
                                </ExportSettings>
                                <MasterTableView DataKeyNames="Sl.No." Width="100%" AllowMultiColumnSorting="True"
                                    AutoGenerateColumns="false" CommandItemDisplay="None">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderStyle-Width="50px" AllowFiltering="false" UniqueName="action"
                                            DataField="action">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" CommandName="Select" HeaderText="Select" ShowHeader="True"
                                                    Text="Select" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn Visible="false" HeaderText="Sl.No." DataField="Sl.No." UniqueName="Sl.No."
                                            SortExpression="Sl.No." AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false"
                                            CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="250px" FooterText="Grand Total" HeaderText="Scrip Code"
                                            DataField="CompanyName" UniqueName="CompanyName" SortExpression="CompanyName"
                                            AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="No of shares"
                                            DataField="Quantity" UniqueName="Quantity" SortExpression="Quantity" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:n2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Average Price  "
                                            DataField="AveragePrice" UniqueName="AveragePrice" SortExpression="AveragePrice"
                                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Cost Of Purchase  "
                                            DataField="CostOfPurchase" UniqueName="CostOfPurchase" SortExpression="CostOfPurchase"
                                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Current Price  "
                                            DataField="MarketPrice" UniqueName="MarketPrice" SortExpression="MarketPrice"
                                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Current Value  "
                                            DataField="CurrentValue" UniqueName="CurrentValue" SortExpression="CurrentValue"
                                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="UnRealized P/L "
                                            DataField="UnRealizedPL" UniqueName="UnRealizedPL" SortExpression="UnRealizedPL"
                                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Realized P/L "
                                            DataField="RealizedPL" UniqueName="RealizedPL" SortExpression="RealizedPL" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="50px"
                                            HeaderText="XIRR (%)" DataField="XIRR" UniqueName="XIRR" SortExpression="XIRR"
                                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            Aggregate="Sum" DataFormatString="{0:N2}">
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                    <Resizing AllowColumnResize="true" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
</telerik:RadMultiPage>
<table style="width: 30%;">
    <tr>
        <td colspan="3">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextSmall" Text="Equity Portfolio Summary"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Current Value  "></asp:Label>
        </td>
        <td colspan="2">
            <asp:Label ID="lblCurrentValue" runat="server" CssClass="Field" Text="lblCurrentValue"></asp:Label>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnScipNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRealizedScipFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRealizedSpecScipFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnUnRealizedScipFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSelectedTab" runat="server" />
<asp:HiddenField ID="hdnNoOfRecords" runat="server" />
