<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineAdviserCustomerTransctionBook.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineAdviserCustomerTransctionBook" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Transaction Book
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton Visible="false" ID="btnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr id="trAMC" runat="server">
        <td id="tdlblAmc" runat="server" align="left">
            <asp:Label runat="server" class="FieldName" Text="AMC:" ID="lblAccount"></asp:Label>
        </td>
        <td id="tdlblFromDate" runat="server" align="right">
            <asp:DropDownList CssClass="cmbField" ID="ddlAmc" runat="server" AutoPostBack="false"
                Width="300px">
            </asp:DropDownList>
        </td>
        <td>
            <asp:Label class="FieldName" ID="lblFromTran" Text="From :" runat="server" />
        </td>
        <td id="tdTxtFromDate" runat="server">
            <telerik:RadDatePicker ID="txtFrom" CssClass="txtField" runat="server" Culture="English (United States)"
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
                <asp:RequiredFieldValidator ID="rfvtxtTransactionDate" ControlToValidate="txtFrom"
                    ErrorMessage="<br />Please select a From Date" CssClass="cvPCG" Display="Dynamic"
                    runat="server" InitialValue="">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                    Type="Date" ControlToValidate="txtFrom" CssClass="cvPCG" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
            </div>
        </td>
        <td id="tdlblToDate" runat="server">
            <asp:Label ID="lblToTran" Text="To :" CssClass="FieldName" runat="server" />
        </td>
        <td id="tdTxtToDate" runat="server">
            <telerik:RadDatePicker ID="txtTo" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar def ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <div id="Div1" runat="server" class="dvInLine">
                <span id="Span2" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTo"
                    ErrorMessage="<br />Please select a To Date" CssClass="cvPCG" Display="Dynamic"
                    runat="server" InitialValue="">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                    Type="Date" ControlToValidate="txtTo" CssClass="cvPCG" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
            </div>
            <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtTo"
                ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                ControlToCompare="txtFrom" CssClass="cvPCG" ValidationGroup="btnViewTransaction"
                Display="Dynamic">
            </asp:CompareValidator>
        </td>
        <td id="tdBtnOrder" runat="server" colspan="4">
            <asp:Button ID="btnViewTransaction" runat="server" CssClass="PCGButton" Text="Go"
                ValidationGroup="btnViewTransaction" OnClick="btnViewTransaction_Click" />
        </td>
    </tr>
