<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerIPOHolding.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.CustomerIPOHolding" %>
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
<table class="tblMessage" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <div class="divOnlinePageHeading" style="height: 25px;">
                <div class="divClientAccountBalance" id="divClientAccountBalance" runat="server">
                </div>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table width="100%">
                    <tr>
                        <td align="left">
                            IPO/FPO Holding
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
<div id="Div2" class="row" style="margin-left: 5%; margin-right: 5%; background-color: #2480C7;"
    visible="true" runat="server">
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="gvIPOHolding" runat="server" GridLines="None" AllowPaging="True"
                    PageSize="5" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                    AllowAutomaticDeletes="false" AllowAutomaticInserts="false" AllowAutomaticUpdates="false"
                    HorizontalAlign="NotSet" CellPadding="15" OnNeedDataSource="gvIPOHolding_OnNeedDataSource"
                    OnItemCommand="gvIPOHolding_OnItemCommand" OnItemDataBound="gvIPOHolding_OnItemDataBound">
                    <ExportSettings FileName="Details" HideStructureColumns="true" ExportOnlyData="true">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="AIM_IssueId,AIM_IssueName,CO_ApplicationNumber,CO_OrderDate,AIA_AllotmentDate,CO_OrderId"
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
                                            <font color="#565656"><b>Allotment Date:</b> </font>
                                            <%# Eval("AIA_AllotmentDate")%>
                                        </div>
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                            <font color="#565656"><b>Transaction No.:</b></font>
                                            <%# Eval("CO_OrderId")%>
                                        </div>
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                            <font color="#565656"><b>Application No.:</b></font>
                                            <%# Eval("CO_ApplicationNumber")%>
                                        </div>
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                            <font color="#565656"><b>Issue Open Date:</b></font>
                                            <%# Eval("OpenDateTime")%>
                                        </div>
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                            <font color="#565656"><b>Issue Close Date:</b></font>
                                            <%# Eval("CloseDateTime")%>
                                        </div>
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                            <font color="#565656"><b>Alloted Qty.:</b></font>
                                            <%# Eval("AllotedQty")%>
                                        </div>
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                            <font color="#565656"><b>Purchase Price:</b></font>
                                            <%# Eval("AIA_Price")%>
                                        </div>
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                            <font color="#565656"><b>Alloted Value:</b></font>
                                            <%# Eval("Amount")%>
                                        </div>
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;
                                            visibility: hidden">
                                            <asp:ImageButton ID="imgBuy" runat="server" CommandName="Buy" Visible="false" ImageUrl="~/Images/Buy-Button.png"
                                                ToolTip="BUY" />&nbsp;
                                            <asp:ImageButton ID="imgSell" runat="server" CommandName="Sell" Visible="false" ImageUrl="~/Images/Sell-Button.png"
                                                ToolTip="SELL" />&nbsp;
                                        </div>
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                            <asp:LinkButton ID="lnkApplicationNo" runat="server" CommandName="Select" Text='<%# Eval("CO_ApplicationNumber").ToString() %>'>
                                            </asp:LinkButton>
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
                    <ClientSettings>
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    </div>

