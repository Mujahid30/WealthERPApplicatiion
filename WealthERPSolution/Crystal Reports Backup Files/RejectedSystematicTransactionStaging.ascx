<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RejectedSystematicTransactionStaging.ascx.cs"
    Inherits="WealthERP.Uploads.RejectedSystematicTransactionStaging" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<script src="Scripts/jquery-1.2.6.js" type="text/javascript"></script>

<script src="Scripts/webtoolkit.jscrollable.js" type="text/javascript"></script>

<script src="Scripts/webtoolkit.scrollabletable.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<script language="javascript" type="text/javascript">
    function ShowPopup() {
        var form = document.forms[0];
        var folioId = "";
        var count = 0
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == 'checkbox') {
                if (form.elements[i].checked == true) {
                    count++;
                    hiddenField = form.elements[i].id.replace("chkId", "hdnchkBx");
                    hiddenFieldValues = document.getElementById(hiddenField).value;
                    var splittedValues = hiddenFieldValues.split("-");
                    if (count == 1) {
                        folioId = splittedValues[0];
                    }
                    else {
                        folioId = folioId + "~" + splittedValues[0];
                    }
                    RejectReasonCode = splittedValues[1];
                }
            }
        }
        if (count == 0) {
            alert("Please select one record.")
            return false;
        }
        window.open('Uploads/MapToCustomers.aspx?SIPFolioid=' + folioId + '', '_blank', 'width=550,height=450,scrollbars=yes,location=no')
        return false;
    }
</script>

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            Systematic Transaction Exceptions
                        </td>
                        <td align="right">
                            <asp:LinkButton runat="server" ID="LinkButton1" CssClass="LinkButtons" Text="View UploadLog"
                                OnClick="lnkBtnBackToUploadLog_Click"></asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkInputRejects" runat="server" Text="View Input Rejects" Visible="false"
                                CssClass="LinkButtons" OnClick="LinkInputRejects_Click"></asp:LinkButton>
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton Visible="false" ID="btnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<%--<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="Systematic Transaction Staging"></asp:Label>--%>

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

<script type="text/javascript">
    function SelectProcessId() {
        var TargetBaseControl = null;
        var count = 0;
        try {
            //get target base control.
            TargetBaseControl = document.getElementById('<%= this.gvSIPReject.ClientID %>');
        }
        catch (err) {
            TargetBaseControl = null;
        }
        if (TargetBaseControl == null) return false;

        //get target child control.

        var TargetChildControl = "chkId";

        //get all the control of the type INPUT in the base control.
        var Inputs = TargetBaseControl.getElementsByTagName("input");

        for (var n = 0; n < Inputs.length; ++n)
            if (Inputs[n].type == 'checkbox' &&
            Inputs[n].id.indexOf(TargetChildControl, 0) >= 0 &&
            Inputs[n].checked) {
            count++;
        }
        if (count == 0) {
            alert('Please select a record to reprocess!');
            return false;
        }
    }
</script>

<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause

        var gvControl = document.getElementById('<%= gvSIPReject.ClientID %>');

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

<script language="javascript" type="text/javascript">
    //    Function to call btnReprocess_Click method to refresh user control
    function Reprocess() {
        document.getElementById('<%= btnReprocess.ClientID %>').click();
    }
</script>

