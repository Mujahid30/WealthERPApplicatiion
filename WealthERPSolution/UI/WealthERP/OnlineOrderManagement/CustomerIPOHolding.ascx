<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerIPOHolding.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.CustomerIPOHolding" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table width="100%">
                    <tr>
                        <td align="left">
                            IPO/FPO Holding
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="ibtExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="ibtExportSummary_OnClick"
                                Height="25px" Width="25px" Visible="false"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<asp:Panel ID="pnlGrid" runat="server" CssClass="Landscape" Width="99%" ScrollBars="Horizontal">
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="gvIPOHolding" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                    AllowAutomaticInserts="false" OnNeedDataSource="gvIPOHolding_OnNeedDataSource"
                    OnItemCommand="gvIPOHolding_OnItemCommand" OnItemDataBound="gvIPOHolding_OnItemDataBound">
                    <ExportSettings FileName="Details" HideStructureColumns="true" ExportOnlyData="true">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="AIM_IssueId,AIM_IssueName,CO_ApplicationNumber,CO_OrderDate,AIA_AllotmentDate,CO_OrderId"
                        Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridBoundColumn Visible="false" DataField="AIM_IssueId" SortExpression="AIM_IssueId"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                           
                            <telerik:GridBoundColumn DataField="AIM_IssueName" SortExpression="AIM_IssueName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Issue Name" UniqueName="AIM_IssueName" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="AIA_AllotmentDate" SortExpression="AIA_AllotmentDate"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Date of allotment" UniqueName="AIA_AllotmentDate"
                                DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HeaderStyle-Width="50px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_OrderId" Visible="false" SortExpression="CO_OrderId" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Order No" UniqueName="CO_OrderId" HeaderStyle-Width="40px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn DataField="CO_ApplicationNumber" SortExpression="CO_ApplicationNumber"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Application Number" UniqueName="CO_ApplicationNumber"
                                HeaderStyle-Width="60px" Visible="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkApplicationNo" runat="server" CommandName="Select" Text='<%# Eval("CO_ApplicationNumber").ToString() %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="OpenDateTime" SortExpression="OpenDateTime" AutoPostBackOnFilter="true"
                                DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Issue Open Date" UniqueName="OpenDateTime"
                                HeaderStyle-Width="60px" Visible="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CloseDateTime" SortExpression="CloseDateTime"
                                DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AllowFiltering="false" HeaderText="Issue Close Date" UniqueName="CloseDateTime"
                                HeaderStyle-Width="60px" Visible="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AllotedQty" SortExpression="AllotedQty" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText=" Alloted Quantity" UniqueName="AllotedQty" HeaderStyle-Width="40px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIA_Price" SortExpression="AIA_Price" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Purchase Price" UniqueName="AIA_Price" HeaderStyle-Width="50px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="true" DataField="Amount" SortExpression="Amount"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Alloted Value" UniqueName="Amount" HeaderStyle-Width="50px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                Visible="false" UniqueName="Action" HeaderText="Action">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBuy" runat="server" CommandName="Buy" Enabled="false" ImageUrl="~/Images/Buy-Button.png"
                                        ToolTip="BUY" />&nbsp;
                                    <asp:ImageButton ID="imgSell" runat="server" CommandName="Sell" Visible="false" ImageUrl="~/Images/Sell-Button.png"
                                        ToolTip="SELL" />&nbsp;
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="true" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
