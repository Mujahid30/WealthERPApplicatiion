<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewEQTransactionInputrejects.ascx.cs"
    Inherits="WealthERP.Uploads.ViewEQTransactionInputrejects" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadCodeBlock ID="radCodeBlock1" runat="server">

    <script language="javascript" type="text/javascript">


        var column = null;
        var columnName = null;


        function MenuShowing(sender, args) {

            if (column == null) return;
            if (columnName == null) return;


            var menu = sender; var items = menu.get_items();


            if (columnName == "ProcessId") {


                var i = 0;


                while (i < items.get_count()) {


                    if (!(items.getItem(i).get_value() in { 'NoFilter': '', 'Contains': '', 'EqualTo': '', 'GreaterThan': '', 'LessThan': '' })) {
                        var item = items.getItem(i);
                        if (item != null)
                            item.set_visible(false);
                    }
                    else {
                        var item = items.getItem(i);
                        if (item != null)
                            item.set_visible(true);
                    } i++;


                }


            }
            else if (DataType="System.String" || columnName == "TransactionType" || columnName == "Exchange") {

                    {
                    var j = 0;

                
                    
                    while (j < items.get_count()) {
                            
//                        alert(items.getItem(j).get_value());
                        if (!(items.getItem(j).get_value() in { 'NoFilter': '', 'Contains': '', 'DoesNotContain': '', 'StartsWith': '', 'EndsWith': '' })) {
                            var item = items.getItem(j);
                            if (item != null)
                                item.set_visible(false);
                        }
                        else {
                            var item = items.getItem(j);
                            if (item != null)
                                item.set_visible(true);
                        } j++;


                    }


                }

            }

            
            column = null;
            columnName = null;


        }


        function filterMenuShowing(sender, eventArgs) {
            column = eventArgs.get_column();
            columnName = eventArgs.get_column().get_uniqueName();
        }


    </script>

</telerik:RadCodeBlock>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<table>
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="View Equity Transaction Input Rejects"></asp:Label>
        </td>
    </tr>
  <tr>
    <td>
        <asp:ImageButton ID="btnExportFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                OnClientClick="setFormat('excel')" Height="25px" Width="25px" ></asp:ImageButton>
    </td>
 </tr>
    <tr>
        <td>
            <telerik:RadGrid ID="gvEquityInputRejects" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                AllowAutomaticInserts="false" OnNeedDataSource="gvEquityInputRejects_OnNeedDataSource">
                <ExportSettings FileName="EquityInputRejectsList" HideStructureColumns="true" ExportOnlyData="true" >
                </ExportSettings>
                <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                    
                    <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                        ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                    <Columns>
                        <%--<telerik:GridBoundColumn DataField="WERPTransactionId" AllowFiltering="false" HeaderText="WERPTransaction Id">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridBoundColumn HeaderText="RejectReason" SortExpression="RejectReason"
                            CurrentFilterFunction="Contains" AutoPostBackOnFilter="false" AllowFiltering="false"
                            DataField="RejectReason">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="ProcessId" SortExpression="ProcessId" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="false" AllowFiltering="true" DataField="ProcessId">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <%-- <telerik:GridBoundColumn HeaderText="RejectReasonCode" SortExpression="RejectReasonCode"
                            CurrentFilterFunction="Contains" AutoPostBackOnFilter="false" AllowFiltering="true"
                            DataField="RejectReasonCode" >
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>--%>
                        <%--<telerik:GridBoundColumn HeaderText="PANNum" SortExpression="PANNum" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="false" AllowFiltering="true" DataField="PANNum" >
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridBoundColumn HeaderText="TradeAccountNumber" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="false" AllowFiltering="false" DataField="TradeAccountNumber">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Scrip" SortExpression="ScripCode" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="false" AllowFiltering="false" DataField="ScripCode">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Exchange" SortExpression="Exchange" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="false" AllowFiltering="true" DataField="Exchange">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Share" SortExpression="Share" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="false" AllowFiltering="false" DataField="Share">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Price" SortExpression="Price" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="false" AllowFiltering="false" DataField="Price">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Amount" SortExpression="Amount" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="false" AllowFiltering="false" DataField="Amount">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="TransactionType" SortExpression="TransactionType"
                            AutoPostBackOnFilter="false" AllowFiltering="true" DataField="TransactionType">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <%--
                        <telerik:GridBoundColumn HeaderText="CustomerName" SortExpression="CustomerName"
                            CurrentFilterFunction="Contains" AutoPostBackOnFilter="false" AllowFiltering="false"
                            DataField="CustomerName" >
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>--%>
                        <%-- <telerik:GridBoundColumn HeaderText="TransactionTypeCode" SortExpression="TransactionTypeCode" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="false" AllowFiltering="true" DataField="TransactionTypeCode" >
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>--%>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    <ClientEvents OnFilterMenuShowing="filterMenuShowing" />
                </ClientSettings>
                <FilterMenu OnClientShown="MenuShowing" />
            </telerik:RadGrid>
        </td>
    </tr>
</table>
