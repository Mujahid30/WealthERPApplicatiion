<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDIssueHoldings.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.NCDIssueHoldings" %>
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
<asp:UpdatePanel ID="upBHGrid" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td>
                    <div class="divPageHeading">
                        <table width="100%">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblProductType" runat="server" CssClass="FieldName"></asp:Label>
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
        <div id="divConditional" runat="server" style="padding-top: 4px" visible="false">
            <table class="TableBackground" cellpadding="2">
                <tr>
                    <td id="td1" runat="server">
                        <asp:Label runat="server" class="FieldName" Text="Account:" ID="Label1"></asp:Label>
                        <asp:DropDownList CssClass="cmbField" ID="ddlAccount" runat="server" AutoPostBack="false">
                        </asp:DropDownList>
                    </td>
                    <td id="tdlblFromDate" runat="server" align="right">
                        <asp:Label class="FieldName" ID="lblFromTran" Text="From :" runat="server" />
                    </td>
                    <td id="tdTxtFromDate" runat="server">
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
                    </td>
                    <td id="tdlblToDate" runat="server">
                        <asp:Label ID="lblToTran" Text="To :" CssClass="FieldName" runat="server" />
                    </td>
                    <td id="tdTxtToDate" runat="server">
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
                    </td>
                    <td id="tdBtnOrder" runat="server">
                        <asp:Button ID="btnViewOrder" runat="server" CssClass="PCGButton" Text="Go" ValidationGroup="btnViewOrder"
                            OnClick="btnViewOrder_Click" />
                    </td>
                </tr>
            </table>
        </div>
<div id="Div2" class="row" style="margin-left: 5%; margin-right: 5%; background-color: #2480C7;"
    visible="false" runat="server">
                        <table width="100%">
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="gvBHList" runat="server" GridLines="None" AllowPaging="True"
                                        PageSize="5" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                        AllowAutomaticDeletes="false" AllowAutomaticInserts="false" AllowAutomaticUpdates="false"
                                        HorizontalAlign="NotSet" CellPadding="15" OnNeedDataSource="gvBHList_OnNeedDataSource"
                                        OnItemCommand="gvBHList_OnItemCommand" OnItemDataBound="gvBHList_OnItemDataBound">
                                        <ExportSettings FileName="Details" HideStructureColumns="true" ExportOnlyData="true">
                                        </ExportSettings>
                                        <MasterTableView DataKeyNames="AIM_IssueId,CO_OrderId" Width="100%" AllowMultiColumnSorting="True"
                                            AutoGenerateColumns="false" CommandItemDisplay="None">
                                            <Columns>
                                                <telerik:GridTemplateColumn>
                                                    <ItemTemplate>
                                                        <div class="col-sm-12 col-sm-12 col-md-12 col-lg-12 divs">
                                                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                <font color="#565656"><b>Issue Name:</b></font>
                                                                <%# Eval("AIM_IssueName")%>
                                                            </div>
                                                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                <font color="#565656"><b>Allotment Date:</b> </font>
                                                                <%# Eval("AIM_AllotmentDate")%>
                                                            </div>
                                                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                <font color="#565656"><b>Alloted Qty.:</b></font>
                                                                <%# Eval("AllotedQty")%>
                                                            </div>
                                                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                <font color="#565656"><b>Ordered Qty.:</b></font>
                                                                <%# Eval("OrderedQty")%>
                                                            </div>
                                                            <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                <font color="#565656"><b>Alloted Amount:</b></font>
                                                                <%# Eval("HoldingAmount")%>
                                                            </div>
                                                            <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1 fk-font-1" style="margin-bottom: 1.5px;
                                                                visibility: hidden;">
                                                                <%# Eval("AIM_IssueId")%>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                  </div>
    </ContentTemplate>
    <Triggers>
      
    </Triggers>
</asp:UpdatePanel>
<asp:HiddenField ID="hdnAccount" runat="server" />
