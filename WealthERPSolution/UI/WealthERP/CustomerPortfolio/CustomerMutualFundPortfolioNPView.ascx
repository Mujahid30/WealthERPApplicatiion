<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerMutualFundPortfolioNPView.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.CustomerMutualFundPortfolioNPView" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<table style="width: 100%" class="TableBackground">
    <tr>
        <td class="HeaderCell" colspan="7">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Mutual Fund Net Position"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table style="width: 100%" class="TableBackground">
    <tr>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblDisplayType" runat="server" CssClass="FieldName" Text="Display type:"></asp:Label>
        </td>
        <td class="rightField" style="width: 15%">
            <asp:DropDownList ID="ddlDisplayType" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlDisplayType_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                <asp:ListItem Text="Returns" Value="1"></asp:ListItem>
                <asp:ListItem Text="Tax" Value="2"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
        </td>
        <td class="rightField" style="width: 15%">
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblDate" runat="server" Text="As on Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 15%">
            <asp:Label ID="lblPickDate" Text="" runat="server" CssClass="FieldName"> </asp:Label>
        </td>
        <td>
        </td>
    </tr>
</table>
<table style="width: 100%" class="TableBackground">
    <tr id="trNoRecords" runat="server">
        <td align="center" colspan="7">
            <div id="divNoRecords" runat="server" class="failure-msg">
                <asp:Label ID="lblNoRecords" Text="No Records found" runat="server"></asp:Label>
            </div>
        </td>
    </tr>
    <tr id="trContent" runat="server">
        <td colspan="7">
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2007" />
            <asp:Panel ID="pnlReturns" runat="server" Style="padding: 5px 0 0 2px;">
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" EnableTheming="True" Skin="Telerik"
                    EnableEmbeddedSkins="false" Width="100%" MultiPageID="ReturnsTabs" SelectedIndex="0"
                    EnableViewState="true">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Holdings" Value="Holdings" TabIndex="0">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="All" Value="All" TabIndex="1">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Realized" Value="Realized" TabIndex="2">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Performance and Analysis" Value="Performance and Analysis"
                            TabIndex="3">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="ReturnsTabs" runat="server" EnableViewState="true" SelectedIndex="0">
                    <telerik:RadPageView ID="MFPortfolioHoldingsTabPage" runat="server">
                        <asp:Panel ID="pnlMFPortfolioHoldings" runat="server" class="Landscape" Width="80%" ScrollBars="Horizontal">
                            <table id="tblHoldings" runat="server" width="99%">
                                <tr>
                                    <td>
                                        <div id="dvHoldings" runat="server" style="width: 98%;">
                                            <asp:Label ID="lblMessageHoldings" Visible="false" Text="No Record Exists" runat="server"
                                                CssClass="Field"></asp:Label>
                                            <asp:LinkButton ID="lnlGoBackHoldings" runat="server" OnClick="lnlGoBackHoldings_Click"
                                                Visible="false" CssClass="FieldName">Go Back</asp:LinkButton>
                                            <telerik:RadGrid ID="rgHoldings" runat="server" Width="100%" PageSize="10" AllowAutomaticDeletes="false"
                                                ShowFooter="true" AllowAutomaticInserts="false" AllowAutomaticUpdates="false"
                                                HorizontalAlign="NotSet" AllowPaging="true" ShowGroupPanel="true" GridLines="None"
                                                AutoGenerateColumns="false" Style="border: 0; outline: none;" Skin="Telerik"
                                                EnableEmbeddedSkins="false" ShowStatusBar="true" OnItemCommand="rgHoldings_ItemCommand"
                                                AllowSorting="true" EnableViewState="true" OnNeedDataSource="rgHoldings_OnNeedDataSource"
                                                AllowFilteringByColumn="true">
                                                <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                                    Excel-Format="ExcelML">
                                                </ExportSettings>
                                                <MasterTableView DataKeyNames="MFNPId,AccountId,AMCCode,SchemeCode" ShowFooter="true"
                                                    AutoGenerateColumns="false" CommandItemDisplay="Top">
                                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                                        ShowRefreshButton="false" ShowExportToCsvButton="true" ShowAddNewRecordButton="false" />
                                                    <Columns>
                                                        <telerik:GridButtonColumn ButtonType="LinkButton" Text="Select" CommandName="Select">
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridBoundColumn UniqueName="Category" HeaderText="Category" DataField="Category"
                                                            FooterText="Grand Total:" FooterStyle-HorizontalAlign="Right" AllowFiltering="true">
                                                        </telerik:GridBoundColumn>
                                                        <%--<telerik:GridTemplateColumn AllowFiltering="false" FooterStyle-HorizontalAlign="Right">
                                                        <HeaderTemplate>
                                                            <asp:Label CssClass="label" ID="lblHeaderCategory" runat="server" Text="Category"></asp:Label>
                                                            <asp:DropDownList ID="ddlCategory" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                                                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                                <asp:ListItem Text="All" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="Commodities" Value="MFCO"></asp:ListItem>
                                                                <asp:ListItem Text="Debt" Value="MFDT"></asp:ListItem>
                                                                <asp:ListItem Text="Equity" Value="MFEQ"></asp:ListItem>
                                                                <asp:ListItem Text="Hybrid" Value="MFHY"></asp:ListItem>
                                                                <asp:ListItem Text="Others" Value="MFOT"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label CssClass="label" ID="lblCategory" runat="server" Text='<%# Eval("Category") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                                <asp:Label ID="lblHoldingsFooter" runat="server" Text="Total:" CssClass="label"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top"/>
                                                    </telerik:GridTemplateColumn>--%>
                                                        <telerik:GridTemplateColumn UniqueName="Scheme" HeaderText="Scheme" Groupable="False"
                                                            ItemStyle-Wrap="false" AllowFiltering="true" DataField="Scheme">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkScheme" runat="server" Text='<%# Eval("Scheme").ToString() %>'
                                                                    CommandName="NavigateToMarketData"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn UniqueName="SubCategoryName" HeaderText="SubCategory" DataField="SubCategoryName"
                                                            AllowFiltering="true">
                                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="FolioNum" HeaderText="Folio" DataField="FolioNum">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="PurchasedUnits" HeaderText="Purchased Units"
                                                            DataField="PurchasedUnits" AllowFiltering="false" DataFormatString="{0:N4}" Aggregate="Sum"
                                                            FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="DVRUnits" HeaderText="DVR Units" DataField="DVRUnits"
                                                            AllowFiltering="false" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N4}"
                                                            Aggregate="Sum">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="OpenUnits" HeaderText="Total Units" DataField="OpenUnits"
                                                            AllowFiltering="false" DataFormatString="{0:N4}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="InvestedCost" HeaderText="Invested Cost" DataField="InvestedCost"
                                                            AllowFiltering="false" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="NAV" HeaderText="NAV" DataField="NAV" AllowFiltering="false"
                                                            DataFormatString="{0:N2}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="MarketValue" HeaderText="Market Value" DataField="MarketValue"
                                                            AllowFiltering="false" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="DVP" HeaderText="DVP" DataField="DVP" AllowFiltering="false"
                                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="TotalPL" HeaderText="Total P/L" DataField="TotalPL"
                                                            AllowFiltering="false" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="AbsoluteReturn" HeaderText="Absolute Return(%)"
                                                            DataField="AbsoluteReturn" AllowFiltering="false" DataFormatString="{0:N2}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="XIRR" HeaderText="XIRR(%)" DataField="XIRR"
                                                            AllowFiltering="false" DataFormatString="{0:N2}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="MFPortfolioAllTabPage" runat="server">
                        <table width="99%">
                                <tr align="left">
                                    <td style="width: 15%;">
                                        <asp:Label ID="lblPortfolioXIRR" Text="Portfolio XIRR(%):" runat="server" CssClass="FieldName"> </asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPortfolioXIRRValue" Text="" runat="server" CssClass="FieldName"> </asp:Label>
                                    </td>
                                </tr>
                        </table>
                        <asp:Panel ID="pnlMFPortfolioAll" runat="server" class="Landscape" Width="65%" ScrollBars="Horizontal">
                            <table width="99%">
                                <tr>
                                    <td colspan="2">
                                        <div id="dvAll" runat="server" style="width: 98%">
                                            <asp:Label ID="lblMessageAll" Visible="false" Text="No Record Exists" runat="server"
                                                CssClass="Field"></asp:Label>
                                            <asp:LinkButton ID="lnkGoBackAll" runat="server" OnClick="lnkGoBackAll_Click" Visible="false"
                                                CssClass="FieldName">Go Back</asp:LinkButton>
                                            <telerik:RadGrid ID="rgAll" runat="server" Width="100%" PageSize="10" AllowPaging="True"
                                                ShowGroupPanel="true" GridLines="None" AutoGenerateColumns="true" Style="border: 0;
                                                outline: none;" Skin="Telerik" EnableEmbeddedSkins="false" OnItemCommand="rgAll_ItemCommand"
                                                AllowSorting="true" EnableViewState="true" OnNeedDataSource="rgAll_OnNeedDataSource"
                                                AllowFilteringByColumn="true">
                                                <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                                    Excel-Format="ExcelML">
                                                </ExportSettings>
                                                <MasterTableView DataKeyNames="MFNPId,AccountId,AMCCode,SchemeCode" ShowFooter="true"
                                                    Width="100%" AutoGenerateColumns="false" CommandItemDisplay="Top">
                                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                                        ShowRefreshButton="false" ShowExportToCsvButton="true" ShowAddNewRecordButton="false" />
                                                    <Columns>
                                                        <telerik:GridButtonColumn ButtonType="LinkButton" Text="Select" CommandName="Select">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridBoundColumn UniqueName="Category" HeaderText="Category" DataField="Category"
                                                            AllowFiltering="true" FooterText="Grand Total:" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <%--<telerik:GridTemplateColumn AllowFiltering="false" FooterStyle-HorizontalAlign="Right">
                                                        <HeaderTemplate>
                                                            <asp:Label CssClass="label" ID="lblAllHeader" runat="server" Text="Category"></asp:Label>
                                                            <asp:DropDownList ID="ddlAllCategory" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                                                OnSelectedIndexChanged="ddlAllCategory_SelectedIndexChanged">
                                                                <asp:ListItem Text="All" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="Commodities" Value="MFCO"></asp:ListItem>
                                                                <asp:ListItem Text="Debt" Value="MFDT"></asp:ListItem>
                                                                <asp:ListItem Text="Equity" Value="MFEQ"></asp:ListItem>
                                                                <asp:ListItem Text="Hybrid" Value="MFHY"></asp:ListItem>
                                                                <asp:ListItem Text="Others" Value="MFOT"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label CssClass="label" ID="lblAllCategory" runat="server" Text='<%# Eval("Category") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                                <asp:Label ID="lblAllFooter" runat="server" Text="Total:" CssClass="label"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                    </telerik:GridTemplateColumn>--%>
                                                        <telerik:GridTemplateColumn UniqueName="Schemes" HeaderText="Scheme" Groupable="False"
                                                            ItemStyle-Wrap="false" AllowFiltering="true" DataField="Scheme">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkScheme" runat="server" Text='<%# Eval("Scheme").ToString() %>'
                                                                    CommandName="NavigateToMarketData"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn UniqueName="SubCategoryName" HeaderText="SubCategory" DataField="SubCategoryName"
                                                            AllowFiltering="true">
                                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="FolioNum" HeaderText="Folio" DataField="FolioNum">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="PurchasedUnits" HeaderText="Purchased Units"
                                                            DataField="PurchasedUnits" AllowFiltering="false" DataFormatString="{0:N4}" Aggregate="Sum"
                                                            FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="DVRUnits" HeaderText="DVR Units" DataField="DVRUnits"
                                                            AllowFiltering="false" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N4}"
                                                            Aggregate="Sum">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="OpenUnits" HeaderText="Total Units" DataField="OpenUnits"
                                                            FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N4}" Aggregate="Sum"
                                                            AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <%--<telerik:GridBoundColumn UniqueName="Price" HeaderText="Price" DataField="Price"
                                                        FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" Aggregate="Sum"
                                                        AllowFiltering="false">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </telerik:GridBoundColumn>--%>
                                                        <telerik:GridBoundColumn UniqueName="InvestedCost" HeaderText="Invested Cost" DataField="InvestedCost"
                                                            FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" Aggregate="Sum"
                                                            AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="NAV" HeaderText="NAV" DataField="NAV" FooterStyle-HorizontalAlign="Right"
                                                            AllowFiltering="false" DataFormatString="{0:N2}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="CurrentValue" HeaderText="Current Value" DataField="CurrentValue"
                                                            FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" Aggregate="Sum"
                                                            AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="UnitsSold" HeaderText="Units Sold" DataField="UnitsSold"
                                                            FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N4}" Aggregate="Sum"
                                                            AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="RedeemedAmount" HeaderText="Redeemed Amount"
                                                            DataField="RedeemedAmount" FooterStyle-HorizontalAlign="Right" AllowFiltering="false"
                                                            DataFormatString="{0:N0}" Aggregate="Sum">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="DVP" HeaderText="DVP" DataField="DVP" AllowFiltering="false"
                                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="TotalPL" HeaderText="Total P/L" DataField="TotalPL"
                                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                                            AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="AbsoluteReturn" HeaderText="Absolute Return(%)"
                                                            DataField="AbsoluteReturn" AllowFiltering="false" DataFormatString="{0:N2}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="DVR" HeaderText="DVR" DataField="DVR" DataFormatString="{0:N0}"
                                                            Aggregate="Sum" AllowFiltering="false" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="XIRR" HeaderText="XIRR(%)" DataField="XIRR"
                                                            AllowFiltering="false" DataFormatString="{0:N2}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="TotalDividends" HeaderText="Total Dividends"
                                                            DataField="TotalDividends" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}"
                                                            Aggregate="Sum" AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="MFPortfolioRealizedTabPage" runat="server">
                        <asp:Panel ID="pnlMFPortfolioRealized" runat="server" class="Landscape" Width="90%"
                            ScrollBars="Horizontal">
                            <table id="tblRealized" runat="server" width="99%">
                                <tr>
                                    <td>
                                        <div id="dvRealized" runat="server" style="width: 98%">
                                            <asp:Label ID="lblMessageRealized" Visible="false" Text="No Record Exists" runat="server"
                                                CssClass="Field"></asp:Label>
                                            <asp:LinkButton ID="lnkGoBackRealized" runat="server" OnClick="lnkGoBackRealized_Click"
                                                Visible="false" CssClass="FieldName">Go Back</asp:LinkButton>
                                            <telerik:RadGrid ID="rgRealized" runat="server" Width="100%" PageSize="10" AllowPaging="True"
                                                AllowSorting="true" ShowGroupPanel="true" GridLines="None" AutoGenerateColumns="true"
                                                Style="border: 0; outline: none;" Skin="Telerik" EnableEmbeddedSkins="false"
                                                OnItemCommand="rgRealized_ItemCommand" EnableViewState="true" OnNeedDataSource="rgRealized_OnNeedDataSource"
                                                AllowFilteringByColumn="true">
                                                <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                                    Excel-Format="ExcelML">
                                                </ExportSettings>
                                                <MasterTableView DataKeyNames="MFNPId,AccountId,AMCCode,SchemeCode" ShowFooter="true"
                                                    Width="100%" AutoGenerateColumns="false" CommandItemDisplay="Top">
                                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                                        ShowRefreshButton="false" ShowExportToCsvButton="true" ShowAddNewRecordButton="false" />
                                                    <Columns>
                                                        <telerik:GridButtonColumn ButtonType="LinkButton" Text="Select" CommandName="Select">
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridBoundColumn UniqueName="Category" HeaderText="Category" DataField="Category"
                                                            AllowFiltering="true" FooterText="Grand Total:" FooterStyle-HorizontalAlign="Right">
                                                        </telerik:GridBoundColumn>
                                                        <%--<telerik:GridTemplateColumn AllowFiltering="false" FooterStyle-HorizontalAlign="Right">
                                                        <HeaderTemplate>
                                                            <asp:Label CssClass="label" ID="lblRealizedHeader" runat="server" Text="Category"></asp:Label>
                                                            <asp:DropDownList ID="ddlRealizedCategory" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                                                OnSelectedIndexChanged="ddlRealizedCategory_SelectedIndexChanged">
                                                                <asp:ListItem Text="All" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="Commodities" Value="MFCO"></asp:ListItem>
                                                                <asp:ListItem Text="Debt" Value="MFDT"></asp:ListItem>
                                                                <asp:ListItem Text="Equity" Value="MFEQ"></asp:ListItem>
                                                                <asp:ListItem Text="Hybrid" Value="MFHY"></asp:ListItem>
                                                                <asp:ListItem Text="Others" Value="MFOT"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label CssClass="label" ID="lblRealizedCategory" runat="server" Text='<%# Eval("Category") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                                <asp:Label ID="lblRealizedFooter" runat="server" Text="Total:" CssClass="label"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                    </telerik:GridTemplateColumn>--%>
                                                        <telerik:GridTemplateColumn UniqueName="Schemes" HeaderText="Scheme" Groupable="False"
                                                            ItemStyle-Wrap="false" AllowFiltering="true" DataField="Scheme">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkScheme" runat="server" Text='<%# Eval("Scheme").ToString() %>'
                                                                    CommandName="NavigateToMarketData"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn UniqueName="SubCategoryName" HeaderText="SubCategory" DataField="SubCategoryName"
                                                            AllowFiltering="true">
                                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="FolioNum" HeaderText="Folio" DataField="FolioNum">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="InvestedCost" HeaderText="Invested Cost" DataField="InvestedCost"
                                                            AllowFiltering="false" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="UnitsSold" HeaderText="Units Sold" DataField="UnitsSold"
                                                            AllowFiltering="false" DataFormatString="{0:N4}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="RedeemedAmount" HeaderText="Redeemed Amount"
                                                            DataField="RedeemedAmount" AllowFiltering="false" DataFormatString="{0:N0}" Aggregate="Sum"
                                                            FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="DVP" HeaderText="DVP" DataField="DVP" AllowFiltering="false"
                                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="TotalDividends" HeaderText="Total Dividends"
                                                            DataField="TotalDividends" AllowFiltering="false" DataFormatString="{0:N0}" Aggregate="Sum"
                                                            FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="TotalPL" HeaderText="Total P/L" DataField="TotalPL"
                                                            AllowFiltering="false" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="AbsoluteReturn" HeaderText="Absolute Return(%)"
                                                            DataField="AbsoluteReturn" AllowFiltering="false" DataFormatString="{0:N2}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="XIRR" HeaderText="XIRR(%)" DataField="XIRR"
                                                            AllowFiltering="false" DataFormatString="{0:N2}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="MFPandATabPage" runat="server">
                        <asp:Panel ID="pnlMFPandATabPage" runat="server" class="Landscape" Width="80%">
                            <table id="tblPandA" runat="server" width="99%">
                                <tr id="trMFCode" runat="server">
                                    <td>
                                        <asp:DropDownList ID="ddlMFClassificationCode" runat="server" CssClass="cmbField"
                                            Height="16px" Width="176px">
                                            <asp:ListItem>MF Classification Code</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="trChart" runat="server">
                                    <td>
                                        <div id="Div2" runat="server">
                                            <asp:Chart ID="chrtMFClassification" runat="server" BackColor="Transparent" Palette="Pastel"
                                                Width="500px" Height="250px">
                                                <Series>
                                                    <asp:Series Name="seriesMFC">
                                                    </asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="careaMFC">
                                                    </asp:ChartArea>
                                                </ChartAreas>
                                            </asp:Chart>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="7">
            <asp:Panel ID="pnlTax" runat="server" Style="padding: 5px 0 0 2px;">
                <telerik:RadTabStrip ID="RadTabStrip2" runat="server" EnableTheming="True" Skin="Telerik"
                    EnableEmbeddedSkins="False" Width="100%" MultiPageID="TaxTabs" SelectedIndex="0"
                    EnableViewState="true">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Holdings" Value="Holdings" TabIndex="0">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Realized" Value="Realized" TabIndex="1">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="TaxTabs" runat="server" EnableViewState="true" SelectedIndex="0">
                    <telerik:RadPageView ID="TaxHoldingsTabPage" runat="server">
                        <asp:Panel ID="pnlTaxHoldings" runat="server" class="Landscape" Width="90%" ScrollBars="Horizontal">
                            <table id="tblTaxHoldings" runat="server" width="99%">
                                <tr>
                                    <td>
                                        <div id="dvTaxHoldings" runat="server" style="width: 98%">
                                            <asp:Label ID="lblMessageTaxHoldings" Visible="false" Text="No Record Exists" runat="server"
                                                CssClass="Field"></asp:Label>
                                            <asp:LinkButton ID="lnlGoBackTaxHoldings" runat="server" OnClick="lnlGoBackTaxHoldings_Click"
                                                Visible="false" CssClass="FieldName">Go Back</asp:LinkButton>
                                            <telerik:RadGrid ID="rgTaxHoldings" runat="server" Width="100%" PageSize="10" AllowPaging="true"
                                                AllowSorting="true" ShowGroupPanel="true" GridLines="None" AutoGenerateColumns="true"
                                                Style="border: 0; outline: none;" Skin="Telerik" EnableEmbeddedSkins="false"
                                                OnItemCommand="rgTaxHoldings_ItemCommand" EnableViewState="true" OnNeedDataSource="rgTaxHoldings_OnNeedDataSource"
                                                AllowFilteringByColumn="true">
                                                <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                                    Excel-Format="ExcelML">
                                                </ExportSettings>
                                                <MasterTableView DataKeyNames="MFNPId,AccountId,AMCCode,SchemeCode" ShowFooter="true"
                                                    AutoGenerateColumns="false" CommandItemDisplay="Top" Width="100%">
                                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                                        ShowRefreshButton="false" ShowExportToCsvButton="true" ShowAddNewRecordButton="false" />
                                                    <Columns>
                                                        <telerik:GridButtonColumn ButtonType="LinkButton" Text="Select" CommandName="Select">
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridBoundColumn UniqueName="Category" HeaderText="Category" DataField="Category"
                                                            AllowFiltering="true" FooterText="Grand Total:" FooterStyle-HorizontalAlign="Right">
                                                        </telerik:GridBoundColumn>
                                                        <%--<telerik:GridTemplateColumn AllowFiltering="false" FooterStyle-HorizontalAlign="Right">
                                                            <HeaderTemplate>
                                                                <asp:Label CssClass="label" ID="lblTaxHoldingsHeader" runat="server" Text="Category"></asp:Label>
                                                                <asp:DropDownList ID="ddlTaxHoldingsCategory" AutoPostBack="true" runat="server"
                                                                    CssClass="GridViewCmbField" OnSelectedIndexChanged="ddlTaxHoldingsCategory_SelectedIndexChanged">
                                                                    <asp:ListItem Text="All" Value=""></asp:ListItem>
                                                                    <asp:ListItem Text="Commodities" Value="MFCO"></asp:ListItem>
                                                                    <asp:ListItem Text="Debt" Value="MFDT"></asp:ListItem>
                                                                    <asp:ListItem Text="Equity" Value="MFEQ"></asp:ListItem>
                                                                    <asp:ListItem Text="Hybrid" Value="MFHY"></asp:ListItem>
                                                                    <asp:ListItem Text="Others" Value="MFOT"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label CssClass="label" ID="lblTaxHoldingsCategory" runat="server" Text='<%# Eval("Category") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTaxHoldingsFooter" runat="server" Text="Total:" CssClass="label"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridTemplateColumn>--%>
                                                        <telerik:GridTemplateColumn UniqueName="Schemes" HeaderText="Scheme" Groupable="False"
                                                            ItemStyle-Wrap="false" AllowFiltering="true" DataField="Scheme">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkScheme" runat="server" Text='<%# Eval("Scheme").ToString() %>'
                                                                    CommandName="NavigateToMarketData"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn UniqueName="SubCategoryName" HeaderText="SubCategory" DataField="SubCategoryName"
                                                            AllowFiltering="true">
                                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="FolioNum" HeaderText="Folio" DataField="FolioNum">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="OpenUnits" HeaderText="Total Units" DataField="OpenUnits"
                                                            AllowFiltering="false" DataFormatString="{0:N4}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="BalanceAmount" HeaderText="Acquisition Cost"
                                                            DataField="BalanceAmount" AllowFiltering="false" DataFormatString="{0:N0}" Aggregate="Sum"
                                                            FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="NAV" HeaderText="NAV" DataField="NAV" AllowFiltering="false"
                                                            DataFormatString="{0:N2}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="MarketValue" HeaderText="Market Value" DataField="MarketValue"
                                                            AllowFiltering="false" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="UnrealizedPL" HeaderText="P/L" DataField="UnrealizedPL"
                                                            AllowFiltering="false" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="EligibleSTCG" HeaderText="Eligible STCG" DataField="EligibleSTCG"
                                                            AllowFiltering="false" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="EligibleLTCG" HeaderText="Eligible LTCG" DataField="EligibleLTCG"
                                                            AllowFiltering="false" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                    </Columns>                                                    
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="TaxRealizedTabPage" runat="server">
                        <asp:Panel ID="pnlTaxRealized" runat="server" class="Landscape" Width="95%" ScrollBars="Horizontal">
                            <table id="tblTaxRealized" runat="server" width="99%">
                                <tr>
                                    <td>
                                        <div id="dvTaxRealized" runat="server" style="width: 98%">
                                            <asp:Label ID="lblTaxRealized" Visible="false" Text="No Record Exists" runat="server"
                                                CssClass="Field"></asp:Label>
                                            <asp:LinkButton ID="lnlGoBackTaxRealized" runat="server" OnClick="lnlGoBackTaxRealized_Click"
                                                Visible="false" CssClass="FieldName">Go Back</asp:LinkButton>
                                            <telerik:RadGrid ID="rgTaxRealized" runat="server" Width="100%" PageSize="10" AllowPaging="true"
                                                AllowSorting="true" ShowGroupPanel="true" GridLines="None" AutoGenerateColumns="true"
                                                Style="border: 0; outline: none;" Skin="Telerik" EnableEmbeddedSkins="false"
                                                OnItemCommand="rgTaxRealized_ItemCommand" EnableViewState="true" OnNeedDataSource="rgTaxRealized_OnNeedDataSource"
                                                AllowFilteringByColumn="true">
                                                <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                                    Excel-Format="ExcelML">
                                                </ExportSettings>
                                                <MasterTableView DataKeyNames="MFNPId,AccountId,AMCCode,SchemeCode" ShowFooter="true"
                                                    AutoGenerateColumns="false" CommandItemDisplay="Top">
                                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                                        ShowRefreshButton="false" ShowExportToCsvButton="true" ShowAddNewRecordButton="false" />
                                                    <Columns>
                                                        <telerik:GridButtonColumn ButtonType="LinkButton" Text="Select" CommandName="Select">
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridBoundColumn UniqueName="Category" HeaderText="Category" DataField="Category"
                                                            AllowFiltering="true" FooterText="Grand Total:" FooterStyle-HorizontalAlign="Right">
                                                        </telerik:GridBoundColumn>
                                                        <%--<telerik:GridTemplateColumn AllowFiltering="false" FooterStyle-HorizontalAlign="Right">
                                                            <HeaderTemplate>
                                                                <asp:Label CssClass="label" ID="lblTaxRealizedHeader" runat="server" Text="Category"></asp:Label>
                                                                <asp:DropDownList ID="ddlTaxRealizedCategory" AutoPostBack="true" runat="server"
                                                                    CssClass="GridViewCmbField" OnSelectedIndexChanged="ddlTaxRealizedCategory_SelectedIndexChanged">
                                                                    <asp:ListItem Text="All" Value=""></asp:ListItem>
                                                                    <asp:ListItem Text="Commodities" Value="MFCO"></asp:ListItem>
                                                                    <asp:ListItem Text="Debt" Value="MFDT"></asp:ListItem>
                                                                    <asp:ListItem Text="Equity" Value="MFEQ"></asp:ListItem>
                                                                    <asp:ListItem Text="Hybrid" Value="MFHY"></asp:ListItem>
                                                                    <asp:ListItem Text="Others" Value="MFOT"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label CssClass="label" ID="lblTaxRealizedCategory" runat="server" Text='<%# Eval("Category") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>                                                            
                                                                <asp:Label ID="lblTaxRealizedFooter" runat="server" Text="Total:" CssClass="label"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridTemplateColumn>--%>
                                                        <telerik:GridTemplateColumn UniqueName="Schemes" HeaderText="Scheme" Groupable="False"
                                                            SortExpression="Scheme" ItemStyle-Wrap="false" AllowFiltering="true" DataField="Scheme">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkScheme" runat="server" Text='<%# Eval("Scheme").ToString() %>'
                                                                    CommandName="NavigateToMarketData"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn UniqueName="SubCategoryName" HeaderText="SubCategory" DataField="SubCategoryName"
                                                            AllowFiltering="true">
                                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="FolioNum" HeaderText="Folio" DataField="FolioNum"
                                                            AllowFiltering="true">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="AcquisitionCost" HeaderText="Acquisition Cost"
                                                            DataField="AcquisitionCost" AllowFiltering="false" FooterStyle-HorizontalAlign="Right"
                                                            DataFormatString="{0:N0}" Aggregate="Sum">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="UnitsSold" HeaderText="Units Sold" DataField="UnitsSold"
                                                            FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" Aggregate="Sum"
                                                            AllowFiltering="false">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="RedeemedAmount" HeaderText="Redeemed Amount"
                                                            DataField="RedeemedAmount" AllowFiltering="false" FooterStyle-HorizontalAlign="Right"
                                                            DataFormatString="{0:N0}" Aggregate="Sum">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="TotalPL" HeaderText="Total P/L" DataField="TotalPL"
                                                            AllowFiltering="false" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}"
                                                            Aggregate="Sum">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="STCG" HeaderText="STCG" DataField="STCG" AllowFiltering="false"
                                                            FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" Aggregate="Sum">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="LTCG" HeaderText="LTCG" DataField="LTCG" AllowFiltering="false"
                                                            FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" Aggregate="Sum">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </asp:Panel>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnReturnsRealizedCategory" runat="server" />
<asp:HiddenField ID="hdnReturnsHoldingsCategory" runat="server" />
<asp:HiddenField ID="hdnReturnsAllCategory" runat="server" />
<asp:HiddenField ID="hdnTaxHoldingsCategory" runat="server" />
<asp:HiddenField ID="hdnTaxRealizedCategory" runat="server" />
