﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RejectedMFTransactionStaging.ascx.cs"
    Inherits="WealthERP.Uploads.RejectedMFTransactionStaging" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
<link href="/YUI/build/container/assets/container.css" rel="stylesheet" type="text/css" />
<link href="/YUI/build/menu/assets/skins/sam/menu.css" rel="stylesheet" type="text/css" />

<script src="/YUI/build/utilities/utilities.js" type="text/javascript"></script>

<script type="text/javascript" src="http://yui.yahooapis.com/2.8.1/build/yahoo/yahoo-min.js"></script>

<script src="/YUI/build/container/container-min.js" type="text/javascript"></script>

<!--This script is used for Progress bar -->
<%--This scripts includes the JQuery coding about the screen info   --%>

<script>
    function ShowPopup() {
        var form = document.forms[0];
        var transactionId = "";
        var count = 0
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == 'checkbox') {
                if (form.elements[i].checked == true) {
                    count++;
                    hiddenField = form.elements[i].id.replace("chkId", "hdnchkBx");
                    hiddenFieldValues = document.getElementById(hiddenField).value;
                    var splittedValues = hiddenFieldValues.split("-");
                    if (count == 1) {
                        transactionId = splittedValues[0];
                    }
                    else {
                        transactionId = transactionId + "~" + splittedValues[0];
                    }
                    RejectReasonCode = splittedValues[1];
                }
            }
        }
        //        if (count > 1) {
        //            alert("You can select only one record at a time.")
        //            return false;
        //        }
        if (count == 0) {
            alert("Please select one record.")
            return false;
        }
        window.open('Uploads/MapToCustomers.aspx?id=' + transactionId + '', 'mywindow', 'width=550,height=450,scrollbars=yes,location=no')
        return false;
    }
</script>

<script type="text/javascript">
    $(document).ready(function() {
        $(".flip").click(function() { $(".panel").slideToggle(); });
    });
</script>

<%--End--%>

<script type="text/javascript">
    function ShowPopupProbableInsert_Delete() {

        var form = document.forms[0];
        var count = 0
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == 'checkbox') {
                if (form.elements[i].checked == true) {
                    count++;

                }
            }
        }
        //        if (count > 1) {
        //            alert("You can select only one record at a time.")
        //            return false;
        //        }
        if (count == 0) {
            alert("Please select probable duplicate transaction record.")
            return false;
        }
    }
</script>

<script>
    function ShowPopup() {
        var form = document.forms[0];
        var transactionId = "";
        var count = 0
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == 'checkbox') {
                if (form.elements[i].checked == true) {
                    count++;
                    hiddenField = form.elements[i].id.replace("chkId", "hdnchkBx");
                    hiddenFieldValues = document.getElementById(hiddenField).value;
                    var splittedValues = hiddenFieldValues.split("-");
                    if (count == 1) {
                        transactionId = splittedValues[0];
                    }
                    else {
                        transactionId = transactionId + "~" + splittedValues[0];
                    }

                }
            }
        }
        //        if (count > 1) {
        //            alert("You can select only one record at a time.")
        //            return false;
        //        }
        if (count == 0) {
            alert("Please select one record.")
            return false;
        }
        window.open('Uploads/MapToCustomers.aspx?id=' + transactionId + '', 'mywindow', 'width=550,height=450,scrollbars=yes,location=no')
        return true;
    }
</script>

