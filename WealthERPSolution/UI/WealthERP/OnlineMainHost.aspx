<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnlineMainHost.aspx.cs"
    Inherits="WealthERP.OnlineMainHost" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.2.6.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script language="javascript" type="text/javascript" src="Scripts/JScript.js"></script>

<script language="javascript" type="text/javascript">
    function calcIFrameHeight(ifrm_id) {
        try {
            setTimeout("calc('" + ifrm_id + "')", 500);
        }
        catch (e) { }
    }


    function calc(iframe_id) {
        try {
            var leftframe_height = document.getElementById('leftframe').contentWindow.document.body.scrollHeight;
            var mainframe_height = document.getElementById('mainframe').contentWindow.document.body.scrollHeight;
            var the_height = (leftframe_height > mainframe_height) ? leftframe_height : mainframe_height;
            if (the_height > 600) {
                var newHeight = the_height + 250;
                if (document.getElementById('leftframe').height != newHeight)
                    document.getElementById('leftframe').height = newHeight;
                if (document.getElementById('mainframe').height != newHeight)
                    document.getElementById('mainframe').height = newHeight;
                if (iframe_id == 'mainframe') {
                    if (document.getElementById('splitter_bar_left').style.height != newHeight)
                        document.getElementById('splitter_bar_left').style.height = newHeight + 'px';
                }

            }
            else {
                if (document.getElementById(iframe_id).height != 600)
                    document.getElementById(iframe_id).height = 600;
                if (iframe_id == 'mainframe') {
                    if (document.getElementById('splitter_bar_left').style.height != 600)
                        document.getElementById('splitter_bar_left').style.height = 600 + 'px';
                }
            }

            //if (timerEvent != null) window.clearInterval(timerEvent);
            //                timerEvent = window.setTimeout("calc('" + iframe_id + "')", 1000);
        }
        catch (e) { }
    }
</script>

<%--<link href="../App_Themes/Blue/StyleSheet.css" rel="stylesheet" type="text/css" />--%>
<style type="text/css">
    #topframe
    {
        width: 100%;
        border: none;
    }
    #bottomframe
    {
        width: 100%;
        border: none;
    }
    #left_menu
    {
        float: left;
        width: 18%;
        display: block;
    }
    #content
    {
        float: left;
        width: 80.50%;
    }
    #UpdateProgress1
    {
        background-color: #CF4342;
        color: White;
        top: 0px;
        right: 0px;
        position: fixed;
    }
    #UpdateProgress1 img
    {
        vertical-align: middle;
        margin: 2px;
    }
    .fltlft
    {
        float: left;
        padding-left: 3px;
        width: 20%;
    }
</style>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true" />
  <%--      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
                <table width="100%">
                    <tr id="trMFOrderMenu" runat="server">
                        <td>
                            <div class="OnlineProductMenu" style="vertical-align: top">
                                <table width="100%" style="vertical-align:top">
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="lnkMFOrderMenuTransact" runat="server" Text="TRANSACT" CssClass="LinkButtons"
                                                Style="text-decoration: none" OnClick="lnkMFOrderMenuTransact_Click"></asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkMFOrderMenuBooks" runat="server" Text="BOOKS" CssClass="LinkButtons"
                                                Style="text-decoration: none" OnClick="lnkMFOrderMenuBooks_Click"></asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkMFOrderMenuHoldings" runat="server" Text="HOLDINGS" CssClass="LinkButtons"
                                                Style="text-decoration: none" OnClick="lnkMFOrderMenuHoldings_Click"></asp:LinkButton>
                                        </td>
                                        <td align="right" style="vertical-align:top">
                                            <asp:Label ID="lblProductType" runat="server" Text="Product Type:"></asp:Label>
                                            <img src="Images/MenuSeparator.jpg" alt="">
                                            <asp:Label ID="lblWelcomeUser" runat="server" Text=""></asp:Label>
                                            <img src="Images/MenuSeparator.jpg" alt="">
                                            <asp:LinkButton ID="lnkLogOut" runat="server" Text="logout" CssClass="LinkButtons"
                                                Style="text-decoration: none" OnClick="lnkLogOut_Click"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr id="trTopMenuFrame" runat="server">
                        <td>
                            <iframe name="topframe" id="topframe" onload="javascript:calcIFrameHeight('topframe');"
                                src="OnlineTopHost.aspx" height="35px" width="100%" scrolling="no"></iframe>
                        </td>
                    </tr>
                    <tr id="trMainFrame" runat="server">
                        <td>
                            <iframe name="bottomframe" class="bottomframe" id="bottomframe" onload="javascript:calcIFrameHeight('bottomframe');"
                                src="OnlineBottomHost.aspx" scrolling="no" height="600px"></iframe>
                        </td>
                    </tr>
                </table>
           <%-- </ContentTemplate>
            <Triggers>
                <%-- <asp:PostBackTrigger ControlID="imgexportButton" />--%>
            </Triggers>
        </asp:UpdatePanel>--%>
    </div>
    </form>
</body>
</html>
