<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineOrderTopMenu.ascx.cs"
    Inherits="WealthERP.OnlineOrder.OnlineOrderTopPanel" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table cellpadding="0" cellspacing="0" style="top: auto; vertical-align: top">
            <tr id="trMFOrderMenuTransactTab" runat="server" visible="false">
                <td>
                    <telerik:RadTabStrip ID="RTSMFOrderMenuTransact" runat="server" EnableTheming="True"
                        Skin="Telerik" EnableEmbeddedSkins="False" MultiPageID="RMPMFOrderMenuTransact"
                        SelectedIndex="0" AutoPostBack="true" OnTabClick="RTSMFOrderMenuTransact_TabClick">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="New Purchase" Value="RTSMFOrderMenuTransactNewPurchase"
                                TabIndex="0" PageViewID="RPVNewPurchase" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="ADDITIONAL PURCHASE" Value="RTSMFOrderMenuTransactAdditionalPurchase"
                                TabIndex="1" PageViewID="RPVAdditionalPurchase">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="REDEEM" Value="RTSMFOrderMenuTransactRedeem"
                                PageViewID="RPVRedeem" TabIndex="2">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="SIP" Value="RTSMFOrderMenuTransactSIP" TabIndex="3"
                                PageViewID="RPVSIP">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="NFO" Value="RTSMFOrderMenuTransactNFO" TabIndex="4"
                                PageViewID="RPVNFO" Visible="false">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="FMP" Value="RTSMFOrderMenuTransactFMP" TabIndex="5"
                                PageViewID="RPVFMP" Visible="false">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RMPMFOrderMenuTransact" EnableViewState="false" runat="server"
                        SelectedIndex="0" Width="100%">
                        <telerik:RadPageView ID="RPVPurchase" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVAdditionalPurchase" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVRedeem" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVSIP" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVNFO" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVFMP" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
            <tr id="trMFOrderMenuBooksTab" runat="server" visible="false">
                <td>
                    <telerik:RadTabStrip ID="RTSMFOrderMenuBooks" runat="server" EnableTheming="True"
                        Skin="Telerik" EnableEmbeddedSkins="False" MultiPageID="RMPMFOrderMenuBooks"
                        SelectedIndex="0" AutoPostBack="true" OnTabClick="RTSMFOrderMenuBooks_TabClick">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="ORDER BOOK" Value="RTSMFOrderMenuBooksOrderBook"
                                TabIndex="0" PageViewID="RPVOrderBook" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="TRANSACTION BOOK" Value="RTSMFOrderMenuBooksTransactionBook"
                                TabIndex="1" PageViewID="RPVTransactionBook">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="SIP BOOK" Value="RTSMFOrderMenuBooksSIPBook"
                                TabIndex="2" PageViewID="RPVSIPBook">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="DIVIDEND BOOK" Value="RTSMFOrderMenuBooksDividendBook"
                                TabIndex="2" PageViewID="RPVDividendBook">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RMPMFOrderMenuBooks" EnableViewState="false" runat="server"
                        SelectedIndex="0" Width="100%">
                        <telerik:RadPageView ID="RPVOrderBook" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVTransactionBook" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVSIPBook" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVDividendBook" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
            <tr id="trMFOrderMenuHoldingsTab" runat="server" visible="false">
                <td>
                    <telerik:RadTabStrip ID="RTSMFOrderMenuHoldings" runat="server" EnableTheming="True"
                        Skin="Telerik" EnableEmbeddedSkins="False" MultiPageID="RMPMFOrderMenuHoldings"
                        SelectedIndex="0" AutoPostBack="true" OnTabClick="RTSMFOrderMenuHoldings_TabClick">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="MF HOLDINGS" Value="RTSMFOrderMenuHoldingsMFHoldings"
                                TabIndex="0" PageViewID="RPVMFHoldings" Selected="True">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RMPMFOrderMenuHoldings" EnableViewState="false" runat="server"
                        SelectedIndex="0" Width="100%">
                        <telerik:RadPageView ID="RPVMFHoldings" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
        <%-- <asp:PostBackTrigger ControlID="imgexportButton" />--%>
    </Triggers>
</asp:UpdatePanel>
