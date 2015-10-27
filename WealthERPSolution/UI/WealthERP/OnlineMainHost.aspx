<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnlineMainHost.aspx.cs"
    Inherits="WealthERP.OnlineMainHost" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!-- Latest compiled and minified JavaScript -->

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.2.6.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script language="javascript" type="text/javascript" src="Scripts/JScript.js"></script>

<script>
    function myFunction() {
        window.open('ReferenceFiles/HelpVideo.htm', 'newwindow', 'width=655, height=520'); return false;
    }
    (function($) {
        $.fn.textWidth = function() {
            var calc = '<span style="display:none">' + $(this).text() + '</span>';
            $('body').append(calc);
            var width = $('body').find('span:last').width();
            $('body').find('span:last').remove();
            return width;
        };

        $.fn.marquee = function(args) {
            var that = $(this);
            var textWidth = that.textWidth(),
                offset = that.width(),
                width = offset,
                css = {
                    'text-indent': that.css('text-indent'),
                    'overflow': that.css('overflow'),
                    'white-space': that.css('white-space')
                },
                marqueeCss = {
                    'text-indent': width,
                    'overflow': 'hidden',
                    'white-space': 'nowrap'
                },
                args = $.extend(true, { count: -1, speed: 1e1, leftToRight: false }, args),
                i = 0,
                stop = textWidth * -1,
                dfd = $.Deferred();

            function go() {
                if (!that.length) return dfd.reject();
                if (width == stop) {
                    i++;
                    if (i == args.count) {
                        that.css(css);
                        return dfd.resolve();
                    }
                    if (args.leftToRight) {
                        width = textWidth * -1;
                    } else {
                        width = offset;
                    }
                }
                that.css('text-indent', width + 'px');
                if (args.leftToRight) {
                    width++;
                } else {
                    width--;
                }
                setTimeout(go, args.speed);
            };
            if (args.leftToRight) {
                width = textWidth * -1;
                width++;
                stop = offset;
            } else {
                width--;
            }
            that.css(marqueeCss);
            go();
            return dfd.promise();
        };
    })(jQuery);

    $('.scroller').marquee();
</script>






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
    .div-with-header
    {
        height: 40px;
        background: url('Images/sbi-capsec-header.png') no-repeat left top;
        width: 100%;
        float: left;
    }
    .div-container
    {
        border: 3px;
        border-style: solid;
        color: #3299FF;
        width: 100%;
        float: right;
        margin-top: 0px;
        padding-top: 0px;
        border-top: 1px;
    }
    .div-log-out
    {
        float: right;
        padding-right: 10px;
        background: url('Images/sign-out.png') no-repeat left top;
        padding-top: 50px;
        padding-right: 30px;
    }
    .user-name
    {
        color: #EA8A04;
        font-family: Arial;
        font-size: 12px;
        font-weight: bold;
    }
    .product-header
    {
        background: url('Images/product-header.png');
        background-repeat: repeat;
        height: 20px;
        width: 100%;
        float: left;
    }
    .product-header-text
    {
        float: left;
        color: White;
        font-family: Arial;
        font-size: 15px;
        font-weight: bold;
        padding-left: 15px;
    }
    .prduct-main-menu
    {
        float: left;
        width: 100%;
        height: 30px;
        padding-top: 0px;
        padding-bottom: 0px;
    }
    .top-menu-frame
    {
        float: left;
        width: 100%;
        height: 30px;
    }
    .bottom-content-frame
    {
        float: left;
        width: 100%;
    }
    ul
    {
        /* padding: 5px;*/
        margin: 5px 0;
        list-style: none;
        float: left;
        padding-left: 4px;
    }
    ul li
    {
        float: left;
        display: inline; /*For ignore double margin in IE6*/
        margin: 0 6px;
        background: url('Images/product-menu-fixed.png') no-repeat top right;
    }
    ul li a
    {
        text-decoration: none;
        float: left;
        color: #999;
        cursor: pointer;
        font: 900 14px/22px "Arial" , Helvetica, sans-serif;
        width: 65px;
    }
    ul li a span
    {
        margin: 0 10px 0 -10px;
        padding: 1px 8px 5px 18px;
        position: relative; /*To fix IE6 problem (not displaying)*/
        float: left;
    }
    ul.blue li :hover
    {
        background: url('Images/product-menu-hover.png') no-repeat top right;
        color: #0d5f83;
    }
    ul.blue li:hover
    {
        background: url('Images/product-menu-hover.png') no-repeat top left;
    }
    ul blue li:selected
    {
        background: url('Images/product-menu-hover.png') no-repeat top left !important;
        color: White;
        font-weight: bold;
    }
    .selected
    {
        background: url('Images/product-menu-hover.png') no-repeat top left !important;
        color: White;
        font-weight: bold;
    }