</table>
<asp:Panel ID="pnlTransactionBook" runat="server" class="Landscape" Width="100%"
    ScrollBars="Horizontal" Visible="false">
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <div id="dvTransactionsView" runat="server" style="margin: 2px; width: 640px;">
                    <telerik:RadGrid ID="gvTransationBookMIS" runat="server" GridLines="None" AutoGenerateColumns="False"
                        allowfiltering="true" AllowFilteringByColumn="true" PageSize="10" AllowSorting="true"
                        AllowPaging="True" ShowStatusBar="True" ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false"
                        Width="120%" AllowAutomaticInserts="false" AllowCustomPaging="true" OnNeedDataSource="gvTransationBookMIS_OnNeedDataSource"
                        OnItemCommand="gvTransationBookMIS_ItemCommand">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true">
                        </ExportSettings>
                        <MasterTableView DataKeyNames="CMFT_MFTransId" Width="100%" AllowMultiColumnSorting="True"
                            AllowFilteringByColumn="true" AutoGenerateColumns="false" CommandItemDisplay="None">
                            <Columns>
                                <telerik:GridBoundColumn Visible="true" DataField="Name" HeaderText="Customer" AllowFiltering="true"
                                    HeaderStyle-Wrap="false" SortExpression="Name" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="Name" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="c_CustCode" HeaderText="Client Code" AllowFiltering="true"
                                    SortExpression="c_CustCode" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="c_CustCode" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="c_PANNum" HeaderText="PAN" AllowFiltering="true"
                                    SortExpression="c_PANNum" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="c_PANNum" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="CMFT_MFTransId" HeaderText="Transaction ID"
                                    AllowFiltering="false" SortExpression="CMFT_MFTransId" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFT_MFTransId"
                                    FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <%-- <telerik:GridBoundColumn DataField="OrderNo" AllowFiltering="true" HeaderText="Order No."
                                    UniqueName="OrderNo" SortExpression="OrderNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" HeaderStyle-Width="75px" FilterControlWidth="50px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn DataField="PA_AMCName" HeaderText="Fund Name" AllowFiltering="true"
                                    HeaderStyle-Wrap="false" SortExpression="PA_AMCName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="PA_AMCName" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFA_FolioNum" HeaderText="Folio No." AllowFiltering="true"
                                    SortExpression="CMFA_FolioNum" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="CMFA_FolioNum" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="PAIC_AssetInstrumentCategoryName"
                                    HeaderText="Category" AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="PAIC_AssetInstrumentCategoryName"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="PAIC_AssetInstrumentCategoryName" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PASP_SchemePlanName" HeaderText="Scheme Name"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="PASP_SchemePlanName"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="PASP_SchemePlanName" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PAISC_AssetInstrumentSubCategoryName" HeaderText="Sub Category"
                                    AllowFiltering="false" HeaderStyle-Wrap="false" SortExpression="PAISC_AssetInstrumentSubCategoryName"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="PAISC_AssetInstrumentSubCategoryName" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridTemplateColumn AllowFiltering="true" DataField="Scheme Name" AutoPostBackOnFilter="true"
                                                        HeaderText="Scheme" ShowFilterIcon="false" FilterControlWidth="280px">
                                                        <ItemStyle Wrap="false" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Scheme" Text='<%# Eval("Scheme Name").ToString() %>' />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>--%>
                                <telerik:GridBoundColumn DataField="WMTT_TransactionClassificationName" HeaderText="Type"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="WMTT_TransactionClassificationName"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="WMTT_TransactionClassificationName" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFT_Price" HeaderText="Actioned NAV" AllowFiltering="false"
                                    SortExpression="CMFT_Price" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    HeaderStyle-Wrap="false" AutoPostBackOnFilter="true" UniqueName="CMFT_Price"
                                    FooterStyle-HorizontalAlign="Right" DataFormatString="{0:n4}">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFT_TransactionDate" HeaderText="Date" AllowFiltering="false"
                                    AllowSorting="true" HeaderStyle-Wrap="false" SortExpression="CMFT_TransactionDate"
                                    ShowFilterIcon="false" DataFormatString="{0:d}" DataType="System.DateTime" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="CMFT_TransactionDate" FooterStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFOD_DividendOption" HeaderText="Dividend Type"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFOD_DividendOption"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="CMFOD_DividendOption" FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn Visible="false" DataField="DivedendFrequency" HeaderText="Divedend Frequency"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="DivedendFrequency"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="DivedendFrequency" FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn DataField="CMFT_Amount" HeaderText="Amount (Rs)" AllowFiltering="false"
                                    HeaderStyle-Wrap="false" SortExpression="CMFT_Amount" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFT_Amount"
                                    FooterStyle-HorizontalAlign="Right" DataFormatString="{0:n0}">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFT_Units" HeaderText="Units" AllowFiltering="false"
                                    HeaderStyle-Wrap="false" SortExpression="CMFT_Units" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="CMFT_Units" FooterStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n3}" Aggregate="Sum">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <%-- <telerik:GridBoundColumn Visible="false" DataField="CurrentNav" HeaderText="Current NAV"
                                    AllowFiltering="false" HeaderStyle-Wrap="false" SortExpression="CurrentNav" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CurrentNav"
                                    FooterStyle-HorizontalAlign="Right" DataFormatString="{0:n3}">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                                <%--   <telerik:GridBoundColumn Visible="false" DataField="CMFT_ExternalBrokerageAmount"
                                    HeaderText="Brokerage(Rs)" AllowFiltering="false" HeaderStyle-Wrap="false" SortExpression="CMFT_ExternalBrokerageAmount"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="CMFT_ExternalBrokerageAmount" FooterStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n2}" Aggregate="Sum">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                                <%--<telerik:GridBoundColumn Visible="false" DataField="STT" HeaderText="STT (Rs)" AllowFiltering="false"
                                    HeaderStyle-Wrap="false" SortExpression="STT" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="STT" FooterStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n}" Aggregate="Sum">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn DataField="Channel" HeaderText="Channel" AllowFiltering="false"
                                    HeaderStyle-Wrap="false" SortExpression="Channel" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="Channel" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <%--     <telerik:GridBoundColumn Visible="false" DataField="ADUL_ProcessId" HeaderText="Process ID"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="ADUL_ProcessId"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="ADUL_ProcessId" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                                <%-- <telerik:GridBoundColumn Visible="false" DataField="CMFT_Area" HeaderText="Area"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFT_Area" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFT_Area"
                                    FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                                <%--<telerik:GridBoundColumn Visible="false" DataField="CMFT_EUIN" HeaderText="EUIN"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFT_EUIN" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFT_EUIN"
                                    FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                                <%--  <telerik:GridBoundColumn Visible="false" DataField="TransactionStatus" HeaderText="Transaction Status"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="TransactionStatus"
                                    ShowFilterIcon="false" AutoPostBackOnFilter="true" UniqueName="TransactionStatus"
                                    FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                    <%-- <FilterTemplate>
                                                            <telerik:RadComboBox ID="RadComboBoxTS" AutoPostBack="true" AllowFiltering="true"
                                                                CssClass="cmbField" Width="100px" IsFilteringEnabled="true" AppendDataBoundItems="true"
                                                                OnPreRender="Transaction_PreRender" EnableViewState="true" OnSelectedIndexChanged="RadComboBoxTS_SelectedIndexChanged"
                                                                SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("TransactionStatus").CurrentFilterValue %>'
                                                                runat="server">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="ALL" Value="" Selected="false"></telerik:RadComboBoxItem>
                                                                    <telerik:RadComboBoxItem Text="OK" Value="OK" Selected="false"></telerik:RadComboBoxItem>
                                                                    <telerik:RadComboBoxItem Text="Cancel" Value="Cancel" Selected="false"></telerik:RadComboBoxItem>
                                                                    <telerik:RadComboBoxItem Text="Original" Value="Original" Selected="false"></telerik:RadComboBoxItem>
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                            <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                                                                <script type="text/javascript">
//                                                                    function TransactionIndexChanged(sender, args) {
//                                                                        var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
//                                                                        //////                                                    sender.value = args.get_item().get_value();
//                                                                        tableView.filter("RadComboBoxTS", args.get_item().get_value(), "EqualTo");
                                                                    }
                                                                </script>
                                                            </telerik:RadScriptBlock>
                                                        </FilterTemplate>-
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn DataField="CMFT_ELSSMaturityDate" DataFormatString="{0:dd/MM/yyyy}"
                                    AllowFiltering="false" HeaderText="Maturity Date" UniqueName="CMFT_ELSSMaturityDate"
                                    SortExpression="CMFT_ELSSMaturityDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CO_OrderDate" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}"
                                    AllowFiltering="false" HeaderText="Request Date/Time" UniqueName="CO_OrderDate"
                                    SortExpression="CO_OrderDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Co_OrderId" AllowFiltering="false" HeaderText="Order No."
                                    UniqueName="Co_OrderId" SortExpression="Co_OrderId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" HeaderStyle-Width="75px" FilterControlWidth="50px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="CMFT_CreatedOn" HeaderText="Add Date (System)"
                                    AllowFiltering="false" HeaderStyle-Wrap="false" SortExpression="CMFT_CreatedOn"
                                    ShowFilterIcon="false" AllowSorting="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="CMFT_CreatedOn" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="hdncustomername" runat="server" />
<asp:HiddenField ID="hdnamcname" runat="server" />
<asp:HiddenField ID="hdnfoliono" runat="server" />
<asp:HiddenField ID="hdnschemename" runat="server" />
<asp:HiddenField ID="hdncustcode" runat="server" />
<asp:HiddenField ID="hdnpanno" runat="server" />
<asp:HiddenField ID="hdntype" runat="server" />
<asp:HiddenField ID="hdndividenttype" runat="server" />
<asp:HiddenField ID="hdnOrderno" runat="server" />
