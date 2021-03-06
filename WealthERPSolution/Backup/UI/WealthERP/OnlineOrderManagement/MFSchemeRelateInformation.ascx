﻿<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="MFSchemeRelateInformation.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.MFSchemeRelateInformation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>
<script src="../Scripts/bootstrap.js" type="text/javascript"></script>
<link href="../Base/CSS/bootstrap-theme.css" rel="stylesheet" type="text/css" />
<link href="../Base/CSS/bootstrap.css" rel="stylesheet" type="text/css" />
<link href="../Base/CSS/bootstrap.min.css" rel="stylesheet" type="text/css" />

<asp:ScriptManager ID="scriptmanager" runat="server" EnablePageMethods="true">
    <%--<Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>--%>
</asp:ScriptManager>


<script type="text/javascript">
    function setCustomPosition(sender, args) {
        sender.moveTo(sender.get_left(), sender.get_top());
    }
   
    
</script>

<style type="text/css">
    @media only screen and (max-width: 900px)
    {
        /* Force table to not be like tables anymore */    #no-more-tables table, #no-more-tables thead, #no-more-tables tbody, #no-more-tables th, #no-more-tables td, #no-more-tables tr
        {
            display: block;
        }
        /* Hide table headers (but not display: none;, for accessibility) */    #no-more-tables thead tr
        {
            position: absolute;
            top: -9999px;
            left: -9999px;
        }
        #no-more-tables tr
        {
            border: 1px solid #ccc;
        }
        #no-more-tables td
        {
            /* Behave  like a "row" */
            border: none;
            border-bottom: 1px solid #eee;
            position: relative;
            padding-left: 60%;
            white-space: normal;
            text-align: left;
        }
        #no-more-tables td:before
        {
            /* Now like a table header */
            position: absolute; /* Top/left values mimic padding */
            top: 6px;
            left: 6px;
            width: 80%;
            padding-right: 10px;
            white-space: nowrap;
            text-align: left;
            font-weight: bold;
            z-index: auto;
        }
        /*
	Label the data
	*/    #no-more-tables td:before
        {
            content: attr(data-title);
        }
    }
    .alignCenter
    {
        text-align: center;
        font-size: 90%;
    }
    tr
    {
        font-family: Times New Roman;
        font-size: smaller;
    }
    table, th
    {
        border: 1px solid black;
    }
    .topBottomBorder
    {
        border-bottom-style: inset;
        border-bottom-width: thin;
        border-top-style: inset;
        border-top-width: thin;
        margin-bottom: 1%;
        padding-top: 1%;
        border-collapse: collapse;
        border-spacing: 10px;
    }
    .linkButton
    {
        font-size: small;
    }
    .linkButton:active
    {
        font-size: larger;
    }
    .page_enabled, .page_disabled
    {
        display: inline-block;
        height: 20px;
        min-width: 20px;
        line-height: 20px;
        text-align: center;
        text-decoration: none;
        border: 1px solid #ccc;
    }
    .page_enabled
    {
        background-color: #eee;
        color: #000;
    }
    .page_disabled
    {
        background-color: #6C6C6C;
        color: #fff !important;
    }
</style>
<style type="text/css">
    ul.tabs
    {
        margin: 0;
        padding: 0;
        float: left;
        list-style: none;
        height: 32px;
        border-bottom: 1px solid #999999;
        border-left: 1px solid #999999;
        width: 100%;
    }
    ul.tabs li
    {
        float: left;
        margin: 0;
        cursor: pointer;
        padding: 0px 21px;
        height: 31px;
        line-height: 31px;
        border: 1px solid #999999;
        border-left: none;
        font-weight: bold;
        background: #EEEEEE;
        overflow: hidden;
        position: relative;
    }
    ul.tabs li:hover
    {
        background: #CCCCCC;
    }
    ul.tabs li.active
    {
        background: #FFFFFF;
        border-bottom: 1px solid #FFFFFF;
    }
    .tab_container
    {
        border: 1px solid #999999;
        border-top: none;
        clear: both;
        float: left;
        width: 100%;
        background: #FFFFFF;
    }
    .tab_content
    {
        padding: 20px;
        font-size: 1.2em;
        display: none;
    }
