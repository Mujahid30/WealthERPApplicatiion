<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDIssueBooks.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.NCDIssueBooks" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css">
<!-- Optional theme -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap-theme.min.css">

<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>

<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css">

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>

<script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>

<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<style type="text/css">
    .ft_sort
    {
        background: #999999 none repeat scroll 0 0;
        border: 0 none;
        border-radius: 12px;
        box-shadow: 0 1.5px 2px #bfbfbf inset;
        color: #ffffff;
        cursor: pointer;
        display: block;
        font-size: 11.5px;
        font-style: normal;
        padding: 3px 12px;
        width: 60px;
    }
    .ft_sort:hover
    {
        background: #565656;
        color: #ffffff;
    }
    .divs
    {
        background-color: #EEEEEE;
        margin-bottom: .5%;
        margin-top: .5%;
        margin-left: .2%;
        margin-right: .1%;
        border: solid 1.5px #EEEEEE;
    }
    .divs:hover
    {
        border: solid 1.5px #00CEFF;
    }
    .dottedBottom
    {
        border-bottom-style: inset;
        border-bottom-width: thin;
        margin-bottom: 1%;
        border-collapse: collapse;
        border-spacing: 10px;
    }
</style>
<%--<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table width="100%">
                    <tr>
                        <td align="left">
                            NCD Order Book
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="ibtExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="ibtExport_OnClick"
                                Height="25px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>--%>
<div class="divOnlinePageHeading" style="float: right; width: 100%">
    <div style="float: right; padding-right: 100px;">
        <table cellspacing="0" cellpadding="3" width="100%">
            <tr>
                <td align="right" style="width: 10px">
                    <asp:ImageButton ID="ibtExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="ibtExport_OnClick"
                        Height="25px" Width="25px" Visible="false"></asp:ImageButton>
                </td>
            </tr>
        </table>
    </div>
</div>

<div id="divConditional" runat="server" class="row" style="margin-left: 5%; margin-bottom: 2%;
    margin-right: 5%; padding-top: 1%; padding-bottom: 1%; height: 20%">
    <div class="col-md-12 col-xs-12 col-sm-12" style="margin-top:2%;">
        <div class="col-md-2">
            Order Status
            <asp:DropDownList CssClass="form-control input-sm" ID="ddlOrderStatus" runat="server"
                AutoPostBack="false">
            </asp:DropDownList>
        </div>
        <div class="col-md-3">
            Issue Name
            <asp:DropDownList CssClass="form-control input-sm" ID="ddlIssueName" runat="server"
                AutoPostBack="false">
            </asp:DropDownList>
        </div>
        <div class="col-md-2">
            From
            <br />
            <telerik:RadDatePicker ID="txtOrderFrom" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <div id="dvTransactionDate" runat="server" class="dvInLine">
                <span id="Span1" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="rfvtxtTransactionDate" ControlToValidate="txtOrderFrom"
                    ErrorMessage="<br />Please select a From Date" CssClass="cvPCG" Display="Dynamic"
                    runat="server" InitialValue="" ValidationGroup="btnViewOrder">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                    Type="Date" ControlToValidate="txtOrderFrom" CssClass="cvPCG" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
            </div>
        </div>
        <div class="col-md-2">
            To
            <br />
            <telerik:RadDatePicker ID="txtOrderTo" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <div id="Div1" runat="server" class="dvInLine">
                <span id="Span2" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtOrderTo"
                    ErrorMessage="<br />Please select a To Date" CssClass="cvPCG" Display="Dynamic"
                    runat="server" InitialValue="" ValidationGroup="btnViewOrder">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                    Type="Date" ControlToValidate="txtOrderTo" CssClass="cvPCG" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
            </div>
            <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtOrderTo"
                ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                ControlToCompare="txtOrderFrom" CssClass="cvPCG" ValidationGroup="btnViewOrder"
                Display="Dynamic">
            </asp:CompareValidator>
        </div>
        <div class="col-md-2">
            <br />
            <asp:Button ID="btnViewOrder" runat="server" CssClass="btn btn-primary btn-primary"
                Text="Go" ValidationGroup="btnViewOrder" OnClick="btnViewOrder_Click" Height="30px" />
        </div>
    </div>
