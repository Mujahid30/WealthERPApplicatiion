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
<table class="tblMessage" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <div class="divOnlinePageHeading">
                <div class="divClientAccountBalance">
                    <asp:Label ID="Label1" runat="server" Text="Available Limits:" CssClass="BalanceLabel"
                        Visible="true"> </asp:Label>
                    <asp:Label ID="lblAvailableLimits" runat="server" Text="" CssClass="BalanceAmount"
                        Visible="true"></asp:Label>
                </div>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Issue List
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="btnNcdIssueList" runat="server" ImageUrl="~/Images/Export_Excel.png"
                                AlternateText="Excel" ToolTip="Export To Excel" Visible="true" OnClientClick="setFormat('excel')"
                                Height="25px" Width="25px" OnClick="btnNcdIssueList_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="60%" runat="server" id="tbNcdIssueList">
    <tr>
        <td class="leftLabel">
            <asp:Label ID="lb1Type" runat="server" Text="Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField" AutoPostBack="true">
                <asp:ListItem Value="Select">Select</asp:ListItem>
                <asp:ListItem Value="Curent">Current Issues</asp:ListItem>
                <asp:ListItem Value="Closed">Closed Issues</asp:ListItem>
                <asp:ListItem Value="Future">Future Issues</asp:ListItem>
            </asp:DropDownList>
            <span id="Span4" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Type"
                CssClass="rfvPCG" ControlToValidate="ddlType" ValidationGroup="btnGo" Display="Dynamic"
                InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
        <td class="rightData">
            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="PCGButton" ValidationGroup="btnGo"
                OnClick="btnGo_Click" />
        </td>
    </tr>
</table>
<%--<asp:UpdatePanel ID="upCMGrid" runat="server">
    <ContentTemplate>--%>