<table id="tableAdviserSelection" runat="server">
</table>
<div id="divConditional" runat="server">
    <table class="TableBackground" cellspacing="0" cellpadding="2">
        <tr>
            <%--  <td id="tdLblAdviser" runat="server" align="right">
            <asp:Label ID="lblAdviser" CssClass="FieldName" runat="server" Text="Please Select Adviser:"></asp:Label>
        </td>
       <td id="tdDdlAdviser" runat="server" align="left">
     <asp:DropDownList ID="ddlAdviser" runat="server" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlAdviser_OnSelectedIndexChanged" ></asp:DropDownList>
     </td>--%>
            <td id="tdlblRejectReason" runat="server" align="right">
                <asp:Label runat="server" class="FieldName" Text="Select reject reason :" ID="lblRejectReason"></asp:Label>
            </td>
            <td id="tdDDLRejectReason" runat="server">
                <asp:DropDownList CssClass="cmbField" ID="ddlRejectReason" runat="server">
                </asp:DropDownList>
            </td>
            <td id="tdlblFromDate" runat="server" align="right">
                <asp:Label class="FieldName" ID="lblFromSIP" Text="From :" runat="server" />
            </td>
            <td id="tdTxtFromDate" runat="server">
                <telerik:RadDatePicker ID="txtFromSIP" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <calendar id="Calendar1" runat="server" userowheadersasselectors="False" usecolumnheadersasselectors="False"
                        viewselectortext="x" skin="Telerik" enableembeddedskins="false">
                    </calendar>
                    <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                    <dateinput id="DateInput1" emptymessage="dd/mm/yyyy" runat="server" displaydateformat="d/M/yyyy"
                        dateformat="d/M/yyyy">
                    </dateinput>
                </telerik:RadDatePicker>
                <div id="dvTransactionDate" runat="server" class="dvInLine">
                    <span id="Span1" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvtxtSIPDate" ControlToValidate="txtFromSIP" ErrorMessage="<br />Please select a From Date"
                        CssClass="cvPCG" Display="Dynamic" runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtFromSIP" CssClass="cvPCG" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </div>
            </td>
            <td id="tdlblToDate" runat="server">
                <asp:Label ID="lblToTran" Text="To :" CssClass="FieldName" runat="server" />
            </td>
            <td id="tdTxtToDate" runat="server">
                <telerik:RadDatePicker ID="txtToSIP" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <calendar id="Calendar2" runat="server" userowheadersasselectors="False" usecolumnheadersasselectors="False"
                        viewselectortext="x" skin="Telerik" enableembeddedskins="false">
                    </calendar>
                    <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                    <dateinput id="DateInput2" runat="server" emptymessage="dd/mm/yyyy" displaydateformat="d/M/yyyy"
                        dateformat="d/M/yyyy">
                    </dateinput>
                </telerik:RadDatePicker>
                <div id="Div1" runat="server" class="dvInLine">
                    <span id="Span2" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToSIP"
                        ErrorMessage="<br />Please select a To Date" CssClass="cvPCG" Display="Dynamic"
                        runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtToSIP" CssClass="cvPCG" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </div>
                <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtToSIP"
                    ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                    ControlToCompare="txtFromSIP" CssClass="cvPCG" ValidationGroup="btnSubmit" Display="Dynamic">
                </asp:CompareValidator>
            </td>
            <td id="tdBtnViewRejetcs" runat="server">
                <asp:Button ID="btnViewSIP" runat="server" CssClass="PCGButton" Text="Go" OnClick="btnViewSIP_Click" />
            </td>
        </tr>
        <tr id="trAdviserSelection" runat="server">
            <td align="right" style="width: 20%">
                <asp:Label ID="lblAdviser" CssClass="FieldName" runat="server" Text="Please Select Adviser:"></asp:Label>
            </td>
            <td id="tdDdlAdviser" runat="server" align="left">
                <asp:DropDownList ID="ddlAdviser" runat="server" CssClass="cmbField" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlAdviser_OnSelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td colspan="5">
            </td>
        </tr>
    </table>
</div>
<table width="100%" cellspacing="" cellpadding="2">
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
            <div id="msgDelete" runat="server" class="success-msg" align="center" visible="false">
                Records has been deleted successfully
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
<table width="100%">
    <tr>
        <td align="center">
            <div id="Msgerror" runat="server" class="failure-msg" align="center" visible="false">
                No Records Found...!
            </div>
        </td>
    </tr>
