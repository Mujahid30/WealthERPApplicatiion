<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerMutualFundPortfolioNPView.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.CustomerMutualFundPortfolioNPView" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<style type="text/css">
    .fk-lres-header
    {
        font-size: 13px;
        margin-bottom: 10px;
        padding: 5px 7px;
        border: solid 1px;
    }
</style>
<table style="width: 100%" class="TableBackground">
    <tr>
        <td class="HeaderCell" colspan="7">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Mutual Fund Net Postition"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table style="width: 100%" class="TableBackground">
    <tr>
        <td>
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td colspan="2">
            <div id="div1" class="fk-lres-header" style="margin-left: 10%; padding-bottom: 5px;
                text-align: center">
                <asp:LinkButton ID="lnkReturns" Text="Returns" CssClass="LinkButtons" OnClick="lnkBtnReturns_Click"
                    runat="server"></asp:LinkButton>
                <span>|</span>
                <asp:LinkButton ID="lnkTax" Text="Tax" CssClass="LinkButtons" OnClick="lnkBtnTax_Click"
                    runat="server"></asp:LinkButton>
            </div>
        </td>
        <td>
            <asp:Label ID="lblPickDate" runat="server" Text="As on Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtPickDate" runat="server" CssClass="cmbField" Enabled="false"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="lblPortfolioXIRR" Text="Portfolio XIRR:" runat="server" CssClass="FieldName"> </asp:Label>
        </td>
        <td>
            <asp:Label ID="lblPortfolioXIRRValue" Text="" runat="server" CssClass="FieldName"> </asp:Label>
        </td>
    </tr>