<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var gvControl = document.getElementById('<%= gvWERPTrans.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chkId";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkIdAll");

        //get an array of input types in the gridview
        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
</script>

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

<script language="javascript" type="text/javascript">

    //    Function to call btnReprocess_Click method to refresh user control

    function Reprocess() {

        document.getElementById('<%= btnReprocess.ClientID %>').click();
    }
</script>

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            MF Transaction Staging Rejects
                        </td>
                        <td align="right">
                            <img src="../Images/helpImage.png" height="15px" width="20px" style="float: right;"
                                class="flip" />
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%" class="TableBackground">
    <%-- <tr>
        <td class="HeaderCell">
            <img src="../Images/helpImage.png" height="25px" width="25px" style="float: right;"
                class="flip" />
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="MF Transaction Staging Rejects"></asp:Label>
            <hr />
        </td>
    </tr>--%>
    <tr>
        <td colspan="4">
            <div class="panel">
                <p style="padding-top: 0; padding: 0; padding-bottom: 0;">
                    View all the rejected transactions from your uploads, also the reason for rejection.
                    <br />
                    Click here to view all the Reject reasons and solutions. -->&nbsp; <a href="http://www.wealtherp.com/help/#clientsetup"
                        style="color: Red;" target="_blank">Reject Reasons and Solutions</a></p>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgReprocessComplete" runat="server" class="success-msg" align="center"
                visible="false">
                Reprocess successfully Completed
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgReprocessincomplete" runat="server" class="failure-msg" align="center"
                visible="false">
                Reprocess Failed!
            </div>
        </td>
    </tr>
</table>
<table style="width: 100%" class="TableBackground">
    <tr>
        <td>
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>
        </td>
    </tr>
    <%-- <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>--%>
</table>
<asp:Panel ID="Panel2" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
    <table cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <telerik:RadGrid ID="gvWERPTrans" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" Skin="Telerik"
                    EnableEmbeddedSkins="false" Width="1050px" AllowFilteringByColumn="true" AllowAutomaticInserts="false"
                    ExportSettings-FileName="MF Transaction Reject Details" ShowFooter="true" OnPreRender="gvWERPTrans_PreRender"
                    OnItemCreated="gvWERPTrans_ItemCreated" OnNeedDataSource="gvWERPTrans_NeedDataSource">
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView TableLayout="Auto" DataKeyNames="CMFTSId,ProcessId,FolioNumber,InvestorName"
                        Width="120%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action">
                                <HeaderTemplate>
                                    <input id="chkIdAll" name="chkIdAll" type="checkbox" onclick="checkAllBoxes()" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkId" runat="server" />
                                    <asp:HiddenField ID="hdnchkBx" runat="server" Value='<%# Eval("CMFTSId").ToString()%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="RejectReason" AllowFiltering="true" HeaderText="RejectReason"
                                UniqueName="RejectReason">
                                <FilterTemplate>
                                    <telerik:RadComboBox AllowCustomText="true" ID="rcbRejectReasonFilter" Width="280px"
                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="rcbRejectReasonFilter_OnSelectedIndexChanged">
                                    </telerik:RadComboBox>
                                </FilterTemplate>
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="InvestorName" AllowFiltering="true" HeaderText="InvestorName"
                                UniqueName="InvestorName" SortExpression="InvestorName" AutoPostBackOnFilter="true"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ProcessId" AllowFiltering="true" HeaderText="ProcessId"
                                UniqueName="ProcessId" SortExpression="ProcessId" AutoPostBackOnFilter="true"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="8px" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FolioNumber" AllowFiltering="true" HeaderText="FolioNumber"
                                UniqueName="FolioNumber" SortExpression="FolioNumber" AutoPostBackOnFilter="true"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SchemeName" AllowFiltering="true" HeaderText="SchemeName"
                                UniqueName="SchemeName" SortExpression="SchemeName" AutoPostBackOnFilter="true"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TransactionType" AllowFiltering="true" HeaderText="TransactionType"
                                UniqueName="TransactionType" SortExpression="TransactionType" AutoPostBackOnFilter="true"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn DataField="TransactionDate" SortExpression="TransactionDate"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" AllowFiltering="true"
                                HeaderText="TransactionDate" UniqueName="TransactionDate" DataFormatString="{0:d}"
                                ShowFilterIcon="false">
                                <ItemStyle Width="110px" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                <FilterTemplate>
                                    <telerik:RadDatePicker Width="250px" ID="calFilter" runat="server">
                                    </telerik:RadDatePicker>
                                </FilterTemplate>
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn DataField="Price" AllowFiltering="false" HeaderText="Price"
                                UniqueName="Price" Aggregate="Sum" DataFormatString="{0:N2}">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Units" AllowFiltering="false" HeaderText="Units"
                                UniqueName="Units" Aggregate="Sum" DataFormatString="{0:N2}">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Amount" AllowFiltering="false" HeaderText="Amount"
                                UniqueName="Amount" Aggregate="Sum" DataFormatString="{0:N2}">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ExternalFileName" AllowFiltering="true" HeaderText="ExternalFileName"
                                UniqueName="ExternalFileName" SortExpression="ExternalFileName" AutoPostBackOnFilter="true"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SourceType" AllowFiltering="true" HeaderText="SourceType"
                                UniqueName="SourceType" SortExpression="SourceType" AutoPostBackOnFilter="true"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XMLCSL_Name" AllowFiltering="true" HeaderText="Active Level"
                                UniqueName="XMLCSL_Name" SortExpression="XMLCSL_Name" AutoPostBackOnFilter="true"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <%--<asp:GridView ID="gvWERPTrans" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ShowFooter="true" CssClass="GridViewStyle" DataKeyNames="CMFTSId,ProcessId,FolioNumber,InvestorName" AllowSorting="true"
                    OnSorting="gvWERPTrans_Sort">
                    <FooterStyle CssClass="FooterStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <HeaderTemplate>
                            <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                            <input id="chkIdAll" name="chkIdAll" type="checkbox" onclick="checkAllBoxes()" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkId" runat="server" />
                            <asp:HiddenField ID="hdnchkBx" runat="server" Value='<%# Eval("CMFTSId").ToString()%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblRejectReason" runat="server" Text="Reject Reason"></asp:Label>
                                <asp:DropDownList ID="ddlRejectReason" CssClass="cmbLongField" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="ddlRejectReason_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblRejectReasonHeader"  runat="server" Text='<%# Eval("RejectReason").ToString() %>'></asp:Label>
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
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        
                        <asp:BoundField HeaderText="PAN Number" DataField="CMFTS_PANNum" />
                        
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblHdrProcessId" runat="server" Text="Process Id"></asp:Label>
                                <asp:DropDownList ID="ddlProcessId" AutoPostBack="true" CssClass="cmbField" runat="server"
                                    OnSelectedIndexChanged="ddlProcessId_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblProcessID" runat="server" Text='<%# Eval("ProcessId").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                  
                        
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblHdrFolioNumber" runat="server" Text="Folio Number"></asp:Label>
                                <asp:DropDownList ID="ddlFolioNumber" AutoPostBack="true" CssClass="cmbField" runat="server"
                                    OnSelectedIndexChanged="ddlFolioNumber_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFolioNumber" runat="server" Text='<%# Bind("FolioNumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Scheme" HeaderText="Scheme" DataFormatString="{0:f4}"
                            ItemStyle-Wrap="false">
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
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
                                <asp:Label ID="lblHdrTransactionType" runat="server" Text="Transaction Type"></asp:Label>
                                <asp:DropDownList ID="ddlTransactionType" AutoPostBack="true" CssClass="cmbField"
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
                                <asp:DropDownList ID="ddlSourceType" AutoPostBack="true" CssClass="cmbField" runat="server"
                                    OnSelectedIndexChanged="ddlSourceType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSourceType" runat="server" Text='<%# Eval("SourceType").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView>--%>
            </td>
        </tr>
    </table>
</asp:Panel>
<table width="100%">
    <tr id="trReprocess" runat="server">
        <td class="SubmitCell">
            <asp:Button ID="btnReprocess" OnClick="btnReprocess_Click" runat="server" Text="Reprocess"
                CssClass="PCGLongButton" OnClientClick="Loading(true);" />
            <asp:Button ID="btnMapFolios" runat="server" CssClass="PCGLongButton" Text="Map Folios"
                OnClientClick="return ShowPopup()" />
            <asp:Button ID="btnDelete" runat="server" CssClass="PCGLongButton" Text="Delete Records"
                OnClick="btnDelete_Click" />
            <asp:Button ID="btnProbableDuplicateInsert" runat="server" CssClass="PCGLongLongButton"
                Text="Insert Probable Duplicate Records" OnClick="btnProbableInsert_Click" OnClientClick="return ShowPopupProbableInsert_Delete()" />
            <asp:Button ID="btnProbableDuplicateDelete" runat="server" CssClass="PCGLongLongButton"
                Text="Delete Probable Duplicate Records" OnClick="btnProbableDelete_Click" Visible="false"
                OnClientClick="return ShowPopupProbableInsert_Delete()" />
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
<%--<div id="DivPager" runat="server" style="display: none">
    <table style="width: 100%">
        <tr align="center">
            <td>
                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
</div>--%>
<%--<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" Value="WERPCustomerName ASC" />
<asp:HiddenField ID="hdnProcessIdFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnInvNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnFolioFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTransactionTypeFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRejectReasonFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnFileNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSourceTypeFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSchemeNameFilter" runat="server" Visible="false" />--%>
<asp:HiddenField ID="hdnProcessIdFilter" runat="server" Visible="false" />
<%--<asp:Button ID="btnGridSearch" runat="server" Text="" OnClick="btnGridSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
--%>