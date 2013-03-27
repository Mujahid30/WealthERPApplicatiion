<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RejectedEquityTransactionStaging.ascx.cs"
    Inherits="WealthERP.Uploads.RejectedEquityTransactionStaging" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>

<script type="text/javascript">
    function selectRecordToReprocess() {
        var TargetBaseControl = null;
        var count = 0;
        try {
            //get target base control.
            TargetBaseControl = document.getElementById('<%= this.gvWERPTrans.ClientID %>');
        }
        catch (err) {
            TargetBaseControl = null;
        }
        if (TargetBaseControl == null) return false;

        //get target child control.

        var TargetChildControl = "chkBxWPTrans";

        //get all the control of the type INPUT in the base control.
        var Inputs = TargetBaseControl.getElementsByTagName("input");

        for (var n = 0; n < Inputs.length; ++n)
            if (Inputs[n].type == 'checkbox' &&
            Inputs[n].id.indexOf(TargetChildControl, 0) >= 0 &&
            Inputs[n].checked)
            return true;
 if (count == 0||count > 1) {
        alert('Please select a record to reprocess!');
        return false;
    }
    
</script>

<script type="text/javascript">
    function GetSelectedNames() {
        var TargetBaseControl = null;
        var count = 0;
        try {
            //get target base control.
            TargetBaseControl = document.getElementById('<%= this.gvWERPTrans.ClientID %>');
        }
        catch (err) {
            TargetBaseControl = null;
        }
        if (TargetBaseControl == null) return false;

        //get target child control.

        var TargetChildControl = "chkBxWPTrans";

        //get all the control of the type INPUT in the base control.
        var Inputs = TargetBaseControl.getElementsByTagName("input");

        for (var n = 0; n < Inputs.length; ++n)
            if (Inputs[n].type == 'checkbox' &&
            Inputs[n].id.indexOf(TargetChildControl, 0) >= 0 &&
            Inputs[n].checked) {
            count++;
        }
        if (count == 0 || count > 1) {
            alert('Please select a single processid');
            return false;
        }
    }
</script>

<script type="text/javascript">
    function ShowPopup() {
        //        alert(transactionId);
        var form = document.forms[0];
        var transactionId = "";
        var count = 0
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == 'checkbox') {
                if (form.elements[i].checked == true) {
                    count++;
                    // alert(count);
                    hiddenField = form.elements[i].id.replace("chkBxWPTrans", "hdnchkBx");
                    // alert("hi");
                    hiddenFieldValues = document.getElementById(hiddenField).value;
                    // alert(hiddenFieldValues);
                    var splittedValues = hiddenFieldValues.split("-");
                    // alert(count);
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
        // alert(transactionId);
        window.open('Uploads/EquityMapToCustomers.aspx?id=' + transactionId + '', 'mywindow', 'width=550,height=450,scrollbars=yes,location=no')
        return true;
    }
   
</script>

<%--  // <script type="text/javascript">--%>

<script language="javascript" type="text/javascript">

    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        //      
        var gvControl = document.getElementById('<%= gvWERPTrans.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chkBxWPTrans";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkBxWPTransAll");

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

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Equity Transaction Exceptions
                        </td>
                        <td align="right">
                            &nbsp; &nbsp;
                            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="View UploadLog"
                                OnClick="lnkBtnBack_Click"></asp:LinkButton>
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
<div id="divConditional" runat="server">
    <table class="TableBackground" cellpadding="2">
        <tr>
            <%--  <td id="tdLblAdviser" runat="server" align="right">
            <asp:Label ID="lblAdviser" CssClass="FieldName" runat="server" Text="Please Select Adviser:"></asp:Label>
        </td>
       <td id="tdDdlAdviser" runat="server" align="left">
     <asp:DropDownList ID="ddlAdviser" runat="server" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlAdviser_OnSelectedIndexChanged" ></asp:DropDownList>
     </td>--%>
            <td id="tdlblRejectReason" runat="server">
                <asp:Label runat="server" class="FieldName" Text="Select reject reason :" ID="lblRejectReason"></asp:Label>
            </td>
            <td id="tdDDLRejectReason" runat="server">
                <asp:DropDownList CssClass="cmbField" ID="ddlRejectReason" runat="server">
                </asp:DropDownList>
            </td>
            <td id="tdlblFromDate" runat="server" align="right">
                <asp:Label class="FieldName" ID="lblFromTran" Text="From :" runat="server" />
            </td>
            <td id="tdTxtFromDate" runat="server">
                <telerik:RadDatePicker ID="txtFromTran" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
                <div id="dvTransactionDate" runat="server" class="dvInLine">
                    <span id="Span1" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvtxtTransactionDate" ControlToValidate="txtFromTran"
                        ErrorMessage="<br />Please select a From Date" CssClass="cvPCG" Display="Dynamic"
                        runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtFromTran" CssClass="cvPCG" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </div>
            </td>
            <td id="tdlblToDate" runat="server">
                <asp:Label ID="lblToTran" Text="To :" CssClass="FieldName" runat="server" />
            </td>
            <td id="tdTxtToDate" runat="server">
                <telerik:RadDatePicker ID="txtToTran" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
                <div id="Div1" runat="server" class="dvInLine">
                    <span id="Span2" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToTran"
                        ErrorMessage="<br />Please select a To Date" CssClass="cvPCG" Display="Dynamic"
                        runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtToTran" CssClass="cvPCG" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </div>
                <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtToTran"
                    ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                    ControlToCompare="txtFromTran" CssClass="cvPCG" ValidationGroup="btnSubmit" Display="Dynamic">
                </asp:CompareValidator>
            </td>
            <td id="tdBtnViewRejetcs" runat="server">
                <asp:Button ID="btnViewTran" runat="server" CssClass="PCGButton" Text="Go" OnClick="btnViewTran_Click" />
            </td>
        </tr>
    </table>
</div>
<%--<table style="width: 100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Equity Transaction Staging Rejects"></asp:Label>
        </td>
    </tr>
</table>--%>
<table style="width: 100%">
    <tr>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr id="msgReprocessComplete" runat="server" visible="false">
        <td align="center">
            <div class="success-msg" align="center" runat="server">
                Reprocess successfully Completed
            </div>
        </td>
    </tr>
    <tr id="msgReprocessincomplete" runat="server" visible="false">
        <td align="center">
            <div runat="server" class="failure-msg" align="center">
                Reprocess Failed!
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
            <div id="Msgerror" runat="server" class="failure-msg" align="center" visible="false">
                No Records Found...!
            </div>
        </td>
    </tr>
</table>
<table style="width: 100%" class="TableBackground">
    <tr>
        <td align="right">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
</table>
<asp:Panel ID="Panel2" Visible="false" runat="server" class="Landscape" Width="100%"
    ScrollBars="Horizontal">
    <table width="100%" cellspacing="0" cellpadding="2">
        <tr>
            <td>
                <asp:LinkButton runat="server" ID="lnkViewInputRejects" Text="View Input Rejects"
                    CssClass="LinkButtons" OnClick="lnkViewInputRejects_OnClick"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="gvWERPTrans" runat="server" GridLines="None" AutoGenerateColumns="False"
                    AllowFiltering="true" PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True"
                    Skin="Telerik" OnItemDataBound="gvWERPTrans_ItemDataBound" EnableEmbeddedSkins="false"
                    Width="80%" AllowFilteringByColumn="true" AllowAutomaticInserts="false" EnableViewState="true"
                    ExportSettings-FileName="MF Transaction Reject Details" ShowFooter="true" OnNeedDataSource="gvWERPTrans_OnNeedDataSource"
                    OnPreRender="gvWERPTrans_PreRender">
                    <%-- OnPreRender="gvWERPTrans_PreRender"
                    OnItemDataBound="gvWERPTrans_ItemDataBound"
                    OnNeedDataSource="gvWERPTrans_NeedDataSource"--%>
                    <%--  OnPreRender="gvWERPTrans_PreRender"--%>
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView TableLayout="Auto" DataKeyNames="WERPTransactionId,ProcessId" AllowFilteringByColumn="true"
                        Width="120%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action"
                                HeaderStyle-Width="65px">
                                <HeaderTemplate>
                                    <input id="chkBxWPTransAll" name="chkIdAll" type="checkbox" onclick="checkAllBoxes()" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkBxWPTrans" runat="server" />
                                    <asp:HiddenField ID="hdnchkBx" runat="server" Value='<%# Eval("WERPTransactionId").ToString()%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="RejectReason" AllowFiltering="true" HeaderText="Reject Reason"
                                HeaderStyle-Width="300px" UniqueName="RejectReasonCode" SortExpression="RejectReasonCode"
                                AutoPostBackOnFilter="false" ShowFilterIcon="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                <FilterTemplate>
                                    <telerik:RadComboBox ID="RadComboBoxRR" Width="290px" CssClass="cmbField" AllowFiltering="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlRejectReason_SelectedIndexChanged"
                                        IsFilteringEnabled="true" AppendDataBoundItems="true" AutoPostBackOnFilter="false"
                                        EnableViewState="true" OnPreRender="rcbContinents1_PreRender" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("RejectReasonCode").CurrentFilterValue %>'
                                        runat="server">
                                        <%--OnPreRender="rcbContinents_PreRender"--%>
                                        <Items>
                                            <telerik:RadComboBoxItem Text="All" Value="" Selected="false"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

                                        <script type="text/javascript">
                                            function InvesterNameIndexChanged(sender, args) {
                                                var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                                //////                                                    sender.value = args.get_item().get_value();
                                                tableView.filter("RejectReasonCode", args.get_item().get_value(), "EqualTo");
                                            } 
                                        </script>

                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ProcessId" AllowFiltering="true" HeaderText="ProcessId"
                                AllowSorting="true" HeaderStyle-Width="60px" UniqueName="ProcessId" SortExpression="ProcessId"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="8px" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TradeAccountNumber" HeaderText="Trade Acct.No."
                                AllowSorting="true" HeaderStyle-Width="60px" UniqueName="TradeAccountNumber"
                                SortExpression="TradeAccountNumber" AutoPostBackOnFilter="true" AllowFiltering="false"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ScripCode" AllowFiltering="false" HeaderText="Scrip Code"
                                AllowSorting="true" HeaderStyle-Width="75px" UniqueName="ScripCode" SortExpression="ScripCode"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Exchange" AllowFiltering="true" HeaderText="Exchange"
                                AllowSorting="true" HeaderStyle-Width="85px" UniqueName="Exchange" SortExpression="Exchange"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Share" AllowFiltering="false" HeaderText="Share"
                                AllowSorting="true" HeaderStyle-Width="80px" UniqueName="Share" SortExpression="Share"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                DataFormatString="{0:n0}">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" DataField="Price" AllowFiltering="false"
                                HeaderStyle-Width="65px" HeaderText="Price" UniqueName="Price" Aggregate="Sum"
                                AllowSorting="true" DataFormatString="{0:N2}">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" DataField="Amount" AllowFiltering="false"
                                HeaderStyle-Width="75px" HeaderText="Amount" UniqueName="Amount" Aggregate="Sum"
                                AllowSorting="true" DataFormatString="{0:N2}">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TransactionType" AllowFiltering="true" HeaderText="Transaction Type"
                                HeaderStyle-Width="100px" UniqueName="TransactionTypeCode" SortExpression="TransactionTypeCode"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                <FilterTemplate>
                                    <telerik:RadComboBox ID="RadComboBoxTT" Width="80px" CssClass="cmbField" AllowFiltering="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlTransactionType_SelectedIndexChanged"
                                        IsFilteringEnabled="true" AppendDataBoundItems="true" AutoPostBackOnFilter="false"
                                        EnableViewState="true" OnPreRender="rcbContinents3_PreRender" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("TransactionTypeCode").CurrentFilterValue %>'
                                        runat="server">
                                        <%--OnPreRender="rcbContinents_PreRender"--%>
                                        <Items>
                                            <telerik:RadComboBoxItem Text="All" Value="" Selected="false"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlock3" runat="server">

                                        <script type="text/javascript">
                                            function TransactionIndexChanged(sender, args) {
                                                var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                                //////                                                    sender.value = args.get_item().get_value();
                                                tableView.filter("TransactionTypeCode", args.get_item().get_value(), "EqualTo");
                                            } 
                                        </script>

                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CustomerName" HeaderText="Customer" AllowFiltering="true"
                                HeaderStyle-Width="100px" HeaderStyle-Wrap="false" SortExpression="CustomerName"
                                AllowSorting="true" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CustomerName" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="BrokerCode" HeaderText="Broker Code" HeaderStyle-Width="85px"
                                AllowSorting="true" AllowFiltering="false" SortExpression="BrokerCode" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="BrokerCode"
                                FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="false" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid>
                <%--  <asp:GridView ID="gvWERPTrans" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Width="101%" ShowFooter="true" CssClass="GridViewStyle" DataKeyNames="WERPTransactionId"
                    AllowSorting="true" OnSorting="gvWERPTrans_Sort">
                    <%--OnRowDataBound="gvWERPTrans_RowDataBound"--%>
                <%-- <FooterStyle CssClass="FooterStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>--%>
                <%--- Check Boxes--%>
                <%-- <asp:TemplateField HeaderText="Select">
                            <HeaderTemplate>
                                <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                                <input id="chkBxWPTransAll" name="chkBxWPTransAll" type="checkbox" onclick="checkAllBoxes()" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkBxWPTrans" runat="server" />
                                <asp:HiddenField ID="hdnBxWPTrans" runat="server" Value='<%# Eval("WERPTransactionId").ToString() + "-" +  Eval("RejectReasonCode").ToString()%>' />
                            </ItemTemplate>
                            <FooterTemplate>--%>
                <%--<asp:Button ID="btnEditSelectedWPTrans" CssClass="FieldName" OnClick="btnEditSelectedWPTrans_Click"
                                runat="server" Text="Save" />--%>
                <%-- </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblRejectReason" runat="server" Text="Reject Reason"></asp:Label>
                                <asp:DropDownList ID="ddlRejectReason" AutoPostBack="true" CssClass="cmbLongField"
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
                                <asp:DropDownList ID="ddlProcessId" AutoPostBack="true" CssClass="GridViewCmbField"
                                    runat="server" OnSelectedIndexChanged="ddlProcessId_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblProcessID" runat="server" Text='<%# Eval("ProcessId").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                <%--<asp:BoundField DataField="ProcessId" HeaderText="Process Id" />--%>
                <%-- <asp:TemplateField>
                       <HeaderTemplate>
                            <asp:Label ID="lblPanNumber" runat="server" Text="PAN Number"></asp:Label>
                            <asp:DropDownList ID="ddlPanNumber" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlPanNumber_SelectedIndexChanged">
                            </asp:DropDownList>
                            <!--<asp:TextBox ID="txtPanNumberSearch" runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_RejectedEquityTransactionStaging_btnGridSearch');" /> -->
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="txtPanNumber" runat="server" Text='<%# Bind("PANNum") %>'></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtPanNumberMultiple" CssClass="FieldName" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>--%>
                <%-- <asp:BoundField DataField="TradeAccountNumber" HeaderText="Trade Account Number" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblScripCode" runat="server" Text="Scrip"></asp:Label>--%>
                <%--<asp:TextBox ID="txtScripCodeSearch" Text='<%# hdnScripFilter.Value %>' runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_RejectedEquityTransactionStaging_btnGridSearch');" />--%>
                <%-- </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="txtScripCode" runat="server" Text='<%# Bind("ScripCode") %>' EnableViewState="true"></asp:Label>
                            </ItemTemplate>--%>
                <%--  <FooterTemplate>
                            <asp:TextBox ID="txtScripCodeMultiple" CssClass="FieldName" runat="server" />
                        </FooterTemplate>--%>
                <%--</asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="20">
                            <HeaderTemplate>
                                <asp:Label ID="lblExchange" runat="server" Text="Exchange"></asp:Label>
                                <asp:TextBox ID="txtExchangeSearch" Width="40" runat="server" Text='<%# hdnExchangeFilter.Value %>'
                                    CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_RejectedEquityTransactionStaging_btnGridSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="txtExchange" Width="20" runat="server" Text='<%# Bind("Exchange") %>'></asp:Label>
                            </ItemTemplate>--%>
                <%-- <FooterTemplate>
                            <asp:TextBox ID="txtExchangeMultiple" CssClass="FieldName" runat="server" />
                        </FooterTemplate>--%>
                <%-- </asp:TemplateField>
                        <asp:BoundField DataField="Share" HeaderText="Share" DataFormatString="{0:f4}" />--%>
                <%--<asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:f4}"/>--%>
                <%--  <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblPrice" runat="server" Text="Price"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="txtPrice" runat="server" Text='<%# Eval("Price","{0:f4}")  %>' Style="text-align: right"></asp:Label>
                            </ItemTemplate>--%>
                <%--  <FooterTemplate>
                            <asp:TextBox ID="txtPriceMultiple" CssClass="FieldName" runat="server" />
                        </FooterTemplate>--%>
                <%-- </asp:TemplateField>
                        <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:f4}"
                            ItemStyle-HorizontalAlign="Right" />
                        <asp:TemplateField HeaderStyle-Width="40">
                            <HeaderTemplate>
                                <asp:Label ID="lblTransactionType" runat="server" Text="Transaction Type"></asp:Label>--%>
                <%--<asp:TextBox ID="txtTransactionTypeSearch"   Text='<%# hdnTransactionTypeFilter.Value %>'   runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_RejectedEquityTransactionStaging_btnGridSearch');" />--%>
                <%--   <asp:DropDownList ID="ddlTransactionType" Width="60" runat="server" AutoPostBack="true"
                                    runat="server" CssClass="cmbLongField" OnSelectedIndexChanged="ddlTransactionType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="txtTransactionType" Width="40" runat="server" Text='<%# Bind("TransactionType") %>'></asp:Label>--%>
                <%--<asp:HiddenField ID="hdnTransactionType" runat="server" Value='<%# Bind("TransactionTypeCode") %>' />
                            <asp:DropDownList ID="ddlTransactionType" runat="server">--%>
                <%--     </asp:DropDownList>
                            </ItemTemplate>--%>
                <%--  <FooterTemplate>
                            <asp:DropDownList ID="ddlTransactionType" runat="server">
                            </asp:DropDownList>
                        </FooterTemplate>--%>
                <%--   </asp:TemplateField>
                        <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                        <asp:BoundField DataField="BrokerCode" HeaderText="Broker Code" />
                    </Columns>
                </asp:GridView>--%>
            </td>
        </tr>
    </table>
    </asp:Panel>
    <div runat="server" id="DivAction" visible="false">
        <tr id="trReprocess" runat="server">
            <td class="SubmitCell">
                <asp:Button ID="btnReprocess" OnClientClick="return selectRecordToReprocess();" OnClick="btnReprocess_Click"
                    runat="server" Text="Reprocess" CssClass="PCGLongButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_RejectedEquityTransactionStaging_btnReprocess','L');"
                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_RejectedEquityTransactionStaging_btnReprocess','L');" />
                <asp:Button ID="btnMapToCustomer" runat="server" CssClass="PCGLongButton" Text="Map to Customer"
                    OnClientClick="return ShowPopup()" />
                <asp:Button ID="btnDelete" runat="server" CssClass="PCGLongButton" Text="Delete Records"
                    OnClick="btnDelete_Click" />
                <asp:Button ID="btnAddLob" runat="server" CssClass="PCGLongButton" Visible="false"
                    OnClick="btnAddLob_Click" Text="Add Broker Code" OnClientClick="return  GetSelectedNames();" />
                <br />
            </td>
        </tr>
    </div>
    <%--<tr id="trMessage" runat="server" visible="false">
    <td class="Message">
        <label id="lblEmptyMsg" class="FieldName">
            There are no records to be displayed!</label>
    </td>
</tr>--%>
    <tr id="trErrorMessage" runat="server" visible="false">
        <td class="Message">
            <asp:Label ID="lblError" CssClass="Message" runat="server">
            </asp:Label>
        </td>
    </tr>
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
    <asp:HiddenField ID="hdnPanNumberFilter" runat="server" Visible="false" />
    <asp:HiddenField ID="hdnTradeAccountNumberFilter" runat="server" Visible="false" />
    <asp:HiddenField ID="hdnScripFilter" runat="server" Visible="false" />
    <asp:HiddenField ID="hdnExchangeFilter" runat="server" Visible="false" />
    <%--<asp:HiddenField ID="hdnShareFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAmountFilter" runat="server" Visible="false" />--%>
    <asp:HiddenField ID="hdnTransactionTypeFilter" runat="server" Visible="false" />
    <asp:HiddenField ID="hdnRejectReasonFilter" runat="server" Visible="false" />
    <asp:HiddenField ID="hdnProcessIdFilter" runat="server" Visible="false" />
    <asp:HiddenField ID="hfRmId" runat="server" />
    <%-- <asp:HiddenField ID="hdnIsRejectedFilter" runat="server" Visible="false" />
--%>
    <asp:Button ID="btnGridSearch" runat="server" Text="" OnClick="btnGridSearch_Click"
        BorderStyle="None" BackColor="Transparent" />
