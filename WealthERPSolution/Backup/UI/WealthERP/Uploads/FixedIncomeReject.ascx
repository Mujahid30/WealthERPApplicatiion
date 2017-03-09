<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FixedIncomeReject.ascx.cs"
    Inherits="WealthERP.Uploads.FixedIncomeReject" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>

<script type="text/javascript">
    function ShowPopup() {
        var form = document.forms[0];
        var transactionId = "";
        var count = 0
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == 'checkbox') {
                if (form.elements[i].checked == true) {
                    count++;
                    hiddenField = form.elements[i].id.replace("chkBxWPTrans", "hdnchkBx");
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
        if (count == 0) {
            alert("Please select one record.")
            return false;
        }
        window.open('Uploads/EquityMapToCustomers.aspx?id=' + transactionId + '', 'mywindow', 'width=550,height=450,scrollbars=yes,location=no')
        return true;
    }
</script>

<script language="javascript" type="text/javascript">

    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        //
        var gvControl = document.getElementById('<%= gvWERPFI.ClientID %>');

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

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Fixed Income Exceptions
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
        <tr id="trDDLRejectReason" runat="server">
            <td id="tdlblRejectReason" runat="server">
                <asp:Label runat="server" class="FieldName" Text="Select reject reason :" ID="lblRejectReason"></asp:Label>
            </td>
            <td id="tdDDLRejectReason" runat="server">
                <asp:DropDownList CssClass="cmbField" ID="ddlRejectReason" runat="server">
                </asp:DropDownList>
            </td>
            <td style="width: 10%">
            </td>
            <td id="tdlblFromDate" runat="server" align="right">
                <asp:Label class="FieldName" ID="lblFromTran" Text="From :" runat="server" />
            </td>
            <td id="tdTxtFromDate" runat="server">
                <telerik:RadDatePicker ID="txtFromFI" CssClass="txtField" runat="server" Culture="English (United States)"
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
                    <asp:RequiredFieldValidator ID="rfvtxtTransactionDate" ControlToValidate="txtFromFI"
                        ErrorMessage="<br />Please select a From Date" CssClass="cvPCG" Display="Dynamic"
                        runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtFromFI" CssClass="cvPCG" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </div>
            </td>
            <td id="tdlblToDate" runat="server">
                <asp:Label ID="lblToTran" Text="To :" CssClass="FieldName" runat="server" />
            </td>
            <td id="tdTxtToDate" runat="server">
                <telerik:RadDatePicker ID="txtToFI" CssClass="txtField" runat="server" Culture="English (United States)"
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToFI"
                        ErrorMessage="<br />Please select a To Date" CssClass="cvPCG" Display="Dynamic"
                        runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtToFI" CssClass="cvPCG" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </div>
                <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtToFI"
                    ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                    ControlToCompare="txtFromFI" CssClass="cvPCG" ValidationGroup="btnViewFI" Display="Dynamic">
                </asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td id="tdBtnViewRejetcs" runat="server">
                <asp:Button ID="btnViewFI" runat="server" CssClass="PCGButton" Text="Go" OnClick="btnViewFI_Click" />
            </td>
        </tr>
    </table>
</div>
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
            <div id="Div2" class="success-msg" align="center" runat="server">
                Reprocess successfully Completed
            </div>
        </td>
    </tr>
    <tr id="msgReprocessincomplete" runat="server" visible="false">
        <td align="center">
            <div id="Div3" runat="server" class="failure-msg" align="center">
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
<asp:Panel ID="Panel2" Visible="false" runat="server" class="Landscape" Width="90%"
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
                <telerik:RadGrid ID="gvWERPFI" runat="server" GridLines="None" AutoGenerateColumns="False"
                    AllowFiltering="true" PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True"
                    Skin="Telerik" OnItemDataBound="gvWERPFI_ItemDataBound" EnableEmbeddedSkins="false"
                    Width="99%" AllowFilteringByColumn="true" AllowAutomaticInserts="false" EnableViewState="true"
                    ExportSettings-FileName="Fixed Income Reject Details" ShowFooter="true" OnNeedDataSource="gvWERPFI_OnNeedDataSource"
                    OnPreRender="gvWERPFI_PreRender">
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView TableLayout="Auto" DataKeyNames="CUS_Id,ADUL_ProcessId" AllowFilteringByColumn="true"
                        Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action"
                                HeaderStyle-Width="65px">
                                <HeaderTemplate>
                                    <input id="chkBxWPTransAll" name="chkIdAll" type="checkbox" onclick="checkAllBoxes()" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkBxWPTrans" runat="server" />
                                    <asp:HiddenField ID="hdnchkBx" runat="server" Value='<%# Eval("CUS_Id").ToString()%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="WRR_RejectReasonDescription" AllowFiltering="true"
                                HeaderText="Reject Reason" HeaderStyle-Width="230px" UniqueName="RejectReasonCode"
                                SortExpression="RejectReasonCode" AutoPostBackOnFilter="false" ShowFilterIcon="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                <FilterTemplate>
                                    <telerik:RadComboBox ID="RadComboBoxRR" Width="230px" CssClass="cmbField" AllowFiltering="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlRejectReason_SelectedIndexChanged"
                                        IsFilteringEnabled="true" AppendDataBoundItems="true" AutoPostBackOnFilter="false"
                                        EnableViewState="true" OnPreRender="rcbContinents1_PreRender" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("RejectReasonCode").CurrentFilterValue %>'
                                        runat="server">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="All" Value="" Selected="false"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

                                        <script type="text/javascript">
                                            function InvesterNameIndexChanged(sender, args) {
                                                var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                                tableView.filter("RejectReasonCode", args.get_item().get_value(), "EqualTo");
                                            } 
                                        </script>

                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CustomerName" HeaderText="Customer" AllowFiltering="true"
                                HeaderStyle-Width="150px" HeaderStyle-Wrap="false" SortExpression="CustomerName"
                                AllowSorting="true" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CustomerName" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CUS_PANno" AllowFiltering="false" HeaderText="Pan Number"
                                AllowSorting="true" HeaderStyle-Width="75px" UniqueName="CUS_PANno" SortExpression="CUS_PANno"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CUS_SubBrokercode" HeaderText="Sub Broker Code"
                                HeaderStyle-Width="85px" AllowSorting="true" AllowFiltering="false" SortExpression="CUS_SubBrokercode"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CUS_SubBrokercode" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ADUL_ProcessId" AllowFiltering="true" HeaderText="ProcessId"
                                AllowSorting="true" HeaderStyle-Width="100px" UniqueName="ADUL_ProcessId" SortExpression="ADUL_ProcessId"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="8px" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CUS_BankSrNo" HeaderText="Account No." AllowSorting="true"
                                HeaderStyle-Width="80px" UniqueName="CUS_BankSrNo" SortExpression="CUS_BankSrNo"
                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%-- <telerik:GridBoundColumn DataField="Exchange" AllowFiltering="true" HeaderText="Exchange"
                                AllowSorting="true" HeaderStyle-Width="85px" UniqueName="Exchange" SortExpression="Exchange"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                            <%--   <telerik:GridBoundColumn DataField="Share" AllowFiltering="false" HeaderText="Share"
                                AllowSorting="true" HeaderStyle-Width="80px" UniqueName="Share" SortExpression="Share"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                DataFormatString="{0:n0}">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                            <%-- <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" DataField="WUXFT_XMLFileName" AllowFiltering="false"
                                HeaderStyle-Width="65px" HeaderText="External Type" UniqueName="WUXFT_XMLFileName" AllowSorting="true">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" DataField="Amount" AllowFiltering="false"
                                HeaderStyle-Width="75px" HeaderText="Amount" UniqueName="Amount" Aggregate="Sum"
                                AllowSorting="true" DataFormatString="{0:N2}">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%-- <telerik:GridBoundColumn DataField="TransactionType" AllowFiltering="true" HeaderText="Transaction Type"
                                HeaderStyle-Width="100px" UniqueName="TransactionTypeCode" SortExpression="TransactionTypeCode"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
<div runat="server" id="DivAction" visible="false">
    <tr id="trReprocess" runat="server">
        <td class="SubmitCell">
            <%-- <asp:Button ID="btnReprocess" OnClientClick="return selectRecordToReprocess();" OnClick="btnReprocess_Click" Visible="false"
                runat="server" Text="Reprocess" CssClass="PCGLongButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_RejectedEquityTransactionStaging_btnReprocess','L');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_RejectedEquityTransactionStaging_btnReprocess','L');" />--%>
            <asp:Button ID="btnMapToCustomer" runat="server" CssClass="PCGLongButton" Text="Map to Customer"
                OnClientClick="return ShowPopup()" Visible="false" />
            <asp:Button ID="btnDelete" runat="server" CssClass="PCGLongButton" Text="Delete Records"
                OnClick="btnDelete_Click" />
            <br />
        </td>
    </tr>
</div>