<%--<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table width="100%">s
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
</table>--%>
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
                                Skin="Telerik" AllowFilteringByColumn="true" OnItemDataBound="gvCommMgmt_ItemDataBound"
                                OnItemCommand="gvCommMgmt_ItemCommand" OnNeedDataSource="gvCommMgmt_OnNeedDataSource" >
                                <MasterTableView AllowMultiColumnSorting="true" AllowSorting="true" DataKeyNames="AIM_IssueId,AIM_IssueName,AIM_MInQty,AIM_MaxQty,IssueTimeType,AR_Filename"
                                    AutoGenerateColumns="false" Width="100%">
                                    <Columns>
                                        <telerik:GridTemplateColumn ShowFilterIcon="false" AllowFiltering="false" CurrentFilterFunction="Contains">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbDetails" runat="server" CommandName="ExpandCollapse" Font-Underline="False"
                                                    Font-Bold="true" UniqueName="Detailslink" OnClick="btnExpandAll_Click" Font-Size="Medium">+</asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="AIM_IssueName" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderText="Issue"
                                            UniqueName="AIM_IssueName" SortExpression="false">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Visible="false" DataField="AIM_IssueId" HeaderStyle-Width="60px"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="Scrip ID" UniqueName="AIM_IssueId" SortExpression="AIM_IssueId">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width=" " Wrap="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Visible="true" DataField="PAIC_AssetInstrumentCategoryName"
                                            HeaderStyle-Width="60px" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true" HeaderText="Category" UniqueName="PAIC_AssetInstrumentCategoryName"
                                            SortExpression="false" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width=" " Wrap="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Visible="false" DataField="PI_IssuerId" HeaderStyle-Width="60px"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="Scrip ID" UniqueName="PI_IssuerId" SortExpression="PI_IssuerId">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width=" " Wrap="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Visible="false" DataField="PI_IssuerCode" HeaderStyle-Width="70px"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="Issuer" UniqueName="PI_IssuerCode" SortExpression="PI_IssuerCode">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Visible="false" DataField="AIM_NatureOfBond" HeaderStyle-Width="100px"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="Type" UniqueName="AIM_NatureOfBond" SortExpression="AIM_NatureOfBond"
                                            AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_Rating" HeaderStyle-Width="200px" HeaderText="Rating"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            UniqueName="AIM_Rating" Visible="true" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="200px" Wrap="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_MInQty" HeaderStyle-Width="120px" HeaderText="Min Qty"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            DataFormatString="{0:N0}" UniqueName="AIM_MInQty" Visible="false" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_MaxQty" HeaderStyle-Width="140px" HeaderText="Max Qty"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            UniqueName="AIM_MaxQty" Visible="false" DataFormatString="{0:N0}" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_FaceValue" HeaderStyle-Width="80px" HeaderText="Face Value"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            UniqueName="FaceValue" Visible="false" DataFormatString="{0:N0}" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AID_MinApplication" HeaderStyle-Width="110px"
                                            DataFormatString="{0:N0}" HeaderText="Min Amt" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" UniqueName="AID_MinApplication"
                                            Visible="false" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Maxamount" HeaderStyle-Width="110px" DataFormatString="{0:N0}"
                                            HeaderText="Max Amt" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true" UniqueName="Maxamount" Visible="false" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_TradingInMultipleOf" HeaderStyle-Width="110px"
                                            HeaderText="Multiples Of" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true" UniqueName="AIM_TradingInMultipleOf" Visible="true"
                                            AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn DataField="AIM_OpenDate" DataFormatString="{0:D}" HeaderStyle-Width="110px"
                                            CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="Opening Date" SortExpression="AIM_OpenDate" UniqueName="AIM_OpenDate"
                                            AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn DataField="AIM_CloseDate" DataFormatString="{0:D}" HeaderStyle-Width="110px"
                                            CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="Closing Date" UniqueName="AIM_CloseDate" SortExpression="AIM_CloseDate"
                                            AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn DataField="Timing" DataFormatString="{0:hh:mm tt}" HeaderStyle-Width="110px"
                                            CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="Timing" UniqueName="Timing" SortExpression="Timing" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn DataField="IsDematFacilityAvail" HeaderStyle-Width="110px"
                                            HeaderText="Is Demat" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true" UniqueName="IsDematFacilityAvail" Visible="true"
                                            AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CatCollection" HeaderStyle-Width="140px" HeaderText="Min-Max Qty(Category Wise)"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            UniqueName="CatCollection">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MinMaxCatCollection" HeaderStyle-Width="140px"
                                            HeaderText="Min-Max Qty(Across All Series)" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" UniqueName="MinMaxCatCollection">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <%--   <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="110px"
                                            UniqueName="Action" HeaderText="Action" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="llPurchase" runat="server"   Text="Closed" BackColor="Red"></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>--%>
                                        <telerik:GridTemplateColumn ItemStyle-Width="140px" AllowFiltering="false" HeaderText="Action"
                                            ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgBuy" runat="server" ImageUrl="~/Images/Buy-Button.png" ToolTip="BUY NCD"
                                                    OnClick="imgBuy_Click" />
                                                <asp:LinkButton ID="llPurchase" runat="server" Text="Closed" Visible="false"></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="download_file" Text="View Prospectus" UniqueName="Download"
                                            HeaderText="Download">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridTemplateColumn ShowFilterIcon="false" AllowFiltering="false">
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
                                                                        <telerik:GridBoundColumn DataField="AID_IssueDetailName" HeaderStyle-Width="60px"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                            HeaderText="Series" UniqueName="AID_IssueDetailName" SortExpression="AID_IssueDetailName">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="AID_Sequence" HeaderStyle-Width="60px" CurrentFilterFunction="Contains"
                                                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Sequence" UniqueName="AID_Sequence"
                                                                            SortExpression="AID_Sequence">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="CatColection" HeaderStyle-Width="60px" CurrentFilterFunction="Contains"
                                                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Categories" UniqueName="CatColection"
                                                                            SortExpression="CatColection">
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
                                                                        <telerik:GridBoundColumn Visible="false" DataField="PI_IssuerCode" HeaderStyle-Width="70px"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                            HeaderText="Issuer Id" UniqueName="PI_IssuerCode" SortExpression="PI_IssuerCode">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn Visible="false" DataField="AID_IssueDetailId" HeaderStyle-Width="60px"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                            HeaderText="Series" UniqueName="AID_IssueDetailId" SortExpression="AID_IssueDetailId">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="AID_SeriesFaceValue" HeaderStyle-Width="80px"
                                                                            HeaderText="Face Value" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                            AutoPostBackOnFilter="true" UniqueName="AID_SeriesFaceValue" Visible="true" DataFormatString="{0:N0}">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="AID_Tenure" HeaderStyle-Width="70px" CurrentFilterFunction="Contains"
                                                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Tenure"
                                                                            UniqueName="AID_Tenure" SortExpression="AID_Tenure">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="true" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="CatSubTypeCode" HeaderStyle-Width="105px" HeaderText="SubType Code"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                            UniqueName="CatSubTypeCode" Visible="true">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="AIDCSR_RedemptionDate" HeaderStyle-Width="105px"
                                                                            HeaderText="Redemption Date Note" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                            AutoPostBackOnFilter="true" UniqueName="AIDCSR_RedemptionDate" Visible="true">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="AIDCSR_RedemptionAmount " HeaderStyle-Width="105px"
                                                                            HeaderText="Redemption Amount(Per Bond)" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                            AutoPostBackOnFilter="true" UniqueName="AIDCSR_RedemptionAmount " Visible="true">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="LockinPeriodCollection " HeaderStyle-Width="105px"
                                                                            HeaderText="Lock In Period" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                            AutoPostBackOnFilter="true" UniqueName="LockinPeriodCollection " Visible="true">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <%--<telerik:GridBoundColumn DataField="AID_CouponFreq" HeaderStyle-Width="85px" CurrentFilterFunction="Contains"
                                                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Frequency of Coupon Payment"
                                                                            UniqueName="AID_CouponFreq" SortExpression="AID_CouponFreq">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>--%>
                                                                        <telerik:GridBoundColumn DataField="AID_CouponFreq" HeaderStyle-Width="85px" CurrentFilterFunction="Contains"
                                                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Frequency of Coupon Payment"
                                                                            UniqueName="AID_CouponFreq" SortExpression="AID_CouponFreq">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="CouponRateCollection" HeaderStyle-Width="90px" CurrentFilterFunction="Contains"
                                                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Coupon Rate(%)"
                                                                            UniqueName="CouponRateCollection" SortExpression="CouponRateCollection">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <%--  <telerik:GridBoundColumn  Visible ="false" DataField="AID_RenewCouponRate" HeaderStyle-Width="100px"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                            HeaderText="Renew Coupon Rate(%)" UniqueName="AID_RenewCouponRate" SortExpression="AID_RenewCouponRate">
                                                                            <HeaderStyle Width="100px" />
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                                                                        </telerik:GridBoundColumn>--%>
                                                                        <telerik:GridBoundColumn Visible="false" DataField="AID_LockingPeriod" HeaderStyle-Width="100px"
                                                                            HeaderText="Lock-in Period" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                            DataFormatString="{0:N0}" AutoPostBackOnFilter="true" UniqueName="AID_LockingPeriod">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <%-- <telerik:GridBoundColumn Visible="false" DataField="MaxRetailValue" HeaderStyle-Width="100px"
                                                                            HeaderText="Max Retail Value" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                            DataFormatString="{0:N0}" AutoPostBackOnFilter="true" UniqueName="MaxRetailValue">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>--%>
                                                                        <telerik:GridBoundColumn Visible="false" DataField="AID_DefaultInterestRate" HeaderStyle-Width="80px"
                                                                            HeaderText="Yield at Call (%)" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                            AutoPostBackOnFilter="true" UniqueName="AID_DefaultInterestRate">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="YieldatMatCollection" HeaderStyle-Width="105px" HeaderText="Yield at Maturity (%)"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                            SortExpression="YieldatMatCollection" UniqueName="YieldatMatCollection" Visible="true">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <%--    <telerik:GridBoundColumn DataField="AID_YieldBuyBack" HeaderStyle-Width="105px"
                                                                            SortExpression="AID_YieldBuyBack" HeaderText="Yield at BuyBack(%)" CurrentFilterFunction="Contains"
                                                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" UniqueName="AID_YieldBuyBack"
                                                                            Visible="true">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>--%>
                                                                        <telerik:GridBoundColumn Visible="false" DataField="AID_CallOption" HeaderStyle-Width="80px"
                                                                            HeaderText="Call Option" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                            AutoPostBackOnFilter="true" UniqueName="AID_CallOption" SortExpression="AID_CallOption">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn Visible="false" DataField="AID_BuyBackFacility" HeaderStyle-Width="120px"
                                                                            HeaderText="Is Buy Back Facility" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                            AutoPostBackOnFilter="true" UniqueName="AID_BuyBackFacility" SortExpression="AID_BuyBackFacility">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="AIM_MInQty" HeaderStyle-Width="140px" HeaderText="Minimum Quantity"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                            UniqueName="AIM_MInQty" Visible="false" DataFormatString="{0:N0}" SortExpression="AIM_MInQty">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="AIM_MaxQty" HeaderStyle-Width="140px" HeaderText="Max Quantity"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                            UniqueName="AIM_MaxQty" Visible="false" DataFormatString="{0:N0}" SortExpression="AIM_MaxQty">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="AIM_TradingInMultipleOf" HeaderStyle-Width="110px"
                                                                            HeaderText="Multiple allowed" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                            AutoPostBackOnFilter="true" UniqueName="AIM_TradingInMultipleOf" Visible="true"
                                                                            SortExpression="AIM_TradingInMultipleOf">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <%--<telerik:GridBoundColumn DataField="AIM_TradingInMultipleOf" HeaderStyle-Width="110px"
                                                                            HeaderText="Multiple allowed" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                            AutoPostBackOnFilter="true" UniqueName="AIM_TradingInMultipleOf" Visible="true"
                                                                            SortExpression="AIM_TradingInMultipleOf">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                        </telerik:GridBoundColumn>--%>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                                 <ClientSettings>
                          <%--<Resizing AllowColumnResize="true" />--%>
                        </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
