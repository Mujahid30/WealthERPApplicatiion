<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerMFUnitHoldingList.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.CustomerMFUnitHoldingList" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>--%>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css">
<!-- Optional theme -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap-theme.min.css">

<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>

<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css">

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>

<script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>

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
<asp:ScriptManager ID="scriptmanager" runat="server">
</asp:ScriptManager>
<style type="text/css">
    .style1
    {
        width: 37%;
    }
</style>
<table width="100%">
    <tr>
        <td>
            <div class="divOnlinePageHeading" style="float: right; width: 100%">
                <div style="float: right; padding-right: 100px; height: 41px;">
                    <table cellspacing="0" cellpadding="3" style="width: 105%">
                        <tr>
                            <td align="right" style="width: 5%">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
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
                            MF Holdings
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<div id="divConditional" runat="server" class="row " style="margin-left: 5%; margin-bottom: 0.5%;
    margin-right: 5%; padding-bottom: 0.5%;">
    <div class="col-md-12 col-xs-12 col-sm-12" style="margin-bottom: 1%">
        <div class="col-md-4">
            Account
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="form-control input-sm">
            </asp:DropDownList>
        </div>
        <div class="col-md-3">
            <br />
            <asp:Button ID="btnUnitHolding" runat="server" Text="GO" CssClass="btn btn-primary btn-primary"
                OnClick="btnUnitHolding_Click" />
            <asp:Label ID="lblPickDate" Text="" runat="server" CssClass="FieldName" Visible="false"> </asp:Label>
        </div>
        <div style="float: right">
            <asp:ImageButton Visible="true" ID="btnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                OnClientClick="setFormat('excel')" Height="25px" Width="29px"></asp:ImageButton>
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
<%--<asp:Panel ID="pnlMFUnitHolding" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal"
    Visible="false">
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="rgUnitHolding" runat="server" PageSize="10" AllowPaging="True"
                    GridLines="None" AutoGenerateColumns="true" Width="100%" ClientSettings-AllowColumnsReorder="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" AllowSorting="true" EnableViewState="true"
                    AllowFilteringByColumn="true" OnNeedDataSource="rgUnitHolding_OnNeedDataSource"
                    OnItemCommand="rgUnitHolding_OnItemCommand" OnItemDataBound="rgUnitHolding_ItemDataBound">
                    <%-- OnItemDataBound="rgUnitHolding_ItemDataBound" AllowSorting="true" EnableViewState="true"
                     OnNeedDataSource="rgUnitHolding_OnNeedDataSource" AllowFilteringByColumn="true"
                    <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                    <MasterTableView DataKeyNames="MFNPId,AccountId,AMCCode,SchemeCode" ShowFooter="true"
                        Width="105%" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridBoundColumn Visible="false" HeaderStyle-Width="100px" SortExpression="MFNPId"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                UniqueName="MFNPId" HeaderText="MFNPId" DataField="MFNPId" AllowFiltering="true"
                                FooterText="Grand Total:" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" HeaderStyle-Width="85px" SortExpression="Category"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                UniqueName="Category" HeaderText="Category" DataField="Category" AllowFiltering="true"
                                FooterText="Grand Total:" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" HeaderStyle-Width="120px" UniqueName="SubCategoryName"
                                SortExpression="SubCategoryName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" HeaderText="SubCategory" DataField="SubCategoryName" AllowFiltering="true">
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="100px" SortExpression="FolioNum" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="FolioNum"
                                HeaderText="Folio" DataField="FolioNum">
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" UniqueName="AmcName" SortExpression="AmcName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                HeaderText="Fund Name" DataField="AmcName" AllowFiltering="true">
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="275px" UniqueName="Scheme" SortExpression="Scheme"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                HeaderText="Scheme" DataField="Scheme" AllowFiltering="true" FilterControlWidth="250px">
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn ItemStyle-Width="250px" AllowFiltering="false" HeaderText="Scheme Rating"
                                HeaderStyle-Width="125px" ItemStyle-Wrap="false">
                                <ItemTemplate>

                                    <script type="text/jscript">

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
                                                windowLeft = $(window).width() - 40;
                                                windowRight = 0;
                                                maxLeft = e.pageX - (parseInt(moveLeft) + $(target).outerWidth() + 20);

                                                if (maxRight > windowLeft && maxLeft > windowRight) {
                                                    leftD = maxLeft;
                                                }

                                                topD = e.pageY - parseInt(moveDown);
                                                maxBottom = parseInt(e.pageY + parseInt(moveDown) + 20);
                                                windowBottom = parseInt(parseInt($(document).scrollTop()) + parseInt($(window).height()));
                                                maxTop = topD;
                                                windowTop = parseInt($(document).scrollTop());
                                                if (maxBottom > windowBottom) {
                                                    topD = windowBottom - $(target).outerHeight() - 20;
                                                } else if (maxTop < windowTop) {
                                                    topD = windowTop + 20;
                                                }

                                                $(target).css('top', topD).css('left', leftD);


                                            });

                                        });
    
                                    </script>

                                    <a href="#" class="popper" data-popbox="divSchemeRatingDetails"><span class="FieldName">
                                    </span>
                                        <asp:Image runat="server" ID="imgSchemeRating" />
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
                                    <div id="divSchemeRatingDetails" class="popbox" runat="server" style="float: left;">
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
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <%--<telerik:GridTemplateColumn HeaderStyle-Width="275px" UniqueName="Scheme" SortExpression="Scheme"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                HeaderText="Scheme" Groupable="False" ItemStyle-Wrap="false" AllowFiltering="true"
                                DataField="Scheme" FilterControlWidth="250px">12121
                            <%-- <ItemTemplate>
                                    <asp:LinkButton ID="lnkScheme" runat="server" Text='<%# Eval("Scheme").ToString() %>'
                                        CommandName="NavigateToMarketData"></asp:LinkButton>
                                </ItemTemplate>12121
                            <%--</telerik:GridTemplateColumn> 12121
                            <telerik:GridDateTimeColumn Visible="false" HeaderStyle-Width="100px" DataField="FolioStartDate"
                                SortExpression="FolioStartDate" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                AllowFiltering="false" HeaderText="Scheme Invst. Date" UniqueName="FolioStartDate"
                                DataFormatString="{0:d}" HtmlEncode="False">
                                <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                <FilterTemplate>
                                    <telerik:RadDatePicker ID="resolveDateFilter" runat="server">
                                    </telerik:RadDatePicker>
                                </FilterTemplate>
                            </telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn Visible="false" HeaderStyle-Width="100px" DataField="InvestmentStartDate"
                                SortExpression="InvestmentStartDate" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                AllowFiltering="false" HeaderText="Holding Start Date" UniqueName="InvestmentStartDate"
                                DataFormatString="{0:d}">
                                <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                <FilterTemplate>
                                    <telerik:RadDatePicker ID="InvestmentStartDateFilter" runat="server">
                                    </telerik:RadDatePicker>
                                </FilterTemplate>
                            </telerik:GridDateTimeColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="80px" UniqueName="PurchasedUnits"
                                HeaderText="Units" DataField="PurchasedUnits" AllowFiltering="false" FooterAggregateFormatString="{0:N3}"
                                FooterStyle-HorizontalAlign="Right">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkprAmcB" runat="server" CommandName="SelectTransaction" Text='<%# String.Format("{0:N3}", DataBinder.Eval(Container.DataItem, "PurchasedUnits")) %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn Visible="false" HeaderStyle-Width="80px" UniqueName="DVRUnits"
                                HeaderText="DVR Units" DataField="DVRUnits" AllowFiltering="false" FooterStyle-HorizontalAlign="Right"
                                Aggregate="Sum" DataFormatString="{0:N3}">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" HeaderStyle-Width="80px" UniqueName="OpenUnits"
                                HeaderText="Total Units" DataField="OpenUnits" FooterStyle-HorizontalAlign="Right"
                                DataFormatString="{0:N3}" Aggregate="Sum" AllowFiltering="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="80px" UniqueName="InvestedCost" HeaderText="Invested Value"
                                DataField="InvestedCost" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}"
                                Aggregate="Sum" AllowFiltering="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="80px" UniqueName="NAV" HeaderText="NAV"
                                DataField="NAV" FooterStyle-HorizontalAlign="Right" AllowFiltering="false" DataFormatString="{0:N4}">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="86px" UniqueName="CMFNP_NAVDate " HeaderText="NAV Date"
                                DataField="CMFNP_NAVDate" AllowFiltering="false" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="65px" UniqueName="TotalPL" HeaderText="Unrealised Gain/Loss"
                                DataField="TotalPL" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right"
                                AllowFiltering="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CurrentValue" HeaderText="Current Value" DataField="CurrentValue"
                                FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" AllowFiltering="false"
                                HeaderStyle-Width="86px" Aggregate="Sum">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <%--  <telerik:GridTemplateColumn AllowFiltering="false" DataField="UnitsSold" AutoPostBackOnFilter="true"
                                HeaderText="Units Sold" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="CurrentValue" Aggregate="Sum" FooterText=" " FooterStyle-HorizontalAlign="Right"
                                FooterAggregateFormatString="{0:n3}">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkprAmcB" runat="server" CommandName="SelectTransaction" Text='<%# String.Format("{0:N3}", DataBinder.Eval(Container.DataItem, "UnitsSold")) %>'>
                                    </asp:LinkButton>
                                    <%-- Text='<%#(Eval("CurrentValue","{0:n3}").ToString()) %>' />1212
                            <%--  </ItemTemplate>
                            </telerik:GridTemplateColumn>-121
                            <telerik:GridBoundColumn HeaderStyle-Width="80px" UniqueName="UnitsSold" HeaderText="Units Sold"
                                DataField="UnitsSold" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"
                                AllowFiltering="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="80px" UniqueName="RedeemedAmount" HeaderText="Sold Value"
                                DataField="RedeemedAmount" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}"
                                Aggregate="Sum" AllowFiltering="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <%-- <telerik:GridBoundColumn HeaderStyle-Width="80px" UniqueName="RedeemedAmount" HeaderText="Sold Price"
                                DataField="RedeemedAmount" FooterStyle-HorizontalAlign="Right" AllowFiltering="false"
                                Aggregate="Sum" DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>1212
                            <telerik:GridBoundColumn Visible="false" HeaderStyle-Width="46px" UniqueName="DVP"
                                HeaderText="DVP" DataField="DVP" AllowFiltering="false" DataFormatString="{0:N0}"
                                FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="65px" UniqueName="RealizesdGain" HeaderText="Realised Gain/Loss"
                                DataField="RealizesdGain" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right"
                                AllowFiltering="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" HeaderStyle-Width="59px" UniqueName="AbsoluteReturn"
                                HeaderText="Unrealised Gain/Loss" DataField="AbsoluteReturn" AllowFiltering="false"
                                DataFormatString="{0:N2}">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" HeaderStyle-Width="59px" UniqueName="DVR"
                                HeaderText="DVR" DataField="DVR" DataFormatString="{0:N0}" AllowFiltering="false"
                                FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" HeaderStyle-Width="63px" UniqueName="XIRR"
                                HeaderText="XIRR (%)" DataField="XIRR" AllowFiltering="false" DataFormatString="{0:N2}">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" HeaderStyle-Width="87px" UniqueName="TotalDividends"
                                HeaderText="Total Dividends" DataField="TotalDividends" FooterStyle-HorizontalAlign="Right"
                                DataFormatString="{0:N0}" Aggregate="Sum" AllowFiltering="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" UniqueName="CMFNP_ValuationDate" HeaderText="Valuation As On"
                                HeaderStyle-Width="86px" DataField="CMFNP_ValuationDate" AllowFiltering="false"
                                FooterStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn ItemStyle-Width="140px" AllowFiltering="false" HeaderText="Action"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSIPSchemeFlag" runat="server" CssClass="cmbField" Text='<%# Eval("IsSchemeSIPType") %>'>
                                    </asp:Label>
                                    <asp:Label ID="lblIsPurcheseFlag" runat="server" CssClass="cmbField" Text='<%# Eval("IsSchemePurchege")%>'>
                                    </asp:Label>
                                    <asp:Label ID="lblISRedeemFlag" runat="server" CssClass="cmbField" Text='<%# Eval("IsSchemeRedeem") %>'>
                                    </asp:Label>
                                    <asp:ImageButton ID="imgBuy" runat="server" CommandName="Buy" ImageUrl="~/Images/Buy-Button.png"
                                        ToolTip="BUY" />&nbsp;
                                    <asp:ImageButton ID="imgSell" runat="server" CommandName="Sell" ImageUrl="~/Images/Sell-Button.png"
                                        ToolTip="SELL" />&nbsp;
                                    <asp:ImageButton ID="imgSip" runat="server" CommandName="SIP" ImageUrl="~/Images/SIP-Button.png"
                                        ToolTip="SIP" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>--%>
