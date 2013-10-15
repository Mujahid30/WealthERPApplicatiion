<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDIssueHoldings.ascx.cs" Inherits="WealthERP.OnlineOrderManagement.NCDIssueHoldings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />

<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<style type="text/css">
    .table
    {
        border: 1px solid orange;
    }
    .leftLabel
    {
        width: 15%;
        text-align: right;
    }
    .rightData
    {
        width: 18%;
        text-align: left;
    }
    .rightDataTwoColumn
    {
        width: 25%;
        text-align: left;
    }
    .rightDataFourColumn
    {
        width: 50%;
        text-align: left;
    }
    .rightDataThreeColumn
    {
        width: 41%;
        text-align: left;
    }
    .tdSectionHeading
    {
        padding-bottom: 6px;
        padding-top: 6px;
        width: 100%;
    }
    .divSectionHeading table td span
    {
        padding-bottom: 5px !important;
    }
    .fltlft
    {
        float: left;
        padding-left: 3px;
        width: 20%;
    }
    .divCollapseImage
    {
        float: left;
        padding-left: 5px;
        width: 2%;
        float: right;
        text-align: right;
        cursor: pointer;
        cursor: hand;
    }
    .imgCollapse
    {
        background: Url(../Images/Section-Expand.png);
        cursor: pointer;
        cursor: hand;
    }
    .imgExpand
    {
        background: Url(../Images/Section-Collapse.png) no-repeat left top;
        cursor: pointer;
        cursor: hand;
    }
    .fltlftStep
    {
        float: left;
    }
    .StepOneContentTable, .StepTwoContentTable, .StageRequestTable, .StepThreeContentTable, .StepFourContentTable
    {
        width: 100%;
    }
    .SectionBody
    {
        width: 100%;
    }
    .collapse
    {
        text-align: right;
    }
    .divStepStatus
    {
        float: left;
        padding-left: 2px;
        padding-right: 5px;
    }
</style>

<asp:UpdatePanel ID="upBHGrid" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td>
                    <div class="divPageHeading">
                        <table width="100%">
                            <tr>
                                <td align="left">
                                    Bonds Holding
                                </td>
                                <td align=right >
                                    <asp:ImageButton ID="ibtExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="ibtExportSummary_OnClick"
                                        Height="25px" Width="25px"></asp:ImageButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>        
        
        <table id="tblCommissionStructureRule" runat="server" width="100%">
            <tr>
                <td>
                    <asp:Panel ID="pnlGrid" runat="server" CssClass="Landscape" Width="100%" ScrollBars="Horizontal">
                        <table width="100%">
                            <tr>
                                <td>
                                   <telerik:RadGrid ID="gvBHList" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                    onpageindexchanged="gvBHList_PageIndexChanged" AllowAutomaticInserts="false" >
                    <ExportSettings FileName="Details" HideStructureColumns="true" ExportOnlyData="true">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="BHScrip" Width="100%" AllowMultiColumnSorting="True"
                        AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridTemplateColumn ItemStyle-Width="80Px" AllowFiltering="false" Visible="false" HeaderText="Action">
                                <%--<ItemTemplate>
                                    <asp:DropDownList ID="ddlMenu" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                        OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged" EnableViewState="True">
                                        <asp:ListItem>Select </asp:ListItem>
                                        <asp:ListItem Text="View profile" Value="View profile">View profile</asp:ListItem>
                                        <asp:ListItem Text="Edit Profile" Value="Edit Profile">Edit Profile</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>--%>
                                <%-- <asp:TemplateField HeaderText="Action" ItemStyle-Width="80Px">--%>
                                <ItemTemplate>
                                    <telerik:RadComboBox ID="ddlMenu"
                                        CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" Skin="Telerik" 
                                        AllowCustomText="true" Width="120px" AutoPostBack="true" OnSelectedIndexChanged="ddlMenu_OnSelectedIndexChanged">
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="~/Images/Select.png" Text="Select" Value="0" Selected="true">
                                            </telerik:RadComboBoxItem>                                           
                                            <telerik:RadComboBoxItem ImageUrl="~/Images/RecordEdit.png" Text="Edit" Value="Edit"
                                                runat="server"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </ItemTemplate>
                                <%-- </asp:TemplateField>--%>
                            </telerik:GridTemplateColumn>
                            <%-- <telerik:GridTemplateColumn AllowFiltering="true">
                                <ItemTemplate>
                                    <asp:Label CssClass="label" ID="lblContainer" runat="server" Text='<%# Eval("RMName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridTemplateColumn> --%>
                            <telerik:GridBoundColumn DataField="BHScrip" SortExpression="BHScrip" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Scrip" UniqueName="BHScrip">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="BHDateofallotment" SortExpression="BHDateofallotment" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Date of allotment" UniqueName="BHDateofallotment">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="BHMaturitydate" SortExpression="BHMaturitydate" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Maturity date" UniqueName="BHMaturitydate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn DataField="RMName" AllowFiltering="true" HeaderText=""
                                UniqueName="ActiveLevel">
                                <FilterTemplate>
                                <asp:DropDownList Visible="true" runat="server" ID="" OnSelectedIndexChanged="ddlNameFilter_OnSelectedIndexChanged" AutoPostBack="true"  CssClass="GridViewCmbField"></asp:DropDownList>
                                </FilterTemplate>
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                           <%-- <telerik:GridBoundColumn DataField="StaffCode" AllowFiltering="false" HeaderText="Staffcode"
                                UniqueName="ActiveLevel">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn DataField="BHCoupon" SortExpression="BHCoupon" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Coupon" UniqueName="BHCoupon">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn Visible="false" DataField="BHInteresttype" SortExpression="BHInteresttype" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Interest type" UniqueName="BHInteresttype"> 
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="BHRenewedcouponrate" AllowFiltering="false" HeaderText="Renewed coupon rate"
                                UniqueName="BHRenewedcouponrate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                               <telerik:GridBoundColumn DataField="BHYieldtillcall" AllowFiltering="false" HeaderText="Yield till call"
                                UniqueName="BHYieldtillcall">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="BHYieldtillMaturity" AllowFiltering="false" HeaderText="Yield till Maturity"
                                UniqueName="BHYieldtillMaturity">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="BHCallOption" AllowFiltering="false" HeaderText="Call Option"
                                UniqueName="BHCallOption">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="BHBuybackfacility" AllowFiltering="false" HeaderText="Buyback facility"
                                UniqueName="BHBuybackfacility">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="BHTradeableatexchange" AllowFiltering="false" HeaderText="Tradeable at exchange"
                                UniqueName="BHTradeableatexchange">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="BHTenure" SortExpression="BHTenure" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Tenure (months)" UniqueName="BHTenure">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="BHQtyinholdings" AllowFiltering="false" HeaderText="Qty in holdings"
                                UniqueName="BHQtyinholdings">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="BHValueofholdings" AllowFiltering="false" HeaderText="Value of holdings"
                                UniqueName="BHValueofholdings">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>                            
                            <telerik:GridBoundColumn DataField="BHCurrentRatings" AllowFiltering="false" HeaderText="Current Ratings( agency �)"
                                UniqueName="BHCurrentRatings">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
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
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="ibtExportSummary" />
    </Triggers>
</asp:UpdatePanel>