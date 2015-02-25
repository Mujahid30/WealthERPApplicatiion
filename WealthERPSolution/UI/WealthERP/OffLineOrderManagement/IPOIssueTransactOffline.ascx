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
    function setCustomPosition(sender, args) {
        sender.moveTo(sender.get_left(), sender.get_top());
    }
    $('input[type="checkbox"]').click(function(event) {
        if (this.checked && $('input:checked').length > 1) {
            event.preventDefault();
            alert('You\'re not allowed to choose more than 3 boxes');
        }
    });
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

<script runat="server">
  
    protected void rbtnNo_Load(object sender, EventArgs e)
    {
        if (rbtnNo.Checked)
        {
            ddlModeOfHolding.Enabled = false;
        }
    }
   
</script>

<script type="text/javascript" language="javascript">

    function GetCustomerId(source, eventArgs) {
        isItemSelected = true;
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();

        return false;
    }

    function ValidateAssociateName() {
        document.getElementById("<%=  lblAssociatetext.ClientID %>").value = eventArgs.get_value();
        document.getElementById("lblAssociatetext").innerHTML = "AgentCode Required";
        return true;
    }
</script>

<script type="text/javascript">
    var isItemSelected = false;
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

<table width="100%">
    <tr>
        <td colspan="5">
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
<asp:Panel ID="Panel2" runat="server" class="Landscape" Width="100%" Height="3000px"
    ScrollBars="Vertical" >
    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td colspan="6">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            <div class="fltlft" style="width: 100%; float: left;">
                                &nbsp; Customer Details
                            </div>
                        </div>
                </tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
                <tr id="trpan" runat="server" visible="true">
                    <td class="leftField" style="width: 20%">
                        <asp:Label ID="lblPansearch" runat="server" Text="Search PAN: " CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField" style="width: 20%">
                        <asp:TextBox ID="txtPansearch" runat="server" CssClass="txtField" AutoComplete="Off"
                            AutoPostBack="True" onclientClick="ShowIsa()" OnTextChanged="OnAssociateTextchanged1"
                            TabIndex="1">
                        </asp:TextBox><span id="Span1" class="spnRequiredField">*</span>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" TargetControlID="txtPansearch"
                            WatermarkText="Enter few chars of PAN" runat="server" EnableViewState="false">
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
                            OnClick="lnkAddcustomer_OnClick" TabIndex="2"></asp:LinkButton>
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
                                        GroupName="grpCustomerType" AutoPostBack="true" OnCheckedChanged="rbtnIndividual_CheckedChanged"
                                        TabIndex="3" />
                                    &nbsp;&nbsp;
                                    <asp:RadioButton ID="rbtnNonIndividual" runat="server" CssClass="txtField" Text="Non Individual"
                                        GroupName="grpCustomerType" AutoPostBack="true" OnCheckedChanged="rbtnNonIndividual_CheckedChanged"
                                        TabIndex="4" />
                                </td>
                                <td>
                                    <asp:Label ID="lblCustomerSubType" runat="server" CssClass="FieldName" Text="Customer Sub Type:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCustomerSubType" runat="server" CssClass="cmbField" TabIndex="5">
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
                                    <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Branch Name:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlAdviserBranchList" AutoPostBack="true" runat="server" CssClass="cmbField"
                                        TabIndex="6">
                                    </asp:DropDownList>
                                    <span id="Span3" class="spnRequiredField">*</span>
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
                                    <asp:DropDownList ID="ddlAdviseRMList" runat="server" CssClass="cmbField" TabIndex="7">
                                    </asp:DropDownList>
                                    <span id="Span8" class="spnRequiredField">*</span>
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
                                    <asp:TextBox ID="txtPanNumber" runat="server" CssClass="txtField" MaxLength="10"
                                        TabIndex="8"></asp:TextBox>
                                    <span id="Span13" class="spnRequiredField">*</span> &nbsp;
                                    <asp:CheckBox ID="chkdummypan" runat="server" Visible="false" CssClass="txtField"
                                        Text="Dummy PAN" AutoPostBack="true" TabIndex="9" />
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
                                                <asp:TextBox ID="txtFirstName" runat="server" MaxLength="75" CssClass="txtField"
                                                    TabIndex="10"></asp:TextBox>
                                                <span id="Span14" class="spnRequiredField">*</span>
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
                                                <asp:TextBox ID="txtCompanyName" CssClass="txtField" runat="server" TabIndex="11"></asp:TextBox>
                                                <span id="Span15" class="spnRequiredField">*</span>
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
                                        CssClass="PCGMediumButton" ValidationGroup="CustomerProfileSubmit" TabIndex="12" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="trCust" runat="server" visible="false">
                    <%-- <td class="leftField" style="width: 20%">
                <asp:Label ID="lblCustomer" runat="server" Text="Customer Name: " CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:TextBox ID="txtCustomerName" onkeydown="return (event.keyCode!=13);" runat="server"
                    CssClass="txtField" AutoComplete="Off" onclientClick="ShowIsa()" AutoPostBack="True"
                    TabIndex="2">
            
                 
                </asp:TextBox><span id="spnCustomer" class="spnRequiredField">*</span>
                <asp:ImageButton ID="btnImgAddCustomer" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                    AlternateText="Add" runat="server" ToolTip="Click here to Add Customer" OnClick="btnImgAddCustomer_OnClick"
                    Height="15px" Width="15px" TabIndex="3" CausesValidation="false"></asp:ImageButton>
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
                    <%--<td class="leftField" style="width: 20%">
                <asp:Label ID="lblPan" runat="server" Text="PAN: " CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField" style="width: 20%">
                <asp:Label ID="lblgetPan" runat="server" Text="" CssClass="FieldName"></asp:Label>
            </td>--%>
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
                        <asp:Label ID="lblAssociateSearch" runat="server" CssClass="FieldName" Text="Agent Code:"></asp:Label>
                    </td>
                    <td class="rightField" style="width: 20%">
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
                    <td class="leftField" style="width: 20%">
                        <asp:Label ID="lblAssociate" runat="server" CssClass="FieldName" Text="Associate:"></asp:Label>
                    </td>
                    <td class="rightField" style="width: 15%">
                        <asp:Label ID="lblAssociatetext" runat="server" CssClass="FieldName" Enabled="false"></asp:Label>
                    </td>
                </tr>
                <tr id="trTaxStatus" runat="server">
                    <td class="leftField" style="width: 20%" visible="false">
                        <asp:Label ID="Label3" runat="server" Text="Customer Tax Status: " CssClass="FieldName"></asp:Label>
                    </td>
                    <td visible="false">
                        <asp:DropDownList ID="ddlTax" runat="server" CssClass="cmbField" AutoPostBack="true"
                            TabIndex="14">
                        </asp:DropDownList>
                        <%--<asp:TextBox ID="txtTax" runat="server" CssClass="txtField" AutoComplete="Off" ReadOnly="true"   />  --%>
                    </td>
                    <td class="leftField" style="width: 20%">
                    </td>
                    <td class="rightField" style="width: 15%">
                    </td>
                    <td class="leftField" style="width: 20%">
                        <asp:Label ID="lblAssociateReport" runat="server" CssClass="FieldName" Text="Report To:"
                            Visible="true"></asp:Label>
                    </td>
                    <td class="rightField" style="width: 15%">
                        <asp:Label ID="lblAssociateReportTo" runat="server" CssClass="FieldName" Enabled="false"></asp:Label>
                    </td>
                    <td colspan="3">
                    </td>
                </tr>
                <tr id="trIsa" runat="server">
                    <td class="leftField" style="width: 20%">
                        <asp:Label ID="lblIsa" runat="server" CssClass="FieldName" Text="ISA No:"></asp:Label>
                    </td>
                    <td class="rightField" style="width: 20%">
                        <asp:DropDownList ID="ddlCustomerISAAccount" runat="server" CssClass="cmbField" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCustomerISAAccount_SelectedIndexChanged" TabIndex="15">
                        </asp:DropDownList>
                        &nbsp
                        <asp:ImageButton ID="btnIsa" ImageUrl="~/App_Themes/Maroon/Images/user_add.png" AlternateText="Add"
                            runat="server" ToolTip="Click here to Request ISA" OnClick="ISA_Onclick" Height="15px"
                            Width="15px" TabIndex="16"></asp:ImageButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel1" runat="server" class="Landscape" Width="100%" Height="80%"
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
                                Text="Click to select Demat Details" CausesValidation="false" TabIndex="17"></asp:LinkButton>
                        </td>
                        <td id="Td5" class="rightField" style="width: 20%" colspan="2">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                                AlternateText="Add Demat Account" runat="server" ToolTip="Click here to Add Demat Account"
                                OnClick="ImageButton1_OnClick" Height="15px" Width="15px" TabIndex="18" CausesValidation="false">
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
                                        <asp:TextBox ID="txtDpName" runat="server" CssClass="txtField" Width="250px" TabIndex="19"></asp:TextBox>
                                        <span id="Span16" class="spnRequiredField">*</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDpName"
                                            ErrorMessage="</br>DP Name Required" CssClass="cvPCG" ValidationGroup="btnsubmitdemate"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="leftField" style="width: 150px;">
                                        &nbsp;<asp:Label ID="lblDPId" runat="server" Text="DP Id:" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td class="rightField">
                                        <asp:TextBox ID="txtDPId" runat="server" CssClass="txtField" TabIndex="20"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
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
                                        &nbsp;<asp:Label ID="lblAccountOpeningDate" runat="server" Text="Account Opening Date:"
                                            CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td class="rightField">
                                        <asp:TextBox ID="txtAccountOpeningDate" runat="server" CssClass="txtField" TabIndex="22"></asp:TextBox>
                                        <span id="Span20" class="spnRequiredField">*</span>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtAccountOpeningDate"
                                            Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtAccountOpeningDate"
                                            WatermarkText="dd/mm/yyyy">
                                        </cc1:TextBoxWatermarkExtender>
                                        <%-- <telerik:RadDatePicker ID="txtAccountOpeningDate" CssClass="txtTo" runat="server"
                                            Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                                            ShowAnimation-Type="Fade" MinDate="1900-01-01">
                                            <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                                ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                                            </Calendar>
                                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                            <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                                            </DateInput>
                                        </telerik:RadDatePicker>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAccountOpeningDate"
                                            ErrorMessage="</br>AccountOpeningDate Required" CssClass="cvPCG" ValidationGroup="btnsubmitdemate"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtAccountOpeningDate"
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
                                            CssClass="txtField" AutoPostBack="True" OnCheckedChanged="RadioButton_CheckChanged"
                                            TabIndex="23" />
                                        <asp:RadioButton ID="rbtnNo" runat="server" Text="No" GroupName="IsHeldJointly" CssClass="txtField"
                                            AutoPostBack="True" OnCheckedChanged="rbtnNo_CheckChanged" OnLoad="rbtnNo_Load"
                                            Checked="true" TabIndex="24" />
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
                                </tr>
                                <tr>
                                    <td class="leftField">
                                        &nbsp;<asp:Label ID="lblModeOfHolding" runat="server" Text="Mode Of Holding:" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td class="rightField">
                                        <asp:DropDownList ID="ddlModeOfHolding" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModeOfHolding_SelectedIndexChanged"
                                            CssClass="cmbField" TabIndex="26">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="rightField">
                                        <asp:Button ID="btnDemateDetails" runat="server" Text="Submit" OnClick="DematebtnSubmit_Click"
                                            ValidationGroup="btnsubmitdemate" CssClass="PCGMediumButton" TabIndex="27" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <tr>
                            <td colspan="4">
                                <telerik:RadGrid ID="gvDematDetailsTeleR" runat="server" AllowAutomaticInserts="false"
                                    AllowFilteringByColumn="false" AllowPaging="true" AllowSorting="false" AutoGenerateColumns="False"
                                    EnableEmbeddedSkins="false" EnableHeaderContextMenu="true" fAllowAutomaticDeletes="false"
                                    GridLines="none" ShowFooter="true" ShowStatusBar="false" Skin="Telerik" OnItemDataBound="gvDematDetailsTeleR_OnItemDataBound"
                                    FooterStyle-BackColor="#2475C7">
                                    <%--<HeaderContextMenu EnableEmbeddedSkins="False">
                                </HeaderContextMenu>--%>
                                    <ExportSettings HideStructureColumns="true">
                                    </ExportSettings>
                                    <MasterTableView AllowMultiColumnSorting="false" AutoGenerateColumns="false" DataKeyNames="CEDA_DematAccountId,CEDA_DPClientId"
                                        Width="99%">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="false" DataField="Action" HeaderStyle-Width="30px"
                                                UniqueName="Action">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkDematId" runat="server" OnCheckedChanged="btnAddDemat_Click"
                                                        AutoPostBack="true" TabIndex="28" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn AllowFiltering="false" CurrentFilterFunction="Contains"
                                                DataField="CEDA_DPName" FilterControlWidth="50px" HeaderStyle-Width="67px" HeaderText="DP Name"
                                                ShowFilterIcon="false" SortExpression="CEDA_DPName" UniqueName="CEDA_DPName">
                                                <HeaderStyle Width="67px" />
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="100px" Wrap="false" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" CurrentFilterFunction="Contains" DataField="CEDA_DepositoryName"
                                                FilterControlWidth="120px" HeaderStyle-Width="140px" HeaderText="Depository Name"
                                                ShowFilterIcon="false" SortExpression="CEDA_DepositoryName" UniqueName="CEDA_DepositoryName">
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="67px" Wrap="false" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" CurrentFilterFunction="Contains" DataField="CEDA_DPClientId"
                                                FilterControlWidth="50px" HeaderStyle-Width="67px" HeaderText="Beneficiary Acct. No."
                                                ShowFilterIcon="false" SortExpression="CEDA_DPClientId" UniqueName="CEDA_DPClientId">
                                                <HeaderStyle Width="120px" />
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="100px" Wrap="false" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" CurrentFilterFunction="Contains" DataField="CEDA_DPId"
                                                FilterControlWidth="50px" HeaderStyle-Width="67px" HeaderText="DP Id" ShowFilterIcon="false"
                                                SortExpression="CEDA_DPId" UniqueName="CEDA_DPId">
                                                <HeaderStyle Width="140px" />
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="140px" Wrap="false" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" DataField="XMOH_ModeOfHolding" HeaderStyle-Width="145px"
                                                HeaderText="Mode of holding" ShowFilterIcon="false" SortExpression="XMOH_ModeOfHolding"
                                                UniqueName="XMOH_ModeOfHolding">
                                                <HeaderStyle Width="145px" />
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="top" Width="145px" Wrap="false" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" DataField="CEDA_AccountOpeningDate"
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
                                    runat="server" CssClass="txtField" TabIndex="29"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtDematid"
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
                                Visible="false" Width="90%" AllowPaging="True" PageSize="20" AllowSorting="false"
                                AutoGenerateColumns="false" ShowStatusBar="true" Skin="Telerik">
                                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                    FileName="Family Associates" Excel-Format="ExcelML">
                                </ExportSettings>
                                <%--<MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                        CommandItemDisplay="None">--%>
                                <MasterTableView DataKeyNames="CDAA_Id,CEDA_DematAccountId,CDAA_Name,CDAA_PanNum,Sex,CDAA_DOB,RelationshipName,AssociateType,CDAA_AssociateTypeNo,CDAA_IsKYC,SexShortName,CDAA_AssociateType,XR_RelationshipCode"
                                    Width="100%" AllowMultiColumnSorting="false" AutoGenerateColumns="false" CommandItemSettings-ShowRefreshButton="false">
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
                        <td class="leftField" style="width: 20%">
                            <asp:Label ID="lblIssueName" runat="server" Text="Select Issue Name:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField" colspan="5" style="width: 20%">
                            <asp:DropDownList ID="ddlIssueList" runat="server" AutoPostBack="true" CssClass="cmbExtraLongField"
                                OnSelectedIndexChanged="ddlIssueList_OnSelectedIndexChanged" TabIndex="30">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvIssueList" runat="server" ControlToValidate="ddlIssueList"
                                ErrorMessage="</br>Please select the Issue Name" CssClass="rfvPCG" Display="Dynamic"
                                ValidationGroup="btnConfirmOrder" InitialValue="Select"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftField" style="width: 20%">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label runat="server" ID="lblBrokerCode" CssClass="FieldName" Text="Select Broker:"></asp:Label>
                        </td>
                        <td class="rightField" style="width: 20%">
                            <asp:DropDownList ID="ddlBrokerCode" runat="server" CssClass="cmbField">
                            </asp:DropDownList>
                            <%-- <asp:RequiredFieldValidator ID="RFVBrokerCode" ControlToValidate="ddlBrokerCode"
                            ErrorMessage="<br />Please Select Broker" Display="Dynamic" runat="server" CssClass="rfvPCG"
                            ValidationGroup="ddlBrokerCode" InitialValue="0"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftField" style="width: 20%">
                            <asp:Label ID="lblApplicationNo" runat="server" Text="Application No.:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField" style="width: 20%">
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
                        <td id="Td1" class="leftField" style="width: 20%" runat="server" visible="false">
                            <asp:Label ID="lblDepository" runat="server" Text="Depository Type: " CssClass="FieldName"></asp:Label>
                        </td>
                        <td id="Td2" class="rightField" style="width: 20%" runat="server" visible="false">
                            <%-- <asp:DropDownList ID="ddlDepositoryName" runat="server" CssClass="cmbField" AutoPostBack="true">
                </asp:DropDownList>--%>
                            <asp:ImageButton ID="ImageddlSyndicate" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                                AlternateText="Add" runat="server" ToolTip="Click here to Add Depository Type"
                                OnClick="ImageddlSyndicate_Click" Height="15px" Width="15px" TabIndex="32"></asp:ImageButton>
                            <br />
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlDepositoryName"
                    ErrorMessage="<br />Please Enter Depository Name" Display="Dynamic" runat="server"
                    CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
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
                    <tr>
                        <td class="leftField" style="width: 20%">
                            <asp:Label ID="Label5" runat="server" Text="Mode of Payment:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField" style="width: 20%">
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
                        <td id="Td3" class="leftField" style="width: 20%" runat="server" visible="false">
                            <asp:Label ID="lblBankAccount" Text="Bank Account No.:" runat="server" CssClass="FieldName"></asp:Label>
                        </td>
                        <td id="Td4" class="rightField" style="width: 20%" runat="server" visible="false">
                            <asp:TextBox ID="txtBankAccount" runat="server" CssClass="txtField" onkeydown="return (event.keyCode!=13);"
                                OnKeypress="javascript:return isNumberKey(event);" MaxLength="9" TabIndex="34"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trPINo" runat="server" visible="false">
                        <td class="leftField" style="width: 20%">
                            <asp:Label ID="lblPaymentNumber" runat="server" Text="Cheque/Demand Draft No.: "
                                CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField" style="width: 20%">
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
                        <td class="leftField" style="width: 20%">
                            <asp:Label ID="lblPIDate" runat="server" Text="Cheque Date:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField" style="width: 20%">
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
                    </tr>
                    <tr id="trASBA" runat="server" visible="false">
                        <td class="leftField" style="width: 20%">
                            <asp:Label ID="lblASBANo" Text="ASBA Bank A/c No.:" runat="server" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField" style="width: 20%">
                            <asp:TextBox ID="txtASBANO" onkeydown="return (event.keyCode!=13);" runat="server"
                                MaxLength="16" CssClass="txtField" TabIndex="37"></asp:TextBox>
                            <span id="Span5" class="spnRequiredField">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtASBANO"
                                ErrorMessage="<br />Please Enter Account No." Display="Dynamic" runat="server"
                                CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RequiredFieldValidator>
                        </td>
                        <td class="leftField" style="width: 20%">
                            <asp:Label ID="lblASBALocation" runat="server" CssClass="FieldName" Text="Location:"></asp:Label>
                        </td>
                        <td class="rightField" style="width: 20%">
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
                    </tr>
                    <tr id="trBankName" runat="server">
                        <td class="leftField" style="width: 20%">
                            <asp:Label ID="lblBankName" runat="server" Text="Bank Name:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField" style="width: 20%">
                            <asp:DropDownList ID="ddlBankName" runat="server" CssClass="cmbField" AutoPostBack="false"
                                AppendDataBoundItems="true" Width="380px" TabIndex="39">
                            </asp:DropDownList>
                            <span id="Span4" class="spnRequiredField">*</span>
                            <asp:ImageButton ID="imgAddBank" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                                AlternateText="Add" runat="server" ToolTip="Click here to Add Bank" OnClientClick="return openpopupAddBank()"
                                Height="15px" Width="15px" Visible="false" TabIndex="40"></asp:ImageButton>
                            <%-- --%>
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
                        <td class="leftField" style="width: 20%">
                            <asp:Label ID="lblBranchName" runat="server" Text="Bank BranchName:" CssClass="FieldName"
                                Visible="false"></asp:Label>
                        </td>
                        <td class="rightField" style="width: 20%" id="tdBankBranch">
                            <asp:TextBox ID="txtBranchName" onkeydown="return (event.keyCode!=13);" runat="server"
                                CssClass="txtField" Visible="false" TabIndex="41"></asp:TextBox>
                            <%--<span id="Span3" class="spnRequiredField" visible="false">*</span>--%>
                            <asp:Label ID="lblBankBranchName" runat="server" Text="*" class="spnRequiredField"
                                Visible="false"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtBranchName"
                                CssClass="rfvPCG" ErrorMessage="<br />Please Enter Bank Branch" Display="Dynamic"
                                runat="server" InitialValue="" ValidationGroup="btnConfirmOrder" Visible="false"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
            <asp:Panel ID="pnlIPOControlContainer" runat="server" ScrollBars="Horizontal" Width="100%"
                Visible="false">
                <div id="divControlContainer" class="divControlContiner" runat="server">
                    <table width="100%">
                        <tr>
                            <td colspan="4">
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
                            <td colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
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
                        <tr class="spaceUnder" id="trTermsCondition" visible="false" runat="server">
                            <td align="left" style="width: 100%" visible="false" colspan="4">
                                <asp:CheckBox ID="chkTermsCondition" runat="server" Font-Bold="True" Font-Names="Shruti"
                                    Enabled="false" Checked="false" ForeColor="#145765" Text="" ToolTip="Click 'Terms & Conditions' to proceed further"
                                    CausesValidation="true" />
                                <asp:LinkButton ID="lnkTermsCondition" CausesValidation="false" Text="Terms & Conditions"
                                    runat="server" CssClass="txtField" OnClick="lnkTermsCondition_Click" ToolTip="Click here to accept terms & conditions"></asp:LinkButton>
                                <span id="Span9" class="spnRequiredField">*</span>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" Text="Please read terms & conditions"
                                    ClientValidationFunction="ValidateTermsConditions" EnableClientScript="true"
                                    OnServerValidate="TermsConditionCheckBox" Display="Dynamic" ValidationGroup="btnTC"
                                    CssClass="rfvPCG">
                               Please read terms & conditions
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton runat="server" ID="lnlBack" CssClass="LinkButtons" Text="Click here to view the issue list"
                                    Visible="false" OnClick="lnlktoviewIPOissue_Click"></asp:LinkButton>
                            </td>
                            <%--<td colspan="2" style="width: 90%">
                        </td>--%>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:Panel ID="PnlSubmit" runat="server" class="Landscape" Width="100%" Height="80%"
                ScrollBars="None">
                <table width="100%">
                    <tr>
                        <td class="leftField" style="width: 25%">
                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField" style="width: 25%">
                            <asp:TextBox ID="txtRemarks" Width="400px" TextMode="MultiLine" MaxLength="300" Height="40px"
                                onkeydown="return (event.keyCode!=13);" runat="server" CssClass="txtField" TabIndex="45"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftField">
                            <asp:Button ID="btnConfirmOrder" runat="server" Text="Submit Order" OnClick="btnConfirmOrder_Click"
                                CssClass="PCGMediumButton" ValidationGroup="btnConfirmOrder, btnTC" OnClientClick="javascript: return  PreventClicks(); Validate();"
                                TabIndex="46" />
                        </td>
                        <td class="rightField">
                            <asp:Button ID="btnAddMore" runat="server" Text="Add More" CssClass="PCGMediumButton"
                                ValidationGroup="btnConfirmOrder" OnClientClick="return  PreventClicks();" OnClick="btnAddMore_Click"
                                TabIndex="47" />
                        </td>
                        <td>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_OnClick"
                                CssClass="PCGButton" Visible="false" ValidationGroup="btnConfirmOrder,ddlBrokerCode"
                                OnClientClick="javascript: return  ValGroup();" TabIndex="48" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnConfirmOrder" />
            <asp:PostBackTrigger ControlID="btnAddMore" />
            <asp:PostBackTrigger ControlID="btnUpdate" />
            <asp:PostBackTrigger ControlID="btnDemateDetails" />
            <asp:AsyncPostBackTrigger ControlID="ImageButton1" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Panel>
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
                        <iframe src="../ReferenceFiles/IPO-Terms-Condition.htm" name="iframeTermsCondition"
                            style="width: 100%"></iframe>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAccept" runat="server" Text="Accept" CssClass="PCGButton" OnClick="btnAccept_Click"
                            CausesValidation="false" ValidationGroup="btnConfirmOrder" />
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
</telerik:RadWindow>
<telerik:RadWindowManager runat="server" ID="RadWindowManager1">
    <Windows>
        <telerik:RadWindow ID="rw_customConfirm" Modal="true" Behaviors="Close, Move" VisibleStatusbar="false"
            Width="700px" Height="160px" runat="server" Title="EUIN Confirm">
            <ContentTemplate>
                <div class="rwDialogPopup radconfirm">
                    <div class="rwDialogText">
                        <asp:Label ID="confirmMessage" Text="" runat="server" />
                    </div>
                    <div>
                        <asp:Button runat="server" ID="rbConfirm_OK" Text="OK" OnClick="rbConfirm_OK_Click"
                            ValidationGroup="btnConfirmOrder" OnClientClick="return PreventClicks();"></asp:Button>
                        <asp:Button runat="server" ID="rbConfirm_Cancel" Text="Cancel" OnClientClicked="closeCustomConfirm">
                        </asp:Button>
                    </div>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
