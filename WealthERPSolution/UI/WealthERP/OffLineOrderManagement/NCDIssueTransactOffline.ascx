<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDIssueTransactOffline.ascx.cs"
    Inherits="WealthERP.OffLineOrderManagement.NCDIssueTransactOffline" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script type="text/javascript">
    function setCustomPosition(sender, args) {
        sender.moveTo(sender.get_left(), sender.get_top());
    }
</script>

<script runat="server">
  
    protected void rbtnNo_Load(object sender, EventArgs e)
    {
        if (rbtnNo.Checked)
        {
            ddlModeOfHolding.Enabled = false;
        }
    }
   
</script>

<%--
<script type="text/javascript">
    function HideLabel(tblMessage) {
        setTimeout("HideLabelHelper('" + tblMessage + "');", 50000);
    }
    function HideLabelHelper(tblMessage) {
        document.getElementById(tblMessage).style.display = "none";
    }
</script>--%>

<script type="text/javascript">
    var TargetBaseControl = null;
    var TragetBaseControl2 = null;



    function TestCheckBox() {
        if (TargetBaseControl == null) return false;

        //get target child control.
        var TargetChildControl = "chkDematId";
        var Count = 0;
        //get all the control of the type INPUT in the base control.
        var Inputs = TargetBaseControl.getElementsByTagName("input");

        for (var n = 0; n < Inputs.length; ++n)
            if (Inputs[n].type == 'checkbox' &&
            Inputs[n].id.indexOf(TargetChildControl, 0) >= 0 &&
            Inputs[n].checked)
            Count++;
        if (Count > 1) {
            alert('Please Select One Demat!');
            return false;
        }
        else if (Count == 0) {
            alert('Please Select Aleast One Demat!');
            return false;
        }

        return true;


    }
    function TriggeredKey(e) {
        var keycode;
        if (window.event) keycode = window.event.keyCode;
        if (window.event.keyCode = 13) return false;
    }
    
</script>

<script type="text/javascript" language="javascript">

    function ValidateAssociateName() {
        //        var x = document.forms["form1"]["TextBoxName"].value;
        document.getElementById("<%=  lblAssociatetext.ClientID %>").value = eventArgs.get_value();
        document.getElementById("lblAssociatetext").innerHTML = "AgentCode Required";
        return true;
    }

    
</script>

<script type="text/javascript">
    function CustomerValidate(type) {
        if (type == 'pdf') {
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=2";
        } else if (type == 'doc') {
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=4";
        }
        else if (type == 'View') {
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=0";
        }
        else {
            window.document.forms[0].target = '_blank';
            window.document.forms[0].action = "/Reports/Display.aspx?mail=3";
        }

        setTimeout(function() {
            window.document.forms[0].target = '';
            window.document.forms[0].action = "ControlHost.aspx?pageid=OrderEntry";
        }, 500);
        return true;

    }
    function ShowIsa() {

        var hdn = document.getElementById("<%=hdnIsSubscripted.ClientID%>").value;

    }
    
</script>

<script type="text/javascript">
    function isNumberKey(evt) { // Numbers only
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            alert('Only Numeric');
            return false;
        }
        return true;  
</script>

<script type="text/javascript">
    var crnt = 0;
    function PreventClicks() {
        if (typeof (Page_ClientValidate('btnConfirmOrder')) == 'function') {
            Page_ClientValidate();
        }

        if (Page_IsValid) {
            if (++crnt > 1) {
                return false;
            }
            return true;
        }
        else {
            return false;
        }
    }


    function Validate() {
        var isValid = false;
        isValid = Page_ClientValidate('btnConfirmOrder');
        if (isValid) {
            isValid = Page_ClientValidate('btnTC');
        }

        return isValid;
    }   
   
    
</script>

<script type="text/javascript">

    function ValGroup() {
        var isValid = false;
        isValid = Page_ClientValidate('btnConfirmOrder');
        if (isValid) {
            isValid = Page_ClientValidate('ddlBrokerCode');
        }
        return isValid;
    }
</script>

