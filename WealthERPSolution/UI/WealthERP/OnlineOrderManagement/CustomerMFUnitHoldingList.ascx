<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerMFUnitHoldingList.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.CustomerMFUnitHoldingList" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>--%>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/bootstrap.js" type="text/javascript"></script>

<link href="../Base/CSS/bootstrap-theme.css" rel="stylesheet" type="text/css" />
<link href="../Base/CSS/bootstrap.css" rel="stylesheet" type="text/css" />
<link href="../Base/CSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
<style>
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
        cursor: auto;
    }
</style>
<asp:ScriptManager ID="scriptmanager" runat="server" EnablePageMethods="true">
</asp:ScriptManager>
<style type="text/css">
    .style1
    {
        width: 37%;
    }
</style>

<script type="text/jscript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function(sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                BeginHandler();
            }
        });
    };
    function BeginHandler() {
        jQuery(document).ready(function($) {
            var moveLeft = 0;
            var moveDown = 0;
            $('a.popper').hover(function(e) {

                //var target = '#' + ($(this).attr('data-popbox'));
                var target = '#' + ($(this).find('img').attr('id')).replace('imgSchemeRating', 'divSchemeRatingDetails');

                $(target).show();
                moveLeft = $(this).outerWidth();
                moveDown = ($(target).outerHeight() / 2);

            }, function() {
                //var target = '#' + ($(this).attr('data-popbox'));
                var target = '#' + ($(this).find('img').attr('id')).replace('imgSchemeRating', 'divSchemeRatingDetails');
                $(target).hide();
            });

            $('a.popper').mousemove(function(e) {
                //var target = '#' + ($(this).attr('data-popbox'));
                var target = '#' + ($(this).find('img').attr('id')).replace('imgSchemeRating', 'divSchemeRatingDetails');

                leftD = e.pageX + parseInt(moveLeft);
                maxRight = leftD + $(target).outerWidth();
                windowLeft = $(window).width();
                windowRight = 0;
                maxLeft = e.pageX - (parseInt(moveLeft) + $(target).outerWidth());

                if (maxRight > windowLeft && maxLeft > windowRight) {
                    leftD = maxLeft;
                }

                topD = e.pageY - parseInt(moveDown);
                maxBottom = parseInt(e.pageY + parseInt(moveDown));
                windowBottom = parseInt(parseInt($(document).scrollTop()) + parseInt($(window).height()));
                maxTop = topD;
                windowTop = parseInt($(document).scrollTop());
                //                alert(maxBottom + 'a' + windowBottom + 'b' + maxTop + 's' + windowTop);

                if (maxBottom > windowBottom) {
                    topD = windowBottom - $(target).outerHeight();


                } else if (maxTop < windowTop) {
                    topD = windowTop;

                }

                //                $(target).css('top', ).css('centre', leftD);
            });
        });
    }
