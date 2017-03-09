<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineAdviserCustomerOrderBook.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineAdviserCustomerOrderBook" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Order Book
                        </td>
                        <%-- <td align="right">
                            &nbsp; &nbsp;
                            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="View UploadLog"
                                OnClick="lnkBtnBack_Click"></asp:LinkButton>
                        </td>--%>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton ID="imgexportButton" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                Visible="false" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                OnClick="btnExportData_OnClick" OnClientClick="setFormat('excel')" Height="22px"
                                Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table id="tblField" runat="server">
    <tr>
        <td align="right">
            <asp:Label ID="lblType" runat="server" CssClass="FieldName" Text="Search Type:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlType_OnSelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem Text="Order No." Value="ON"></asp:ListItem>
                <asp:ListItem Text="Without Order No." Value="WON" Selected="True"></asp:ListItem>
            </asp:DropDownList>
            <span id="Span3" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlType"
                ErrorMessage="<br />Please select search type" CssClass="cvPCG" Display="Dynamic"
                runat="server" InitialValue="Select" ValidationGroup="btnViewOrder">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
<div id="divConditional" runat="server" style="padding-top: 4px" visible="false">
    <table>
        <tr>
            <td>
                <asp:Label runat="server" class="FieldName" Text="Mode:" ID="lblMode"></asp:Label>
            </td>
            <td>
            <asp:DropDownList ID="ddlMode"  runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlMode_OnSelectedIndexChanged" AutoPostBack="true" >
            <asp:ListItem Value="0" Text="Online"></asp:ListItem>
            <asp:ListItem Value="1" Text="Demat"></asp:ListItem>
            </asp:DropDownList>
            </td>
            <td id="tdlblRejectReason" runat="server" align="right" style="padding-left: 45px;">
                <asp:Label runat="server" class="FieldName" Text="AMC:" ID="lblAccount"></asp:Label>
            </td>
            <td>
                <asp:DropDownList CssClass="cmbField" ID="ddlAmc" runat="server" AutoPostBack="true">
                </asp:DropDownList>
            </td>
            <%--  <td id="tdAccount" runat="server" align="left">
               
            </td>
           &nbsp--%>
            <td id="td1" runat="server" align="right">
                <asp:Label runat="server" class="FieldName" Text="Order Status:" ID="Label1"></asp:Label>
            </td>
            <td>
                <asp:DropDownList CssClass="cmbField" ID="ddlOrderStatus" runat="server" AutoPostBack="false">
                </asp:DropDownList>
            </td>
            <%--  <td id="td2" runat="server">
               
            </td>--%>
            <td id="tdlblFromDate" runat="server" align="right">
                <asp:Label class="FieldName" ID="lblFromTran" Text="From :" runat="server" />
            </td>
            <td id="tdTxtFromDate" runat="server">
                <telerik:RadDatePicker ID="txtOrderFrom" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
                <div id="dvTransactionDate" runat="server" class="dvInLine">
                    <span id="Span1" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvtxtTransactionDate" ControlToValidate="txtOrderFrom"
                        ErrorMessage="<br />Please select a From Date" CssClass="cvPCG" Display="Dynamic"
                        runat="server" InitialValue="" ValidationGroup="btnViewOrder">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtOrderFrom" CssClass="cvPCG" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </div>
            </td>
            <td id="tdlblToDate" runat="server">
                <asp:Label ID="lblToTran" Text="To :" CssClass="FieldName" runat="server" />
            </td>
            <td id="tdTxtToDate" runat="server">
                <telerik:RadDatePicker ID="txtOrderTo" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
                <div id="Div1" runat="server" class="dvInLine">
                    <span id="Span2" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtOrderTo"
                        ErrorMessage="<br />Please select a To Date" CssClass="cvPCG" Display="Dynamic"
                        runat="server" InitialValue="" ValidationGroup="btnViewOrder">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtOrderTo" CssClass="cvPCG" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </div>
                <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtOrderTo"
                    ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                    ControlToCompare="txtOrderFrom" CssClass="cvPCG" ValidationGroup="btnViewOrder"
                    Display="Dynamic">
                </asp:CompareValidator>
            </td>
        </tr>
    </table>
