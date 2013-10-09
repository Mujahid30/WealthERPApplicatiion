<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFOrderManagement.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.OnlineMFOrderManagement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/OnlineOrderManagement/MFOrderPurchaseTransType.ascx" TagPrefix="MFOPTT"
    TagName="Purchase" %>
<%@ Register Src="~/OnlineOrderManagement/MFOrderRdemptionTransType.ascx" TagPrefix="MFORTT"
    TagName="Rdemption" %>
<%@ Register Src="~/OnlineOrderManagement/MFOrderSIPTransType.ascx" TagPrefix="MFOSIPTT"
    TagName="SIP" %>
<%@ Register Src="~/OnlineOrderManagement/CustomerMFOrderBookList.ascx" TagPrefix="CMFOBL"
    TagName="OrderBookList" %>
<%@ Register Src="~/CustomerPortfolio/CustomerMutualFundPortfolioNPView.ascx" TagPrefix="CMFPNP"
    TagName="NPView" %>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.2.6.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<style type="text/css">
    .fltlft
    {
        float: left;
        padding-left: 3px;
        width: 20%;
    }
</style>

<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
        <div class="divSectionHeading">
            <div class="fltlft">
                <asp:LinkButton runat="server" ID="lnkTransact" CssClass="LinkButtons" Text="Transact"
                    OnClick="lnkTransact_Click"></asp:LinkButton>
            </div>
            <div class="fltlft">
                <asp:LinkButton runat="server" ID="lnkBooks" CssClass="LinkButtons" Text="Books"
                    OnClick="lnkBooks_Click"></asp:LinkButton>
            </div>
            <div class="fltlft">
                <asp:LinkButton runat="server" ID="lnkHoldings" CssClass="LinkButtons" Text="Unit Holdings"
                    OnClick="lnkHoldings_Click"></asp:LinkButton>
            </div>
        </div>
        <table width="100%">
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr id="trTransact" runat="server">
                <td>
                    <telerik:RadTabStrip ID="RTSTransact" runat="server" Skin="Telerik" MultiPageID="Transact"
                        SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Purchase" Value="Purchase" TabIndex="0" PageViewID="RPVPurchase"
                                Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="Redeem" Value="Redeem" PageViewID="RPVRedeem"
                                TabIndex="1">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="SIP" Value="SIP" TabIndex="2" PageViewID="RPVSIP">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="Switch" Value="Switch" TabIndex="3" PageViewID="RPVSwitch">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="TI" Value="TI" TabIndex="4" PageViewID="RPVTI">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="TO" Value="TO" TabIndex="5" PageViewID="RPVTO">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RMPTransact" EnableViewState="false" runat="server" SelectedIndex="0"
                        Width="100%">
                        <telerik:RadPageView ID="RPVPurchase" runat="server" Style="margin-top: 20px">
                            <MFOPTT:Purchase ID="PurchasePageCall" runat="server" />
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVRedeem" runat="server" Style="margin-top: 20px">
                            <MFORTT:Rdemption ID="RedeemPageCall" runat="server" />
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVSIP" runat="server" Style="margin-top: 20px">
                            <MFOSIPTT:SIP ID="Purchase2" runat="server" />
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVSwitch" runat="server" Style="margin-top: 20px">
                            <MFOPTT:Purchase ID="Purchase3" runat="server" />
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVTI" runat="server" Style="margin-top: 20px">
                            <MFOPTT:Purchase ID="Purchase4" runat="server" />
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVTO" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
            <tr id="trBooks" runat="server">
                <td>
                    <telerik:RadTabStrip ID="RTSOrderBooks" runat="server" Skin="Telerik" MultiPageID="Transact"
                        SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Order Book" Value="Order_Book" TabIndex="0"
                                PageViewID="RPVOrderBook" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="SIP Book" Value="SIP_Book" PageViewID="RPVSIPBook"
                                TabIndex="1">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="TI/TO Book" Value="TI_TI_Book" TabIndex="2"
                                PageViewID="RPVTITOBok">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RMPOrderBooks" EnableViewState="false" runat="server" SelectedIndex="0"
                        Width="100%">
                        <telerik:RadPageView ID="RPVOrderBook" runat="server" Style="margin-top: 20px">
                            <CMFOBL:OrderBookList ID="OrderBookList" runat="server" />
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVSIPBook" runat="server" Style="margin-top: 20px">
                            <CMFOBL:OrderBookList ID="OrderBookList1" runat="server" />
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVTITOBok" runat="server" Style="margin-top: 20px">
                            <CMFOBL:OrderBookList ID="OrderBookList2" runat="server" />
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
            <tr id="trUnitHoldings" runat="server">
                <td>
                    <CMFPNP:NPView ID="CustomerNPView" runat="server" />
                </td>
            </tr>
        </table>
   <%-- </ContentTemplate>
    <Triggers>
        
    </Triggers>
</asp:UpdatePanel>--%>