</div>
<div id="Div2" class="row" style="margin-left: 5%; margin-right: 5%; background-color: #2480C7;"
    visible="false" runat="server">
    
                <table width="100%">
                    <tr>
                        <td>
                            <telerik:RadGrid ID="gvBBList" runat="server" GridLines="None" AllowPaging="True"
                                PageSize="5" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                AllowAutomaticDeletes="false" AllowAutomaticInserts="false" AllowAutomaticUpdates="false"
                                HorizontalAlign="NotSet" CellPadding="15" OnNeedDataSource="gvBBList_OnNeedDataSource"
                                OnUpdateCommand="gvBBList_UpdateCommand" OnItemDataBound="gvBBList_OnItemDataCommand"
                                OnItemCommand="gvBBList_OnItemCommand">
                                <MasterTableView DataKeyNames="CO_OrderId,AIM_IssueId,Scrip,WTS_TransactionStatusCode,WOS_OrderStepCode,BBAmounttoinvest,WES_Code"
                                     EditMode="PopUp"
                                    CommandItemDisplay="None">
                                    <Columns>
                                        <telerik:GridTemplateColumn>
                                            <ItemTemplate>
                                                <div class="col-sm-12 col-sm-12 col-md-12 col-lg-12 divs">
                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                        <font color="#565656"><b>Issue Name:</b></font>
                                                        <%# Eval("Scrip")%>
                                                    </div>
                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                        <font color="#565656"><b>Transaction Date:</b> </font>
                                                        <%# Eval("CO_OrderDate")%>
                                                    </div>
                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                        <font color="#565656"><b>Transaction No.:</b></font>
                                                        <%# Eval("CO_OrderId")%>
                                                    </div>
                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                        <font color="#565656"><b>Application No.:</b></font>
                                                        <%# Eval("AIM_MaxApplNo")%>
                                                    </div>
                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                        <font color="#565656"><b>Start Date:</b></font>
                                                        <%# Eval("BBStartDate")%>
                                                    </div>
                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                        <font color="#565656"><b>End Date:</b></font>
                                                        <%# Eval("BBEndDate")%>
                                                    </div>
                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                        <font color="#565656"><b>Amount to Invest:</b></font>
                                                        <%# Eval("BBAmounttoinvest")%>
                                                    </div>
                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                        <font color="#565656"><b>Status:</b></font>
                                                        <%# Eval("WOS_OrderStep")%>
                                                    </div>
                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                        <asp:LinkButton ID="lbDetails" runat="server" CommandName="ExpandCollapse" class="ft_sort btn-sm btn-info"
                                                            UniqueName="Detailslink" OnClick="btnExpandAll_Click" Text="Bid Details" Width="80px"></asp:LinkButton>
                                                    </div>
                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                        <font color="#565656"><b>Order View:</b></font>
                                                        <asp:LinkButton ID="imgView" runat="server" CommandName="View" class="glyphicon glyphicon-eye-open"
                                                            ToolTip="View" />
                                                    </div>
                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                        <asp:LinkButton ID="lbtnMarkAsReject" UniqueName="Edit" runat="server" CommandName="Edit"
                                                            class="ft_sort btn-sm btn-info" Text="Cancel"></asp:LinkButton>
                                                    </div>
                                                    <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1 fk-font-1" style="margin-bottom: 1.5px;
                                                        visibility: hidden;">
                                                        <%# Eval("AIM_IssueId")%>
                                                    </div>
                                                    <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1 fk-font-1" style="margin-bottom: 1.5px;
                                                        visibility: hidden;">
                                                        <%# Eval("PI_IssuerId")%>
                                                    </div>
                                                    <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1 fk-font-1"" " style="margin-bottom: 1.5px;
                                                        visibility: hidden;">
                                                        <%# Eval("PI_IssuerCode")%>
                                                    </div>
                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;
                                                        visibility: hidden;">
                                                        <%# Eval("WES_Code")%>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false">
                                            <ItemTemplate>
                                                <tr>
                                                    <td colspan="100%">
                                                        <asp:Panel ID="pnlchild" runat="server" Style="display: inline" CssClass="Landscape"
                                                            Width="100%" ScrollBars="Both" Visible="false">
                                                            <telerik:RadGrid ID="gvChildDetails" runat="server" AllowPaging="false" AllowSorting="True"
                                                                AutoGenerateColumns="False" ShowStatusBar="true" AllowAutomaticDeletes="false"
                                                                AllowAutomaticInserts="false" AllowAutomaticUpdates="false" HorizontalAlign="NotSet"
                                                                CellPadding="15" OnNeedDataSource="gvChildDetails_OnNeedDataSource" OnItemDataBound="gvChildDetails_OnItemDataBound">
                                                                <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="AIM_IssueId,CO_OrderId,AID_IssueDetailId"
                                                                    AutoGenerateColumns="false" Width="100%">
                                                                    <Columns>
                                                                        <telerik:GridTemplateColumn>
                                                                            <HeaderStyle />
                                                                            <ItemTemplate>
                                                                                <div class="col-sm-12 col-sm-12 col-md-12 col-lg-12">
                                                                                    <div class="col-xs-3 col-sm-3 col-md-3col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                                        <font color="#565656"><b>Series:</b> </font>
                                                                                        <%# Eval("AID_IssueDetailName")%>
                                                                                    </div>
                                                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                                        <font color="#565656"><b>Tenure (Months):</b></font>
                                                                                        <%# Eval("BBTenure")%>
                                                                                    </div>
                                                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                                        <font color="#565656"><b>Coupon rate(%):</b></font>
                                                                                        <%# Eval("BBCouponrate")%>
                                                                                    </div>
                                                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                                        <font color="#565656"><b>Interest Payment Freq:</b></font>
                                                                                        <%# Eval("BBInterestPaymentFreq")%>
                                                                                    </div>
                                                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                                        <font color="#565656"><b>Renewed coupon rate(%):</b></font>
                                                                                        <%# Eval("BBRenewedcouponrate")%>
                                                                                    </div>
                                                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                                        <font color="#565656"><b>Face Value:</b></font>
                                                                                        <%# Eval("BBFacevalue")%>
                                                                                    </div>
                                                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                                        <font color="#565656"><b>Yield at Call(%):</b></font>
                                                                                        <%# Eval("BBYieldatCall")%>
                                                                                    </div>
                                                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                                        <font color="#565656"><b>Yield at Maturity(%):</b></font>
                                                                                        <%# Eval("BBYieldatMaturity")%>
                                                                                    </div>
                                                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                                        <font color="#565656"><b>Yield at buyback(%):</b></font>
                                                                                        <%# Eval("BBYieldatbuyback")%>
                                                                                    </div>
                                                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                                        <font color="#565656"><b>Lockin Period:</b></font>
                                                                                        <%# Eval("BBLockinPeriod")%>
                                                                                    </div>
                                                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                                        <font color="#565656"><b>Call Option:</b></font>
                                                                                        <%# Eval("BBCallOption")%>
                                                                                    </div>
                                                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                                        <font color="#565656"><b>Buyback facility:</b></font>
                                                                                        <%# Eval("BBBuybackfacility")%>
                                                                                    </div>
                                                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                                        <font color="#565656"><b>Qty to invest:</b></font>
                                                                                        <%# Eval("BBQtytoinvest")%>
                                                                                    </div>
                                                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                                        <font color="#565656"><b>Amount to invest:</b></font>
                                                                                        <%# Eval("BBAmounttoinvest")%>
                                                                                    </div>
                                                                                     <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;" id="nomineedetails" runat="server">
                                                                                        <font color="#565656"><b>Nominee Qty.:</b></font>
                                                                                        <%# Eval("COID_NomineeQuantity")%>
                                                                                    </div>
                                                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                                        <font color="#565656"><b>Channel:</b></font>
                                                                                        <%# Eval("Channel")%>
                                                                                    </div>
                                                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;
                                                                                        visibility: hidden;">
                                                                                        <%# Eval("AID_Sequence")%>
                                                                                    </div>
                                                                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;
                                                                                        visibility: hidden;">
                                                                                        <%# Eval("AID_IssueDetailId")%>
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <EditFormSettings FormTableStyle-Height="40%" EditFormType="Template" FormMainTableStyle-Width="450px"
                                        CaptionFormatString="Order Cancel">
                                        <FormTemplate>
                                            <table style="background-color: White;" border="0">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Request No.:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtRejOrderId" runat="server" CssClass="txtField" Style="width: 250px;"
                                                            Text='<%# Bind("CO_OrderId") %>' ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label20" runat="server" Text="Remark:" CssClass="FieldName"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="txtField" Style="width: 250px;"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <asp:Button ID="Button1" Text="OK" runat="server" class="ft_sort btn-sm btn-info"
                                                            CommandName="Update" ValidationGroup="btnSubmit"></asp:Button>
                                                            </td><td>
                                                        <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" class="ft_sort btn-sm btn-info"
                                                            CommandName="Cancel"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FormTemplate>
                                    </EditFormSettings>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
           
</div>
<asp:HiddenField ID="hdnOrderStatus" runat="server" />
