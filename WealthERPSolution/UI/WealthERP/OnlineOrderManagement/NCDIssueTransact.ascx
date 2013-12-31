<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDIssueTransact.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.NCDIssueTransact" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script type="text/javascript">
    function isNumberKey(evt) { // Numbers only
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            alert('Only Numeric');
            return false;
        }
        return true;  
</script>

<script type="text/javascript">
    function ValidateTermsConditions(sender, args) {

        if (document.getElementById("<%=chkTermsCondition.ClientID %>").checked == true) {
            args.IsValid = true;
        } else {
            args.IsValid = false;
        }
    }
</script>

<script type="text/javascript">
    var crnt = 0;
    function PreventClicks() {

        if (typeof (Page_ClientValidate('btnSubmit')) == 'function') {
            Page_ClientValidate();
        }

        if (Page_IsValid) {
            if (++crnt > 1) {
                return false;
            }
            return true;
        }
        else {
            return false;
        }
    }
</script>

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
<%--<asp:UpdatePanel ID="upCMGrid" runat="server">
    <ContentTemplate>--%>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table width="100%">
                    <tr>
                        <td align="left">
                            NCD Issue Transact
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="ibtExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" Height="25px"
                                Visible="false" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="divOnlinePageHeading" style="float: right; width: 100%">
            <div style="float: right; padding-right: 100px;">
                <span style="color: Black; font: arial; font-size: smaller">Available Limits:</span>
                <asp:Label ID="lblAvailableLimits" runat="server" Text="" CssClass="FieldName"></asp:Label>
            </div>
        </div>
        <table id="tblMessage" width="100%" runat="server" visible="false" style="padding-top: 20px;">
            <tr id="trSumbitSuccess">
                <td align="center">
                    <div id="msgRecordStatus" class="success-msg" align="center" runat="server">
                    </div>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<table width="100%" class="TableBackground">
    <tr>
        <td class="leftField" colspan="2" align="center">
            <asp:Label ID="lblMSG" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr id="trJointNom" runat="server" align="center">
        <td>
            <asp:Label ID="lblHolderDetails" runat="server" Text="Joint Holders Name:" CssClass="FieldName"></asp:Label>
            <%-- </td>--%>
            <%--<td align="left">--%>
            <asp:Label ID="lblHolderTwo" runat="server" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr id="trcustcode" runat="server" align="center">
        <td>
            <asp:Label ID="lblNominee" runat="server" Text="Nominee Name:" CssClass="FieldName"></asp:Label>
            <%-- </td>
        <td align="left">--%>
            <asp:Label ID="lblNomineeTwo" runat="server" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr align="center">
        <td>
            <asp:Label ID="lblIssuer" runat="server" Text=":" CssClass="FieldName"></asp:Label>
            <asp:DropDownList ID="ddIssuerList" runat="server" CssClass="cmbField" AutoPostBack="false">
            </asp:DropDownList>
        </td>
        <td align="left">
            <asp:Button ID="btnConfirm" runat="server" Text="Go" OnClick="btnConfirm_Click" CssClass="PCGButton" />
        </td>
    </tr>
