<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FAQandDemo.ascx.cs" Inherits="WealthERP.OnlineOrderManagement.FAQandDemo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    @media only screen and (max-width: 800px)
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
        font-size: small;
    }
    table, th
    {
        border: 1px solid black;
    }
    .dottedBottom
    {
        border-bottom-style: inset;
        border-bottom-width: thin;
        margin-bottom: 1%;
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

<script src="../Scripts/bootstrap.js" type="text/javascript"></script>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<link href="../Base/CSS/bootstrap-theme.css" rel="stylesheet" type="text/css" />
<link href="../Base/CSS/bootstrap.css" rel="stylesheet" type="text/css" />
<link href="../Base/CSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
<table cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <div style="height: 25px;">
            </div>
        </td>
    </tr>
</table>
<div id="Div1" style="margin-left: 11%; margin-top: 1%; margin-bottom: 0.5%; margin-right: 5%;
            padding-top: 0.5%; padding-bottom: 0.5%; width: 80%" visible="false" runat="server">
            <div class="col-md-12 col-xs-12col-sm-12" style="margin-top: 0px;">
                <div class="col-md-12 dottedBottom">
                    <b>Demo's</b>
                </div>
                <div id="Div2" runat="server">
                    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_OnItemCommand">
                        <ItemTemplate>
                            <div class="dottedBottom">
                                <div>
                                    <asp:LinkButton ID="lnkDemo"  runat="server" Text='<%# Eval("PUHD_Heading")%>'
                                        ToolTip="Click To View" CommandName="OpenDemo" CommandArgument='<%# Eval("PUHD_HelpDetails")%>'></asp:LinkButton>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
<div id="Div3" style="margin-left: 11%; margin-top: 1%; margin-bottom: 0.5%; margin-right: 5%;
    padding-top: 0.5%; padding-bottom: 0.5%; width: 80%" visible="false" runat="server">
    <div class="col-md-12 col-xs-12 col-sm-12" style="margin-top: 0px;">
        <div class="col-md-12 dottedBottom">
            <b>FAQ's</b>
        </div>
        <div id="Div4" runat="server">
            <asp:Repeater ID="Repeater2" runat="server" OnItemCommand="Repeater2_OnItemCommand">
                <ItemTemplate>
                    <div class="dottedBottom">
                        <div>
                            <asp:LinkButton ID="lnkFaq" runat="server" Text='<%# Eval("PUHD_Heading")%>'
                                ToolTip="Click To View"
                                CommandName="OpenFaq" CommandArgument='<%# Eval("PUHD_HelpDetails")%>'></asp:LinkButton>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>