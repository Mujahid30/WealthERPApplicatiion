<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerMFUnitHoldingList.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.CustomerMFUnitHoldingList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.bxslider.js" type="text/javascript"></script>

<asp:ScriptManager ID="scriptmanager" runat="server">
</asp:ScriptManager>
<style type="text/css">
    .style1
    {
        width: 37%;
    }
</style>

<script type="text/jscript">
    jQuery(document).ready(function($) {
        $('.bxslider').bxSlider(
    {
        auto: true,
        autoControls: true
    }
    );
    });

        
</script>

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
                            <telerik:GridTemplateColumn ItemStyle-Width="250px" AllowFiltering="false" HeaderText="Scheme Rating"
                                HeaderStyle-Width="125px" ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <a href="#" class="popper" data-popbox="divSchemeRatingDetails">
                                        <span class="FieldName"></span>
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
                                    <div id="divSchemeRatingDetails" class="popbox">
                                        <h2 class="popup-title">
                                            SCHEME RATING DETAILS
                                        </h2>
                                        <table border="1" cellpadding="1" cellspacing="2" style="border-collapse: collapse;">
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
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
<table width="100%" style="padding: 25px;">
    <tr>
        <td align="center">
            <div style="float: left; width: 98%">
                <ul class="bxslider">
                    <li>
                        <img src="../Images/InvestorPageSlider/1.jpg" /></li>
                    <li>
                        <img src="../Images/InvestorPageSlider/2.jpg" /></li>
                    <li>
                        <img src="../Images/InvestorPageSlider/3.jpg" /></li>
                    <li>
                        <img src="../Images/InvestorPageSlider/4.jpg" /></li>
                    <li>
                        <img src="../Images/InvestorPageSlider/5.jpg" /></li>
                </ul>
            </div>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnAccount" runat="server" Value="0" />
