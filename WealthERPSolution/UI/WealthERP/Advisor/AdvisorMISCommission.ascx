<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="true" CodeBehind="AdvisorMISCommission.ascx.cs"
    Inherits="WealthERP.Advisor.AdvisorMISCommission" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager runat="server">
</telerik:RadStyleSheetManager>
<telerik:RadScriptManager runat="server">
</telerik:RadScriptManager>

<script type="text/javascript" language="javascript">

    function keyPress(sender, args) {
        if (args.keyCode == 13) {
            return false;
        }
    }
</script>

<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Product Mobilization MIS
                        </td>
                        <td align="right">
                            <asp:ImageButton Visible="false" ID="btnCommissionMIS" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnCommissionMIS_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                            <asp:ImageButton Visible="false" ID="imgZoneClusterCommissionMIS" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnZoneCLusterMISCommission_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                            <asp:ImageButton Visible="false" ID="imgMISCommission" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnCategoryWise_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <%--<tr>
        <td colspan="6">
            <asp:Label ID="Label7" runat="server" CssClass="HeaderTextSmall" Text="MF MIS Commission"></asp:Label>
            <hr />
        </td>
    </tr>--%>
    <tr>
        <td colspan="6">
            <table width="100%">
                <tr id="trProduct">
                    <td id="Td1" runat="server">
                        <asp:Label ID="lblProductType" runat="server" Text="Product Type:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td id="Td2" runat="server">
                        <telerik:RadComboBox ID="rcbProductType" AutoPostBack="true" OnSelectedIndexChanged="rcbProductType_SelectedIndexChanged"
                            runat="server" CssClass="cmbFielde" EnableEmbeddedSkins="false" Skin="Telerik"
                            AllowCustomText="true">
                        </telerik:RadComboBox>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="rcbProductType"
                            CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Product type"
                            Operator="NotEqual" ValidationGroup="btnView" ValueToCompare="Select"></asp:CompareValidator>
                    </td>
                    <td id="tdCategory" runat="server" visible="false">
                        <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td id="tdDdlCategory" runat="server" visible="false">
                        <telerik:RadComboBox ID="RcbProductCategory" AutoPostBack="true" OnSelectedIndexChanged="RcbProductCategory_OnSelectedIndexChanged"
                            runat="server" CssClass="cmbFielde" EnableEmbeddedSkins="false" Skin="Telerik"
                            AllowCustomText="true">
                        </telerik:RadComboBox>
                        <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="RcbProductCategory"
                            CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Category type"
                            Operator="NotEqual" ValidationGroup="btnView" ValueToCompare="Select"></asp:CompareValidator>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label1" runat="server" Text="Mode:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rcbMode" AutoPostBack="true" runat="server" CssClass="cmbFielde"
                            EnableEmbeddedSkins="false" Skin="Telerik" AllowCustomText="true" OnSelectedIndexChanged="rcbMode_OnSelectedIndexChanged">
                            <Items>
                                <telerik:RadComboBoxItem Text="Offline" Value="0" Selected="true" />
                                <telerik:RadComboBoxItem Text="Online" Value="1" />
                                <telerik:RadComboBoxItem Text="All" Value="2" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td id="tdOnline" visible="false" runat="server">
                        <asp:Label ID="Label3" runat="server" Text="Online Mode:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td id="tdrcbOnlineMode" visible="false" runat="server">
                        <telerik:RadComboBox ID="rcbOnlineMode" AutoPostBack="true" runat="server" CssClass="cmbFielde"
                            EnableEmbeddedSkins="false" Skin="Telerik" AllowCustomText="true">
                            <Items>
                                <telerik:RadComboBoxItem Text="Demat" Value="1" />
                                <telerik:RadComboBoxItem Text="online" Value="0" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td align="left" id="tdlblIssueName" runat="server" visible="false">
                        <asp:Label ID="lblIssueName" runat="server" CssClass="FieldName" Text="Issue Name"></asp:Label>
                    </td>
                    <td runat="server" id="tdIssueName" runat="server" visible="false">
                        <telerik:RadComboBox ID="rcbIssueName" AutoPostBack="true" runat="server" CssClass="cmbFielde"
                            EnableEmbeddedSkins="false" Skin="Telerik" AllowCustomText="true" Width="250px">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr id="tr1" runat="server">
                    <td id="Td3" runat="server">
                        <asp:Label ID="lblMISType" runat="server" Text="MIS Type:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td runat="server">
                        <telerik:RadComboBox ID="ddlMISType" AutoPostBack="true" OnSelectedIndexChanged="ddlMISType_SelectedIndexChanged"
                            runat="server" CssClass="cmbFielde" EnableEmbeddedSkins="false" Skin="Telerik"
                            AllowCustomText="true">
                            <Items>
                                <telerik:RadComboBoxItem Text="Summary" Value="0" />
                                <telerik:RadComboBoxItem Text="AMC Wise" Value="1" />
                                <telerik:RadComboBoxItem Text="Broke code Wise" Value="2" />
                                <telerik:RadComboBoxItem Text="Branch Wise" Value="3" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblFromDate" Text="From:" runat="server" CssClass="FieldName">
                        </asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="txtFromDate" CssClass="txtField" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                    </td>
                    <td>
                        <asp:Label ID="lblToDate" runat="server" CssClass="FieldName" Text="To:"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="txtToDate" CssClass="txtField" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <asp:CompareValidator ID="CompareValidator1" ControlToCompare="txtFromDate" ControlToValidate="txtToDate"
                            ErrorMessage="To date should be greater than from date" Display="Dynamic" runat="server"
                            CssClass="rfvPCG" Operator="GreaterThanEqual" ValidationGroup="btnView">
                        </asp:CompareValidator>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator CssClass="rfvPCG" ValidationGroup="btnView" Display="Dynamic"
                            runat="server" ID="txtFromDate_RequiredFieldValidator" ControlToValidate="txtFromDate"
                            ErrorMessage="Please Select a from date">
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator CssClass="rfvPCG" ValidationGroup="btnView" Display="Dynamic"
                            runat="server" ID="txtToDate_RequiredFieldValidator" ControlToValidate="txtToDate"
                            ErrorMessage="Please Select a to date">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td colspan="6">
                        <table>
                            <tr onkeypress="return keyPress(this, event)">
                            </tr>
                        </table>
                        <asp:Button ID="btnView" runat="server" CssClass="PCGButton" Text="Go" OnClick="btnView_Click"
                            ValidationGroup="btnView" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trAMCSelection" visible="false" runat="server">
        <td align="left">
            <table width="70%">
                <tr id="trSelectMutualFund" runat="server" align="left">
                    <td align="right">
                        <asp:Label ID="lblSelectMutualFund" runat="server" CssClass="FieldName" Text="Select AMC Code:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSelectMutualFund" runat="server" AutoPostBack="true" CssClass="cmbField"
                            OnSelectedIndexChanged="ddlSelectMutualFund_OnSelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="cvddlSelectMutualFund" runat="server" ControlToValidate="ddlSelectMutualFund"
                            CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select AMC Code" Operator="NotEqual"
                            ValidationGroup="vgbtnSubmit" ValueToCompare="Select AMC Code"></asp:CompareValidator>
                    </td>
                    <td id="tdscheme" runat="server" visible="true">
                        <td align="right">
                            <asp:Label ID="lblSelectSchemeNAV" runat="server" CssClass="FieldName" Text="Select Scheme Name:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSelectSchemeNAV" runat="server" CssClass="cmbFielde">
                                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlSelectSchemeNAV"
                                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select Scheme" Operator="NotEqual"
                                ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
                        </td>
                    </td>
                </tr>
                <tr id="trNavCategory" runat="server" visible="false">
                    <td align="left" class="leftField">
                        <asp:Label ID="lblNAVCategory" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlNAVCategory" runat="server" AutoPostBack="true" CssClass="cmbField"
                            OnSelectedIndexChanged="ddlNAVCategory_OnSelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <%--<tr id="trNavSubCategory" runat="server">
                <td align="right">
                    <asp:Label ID="lblNAVSubCategory" runat="server" CssClass="FieldName" 
                        Text="Sub Category:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlNAVSubCategory" runat="server" AutoPostBack="true" 
                        CssClass="cmbField" 
                        OnSelectedIndexChanged="ddlNAVSubCategory_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>--%>
                <tr id="trSelectSchemeNAV" runat="server">
                </tr>
                <%-- <tr id="trbtnSubmit" runat="server">
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" OnClick="OnClick_Submit"
                                        Text="Submit" ValidationGroup="vgbtnSubmit" />
                                </td>
                            </tr>--%>
            </table>
        </td>
    </tr>
