<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnlineMainHost.aspx.cs"
    Inherits="WealthERP.OnlineMainHost" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!-- Latest compiled and minified JavaScript -->

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script src="../Scripts/bootstrap.js" type="text/javascript"></script>

<script language="javascript" type="text/javascript" src="Scripts/JScript.js"></script>

<script>

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
        min-width: 980px;
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
        min-width: 980px;
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
        min-width: 980px;
        height: 30px;
        padding-top: 0px;
        padding-bottom: 0px;
    }
    #mycrawler
    {
        float: left;
        width: 100%;
        height: 20px;
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
    .selected
    {
        background: url('Images/product-menu-hover.png') no-repeat top left !important;
        color: White;
        font-weight: bold;
    }
</style>
<%--Drop Down Menu--%>
<style>
    ul, li
    {
        font-size: 14px;
        font-family: Arial, Helvetica, sans-serif;
        line-height: 21px;
        text-align: left;
    }
    #menuMF, #menuNCD, #menuIPO
    {
        list-style: none;
        width: 940px;
        margin: 0px auto 5px auto;
        height: 30px;
        padding: 0px 10px 0px 10px; /* Rounded Corners */
        -moz-border-radius: 10px;
        -webkit-border-radius: 10px;
        border-radius: 10px; /* Background color and gradients */
        background: #81CCE9;
        background: -moz-linear-gradient(top, #81CCE9, #81CCE9);
        background: -webkit-gradient(linear, 0% 0%, 0% 100%, from(#81CCE9), to(#81CCE9)); /* Borders */
        -moz-box-shadow: inset 0px 0px 1px #edf9ff;
        -webkit-box-shadow: inset 0px 0px 1px #edf9ff;
        box-shadow: inset 0px 0px 1px #edf9ff;
    }
    #menuMF li, #menuNCD li, #menuIPO li
    {
        float: left;
        display: block;
        text-align: center;
        position: relative;
        padding: 4px 10px 4px 10px;
        margin-right: 30px;
        margin-top: 1px;
        border: none;
    }
    #menuMF li:hover, #menuNCD li:hover, #menuIPO li:hover
    {
        border: 1px solid #777777;
        padding: 4px 9px 4px 9px; /* Background color and gradients */
        background: #F4F4F4;
        background: -moz-linear-gradient(top, #F4F4F4, #EEEEEE);
        background: -webkit-gradient(linear, 0% 0%, 0% 100%, from(#F4F4F4), to(#EEEEEE)); /* Rounded corners */
        -moz-border-radius: 5px 5px 0px 0px;
        -webkit-border-radius: 5px 5px 0px 0px;
        border-radius: 5px 5px 0px 0px;
    }
    #menuMF li a, #menuNCD li a, #menuIPO li a
    {
        font-family: Arial, Helvetica, sans-serif;
        font-size: 14px;
        color: #EEEEEE;
        display: block;
        outline: 0;
        text-decoration: none;
        text-shadow: 1px 1px 1px #000;
    }
    #menuMF li:hover a, #menuNCD li:hover a, #menuIPO li:hover a
    {
        color: #161616;
        text-shadow: 1px 1px 1px #ffffff;
        cursor: pointer;
    }
    #menuMF li .drop, #menuNCD li .drop, #menuIPO li .drop
    {
        padding-right: 21px;
        background: url('Images/drop.png') no-repeat right 8px;
    }
    #menuMF li:hover .drop, #menuNCD li:hover .drop, #menuIPO li:hover .drop
    {
        background: url('Images/drop.png') no-repeat right 7px;
    }
    .dropdown_1column, .dropdown_2columns, .dropdown_3columns, .dropdown_4columns, .dropdown_5columns
    {
        margin: 1px auto;
        float: left;
        position: absolute;
        left: -999em; /* Hides the drop down */
        text-align: left;
        padding: 2px 2px 2px 2px;
        border: 1px solid #777777;
        border-top: none; /* Gradient background */
        background: #F4F4F4;
        background: -moz-linear-gradient(top, #EEEEEE, #BBBBBB);
        background: -webkit-gradient(linear, 0% 0%, 0% 100%, from(#EEEEEE), to(#BBBBBB)); /* Rounded Corners */
        -moz-border-radius: 0px 2px 2px 2px;
        -webkit-border-radius: 0px 2px 2px 2px;
        border-radius: 0px 2px 2px 2px;
    }
    .dropdown_1column
    {
        width: 180px;
    }
    .dropdown_2columns
    {
        width: 280px;
    }
    .dropdown_3columns
    {
        width: 420px;
    }
    .dropdown_4columns
    {
        width: 560px;
    }
    .dropdown_5columns
    {
        width: 700px;
    }
    #menuMF li:hover .dropdown_1column, #menuNCD li:hover .dropdown_1column, #menuIPO li:hover .dropdown_1column
    {
        left: -1px;
        top: auto;
    }
    .col_1, .col_2, .col_3, .col_4, .col_5
    {
        display: inline;
        float: left;
        position: relative;
        margin-left: 5px;
        margin-right: 5px;
    }
    .col_1
    {
        width: 170px;
    }
    .col_2
    {
        width: 270px;
    }
    .col_3
    {
        width: 410px;
    }
    .col_4
    {
        width: 550px;
    }
    .col_5
    {
        width: 690px;
    }
    #menuMF .menu_right, #menuNCD .menu_right, #menuIPO .menu_right
    {
        float: right;
        margin-right: 0px;
    }
    #menuMF li .align_right, #menuNCD li .align_right, #menuIPO li .align_right
    {
        /* Rounded Corners */
        -moz-border-radius: 5px 0px 5px 5px;
        -webkit-border-radius: 5px 0px 5px 5px;
        border-radius: 5px 0px 5px 5px;
    }
    #menuMF li:hover .align_right, #menuNCD li:hover .align_right, #menuIPO li:hover .align_right
    {
        left: auto;
        right: -1px;
        top: auto;
    }
    #menuMF p, #menuMF h2, #menuMF h3, #menuMF ul li, #menuNCD p, #menuNCD h2, #menuNCD h3, #menuNCD ul li, #menuIPO p, #menuIPO h2, #menuIPO h3, #menuIPO ul li
    {
        font-family: Arial, Helvetica, sans-serif;
        line-height: 21px;
        font-size: 12px;
        text-align: left;
        text-shadow: 1px 1px 1px #FFFFFF;
    }
    #menuMF h2, #menuNCD h2, #menuIPO h2
    {
        font-size: 21px;
        font-weight: 400;
        letter-spacing: -1px;
        margin: 7px 0 14px 0;
        padding-bottom: 14px;
        border-bottom: 1px solid #666666;
    }
    #menuMF h3, #menuNCD h3, #menuIPO h3
    {
        font-size: 14px;
        margin: 7px 0 14px 0;
        padding-bottom: 7px;
        border-bottom: 1px solid #888888;
    }
    #menuMF p, #menuNCD p, #menuIPO p
    {
        line-height: 18px;
        margin: 0 0 10px 0;
    }
    #menuMF li:hover div a, #menuNCD li:hover div a, #menuIPO li:hover div a
    {
        font-size: 12px;
        color: #015b86;
    }
    #menuMF li:hover div a:hover, #menuNCD li:hover div a:hover, #menuIPO li:hover div a:hover
    {
        color: #029feb;
    }
    .strong
    {
        font-weight: bold;
    }
    .italic
    {
        font-style: italic;
    }
    .imgshadow
    {
        /* Better style on light background */
        background: #FFFFFF;
        padding: 4px;
        border: 1px solid #777777;
        margin-top: 5px;
        -moz-box-shadow: 0px 0px 5px #666666;
        -webkit-box-shadow: 0px 0px 5px #666666;
        box-shadow: 0px 0px 5px #666666;
    }
    .img_left
    {
        /* Image sticks to the left */
        width: auto;
        float: left;
        margin: 5px 15px 5px 5px;
    }
    #menuMF li .black_box, #menuNCD li .black_box, #menuIPO li .black_box
    {
        background-color: #333333;
        color: #eeeeee;
        text-shadow: 1px 1px 1px #000;
        padding: 4px 6px 4px 6px; /* Rounded Corners */
        -moz-border-radius: 5px;
        -webkit-border-radius: 5px;
        border-radius: 5px; /* Shadow */
        -webkit-box-shadow: inset 0 0 3px #000000;
        -moz-box-shadow: inset 0 0 3px #000000;
        box-shadow: inset 0 0 3px #000000;
    }
    #menuMF li ul, #menuNCD li ul, #menuIPO li ul
    {
        list-style: none;
        padding: 0;
        margin: 2px 0px 2px 7px;
    }
    #menuMF li ul li, #menuNCD li ul li, #menuIPO li ul li
    {
        font-size: 12px;
        line-height: 24px;
        position: relative;
        text-shadow: 1px 1px 1px #ffffff;
        padding: 0;
        margin: 0;
        float: none;
        text-align: left;
        width: 130px;
    }
    #menuMF li ul li:hover, #menuNCD li ul li:hover, #menuIPO li ul li:hover
    {
        background: none;
        border: none;
        padding: 0;
        margin: 0;
    }
    #menuMF li .greybox li, #menuNCD li .greybox li, #menuIPO li .greybox li
    {
        background: #F4F4F4;
        border: 1px solid #bbbbbb;
        margin: 0px 0px 4px 0px;
        padding: 4px 6px 4px 6px;
        width: 150px; /* Rounded Corners */
        -moz-border-radius: 5px;
        -webkit-border-radius: 5px;
        -khtml-border-radius: 5px;
        border-radius: 5px;
    }
    #menuMF li .greybox li:hover, #menuNCD li .greybox li:hover, #menuIPO li .greybox li:hover
    {
        background: #ffffff;
        border: 1px solid #aaaaaa;
        padding: 4px 6px 4px 6px;
        margin: 0px 0px 4px 0px;
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
                <asp:LinkButton ID="lnkFAQ" Style="text-decoration: none" Visible="false" runat="server"
                    Text="FAQ" CssClass="LinkButtons"></asp:LinkButton>
            </div>
            <div style="float: right; width: 5%; padding-top: 23px; padding-right: 1%;">
                <asp:LinkButton ID="lnkDemo" CssClass="LinkButtons" Style="text-decoration: none"
                    Visible="false" runat="server" Text="Demo"></asp:LinkButton>
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
        <div class="product-header" style="float: left; clear: both;">
            <asp:Label ID="lblOnlieProductType" runat="server" Text="" CssClass="product-header-text"></asp:Label>
        </div>
        <div id="scroller" runat="server" style="margin: 5px auto 3px auto; padding: 0px 20px 0px 10px;
            border-radius: 2px; clear: both; height: 20px; color: #fff; background: #000;
            width: 940px;">
            <div id="mycrawler" class="scroller">
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </div>
        </div>
        <div id="mainmenuMF" runat="server" style="width: 100%; clear: both;">
            <ul id="menuMF">
                <li><a onclick="" class="drop">MARKET</a>
                    <div class="dropdown_1column">
                        <div class="col_1">
                            <ul class="greybox">
                                <li><a onclick="LoadBottomPanelControl('MFSchemeRelateInformation','login');">HOME</a></li>
                                <li><a onclick="LoadBottomPanelControl('MFSchemeDetails','login');">SCHEME RESEARCH</a></li>
                                <li><a onclick="LoadBottomPanelControl('OnlineMFSchemeCompare','login');">SCHEME COMPARE</a></li>
                                <li><a onclick="LoadBottomPanelControl('ProductOnlineFundNews','login');">NEWS</a></li>
                            </ul>
                        </div>
                    </div>
                </li>
                <li><a onclick="" class="drop">BOOKS</a>
                    <div class="dropdown_1column">
                        <div class="col_1">
                            <ul class="greybox">
                                <li><a onclick="LoadBottomPanelControl('OnlineCustomerOrderandTransactionBook','login');">
                                    ORDER BOOK</a> </li>
                                <li><a onclick="LoadBottomPanelControl('CustomerTransactionBookList','login');">TRANSACTION
                                    BOOK</a></li>
                                <li><a onclick="LoadBottomPanelControl('SIPBookSummmaryList','?systematicType=SIP');">
                                    SIP BOOK</a></li>
                            </ul>
                        </div>
                    </div>
                </li>
                <li><a>HOLDINGS</a>
                    <div class="dropdown_1column">
                        <div class="col_1">
                            <ul class="greybox">
                                <li><a onclick="LoadBottomPanelControl('CustomerMFUnitHoldingList','login');">MF HOLDINGS</a></li>
                            </ul>
                        </div>
                    </div>
                </li>
                <li class="menu_right"><a onclick="LoadBottomPanelControl('FAQandDemo','login');">FAQ</a>
                </li>
                <li class="menu_right"><a onclick="LoadBottomPanelControl('FAQandDemo','login');">Demo</a>
                </li>
            </ul>
        </div>
        <div id="mainmenuNCD" runat="server" style="width: 100%; clear: both;">
            <ul id="menuNCD">
                <li><a onclick="" class="drop">TRANSACT</a>
                    <div class="dropdown_1column">
                        <div class="col_1">
                            <ul class="greybox">
                                <li><a onclick="LoadBottomPanelControl('NCDIssueList','?BondType=" + "FISDSD" + "');">
                                    NCD ISSUE LIST</a></li>
                                <li><a onclick="LoadBottomPanelControl('NCDIssueList','?BondType=" + "FISSGB" + "');">
                                    SGB ISSUE LIST</a></li>
                            </ul>
                        </div>
                    </div>
                </li>
                <li><a onclick="" class="drop">BOOKS</a>
                    <div class="dropdown_1column">
                        <div class="col_1">
                            <ul class="greybox">
                                <li><a onclick="LoadBottomPanelControl('NCDIssueBooks','?BondType=" + "FISDSD" + "');">
                                    NCD BOOK</a> </li>
                                <li><a onclick="LoadBottomPanelControl('NCDIssueBooks','?BondType=" + "FISSGB" + "');">
                                    SGB BOOK</a></li>
                            </ul>
                        </div>
                    </div>
                </li>
                <li><a>HOLDINGS</a>
                    <div class="dropdown_1column">
                        <div class="col_1">
                            <ul class="greybox">
                                <li><a onclick="LoadBottomPanelControl('NCDIssueHoldings','?BondType=" + "FISDSD" + "');">
                                    NCD HOLDINGS</a></li>
                                <li><a onclick="LoadBottomPanelControl('NCDIssueHoldings','?BondType=" + "FISSGB" + "');">
                                    SGB HOLDINGS</a></li>
                            </ul>
                        </div>
                    </div>
                </li>
                <li class="menu_right"><a onclick="LoadBottomPanelControl('FAQandDemo','login');">FAQ</a>
                </li>
                <li class="menu_right"><a onclick="LoadBottomPanelControl('FAQandDemo','login');">Demo</a>
                </li>
            </ul>
        </div>
        <div id="mainmenuIPO" runat="server" style="width: 100%; clear: both;">
            <ul id="menuIPO">
                <li><a onclick="" class="drop">TRANSACT</a>
                    <div class="dropdown_1column">
                        <div class="col_1">
                            <ul class="greybox">
                                <li><a onclick="LoadBottomPanelControl('IPOIssueList','login');">IPO/FPO ISSUE LIST</a></li>
                            </ul>
                        </div>
                    </div>
                </li>
                <li><a onclick="" class="drop">BOOKS</a>
                    <div class="dropdown_1column">
                        <div class="col_1">
                            <ul class="greybox">
                                <li><a onclick="LoadBottomPanelControl('CustomerIPOOrderBook','login');">IPO/FPO BOOK</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </li>
                <li><a>HOLDINGS</a>
                    <div class="dropdown_1column">
                        <div class="col_1">
                            <ul class="greybox">
                                <li><a onclick="LoadBottomPanelControl('CustomerIPOHolding','login');">IPO/FPO HOLDINGS</a></li>
                            </ul>
                        </div>
                    </div>
                </li>
                <li class="menu_right"><a onclick="LoadBottomPanelControl('FAQandDemo','login');">FAQ</a>
                </li>
                <li class="menu_right"><a onclick="LoadBottomPanelControl('FAQandDemo','login');">Demo</a>
                </li>
            </ul>
        </div>
        <div style="margin-top: 10px; z-index: 3;">
            <iframe name="bottomframe" class="bottomframe" width="100%" id="bottomframe" onload="javascript:calcIFrameHeight('bottomframe');"
                src="OnlineBottomHost.aspx" scrolling="no"></iframe>
        </div>
        <div style="clear:both;">
            <style>
                .tabs
                {
                     /* This part sucks */
                    clear: both;
                   list-style: none;
        
        width:auto;
        height: 30px;
        padding: 0px 2px 0px 2px; /* Rounded Corners */
        -moz-border-radius: 3px;
        -webkit-border-radius: 3px;
        border-radius: 3px; /* Background color and gradients 5DB2FF    #0295CD */
        background: #d9d9d9;
        background: -moz-linear-gradient(top, #d9d9d9, #d9d9d9);
        background: -webkit-gradient(linear, 0% 0%, 0% 100%, from(#d9d9d9), to(#d9d9d9)); /* Borders */
        -moz-box-shadow: inset 0px 0px 1px #edf9ff;
        -webkit-box-shadow: inset 0px 0px 1px #edf9ff;
        box-shadow: inset 0px 0px 1px #edf9ff;
                }
                .tab
                {
                     float: left;
                      padding: 4px 4px 4px 4px;
        margin-right: 5px;
        margin-top: 1px;
        border: none;
                   
                    }
                .tab label
                {
                   
                    float: left;
                    display: block;
                    text-align: center;
                    position: relative;
                   color:White;
                     padding: 4px 9px 4px 9px; /* Background color and gradients */
                }
                .tab label:hover
                {
                      color:Black;
                    border: 1px solid #777777;
        padding: 4px 9px 4px 9px; /* Background color and gradients */
        background: #F4F4F4;
        background: -moz-linear-gradient(top, #F4F4F4, #EEEEEE);
        background: -webkit-gradient(linear, 0% 0%, 0% 100%, from(#F4F4F4), to(#EEEEEE)); /* Rounded corners */
        -moz-border-radius: 5px 5px 0px 0px;
        -webkit-border-radius: 5px 5px 0px 0px;
        border-radius: 5px 5px 0px 0px;
                    cursor:pointer;
                }
                .tab [type=radio]
                {
                    display: none;
                }
               
                 [type=radio]:checked ~label
                {
                    color:Black;
                    background-color:#fff;
                    border-bottom: 1px solid white;
                    z-index: 2;
                }
            </style>
            <div id="TransactTab" class="tabs">
                <div class="tab">
                    <input type="radio" id="tab-1" name="tab-group-1" onclick="LoadTransactPanel('MFOrderPurchaseTransType','login');"
                        checked>
                    <label for="tab-1">
                        New Purchase</label>
                </div>
                <div class="tab">
                    <input type="radio" id="tab-2" onclick="LoadTransactPanel('MFOrderAdditionalPurchase','login');"
                        name="tab-group-1">
                    <label for="tab-2">
                        Additional Purchase</label>
                </div>
                <div class="tab">
                    <input type="radio" id="tab-3" onclick="LoadTransactPanel('MFOrderSIPTransType','login');"
                        name="tab-group-1">
                    <label for="tab-3">
                        SIP</label>
                </div>
                <div class="tab">
                    <input type="radio" id="tab-4" onclick="LoadTransactPanel('MFOrderNFOTransType','login');"
                        name="tab-group-1">
                    <label for="tab-4">
                        NFO</label>
                </div>
            </div>
        </div>
        <div class="top-menu-frame" style=" Position:Relative">
            <iframe name="topframe" id="topframe" onload="javascript:calcIFrameHeight('topframe');"
                src="OnlineTopHost.aspx"  width="100%" scrolling="no"></iframe>
        </div>
    </div>
    </form>
</body>
</html>
