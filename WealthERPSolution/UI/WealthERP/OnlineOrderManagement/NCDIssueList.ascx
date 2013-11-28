<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDIssueList.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.NCDIssueList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script type="text/javascript" language="javascript">
    function RowClicked(sender, args) {
        var grid = sender;
        var MasterTable = grid.get_masterTableView();
        var row = MasterTable.get_dataItems()[args.get_itemIndexHierarchical()];
        row.set_expanded(true);
    }

    function divexpandcollapse(divname) {
        var div = document.getElementById(divname);
        var img = document.getElementById('img' + divname);

        if (div.style.display == "none") {
            div.style.display = "inline";
            img.src = "Images/minus.gif";
        }
        else {
            div.style.display = "none";
            img.src = "Images/plus.gif";

        }

    } 
</script>

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
<%--<asp:UpdatePanel ID="upCMGrid" runat="server">
    <ContentTemplate>--%>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table width="100%">
                    <tr>
                        <td align="left">
                            NCD Issue List
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="ibtExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" Height="25px"
                                Visible="false" OnClick="ibtExportSummary_OnClick" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<%--<table width="100%">
    <tr>
        <td>
            <asp:Label ID="lbl" Text="Sell / Buy Bond:" CssClass="FieldName" runat="server">
            </asp:Label>
            <asp:DropDownList ID="ddlListOfBonds" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <%--<td>
            <asp:DropDownList ID="ddlListOfBonds" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>--%>
<%-- <td>
        </td>
        <td>
            <asp:Button ID="btnEquityBond" runat="server" Text="Purchase Bonds" OnClick="btnEquityBond_Click" />
        </td>
    </tr>
