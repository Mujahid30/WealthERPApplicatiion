<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineAdviserCustomerIPOHoldings.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineAdviserCustomerIPOHoldings" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            IPO Holdings
                        </td>
                        <td align="right">
                            <%--   <asp:ImageButton ID="imgexportButton" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                Visible="false" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                OnClick="btnExportData_OnClick" OnClientClick="setFormat('excel')" Height="22px"
                                Width="25px"></asp:ImageButton>--%>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td align="right">
            <asp:Label ID="lblIssueName" runat="server" CssClass="FieldName" Text="Issue Name"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlIssueName" runat="server" CssClass="cmbField" Width="300px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlProduct" runat="server" ErrorMessage="Please Select Issue Name"
                CssClass="rfvPCG" ControlToValidate="ddlIssueName" ValidationGroup="btnbasicsubmit"
                Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:Button ID="btnGo" runat="server" CssClass="PCGButton" OnClick="btnGo_OnClick"
                Text="Go" ValidationGroup="btnbasicsubmit" />
        </td>
    </tr>
</table>
<asp:Panel ID="pnlIPOIssueHoldings" runat="server" ScrollBars="Horizontal" Width="100%"
    Visible="false">
    <table width="100%" cellspacing="0" cellpadding="1">
        <tr>
            <td>
                <telerik:RadGrid ID="gvIPOIssueHoldings" runat="server" fAllowAutomaticDeletes="false"
                    EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                    ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true"
                    GridLines="none" AllowAutomaticInserts="false" Skin="Telerik" EnableHeaderContextMenu="true"
                    OnNeedDataSource="gvIPOIssueHoldings_OnNeedDataSource" AllowCustomPaging="true"
                    OnItemCommand="gvIPOIssueHoldings_OnItemCommand">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                        FileName="IPO Holdings" Excel-Format="ExcelML">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="AIM_IssueId,CO_ApplicationNumber,AIA_AllotmentDate,CO_OrderId,AIM_OpenDate"
                        Width="99%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" PageSize="20">
                        <Columns>
                            <%--   <telerik:GridTemplateColumn ShowFilterIcon="false" AllowFiltering="false" CurrentFilterFunction="Contains"
                                HeaderStyle-Width="20px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbDetails" runat="server" CommandName="ExpandCollapse" Font-Underline="False"
                                        Font-Bold="true" UniqueName="Detailslink" OnClick="btnExpandAll_Click" Font-Size="Medium"
                                        Visible="false">+</asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>--%>
                            <telerik:GridBoundColumn DataField="customername" UniqueName="customername" HeaderText="Customer Name"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                                SortExpression="customername" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIA_AllotmentDate" SortExpression="AIA_AllotmentDate"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Date of allotment" UniqueName="AIA_AllotmentDate"
                                DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HeaderStyle-Width="50px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIM_IssueName" SortExpression="AIM_IssueName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Scrip" UniqueName="AIM_IssueName" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_OrderId" SortExpression="CO_OrderId" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Order No" UniqueName="CO_OrderId" HeaderStyle-Width="40px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn DataField="CO_ApplicationNumber" SortExpression="CO_ApplicationNumber"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Application Number" UniqueName="CO_ApplicationNumber"
                                HeaderStyle-Width="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkApplicationNo" runat="server" CommandName="Select" Text='<%# Eval("CO_ApplicationNumber").ToString() %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="OpenDateTime" SortExpression="OpenDateTime" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Issue OPen Date" UniqueName="OpenDateTime" HeaderStyle-Width="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CloseDateTime" SortExpression="CloseDateTime"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Issue Close Date" UniqueName="CloseDateTime"
                                HeaderStyle-Width="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AllotedQty" SortExpression="AllotedQty" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Quantity" UniqueName="AllotedQty" HeaderStyle-Width="40px">
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
                            <%--  <telerik:GridBoundColumn DataField="OrderStep" UniqueName="OrderStep" HeaderText="Order Status"
                                SortExpression="OrderStep" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                                FilterControlWidth="80px" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false">
                                <ItemStyle Width="80px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>--%>
                            <%--  <telerik:GridTemplateColumn ShowFilterIcon="false" AllowFiltering="false" HeaderStyle-Width="2px">
                                <ItemTemplate>
                                    <tr>
                                        <td colspan="100%">
                                            <asp:Panel ID="pnlchild" runat="server" Style="display: inline" CssClass="Landscape"
                                                Width="100%" ScrollBars="Both" Visible="false">
                                                <%-- <div style="display: inline; position: relative; left: 25px;">
                                                <telerik:RadGrid ID="gvChildDetails" runat="server" AutoGenerateColumns="False" enableloadondemand="True"
                                                    PageSize="10" AllowPaging="false" EnableEmbeddedSkins="False" GridLines="None"
                                                    ShowFooter="True" PagerStyle-AlwaysVisible="true" ShowStatusBar="True" Skin="Telerik"
                                                    AllowFilteringByColumn="false" OnNeedDataSource="gvChildDetails_OnNeedDataSource">
                                                    <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames=""
                                                        AutoGenerateColumns="false" Width="100%">
                                                        <Columns>
                                                            <telerik:GridBoundColumn Visible="false" DataField="AIM_IssueId" SortExpression="AIM_IssueId"
                                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>
                                                              <telerik:GridBoundColumn Visible="false" DataField="PFISM_SchemeId" HeaderStyle-Width="60px"
                                                                            CurrentFilterFunction="Contains" HeaderText="Scheme" SortExpression="PFISM_SchemeId">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="SeriesName" SortExpression="SeriesName" AutoPostBackOnFilter="true"
                                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                                                HeaderText="SeriesName" UniqueName="SeriesName">
                                                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="MaturityDate" SortExpression="MaturityDate" AutoPostBackOnFilter="true"
                                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                                                HeaderText="Maturity date" UniqueName="MaturityDate">
                                                                <ItemStyle Width="100px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="OrderdQty" SortExpression="OrderdQty" AutoPostBackOnFilter="true"
                                                                HeaderStyle-Width="200px" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                AllowFiltering="false" HeaderText="Order Quantity" UniqueName="OrderdQty">
                                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="AllotedQty" SortExpression="AllotedQty" AutoPostBackOnFilter="true"
                                                                HeaderStyle-Width="200px" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                AllowFiltering="false" HeaderText="Alloted Quantity" UniqueName="AllotedQty">
                                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Couponrate" SortExpression="Couponrate" AutoPostBackOnFilter="true"
                                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                                                HeaderText="Coupon Rate(%)" UniqueName="Couponrate">
                                                                <ItemStyle Width="100px" HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Renewedcouponrate" AllowFiltering="false" HeaderText="Renewed Coupon Rate(%)"
                                                                UniqueName="Renewedcouponrate" SortExpression="Renewedcouponrate">
                                                                <ItemStyle Width="100px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="CallOption" AllowFiltering="false" HeaderText="Yield To Call(%)"
                                                                UniqueName="CallOption">
                                                                <ItemStyle Width="100px" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="YieldtillMaturity" AllowFiltering="false" HeaderText="Yield Till Maturity(%)"
                                                                UniqueName="YieldtillMaturity">
                                                                <ItemStyle Width="100px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Buybackfacility" AllowFiltering="false" HeaderText="Buyback facility"
                                                                UniqueName="Buybackfacility">
                                                                <ItemStyle Width="100px" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="AID_Tenure" SortExpression="AID_Tenure" AutoPostBackOnFilter="true"
                                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                                                HeaderText="Tenure (months)" UniqueName="AID_Tenure">
                                                                <ItemStyle Width="100px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>--%>
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
<asp:HiddenField ID="hdncustomerName" runat="server" />
