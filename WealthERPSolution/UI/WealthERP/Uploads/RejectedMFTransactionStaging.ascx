<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RejectedMFTransactionStaging.ascx.cs"
    Inherits="WealthERP.Uploads.RejectedMFTransactionStaging" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<script type="text/javascript" src="../Scripts/JScript.js"></script>
<script src="../Scripts/jquery.min.js" type="text/javascript"></script>   

<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
<link href="/YUI/build/container/assets/container.css" rel="stylesheet" type="text/css" />
<link href="/YUI/build/menu/assets/skins/sam/menu.css" rel="stylesheet" type="text/css" />

<script src="/YUI/build/utilities/utilities.js" type="text/javascript"></script>
<script type="text/javascript" src="http://yui.yahooapis.com/2.8.1/build/yahoo/yahoo-min.js"></script>
<script src="/YUI/build/container/container-min.js" type="text/javascript"></script>

<!--This script is used for Progress bar -->
<%--This scripts includes the JQuery coding about the screen info   --%>

<script type="text/javascript">
    $(document).ready(function() {
        $(".flip").click(function() { $(".panel").slideToggle(); });
    });
</script>

<%--End--%>
<script type="text/javascript">
    function pageLoad() {
        InitDialogs();
        Loading(false);
    }

    function UpdateImg(ctrl, imgsrc) {
        var img = document.getElementById(ctrl);
        img.src = imgsrc;
    }

    // sets up all of the YUI dialog boxes
    function InitDialogs() {
        DialogBox_Loading = new YAHOO.widget.Panel("waitBox",
	{ fixedcenter: true, modal: true, visible: false,
	    width: "230px", close: false, draggable: true
	});
        DialogBox_Loading.setHeader("Processing, please wait...");
        DialogBox_Loading.setBody('<div style="text-align:center;"><img src="/Images/Wait.gif" id="Image1" /></div>');
        DialogBox_Loading.render(document.body);
    }
    function Loading(b) {
        if (b == true) {
            DialogBox_Loading.show();
        }
        else {
            DialogBox_Loading.hide();
        }
    }
</script>

<table width="100%" class="TableBackground">
<tr>
         <td class="HeaderCell">
            <img src="../Images/helpImage.png" height="25px" width="25px" style="float: right;" class="flip" />
          <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="MF Transaction Staging Rejects"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="4">
           <div class="panel">
              <p style="padding-top: 0; padding: 0; padding-bottom: 0;">View all the rejected Transactions from your Uploads, also the reason for Rejection. <br /> Click here to view all the Reject resaons and solutions -->&nbsp; <a href="http://www.wealtherp.com/help/#clientsetup" style="color: Red;" target="_blank">Reject Reasons and Solutions</a></p>
           </div>
        </td>
    </tr>
</table>


