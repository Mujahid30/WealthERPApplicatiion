<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerIPOOrderBook.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.CustomerIPOOrderBook" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scriptmanager" runat="server">
</asp:ScriptManager>
<asp:Panel ID="pnlIPOBook" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal"
    Visible="true" Style="float: left; padding-top: 20px; padding-left: 20px;">
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="RadGridIssueIPOBook" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" Width="102%" ClientSettings-AllowColumnsReorder="true"
                    AllowAutomaticInserts="false">
                    <%--  OnNeedDataSource="gvOrderList_OnNeedDataSource" OnItemDataBound="gvOrderList_ItemDataBound"--%>
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="IPOIssueList">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="CO_OrderId,C_CustomerId,PAG_AssetGroupCode,CO_OrderDate"
                        AllowFilteringByColumn="true" Width="102%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                        CommandItemDisplay="None">
                        <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                            ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="AIM_IssueName" SortExpression="AIM_IssueName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Scrip Name" UniqueName="AIM_IssueName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_OrderDate" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                AllowFiltering="true" HeaderText="Transaction Date" UniqueName="CO_OrderDate"
                                SortExpression="CO_OrderDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="120px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_OrderId" AllowFiltering="true" HeaderText="Transaction No."
                                UniqueName="CO_OrderId" SortExpression="CO_OrderId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="75px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ApplicationNo" AllowFiltering="true" HeaderText="Application No."
                                UniqueName="CO_ApplicationNo" SortExpression="CO_ApplicationNo" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"
                                FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IssueStartDateANDTime" SortExpression="IssueStartDateANDTime"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HeaderText="Start Date"
                                UniqueName="IssueStartDateANDTime" HeaderStyle-Width="77px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IssueEndDateANDTime" SortExpression="IssueEndDateANDTime"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="End Date" UniqueName="IssueEndDateANDTime"
                                HeaderStyle-Width="77px" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Amounttoinvest" AllowFiltering="true" HeaderText="Amount to invest"
                                UniqueName="Amounttoinvest" SortExpression="Amounttoinvest" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"
                                FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WOS_OrderStep" AllowFiltering="true" HeaderText="Status"
                                HeaderStyle-Width="70px" UniqueName="WOS_OrderStep" SortExpression="WOS_OrderStep"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WES_Code" AllowFiltering="true" HeaderText="ExtractionStatus"
                                HeaderStyle-Width="70px" UniqueName="WES_Code" SortExpression="WES_Code" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" Visible="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridEditCommandColumn Visible="true" HeaderStyle-Width="60px" UniqueName="MarkAsReject"
                                EditText="Cancel" CancelText="Cancel" UpdateText="OK" HeaderText="Cancel">
                            </telerik:GridEditCommandColumn>
                            <%-- <telerik:GridBoundColumn DataField="CO_OrderDate" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}"
                                AllowFiltering="true" HeaderText="Order Date/Time" UniqueName="CO_OrderDate"
                                SortExpression="CO_OrderDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_OrderId" AllowFiltering="true" HeaderText="Transaction No./Order No."
                                UniqueName="CO_OrderId" SortExpression="CO_OrderId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="75px" FilterControlWidth="50px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ApplicationNumber" HeaderText="Application No"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CO_ApplicationNumber"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="100px" UniqueName="CO_ApplicationNumber" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIM_IssueName" HeaderText="Fund Name" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="AIM_IssueName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                                UniqueName="AIM_IssueName" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn ItemStyle-Width="60px" AllowFiltering="false" HeaderText="Action"
                                Visible="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Edit" ImageUrl="~/Images/Buy-Button.png" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>--%>
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
