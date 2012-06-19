<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewEQTransactionInputrejects.ascx.cs" Inherits="WealthERP.Uploads.ViewEQTransactionInputrejects" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<table>
    <tr>
        <td>
            <telerik:RadGrid ID="gvEquityInputRejects" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                AllowAutomaticInserts="false" OnNeedDataSource="gvEquityInputRejects_OnNeedDataSource">
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true">
                </ExportSettings>
                <MasterTableView Width="100%" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false" CommandItemDisplay="Top">
                    <CommandItemSettings  ShowExportToWordButton="true" ShowExportToExcelButton="true"
                        ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="true" />
                    <Columns>
                        <%--<telerik:GridBoundColumn DataField="WERPTransactionId" AllowFiltering="false" HeaderText="WERPTransaction Id">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridBoundColumn HeaderText="RejectReason" SortExpression="RejectReason" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="false" AllowFiltering="false" DataField="RejectReason" >
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="ProcessId" SortExpression="ProcessId" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="false" AllowFiltering="true" DataField="ProcessId" >
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
                        <telerik:GridBoundColumn HeaderText="TradeAccountNumber"  CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="false" AllowFiltering="false" DataField="TradeAccountNumber" >
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Scrip" SortExpression="ScripCode" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="false" AllowFiltering="false" DataField="ScripCode">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Exchange" SortExpression="Exchange" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="false" AllowFiltering="true" DataField="Exchange" >
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Share" SortExpression="Share" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="false" AllowFiltering="false" DataField="Share" >
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn HeaderText="Price" SortExpression="Price" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="false" AllowFiltering="false" DataField="Price" >
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn HeaderText="Amount" SortExpression="Amount" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="false" AllowFiltering="false" DataField="Amount" >
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="TransactionType" SortExpression="TransactionType" CurrentFilterFunction="Contains"
                            AutoPostBackOnFilter="false" AllowFiltering="true" DataField="TransactionType">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn><%--
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
                </ClientSettings>
            </telerik:RadGrid>
        </td>
    </tr>
</table>
