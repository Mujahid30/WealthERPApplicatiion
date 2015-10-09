<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineOrderTopMenu.ascx.cs"
    Inherits="WealthERP.OnlineOrder.OnlineOrderTopPanel" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.2.6.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<style type="text/css">
   
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table cellpadding="0" cellspacing="0" style="top: auto; vertical-align: top; padding-left: 13px;"
            id="tblMF" visible="false" runat="server">
            <tr id="trMFOrderMenuMarketTab" runat="server" visible="false">
                <td>
                    <telerik:RadTabStrip ID="RTSMFOrderMenuHome" runat="server" EnableTheming="True"
                        Skin="Telerik" EnableEmbeddedSkins="False" MultiPageID="RMPMFOrderMenuTransact"
                        SelectedIndex="7" AutoPostBack="true" OnTabClick="RTSMFOrderMenuHome_TabClick">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="HOME" Value="RTSMFOrderMenuHomeMarket" TabIndex="0"
                                PageViewID="RPVMarket" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="SCHEME RESEARCH" Value="RTSMFOrderMenuHomeSchemeResearch"
                                TabIndex="1" PageViewID="RPVSchemeResearch" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="SCHEME COMPARE" Value="RTSMFOrderMenuHomeSchemeCompare"
                                TabIndex="2" PageViewID="RPVSchemeCompare" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="NEWS" Value="RTSMFOrderMenuHomeNews"
                                TabIndex="3" PageViewID="RPVNews" Selected="True">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RMPMFOrderMenuMarketHome" EnableViewState="false" runat="server"
                        SelectedIndex="0" Width="100%">
                        <telerik:RadPageView ID="RPVMarket" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVSchemeResearch" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVSchemeCompare" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVNews" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
                <td style="width:300px;">
                </td>
                <td align="right">
                    <asp:TextBox runat="server" ID="SchemeSearch" AutoPostBack="true" Style="margin-top: 0px;float: right;background-color:#D7E9F5" Width="300px" OnTextChanged="SchemeSearch_OnTextChanged"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="txtSchemeName_water" TargetControlID="SchemeSearch"
                        WatermarkText="Search Scheme" runat="server" EnableViewState="false">
                    </cc1:TextBoxWatermarkExtender>
                    <div id="listPlacement" style="height: 150px; overflow-y: scroll;text-align:left">
                    </div>
                    <ajaxToolkit:AutoCompleteExtender ID="txtSchemeName_AutoCompleteExtender" runat="server"
                        TargetControlID="SchemeSearch" ServiceMethod="GetInvestorScheme" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                        MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                        CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                        CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                        UseContextKey="True" OnClientItemSelected="GetSchemePlanCode" DelimiterCharacters=""
                        CompletionListElementID="listPlacement" Enabled="True" />
                </td>
            </tr>
            <tr id="trMFOrderMenuTransactTab" runat="server" visible="false">
                <td>
                    <telerik:RadTabStrip ID="RTSMFOrderMenuTransact" runat="server" EnableTheming="True"
                        Skin="Telerik" EnableEmbeddedSkins="False" MultiPageID="RMPMFOrderMenuTransact"
                        SelectedIndex="7" AutoPostBack="true" OnTabClick="RTSMFOrderMenuTransact_TabClick">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="NEW PURCHASE" Value="RTSMFOrderMenuTransactNewPurchase"
                                TabIndex="0" PageViewID="RPVNewPurchase" Selected="true">
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
                                PageViewID="RPVNFO">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="Switch" Value="RTSMFOrderMenuTransactSwitch"
                                TabIndex="5" PageViewID="RPVNFO" Visible="false">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="SWP" Value="RTSMFOrderMenuTransactSWP" TabIndex="6"
                                PageViewID="RPVNFO" Visible="false">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="STP" Value="RTSMFOrderMenuTransactSTP" TabIndex="7"
                                PageViewID="RPVNFO" Visible="false">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="FMP" Value="RTSMFOrderMenuTransactFMP" TabIndex="8"
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
                                TabIndex="1" PageViewID="RPVTransactionBook" Visible="false">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="SIP BOOK" Value="RTSMFOrderMenuBooksSIPBook"
                                TabIndex="2" PageViewID="RPVSIPBook">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="SWP BOOK" Value="RTSMFOrderMenuBooksSWPBook"
                                TabIndex="3" PageViewID="RPVSWPBook" Visible="false">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="DIVIDEND BOOK" Value="RTSMFOrderMenuBooksDividendBook"
                                TabIndex="2" PageViewID="RPVDividendBook" Visible="false">
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
                        <telerik:RadPageView ID="RPVSWPBook" runat="server" Style="margin-top: 20px">
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
        <table cellpadding="0" cellspacing="0" style="top: auto; vertical-align: top; padding-left: 13px;
            z-index: 2" id="tblNCD" visible="false" runat="server">
            <tr id="trNCDOrderMenuTransactTab" runat="server" visible="false">
                <td>
                    <telerik:RadTabStrip ID="RTSNCDOrderMenuTransact" runat="server" EnableTheming="True"
                        Skin="Telerik" EnableEmbeddedSkins="False" MultiPageID="RMPNCDOrderMenuTransact"
                        SelectedIndex="0" AutoPostBack="true" OnTabClick="RTSNCDOrderMenuTransact_TabClick">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="NCD ISSUE LIST" Value="RTSNCDOrderMenuTransactNCDIssueList"
                                TabIndex="0" PageViewID="RPVNCDIssueList" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="NCD ISSUE TRANSACT" Value="RTSNCDOrderMenuTransactIssueTransact"
                                TabIndex="1" PageViewID="RPVNCDIssueTransact" Visible="false">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RMPNCDOrderMenuTransact" EnableViewState="false" runat="server"
                        SelectedIndex="0" Width="100%">
                        <telerik:RadPageView ID="RPVNCDIssueList" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RPVNCDIssueTransact" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
            <tr id="trNCDOrderMenuBooksTab" runat="server" visible="false">
                <td>
                    <telerik:RadTabStrip ID="RTSNCDOrderMenuBooks" runat="server" EnableTheming="True"
                        Skin="Telerik" EnableEmbeddedSkins="False" MultiPageID="RMPNCDOrderMenuBooks"
                        SelectedIndex="0" AutoPostBack="true" OnTabClick="RTSNCDOrderMenuBooks_TabClick">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="NCD BOOK" Value="RTSNCDOrderMenuBooksNCDBook"
                                TabIndex="0" PageViewID="RPVNCDBook" Selected="True">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RMPNCDOrderMenuBooks" EnableViewState="false" runat="server"
                        SelectedIndex="0" Width="100%">
                        <telerik:RadPageView ID="RPVNCDBook" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
            <tr id="trNCDOrderMenuHoldingsTab" runat="server" visible="false">
                <td>
                    <telerik:RadTabStrip ID="RTSNCDOrderMenuHoldings" runat="server" EnableTheming="True"
                        Skin="Telerik" EnableEmbeddedSkins="False" MultiPageID="RMPMFOrderMenuHoldings"
                        SelectedIndex="0" AutoPostBack="true" OnTabClick="RTSNCDOrderMenuHoldings_TabClick">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="NCD HOLDING" Value="RTSNCDOrderMenuHoldingsNCDHolding"
                                TabIndex="0" PageViewID="RPVNCDHolding" Selected="True">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RMPNCDOrderMenuHoldings" EnableViewState="false" runat="server"
                        SelectedIndex="0" Width="100%">
                        <telerik:RadPageView ID="RPVNCDHolding" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="top: auto; vertical-align: top; padding-left: 13px;"
            id="tblIPO" visible="false" runat="server">
            <tr id="trIPOOrderMenuTransactTab" runat="server" visible="false">
                <td>
                    <telerik:RadTabStrip ID="RTSIPOOrderMenuTransact" runat="server" EnableTheming="True"
                        Skin="Telerik" EnableEmbeddedSkins="False" MultiPageID="RMPIPOOrderMenuTransact"
                        SelectedIndex="0" AutoPostBack="true" OnTabClick="RTSIPOOrderMenuTransact_TabClick">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="IPO/FPO ISSUE LIST" Value="RTSIPOOrderMenuTransactIPOIssueList"
                                TabIndex="0" PageViewID="RPVIPOIssueList" Selected="True">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RMPIPOOrderMenuTransact" EnableViewState="false" runat="server"
                        SelectedIndex="0" Width="100%">
                        <telerik:RadPageView ID="RPVIPOIssueList" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
            <tr id="trIPOOrderMenuBooksTab" runat="server" visible="false">
                <td>
                    <telerik:RadTabStrip ID="RTSIPPOOrderMenuBooks" runat="server" EnableTheming="True"
                        Skin="Telerik" EnableEmbeddedSkins="False" MultiPageID="RMPIPOOrderMenuBooks"
                        SelectedIndex="0" AutoPostBack="true" OnTabClick="RTSIPPOOrderMenuBooks_TabClick">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="IPO/FPO BOOK" Value="RTSIPOOrderMenuBooksIPOBook"
                                TabIndex="0" PageViewID="RPVIPOBook" Selected="True">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RMPIPOOrderMenuBooks" EnableViewState="false" runat="server"
                        SelectedIndex="0" Width="100%">
                        <telerik:RadPageView ID="RPVIPOBook" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
            <tr id="trIPOOrderMenuHoldingsTab" runat="server" visible="false">
                <td>
                    <telerik:RadTabStrip ID="RTSIPOOrderMenuHoldingsIPO" runat="server" EnableTheming="True"
                        Skin="Telerik" EnableEmbeddedSkins="False" MultiPageID="RMPIPOOrderMenuBooks"
                        SelectedIndex="0" AutoPostBack="true" OnTabClick="RTSIPOOrderMenuHoldingsIPO_TabClick">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="IPO/FPO HOLDINGS" Value="RTSIPOOrderMenuHoldingsIPOHolding"
                                TabIndex="0" PageViewID="RPVIPOHoldings" Selected="True">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RMPIPOOMenuHoldings" EnableViewState="false" runat="server"
                        SelectedIndex="0" Width="100%">
                        <telerik:RadPageView ID="RPVIPOHoldings" runat="server" Style="margin-top: 20px">
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
        <%-- <asp:PostBackTrigger ControlID="imgexportButton" />--%>
        <asp:PostBackTrigger ControlID="SchemeSearch" />
    </Triggers>
</asp:UpdatePanel>
<asp:HiddenField ID="schemeCode" runat="server" />

<script type="text/javascript" language="javascript">
    function GetSchemePlanCode(source, eventArgs) {
        isItemSelected = true;
        document.getElementById("<%= schemeCode.ClientID %>").value = eventArgs.get_value();

        return false;
    }
</script>