</table>
<asp:Panel ID="pnlIssuList" runat="server" CssClass="Landscape" Width="100%">
    <table id="tblCommissionStructureRule" runat="server" width="100%">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <telerik:RadGrid ID="gvIssueList" runat="server" AllowSorting="True" enableloadondemand="True"
                                PageSize="10" AllowPaging="false" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                                GridLines="None" ShowFooter="True" PagerStyle-AlwaysVisible="true" ShowStatusBar="True"
                                Skin="Telerik" AllowFilteringByColumn="false">
                                <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="AIM_IssueId,AIM_SchemeName,IssueTimeType"
                                    AutoGenerateColumns="false" Width="100%">
                                    <Columns>
                                        <%--  <telerik:GridTemplateColumn>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbDetails" runat="server" CommandName="ExpandCollapse" Font-Underline="False"
                                                    Font-Bold="true" UniqueName="Detailslink" OnClick="btnExpandAll_Click" Font-Size="Medium">+</asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>--%>
                                        <telerik:GridBoundColumn DataField="AIM_SchemeName" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Issue" UniqueName="AIM_SchemeName"
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
                                        <telerik:GridBoundColumn Visible="false" DataField="PI_IssuerCode" HeaderStyle-Width="70px"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="Issuer" UniqueName="PI_IssuerCode" SortExpression="PI_IssuerCode">
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
                                        <telerik:GridBoundColumn DataField="AIM_MInQty" HeaderStyle-Width="120px" HeaderText="Min Qty"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            DataFormatString="{0:N0}" UniqueName="AIM_MInQty" Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_MaxQty" HeaderStyle-Width="140px" HeaderText="Max Qty"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            UniqueName="AIM_MaxQty" Visible="true" DataFormatString="{0:N0}">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_FaceValue" HeaderStyle-Width="80px" HeaderText="Face Value"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            UniqueName="FaceValue" Visible="true" DataFormatString="{0:N0}">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AID_MinApplication" HeaderStyle-Width="110px"
                                            HeaderText="Min Amt" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true" UniqueName="AID_MinApplication" Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Maxamount" HeaderStyle-Width="110px" HeaderText="Max Amt"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            UniqueName="Maxamount" Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_TradingInMultipleOf" HeaderStyle-Width="110px"
                                            HeaderText="Is Multiple" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true" UniqueName="AIM_TradingInMultipleOf" Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn DataField="AIM_OpenDate" DataFormatString="{0:D}" HeaderStyle-Width="110px"
                                            CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="Opening Date" SortExpression="AIM_OpenDate" UniqueName="AIM_OpenDate">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridDateTimeColumn DataField="AIM_CloseDate" DataFormatString="{0:D}" HeaderStyle-Width="110px"
                                            CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="Closing Date" UniqueName="AIM_CloseDate" SortExpression="AIM_CloseDate">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridDateTimeColumn DataField="Timing" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                            HeaderStyle-Width="110px" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true" HeaderText="Timing" UniqueName="Timing" SortExpression="Timing">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn DataField="IsDematFacilityAvail" HeaderStyle-Width="110px"
                                            HeaderText="Is Demat" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true" UniqueName="IsDematFacilityAvail" Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <%-- <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="110px"
                                            UniqueName="Action" HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="llPurchase" runat="server" OnClick="llPurchase_Click" Text="Purchase"></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>--%>
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
<asp:Panel ID="pnlNCDTransactact" runat="server" ScrollBars="Horizontal" Width="100%"
    Visible="false">
    <table>
        <tr>
            <td>
                <telerik:RadGrid ID="gvCommMgmt" AllowSorting="false" runat="server" EnableLoadOnDemand="True"
                    AllowPaging="false" AutoGenerateColumns="False" EnableEmbeddedSkins="False" GridLines="None"
                    ShowFooter="True" PagerStyle-AlwaysVisible="true" ShowStatusBar="True" Skin="Telerik"
                    AllowFilteringByColumn="false" OnItemDataBound="gvCommMgmt_ItemDataBound" OnNeedDataSource="gvCommMgmt_OnNeedDataSource">
                    <HeaderContextMenu EnableEmbeddedSkins="False">
                    </HeaderContextMenu>
                    <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="LiveBondList">
                    </ExportSettings>
                    <PagerStyle AlwaysVisible="True" />
                    <MasterTableView AllowMultiColumnSorting="True" AllowSorting="false" DataKeyNames="AID_IssueDetailId,AIM_IssueId,AID_DefaultInterestRate,AID_Tenure,AIM_FaceValue,AIM_TradingInMultipleOf,AIM_MInQty,AIM_MaxQty,AIM_MaxApplNo"
                        AutoGenerateColumns="false" Width="100%">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <Columns>
                            <%--<telerik:GridBoundColumn visible="false" DataField="PFISM_SchemeId" HeaderStyle-Width="60px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Scheme" UniqueName=" SchemeId"
                                SortExpression="SeriesId">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>--%>
                            <%-- <telerik:GridBoundColumn Visible="false" DataField="CO_OrderId" HeaderStyle-Width="60px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="OrderNo" UniqueName="CO_OrderId"
                                SortExpression="CO_OrderId">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn DataField="AID_IssueDetailName" HeaderStyle-Width="60px"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                HeaderText="Series" UniqueName="AID_IssueDetailName" SortExpression="AID_IssueDetailName">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="AID_Sequence" HeaderStyle-Width="60px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Sequence" UniqueName="AID_Sequence"
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
                            <telerik:GridBoundColumn Visible="false" DataField="PI_IssuerCode" HeaderStyle-Width="70px"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                HeaderText="Issuer" UniqueName="PI_IssuerCode" SortExpression="PI_IssuerCode">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="AID_IssueDetailId" HeaderStyle-Width="60px"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                HeaderText="Series" UniqueName="AID_IssueDetailId" SortExpression="AID_IssueDetailId">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="AIM_FaceValue" HeaderStyle-Width="80px"
                                HeaderText="Face Value" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" UniqueName="AIM_FaceValue" DataFormatString="{0:N0}">
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
                            <telerik:GridBoundColumn DataField="CouponRate" HeaderStyle-Width="90px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Coupon Rate(%)"
                                UniqueName="CouponRate" SortExpression="CouponRate">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="AID_RenewCouponRate" HeaderStyle-Width="100px"
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
                            <telerik:GridBoundColumn Visible="false" DataField="AID_DefaultInterestRate" HeaderStyle-Width="80px"
                                HeaderText="Yield at Call(%)" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" UniqueName="AID_DefaultInterestRate">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="YieldAtMAturity" HeaderStyle-Width="105px" HeaderText="Yield at Maturity(%)"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                UniqueName="YieldAtMAturity" Visible="true">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="AID_YieldatBuyBack" HeaderStyle-Width="105px"
                                HeaderText="Yield at BuyBack(%)" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" UniqueName="AID_YieldatBuyBack">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="AID_CallOption" HeaderStyle-Width="80px"
                                HeaderText="Call Option" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" UniqueName="AID_CallOption">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="AID_BuyBackFacility" HeaderStyle-Width="120px"
                                HeaderText="Is Buy Back Facility" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" UniqueName="AID_BuyBackFacility">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="AIM_MInQty" HeaderStyle-Width="140px"
                                HeaderText="Minimum Quantity" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" UniqueName="AIM_MInQty" DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="AIM_MaxQty" HeaderStyle-Width="140px"
                                HeaderText="Max Quantity" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" UniqueName="AIM_MaxQty" DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="AIM_TradingInMultipleOf" HeaderStyle-Width="110px"
                                HeaderText="Multiple allowed" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" UniqueName="AIM_TradingInMultipleOf">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                UniqueName="Quantity" HeaderText="Enter Purchase Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQuantity" runat="server" OnTextChanged="txtQuantity_TextChanged"
                                        ForeColor="White" MaxLength="5" Text='<%# Bind("COID_Quantity")%>' Width="50px"
                                        AutoPostBack="true" BackColor="Gray" OnKeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                                    <%--  <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="*Required"
                                        ClientValidationFunction="ValidateTextValue(this)"></asp:CustomValidator>--%>
                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator" ControlToValidate="txtQuantity"
                                        ValidationGroup="gvCommMgmt" ErrorMessage="<br />Please enter a Quantity" Display="Dynamic"
                                        runat="server" CssClass="rfvPCG">
                                    </asp:RequiredFieldValidator>--%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label runat="server" ID="lblQuantity"></asp:Label>
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                FooterText="" UniqueName="Amount" HeaderText="Purchase Amt" FooterAggregateFormatString="{0:N2}">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAmount" runat="server" ReadOnly="true" ForeColor="White" BackColor="Gray"
                                        Width="50px" Font-Bold="true" Text='<%# Bind("COID_AmountPayable")%>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label runat="server" ID="lblAmount"></asp:Label>
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                Visible="false" UniqueName="Check" HeaderText="Check Order">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbOrderCheck" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" DataField="" Visible="false" HeaderStyle-Width="100px"
                                UniqueName="AmountAtMaturity" HeaderText="Amount at Maturity">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbconfirmOrder" runat="server" Text="Confirm Order" OnClick="lbconfirmOrder_Click"></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <EditFormSettings>
                            <EditColumn CancelImageUrl="Cancel.gif" EditImageUrl="Edit.gif" InsertImageUrl="Update.gif"
                                UpdateImageUrl="Update.gif">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle AlwaysVisible="True" />
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        <Resizing AllowColumnResize="true" />
                    </ClientSettings>
                    <FilterMenu EnableEmbeddedSkins="False">
                    </FilterMenu>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
