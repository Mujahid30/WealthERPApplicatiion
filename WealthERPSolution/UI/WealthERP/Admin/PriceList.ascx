<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PriceList.ascx.cs" Inherits="WealthERP.Admin.PriceList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<script language="javascript" type="text/javascript">
    function checkLastDate(sender, args) {

        var fromDateString = document.getElementById('ctrl_PriceList_txtFromDate').value;
        var fromDate = changeDate(fromDateString);
        var toDateString = document.getElementById('ctrl_PriceList_txtToDate').value;
        var toDate = changeDate(toDateString);
        var todayDate = new Date();

        if (Date.parse(toDate) < Date.parse(fromDate)) {
            //sender._selectedDate = todayDate;
            //sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            sender._textbox.set_Value('dd/mm/yyyy');
            alert("Warning! - ToDate cannot be less than the FromDate");
        }
    }
</script>--%>
<%-- <script type="text/javascript">
     //override the onload event handler to change the picker after the page is loaded
     Sys.Application.add_load(setCalendarTable);

     function setCalendarTable() {

         var picker = $find("<%= RadDatePicker1.ClientID %>");
         var calendar = picker.get_calendar();
         var fastNavigation = calendar._getFastNavigation();

         $clearHandlers(picker.get_popupButton());
         picker.get_popupButton().href = "javascript:void(0);";
         $addHandler(picker.get_popupButton(), "click", function() {
             var textbox = picker.get_textBox();
             //adjust where to show the popup table 
             var x, y;
             var adjustElement = textbox;
             if (textbox.style.display == "none")
                 adjustElement = picker.get_popupImage();

             var pos = picker.getElementPosition(adjustElement);
             x = pos.x;
             y = pos.y + adjustElement.offsetHeight;

             var e = {
                 clientX: x,
                 clientY: y - document.documentElement.scrollTop
             };
             //synchronize the input date if set with the picker one
             var date = picker.get_selectedDate();
             if (date) {
                 calendar.get_focusedDate()[0] = date.getFullYear();
                 calendar.get_focusedDate()[1] = date.getMonth() + 1;
             }

             $get(calendar._titleID).onclick(e);

             return false;
         });

         fastNavigation.OnOK =
                    function() {
                        var date = new Date(fastNavigation.Year, fastNavigation.Month, 1);
                        picker.get_dateInput().set_selectedDate(date);
                        fastNavigation.Popup.Hide();
                    }


         fastNavigation.OnToday =
                    function() {
                        var date = new Date();
                        picker.get_dateInput().set_selectedDate(date);
                        fastNavigation.Popup.Hide();
                    }
     }   
        </script>--%>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblheader" runat="server" Class="HeaderTextBig"></asp:Label>
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton ID="imgBtnrgHoldings" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="22px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<div>
    <div>
        <%--<asp:ScriptManager ID="UploadScripManager" runat="server">
        </asp:ScriptManager>--%>
        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" EnableTheming="True" Skin="Telerik"
            EnableEmbeddedSkins="False" MultiPageID="FactsheetMultiPage" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab runat="server" Text="NAV" Value="Price" TabIndex="0" Selected="True">
                </telerik:RadTab>
                <%-- <telerik:RadTab runat="server" Text="Scheme Comparison" Value="Scheme_Comparison"
                    TabIndex="1" Visible="true">
                </telerik:RadTab>
                <telerik:RadTab runat="server" Text="Factsheet" Value="Factsheet" TabIndex="2">
                </telerik:RadTab>--%>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Telerik"
            EnableEmbeddedSkins="false">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadMultiPage ID="FactsheetMultiPage" EnableViewState="true" runat="server"
            SelectedIndex="0">
            <telerik:RadPageView ID="RadPageView1" runat="server">
                &nbsp;&nbsp;&nbsp;&nbsp;
                <%-- <asp:Panel ID="pnlPrice" runat="server">
                    <div id="MainDiv" runat="server">--%>
                <%--<table width="70%">        
                        <tr>--%>
                <div>
                    <table width="100%" class="TableBackground" cellspacing="0" cellpadding="2">
                        <tr>
                            <td>
                                <asp:Label ID="Select" runat="server" Text="Select:" CssClass="FieldName"></asp:Label>
                                &nbsp
                                <asp:RadioButton ID="rbtnCurrent" runat="server" AutoPostBack="true" CssClass="cmbField"
                                    GroupName="Snapshot" OnCheckedChanged="rbtnCurrent_CheckedChanged" Text="Latest" />
                                &nbsp
                                <asp:RadioButton ID="rbtnHistorical" runat="server" AutoPostBack="true" CssClass="cmbField"
                                    GroupName="Snapshot" OnCheckedChanged="rbtnHistorical_CheckedChanged" Text="Historical" />
                            </td>
                            <td id="tdFromDate" align="left" runat="server">
                                <td align="Left">
                                    <asp:Label ID="Label10" Text="From Date:" runat="server" CssClass="FieldName"></asp:Label>
                                </td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="txtFrom" CssClass="txtField" runat="server" Culture="English (United States)"
                                        Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                                        <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                            ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                    <asp:CompareValidator ID="cvChkFutureDate" runat="server" ControlToValidate="txtFrom"
                                        CssClass="cvPCG" Display="Dynamic" ErrorMessage="Date Can't be in future" Operator="LessThanEqual"
                                        Type="Date" ValidationGroup="vgbtnSubmit">
                                    </asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFrom"
                                        CssClass="cvPCG" Display="Dynamic" ErrorMessage="please enter from date" ValidationGroup="vgbtnSubmit"></asp:RequiredFieldValidator>
                                </td>
                            </td>
                            <td id="tdToDate" align="left" runat="server">
                                <td align="left">
                                    <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="To Date:"></asp:Label>
                                </td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="txtTo" CssClass="txtTo" runat="server" Culture="English (United States)"
                                        Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                                        <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                            ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                    <asp:CompareValidator ID="compDateValidator" runat="server" ControlToValidate="txtTo"
                                        CssClass="cvPCG" Display="Dynamic" ErrorMessage="Date Can't be in future" Operator="LessThanEqual"
                                        Type="Date" ValidationGroup="vgbtnSubmit"></asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTo"
                                        CssClass="cvPCG" Display="Dynamic" ErrorMessage="please enter to date" ValidationGroup="vgbtnSubmit"></asp:RequiredFieldValidator>
                                    <br />
                                    <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToCompare="txtFrom"
                                        ControlToValidate="txtTo" CssClass="cvPCG" Display="Dynamic" ErrorMessage="ToDate should be greater than FromDate"
                                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="vgbtnSubmit">
                                    </asp:CompareValidator>
                                </td>
                            </td>
                            <td align="left" style="padding-right: 50px">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" OnClick="OnClick_Submit"
                                    Text="GO" ValidationGroup="vgbtnSubmit" />
                            </td>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblIllegal" runat="server" CssClass="Error" Text="" />
                                </td>
                            </tr>
                        </tr>
                    </table>
                </div>
                <div>
                    <table width="70%">
                        <tr id="trSelectMutualFund" runat="server">
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
                        </tr>
                        <tr id="trNavCategory" runat="server">
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
                            <td align="right">
                                <asp:Label ID="lblSelectSchemeNAV" runat="server" CssClass="FieldName" Text="Select Scheme Name:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSelectSchemeNAV" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlSelectSchemeNAV"
                                    CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select Scheme" Operator="NotEqual"
                                    ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
                            </td>
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
                </div>
                <div id="DivMF" runat="server" style="display: none">
                    <table style="width: 100%">
                        <tr id="trMfPagecount" runat="server">
                            <td class="leftField">
                                <asp:Label ID="lblMFCurrentPage" runat="server" class="Field"></asp:Label>
                                <asp:Label ID="lblMFTotalRows" runat="server" class="Field"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                &#160;
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="Panel1" runat="server" class="Landscape" Width="99%" ScrollBars="Horizontal">
                        <table width="99%">
                            <tr id="trExportFilteredMFRecord" runat="server">
                                <td>
                                    <asp:ImageButton ID="btnExportFilteredMFRecord" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredMFRecord_OnClick"
                                        OnClientClick="setFormat('CSV')" Height="25px" Width="25px"></asp:ImageButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadGrid OnNeedDataSource="gvMFRecord_OnNeedDataSource" ID="gvMFRecord" runat="server"
                                        GridLines="None" AutoGenerateColumns="False" PageSize="10" AllowSorting="true"
                                        AllowPaging="True" ShowStatusBar="True" ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false"
                                        Width="120%" AllowFilteringByColumn="false" AllowAutomaticInserts="false">
                                        <ExportSettings ExportOnlyData="true" HideStructureColumns="true" FileName="MFNavRecordslist">
                                        </ExportSettings>
                                        <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                            CommandItemDisplay="None">
                                            <%-- <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                    ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="true"/>--%>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="SchemePlanCode" AllowFiltering="false" HeaderText="Scheme Plan Code"
                                                    UniqueName="SchemePlanCode">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="SchemePlanName" AllowFiltering="false" HeaderText="Scheme Plan Name"
                                                    UniqueName="SchemePlanName">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PAIC_AssetInstrumentCategoryName" HeaderText="Category"
                                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="Category" ShowFilterIcon="false"
                                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="Category"
                                                    FooterStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PAISC_AssetInstrumentSubCategoryName" HeaderText="Sub Category"
                                                    AllowFiltering="false" HeaderStyle-Wrap="false" SortExpression="PAISC_AssetInstrumentSubCategoryName"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                    UniqueName="PAISC_AssetInstrumentSubCategoryName" FooterStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NetAssetValue" AllowFiltering="false" HeaderText="Net AssetValue"
                                                    UniqueName="NetAssetValue">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="RepurchasePrice" AllowFiltering="false" HeaderText="Repurchase Price"
                                                    UniqueName="RepurchasePrice">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="SalePrice" AllowFiltering="false" HeaderText="Sale Price"
                                                    UniqueName="SalePrice">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataFormatString="{0:d}" DataField="PostDate" AllowFiltering="false"
                                                    HeaderText="NAV Date" UniqueName="NAVDate">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
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
                    </asp:Panel>
                    <%--<table style="width: 100%">
            <tr id="trgrMfView" runat="server">
                <td>
                    <asp:GridView ID="gvMFRecord" runat="server" AutoGenerateColumns="False" 
                        CssClass="GridViewStyle" Font-Size="Small" ShowFooter="true">
                        <RowStyle CssClass="RowStyle" />
                        <FooterStyle CssClass="FooterStyle" />
                        <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <Columns>
                           <asp:BoundField DataField="SchemePlanName" HeaderText="Scheme Plan Name" SortExpression="PASP_SchemePlanName" />
                            <asp:BoundField DataField="SchemePlanCode" HeaderText="Scheme Plan Code" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblName" runat="server" align="center" Text="Scheme Plan Name"></asp:Label>
                                    <br />
                                  <asp:TextBox ID="txtSchemeSearch" runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_AdminPriceList_btnSearch');" />
                                </HeaderTemplate>
                            
                                <ItemTemplate>
                                    <asp:Label ID="lblSchemeName" runat="server" 
                                        Text='<%# Eval("SchemePlanName").ToString() %>'></asp:Label>
                                </ItemTemplate>
                            
                            </asp:TemplateField>
                            <asp:BoundField DataField="NetAssetValue" HeaderText="Net AssetValue" />
                            <asp:BoundField DataField="RepurchasePrice" HeaderText="Repurchase Price" />
                            <asp:BoundField DataField="SalePrice" HeaderText="Sale Price" />
                            <asp:BoundField DataField="PostDate" HeaderText="NAV Date" />
                            <asp:BoundField DataField="Date" HeaderText="Date" />
                        </Columns>
                    
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>--%>
                </div>
                <%--<div id="DivEquity" runat="server" style="display: none">
                        <table style="width: 100%">
                            <tr>
                                <td id="trPageCount" runat="server" class="leftField">
                                    <asp:Label ID="lblCurrentPage" runat="server" class="Field"></asp:Label>
                                    <asp:Label ID="lblTotalRows" runat="server" class="Field"></asp:Label>
                                </td>
                            </tr>
                        </table>--%>
                <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                <div id="DivEquity" style="overflow: auto" visible="false">
                    <table width="100%" cellspacing="0" cellpadding="2">
                        <tr id="trgvEquityView" runat="server" visible="false">
                            <td>
                                <telerik:RadGrid ID="gvEquityRecord" OnNeedDataSource="gvEquityRecord_NeedDataSource"
                                    runat="server" GridLines="None" AutoGenerateColumns="False" EnableLoadOnDemand="false"
                                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" Skin="Telerik"
                                    EnableEmbeddedSkins="false" Width="100px" AllowFilteringByColumn="true" AllowAutomaticInserts="false"
                                    EnableViewState="true" ShowFooter="true">
                                    <%--  OnPreRender="gvWERPTrans_PreRender"--%>
                                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                        FileName="Query Equity Data" Excel-Format="ExcelML">
                                    </ExportSettings>
                                    <MasterTableView TableLayout="Auto" AllowFilteringByColumn="true" AllowMultiColumnSorting="True"
                                        AutoGenerateColumns="false" CommandItemDisplay="None">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="CompanyName" HeaderText="Company Name" AllowFiltering="True"
                                                HeaderStyle-Width="160px" FilterControlWidth="145px" UniqueName="CompanyName"
                                                SortExpression="CompanyName" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                                                CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Exchange" AllowFiltering="false" HeaderText="Exchange"
                                                AllowSorting="true" HeaderStyle-Width="80px" UniqueName="Exchange" SortExpression="Exchange"
                                                AutoPostBackOnFilter="true" ShowFilterIcon="false">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Series" AllowFiltering="false" HeaderText="Series"
                                                AllowSorting="true" HeaderStyle-Width="60px" UniqueName="Series" SortExpression="Series"
                                                AutoPostBackOnFilter="true" ShowFilterIcon="false">
                                                <ItemStyle Width="8px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="OpenPrice" AllowFiltering="false" HeaderText="Open Price"
                                                AllowSorting="true" HeaderStyle-Width="80px" UniqueName="OpenPrice" SortExpression="OpenPrice"
                                                AutoPostBackOnFilter="true" ShowFilterIcon="false" DataFormatString="{0:n3}">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="HighPrice" AllowFiltering="false" HeaderText="High Price"
                                                AllowSorting="true" HeaderStyle-Width="80px" UniqueName="HighPrice" SortExpression="HighPrice"
                                                AutoPostBackOnFilter="true" ShowFilterIcon="false" DataFormatString="{0:n3}">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="LowPrice" AllowFiltering="false" HeaderText="Low Price"
                                                AllowSorting="true" HeaderStyle-Width="80px" UniqueName="LowPrice" SortExpression="LowPrice"
                                                AutoPostBackOnFilter="true" ShowFilterIcon="false" DataFormatString="{0:n3}">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ClosePrice" AllowFiltering="false" HeaderText="Close Price"
                                                HeaderStyle-Width="80px" UniqueName="ClosePrice" SortExpression="ClosePrice"
                                                AllowSorting="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataFormatString="{0:n3}">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="LastPrice" AllowFiltering="false" HeaderText="Last Price"
                                                AllowSorting="true" HeaderStyle-Width="80px" UniqueName="LastPrice" SortExpression="LastPrice"
                                                AutoPostBackOnFilter="true" ShowFilterIcon="false" DataFormatString="{0:n3}">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PreviousClose" AllowFiltering="false" HeaderText="Previous Close"
                                                HeaderStyle-Width="80px" UniqueName="PreviousClose" SortExpression="PreviousClose"
                                                AllowSorting="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataFormatString="{0:n3}">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TotalTradeQuantity" AllowFiltering="false" HeaderText="Total Trade Quantity"
                                                HeaderStyle-Width="100px" UniqueName="TotalTradeQuantity" SortExpression="TotalTradeQuantity"
                                                AllowSorting="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataFormatString="{0:n0}">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TotalTradeValue" AllowFiltering="false" HeaderText="Total Trade Value"
                                                HeaderStyle-Width="100px" UniqueName="TotalTradeValue" SortExpression="TotalTradeValue"
                                                AllowSorting="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataFormatString="{0:n3}">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NoOfTrades" AllowFiltering="false" HeaderText="No.of Trades"
                                                HeaderStyle-Width="90px" UniqueName="NoOfTrades" SortExpression="NoOfTrades"
                                                AllowSorting="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataFormatString="{0:n0}">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Date" HeaderText="Date" AllowFiltering="false"
                                                AllowSorting="true" DataFormatString="{0:d}" HeaderStyle-Width="80px" UniqueName="Date"
                                                SortExpression="Date" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Resizing AllowColumnResize="True" />
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                                <%-- <asp:GridView ID="gvEquityRecord" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                        Font-Size="Small" ShowFooter="true">
                                        <RowStyle CssClass="RowStyle" />
                                        <FooterStyle CssClass="FooterStyle" />
                                        <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                        <AlternatingRowStyle CssClass="AltRowStyle" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblEquityName" runat="server" align="center" Text="Company Name"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtCompanySearch" runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_AdminPriceList_btnSearch');" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:BoundField DataField="CompanyName" HeaderText="Company Name" SortExpression="PEM_CompanyName" />--%>
                                <%--  <asp:BoundField DataField="Exchange" HeaderText="Exchange" />
                                            <asp:BoundField DataField="Series" HeaderText="Series" />
                                            <asp:BoundField DataField="OpenPrice" HeaderText="Open Price" />
                                            <asp:BoundField DataField="HighPrice" HeaderText="High Price" />
                                            <asp:BoundField DataField="LowPrice" HeaderText="Low Price" />
                                            <asp:BoundField DataField="ClosePrice" HeaderText="Close Price" />
                                            <asp:BoundField DataField="LastPrice" HeaderText="Last Price" />
                                            <asp:BoundField DataField="PreviousClose" HeaderText="Previous Close" />
                                            <asp:BoundField DataField="TotalTradeQuantity" HeaderText="Total Trade Quantity" />
                                            <asp:BoundField DataField="TotalTradeValue" HeaderText="Total Trade Value" />
                                            <asp:BoundField DataField="NoOfTrades" HeaderText="No Of Trades" />
                                            <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:d}"/>
                                        </Columns>
                                    </asp:GridView>--%>
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
                <%-- <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>--%>
                <div id="DivPager" runat="server" style="display: none">
                    <table style="width: 100%">
                        <tr id="trPager" runat="server">
                            <td>
                                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
                            </td>
                        </tr>
                    </table>
                </div>
                <%--</asp:Panel> &nbsp;&nbsp;&nbsp;--%>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView3" runat="server">
                &nbsp;&nbsp;&nbsp;
                <asp:Panel ID="pnlSchemeComparison" runat="server">
                    <table class="TableBackground" width="100%">
                        <%-- <tr>
                            <td>
                                <asp:Label ID="lblFundPerformance" runat="server" CssClass="HeaderText" Text="Fund Performance"></asp:Label>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td class="leftField">
                                            <asp:Label ID="lblReturn" runat="server" CssClass="FieldName" Text="Return:">                                
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlReturn" runat="server" CssClass="cmbField">
                                                <asp:ListItem Text="Choose return Period" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="1 Week" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="1 Month" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="3 Months" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="6 Months" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="1 Year" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="2 Years" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="3 Years" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="5 Years" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="Since Launch" Value="9"></asp:ListItem>
                                            </asp:DropDownList>
                                            <span id="Span3" class="spnRequiredField">*</span>
                                            <asp:CompareValidator ID="ddlReturn_CompareValidator" runat="server" ControlToValidate="ddlReturn"
                                                CssClass="cvPCG" ErrorMessage="Please select a Return" Operator="NotEqual" ValidationGroup="btnGo"
                                                ValueToCompare="0">
                                            </asp:CompareValidator>
                                        </td>
                                        <td class="leftField">
                                            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Condition:">
                                            </asp:Label>
                                        </td>
                                        <td class="rightField">
                                            <asp:DropDownList ID="ddlCondition" runat="server" CssClass="cmbField">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Above 30%" Value="&gt;30"></asp:ListItem>
                                                <asp:ListItem Text="Above 20%" Value="&gt;20"></asp:ListItem>
                                                <asp:ListItem Text="Above 10%" Value="&gt;10"></asp:ListItem>
                                                <asp:ListItem Text="Above 5%" Value="&gt;5"></asp:ListItem>
                                                <asp:ListItem Text="Gainer(+ve return)" Value="&gt;0"></asp:ListItem>
                                                <asp:ListItem Text="Loser(-ve return)" Value="&lt;0"></asp:ListItem>
                                                <asp:ListItem Text="Less than 5%" Value="&lt;-5"></asp:ListItem>
                                                <asp:ListItem Text="Less than 10%" Value="&lt;-10"></asp:ListItem>
                                                <asp:ListItem Text="Less than 20%" Value="&lt;-20"></asp:ListItem>
                                                <asp:ListItem Text="Less than 30%" Value="&lt;-30"></asp:ListItem>
                                            </asp:DropDownList>
                                            <span id="Span2" class="spnRequiredField">*</span>
                                            <asp:CompareValidator ID="ddlCondition_CompareValidator" runat="server" ControlToValidate="ddlCondition"
                                                CssClass="cvPCG" ErrorMessage="Please select a Condition" Operator="NotEqual"
                                                ValidationGroup="btnGo" ValueToCompare="0">
                                            </asp:CompareValidator>
                                        </td>
                                        <td class="leftField">
                                            <asp:Label ID="lblSelectAMC" runat="server" CssClass="FieldName" Text="AMC:">
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSelectAMC" runat="server" CssClass="cmbField">
                                            </asp:DropDownList>
                                            <span id="Span4" class="spnRequiredField">*</span>
                                            <asp:CompareValidator ID="ddlAMC_CompareValidator" runat="server" ControlToValidate="ddlSelectAMC"
                                                CssClass="cvPCG" ErrorMessage="Please select an AMC" Operator="NotEqual" ValidationGroup="btnGo"
                                                ValueToCompare="Select AMC Code">
                                            </asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                                       
                        </tr>--%>
                                    <%-- <tr>
                            
                        </tr>--%>
                                    <%--<tr>
                            <td class="leftField">
                                <asp:Label ID="lblSelectScheme" runat="server" CssClass="FieldName" Text="Scheme:">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList CssClass="cmbField" ID="ddlSelectScheme" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>--%>
                                    <tr>
                                        <td class="leftField">
                                            <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Category:">
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField">
                                            </asp:DropDownList>
                                            <%--<span id="Span1" class="spnRequiredField">*</span>
                                <asp:CompareValidator ID="ddlCategory_CompareValidator" runat="server"
                                    ControlToValidate="ddlCategory" ErrorMessage="Please select a Category"
                                    Operator="NotEqual" ValueToCompare="0" CssClass="cvPCG" ValidationGroup="btnGo">
                                </asp:CompareValidator>--%>
                                        </td>
                                        <%-- <div id="divSubCategory" runat="server" visible="false">
                            <td class="leftField">
                                <asp:Label ID="lblSubCategory" runat="server" CssClass="FieldName" 
                                    Text="SubCategory:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="cmbField">
                                </asp:DropDownList>                                
                            </td>
                            </div>--%>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnGo" runat="server" CssClass="PCGButton" OnClick="OnClick_btnGo"
                                                Text="Go" ValidationGroup="btnGo" />
                                        </td>
                                    </tr>
                                    <%--   <tr>
                            
                        </tr>--%>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <%--<table  style="width: 100%;">
            <tr>
                <td>
                    <table id="ErrorMessage" align="center" runat="server" visible="false">
                        <tr>
                            <td>
                                <div class="failure-msg" align="center">
                                    No Records found ...
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>--%>
                    <%--<table>
            <tr>
                <td>--%>
                    <%--  <div 
            style="overflow-x:auto;overflow-y:hidden;width:100%;padding: 0 0 20px 0">--%>
                    <asp:Panel ID="Panel2" runat="server" class="Landscape" Width="99%" ScrollBars="Horizontal"
                        Visible="false">
                        <tr id="trMFFundPerformance" runat="server">
                            <td>
                                <asp:ImageButton ID="btnMFFundPerformance" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportMFFundPerformance_OnClick"
                                    OnClientClick="setFormat('CSV')" Height="25px" Width="25px"></asp:ImageButton>
                            </td>
                        </tr>
                        <telerik:RadGrid AllowFilteringByColumn="true" ID="gvMFFundPerformance" runat="server"
                            AllowAutomaticInserts="false" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            EnableEmbeddedSkins="false" OnItemCommand="RadGrid1_ItemCommand" GridLines="None"
                            PageSize="10" ShowFooter="true" ShowStatusBar="True" Skin="Telerik" Width="100%"
                            OnNeedDataSource="gvMFFundPerformance_OnNeedDataSource">
                            <%--<PagerStyle Mode="NumericPages"></PagerStyle>--%>
                            <MasterTableView AllowMultiColumnSorting="true" AutoGenerateColumns="false" CommandItemDisplay="none"
                                Width="99%">
                                <%-- <ExportSettings HideStructureColumns="true" />--%>
                                <%--<CommandItemTemplate>
                                <table class="rcCommandTable" width="100%">
                                    <td>
                                        <asp:Button ID="Button1" runat="server" Text=" " ToolTip="Export To CSV" CssClass="rgExpCSV"
                                            CommandName="ExportToCSV" />
                                        <asp:Button ID="Button2" runat="server" Text=" " ToolTip="Export To Excel" CssClass="rgExpXLS"
                                            CommandName="ExportToExcel" />
                                    </td>
                                </table>
                            </CommandItemTemplate>--%>
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                    ShowExportToCsvButton="true" />
                                <Columns>
                                    <telerik:GridBoundColumn AndCurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        AllowFiltering="true" SortExpression="SchemeName" AutoPostBackOnFilter="true"
                                        DataField="SchemeName" HeaderText="Scheme Name" UniqueName="SchemeName">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn  DataField="AUM"  HeaderText="AUM" UniqueName="AUM" >
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>--%>
                                    <telerik:GridBoundColumn AllowFiltering="false" DataField="LaunchDate" DataFormatString="{0:d}"
                                        HeaderText="Launch Date" UniqueName="LaunchDate">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" DataField="ClosingDate" DataFormatString="{0:d}"
                                        HeaderText="As On Date" UniqueName="ClosingDate">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" DataField="NAV" DataFormatString="{0:0.0000}"
                                        HeaderText="Current NAV" UniqueName="NAV">
                                        <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" DataField="OneYearHighNAV" DataFormatString="{0:0.0000}"
                                        HeaderText="52 Weeks Highest NAV" UniqueName="OneYearHighNAV">
                                        <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" DataField="OneYearLowNAV" DataFormatString="{0:0.0000}"
                                        HeaderText="52 Weeks Lowest NAV" UniqueName="OneYearLowNAV">
                                        <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn  DataField="YTD"  HeaderText="YTD" UniqueName="YTD" >
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>--%>
                                    <telerik:GridBoundColumn AllowFiltering="false" DataField="OneWeekReturn" DataFormatString="{0:0.00}"
                                        HeaderText="1 Week Return(%)" UniqueName="OneWeekReturn">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" DataField="OneMonthReturn" DataFormatString="{0:0.00}"
                                        HeaderText="1 Month Return(%)" UniqueName="OneMonthReturn">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" DataField="ThreeMonthReturn" DataFormatString="{0:0.00}"
                                        HeaderText="3 Months Return(%)" UniqueName="ThreeMonthReturn">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" DataField="SixMonthReturn" DataFormatString="{0:0.00}"
                                        HeaderText="6 Months Return(%)" UniqueName="SixMonthReturn">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" DataField="OneYearReturn" DataFormatString="{0:0.00}"
                                        HeaderText="1 Year Return(%)" UniqueName="OneYearReturn">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" DataField="TwoYearReturn" DataFormatString="{0:0.00}"
                                        HeaderText="2 Years Return(%)" UniqueName="TwoYearReturn">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" DataField="ThreeYearReturn" DataFormatString="{0:0.00}"
                                        HeaderText="3 Years Return(%)" UniqueName="ThreeYearReturn">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" DataField="FiveYearReturn" DataFormatString="{0:0.00}"
                                        HeaderText="5 Years Return(%)" UniqueName="FiveYearReturn">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" DataField="InceptionReturn" DataFormatString="{0:0.00}"
                                        HeaderText="Inception Ret." UniqueName="InceptionReturn">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" DataField="PE" DataFormatString="{0:0.00}"
                                        HeaderText="PE" UniqueName="PE">
                                        <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" DataField="PB" DataFormatString="{0:0.00}"
                                        HeaderText="PB" UniqueName="PB">
                                        <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <%-- <telerik:GridBoundColumn  DataField="Cash"  HeaderText="Cash %" UniqueName="Cash" >
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>--%>
                                    <telerik:GridBoundColumn AllowFiltering="false" DataField="Sharpe" DataFormatString="{0:0.00}"
                                        HeaderText="Sharpe" UniqueName="Sharpe">
                                        <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" DataField="SD" DataFormatString="{0:0.00}"
                                        HeaderText="SD" UniqueName="SD">
                                        <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <%-- <telerik:GridBoundColumn  DataField="Top5Holdings"  HeaderText="Top 5 Holdings" UniqueName="Top5Holdings" >
                            <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>--%>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings>
                                <%--<Scrolling AllowScroll="false" UseStaticHeaders="True" SaveScrollPosition="true">
                        </Scrolling>--%>
                                <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                <%-- <Resizing AllowColumnResize="True"></Resizing>--%>
                            </ClientSettings>
                        </telerik:RadGrid>
                    </asp:Panel>
                    <%--</div>--%>
                    <%--</td>
            </tr>
        </table>--%></asp:Panel>
                &nbsp;&nbsp;&nbsp;
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView2" runat="server">
                &nbsp;&nbsp;&nbsp;
                <asp:Panel ID="pnlFactSheet" runat="server">
                    <table class="TableBackground" width="100%">
                        <tr>
                            <td>
                                <table align="Left" width="80%">
                                    <tr>
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2" width="40%">
                                            <asp:Label ID="lblAmcCode" runat="server" CssClass="FieldName" Text="AMC:"></asp:Label>&nbsp;&nbsp;&nbsp;
                                            <asp:DropDownList ID="ddlAmcCode" runat="server" AutoPostBack="true" CssClass="cmbField"
                                                OnSelectedIndexChanged="ddlAmcCode_SelectedIndexChanged" Style="width: 350px;">
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlAmcCode"
                                                CssClass="cvPCG" Display="Dynamic" ErrorMessage="&lt;br /&gt;Please Select AMC Code"
                                                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
                                        </td>
                                        <td align="left" colspan="2" width="60%">
                                            <asp:Label ID="lblFactCategory" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>&nbsp;&nbsp;
                                            <asp:DropDownList ID="ddlFactCategory" runat="server" AutoPostBack="true" CssClass="cmbField"
                                                OnSelectedIndexChanged="ddlFactCategory_SelectedIndexChanged" Style="width: 350px;">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2" width="40%">
                                            <asp:Label ID="lblSchemeList" runat="server" CssClass="FieldName" Text="Scheme:"></asp:Label>
                                            <asp:DropDownList ID="ddlSchemeList" runat="server" CssClass="cmbField" Style="width: 350px;">
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlSchemeList"
                                                CssClass="cvPCG" Display="Dynamic" ErrorMessage="&lt;br /&gt;Please Select Scheme"
                                                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
                                        </td>
                                        <td align="left" colspan="2" width="60%">
                                            <asp:Label ID="lblYear" runat="server" CssClass="FieldName" Text="Month &amp; Year:">
                                            </asp:Label>
                                            <asp:DropDownList ID="ddYear" runat="server" CssClass="cmbField">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddMonth" runat="server" CssClass="cmbField">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" align="left" width="20%">
                                            <asp:Button ID="btnViewFactsheet" runat="server" CssClass="PCGMediumButton" OnClick="btnViewFactsheet_Click"
                                                Text="View Factsheet" ValidationGroup="MFSubmit" />
                                        </td>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table id="tblFactSheet" runat="server" align="Left" width="80%">
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%">
                                            <asp:Label ID="lblFactSheetHeading" runat="server" CssClass="HeaderTextSmall" Text="FactSheet for "></asp:Label>
                                            <asp:Label ID="lblGetScheme" runat="server" CssClass="HeaderTextSmall" Text=" "></asp:Label>
                                        </td>
                                        <td width="10%">
                                        </td>
                                        <td align="left" width="30%">
                                            <asp:Label ID="lblMonth" runat="server" CssClass="HeaderTextSmall" Text="Month: "></asp:Label><asp:Label
                                                ID="lblGetMonth" runat="server" CssClass="HeaderTextSmall" Text=" "></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%">
                                            <asp:Label ID="lblObjective" runat="server" CssClass="HeaderTextSmall" Text="Fund Objective"></asp:Label>
                                        </td>
                                        <td width="10%">
                                        </td>
                                        <td width="30%">
                                            <asp:Label ID="lblInvestInfo" runat="server" CssClass="HeaderTextSmall" Text="Investment Information"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%" valign="top">
                                            <table id="tblFundObject" runat="server" width="100%">
                                                <tr id="tr3" runat="server">
                                                    <td align="center" style="background-color: White; color: Black;">
                                                        <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="No Records Found!"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblFactFundObject" runat="server" border="1" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblObjPara" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="10%">
                                        </td>
                                        <td width="30%" valign="top">
                                            <table id="tblInvInformation" runat="server" width="100%">
                                                <tr id="tr4" runat="server">
                                                    <td align="center" style="background-color: White; color: Black;">
                                                        <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="No Records Found!"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblFactInvInformation" runat="server" border="1" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblSchemeType" runat="server" CssClass="FieldName" Text="Scheme Type:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblgetSchemeType" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblLaunchDate" runat="server" CssClass="FieldName" Text="Launch date:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblgetlunchDate" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFundMgr" runat="server" CssClass="FieldName" Text="Fund Manager: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblGetFundMgr" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblBenchMark" runat="server" CssClass="FieldName" Text="Benchmark:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblgetBenchMark" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%">
                                            <asp:Label ID="lblFHDet" runat="server" CssClass="HeaderTextSmall" Text="Fund House Details"></asp:Label>
                                        </td>
                                        <td width="10%">
                                        </td>
                                        <td width="30%">
                                            <asp:Label ID="lblFundstr" runat="server" CssClass="HeaderTextSmall" Text="Fund Structure"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" width="30%">
                                            <table id="tblFundHouseDetails" runat="server" width="100%">
                                                <tr id="tr5" runat="server">
                                                    <td align="center" style="background-color: White; color: Black;">
                                                        <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="No Records Found!"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblFactFundHouseDetails" runat="server" border="1" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblAMC" runat="server" CssClass="FieldName" Text="AMC : "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblgetAMC" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblAddress" runat="server" CssClass="FieldName" Text="Addres: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblgetAddress" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblWebsite" runat="server" CssClass="FieldName" Text="Website: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblgetWebsite" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="10%">
                                        </td>
                                        <td valign="top" width="30%">
                                            <table id="tblFundStructure" runat="server" width="100%">
                                                <tr id="tr6" runat="server">
                                                    <td align="center" style="background-color: White; color: Black;">
                                                        <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="No Records Found!"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblFactFundStructure" runat="server" border="1" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblPERatio" runat="server" CssClass="FieldName" Text="P/E Ratio:"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblgetPERatio" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblPBRatio" runat="server" CssClass="FieldName" Text="P/B Ratio:"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblgetPBRatio" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblAvgMkt" runat="server" CssClass="FieldName" Text="Avg Market Cap(Rs):"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblgetAvgMkt" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%">
                                            <asp:Label ID="lblFinacialDetail" runat="server" CssClass="HeaderTextSmall" Text="Financial Details"></asp:Label>
                                        </td>
                                        <td width="10%">
                                        </td>
                                        <td valign="top" width="30%">
                                            <asp:Label ID="lblPerformance" runat="server" CssClass="HeaderTextSmall" Text="Scheme Performance"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" width="30%">
                                            <table id="tblFinancialDetails" runat="server" width="100%">
                                                <tr id="tr7" runat="server">
                                                    <td align="center" style="background-color: White; color: Black;">
                                                        <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="No Records Found!"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblFactFinancialDetails" runat="server" border="1" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="50%">
                                                        <asp:Label ID="lblAUM" runat="server" CssClass="FieldName" Text="AUM"></asp:Label>
                                                    </td>
                                                    <td align="right" width="50%">
                                                        <asp:Label ID="lblgetAUM" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        <asp:Label ID="lblNAV" runat="server" CssClass="FieldName" Text="NAV"></asp:Label>
                                                    </td>
                                                    <td align="right" width="50%">
                                                        <asp:Label ID="lblgetNAV" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        <asp:Label ID="lblMinInvestment" runat="server" CssClass="FieldName" Text="Min Investment"></asp:Label>
                                                    </td>
                                                    <td align="right" width="50%">
                                                        <asp:Label ID="lblgetMinInvestment" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        <asp:Label ID="lblNAV52high" runat="server" CssClass="FieldName" Text="NAV(52 week high)"></asp:Label>
                                                    </td>
                                                    <td align="right" width="50%">
                                                        <asp:Label ID="lblgetNAV52high" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        <asp:Label ID="lblNAV52Low" runat="server" CssClass="FieldName" Text="NAV (52 week Low)"></asp:Label>
                                                    </td>
                                                    <td align="right" width="50%">
                                                        <asp:Label ID="lblgetNAV52Low" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="10%">
                                        </td>
                                        <td valign="top" width="30%">
                                            <table id="tblmsgSchemePerformance" runat="server" width="100%">
                                                <tr id="tr1" runat="server">
                                                    <td align="center" style="background-color: White; color: Black;">
                                                        <asp:Label ID="Label9" runat="server" CssClass="FieldName" Text="No Records Found!"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblFactSchemePerformance" runat="server" border="1" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="center" width="50%">
                                                        <asp:Label ID="lblPeriod" runat="server" CssClass="FieldName" Text="Period"></asp:Label>
                                                    </td>
                                                    <td align="center" width="50%">
                                                        <asp:Label ID="lblReturns" runat="server" CssClass="FieldName" Text="Returns"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        <asp:Label ID="lbl3Month" runat="server" CssClass="FieldName" Text="3Months"></asp:Label>
                                                    </td>
                                                    <td align="right" width="50%">
                                                        <asp:Label ID="lblGet3Month" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        <asp:Label ID="lbl6Month" runat="server" CssClass="FieldName" Text="6Months"></asp:Label>
                                                    </td>
                                                    <td align="right" width="50%">
                                                        <asp:Label ID="lblGet6Month" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        <asp:Label ID="lbl1year" runat="server" CssClass="FieldName" Text="1Year"></asp:Label>
                                                    </td>
                                                    <td align="right" width="50%">
                                                        <asp:Label ID="lblGet1year" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        <asp:Label ID="lbl3Years" runat="server" CssClass="FieldName" Text="3Years"></asp:Label>
                                                    </td>
                                                    <td align="right" width="50%">
                                                        <asp:Label ID="lblGet3Years" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        <asp:Label ID="lbl5Years" runat="server" CssClass="FieldName" Text="5Years"></asp:Label>
                                                    </td>
                                                    <td align="right" width="50%">
                                                        <asp:Label ID="lblGet5Years" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="50%">
                                                        <asp:Label ID="lblSinceInception" runat="server" CssClass="FieldName" Text="Since Inception"></asp:Label>
                                                    </td>
                                                    <td align="right" width="50%">
                                                        <asp:Label ID="lblGetSinceInception" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%">
                                            <asp:Label ID="lblgetFinacialDetail" runat="server" CssClass="HeaderTextSmall" Text="Volatality Measures"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" width="30%">
                                            <table id="tblVolatality" runat="server" width="100%">
                                                <tr id="tr8" runat="server">
                                                    <td align="center" style="background-color: White; color: Black;">
                                                        <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="No Records Found!"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblFactVolatality" runat="server" border="1" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFama" runat="server" CssClass="FieldName" Text="Fama"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblgetFama" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstdDev" runat="server" CssClass="FieldName" Text="Std Deviation"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblgetstdDev" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblBeta" runat="server" CssClass="FieldName" Text="Beta"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblgetBeta" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSharpe" runat="server" CssClass="FieldName" Text="Sharpe"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblgetSharpe" runat="server" CssClass="FieldName" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%">
                                            <asp:Label ID="lblCompany" runat="server" CssClass="HeaderTextSmall" Text="Top 10 Companies"></asp:Label>
                                        </td>
                                        <td width="10%">
                                        </td>
                                        <td width="30%">
                                            <asp:Label ID="lblSector" runat="server" CssClass="HeaderTextSmall" Text="Top 10 Sector wise holdings"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" width="30%">
                                            <table width="100%">
                                                <tr id="trTop10Company" runat="server">
                                                    <td align="center" style="background-color: White; color: Black;">
                                                        <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="No Records Found!"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="gvTop10Companies" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                            CellPadding="4" CssClass="GridViewStyle" HeaderStyle-Width="90%" ShowFooter="true">
                                                            <RowStyle CssClass="RowStyle" />
                                                            <FooterStyle CssClass="FooterStyle" />
                                                            <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                            <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            <EditRowStyle CssClass="EditRowStyle" />
                                                            <AlternatingRowStyle CssClass="AltRowStyle " />
                                                            <Columns>
                                                                <asp:BoundField DataField="PEM_CompanyName" HeaderStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-Width="20%" HeaderText="Name" />
                                                                <asp:BoundField DataField="PASPO_HoldPercentage" DataFormatString="{0:n2}" HeaderStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-Width="20%" HeaderText="%" ItemStyle-HorizontalAlign="Center" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="10%">
                                        </td>
                                        <td valign="top" width="30%">
                                            <table width="100%">
                                                <tr id="trSector" runat="server">
                                                    <td align="center" style="background-color: White; color: Black;">
                                                        <asp:Label ID="lblmsgAbsRetn" runat="server" CssClass="FieldName" Text="No Records Found!"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="gvSectorWiseHolding" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                            CellPadding="4" CssClass="GridViewStyle" HeaderStyle-Width="90%" ShowFooter="true">
                                                            <RowStyle CssClass="RowStyle" />
                                                            <FooterStyle CssClass="FooterStyle" />
                                                            <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                            <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            <EditRowStyle CssClass="EditRowStyle" />
                                                            <AlternatingRowStyle CssClass="AltRowStyle " />
                                                            <Columns>
                                                                <asp:BoundField DataField="Sector" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="20%"
                                                                    HeaderText="Name" />
                                                                <asp:BoundField DataField="HoldPercen" DataFormatString="{0:n2}" HeaderStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-Width="20%" HeaderText="%" ItemStyle-HorizontalAlign="Center" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <%-- <asp:UpdatePanel ID="updatePanel" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>--%>
                                <%--<asp:GridView ID="gvCustomers" runat="server" AllowPaging="true" AllowSorting="true"
                                    PageSize="20" Width="95%">
                                    <AlternatingRowStyle BackColor="aliceBlue" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:GridView>--%>
                                <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                &nbsp;&nbsp;&nbsp;
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
    <%--<table width="100%">
    </table>--%>
    <asp:HiddenField ID="hdnFromDate" runat="server" />
    <asp:HiddenField ID="hdnToDate" runat="server" />
    <asp:HiddenField ID="hdnMFCount" runat="server" />
    <asp:HiddenField ID="hdnAssetGroup" runat="server" />
    <asp:HiddenField ID="hdnEquityCount" runat="server" />
    <asp:HiddenField ID="hdnSchemeSearch" runat="server" />
    <asp:HiddenField ID="hdnCurrentPage" runat="server" />
    <asp:HiddenField ID="hdnCompanySearch" runat="server" />
    <asp:HiddenField ID="hdnSort" runat="server" />
    <asp:HiddenField ID="hdnAmcCode" runat="server" />
    <asp:HiddenField ID="hdnSubCategory" runat="server" />
    <asp:HiddenField ID="hdnCondition" runat="server" />
    <asp:HiddenField ID="hdnReturnPeriod" runat="server" />
    <asp:Button ID="btnSearch" runat="server" Text="" OnClick="btnSearch_Click" BorderStyle="None"
        BackColor="Transparent" />
    <asp:HiddenField ID="hdnassetType" runat="server" />
