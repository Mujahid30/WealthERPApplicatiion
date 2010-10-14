<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WealthERP._Default" %>

<%@ Register Src="General/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="General/LeftPanel_Links.ascx" TagName="LeftPanel_Links" TagPrefix="uc2" %>
<%@ Register Src="General/GeneralHome.ascx" TagName="GeneralHome" TagPrefix="uc3" %>
<%@ Register Src="General/UserLogin.ascx" TagName="GeneralHome" TagPrefix="uc4" %>
<%@ Register Src="Advisor/AdvisorLeftPane.ascx" TagName="AdvisorLeftPane" TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Wealth ERP</title>
    <%-- <link href="CSS/ControlsStyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="CSS/StyleSheet.css" rel="stylesheet" type="text/css" />--%>
<link rel="Shortcut Icon" href="favicon.ico" type="image/x-icon" />
    <script language="javascript" type="text/javascript" src="Scripts/JScript.js"></script>

<script language="javascript" type="text/javascript">
    var timerEvent = null;
    
    function calcHeight(ifrm_id) {
        try {
            setTimeout("calc('" + ifrm_id + "')", 200);
        }
        catch (e) { }
    }
    function calc(iframe_id) {
        try {
            var leftframe_height = document.getElementById('leftframe').contentWindow.document.body.scrollHeight;
            var mainframe_height = document.getElementById('mainframe').contentWindow.document.body.scrollHeight;
            var the_height = (leftframe_height > mainframe_height) ? leftframe_height : mainframe_height;
            if (the_height > 600) {
                var newHeight = the_height + 20;
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

            if (timerEvent != null) window.clearInterval(timerEvent);
            timerEvent = window.setTimeout("calc('" + iframe_id + "')", 1000);
        }
        catch (e) { }
    }
</script>

    <script type="text/javascript" language="javascript">
        function hb1(clicked) //Show or hide the left_menu
        {
            if (clicked == 'left') {
                // Clicked on the left bar
                if (document.getElementById('left_menu').style.display == 'none') {
                    // If left menu is invisble
                    document.getElementById('left_menu').style.display = 'block';

                    document.getElementById('splitter_bar_left').style.backgroundImage = 'url(Images/splitter_bar_left_pointer.jpg)';
                    document.getElementById('splitter_bar_left').style.cssFloat = 'left';
                    document.getElementById('splitter_bar_left').style.styleFloat = 'left';
                    document.getElementById('content').style.cssFloat = 'left';
                    document.getElementById('content').style.styleFloat = 'left';
                    document.getElementById('content').style.width = "76.50%";
                    document.getElementById('content').style.position = 'relative';
                }
                else if (document.getElementById('left_menu').style.display == 'block') {
                    // If left menu is Visible
                    document.getElementById('left_menu').style.display = 'none';

                    document.getElementById('splitter_bar_left').style.backgroundImage = 'url(Images/splitter_bar_right_pointer.jpg)';
                    document.getElementById('splitter_bar_left').style.cssFloat = 'left';
                    document.getElementById('splitter_bar_left').style.styleFloat = 'left';
                    document.getElementById('content').style.cssFloat = 'left';
                    document.getElementById('content').style.styleFloat = 'left';
                    document.getElementById('content').style.width = "98.75%";
                    document.getElementById('content').style.position = 'relative';
                    
                }
            }
        }
    </script>

    <style type="text/css">
        #leftframe
        {
            width: 100%;
        }
        #mainframe
        {
            width: 100%;
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
                background-color:#CF4342;
                color:White;
                top: 0px;
                right: 0px;
                position:fixed;
        }
 
        #UpdateProgress1 img 
        {
               vertical-align:middle;
                 margin:2px;
        }
    </style>
</head>
<body>
    <form id="MainForm" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true" />
        <table class="TDBackground" width="100%" style="height: auto; margin: 0px; padding: 0px;">
            <tr>
                <td colspan="3" style="border: inherit; height: 50px" align="left" valign="top">
                    <div id="GeneralHeader" style="height: auto" visible="true" runat="server">
                        <table width="100%">
                            <tr class='header'>
                                <%-- style="background-image: url(Images/Header2.jpg); background-repeat: repeat;
                                height: 90px;" --%>
                                <%--background-color: #D1E1F7"--%>
                                <td colspan="3" valign="top">
                                    <div style="width: 100%; height: 50px;">
                                        <div style="float:left; z-index: 2000;">
                                            <img alt="Advisor Logo" id="AdvisorLogo" runat="server" height="50" />
                                        </div>
                                        <div style="float:right; z-index: 2000;">
                                            <img id="BranchLogo" runat="server"/>
                                        </div>
                                    </div>
                                    <div style="clear:both; z-index: 2500; text-align:right;">
                                        &nbsp; <a id="LinkButtonUserSettings" onclick="javascript:loadcontrol('UserSettings','none'); return false;"
                                            class="LinkButtons" href="#">Settings</a>&nbsp;
                                        <asp:LinkButton ID="LinkButtonSignIn" runat="server" Text="Sign In" OnClientClick="javascript:loadcontrol('Userlogin','none'); return false;"
                                            CssClass="LinkButtons"></asp:LinkButton>
                                        &nbsp;
                                        <asp:LinkButton ID="LinkButtonContactUs" runat="server" OnClientClick="javascript:loadcontrol('GeneralHome','none'); return false;"
                                            CssClass="LinkButtons">Contact Us</asp:LinkButton>
                                        &nbsp; <a href="Demo/Demo.html" class="LinkButtons" target="_blank">Demo</a> &nbsp;
                                        <a id="lnkHelp" name="lnkHelp" href="help/Index.htm" class="LinkButtons" target="_blank">
                                            Help</a>
                                        <%--<asp:LinkButton ID="LinkButtonHelp" runat="server" CssClass="LinkButtons" PostBackUrl="Help.htm">Help</asp:LinkButton>--%>
                                        &nbsp;
                                        <asp:LinkButton ID="lblSignOut" runat="server" Text="" OnClientClick="javascript:logoutloadcontrol('UserLogout','~/Images/logo.jpg','~/Images/logo.jpg'); return false"
                                            CssClass="LinkButtons"></asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                            <%-- <tr>
                                <td colspan="3" height="10px">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 200px">
                                            </td>
                                            <td align="center">
                                                Test Space
                                            </td>
                                            <td align="center" style="width: 200px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>--%>
                            <tr style="height: 35px; border: border 2 #0000FF">
                                <td class="PCGRedBcknd" style="width: 17.25%;">
                                    <asp:Label ID="lblUserName" runat="server" CssClass="HeaderDateText"></asp:Label>
                                </td>
                                <td colspan="2" class="PCGRedBcknd" valign="middle">
                                   <%-- <div id="GeneralMenu" style="height: auto; width: 78%; float: left;" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Menu ID="GeneralHeaderMenu" runat="server" Orientation="Horizontal" BorderStyle="Solid"
                                                        BorderWidth="2px" CssClass="MenuEX">
                                                        <LevelMenuItemStyles>
                                                            <asp:MenuItemStyle CssClass="level1" />
                                                            <asp:MenuItemStyle CssClass="level2" />
                                                            <asp:MenuItemStyle CssClass="level3" />
                                                        </LevelMenuItemStyles>
                                                        <StaticHoverStyle CssClass="hoverstyle" />
                                                        <LevelSubMenuStyles>
                                                            <asp:SubMenuStyle CssClass="sublevel1" />
                                                        </LevelSubMenuStyles>
                                                        <Items>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('Userlogin','none');" Text="Home"
                                                                Value="Home" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('UnderConstruction','none');"
                                                                Text="Knowledge Center" Value="KnowldgeCenterHome" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                                <asp:MenuItem NavigateUrl="javascript:loadfrommenu('UnderConstruction','none');"
                                                                    Text="Market Monitor" Value="MarketMonitor">
                                                                    <asp:MenuItem NavigateUrl="javascript:loadfrommenu('UnderConstruction','none');"
                                                                        Text="Debt" Value="Debt"></asp:MenuItem>
                                                                    <asp:MenuItem NavigateUrl="javascript:loadfrommenu('UnderConstruction','none');"
                                                                        Text="Equity" Value="Equity"></asp:MenuItem>
                                                                </asp:MenuItem>
                                                                <asp:MenuItem NavigateUrl="javascript:loadfrommenu('UnderConstruction','none');"
                                                                    Text="Advisor Speak" Value="AdvisorSpeak">
                                                                    <asp:MenuItem NavigateUrl="javascript:loadfrommenu('UnderConstruction','none');"
                                                                        Text="Debt" Value="Debt"></asp:MenuItem>
                                                                    <asp:MenuItem NavigateUrl="javascript:loadfrommenu('UnderConstruction','none');"
                                                                        Text="Equity" Value="Equity"></asp:MenuItem>
                                                                </asp:MenuItem>
                                                            </asp:MenuItem>
                                                            <%--<asp:MenuItem NavigateUrl="javascript:loadfrommenu('UnderConstruction','none');"
                                                                Text="Financial Planning" Value="FinancialPlanning"></asp:MenuItem>
                                                        </Items>
                                                    </asp:Menu>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>--%>
                                    <div id="AdvisorHeader" style="height: auto; width: 78%; float: left; display: none;"
                                        runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Menu ID="AdvisorMenu" runat="server" Orientation="Horizontal" BorderStyle="Solid"
                                                        BorderWidth="2px" CssClass="MenuEX">
                                                        <%-- OnMenuItemClick="AdvisorMenu_MenuItemClick"--%>
                                                        <LevelMenuItemStyles>
                                                            <asp:MenuItemStyle CssClass="level1" />
                                                            <asp:MenuItemStyle CssClass="level2" />
                                                            <asp:MenuItemStyle CssClass="level3" />
                                                        </LevelMenuItemStyles>
                                                        <StaticHoverStyle CssClass="hoverstyle" />
                                                        <LevelSubMenuStyles>
                                                            <asp:SubMenuStyle CssClass="sublevel1" />
                                                        </LevelSubMenuStyles>
                                                        <Items>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('IFAAdminMainDashboard','login');"
                                                                Text="Home" Value="Home" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('AdvisorProfile','login');" Text="Profile"
                                                                Value="Profile" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('ViewLOB','login');" Text="LOB"
                                                                Value="LOB" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('ViewBranches','login');" Text="Branch"
                                                                Value="Branch" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('ViewRM','login');" Text="RM"
                                                                Value="RM" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('AdminPriceList','login');" Text="Price List"
                                                                Value="Price List" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('FinancialPlanning','login');"
                                                                Text="Financial Planning" Value="FinancialPlanning"></asp:MenuItem>
                                                        </Items>
                                                    </asp:Menu>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="CustomerIndividualHeader" style="height: auto; width: 78%; float: left;
                                        display: none" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Menu ID="CustomerIndividualMenu" runat="server" Orientation="Horizontal" BorderStyle="Solid"
                                                        BorderWidth="2px" CssClass="MenuEX">
                                                        <%--OnMenuItemClick="CustomerMenu_MenuItemClick"--%>
                                                        <LevelMenuItemStyles>
                                                            <asp:MenuItemStyle CssClass="level1" />
                                                            <asp:MenuItemStyle CssClass="level2" />
                                                            <asp:MenuItemStyle CssClass="level3" />
                                                        </LevelMenuItemStyles>
                                                        <StaticHoverStyle CssClass="hoverstyle" />
                                                        <LevelSubMenuStyles>
                                                            <asp:SubMenuStyle CssClass="sublevel1" />
                                                        </LevelSubMenuStyles>
                                                        <Items>
                                                            <asp:MenuItem NavigateUrl="javascript:loadcontrolCustomer('AdvisorRMCustIndiDashboard','login');"
                                                                Text="Home" Value="Home" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadcontrolCustomer('ViewCustomerIndividualProfile','login');"
                                                                Text="Profile" Value="Profile" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                            </asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadcontrolCustomer('PortfolioDashboard','login');"
                                                                Text="Portfolio" Value="Portfolio" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                            </asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadcontrolCustomer('UnderConstruction','none');"
                                                                Text="Tools" Value="Tools" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadcontrolCustomer('RMAlertNotifications','none');"
                                                                Text="Alerts" Value="Alerts" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                            </asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadcontrolCustomer('UnderConstruction','none');"
                                                                Text="Reports" Value="Reports"></asp:MenuItem>
                                                        </Items>
                                                    </asp:Menu>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="CustomerNonIndividualHeader" style="height: auto; width: 78%; float: left;
                                        display: none" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Menu ID="CustomerNonIndividualMenu" runat="server" Orientation="Horizontal"
                                                        BorderStyle="Solid" BorderWidth="2px" CssClass="MenuEX">
                                                        <%--OnMenuItemClick="CustomerMenu_MenuItemClick"--%>
                                                        <LevelMenuItemStyles>
                                                            <asp:MenuItemStyle CssClass="level1" />
                                                            <asp:MenuItemStyle CssClass="level2" />
                                                            <asp:MenuItemStyle CssClass="level3" />
                                                        </LevelMenuItemStyles>
                                                        <StaticHoverStyle CssClass="hoverstyle" />
                                                        <LevelSubMenuStyles>
                                                            <asp:SubMenuStyle CssClass="sublevel1" />
                                                        </LevelSubMenuStyles>
                                                        <Items>
                                                            <asp:MenuItem NavigateUrl="javascript:loadcontrolCustomer('AdvisorRMCustIndiDashboard','login');"
                                                                Text="Home" Value="Home" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('ViewNonIndividualProfile','login');"
                                                                Text="Profile" Value="Profile" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                            </asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('PortfolioDashboard','login');"
                                                                Text="Portfolio" Value="Portfolio" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                            </asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('UnderConstruction','none');"
                                                                Text="Tools" Value="Tools" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('UnderConstruction','none');"
                                                                Text="Alerts" Value="Alerts" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                            </asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('UnderConstruction','none');"
                                                                Text="Reports" Value="Reports"></asp:MenuItem>
                                                        </Items>
                                                    </asp:Menu>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="RMHeader" style="height: auto; width: 78%; float: left; display: none" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Menu ID="RMMenu" runat="server" Orientation="Horizontal" BorderStyle="Solid"
                                                        BorderWidth="2px" CssClass="MenuEX">
                                                        <%--OnMenuItemClick="RMMenu_MenuItemClick"--%>
                                                        <LevelMenuItemStyles>
                                                            <asp:MenuItemStyle CssClass="level1" />
                                                            <asp:MenuItemStyle CssClass="level2" />
                                                            <asp:MenuItemStyle CssClass="level3" />
                                                        </LevelMenuItemStyles>
                                                        <StaticHoverStyle CssClass="hoverstyle" />
                                                        <LevelSubMenuStyles>
                                                            <asp:SubMenuStyle CssClass="sublevel1" />
                                                        </LevelSubMenuStyles>
                                                        <Items>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('RMDashBoard','login');" Text="Home"
                                                                Value="Home" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('ViewRMDetails','login');" Text="My Profile"
                                                                Value="Profile" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('RMCustomer','login');" Text="Customers"
                                                                Value="Clients" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                                <asp:MenuItem NavigateUrl="javascript:loadfrommenu('FinancialPlanning','login');"
                                                                Text="Financial Planning" Value="FinancialPlanning"></asp:MenuItem>
                                                            <%--<asp:MenuItem NavigateUrl="javascript:loadfrommenu('RMAlertDashBoard','?Clear=true');"
                                                                Text="Alerts" Value="Alerts"></asp:MenuItem>--%>
                                                        </Items>
                                                    </asp:Menu>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="RMCLientHeaderIndividual" style="height: auto; width: 78%; float: left;
                                        display: none" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Menu ID="RMIndividualCLientMenu" runat="server" Orientation="Horizontal" BorderStyle="Solid"
                                                        BorderWidth="2px" CssClass="MenuEX">
                                                        <%--OnMenuItemClick="RMCLientMenu_MenuItemClick"--%>
                                                        <LevelMenuItemStyles>
                                                            <asp:MenuItemStyle CssClass="level1" />
                                                            <asp:MenuItemStyle CssClass="level2" />
                                                            <asp:MenuItemStyle CssClass="level3" />
                                                        </LevelMenuItemStyles>
                                                        <StaticHoverStyle CssClass="hoverstyle" />
                                                        <LevelSubMenuStyles>
                                                            <asp:SubMenuStyle CssClass="sublevel1" />
                                                        </LevelSubMenuStyles>
                                                        <Items>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('RMDashBoard','login');" Text="Home"
                                                                Value="Home" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('AdvisorRMCustIndiDashboard','login');"
                                                                Text="Customer Dashboard" Value="CustomerDashboard" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                            </asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('RMCustomerIndividualDashboard','login');"
                                                                Text="Profile" Value="Profile" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                            </asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('PortfolioDashboard','login');"
                                                                Text="Portfolio" Value="Portfolio" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                            </asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('UnderConstruction','none');"
                                                                Text="Planning" Value="Planning" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                            </asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('UnderConstruction','none');"
                                                                Text="Tools" Value="Tools" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('RMAlertDashBoard','login');"
                                                                Text="Alerts" Value="Alerts" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                            </asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('UnderConstruction','none');"
                                                                Text="Research/Analytics" Value="ResearchAnalytics" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                            </asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('UnderConstruction','none');"
                                                                Text="Reports" Value="Reports"></asp:MenuItem>
                                                        </Items>
                                                    </asp:Menu>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="RMCLientHeaderNonIndividual" style="height: auto; width: 78%; float: left;
                                        display: none" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Menu ID="RMNonIndividualCLientMenu" runat="server" Orientation="Horizontal"
                                                        BorderStyle="Solid" BorderWidth="2px" CssClass="MenuEX">
                                                        <%--OnMenuItemClick="RMCLientMenu_MenuItemClick"--%>
                                                        <LevelMenuItemStyles>
                                                            <asp:MenuItemStyle CssClass="level1" />
                                                            <asp:MenuItemStyle CssClass="level2" />
                                                            <asp:MenuItemStyle CssClass="level3" />
                                                        </LevelMenuItemStyles>
                                                        <StaticHoverStyle CssClass="hoverstyle" />
                                                        <LevelSubMenuStyles>
                                                            <asp:SubMenuStyle CssClass="sublevel1" />
                                                        </LevelSubMenuStyles>
                                                        <Items>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('RMDashBoard','login');" Text="Home"
                                                                Value="Home" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('RMCustomerNonIndividualDashboard','login');"
                                                                Text="Customer Dashboard" Value="CustomerDashboard" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                            </asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('ViewNonIndividualProfile','login');"
                                                                Text="Profile" Value="Profile" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                            </asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('PortfolioDashboard','login');"
                                                                Text="Portfolio" Value="Portfolio" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                            </asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('UnderConstruction','none');"
                                                                Text="Planning" Value="Planning" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                            </asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('UnderConstruction','none');"
                                                                Text="Tools" Value="Tools" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('UnderConstruction','none');"
                                                                Text="Alerts" Value="Alerts" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                            </asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('UnderConstruction','none');"
                                                                Text="Research/Analytics" Value="ResearchAnalytics" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                            </asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('UnderConstruction','none');"
                                                                Text="Reports" Value="Reports"></asp:MenuItem>
                                                        </Items>
                                                    </asp:Menu>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="BMHeader" style="height: auto; width: 78%; float: left; display: none" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Menu ID="BMMenu" runat="server" Orientation="Horizontal" BorderStyle="Solid"
                                                        BorderWidth="2px" CssClass="MenuEX">
                                                        <%--OnMenuItemClick="RMCLientMenu_MenuItemClick"--%>
                                                        <LevelMenuItemStyles>
                                                            <asp:MenuItemStyle CssClass="level1" />
                                                            <asp:MenuItemStyle CssClass="level2" />
                                                            <asp:MenuItemStyle CssClass="level3" />
                                                        </LevelMenuItemStyles>
                                                        <StaticHoverStyle CssClass="hoverstyle" />
                                                        <LevelSubMenuStyles>
                                                            <asp:SubMenuStyle CssClass="sublevel1" />
                                                        </LevelSubMenuStyles>
                                                        <Items>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('BMDashBoard','login');" Text="Home"
                                                                Value="Home" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('ViewBMProfile','login');" Text="Profile"
                                                                Value="Profile" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('ViewBranchDetails','login');"
                                                                Text="Branch Details" Value="Branch Details" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                            </asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('ViewRM','login');" Text="View RMs"
                                                                Value="View RMs" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                        </Items>
                                                    </asp:Menu>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="AdminHeader" style="height: auto; width: 78%; float: left; display: none"
                                        runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Menu ID="AdminMenu" runat="server" Orientation="Horizontal" BorderStyle="Solid"
                                                        BorderWidth="2px" CssClass="MenuEX">
                                                        <%--OnMenuItemClick="RMCLientMenu_MenuItemClick"--%>
                                                        <LevelMenuItemStyles>
                                                            <asp:MenuItemStyle CssClass="level1" />
                                                            <asp:MenuItemStyle CssClass="level2" />
                                                            <asp:MenuItemStyle CssClass="level3" />
                                                        </LevelMenuItemStyles>
                                                        <StaticHoverStyle CssClass="hoverstyle" />
                                                        <LevelSubMenuStyles>
                                                            <asp:SubMenuStyle CssClass="sublevel1" />
                                                        </LevelSubMenuStyles>
                                                        <Items>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('AdminDownload','login');" Text="Download"
                                                                Value="Download" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('WERPAdminDownloadProcessLog','login');"
                                                                Text="Download Process Log" Value="DownloadPrLog" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                            </asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('AdminUpload','login');" Text="Upload"
                                                                Value="Upload" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('AdminMaintenance','login');"
                                                                Text="Product Master Maintenance" Value="PMM" SeparatorImageUrl="~/Images/MenuSeparator.jpg">
                                                            </asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadfrommenu('AdminPriceList','login');" Text="Price List"
                                                                Value="PriceList" SeparatorImageUrl="~/Images/MenuSeparator.jpg"></asp:MenuItem>
                                                            <asp:MenuItem NavigateUrl="javascript:loadcontrol('AlertServicesExecution', 'none');"
                                                                Text="Alert Services" Value="Alerts Services"></asp:MenuItem>
                                                        </Items>
                                                    </asp:Menu>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="SwitchRolesHeader" style="height: auto; width: 78%; float: left; display: none;"
                                        runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Menu ID="SwitchRolesMenu" runat="server" Orientation="Horizontal" BorderStyle="Solid"
                                                        BorderWidth="2px" CssClass="MenuEX">
                                                    </asp:Menu>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="dvDate" style="float: right;">
                                        <table>
                                            <tr style="height: 30px;">
                                                <td>
                                                    <asp:Label ID="lblDate" runat="server" CssClass="HeaderDateText"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <%--<td>--%>
                <td id="left_menu" style="display: block;" valign="top">
                    <iframe name="leftframe" id="leftframe" onload="javascript:calcHeight('leftframe');"
                        src="ControlLeftHost.aspx"></iframe>
                </td>
                <td id="splitter_bar_left" style="display: block" onclick="javascript:hb1('left');">
                    &nbsp;
                    <%--<img alt="collapse" id="imgCollapseLeft" src="Images/splitter_bar_left_pointer.jpg" style="vertical-align:middle;" /> --%>
                </td>
                <td id="content" style="display: block" valign="top">
                    <iframe name="mainframe" id="mainframe" onload="javascript:calcHeight('mainframe');"
                        src="ControlHost.aspx"></iframe>
                </td>
                <%--<td id="splitter_bar_right" style="display: block" onclick="javascript:hb1('right');">
                    &nbsp;&nbsp;
                    <img alt="collapse" id="imgCollapseRight" src="Images/splitter_bar_right_pointer.jpg" style="vertical-align:middle;" />
                </td>--%>
                <%--<td id="right_menu" style="display: block">
                </td>--%>
                <%--</td>--%>
                <%--<td style="width: 17%;" align="left" valign="top">
                    <iframe name="leftframe" id="leftframe" onload="javascript:calcHeight('leftframe');"
                        src="ControlLeftHost.aspx"></iframe>
                </td>
                <td style="width: 100%; min-height: 700px" valign="top">
                    <iframe name="mainframe" id="mainframe" onload="javascript:calcHeight('mainframe');"
                        src="ControlHost.aspx"></iframe>
                </td>
                <td>
                    <asp:Panel ID="RightPanel" runat="server">
                    </asp:Panel>
                </td>--%>
            </tr>
            <tr>
                <td style="border: inherit; height: 60px" class="PCGRedBcknd" colspan="3">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblBestViewed" Text="Best Viewed in Mozilla Firefox Version 3.0 and above"
                                    runat="server" CssClass="PCGWhiteText" Font-Size="X-Small"></asp:Label>
                            </td>
                            <td align="right">
                            <span id="siteseal"><script type="text/javascript" src="https://seal.godaddy.com/getSeal?sealID=OWPyWbNsq7qPWzrss8sCH3weSSj3SjP21EhheOl4L7s2vBTlMzf"></script><br/><a style="font-family: arial; font-size: 9px" href="https://www.godaddy.com" target="_blank">Best Web Hosting</a></span>
                                <asp:Label ID="PCGLabel" Text="2010 @ Ampsys Consulting Pvt. Ltd." runat="server"
                                    CssClass="PCGWhiteText"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DynamicLayout="false">
        <ProgressTemplate>
        <img src="Images/ajax-loader.gif" />loading.....
        </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    </form>
</body>
</html>

