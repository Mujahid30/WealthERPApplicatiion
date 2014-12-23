<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerMFUnitHoldingList.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.CustomerMFUnitHoldingList" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<style type="text/css">
    .style1
    {
        width: 37%;
    }
</style>
<asp:ScriptManager ID="scriptmanager" runat="server">
</asp:ScriptManager>
<%--<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Unit Holdings
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton Visible="false" ID="btnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>--%>
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
<div style="padding-top: 4px">
    <table style="width: 100%" class="TableBackground" style="padding-top: 4px">
        <tr>
            <td class="leftField" style="width: 10%">
                <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Account:"></asp:Label>
            </td>
            <td class="rightField" style="width: 15%">
                <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField">
                </asp:DropDownList>
            </td>
            <td class="rightField" style="width: 10%">
                <asp:Button ID="btnUnitHolding" runat="server" Text="GO" CssClass="PCGButton" OnClick="btnUnitHolding_Click" />
            </td>
            <td class="style1">
                <asp:Label ID="lblPickDate" Text="" runat="server" CssClass="FieldName" Visible="false"> </asp:Label>
            </td>
            <td style="margin-left: 80px" align="right">
                <asp:ImageButton Visible="true" ID="btnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                    OnClientClick="setFormat('excel')" Height="25px" Width="29px"></asp:ImageButton>
            </td>
        </tr>
    </table>
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
<asp:Panel ID="pnlMFUnitHolding" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal"
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
                     OnNeedDataSource="rgUnitHolding_OnNeedDataSource" AllowFilteringByColumn="true"--%>
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
                            <%--<telerik:GridTemplateColumn HeaderStyle-Width="275px" UniqueName="Scheme" SortExpression="Scheme"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                HeaderText="Scheme" Groupable="False" ItemStyle-Wrap="false" AllowFiltering="true"
                                DataField="Scheme" FilterControlWidth="250px">--%>
                            <%-- <ItemTemplate>
                                    <asp:LinkButton ID="lnkScheme" runat="server" Text='<%# Eval("Scheme").ToString() %>'
                                        CommandName="NavigateToMarketData"></asp:LinkButton>
                                </ItemTemplate>--%>
                            <%--</telerik:GridTemplateColumn> --%>
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
                                    <%-- Text='<%#(Eval("CurrentValue","{0:n3}").ToString()) %>' />--%>
                            <%--  </ItemTemplate>
                            </telerik:GridTemplateColumn>--%>
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
                            </telerik:GridBoundColumn>--%>
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
                            <telerik:GridTemplateColumn ItemStyle-Width="250px" AllowFiltering="false" HeaderText="Scheme Rating" HeaderStyle-Width="125px"
                                ItemStyle-Wrap="false" >
                               
                                <ItemTemplate>

                                    <script type="text/jscript">
                                        jQuery(document).ready(function($) {
                                            $('[data-popup-target]').click(function() {
                                                $('html').addClass('overlay');
                                                var activePopup = $(this).attr('data-popup-target');
                                                $(activePopup).addClass('visible');

                                            });

                                            $(document).keyup(function(e) {
                                                if (e.keyCode == 27 && $('html').hasClass('overlay')) {
                                                    clearPopup();
                                                }
                                            });

                                            $('.popup-exit').click(function() {
                                                clearPopup();

                                            });

                                            $('.popup-overlay').click(function() {
                                                clearPopup();
                                            });

                                            function clearPopup() {
                                                $('.popup.visible').addClass('transitioning').removeClass('visible');
                                                $('html').removeClass('overlay');

                                                setTimeout(function() {
                                                    $('.popup').removeClass('transitioning');
                                                }, 200);
                                            }

                                        });

    
                                    </script>

                                    <asp:Image ID="imgSchemeRating" runat="server" CommandName="MorningStarRating" data-popup-target="#Rating-popup" />
                                    <asp:Label ID="lblSchemeRating" runat="server" CssClass="cmbField" Text='<%# Eval("SchemeRatingOverall") %>'
                                        Visible="false">
                                    </asp:Label>
                                    <div id="Rating-popup" class="popup">
                                        <div class="popup-body">
                                            <span class="popup-exit"></span>
                                            <div class="popup-content">
                                                <h2 class="popup-title">
                                                    <asp:Image runat="server" ID="imgRatingDetails" />
                                                </h2>
                                                <p>
                                                    ©2014 Morningstar. All Rights Reserved. The information, data, analyses and opinions
                                                    (“Information”) contained herein: (1) include the proprietary information of Morningstar
                                                    and its content providers; (2) may not be copied or redistributed except as specifically
                                                    authorised; (3) do not constitute investment advice; (4) are provided solely for
                                                    informational purposes; (5) are not warranted to be complete, accurate or timely;
                                                    and (6) may be drawn from fund data published on various dates. Morningstar is not
                                                    responsible for any trading decisions, damages or other losses related to the Information
                                                    or its use. Please verify all of the Information before using it and don’t make
                                                    any investment decision except upon the advice of a professional financial adviser.
                                                    Past performance is no guarantee of future results. The value and income derived
                                                    from investments may go down as well as up.</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="popup-overlay">
                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
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
</asp:Panel>
<asp:HiddenField ID="hdnAccount" runat="server" Value="0" />