</table>
<asp:Panel ID="Panel3" Visible="false" runat="server" class="Landscape" Width="100%"
    ScrollBars="Horizontal">
    <table width="100%" cellspacing="0" cellpadding="2">
        <tr>
            <td>
                <asp:LinkButton runat="server" ID="lnkViewInputRejects" Text="View Input Rejects"
                    CssClass="LinkButtons"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="gvSIPReject" runat="server" GridLines="None" AutoGenerateColumns="False"
                    AllowFiltering="true" PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True"
                    Skin="Telerik" OnItemDataBound="gvSIPReject_ItemDataBound" EnableEmbeddedSkins="false"
                    Width="80%" AllowFilteringByColumn="true" AllowAutomaticInserts="false" EnableViewState="true"
                    ShowFooter="true" OnNeedDataSource="gvSIPReject_NeedDataSource" OnPreRender="gvSIPReject_PreRender">
                    <exportsettings hidestructurecolumns="true">
                    </exportsettings>
                    <mastertableview tablelayout="Auto" datakeynames="CMFSCS_ID,WUPL_ProcessId,CMFSCS_FolioNum"
                        allowfilteringbycolumn="true" width="100%" allowmulticolumnsorting="True" autogeneratecolumns="false"
                        commanditemdisplay="None">
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action"
                                HeaderStyle-Width="50px">
                                <HeaderTemplate>
                                    <input id="chkIdAll" name="chkIdAll" type="checkbox" onclick="checkAllBoxes()" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkId" runat="server" />
                                    <asp:HiddenField ID="hdnchkBx" runat="server" Value='<%# Eval("CMFSCS_ID").ToString() + "-" +  Eval("WRR_RejectReasonCode").ToString()%>' />
                                    <asp:HiddenField ID="hdnBxProcessID" runat="server" Value='<%# Eval("WUPL_ProcessId").ToString() %>' />
                                    <asp:HiddenField ID="hdnBxStagingId" runat="server" Value='<%# Eval("CMFSCS_ID").ToString() %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="WRR_RejectReasonDescription" AllowFiltering="true"
                                HeaderText="Reject Reason" HeaderStyle-Width="300px" UniqueName="WRR_RejectReasonCode"
                                SortExpression="WRR_RejectReasonCode" AutoPostBackOnFilter="false" ShowFilterIcon="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                <FilterTemplate>
                                    <telerik:RadComboBox ID="RadComboBoxRR" Width="290px" CssClass="cmbField" AllowFiltering="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlRejectReason_SelectedIndexChanged"
                                        IsFilteringEnabled="true" AppendDataBoundItems="true" AutoPostBackOnFilter="false"
                                        EnableViewState="true" OnPreRender="rcbContinents1_PreRender" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("WRR_RejectReasonCode").CurrentFilterValue %>'
                                        runat="server">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="All" Value="" Selected="false"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

                                        <script type="text/javascript">
                                            function InvesterNameIndexChanged(sender, args) {
                                                var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");

                                                tableView.filter("WRR_RejectReasonCode", args.get_item().get_value(), "EqualTo");
                                            } 
                                        </script>

                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="ProcessId" HeaderStyle-Width="60px" DataField="WUPL_ProcessId"
                                UniqueName="ProcessId" SortExpression="WUPL_ProcessId" AutoPostBackOnFilter="true"
                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ADUL_FileName" HeaderText="File Name" HeaderStyle-Width="60px"
                                UniqueName="ADUL_FileName" SortExpression="ADUL_FileName" AutoPostBackOnFilter="true"
                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SystematicType" AllowFiltering="false" HeaderText="Source Type"
                                HeaderStyle-Width="75px" UniqueName="SystematicType" SortExpression="SystematicType"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSCS_InvName" AllowFiltering="true" HeaderText="Investor Name"
                                HeaderStyle-Width="85px" UniqueName="CMFSCS_InvName" SortExpression="CMFSCS_InvName"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSCS_FolioNum" HeaderText="Folio No." AllowFiltering="true"
                                HeaderStyle-Width="80px" UniqueName="CMFSCS_FolioNum" SortExpression="CMFSCS_FolioNum"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSCS_SchemeName" ShowFilterIcon="false" FilterControlWidth="250px"
                                AllowFiltering="true" HeaderStyle-Width="250px" HeaderText="Scheme" UniqueName="CMFSCS_SchemeName"
                                CurrentFilterFunction="Contains" SortExpression="CMFSCS_SchemeName" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSCS_SystematicCode" AllowFiltering="true"
                                HeaderText="Transaction Type" HeaderStyle-Width="100px" UniqueName="CMFSCS_SystematicCode"
                                SortExpression="CMFSCS_SystematicCode" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                <FilterTemplate>
                                    <telerik:RadComboBox ID="RadComboBoxTT" Width="80px" CssClass="cmbField" AllowFiltering="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlTransactionType_SelectedIndexChanged"
                                        IsFilteringEnabled="true" AppendDataBoundItems="true" AutoPostBackOnFilter="false"
                                        EnableViewState="true" OnPreRender="rcbContinents7_PreRender" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("CMFSCS_SystematicCode").CurrentFilterValue %>'
                                        runat="server">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="All" Value="" Selected="false"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlock7" runat="server">

                                        <script type="text/javascript">
                                            function TransactionIndexChanged(sender, args) {
                                                var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                                tableView.filter("CMFSCS_SystematicCode", args.get_item().get_value(), "EqualTo");
                                            } 
                                        </script>

                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSCS_StartDate" HeaderText="Start Date" AllowFiltering="false"
                                HeaderStyle-Width="100px" HeaderStyle-Wrap="false" SortExpression="CMFSCS_StartDate"
                                DataFormatString="{0:d}" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="CMFSCS_StartDate" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSCS_ToDate" HeaderText="End Date" AllowFiltering="false"
                                HeaderStyle-Width="100px" HeaderStyle-Wrap="false" SortExpression="CMFSCS_ToDate"
                                DataFormatString="{0:d}" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="CMFSCS_ToDate" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSCS_SystematicDate" HeaderText="Systematic Date"
                                AllowFiltering="false" HeaderStyle-Width="100px" HeaderStyle-Wrap="false" SortExpression="CMFSCS_SystematicDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CMFSCS_SystematicDate" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XF_Frequency" HeaderText="Frequency" AllowFiltering="false"
                                HeaderStyle-Width="100px" HeaderStyle-Wrap="false" SortExpression="XF_Frequency"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="XF_Frequency" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" DataField="CMFSCS_Amount"
                                AllowFiltering="false" HeaderStyle-Width="75px" HeaderText="Amount" UniqueName="CMFSCS_Amount"
                                Aggregate="Sum" DataFormatString="{0:N2}">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFSCS_TARGET_SCH" HeaderText="Target Scheme"
                                AllowFiltering="false" HeaderStyle-Width="100px" HeaderStyle-Wrap="false" SortExpression="CMFSCS_TARGET_SCH"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CMFSCS_TARGET_SCH" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </mastertableview>
                    <clientsettings>
                        <Resizing AllowColumnResize="false" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </clientsettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
<table width="100%">
    <div id="DivAction" runat="server" visible="false">
        <tr id="trReprocess" runat="server">
            <td class="SubmitCell">
                <asp:Button ID="btnReprocess" runat="server" Text="Reprocess" OnClick="btnReprocess_Click"
                    CssClass="PCGLongButton" OnClientClick="SelectProcessId();" />
                <asp:Button ID="btnDelete" runat="server" CssClass="PCGLongButton" Text="Delete Records"
                    OnClick="btnDelete_Click" />
                <asp:Button ID="btnMapToCustomer" runat="server" CssClass="PCGLongButton" Text="Map to Customer"
                    OnClientClick="return ShowPopup()" />
            </td>
        </tr>
    </div>
</table>
<asp:HiddenField ID="hdnProcessIdFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnFolioFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnInvNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSchemeNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnTransactionTypeFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnFileNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnRejectReasonFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hfRmId" runat="server" />
