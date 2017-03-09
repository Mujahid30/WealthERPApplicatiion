<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TrailCommisionTransactionRejects.ascx.cs"
    Inherits="WealthERP.Uploads.TrailCommisionTransactionRejects" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause

        var gvControl = document.getElementById('<%= GVTrailTransactionRejects.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "ChkOne";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("ChkALL");

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
    function ShowPopup() {
        var form = document.forms[0];
        var folioId = "";
        var count = 0
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == 'checkbox') {
                if (form.elements[i].checked == true) {
                    count++;
                    hiddenField = form.elements[i].id.replace("ChkOne", "hdnchkBx");
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
        window.open('Uploads/MapToCustomers.aspx?TrailFolioid=' + folioId + '', '_blank', 'width=550,height=450,scrollbars=yes,location=no')
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
                            Trail Commission Exceptions
                        </td>
                        <td align="right">
                            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="View UploadLog"
                                OnClick="lnkBtnBack_Click"></asp:LinkButton>
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton ID="imgBtnrgHoldings" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                Visible="false" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                OnClick="btnExportFilteredData_OnClick" OnClientClick="setFormat('excel')" Height="22px"
                                Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<div id="divConditional" runat="server">
    <table class="TableBackground" cellspacing="0" cellpadding="2">
        <tr>
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
                <telerik:RadDatePicker ID="txtFromMFT" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput ID="DateInput1" EmptyMessage="dd/mm/yyyy" runat="server" DisplayDateFormat="d/M/yyyy"
                        DateFormat="d/M/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
                <div id="dvTransactionDate" runat="server" class="dvInLine">
                    <span id="Span1" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvtxtSIPDate" ControlToValidate="txtFromMFT" ErrorMessage="<br />Please select a From Date"
                        CssClass="cvPCG" Display="Dynamic" runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtFromMFT" CssClass="cvPCG" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </div>
            </td>
            <td id="tdlblToDate" runat="server">
                <asp:Label ID="lblToTran" Text="To :" CssClass="FieldName" runat="server" />
            </td>
            <td id="tdTxtToDate" runat="server">
                <telerik:RadDatePicker ID="txtToMFT" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput ID="DateInput2" runat="server" EmptyMessage="dd/mm/yyyy" DisplayDateFormat="d/M/yyyy"
                        DateFormat="d/M/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
                <div id="Div1" runat="server" class="dvInLine">
                    <span id="Span2" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToMFT"
                        ErrorMessage="<br />Please select a To Date" CssClass="cvPCG" Display="Dynamic"
                        runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtToMFT" CssClass="cvPCG" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </div>
                <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtToMFT"
                    ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                    ControlToCompare="txtFromMFT" CssClass="cvPCG" ValidationGroup="btnSubmit" Display="Dynamic">
                </asp:CompareValidator>
            </td>
            <td id="tdBtnViewRejetcs" runat="server">
                <asp:Button ID="btnViewTrail" runat="server" CssClass="PCGButton" Text="Go" OnClick="btnViewTrail_Click" />
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
            <div id="Msgerror" runat="server" class="success-msg" align="center" visible="false">
                No Records Found...!
            </div>
        </td>
    </tr>
</table>
<asp:Panel ID="Panel2" Visible="false" runat="server" class="Landscape" Width="100%"
    ScrollBars="Horizontal">
    <table>
        <tr>
            <td>
                <%--<telerik:RadGrid ID="GVTrailTransactionRejects" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    AllowFilteringByColumn="true" Skin="Telerik" EnableEmbeddedSkins="false" AllowAutomaticInserts="false"
                    OnNeedDataSource="GVTrailTransactionRejects_NeedDataSource" OnItemDataBound="GVTrailTransactionRejects_ItemDataBound"
                    OnPreRender="GVTrailTransactionRejects_PreRender">
                    <ExportSettings ExportOnlyData="true" FileName="Trail Commission Reject Details">
                    </ExportSettings>
                    <MasterTableView AllowFilteringByColumn="true" DataKeyNames="CMFTCCS_Id,A_AdviserId,Adul_ProcessId"
                        Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                                  <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action"
                                HeaderStyle-Width="50px">
                                <HeaderTemplate>
                                    <input id="ChkALL" name="ChkALL" type="checkbox" onclick="checkAllBoxes()" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="ChkOne" />
                                    <asp:HiddenField ID="hdnchkBx" runat="server" Value='<%# Eval("CMFTCCS_Id").ToString() + "-" +  Eval("RejectReasonCode").ToString()%>' />
                                    <asp:HiddenField ID="hdnBxProcessID" runat="server" Value='<%# Eval("Adul_ProcessId").ToString() %>' />
                                    <asp:HiddenField ID="hdnBxStagingId" runat="server" Value='<%# Eval("CMFTCCS_Id").ToString() %>' />
                                 </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="WRR_RejectReasonDescription" AllowFiltering="true"
                                HeaderText="Reject Reason" UniqueName="RejectReasonCode" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                <FilterTemplate>
                                    <telerik:RadComboBox ID="RadComboBoxRR" Width="210px" CssClass="cmbField" AllowFiltering="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxRR_SelectedIndexChanged"
                                        IsFilteringEnabled="true" AppendDataBoundItems="true" AutoPostBackOnFilter="false"
                                        OnPreRender="rcbContinents1_PreRender" EnableViewState="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("RejectReasonCode").CurrentFilterValue %>'
                                        runat="server">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="All" Value="" Selected="false"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

                                        <script type="text/javascript">
                                            function RejectReasonIndexChanged(sender, args) {
                                                alert(tableView);
                                                var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                                tableView.filter("RejectReasonCode", args.get_item().get_value(), "EqualTo");
                                            } 
                                        </script>

                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Adul_ProcessId" AllowFiltering="true" HeaderText="Process Id"
                                UniqueName="ProcessId" SortExpression="Adul_ProcessId" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="ADUL_FileName" HeaderText="File Name"
                                FilterControlWidth="150px" UniqueName="ADUL_FileName" SortExpression="ADUL_FileName"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFA_FolioNum" HeaderText="Folio Number"
                                UniqueName="CMFA_FolioNum" SortExpression="CMFA_FolioNum" AutoPostBackOnFilter="true"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_InvName" HeaderText="InvName"
                                UniqueName="CMFTTCS_InvName" SortExpression="CMFTTCS_InvName" AutoPostBackOnFilter="true"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PASP_SchemePlanName" HeaderText="Scheme Plan Name"
                                FilterControlWidth="220px" UniqueName="PASP_SchemePlanName" SortExpression="PASP_SchemePlanName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_FROMDate" HeaderText="From Date"
                                UniqueName="FromDate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_TODate" HeaderText="To Date"
                                UniqueName="ToDate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_PRDate" HeaderText="Purchase Date"
                                UniqueName="purchasedate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_TransactionType"
                                HeaderText="Transaction Type" UniqueName="transactiontype" SortExpression="ADUL_FileName"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_Units" HeaderText="Units"
                                UniqueName="units" DataFormatString="{0:N2}">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_Amount" HeaderText="Amount"
                                UniqueName="amount" DataFormatString="{0:N2}">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_SubBroker" HeaderText="Sub Broker"
                                UniqueName="subbroker">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <ClientEvents OnRowClick="onRowClick" />
                    </ClientSettings>
                </telerik:RadGrid>--%>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="GVTrailTransactionRejects" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                    AllowAutomaticInserts="false" ExportSettings-FileName="TrailTransactionRejects DETAILS"
                    OnNeedDataSource="GVTrailTransactionRejects_NeedDataSource" OnItemDataBound="GVTrailTransactionRejects_ItemDataBound"
                    OnPreRender="GVTrailTransactionRejects_PreRender">
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="CMFTCCS_Id,A_AdviserId,Adul_ProcessId" Width="100%"
                        AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                                  <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action"
                                HeaderStyle-Width="50px">
                                <HeaderTemplate>
                                    <input id="ChkALL" name="ChkALL" type="checkbox" onclick="checkAllBoxes()" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="ChkOne" />
                                    <asp:HiddenField ID="hdnchkBx" runat="server" Value='<%# Eval("CMFTCCS_Id").ToString() + "-" +  Eval("RejectReasonCode").ToString()%>' />
                                    <asp:HiddenField ID="hdnBxProcessID" runat="server" Value='<%# Eval("Adul_ProcessId").ToString() %>' />
                                    <asp:HiddenField ID="hdnBxStagingId" runat="server" Value='<%# Eval("CMFTCCS_Id").ToString() %>' />
                                 </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="WRR_RejectReasonDescription" AllowFiltering="true"
                                HeaderText="Reject Reason" UniqueName="RejectReasonCode" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                <FilterTemplate>
                                    <telerik:RadComboBox ID="RadComboBoxRR" Width="210px" CssClass="cmbField" AllowFiltering="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxRR_SelectedIndexChanged"
                                        IsFilteringEnabled="true" AppendDataBoundItems="true" AutoPostBackOnFilter="false"
                                        OnPreRender="rcbContinents1_PreRender" EnableViewState="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("RejectReasonCode").CurrentFilterValue %>'
                                        runat="server">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="All" Value="" Selected="false"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

                                        <script type="text/javascript">
                                            function RejectReasonIndexChanged(sender, args) {
                                                alert(tableView);
                                                var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                                tableView.filter("RejectReasonCode", args.get_item().get_value(), "EqualTo");
                                            } 
                                        </script>

                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Adul_ProcessId" AllowFiltering="true" HeaderText="Process Id"
                                UniqueName="ProcessId" SortExpression="Adul_ProcessId" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="ADUL_FileName" HeaderText="File Name"
                                FilterControlWidth="150px" UniqueName="ADUL_FileName" SortExpression="ADUL_FileName"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFA_FolioNum" HeaderText="Folio Number"
                                UniqueName="CMFA_FolioNum" SortExpression="CMFA_FolioNum" AutoPostBackOnFilter="true"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_InvName" HeaderText="InvName"
                                UniqueName="CMFTTCS_InvName" SortExpression="CMFTTCS_InvName" AutoPostBackOnFilter="true"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PASP_SchemePlanName" HeaderText="Scheme Plan Name"
                                FilterControlWidth="220px" UniqueName="PASP_SchemePlanName" SortExpression="PASP_SchemePlanName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_FROMDate" HeaderText="From Date"
                                UniqueName="FromDate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_TODate" HeaderText="To Date"
                                UniqueName="ToDate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_PRDate" HeaderText="Purchase Date"
                                UniqueName="purchasedate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_TransactionType"
                                HeaderText="Transaction Type" UniqueName="transactiontype" SortExpression="ADUL_FileName"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_Units" HeaderText="Units"
                                UniqueName="units" DataFormatString="{0:N2}">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_Amount" HeaderText="Amount"
                                UniqueName="amount" DataFormatString="{0:N2}">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_SubBroker" HeaderText="Sub Broker"
                                UniqueName="subbroker">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
<table width="100%">
    <tr id="trReprocess" visible="false" runat="server">
        <td class="SubmitCell">
            <asp:Button ID="btnReprocess" runat="server" Text="Reprocess" CssClass="PCGLongButton"
                OnClick="btnReprocess_Click" OnClientClick="Loading(true);" />
            <asp:Button ID="btnDelete" runat="server" CssClass="PCGLongButton" Text="Delete Records"
                OnClick="btnDelete_Click" />
            <asp:Button ID="btnMapToCustomer" runat="server" CssClass="PCGLongButton" Text="Map to Customer"
                OnClientClick="return ShowPopup()" />
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
<asp:HiddenField ID="hfRmId" runat="server" />
