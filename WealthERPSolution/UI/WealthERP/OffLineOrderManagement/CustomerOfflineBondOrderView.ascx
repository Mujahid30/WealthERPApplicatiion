<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerOfflineBondOrderView.ascx.cs"
    Inherits="WealthERP.OffLineOrderManagement.CustomerOfflineBondOrderView" %>
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
                           Bond
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="80%">
    <tr>
        <td>
            <asp:Panel ID="pnlGrid" runat="server" CssClass="Landscape" Width="85%" ScrollBars="Horizontal"
                Visible="false">
                <table>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <telerik:RadGrid ID="gvBondOrderList" runat="server" GridLines="None" AutoGenerateColumns="False"
                                            PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" OnNeedDataSource="gvBondOrderList_OnNeedDataSource"
                                            ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="false"
                                            AllowAutomaticInserts="false" Height="400px">
                                            <MasterTableView DataKeyNames="COAD_Id,PAISC_AssetInstrumentSubCategoryCode"  AllowMultiColumnSorting="True"
                                                AutoGenerateColumns="false" AllowFilteringByColumn="true" EditMode="PopUp">
                                                <Columns>
                                                 <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="Action" DataField="Action"
                                                        HeaderStyle-Width="110px">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlAction" CssClass="cmbField" runat="server" EnableEmbeddedSkins="false"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChanged"
                                                                Width="110px">
                                                                <Items>
                                                                    <asp:ListItem Text="Select" Value="Select" Selected="true" />
                                                                    <asp:ListItem Text="View" Value="View" />
                                                                    <asp:ListItem Text="Edit" Value="Edit" />
                                                                </Items>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="AIM_IssueName" SortExpression="AIM_IssueName"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                        AllowFiltering="true" HeaderStyle-Width="60px" HeaderText="Issure" UniqueName="AIM_IssueName">
                                                        <ItemStyle Width="60px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="PAISC_AssetInstrumentSubCategoryName" AllowFiltering="true"
                                                        HeaderText="CategoryName" UniqueName="PAISC_AssetInstrumentSubCategoryName" SortExpression="PAISC_AssetInstrumentSubCategoryName"
                                                        ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                        HeaderStyle-Width="60px" FilterControlWidth="75px">
                                                        <ItemStyle Width="60px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="AIM_AllotmentDate" SortExpression="AIM_AllotmentDate" AutoPostBackOnFilter="true"
                                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                        HeaderStyle-Width="160px" HeaderText="AllotmentDate" UniqueName="AIM_AllotmentDate" >
                                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="COAD_Quantity" SortExpression="COAD_Quantity" AutoPostBackOnFilter="true"
                                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                        HeaderStyle-Width="160px" HeaderText="Quantity" UniqueName="COAD_Quantity" >
                                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="COAD_Price" SortExpression="COAD_Price" AutoPostBackOnFilter="true"
                                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                        HeaderStyle-Width="160px" HeaderText="Price" UniqueName="COAD_Price" >
                                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                    </telerik:GridBoundColumn>
                                                     <telerik:GridBoundColumn DataField="COAD_InterestAmount" SortExpression="COAD_InterestAmount" AutoPostBackOnFilter="true"
                                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                        HeaderStyle-Width="160px" HeaderText="Interest Rate(%)" UniqueName="COAD_InterestAmount">
                                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="WCMV_Name" SortExpression="WCMV_Name"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                        AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Frequency" UniqueName="WCMV_Name">
                                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="AIIC_InvestorCatgeoryName" AllowFiltering="true"
                                                        HeaderText="Investor CatgeoryName" UniqueName="Price" SortExpression="AIIC_InvestorCatgeoryName"
                                                        ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                        HeaderStyle-Width="80px" FilterControlWidth="75px" >
                                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="AID_IssueDetailName" SortExpression="AID_IssueDetailName"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                        AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Series" UniqueName="AID_IssueDetailName" Visible="false">
                                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="COAD_MaturityDate" SortExpression="COAD_MaturityDate" AutoPostBackOnFilter="true"
                                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                        HeaderStyle-Width="160px" HeaderText="MaturityDate" UniqueName="COAD_MaturityDate" Visible="false">
                                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                    </telerik:GridBoundColumn>
                                                     <telerik:GridBoundColumn DataField="currentValue" SortExpression="currentValue"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                        AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Current Value"
                                                        UniqueName="currentValue">
                                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="COAD_MaturityAmount" SortExpression="COAD_MaturityAmount"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                        AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Maturity Amount"
                                                        UniqueName="COAD_MaturityAmount">
                                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="COAD_OrderDate" SortExpression="COAD_OrderDate"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                        AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Order Date"
                                                        UniqueName="COAD_OrderDate" Visible="false">
                                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
