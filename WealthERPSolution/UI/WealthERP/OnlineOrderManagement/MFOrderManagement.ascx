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

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.2.6.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script type="text/javascript" language="javascript">
    function DisplayMenuType(type) {       
        if (type == 'TRANSACT') {
            document.getElementById("<%= trTransact.ClientID %>").style.display = 'block';
            document.getElementById("<%= trOrderBooks.ClientID %>").style.display = 'none';
            document.getElementById("<%= trUnitHoldings.ClientID %>").style.display = 'none';
        }
        else if (type == 'ORDER_BOOKS') {
            document.getElementById("<%= trTransact.ClientID %>").style.display = 'none';
            document.getElementById("<%= trOrderBooks.ClientID %>").style.display = 'block';
            document.getElementById("<%= trUnitHoldings.ClientID %>").style.display = 'none';
        }
        else if (type == 'UNIT_HOLDINGS') {
            document.getElementById("<%= trTransact.ClientID %>").style.display = 'none';
            document.getElementById("<%= trOrderBooks.ClientID %>").style.display = 'none';
            document.getElementById("<%= trUnitHoldings.ClientID %>").style.display = 'block';
        }
    }
            
</script>

<style type="text/css">
    .fltlft
    {
        float: left;
        padding-left: 3px;
        width: 20%;
    }
    ul#nav
    {
        clear: both;
        float: left;
        margin: 0;
        padding: 0;
        list-style: none;
        width: 100%;
    }
    ul#nav li
    {
        margin: 0;
        padding: 2px;
        float: left;
        width: 30%; /*width:16.66%;*/
        text-align: center;
    }
    ul#nav li a
    {
        display: block;
        font-size: small;
        color: #FFFFFF;
        font-weight: bold;
        text-decoration: none;
        background: #5588CC;
        padding: 2px 0px 2px 0px;
    }
    ul#nav li a:hover
    {
        background: #3366CC;
        color: #FFFFFF;
    }
    ul#nav a.selected:link
    {
        background: #3366CC;
        color: #FFFFFF;
    }
    ul#nav a.selected:visited
    {
        background: #3366CC;
        color: #FFFFFF;
    }
</style>
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
<%--<div class="divSectionHeading">--%>
<%-- <div class="fltlft">
        <%-- <asp:Label ID="lblPAN" runat="server" CssClass="FieldName" Text="PAN" OnClientClick="return DisplayMenuType('TRANSACT');return false"></asp:Label><asp:TextBox--%>
<%--<asp:LinkButton runat="server" ID="lnkTransact" CssClass="LinkButtons" Text="Transact"   OnClientClick="return DisplayMenuType('TRANSACT');return false" > </asp:LinkButton>--%>
<%--</div>--%>
<%--  <div class="fltlft">
        <asp:Button ID="btnExportToPDF" runat="server" CssClass="PDFButton" OnClientClick="return DisplayMenuType('ORDER_BOOKS');return false" />--%>
<%--<asp:LinkButton runat="server" ID="lnkBooks" CssClass="LinkButtons" Text="Books" OnClientClick="return DisplayMenuType('ORDER_BOOKS');return false"> </asp:LinkButton>--%>
<%-- </div>
    <div class="fltlft">--%>
<%-- <asp:LinkButton runat="server" ID="lnkHoldings" CssClass="LinkButtons" Text="Unit Holdings"  OnClientClick="return DisplayMenuType('UNIT_HOLDINGS');return false"> </asp:LinkButton>--%>
<%--  <asp:Button ID="Button1" runat="server" CssClass="PDFButton" OnClientClick="return DisplayMenuType('UNIT_HOLDINGS');return false" />
    </div>
</div>--%>
<%--<div id="navigation" class="divSectionHeading">
</div>--%>
<table width="100%">
    <tr>
        <td>
            <ul id="nav">
                <li><a href="#" onclick="DisplayMenuType('TRANSACT');">Transact</a> </li>
                <li><a href="#" onclick="DisplayMenuType('ORDER_BOOKS');">Books</a> </li>
                <li><a href="#" onclick="DisplayMenuType('UNIT_HOLDINGS');">Unit Holdings</a> </li>
            </ul>
        </td>
    </tr>
    <tr id="trTransact" runat="server" style="display: none">
        <td>
            <telerik:RadTabStrip ID="RTSTransact" runat="server" Skin="Telerik" MultiPageID="RMPTransact"
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
    <tr id="trOrderBooks" runat="server" style="display: none">
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
                </telerik:RadPageView>
                <telerik:RadPageView ID="RPVTITOBok" runat="server" Style="margin-top: 20px">
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </td>
    </tr>
    <tr id="trUnitHoldings" runat="server" style="display: none">
        <td>
        </td>
    </tr>
</table>
<%-- </ContentTemplate>
    <Triggers>
        
    </Triggers>
</asp:UpdatePanel>--%>
