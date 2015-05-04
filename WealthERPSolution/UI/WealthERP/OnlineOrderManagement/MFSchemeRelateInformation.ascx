<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFSchemeRelateInformation.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.MFSchemeRelateInformation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<asp:ScriptManager ID="scriptmanager" runat="server">
</asp:ScriptManager>
<style type="text/css">
    .style1
    {
        width: 37%;
    }
</style>
<table width="100%" class="TableBackground">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Scheme Information
                        </td>
                        <td align="right" style="padding-bottom: 2px;">
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td align="right">
            <asp:Label ID="lblAMC" runat="server" CssClass="FieldName" Text="AMC:">
            </asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlAMC" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlAMC_SelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Label ID="lblScheme" runat="server" CssClass="FieldName" Text="Scheme:">
            </asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Category:">
            </asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btnGo" runat="server" CssClass="PCGButton" Text="Go" OnClick="Go_OnClick" />
        </td>
    </tr>
</table>
<asp:Panel ID="pnlSchemeDetails" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal"
    Visible="false">
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="rgSchemeDetails" runat="server" PageSize="10" AllowPaging="True"
                    GridLines="None" AutoGenerateColumns="true" Width="100%" ClientSettings-AllowColumnsReorder="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" AllowSorting="true" EnableViewState="true"
                    AllowFilteringByColumn="true" OnItemDataBound="rgSchemeDetails_ItemDataBound"
                    OnItemCommand="rgSchemeDetails_OnItemCommand" OnNeedDataSource="rgSchemeDetails_OnNeedDataSource">
                    <%-- OnItemDataBound="rgUnitHolding_ItemDataBound" AllowSorting="true" EnableViewState="true"
                     OnNeedDataSource="rgUnitHolding_OnNeedDataSource" AllowFilteringByColumn="true"--%>
                    <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                    <MasterTableView DataKeyNames="PASP_SchemePlanCode" ShowFooter="true" Width="105%"
                        AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-Width="200px" SortExpression="PA_AMCName" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="PA_AMCName"
                                HeaderText="AMC" DataField="PA_AMCName" AllowFiltering="true" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="200px" SortExpression="PASP_SchemePlanName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                UniqueName="PASP_SchemePlanName" HeaderText="SchemePlan Name" DataField="PASP_SchemePlanName"
                                AllowFiltering="true" FooterText="Grand Total:" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" UniqueName="PAIC_AssetInstrumentCategoryName"
                                SortExpression="PAIC_AssetInstrumentCategoryName" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" HeaderText="Category"
                                DataField="PAIC_AssetInstrumentCategoryName" AllowFiltering="true">
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn ItemStyle-Width="100px" AllowFiltering="false" HeaderText="Scheme Rating"
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
                            <telerik:GridBoundColumn HeaderStyle-Width="80px" UniqueName="PASPD_ExitLoadPercentage"
                                HeaderText="Exit Load" DataField="PASPD_ExitLoadPercentage" FooterStyle-HorizontalAlign="Right"
                                AllowFiltering="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="86px" UniqueName="CMFNP_NAVDate " HeaderText="NAV Date"
                                DataField="CMFNP_NAVDate" AllowFiltering="false" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CMFNP_CurrentValue" HeaderText="NAV" DataField="CMFNP_CurrentValue"
                                FooterStyle-HorizontalAlign="Right" AllowFiltering="false" HeaderStyle-Width="86px">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn ItemStyle-Width="100px" AllowFiltering="false" HeaderText="Action"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSIPSchemeFlag" runat="server" CssClass="cmbField" Text='<%# Eval("IsSchemeSIPType") %>'>
                                    </asp:Label>
                                    <asp:Label ID="lblIsPurcheseFlag" runat="server" CssClass="cmbField" Text='<%# Eval("IsSchemePurchege")%>'>
                                    </asp:Label>
                                    <asp:ImageButton ID="imgBuy" runat="server" CommandName="Buy" ImageUrl="~/Images/Buy-Button.png"
                                        ToolTip="BUY" />&nbsp;
                                    <asp:ImageButton ID="imgSip" runat="server" CommandName="SIP" ImageUrl="~/Images/SIP-Button.png"
                                        ToolTip="SIP" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