<div id="Div1" class="row" style="margin-left: 5%; margin-right: 3%; background-color: #2480C7;"
    visible="false" runat="server">
    <telerik:RadGrid ID="rgUnitHolding" runat="server" GridLines="None" AllowPaging="True"
        PageSize="5" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
        AllowAutomaticDeletes="false" AllowAutomaticInserts="false" AllowAutomaticUpdates="false"
        HorizontalAlign="NotSet" CellPadding="15" OOnNeedDataSource="rgUnitHolding_OnNeedDataSource"
        OnItemCommand="rgUnitHolding_OnItemCommand" OnItemDataBound="rgUnitHolding_ItemDataBound">
        <MasterTableView CommandItemDisplay="None" DataKeyNames="MFNPId,AccountId,AMCCode,SchemeCode"
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
                        <font color="#565656"><b>Fund Name:</b></font>
                        <%# Eval("AmcName")%>
                    </div>
                    <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6 fk-font-6" style="margin-bottom: 1.5px;">
                        <font color="#565656"><b>Scheme Name:</b> </font>
                        <%# Eval("Scheme")%>
                    </div>
                    
                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                        <font color="#565656"><b>Scheme Rating:</b></font>

                        <script type="text/jscript">

                            jQuery(document).ready(function($) {
                                var moveLeft = 0;
                                var moveDown = 0;
                                $('a.popper').hover(function(e) {

                                    //var target = '#' + ($(this).attr('data-popbox'));
                                    var target = '#' + ($(this).find('img').attr('id')).replace('imgSchemeRating', 'divSchemeRatingDetails');

                                    $(target).show();
                                    moveLeft = $(this).outerWidth();
                                    moveDown = ($(target).outerHeight() / 0);
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
                                    //                                            if (maxBottom > windowBottom) {
                                    //                                                topD = windowBottom - $(target).outerHeight() - 20;
                                    //                                            } else if (maxTop < windowTop) {
                                    //                                                topD = windowTop ;
                                    //                                            }

                                    $(target).css('top', topD).css('centre', leftD);
                                });
                            });
    
                        </script>

                        <a href="#" class="popper" data-popbox="divSchemeRatingDetails"><span class="FieldName">
                        </span>
                            <asp:Image runat="server" ID="imgSchemeRating" />
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
                        <font color="#565656"><b>PurchasedUnits:</b></font>
                        <%# Eval("PurchasedUnits")%>
                    </div>
                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                        <font color="#565656"><b>Units:</b></font>
                        <asp:LinkButton ID="lnkprAmcB" runat="server" style="color:#0396CC" CommandName="SelectTransaction" Text='<%# String.Format("{0:N3}", DataBinder.Eval(Container.DataItem, "PurchasedUnits")) %>'>
                        </asp:LinkButton>
                    </div>
                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                        <font color="#565656"><b>Invested Cost:</b></font>
                        <%# Eval("InvestedCost")%>
                    </div>
                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                        <font color="#565656"><b>NAV:</b></font>
                        <%# Eval("NAV")%>
                    </div>
                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                        <font color="#565656"><b>Unrealised Gain/Loss:</b></font>
                        <%# Eval("TotalPL")%>
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
                        <font color="#565656"><b>Sold Value:</b></font>
                        <%# Eval("RedeemedAmount")%>
                    </div>
                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                        <font color="#565656"><b>Realised Gain/Loss:</b></font>
                        <%# Eval("RealizesdGain")%>
                    </div>
                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;">
                        <font color="#565656"><b>Action:</b></font>
                        <asp:Label ID="lblSIPSchemeFlag" runat="server" CssClass="cmbField" Text='<%# Eval("IsSchemeSIPType") %>'>
                        </asp:Label>
                        <asp:Label ID="lblIsPurcheseFlag" runat="server" CssClass="cmbField" Text='<%# Eval("IsSchemePurchege")%>'>
                        </asp:Label>
                        <asp:Label ID="lblISRedeemFlag" runat="server" CssClass="cmbField" Text='<%# Eval("IsSchemeRedeem") %>'>
                        </asp:Label>
                        <asp:ImageButton ID="imgBuy" runat="server" CommandName="Buy" ImageUrl="~/Images/Buy-Button.png"
                            ToolTip="BUY" OnClientClick="LoadTransactPanel('MFOrderPurchaseTransType');"/>&nbsp;
                        <asp:ImageButton ID="imgSell" runat="server" CommandName="Sell" ImageUrl="~/Images/Sell-Button.png"
                            ToolTip="SELL" OnClientClick="LoadTransactPanel('MFOrderPurchaseTransType');"/>&nbsp;
                        <asp:ImageButton ID="imgSip" runat="server" CommandName="SIP" ImageUrl="~/Images/SIP-Button.png"
                            ToolTip="SIP" OnClientClick="LoadTransactPanel('MFOrderPurchaseTransType');"/>
                    </div>
                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3 fk-font-3" style="margin-bottom: 1.5px;
                        visibility: hidden;">
                        <font color="#565656"><b>InProcessCount:</b></font>
                        <%# Eval("MFNPId")%>
                    </div>
                </div>
            </ItemTemplate>
            </telerik:GridTemplateColumn>
           <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <tr>
                            <td colspan="100%">
                                <telerik:RadGrid ID="gvChildDetails" runat="server"  AllowPaging="false"
                                    AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="true"
                                    AllowAutomaticDeletes="false" AllowAutomaticInserts="false" AllowAutomaticUpdates="false"
                                    HorizontalAlign="NotSet" CellPadding="15" Visible="false" >
                                    <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" AutoGenerateColumns="false"
                                        Width="100%" ShowHeader="false" >
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
