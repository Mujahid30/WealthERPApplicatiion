<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMMultipleEqTransactionsView.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.RMMultipleEqTransactionsView" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<asp:ScriptManager ID="scptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script type="text/javascript" language="javascript">
    function GetParentCustomerId(source, eventArgs) {
        document.getElementById("<%= txtParentCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    }

    function keyPress(sender, args) {
        if (args.keyCode == 13) {
            return false;
        }
    }
    function GridCreated(sender, args) {
        var scrollArea = sender.GridDataDiv;
        var dataHeight = sender.get_masterTableView().get_element().clientHeight;
        if (dataHeight < 300) {
            scrollArea.style.height = dataHeight + 17 + "px";
        }

    }  
</script>

<style type="text/css" runat="server">
    .rgDataDiv
    {
        height: auto;
        width: 101.5% !important;
    }
</style>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            View Equity Transactions
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="imgBtnrgHoldings" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                Visible="false" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px" OnClick="btnExportFilteredData_OnClick">
                            </asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:RadioButton ID="rbtnPickDate" AutoPostBack="true" Checked="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date" />
            <asp:Label ID="lblPickDate" runat="server" Text="Pick a date range" CssClass="Field"></asp:Label>
        </td>
        <td>
            <asp:RadioButton ID="rbtnPickPeriod" AutoPostBack="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date" />
            <asp:Label ID="lblPickPeriod" runat="server" Text="Pick a Period" CssClass="Field"></asp:Label>
        </td>
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
        </td>
        <td>
            <asp:Label ID="lblCustomerGroup" runat="server" CssClass="FieldName" Text="Customer :"></asp:Label>
            &nbsp;
            <asp:RadioButton ID="rbtnAll" AutoPostBack="true" Checked="true" runat="server" GroupName="GroupAll"
                Text="All" CssClass="cmbField" OnCheckedChanged="rbtnAll_CheckedChanged" />
            <asp:RadioButton ID="rbtnGroup" AutoPostBack="true" runat="server" GroupName="GroupAll"
                Text="Group" CssClass="cmbField" OnCheckedChanged="rbtnAll_CheckedChanged" />
        </td>
    </tr>
</table>
<table>
    <tr id="trRange" visible="true" runat="server" onkeypress="return keyPress(this, event)">
        <td align="right" valign="middle">
            <asp:Label ID="lblFromDate" runat="server" CssClass="FieldName">From:</asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtFromDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput EmptyMessage="dd/mm/yyyy" ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy"
                    DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFromDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnGo">
            </asp:RequiredFieldValidator>
        </td>
        <td align="right" valign="middle">
            <asp:Label ID="lblToDate" runat="server" CssClass="FieldName">To:</asp:Label>
        </td>
        <td valign="top">
            <telerik:RadDatePicker ID="txtToDate" CssClass="txtTo" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput EmptyMessage="dd/mm/yyyy" ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy"
                    DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a To Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnGo">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="To Date should not be less than From Date"
                Type="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnGo"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trPeriod" visible="false" runat="server">
        <td align="right" valign="top">
            <asp:Label ID="lblPeriod" runat="server" CssClass="FieldName">Period:</asp:Label>
        </td>
        <td valign="top">
            <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span4" class="spnRequiredField">*</span>
            <br />
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlPeriod"
                CssClass="rfvPCG" ErrorMessage="Please select a Period" Operator="NotEqual" ValueToCompare="Select a Period"
                ValidationGroup="btnGo">
            </asp:CompareValidator>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td id="tdGroupHead" runat="server" align="right" valign="top">
            <asp:Label ID="lblGroupHead" runat="server" CssClass="FieldName" Text="Group Head :"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtParentCustomer" runat="server" CssClass="txtField" AutoPostBack="true"
                AutoComplete="Off"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvGroupHead" ControlToValidate="txtParentCustomer"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a group head" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnGo">
            </asp:RequiredFieldValidator>
            <ajaxToolkit:TextBoxWatermarkExtender ID="txtParentCustomer_TextBoxWatermarkExtender"
                runat="server" TargetControlID="txtParentCustomer" WatermarkText="Type the Customer Name">
            </ajaxToolkit:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtParentCustomer_autoCompleteExtender" runat="server"
                TargetControlID="txtParentCustomer" ServiceMethod="GetParentCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="true" OnClientItemSelected="GetParentCustomerId" />
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Portfolio : "></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlPortfolioGroup" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Managed" Value="1">Managed</asp:ListItem>
                <asp:ListItem Text="UnManaged" Value="0">UnManaged</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" Text="Go" CssClass="PCGButton"
                ValidationGroup="btnGo" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_RMEQMultipleTransactionsView_btnGo', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_RMEQMultipleTransactionsView_btnGo', 'S');" />
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="Msgerror" class="failure-msg" visible="true" runat="server" align="center">
                No Records found.....
            </div>
        </td>
    </tr>
</table>
<html>
<body class="Landscape">
    <%--<div id="tbl" runat="server">--%>
    <%-- <asp:Panel ID="Panel2" runat="server" class="Landscape" Width="100%">--%>
    <table cellspacing="0" cellpadding="1">
        <tr>
            <td>
                <telerik:RadGrid ID="gvMFTransactions" runat="server" GridLines="None" AutoGenerateColumns="False"
                    AllowSorting="true" AllowPaging="True" ShowStatusBar="True" Skin="Telerik" EnableEmbeddedSkins="false"
                    Width="1062px" AllowFilteringByColumn="true" AllowAutomaticInserts="false" OnItemDataBound="gvMFTransactions_ItemDataBound"  ExportSettings-FileName="Equity transaction Details"
                    ShowFooter="true" OnNeedDataSource="gvMFTransactions_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="TransactionId" Width="99%" AllowMultiColumnSorting="True"
                        TableLayout="Fixed" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridBoundColumn Visible="false" DataField="TransactionId" UniqueName="TransactionId"
                                SortExpression="TransactionId" AutoPostBackOnFilter="true" AllowFiltering="true"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FooterText="Grand Total" HeaderText="Customer" DataField="Customer Name"
                                UniqueName="CustomerName" SortExpression="Customer Name" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="110px" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Scrip" DataField="Scrip" UniqueName="Scrip"
                                HeaderStyle-Width="190px" SortExpression="Scrip" AutoPostBackOnFilter="true"
                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Wrap="false" HeaderText="Trade Acc Number" DataField="TradeAccNumber"
                                UniqueName="TradeAccNumber" SortExpression="TradeAccNumber" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="120px" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Type" DataField="Type" UniqueName="Type" SortExpression="Type"
                                HeaderStyle-Width="100px" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Date" DataField="Date" HeaderText="Trade Date"
                                DataFormatString="{0:d}" HeaderStyle-Width="100px" HeaderStyle-Wrap="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                <FilterTemplate>
                                    <telerik:RadDatePicker ID="Datepk" runat="server"
                                       ClientEvents-OnDateSelected="DateSelected"
                                     />
                                    <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">

                                        <script type="text/javascript">
                                            function DateSelected(sender, args) {
                                                var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");

                                                var date = FormatSelectedDate(sender);

                                                tableView.filter("Date", date, "EqualTo");
                                            }
                                            function FormatSelectedDate(picker) {
                                                var date = picker.get_selectedDate();
                                                var dateInput = picker.get_dateInput();
                                                var formattedDate = dateInput.get_dateFormatInfo().FormatDate(date,dateInput.get_displayDateFormat());

                                                return formattedDate;
                                            }
                                           
                                        </script>

                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Rate (Per Unit)"
                                DataField="Rate" UniqueName="Rate" SortExpression="Rate" AutoPostBackOnFilter="true"
                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                HeaderStyle-Width="70px" Aggregate="Sum" DataFormatString="{0:N2}">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Quantity"
                                DataField="Quantity" UniqueName="Quantity" SortExpression="Quantity" AutoPostBackOnFilter="true"
                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                HeaderStyle-Width="65px" Aggregate="Sum" DataFormatString="{0:N2}">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Brokerage (Per Unit)"
                                DataField="Brokerage" UniqueName="Brokerage" SortExpression="Brokerage" AutoPostBackOnFilter="true"
                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                HeaderStyle-Width="72px" Aggregate="Sum" DataFormatString="{0:N2}">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="OtherCharges (Per Unit)"
                                DataField="OtherCharges" UniqueName="OtherCharges" SortExpression="OtherCharges"
                                HeaderStyle-Width="94px" AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" Aggregate="Sum" DataFormatString="{0:N2}">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="STT (Per Unit)"
                                DataField="STT" UniqueName="STT" SortExpression="STT" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="60px" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                Aggregate="Sum" DataFormatString="{0:N2}">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridCalculatedColumn ShowFilterIcon="false" AllowFiltering="false" HeaderText="Gross Price"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="110px" UniqueName="TotalPrice"
                                DataType="System.Double" DataFields="Rate,Quantity,STT,Brokerage,OtherCharges"
                                Expression="({0}*{1})+{2}+{3}+{4}" Aggregate="Sum" DataFormatString="{0:N2}"
                                ItemStyle-HorizontalAlign="Right" />
                            <telerik:GridBoundColumn HeaderText="Speculative/Delivery" DataField="Speculative Or Delivery"
                                UniqueName="Speculative Or Delivery" SortExpression="Speculative Or Delivery"
                                HeaderStyle-Width="110px" AutoPostBackOnFilter="true" AllowFiltering="false"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Portfolio Name" DataField="Portfolio Name" UniqueName="Portfolio Name"
                                SortExpression="Portfolio Name" AutoPostBackOnFilter="true" AllowFiltering="false"
                                HeaderStyle-Width="80px" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Transaction Status" DataField="TransactionStatus"
                                HeaderStyle-Width="110px" UniqueName="TransactionStatus" SortExpression="TransactionStatus"
                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="True" ScrollHeight="300px" />
                        <ClientEvents OnGridCreated="GridCreated" OnKeyPress="" />
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="true" EnableDragToSelectRows="true" />
                        <%--<Resizing AllowColumnResize="true"  />--%>
                    </ClientSettings>
                </telerik:RadGrid>
                <%-- <asp:GridView ID="gvMFTransactions" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        CellPadding="4" EnableViewState="true" AllowPaging="false" CssClass="GridViewStyle"
                        ShowFooter="True" DataKeyNames="TransactionId" OnRowDataBound="gvMFTransactions_RowDataBound">
                        <FooterStyle CssClass="FooterStyle" />
                        <RowStyle CssClass="RowStyle" Wrap="False" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <PagerStyle CssClass="PagerStyle" HorizontalAlign="center" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="TransactionId" HeaderText="TransactionId" Visible="false" />
                            <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Customer Name">
                                <HeaderTemplate>
                                    <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                                    <asp:TextBox ID="txtNameSearch" Text='<%# hdnCustomerNameSearch.Value %>' runat="server"
                                        CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_RMEQMultipleTransactionsView_btnCustomerSearch');" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNameHeader" runat="server" Text='<%# Eval("Customer Name").ToString() %>'
                                        ItemStyle-Wrap="false"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Scrip">
                                <HeaderTemplate>
                                    <asp:Label ID="lblScheme" runat="server" Text="Scrip"></asp:Label><br />
                                    <asp:TextBox Text='<%# hdnSchemeSearch.Value %>' ID="txtSchemeSearch" runat="server"
                                        CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_RMEQMultipleTransactionsView_btnScripSearch');" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSchemeHeader" runat="server" Text='<%# Eval("Scrip").ToString() %>'
                                        ItemStyle-Wrap="false"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Trade Account">
                                <HeaderTemplate>
                                    <asp:Label ID="lblFolio" runat="server" Text="Trade Acc No"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTradeAccNumber" runat="server" Text='<%# Eval("TradeAccNumber").ToString() %>'
                                        ItemStyle-Wrap="false"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Type">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTranType" runat="server" Text="Type"></asp:Label>
                                    <asp:DropDownList ID="ddlTranType" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                        OnSelectedIndexChanged="ddlTranType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTranTypeHeader" runat="server" Text='<%# Eval("Type").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Right">
                            </asp:BoundField>
                            <asp:BoundField DataField="Rate" HeaderText="Rate(Per Unit)" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Brokerage" HeaderText="Brokerage" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="OtherCharges" HeaderText="OtherCharges" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="STT" HeaderText="STT" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Gross Price">
                                <HeaderTemplate>
                                    <asp:Label ID="lblGrossPrice" runat="server" Text="Gross Price"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblGrossPrice" runat="server" Text='' ItemStyle-Wrap="false"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Speculative Or Delivery" HeaderText="Speculative/Delivery">
                            </asp:BoundField>
                            <asp:BoundField DataField="Portfolio Name" HeaderText="Portfolio Name"></asp:BoundField>
                        </Columns>
                    </asp:GridView>--%>
            </td>
        </tr>
    </table>
    <%-- </asp:Panel>--%>
    <%--<table width="100%">
        <tr id="trMessage" runat="server">
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server" CssClass="Error" Text="No Records Found..."></asp:Label>
            </td>
        </tr>
    </table>--%>
    <%--  </div>--%>
</body>
</html>
<%--<asp:Button ID="btnCustomerSearch" runat="server" Text="" OnClick="btnCustomerSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:Button ID="btnScripSearch" runat="server" Text="" BorderStyle="None" BackColor="Transparent"
    OnClick="btnScripSearch_Click" />--%>
<%--<asp:HiddenField ID="hdnRecordCount" runat="server" Visible="false" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnCustomerNameSearch" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSchemeSearch" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTranType" runat="server" Visible="false" />--%>
<%--<asp:HiddenField ID="hdnFolioNumber" runat="server" Visible="false" />--%>
<asp:HiddenField ID="hdnDownloadPageType" runat="server" Visible="true" />
<asp:HiddenField ID="hdnDownloadFormat" runat="server" Visible="true" />
<asp:HiddenField ID="txtParentCustomerId" runat="server" />