</table>--%>
<asp:Panel ID="pnlchild" runat="server" CssClass="Landscape" Width="100%" ScrollBars="Horizontal">
    <table id="tblCommissionStructureRule" runat="server" width="100%">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <telerik:RadGrid ID="gvCommMgmt" runat="server" AllowSorting="True" enableloadondemand="True"
                                PageSize="10" AllowPaging="True" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                                GridLines="None" ShowFooter="True" PagerStyle-AlwaysVisible="true" ShowStatusBar="True"
                                Skin="Telerik" AllowFilteringByColumn="false" OnItemDataBound="gvCommMgmt_ItemDataBound"
                                OnNeedDataSource="gvCommMgmt_OnNeedDataSource">
                                <mastertableview allowmulticolumnsorting="True" allowsorting="true" datakeynames="AIM_IssueId,AIM_SchemeName"
                                    autogeneratecolumns="false" width="100%">
                                    <Columns>
                                        <telerik:GridTemplateColumn>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbDetails" runat="server" CommandName="ExpandCollapse" Font-Underline="False"
                                                    Font-Bold="true" UniqueName="Detailslink" OnClick="btnExpandAll_Click" Font-Size="Medium">+</asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="AIM_SchemeName" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Scrip Name" UniqueName="AIM_SchemeName"
                                            SortExpression="AIM_SchemeName">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Visible="false" DataField="AIM_IssueId" HeaderStyle-Width="60px"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="Scrip ID" UniqueName="AIM_IssueId" SortExpression="AIM_IssueId">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width=" " Wrap="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Visible="false" DataField="PI_IssuerId" HeaderStyle-Width="60px"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="Scrip ID" UniqueName="PI_IssuerId" SortExpression="PI_IssuerId">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width=" " Wrap="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Visible="false" DataField="PI_IssuerCode" HeaderStyle-Width="70px" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Issuer" UniqueName="PI_IssuerCode"
                                            SortExpression="PI_IssuerCode">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_NatureOfBond" HeaderStyle-Width="100px" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Type" UniqueName="AIM_NatureOfBond"
                                            SortExpression="AIM_NatureOfBond">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn DataField="AIM_Rating" HeaderStyle-Width="70px" HeaderText="Rating"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            UniqueName="AIM_Rating" Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                          <telerik:GridBoundColumn DataField="AIM_BestBidQuantity" HeaderStyle-Width="120px" HeaderText="Minimum Qty"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:N0}"
                                            UniqueName="AIM_BestBidQuantity" Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_FaceValue" HeaderStyle-Width="80px" HeaderText="Face Value"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            UniqueName="FaceValue" Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>                                       
                                        <telerik:GridBoundColumn DataField="AID_MinApplication" HeaderStyle-Width="110px"
                                            HeaderText="Minimum Application Amount" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true" UniqueName="AID_MinApplication" Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>                                      
                                        <telerik:GridBoundColumn DataField="AIM_TradingInMultipleOf" HeaderStyle-Width="110px"
                                            HeaderText="Multiples allowed" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true" UniqueName="AIM_TradingInMultipleOf" Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn DataField="AIM_OpenDate" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                            HeaderStyle-Width="110px" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true" HeaderText="Start Date" SortExpression="AIM_OpenDate"
                                            UniqueName="AIM_OpenDate">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridDateTimeColumn DataField="AIM_CloseDate" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                            HeaderStyle-Width="110px" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true" HeaderText="End Date" UniqueName="AIM_CloseDate" SortExpression="AIM_CloseDate">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn DataField="IsDematFacilityAvail" HeaderStyle-Width="110px"
                                            HeaderText="Demat Facility" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true" UniqueName="IsDematFacilityAvail" Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="110px"
                                            UniqueName="Action" HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="llPurchase" runat="server" OnClick="llPurchase_Click" Text="Purchase"></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn>
                                            <ItemTemplate>
                                                <tr>
                                                    <td colspan="100%">
                                                        <asp:Panel ID="pnlchild" runat="server" Style="display: inline" CssClass="Landscape"
                                                            Width="100%" ScrollBars="Both" Visible="false">
                                                            <%-- <div style="display: inline; position: relative; left: 25px;">--%>
                                                            <telerik:RadGrid ID="gvChildDetails" runat="server" AutoGenerateColumns="False" enableloadondemand="True"
                                                                PageSize="10" AllowPaging="false" EnableEmbeddedSkins="False" GridLines="None"
                                                                ShowFooter="True" PagerStyle-AlwaysVisible="true" ShowStatusBar="True" Skin="Telerik"
                                                                AllowFilteringByColumn="false" OnNeedDataSource="gvChildDetails_OnNeedDataSource">
                                                                <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="AIM_IssueId"
                                                                    AutoGenerateColumns="false" Width="100%">
                                                                    <Columns>
                                                                        <%--  <telerik:GridBoundColumn Visible="false" DataField="PFISM_SchemeId" HeaderStyle-Width="60px"
                                                                            CurrentFilterFunction="Contains" HeaderText="Scheme" SortExpression="PFISM_SchemeId">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>--%>
                                                                        <telerik:GridBoundColumn DataField="AID_Sequence" HeaderStyle-Width="60px" CurrentFilterFunction="Contains"
                                                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Series" UniqueName="AID_Sequence"
                                                                            SortExpression="AID_Sequence">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn Visible="false" DataField="AIM_IssueId" HeaderStyle-Width="60px"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                            HeaderText="Scrip ID" UniqueName="AIM_IssueId" SortExpression="AIM_IssueId">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width=" " Wrap="true" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn Visible="false" DataField="PI_IssuerId" HeaderStyle-Width="70px"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                            HeaderText="Issuer Id" UniqueName="PI_IssuerId" SortExpression="PI_IssuerId">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="PI_IssuerCode" HeaderStyle-Width="70px" CurrentFilterFunction="Contains"
                                                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Issuer Id" UniqueName="PI_IssuerCode"
                                                                            SortExpression="PI_IssuerCode">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn Visible="false" DataField="AID_IssueDetailId" HeaderStyle-Width="60px"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                            HeaderText="Series" UniqueName="AID_IssueDetailId" SortExpression="AID_IssueDetailId">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="AIM_FaceValue" HeaderStyle-Width="80px" HeaderText="Face Value"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                            UniqueName="AIM_FaceValue" Visible="true">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="AID_Tenure" HeaderStyle-Width="70px" CurrentFilterFunction="Contains"
                                                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Tenure (Months)"
                                                                            UniqueName="AID_Tenure" SortExpression="AID_Tenure">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="true" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="AID_CouponFreq" HeaderStyle-Width="85px" CurrentFilterFunction="Contains"
                                                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Frequency of Coupon Payment"
                                                                            UniqueName="AID_CouponFreq" SortExpression="AID_CouponFreq">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="AID_CouponRate" HeaderStyle-Width="90px" CurrentFilterFunction="Contains"
                                                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Coupon Rate(%)"
                                                                            UniqueName="AID_CouponRate" SortExpression="AID_CouponRate">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="AID_RenewCouponRate" HeaderStyle-Width="100px"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                            HeaderText="Renew Coupon Rate(%)" UniqueName="AID_RenewCouponRate" SortExpression="AID_RenewCouponRate">
                                                                            <HeaderStyle Width="100px" />
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn Visible="false" DataField="AID_LockingPeriod" HeaderStyle-Width="100px"
                                                                            HeaderText="Lock-in Period" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                            DataFormatString="{0:N0}" AutoPostBackOnFilter="true" UniqueName="AID_LockingPeriod">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn Visible="false" DataField="MaxRetailValue" HeaderStyle-Width="100px"
                                                                            HeaderText="Max Retail Value" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                            DataFormatString="{0:N0}" AutoPostBackOnFilter="true" UniqueName="MaxRetailValue">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn Visible="false" DataField="AID_DefaultInterestRate" HeaderStyle-Width="80px"
                                                                            HeaderText="Yield at Call(%)" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                            AutoPostBackOnFilter="true" UniqueName="AID_DefaultInterestRate">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="AID_YieldUpto" HeaderStyle-Width="105px" HeaderText="Yield at Maturity(%)"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                            UniqueName="AID_YieldUpto" Visible="true">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="AID_YieldatBuyBack" HeaderStyle-Width="105px"
                                                                            HeaderText="Yield at BuyBack(%)" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                            AutoPostBackOnFilter="true" UniqueName="AID_YieldatBuyBack" Visible="true">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn Visible="false" DataField="AID_CallOption" HeaderStyle-Width="80px" HeaderText="Call Option"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                            UniqueName="AID_CallOption">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn Visible="false" DataField="AID_BuyBackFacility" HeaderStyle-Width="120px"
                                                                            HeaderText="Is Buy Back Facility" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                            AutoPostBackOnFilter="true" UniqueName="AID_BuyBackFacility">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="AIM_BestBidQuantity" HeaderStyle-Width="140px" HeaderText="Minimum Quantity"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                            UniqueName="AIM_BestBidQuantity" Visible="true" DataFormatString="{0:N0}">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="AIM_TradingInMultipleOf" HeaderStyle-Width="110px"
                                                                            HeaderText="Multiple allowed" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                            AutoPostBackOnFilter="true" UniqueName="AIM_TradingInMultipleOf" Visible="true">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </mastertableview>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