</script>

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table width="100%">
                    <tr>
                        <td align="left">
                            MF Holdings
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="upMFHoldings" runat="server">
    <ContentTemplate>
        <div id="divConditional" runat="server" class="row " style="margin-left: 5%; margin-bottom: 0.5%;
            margin-right: 5%; padding-bottom: 0.5%;">

          <%--  <script>
              
            </script>--%>

            <div class="col-md-12 col-xs-12 col-sm-12" style="margin-bottom: 1%">
                <div class="col-md-1" style="text-align: right; padding-top: 3px">
                    Account
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="form-control input-sm">
                    </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnUnitHolding" runat="server" Text="GO" CssClass="btn btn-primary btn-primary"
                        OnClick="btnUnitHolding_Click" />
                    <asp:Label ID="lblPickDate" Text="" runat="server" CssClass="FieldName" Visible="false"> </asp:Label>
                </div>
                <div style="float: right">
                    <asp:ImageButton Visible="false" ID="btnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                       Height="25px" Width="29px"></asp:ImageButton>
                </div>
            </div>
        </div>
        <table style="width: 100%" class="TableBackground">
            <tr id="trNoRecords" runat="server" visible="false">
                <td align="center">
                    <%-- <div id="divNoRecords" runat="server" class="failure-msg">--%>
                    <asp:Label ID="lblNoRecords" Text="No Records found" runat="server"></asp:Label>
                    <%--  </div>--%>
                </td>
            </tr>
        </table>
        <div id="Div1" class="row" style="margin-left: 5%; margin-right: 3%; background-color: #2480C7;"
            visible="false" runat="server">
            <telerik:RadGrid ID="rgUnitHolding" runat="server" GridLines="None" AllowPaging="True"
                PageSize="3" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                AllowAutomaticDeletes="false" AllowAutomaticInserts="false" AllowAutomaticUpdates="false"
                HorizontalAlign="NotSet" CellPadding="15" OnNeedDataSource="rgUnitHolding_OnNeedDataSource"
                OnItemCommand="rgUnitHolding_OnItemCommand" OnItemDataBound="rgUnitHolding_ItemDataBound">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="MFNPId,AccountId,AMCCode,SchemeCode,Status"
                    AllowCustomSorting="true">
                    <Columns>
                        <telerik:GridTemplateColumn>
                            <ItemTemplate>
                                <div class="col-sm-12 col-sm-12 col-md-12 col-lg-12 divs">
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Folio:</b></font>
                                        <%# Eval("FolioNum")%>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Scheme:</b> </font>
                                        <%# Eval("Scheme")%>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Sold Value:</b></font>
                                        <%# Eval("Sold Value")%>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Action:</b></font>
                                        <asp:Label ID="lblSIPSchemeFlag" runat="server" CssClass="cmbField" Text='<%# Eval("IsSchemeSIPType") %>'>
                                        </asp:Label>
                                        <asp:Label ID="lblIsPurcheseFlag" runat="server" CssClass="cmbField" Text='<%# Eval("IsSchemePurchege")%>'>
                                        </asp:Label>
                                        <asp:Label ID="lblISRedeemFlag" runat="server" CssClass="cmbField" Text='<%# Eval("IsSchemeRedeem") %>'>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlAction" runat="server" CssClass="cmbField" class="form-control"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Purchase" Value="BUY" Enabled="false"></asp:ListItem>
                                            <asp:ListItem Text="SIP" Value="SIP" Enabled="false"></asp:ListItem>
                                            <asp:ListItem Text="Redeem" Value="SEL" Enabled="false"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%--  <asp:ImageButton ID="imgBuy" runat="server" CommandName="Buy" ImageUrl="~/Images/Buy-Button.png"
                            ToolTip="BUY" />&nbsp;
                        <asp:ImageButton ID="imgSell" runat="server" CommandName="Sell" ImageUrl="~/Images/Sell-Button.png"
                            ToolTip="SELL" />&nbsp;
                        <asp:ImageButton ID="imgSip" runat="server" CommandName="SIP" ImageUrl="~/Images/SIP-Button.png"
                            ToolTip="SIP"/>--%>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Units:</b></font>
                                        <asp:LinkButton ID="lnkprAmcB" runat="server" Style="color: #0396CC" CommandName="SelectTransaction"
                                            Text='<%# String.Format("{0:N4}", DataBinder.Eval(Container.DataItem, "PurchasedUnits")) %>'>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Invested Cost:</b></font>
                                        <%# Eval("InvestedCost")%>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>NAV:</b></font><%# String.Format("{0:N4}", DataBinder.Eval(Container.DataItem, "NAV"))%>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Unrealised Gain/Loss:</b></font>
                                        <%# Eval("Unrealised Gain/Loss")%>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Current Value:</b></font>
                                        <%# Eval("CurrentValue")%>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Units Sold:</b></font>
                                        <%# Eval("UnitsSold")%>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Scheme Rating:</b></font> <a href="#" class="popper" data-popbox="divSchemeRatingDetails">
                                            <span class="FieldName"></span>
                                            <asp:Image runat="server" ID="imgSchemeRating" onmouseover="BeginHandler()" />
                                        </a>
                                        <asp:Label ID="lblSchemeRating" runat="server" CssClass="cmbField" Text='<%# Eval("SchemeRatingOverall") %>'
                                            Visible="false">
                                        </asp:Label>
                                        <asp:Label ID="lblRating3Year" runat="server" CssClass="cmbField" Text='<%# Eval("SchemeRating3Year") %>'
                                            Visible="false">
                                        </asp:Label>
                                        <asp:Label ID="lblRating5Year" runat="server" CssClass="cmbField" Text='<%# Eval("SchemeRating5Year") %>'
                                            Visible="false">
                                        </asp:Label>
                                        <asp:Label ID="lblRating10Year" runat="server" CssClass="cmbField" Text='<%# Eval("SchemeRating10Year") %>'
                                            Visible="false">
                                        </asp:Label>
                                        <div id="divSchemeRatingDetails" class="popbox" runat="server">
                                            <h2 class="popup-title">
                                                SCHEME RATING DETAILS
                                            </h2>
                                            <table border="1" cellpadding="1" cellspacing="2" style="border-collapse: collapse;"
                                                width="10% !important;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblRatingAsOnPopUp" runat="server" CssClass="readOnlyField" Text='<%# Eval("SchemeRatingDate") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <span class="readOnlyField">RATING</span>
                                                    </td>
                                                    <td>
                                                        <span class="readOnlyField">RETURN</span>
                                                    </td>
                                                    <td>
                                                        <span class="readOnlyField">RISK</span>
                                                    </td>
                                                    <td>
                                                        <span class="readOnlyField">RATING OVERALL</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="readOnlyField">3 YEAR</span>
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="imgRating3yr" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSchemeRetrun3yr" runat="server" CssClass="readOnlyField" Text='<%# Eval("SchemeReturn3Year") %>'> </asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSchemeRisk3yr" runat="server" CssClass="readOnlyField" Text='<%# Eval("SchemeRisk3Year")%>'> </asp:Label>
                                                    </td>
                                                    <td rowspan="3">
                                                        <asp:Image runat="server" ID="imgRatingOvelAll" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="readOnlyField">5 YEAR</span>
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="imgRating5yr" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSchemeRetrun5yr" runat="server" CssClass="readOnlyField" Text='<%# Eval("SchemeReturn5Year") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSchemeRisk5yr" runat="server" CssClass="readOnlyField" Text='<%# Eval("SchemeRisk5Year")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="readOnlyField">10 YEAR</span>
                                                    </td>
                                                    <td>
                                                        <asp:Image runat="server" ID="imgRating10yr" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSchemeRetrun10yr" runat="server" CssClass="readOnlyField" Text='<%# Eval("SchemeReturn10Year") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSchemeRisk10yr" runat="server" CssClass="readOnlyField" Text='<%# Eval("SchemeRisk10Year")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                        <font color="#565656"><b>Realised Gain/Loss:</b></font>
                                        <%# Eval("Realised Gain/Loss")%>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;
                                        visibility: hidden;">
                                        <font color="#565656"><b>InProcessCount:</b></font>
                                        <%# Eval("MFNPId")%>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;
                                        visibility: hidden;">
                                        <font color="#565656"><b>PurchasedUnits:</b></font>
                                        <%# Eval("PurchasedUnits")%>
                                    </div>
                                     <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;
                                        visibility: hidden;">
                                        <font color="#565656"><b>Status:</b></font>
                                        <%# Eval("Status")%>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn>
                            <ItemTemplate>
                                <tr>
                                    <td colspan="100%">
                                        <telerik:RadGrid ID="gvChildDetails" runat="server" AllowPaging="false" AllowSorting="True"
                                            AutoGenerateColumns="False" ShowStatusBar="true" AllowAutomaticDeletes="false"
                                            AllowAutomaticInserts="false" AllowAutomaticUpdates="false" HorizontalAlign="NotSet"
                                            CellPadding="15" Visible="false">
                                            <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" AutoGenerateColumns="false"
                                                Width="100%" ShowHeader="false">
                                                <Columns>
                                                    <telerik:GridTemplateColumn>
                                                        <HeaderStyle />
                                                        <ItemTemplate>
                                                            <div class="col-sm-12 col-sm-12 col-md-12 col-lg-12">
                                                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                    <font color="#565656"><b>Transaction Type:</b></font>
                                                                    <%# Eval("Transaction Type")%>
                                                                </div>
                                                                <div class="col-xs-3 col-sm-3 col-md-3col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                    <font color="#565656"><b>Actioned NAV:<b> </font>
                                                                    <%# Eval("Price")%>
                                                                </div>
                                                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                    <font color="#565656"><b>Transaction Date:</b></font>
                                                                    <%# Eval("CO_OrderDate")%>
                                                                </div>
                                                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                    <font color="#565656"><b>Order No.:</b></font>
                                                                    <%# Eval("OrderNo")%>
                                                                </div>
                                                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                    <font color="#565656"><b>Dividend Type:</b></font>
                                                                    <%# Eval("DivReinvestment")%>
                                                                </div>
                                                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                    <font color="#565656"><b>Amount (Rs):</b></font>
                                                                    <%# Eval("Amount")%>
                                                                </div>
                                                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                    <font color="#565656"><b>Units:</b></font>
                                                                    <%# Eval("Units")%>
                                                                </div>
                                                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                    <font color="#565656"><b>Channel:</b></font>
                                                                    <%# Eval("Channel")%>
                                                                </div>
                                                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                    <font color="#565656"><b>Request Date/Time:</b></font>
                                                                    <%# Eval("CO_OrderDate")%>
                                                                </div>
                                                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                                                                    <font color="#565656"><b>Maturity Date:</b></font>
                                                                    <%# Eval("ELSSMaturityDate")%>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <%-- <ClientSettings EnableAlternatingItems="false" AllowGroupExpandCollapse="true">
        </ClientSettings>
        <PagerStyle Mode="NextPrevAndNumeric" />--%>
            </telerik:RadGrid>
        </div>
        <asp:HiddenField ID="hdnAccount" runat="server" Value="0" />
    </ContentTemplate>
    <Triggers>
       
    </Triggers>
</asp:UpdatePanel>