</table>
<table class="TableBackground" width="100%">
    <tr id="trNoRecords" runat="server">
        <td align="center">
            <div id="divNoRecords" runat="server" class="failure-msg">
                <asp:Label ID="lblNoRecords" Text="No Records found" runat="server"></asp:Label>
            </div>
        </td>
    </tr>
    <tr id="trContent" runat="server">
        <td>
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2007" />
            <asp:Panel ID="pnlReturns" runat="server" ScrollBars="Auto" Style="padding: 20px 0 0 10px;">
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
                        <asp:Panel ID="pnlMFPortfolioHoldings" runat="server" class="Landscape" Width="100%"
                            ScrollBars="Horizontal">
                            <table id="tblHoldings" runat="server">
                                <tr>
                                    <td>
                                        <div id="dvHoldings" runat="server">
                                            <asp:Label ID="lblMessageHoldings" Visible="false" Text="No Record Exists" runat="server"
                                                CssClass="Field"></asp:Label>
                                            <asp:LinkButton ID="lnlGoBackHoldings" runat="server" OnClick="lnlGoBackHoldings_Click"
                                                Visible="false" CssClass="FieldName">Go Back</asp:LinkButton>
                                            <telerik:RadGrid ID="rgHoldings" runat="server" Width="860px" Height="250px" PageSize="6"
                                                AllowPaging="true" ShowGroupPanel="true" GridLines="None" AutoGenerateColumns="true"
                                                Style="border: 0; outline: none;" Skin="Telerik" EnableEmbeddedSkins="false"
                                                OnItemCommand="rgHoldings_ItemCommand" EnableViewState="true">
                                                <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                <MasterTableView DataKeyNames="MFNPId,AccountId,AMCCode,SchemeCode" ShowFooter="true"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <telerik:GridButtonColumn ButtonType="LinkButton" Text="Select" CommandName="Select">
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridBoundColumn UniqueName="Category" HeaderText="Category" DataField="Category" FooterText="Total:" FooterStyle-HorizontalAlign="Right">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn UniqueName="Scheme" HeaderText="Scheme" Groupable="False"
                                                            ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkScheme" runat="server" Text='<%# Eval("Scheme").ToString() %>'
                                                                    CommandName="NavigateToMarketData"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn UniqueName="FolioNum" HeaderText="Folio No." DataField="FolioNum">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="PurchasedUnits" HeaderText="Purchased Units"
                                                            DataField="PurchasedUnits">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="DVRUnits" HeaderText="DVR Units" DataField="DVRUnits">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="OpenUnits" HeaderText="Open Units" DataField="OpenUnits">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="InvestedCost" HeaderText="Invested Cost" DataField="InvestedCost" DataFormatString="{0:n2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="NAV" HeaderText="NAV" DataField="NAV">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="MarketValue" HeaderText="Market Value" DataField="MarketValue">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="DVP" HeaderText="DVP" DataField="DVP">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="TotalPL" HeaderText="Total PL" DataField="TotalPL" DataFormatString="{0:n2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="AbsoluteReturn" HeaderText="Absolute Return"
                                                            DataField="AbsoluteReturn">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="XIRR" HeaderText="XIRR" DataField="XIRR">
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
                        <asp:Panel ID="pnlMFPortfolioAll" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
                            <table id="tblAll" runat="server">
                                <tr>
                                    <td>
                                        <div id="dvAll" runat="server">
                                            <asp:Label ID="lblMessageAll" Visible="false" Text="No Record Exists" runat="server"
                                                CssClass="Field"></asp:Label>
                                            <asp:LinkButton ID="lnkGoBackAll" runat="server" OnClick="lnkGoBackAll_Click" Visible="false"
                                                CssClass="FieldName">Go Back</asp:LinkButton>
                                            <telerik:RadGrid ID="rgAll" runat="server" Width="860px" Height="250px" PageSize="6"
                                                AllowPaging="True" ShowGroupPanel="true" GridLines="None" AutoGenerateColumns="true"
                                                Style="border: 0; outline: none;" Skin="Telerik" EnableEmbeddedSkins="false"
                                                OnItemCommand="rgAll_ItemCommand" EnableViewState="true">
                                                <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                <MasterTableView DataKeyNames="MFNPId,AccountId,AMCCode,SchemeCode" ShowFooter="true"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <telerik:GridButtonColumn ButtonType="LinkButton" Text="Select" CommandName="Select">
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridBoundColumn UniqueName="Category" HeaderText="Category" DataField="Category">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn UniqueName="Schemes" HeaderText="Scheme" Groupable="False"
                                                            ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkScheme" runat="server" Text='<%# Eval("Scheme").ToString() %>'
                                                                    CommandName="NavigateToMarketData"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn UniqueName="FolioNum" HeaderText="Folio No." DataField="FolioNum">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="Price" HeaderText="Price" DataField="Price">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="InvestedCost" HeaderText="Invested Cost" DataField="InvestedCost">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="OpenUnits" HeaderText="Open Units" DataField="OpenUnits">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="NAV" HeaderText="NAV" DataField="NAV">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="CurrentValue" HeaderText="Current Value" DataField="CurrentValue">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="UnitsSold" HeaderText="Units Sold" DataField="UnitsSold">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="RedeemedAmount" HeaderText="Redeemed Amount"
                                                            DataField="RedeemedAmount">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="DVP" HeaderText="DVP" DataField="DVP">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="TotalPL" HeaderText="Total PL" DataField="TotalPL">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="AbsoluteReturn" HeaderText="Absolute Return"
                                                            DataField="AbsoluteReturn">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="DVR" HeaderText="DVR" DataField="DVR">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="XIRR" HeaderText="XIRR" DataField="XIRR">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="TotalDividends" HeaderText="Total Dividends"
                                                            DataField="TotalDividends">
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
                        <asp:Panel ID="pnlMFPortfolioRealized" runat="server" class="Landscape" Width="100%"
                            ScrollBars="Horizontal">
                            <table id="tblRealized" runat="server">
                                <tr>
                                    <td>
                                        <div id="dvRealized" runat="server">
                                            <asp:Label ID="lblMessageRealized" Visible="false" Text="No Record Exists" runat="server"
                                                CssClass="Field"></asp:Label>
                                            <asp:LinkButton ID="lnkGoBackRealized" runat="server" OnClick="lnkGoBackRealized_Click"
                                                Visible="false" CssClass="FieldName">Go Back</asp:LinkButton>
                                            <telerik:RadGrid ID="rgRealized" runat="server" Width="860px" Height="250px" PageSize="6"
                                                AllowPaging="True" ShowGroupPanel="true" GridLines="None" AutoGenerateColumns="true"
                                                Style="border: 0; outline: none;" Skin="Telerik" EnableEmbeddedSkins="false"
                                                OnItemCommand="rgRealized_ItemCommand" EnableViewState="true">
                                                <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                <MasterTableView DataKeyNames="MFNPId,AccountId,AMCCode,SchemeCode" ShowFooter="true"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <telerik:GridButtonColumn ButtonType="LinkButton" Text="Select" CommandName="Select">
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridBoundColumn UniqueName="Category" HeaderText="Category" DataField="Category">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn UniqueName="Schemes" HeaderText="Scheme" Groupable="False"
                                                            ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkScheme" runat="server" Text='<%# Eval("Scheme").ToString() %>'
                                                                    CommandName="NavigateToMarketData"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn UniqueName="FolioNum" HeaderText="Folio No." DataField="FolioNum">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="InvestedCost" HeaderText="Invested Cost" DataField="InvestedCost">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="UnitsSold" HeaderText="Units Sold" DataField="UnitsSold">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="RedeemedAmount" HeaderText="Redeemed Amount"
                                                            DataField="RedeemedAmount">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="DVP" HeaderText="DVP" DataField="DVP">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="TotalDividends" HeaderText="Total Dividends"
                                                            DataField="TotalDividends">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="TotalPL" HeaderText="TotalPL" DataField="Total PL">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="AbsoluteReturn" HeaderText="Absolute Return"
                                                            DataField="AbsoluteReturn">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="XIRR" HeaderText="XIRR" DataField="XIRR">
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
                        <asp:Panel ID="pnlMFPandATabPage" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
                            <table id="tblPandA" runat="server">
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
            <asp:Panel ID="pnlTax" runat="server" ScrollBars="Auto" Style="padding: 20px 0 0 10px;">
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
                        <asp:Panel ID="pnlTaxHoldings" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
                            <table id="tblTaxHoldings" runat="server">
                                <tr>
                                    <td>
                                        <div id="dvTaxHoldings" runat="server">
                                            <asp:Label ID="lblMessageTaxHoldings" Visible="false" Text="No Record Exists" runat="server"
                                                CssClass="Field"></asp:Label>
                                            <asp:LinkButton ID="lnlGoBackTaxHoldings" runat="server" OnClick="lnlGoBackTaxHoldings_Click"
                                                Visible="false" CssClass="FieldName">Go Back</asp:LinkButton>
                                            <telerik:RadGrid ID="rgTaxHoldings" runat="server" Width="860px" Height="250px" PageSize="6"
                                                AllowPaging="true" ShowGroupPanel="true" GridLines="None" AutoGenerateColumns="true"
                                                Style="border: 0; outline: none;" Skin="Telerik" EnableEmbeddedSkins="false"
                                                OnItemCommand="rgTaxHoldings_ItemCommand" EnableViewState="true">
                                                <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                <MasterTableView DataKeyNames="MFNPId,AccountId,AMCCode,SchemeCode" ShowFooter="true"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <telerik:GridButtonColumn ButtonType="LinkButton" Text="Select" CommandName="Select">
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridBoundColumn UniqueName="Category" HeaderText="Category" DataField="Category">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn UniqueName="Schemes" HeaderText="Scheme" Groupable="False"
                                                            ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkScheme" runat="server" Text='<%# Eval("Scheme").ToString() %>'
                                                                    CommandName="NavigateToMarketData"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn UniqueName="FolioNum" HeaderText="Folio No." DataField="FolioNum">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="OpenUnits" HeaderText="Open Units" DataField="OpenUnits">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="BalanceAmount" HeaderText="Balance Amount" DataField="BalanceAmount">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="NAV" HeaderText="NAV" DataField="NAV">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="MarketValue" HeaderText="Market Value" DataField="MarketValue">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="UnrealizedPL" HeaderText="PL" DataField="UnrealizedPL">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="EligibleSTCG" HeaderText="Eligible STCG" DataField="EligibleSTCG">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="EligibleLTCG" HeaderText="Eligible LTCG" DataField="EligibleLTCG">
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
                        <asp:Panel ID="pnlTaxRealized" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
                            <table id="tblTaxRealized" runat="server">
                                <tr>
                                    <td>
                                        <div id="dvTaxRealized" runat="server">
                                            <asp:Label ID="lblTaxRealized" Visible="false" Text="No Record Exists" runat="server"
                                                CssClass="Field"></asp:Label>
                                            <asp:LinkButton ID="lnlGoBackTaxRealized" runat="server" OnClick="lnlGoBackTaxRealized_Click"
                                                Visible="false" CssClass="FieldName">Go Back</asp:LinkButton>
                                            <telerik:RadGrid ID="rgTaxRealized" runat="server" Width="860px" Height="250px" PageSize="6"
                                                AllowPaging="true" ShowGroupPanel="true" GridLines="None" AutoGenerateColumns="true"
                                                Style="border: 0; outline: none;" Skin="Telerik" EnableEmbeddedSkins="false"
                                                OnItemCommand="rgTaxRealized_ItemCommand" EnableViewState="true">
                                                <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                <MasterTableView DataKeyNames="MFNPId,AccountId,AMCCode,SchemeCode" ShowFooter="true"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <telerik:GridButtonColumn ButtonType="LinkButton" Text="Select" CommandName="Select">
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridBoundColumn UniqueName="Category" HeaderText="Category" DataField="Category">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn UniqueName="Schemes" HeaderText="Scheme" Groupable="False"
                                                            ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkScheme" runat="server" Text='<%# Eval("Scheme").ToString() %>'
                                                                    CommandName="NavigateToMarketData"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn UniqueName="FolioNum" HeaderText="Folio No." DataField="FolioNum">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="AcquisitionCost" HeaderText="Acquisition Cost"
                                                            DataField="AcquisitionCost">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="RedeemedAmount" HeaderText="Redeemed Amount"
                                                            DataField="RedeemedAmount">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="TotalPL" HeaderText="Total PL" DataField="TotalPL">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="STCG" HeaderText="STCG" DataField="STCG">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="LTCG" HeaderText="LTCG" DataField="LTCG">
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
            <%--<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="lnkReturns">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="pnlReturns" />
                            <telerik:AjaxUpdatedControl ControlID="pnlTax" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                    <telerik:AjaxSetting AjaxControlID="lnkTax">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="pnlReturns" />
                            <telerik:AjaxUpdatedControl ControlID="pnlTax" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                    <telerik:AjaxSetting AjaxControlID="RadTabStrip1">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadTabStrip1" />
                            <telerik:AjaxUpdatedControl ControlID="ReturnsTabs" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                    <telerik:AjaxSetting AjaxControlID="RadTabStrip2">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadTabStrip2" />
                            <telerik:AjaxUpdatedControl ControlID="TaxTabs" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>--%>
        </td>
    </tr>
</table>