<style type="text/css">
    .Page_Left_Padding
    {
        width: 5%;
    }
    .Page_Right_Padding
    {
        width: 8%;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
    <ContentTemplate>
        <table width="100%" id="tblNCDOrder" runat="server">
            <tr>
                <td colspan="6">
                    <div class="divPageHeading">
                        <table cellspacing="0" cellpadding="3" width="100%">
                            <tr>
                                <td align="left">
                                    NCD Order Entry
                                </td>
                                <td>
                                    <div class="divViewEdit" style="float: right; padding-right: 50px">
                                        <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_LinkButtons" CssClass="LinkButtons"
                                            Text="Edit" Visible="false"></asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr id="trMessage" runat="server" visible="false">
                <td colspan="6">
                    <table class="tblMessage" cellspacing="0">
                        <tr>
                            <td align="center">
                                <div id="divMessage" align="center">
                                </div>
                                <div style="clear: both">
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        <div class="fltlft" style="width: 100%; float: left;">
                            Customer Details
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="Page_Left_Padding">
                </td>
                <td class="leftField">
                    <asp:Label ID="lblName" runat="server" CssClass="FieldName" Text="Name:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtFirstName" runat="server" MaxLength="75" CssClass="txtField"
                        TabIndex="1"></asp:TextBox>
                    <span id="Span15" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtFirstName"
                        ErrorMessage="<br />Please Enter Name" Display="Dynamic" runat="server" CssClass="rfvPCG"
                        ValidationGroup="btnConfirmOrder">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="leftField" style="width: 15%">
                    <asp:Label ID="lblPanNum" runat="server" CssClass="FieldName" Text="PAN:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPanNumber" runat="server" CssClass="txtField" MaxLength="10"
                        TabIndex="2" Style="text-transform: uppercase;"></asp:TextBox>
                    <span id="Span14" class="spnRequiredField">*</span> &nbsp;
                    <br />
                    <asp:RequiredFieldValidator ID="rfvPanNumber" ControlToValidate="txtPanNumber" ErrorMessage="Please Enter  PAN"
                        Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="btnConfirmOrder">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                        CssClass="rfvPCG" ErrorMessage="Please Check PAN Format" ControlToValidate="txtPanNumber"
                        ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}" ValidationGroup="btnConfirmOrder">
                    </asp:RegularExpressionValidator>
                    <asp:Label ID="lblPanDuplicate" runat="server" CssClass="Error" Text="PAN already exists"
                        Visible="false"></asp:Label>
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
            <tr id="trCustomerType" runat="server" visible="false">
                <td class="Page_Left_Padding">
                </td>
                <td class="leftField" >
                    <asp:Label ID="lblCustomerType" runat="server" CssClass="FieldName" Text="Customer Type:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:RadioButton ID="rbtnIndividual" runat="server" CssClass="txtField" Text="Individual"
                        GroupName="grpCustomerType" AutoPostBack="true" OnCheckedChanged="rbtnIndividual_CheckedChanged"
                        TabIndex="3" Checked="true" />
                    &nbsp;&nbsp;
                    <asp:RadioButton ID="rbtnNonIndividual" runat="server" CssClass="txtField" Text="Non Individual"
                        GroupName="grpCustomerType" AutoPostBack="true" OnCheckedChanged="rbtnNonIndividual_CheckedChanged"
                        TabIndex="4" />
                </td>
                <td class="leftField">
                    <asp:Label ID="lblCustomerSubType" runat="server" CssClass="FieldName" Text="Customer Sub Type:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlCustomerSubType" runat="server" CssClass="cmbField" TabIndex="5"
                        OnSelectedIndexChanged="ddlCustomerSubType_OnSelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <span id="Span19" class="spnRequiredField">*</span> &nbsp;
                    <br />
                    <asp:RequiredFieldValidator ID="ReqddlCustomerSubType" ControlToValidate="ddlCustomerSubType"
                        ErrorMessage="Please Select Customer Sub Type" Display="Dynamic" runat="server"
                        CssClass="rfvPCG" ValidationGroup="btnConfirmOrder" InitialValue="0">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
            <tr>
                <td class="Page_Left_Padding">
                </td>
                 <td class="leftField">
                    <asp:Label ID="lblDepositoryName" runat="server" Text="Depository Name:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlDepositoryName" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlDepositoryName_SelectedIndexChanged" TabIndex="6">
                    </asp:DropDownList>
                    <span id="Span27" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="ddlDepositoryName"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please Select Depository Name"
                        InitialValue="Select" ValidationGroup="btnConfirmOrder">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="leftField">
                   &nbsp; <asp:Label ID="lblBeneficiary" runat="server" Text="Beneficiary Acct. No.:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtDpClientId" runat="server" CssClass="txtField" MaxLength="16"
                        TabIndex="7"></asp:TextBox>
                    <span id="Span26" class="spnRequiredField">*</span>
                    <asp:RegularExpressionValidator ID="rev" runat="server" ControlToValidate="txtDpClientId"
                        ValidationGroup="btnConfirmOrder" ErrorMessage="Special Character Are Not Allowed!"
                        CssClass="cvPCG" Display="Dynamic" ValidationExpression="^[a-zA-Z0-9]+(?:--?[a-zA-Z0-9]+)*$" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtDpClientId"
                        ErrorMessage="</br>Please  Enter  Beneficiary Acct. No." CssClass="cvPCG" ValidationGroup="btnConfirmOrder"
                        Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" OnServerValidate="CVBenificalNo_ServerValidat"
                        Text="" ErrorMessage="Beneficiary Acct. No. NSDL-8 characters,CDSL-16 characters" ControlToValidate="txtDpClientId"
                        Display="Dynamic" ValidationGroup="btnConfirmOrder" CssClass="rfvPCG">                                                
                    </asp:CustomValidator>
                </td>
               
                <td class="Page_Right_Padding">
                </td>
            </tr>
            <tr>
                <td class="Page_Left_Padding">
                </td>
                <td class="leftField">
                    <asp:Label ID="lblDPId" runat="server" Text="DP Id:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtDPId" runat="server" CssClass="txtField" TabIndex="8" MaxLength="8"></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic"
                        CssClass="rfvPCG" ErrorMessage="DP Id should be 8 characters" ControlToValidate="txtDPId"
                        ValidationExpression="^[A-Za-z0-9]{8}$" ValidationGroup="btnConfirmOrder">
                    </asp:RegularExpressionValidator>
                </td>
                <td class="leftField">
                </td>
                <td class="rightField">
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
            <tr>
                <td class="Page_Left_Padding">
                </td>
                <td class="leftField">
                    <asp:Label ID="lblAssociateSearch" runat="server" CssClass="FieldName" Text="Agent Code:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtAssociateSearch" onkeydown="return (event.keyCode!=13);" runat="server"
                        CssClass="txtField" AutoComplete="Off" OnTextChanged="OnAssociateTextchanged"
                        AutoPostBack="True" TabIndex="9">
                    </asp:TextBox><span id="Span7" class="spnRequiredField">*</span>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" TargetControlID="txtAssociateSearch"
                        WatermarkText="Enter few chars of Agent code" runat="server" EnableViewState="false">
                    </cc1:TextBoxWatermarkExtender>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtAssociateSearch"
                        ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                        MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                        CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                        CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                        UseContextKey="True" DelimiterCharacters="" Enabled="True" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAssociateSearch"
                        ErrorMessage="<br />Please Enter An Agent Code" Display="Dynamic" runat="server"
                        CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblAssociate" runat="server" CssClass="FieldName" Text="Associate:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:Label ID="lblAssociatetext" runat="server" CssClass="txtField" Enabled="false"></asp:Label>
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
            <tr id="trTaxStatus" runat="server">
                <td class="Page_Left_Padding">
                </td>
                <td colspan="2">
                </td>
                <td class="leftField">
                    <asp:Label ID="lblAssociateReport" runat="server" CssClass="FieldName" Text="Report To:"
                        Visible="true"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:Label ID="lblAssociateReportTo" runat="server" CssClass="txtField" Enabled="false"></asp:Label>
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        Order Detail Section
                    </div>
                </td>
            </tr>
            <tr>
                <td class="Page_Left_Padding">
                </td>
                <td class="leftField">
                    <asp:Label ID="lblIssueName" runat="server" Text="Select Issue Name:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:DropDownList ID="ddlIssueList" runat="server" AutoPostBack="true" CssClass="cmbExtraLongField"
                        OnSelectedIndexChanged="ddlIssueList_OnSelectedIndexChanged" TabIndex="10">
                    </asp:DropDownList>
                    <span id="Span6" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvIssueList" runat="server" ControlToValidate="ddlIssueList"
                        ErrorMessage="Please Select The Issue Name" CssClass="rfvPCG" Display="Dynamic"
                        ValidationGroup="btnConfirmOrder" InitialValue="Select"></asp:RequiredFieldValidator>
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
            <tr>
                <td class="Page_Left_Padding">
                </td>
                   <td class="leftField">
                    <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Category"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" TabIndex="11" OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                     </asp:DropDownList>
                       <span id="Span8" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlCategory"
                        ErrorMessage="<br />Please Select Category" Display="Dynamic" runat="server"
                        CssClass="rfvPCG" ValidationGroup="btnConfirmOrder" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
                <td class="leftField">
                    <asp:Label runat="server" ID="lblBrokerCode" CssClass="FieldName" Text="Select Broker:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlBrokerCode" runat="server" CssClass="cmbField" TabIndex="12">
                    </asp:DropDownList>
                       <span id="Span1" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="ReqddlBrokerCode" ControlToValidate="ddlBrokerCode"
                        ErrorMessage="<br />Please Select Broker" Display="Dynamic" runat="server"
                        CssClass="rfvPCG" ValidationGroup="btnConfirmOrder" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
            <tr>
                <td class="Page_Left_Padding">
                </td>
                <td class="leftField">
                    <asp:Label ID="lblApplicationNo" runat="server" Text="Application No.:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtApplicationNo" MaxLength="9" onkeydown="return (event.keyCode!=13);"
                        runat="server" CssClass="txtField" AutoPostBack="false" OnKeypress="javascript:return isNumberKey(event);"
                        TabIndex="13"></asp:TextBox>
                    <span id="Span2" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtApplicationNo"
                        ErrorMessage="<br />Please Enter Application No." Display="Dynamic" runat="server"
                        CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
                   <%-- <asp:Label ID="lblApplicationDuplicate" runat="server" CssClass="Error" Text="Application Number Already Exists"
                        Visible="false"></asp:Label>--%>
                        <asp:CustomValidator ID="CVApplicationNo" runat="server" OnServerValidate="CVApplicationNo_ServerValidat"
                        Text="" ErrorMessage="Application Number already exists" ControlToValidate="txtApplicationNo"
                        Display="Dynamic" ValidationGroup="btnConfirmOrder" CssClass="rfvPCG">                                                
                    </asp:CustomValidator>
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
            <tr>
                <td class="Page_Left_Padding">
                </td>
                <td class="leftField">
                    <asp:Label ID="Label5" runat="server" Text="Mode of Payment:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlPaymentMode" runat="server" AutoPostBack="true" CssClass="cmbField"
                        OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged" Width="180px" TabIndex="14">
                        <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                        <asp:ListItem Text="Cheque/Demand Draft" Value="CQ"></asp:ListItem>
                        <asp:ListItem Text="ASBA" Value="ES"></asp:ListItem>
                    </asp:DropDownList>
                    <span id="Span10" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator13" runat="server" ControlToValidate="ddlPaymentMode"
                        CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please Select  Mode of Payment"
                        Operator="NotEqual" ValidationGroup="btnConfirmOrder" ValueToCompare="Select"></asp:CompareValidator>
                </td>
                <td id="Td3" class="leftField" runat="server" visible="false">
                    <asp:Label ID="lblBankAccount" Text="Bank Account No.:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td id="Td4" class="rightField" runat="server" visible="false">
                    <asp:TextBox ID="txtBankAccount" runat="server" CssClass="txtField" onkeydown="return (event.keyCode!=13);"
                        OnKeypress="javascript:return isNumberKey(event);" MaxLength="16" TabIndex="15"></asp:TextBox>
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
            <tr id="trPINo" runat="server" visible="false">
                <td class="Page_Left_Padding">
                </td>
                <td class="leftField">
                    <asp:Label ID="lblPaymentNumber" runat="server" Text="Cheque/Demand Draft NO: " CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPaymentNumber" onkeydown="return (event.keyCode!=13);" runat="server"
                        MaxLength="6" OnKeypress="javascript:return isNumberKey(event);" CssClass="txtField"
                        TabIndex="16"></asp:TextBox>
                    <span id="Span12" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtPaymentNumber"
                        ErrorMessage="<br />Please Enter Cheque/Demand Draft NO." Display="Dynamic" runat="server"
                        CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblPIDate" runat="server" Text="Cheque Date:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <telerik:RadDatePicker ID="txtPaymentInstDate" onkeydown="return (event.keyCode!=13);"
                        CssClass="txtField" runat="server" Culture="English (United States)" Skin="Telerik"
                        EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01" TabIndex="17"
                        OnSelectedDateChanged="txtPaymentInstDate_OnSelectedDateChanged">
                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                            Skin="Telerik" EnableEmbeddedSkins="false">
                        </Calendar>
                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                   <%-- <span id="Span11" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator14" runat="server" ErrorMessage="<br/>Please enter a valid date."
                        Type="Date" ControlToValidate="txtPaymentInstDate" CssClass="cvPCG" Operator="DataTypeCheck"
                        ValueToCompare="" Display="Dynamic" ValidationGroup="btnConfirmOrder" Enabled="true"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtPaymentInstDate"
                        ErrorMessage="<br />Please Enter Cheque Date." Display="Dynamic" runat="server"
                        CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>--%>
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
            <tr id="trASBA" runat="server" visible="false">
                <td class="Page_Left_Padding">
                </td>
                <td class="leftField">
                    <asp:Label ID="lblASBANo" Text="ASBA Bank A/c NO:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtASBANO" MaxLength="16" onkeydown="return (event.keyCode!=13);"
                        runat="server" CssClass="txtField" TabIndex="18"></asp:TextBox>
                    <span id="Span5" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtASBANO"
                        ErrorMessage="<br />Please Enter Account No." Display="Dynamic" runat="server"
                        CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
            <tr id="trBankName" runat="server">
                <td class="Page_Left_Padding">
                </td>
                <td class="leftField">
                    <asp:Label ID="lblBankName" runat="server" Text="Bank Name:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlBankName" runat="server" CssClass="cmbField" AutoPostBack="false"
                        AppendDataBoundItems="true" Width="380px" TabIndex="19">
                    </asp:DropDownList>
                    <span id="Span4" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator18" runat="server" ControlToValidate="ddlBankName"
                        CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a Bank"
                        Operator="NotEqual" ValidationGroup="btnConfirmOrder" ValueToCompare="Select"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ControlToValidate="ddlBankName"
                        CssClass="rfvPCG" ErrorMessage="<br />Please Select a Bank" Display="Dynamic"
                        runat="server" InitialValue="" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblBranchName" runat="server" Text="Bank BranchName:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtBranchName" onkeydown="return (event.keyCode!=13);" runat="server"
                        CssClass="txtField" TabIndex="19"></asp:TextBox>
                   <%-- <span id="Span3" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtBranchName"
                        CssClass="rfvPCG" ErrorMessage="<br />Please Enter Bank Branch" Display="Dynamic"
                        runat="server" InitialValue="" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>--%>
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div class="divSectionHeading" style="vertical-align: text-bottom; width: auto;">
                        NCD Issue Details
                    </div>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlNCDIssueList" runat="server" ScrollBars="Vertical" Width="100%" Visible="false">
                <table width="100%" id="tblgvIssueList" runat="server" visible="false">
            <tr>
                <td>
                    <telerik:RadGrid ID="gvIssueList" runat="server" AllowSorting="false" enableloadondemand="True"
                        PageSize="10" AllowPaging="false" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                        GridLines="None" ShowFooter="false" PagerStyle-AlwaysVisible="true" ShowStatusBar="True"
                        Skin="Telerik" AllowFilteringByColumn="false" Width="100%">
                        <MasterTableView AllowMultiColumnSorting="false" AllowSorting="false" DataKeyNames="AIM_IssueId,AIM_IssueName,IssueTimeType,AIM_MInQty,AIM_MaxQty"
                            AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridBoundColumn DataField="AIM_IssueName" HeaderStyle-Width="150px" CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false" AutoPostBackOnFilter="false" HeaderText="Issue" UniqueName="AIM_IssueName"
                                    SortExpression="AIM_IssueName">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="AIM_IssueId" HeaderStyle-Width="60px"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                    HeaderText="Scrip ID" UniqueName="AIM_IssueId" SortExpression="AIM_IssueId">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width=" " Wrap="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="PI_IssuerId" HeaderStyle-Width="60px"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                    HeaderText="Scrip ID" UniqueName="PI_IssuerId" SortExpression="PI_IssuerId">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width=" " Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="PI_IssuerCode" HeaderStyle-Width="70px"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                    HeaderText="Issuer" UniqueName="PI_IssuerCode" SortExpression="PI_IssuerCode">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIM_NatureOfBond" HeaderStyle-Width="100px" CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false" AutoPostBackOnFilter="false" HeaderText="Type" UniqueName="AIM_NatureOfBond"
                                    SortExpression="AIM_NatureOfBond" Visible="false">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="true" DataField="PAIC_AssetInstrumentCategoryName"
                                    HeaderStyle-Width="60px" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="false" HeaderText="Category" UniqueName="PAIC_AssetInstrumentCategoryName"
                                    SortExpression="false" AllowFiltering="false">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width=" " Wrap="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIM_Rating" HeaderStyle-Width="70px" HeaderText="Rating"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                    UniqueName="AIM_Rating" Visible="true">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CatCollection" HeaderStyle-Width="140px" HeaderText="Min-Max Qty(Category Wise)"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                    UniqueName="CatCollection">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="MinMaxCatCollection" HeaderStyle-Width="100px"
                                    HeaderText="Min-Max Qty(Across All Series)" CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false" AutoPostBackOnFilter="false" UniqueName="MinMaxCatCollection">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIM_MInQty" HeaderStyle-Width="120px" HeaderText="Min. Qty (accross all series)"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                    DataFormatString="{0:N0}" UniqueName="AIM_MInQty" Visible="false">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIM_MaxQty" HeaderStyle-Width="140px" HeaderText="Max Qty"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                    UniqueName="AIM_MaxQty" DataFormatString="{0:N0}" Visible="false">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                               <%-- <telerik:GridBoundColumn DataField="AIM_FaceValue" HeaderStyle-Width="80px" HeaderText="Minimum Application Amount"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                    UniqueName="FaceValue" Visible="true" DataFormatString="{0:N0}">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn DataField="AID_MinApplication" HeaderStyle-Width="110px"
                                    HeaderText="Min Amt" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="false" UniqueName="AID_MinApplication" Visible="false">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Maxamount" HeaderStyle-Width="110px" HeaderText="Max Amt"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                    UniqueName="Maxamount" Visible="false">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIM_TradingInMultipleOf" HeaderStyle-Width="70px"
                                    HeaderText=" Multiples of" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="false" UniqueName="AIM_TradingInMultipleOf" Visible="true">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn DataField="AIM_OpenDate" DataFormatString="{0:D}" HeaderStyle-Width="80px"
                                    CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                    HeaderText="Opening Date" SortExpression="AIM_OpenDate" UniqueName="AIM_OpenDate">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridDateTimeColumn>
                                <telerik:GridDateTimeColumn DataField="AIM_CloseDate" DataFormatString="{0:D}" HeaderStyle-Width="80px"
                                    CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                    HeaderText="Closing Date" UniqueName="AIM_CloseDate" SortExpression="AIM_CloseDate">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridDateTimeColumn>
                                <telerik:GridDateTimeColumn DataField="Timing" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                    HeaderStyle-Width="110px" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="false" HeaderText="Timing" UniqueName="Timing" SortExpression="Timing">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn DataField="IsDematFacilityAvail" HeaderStyle-Width="30px"
                                    HeaderText="Is Demat" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="false" UniqueName="IsDematFacilityAvail" Visible="true">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
        <table width="100%" id="tblgvCommMgmt" runat="server" visible="false">
            <tr>
                <td>
                    <telerik:RadGrid ID="gvCommMgmt" AllowSorting="false" runat="server" EnableLoadOnDemand="True"
                        AllowPaging="false" AutoGenerateColumns="False" EnableEmbeddedSkins="False" GridLines="None"
                        ShowFooter="True" PagerStyle-AlwaysVisible="true" ShowStatusBar="True" Skin="Telerik"
                        AllowFilteringByColumn="false" OnNeedDataSource="gvCommMgmt_OnNeedDataSource"
                        OnItemDataBound="gvCommMgmt_ItemDataBound" FooterStyle-BackColor="#2475C7" OnItemCommand="gvCommMgmt_OnItemCommand">
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="LiveBondList">
                        </ExportSettings>
                        <PagerStyle AlwaysVisible="True" />
                        <MasterTableView AllowMultiColumnSorting="false" AllowSorting="false" DataKeyNames="AID_SeriesFaceValue,AID_IssueDetailId,AIM_IssueId,AID_DefaultInterestRate,AID_Tenure,AIM_FaceValue,AIM_TradingInMultipleOf,AIM_MInQty,AIM_MaxQty,AIM_MaxApplNo,AIDCSR_Id,AID_Sequence,AIIC_InvestorCatgeoryId,AIIC_MaxBidAmount,AIIC_MInBidAmount"
                            AutoGenerateColumns="false" ShowFooter="true" Width="100%">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="AID_IssueDetailName" HeaderStyle-Width="45px"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                    HeaderText="Series" UniqueName="AID_IssueDetailName" SortExpression="AID_IssueDetailName">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CatColection" HeaderStyle-Width="70px" CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false" AutoPostBackOnFilter="false" HeaderText="Categories" UniqueName="CatColection"
                                    SortExpression="CatColection">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100Px" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CouponRateCollection" HeaderStyle-Width="120px"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                    HeaderText="Coupon Rate (%)" UniqueName="CouponRateCollection" SortExpression="CouponRateCollection">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100Px" Wrap="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="YieldatMatCollection" HeaderStyle-Width="120px"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                    HeaderText="Yield at Maturity (%)" UniqueName="YieldatMatCollection" SortExpression="YieldatMatCollection">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="BidCollection" HeaderStyle-Width="60px" CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false" AutoPostBackOnFilter="false" HeaderText="Bidding Rates"
                                    UniqueName="BidCollection" SortExpression="BidCollection" Visible="false">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="AID_Sequence" HeaderStyle-Width="60px"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                    HeaderText="Sequence" UniqueName="AID_Sequence" SortExpression="AID_Sequence">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="AIM_IssueId" HeaderStyle-Width="60px"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                    HeaderText="Scrip ID" UniqueName="AIM_IssueId" SortExpression="AIM_IssueId">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width=" " Wrap="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="PI_IssuerId" HeaderStyle-Width="70px"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                    HeaderText="Issuer Id" UniqueName="PI_IssuerId" SortExpression="PI_IssuerId">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="PI_IssuerCode" HeaderStyle-Width="70px"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                    HeaderText="Issuer" UniqueName="PI_IssuerCode" SortExpression="PI_IssuerCode">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="AID_IssueDetailId" HeaderStyle-Width="60px"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                    HeaderText="Series" UniqueName="AID_IssueDetailId" SortExpression="AID_IssueDetailId">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="AIM_FaceValue" HeaderStyle-Width="40px"
                                    HeaderText="Face Value" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="false" UniqueName="AIM_FaceValue" DataFormatString="{0:N0}">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AID_Tenure" HeaderStyle-Width="70px" CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false" AutoPostBackOnFilter="false" HeaderText="Tenure" UniqueName="AID_Tenure"
                                    SortExpression="AID_Tenure">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AID_CouponFreq" HeaderStyle-Width="85px" CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false" AutoPostBackOnFilter="false" HeaderText="Frequency of Coupon Payment"
                                    UniqueName="AID_CouponFreq" SortExpression="AID_CouponFreq">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CouponRate" HeaderStyle-Width="90px" CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false" AutoPostBackOnFilter="false" HeaderText="Coupon Rate(%)"
                                    UniqueName="CouponRate" SortExpression="CouponRate" Visible="false">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="AID_RenewCouponRate" HeaderStyle-Width="100px"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                    HeaderText="Renew Coupon Rate(%)" UniqueName="AID_RenewCouponRate" SortExpression="AID_RenewCouponRate">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="AID_LockingPeriod" HeaderStyle-Width="100px"
                                    HeaderText="Lock-in Period" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                    DataFormatString="{0:N0}" AutoPostBackOnFilter="false" UniqueName="AID_LockingPeriod">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="AID_DefaultInterestRate" HeaderStyle-Width="80px"
                                    HeaderText="Yield at Call(%)" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="false" UniqueName="AID_DefaultInterestRate">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CatSubTypeCode" HeaderStyle-Width="105px" HeaderText="SubType Code"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                    UniqueName="CatSubTypeCode" Visible="true">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIDCSR_RedemptionDate" HeaderStyle-Width="105px"
                                    HeaderText="Redemption Date Note" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="false" UniqueName="AIDCSR_RedemptionDate" Visible="true">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIDCSR_RedemptionAmount " HeaderStyle-Width="105px"
                                    HeaderText="Redemption Amount(Per Bond)" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="false" UniqueName="AIDCSR_RedemptionAmount " Visible="true">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="LockinPeriodCollection " HeaderStyle-Width="105px"
                                    HeaderText="Lock In Period" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="false" UniqueName="LockinPeriodCollection " Visible="true">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn AllowFiltering="false" DataField="YieldAtMAturity" HeaderStyle-Width="100px"
                                    UniqueName="YieldAtMAturity" HeaderText="Yield at Maturity(%)" Visible="false">
                                    <%-- <ItemTemplate>
                                    <asp:TextBox ID="txtYieldAtMAturity" runat="server" ForeColor="White" MaxLength="5" OnTextChanged="txtYieldAtMAturity_TextChanged"
                                        Text='<%# Bind("YieldAtMAturity")%>' Width="50px" BackColor="Gray"></asp:TextBox>
                                </ItemTemplate>--%>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="lblYieldAtMAturity" Text="Total"></asp:Label>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="AID_YieldatBuyBack" HeaderStyle-Width="105px"
                                    HeaderText="Yield at BuyBack(%)" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="false" UniqueName="AID_YieldatBuyBack">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="AID_CallOption" HeaderStyle-Width="80px"
                                    HeaderText="Call Option" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="false" UniqueName="AID_CallOption">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="AID_BuyBackFacility" HeaderStyle-Width="120px"
                                    HeaderText="Is Buy Back Facility" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="false" UniqueName="AID_BuyBackFacility">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AID_SeriesFaceValue " HeaderStyle-Width="50px"
                                    HeaderText="Face Value" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="false" UniqueName="AID_SeriesFaceValue ">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="AIM_MInQty" HeaderStyle-Width="100px"
                                    HeaderText="Min Qty(across all series)" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="false" UniqueName="AIM_MInQty" DataFormatString="{0:N0}">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="AIM_MaxQty" HeaderStyle-Width="100px"
                                    HeaderText="Max Quantity" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="false" UniqueName="AIM_MaxQty" DataFormatString="{0:N0}">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                  <telerik:GridBoundColumn  DataField="categorywisePrice" HeaderStyle-Width="130px"
                                    HeaderText="Min-Max Category Amt.(Across All Series)" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="false" UniqueName="categorywisePrice" HeaderStyle-Wrap="true" >
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="AIM_TradingInMultipleOf" HeaderStyle-Width="110px"
                                    HeaderText="Multiple allowed" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="false" UniqueName="AIM_TradingInMultipleOf">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="70px"
                                    UniqueName="Quantity" HeaderText="Enter Purchase Qty" HeaderStyle-Wrap="true"  >
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQuantity" runat="server" OnTextChanged="txtQuantity_TextChanged"
                                            ForeColor="White" MaxLength="5" Text='<%# Bind("COID_Quantity")%>' Width="50px"
                                            AutoPostBack="true" BackColor="Gray" OnKeypress="javascript:return isNumberKey(event);" ></asp:TextBox>
                                        <%--  <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="*Required"
                                        ClientValidationFunction="ValidateTextValue(this)"></asp:CustomValidator>--%>
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator" ControlToValidate="txtQuantity"
                                        ValidationGroup="gvCommMgmt" ErrorMessage="<br />Please enter a Quantity" Display="Dynamic"
                                        runat="server" CssClass="rfvPCG">
                                    </asp:RequiredFieldValidator>--%>
                                    </ItemTemplate>
                                    <FooterTemplate >
                                        <asp:Label runat="server" ID="lblQuantity" ></asp:Label>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="70px"
                                    FooterText="" UniqueName="Amount" HeaderText="Purchase Amt" FooterAggregateFormatString="{0:N2}"
                                    HeaderStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAmount" runat="server" ReadOnly="true" ForeColor="White" BackColor="Gray"
                                            Width="50px" Font-Bold="true" Text='<%# Bind("COID_AmountPayable")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="lblAmount" AutoPostBack="true"></asp:Label>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="COID_ExchangeRefrenceNo" HeaderStyle-Width="60px"
                                    CurrentFilterFunction="Contains" ShowFilterIcon="true" AutoPostBackOnFilter="true"
                                    HeaderText="Exchange Refrence No." UniqueName="COID_ExchangeRefrenceNo" SortExpression="COID_ExchangeRefrenceNo"
                                    Visible="false" HeaderStyle-Wrap="true">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn HeaderStyle-Width="100px" Text="Delete" ButtonType="PushButton"
                                    ConfirmText="Do you want to delete" CommandName="Delete" Visible="false" UniqueName="DeleteBid">
                                </telerik:GridButtonColumn>
                                <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                    Visible="false" UniqueName="Check" HeaderText="Check Order">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbOrderCheck" runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="false" DataField="" Visible="false" HeaderStyle-Width="100px"
                                    UniqueName="AmountAtMaturity" HeaderText="Amount at Maturity">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbconfirmOrder" runat="server" Text="Confirm Order"></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <EditFormSettings>
                                <EditColumn CancelImageUrl="Cancel.gif" EditImageUrl="Edit.gif" InsertImageUrl="Update.gif"
                                    UpdateImageUrl="Update.gif">
                                </EditColumn>
                            </EditFormSettings>
                            <PagerStyle AlwaysVisible="True" />
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                            <Resizing AllowColumnResize="true" />
                        </ClientSettings>
                        <FilterMenu EnableEmbeddedSkins="False">
                        </FilterMenu>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
        </asp:Panel>
        <table width="100%">
            <tr>
                <td class="Page_Left_Padding">
                </td>
                <td class="leftField">
                    <asp:Label ID="lblRemarks" runat="server" Text="Remarks:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtRemarks" Width="400px" TextMode="MultiLine" MaxLength="300" Height="40px"
                        onkeydown="return (event.keyCode!=13);" runat="server" CssClass="txtField" TabIndex="40"></asp:TextBox>
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
            <tr>
                <td class="Page_Left_Padding">
                </td>
                <td id="tdsubmit" runat="server" class="leftField">
                    <asp:Label ID="Label3" runat="server" Text="Confirm Your Order :" CssClass="FieldName"></asp:Label>
                    <asp:Button ID="btnConfirmOrder" runat="server" Text="Submit" OnClientClick="return  PreventClicks(); Validate(); "
                        OnClick="btnConfirmOrder_Click" CssClass="PCGButton" ValidationGroup="btnConfirmOrder" />
                </td>
                <td id="tdAddMore" runat="server" class="rightField">
                    <asp:Button ID="btnAddMore" runat="server" Text="Add More" CssClass="PCGMediumButton"
                        ValidationGroup="btnConfirmOrder" OnClick="btnAddMore_Click" OnClientClick="return  PreventClicks(); Validate(); " />
                </td>
                <td>
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="PCGButton" OnClick="btnUpdate_OnClick"
                        ValidationGroup="btnConfirmOrder,ddlBrokerCode" Visible="false" OnClientClick="javascript: return  ValGroup();" />
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdnPortfolioId" runat="server" />
        <asp:HiddenField ID="hdnApplicationNo" runat="server" />
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnConfirmOrder" />
        <asp:PostBackTrigger ControlID="btnAddMore" />
        <asp:PostBackTrigger ControlID="btnUpdate" />
    </Triggers>
</asp:UpdatePanel>
<asp:HiddenField ID="txtTotAmt" runat="server" />
<asp:HiddenField ID="hdnType" runat="server" />
<asp:HiddenField ID="hdnSchemeCode" runat="server" />
<asp:HiddenField ID="hdnAccountId" runat="server" />
<asp:HiddenField ID="hdnAmcCode" runat="server" />
<asp:HiddenField ID="hdnSchemeName" runat="server" />
<asp:HiddenField ID="hdnSchemeSwitch" runat="server" />
<asp:HiddenField ID="hdnBankName" runat="server" />
<asp:HiddenField ID="hdnIsSubscripted" runat="server" />
<asp:HiddenField ID="txtAgentId" runat="server" OnValueChanged="txtAgentId_ValueChanged1" />
<asp:HiddenField ID="hdnAplicationNo" runat="server" OnValueChanged="txtAgentId_ValueChanged1" />
<asp:HiddenField ID="hidValidCheck" runat="server" EnableViewState="true" />
<asp:HiddenField ID="HiddenField1" runat="server" OnValueChanged="HiddenField1_ValueChanged1" />
<asp:HiddenField ID="hidAmt" runat="server" />
<asp:HiddenField ID="hdnOrderId" runat="server" />
