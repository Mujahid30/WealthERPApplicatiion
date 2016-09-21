<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFManualSingleTran.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.MFManualSingleTran" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html>
<head>
    <meta http-equiv="CACHE-CONTROL" content="NO-CACHE">
</head>
<body>
</body>
</html>

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        isItemSelected = true;
        //         document.getElementById("lblgetPan").innerHTML = "";
        document.getElementById("<%= hdntxtcustomerId.ClientID %>").value = eventArgs.get_value();
        //  alert(document.getElementById("<%= hdntxtcustomerId.ClientID %>").value);
        return false;
    }
    
</script>

<script type="text/javascript">
    function checkDate(sender, args) {

        var selectedDate = new Date();
        selectedDate = sender._selectedDate;

        var todayDate = new Date();
        var msg = "";

        if (selectedDate > todayDate) {
            sender._selectedDate = todayDate;
            sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            alert("Warning! - Date Cannot be in the future");
            return false;
        }

    }

    function GetSwitchSchemeCode(source, eventArgs) {

        document.getElementById("<%= txtSwitchSchemeCode.ClientID %>").value = eventArgs.get_value();

        return false;
    };
    //****************************************************************
    //    function ShowMessage() {
    //        alert('Transaction aded Successfully');
    //    }

    
</script>

<script type="text/javascript">
    function ChkForMainPortFolio(source, args) {

        var hdnIsCustomerLogin = document.getElementById('ctrl_MFManualSingleTran_hdnIsCustomerLogin').value;
        var hdnIsMainPortfolio = document.getElementById('ctrl_MFManualSingleTran_hdnIsMainPortfolio').value;

        if (hdnIsCustomerLogin == "Customer" && hdnIsMainPortfolio == "1") {

            args.IsValid = false;
        }
        else {
            args.IsValid = true;
        }

    }    
</script>

<script type="text/javascript">
    function checkTrxnDate() {
        var selectedDate = new Date();
        selectedDate = document.getElementById("<%= txtTransactionDate.ClientID %>").value;

        var todayDate = new Date();
        //        var day = selectedDate.value.split("/")[0];
        //        var month = selectedDate.value.split("/")[1];

        //        var year = selectedDate.value.split("/")[2];

        //        var dt = new Date(year, month - 1, day);

        //        var today = new Date();
        //        
        //        if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {
        //            alert("Invalid Date"); return false;


        alert(selectedDate);
        var trxnDate = new Date(selectedDate);

        alert(todayDate);

        alert(trxnDate);

        if (trxnDate > todayDate) {
            alert("Warning! - Date Cannot be in the future");
            return false;

        }

        var select = document.getElementById('<%=ddlFolioNum.ClientID %>');
        if (select.options[select.selectedIndex].value == "Select") {
            alert('Please Select Folio');
            return false;
        }
    }
  
</script>