</style>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidUserLogOutPageUrl" Value="" runat="server" />
    <asp:HiddenField ID="hidUserLogInPageUrl" Value="" runat="server" />
    <div>
        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true" />
        <div class="div-with-header">
            <div style="float: right; width: 5%; padding-top: 23px; padding-right: 2%;">
                <asp:LinkButton ID="lnkFAQ" Style="text-decoration: none"    Visible="false"
                    runat="server" Text="FAQ"  CssClass="LinkButtons"></asp:LinkButton>
            </div>
            <div style="float: right; width: 5%; padding-top: 23px; padding-right: 1%;">
                <asp:LinkButton ID="lnkDemo" CssClass="LinkButtons" Style="text-decoration: none" Visible="false"
                    runat="server" Text="Demo" ></asp:LinkButton>
            </div>
            <div style="float: right; width: 10%; padding-top: 8px; padding-right: 28%;">
                <asp:LinkButton ID="lnkLogOut" runat="server" Text="" CssClass="div-log-out" Style="text-decoration: none"
                    OnClick="lnkLogOut_Click"></asp:LinkButton>
            </div>
            <div style="float: right; width: 15%; padding-top: 10px; padding-right: 3%;">
                <asp:Label ID="lblWelcomeUser" runat="server" Text="" CssClass="user-name"></asp:Label>
                <asp:Label ID="lblTest" runat="server" Text="" CssClass="user-name" Visible="false"></asp:Label>
            </div>
        </div>
        <div class="product-header" style="float: left">
            <asp:Label ID="lblOnlieProductType" runat="server" Text="" CssClass="product-header-text"></asp:Label>
        </div>
        <div id="mycrawler" style="margin-left:20px;margin-right:20px;float: left;" class="scroller" >
      
        <asp:Label ID="Label1" runat="server" ></asp:Label>
      
        </div>
        <div class="prduct-main-menu" id="divMFMenu" runat="server" visible="false">
            <ul class="blue">
                <li>
                    <asp:LinkButton  ID="lnkMFOrderMenuHome" runat="server" Text="MARKET" CssClass="LinkButtons"
                        Style="text-decoration: none; text-align: center;" OnClick="lnkMFOrderMenuHome_Click"
                        Width="120px"></asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="lnkMFOrderMenuTransact" runat="server" Text="TRANSACT" CssClass="LinkButtons"
                        Style="text-decoration: none; text-align: center;" OnClick="lnkMFOrderMenuTransact_Click"
                        Width="120px"></asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="lnkMFOrderMenuBooks" runat="server" Text="BOOKS" CssClass="LinkButtons"
                        Style="text-decoration: none; text-align: center;" OnClick="lnkMFOrderMenuBooks_Click"
                        Width="120px"></asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="lnkMFOrderMenuHoldings" runat="server" Text="HOLDINGS" CssClass="LinkButtons"
                        Style="text-decoration: none; text-align: center;" OnClick="lnkMFOrderMenuHoldings_Click"
                        Width="120px"></asp:LinkButton>
                </li>
            </ul>
        </div>
        <div class="prduct-main-menu" id="divNCDMenu" runat="server" visible="false">
            <ul class="blue">
                <li>
                    <asp:LinkButton ID="lnkNCDOrderMenuTransact" runat="server" Text="TRANSACT" CssClass="LinkButtons"
                        Style="text-decoration: none; text-align: center;" OnClick="lnkNCDOrderMenuTransact_Click"
                        Width="120px"></asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="lnkNCDOrderMenuBooks" runat="server" Text="BOOKS" CssClass="LinkButtons"
                        Style="text-decoration: none; text-align: center;" OnClick="lnkNCDOrderMenuBooks_Click"
                        Width="120px"></asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="lnkNCDOrderMenuHoldings" runat="server" Text="HOLDINGS" CssClass="LinkButtons"
                        Style="text-decoration: none; text-align: center;" OnClick="lnkNCDOrderMenuHoldings_Click"
                        Width="120px"></asp:LinkButton>
                </li>
            </ul>
        </div>
        <div class="prduct-main-menu" id="divIPOMenu" runat="server" visible="false">
            <ul class="blue">
                <li>
                    <asp:LinkButton ID="lnkIPOOrderMenuTransact" runat="server" Text="TRANSACT" CssClass="LinkButtons"
                        Style="text-decoration: none; text-align: center;" OnClick="lnkIPOOrderMenuTransact_Click"
                        Width="120px"></asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="lnkIPOOrderMenuBooks" runat="server" Text="BOOKS" CssClass="LinkButtons"
                        Style="text-decoration: none; text-align: center;" OnClick="lnkIPOOrderMenuBooks_Click"
                        Width="120px"></asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="lnkIPOOrderMenuHoldings" runat="server" Text="HOLDINGS" CssClass="LinkButtons"
                        Style="text-decoration: none; text-align: center;" OnClick="lnkIPOOrderMenuHoldings_Click"
                        Width="120px"></asp:LinkButton>
                </li>
            </ul>
        </div> 
        <div class="top-menu-frame" style=" Position:Relative">
            <iframe name="topframe" id="topframe" onload="javascript:calcIFrameHeight('topframe');"
                src="OnlineTopHost.aspx" height="100px" width="100%" scrolling="no"></iframe>
        </div>
        <div class="bottom-content-frame" style="margin-top:10px;z-index:3" >
            <iframe name="bottomframe" class="bottomframe" id="bottomframe" onload="javascript:calcIFrameHeight('bottomframe');"
                src="OnlineBottomHost.aspx" scrolling="no" height="2000px"></iframe>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
