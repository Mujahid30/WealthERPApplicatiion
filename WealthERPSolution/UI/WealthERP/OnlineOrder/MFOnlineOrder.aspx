<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MFOnlineOrder.aspx.cs"
    Inherits="WealthERP.OnlineOrder.MFOnlineOrder" %>

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

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true" />
        <%--   <table width="100%">
                    <tr>
                        <td>
                            <div class="divSectionHeading">
                                <table>
                                    <tr>
                                        <td align="right">
                                            <asp:LinkButton ID="lblSignOut" runat="server" Text="" Style="text-decoration: none"
                                                CssClass="LinkButtons" OnClick="lblSignOut_Click"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>                             
                                <tr id="trOrderBooks" runat="server">
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
                        </td>
                    </tr>
                </table>--%>
        <table>
            <tr id="trHeaderFrame" runat="server">
                <td>
                </td>
            </tr>
            <tr id="trTopMenuFrame" runat="server">
                <td>
                    <iframe name="leftframe" id="leftframe" onload="javascript:calcHeight('leftframe');"
                        src="~\\ControlLeftHost.aspx"></iframe>
                </td>
            </tr>
            <tr id="trMainFrame" runat="server">
                <td>
                </td>
                <iframe name="mainframe" class="MainFrame" id="mainframe" onload="javascript:calcHeight('mainframe');"
                    src="ControlHost.aspx"></iframe>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