<script type="text/javascript">
    function PortfolioManageMessage(source, args) {
        //     function PortfolioManageMessage() {
        var e = document.getElementById("<%= ddlPortfolio.ClientID %>");
        var strUser = e.options[e.selectedIndex].text;

        if (strUser == "MyPortfolio") {
            args.IsValid = false;


        }
        else {
            args.IsValid = true;
        }

    }
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<asp:UpdatePanel ID="upnlMFTrab" runat="server">
    <ContentTemplate>
        <style type="text/css">
            .style1
            {
                width: 197px;
            }
            .style2
            {
                width: 47px;
            }
        </style>
        <%--<table width="100%" class="TableBackground">
            <tr>
                <td class="HeaderCell">
                    <asp:Label ID="lblView" runat="server" CssClass="HeaderTextBig" Text="Add MF Transaction"></asp:Label>
                    <hr />
                </td>
            </tr>
        </table>--%>
        <table width="100%">
            <tr>
                <td colspan="3" style="width: 100%;">
                    <div class="divPageHeading">
                        <table cellspacing="0" cellpadding="3" width="100%">
                            <tr>
                                <td align="left">
                                    Add MF Transaction
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td align="center">
                    <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                        Transaction added Successfully
                    </div>
                </td>
            </tr>
        </table>
        <table style="width: 100%;" class="TableBackground">
            <tr id="trCustomerSearch" runat="server">
                <td class="leftField" style="width: 20%;">
                    <asp:Label ID="lblCustomerName" runat="server" Text="Search For:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" style="width: 80%;" colspan="4">
                    <asp:DropDownList ID="ddlSearchOption" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlSearchOption_SelectedIndexChanged">
                        <asp:ListItem Text="Select" Value="Select" Selected="true" />
                        <asp:ListItem Text="Name" Value="Name" />
                        <%--   <asp:ListItem Text="PAN" Value="Panno" />--%>
                        <asp:ListItem Text="Client Code" Value="Clientcode" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trtxtcustomerName" runat="server">
                <td class="leftField" style="width: 20%;">
                </td>
                <td class="rightField" style="width: 80%;" colspan="4">
                    <asp:TextBox ID="txtcustomerName" runat="server" CssClass="txtField" AutoPostBack="true"
                        Visible="false" OnTextChanged="txtcustomerName_TextChanged"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="txtCustomerName_water" TargetControlID="txtcustomerName"
                        WatermarkText="Enter Three Characters of Customer" runat="server" EnableViewState="false">
                    </cc1:TextBoxWatermarkExtender>
                    <ajaxToolkit:AutoCompleteExtender ID="txtCustomerName_autoCompleteExtender" runat="server"
                        TargetControlID="txtcustomerName" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                        MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                        CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                        CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                        UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                        Enabled="True" />
                    <asp:TextBox ID="txtCustCode" runat="server" CssClass="txtField" AutoPostBack="true"
                        Visible="false" OnTextChanged="txtCustCode_TextChanged"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" TargetControlID="txtCustCode"
                        WatermarkText="Enter few characters of Client Code" runat="server" EnableViewState="false">
                    </cc1:TextBoxWatermarkExtender>
                    <ajaxToolkit:AutoCompleteExtender ID="txtClientCode_autoCompleteExtender" runat="server"
                        TargetControlID="txtCustCode" ServiceMethod="GetCustCode" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                        MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                        CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                        CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                        UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                        Enabled="True" />
                    <asp:Label ID="lblcustandcustomer" runat="server" Text="*" CssClass="spnRequiredField"
                        Visible="false"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtcustomerName"
                        ErrorMessage="<br />Please Enter Customer Name" Display="Dynamic" runat="server"
                        Enabled="false" CssClass="rfvPCG" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtCustCode"
                        ErrorMessage="<br />Please Enter Cust Code" Display="Dynamic" runat="server"
                        Enabled="false" CssClass="rfvPCG" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField" style="width: 20%;">
                    <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Select the Portfolio :"></asp:Label>
                </td>
                <td class="rightField" style="width: 80%;" colspan="4">
                    <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
                    </asp:DropDownList>
                    <br />
                    <asp:CustomValidator ID="cvCheckForManageOrUnManage" runat="server" ValidationGroup="MFSubmit"
                        Display="Dynamic" ClientValidationFunction="ChkForMainPortFolio" CssClass="revPCG"
                        ErrorMessage="CustomValidator">Permisssion denied for Manage Portfolio !!</asp:CustomValidator>
                    <%-- <asp:CustomValidator ID="cv" runat="server" ValidationGroup="MFSubmit"
                         Display="Dynamic" ClientValidationFunction="PortfolioManageMessage" CssClass="revPCG"
                          ErrorMessage="CustomValidator">You can't add transaction for Manage Portfolio.Please select unmanage Portfolio</asp:CustomValidator>--%>
                </td>
                <%--<td style="width : 15%;">
                </td>
                <td style="width : 20%;">
                </td>
                <td style="width : 25%;">
                </td>--%>
            </tr>
            <tr>
                <td class="leftField" id="td1" runat="server">
                    <asp:Label ID="Label1" runat="server" Text="SubBroker Code :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" id="td2" runat="server">
                    <asp:TextBox ID="TxtsubBrokerCode" runat="server" CssClass="txtField" MaxLength="18"></asp:TextBox>
                    <%--<span id="Span17" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TxtsubBrokerCode"
                        CssClass="rfvPCG" ValidationGroup="MFSubmit" ErrorMessage="<br />Please enter the SubBroker Code"
                        Display="Dynamic" runat="server" InitialValue="">
                    </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <%-- <tr>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblAssociateSearch" runat="server" CssClass="FieldName" Text="SubBroker Code:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:TextBox ID="txtAssociateSearch" runat="server" CssClass="txtField" AutoComplete="Off"
                        AutoPostBack="True">
                    </asp:TextBox><span id="Span16" class="spnRequiredField">*</span>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" TargetControlID="txtAssociateSearch"
                        WatermarkText="Enter few chars of SubBroker code" runat="server" EnableViewState="false">
                    </cc1:TextBoxWatermarkExtender>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtAssociateSearch"
                        ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                        MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                        CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                        CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                        UseContextKey="True" OnClientItemSelected="" DelimiterCharacters="" Enabled="True" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAssociateSearch"
                        ErrorMessage="<br />Please Enter a SubBroker code" Display="Dynamic" runat="server"
                        CssClass="rfvPCG" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                </td>--%>
            <%-- </tr>--%>
            <tr>
                <td class="leftField" valign="top">
                    <asp:Label ID="lblAMC" runat="server" CssClass="FieldName" Text="AMC :"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlAMC" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlAMC_SelectedIndexChanged" Width="300px">
                    </asp:DropDownList>
                    <span id="Span7" class="spnRequiredField">* </span>
                    <asp:CompareValidator ID="cmpamc" runat="server" ErrorMessage="<br />Please select an AMC Name"
                        ValidationGroup="MFSubmit" ControlToValidate="ddlAMC" Operator="NotEqual" CssClass="rfvPCG"
                        ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr style="width: 100%">
                <td class="leftField" valign="top">
                    <asp:Label ID="lblSchemeSearch" runat="server" Text="Scheme Search :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="4">
                    <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" Width="400px">
                    </asp:DropDownList>
                    <span id="Span6" class="spnRequiredField">*
                        <asp:CompareValidator ID="cmpScheme" runat="server" ErrorMessage="<br />Please select a Scheme Name"
                            ValidationGroup="MFSubmit" ControlToValidate="ddlScheme" Operator="NotEqual"
                            CssClass="rfvPCG" ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>
                    </span>
                    <%-- <asp:HiddenField ID="txtSchemeCode" runat="server" OnValueChanged="txtSchemeCode_ValueChanged" />--%>
                    <%--<asp:TextBox ID="txtSearchScheme" runat="server" CssClass="txtSchemeName"  AutoComplete="Off"
                                        AutoPostBack="true"></asp:TextBox><cc1:TextBoxWatermarkExtender ID="txtSearchScheme_TextBoxWatermarkExtender"
                                            runat="server" TargetControlID="txtSearchScheme" WatermarkText="Type the Scheme Name">
                                        </cc1:TextBoxWatermarkExtender>
                                    <ajaxToolkit:AutoCompleteExtender ID="txtSearchScheme_autoCompleteExtender" runat="server"
                                        TargetControlID="txtSearchScheme" ServiceMethod="GetSchemeList" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                                        MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5" CompletionInterval="100"
                                        CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                                        CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                                        UseContextKey="true" OnClientItemSelected="GetSchemeCode" />--%>
                    <%--<span id="Span6" class="spnRequiredField">*<br />
                                    </span>--%>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="leftField" valign="top">
                    <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Category :"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                    </asp:DropDownList>
                    <span id="Span5" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="cmpCategory" runat="server" ErrorMessage="<br />Please select a Category"
                        ValidationGroup="MFSubmit" ControlToValidate="ddlCategory" Operator="NotEqual"
                        CssClass="rfvPCG" ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="leftField" valign="top">
                    <asp:Label ID="lblSchemeType" runat="server" CssClass="FieldName" Text="Scheme Type :"></asp:Label>
                </td>
                <td colspan="4" class="rightField">
                    <asp:DropDownList ID="ddlSchemeType" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlSchemeType_SelectedIndexChanged">
                        <asp:ListItem Text="Active" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <%--<td><span style='font-size: 8px; font-weight: normal' class='FieldName'>Enter
                                        few characters of Scheme Name.</span><br/>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtSearchScheme"
                                        ErrorMessage="Please Enter Scheme Name" Display="Dynamic" runat="server" CssClass="rfvPCG"
                                        ValidationGroup="MFSubmit">
                                    </asp:RequiredFieldValidator>
                 <asp:CompareValidator ID="cmpSchemeName" runat="server" ErrorMessage="<br />Please select a transaction type"
                                  ValidationGroup="MFSubmit" ControlToValidate="txtSearchScheme" Operator="NotEqual"
                                  CssClass="rfvPCG" ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>
                </td>--%>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label4" runat="server" Text="Folio Number :" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlFolioNum" runat="server" CssClass="cmbField" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlFolioNum_SelectedIndexChanged">
                        </asp:DropDownList>
                        <span id="Span3" class="spnRequiredField">*</span>
                        <br />
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />Please select a folio number"
                            ValidationGroup="MFSubmit" ControlToValidate="ddlFolioNum" Operator="NotEqual"
                            CssClass="rfvPCG" ValueToCompare="Select a Folio Number" Display="Dynamic"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="reqddlFolio" runat="server" CssClass="rfvPCG" ErrorMessage="Please select a folio number"
                            Text="Please select a folio number" Display="Dynamic" ValidationGroup="MFSubmit"
                            ControlToValidate="ddlFolioNum" InitialValue="0">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Button ID="btnNewFolioAdd" runat="server" Text="Add New Folio Number" CssClass="PCGLongButton"
                            OnClick="btnNewFolioAdd_Click" Height="26px" ValidationGroup="vgs"  Width="167px" Visible="true" />
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label3" runat="server" Text="Transaction Type:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlTransactionType" runat="server" CssClass="cmbField" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlTransactionType_SelectedIndexChanged">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>Buy</asp:ListItem>
                            <asp:ListItem>Sell</asp:ListItem>
                            <asp:ListItem Enabled="false">Bonus</asp:ListItem>
                            <asp:ListItem Enabled="false">Dividend Reinvestment</asp:ListItem>
                            <asp:ListItem>SIP</asp:ListItem>
                            <asp:ListItem Enabled="false">SWP</asp:ListItem>
                            <asp:ListItem Enabled="false">STP</asp:ListItem>
                            <asp:ListItem Enabled="false">Dividend Payout</asp:ListItem>
                            <asp:ListItem Enabled="false">Switch</asp:ListItem>
                            <asp:ListItem Enabled="false">Holdings</asp:ListItem>
                        </asp:DropDownList>
                        <span id="Span2" class="spnRequiredField">*</span>
                        <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />Please select a transaction type"
                            ValidationGroup="MFSubmit" ControlToValidate="ddlTransactionType" Operator="NotEqual"
                            CssClass="rfvPCG" ValueToCompare="lblScheme" Display="Dynamic"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="rfvPCG"
                            ErrorMessage="Please select a transaction type" Display="Dynamic" ValidationGroup="MFSubmit"
                            ControlToValidate="ddlTransactionType" InitialValue="Select">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <%--From here there is lots of work to do--%>
                <tr>
                    <td class="leftField" id="tdSchemeNameLabel" runat="server">
                        <asp:Label ID="lblSchemeName" runat="server" CssClass="FieldName" Text="Scheme Name :"></asp:Label>
                    </td>
                    <td class="rightField" id="tdSchemeNameValue" runat="server" colspan="3">
                        <asp:Label ID="lblScheme" runat="server" Text="Label" CssClass="Field"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label6" runat="server" Text="Transaction Date :" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtTransactionDate" runat="server" CssClass="txtField" OnTextChanged="txtTransactionDate_TextChanged"
                            AutoPostBack="true"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtTransactionDate_CalendarExtender" runat="server"
                            Format="dd/MM/yyyy" TargetControlID="txtTransactionDate" OnClientDateSelectionChanged="checkDate">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtTransactionDate_TextBoxWatermarkExtender"
                            runat="server" TargetControlID="txtTransactionDate" WatermarkText="dd/mm/yyyy">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <span id="Span4" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTransactionDate"
                            CssClass="rfvPCG" ValidationGroup="MFSubmit" ErrorMessage="<br />Please select a Transaction Date"
                            Display="Dynamic" runat="server" InitialValue="">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CVTransactionDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                            Type="Date" ControlToValidate="txtTransactionDate" CssClass="cvPCG" Operator="DataTypeCheck"
                            ValidationGroup="MFSubmit" ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="MFSubmit"
                            CssClass="cvPCG" ControlToValidate="txtTransactionDate" ErrorMessage="<br/>Date Cannot be in the future"
                            Display="Dynamic" Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
                        <%--<asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="<br />The date format should be dd/MM/yyyy"
                ValidationGroup="MFSubmit" Type="Date" ControlToValidate="txtTransactionDate"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>--%>
                    </td>
                    <td align="right" id="tdLblNav" runat="server">
                        <asp:Label ID="Label19" runat="server" Text="" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="style1" id="tdtxtNAV" runat="server">
                        <asp:TextBox ID="txtNAV" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td id="tdNavButton" runat="server">
                        <asp:Button ID="btnUseNAV" runat="server" Text="Use Price" CssClass="PCGMediumButton"
                            OnClick="btnUseNAV_Click" />
                    </td>
                </tr>
                <tr id="trDividentRate" runat="server">
                    <td class="leftField">
                        <asp:Label ID="Label7" runat="server" Text="Dividend Rate(%) :" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtDividentRate" runat="server" CssClass="txtField"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtDividentRate"
                        CssClass="rfvPCG" ValidationGroup="MFSubmit" ErrorMessage="<br />Please enter a Dividend Rate"
                        Display="Dynamic" runat="server" InitialValue="">--%>
                        <%-- </asp:RequiredFieldValidator>--%>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<br />Enter a numeric value"
                            CssClass="rfvPCG" ValidationGroup="MFSubmit" Type="Double" ControlToValidate="txtDividentRate"
                            Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td class="leftField" id="tdSchemeToLabel" runat="server" colspan="2">
                        <asp:Label ID="lblSchemeTo" runat="server" Text="Scheme To :" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField" id="tdSchemeToValue" runat="server">
                        <asp:HiddenField ID="txtSwitchSchemeCode" runat="server" OnValueChanged="txtSwitchSchemeCode_ValueChanged" />
                        <asp:TextBox ID="txtSwicthSchemeSearch" runat="server" CssClass="txtField" AutoComplete="Off"
                            AutoPostBack="true"></asp:TextBox><cc1:TextBoxWatermarkExtender ID="txtSwitchSchemeCode_TextBoxWatermarkExtender"
                                runat="server" TargetControlID="txtSwicthSchemeSearch" WatermarkText="Type the Scheme Name">
                            </cc1:TextBoxWatermarkExtender>
                        <ajaxToolkit:AutoCompleteExtender ID="txtSwitchSchemeCode_AutoCompleteExtender" runat="server"
                            TargetControlID="txtSwicthSchemeSearch" ServiceMethod="GetSwitchSchemeList" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                            MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5" CompletionInterval="100"
                            CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                            CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                            UseContextKey="true" OnClientItemSelected="GetSwitchSchemeCode" />
                        <span id="Span1" class="spnRequiredField">*<br />
                        </span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtSwicthSchemeSearch"
                            ErrorMessage="Please Enter Scheme Name" Display="Dynamic" runat="server" CssClass="rfvPCG"
                            ValidationGroup="MFSubmit">
                        </asp:RequiredFieldValidator><span style='font-size: 8px; font-weight: normal' class='FieldName'>Enter
                            few characters of a Scheme name.</span>
                    </td>
                </tr>
                <tr runat="server" id="trPrice">
                    <td class="leftField">
                        <asp:Label ID="Label18" runat="server" Text="Price :" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtPrice" runat="server" CssClass="txtField" MaxLength="18"></asp:TextBox>
                        <span id="Span8" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtPrice"
                            ValidationGroup="MFSubmit" ErrorMessage="<br />Please enter a Price" Display="Dynamic"
                            CssClass="rfvPCG" runat="server" InitialValue="">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtPrice"
                            CssClass="rfvPCG" ValidationGroup="MFSubmit" Display="Dynamic" runat="server"
                            ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblPurDate" runat="server" Text="As on Date :" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblNavAsOnDate" runat="server" CssClass="txtField" Enabled="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td id="tdNAVPurchasedLabel" runat="server" align="right" colspan="2">
                        <asp:Label ID="lblNAVPurchased" runat="server" Text="NAV of Scheme Purchased into :"
                            CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField" id="tdNAVPurchasedValue" runat="server">
                        <asp:TextBox ID="txtNAVPurchased" runat="server" CssClass="txtField" MaxLength="18"></asp:TextBox>
                        <span id="Span9" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtNAVPurchased"
                            CssClass="rfvPCG" ValidationGroup="MFSubmit" ErrorMessage="<br />Please enter the purchased NAV"
                            Display="Dynamic" runat="server" InitialValue="">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtNAVPurchased"
                            CssClass="rfvPCG" ValidationGroup="MFSubmit" Display="Dynamic" runat="server"
                            ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="rightfield" runat="server" id="tdSwitchUseNAV" visible="false">
                        <asp:Button ID="btnSwitchNAV" runat="server" Text="Use NAV" CssClass="PCGMediumButton"
                            OnClick="btnSwitchNAV_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="leftField" id="tdAmountlbl">
                        <asp:Label ID="Label9" runat="server" Text="Amount :" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField" id="tdAmount">
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField" OnTextChanged="txtAmount_TextChanged"
                            MaxLength="18" AutoPostBack="true"></asp:TextBox>
                        <span id="Span10" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtAmount"
                            CssClass="rfvPCG" ValidationGroup="MFSubmit" ErrorMessage="<br />Please enter an Amount"
                            Display="Dynamic" runat="server" InitialValue="">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtAmount"
                            CssClass="rfvPCG" ValidationGroup="MFSubmit" Display="Dynamic" runat="server"
                            ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="leftField" id="tdPricePurchasedLabel" runat="server" colspan="2">
                        <asp:Label ID="lblPricePurchased" runat="server" Text="Price of Scheme Purchased into :"
                            CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField" id="tdPricePurchasedValue" runat="server">
                        <asp:TextBox ID="txtPricePurchased" runat="server" CssClass="txtField" MaxLength="18"></asp:TextBox>
                        <span id="Span11" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtPricePurchased"
                            CssClass="rfvPCG" ValidationGroup="MFSubmit" ErrorMessage="<br />Please enter the purchased price"
                            Display="Dynamic" runat="server" InitialValue="">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtPricePurchased"
                            CssClass="rfvPCG" ValidationGroup="MFSubmit" Display="Dynamic" runat="server"
                            ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField" id="tdUnitsLabel" runat="server">
                        <asp:Label ID="Label10" runat="server" Text="Units :" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField" id="tdUnitsValue" runat="server">
                        <asp:TextBox ID="txtUnits" runat="server" CssClass="txtField" OnTextChanged="txtUnits_TextChanged"
                            MaxLength="18" AutoPostBack="true"></asp:TextBox>
                        <span id="Span12" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtUnits"
                            CssClass="rfvPCG" ValidationGroup="MFSubmit" ErrorMessage="<br />Please enter the number of units"
                            Display="Dynamic" runat="server" InitialValue="">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtUnits"
                            CssClass="rfvPCG" ValidationGroup="MFSubmit" Display="Dynamic" runat="server"
                            ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="leftField" id="tdAmtPurchasedLabel" runat="server" colspan="2">
                        <asp:Label ID="lblAmtPurchased" runat="server" Text="Amount :" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField" id="tdAmtPurchasedValue" runat="server">
                        <asp:TextBox ID="txtAmtPurchased" runat="server" CssClass="txtField" MaxLength="18"
                            OnTextChanged="txtAmtPurchased_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <span id="Span15" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtAmtPurchased"
                            CssClass="rfvPCG" ValidationGroup="MFSubmit" ErrorMessage="<br />Please enter the Amount"
                            Display="Dynamic" runat="server" InitialValue="">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ControlToValidate="txtAmtPurchased"
                            CssClass="rfvPCG" ValidationGroup="MFSubmit" Display="Dynamic" runat="server"
                            ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField" id="tdSTTLabel" runat="server">
                        <asp:Label ID="Label11" runat="server" Text="STT :" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField" id="tdSTTValue" runat="server">
                        <asp:TextBox ID="txtSTT" runat="server" CssClass="txtField" MaxLength="18"></asp:TextBox>
                        <span id="Span14" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtSTT"
                            CssClass="rfvPCG" ValidationGroup="MFSubmit" ErrorMessage="<br />Please enter the STT"
                            Display="Dynamic" runat="server" InitialValue="">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="txtSTT"
                            CssClass="rfvPCG" ValidationGroup="MFSubmit" Display="Dynamic" runat="server"
                            ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="leftField" id="tdUnitsAllotedLabel" runat="server" colspan="2">
                        <asp:Label ID="lblUnitsAlloted" runat="server" Text="Units Allotted :" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField" id="tdUnitsAllotedValue" runat="server">
                        <asp:TextBox ID="txtUnitsAlloted" runat="server" CssClass="txtField" MaxLength="18"></asp:TextBox>
                        <span id="Span13" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator50" ControlToValidate="txtUnitsAlloted"
                            CssClass="rfvPCG" ValidationGroup="MFSubmit" ErrorMessage="<br />Please enter the units alloted"
                            Display="Dynamic" runat="server" InitialValue="">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ValidationGroup="MFSubmit"
                            ControlToValidate="txtUnitsAlloted" CssClass="rfvPCG" Display="Dynamic" runat="server"
                            ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField" id="td3" runat="server">
                        <asp:Label ID="Label2" runat="server" Text="Area:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField" id="td4" runat="server">
                        <asp:TextBox ID="txtArea" runat="server" CssClass="txtField" AutoPostBack="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField" id="td5" runat="server">
                        <asp:Label ID="Label5" runat="server" Text="EUIN:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField" id="td6" runat="server">
                        <asp:TextBox ID="txtEUIN" runat="server" CssClass="txtField" AutoPostBack="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="SubmitCell" colspan="5">
                    </td>
                </tr>
        </table>
        <asp:HiddenField ID="hdnIsMainPortfolio" runat="server" />
        <asp:HiddenField ID="hdnIsCustomerLogin" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<table>
    <tr>
        <td class="leftField">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_MFManualSingleTran_btnSubmit', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_MFManualSingleTran_btnSubmit', 'S');"
                ValidationGroup="MFSubmit" OnClick="btnSubmit_Click" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdntxtcustomerId" runat="server" />
