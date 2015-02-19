<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDIssueTransactOffline.ascx.cs"
    Inherits="WealthERP.OffLineOrderManagement.NCDIssueTransactOffline" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    .style1
    {
        width: 49%;
    }
    .cmbField
    {
        margin-left: 0px;
    }
</style>
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

    window.onload = function() {
        try {
            //get target base control.
            TargetBaseControl =
           document.getElementById('<%=this.gvDematDetailsTeleR.ClientID %>');

        }
        catch (err) {
            TargetBaseControl = null;
        }
    }

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

    function GetCustomerId(source, eventArgs) {
        isItemSelected = true;
        //         document.getElementById("lblgetPan").innerHTML = "";
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();

        return false;
    }

    function ValidateAssociateName() {
        //        var x = document.forms["form1"]["TextBoxName"].value;
        document.getElementById("<%=  lblAssociatetext.ClientID %>").value = eventArgs.get_value();
        document.getElementById("lblAssociatetext").innerHTML = "AgentCode Required";
        return true;
    }

    function openpopupAddCustomer() {

        window.open('PopUp.aspx?AddMFCustLinkId=mf&pageID=CustomerType&', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')


    }
    function openpopupAddDematAccount() {

        var customerId = document.getElementById("<%=txtCustomerId.ClientID %>").value;
        var customerPortfolioId = document.getElementById("<%=hdnPortfolioId.ClientID %>").value;
        if (customerId != 0) {
            window.open('PopUp.aspx?PageId=AddDematAccountDetails&CustomerId=' + customerId + '&CustomerPortfolioId=' + customerPortfolioId, 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')

        }
        else {
            alert("Please Select the Customer From Search")
        }
    }
    
</script>

<script type="text/javascript">

    var isItemSelected = false;

    //Handler for textbox blur event
    function checkItemSelected(txtPanNumber) {
        var returnValue = true;
        if (!isItemSelected) {

            if (txtPanNumber.value != "") {
                txtPanNumber.focus();
                alert("Please select Pan Number from the Pan list only");
                txtPanNumber.value = "";
                returnValue = false;
            }
        }
        return returnValue;
    }
    
</script>

<script type="text/javascript" language="javascript">

    function openpopupAddBank() {
        var custId = document.getElementById("<%= txtCustomerId.ClientID %>").value
        window.open('PopUp.aspx?PageId=AddBankAccount&bankId=0&action=OfflineMF&custId=' + custId, 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return false;

        //        document.getElementById("<%= HiddenField1.ClientID %>").value = 1;

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
    function ValidateTermsConditions(sender, args) {

        if (document.getElementById("<%=chkTermsCondition.ClientID %>").checked == true) {
            args.IsValid = true;
        } else {
            args.IsValid = false;
        }
    }
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

<table width="100%">
    <tr>
        <td colspan="5">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            NCD Order Entry
                        </td>
                        <td style="float:right;">
                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="LinkButtons" OnClick="lnkEdit_LinkButtons" Visible="false"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%" id="trOfficeUse" runat="server" visible="false">
        <tr>
            <td colspan="5">
                <div class="divSectionHeading" style="vertical-align: text-bottom">
                    For Office Use
                </div>
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 20%">
            </td>
            <td id="tdauthentication" runat="server">
                <%--<asp:CheckBox ID="chkAuthentication" runat="server" CssClass="cmbFielde" Text="Authenticate"
                    OnCheckedChanged="chkAuthentication_OnCheckedChanged" AutoPostBack="true" />--%>
                <asp:RadioButton ID="rbtnAuthentication" runat="server" CssClass="txtField" Text="Authenticate"
                    GroupName="rejAut" AutoPostBack="true" OnCheckedChanged="rbtnAuthentication_OnCheckedChanged" Checked="true"/>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="rbtnReject" runat="server" CssClass="txtField" Text="Rejected"
                    GroupName="rejAut" AutoPostBack="true" OnCheckedChanged="rbtnReject_CheckedChanged" />
                <%--  <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="location"
                    ErrorMessage="Please select one of the radio button" Display="Dynamic"></asp:CustomValidator>--%>
            </td>
            <td align="right">
                <asp:Label runat="server" ID="lblBrokerCode" CssClass="FieldName" Text="Select Broker:"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlBrokerCode" runat="server" CssClass="cmbField">
                </asp:DropDownList>
                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="ddlBrokerCode"
                    ErrorMessage="<br />Please Select Broker" Display="Dynamic" runat="server" CssClass="rfvPCG"
                    ValidationGroup="btnSubmitAuthenticate" InitialValue=""></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr>
            <td id="tdauthenticationDetails" runat="server" align="right" style="width: 20%">
                <asp:Label ID="lblAuthenticated" runat="server" Text="Authenticated/Rejected By:"
                    CssClass="FieldName"></asp:Label>
            </td>
            <td style="width: 38%;">
                <asp:Label ID="lblAuthenticatedBy" runat="server" CssClass="FieldName"></asp:Label><br />
                <asp:Label ID="lblAuthenticationDate" runat="server" CssClass="FieldName"></asp:Label>
            </td>
            <td id="tdlblReject" visible="false" align="right">
                <asp:Label ID="lblRejectRes" runat="server" Text="Reject Reseaon:" CssClass="FieldName">           
                </asp:Label>
            </td>
            <td id="tdtxtReject" visible="false">
                <asp:TextBox ID="txtRejectReseaon" runat="server" CssClass="txtField" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtRejectReseaon"
                    ErrorMessage="<br />Please Enter Remarks" Display="Dynamic" runat="server" CssClass="rfvPCG"
                    ValidationGroup="btnSubmitAuthenticate" InitialValue=""></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button runat="server" ID="btnSubmitAuthenticate" CssClass="PCGButton" runat="server"
                    Text="Submit" OnClick="btnSubmitAuthenticate_btnSubmit" ValidationGroup="btnSubmitAuthenticate" Visible="false"/>
            </td>
        </tr>
    </table>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table id="tblMessage" width="100%" runat="server" visible="false" style="padding-top: 20px;">
            <tr id="trSumbitSuccess">
                <td align="center">
                    <div id="msgRecordStatus" class="success-msg" align="center" runat="server">
                    </div>
                </td>
            </tr>
            <tr id="trinsufficentmessage" runat="server" visible="false">
                <td align="center">
                    <asp:Label ID="lblinsufficent" runat="server" ForeColor="Red" Text="Order cannot be processed due to insufficient balance"></asp:Label>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:Panel ID="pnl_OrderSection" runat="server" class="Landscape" Width="100%" Height="80%"
    ScrollBars="None">
    <table width="100%">
        <tr>
            <td colspan="4">
                <div class="divSectionHeading" style="vertical-align: text-bottom">
                    Customer Details
                </div>
            </td>
        </tr>
        <tr id="trpan" runat="server" visible="true">
            <td class="leftField" style="width: 20%">
                <asp:Label ID="lblPansearch" runat="server" Text="PAN Search:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:TextBox ID="txtPansearch" runat="server" CssClass="txtField" AutoComplete="Off"
                    AutoPostBack="True" onclientClick="ShowIsa()" OnTextChanged="OnAssociateTextchanged1"
                    TabIndex="2">
                </asp:TextBox><span id="Span1" class="spnRequiredField">*</span>
                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" TargetControlID="txtPansearch"
                    WatermarkText="Enter few chars of Pan" runat="server" EnableViewState="false">
                </cc1:TextBoxWatermarkExtender>
                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtPansearch"
                    ServiceMethod="GetAdviserCustomerPan" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                    MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="1" CompletionInterval="0"
                    CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                    CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                    UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                    Enabled="True" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPansearch"
                    ErrorMessage="<br />Please Enter PAN" Display="Dynamic" runat="server" CssClass="rfvPCG"
                    ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
            </td>
            <td class="leftField" style="width: 20%">
                <asp:Label ID="label2" runat="server" Text="Customer Name: " CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:Label ID="lblgetcust" runat="server" Text="" CssClass="FieldName" onclientClick="CheckPanno()"></asp:Label>
            </td>
        </tr>
        <tr id="trPanExist" runat="server" visible="false">
            <td colspan="2" align="center">
                <asp:Label ID="lblPANNotExist" runat="server" CssClass="cvPCG" ForeColor="Red"></asp:Label>
                <asp:LinkButton ID="lnkAddcustomer" runat="server" CssClass="FieldName" Text="Add Customer"
                    OnClick="lnkAddcustomer_OnClick"></asp:LinkButton>
            </td>
        </tr>
        <tr id="trCustomerAdd" runat="server" visible="false">
            <td colspan="4" style="padding-left: 114px">
                <table>
                    <tr>
                        <td align="right" style="width: 20%;">
                            <asp:Label ID="lblCustomerType" runat="server" CssClass="FieldName" Text="Customer Type:"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButton ID="rbtnIndividual" runat="server" CssClass="txtField" Text="Individual"
                                GroupName="grpCustomerType" AutoPostBack="true" OnCheckedChanged="rbtnIndividual_CheckedChanged" />
                            &nbsp;&nbsp;
                            <asp:RadioButton ID="rbtnNonIndividual" runat="server" CssClass="txtField" Text="Non Individual"
                                GroupName="grpCustomerType" AutoPostBack="true" OnCheckedChanged="rbtnNonIndividual_CheckedChanged" />
                        </td>
                        <td>
                            <asp:Label ID="lblCustomerSubType" runat="server" CssClass="FieldName" Text="Customer Sub Type:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCustomerSubType" runat="server" CssClass="cmbField">
                            </asp:DropDownList>
                            <span id="Span19" class="spnRequiredField">*</span> &nbsp;
                            <br />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlCustomerSubType"
                                ErrorMessage="Please select a Customer Sub-Type" Operator="NotEqual" ValueToCompare="Select"
                                CssClass="cvPCG" Display="Dynamic" ValidationGroup="CustomerProfileSubmit"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr id="trBranchlist" runat="server" visible="false">
                        <td>
                            <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Branch Name:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAdviserBranchList" AutoPostBack="true" runat="server" CssClass="cmbField">
                            </asp:DropDownList>
                            <span id="Span8" class="spnRequiredField">*</span>
                            <br />
                            <asp:CompareValidator ID="ddlAdviserBranchList_CompareValidator2" runat="server"
                                ControlToValidate="ddlAdviserBranchList" ErrorMessage="Please select a Branch"
                                Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"
                                ValidationGroup="CustomerProfileSubmit">
                            </asp:CompareValidator>
                        </td>
                    </tr>
                    <tr id="trRMlist" runat="server" visible="false">
                        <td>
                            <asp:Label ID="lblRMName" runat="server" CssClass="FieldName" Text="Select RM:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAdviseRMList" runat="server" CssClass="cmbField">
                            </asp:DropDownList>
                            <span id="Span13" class="spnRequiredField">*</span>
                            <br />
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlAdviseRMList"
                                ErrorMessage=" " Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG"
                                Display="Dynamic" ValidationGroup="CustomerProfileSubmit">
                            </asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 20%;">
                            <asp:Label ID="lblPanNum" runat="server" CssClass="FieldName" Text="PAN:"></asp:Label>
                        </td>
                        <td class="rightField" width="51%">
                            <asp:TextBox ID="txtPanNumber" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
                            <span id="Span14" class="spnRequiredField">*</span> &nbsp;
                            <asp:CheckBox ID="chkdummypan" runat="server" Visible="false" CssClass="txtField"
                                Text="Dummy PAN" AutoPostBack="true" />
                            <br />
                            <asp:RequiredFieldValidator ID="rfvPanNumber" ControlToValidate="txtPanNumber" ErrorMessage="Please enter a PAN Number"
                                Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="CustomerProfileSubmit">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                                CssClass="rfvPCG" ErrorMessage="Please check PAN Format" ControlToValidate="txtPanNumber"
                                ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}" ValidationGroup="CustomerProfileSubmit">
                            </asp:RegularExpressionValidator>
                            <asp:Label ID="lblPanDuplicate" runat="server" CssClass="Error" Text="PAN Number already exists"></asp:Label>
                        </td>
                        <td colspan="2">
                            <table>
                                <tr id="trIndividualName" runat="server">
                                    <td align="right" style="width: 41%;">
                                        <asp:Label ID="lblName" runat="server" CssClass="FieldName" Text="Name:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFirstName" runat="server" MaxLength="75" CssClass="txtField"></asp:TextBox>
                                        <span id="Span15" class="spnRequiredField">*</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtFirstName"
                                            ErrorMessage="<br />Please enter the First Name" Display="Dynamic" runat="server"
                                            CssClass="rfvPCG" ValidationGroup="CustomerProfileSubmit">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr id="trNonIndividualName" runat="server">
                                    <td align="right" style="width: 41%;">
                                        <asp:Label ID="lblCompanyName" runat="server" CssClass="FieldName" Text="Company Name:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCompanyName" CssClass="txtField" runat="server"></asp:TextBox>
                                        <span id="Span16" class="spnRequiredField">*</span>
                                        <br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtCompanyName"
                                            ErrorMessage="Please enter the Company Name" Display="Dynamic" runat="server"
                                            CssClass="rfvPCG" ValidationGroup="CustomerProfileSubmit">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                CssClass="PCGButton" ValidationGroup="CustomerProfileSubmit" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trCust" runat="server" visible="false">
            <td class="leftField" style="width: 20%">
                <asp:Label ID="lblCustomer" runat="server" Text="Customer Name: " CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:TextBox ID="txtCustomerName" onkeydown="return (event.keyCode!=13);" runat="server"
                    CssClass="txtField" AutoComplete="Off" onclientClick="ShowIsa()" AutoPostBack="True"
                    TabIndex="2">
            
                 
                </asp:TextBox><span id="spnCustomer" class="spnRequiredField">*</span>
                <asp:ImageButton ID="btnImgAddCustomer" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                    AlternateText="Add" CausesValidation="false" runat="server" ToolTip="Click here to Add Customer"
                    OnClientClick="return openpopupAddCustomer()" Height="15px" Width="15px" TabIndex="3">
                </asp:ImageButton>
                <cc1:TextBoxWatermarkExtender ID="txtCustomer_water" TargetControlID="txtCustomerName"
                    WatermarkText="Enter few chars of Customer" runat="server" EnableViewState="false">
                </cc1:TextBoxWatermarkExtender>
                <ajaxToolkit:AutoCompleteExtender ID="txtCustomerName_autoCompleteExtender" runat="server"
                    TargetControlID="txtCustomerName" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                    MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                    CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                    CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                    UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                    Enabled="True" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCustomerName"
                    ErrorMessage="<br />Please Enter Customer Name" Display="Dynamic" runat="server"
                    CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
            </td>
            <td class="leftField" style="width: 20%">
                <asp:Label ID="lblPan" runat="server" Text="PAN: " CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:Label ID="lblgetPan" runat="server" Text="" CssClass="FieldName"></asp:Label>
            </td>
            <%-- <td class="leftField" style="width: 20%">
            <asp:Label ID="lblRM" runat="server" Text="RM: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
            <asp:Label ID="lblGetRM" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>--%>
        </tr>
        <tr>
            <td class="leftField" style="width: 20%">
                <asp:Label ID="lblBranch" runat="server" Text="Branch: " CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:Label ID="lblGetBranch" runat="server" Text="" CssClass="FieldName"></asp:Label>
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td class="leftField" style="width: 20%">
                <asp:Label ID="lblAssociateSearch" runat="server" CssClass="FieldName" Text="Sub Broker Code:"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:TextBox ID="txtAssociateSearch" onkeydown="return (event.keyCode!=13);" runat="server"
                    CssClass="txtField" AutoComplete="Off" OnTextChanged="OnAssociateTextchanged"
                    AutoPostBack="True" TabIndex="4">
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
            <td class="leftField" style="width: 20%">
                <asp:Label ID="lblAssociate" runat="server" CssClass="FieldName" Text="Associate:"></asp:Label>
            </td>
            <td class="rightField" style="width: 15%">
                <asp:Label ID="lblAssociatetext" runat="server" CssClass="FieldName" Enabled="false"></asp:Label>
            </td>
        </tr>
        <tr id="trIsa" runat="server">
            <td class="leftField" style="width: 20%">
                <asp:Label ID="lblIsa" runat="server" CssClass="FieldName" Text="ISA No:"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:DropDownList ID="ddlCustomerISAAccount" runat="server" CssClass="cmbField" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlCustomerISAAccount_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp
                <asp:ImageButton ID="btnIsa" ImageUrl="~/App_Themes/Maroon/Images/user_add.png" AlternateText="Add"
                    runat="server" ToolTip="Click here to Request ISA" OnClick="ISA_Onclick" Height="15px"
                    Width="15px"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td colspan="4">
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" class="Landscape" Width="100%" Height="80%"
    ScrollBars="None" Visible="false">
    <table width="100%" cellspacing="10">
        <tr>
            <td colspan="3">
                <div class="divSectionHeading" style="vertical-align: text-bottom">
                    Demat Details
                </div>
            </td>
        </tr>
        <tr id="tdlnkbtn" runat="server">
            <td class="leftField" style="width: 20%; visibility: hidden;">
                <asp:LinkButton ID="lnkBtnDemat" runat="server" OnClick="lnkBtnDemat_onClick" CssClass="LinkButtons"
                    Text="Click to select Demat Details" CausesValidation="false"></asp:LinkButton>
            </td>
            <td id="Td5" class="rightField" style="width: 20%" colspan="2">
                <asp:ImageButton ID="ImageButton4" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                    AlternateText="Add Demat Account" runat="server" ToolTip="Click here to Add Demat Account"
                    OnClick="ImageButton1_OnClick" Height="15px" Width="15px" TabIndex="3" CausesValidation="false">
                </asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="padding-left: 154px" id="tdDemate" runat="server" visible="false">
                <table>
                    <tr>
                        <td class="leftField" style="width: 150px;">
                            <asp:Label ID="lblDpName" runat="server" Text="DP Name:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField" width="51%">
                            <asp:TextBox ID="txtDpName" runat="server" CssClass="txtField" Width="250px"></asp:TextBox>
                            <span id="Span25" class="spnRequiredField">*</span>
                        </td>
                        <td class="leftField" style="width: 150px;">
                            &nbsp;<asp:Label ID="lblDPId" runat="server" Text="DP Id:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField">
                            <asp:TextBox ID="txtDPId" runat="server" CssClass="txtField"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftField">
                            <asp:Label ID="Label16" runat="server" Text="Beneficiary Acct No:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField">
                            <asp:TextBox ID="txtDpClientId" runat="server" CssClass="txtField"></asp:TextBox>
                            <span id="Span26" class="spnRequiredField">*</span>
                            <asp:RegularExpressionValidator ID="rev" runat="server" ControlToValidate="txtDpClientId"
                                ValidationGroup="btnsubmit" ErrorMessage="Special Character are not allowed!"
                                CssClass="cvPCG" Display="Dynamic" ValidationExpression="^[a-zA-Z0-9]+(?:--?[a-zA-Z0-9]+)*$" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtDpClientId"
                                ErrorMessage="Client Id Required" CssClass="cvPCG" ValidationGroup="btnsubmitdemate"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td class="leftField">
                            &nbsp;<asp:Label ID="lblAccountOpeningDate" runat="server" Text="Account Opening Date:"
                                CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField">
                            <!-- calAccountOpeningDate -->
                            <telerik:RadDatePicker ID="txtAccountOpeningDate" CssClass="txtTo" runat="server"
                                Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                                ShowAnimation-Type="Fade" MinDate="1900-01-01">
                                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                                </Calendar>
                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                            <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="txtAccountOpeningDate"
                                Type="Date" Operator="DataTypeCheck" ErrorMessage="Please Enter a Valid Date"
                                Display="Dynamic" CssClass="cvPCG" ValidationGroup="btnsubmitdemate" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftField">
                            <asp:Label ID="lblIsHeldJointly" runat="server" Text="Is Held Jointly:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField">
                            <asp:RadioButton ID="rbtnYes" runat="server" Text="Yes" GroupName="IsHeldJointly"
                                CssClass="txtField" AutoPostBack="True" OnCheckedChanged="RadioButton_CheckChanged" />
                            <asp:RadioButton ID="rbtnNo" runat="server" Text="No" GroupName="IsHeldJointly" CssClass="txtField"
                                AutoPostBack="True" OnCheckedChanged="rbtnNo_CheckChanged" OnLoad="rbtnNo_Load"
                                Checked="true" />
                        </td>
                        <td class="leftField">
                            &nbsp;<asp:Label ID="lblDepositoryName" runat="server" Text="Depository Name:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField">
                            <asp:DropDownList ID="ddlDepositoryName" runat="server" CssClass="cmbField" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlDepositoryName_SelectedIndexChanged">
                            </asp:DropDownList>
                            <span id="Span27" class="spnRequiredField">*</span>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="ddlDepositoryName"
                                CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please select Depository Name"
                                InitialValue="Select" ValidationGroup="btnsubmitdemate">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftField">
                            &nbsp;<asp:Label ID="lblModeOfHolding" runat="server" Text="Mode Of Holding:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField">
                            <asp:DropDownList ID="ddlModeOfHolding" runat="server" AutoPostBack="True" CssClass="cmbField">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td class="rightField">
                            <asp:Button ID="btnDemateDetails" runat="server" Text="Submit" OnClick="DematebtnSubmit_Click"
                                ValidationGroup="btnsubmitdemate" CssClass="PCGButton" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <telerik:RadGrid ID="gvDematDetailsTeleR" runat="server" AllowAutomaticInserts="false"
                    AllowFilteringByColumn="false" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="False"
                    EnableEmbeddedSkins="false" EnableHeaderContextMenu="true" fAllowAutomaticDeletes="false"
                    GridLines="none" ShowFooter="true" ShowStatusBar="false" Skin="Telerik" OnItemDataBound="gvDematDetailsTeleR_OnItemDataBound"
                    FooterStyle-BackColor="#2475C7">
                    <%--<HeaderContextMenu EnableEmbeddedSkins="False">
                                </HeaderContextMenu>--%>
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false" DataKeyNames="CEDA_DematAccountId,CEDA_DPClientId"
                        Width="99%">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" DataField="Action" HeaderStyle-Width="30px"
                                UniqueName="Action">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDematId" runat="server" OnCheckedChanged="btnAddDemat_Click"
                                        AutoPostBack="true" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="CEDA_DPName" FilterControlWidth="50px" HeaderStyle-Width="67px" HeaderText="DP Name"
                                ShowFilterIcon="false" SortExpression="CEDA_DPName" UniqueName="CEDA_DPName">
                                <HeaderStyle Width="67px" />
                                <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="100px" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="CEDA_DepositoryName" FilterControlWidth="120px" HeaderStyle-Width="140px"
                                HeaderText="Depository Name" ShowFilterIcon="false" SortExpression="CEDA_DepositoryName"
                                UniqueName="CEDA_DepositoryName">
                                <HeaderStyle Width="100px" />
                                <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="67px" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="CEDA_DPClientId" FilterControlWidth="50px" HeaderStyle-Width="67px"
                                HeaderText="Beneficiary Acct No" ShowFilterIcon="false" SortExpression="CEDA_DPClientId"
                                UniqueName="CEDA_DPClientId">
                                <HeaderStyle Width="120px" />
                                <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="100px" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                DataField="CEDA_DPId" FilterControlWidth="50px" HeaderStyle-Width="67px" HeaderText="DP Id"
                                ShowFilterIcon="false" SortExpression="CEDA_DPId" UniqueName="CEDA_DPId">
                                <HeaderStyle Width="140px" />
                                <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="140px" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" DataField="XMOH_ModeOfHolding"
                                HeaderStyle-Width="145px" HeaderText="Mode of holding" ShowFilterIcon="false"
                                SortExpression="XMOH_ModeOfHolding" UniqueName="XMOH_ModeOfHolding">
                                <HeaderStyle Width="145px" />
                                <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="145px" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" DataField="CEDA_AccountOpeningDate"
                                DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="145px" HeaderText="Account Opening Date"
                                ShowFilterIcon="false" SortExpression="CEDA_AccountOpeningDate" UniqueName="CEDA_AccountOpeningDate"
                                Visible="false">
                                <HeaderStyle Width="145px" />
                                <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="145px" Wrap="false" />
                            </telerik:GridBoundColumn>
                        </Columns>
                        <EditFormSettings>
                            <EditColumn CancelImageUrl="Cancel.gif" EditImageUrl="Edit.gif" InsertImageUrl="Update.gif"
                                UpdateImageUrl="Update.gif">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="true" ScrollHeight="70px" UseStaticHeaders="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
        <tr>
            <td class="leftField" style="width: 25%">
                <asp:Label ID="lblDpClientId" runat="server" Text="Beneficiary Acct No.:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:TextBox ID="txtDematid" Enabled="false" onkeydown="return (event.keyCode!=13);"
                    runat="server" CssClass="txtField"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ControlToValidate="txtDematid"
                    ErrorMessage="<br />Please Select Demat from the List" Display="Dynamic" runat="server"
                    CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlJointHolderNominee" runat="server" class="Landscape" Width="100%"
    Height="80%" ScrollBars="None" Visible="false">
    <table width="100%" cellspacing="10">
        <tr>
            <td>
                <div class="divSectionHeading" style="vertical-align: text-bottom">
                    Joint Holder/Nominee Details
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="gvAssociate" runat="server" CssClass="RadGrid" GridLines="Both"
                    Visible="false" Width="90%" AllowPaging="True" PageSize="20" AllowSorting="True"
                    AutoGenerateColumns="false" ShowStatusBar="true" Skin="Telerik">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                        FileName="Family Associates" Excel-Format="ExcelML">
                    </ExportSettings>
                    <%--<MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                        CommandItemDisplay="None">--%>
                    <MasterTableView DataKeyNames="CDAA_Id,CEDA_DematAccountId,CDAA_Name,CDAA_PanNum,Sex,CDAA_DOB,RelationshipName,AssociateType,CDAA_AssociateTypeNo,CDAA_IsKYC,SexShortName,CDAA_AssociateType,XR_RelationshipCode"
                        Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemSettings-ShowRefreshButton="false">
                        <Columns>
                            <telerik:GridBoundColumn DataField="CDAA_Name" HeaderText="Member name" UniqueName="AssociateName"
                                SortExpression="AssociateName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AssociateType" HeaderText="Associate Type" UniqueName="AssociateType"
                                SortExpression="AssociateType">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CDAA_PanNum" HeaderText="PAN" UniqueName="CDAA_PanNum"
                                SortExpression="CDAA_PanNum">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CDAA_IsKYC" HeaderText="IsKYC" UniqueName="CDAA_IsKYC"
                                SortExpression="CDAA_IsKYC">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CDAA_DOB" HeaderText="Date Of Birth" UniqueName="CDAA_DOB"
                                SortExpression="CDAA_DOB" DataFormatString="{0:d}">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="RelationshipName" HeaderText="Relationship" AllowFiltering="false"
                                UniqueName="RelationshipName" SortExpression="RelationshipName">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
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
<%--<asp:Panel ID="Panel1" runat="server" class="Landscape" Width="100%"
    Height="80%" ScrollBars="None">
    <table width="100%">
        <tr>
            <td colspan="5">
                <div class="divSectionHeading" style="vertical-align: text-bottom">
                    Order Detail Section
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td class="leftField" style="width: 20%">
                <asp:Label ID="Label6" runat="server" Text="Select Issue Name:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" colspan="5" style="width: 20%">
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" CssClass="cmbExtraLongField"
                    OnSelectedIndexChanged="ddlIssueList_OnSelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlIssueList"
                    ErrorMessage="Please select the Issue Name" CssClass="rfvPCG" Display="Dynamic"
                    ValidationGroup="btnConfirmOrder" InitialValue="Select"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="leftField" style="width: 20%">
                <asp:Label ID="Label7" runat="server" Text="Application No.:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:TextBox ID="TextBox1" MaxLength="9" onkeydown="return (event.keyCode!=13);"
                    runat="server" CssClass="txtField" OnKeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                <span id="Span17" class="spnRequiredField">*</span>
                <asp:RegularExpressionValidator ID="revPan" runat="server" Display="Dynamic" ValidationGroup="btnConfirmOrder"
                    ErrorMessage="<br/>Please Enter Numeric" ControlToValidate="txtApplicationNo"
                    CssClass="rfvPCG" ValidationExpression="^([0-9]*[1-9])\d*$">
                </asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtApplicationNo"
                    ErrorMessage="<br />Please Enter Application No" Display="Dynamic" runat="server"
                    CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="Label8" runat="server" Visible="false" CssClass="Error" Text="Application Number already exists"></asp:Label>
            </td>
            <td id="Td1" class="leftField" style="width: 20%" runat="server" visible="false">
                <asp:Label ID="Label9" runat="server" Text="Depository Type: " CssClass="FieldName"></asp:Label>
            </td>
            <td id="Td2" class="rightField" style="width: 20%" runat="server" visible="false">
                <%-- <asp:DropDownList ID="ddlDepositoryName" runat="server" CssClass="cmbField" AutoPostBack="true">
                </asp:DropDownList>--%>
<%--<asp:ImageButton ID="ImageButton1" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                    AlternateText="Add" runat="server" ToolTip="Click here to Add Depository Type"
                    OnClick="ImageddlSyndicate_Click" Height="15px" Width="15px"></asp:ImageButton>
                <br />--%>
<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlDepositoryName"
                    ErrorMessage="<br />Please Enter Depository Name" Display="Dynamic" runat="server"
                    CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>--%>
<%--<tr>
    <td class="leftField" style="width: 20%">
            <asp:Label ID="lblApplicationNo" runat="server" Text="Application No: "
                CssClass="FieldName"></asp:Label>
        </td>
       <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtApplicationNo" runat="server" CssClass="txtField" MaxLength="6" OnKeypress="javascript:return isNumberKey(event);"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtApplicationNo"
                ErrorMessage="<br />Please Enter Application No" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
        </td>
        <td id="Td1" class="leftField" style="width: 20%" runat="server" visible="false">
        <asp:Label ID="lblDepository" runat="server" Text="Depository Type: "
                CssClass="FieldName"></asp:Label>
        </td>
        <td id="Td2" class="rightField" style="width: 20%" runat="server" visible="false">
        <asp:DropDownList ID="ddlDepositoryName" runat="server" CssClass="cmbField" AutoPostBack="true"></asp:DropDownList>
       
                 <asp:ImageButton ID="ImageddlSyndicate" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                AlternateText="Add" runat="server" ToolTip="Click here to Add Depository Type" OnClick="ImageddlSyndicate_Click"
                Height="15px" Width="15px"></asp:ImageButton>
            <br />
       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlDepositoryName"
                ErrorMessage="<br />Please Enter Depository Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
        </td>
       
    </tr>
    <%--<tr>
    <td class="leftField" style="width: 20%">
    <asp:Label ID="lblDPId" runat="server" Text="DP ID: "
                CssClass="FieldName"></asp:Label>
    </td>
     <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtDPId" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        
        <td class="leftField" style="width: 20%">
        <asp:Label ID="Label4" runat="server" Text="DP ClientId: "
                CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
        <asp:TextBox ID="txtDPIDClientId" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>--%>
<%--   <tr>
            <td class="leftField" style="width: 20%">
                <asp:Label ID="Label10" runat="server" Text="Mode of Payment:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" CssClass="cmbField"
                    OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged">
                    <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                    <asp:ListItem Text="Cheque/Demand Draft" Value="CQ"></asp:ListItem>
                    <asp:ListItem Text="ASBA" Value="ES"></asp:ListItem>
                </asp:DropDownList>
                <span id="Span18" class="spnRequiredField">*</span>
                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlPaymentMode"
                    CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select  Mode Of Payment"
                    Operator="NotEqual" ValidationGroup="btnConfirmOrder" ValueToCompare="Select"></asp:CompareValidator>
            </td>
            <td id="Td3" class="leftField" style="width: 20%" runat="server" visible="false">
                <asp:Label ID="lblBankAccount" Text="Bank Account No." runat="server" CssClass="FieldName"></asp:Label>
            </td>
            <td id="Td4" class="rightField" style="width: 20%" runat="server" visible="false">
                <asp:TextBox ID="txtBankAccount" runat="server" CssClass="txtField" onkeydown="return (event.keyCode!=13);"
                    OnKeypress="javascript:return isNumberKey(event);" MaxLength="9"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr1" runat="server" visible="false">
            <td class="leftField" style="width: 20%">
                <asp:Label ID="Label11" runat="server" Text="Cheque/Demand Draft No.: "
                    CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:TextBox ID="TextBox2" onkeydown="return (event.keyCode!=13);" OnKeypress="javascript:return isNumberKey(event);"
                    runat="server" MaxLength="6" CssClass="txtField" TabIndex="16"></asp:TextBox>
                <span id="Span20" class="spnRequiredField">*</span>
                <asp:RegularExpressionValidator ID="revtxtPaymentNumber" runat="server" Display="Dynamic"
                    ValidationGroup="btnConfirmOrder" ErrorMessage="<br/>Please Enter Numeric" ControlToValidate="txtPaymentNumber"
                    CssClass="rfvPCG" ValidationExpression="^([0-9]*[1-9])\d*$">
                </asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="txtPaymentNumber"
                    ErrorMessage="<br />Please Enter Cheque/Demand Draft NO." Display="Dynamic" runat="server"
                    CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
            </td>
            <td class="leftField" style="width: 20%">
                <asp:Label ID="Label12" runat="server" Text="Cheque Date:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <telerik:RadDatePicker ID="RadDatePicker1" onkeydown="return (event.keyCode!=13);"
                    CssClass="txtField" runat="server" Culture="English (United States)" Skin="Telerik"
                    EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01" TabIndex="17">
                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                        Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
                <span id="Span21" class="spnRequiredField">*</span>
                <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="<br/>Please enter a valid date."
                    Type="Date" ControlToValidate="txtPaymentInstDate" CssClass="cvPCG" Operator="DataTypeCheck"
                    ValueToCompare="" Display="Dynamic" ValidationGroup="btnConfirmOrder" Enabled="true"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="txtPaymentInstDate"
                    ErrorMessage="<br />Please Enter Cheque Date." Display="Dynamic" runat="server"
                    CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="tr2" runat="server" visible="false">
            <td class="leftField" style="width: 20%">
                <asp:Label ID="Label13" Text="ASBA Bank A/c No.:" runat="server" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:TextBox ID="TextBox3" onkeydown="return (event.keyCode!=13);" runat="server"
                    MaxLength="16" CssClass="txtField"></asp:TextBox>
                <span id="Span22" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ControlToValidate="txtASBANO"
                    ErrorMessage="<br />Please Enter Account No." Display="Dynamic" runat="server"
                    CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
            </td>
            <td class="leftField" style="width: 20%">
                <asp:Label ID="lblASBALocation" runat="server" CssClass="FieldName" Text="Location:"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:TextBox ID="txtASBALocation" onkeydown="return (event.keyCode!=13);" runat="server"
                    CssClass="txtField" AutoComplete="Off" AutoPostBack="True" TabIndex="4">
                </asp:TextBox><span id="Span23" class="spnRequiredField">*</span>
                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" TargetControlID="txtASBALocation"
                    WatermarkText="Enter few chars of Location" runat="server" EnableViewState="false">
                </cc1:TextBoxWatermarkExtender>
                <ajaxToolkit:AutoCompleteExtender ID="txtASBALocation_AutoCompleteExtender3" runat="server"
                    TargetControlID="txtASBALocation" ServiceMethod="GetASBALocation" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                    MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                    CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                    CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                    UseContextKey="True" DelimiterCharacters="" Enabled="True" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ControlToValidate="txtASBALocation"
                    ErrorMessage="<br />Please Enter Location" Display="Dynamic" runat="server" CssClass="rfvPCG"
                    ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="tr3" runat="server">
            <td class="leftField" style="width: 20%">
                <asp:Label ID="Label14" runat="server" Text="Bank Name:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:DropDownList ID="DropDownList3" runat="server" CssClass="cmbField" AutoPostBack="false"
                    AppendDataBoundItems="true" TabIndex="18">
                </asp:DropDownList>
                <span id="Span24" class="spnRequiredField">*</span>
                <asp:ImageButton ID="ImageButton2" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                    AlternateText="Add" runat="server" ToolTip="Click here to Add Bank" OnClientClick="return openpopupAddBank()"
                    Height="15px" Width="15px" Visible="false"></asp:ImageButton>
                <%-- --%>
<%--    <asp:ImageButton ID="ImageButton3" ImageUrl="~/Images/refresh.png" AlternateText="Refresh"
                    runat="server" ToolTip="Click here to refresh Bank List" OnClick="imgBtnRefereshBank_OnClick"
                    OnClientClick="return closepopupAddBank()" Height="15px" Width="25px" TabIndex="19"
                    Visible="false"></asp:ImageButton>
                <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="ddlBankName"
                    CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a Bank"
                    Operator="NotEqual" ValidationGroup="btnConfirmOrder" ValueToCompare="Select"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ControlToValidate="ddlBankName"
                    CssClass="rfvPCG" ErrorMessage="<br />Please select an Bank" Display="Dynamic"
                    runat="server" InitialValue="" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
            </td>
            <td class="leftField" style="width: 20%">
                <asp:Label ID="Label15" runat="server" Text="Bank BranchName:" CssClass="FieldName"
                    Visible="false"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%" id="tdBankBranch">
                <asp:TextBox ID="TextBox4" onkeydown="return (event.keyCode!=13);" runat="server"
                    CssClass="txtField" Visible="false"></asp:TextBox>
                <%--<span id="Span3" class="spnRequiredField" visible="false">*</span>--%>
<%--     <asp:Label ID="lblBankBranchName" runat="server" Text="*" class="spnRequiredField"
                    Visible="false"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ControlToValidate="txtBranchName"
                    CssClass="rfvPCG" ErrorMessage="<br />Please Enter Bank Branch" Display="Dynamic"
                    runat="server" InitialValue="" ValidationGroup="btnConfirmOrder" Visible="false"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
</asp:Panel>--%>
<asp:Panel ID="PnlNCDApplicationDetails" runat="server" class="Landscape" Width="100%"
    Height="80%" ScrollBars="None">
    <table width="100%">
        <tr>
            <td colspan="5">
                <div class="divSectionHeading" style="vertical-align: text-bottom">
                    Order Detail Section
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblIssueName" runat="server" Text="Select Issue Name:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" colspan="5">
                <asp:DropDownList ID="ddlIssueList" runat="server" AutoPostBack="true" CssClass="cmbExtraLongField"
                    OnSelectedIndexChanged="ddlIssueList_OnSelectedIndexChanged">
                </asp:DropDownList>
                <span id="Span6" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="rfvIssueList" runat="server" ControlToValidate="ddlIssueList"
                    ErrorMessage="Please select the Issue Name" CssClass="rfvPCG" Display="Dynamic"
                    ValidationGroup="btnConfirmOrder" InitialValue="Select"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="leftField" style="width: 20%">
                <asp:Label ID="lblApplicationNo" runat="server" Text="Application No.: " CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:TextBox ID="txtApplicationNo" MaxLength="9" onkeydown="return (event.keyCode!=13);"
                    runat="server" CssClass="txtField" AutoPostBack="false" OnKeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                <span id="Span2" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtApplicationNo"
                    ErrorMessage="<br />Please Enter Application No" Display="Dynamic" runat="server"
                    CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
                <asp:Label ID="lblApplicationDuplicate" runat="server" CssClass="Error" Text="Application Number already exists"
                    Visible="false"></asp:Label>
            </td>
            <td class="leftField" style="width: 20%" runat="server" visible="false">
                <asp:Label ID="lblDepository" runat="server" Text="Depository Type: " CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%" runat="server" visible="false">
                <asp:DropDownList ID="DropDownList4" runat="server" CssClass="cmbField" AutoPostBack="true">
                </asp:DropDownList>
                <asp:ImageButton ID="ImageddlSyndicate" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                    AlternateText="Add" runat="server" ToolTip="Click here to Add Depository Type"
                    OnClick="ImageddlSyndicate_Click" Height="15px" Width="15px"></asp:ImageButton>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlDepositoryName"
                    ErrorMessage="<br />Please Enter Depository Name" Display="Dynamic" runat="server"
                    CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <%--<tr>
    <td class="leftField" style="width: 20%">
    <asp:Label ID="lblDPId" runat="server" Text="DP ID: "
                CssClass="FieldName"></asp:Label>
    </td>
     <td class="rightField" style="width: 20%">
            <asp:TextBox ID="txtDPId" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        
        <td class="leftField" style="width: 20%">
        <asp:Label ID="Label4" runat="server" Text="DP ClientId: "
                CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 20%">
        <asp:TextBox ID="txtDPIDClientId" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>--%>
        <tr>
            <td class="leftField" style="width: 20%">
                <asp:Label ID="Label5" runat="server" Text="Mode of Payment:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:DropDownList ID="ddlPaymentMode" runat="server" AutoPostBack="true" CssClass="cmbField"
                    OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged" Width="180">
                    <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                    <asp:ListItem Text="Cheque/Demand Draft" Value="CQ"></asp:ListItem>
                    <asp:ListItem Text="ASBA" Value="ES"></asp:ListItem>
                </asp:DropDownList>
                <span id="Span10" class="spnRequiredField">*</span>
                <asp:CompareValidator ID="CompareValidator13" runat="server" ControlToValidate="ddlPaymentMode"
                    CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select  Mode Of Payment"
                    Operator="NotEqual" ValidationGroup="btnConfirmOrder" ValueToCompare="Select"></asp:CompareValidator>
            </td>
            <td class="leftField" style="width: 20%" runat="server" visible="false">
                <asp:Label ID="lblAmount" Text="Amount" runat="server" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%" runat="server" visible="false">
                <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField" ReadOnly="true"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr id="trPINo" runat="server" visible="false">
            <td class="leftField" style="width: 20%">
                <asp:Label ID="lblPaymentNumber" runat="server" Text="Cheque/Demand Draft NO: " CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:TextBox ID="txtPaymentNumber" onkeydown="return (event.keyCode!=13);" runat="server"
                    MaxLength="6" OnKeypress="javascript:return isNumberKey(event);" CssClass="txtField"
                    TabIndex="16"></asp:TextBox>
                <span id="Span12" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtPaymentNumber"
                    ErrorMessage="<br />Please Enter Cheque/Demand Draft NO." Display="Dynamic" runat="server"
                    CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
            </td>
            <td class="leftField" style="width: 20%">
                <asp:Label ID="lblPIDate" runat="server" Text="Cheque Date:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <telerik:RadDatePicker ID="txtPaymentInstDate" onkeydown="return (event.keyCode!=13);"
                    CssClass="txtField" runat="server" Culture="English (United States)" Skin="Telerik"
                    EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01" TabIndex="17">
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
        </tr>
        <tr id="trASBA" runat="server" visible="false">
            <td class="leftField">
                <asp:Label ID="lblASBANo" Text="ASBA Bank A/c NO:" runat="server" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtASBANO" MaxLength="16" onkeydown="return (event.keyCode!=13);"
                    runat="server" CssClass="txtField"></asp:TextBox>
                <span id="Span5" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtASBANO"
                    ErrorMessage="<br />Please Enter Account No." Display="Dynamic" runat="server"
                    CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
            </td>
            <td colspan="3">
            </td>
        </tr>
        <tr id="trBankName" runat="server">
            <td class="leftField" style="width: 20%">
                <asp:Label ID="lblBankName" runat="server" Text="Bank Name:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:DropDownList ID="ddlBankName" runat="server" CssClass="cmbField" AutoPostBack="false"
                    AppendDataBoundItems="true" TabIndex="18" Width="380px">
                </asp:DropDownList>
                <span id="Span4" class="spnRequiredField">*</span>
                <asp:ImageButton ID="imgAddBank" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                    AlternateText="Add" runat="server" ToolTip="Click here to Add Bank" OnClientClick="return openpopupAddBank()"
                    Height="15px" Width="15px" Visible="false"></asp:ImageButton>
                <%-- --%>
                <asp:ImageButton ID="imgBtnRefereshBank" ImageUrl="~/Images/refresh.png" AlternateText="Refresh"
                    runat="server" ToolTip="Click here to refresh Bank List" OnClick="imgBtnRefereshBank_OnClick"
                    OnClientClick="return closepopupAddBank()" Height="15px" Width="25px" TabIndex="19"
                    Visible="false"></asp:ImageButton>
                <asp:CompareValidator ID="CompareValidator18" runat="server" ControlToValidate="ddlBankName"
                    CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a Bank"
                    Operator="NotEqual" ValidationGroup="btnConfirmOrder" ValueToCompare="Select"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ControlToValidate="ddlBankName"
                    CssClass="rfvPCG" ErrorMessage="<br />Please select an Bank" Display="Dynamic"
                    runat="server" InitialValue="" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
            </td>
            <td class="leftField" style="width: 20%">
                <asp:Label ID="lblBranchName" runat="server" Text="Bank BranchName:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:TextBox ID="txtBranchName" onkeydown="return (event.keyCode!=13);" runat="server"
                    CssClass="txtField"></asp:TextBox>
                <span id="Span3" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtBranchName"
                    CssClass="rfvPCG" ErrorMessage="<br />Please Enter Bank Branch" Display="Dynamic"
                    runat="server" InitialValue="" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlNCDOOrder" runat="server" class="Landscape" Width="100%" Height="80%"
    ScrollBars="None" Visible="false">
    <table width="100%">
        <tr>
            <td>
                <div class="divSectionHeading" style="vertical-align: text-bottom">
                    NCD Issue Details
                </div>
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
</asp:Panel>
<asp:Panel ID="pnlIssuList" runat="server" CssClass="Landscape" Width="100%" Visible="false">
    <table id="tblCommissionStructureRule" runat="server" width="100%">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <telerik:RadGrid ID="gvIssueList" runat="server" AllowSorting="false" enableloadondemand="True"
                                PageSize="10" AllowPaging="false" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                                GridLines="None" ShowFooter="false" PagerStyle-AlwaysVisible="true" ShowStatusBar="True"
                                Skin="Telerik" AllowFilteringByColumn="false">
                                <MasterTableView AllowMultiColumnSorting="false" AllowSorting="true" DataKeyNames="AIM_IssueId,AIM_IssueName,IssueTimeType,AIM_MInQty,AIM_MaxQty"
                                    AutoGenerateColumns="false" Width="100%">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="AIM_IssueName" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
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
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            UniqueName="CatCollection">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MinMaxCatCollection" HeaderStyle-Width="140px"
                                            HeaderText="Min-Max Qty(Across All Series)" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="false" UniqueName="MinMaxCatCollection">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
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
                                        <telerik:GridBoundColumn DataField="AIM_FaceValue" HeaderStyle-Width="80px" HeaderText="Minimum Application Amount"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                            UniqueName="FaceValue" Visible="true" DataFormatString="{0:N0}">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AID_MinApplication" HeaderStyle-Width="110px"
                                            HeaderText="Min Amt" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true" UniqueName="AID_MinApplication" Visible="false">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Maxamount" HeaderStyle-Width="110px" HeaderText="Max Amt"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                            UniqueName="Maxamount" Visible="false">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIM_TradingInMultipleOf" HeaderStyle-Width="110px"
                                            HeaderText=" Multiples of" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="false" UniqueName="AIM_TradingInMultipleOf" Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn DataField="AIM_OpenDate" DataFormatString="{0:D}" HeaderStyle-Width="110px"
                                            CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                            HeaderText="Opening Date" SortExpression="AIM_OpenDate" UniqueName="AIM_OpenDate">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridDateTimeColumn DataField="AIM_CloseDate" DataFormatString="{0:D}" HeaderStyle-Width="110px"
                                            CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                            HeaderText="Closing Date" UniqueName="AIM_CloseDate" SortExpression="AIM_CloseDate">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridDateTimeColumn DataField="Timing" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                            HeaderStyle-Width="110px" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="false" HeaderText="Timing" UniqueName="Timing" SortExpression="Timing">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn DataField="IsDematFacilityAvail" HeaderStyle-Width="110px"
                                            HeaderText="Is Demat" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="false" UniqueName="IsDematFacilityAvail" Visible="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <%-- <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="110px"
                                            UniqueName="Action" HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="llPurchase" runat="server" OnClick="llPurchase_Click" Text="Purchase"></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>--%>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlNCDTransact" runat="server" ScrollBars="Horizontal" Width="100%"
    Visible="false">
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="gvCommMgmt" AllowSorting="false" runat="server" EnableLoadOnDemand="True"
                    AllowPaging="false" AutoGenerateColumns="False" EnableEmbeddedSkins="False" GridLines="None"
                    ShowFooter="True" PagerStyle-AlwaysVisible="true" ShowStatusBar="True" Skin="Telerik"
                    AllowFilteringByColumn="false" OnNeedDataSource="gvCommMgmt_OnNeedDataSource"  FooterStyle-BackColor="#2475C7">
                    <HeaderContextMenu EnableEmbeddedSkins="False">
                    </HeaderContextMenu>
                    <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="LiveBondList">
                    </ExportSettings>
                    <PagerStyle AlwaysVisible="True" />
                    <MasterTableView AllowMultiColumnSorting="false" AllowSorting="false" DataKeyNames="AID_SeriesFaceValue,AID_IssueDetailId,AIM_IssueId,AID_DefaultInterestRate,AID_Tenure,AIM_FaceValue,AIM_TradingInMultipleOf,AIM_MInQty,AIM_MaxQty,AIM_MaxApplNo,AIDCSR_Id"
                        AutoGenerateColumns="false" Width="100%" ShowFooter="true">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <Columns>
                            <%--<telerik:GridBoundColumn visible="false" DataField="PFISM_SchemeId" HeaderStyle-Width="60px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Scheme" UniqueName=" SchemeId"
                                SortExpression="SeriesId">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>--%>
                            <%-- <telerik:GridBoundColumn Visible="false" DataField="CO_OrderId" HeaderStyle-Width="60px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="OrderNo" UniqueName="CO_OrderId"
                                SortExpression="CO_OrderId">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn DataField="AID_IssueDetailName" HeaderStyle-Width="60px"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                HeaderText="Series" UniqueName="AID_IssueDetailName" SortExpression="AID_IssueDetailName">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CatColection" HeaderStyle-Width="60px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="false" HeaderText="Categories" UniqueName="CatColection"
                                SortExpression="CatColection">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CouponRateCollection" HeaderStyle-Width="160px"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="false"
                                HeaderText="Coupon Rate (%)" UniqueName="CouponRateCollection" SortExpression="CouponRateCollection">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="YieldatMatCollection" HeaderStyle-Width="160px"
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
                            <telerik:GridBoundColumn Visible="false" DataField="AIM_FaceValue" HeaderStyle-Width="80px"
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
                            <telerik:GridBoundColumn DataField="AID_SeriesFaceValue " HeaderStyle-Width="120px"
                                HeaderText="Face Value" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AutoPostBackOnFilter="false" UniqueName="AID_SeriesFaceValue ">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="AIM_MInQty" HeaderStyle-Width="140px"
                                HeaderText="Min Qty(across all series)" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AutoPostBackOnFilter="false" UniqueName="AIM_MInQty" DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="AIM_MaxQty" HeaderStyle-Width="140px"
                                HeaderText="Max Quantity" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AutoPostBackOnFilter="false" UniqueName="AIM_MaxQty" DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="AIM_TradingInMultipleOf" HeaderStyle-Width="110px"
                                HeaderText="Multiple allowed" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AutoPostBackOnFilter="false" UniqueName="AIM_TradingInMultipleOf">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                UniqueName="Quantity" HeaderText="Enter Purchase Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQuantity" runat="server" OnTextChanged="txtQuantity_TextChanged"
                                        ForeColor="White" MaxLength="5" Text='<%# Bind("COID_Quantity")%>' Width="50px"
                                        AutoPostBack="true" BackColor="Gray" OnKeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                                    <%--  <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="*Required"
                                        ClientValidationFunction="ValidateTextValue(this)"></asp:CustomValidator>--%>
                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator" ControlToValidate="txtQuantity"
                                        ValidationGroup="gvCommMgmt" ErrorMessage="<br />Please enter a Quantity" Display="Dynamic"
                                        runat="server" CssClass="rfvPCG">
                                    </asp:RequiredFieldValidator>--%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label runat="server" ID="lblQuantity" ></asp:Label>
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                FooterText="" UniqueName="Amount" HeaderText="Purchase Amt" FooterAggregateFormatString="{0:N2}">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAmount" runat="server" ReadOnly="true" ForeColor="White" BackColor="Gray"
                                        Width="50px" Font-Bold="true" Text='<%# Bind("COID_AmountPayable")%>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label runat="server" ID="lblAmount" AutoPostBack="true"></asp:Label>
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
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
<table>
    <tr>
        <td>
        </td>
    </tr>
</table>
<div align="center">
    <asp:Label ID="lb1AvailbleCat" runat="server" CssClass="FieldName" Visible="false"></asp:Label></div>
<%--</div>--%>
<table>
    <tr class="spaceUnder" id="trTermsCondition" runat="server" visible="false">
        <td align="left" style="width: 35%" visible="false">
            <asp:CheckBox ID="chkTermsCondition" runat="server" Font-Bold="True" Font-Names="Shruti"
                Enabled="false" Checked="false" ForeColor="#145765" Text="" ToolTip="Click 'Terms & Conditions' to proceed further"
                CausesValidation="true" />
            <asp:LinkButton ID="lnkTermsCondition" CausesValidation="false" Text="Terms & Conditions"
                runat="server" CssClass="txtField" OnClick="lnkTermsCondition_Click" ToolTip="Click here to accept terms & conditions"></asp:LinkButton>
            <span id="Span9" class="spnRequiredField">*</span>
        </td>
        <td colspan="3" style="width: 85%" align="left" visible="false">
            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please read terms & conditions"
                ClientValidationFunction="ValidateTermsConditions" EnableClientScript="true"
                OnServerValidate="TermsConditionCheckBox" Display="Dynamic" ValidationGroup="btnConfirmOrder"
                CssClass="rfvPCG">
                Please read terms & conditions
            </asp:CustomValidator>
        </td>
    </tr>
    <tr id="trSubmit" runat="server" visible="true">
        <td>
            <asp:Label ID="lb1CustOffTimeMsg" Visible="false" runat="server" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:LinkButton runat="server" ID="lnlBack" CssClass="LinkButtons" Text="Click here to view the issue list"
                Visible="false"></asp:LinkButton>
        </td>
    </tr>
</table>
<asp:Panel ID="PnlSubmit" runat="server" class="Landscape" Width="100%" Height="80%"
    ScrollBars="None">
    <table width="80%">
        <tr>
            <td class="leftField" style="width: 11%">
                <asp:Label ID="lblRemarks" runat="server" Text="Remarks:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 11%">
                <asp:TextBox ID="txtRemarks" Width="400px" TextMode="MultiLine" MaxLength="300" Height="40px"
                    onkeydown="return (event.keyCode!=13);" runat="server" CssClass="txtField"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td id="tdsubmit" runat="server" class="leftField">
                <asp:Label ID="Label3" runat="server" Text="Confirm Your Order :" CssClass="FieldName"></asp:Label>
                <asp:Button ID="btnConfirmOrder" runat="server" Text="Submit" OnClientClick="return  PreventClicks(); Validate(); "
                    OnClick="btnConfirmOrder_Click" CssClass="PCGButton" ValidationGroup="btnConfirmOrder" />
            </td>
            <td id="tdAddMore" runat="server" class="rightField">
                <asp:Button ID="btnAddMore" runat="server" Text="Add NCD order" CssClass="PCGMediumButton"
                    ValidationGroup="btnConfirmOrder" OnClick="btnAddMore_Click" OnClientClick="return  PreventClicks(); Validate(); " />
            </td>
        </tr>
        <tr>
        <td>
              <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="PCGButton" OnClick="btnUpdate_OnClick"   ValidationGroup="btnConfirmOrder" Visible="false"/>
        </td></tr>
    </table>
</asp:Panel>
<table>
    <tr runat="server" visible="false">
        <%--<td id="tdsubmit" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Confirm Your Order :" CssClass="FieldName"></asp:Label>
        <asp:Button ID="btnConfirmOrder" runat="server" Text="Submit" OnClick="btnConfirmOrder_Click"
            CssClass="PCGButton" />
    </td>--%>
        <td id="tdupdate" runat="server" visible="false">
            <asp:Label ID="Label1" runat="server" Text="Confirm Your Order :" CssClass="FieldName"></asp:Label>
      
        </td>
    </tr>
</table>
<telerik:RadWindow ID="rwDematDetails" runat="server" VisibleOnPageLoad="false" Height="230px"
    Width="800px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="Resize, Close, Move"
    Title="Select Demat " RestrictionZoneID="radWindowZone" OnClientShow="setCustomPosition"
    Top="120" Left="70">
    <ContentTemplate>
        <table>
            <tr>
            </tr>
            <tr>
                <%--<td>
                    <telerik:RadGrid ID="gvDematDetailsTeleR" runat="server" AllowAutomaticInserts="false"
                        AllowPaging="true" AllowSorting="true" AutoGenerateColumns="False" Height="150px"
                        EnableEmbeddedSkins="false" EnableHeaderContextMenu="true" fAllowAutomaticDeletes="false"
                        GridLines="none" ShowFooter="false" ShowStatusBar="false" Skin="Telerik">
                        <%--<HeaderContextMenu EnableEmbeddedSkins="False">
                                </HeaderContextMenu>
                        <ExportSettings HideStructureColumns="true">
                        </ExportSettings>
                        <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false" DataKeyNames="CEDA_DematAccountId,CEDA_DPClientId"
                            Width="99%">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="false" DataField="Action" HeaderStyle-Width="30px"
                                    UniqueName="Action">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDematId" runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" DataField="CEDA_DPName" HeaderStyle-Width="67px"
                                    HeaderText="DP Name" ShowFilterIcon="false" SortExpression="CEDA_DPName" UniqueName="CEDA_DPName">
                                    <HeaderStyle Width="67px" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="100px" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="CEDA_DepositoryName" HeaderStyle-Width="140px" HeaderText="Depository Name"
                                    ShowFilterIcon="false" SortExpression="CEDA_DepositoryName" UniqueName="CEDA_DepositoryName">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="67px" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="CEDA_DPClientId" HeaderStyle-Width="67px" HeaderText="Beneficiary Acct No"
                                    ShowFilterIcon="false" SortExpression="CEDA_DPClientId" UniqueName="CEDA_DPClientId">
                                    <HeaderStyle Width="120px" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="100px" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="CEDA_DPId" HeaderStyle-Width="67px" HeaderText="DP Id" ShowFilterIcon="false"
                                    SortExpression="CEDA_DPId" UniqueName="CEDA_DPId">
                                    <HeaderStyle Width="140px" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="140px" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" DataField="XMOH_ModeOfHolding"
                                    HeaderStyle-Width="145px" HeaderText="Mode of holding" ShowFilterIcon="false"
                                    SortExpression="XMOH_ModeOfHolding" UniqueName="XMOH_ModeOfHolding">
                                    <HeaderStyle Width="145px" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="145px" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" DataField="CEDA_AccountOpeningDate"
                                    DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="145px" HeaderText="Account Opening Date"
                                    ShowFilterIcon="false" SortExpression="CEDA_AccountOpeningDate" UniqueName="CEDA_AccountOpeningDate"
                                    Visible="false">
                                    <HeaderStyle Width="145px" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="145px" Wrap="false" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <EditFormSettings>
                                <EditColumn CancelImageUrl="Cancel.gif" EditImageUrl="Edit.gif" InsertImageUrl="Update.gif"
                                    UpdateImageUrl="Update.gif">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <ClientSettings>
                            <Scrolling AllowScroll="true" ScrollHeight="70px" UseStaticHeaders="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </td>--%>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnAddDemat" runat="server" Text="Accept" CssClass="PCGButton" OnClick="btnAddDemat_Click"
                        CausesValidation="false" OnClientClick="javascript:return  TestCheckBox();" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</telerik:RadWindow>
<telerik:RadWindow ID="RadDepository" runat="server" VisibleOnPageLoad="false" Height="30%"
    Width="400px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="Resize, Close, Move"
    Title="Add New Depository" RestrictionZoneID="radWindowZone">
    <ContentTemplate>
        <table>
            <tr>
                <td align="right">
                    <asp:Label ID="lblDepositoryAdd" runat="server" Text="Depository Name:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDepository" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblDescription" runat="server" Text="Depository Description:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnDepository" runat="server" Text="Submit" CssClass="PCGButton"
                        OnClick="btnDepository_OnClick" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</telerik:RadWindow>
<telerik:RadWindow ID="rwTermsCondition" runat="server" VisibleOnPageLoad="false"
    Width="1000px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="Move, Resize,Close"
    Title="Terms & Conditions" EnableShadow="true" OnClientShow="setCustomPosition"
    Top="10" Left="20">
    <ContentTemplate>
        <div style="padding: 0px; width: 100%">
            <table width="100%" cellpadding="0" cellpadding="0">
                <tr>
                    <td align="left">
                        <%--  <a href="../ReferenceFiles/MF-Terms-Condition.html">../ReferenceFiles/MF-Terms-Condition.html</a>--%>
                        <iframe src="../ReferenceFiles/NCD-Terms-Condition.html" name="iframeTermsCondition"
                            style="width: 100%"></iframe>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAccept" runat="server" Text="Accept" CssClass="PCGButton" OnClick="btnAccept_Click"
                            CausesValidation="false" ValidationGroup="btnSubmit" />
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
</telerik:RadWindow>
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
<asp:HiddenField id="hdnOrderId" runat="server" />     