<telerik:RadWindow ID="rwDematDetails" runat="server" VisibleOnPageLoad="false" Height="230px"
    Width="800px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="Resize, Close, Move"
    Title="Select Demat " RestrictionZoneID="radWindowZone" OnClientShow="setCustomPosition"
    Top="120" Left="70">
    <ContentTemplate>
        <table>
            <tr>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnAddDemat" runat="server" Text="Accept" CssClass="PCGButton" CausesValidation="false" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</telerik:RadWindow>
<telerik:RadWindow ID="RadWindow1" runat="server" VisibleOnPageLoad="false" Width="1000px"
    Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="Move, Resize,Close"
    Title="Add Customer" EnableShadow="true" OnClientShow="setCustomPosition" Top="10"
    Left="20">
    <ContentTemplate>
        <table width="100%" cellpadding="0" cellpadding="0">
            <tr>
                <tr>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
        </table>
        <table class="TableBackground" style="width: 100%">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="SubmitCell">
                    <%----%>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</telerik:RadWindow>
<telerik:RadWindow ID="RadDemateAdd" runat="server" VisibleOnPageLoad="false" Width="1000px"
    Height="200px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="Move, Resize,Close"
    Title="Add Demate Account" EnableShadow="true" OnClientShow="setCustomPosition"
    Top="10" Left="20">
    <ContentTemplate>
        <table class="TableBackground" width="100%">
            <tr>
            </tr>
            <tr>
            </tr>
        </table>
    </ContentTemplate>
</telerik:RadWindow>
<tr>
    <td colspan="4">
        <asp:ValidationSummary ID="vsSummary" runat="server" CssClass="rfvPCG" Visible="true"
            ValidationGroup="btnConfirmOrder" ShowSummary="true" DisplayMode="BulletList" />
    </td>
</tr>
<%--</ContentTemplate>
    <Triggers>
    </Triggers>
</asp:UpdatePanel>--%>
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
 
