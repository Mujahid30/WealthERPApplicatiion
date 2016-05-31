<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerMutualFundPortfolioNPView.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.CustomerMutualFundPortfolioNPView" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<script type="text/javascript" language="javascript">
    
</script>

<%--<table style="width: 100%" class="TableBackground">
    <tr>
        <td class="HeaderCell" colspan="7">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Mutual Fund Net Position"></asp:Label>
            <hr />
        </td>
    </tr>
</table>--%>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td style="width: 100%;">
                    <div class="divPageHeading">
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td align="left" style="width: 33%">
                                    Mutual Fund Net Position
                                </td>
                                <td style="width: 34%">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                        <ProgressTemplate>
                                            <asp:Image ID="imgProgress" ImageUrl="~/Images/ajax-loader.gif" AlternateText="Processing"
                                                runat="server" />
                                            <%--<img alt="Processing" src="~/Images/ajax_loader.gif" style="width: 200px; height: 100px" />--%>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                                <td align="right" style="width: 33%">
                                    <%--<asp:ImageButton Visible="true" ID="ImageButton1" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                     runat="server" AlternateText="Excel" ToolTip="Export To Excel" 
                                                        OnClick="GetParentCustomerId();" Height="25px" Width="25px">
                                </asp:ImageButton>--%>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <table style="width: 100%" class="TableBackground">
            <tr>
                <td class="leftField" style="width: 10%">
                    <asp:Label ID="lblDisplayType" runat="server" CssClass="FieldName" Text="Display type:"></asp:Label>
                </td>
                <td class="rightField" style="width: 15%">
                    <asp:DropDownList ID="ddlDisplayType" runat="server" CssClass="cmbField">
                        <%-- <asp:ListItem Text="Select" Value="0"></asp:ListItem>--%>
                        <asp:ListItem Text="Returns" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Tax" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="leftField" style="width: 10%">
                    <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
                </td>
                <td class="rightField" style="width: 15%">
                    <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
             <%-- <td class="leftField" style="width: 10%">
                    <asp:Label ID="lblDate" runat="server" Text="As on Date:" CssClass="FieldName"></asp:Label>
                </td>--%>
                <td class="rightField" style="width: 10%">
                    <asp:Label ID="lblPickDate" Text="" runat="server" CssClass="FieldName" Visible="false"> </asp:Label>
                </td>
                <td class="rightField" style="width: 15%">
                    <asp:Button ID="btnGo" runat="server" Text="GO" CssClass="PCGButton" OnClick="btnGo_Click" />
                </td>
                <td>
                </td>
            </tr>
        </table>
        <table style="width: 100%" class="TableBackground">
            <tr id="trNoRecords" runat="server">
                <td align="center">
                    <div id="divNoRecords" runat="server" class="failure-msg">
                        <asp:Label ID="lblNoRecords" Text="No Records found" runat="server"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr id="trContent" runat="server">
                <td style="width: 100%">
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
                                <telerik:RadTab runat="server" Text="Analysis Chart" Value="Performance and Analysis"
                                    TabIndex="3" Visible="false">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="ReturnsTabs" runat="server" EnableViewState="true" SelectedIndex="0">
                            <telerik:RadPageView ID="MFPortfolioHoldingsTabPage" runat="server">
                                <table width="100%">
                                    <tr align="left">
                                        <td style="width: 100%;">
                                            <asp:Label ID="lblHoldingTotalPL" Text="Total P/L: " runat="server" CssClass="FieldName"> </asp:Label>
                                            <asp:Label ID="lblHoldingTotalPLValue" Text="" runat="server" CssClass="FieldName"> </asp:Label>
                                            &nbsp;
                                            <asp:Label ID="lblHoldingAbsoluteReturn" Text="Absolute Returns(%): " runat="server"
                                                CssClass="FieldName"> </asp:Label>
                                            <asp:Label ID="lblHoldingAbsoluteReturnValue" Text="" runat="server" CssClass="FieldName"> </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="pnlMFPortfolioHoldings" runat="server" Width="99%" ScrollBars="Horizontal">
                                    <table id="tblHoldings" runat="server" width="100%">
                                        <tr>
                                            <td>
                                                <div id="dvHoldings" runat="server" style="width: 640px;">
                                                    <asp:Label ID="lblMessageHoldings" Visible="false" Text="No Record Exists" runat="server"
                                                        CssClass="Field"></asp:Label>
                                                    <asp:LinkButton ID="lnlGoBackHoldings" runat="server" OnClick="lnlGoBackHoldings_Click"
                                                        Visible="false" CssClass="FieldName">Go Back</asp:LinkButton>
                                                    <asp:ImageButton Visible="false" ID="imgBtnrgHoldings" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportrgHoldingsFilteredData_OnClick"
                                                        OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
                                                    <telerik:RadGrid ID="rgHoldings" runat="server" Width="1500px" PageSize="10" AllowAutomaticDeletes="false"
                                                        ShowFooter="true" AllowAutomaticInserts="false" AllowAutomaticUpdates="false"
                                                        HorizontalAlign="NotSet" AllowPaging="true" GridLines="None" AutoGenerateColumns="false"
                                                        Style="border: 0; outline: none;" Skin="Telerik" EnableEmbeddedSkins="false"
                                                        OnExcelMLExportStylesCreated="rgHoldings_OnExcelMLExportStylesCreated" OnExcelMLExportRowCreated="rgHoldings_OnExcelMLExportRowCreated"
                                                        ShowStatusBar="false" OnItemCommand="rgHoldings_ItemCommand" AllowSorting="true"
                                                        OnItemDataBound="rgHoldings_ItemDataBound" EnableViewState="true" OnNeedDataSource="rgHoldings_OnNeedDataSource"
                                                        AllowFilteringByColumn="true">
                                                        <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                        <%-- <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                                    Excel-Format="ExcelML">
                                                </ExportSettings>--%>
                                                        <MasterTableView DataKeyNames="MFNPId,AccountId,AMCCode,SchemeCode" ShowFooter="true"
                                                            AutoGenerateColumns="false" CommandItemDisplay="None">
                                                            <%-- <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                                        ShowRefreshButton="false" ShowExportToCsvButton="true" ShowAddNewRecordButton="false" />--%>
                                                            <Columns>
                                                                <telerik:GridButtonColumn HeaderStyle-Width="50px" ButtonType="LinkButton" Text="Select"
                                                                    CommandName="Select">
                                                                </telerik:GridButtonColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" SortExpression="Category" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="Category"
                                                                    HeaderText="Category" DataField="Category" FooterText="Grand Total:" FooterStyle-HorizontalAlign="Right"
                                                                    AllowFiltering="true">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" SortExpression="SubCategoryName"
                                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                    UniqueName="SubCategoryName" HeaderText="SubCategory" DataField="SubCategoryName"
                                                                    AllowFiltering="true">
                                                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
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
<%--                                                                <telerik:GridTemplateColumn HeaderStyle-Width="350px" UniqueName="Scheme" SortExpression="Scheme"
                                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                    HeaderText="Scheme" Groupable="False" ItemStyle-Wrap="false" AllowFiltering="true"
                                                                    DataField="Scheme">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkScheme" runat="server" Text='<%# Eval("Scheme").ToString() %>'
                                                                            CommandName="NavigateToMarketData" style="text-decoration:none;color:Black;cursor:"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>--%>
                                                                
                                                                   <telerik:GridBoundColumn HeaderStyle-Width="350px" SortExpression="Scheme" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="Scheme"
                                                                    HeaderText="Scheme" DataField="Scheme">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="103px" SortExpression="FolioNum" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="FolioNum"
                                                                    HeaderText="Folio" DataField="FolioNum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" UniqueName="FolioStartDate"
                                                                    AllowFiltering="false" HeaderText="Scheme Invst. Date" DataField="FolioStartDate"
                                                                    DataFormatString="{0:d}" HtmlEncode="False" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                                    SortExpression="FolioStartDate">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                    <FilterTemplate>
                                                                        <telerik:RadDatePicker ID="resolveDateFilter" runat="server">
                                                                        </telerik:RadDatePicker>
                                                                    </FilterTemplate>
                                                                </telerik:GridDateTimeColumn>
                                                                <telerik:GridDateTimeColumn HeaderStyle-Width="93px" DataField="InvestmentStartDate"
                                                                    SortExpression="InvestmentStartDate" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                                    AllowFiltering="false" HeaderText="Holding Start Date" UniqueName="InvestmentStartDate"
                                                                    DataFormatString="{0:d}">
                                                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                                                    <FilterTemplate>
                                                                        <telerik:RadDatePicker ID="InvestmentStartDateFilter" runat="server">
                                                                        </telerik:RadDatePicker>
                                                                    </FilterTemplate>
                                                                </telerik:GridDateTimeColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" UniqueName="PurchasedUnits" HeaderText="Purchased Units"
                                                                    DataField="PurchasedUnits" AllowFiltering="false" DataFormatString="{0:N3}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="77px" UniqueName="DVRUnits" HeaderText="DVR Units"
                                                                    DataField="DVRUnits" AllowFiltering="false" FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                                                                    DataFormatString="{0:N3}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="120px" UniqueName="OpenUnits" HeaderText="Total Units" Aggregate="Sum"
                                                                    DataField="OpenUnits" AllowFiltering="false" DataFormatString="{0:N3}" FooterStyle-HorizontalAlign="Right">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="77px" UniqueName="CMFNP_RET_Hold_DVRAmounts" Aggregate="Sum"
                                                                    HeaderText="DVR Amount" DataField="CMFNP_RET_Hold_DVRAmounts" AllowFiltering="false"
                                                                    FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="120px" UniqueName="InvestedCost" HeaderText="Invested Cost" Aggregate="Sum"
                                                                    DataField="InvestedCost" AllowFiltering="false" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="86px" UniqueName="NAV" HeaderText="NAV"
                                                                    DataField="NAV" AllowFiltering="false" DataFormatString="{0:N4}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="86px" UniqueName="CMFNP_NAVDate " HeaderText="NAV Date"
                                                                    DataField="CMFNP_NAVDate" AllowFiltering="false" DataFormatString="{0:d}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="88px" UniqueName="MarketValue" HeaderText="Market Value" Aggregate="Sum"
                                                                    DataField="MarketValue" AllowFiltering="false" DataFormatString="{0:N0}" DataType="System.Double"
                                                                    FooterStyle-HorizontalAlign="Right" FooterText="">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="40px" UniqueName="DVP" HeaderText="DVP" Aggregate="Sum"
                                                                    DataField="DVP" AllowFiltering="false" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="77px" UniqueName="TotalPL" HeaderText="Total P/L" 
                                                                    DataField="TotalPL" AllowFiltering="false" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="79px" UniqueName="AbsoluteReturn" HeaderText="Absolute Return (%)"
                                                                    DataField="AbsoluteReturn" AllowFiltering="false" DataFormatString="{0:N2}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="54px" UniqueName="XIRR" HeaderText="XIRR (%)"
                                                                    DataField="XIRR" AllowFiltering="false" DataFormatString="{0:N2}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn SortExpression="Weighted NAV" HeaderStyle-Width="77px" UniqueName="Weighted_NAV"
                                                                    HeaderText="Weighted NAV" DataField="Weighted NAV" AllowFiltering="false" DataFormatString="{0:N4}"
                                                                    FooterStyle-HorizontalAlign="Right">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                  <telerik:GridBoundColumn UniqueName="CMFNP_ValuationDate" HeaderText="Valuation As On" DataField="CMFNP_ValuationDate"
                                                                    AllowFiltering="false"  FooterStyle-HorizontalAlign="Right">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <%-- <telerik:GridBoundColumn HeaderStyle-Width="77px" SortExpression="weightage returns" UniqueName="Annualized_Return" HeaderText="Annualized Return (%)"
                                                                    DataField="weightage returns"  AllowFiltering="false" DataFormatString="{0:N2}"  FooterStyle-HorizontalAlign="Right">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                
                                                                 <telerik:GridBoundColumn   HeaderStyle-Width="77px" SortExpression="Weighted Days" UniqueName="Weighted_Days" HeaderText="Weighted Days"
                                                                    DataField="Weighted Days" AllowFiltering="false">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>   --%>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <HeaderStyle Width="150px" />
                                                        <ClientSettings>
                                                            <Resizing AllowColumnResize="true" />
                                                            <Selecting AllowRowSelect="true" />
                                                        </ClientSettings>
                                                    </telerik:RadGrid>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="MFPortfolioAllTabPage" runat="server">
                                <table width="100%">
                                    <tr align="left">
                                        <td style="width: 100%;">
                                            <asp:Label ID="lblALLTotalPL" Text="Total P/L: " runat="server" CssClass="FieldName"> </asp:Label>
                                            <asp:Label ID="lblALLTotalPLValue" Text="" runat="server" CssClass="FieldName"> </asp:Label>
                                            &nbsp;
                                            <asp:Label ID="lblALLAbsoluteReturns" Text="Absolute Returns(%): " runat="server"
                                                CssClass="FieldName"> </asp:Label>
                                            <asp:Label ID="lblALLAbsoluteReturnsValue" Text="" runat="server" CssClass="FieldName"> </asp:Label>
                                            &nbsp;
                                            <asp:Label ID="lblPortfolioXIRR" Text="Portfolio XIRR (%): " runat="server" CssClass="FieldName"> </asp:Label>
                                            <asp:Label ID="lblPortfolioXIRRValue" Text="" runat="server" CssClass="FieldName"> </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="pnlMFPortfolioAll" runat="server" class="Landscape" Width="99%" ScrollBars="Horizontal">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <div id="dvAll" runat="server" style="width: 640px">
                                                    <asp:Label ID="lblMessageAll" Visible="false" Text="No Record Exists" runat="server"
                                                        CssClass="Field"></asp:Label>
                                                    <asp:LinkButton ID="lnkGoBackAll" runat="server" OnClick="lnkGoBackAll_Click" Visible="false"
                                                        CssClass="FieldName">Go Back</asp:LinkButton>
                                                    <asp:ImageButton Visible="false" ID="imgBtnrgAll" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportrgAllFilteredData_OnClick"
                                                        OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
                                                    <telerik:RadGrid ID="rgAll" runat="server" Width="1500px" PageSize="10" AllowPaging="True"
                                                        OnExcelMLExportStylesCreated="rgAll_OnExcelMLExportStylesCreated" OnExcelMLExportRowCreated="rgAll_OnExcelMLExportRowCreated"
                                                        GridLines="None" AutoGenerateColumns="true" Style="border: 0; outline: none;"
                                                        Skin="Telerik" EnableEmbeddedSkins="false" OnItemCommand="rgAll_ItemCommand"
                                                        OnItemDataBound="rgAll_ItemDataBound" AllowSorting="true" EnableViewState="true"
                                                        OnNeedDataSource="rgAll_OnNeedDataSource" AllowFilteringByColumn="true">
                                                        <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                        <%--  <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                                    Excel-Format="ExcelML">
                                                </ExportSettings>--%>
                                                        <MasterTableView DataKeyNames="MFNPId,AccountId,AMCCode,SchemeCode" ShowFooter="true"
                                                            Width="100%" AutoGenerateColumns="false" CommandItemDisplay="None">
                                                            <%--<CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                                        ShowRefreshButton="false" ShowExportToCsvButton="true" ShowAddNewRecordButton="false" />--%>
                                                            <Columns>
                                                                <telerik:GridButtonColumn HeaderStyle-Width="50px" ButtonType="LinkButton" Text="Select"
                                                                    CommandName="Select">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridButtonColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" SortExpression="Category" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="Category"
                                                                    HeaderText="Category" DataField="Category" AllowFiltering="true" FooterText="Grand Total:"
                                                                    FooterStyle-HorizontalAlign="Right">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" UniqueName="SubCategoryName" SortExpression="SubCategoryName"
                                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                    HeaderText="SubCategory" DataField="SubCategoryName" AllowFiltering="true">
                                                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                                </telerik:GridBoundColumn>
                                                                <%--<telerik:GridTemplateColumn AllowFiltering="false" FooterStyle-HorizontalAlign="Right">
                                                        <HeaderTemplateFolioStartDate
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
                                                            <%--    <telerik:GridTemplateColumn HeaderStyle-Width="350px" UniqueName="Schemes" SortExpression="Scheme"
                                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                    HeaderText="Scheme" Groupable="False" ItemStyle-Wrap="false" AllowFiltering="true"
                                                                    DataField="Scheme">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkScheme" runat="server" Text='<%# Eval("Scheme").ToString() %>'
                                                                            CommandName="NavigateToMarketData"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>--%>
                                                                  <telerik:GridBoundColumn HeaderStyle-Width="350px" SortExpression="Scheme" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="Scheme"
                                                                    HeaderText="Scheme" DataField="Scheme">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" SortExpression="FolioNum" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="FolioNum"
                                                                    HeaderText="Folio" DataField="FolioNum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <%-- <telerik:GridDateTimeColumn UniqueName="FolioStartDate" 
                                                              AllowFiltering="True" HeaderText="FolioStartDate"
                                                            DataField="FolioStartDate" DataFormatString="{0:d}"  AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                            SortExpression="FolioStartDate">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FilterTemplate>
                                                                <telerik:RadDatePicker  ID="resolveDateFilter" runat="server">
                                                                </telerik:RadDatePicker>
                                                            </FilterTemplate>
                                                        </telerik:GridDateTimeColumn>--%>
                                                                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="FolioStartDate"
                                                                    SortExpression="FolioStartDate" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                                    AllowFiltering="false" HeaderText="Scheme Invst. Date" UniqueName="FolioStartDate"
                                                                    DataFormatString="{0:d}" HtmlEncode="False">
                                                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                                                    <FilterTemplate>
                                                                        <telerik:RadDatePicker ID="resolveDateFilter" runat="server">
                                                                        </telerik:RadDatePicker>
                                                                    </FilterTemplate>
                                                                </telerik:GridDateTimeColumn>
                                                                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="InvestmentStartDate"
                                                                    SortExpression="InvestmentStartDate" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                                    AllowFiltering="false" HeaderText="Holding Start Date" UniqueName="InvestmentStartDate"
                                                                    DataFormatString="{0:d}">
                                                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                                                    <FilterTemplate>
                                                                        <telerik:RadDatePicker ID="InvestmentStartDateFilter" runat="server">
                                                                        </telerik:RadDatePicker>
                                                                    </FilterTemplate>
                                                                </telerik:GridDateTimeColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" UniqueName="PurchasedUnits" HeaderText="Purchased Units"
                                                                    DataField="PurchasedUnits" AllowFiltering="false" DataFormatString="{0:N3}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" UniqueName="DVRUnits" HeaderText="DVR Units"
                                                                    DataField="DVRUnits" AllowFiltering="false" FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                                                                    DataFormatString="{0:N3}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="120px" UniqueName="OpenUnits" HeaderText="Total Units"
                                                                    DataField="OpenUnits" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N3}" Aggregate="Sum"
                                                                    AllowFiltering="false">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <%--<telerik:GridBoundColumn UniqueName="Price" HeaderText="Price" DataField="Price"
                                                        FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" Aggregate="Sum"
                                                        AllowFiltering="false">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </telerik:GridBoundColumn>--%>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="120px" UniqueName="InvestedCost" HeaderText="Invested Cost"
                                                                    DataField="InvestedCost" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" Aggregate="Sum"
                                                                    AllowFiltering="false">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" UniqueName="NAV" HeaderText="NAV"
                                                                    DataField="NAV" FooterStyle-HorizontalAlign="Right" AllowFiltering="false" DataFormatString="{0:N4}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="86px" UniqueName="CMFNP_NAVDate " HeaderText="NAV Date"
                                                                    DataField="CMFNP_NAVDate" AllowFiltering="false" DataFormatString="{0:d}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="CurrentValue" HeaderText="Current Value" DataField="CurrentValue"
                                                                    FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" AllowFiltering="false" Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" UniqueName="UnitsSold" HeaderText="Units Sold"
                                                                    DataField="UnitsSold" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" Aggregate="Sum"
                                                                    AllowFiltering="false">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" UniqueName="RedeemedAmount" HeaderText="Redeemed Amount"
                                                                    DataField="RedeemedAmount" FooterStyle-HorizontalAlign="Right" AllowFiltering="false" Aggregate="Sum"
                                                                    DataFormatString="{0:N0}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="46px" UniqueName="DVP" HeaderText="DVP"
                                                                    DataField="DVP" AllowFiltering="false" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="65px" UniqueName="TotalPL" HeaderText="Total P/L"
                                                                    DataField="TotalPL" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                                                                    AllowFiltering="false">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="59px" UniqueName="AbsoluteReturn" HeaderText="Absolute Return (%)"
                                                                    DataField="AbsoluteReturn" AllowFiltering="false" DataFormatString="{0:N2}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="59px" UniqueName="DVR" HeaderText="DVR"
                                                                    DataField="DVR" DataFormatString="{0:N0}" AllowFiltering="false" FooterStyle-HorizontalAlign="Right"  Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="63px" UniqueName="XIRR" HeaderText="XIRR (%)"
                                                                    DataField="XIRR" AllowFiltering="false" DataFormatString="{0:N2}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="87px" UniqueName="TotalDividends" HeaderText="Total Dividends"
                                                                    DataField="TotalDividends" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" Aggregate="Sum"
                                                                    AllowFiltering="false">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                  <telerik:GridBoundColumn UniqueName="CMFNP_ValuationDate" HeaderText="Valuation As On" DataField="CMFNP_ValuationDate"
                                                                    AllowFiltering="false"  FooterStyle-HorizontalAlign="Right">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <HeaderStyle Width="150px" />
                                                        <ClientSettings>
                                                            <Resizing AllowColumnResize="true" />
                                                            <Selecting AllowRowSelect="true" />
                                                        </ClientSettings>
                                                    </telerik:RadGrid>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="MFPortfolioRealizedTabPage" runat="server">
                                <table width="99%">
                                    <tr align="left">
                                        <td style="width: 100%;">
                                            <asp:Label ID="lblRealizedTotalPL" Text="Total P/L: " runat="server" CssClass="FieldName"> </asp:Label>
                                            <asp:Label ID="lblRealizedTotalPLValue" Text="" runat="server" CssClass="FieldName"> </asp:Label>
                                            &nbsp;
                                            <asp:Label ID="lblRealizedAbsoluteReturn" Text="Absolute Returns(%): " runat="server"
                                                CssClass="FieldName"> </asp:Label>
                                            <asp:Label ID="lblRealizedAbsoluteReturnValue" Text="" runat="server" CssClass="FieldName"> </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="pnlMFPortfolioRealized" runat="server" class="Landscape" Width="99%"
                                    ScrollBars="Horizontal">
                                    <table id="tblRealized" runat="server" width="99%">
                                        <tr>
                                            <td>
                                                <div id="dvRealized" runat="server" style="width: 640px">
                                                    <asp:Label ID="lblMessageRealized" Visible="false" Text="No Record Exists" runat="server"
                                                        CssClass="Field"></asp:Label>
                                                    <asp:LinkButton ID="lnkGoBackRealized" runat="server" OnClick="lnkGoBackRealized_Click"
                                                        Visible="false" CssClass="FieldName">Go Back</asp:LinkButton>
                                                    <asp:ImageButton Visible="false" ID="imgBtnrgRealized" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportrgRealizedFilteredData_OnClick"
                                                        OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
                                                    <telerik:RadGrid ID="rgRealized" runat="server" Width="1500px" PageSize="10" AllowPaging="True"
                                                        AllowSorting="true" GridLines="None" AutoGenerateColumns="true" Style="border: 0;
                                                        outline: none;" OnExcelMLExportStylesCreated="rgRealized_OnExcelMLExportStylesCreated"
                                                        OnExcelMLExportRowCreated="rgRealized_OnExcelMLExportRowCreated" Skin="Telerik"
                                                        EnableEmbeddedSkins="false" OnItemCommand="rgRealized_ItemCommand" OnItemDataBound="rgRealized_ItemDataBound"
                                                        EnableViewState="true" OnNeedDataSource="rgRealized_OnNeedDataSource" AllowFilteringByColumn="true">
                                                        <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                        <%-- <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                                    Excel-Format="ExcelML">
                                                </ExportSettings>--%>
                                                        <MasterTableView DataKeyNames="MFNPId,AccountId,AMCCode,SchemeCode" ShowFooter="true"
                                                            Width="100%" AutoGenerateColumns="false" CommandItemDisplay="None">
                                                            <%--<CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                                        ShowRefreshButton="false" ShowExportToCsvButton="true" ShowAddNewRecordButton="false" />--%>
                                                            <Columns>
                                                                <telerik:GridButtonColumn HeaderStyle-Width="50px" ButtonType="LinkButton" Text="Select"
                                                                    CommandName="Select">
                                                                </telerik:GridButtonColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" SortExpression="Category" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="Category"
                                                                    HeaderText="Category" DataField="Category" AllowFiltering="true" FooterText="Grand Total:"
                                                                    FooterStyle-HorizontalAlign="Right">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" SortExpression="SubCategory" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="SubCategoryName"
                                                                    HeaderText="SubCategory" DataField="SubCategoryName" AllowFiltering="true">
                                                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
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
                                                               <%-- <telerik:GridTemplateColumn HeaderStyle-Width="350px" SortExpression="Scheme" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="Schemes"
                                                                    HeaderText="Scheme" Groupable="False" ItemStyle-Wrap="false" AllowFiltering="true"
                                                                    DataField="Scheme">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkScheme" runat="server" Text='<%# Eval("Scheme").ToString() %>'
                                                                            CommandName="NavigateToMarketData"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>--%>
                                                                 <telerik:GridBoundColumn HeaderStyle-Width="350px" SortExpression="Scheme" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="Scheme"
                                                                    HeaderText="Scheme" DataField="Scheme">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" SortExpression="FolioNum" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="FolioNum"
                                                                    HeaderText="Folio" DataField="FolioNum">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridBoundColumn>
                                                                <%--  <telerik:GridDateTimeColumn  AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                            SortExpression="FolioStartDate" UniqueName="FolioStartDate" AllowFiltering="True" HeaderText="FolioStartDate"
                                                            DataField="FolioStartDate"  
                                                            DataFormatString="{0:d}"  >
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FilterTemplate>
                                                                <telerik:RadDatePicker ID="resolveDateFilter" runat="server">
                                                                </telerik:RadDatePicker>
                                                            </FilterTemplate>
                                                        </telerik:GridDateTimeColumn>--%>
                                                                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="FolioStartDate"
                                                                    SortExpression="FolioStartDate" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                                    AllowFiltering="false" HeaderText="Scheme Invst. Date" UniqueName="FolioStartDate"
                                                                    DataFormatString="{0:d}">
                                                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                                                    <FilterTemplate>
                                                                        <telerik:RadDatePicker ID="resolveDateFilter" runat="server">
                                                                        </telerik:RadDatePicker>
                                                                    </FilterTemplate>
                                                                </telerik:GridDateTimeColumn>
                                                                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="InvestmentStartDate"
                                                                    SortExpression="InvestmentStartDate" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                                    AllowFiltering="false" HeaderText="Holding Start Date" UniqueName="InvestmentStartDate"
                                                                    DataFormatString="{0:d}">
                                                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                                                    <FilterTemplate>
                                                                        <telerik:RadDatePicker ID="InvestmentStartDateFilter" runat="server">
                                                                        </telerik:RadDatePicker>
                                                                    </FilterTemplate>
                                                                </telerik:GridDateTimeColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="120px" UniqueName="InvestedCost" HeaderText="Invested Cost"
                                                                    DataField="InvestedCost" AllowFiltering="false" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right"  Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" UniqueName="UnitsSold" HeaderText="Units Sold"
                                                                    DataField="UnitsSold" AllowFiltering="false" DataFormatString="{0:N3}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" UniqueName="RedeemedAmount" HeaderText="Redeemed Amount"
                                                                    DataField="RedeemedAmount" AllowFiltering="false" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="50px" UniqueName="DVP" HeaderText="DVP"
                                                                    DataField="DVP" AllowFiltering="false" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="90px" UniqueName="TotalDividends" HeaderText="Total Dividends"
                                                                    DataField="TotalDividends" AllowFiltering="false" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="81px" UniqueName="TotalPL" HeaderText="Total P/L"
                                                                    DataField="TotalPL" AllowFiltering="false" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="63px" UniqueName="AbsoluteReturn" HeaderText="Absolute Return (%)"
                                                                    DataField="AbsoluteReturn" AllowFiltering="false" DataFormatString="{0:N2}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="63px" UniqueName="XIRR" HeaderText="XIRR (%)"
                                                                    DataField="XIRR" AllowFiltering="false" DataFormatString="{0:N2}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                  <telerik:GridBoundColumn UniqueName="CMFNP_ValuationDate" HeaderText="Valuation As On" DataField="CMFNP_ValuationDate"
                                                                    AllowFiltering="false"  FooterStyle-HorizontalAlign="Right">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <HeaderStyle Width="150px" />
                                                        <ClientSettings>
                                                            <Resizing AllowColumnResize="true" />
                                                            <Selecting AllowRowSelect="true" />
                                                        </ClientSettings>
                                                    </telerik:RadGrid>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="MFPandATabPage" runat="server">
                                <asp:Panel ID="pnlMFPandATabPage" runat="server" class="Landscape" Width="99%">
                                    <table id="tblPandA" runat="server" width="99%">
                                        <tr id="trMFCode" runat="server">
                                            <td>
                                                <asp:DropDownList ID="ddlMFClassificationCode" runat="server" CssClass="cmbField"
                                                    AutoPostBack="true" Height="16px" Width="176px" OnSelectedIndexChanged="ddlMFClassificationCode_SelectedIndexChanged">
                                                    <%--<asp:ListItem>MF Classification Code</asp:ListItem>--%>
                                                    <asp:ListItem Text="Category Wise" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Scheme Performance" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Top ten Holdings" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Top ten Sectors" Value="3"></asp:ListItem>
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
                                            <td>
                                            </td>
                                        </tr>
                                        <tr id="trSubCategoryWise" runat="server">
                                            <td>
                                                <div id="divSubCategory" runat="server">
                                                    <asp:Chart ID="chrtSubCategory" runat="server" BackColor="Transparent" Palette="Pastel"
                                                        Width="500px" Height="250px">
                                                        <Series>
                                                            <asp:Series Name="Series1">
                                                            </asp:Series>
                                                        </Series>
                                                        <ChartAreas>
                                                            <asp:ChartArea Name="ChartArea1">
                                                            </asp:ChartArea>
                                                        </ChartAreas>
                                                    </asp:Chart>
                                                </div>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <table width="100%">
                                    <tr id="trSchemePerformance" runat="server" visible="false">
                                        <td>
                                            <asp:Panel ID="tbl" runat="server" class="Landscape" ScrollBars="Horizontal" Width="99%">
                                                <table width="99%">
                                                    <tr>
                                                        <td>
                                                            <div id="Div1" runat="server" style="width: 640px;">
                                                                <telerik:RadAjaxPanel ID="PanelScheme" runat="server" Width="99%" EnableHistory="True"
                                                                    HorizontalAlign="NotSet" LoadingPanelID="SchemePerformanceLoading">
                                                                    <telerik:RadGrid ID="gvSchemePerformance" runat="server" Width="1500px" PageSize="10"
                                                                        AllowPaging="True" AllowSorting="true" GridLines="None" AutoGenerateColumns="true"
                                                                        Style="border: 0; outline: none;" Skin="Telerik" EnableEmbeddedSkins="false"
                                                                        OnItemCommand="rgRealized_ItemCommand" EnableViewState="true" OnNeedDataSource="gvSchemePerformance_OnNeedDataSource">
                                                                        <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                                                                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                                            <Columns>
                                                                                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="SchemeName" HeaderText="Scheme Name"
                                                                                    SortExpression="SchemeName" UniqueName="SchemeName" AutoPostBackOnFilter="true"
                                                                                    AllowFiltering="true" ShowFilterIcon="false">
                                                                                    <HeaderStyle></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="false" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="OneMonthReturn" HeaderText="1M Rtn(%)" SortExpression="OneMonthReturn"
                                                                                    UniqueName="OneMonthReturn" AutoPostBackOnFilter="true" DataFormatString="{0:N2}"
                                                                                    AllowFiltering="true" ShowFilterIcon="false">
                                                                                    <HeaderStyle></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="SixMonthReturn" HeaderText="6M Rtn(%)" SortExpression="SixMonthReturn"
                                                                                    UniqueName="SixMonthReturn" AutoPostBackOnFilter="true" DataFormatString="{0:N2}"
                                                                                    AllowFiltering="true" ShowFilterIcon="false">
                                                                                    <HeaderStyle></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="OneYearReturn" HeaderText="1Y Rtn(%)" SortExpression="OneYearReturn"
                                                                                    UniqueName="OneYearReturn" AutoPostBackOnFilter="true" DataFormatString="{0:N2}"
                                                                                    AllowFiltering="true" ShowFilterIcon="false">
                                                                                    <HeaderStyle></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="ThreeYearReturn" HeaderText="3Y Rtn(%)" SortExpression="ThreeYearReturn"
                                                                                    UniqueName="ThreeYearReturn" AutoPostBackOnFilter="true" DataFormatString="{0:N2}"
                                                                                    AllowFiltering="true" ShowFilterIcon="false">
                                                                                    <HeaderStyle></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="FiveYearReturn" HeaderText="5Y Rtn(%)" SortExpression="FiveYearReturn"
                                                                                    UniqueName="FiveYearReturn" AutoPostBackOnFilter="true" DataFormatString="{0:N2}"
                                                                                    AllowFiltering="true" ShowFilterIcon="false">
                                                                                    <HeaderStyle></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="InceptionReturn" HeaderText="Since Inception"
                                                                                    SortExpression="InceptionReturn" UniqueName="InceptionReturn" AutoPostBackOnFilter="true"
                                                                                    DataFormatString="{0:N2}" AllowFiltering="true" ShowFilterIcon="false">
                                                                                    <HeaderStyle></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="PE" HeaderText="PE" SortExpression="PE" UniqueName="PE"
                                                                                    AutoPostBackOnFilter="true" DataFormatString="{0:N2}" AllowFiltering="true" ShowFilterIcon="false">
                                                                                    <HeaderStyle></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="PB" HeaderText="PB" SortExpression="PB" UniqueName="PB"
                                                                                    AutoPostBackOnFilter="true" DataFormatString="{0:N2}" AllowFiltering="true" ShowFilterIcon="false">
                                                                                    <HeaderStyle></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Sharpe" HeaderText="Sharpe" SortExpression="Sharpe"
                                                                                    UniqueName="Sharpe" AutoPostBackOnFilter="true" DataFormatString="{0:N2}" AllowFiltering="true"
                                                                                    ShowFilterIcon="false">
                                                                                    <HeaderStyle></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Sd" HeaderText="Sd" SortExpression="Sd" UniqueName="Sd"
                                                                                    AutoPostBackOnFilter="true" DataFormatString="{0:N2}" AllowFiltering="true" ShowFilterIcon="false">
                                                                                    <HeaderStyle></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Top5Holding" HeaderText="Top5Holding" SortExpression="Top5Holding"
                                                                                    UniqueName="Top5Holding" AutoPostBackOnFilter="true" DataFormatString="{0:N2}"
                                                                                    AllowFiltering="true" ShowFilterIcon="false">
                                                                                    <HeaderStyle></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                                                </telerik:GridBoundColumn>
                                                                            </Columns>
                                                                        </MasterTableView>
                                                                        <ClientSettings>
                                                                            <Resizing AllowColumnResize="true" />
                                                                            <Selecting AllowRowSelect="true" EnableDragToSelectRows="true" />
                                                                            <Scrolling AllowScroll="true" SaveScrollPosition="true" FrozenColumnsCount="1"></Scrolling>
                                                                            <ClientEvents />
                                                                        </ClientSettings>
                                                                    </telerik:RadGrid>
                                                                </telerik:RadAjaxPanel>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr id="trTop10HoldingsPie" runat="server">
                                        <td>
                                            <asp:Chart ID="chrtTopHoldings" runat="server" BackColor="Transparent" Width="400px"
                                                Height="200px">
                                                <Series>
                                                    <asp:Series Name="Series1">
                                                    </asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="ChartArea1">
                                                    </asp:ChartArea>
                                                </ChartAreas>
                                            </asp:Chart>
                                        </td>
                                    </tr>
                                    <tr id="trHoldingGrid" runat="server">
                                        <td>
                                            <asp:Panel ID="pnlHoldingGrid" runat="server" class="Landscape" Width="99%">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <telerik:RadGrid ID="gvTopTenHoldings" runat="server" GridLines="None" AutoGenerateColumns="False"
                                                                PageSize="10" AllowSorting="false" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                                                Skin="Telerik" EnableEmbeddedSkins="false" Width="700px" AllowFilteringByColumn="false"
                                                                AllowAutomaticInserts="false" ExportSettings-FileName="TopTenHoldings">
                                                                <ExportSettings HideStructureColumns="true">
                                                                </ExportSettings>
                                                                <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                                                    CommandItemDisplay="None">
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="Instrument" HeaderText="Instrument" SortExpression="Instrument"
                                                                            UniqueName="Instrument" AutoPostBackOnFilter="true" FooterText="Grand Total:"
                                                                            FooterStyle-HorizontalAlign="Right">
                                                                            <HeaderStyle></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="InsType" HeaderText="InsType" SortExpression="InsType"
                                                                            UniqueName="InsType" AutoPostBackOnFilter="true">
                                                                            <HeaderStyle></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Amount" HeaderText="Amount" SortExpression="Amount"
                                                                            UniqueName="Amount" AutoPostBackOnFilter="true" DataFormatString="{0:N0}" Aggregate="Sum"
                                                                            FooterStyle-HorizontalAlign="Right">
                                                                            <HeaderStyle></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                                <HeaderStyle Width="150px" />
                                                                <ClientSettings>
                                                                    <Resizing AllowColumnResize="true" />
                                                                    <Selecting AllowRowSelect="true" />
                                                                </ClientSettings>
                                                            </telerik:RadGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr id="trTopTenSectors" runat="server">
                                        <td>
                                            <asp:Chart ID="chrtTopTenSectors" runat="server" BackColor="Transparent" Width="400px"
                                                Height="200px">
                                                <Series>
                                                    <asp:Series Name="Series1">
                                                    </asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="ChartArea1">
                                                    </asp:ChartArea>
                                                </ChartAreas>
                                            </asp:Chart>
                                        </td>
                                    </tr>
                                    <tr id="trSectorGrid" runat="server">
                                        <td>
                                            <telerik:RadGrid ID="gvSectors" runat="server" GridLines="None" AutoGenerateColumns="False"
                                                PageSize="10" AllowSorting="false" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                                Skin="Telerik" EnableEmbeddedSkins="false" Width="700px" AllowFilteringByColumn="false"
                                                AllowAutomaticInserts="false" ExportSettings-FileName="TopTenHoldings">
                                                <ExportSettings HideStructureColumns="true">
                                                </ExportSettings>
                                                <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                                    CommandItemDisplay="None">
                                                    <Columns>
                                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SectorCode" HeaderText="SectorCode"
                                                            SortExpression="SectorCode" UniqueName="SectorCode" AutoPostBackOnFilter="true"
                                                            FooterText="Grand Total:" FooterStyle-HorizontalAlign="Right">
                                                            <HeaderStyle></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Sector" HeaderText="Sector" SortExpression="Sector"
                                                            UniqueName="Sector" AutoPostBackOnFilter="true">
                                                            <HeaderStyle></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Amount" HeaderText="Amount" SortExpression="Amount"
                                                            UniqueName="Amount" AutoPostBackOnFilter="true" DataFormatString="{0:N0}" Aggregate="Sum"
                                                            FooterStyle-HorizontalAlign="Right">
                                                            <HeaderStyle></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <HeaderStyle Width="150" />
                                                <ClientSettings>
                                                    <Resizing AllowColumnResize="true" />
                                                    <Selecting AllowRowSelect="true" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
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
                                <asp:Panel ID="pnlTaxHoldings" runat="server" class="Landscape" Width="99%" ScrollBars="Horizontal">
                                    <table id="tblTaxHoldings" runat="server" width="99%">
                                        <tr>
                                            <td>
                                                <div id="dvTaxHoldings" runat="server" style="width: 640px">
                                                    <asp:Label ID="lblMessageTaxHoldings" Visible="false" Text="No Record Exists" runat="server"
                                                        CssClass="Field"></asp:Label>
                                                    <asp:LinkButton ID="lnlGoBackTaxHoldings" runat="server" OnClick="lnlGoBackTaxHoldings_Click"
                                                        Visible="false" CssClass="FieldName">Go Back</asp:LinkButton>
                                                    <asp:ImageButton Visible="false" ID="imgBtnrgTaxHoldings" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportrgTaxHoldingsFilteredData_OnClick"
                                                        OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
                                                    <telerik:RadGrid ID="rgTaxHoldings" runat="server" Width="1500px" PageSize="10" AllowPaging="true"
                                                        AllowSorting="true" GridLines="None" AutoGenerateColumns="true" Style="border: 0;
                                                        outline: none;" OnExcelMLExportStylesCreated="rgTaxHoldings_OnExcelMLExportStylesCreated"
                                                        OnExcelMLExportRowCreated="rgTaxHoldings_OnExcelMLExportRowCreated" Skin="Telerik"
                                                        EnableEmbeddedSkins="false" OnItemCommand="rgTaxHoldings_ItemCommand" OnItemDataBound="rgTaxHoldings_OnItemDataBound"
                                                        EnableViewState="true" OnNeedDataSource="rgTaxHoldings_OnNeedDataSource" AllowFilteringByColumn="true">
                                                        <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                        <%--<ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                                    Excel-Format="ExcelML">
                                                </ExportSettings>--%>
                                                        <MasterTableView DataKeyNames="MFNPId,AccountId,AMCCode,SchemeCode" ShowFooter="true"
                                                            AutoGenerateColumns="false" CommandItemDisplay="None" Width="100%">
                                                            <%--<CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                                        ShowRefreshButton="false" ShowExportToCsvButton="true" ShowAddNewRecordButton="false" />--%>
                                                            <Columns>
                                                                <telerik:GridButtonColumn HeaderStyle-Width="50px" ButtonType="LinkButton" Text="Select"
                                                                    CommandName="Select">
                                                                </telerik:GridButtonColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" SortExpression="Category" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="Category"
                                                                    HeaderText="Category" DataField="Category" AllowFiltering="true" FooterText="Grand Total:"
                                                                    FooterStyle-HorizontalAlign="Right">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn SortExpression="SubCategory" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="SubCategoryName"
                                                                    HeaderText="SubCategory" DataField="SubCategoryName" AllowFiltering="true">
                                                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
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
                                                                <telerik:GridTemplateColumn HeaderStyle-Width="350px" SortExpression="Scheme" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="Schemes"
                                                                    HeaderText="Scheme" Groupable="False" ItemStyle-Wrap="false" AllowFiltering="true"
                                                                    DataField="Scheme">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkScheme" runat="server" Text='<%# Eval("Scheme").ToString() %>'
                                                                            CommandName="NavigateToMarketData"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridBoundColumn SortExpression="FolioNum" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                                    ShowFilterIcon="false" UniqueName="FolioNum" HeaderText="Folio" DataField="FolioNum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <%-- <telerik:GridDateTimeColumn
                                                            UniqueName="FolioStartDate" HeaderText=""
                                                            AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                            SortExpression=""
                                                             AllowFiltering="True" DataField="" DataFormatString="{0:d}"  >
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FilterTemplate>
                                                                <telerik:RadDatePicker ID="" runat="server">
                                                                </telerik:RadDatePicker>
                                                            </FilterTemplate>
                                                        </telerik:GridDateTimeColumn>--%>
                                                                <telerik:GridDateTimeColumn DataField="FolioStartDate" SortExpression="FolioStartDate"
                                                                    AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" AllowFiltering="false"
                                                                    HeaderText="Scheme Invst. Date" UniqueName="FolioStartDate" DataFormatString="{0:d}">
                                                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                                                    <FilterTemplate>
                                                                        <telerik:RadDatePicker ID="resolveDateFilter" runat="server">
                                                                        </telerik:RadDatePicker>
                                                                    </FilterTemplate>
                                                                </telerik:GridDateTimeColumn>
                                                                <telerik:GridDateTimeColumn DataField="InvestmentStartDate" SortExpression="InvestmentStartDate"
                                                                    AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" AllowFiltering="false"
                                                                    HeaderText="Holding Start Date" UniqueName="InvestmentStartDate" DataFormatString="{0:d}">
                                                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                                                    <FilterTemplate>
                                                                        <telerik:RadDatePicker ID="InvestmentStartDateFilter" runat="server">
                                                                        </telerik:RadDatePicker>
                                                                    </FilterTemplate>
                                                                </telerik:GridDateTimeColumn>
                                                                <telerik:GridBoundColumn UniqueName="OpenUnits" HeaderText="Total Units" DataField="OpenUnits"
                                                                    AllowFiltering="false" DataFormatString="{0:N3}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="BalanceAmount" HeaderText="Acquisition Cost"
                                                                    DataField="BalanceAmount" AllowFiltering="false" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="NAV" HeaderText="NAV" DataField="NAV" AllowFiltering="false"
                                                                    DataFormatString="{0:N4}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="86px" UniqueName="CMFNP_NAVDate " HeaderText="NAV Date"
                                                                    DataField="CMFNP_NAVDate" AllowFiltering="false" DataFormatString="{0:d}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="MarketValue" HeaderText="Market Value" DataField="MarketValue"
                                                                    AllowFiltering="false" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="UnrealizedPL" HeaderText="P/L" DataField="UnrealizedPL"
                                                                    AllowFiltering="false" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="EligibleSTCG" HeaderText="Eligible STCG" DataField="EligibleSTCG"
                                                                    AllowFiltering="false" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="EligibleLTCG" HeaderText="Eligible LTCG" DataField="EligibleLTCG"
                                                                    AllowFiltering="false" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="CMFNP_ValuationDate" HeaderText="Valuation As On" DataField="CMFNP_ValuationDate"
                                                                    AllowFiltering="false"  FooterStyle-HorizontalAlign="Right">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings>
                                                            <Resizing AllowColumnResize="true" />
                                                            <Selecting AllowRowSelect="true" />
                                                        </ClientSettings>
                                                    </telerik:RadGrid>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="TaxRealizedTabPage" runat="server">
                                <asp:Panel ID="pnlTaxRealized" runat="server" class="Landscape" Width="99%" ScrollBars="Horizontal">
                                    <table id="tblTaxRealized" runat="server" width="99%">
                                        <tr>
                                            <td>
                                                <div id="dvTaxRealized" runat="server" style="width: 640px">
                                                    <asp:Label ID="lblTaxRealized" Visible="false" Text="No Record Exists" runat="server"
                                                        CssClass="Field"></asp:Label>
                                                    <asp:LinkButton ID="lnlGoBackTaxRealized" runat="server" OnClick="lnlGoBackTaxRealized_Click"
                                                        Visible="false" CssClass="FieldName">Go Back</asp:LinkButton>
                                                    <asp:ImageButton Visible="false" ID="imgBtnrgTaxRealized" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportrgTaxRealizedFilteredData_OnClick"
                                                        OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
                                                    <telerik:RadGrid ID="rgTaxRealized" runat="server" Width="1500px" PageSize="10" AllowPaging="true"
                                                        AllowSorting="true" GridLines="None" AutoGenerateColumns="true" Style="border: 0;
                                                        outline: none;" OnExcelMLExportStylesCreated="rgTaxRealized_OnExcelMLExportStylesCreated"
                                                        OnExcelMLExportRowCreated="rgTaxRealized_OnExcelMLExportRowCreated" Skin="Telerik"
                                                        EnableEmbeddedSkins="false" OnItemCommand="rgTaxRealized_ItemCommand" OnItemDataBound="rgTaxRealized_OnItemDataBound"
                                                        EnableViewState="true" OnNeedDataSource="rgTaxRealized_OnNeedDataSource" AllowFilteringByColumn="true">
                                                        <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                        <%--     <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                                    Excel-Format="ExcelML">
                                                </ExportSettings>--%>
                                                        <MasterTableView DataKeyNames="MFNPId,AccountId,AMCCode,SchemeCode" ShowFooter="true"
                                                            AutoGenerateColumns="false" CommandItemDisplay="None">
                                                            <%-- <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                                        ShowRefreshButton="false" ShowExportToCsvButton="true" ShowAddNewRecordButton="false" />--%>
                                                            <Columns>
                                                                <telerik:GridButtonColumn HeaderStyle-Width="50px" ButtonType="LinkButton" Text="Select"
                                                                    CommandName="Select">
                                                                </telerik:GridButtonColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" SortExpression="Category" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="Category"
                                                                    HeaderText="Category" DataField="Category" AllowFiltering="true" FooterText="Grand Total:"
                                                                    FooterStyle-HorizontalAlign="Right">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn SortExpression="SubCategoryName" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="SubCategoryName"
                                                                    HeaderText="SubCategory" DataField="SubCategoryName" AllowFiltering="true">
                                                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
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
                                                                <telerik:GridTemplateColumn HeaderStyle-Width="350px" SortExpression="Scheme" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="Schemes"
                                                                    HeaderText="Scheme" Groupable="False" ItemStyle-Wrap="false" AllowFiltering="true"
                                                                    DataField="Scheme">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkScheme" runat="server" Text='<%# Eval("Scheme").ToString() %>'
                                                                            CommandName="NavigateToMarketData"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridBoundColumn SortExpression="FolioNum" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                                    ShowFilterIcon="false" UniqueName="FolioNum" HeaderText="Folio" DataField="FolioNum"
                                                                    AllowFiltering="true">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridDateTimeColumn UniqueName="FolioStartDate" HeaderText="Scheme Invst. Date"
                                                                    AllowFiltering="false" DataField="FolioStartDate" DataFormatString="{0:d}" HtmlEncode="False"
                                                                    AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" SortExpression="FolioStartDate">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                    <FilterTemplate>
                                                                        <telerik:RadDatePicker ID="resolveDateFilter" runat="server">
                                                                        </telerik:RadDatePicker>
                                                                    </FilterTemplate>
                                                                </telerik:GridDateTimeColumn>
                                                                <telerik:GridDateTimeColumn DataField="InvestmentStartDate" SortExpression="InvestmentStartDate"
                                                                    AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" AllowFiltering="false"
                                                                    HeaderText="Holding Start Date" UniqueName="InvestmentStartDate" DataFormatString="{0:d}">
                                                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                                                    <FilterTemplate>
                                                                        <telerik:RadDatePicker ID="InvestmentStartDateFilter" runat="server">
                                                                        </telerik:RadDatePicker>
                                                                    </FilterTemplate>
                                                                </telerik:GridDateTimeColumn>
                                                                <telerik:GridBoundColumn UniqueName="AcquisitionCost" HeaderText="Acquisition Cost"
                                                                    DataField="AcquisitionCost" AllowFiltering="false" FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                                                                    DataFormatString="{0:N0}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="UnitsSold" HeaderText="Units Sold" DataField="UnitsSold"
                                                                    FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N3}" AllowFiltering="false" Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="RedeemedAmount" HeaderText="Redeemed Amount"
                                                                    DataField="RedeemedAmount" AllowFiltering="false" FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                                                                    DataFormatString="{0:N0}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="TotalPL" HeaderText="Total P/L" DataField="TotalPL"
                                                                    AllowFiltering="false" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" Aggregate="Sum">
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
                                                                  <telerik:GridBoundColumn UniqueName="CMFNP_ValuationDate" HeaderText="Valuation As On" DataField="CMFNP_ValuationDate"
                                                                    AllowFiltering="false"  FooterStyle-HorizontalAlign="Right">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings>
                                                            <Resizing AllowColumnResize="true" />
                                                            <Selecting AllowRowSelect="true" />
                                                        </ClientSettings>
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
        <table>
            <tr id="trNote" runat="server"  visible="false">
                <td>
                    <div id="Div3" class="Note">
                        <p>
                            <span style="font-weight: bold">Note:</span><br />                          
                           Schemes with negative balances are highlighted in red to correct the unit balance please check if all the transactions and  corporate actions are uploaded
                        </p>
                    </div>
                </td>
            </tr>
        </table>
        <table id="ErrorMessage" align="center" runat="server">
            <tr>
                <td>
                    <div class="failure-msg" align="center">
                        No Records found.....
                    </div>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdnReturnsRealizedCategory" runat="server" />
        <asp:HiddenField ID="hdnReturnsHoldingsCategory" runat="server" />
        <asp:HiddenField ID="hdnReturnsAllCategory" runat="server" />
        <asp:HiddenField ID="hdnTaxHoldingsCategory" runat="server" />
        <asp:HiddenField ID="hdnTaxRealizedCategory" runat="server" />
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="imgBtnrgTaxRealized" />
        <asp:PostBackTrigger ControlID="imgBtnrgTaxHoldings" />
        <asp:PostBackTrigger ControlID="imgBtnrgRealized" />
        <asp:PostBackTrigger ControlID="imgBtnrgAll" />
        <asp:PostBackTrigger ControlID="imgBtnrgHoldings" />
        <%--<asp:PostBackTrigger ControlID="imgBtnrgHoldings" />--%>
    </Triggers>
</asp:UpdatePanel>
