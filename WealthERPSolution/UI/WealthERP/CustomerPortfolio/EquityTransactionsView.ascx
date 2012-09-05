<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EquityTransactionsView.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.EquityTransactionsView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../General/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<script language="javascript" type="text/javascript">
    function FilterRows(sender, args) {
        alert("hi   ");
        var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
        alert(tableView);
        tableView.filter("TradeDate", args.get_item().get_value(), "EqualTo");
    }
</script>

<script language="javascript" type="text/javascript">
    function Print_Click(div, btnID) {
        var ContentToPrint = document.getElementById(div);
        var myWindowToPrint = window.open('', '', 'width=200,height=100,toolbar=0,scrollbars=0,status=0,resizable=0,location=0,directories=0');
        myWindowToPrint.document.write(document.getElementById(div).innerHTML);
        myWindowToPrint.document.close();
        myWindowToPrint.focus();
        myWindowToPrint.print();
        myWindowToPrint.close();

        var btn = document.getElementById(btnID);
        btn.click();
    } function DownloadScript() {
        if (document.getElementById('<%= gvEquityTransactions.ClientID %>') == null) {
            alert("No records to export");
            return false;
        }

        btn.click();
    }

    function setPageType(pageType) {
        document.getElementById('<%= hdnDownloadPageType.ClientID %>').value = pageType;
    }
    function AferExportAll(btnID) {
        var btn = document.getElementById(btnID);
        btn.click();
    }
    function checkDate(sender, args) {

        var selectedDate = new Date();
        selectedDate = sender._selectedDate;

        var todayDate = new Date();
        var msg = "";

        if (selectedDate > todayDate) {
            sender._selectedDate = todayDate;
            sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            alert("Warning! - Date Cannot be in the future");
        }
    }
</script>

<table style="width: 100%;" class="TableBackground" runat="server">
    <tr>
        <td colspan="2">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            View Equity Transactions
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="imgBtnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('excel')"
                                Height="20px" Width="25px" OnClick="imgBtnGvEquityTransactions_OnClick" Visible="false">
                            </asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
    </tr>
    <tr>
    </tr>
    <tr>
    </tr>
    <tr>
        <td class="rightField">
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
            <telerik:RadComboBox ID="ddlPortfolio" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged"
                CssClass="cmbField" EnableEmbeddedSkins="false" Skin="Telerik" AllowCustomText="true"
                Width="120px">
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
    </tr>
</table>
<table class="TableBackground">
    <tr>
        <td>
            <asp:Label ID="lblFromTran" Text="From" CssClass="Field" runat="server" />
        </td>
        <td>
            <telerik:RadDatePicker ID="txtFromTran" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput EmptyMessage="dd/mm/yyyy" runat="server" DisplayDateFormat="d/M/yyyy"
                    DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
        </td>
        <td>
            <asp:Label ID="lblToTran" Text="To" CssClass="Field" runat="server" />
        </td>
        <td>
            <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtToTran"
                ErrorMessage="To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                ControlToCompare="txtFromTran" CssClass="cvPCG" ValidationGroup="btnSubmit" Display="Dynamic">
            </asp:CompareValidator>
            <telerik:RadDatePicker ID="txtToTran" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput runat="server" EmptyMessage="dd/mm/yyyy" DisplayDateFormat="d/M/yyyy"
                    DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
        </td>
        <td>
            <asp:Button ID="btnViewTran" runat="server" CssClass="PCGButton" Text="Go" OnClick="btnViewTran_Click" />
        </td>
    </tr>
</table>
<table id="ErrorMessage" align="center" runat="server" visible="false">
    <tr>
        <td>
            <div class="failure-msg" align="center">
                No Records found.....
            </div>
        </td>
    </tr>