<table>
    <tr>
        <td>
        </td>
    </tr>
</table>
<table>
    <tr class="spaceUnder" id="trTermsCondition" runat="server">
        <td align="left" style="width: 15%">
            <asp:CheckBox ID="chkTermsCondition" runat="server" Font-Bold="True" Font-Names="Shruti"
                Enabled="false" Checked="false" ForeColor="#145765" Text="" ToolTip="Click 'Terms & Conditions' to proceed further"
                CausesValidation="true" />
            <asp:LinkButton ID="lnkTermsCondition" CausesValidation="false" Text="Terms & Conditions"
                runat="server" CssClass="txtField" OnClick="lnkTermsCondition_Click" ToolTip="Click here to accept terms & conditions"></asp:LinkButton>
            <span id="Span9" class="spnRequiredField">*</span>
        </td>
        <td colspan="3" style="width: 85%" align="left">
            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please read terms & conditions"
                ClientValidationFunction="ValidateTermsConditions" EnableClientScript="true"
                OnServerValidate="TermsConditionCheckBox" Display="Dynamic" ValidationGroup="btnConfirmOrder"
                CssClass="rfvPCG">
                Please read terms & conditions
            </asp:CustomValidator>
        </td>
    </tr>
    <tr >
    <td>
    <asp:Label ID="lb1CustOffTimeMsg" runat="server"  CssClass="FieldName"></asp:Label>
    
    </td>
        <td id="tdsubmit" runat="server" align="left" style="width: 60%">
            <asp:Label ID="Label3" runat="server" Text="Confirm Your Order :" CssClass="FieldName"></asp:Label>
            <asp:Button ID="btnConfirmOrder" runat="server" Text="Submit" OnClick="btnConfirmOrder_Click"
                CssClass="PCGButton" ValidationGroup="btnConfirmOrder" />
            
        </td>
        <td><asp:LinkButton runat="server" ID="lnlBack" CssClass="LinkButtons" Text="Click here to view the issue list"
                Visible="false" OnClick="lnlktoviewncdissue_Click"></asp:LinkButton></td>
    </tr>