</style>
<style type="text/css">
    body
    {
        font-family: Arial;
        font-size: 10pt;
    }
    .Repeater, .Repeater td, .Repeater td
    {
        border: 1px solid #ccc;
    }
    .Repeater td
    {
        background-color: #eee !important;
    }
    .Repeater th
    {
        background-color: #6C6C6C !important;
        color: White;
        font-size: 10pt;
        line-height: 200%;
    }
    .Repeater span
    {
        color: black;
        font-size: 10pt;
        line-height: 200%;
    }
    .page_enabled, .page_disabled
    {
        display: inline-block;
        height: 20px;
        min-width: 20px;
        line-height: 20px;
        text-align: center;
        text-decoration: none;
        border: 1px solid #ccc;
    }
    .page_enabled
    {
        background-color: #eee;
        color: #000;
    }
    .page_disabled
    {
        background-color: #6C6C6C;
        color: #fff !important;
    }
</style>


<asp:UpdatePanel ID="UpdatePanel1" runat="server"  RenderMode="Inline">
    <ContentTemplate>
        <div class="col-md-12" style="margin: 0px 20px 3px 12%; padding-top: 0.5%; padding-bottom: 0.5%;
            width: 75%;">
            <div id="dvDemo" visible="true" runat="server">
                <div class="col-md-12  col-xs-12 col-sm-12  topBottomBorder" runat="server" id="dvFundFilter"
                    visible="false">
                    <div class="col-md-12 col-xs-12 col-sm-12 " style="margin-bottom: 0%; margin-left: 0%;
                        padding-left: 0px">
                        <div class="col-md-2" style="padding-left: 0px; width: 9%; padding-top: 4px;">
                            <b style="font-family: Times New Roman">Fund Filter </b>
                        </div>
                        <div class="col-md-3" style="margin-bottom: 1%; margin-left: 0%; padding-left: 0px">
                            <asp:DropDownList ID="ddlAMC" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlAMC_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvtxtTransactionDate" ControlToValidate="ddlAMC"
                                ErrorMessage="<br />Please select AMC" Style="color: Red;" Display="Dynamic"
                                runat="server" InitialValue="0" ValidationGroup="btnViewscheme">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-2">
                            <fieldset>
                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged">
                                </asp:DropDownList>
                                <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlCategory"
                                ErrorMessage="<br />Please select category" Style="color: Red;" Display="Dynamic"
                                runat="server" InitialValue="0" ValidationGroup="btnViewscheme">
                            </asp:RequiredFieldValidator>--%>
                            </fieldset>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control input-sm"
                                class="form-control">
                            </asp:DropDownList>
                        </div>
                         <div class="col-md-2">
                                    <asp:DropDownList ID="ddlFilter" runat="server" AutoPostBack="false" CssClass="form-control input-sm">
                                        <asp:ListItem Text="SIP" Value="true"></asp:ListItem>
                                        <asp:ListItem Text="Normal" Value="false" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                        <div class="col-md-1">
                            <asp:Button ID="Button1" runat="server" class="btn btn-sm btn-primary" Text="GO"
                                OnClick="Go_OnClick" ValidationGroup="btnViewscheme"></asp:Button></div>
                    </div>
                    <div class="col-md-12" runat="server" visible="false">
                        <div class="col-md-3">
                            <asp:LinkButton ID="lbNFOList" runat="server" OnClick="GetSchemeDetails" CssClass="linkButton">NFO Scheme </asp:LinkButton>
                        </div>
                        <div class="col-md-3 ">
                            <asp:LinkButton ID="lbTopSchemes" runat="server" OnClick="GetSchemeDetails" CssClass="linkButton">Top Ten Schemes </asp:LinkButton>
                        </div>
                        <div class="col-md-3">
                            <asp:LinkButton ID="lbViewWatchList" runat="server" OnClick="GetSchemeDetails" CssClass="linkButton">My Watch list  </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="container">
                <telerik:RadTabStrip ID="RadTabStripAdsUpload" runat="server" EnableTheming="True"
                    EnableEmbeddedSkins="true" MultiPageID="multipageAdsUpload" SelectedIndex="0"
                    Skin="Outlook">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Top Performers" Value="TRated" Width="455px"
                            font-family="Times New Roman" Font-Bold="true">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Top Rated" Value="Tperformer" TabIndex="0" Width="455px"
                            font-family="Times New Roman" Font-Bold="true">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="multipageAdsUpload" EnableViewState="true" runat="server">
                    <telerik:RadPageView ID="rpvTopRated" runat="server" Selected="true">
                        <div id="dvMarketData">
                            <div class="row" style="margin-bottom: 0%; padding-top: 3px">
                                <div class="col-md-1" style="width: 65px; padding-top: 5px; font-family: Times New Roman">
                                    Category
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlMarketCategory" runat="server" CssClass="form-control input-sm"
                                        AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1" style="width: 40px; padding-top: 5px; font-family: Times New Roman;
                                    display: none">
                                    Type
                                </div>
                                <div class="col-md-1" style="display: none">
                                    <asp:DropDownList ID="ddlSIP" runat="server" AutoPostBack="false" CssClass="form-control input-sm">
                                        <asp:ListItem Text="SIP" Value="true"></asp:ListItem>
                                        <asp:ListItem Text="Normal" Value="false" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1" style="width: 60px; padding-top: 5px; font-family: Times New Roman">
                                    Returns
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlReturns" runat="server" AutoPostBack="false" CssClass="form-control input-sm">
                                        <asp:ListItem Text="1 Year" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="3 Year" Value="2" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="5 Year" Value="3" ></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1">
                                    <asp:DropDownList ID="ddlCompare" runat="server" AutoPostBack="false" CssClass="form-control input-sm">
                                        <asp:ListItem Text="<" Value="1" ></asp:ListItem>
                                        <asp:ListItem Text=">" Value="2" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1">
                                    <asp:TextBox ID="txtcmpvalue" runat="server" Width="60px" Height="25px" Text="10"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <asp:Button ID="btnTopRated" runat="server" class="btn btn-sm btn-primary" Text="GO"
                                        OnClick="btnTopRated_OnClick"></asp:Button></div>
                                <div class="col-md-1" style="padding: 5px 0px 0px 0px; font-family: Times New Roman;
                                    width: 4.2%">
                                    Sort On:
                                </div>
                                <div class="col-md-2" style="padding: 1px !important;">
                                    <asp:DropDownList ID="ddlMarketDataSort" runat="server" AutoPostBack="true" CssClass="form-control input-sm"
                                        OnSelectedIndexChanged="ddlMarketDataSort_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="no-more-tables">
                            <table class="col-md-12 table-bordered table-striped table-condensed cf" style="width: 90%;">
                                <thead class="cf">
                                    <tr style="background-color: #2480c7; font-size: small; color: White; text-align: center">
                                        <th data-title="Scheme Name" class="alignCenter">
                                            Scheme
                                        </th>
                                        <th data-title="category" class="alignCenter" runat="server" visible="true" id="th2">
                                            Category
                                        </th>
                                        <th data-title="Rating" class="alignCenter">
                                            Rating
                                        </th>
                                        <th data-title="NAV" class="alignCenter" runat="server" visible="true" id="th6">
                                            NAV (Date)
                                        </th>
                                        <th data-title="Return" class="alignCenter" runat="server" visible="true" id="th7">
                                            Returns (1yr / 3Yr / 5yr)
                                        </th>
                                        <th data-title="Buy" class="alignCenter" runat="server" visible="false" id="th8">
                                            Buy
                                        </th>
                                        <th data-title="SIP" class="alignCenter" runat="server" visible="false" id="th9">
                                            SIP
                                        </th>
                                        <th data-title="Action" class="alignCenter" runat="server" visible="true" id="th10">
                                            Action
                                        </th>
                                        <th data-title="Mode" class="alignCenter" runat="server" visible="true" id="th3">
                                            Mode
                                        </th>
                                        <th data-title="Watch" class="alignCenter">
                                            Watch
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr style="font-family: Times New Roman; font-size: smaller">
                                        <asp:Repeater ID="rptTopMarketSchemes" runat="server" OnItemCommand="rptTopMarketSchemes_OnItemCommand"
                                            OnItemDataBound="rptTopMarketSchemes_OnItemDataBound">
                                            <ItemTemplate>
                                                <td data-title="Scheme Name">
                                                    <asp:LinkButton ID="lbSchemeName" runat="server" ToolTip="Click To view Details Information"
                                                        CommandName="schemeDetails" CommandArgument='<%# Eval("PASP_SchemePlanCode")%>'
                                                        Font-Size="Small"> <%# Eval("PASP_SchemePlanName")%>
                                                    </asp:LinkButton>
                                                    <asp:Label ID="lblschemeCode" runat="server" Text='<%# Eval("PASP_SchemePlanCode")%>'
                                                        Font-Size="Small" Visible="false"></asp:Label>
                                                </td>
                                                <td data-title="category" runat="server" visible="true" id="tdNFOcategory">
                                                    <asp:Label ID="lblNFOCategory" runat="server" Text='<%# Eval("PAIC_AssetInstrumentCategoryName")%>'
                                                        Font-Size="Small"></asp:Label>
                                                </td>
                                                <td data-title="Rating">
                                                    <asp:Image runat="server" ID="imgRatingOvelAll" ImageUrl='<%# string.Format("../Images/MorningStarRating/RatingSmallIcon/{0}.{1}", Eval("SchemeRatingOverall"),"png")%>' />
                                                </td>
                                                <td data-title="NAV" runat="server" visible="true" id="tdNAV">
                                                    <asp:Label ID="lblNavVale" runat="server" Text='<%# Eval("CMFNP_NAVDate")%> ' Font-Size="Small"></asp:Label>
                                                </td>
                                                <td data-title="Return" runat="server" visible="true" id="tdReturn">
                                                    <asp:Label ID="lblReturn" runat="server" Text='<%# Eval("schemeReturns")%>' Font-Size="Small"></asp:Label>
                                                </td>
                                                <td data-title="Buy" runat="server" visible="false" id="tdBuy">
                                                    <asp:LinkButton ID="lbBuy" runat="server" CommandArgument='<%# Eval("PASP_SchemePlanCode")%>'
                                                        CommandName="Buy" Visible='<%# Eval("IsSchemePurchege")%>'>   
                                                <img src="../Images/Buy_BIG_Buttons.png" height="30px" width="50px"/>
                                               
                                              
                                    </span>
                                                    </asp:LinkButton>
                                                </td>
                                                <td data-title="SIP" runat="server" visible="false" id="tdSIP">
                                                    <asp:LinkButton ID="lbSIP" runat="server" CommandArgument='<%# Eval("PASP_SchemePlanCode")%>'
                                                        CommandName="SIP" Visible='<%# Eval("IsSchemeSIPType")%>'> <img src="../Images/SIP_BIG_Buttons.png" height="30px" width="50px"/>  </asp:LinkButton>
                                                </td>
                                                <td data-title="Action" runat="server" id="tdAction">
                                                    <asp:DropDownList ID="ddlMarketlAction" runat="server" AutoPostBack="true" CssClass="cmbField"
                                                        Width="70px" OnSelectedIndexChanged="ddlMarketlAction_OnSelectedIndexChanged">
                                                        <asp:ListItem Text="select" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Purchase" Value="Buy"></asp:ListItem>
                                                        <asp:ListItem Text="SIP" Value="SIP"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblIsPurchase" runat="server" Text='<%# Eval("IsSchemePurchege")%>'
                                                        Font-Size="Small" Visible="false"></asp:Label>
                                                    <asp:Label ID="lblIsSIP" runat="server" Text='<%# Eval("IsSchemeSIPType")%>' Font-Size="Small"
                                                        Visible="false"></asp:Label>
                                                </td>
                                                <td data-title="Mode" runat="server" visible="true" id="td1">
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Mode")%> ' Font-Size="Small"></asp:Label>
                                                </td>
                                                <td data-title="Watch">
                                                    <asp:LinkButton ID="lbRemoveWatch" runat="server" CommandName="RemoveFrmWatch" Visible='<% #(Convert.ToBoolean(Eval("IsInWatch"))==true) ? true : false %>'
                                                        Font-Size="Small" CommandArgument='<%# Eval("PASP_SchemePlanCode")%>'>Remove <span class="glyphicon glyphicon-dashboard">
                            </span></asp:LinkButton>
                                                    <asp:LinkButton ID="lbAddToWatch" runat="server" CommandName="addToWatch" CommandArgument='<%# Eval("PASP_SchemePlanCode")%>'
                                                        Visible='<% #(Convert.ToBoolean(Eval("IsInWatch"))==true) ? false :true %>' Font-Size="Small"> Add To <span class="glyphicon glyphicon-dashboard">
                            </span>
                                                    </asp:LinkButton>
                                                </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                        <div class="row">
                            <div class="col-md-12" style="margin-top: 1%">
                                <asp:Repeater ID="rpTopPager" runat="server">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                            CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                            OnClick="rpTopPager_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvTopPerformer" runat="server">
                        <div id="dvSchemeDetails">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:RadioButtonList ID="rblNFOType" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rblNFOType_OnSelectedIndexChanged" BorderStyle="None"
                                        CellSpacing="2" CellPadding="2">
                                        <asp:ListItem Text="Active" Value="true" Selected="True" style="margin-right: 10px;
                                            font-size: small; color: #2475C7"></asp:ListItem>
                                        <asp:ListItem Text="Closed" Value="false" style="font-size: small; color: #2475C7"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblHeading" runat="server" Text="Scheme Details" Font-Bold="true"
                                        Font-Size="Larger" ForeColor="#2475C7" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="row" style="margin-bottom: 1%; padding-top: 3px" runat="server" id="dvfilterTopRated">
                                <div class="col-md-1" style="width: 65px; padding-top: 5px">
                                    Category
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlTopCategory" runat="server" CssClass="form-control input-sm"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1" style="width: 40px; padding-top: 5px">
                                    Type
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlTopRatedType" runat="server" AutoPostBack="false" CssClass="form-control input-sm">
                                        <asp:ListItem Text="SIP" Value="true"></asp:ListItem>
                                        <asp:ListItem Text="Normal" Value="false" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1">
                                    <asp:Button ID="btnTopPeformers" runat="server" class="btn btn-sm btn-primary" Text="GO"
                                        OnClick="btnTopPeformers_OnClick"></asp:Button>
                                </div>
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-1" style="padding: 5px 0px 0px 0px; font-family: Times New Roman;
                                    width: 4.2%">
                                    Sort On:
                                </div>
                                <div class="col-md-2" style="padding: 1px !important;">
                                    <asp:DropDownList ID="ddlTopRatedSort" runat="server" AutoPostBack="true" CssClass="form-control input-sm"
                                        OnSelectedIndexChanged="ddlTopRatedSort_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div id="no-more-tables">
                                <table class="col-md-12 table-bordered table-striped table-condensed cf" style="width: 90%">
                                    <thead class="cf">
                                        <tr style="background-color: #2480c7; font-size: small; color: White; text-align: center">
                                            <th data-title="SchemeRank" class="alignCenter" runat="server" visible="false" id="thSchemeRank">
                                                Rank
                                            </th>
                                            <th data-title="Scheme Name" class="alignCenter">
                                                Scheme
                                            </th>
                                            <th data-title="category" class="alignCenter" runat="server" visible="false" id="thNFOcategory">
                                                Category
                                            </th>
                                            <th data-title="Rating" class="alignCenter">
                                                Rating
                                            </th>
                                            <th data-title="FundManager" class="alignCenter">
                                                Fund Manager
                                            </th>
                                            <th data-title="startDate" class="alignCenter" runat="server" visible="false" id="thNFOStrtDate">
                                                Start Date
                                            </th>
                                            <th data-title="endDate" class="alignCenter" runat="server" visible="false" id="thNFOEndDate">
                                                End Date
                                            </th>
                                            <th data-title="MiniAmt" class="alignCenter" runat="server" visible="false" id="thNFOAmt">
                                                Min Initial Amt
                                            </th>
                                            <th data-title="NAV" class="alignCenter" runat="server" visible="false" id="thNAV">
                                                NAV (Date)
                                            </th>
                                            <th data-title="Return" class="alignCenter" runat="server" visible="false" id="thReturn">
                                                Returns (1yr / 3Yr / 5yr)
                                            </th>
                                            <th data-title="Buy" class="alignCenter" runat="server" visible="false" id="thBuy">
                                                Buy
                                            </th>
                                            <th data-title="SIP" class="alignCenter" runat="server" visible="false" id="thSIP">
                                                SIP
                                            </th>
                                            <th data-title="Action" class="alignCenter" runat="server" visible="true" id="th1">
                                                Action
                                            </th>
                                            <th data-title="Mode" class="alignCenter" runat="server" visible="true" id="th4">
                                                Mode
                                            </th>
                                            <th data-title="Watch" class="alignCenter" runat="server" visible="true" id="thWatch">
                                                Watch
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <asp:Repeater ID="rpSchemeDetails" runat="server" OnItemCommand="rpSchemeDetails_OnItemCommand"
                                                OnItemDataBound="rpSchemeDetails_OnItemDataBound">
                                                <ItemTemplate>
                                                    <td data-title="SchemeRank" class="alignCenter" runat="server" visible="false" id="tdSchemeRank">
                                                        <asp:Label ID="lblSchemeRank" runat="server" Text='<%# Eval("PMFSR_SchemeRank")%>'
                                                            Font-Size="90%"></asp:Label>
                                                        <asp:Label ID="lblSchemeCode" runat="server" Text='<%# Eval("PASP_SchemePlanCode")%>'
                                                            Font-Size="90%" Visible="false"></asp:Label>
                                                    </td>
                                                    <td data-title="Scheme Name">
                                                        <asp:LinkButton ID="lbSchemeName" runat="server" ToolTip="Click To view Details Information"
                                                            CommandName="schemeDetails" CommandArgument='<%# Eval("PASP_SchemePlanCode")%>'
                                                            Font-Size="Small"> <%# Eval("PASP_SchemePlanName")%>
                                                        </asp:LinkButton>
                                                    </td>
                                                    <td data-title="category" runat="server" visible="false" id="tdNFOcategory">
                                                        <asp:Label ID="lblNFOCategory" runat="server" Text='<%# Eval("PAIC_AssetInstrumentCategoryName")%>'
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td data-title="Rating">
                                                        <asp:Image runat="server" ID="imgRatingOvelAll" ImageUrl='<%# string.Format("../Images/MorningStarRating/RatingSmallIcon/{0}.{1}", Eval("SchemeRatingOverall"),"png")%>' />
                                                    </td>
                                                    <td data-title="FundManager">
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("PMFRD_FundManagerName")%>'
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td data-title="startDate" runat="server" visible="false" id="tdNFOStrtDate">
                                                        <asp:Label ID="lblNFOStart" runat="server" Text='<%# Eval("PASPD_NFOStartDate")%>'
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td data-title="endDate" runat="server" visible="false" id="tdNFOEndDate">
                                                        <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("PASPD_NFOEndDate")%>' Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td data-title="MiniAmt " runat="server" visible="false" id="tdNFOAmt">
                                                        <asp:Label ID="lblMiniAmt" runat="server" Text='<%# Eval("MiniAmt")%>' Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td data-title="NAV" runat="server" visible="false" id="tdNAV">
                                                        <asp:Label ID="lblNavVale" runat="server" Text='<%# Eval("CMFNP_NAVDate")%> ' Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td data-title="Return" runat="server" visible="false" id="tdReturn">
                                                        <asp:Label ID="lblReturn" runat="server" Text='<%# Eval("schemeReturns")%>' Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td data-title="Buy" runat="server" visible="false" id="tdBuy">
                                                        <asp:LinkButton ID="lbBuy" runat="server" CommandArgument='<%# Eval("PASP_SchemePlanCode")%>'
                                                            CommandName="Buy" Visible='<%# Eval("IsSchemePurchege")%>'>   
                                                <img src="../Images/Buy_BIG_Buttons.png" height="30px" width="50px"/>
                                              
                                    </span>
                                                        </asp:LinkButton>
                                                    </td>
                                                    <td data-title="SIP" runat="server" visible="false" id="tdSIP">
                                                        <asp:LinkButton ID="lbSIP" runat="server" CommandArgument='<%# Eval("PASP_SchemePlanCode")%>'
                                                            CommandName="SIP" Visible='<%# Eval("IsSchemeSIPType")%>'> <img src="../Images/SIP_BIG_Buttons.png" height="30px" width="50px"/>  </asp:LinkButton>
                                                    </td>
                                                    <td data-title="Action" runat="server" id="tdAction">
                                                        <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" CssClass="cmbField"
                                                            Width="70px" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChanged">
                                                            <asp:ListItem Text="select" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Purchase" Value="Buy"></asp:ListItem>
                                                            <asp:ListItem Text="SIP" Value="SIP"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lblIsPurchase" runat="server" Text='<%# Eval("IsSchemePurchege")%>'
                                                            Font-Size="Small" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblIsSIP" runat="server" Text='<%# Eval("IsSchemeSIPType")%>' Font-Size="Small"
                                                            Visible="false"></asp:Label>
                                                    </td>
                                                    <td data-title="Mode" runat="server" visible="true" id="td1">
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Mode")%> ' Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td data-title="Watch" runat="server" visible="true" id="tdWatch">
                                                        <asp:LinkButton ID="lbRemoveWatch" runat="server" CommandName="RemoveFrmWatch" Visible='<% #(Convert.ToBoolean(Eval("IsInWatch"))==true) ? true : false %>'
                                                            Font-Size="Small" CommandArgument='<%# Eval("PASP_SchemePlanCode")%>'>Remove <span class="glyphicon glyphicon-dashboard">
                            </span></asp:LinkButton>
                                                        <asp:LinkButton ID="lbAddToWatch" runat="server" CommandName="addToWatch" CommandArgument='<%# Eval("PASP_SchemePlanCode")%>'
                                                            Visible='<% #(Convert.ToBoolean(Eval("IsInWatch"))==true) ? false :true %>' Font-Size="Small"> Add To <span class="glyphicon glyphicon-dashboard">
                            </span>
                                                        </asp:LinkButton>
                                                    </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                            <div class="row">
                                <div class="col-md-12" style="margin-top: 1%">
                                    <asp:Repeater ID="rptPager" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                                OnClick="Page_Changed"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </div>
        </div>
        <asp:HiddenField ID="hfAMCCode" runat="server" />
        <asp:HiddenField ID="hfSchemeCode" runat="server" />
        <asp:HiddenField ID="hfCategory" runat="server" />
        <asp:HiddenField ID="hfCustomerId" runat="server" />
        <asp:HiddenField ID="hfIsSchemeDetails" runat="server" />
        <asp:HiddenField ID="hfNFOType" runat="server" />
        <asp:HiddenField ID="hfIsSIP" runat="server" />
        <asp:HiddenField ID="hfMarketSortOn" runat="server" />
        <asp:HiddenField ID="hfSortOn" runat="server" />
    </ContentTemplate>
    <Triggers>
       
    </Triggers>
</asp:UpdatePanel>