</table>
<br />
<div id="tbl" runat="server">
    <asp:Panel ID="Panel1" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
        <%-- <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td>--%>
        <telerik:RadGrid ID="gvEquityTransactions" runat="server" GridLines="None" AutoGenerateColumns="False"
            PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
            Skin="Telerik" EnableEmbeddedSkins="false" Width="1090px" AllowFilteringByColumn="true"
            AllowAutomaticInserts="false" ExportSettings-FileName="Equity Transaction Details"
            OnNeedDataSource="gvEquityTransactions_OnNeedDataSource">
            <ExportSettings HideStructureColumns="true">
            </ExportSettings>
            <MasterTableView DataKeyNames="TransactionId" Width="100%" AllowMultiColumnSorting="True"
                AutoGenerateColumns="false" CommandItemDisplay="None">
                <Columns>
                    <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action">
                        <ItemTemplate>
                            <asp:LinkButton OnClick="btnViewDetails_OnClick" CommandName="Select" ID="btnViewDetails"
                                Text="View Details" runat="server" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn FooterText="Grand Total" HeaderText="Trade Account No."
                        DataField="TradeAccountNum" UniqueName="TradeAccountNum" SortExpression="TradeAccountNum"
                        AllowFiltering="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                        ShowFilterIcon="false">
                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Scrip Name" DataField="Scheme Name" UniqueName="Scheme Name"
                        SortExpression="Scheme Name" AllowFiltering="true" CurrentFilterFunction="Contains"
                        AutoPostBackOnFilter="true" ShowFilterIcon="false">
                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Transaction Type" DataField="Transaction Type"
                        UniqueName="Transaction Type" SortExpression="Transaction Type" AutoPostBackOnFilter="true"
                        AllowFiltering="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Exchange" DataField="Exchange" UniqueName="Exchange"
                        SortExpression="Exchange" AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                        ShowFilterIcon="false">
                        <ItemStyle Width="50px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="TradeDate" SortExpression="TradeDate" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" AllowFiltering="true" HeaderText="TradeDate  (dd/mm/yyyy)"
                        UniqueName="TradeDate" DataFormatString="{0:d}" ShowFilterIcon="false">
                        <ItemStyle Width="110px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        <FilterTemplate>
                            <telerik:RadDatePicker Width="250px" ID="calFilter" runat="server">
                            </telerik:RadDatePicker>
                        </FilterTemplate>
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn AllowFiltering="false" DataField="Quantity" HeaderText="No of Shares" ItemStyle-HorizontalAlign="Right"
                        SortExpression="Quantity" DataFormatString="{0:N2}" Aggregate="Sum">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Rate (Rs)" DataField="Rate" UniqueName="Rate"
                        SortExpression="Rate" AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"
                        Aggregate="Sum">
                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Brokerage (Rs)" DataField="Brokerage" UniqueName="Brokerage"
                        SortExpression="Brokerage" AutoPostBackOnFilter="true" AllowFiltering="false"
                        ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right"
                        DataFormatString="{0:N2}" Aggregate="Sum">
                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="OtherCharges (Rs)" DataField="OtherCharges"
                        UniqueName="OtherCharges" SortExpression="OtherCharges" AutoPostBackOnFilter="true"
                        AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                        FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" Aggregate="Sum">
                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="TradeTotal (Rs)" DataField="TradeTotal" UniqueName="TradeTotal"
                        SortExpression="TradeTotal" AutoPostBackOnFilter="true" AllowFiltering="false"
                        ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right"
                        DataFormatString="{0:N2}" Aggregate="Sum">
                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        <FooterStyle />
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                <Resizing AllowColumnResize="true" />
            </ClientSettings>
        </telerik:RadGrid>
        <%-- </td>
        </tr>
    </table>--%>
    </asp:Panel>
    <%-- <table width="100%">
        <tr id="trPager" runat="server">
            <td>
                <uc1:Pager ID="Pager1" runat="server" />
            </td>
        </tr>
    </table>--%>
</div>
<%--<asp:Button ID="btnScripSearch" runat="server" Text="" OnClick="btnScripSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:Button ID="btnTradeNumSearch" runat="server" Text="" OnClick="btnTradeNumSearch_Click"
    BorderStyle="None" BackColor="Transparent" />--%>
<asp:HiddenField ID="hdnRecordCount" runat="server" Visible="false" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" Visible="false" />
<asp:HiddenField ID="hdnScripFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTradeNum" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTranType" runat="server" Visible="false" />
<asp:HiddenField ID="hdnExchange" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTradeDate" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSort" runat="server" Visible="false" Value="Name ASC" />
<asp:HiddenField ID="hdnDownloadPageType" runat="server" Visible="true" />
