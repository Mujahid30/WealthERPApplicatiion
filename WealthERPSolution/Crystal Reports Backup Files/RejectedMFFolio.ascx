<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RejectedMFFolio.ascx.cs"
    Inherits="WealthERP.Uploads.RejectedMFFolio" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<asp:ScriptManager runat="server">
</asp:ScriptManager>

<script type="text/javascript">
    function GridCreated(sender, args) {
        var scrollArea = sender.GridDataDiv;
        var dataHeight = sender.get_masterTableView().get_element().clientHeight;
        if (dataHeight < 420) {
            scrollArea.style.height = dataHeight + 17 + "px";
        }
    }
    function GetSelectedNames() {
        var TargetBaseControl = null;
        var count = 0;
        try {
            //get target base control.
            TargetBaseControl = document.getElementById('<%= this.gvCAMSProfileReject.ClientID %>');
        }
        catch (err) {
            TargetBaseControl = null;
        }
        if (TargetBaseControl == null) return false;

        //get target child control.

        var TargetChildControl = "chkBx";

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

<script language="javascript" type="text/javascript">
    function checkAllFPBoxes() {
        var totalChkBoxes = parseInt('<%= gvCAMSProfileReject.Items.Count %>');
        var gvAssociation = document.getElementById('<%= gvCAMSProfileReject.ClientID %>');
        var gvChkBoxControl = "chkBx";
        var mainChkBox = document.getElementById("chkBxAll");
        if (mainChkBox.checked == false) {
            mainChkBox.checked = true;
        }
        else {
            mainChkBox.checked = false;
        }
        var inputTypes = gvAssociation.getElementsByTagName("input");
        for (var i = 0; i < inputTypes.length; i++) {
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    } 
</script>

<script>
    function ShowPopup() {
        var form = document.forms[0];
        var folioId = "";
        var count = 0
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == 'checkbox') {
                if (form.elements[i].checked == true) {
                    count++;
                    hiddenField = form.elements[i].id.replace("chkBx", "hdnchkBx");
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
        window.open('Uploads/MapToCustomers.aspx?Folioid=' + folioId + '', '_blank', 'width=550,height=450,scrollbars=yes,location=no')
        return false;
    }
</script>

<script language="javascript" type="text/javascript">
    function checkAllBoxes() {
        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var gvControl = document.getElementById('<%= gvCAMSProfileReject.ClientID %>');
        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chkBx";
        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkBxAll");
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

<style id="Style1" type="text/css" runat="server">
    .rgDataDiv
    {
        height: auto;
        width: 101.5% !important;
        margin-right: 15%;
    }
</style>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Mutual Fund Folio Exceptions
                        </td>
                        <td align="right">
                            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="View Upload Log"
                                OnClick="lnkBtnBack_Click"></asp:LinkButton>
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton Visible="false" ID="imgBtnrgHoldings" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
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
            <td id="tdLblAdviser" runat="server" align="right">
                <asp:Label ID="lblAdviser" CssClass="FieldName" runat="server" Text="Please Select Adviser:"></asp:Label>
            </td>
            <td id="tdDdlAdviser" runat="server" align="left">
                <asp:DropDownList ID="ddlAdviser" runat="server" CssClass="cmbField" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlAdviser_OnSelectedIndexChanged">
                </asp:DropDownList>
            </td>
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
                    <DateInput ID="DateInput1" EmptyMessage="dd/mm/yyyy" runat="server" DisplayDateFormat="d/M/yyyy"
                        DateFormat="d/M/yyyy">
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
                    <DateInput ID="DateInput2" runat="server" EmptyMessage="dd/mm/yyyy" DisplayDateFormat="d/M/yyyy"
                        DateFormat="d/M/yyyy">
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
<table width="100%">
    <tr align="center">
        <td>
            <div id="divLobAdded" runat="server" class="success-msg" align="center" visible="false">
                WERP Broker Code Added successfully
            </div>
        </td>
    </tr>
    <tr align="center">
        <td>
            <div id="msgReprocessComplete" runat="server" class="success-msg" align="center"
                visible="false">
                Reprocess successfully Completed
            </div>
        </td>
    </tr>
    <tr>
        <td align="center">
            <div id="msgReprocessincomplete" runat="server" class="failure-msg" align="center"
                visible="false">
                Reprocess Failed!
            </div>
        </td>
    </tr>
</table>
<div>
    <asp:LinkButton ID="LinkInputRejects" runat="server" Text="View Input Rejects" CssClass="LinkButtons"
        OnClick="LinkInputRejects_Click"></asp:LinkButton>
</div>
<table id="tblProcessIdDetails" runat="server">
<tr><td colspan="2"></td></tr> 

<tr>
<td>
<asp:Label ID="lblProcessId" Text="Process Id: " CssClass="HeaderTextSmall" runat="server"></asp:Label>
<asp:Label ID="lblProcessIdValue"  CssClass="HeaderTextSmall" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</td> 
<td>
<asp:Label ID="lblRTName" Text="File Type: " CssClass="HeaderTextSmall" runat="server"></asp:Label>
<asp:Label ID="lblGetRTName"  CssClass="HeaderTextSmall" runat="server"></asp:Label>
</td>
</tr>

<tr>
<td>
<asp:Label ID="lbluploaddate" Text="Upload Date: " CssClass="HeaderTextSmall" runat="server"></asp:Label>
<asp:Label ID="lblGetUploadDate"  CssClass="HeaderTextSmall" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</td> 
<td>
<asp:Label ID="lblExtractType" Text="Extract Type: " CssClass="HeaderTextSmall" runat="server"></asp:Label>
<asp:Label ID="lblExtractTypeValue"  CssClass="HeaderTextSmall" runat="server"></asp:Label>

</td>
</tr>
</table>
<br />
<div id="divGvCAMSProfileReject" runat="server">
    <%-- style="width: 100%; height: auto; overflow: scroll"--%>
    <telerik:RadGrid ID="gvCAMSProfileReject" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        EnableViewState="true" Skin="Telerik" EnableEmbeddedSkins="false" Width="1050px"
        AllowFilteringByColumn="true" OnPreRender="gvCAMSProfileReject_PreRender" AllowAutomaticInserts="false"
        ExportSettings-FileName="MF Folio Reject Details" OnItemDataBound="gvCAMSProfileReject_ItemDataBound"
        OnNeedDataSource="gvCAMSProfileReject_OnNeedDataSource">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView DataKeyNames="CMFFS_Id,CMFFS_MainStagingId,ADUL_ProcessId,CMFSS_BrokerCode,WUXFT_XMLFileTypeId,XUET_ExtractTypeCode"
            Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
            <Columns>
                <telerik:GridTemplateColumn HeaderStyle-Width="80px" AllowFiltering="false" UniqueName="action"
                    DataField="action">
                    <HeaderTemplate>
                        <input id="chkBxAll" name="chkBxAll" type="checkbox" onclick="checkAllBoxes()" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkBx" runat="server" />
                        <asp:HiddenField ID="hdnchkBx" runat="server" Value='<%# Eval("CMFFS_Id").ToString() + "-" +  Eval("WRR_RejectReasonCode").ToString()%>' />
                        <asp:HiddenField ID="hdnBxProcessID" runat="server" Value='<%# Eval("ADUL_ProcessId").ToString() %>' />
                        <asp:HiddenField ID="hdnBxStagingId" runat="server" Value='<%# Eval("CMFFS_Id").ToString() %>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Button ID="btnSave" CssClass="FieldName" OnClick="btnSave_Click" runat="server"
                            Text="Save" />
                    </FooterTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="130px" HeaderText="Inv Name" DataField="CMFFS_INV_NAME"
                    UniqueName="InvName" SortExpression="CMFFS_INV_NAME" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="160px" HeaderText="PAN Number" DataField="CMFSFS_PANNum"
                    SortExpression="PANNumber" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemTemplate>
                        <asp:TextBox ID="txtPan" CssClass="txtField" runat="server" Text='<%# Bind("CMFSFS_PANNum") %>'></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtPanMultiple" CssClass="txtField" runat="server" />
                    </FooterTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="160px" HeaderText="Folio" DataField="CMFSFS_FolioNum"
                    SortExpression="CMFSFS_FolioNum" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemTemplate>
                        <asp:TextBox ID="txtFolio" CssClass="txtField" runat="server" Text='<%# Bind("CMFSFS_FolioNum") %>'></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtFolioMultiple" CssClass="txtField" runat="server" />
                    </FooterTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="116px" HeaderText="Broker Code" DataField="CMFSS_BrokerCode"
                    SortExpression="CMFSS_BrokerCode" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemTemplate>
                        <asp:TextBox ID="txtBroker" CssClass="txtField" runat="server" Text='<%# Bind("CMFSS_BrokerCode") %>'></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtBrokerMultiple" CssClass="txtField" runat="server" />
                    </FooterTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="AMC Name" DataField="PA_AMCName"
                    UniqueName="PA_AMCName" SortExpression="PA_AMCName" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="AMC Code" DataField="PA_AMCCode"
                    UniqueName="PA_AMCCode" SortExpression="PA_AMCCode" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" HeaderStyle-Width="116px" HeaderText="Product Code"
                    DataField="PASC_AMC_ExternalCode" UniqueName="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" HeaderStyle-Width="116px" HeaderText="Scheme Code"
                    DataField="PASP_SchemePlanCode" UniqueName="PASP_SchemePlanCode" SortExpression="PASP_SchemePlanCode"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="220px" HeaderText="Reject Reason" DataField="RejectReason"
                    UniqueName="RejectReason" SortExpression="RejectReason" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                    <FilterTemplate>
                        <telerik:RadComboBox ID="RadComboBoxRR" Width="200px" CssClass="cmbField" AllowFiltering="true"
                            AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxRR_SelectedIndexChanged"
                            IsFilteringEnabled="true" AppendDataBoundItems="true" AutoPostBackOnFilter="false"
                            OnPreRender="rcbContinents1_PreRender" EnableViewState="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("RejectReason").CurrentFilterValue %>'
                            runat="server">
                            <%--OnPreRender="rcbContinents_PreRender"--%>
                            <Items>
                                <telerik:RadComboBoxItem Text="All" Value="" Selected="true"></telerik:RadComboBoxItem>
                            </Items>
                        </telerik:RadComboBox>
                        <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">

                            <script type="text/javascript">
                                function InvesterNameIndexChanged(sender, args) {
                                    var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                    //////                                                    sender.value = args.get_item().get_value();
                                    tableView.filter("RejectReason", args.get_item().get_value(), "EqualTo");
                                } 
                            </script>

                        </telerik:RadScriptBlock>
                    </FilterTemplate>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" HeaderStyle-Width="322px" HeaderText="WUXFT_XMLFileTypeId"
                    DataField="WUXFT_XMLFileTypeId" UniqueName="WUXFT_XMLFileTypeId" SortExpression="WUXFT_XMLFileTypeId"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" HeaderStyle-Width="322px" HeaderText="CPS_Id"
                    DataField="CPS_Id" UniqueName="CPS_Id" SortExpression="CPS_Id" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" HeaderStyle-Width="322px" HeaderText="XUET_ExtractTypeCode"
                    DataField="XUET_ExtractTypeCode" UniqueName="XUET_ExtractTypeCode" SortExpression="XUET_ExtractTypeCode"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="ProcessId" HeaderStyle-Width="100px" DataField="ADUL_ProcessId"
                    UniqueName="ProcessId" SortExpression="ADUL_ProcessId" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <%-- <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="Broker Code" DataField="CMFSS_BrokerCode"
                    UniqueName="CMFSS_BrokerCode" SortExpression="CMFSS_BrokerCode" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="Bank Name" DataField="CMFFS_BANK_NAME"
                    UniqueName="CMFFS_BANK_NAME" SortExpression="CMFFS_BANK_NAME" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="Add1" DataField="CMGCXP_ADDRESS1"
                    UniqueName="CMGCXP_ADDRESS1" SortExpression="CMGCXP_ADDRESS1" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="Add2" DataField="CMGCXP_ADDRESS2"
                    UniqueName="CMGCXP_ADDRESS2" SortExpression="CMGCXP_ADDRESS2" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="Add3" DataField="CMGCXP_ADDRESS3"
                    UniqueName="CMGCXP_ADDRESS3" SortExpression="CMGCXP_ADDRESS3" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="City" DataField="CMGCXP_CITY"
                    UniqueName="CMGCXP_CITY" SortExpression="CMGCXP_CITY" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="Pin Code" DataField="CMGCXP_PINCODE"
                    UniqueName="CMGCXP_PINCODE" SortExpression="CMGCXP_PINCODE" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="Ph. Off" DataField="CMGCXP_PHONE_OFF"
                    UniqueName="CMGCXP_PHONE_OFF" SortExpression="CMGCXP_PHONE_OFF" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="Ph. Res" DataField="CMGCXP_PHONE_RES"
                    UniqueName="CMGCXP_PHONE_RES" SortExpression="CMGCXP_PHONE_RES" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="DOB" DataField="CMGCXP_DOB"
                    UniqueName="CMGCXP_DOB" SortExpression="CMGCXP_DOB" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="420px"></Scrolling>
            <ClientEvents OnGridCreated="GridCreated" />
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
<div runat="server" id="divBtnActionSection" visible="false">
    <asp:Button ID="btnReprocess" OnClick="btnReprocess_Click" runat="server" Text="Reprocess"
        CssClass="PCGLongButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_RejectedMFFolio_btnReprocess','L');"
        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_RejectedMFFolio_btnReprocess','L');" />
    <asp:Button ID="btnMapToCustomer" runat="server" CssClass="PCGLongButton" Text="Map to Customer"
        OnClientClick="return ShowPopup()" />
    <asp:Button ID="btnDelete" runat="server" CssClass="PCGLongButton" OnClick="btnDelete_Click"
        Text="Delete Records" />
    <asp:Button ID="btnAddLob" runat="server" CssClass="PCGLongButton" OnClick="btnAddLob_Click"
        Text="Add Broker Code" OnClientClick="return  GetSelectedNames();" />
    <br />
</div>
<div id="divProfileMessage" runat="server" visible="false" class="message">
    <asp:Label ID="lblEmptyMsg" class="FieldName" runat="server" Text="There are no records to be displayed!">
    </asp:Label>
</div>
<table>
    <tr id ="trNote" runat="server">
        <td colspan="2">
            <div id="Div2" class="Note">
                <p>
                    <span style="font-weight: bold">Note:</span><br />
                    1.WERP AMC Code was not found <br />
                      For This Type Reject Reason Please Contact to CustomerCare:
                      E-mail:CustCare@ampsys.in or Contact:080 - 32429514
                   
                </p>
            </div>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hfRmId" runat="server" />