</div>
<%--<table>
    <tr>
        <td>
            <telerik:RadGrid ID="gvNAVPriceList" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true" 
                    AllowAutomaticInserts="false">
                   
                    <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="Top">
                     <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                    ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="true"/>
                      <Columns>                         
                        <telerik:GridBoundColumn DataField="SchemePlanCode" AllowFiltering="false"  HeaderText="Scheme Plan Code" UniqueName="SchemePlanCode" >
                            <ItemStyle  Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />                            
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SchemePlanName" AllowFiltering="false"  HeaderText="Scheme Plan Name" UniqueName="SchemePlanName" >
                            <ItemStyle  Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />                            
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="NetAssetValue" AllowFiltering="false"  HeaderText="Net AssetValue" UniqueName="NetAssetValue" >
                            <ItemStyle  Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />                            
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RepurchasePrice" AllowFiltering="false"  HeaderText="Repurchase Price" UniqueName="RepurchasePrice" >
                            <ItemStyle  Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />                            
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SalePrice" AllowFiltering="false"  HeaderText="Sale Price" UniqueName="SalePrice" >
                            <ItemStyle  Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />                            
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PostDate" AllowFiltering="false"  HeaderText="NAV Date" UniqueName="NAVDate" >
                            <ItemStyle  Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />                            
                        </telerik:GridBoundColumn>  
                    </Columns>
                    </MasterTableView>
                    <ClientSettings>                       
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                    </telerik:RadGrid>
        </td>
    </tr>
</table>--%>
