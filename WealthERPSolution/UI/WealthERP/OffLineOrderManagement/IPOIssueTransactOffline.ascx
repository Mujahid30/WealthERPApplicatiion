<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IPOIssueTransactOffline.ascx.cs"
    Inherits="WealthERP.OffLineOrderManagement.IPOIssueTransactOffline" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server" EnablePageMethods="true">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<%@ Register Src="~/Customer/CustomerType.ascx" TagName="Customerprofileadd" TagPrefix="uc" %>

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

<script type="text/javascript" language="javascript">

    function openpopupAddBank() {
        var custId = document.getElementById("<%= txtCustomerId.ClientID %>").value
        window.open('PopUp.aspx?PageId=AddBankAccount&bankId=0&action=OfflineMF&custId=' + custId, 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return false;
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

<%--<script type="text/javascript">
window.onload = function () {
var div = document.getElementById('<%= this.div_position.ClientID %>');

var div_position = document.getElementById("div_position");
    var position = parseInt('<%=Request.Form["div_position"] %>');
    if (isNaN(position)) {
        position = 0;
    }
    div.scrollTop = position;
    div.onscroll = function () {
        div_position.value = div.scrollTop;
    };
};
</script>--%>

<script type="text/javascript">
    function abc() {
        var x = 0, y = 0;
        if (typeof (window.pageYOffset) == 'number') {
            // Netscape
            x = window.pageXOffset;
            y = window.pageYOffset;
        } else if (document.body && (document.body.scrollLeft || document.body.scrollTop)) {
            // DOM
            x = document.body.scrollLeft;
            y = document.body.scrollTop;
        } else if (document.documentElement && (document.documentElement.scrollLeft || document.documentElement.scrollTop)) {
            // IE6 standards compliant mode
            x = document.documentElement.scrollLeft;
            y = document.documentElement.scrollTop;
        }
        alert(x);
        alert(y);

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

<style type="text/css">
    .Page_Left_Padding
    {
        width: 10%;
    }
    .Page_Right_Padding
    {
        width: 5%;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
    <ContentTemplate>
        <table width="100%" id="tblIPOOrder" runat="server">
            <tr>
                <td colspan="6">
                    <div class="divPageHeading">
                        <table cellspacing="0" cellpadding="3" width="100%">
                            <tr>
                                <td align="left">
                                    IPO Order Entry
                                </td>
                                <td>
                                    <div class="divViewEdit" style="float: right; padding-right: 50px">
                                        <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_OnClick" CssClass="LinkButtons"
                                            Text="Edit" Visible="false"></asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr id="tblMessagee" runat="server" visible="false">
                <td colspan="6">
                    <table width="100%" style="padding-top: 20px;">
                        <tr id="trSumbitSuccess">
                            <td align="center">
                                <div id="msgRecordStatus" class="success-msg" align="center" runat="server">
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
                            &nbsp; Customer Details
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
                    <asp:TextBox ID="txtFirstName" runat="server" MaxLength="130" CssClass="txtField"
                        TabIndex="1" Width="60%"></asp:TextBox>
                    <span id="Span14" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtFirstName"
                        ErrorMessage="<br />Please enter the First Name" Display="Dynamic" runat="server"
                        CssClass="rfvPCG" ValidationGroup="CustomerProfileSubmit">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblPanNum" runat="server" CssClass="FieldName" Text="PAN:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPanNumber" runat="server" CssClass="txtField" MaxLength="10"
                        TabIndex="2"></asp:TextBox>
                    <span id="Span13" class="spnRequiredField">*</span> &nbsp;
                    <%--<asp:CheckBox ID="chkdummypan" runat="server" Visible="false" CssClass="txtField"
                            Text="Dummy PAN" AutoPostBack="true" TabIndex="9" />--%>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvPanNumber" ControlToValidate="txtPanNumber" ErrorMessage="Please enter a PAN Number"
                        Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="CustomerProfileSubmit">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                        CssClass="rfvPCG" ErrorMessage="Please check PAN Format" ControlToValidate="txtPanNumber"
                        ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}" ValidationGroup="CustomerProfileSubmit">
                    </asp:RegularExpressionValidator>
                    <asp:Label ID="lblPanDuplicate" runat="server" CssClass="Error" Text="PAN Number already exists"
                        Visible="false"></asp:Label>
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
            <tr>
                <td class="Page_Left_Padding">
                </td>
                <td class="leftField">
                    <asp:Label ID="lblCustomerType" runat="server" CssClass="FieldName" Text="Customer Type:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:RadioButton ID="rbtnIndividual" runat="server" CssClass="txtField" Text="Individual"
                        GroupName="grpCustomerType" AutoPostBack="true" OnCheckedChanged="rbtnIndividual_CheckedChanged"
                        CausesValidation="false" TabIndex="3" />
                    &nbsp;&nbsp;
                    <asp:RadioButton ID="rbtnNonIndividual" runat="server" CssClass="txtField" Text="Non Individual"
                        GroupName="grpCustomerType" AutoPostBack="true" OnCheckedChanged="rbtnNonIndividual_CheckedChanged"
                        TabIndex="4" CausesValidation="false" />
                </td>
                <td class="leftField">
                    <asp:Label ID="lblCustomerSubType" runat="server" CssClass="FieldName" Text="Customer Sub Type:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlCustomerSubType" runat="server" CssClass="cmbField" TabIndex="5"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerSubType_SelectedIndexChanged">
                    </asp:DropDownList>
                    <span id="Span19" class="spnRequiredField">*</span> &nbsp;
                    <br />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlCustomerSubType"
                        ErrorMessage="Please select a Customer Sub-Type" Operator="NotEqual" ValueToCompare="Select"
                        CssClass="cvPCG" Display="Dynamic" ValidationGroup="CustomerProfileSubmit"></asp:CompareValidator>
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
            <tr>
                <td class="Page_Left_Padding">
                </td>
                <td class="leftField">
                    <asp:Label ID="Label4" runat="server" Text="Beneficiary Acct. No.:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtDpClientId" runat="server" CssClass="txtField" TabIndex="21"></asp:TextBox>
                    <span id="Span17" class="spnRequiredField">*</span>
                    <asp:RegularExpressionValidator ID="rev" runat="server" ControlToValidate="txtDpClientId"
                        ValidationGroup="btnsubmit" ErrorMessage="Special Character are not allowed!"
                        CssClass="cvPCG" Display="Dynamic" ValidationExpression="^[a-zA-Z0-9]+(?:--?[a-zA-Z0-9]+)*$" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtDpClientId"
                        ErrorMessage="</br>Beneficiary Acct. No. Required" CssClass="cvPCG" ValidationGroup="btnsubmitdemate"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td class="leftField">
                    &nbsp;<asp:Label ID="lblDepositoryName" runat="server" Text="Depository Name:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlDepositoryName" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlDepositoryName_SelectedIndexChanged" TabIndex="25">
                    </asp:DropDownList>
                    <span id="Span18" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlDepositoryName"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please select Depository Name"
                        InitialValue="Select" ValidationGroup="btnsubmitdemate">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
            <tr>
                <td class="Page_Left_Padding">
                </td>
                <td class="leftField">
                    &nbsp;<asp:Label ID="lblDPId" runat="server" Text="DP Id:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtDPId" runat="server" CssClass="txtField" TabIndex="20"></asp:TextBox>
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
                        AutoPostBack="True" TabIndex="13">
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
                        ErrorMessage="<br />Please Enter a agent code" Display="Dynamic" runat="server"
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
            <tr>
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
                        OnSelectedIndexChanged="ddlIssueList_OnSelectedIndexChanged" TabIndex="30">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvIssueList" runat="server" ControlToValidate="ddlIssueList"
                        ErrorMessage="</br>Please select the Issue Name" CssClass="rfvPCG" Display="Dynamic"
                        ValidationGroup="btnConfirmOrder" InitialValue="Select"></asp:RequiredFieldValidator>
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
            <tr>
                <td class="Page_Left_Padding">
                </td>
                <td class="leftField">
                    <asp:Label runat="server" ID="lblBrokerCode" CssClass="FieldName" Text="Select Broker:"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:DropDownList ID="ddlBrokerCode" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
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
                        runat="server" CssClass="txtField" OnKeypress="javascript:return isNumberKey(event);"
                        TabIndex="31"></asp:TextBox>
                    <span id="Span2" class="spnRequiredField">*</span>
                    <asp:RegularExpressionValidator ID="revPan" runat="server" Display="Dynamic" ValidationGroup="btnConfirmOrder"
                        ErrorMessage="<br/>Please Enter Numeric" ControlToValidate="txtApplicationNo"
                        CssClass="rfvPCG" ValidationExpression="^([0-9]*[1-9])\d*$">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtApplicationNo"
                        ErrorMessage="<br />Please Enter Application No." Display="Dynamic" runat="server"
                        CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
                    <br />
                    <asp:Label ID="lblApplicationDuplicate" runat="server" CssClass="Error" Text="Application Number already exists"></asp:Label>
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
                        OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged" Width="185" TabIndex="33">
                        <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                        <asp:ListItem Text="Cheque/Demand Draft" Value="CQ"></asp:ListItem>
                        <asp:ListItem Text="ASBA" Value="ES"></asp:ListItem>
                    </asp:DropDownList>
                    <span id="Span10" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator13" runat="server" ControlToValidate="ddlPaymentMode"
                        CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select  Mode of Payment"
                        Operator="NotEqual" ValidationGroup="btnConfirmOrder" ValueToCompare="Select"></asp:CompareValidator>
                </td>
                <td id="Td3" class="leftField">
                    <asp:Label ID="lblBankAccount" Text="Bank Account No.:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td id="Td4" class="rightField">
                    <asp:TextBox ID="txtBankAccount" runat="server" CssClass="txtField" onkeydown="return (event.keyCode!=13);"
                        OnKeypress="javascript:return isNumberKey(event);" MaxLength="9" TabIndex="34"></asp:TextBox>
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
            <tr id="trPINo" runat="server" visible="false">
                <td class="Page_Left_Padding">
                </td>
                <td class="leftField">
                    <asp:Label ID="lblPaymentNumber" runat="server" Text="Cheque/Demand Draft No.: "
                        CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPaymentNumber" onkeydown="return (event.keyCode!=13);" OnKeypress="javascript:return isNumberKey(event);"
                        runat="server" MaxLength="6" CssClass="txtField" TabIndex="35"></asp:TextBox>
                    <span id="Span12" class="spnRequiredField">*</span>
                    <asp:RegularExpressionValidator ID="revtxtPaymentNumber" runat="server" Display="Dynamic"
                        ValidationGroup="btnConfirmOrder" ErrorMessage="<br/>Please Enter Numeric" ControlToValidate="txtPaymentNumber"
                        CssClass="rfvPCG" ValidationExpression="^([0-9]*[1-9])\d*$">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtPaymentNumber"
                        ErrorMessage="<br />Please Enter Cheque/Demand Draft No." Display="Dynamic" runat="server"
                        CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblPIDate" runat="server" Text="Cheque Date:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <telerik:RadDatePicker ID="txtPaymentInstDate" onkeydown="return (event.keyCode!=13);"
                        CssClass="txtField" runat="server" Culture="English (United States)" Skin="Telerik"
                        AutoPostBack="true" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                        OnSelectedDateChanged="txtPaymentInstDate_OnSelectedDateChanged" TabIndex="36">
                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                            Skin="Telerik" EnableEmbeddedSkins="false">
                        </Calendar>
                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                    <span id="Span11" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator14" runat="server" ErrorMessage="<br/>Please enter a valid date."
                        Type="Date" ControlToValidate="txtPaymentInstDate" CssClass="cvPCG" Operator="DataTypeCheck"
                        ValueToCompare="" Display="Dynamic" ValidationGroup="btnConfirmOrder" Enabled="true"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtPaymentInstDate"
                        ErrorMessage="<br />Please Enter Cheque Date." Display="Dynamic" runat="server"
                        CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
            <tr id="trASBA" runat="server" visible="false">
                <td class="Page_Left_Padding">
                </td>
                <td class="leftField">
                    <asp:Label ID="lblASBANo" Text="ASBA Bank A/c No.:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtASBANO" onkeydown="return (event.keyCode!=13);" runat="server"
                        MaxLength="16" CssClass="txtField" TabIndex="37"></asp:TextBox>
                    <span id="Span5" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtASBANO"
                        ErrorMessage="<br />Please Enter Account No." Display="Dynamic" runat="server"
                        CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblASBALocation" runat="server" CssClass="FieldName" Text="Location:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtASBALocation" onkeydown="return (event.keyCode!=13);" runat="server"
                        CssClass="txtField" AutoComplete="Off" AutoPostBack="True" OnTextChanged="txtASBALocation_OnTextChanged"
                        TabIndex="38">
                    </asp:TextBox><span id="Span6" class="spnRequiredField">*</span>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" TargetControlID="txtASBALocation"
                        WatermarkText="Enter few chars of Location" runat="server" EnableViewState="false">
                    </cc1:TextBoxWatermarkExtender>
                    <ajaxToolkit:AutoCompleteExtender ID="txtASBALocation_AutoCompleteExtender3" runat="server"
                        TargetControlID="txtASBALocation" ServiceMethod="GetASBALocation" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                        MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                        CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                        CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                        UseContextKey="True" DelimiterCharacters="" Enabled="True" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtASBALocation"
                        ErrorMessage="<br />Please Enter Location" Display="Dynamic" runat="server" CssClass="rfvPCG"
                        ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
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
                        AppendDataBoundItems="true" Width="380px" TabIndex="39">
                    </asp:DropDownList>
                    <span id="Span4" class="spnRequiredField">*</span>
                    <asp:ImageButton ID="imgAddBank" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                        AlternateText="Add" runat="server" ToolTip="Click here to Add Bank" OnClientClick="return openpopupAddBank()"
                        Height="15px" Width="15px" Visible="false" TabIndex="40"></asp:ImageButton>
                    <asp:ImageButton ID="imgBtnRefereshBank" ImageUrl="~/Images/refresh.png" AlternateText="Refresh"
                        runat="server" ToolTip="Click here to refresh Bank List" OnClick="imgBtnRefereshBank_OnClick"
                        OnClientClick="return closepopupAddBank()" Height="15px" Width="25px" Visible="false">
                    </asp:ImageButton>
                    <asp:CompareValidator ID="CompareValidator18" runat="server" ControlToValidate="ddlBankName"
                        CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a Bank"
                        Operator="NotEqual" ValidationGroup="btnConfirmOrder" ValueToCompare="Select"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ControlToValidate="ddlBankName"
                        CssClass="rfvPCG" ErrorMessage="<br />Please select a Bank" Display="Dynamic"
                        runat="server" InitialValue="" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblBranchName" runat="server" Text="Bank BranchName:" CssClass="FieldName"
                        Visible="false"></asp:Label>
                </td>
                <td class="rightField" id="tdBankBranch">
                    <asp:TextBox ID="txtBranchName" onkeydown="return (event.keyCode!=13);" runat="server"
                        CssClass="txtField" Visible="false" TabIndex="41"></asp:TextBox>
                    <asp:Label ID="lblBankBranchName" runat="server" Text="*" class="spnRequiredField"
                        Visible="false"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtBranchName"
                        CssClass="rfvPCG" ErrorMessage="<br />Please Enter Bank Branch" Display="Dynamic"
                        runat="server" InitialValue="" ValidationGroup="btnConfirmOrder" Visible="false"></asp:RequiredFieldValidator>
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td colspan="6">
                    <asp:Panel ID="pnlIPOControlContainer" runat="server" ScrollBars="Horizontal" Width="85%"
                        Visible="false">
                        <div id="divControlContainer" class="divControlContiner" runat="server">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <telerik:RadGrid ID="RadGridIPOIssueList" runat="server" AllowSorting="false" enableloadondemand="True"
                                            PageSize="10" AllowPaging="false" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                                            GridLines="None" ShowFooter="false" PagerStyle-AlwaysVisible="false" ShowStatusBar="True"
                                            Skin="Telerik" AllowFilteringByColumn="false">
                                            <MasterTableView AllowMultiColumnSorting="false" AllowSorting="false" DataKeyNames="AIM_IssueId,AIIC_PriceDiscountType,AIM_IsMultipleApplicationsallowed,AIIC_PriceDiscountValue,AIM_CutOffTime,AIM_TradingInMultipleOf,AIM_MInQty,AIM_MaxQty,AIIC_MInBidAmount,AIIC_MaxBidAmount,AIM_CloseDate"
                                                AutoGenerateColumns="false" Width="100%" PagerStyle-AlwaysVisible="false">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="AIM_IssueName" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
                                                        ShowFilterIcon="true" HeaderText="Issue Name" UniqueName="AIM_IssueName" SortExpression="AIM_IssueName">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="AIM_IssueSizeQty" HeaderStyle-Width="200px" HeaderText="Issue Size"
                                                        ShowFilterIcon="false" UniqueName="AIM_IssueSizeQty" Visible="true">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="AIM_IssueSizeAmt" HeaderStyle-Width="200px" HeaderText="Issue Size Amount"
                                                        ShowFilterIcon="false" UniqueName="AIM_IssueSizeAmt" Visible="true">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="AIM_Rating" HeaderStyle-Width="200px" ShowFilterIcon="false"
                                                        HeaderText="Grading" UniqueName="AIM_Rating" SortExpression="AIM_Rating">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="AIM_IsBookBuilding" HeaderStyle-Width="200px"
                                                        ShowFilterIcon="false" HeaderText="Basis" UniqueName="AIM_IsBookBuilding" SortExpression="AIM_IsBookBuilding">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="AIM_FaceValue" HeaderStyle-Width="200px" HeaderText="Face Value"
                                                        ShowFilterIcon="false" UniqueName="AIM_FaceValue" Visible="true">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="AIM_MInQty" HeaderStyle-Width="200px" HeaderText="Minimum Qty"
                                                        ShowFilterIcon="false" UniqueName="AIM_MInQty" Visible="true">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="AIM_TradingInMultipleOf" HeaderStyle-Width="200px"
                                                        HeaderText="Multiples of Qty" ShowFilterIcon="false" UniqueName="AIM_TradingInMultipleOf"
                                                        Visible="true">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="AIM_MaxQty" HeaderStyle-Width="200px" HeaderText="Maximum Qty"
                                                        ShowFilterIcon="false" UniqueName="AIM_MaxQty" Visible="true">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="AIM_FloorPrice" HeaderStyle-Width="200px" HeaderText="Min. Bid Price"
                                                        ShowFilterIcon="false" UniqueName="AIM_FloorPrice" Visible="true" DataType="System.Decimal"
                                                        DataFormatString="{0:0.00}">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="AIM_CapPrice" HeaderStyle-Width="200px" HeaderText="Max. Bid Price"
                                                        ShowFilterIcon="false" UniqueName="AIM_CapPrice" Visible="true" DataType="System.Decimal"
                                                        DataFormatString="{0:0.00}">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="AIM_FixedPrice" HeaderStyle-Width="200px" HeaderText="Max. Bid Price"
                                                        Visible="false" ShowFilterIcon="false" UniqueName="AIM_FixedPrice">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="AIIC_MInBidAmount" HeaderStyle-Width="200px"
                                                        HeaderText="Min. Bid Amount" ShowFilterIcon="false" UniqueName="AIIC_MInBidAmount"
                                                        Visible="true" DataType="System.Decimal" DataFormatString="{0:0.00}">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="AIIC_MaxBidAmount" HeaderStyle-Width="200px"
                                                        HeaderText="Max. Bid Amount" Visible="true" ShowFilterIcon="false" UniqueName="AIIC_MaxBidAmount">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="AIM_OpenDate" HeaderStyle-Width="200px" HeaderText="Open Date"
                                                        ShowFilterIcon="false" UniqueName="AIM_OpenDate" Visible="true">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="AIM_CloseDate" HeaderStyle-Width="200px" HeaderText="Close Date"
                                                        ShowFilterIcon="false" UniqueName="AIM_CloseDate" Visible="true">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DiscountType" HeaderStyle-Width="200px" HeaderText="Discount Type"
                                                        ShowFilterIcon="false" UniqueName="DiscountType" Visible="true">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DiscountValue" HeaderStyle-Width="200px" HeaderText="Discount Value/Bid Qty"
                                                        ShowFilterIcon="false" UniqueName="DiscountValue" Visible="true" DataFormatString="{0:0.00}">
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
                                <tr>
                                    <td>
                                        <telerik:RadGrid ID="RadGridIPOBid" runat="server" AllowSorting="True" enableloadondemand="True"
                                            PageSize="10" AllowPaging="false" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                                            GridLines="None" ShowFooter="true" PagerStyle-AlwaysVisible="false" ShowStatusBar="True"
                                            Skin="Telerik" AllowFilteringByColumn="false" FooterStyle-BackColor="#2475C7"
                                            OnItemDataBound="RadGridIPOBid_ItemDataBound">
                                            <MasterTableView AllowMultiColumnSorting="false" AllowSorting="false" AutoGenerateColumns="false"
                                                DataKeyNames="IssueBidNo,COID_TransactionType" Width="100%" PagerStyle-AlwaysVisible="false">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="BidOptions" AllowFiltering="false" HeaderStyle-Width="120px"
                                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                                        HeaderText="Bidding Options" UniqueName="BidOptions" SortExpression="BidOptions">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="80px"
                                                        Visible="true" UniqueName="CheckCutOff" HeaderText="Cut-Off" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbCutOffCheck" runat="server" Visible='<%# (Convert.ToInt32(Eval("IssueBidNo")) == 1)? true: false %>'
                                                                AutoPostBack="true" OnCheckedChanged="CutOffCheckBox_Changed" TabIndex="42" />
                                                            <%-- <a href="#" class="popper" data-popbox="divCutOffCheck">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
                                                <div id="divCutOffCheck" class="popbox">
                                                    <h2>
                                                        CUT-OFF!</h2>
                                                    <p>
                                                        1)If this box is checked then price filed will auto fill with Max Bid Price.</p>
                                                </div>--%>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                                        UniqueName="BidQuantity" HeaderText="Quantity" FooterAggregateFormatString="{0:N2}"
                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtBidQuantity" runat="server" Text='<%# Bind("BidQty")%>' CssClass='<% #(Convert.ToString(Eval("COID_TransactionType"))=="D") ? "txtDisableField" : "txtField" %>'
                                                                OnTextChanged="BidQuantity_TextChanged" TabIndex="43" AutoPostBack="true" onkeypress="return isNumberKey(event)"
                                                                ReadOnly='<% #(Convert.ToString(Eval("COID_TransactionType"))=="D") ? true : false %>'
                                                                ToolTip='<% #(Convert.ToString(Eval("COID_TransactionType"))=="D") ? "The bid Cannot be edited because it was Cancelled previously" : "" %>'>
                                            
                                                            </asp:TextBox>
                                                            <a href="#" class="popper" data-popbox="divBidQuantity" style="display: none">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
                                                            <div id="divBidQuantity" class="popbox">
                                                                <h2>
                                                                    BID-QUANTITY!</h2>
                                                                <p>
                                                                    1)Please enter value between MinQuantity and MaxQuantity.</p>
                                                            </div>
                                                            <asp:RangeValidator ID="rvQuantity" runat="server" ControlToValidate="txtBidQuantity"
                                                                ValidationGroup="btnConfirmOrder" Type="Integer" CssClass="rfvPCG" Text="*" ErrorMessage="BidQuantity should be between MinQuantity and MaxQuantity"
                                                                Display="Dynamic" />
                                                            <asp:RegularExpressionValidator ID="revtxtBidQuantity" ControlToValidate="txtBidQuantity"
                                                                runat="server" ErrorMessage="Please enter a valid bid quantity" Text="*" Display="Dynamic"
                                                                ValidationExpression="[0-9]*" CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RegularExpressionValidator>
                                                            <asp:CustomValidator ID="CVBidQtyMultiple" runat="server" OnServerValidate="CVBidQtyMultiple_ServerValidate"
                                                                Text="*" ErrorMessage="Please enter Quantity in multiples permissibile for this issue"
                                                                ControlToValidate="txtBidQuantity" Display="Dynamic" ValidationGroup="btnConfirmOrder"
                                                                CssClass="rfvPCG">                                                
                                                            </asp:CustomValidator>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                                        FooterText="" UniqueName="BidPrice" HeaderText="Price" FooterAggregateFormatString="{0:N2}"
                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtBidPrice" runat="server" Text='<%# Bind("BidPrice")%>' CssClass='<% #(Convert.ToString(Eval("COID_TransactionType"))=="D") ? "txtDisableField" : "txtField" %>'
                                                                AutoPostBack="true" OnTextChanged="BidPrice_TextChanged" TabIndex="44" onkeypress="return isNumberKey(event)"
                                                                ReadOnly='<% #(Convert.ToString(Eval("COID_TransactionType"))=="D") ? true : false %>'
                                                                ToolTip='<% #(Convert.ToString(Eval("COID_TransactionType"))=="D") ? "The bid Cannot be edited because it was Cancelled previously" : "" %>'> </asp:TextBox>
                                                            <a href="#" class="popper" data-popbox="divBidPrice" style="display: none">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
                                                            <div id="divBidPrice" class="popbox">
                                                                <h2>
                                                                    BID-PRICE!</h2>
                                                                <p>
                                                                    1)Please enter value between Min Bid Price and Min Max Price.
                                                                    <br />
                                                                    2)In case of cutoff cheked Max Bid price will be use for same field</p>
                                                            </div>
                                                            <asp:RangeValidator ID="rvBidPrice" runat="server" ControlToValidate="txtBidPrice"
                                                                ValidationGroup="btnConfirmOrder" Type="Double" CssClass="rfvPCG" Text="*" ErrorMessage="BidPrice should be between Min Bid Price and Min Max Price"
                                                                Display="Dynamic" />
                                                            <asp:RegularExpressionValidator ID="revtxtBidPrice" ControlToValidate="txtBidPrice"
                                                                runat="server" ErrorMessage="Please enter a valid bid price" Text="*" Display="Dynamic"
                                                                ValidationExpression="^\d+(\.\d{1,2})?$" CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RegularExpressionValidator>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblBidHighestValue" Text="Highest Bid Value"></asp:Label>
                                                        </FooterTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                                        FooterText="" UniqueName="BidAmountPayable" HeaderText="Amount Payable" FooterAggregateFormatString="{0:N2}"
                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtBidAmountPayable" runat="server" ReadOnly="true" CssClass="txtDisableField"
                                                                Text='<%# Bind("BidAmountPayable")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblFinalBidAmountPayable" Text="0"></asp:Label>
                                                            <asp:TextBox ID="txtFinalBidValue" runat="server" CssClass="txtField" Text="0" Visible="false"
                                                                TabIndex="46">
                                                            </asp:TextBox>
                                                        </FooterTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                                        FooterText="" UniqueName="BidAmount" HeaderText="Amount Bid" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtBidAmount" runat="server" ReadOnly="true" CssClass="txtDisableField"
                                                                Text='<%# Bind("BidAmount")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="COID_ExchangeRefrenceNo" HeaderStyle-Width="120px"
                                                        CurrentFilterFunction="Contains" ShowFilterIcon="true" AutoPostBackOnFilter="true"
                                                        HeaderText="ExchangeRefrenceNo" UniqueName="COID_ExchangeRefrenceNo" SortExpression="COID_ExchangeRefrenceNo"
                                                        Visible="false">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="Page_Left_Padding">
                </td>
                <td class="leftField">
                    <asp:Label ID="lblRemarks" runat="server" Text="Remarks:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtRemarks" Width="400px" TextMode="MultiLine" MaxLength="300" Height="40px"
                        onkeydown="return (event.keyCode!=13);" runat="server" CssClass="txtField" TabIndex="45"></asp:TextBox>
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
            <tr>
                <td class="Page_Left_Padding">
                </td>
                <td class="leftField">
                </td>
                <td class="rightField" colspan="3">
                    <asp:Button ID="btnConfirmOrder" runat="server" Text="Submit Order" OnClick="btnConfirmOrder_Click"
                        CssClass="PCGMediumButton" ValidationGroup="btnConfirmOrder, btnTC" OnClientClick="javascript: return  PreventClicks(); Validate();"
                        TabIndex="46" />
                    <asp:Button ID="btnAddMore" runat="server" Text="Add More" CssClass="PCGMediumButton"
                        ValidationGroup="btnConfirmOrder" OnClientClick="return  PreventClicks();" OnClick="btnAddMore_Click"
                        TabIndex="47" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_OnClick"
                        CssClass="PCGButton" Visible="false" ValidationGroup="btnConfirmOrder,ddlBrokerCode"
                        OnClientClick="javascript: return  ValGroup();" TabIndex="48" />
                </td>
                <td class="Page_Right_Padding">
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
    </Triggers>
