<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerIPOOrderBook.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.CustomerIPOOrderBook" %>
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

<script type="text/javascript">
    function GetBalanceQty(value) {
      
        if (value == "Cancel") {
            if (confirm('Are you sure you want to cancel the order?')) {
                document.getElementById("<%= hdnAmount.ClientID %>").value = "Yes";
            }
            else
                document.getElementById("<%= hdnAmount.ClientID %>").value = "No";
        }

    }
</script>

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table width="100%">
                    <tr>
                        <td align="left">
                            IPO/FPO Order Book
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="ibtExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="ibtExport_OnClick"
                                Height="25px" Width="25px" Visible="false"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<div id="divConditional" runat="server" class="row" style="margin-left: 5%; margin-bottom: 2%;
    margin-right: 5%; padding-top: 1%; padding-bottom: 1%; height: 20%">
    <div class="col-md-12 col-xs-12 col-sm-12">
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
                Text="Go" ValidationGroup="btnViewOrder" OnClick="btnViewOrder_Click" />
        </div>
    </div>
</div>
<div id="Div2" class="row"  style="margin-left: 5%; margin-right: 5%; background-color: #2480C7;"
    visible="false" runat="server">
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="RadGridIssueIPOBook" runat="server" GridLines="None" AllowPaging="True"
                    PageSize="5" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                    AllowAutomaticDeletes="false" AllowAutomaticInserts="false" AllowAutomaticUpdates="false"
                    HorizontalAlign="NotSet" CellPadding="15" OnNeedDataSource="RadGridIssueIPOBook_OnNeedDataSource"
                    OnItemDataBound="RadGridIssueIPOBook_OnItemDataBound" OnItemCommand="RadGridIssueIPOBook_OnItemCommand">
                    <MasterTableView DataKeyNames="CO_OrderId,C_CustomerId,PAG_AssetGroupCode,CO_OrderDate,WOS_OrderStep,C_CustCode,Amounttoinvest,AIM_IssueId,IssueEndDateANDTime,AIM_IsModificationAllowed,AIM_IsCancelAllowed,COID_MaxBidAmt,CO_IsCancelled"
                        Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <div class="col-sm-12 col-sm-12 col-md-12 col-lg-12 divs">
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                            <font color="#565656"><b>Scrip Name:</b></font>
                                            <%# Eval("AIM_IssueName")%>
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
                                            <%# Eval("CO_ApplicationNo")%>
                                        </div>
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                            <font color="#565656"><b>Start Date:</b></font>
                                            <%# Eval("IssueStartDateANDTime")%>
                                        </div>
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                            <font color="#565656"><b>End Date:</b></font>
                                            <%# Eval("IssueEndDateANDTime")%>
                                        </div>
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                            <font color="#565656"><b>Amount Invested:</b></font>
                                            <%# Eval("Amounttoinvest")%>
                                        </div>
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                            <font color="#565656"><b>Bid Amount:</b></font>
                                            <%# Eval("AmountBid")%>
                                        </div>
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                            <font color="#565656"><b>Status:</b></font>
                                            <%# Eval("WOS_OrderStep")%>
                                        </div>
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                            <font color="#565656"><b>Bidding Exchange:</b></font>
                                            <%# Eval("Bidding_Exchange")%>
                                        </div>
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                            <font color="#565656"><b>Reject Reason:</b></font>
                                            <%# Eval("COS_Reason")%>
                                        </div>
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                            <asp:LinkButton ID="lbDetails" runat="server" CommandName="ExpandCollapse" class="ft_sort btn-sm btn-info"
                                                UniqueName="Detailslink" OnClick="btnExpandAll_Click" Text="Bid Details" Width="80px"></asp:LinkButton>
                                        </div>
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                            <font color="#565656"><b>Action:</b></font>
                                            <asp:LinkButton ID="lnkModify" runat="server" OnClick="btnEditModiY_Click" class="glyphicon glyphicon-edit"
                                                ToolTip="Modify" />
                                                 <asp:LinkButton ID="lnkCancel" runat="server" Enabled="false" style="font-style:underline" OnClick="btnEditModiY_Click" class="glyphicon glyphicon-remove-circle"
                                                ToolTip="Cancel" OnClientClick="GetBalanceQty('Cancel');" />
                                        </div>
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                            <asp:LinkButton ID="lbtnMarkAsReject" UniqueName="Edit" runat="server" CommandName="Edit"
                                                class="ft_sort btn-sm btn-info" Text="Cancel" Visible="false"></asp:LinkButton>
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
                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false">
                                <ItemTemplate>
                                    <tr>
                                        <td colspan="100%">
                                            <asp:Panel ID="pnlchild" runat="server" Style="display: inline"
                                                Width="100%" ScrollBars="Both" Visible="false">
                                                <telerik:RadGrid ID="gvIPODetails" runat="server" AllowPaging="false" AllowSorting="True"
                                                    AutoGenerateColumns="False" ShowStatusBar="true" AllowAutomaticDeletes="false"
                                                    AllowAutomaticInserts="false" AllowAutomaticUpdates="false" HorizontalAlign="NotSet"
                                                    CellPadding="15">
                                                    <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="AIM_IssueId,CO_OrderId"
                                                        AutoGenerateColumns="false" Width="100%">
                                                        <Columns>
                                                            <telerik:GridTemplateColumn>
                                                                <HeaderStyle />
                                                                <ItemTemplate>
                                                                    <div class="col-sm-12 col-sm-12 col-md-12 col-lg-12">
                                                                        <div class="col-xs-3 col-sm-3 col-md-3col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                            <font color="#565656"><b>Bid Issue:</b> </font>
                                                                            <%# Eval("COID_IssueBidNo")%>
                                                                        </div>
                                                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                            <font color="#565656"><b>Price:</b></font>
                                                                            <%# Eval("COID_Price")%>
                                                                        </div>
                                                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                            <font color="#565656"><b>Quantity:</b></font>
                                                                            <%# Eval("COID_Quantity")%>
                                                                        </div>
                                                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                            <font color="#565656"><b>Amount Invested:</b></font>
                                                                            <%# Eval("COID_AmountPayable")%>
                                                                        </div>
                                                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                            <font color="#565656"><b>Bid Amount:</b></font>
                                                                            <%# Eval("COID_AmountBidPayable")%>
                                                                        </div>
                                                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                            <font color="#565656"><b>Modification Type:</b></font>
                                                                            <%# Eval("TransactionType")%>
                                                                        </div>
                                                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                            <font color="#565656"><b>Discount Type:</b></font>
                                                                            <%# Eval("PriceDiscountType")%>
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
                        <EditFormSettings FormTableStyle-Height="40%" EditFormType="Template" FormMainTableStyle-Width="300px"
                            CaptionFormatString="Order canceling Request">
                            <FormTemplate>
                                <table style="background-color: White;" border="0">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Request No.:"></asp:Label>
                                        </td>
                                        <td class="rightField">
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
                                        <td>
                                            <asp:Button ID="Button1" Text="OK" runat="server" CssClass="PCGButton" CommandName="Update"
                                                ValidationGroup="btnSubmit"></asp:Button>
                                            <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
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
<asp:HiddenField ID="hdneligible" runat="server" />
<asp:HiddenField ID="hdnAmount" runat="server" />