<table width="100%">
<tr>
<td align="center">
<div id="msgReprocessComplete" runat="server" class="success-msg" align="center" visible="false">
Reprocess successfully Completed
</td>
</tr>
</div>
</table>
<table style="width: 100%" class="TableBackground">
    <tr>
        <td>
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvWERPTrans" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ShowFooter="true" CssClass="GridViewStyle" DataKeyNames="CMFTSId" AllowSorting="true"
                OnSorting="gvWERPTrans_Sort">
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblRejectReason" runat="server" Text="Reject Reason"></asp:Label>
                            <asp:DropDownList ID="ddlRejectReason" CssClass="cmbLongField" AutoPostBack="true"
                                runat="server" OnSelectedIndexChanged="ddlRejectReason_SelectedIndexChanged">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRejectReasonHeader" runat="server" Text='<%# Eval("RejectReason").ToString() %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblHdrProcessId" runat="server" Text="Process Id"></asp:Label>
                            <asp:DropDownList ID="ddlProcessId" AutoPostBack="true" CssClass="cmbLongField" runat="server"
                                OnSelectedIndexChanged="ddlProcessId_SelectedIndexChanged">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblProcessID" runat="server" Text='<%# Eval("ProcessId").ToString() %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblHdrFileName" runat="server" Text="File Name"></asp:Label>
                            <asp:DropDownList ID="ddlFileName" AutoPostBack="true" CssClass="cmbLongField" runat="server"
                                OnSelectedIndexChanged="ddlFileName_SelectedIndexChanged">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("ExternalFileName").ToString() %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblHdrSourceType" runat="server" Text="Source Type"></asp:Label>
                            <asp:DropDownList ID="ddlSourceType" AutoPostBack="true" CssClass="cmbLongField"
                                runat="server" OnSelectedIndexChanged="ddlSourceType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSourceType" runat="server" Text='<%# Eval("SourceType").ToString() %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblInvName" runat="server" Text="Investor Name"></asp:Label><br />
                            <asp:DropDownList ID="ddlInvName" AutoPostBack="true" CssClass="cmbLongField" runat="server"
                                OnSelectedIndexChanged="ddlInvName_SelectedIndexChanged">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblInvNameData" runat="server" Text='<%# Bind("InvestorName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblHdrFolioNumber" runat="server" Text="Folio Number"></asp:Label>
                            <asp:DropDownList ID="ddlFolioNumber" AutoPostBack="true" CssClass="cmbLongField"
                                runat="server" OnSelectedIndexChanged="ddlFolioNumber_SelectedIndexChanged">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFolioNumber" runat="server" Text='<%# Bind("FolioNumber") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Scheme" HeaderText="Scheme" DataFormatString="{0:f4}"
                        ItemStyle-Wrap="false" />
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblHdrSchemeName" runat="server" Text="Scheme Name"></asp:Label>
                            <asp:DropDownList ID="ddlSchemeName" AutoPostBack="true" CssClass="cmbLongField"
                                runat="server" OnSelectedIndexChanged="ddlSchemeName_SelectedIndexChanged">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSchemeName" runat="server" Text='<%# Bind("SchemeName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblHdrTransactionType" runat="server" Text="Traansation Type"></asp:Label>
                            <asp:DropDownList ID="ddlTransactionType" AutoPostBack="true" CssClass="cmbLongField"
                                runat="server" OnSelectedIndexChanged="ddlTransactionType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTransactionType" runat="server" Text='<%# Bind("TransactionType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TransactionDate" HeaderText="Transaction Date" />
                    <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:f4}" />
                    <asp:BoundField DataField="Units" HeaderText="Units" DataFormatString="{0:f4}" />
                    <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:f4}" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr id="trReprocess" runat="server">
        <td class="SubmitCell">
            <asp:Button ID="btnReprocess" OnClick="btnReprocess_Click" runat="server" Text="Reprocess"
                CssClass="PCGButton" OnClientClick="Loading(true);" />
            <asp:Button ID="btnMapFolios" runat="server" CssClass="PCGButton" Text="Map Folios"
                OnClick="btnMapFolios_Click" />
        </td>
    </tr>
    <tr id="trMessage" runat="server" visible="false">
        <td class="Message">
            <label id="lblEmptyMsg" class="FieldName">
                There are no records to be displayed!</label>
        </td>
    </tr>
    <tr id="trErrorMessage" runat="server" visible="false">
        <td class="Message">
            <asp:Label ID="lblError" CssClass="Message" runat="server">
            </asp:Label>
        </td>
    </tr>
</table>
<div id="DivPager" runat="server" style="display: none">
    <table style="width: 100%">
        <tr align="center">
            <td>
                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" Value="WERPCustomerName ASC" />
<asp:HiddenField ID="hdnProcessIdFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnInvNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnFolioFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTransactionTypeFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRejectReasonFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnFileNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSourceTypeFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSchemeNameFilter" runat="server" Visible="false" />
<asp:Button ID="btnGridSearch" runat="server" Text="" OnClick="btnGridSearch_Click"
    BorderStyle="None" BackColor="Transparent" />