</table>
<table id="tdProductMoblization" width="100%" style="margin-left: 8%">
    <tr>
        <td>
            <telerik:RadGrid ID="gvCommissionMIS" Visible="false" runat="server" GridLines="None"
                AutoGenerateColumns="False" PageSize="10" AllowSorting="true" AllowPaging="True"
                ShowStatusBar="True" ShowFooter="true" EnableViewState="false" Skin="Telerik"
                OnNeedDataSource="gvCommissionMIS_OnNeedDataSource" EnableEmbeddedSkins="false"
                Width="80%" AllowFilteringByColumn="true" AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true">
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                    FileName="MF Mobilization" Excel-Format="ExcelML">
                </ExportSettings>
                <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="none">
                    <%--<CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                        ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="true" />--%>
                        <PagerStyle Mode="NextPrevAndNumeric"   />
                    <Columns>
                        <telerik:GridBoundColumn UniqueName="MT_Type" DataField="MT_Type" AllowFiltering="false"
                            HeaderText="Report Type" SortExpression="MT_Type" AutoPostBackOnFilter="true"
                            ShowFilterIcon="false" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Debt" DataField="Debt" AllowFiltering="false"
                            HeaderText="Debt" SortExpression="Debt" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                            FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Equity" AllowFiltering="false" AutoPostBackOnFilter="true"
                            ShowFilterIcon="false" HeaderText="Equity" FooterStyle-HorizontalAlign="Right"
                            AllowSorting="true" DataField="Equity">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="[Gold_ETF]" AllowFiltering="false" AutoPostBackOnFilter="true"
                            DataField="[Gold ETF]" ShowFilterIcon="false" HeaderText="Gold ETF" FooterStyle-HorizontalAlign="Right"
                            AllowSorting="true">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="[GOLD FUND]" DataField="GOLD FUND" AllowFiltering="false"
                            HeaderText="Gold Fund" SortExpression="[GOLD FUND]" AutoPostBackOnFilter="true"
                            ShowFilterIcon="false">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Index" DataField="Index" AllowFiltering="false"
                            HeaderText="Index" SortExpression="Index" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="LIQUID" DataField="LIQUID" AllowFiltering="false"
                            HeaderText="Liquid" SortExpression="LIQUID" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Total" DataField="Total" AllowFiltering="false"
                            HeaderText="Total" SortExpression="Total" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </td>
    </tr>