</div>
<table id="tblOrder" runat="server">
    <tr>
        <td align="right">
            <asp:Label ID="lblOrderNo" runat="server" Text="Order No:" CssClass="FieldName" Visible="false"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtOrderNo" runat="server" Visible="false"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td id="tdBtnOrder" runat="server">
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnViewOrder" runat="server" CssClass="PCGButton" Text="Go" ValidationGroup="btnViewOrder"
                OnClick="btnViewOrder_Click" />
        </td>
    </tr>
</table>
<table style="width: 100%" class="TableBackground">
    <tr id="trNoRecords" runat="server" visible="false">
        <td align="center">
            <div id="divNoRecords" runat="server" class="failure-msg">
                <asp:Label ID="lblNoRecords" Text="No Records found" runat="server"></asp:Label>
            </div>
        </td>
    </tr>
</table>
<asp:Panel ID="pnlOrderBook" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal"
    Visible="false">
    <table width="100%">
        <tr id="trExportFilteredDupData" runat="server">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="gvOrderBookMIS" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" Width="102%" ClientSettings-AllowColumnsReorder="true"
                    AllowAutomaticInserts="false" OnNeedDataSource="gvOrderBookMIS_OnNeedDataSource"
                    OnItemCommand="gvOrderBookMIS_OnItemCommand" AllowFilteringByColumn="true" OnItemDataBound="gvOrderBookMIS_ItemDataBound">
                    <%--  OnNeedDataSource="gvOrderList_OnNeedDataSource" OnItemDataBound="gvOrderList_ItemDataBound"--%>
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="CO_OrderId,C_CustomerId,PAG_AssetGroupCode,CO_OrderDate,WMTT_TransactionClassificationCode,XS_Status"
                        Width="102%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None"
                        AllowFilteringByColumn="true" EditMode="PopUp">
                        <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                            ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="CO_OrderDate" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                AllowFiltering="false" HeaderText="Request Date/Time" UniqueName="CO_OrderDate"
                                SortExpression="CO_OrderDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="120px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_OrderId" AllowFiltering="true" HeaderText="Order No."
                                UniqueName="CO_OrderId" SortExpression="CO_OrderId" ShowFilterIcon="false" CurrentFilterFunction="EqualTo"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn  DataField="BMOERD_BSEOrderId" AllowFiltering="true" HeaderText="BSE Exchange No"
                                UniqueName="BMOERD_BSEOrderId" SortExpression="BMOERD_BSEOrderId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%--  <telerik:GridBoundColumn DataField="CO_OrderId" AllowFiltering="true" HeaderText="Order No."
                                UniqueName="CO_OrderId" SortExpression="CO_OrderId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="75px" FilterControlWidth="50px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn Visible="false" DataField="WMTT_TransactionClassificationCode"
                                AllowFiltering="true" HeaderText="Order No." UniqueName="WMTT_TransactionClassificationCode"
                                SortExpression="WMTT_TransactionClassificationCode" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="75px" FilterControlWidth="50px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PA_AMCName" HeaderText="Fund Name" AllowFiltering="false"
                                HeaderStyle-Wrap="false" SortExpression="PA_AMCName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="100px" UniqueName="PA_AMCName"
                                FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn DataField="CMFA_FolioNum" AllowFiltering="true" HeaderText="Folio"
                                UniqueName="CMFA_FolioNum" SortExpression="CMFA_FolioNum" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"
                                FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn DataField="CMFA_FolioNum" HeaderText="Folio" AllowFiltering="true"
                                SortExpression="CMFA_FolioNum" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="CMFA_FolioNum" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="220px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_CustCode" AllowFiltering="true" HeaderText="Client Id"
                                UniqueName="C_CustCode" SortExpression="C_CustCode " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="75px" FilterControlWidth="50px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="true" DataField="Name" AllowFiltering="true" HeaderText="Customer"
                                UniqueName="Name" SortExpression="Name" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="true" DataField="C_PANNum" AllowFiltering="true"
                                HeaderText="PAN" UniqueName="C_PANNum" SortExpression="C_PANNum" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="PAG_AssetGroupName" AllowFiltering="true"
                                HeaderText="Asset Name" UniqueName="PAG_AssetGroupName" SortExpression="PAG_AssetGroupName"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PASP_SchemePlanName" HeaderText="SchemePlanName"
                                AllowFiltering="true" SortExpression="PASP_SchemePlanName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="PASP_SchemePlanName"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="220px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PAISC_AssetInstrumentSubCategoryName" HeaderText="Sub Category"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="PAISC_AssetInstrumentSubCategoryName"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="PAISC_AssetInstrumentSubCategoryName" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WMTT_TransactionType" AllowFiltering="true" HeaderText="Order Type"
                                UniqueName="WMTT_TransactionType" SortExpression="WMTT_TransactionType" ShowFilterIcon="false"
                                HeaderStyle-Width="80px" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="CMFT_Price" AllowFiltering="true"
                                HeaderText="Actioned NAV" DataFormatString="{0:N0}" UniqueName="CMFT_Price" SortExpression="CMFT_Price"
                                ShowFilterIcon="false" HeaderStyle-Width="80px" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="CMFT_TransactionDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="true" HeaderText="Date" UniqueName="CMFT_TransactionDate" SortExpression="CMFT_TransactionDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFOD_DividendOption" HeaderText="Dividend Type"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFOD_DividendOption"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CMFOD_DividendOption" FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="DivedendFrequency" HeaderText="Div-Reinvestment Freq."
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="DivedendFrequency"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="DivedendFrequency" FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="TransactionId" HeaderText="TransactionId"
                                AllowFiltering="false" SortExpression="TransactionId" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="TransactionId"
                                FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFOD_Amount" AllowFiltering="true" HeaderText="Amount"
                                DataFormatString="{0:N2}" UniqueName="CMFOD_Amount" SortExpression="CMFOD_Amount"
                                ShowFilterIcon="false" HeaderStyle-Width="80px" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFOD_Units" AllowFiltering="true" HeaderText="Units"
                                DataFormatString="{0:N0}" UniqueName="CMFOD_Units" SortExpression="CMFOD_Units"
                                ShowFilterIcon="false" HeaderStyle-Width="80px" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="Nav" HeaderText="Current NAV"
                                AllowFiltering="false" HeaderStyle-Wrap="false" SortExpression="Nav" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="Nav"
                                FooterStyle-HorizontalAlign="Right" DataFormatString="{0:n3}">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="NavDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="false" HeaderText="Nav Date" UniqueName="NavDate" SortExpression="NavDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="AdditionalTaken" HeaderText="Add.Action Taken"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="AdditionalTaken"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="AdditionalTaken" FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="CO_PaymentDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="false" HeaderText="Payment Date" UniqueName="CO_PaymentDate"
                                SortExpression="CO_PaymentDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="CMFOD_ApprovalDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="true" HeaderText="Approval Date" UniqueName="CMFOD_ApprovalDate"
                                SortExpression="CMFOD_ApprovalDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%--  <telerik:GridBoundColumn DataField="CMFOD_ISAllunits" HeaderText="Redeem All"
                                UniqueName="CMFOD_ISAllunits" SortExpression="CMFOD_ISAllunits" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="false" HeaderStyle-Width="80px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn DataField="CMFOD_ISAllunits" AllowFiltering="false" HeaderText="Redeem All"
                                HeaderStyle-Width="105px" UniqueName="CMFOD_ISAllunits " SortExpression="CMFOD_ISAllunits "
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XS_Status" AllowFiltering="false" HeaderText="Status"
                                HeaderStyle-Width="90px" UniqueName="XS_Status" SortExpression="XS_Status" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_CreatedOn" AllowFiltering="false" HeaderText="Created On"
                                HeaderStyle-Width="90px" UniqueName="CO_CreatedOn" SortExpression="CO_CreatedOn"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_CreatedBy" AllowFiltering="false" HeaderText="Created By"
                                HeaderStyle-Width="90px" UniqueName="CO_CreatedBy" SortExpression="CO_CreatedBy"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ModifiedOn" AllowFiltering="false" HeaderText="Modified On"
                                HeaderStyle-Width="90px" UniqueName="CO_ModifiedOn" SortExpression="CO_ModifiedOn"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ModifiedBy" AllowFiltering="false" HeaderText="Modified By"
                                HeaderStyle-Width="90px" UniqueName="CO_ModifiedBy" SortExpression="CO_ModifiedBy"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COS_Reason" AllowFiltering="false" HeaderText="Remark"
                                HeaderStyle-Width="105px" UniqueName="COS_Reason " SortExpression="COS_Reason "
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Channel" AllowFiltering="false" HeaderText="Channel"
                                HeaderStyle-Width="65px" UniqueName="Channel" SortExpression="Channel" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%-- <telerik:GridTemplateColumn ItemStyle-Width="60px" AllowFiltering="false" HeaderText="Action"
                                Visible="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Edit" ImageUrl="~/Images/Buy-Button.png" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>--%>
                            <telerik:GridEditCommandColumn Visible="true" HeaderStyle-Width="60px" UniqueName="MarkAsReject"
                                EditText="Mark As Reject" CancelText="Cancel" UpdateText="OK"  >
                            </telerik:GridEditCommandColumn>
                            <telerik:GridTemplateColumn ItemStyle-Width="60px" AllowFiltering="false" HeaderText="Action" UniqueName="RevertToExecute"
                                Visible="false">
                                <ItemTemplate>
                                <asp:LinkButton ID="RevertToExecute" runat="server" Text="Revert To Execute" CommandName="RevertToExecute"  OnClientClick="return confirm('The Order step will be Reverted to Executed.Would you like to Continue?');"></asp:LinkButton>
                                </ItemTemplate>
                                </telerik:GridTemplateColumn> 
                                
                            
                            <%--<telerik:GridEditCommandColumn Visible="false" HeaderStyle-Width="60px" UniqueName="RevertToExecute"
                                EditText="Revert To Execute" CancelText="Cancel" UpdateText="OK" HeaderText="Action">
                            </telerik:GridEditCommandColumn>--%>
                        </Columns>
                        <EditFormSettings FormTableStyle-Height="40%" EditFormType="Template" FormMainTableStyle-Width="300px"
                            CaptionFormatString=" Order canceling Request">
                            <FormTemplate>
                                <table style="background-color: White;" border="0">
                                    <tr>
                                        <td class="leftField">
                                            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Request No.:"></asp:Label>
                                        </td>
                                        <td class="rightField">
                                            <asp:TextBox ID="txtRejOrderId" runat="server" CssClass="txtField" Style="width: 250px;"
                                                Text='<%# Bind("CO_OrderId") %>' ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftField">
                                            <asp:Label ID="Label20" runat="server" Text="Remark:" CssClass="FieldName"></asp:Label>
                                        </td>
                                        <td class="rightField">
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="txtField" Style="width: 250px;"></asp:TextBox>
                                        </td>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <%--  <td colspan="2">
                                                    &nbsp;
                                                </td>--%>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left">
                                            <asp:Button ID="Button1" Text="OK" runat="server" CssClass="PCGButton" CommandName="Update"
                                                ValidationGroup="btnSubmit">
                                                <%-- OnClientClick='<%# (Container is GridEditFormInsertItem) ?  " javascript:return ShowPopup();": "" %>'--%>
                                            </asp:Button>
                                            <%--</td>
                                                    <td  >--%>
                                            <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                                                CommandName="Cancel"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </FormTemplate>
                        </EditFormSettings>
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
<asp:HiddenField ID="hdnAmc" runat="server" />
<asp:HiddenField ID="hdnOrderStatus" runat="server" />