</table>
<table>
    <tr id="trSubmit" runat="server" visible="false">
        <%--<td id="tdsubmit" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Confirm Your Order :" CssClass="FieldName"></asp:Label>
        <asp:Button ID="btnConfirmOrder" runat="server" Text="Submit" OnClick="btnConfirmOrder_Click"
            CssClass="PCGButton" />
    </td>--%>
        <td id="tdupdate" runat="server" visible="false">
            <asp:Label ID="Label2" runat="server" Text="Confirm Your Order :" CssClass="FieldName"></asp:Label>
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdateOrder_Click"
                CssClass="PCGButton" />
        </td>
    </tr>
</table>
<telerik:RadWindow ID="rwTermsCondition" runat="server" VisibleOnPageLoad="false"
    Width="1000px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="Move, Resize,Close"
    Title="Terms & Conditions" EnableShadow="true" Left="580" Top="-8">
    <ContentTemplate>
        <div style="padding: 0px; width: 100%">
            <table width="100%" cellpadding="0" cellpadding="0">
                <tr>
                    <td align="left">
                        <%--  <a href="../ReferenceFiles/MF-Terms-Condition.html">../ReferenceFiles/MF-Terms-Condition.html</a>--%>
                        <iframe src="../ReferenceFiles/NCD-Terms-Condition.html" name="iframeTermsCondition"
                            style="width: 100%"></iframe>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAccept" runat="server" Text="Accept" CssClass="PCGButton" OnClick="btnAccept_Click"
                            CausesValidation="false" ValidationGroup="btnSubmit" />
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
</telerik:RadWindow>
<%--<telerik:RadWindowManager runat="server" ID="RadWindowManager1">
    <Windows>
        <telerik:RadWindow ID="rw_customConfirm" Modal="true" Behaviors="Close, Move" VisibleStatusbar="false"
            Width="700px" Height="160px" runat="server" Title="EUIN Confirm">
            <ContentTemplate>
                <div class="rwDialogPopup radconfirm">
                    <div class="rwDialogText">
                        <asp:Label ID="confirmMessage" Text="" runat="server" />
                    </div>
                    <div>
                        <%--<td id="tdsubmit" runat="server">
                            <asp:Label ID="Label1" runat="server" Text="Confirm Your Order :" CssClass="FieldName"></asp:Label>
                            <asp:Button ID="btnConfirmOrder" runat="server" Text="Submit" OnClick="btnConfirmOrder_Click"
                                CssClass="PCGButton" OnClientClick="return PreventClicks();" />
                        </td>--%>
<%-- <asp:Button runat="server" ID="rbConfirm_OK" Text="OK" OnClick="rbConfirm_OK_Click"
                            ValidationGroup="btnSubmit" OnClientClick="return PreventClicks();"></asp:Button>
                        <asp:Button runat="server" ID="rbConfirm_Cancel" Text="Cancel" OnClientClicked="closeCustomConfirm">
                        </asp:Button>--%>
<%--   </div>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>--%>