</table>
<table runat="server" id="tblCommissionMIS" width="95%" style="margin-left: 8%">
    <tr>
        <td>
            <asp:Panel Visible="false" ID="pnlCommissionMIS" ScrollBars="Horizontal" Width="80%"
                runat="server">
                <div runat="server" id="divCommissionMIS">
                    <telerik:RadGrid ID="gvMISCommission" runat="server" GridLines="None" AutoGenerateColumns="False"
                        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                        Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true"
                        OnNeedDataSource="gvMISCommission_OnNeedDataSource" EnableHeaderContextMenu="true"
                        EnableHeaderContextFilterMenu="true" AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                            FileName="Mobilization Details" Excel-Format="ExcelML">
                        </ExportSettings>
                        <MasterTableView GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                            GroupLoadMode="Client" EditMode="EditForms" ShowGroupFooter="true" Width="100%"
                            AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="" HeaderTooltip="[Type]" DataField="[Type]"
                                    UniqueName="[Type]" SortExpression="[Type]" AllowFiltering="false" AutoPostBackOnFilter="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" HeaderStyle-Width="350px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Amount Raised (Ordered)" HeaderTooltip="[Ordered Amount]"
                                    DataField="[Ordered Amount]" HeaderStyle-Width="250px" UniqueName="[Ordered Amount]"
                                    SortExpression="[Ordered Amount]" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Amount Alloted (Accepted)" HeaderTooltip="[Accepted Amount]"
                                    DataField="[Accepted Amount]" HeaderStyle-Width="120px" UniqueName="[Accepted Amount]"
                                    SortExpression="[Accepted Amount]" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <HeaderStyle Width="150px" />
                        <ClientSettings ReorderColumnsOnClient="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
                <%-- <div runat="server" id="divCommissionMIS" style="margin: 2px; width: 640px;">
                    <telerik:RadGrid Visible="false" ID="gvMISCommission" runat="server" CssClass="RadGrid" GridLines="None"
                        Width="100%" AllowPaging="True" PageSize="15" AllowSorting="True" AutoGenerateColumns="false"
                        ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
                        AllowAutomaticUpdates="false" Skin="Telerik" EnableEmbeddedSkins="false" EnableHeaderContextMenu="true"
                        EnableHeaderContextFilterMenu="true" AllowFilteringByColumn="true" OnNeedDataSource="gvMISCommission_OnNeedDataSource">
                        <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="ExistMFInvestlist">
                        </ExportSettings>
                        <MasterTableView CommandItemDisplay="None">
                            <Columns>
                              <telerik:GridBoundColumn UniqueName="AMCName" HeaderText="AMC" DataField="AMCName" HeaderStyle-Width="108px"
                            SortExpression="AMCName" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                          <telerik:GridBoundColumn UniqueName="SchemeName" HeaderText="Scheme" DataField="SchemeName" HeaderStyle-Width="108px"
                            SortExpression="SchemeName" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TransactionType" HeaderText="Transaction Classification" DataField="TransactionType" HeaderStyle-Width="108px"
                            SortExpression="TransactionType" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                          <telerik:GridBoundColumn UniqueName="Parent" HeaderText="Group Name" DataField="Parent" HeaderStyle-Width="108px"
                            SortExpression="Parent" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CustomerName" HeaderText="Customer" DataField="CustomerName" HeaderStyle-Width="108px"
                            SortExpression="CustomerName" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                          <telerik:GridBoundColumn UniqueName="FolioNum" HeaderText="Folio" DataField="FolioNum" HeaderStyle-Width="108px"
                            SortExpression="FolioNum" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="RM_Name" HeaderText="RM Name" DataField="RM_Name" HeaderStyle-Width="108px"
                            SortExpression="RM_Name" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                          <telerik:GridBoundColumn UniqueName="Brokerage" HeaderText="Brokerage" DataField="Brokerage" HeaderStyle-Width="108px"
                            SortExpression="Brokerage" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="trailFee" HeaderText="trailFee" DataField="trailFee" HeaderStyle-Width="108px"
                            SortExpression="trailFee" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings ReorderColumnsOnClient="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
                            <Scrolling AllowScroll="false" />
                            <Resizing AllowColumnResize="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>--%>
            </asp:Panel>
        </td>
    </tr>