</asp:UpdatePanel>
<tr>
    <td colspan="4">
        <asp:ValidationSummary ID="vsSummary" runat="server" CssClass="rfvPCG" Visible="true"
            ValidationGroup="btnConfirmOrder" ShowSummary="true" DisplayMode="BulletList" />
    </td>
</tr>
<asp:HiddenField ID="txtTotAmt" runat="server" />
<asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged1" />
<asp:HiddenField ID="hdnCustomerId" runat="server" />
<asp:HiddenField ID="hdnType" runat="server" />
<asp:HiddenField ID="hdnSchemeCode" runat="server" />
<asp:HiddenField ID="hdnPortfolioId" runat="server" />
<asp:HiddenField ID="hdnAccountId" runat="server" />
<asp:HiddenField ID="hdnAmcCode" runat="server" />
<asp:HiddenField ID="hdnSchemeName" runat="server" />
<asp:HiddenField ID="hdnSchemeSwitch" runat="server" />
<asp:HiddenField ID="hdnBankName" runat="server" />
<asp:HiddenField ID="hdnIsSubscripted" runat="server" />
<asp:HiddenField ID="txtSwitchSchemeCode" runat="server" />
<asp:HiddenField ID="txtAgentId" runat="server" OnValueChanged="txtAgentId_ValueChanged1" />
<asp:HiddenField ID="hdnAplicationNo" runat="server" OnValueChanged="txtAgentId_ValueChanged1" />
<asp:HiddenField ID="hidValidCheck" runat="server" EnableViewState="true" />
<asp:HiddenField ID="HiddenField1" runat="server" OnValueChanged="HiddenField1_ValueChanged1" />
<asp:HiddenField ID="hidAmt" runat="server" />
<asp:HiddenField ID="hdnCloseDate" runat="server" />
<asp:HiddenField ID="div_position" runat="server" />
