<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFOnlineUserLandingPage.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.MFOnlineUserLandingPage" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
<div class="divOnlinePageHeading" style="float: right; width: 100%">
    <div style="float: right; padding-right: 100px;">
        <table cellspacing="0" cellpadding="3" width="100%">
            <tr>
                <td align="right" style="width: 10px">
                    <asp:ImageButton Visible="false" ID="btnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                        OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                </td>
            </tr>
        </table>
    </div>
</div>
<div id="divConditional" runat="server" style="padding-top: 4px">
    <table class="TableBackground" cellpadding="2">
        <tr>
            <td id="tdlblRejectReason" runat="server" style="padding-right: 20px">
                <asp:Label runat="server" class="FieldName" Text="AMC:" ID="lblAccount"></asp:Label>
                <asp:DropDownList CssClass="cmbField" ID="ddlAmc" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlAmc_OnSelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td id="td1" runat="server">
                <asp:Label runat="server" class="FieldName" Text="Category:" ID="Label1"></asp:Label>
                <asp:DropDownList CssClass="cmbField" ID="ddlCategory" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td id="td2" runat="server">
                <asp:Label runat="server" class="FieldName" Text="Scheme:" ID="Label2"></asp:Label>
                <asp:DropDownList CssClass="cmbField" ID="ddlScheme" runat="server" AutoPostBack="false">
                </asp:DropDownList>
            </td>
            <td id="tdBtnOrder" runat="server">
                <asp:Button ID="btnschemlanding" runat="server" CssClass="PCGButton" Text="Go" ValidationGroup="btnschemlanding"
                    OnClick="btnschemlanding_Click" />
            </td>
        </tr>
    </table>
</div>
<asp:Panel ID="pnlMFSchemeLanding" runat="server" class="Landscape" Width="100%"
    ScrollBars="Horizontal" Visible="false">
    <table>
        <tr>
            <td>
                <telerik:RadGrid ID="gvMFSchemeLanding" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" ClientSettings-AllowColumnsReorder="true" Width="104%"
                    AllowAutomaticInserts="false" OnNeedDataSource="gvMFSchemeLanding_OnNeedDataSource" OnItemDataBound="gvMFSchemeLanding_OnItemDataBound">
                    <MasterTableView DataKeyNames="PASP_SchemePlanCode" Width="102%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                        CommandItemDisplay="None" AllowFilteringByColumn="true">
                        <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                            ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                        <Columns>
                        <telerik:GridBoundColumn DataField="PASP_SchemePlanCode" AllowFiltering="true" HeaderText="Scheme Code"
                                UniqueName="PASP_SchemePlanCode" SortExpression="PASP_SchemePlanCode" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="70px"
                                FilterControlWidth="30px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PASP_SchemePlanName" AllowFiltering="true" HeaderText="Scheme Name"
                                UniqueName="PASP_SchemePlanName" SortExpression="PASP_SchemePlanName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="180px"
                                FilterControlWidth="120px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PA_AMCName" AllowFiltering="true" HeaderText="Amc"
                                UniqueName="PA_AMCName" SortExpression="PA_AMCName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="180px" FilterControlWidth="120px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PAIC_AssetInstrumentCategoryName" AllowFiltering="true"
                                HeaderText="Category" UniqueName="PAIC_AssetInstrumentCategoryName" SortExpression="PAIC_AssetInstrumentCategoryName"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="95px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PASPD_CutOffTime" AllowFiltering="true" HeaderText="Cut-OffTime"
                                UniqueName="PASPD_CutOffTime" SortExpression="PASPD_CutOffTime" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="90px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Rating" HeaderText="Rating" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="Rating" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="Rating" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="50px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PASPD_IsSIPAvailable" HeaderText="IsSIPAvailable" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="PASPD_IsSIPAvailable" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="PASPD_IsSIPAvailable" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="50px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FundManagerRating" AllowFiltering="false" HeaderText="Fund Manager Rating"
                                UniqueName="FundManagerRating" SortExpression="FundManagerRating" ShowFilterIcon="false"
                                HeaderStyle-Width="80px" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SchemeBenchmark" HeaderText="Scheme & Benchmark Performance"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="SchemeBenchmark"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="SchemeBenchmark" FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="90px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Nav" HeaderText="Last NAV" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="Nav" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="Nav" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="80px">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NavDate" HeaderText="NAV Date" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="NavDate" DataFormatString="{0:d}" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="NavDate"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PASPD_InitialPurchaseAmount" HeaderText="Initial Amount(Regular)"
                                AllowFiltering="false" SortExpression="PASPD_InitialPurchaseAmount" ShowFilterIcon="false"
                                DataFormatString="{0:N2}" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"
                                UniqueName="PASPD_InitialPurchaseAmount" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PASPSD_MinAmount" AllowFiltering="true" HeaderText="Initial Amount(SIP)"
                                DataFormatString="{0:N2}" UniqueName="PASPSD_MinAmount" SortExpression="PASPSD_MinAmount"
                                ShowFilterIcon="false" HeaderStyle-Width="80px" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PASPD_AdditionalPruchaseAmount" AllowFiltering="true"
                                HeaderText="Additional Purchase(Regular)" DataFormatString="{0:N0}" UniqueName="PASPD_AdditionalPruchaseAmount"
                                SortExpression="PASPD_AdditionalPruchaseAmount" ShowFilterIcon="false" HeaderStyle-Width="80px"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PASPSD_MultipleAmount" AllowFiltering="true"
                                HeaderText="Additional Purchase(SIP)" UniqueName="PASPSD_MultipleAmount" SortExpression="PASPSD_MultipleAmount"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="75px" FilterControlWidth="50px" DataFormatString="{0:N2}">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="Recomndation" HeaderText="Recomndation"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="Recomndation"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="Recomndation" FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn ItemStyle-Width="60px" AllowFiltering="false" HeaderText="Action">
                                <ItemTemplate>
                                    <asp:Label ID="lblSIPSchemeFlag" runat="server" CssClass="cmbField" Text='<%# Eval("PASPD_IsSIPAvailable") %>'>
                                    </asp:Label>
                                    <asp:ImageButton ID="imgBuy" runat="server" CommandName="Buy" ImageUrl="~/Images/Buy-Button.png"
                                        ToolTip="BUY" />
                                    &nbsp;
                                    <asp:ImageButton ID="imgSip" runat="server" CommandName="SIP" ImageUrl="~/Images/SIP-Button.png"
                                        ToolTip="SIP" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="hdnScheme" runat="server" />
<asp:HiddenField ID="hdnCategory" runat="server" />