</table>
<table runat="server" id="tblZoneClusterWiseMIS" width="100%">
    <tr>
        <td>
            <asp:Panel ID="pnlZoneClusterWiseMIS" Visible="false" ScrollBars="Horizontal" Height="440px"
                runat="server">
                <div runat="server" id="divZoneClusterWiseMIS" style="margin: 2px; width: 440px;">
                    <telerik:RadGrid Visible="false" ID="gvZoneClusterWiseCommissionMIS" runat="server"
                        GridLines="None" AutoGenerateColumns="False" AllowSorting="true" ShowStatusBar="True"
                        ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                        OnNeedDataSource="gvZoneClusterWiseCommissionMIS_OnNeedDataSource" EnableHeaderContextMenu="true"
                        EnableHeaderContextFilterMenu="true" AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true"
                        OnItemDataBound="gvZoneClusterWiseCommissionMIS_OnItemDataBound">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                            FileName="Zone/Cluster Commission MIS Details" Excel-Format="ExcelML">
                        </ExportSettings>
                        <MasterTableView GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                            GroupLoadMode="Client" EditMode="EditForms" ShowGroupFooter="true" Width="100%"
                            GroupHeaderItemStyle-ForeColor="Black" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                            CommandItemDisplay="None">
                            <GroupByExpressions>
                                <telerik:GridGroupByExpression>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="ZoneName" />
                                    </GroupByFields>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldName="ZoneName" FieldAlias="Zone" />
                                    </SelectFields>
                                </telerik:GridGroupByExpression>
                            </GroupByExpressions>
                            <GroupByExpressions>
                                <telerik:GridGroupByExpression>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="ClusterName" />
                                    </GroupByFields>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldName="ClusterName" FieldAlias="Cluster" />
                                    </SelectFields>
                                </telerik:GridGroupByExpression>
                            </GroupByExpressions>
                            <GroupByExpressions>
                                <telerik:GridGroupByExpression>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="BranchName" />
                                    </GroupByFields>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldName="BranchName" FieldAlias="Branch" />
                                    </SelectFields>
                                </telerik:GridGroupByExpression>
                            </GroupByExpressions>
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="Zone" HeaderTooltip="Zone" DataField="ZoneName"
                                    UniqueName="ZoneName" SortExpression="ZoneName" FooterText="Grand Total:" AllowFiltering="true"
                                    AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" ForeColor="Black" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Cluster" HeaderTooltip="Cluster" DataField="ClusterName"
                                    UniqueName="ClusterName" SortExpression="ClusterName" AutoPostBackOnFilter="true"
                                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" ForeColor="Black" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Branch" HeaderTooltip="Branch Name" DataField="BranchName"
                                    Aggregate="Count" FooterText="Row Count : " UniqueName="BranchName" SortExpression="BranchName"
                                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" ForeColor="Black" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderTooltip="Brokerage Amount" HeaderText="Brokerage Amount"
                                    DataField="Brokerage" HeaderStyle-HorizontalAlign="Right" UniqueName="Brokerage"
                                    SortExpression="Brokerage" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" ForeColor="Black" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderTooltip="Trail Commission" HeaderText="Trail Commission"
                                    DataField="trailFee" HeaderStyle-HorizontalAlign="Right" UniqueName="trailFee"
                                    SortExpression="trailFee" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" ForeColor="Black" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <HeaderStyle Width="150px" />
                        <ClientSettings ReorderColumnsOnClient="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </asp:Panel>
        </td>
    </tr>
</table>
<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
    visible="false">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
            </div>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hidDateType" Value="" runat="server" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnScheme" runat="server" />
<asp:HiddenField ID="hdnAMC" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnMISType" runat="server" />
<asp:HiddenField ID="hdnFromDate" runat="server" />
<asp:HiddenField ID="hdnToDate" runat="server" />
