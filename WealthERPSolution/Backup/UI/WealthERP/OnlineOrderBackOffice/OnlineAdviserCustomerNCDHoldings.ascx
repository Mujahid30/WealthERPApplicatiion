<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineAdviserCustomerNCDHoldings.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineAdviserCustomerNCDHoldings" %>
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
                            NCD Holdings
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
        <td>
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
<asp:Panel ID="pnlNCDIssueHoldings" runat="server" ScrollBars="Horizontal" Width="99%"
    Visible="false">
    <table id="tblCommissionStructureRule" runat="server" width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="gvNCDIssueHoldings" runat="server" fAllowAutomaticDeletes="false"
                    EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                    ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true"
                    GridLines="none" AllowAutomaticInserts="false" Skin="Telerik" EnableHeaderContextMenu="true"
                    OnNeedDataSource="gvNCDIssueHoldings_OnNeedDataSource" AllowCustomPaging="true"
                    OnItemCommand="gvNCDIssueHoldings_ItemCommand">
                    <MasterTableView DataKeyNames="AIM_IssueId" Width="100%" AllowMultiColumnSorting="True"
                        AutoGenerateColumns="false" PageSize="20">
                        <Columns>
                            <telerik:GridTemplateColumn ShowFilterIcon="false" AllowFiltering="false" CurrentFilterFunction="Contains"
                                HeaderStyle-Width="20px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbDetails" runat="server" CommandName="ExpandCollapse" Font-Underline="False"
                                        Font-Bold="true" UniqueName="Detailslink" OnClick="btnExpandAll_Click" Font-Size="Medium">+</asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="customername" UniqueName="customername" HeaderText="Customer Name"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="200px"
                                SortExpression="customername" FilterControlWidth="150px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="200px" HorizontalAlign="Left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIM_IssueName" UniqueName="AIM_IssueName" HeaderText="Issue Name"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="false" HeaderStyle-Width="150px"
                                SortExpression="AIM_IssueName" FilterControlWidth="130px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="200px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIM_AllotmentDate" UniqueName="AIM_AllotmentDate"
                                HeaderText="Alloted Date" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                AllowFiltering="false" HeaderStyle-Width="100px" SortExpression="AIM_AllotmentDate"
                                FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AllotedQty" UniqueName="AllotedQty" HeaderText="Alloted Quantity"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="false" HeaderStyle-Width="67px"
                                SortExpression="AllotedQty" FilterControlWidth="50px" CurrentFilterFunction="Contains"
                                Visible="true">
                                <ItemStyle Width="67px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="OrderedQty" UniqueName="OrderedQty" HeaderText="Order Quantity"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="false" HeaderStyle-Width="80px"
                                SortExpression="OrderedQty" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="70px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="true" DataField="HoldingAmount" UniqueName="HoldingAmount"
                                HeaderText="Holding Amount" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                AllowFiltering="false" HeaderStyle-Width="100px" SortExpression="HoldingAmount"
                                FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="OrderStep" UniqueName="OrderStep" HeaderText="Order Status"
                                SortExpression="OrderStep" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                                FilterControlWidth="80px" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false">
                                <ItemStyle Width="80px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn ShowFilterIcon="false" AllowFiltering="false" HeaderStyle-Width="2px">
                                <ItemTemplate>
                                    <tr>
                                        <td colspan="100%">
                                            <asp:Panel ID="pnlgvChildDetails" runat="server" Style="display: inline" CssClass="Landscape"
                                                Width="100%" ScrollBars="Both" Visible="false">
                                                <%-- <div style="display: inline; position: relative; left: 25px;">--%>
                                                <telerik:RadGrid ID="gvChildDetails" runat="server" AutoGenerateColumns="False" enableloadondemand="True"
                                                    PageSize="10" AllowPaging="false" EnableEmbeddedSkins="False" GridLines="None"
                                                    ShowFooter="True" PagerStyle-AlwaysVisible="true" ShowStatusBar="True" Skin="Telerik"
                                                    AllowFilteringByColumn="false" OnNeedDataSource="gvChildDetails_OnNeedDataSource">
                                                    <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="AIM_IssueId"
                                                        AutoGenerateColumns="false" Width="100%">
                                                        <Columns>
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
                                                            </telerik:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                                <%-- </div>--%>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="hdncustomerName" runat="server" />

