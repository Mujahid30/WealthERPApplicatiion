<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductOnlineFundNews.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.ProductOnlineFundNews" %>
<asp:ScriptManager ID="scriptmanager" runat="server">
</asp:ScriptManager>
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
<table class="tblMessage" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <div class="divOnlinePageHeading" style="height: 25px;">
               
            </div>
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="dvDemo" style="margin-left: 11%; margin-top: 1%; margin-bottom: 0.5%; margin-right: 5%;
            padding-top: 0.5%; padding-bottom: 0.5%; width: 80%" visible="true" runat="server">
            <div class="col-md-12 col-xs-12col-sm-12" style="margin-top: 0px;">
                <div class="col-md-12 dottedBottom">
                    <b>Fund News</b>
                    <b><asp:LinkButton ID="lblBack" runat="server" Visible="false" OnClick="lnkBack_lnkMoreNews" Text="Back" style="float:right;"></asp:LinkButton></b>
                </div>
                 
                <div id="FundNews" runat="server">
                    <asp:Repeater ID="RepNews" runat="server" OnItemCommand="repFundDetails_OnItemCommand">
                        <ItemTemplate>
                            <div class="dottedBottom">
                                <div>
                                    <asp:Label ID="lblHeading" runat="server" ></asp:Label>
                                    <asp:LinkButton ID="lnkMoreNews" Style=" color: Black;"
                                        runat="server" Text='<%# Eval("heading")%>' ToolTip="Detail"  CommandName="NewsDetailsLnk" CommandArgument='<%# Eval("sno")%>'></asp:LinkButton>
                                </div>
                                <div>
                                    <asp:Label ID="lblDate" Style="font-size: x-small; color:Gray;" runat="server" Text='<%# Eval("date")%>'></asp:Label>
                                    
                                        <asp:Label ID="lblSchemeID" runat="server" Text='<%# Eval("sno")%>' Visible="false"></asp:Label>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div id="divNewsDetails" runat="server" visible="false">
                    <asp:Repeater ID="repFundDetails" runat="server">
                        <ItemTemplate>
                        <div style="margin-bottom:1%;">
                         <b style="font-family:Times New Roman;"><asp:Label ID="lblNewsDetails" runat="server" Text='<%# Eval("heading")%>'></asp:Label></b>
                        </div>
                            <div>
                                <asp:Label ID="lblNewsDetais" style="font-family:Times New Roman;" runat="server" Text='<%# Eval("arttext")%>'></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
