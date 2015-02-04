<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerTransactionBookList.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.CustomerTransactionBookList" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
<%--<table width="100%">
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
</table>--%>
<div class="divOnlinePageHeading" style="float: right; width: 100%">
    <div style="float: right; padding-right: 100px;">
        <table cellspacing="0" cellpadding="3" width="100%">
            <tr>
                <td align="right">
                    <asp:LinkButton runat="server" ID="lbBack" CssClass="LinkButtons" Text="Back" Visible="false"
                        OnClick="lbBack_Click"></asp:LinkButton>
                </td>
                <td align="right" style="width:5%">
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
                <asp:DropDownList CssClass="cmbField" ID="ddlAmc" runat="server" AutoPostBack="false">
                </asp:DropDownList>
            </td>
            <td align="right" visible="false" runat="server">
                <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Portfolio:"></asp:Label>
                <asp:DropDownList ID="ddlPortfolioGroup" runat="server" CssClass="cmbField">
                    <asp:ListItem Text="Managed" Value="1">Managed</asp:ListItem>
                    <asp:ListItem Text="UnManaged" Value="0">UnManaged</asp:ListItem>
                </asp:DropDownList>
            </td>
            <%-- <td id="td1" runat="server">
                <asp:Label runat="server" class="FieldName" Text="Order Status:" ID="Label2"></asp:Label>
                <asp:DropDownList CssClass="cmbField" ID="ddlOrderStatus" runat="server" AutoPostBack="false">
                </asp:DropDownList>
            </td>--%>
            <td id="tdlblFromDate" runat="server" align="right">
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
</div>
<table style="width: 100%" class="TableBackground">
    <tr id="trNoRecords" runat="server" visible="false">
        <td align="center">
            <div id="divNoRecords" runat="server" class="failure-msg">
                <asp:Label ID="lblNoRecords" Text="No Records found" runat="server"></asp:Label>
            </div>
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
                        AllowPaging="True" ShowStatusBar="True" OnNeedDataSource="gvTransationBookMIS_OnNeedDataSource"
                        ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowAutomaticInserts="false" OnItemDataBound="ItemDataBound" >
                        <%--OnItemCommand="gvTransationBookMIS_OnItemCommand"OnPreRender="gvTransationBookMIS_PreRender" 
                                            OnItemDataBound="gvTransationBookMIS_ItemDataBound"--%>
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true">
                        </ExportSettings>
                        <MasterTableView DataKeyNames="TransactionId" Width="100%" AllowMultiColumnSorting="True"
                            AllowFilteringByColumn="true" AutoGenerateColumns="false" CommandItemDisplay="None">
                            <Columns>
                                <%--<telerik:GridTemplateColumn AllowFiltering="false" FooterText="Grand Total:" HeaderStyle-Wrap="false" Visible="false">
                                                        <ItemStyle Wrap="false" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkView" runat="server" CssClass="cmbField" Text="View Details"
                                                                OnClick="lnkView_Click">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>--%>
                                <telerik:GridBoundColumn Visible="false" DataField="Customer Name" HeaderText="Customer"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="Customer Name"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="Customer Name" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                               <%-- <telerik:GridBoundColumn DataField="CO_OrderDate" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}"
                                    AllowFiltering="true" HeaderText="Request Date/Time" UniqueName="CO_OrderDate"
                                    SortExpression="CO_OrderDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                                <%--  <telerik:GridBoundColumn DataField="TransactionNumber" HeaderText="TransactionNo"
                                    AllowFiltering="false" SortExpression="TransactionNumber" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="TransactionNumber"
                                    FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn Visible="false" DataField="TransactionId" HeaderText="Transaction ID"
                                    AllowFiltering="false" SortExpression="TransactionId" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="TransactionId"
                                    FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                               <%-- <telerik:GridBoundColumn DataField="OrderNo" AllowFiltering="true" HeaderText="Order No."
                                    UniqueName="OrderNo" SortExpression="OrderNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" HeaderStyle-Width="75px" FilterControlWidth="50px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn DataField="AMC" HeaderText="Fund Name" AllowFiltering="true"
                                    HeaderStyle-Wrap="false" SortExpression="AMC" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="AMC" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Folio Number" HeaderText="Folio No." AllowFiltering="true"
                                    SortExpression="Folio Number" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="Folio Number" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="Category" HeaderText="Category"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="Category" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="Category"
                                    FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Scheme Name" HeaderText="Scheme Name" AllowFiltering="true"
                                    HeaderStyle-Wrap="false" SortExpression="Scheme Name" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="Scheme Name"
                                    FooterStyle-HorizontalAlign="Left">
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
                                <telerik:GridBoundColumn DataField="Transaction Type" HeaderText="Type" AllowFiltering="true"
                                    HeaderStyle-Wrap="false" SortExpression="Transaction Type" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="Transaction Type"
                                    FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Price" HeaderText="Actioned NAV" AllowFiltering="false"
                                    SortExpression="Price" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    HeaderStyle-Wrap="false" AutoPostBackOnFilter="true" UniqueName="Price" FooterStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n4}" >
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Transaction Date" HeaderText="Date" AllowFiltering="false"
                                    AllowSorting="true" HeaderStyle-Wrap="false" SortExpression="Transaction Date"
                                    ShowFilterIcon="false" DataFormatString="{0:d}" DataType="System.DateTime" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="Transaction Date" FooterStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DivReinvestment" HeaderText="Dividend Type"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="DivReinvestment"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="DivReinvestment" FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn visible="false" DataField="DivedendFrequency" HeaderText="Divedend Frequency"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="DivedendFrequency"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="DivedendFrequency" FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Amount" HeaderText="Amount (Rs)" AllowFiltering="false"
                                    HeaderStyle-Wrap="false" SortExpression="Amount" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="Amount" FooterStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n0}">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Units" HeaderText="Units" AllowFiltering="false"
                                    HeaderStyle-Wrap="false" SortExpression="Units" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="Units" FooterStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n3}" Aggregate="Sum">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="CurrentNav" HeaderText="Current NAV"
                                    AllowFiltering="false" HeaderStyle-Wrap="false" SortExpression="CurrentNav" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CurrentNav"
                                    FooterStyle-HorizontalAlign="Right" DataFormatString="{0:n3}">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="CMFT_ExternalBrokerageAmount"
                                    HeaderText="Brokerage(Rs)" AllowFiltering="false" HeaderStyle-Wrap="false" SortExpression="CMFT_ExternalBrokerageAmount"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="CMFT_ExternalBrokerageAmount" FooterStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n2}" Aggregate="Sum">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="STT" HeaderText="STT (Rs)" AllowFiltering="false"
                                    HeaderStyle-Wrap="false" SortExpression="STT" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="STT" FooterStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n}" Aggregate="Sum">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Channel" HeaderText="Channel" AllowFiltering="false"
                                    HeaderStyle-Wrap="false" SortExpression="Channel" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="Channel" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="ADUL_ProcessId" HeaderText="Process ID"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="ADUL_ProcessId"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="ADUL_ProcessId" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="CMFT_Area" HeaderText="Area"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFT_Area" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFT_Area"
                                    FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="CMFT_EUIN" HeaderText="EUIN"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CMFT_EUIN" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="CMFT_EUIN"
                                    FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="TransactionStatus" HeaderText="Transaction Status"
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
                                                                    function TransactionIndexChanged(sender, args) {
                                                                        var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                                                        //////                                                    sender.value = args.get_item().get_value();
                                                                        tableView.filter("RadComboBoxTS", args.get_item().get_value(), "EqualTo");
                                                                    }
                                                                </script>
                                                            </telerik:RadScriptBlock>
                                                        </FilterTemplate>--%>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ELSSMaturityDate" DataFormatString="{0:d}"  DataType="System.DateTime"
                                    AllowFiltering="true" HeaderText="Maturity Date" UniqueName="ELSSMaturityDate"
                                    SortExpression="ELSSMaturityDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px" >
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn DataField="CO_OrderDate" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}"
                                    AllowFiltering="true" HeaderText="Request Date/Time" UniqueName="CO_OrderDate"
                                    SortExpression="CO_OrderDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn DataField="OrderNo" AllowFiltering="true" HeaderText="Order No."
                                    UniqueName="OrderNo" SortExpression="OrderNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" HeaderStyle-Width="75px" FilterControlWidth="50px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="CreatedOn" HeaderText="Add Date (System)"
                                    AllowFiltering="false" HeaderStyle-Wrap="false" SortExpression="CreatedOn" ShowFilterIcon="false"
                                    AllowSorting="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="CreatedOn" FooterStyle-HorizontalAlign="Left">
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
<asp:HiddenField ID="hdnAmc" runat="server" />
<asp:HiddenField ID="hdnOrderStatus" runat="server" />
