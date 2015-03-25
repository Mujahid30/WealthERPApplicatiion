<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFOrderEntry.ascx.cs"
    Inherits="WealthERP.OPS.MFOrderEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.bxslider.js" type="text/javascript"></script>

<script type="text/javascript">
    function Confirm() {
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (confirm("Do you want to save data?")) {
            confirm_value.value = "Yes";
        } else {
            confirm_value.value = "No";
        }
        document.forms[0].appendChild(confirm_value);
    }
</script>

<script type="text/javascript" language="javascript">


    function GetCustomerId(source, eventArgs) {
        isItemSelected = true;

        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();

        return false;
    }
    function GeAgentId(source, eventArgs) {
        isItemSelected = true;

        document.getElementById("<%= txtAgentId.ClientID %>").value = eventArgs.get_value();

        return false;
    }


    function CheckPanno() {

    }


    function ValidateAssociateName() {

        document.getElementById("<%=  lblAssociatetext.ClientID %>").value = eventArgs.get_value();
        document.getElementById("lblAssociatetext").innerHTML = "AgentCode Required";
        return true;
    }

    function openpopupAddCustomer() {
        window.open('PopUp.aspx?AddMFCustLinkId=mf&pageID=CustomerType&', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return false;
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
                alert("Please select PAN from the PAN list only");
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
    function ShowInitialIsa() {
 
    }
    function CheckSubscription() {

        document.getElementById("<%= trIsa.ClientID %>").style.visibility = 'visible';
        document.getElementById("<%= trJointHoldersList.ClientID %>").style.visibility = 'collapse';
    }
</script>

<script type="text/javascript">

    var isValidFolio = false;
    function GetSchemeCode(source, eventArgs) {

        document.getElementById("<%= txtSchemeCode.ClientID %>").value = eventArgs.get_value();

        return false;
    };
    function GetSwitchSchemeCode(source, eventArgs) {

        document.getElementById("<%= txtSwitchSchemeCode.ClientID %>").value = eventArgs.get_value();

        return false;
    };

    function GetFolioAccount(source, eventArgs) {
        isValidFolio = true;
        document.getElementById("<%= hidFolioNumber.ClientID %>").value = eventArgs.get_value();

        return false;
    };
    function GetSwitchFolioAccount(source, eventArgs) {
        isValidFolio = true;
        document.getElementById("<%= hdnAccountIdSwitch.ClientID %>").value = eventArgs.get_value();

        return false;
    };

    function ValidateFolioSelection(txtFolioNuber) {

        var returnValue = true;
        if (!isValidFolio) {

            if (txtFolioNuber.value != "") {
                txtFolioNuber.focus();
                alert("Please select valid folio");
                txtFolioNuber.value = "";
                returnValue = false;
            }
        }
        //         else {
        //            if (txtFolioNuber.value != "")
        //                alert("Valid folio found");
        //        }
        return returnValue;


    }

</script>

<script language="javascript" type="text/javascript">
    var crnt = 0;
    function PreventClicks(btnsubmit) {

        if (typeof (Page_ClientValidate('btnSubmit')) == 'function') {
            Page_ClientValidate();

        }

        if (Page_IsValid) {
            if (++crnt > 1) {
                // alert(crnt);
                return false;
            }
            return true;
        }
        else {
            return false;
        }
    }
</script>

<script type="text/javascript">
    function setCustomPosition(sender, args) {
        sender.moveTo(sender.get_left(), sender.get_top());
    }
</script>

<script type="text/javascript">
    function checkFolioDuplicate() {
        $("#<%= hidValidCheck.ClientID %>").val("0");
        alert("here..");
        if ($("#<%=txtFolioNumber.ClientID %>").val() == "") {
            $("#spnExistingFolio").html("");
            alert("here-null");
            return;
        }
        $("#spnExistingFolio").html("<img src='Images/loader.gif' />");
        alert("here1");
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "ControlHost.aspx/CheckFolioDuplicate",
            data: "{ 'customerId': '" + $("#<%=txtCustomerId.ClientID %>").val() + "','folioNumber': '" + $("#<%=txtFolioNumber.ClientID %>").val() + "' }",
            error: function(xhr, status, error) {
                //                alert("Please select AMC!");
            },
            success: function(msg) {

                if (msg.d) {

                    $("#<%= hidValidCheck.ClientID %>").val("1");
                    $("#spnExistingFolio").html("");
                    alert("here-sucess");
                }
                else {


                    $("#<%= hidValidCheck.ClientID %>").val("0");
                    $("#spnExistingFolio").removeClass();
                    alert("Folio Number Already Exists!");
                    return false;
                }
            }

        });
    }
   
</script>

<telerik:RadWindow ID="radCustomApp" Visible="false" runat="server" VisibleOnPageLoad="false"
    Height="30%" Width="600px" Modal="true" BackColor="#DADADA" Top="10" Left="20"
    VisibleStatusbar="false" OnClientShow="setCustomPosition" Behaviors="close,Move"
    Title="Add New Customer">
    <ContentTemplate>
        <div style="padding: 20px">
            <table width="100%" class="TableBackground">
                <tr>
                    <td colspan="3">
                        <div class="divPageHeading">
                            <table cellspacing="0" cellpadding="3" width="100%">
                                <tr>
                                    <td align="left">
                                        Add Customer
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr id="trBtnSaveMsg" runat="server" visible="false">
                    <td class="leftField">
                    </td>
                    <td>
                        <asp:Label ID="lblbtnSaveMsg" runat="server" Text="Customer Added Successfully" CssClass="FieldName"
                            Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="leftField" style="width: 20%">
                        <asp:Label ID="lblCustomerType" runat="server" CssClass="FieldName" Text="Customer Type:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:RadioButton ID="rbtnIndividual" runat="server" CssClass="txtField" Text="Individual"
                            GroupName="grpCustomerType" AutoPostBack="true" OnCheckedChanged="rbtnIndividual_CheckedChanged" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbtnNonIndividual" runat="server" CssClass="txtField" Text="Non Individual"
                            GroupName="grpCustomerType" AutoPostBack="true" OnCheckedChanged="rbtnNonIndividual_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblCustomerSubType" runat="server" CssClass="FieldName" Text="Customer Sub Type:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCustomerSubType" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                        <span id="Span30" class="spnRequiredField">*</span> &nbsp;
                        <asp:CompareValidator ID="CompareValidatorSubtype" runat="server" ControlToValidate="ddlCustomerSubType"
                            ErrorMessage="<br />Please select a Customer Sub-Type" Operator="NotEqual" ValueToCompare="0"
                            CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnCustomerSubmit"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblPanNum" runat="server" CssClass="FieldName" Text="PAN Number:"></asp:Label>
                    </td>
                    <td class="rightField" width="75%">
                        <asp:TextBox ID="txtPanNumber" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
                        <span id="Span31" class="spnRequiredField">*</span> &nbsp;
                        <asp:CheckBox ID="chkdummypan" runat="server" Visible="false" CssClass="txtField"
                            Text="Dummy PAN" AutoPostBack="true" />
                        <br />
                        <asp:RequiredFieldValidator ID="rfvPanNumber" ControlToValidate="txtPanNumber" ErrorMessage="Please enter a PAN Number"
                            Display="Dynamic" runat="server" ValidationGroup="btnCustomerSubmit" CssClass="rfvPCG">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revPan" runat="server" Display="Dynamic" CssClass="rfvPCG"
                            ErrorMessage="Please check PAN Format" ControlToValidate="txtPanNumber" ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}">
                        </asp:RegularExpressionValidator>
                        <asp:Label ID="lblPanDuplicate" runat="server" Visible="false" CssClass="Error" Text="PAN already exists"></asp:Label>
                    </td>
                </tr>
                <tr id="trSalutation" runat="server" visible="false">
                    <td class="leftField">
                        <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="Salutation:"></asp:Label>
                    </td>
                    <td class="rightField" width="75%">
                        <asp:DropDownList ID="ddlSalutation" runat="server" CssClass="cmbField">
                            <asp:ListItem>Select a Salutation</asp:ListItem>
                            <asp:ListItem>Mr.</asp:ListItem>
                            <asp:ListItem>Mrs.</asp:ListItem>
                            <asp:ListItem>Ms.</asp:ListItem>
                            <asp:ListItem>M/S.</asp:ListItem>
                            <asp:ListItem>Dr.</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="trIndividualName" runat="server">
                    <td class="leftField">
                        <asp:Label ID="lblName" runat="server" CssClass="FieldName" Text="Name:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtFirstName" runat="server" MaxLength="75" Style="width: 30%" CssClass="txtField"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtFirstName_TextBoxWatermarkExtender" runat="server"
                            Enabled="True" TargetControlID="txtFirstName" WatermarkText="FirstName">
                        </cc1:TextBoxWatermarkExtender>
                        <span id="Span32" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" ControlToValidate="txtFirstName"
                            ErrorMessage="<br />Please enter the First Name" ValidationGroup="btnCustomerSubmit"
                            Display="Dynamic" runat="server" CssClass="rfvPCG">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="trNonIndividualName" runat="server">
                    <td class="leftField">
                        <asp:Label ID="lblCompanyName" runat="server" CssClass="FieldName" Text="Company Name:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCompanyName" CssClass="txtField" Style="width: 30%" runat="server"></asp:TextBox>
                        <span id="Span33" class="spnRequiredField">*</span>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" ControlToValidate="txtCompanyName"
                            ErrorMessage="Please enter the Company Name" Display="Dynamic" ValidationGroup="btnCustomerSubmit"
                            runat="server" CssClass="rfvPCG">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblEmail" runat="server" CssClass="FieldName" Text="Email Id:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" Style="width: 30%" CssClass="txtField"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtEmail"
                            ErrorMessage="Please enter a valid Email ID" Display="Dynamic" runat="server"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="revPCG"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label20" runat="server" CssClass="FieldName" Text="Mobile No:"></asp:Label>
                    </td>
                    <td class="rightField" colspan="3">
                        <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
                        <span id="Span34" class="spnRequiredField">*</span>
                        <br />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator14" ControlToValidate="txtMobileNumber"
                            Display="Dynamic" runat="server" CssClass="rfvPCG" ErrorMessage="Not acceptable format"
                            ValidationExpression="^\d{10,10}$">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" ControlToValidate="txtMobileNumber"
                            ErrorMessage="Please enter a Contact Number" ValidationGroup="btnCustomerSubmit"
                            Display="Dynamic" runat="server" CssClass="rfvPCG">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr id="trBranchlist" runat="server" visible="false">
                    <td>
                        <asp:Label ID="Label9" runat="server" CssClass="FieldName" Text="Branch Name:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAdviserBranchList" AutoPostBack="true" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                        <span id="Span35" class="spnRequiredField">*</span>
                        <br />
                        <asp:CompareValidator ID="ddlAdviserBranchList_CompareValidator2" runat="server"
                            ControlToValidate="ddlAdviserBranchList" ErrorMessage="Please select a Branch"
                            Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"
                            ValidationGroup="btnCustomerSubmit">
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
                        <span id="Span36" class="spnRequiredField">*</span>
                        <br />
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlAdviseRMList"
                            ErrorMessage=" " Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG"
                            Display="Dynamic" ValidationGroup="btnCustomerSubmit">
                        </asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                    </td>
                    <td class="rightField">
                        <asp:Button ID="btnChannelSubmit" runat="server" Text="Submit" CssClass="PCGButton"
                            ValidationGroup="btnCustomerSubmit" OnClick="btnCustomerSubmit_Click" />
                        <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
</telerik:RadWindow>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="true" EnableViewState="true">
    <ContentTemplate>
        <telerik:RadWindow ID="radwindowPopup" runat="server" VisibleOnPageLoad="false" Height="10%"
            Width="400px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Title="Add New Folio"
            OnClientShow="setCustomPosition" Behaviors="close,Move">
            <ContentTemplate>
                <div style="padding: 20px">
                    <table width="100%">
                        <tr>
                            <td class="leftField" style="width: 10%">
                                <asp:Label ID="lblAMCName" runat="server" Text="AMC Name: " CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" style="width: 25%">
                                <asp:Label ID="lblFolioAMC" runat="server" Text="" CssClass="FieldName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" style="width: 10%">
                                <asp:Label ID="lblFolioNo" runat="server" Text="Folio Number: " CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" style="width: 25%">
                                <asp:TextBox ID="txtNewFolio" runat="server" CssClass="txtField"></asp:TextBox><br />
                                <span id="spnNewFolioValidation"></span>
                                <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtNewFolio" ErrorMessage="Please enter folio name"
                                    ValidationGroup="vgOK" Display="Dynamic" runat="server" CssClass="rfvPCG">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" style="width: 10%">
                                <asp:Button ID="btnSubmitFolio" runat="server" Text="Submit" CssClass="PCGButton"
                                    OnClick="btnOk_Click" ValidationGroup="vgOK" />
                            </td>
                            <td class="rightField" style="width: 25%">
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="PCGButton" CausesValidation="false"
                                    OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
        <telerik:RadWindow ID="radWindowSwitchScheme" runat="server" VisibleOnPageLoad="false"
            Height="30%" Width="400px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false"
            Behaviors="None" Title="Add New Folio">
            <ContentTemplate>
                <div style="padding: 20px">
                    <table width="100%">
                        <tr>
                            <td class="leftField" style="width: 10%">
                                <asp:Label ID="Label4" runat="server" Text="AMC Name: " CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" style="width: 25%">
                                <asp:Label ID="Label5" runat="server" Text="" CssClass="FieldName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" style="width: 10%">
                                <asp:Label ID="Label6" runat="server" Text="Folio Number: " CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" style="width: 25%">
                                <asp:TextBox ID="txtSwitchFolio" runat="server" CssClass="txtField"></asp:TextBox><br />
                                <span id="Span28"></span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ControlToValidate="txtSwitchFolio"
                                    ErrorMessage="Please enter folio name" ValidationGroup="vgSwitchOK" Display="Dynamic"
                                    runat="server" CssClass="rfvPCG">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" style="width: 10%">
                                <asp:Button ID="btnSwichSchemeOk" runat="server" Text="Submit" CssClass="PCGButton"
                                    OnClick="btnSwichSchemeOk_Click" ValidationGroup="vgSwitchOK" />
                            </td>
                            <td class="rightField" style="width: 25%">
                                <asp:Button ID="btnSwichSchemeCancel" runat="server" Text="Cancel" CssClass="PCGButton"
                                    CausesValidation="false" OnClick="btnSwichSchemeCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
        <table width="100%">
            <tr>
                <td colspan="5">
                    <div class="divPageHeading">
                        <table cellspacing="0" cellpadding="3" width="100%">
                            <tr>
                                <td align="left">
                                    MF Order Entry
                                </td>
                                <td align="right">
                                    <asp:LinkButton ID="lnkBtnEdit" runat="server" CssClass="LinkButtons" Text="Edit"
                                        OnClick="lnkBtnEdit_Click"></asp:LinkButton>
                                    &nbsp; &nbsp;
                                    <asp:LinkButton runat="server" ID="lnlBack" CssClass="LinkButtons" Text="Back" Visible="false"
                                        OnClick="lnlBack_Click"></asp:LinkButton>&nbsp; &nbsp;
                                    <asp:LinkButton runat="server" ID="lnkDelete" CssClass="LinkButtons" Text="Delete"
                                        OnClick="lnkDelete_Click" OnClientClick="javascript: return confirm('Are you sure you want to Delete the Order?')"></asp:LinkButton>&nbsp;
                                    &nbsp;
                                    <asp:Button ID="btnViewReport" runat="server" PostBackUrl="~/Reports/Display.aspx?mail=0"
                                        CssClass="CrystalButton" ValidationGroup="MFSubmit" OnClientClick="return CustomerValidate('View')" />&nbsp;&nbsp;
                                    <div id="div1" style="display: none;">
                                        <p class="tip">
                                            Click here to view order details.
                                        </p>
                                    </div>
                                    <asp:Button ID="btnViewInPDF" runat="server" ValidationGroup="MFSubmit" OnClientClick="return CustomerValidate('pdf')"
                                        PostBackUrl="~/Reports/Display.aspx?mail=2" CssClass="PDFButton" />&nbsp;&nbsp;
                                    <asp:Button ID="btnreport" runat="server" CssClass="CrystalButton" Visible="false"
                                        OnClick="btnreport_Click" />
                                    <asp:Button ID="btnpdfReport" runat="server" CssClass="PDFButton" Visible="false"
                                        OnClick="btnpdfReport_Click" />
                                    <div id="div2" style="display: none;">
                                        <p class="tip">
                                            Click here to view order details.
                                        </p>
                                    </div>
                                    <asp:Button ID="btnViewInDOC" runat="server" ValidationGroup="MFSubmit" CssClass="DOCButton"
                                        OnClientClick="return CustomerValidate('doc')" PostBackUrl="~/Reports/Display.aspx?mail=4"
                                        Visible="false" />
                                    <div id="div3" style="display: none;">
                                        <p class="tip">
                                            Click here to view order details in word doc.</p>
                                    </div>
                                    <asp:Button ID="btnViewInPDFNew" runat="server" ValidationGroup="MFSubmit" CssClass="PDFButton"
                                        Visible="false" OnClientClick="return CustomerValidate('pdf')" PostBackUrl="~/Reports/Display.aspx?mail=2" />
                                    <asp:Button ID="btnViewInDOCNew" runat="server" ValidationGroup="MFSubmit" CssClass="DOCButton"
                                        Visible="false" OnClientClick="return CustomerValidate('doc')" PostBackUrl="~/Reports/Display.aspx?mail=4" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr id="trMessage" runat="server">
                <td colspan="6">
                    <table class="tblMessage" cellspacing="0">
                        <tr>
                            <td align="center">
                                <div id="div4" align="center">
                                </div>
                                <div style="clear: both">
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="tblMessage" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center">
                    <div id="divMessage" align="center">
                    </div>
                    <div style="clear: both">
                    </div>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnl_OrderSection" runat="server" class="Landscape" Width="120%" Height="300%"
            ScrollBars="None">
            <table width="100%">
                <tr>
                    <td colspan="6">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            Customer Details
                        </div>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="right" style="width: 15%;">
                        <asp:Label ID="lblsearch" runat="server" CssClass="FieldName" Text="Search for:"></asp:Label>
                    </td>
                    <td style="width: 23.5%">
                        <asp:DropDownList ID="ddlsearch" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlsearch_Selectedindexchanged"
                            AutoPostBack="true" TabIndex="1">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Customer Name" Value="1"></asp:ListItem>
                            <asp:ListItem Text="PAN" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblARNNo" runat="server" CssClass="FieldName" Text="ARN No:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlARNNo" runat="server" CssClass="cmbField" AutoPostBack="false"
                            TabIndex="2">
                        </asp:DropDownList>
                        <span id="Span14" class="spnRequiredField">*</span>
                        <asp:CompareValidator ID="CompareValidator12" runat="server" ControlToValidate="ddlARNNo"
                            CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an ARN"
                            Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
                    </td>
                </tr>
                <tr id="trpan" runat="server">
                    <td align="right">
                        <asp:Label ID="lblPansearch" runat="server" Text="PAN:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPansearch" runat="server" CssClass="txtField" AutoComplete="Off"
                            AutoPostBack="True" onclientClick="ShowIsa()" onblur="return checkItemSelected(this)"
                            OnTextChanged="OnAssociateTextchanged1" TabIndex="3">
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
                            ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                    </td>
                    <td align="right" style="width: 15.4%;">
                        <asp:Label ID="label2" runat="server" Text="Customer Name: " CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblgetcust" runat="server" Text="" CssClass="FieldName" onclientClick="CheckPanno()"></asp:Label>
                    </td>
                </tr>
                <tr id="trCust" runat="server">
                    <td align="right">
                        <asp:Label ID="lblCustomer" runat="server" Text="Customer Name:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <%-- OnClientClick="return openpopupAddCustomer()"--%>
                        <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txtField" AutoComplete="Off"
                            onclientClick="ShowIsa()" AutoPostBack="True" TabIndex="4" Width="200px">
                        </asp:TextBox><span id="spnCustomer" class="spnRequiredField">*</span>
                        <asp:ImageButton ID="btnImgAddCustomer" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                            AlternateText="Add" runat="server" ToolTip="Click here to Add Customer" OnClick="openpopupAddCustomer_Click"
                            Height="15px" Width="15px" CausesValidation="false"></asp:ImageButton>
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
                            CssClass="rfvPCG" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                    </td>
                    <td align="right" style="width: 15.4%;">
                        <asp:Label ID="lblPan" runat="server" Text="PAN No: " CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblgetPan" runat="server" Text="" CssClass="FieldName"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblRM" runat="server" Text="RM: " CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblGetRM" runat="server" Text="" CssClass="FieldName"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblBranch" runat="server" Text="Branch: " CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblGetBranch" runat="server" Text="" CssClass="FieldName"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label1" runat="server" Text="EUIN: " CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lb1EUIN" runat="server" Text="" CssClass="FieldName"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblAssociateSearch" runat="server" CssClass="FieldName" Text="Sub Broker Code:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAssociateSearch" runat="server" CssClass="txtField" AutoComplete="Off"
                            OnTextChanged="OnAssociateTextchanged" AutoPostBack="True" TabIndex="5">
                        </asp:TextBox><span id="Span7" class="spnRequiredField">*</span>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" TargetControlID="txtAssociateSearch"
                            WatermarkText="Enter few chars of Sub Broker Code" runat="server" EnableViewState="false">
                        </cc1:TextBoxWatermarkExtender>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtAssociateSearch"
                            ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                            OnClientItemSelected="GeAgentId" MinimumPrefixLength="1" EnableCaching="False"
                            CompletionSetCount="5" CompletionInterval="100" CompletionListCssClass="AutoCompleteExtender_CompletionList"
                            CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ShowOnlyCurrentWordInCompletionListItem="true" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAssociateSearch"
                            ErrorMessage="<br />Please Enter a Sub Broker Code" Display="Dynamic" runat="server"
                            CssClass="rfvPCG" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblAssociate" runat="server" CssClass="FieldName" Text="Associate:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAssociatetext" runat="server" CssClass="FieldName" Enabled="false"></asp:Label>
                    </td>
                    <td>
                        <%-- <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Reporting To:"></asp:Label>
                        <asp:Label ID="lb1RepTo" runat="server" CssClass="FieldName" Enabled="false"></asp:Label>
                    --%>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                    </td>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Reporting To:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lb1RepTo" runat="server" CssClass="FieldName" Enabled="false"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr id="trIsa" runat="server">
                    <td align="right">
                        <asp:Label ID="lblIsa" runat="server" CssClass="FieldName" Text="ISA No:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCustomerISAAccount" runat="server" CssClass="cmbField" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCustomerISAAccount_SelectedIndexChanged" TabIndex="6">
                        </asp:DropDownList>
                        &nbsp
                        <asp:ImageButton ID="btnIsa" ImageUrl="~/App_Themes/Maroon/Images/user_add.png" AlternateText="Add"
                            runat="server" ToolTip="Click here to Request ISA" OnClick="ISA_Onclick" Height="15px"
                            Width="15px"></asp:ImageButton>
                    </td>
                </tr>
                <tr id="trJointHoldersList" runat="server">
                    <td align="right">
                    </td>
                    <td>
                        <telerik:RadGrid ID="gvJointHoldersList" Height="70px" runat="server" GridLines="None"
                            AutoGenerateColumns="False" Width="45%" PageSize="4" AllowSorting="false" AllowPaging="True"
                            ShowStatusBar="True" ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false"
                            AllowFilteringByColumn="false" AllowAutomaticInserts="false" ExportSettings-FileName="Count">
                            <MasterTableView Width="100%" AllowMultiColumnSorting="false" AutoGenerateColumns="false"
                                CommandItemDisplay="None">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="AccountNumber" HeaderText="Account Number" UniqueName="AccountNumber"
                                        SortExpression="Account Number">
                                        <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Customer" HeaderText="Customer" AllowFiltering="false"
                                        HeaderStyle-HorizontalAlign="Left" UniqueName="Customer">
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ModeOfHolding" HeaderText="Mode Of Holding" AllowFiltering="false"
                                        HeaderStyle-HorizontalAlign="Left" UniqueName="ModeOfHolding">
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
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
        <asp:Panel ID="pnl_OrderDetailsSection" runat="server" class="Landscape" Width="100%"
            Height="300%" ScrollBars="None">
            <table width="120%">
                <tr>
                    <td colspan="6">
                        <div class="divSectionHeading" style="vertical-align: text-bottom">
                            Order Details
                        </div>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr id="trTransactionType" runat="server">
                    <td align="right" style="width: 18%">
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="ddlTransactionType" runat="server" Text="Transaction Type: "
                            CssClass="FieldName"></asp:Label>
                    </td>
                    <td style="width: 30px;">
                        <asp:DropDownList ID="ddltransType" runat="server" CssClass="cmbField" AutoPostBack="true"
                            OnSelectedIndexChanged="ddltransType_SelectedIndexChanged" TabIndex="7">
                            <asp:ListItem Text="Select" Value="Select" Selected="true"></asp:ListItem>
                            <asp:ListItem Text="New Purchase" Value="BUY"></asp:ListItem>
                            <asp:ListItem Text="Additional Purchase" Value="ABY"></asp:ListItem>
                            <asp:ListItem Text="Redemption" Value="Sel"></asp:ListItem>
                            <asp:ListItem Text="SIP" Value="SIP"></asp:ListItem>
                            <asp:ListItem Text="SWP" Value="SWP"></asp:ListItem>
                            <asp:ListItem Text="STP" Value="STB" Enabled="true"></asp:ListItem>
                            <asp:ListItem Text="Switch" Value="SWB" Enabled="true"></asp:ListItem>
                            <asp:ListItem Text="Change Of Address Form" Value="CAF" Enabled="false"></asp:ListItem>
                            <asp:ListItem Text="Transfer IN" Value="TI" Enabled="false"></asp:ListItem>
                            <asp:ListItem Text="NFO" Value="NFO" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                        <span id="spnTransType" class="spnRequiredField">*</span>
                        <asp:CompareValidator ID="CVTrxType" runat="server" ControlToValidate="ddltransType"
                            CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a transaction type"
                            Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblReceivedDate" runat="server" Text="App. Recv. Date:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="txtReceivedDate" CssClass="txtField" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                            AutoPostBack="true" OnSelectedDateChanged="txtReceivedDate_SelectedDateChanged"
                            TabIndex="8">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Telerik" EnableEmbeddedSkins="false">
                                <SpecialDays>
                                </SpecialDays>
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <span id="Span7" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="appRecidRequiredFieldValidator" ControlToValidate="txtReceivedDate"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select an Application received Date"
                            Display="Dynamic" runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="trARDate" runat="server">
                    <td align="right">
                        <asp:Label ID="lblApplicationNumber" runat="server" Text="Application Number:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtApplicationNumber" runat="server" CssClass="txtField" TabIndex="9"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblOrderDate" runat="server" Text="Order Date:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="txtOrderDate" CssClass="txtField" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                            AutoPostBack="true" OnSelectedDateChanged="txtOrderDate_DateChanged" TabIndex="10">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <span id="Span6" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtOrderDate"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select order date" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="trAplNumber" runat="server">
                    <td align="right">
                        <asp:Label ID="lblAMC" runat="server" Text="AMC: " CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAMCList" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlAMCList_SelectedIndexChanged" TabIndex="11">
                        </asp:DropDownList>
                        <span id="spnAMC" runat="server" class="spnRequiredField">*</span>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlAMCList"
                            CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an AMC"
                            Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"
                            Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" TabIndex="12" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="trOrderDate" runat="server">
                    <td align="right">
                        <asp:Label ID="lblSearchScheme" runat="server" Text="Scheme:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td style="width: 24.3%">
                        <asp:HiddenField ID="txtSchemeCode" runat="server" OnValueChanged="txtSchemeCode_ValueChanged" />
                        <asp:TextBox ID="txtSearchScheme" runat="server" Style="width: 280px;" CssClass="txtField"
                            AutoComplete="Off" AutoPostBack="True" TabIndex="13">
                        </asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtSearchScheme_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtSearchScheme" WatermarkText="Type the Scheme Name">
                        </cc1:TextBoxWatermarkExtender>
                        <ajaxToolkit:AutoCompleteExtender ID="txtSearchScheme_autoCompleteExtender" runat="server"
                            TargetControlID="txtSearchScheme" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                            ServiceMethod="GetSchemeName" MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5"
                            CompletionInterval="100" CompletionListCssClass="AutoCompleteExtender_CompletionList"
                            CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                            UseContextKey="true" OnClientItemSelected="GetSchemeCode" DelimiterCharacters="" />
                        <span id="Span9" class="spnRequiredField" runat="server">*<br />
                        </span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtSearchScheme"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select Scheme" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                    </td>
                    <td align="right" id="tdlblDivType">
                        <asp:Label ID="lblDivType" runat="server" Text="Dividend Type:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td id="tdddlDivType">
                        <asp:DropDownList ID="ddlDivType" runat="server" CssClass="cmbField" TabIndex="14">
                            <%--<asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Dividend Reinvestment" Value="DVR"></asp:ListItem>
                            <asp:ListItem Text="Dividend Payout" Value="DVP"></asp:ListItem>--%>
                        </asp:DropDownList>
                        <span id="Span29" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" CssClass="rfvPCG"
                            ErrorMessage="Please Select an Dividend Type" Display="Dynamic" ControlToValidate="ddlDivType"
                            InitialValue="0" ValidationGroup="btnSubmit">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <%-- <td align="right">
                        <asp:Label ID="lblDivType" runat="server" Text="Dividend Type:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDivType" runat="server" CssClass="cmbField" Style="width: 250px;">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Dividend Reinvestment" Value="DVR"></asp:ListItem>
                            <asp:ListItem Text="Dividend Payout" Value="DVP"></asp:ListItem>
                        </asp:DropDownList>
                        <span id="Span29" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" CssClass="rfvPCG"
                            ErrorMessage="Please Select an Dividend Type" Display="Dynamic" ControlToValidate="ddlDivType"
                            InitialValue="0" ValidationGroup="btnSubmit">
                        </asp:RequiredFieldValidator>
                    </td>--%>
                </tr>
                <tr class="spaceUnder">
                    <td>
                    </td>
                    <td align="left" style="vertical-align: top;" colspan="5">
                        <table width="70%" class="SchemeInfoTable">
                            <tr class="SchemeInfoTable">
                                <td align="left" style="vertical-align: top;">
                                    <asp:Label ID="lblNav" runat="server" Text=" Last Recorded NAV (Rs):" CssClass="FieldName"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblNavDisplay" runat="server" CssClass="readOnlyField"></asp:Label>
                                </td>
                                <td align="left" style="vertical-align: top;">
                                    <asp:Label ID="lblMin" runat="server" Text="Minimum Initial Amount:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblMintxt" runat="server" CssClass="readOnlyField"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td align="left" style="vertical-align: top;" runat="server" visible="false">
                                    <asp:Label ID="lblCutt" runat="server" Text="Cut-Off time:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td runat="server" visible="false">
                                    <asp:Label ID="lbltime" runat="server" Text="" CssClass="readOnlyField"></asp:Label>
                                </td>
                                <td align="left" style="vertical-align: top;">
                                    <asp:Label ID="lblMultiple" runat="server" Text="Subsequent Amount:</br>(In Multiples Of)"
                                        CssClass="FieldName"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblMulti" runat="server" CssClass="readOnlyField"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="Tr1" runat="server" visible="true">
                    <td align="right">
                        <asp:Label ID="lblFolioNumber" runat="server" Text="Folio Number:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:HiddenField ID="hidFolioNumber" runat="server" OnValueChanged="hidFolioNumber_ValueChanged" />
                        <asp:TextBox ID="txtFolioNumber" runat="server" CssClass="txtField" AutoPostBack="true"
                            TabIndex="15">
                        </asp:TextBox>
                        <span id="spnExistingFolio"></span>
                        <asp:ImageButton ID="imgFolioAdd" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                            AlternateText="Add" runat="server" ToolTip="Click here to Add folio" OnClick="btnOpenPopup_Click"
                            Height="15px" Width="15px" CausesValidation="false"></asp:ImageButton>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtFolioNumber"
                            WatermarkText="Enter Three Characters Of Folio">
                        </cc1:TextBoxWatermarkExtender>
                        <ajaxToolkit:AutoCompleteExtender ID="txtFolioNumber_autoCompleteExtender" runat="server"
                            TargetControlID="txtFolioNumber" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                            ServiceMethod="GetCustomerFolioAccount" MinimumPrefixLength="3" EnableCaching="false"
                            CompletionSetCount="1" CompletionInterval="1" CompletionListCssClass="AutoCompleteExtender_CompletionList"
                            CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                            UseContextKey="true" OnClientItemSelected="GetFolioAccount" />
                    </td>
                </tr>
                <tr runat="server" visible="false">
                    <td align="right">
                        <asp:DropDownList ID="ddlAmcSchemeList" runat="server" CssClass="cmbField" Width="400px"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlAmcSchemeList_SelectedIndexChanged"
                            TabIndex="16">
                        </asp:DropDownList>
                        <span id="spnScheme" runat="server" class="spnRequiredField">*</span>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlAmcSchemeList"
                            CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a scheme"
                            Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
                    </td>
                </tr>
                <tr id="trOrderNo" runat="server">
                    <td align="right">
                        <asp:Label ID="lblOrderNumber" runat="server" Text="Order Number:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblGetOrderNo" runat="server" Text="" CssClass="txtField"></asp:Label>
                    </td>
                    <td align="right" id="tdLblNav" runat="server" visible="false">
                        <asp:Label ID="Label19" runat="server" Text="Purchase Price:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td id="tdtxtNAV" runat="server" visible="false">
                        <asp:TextBox ID="txtNAV" runat="server" CssClass="txtField" onkeypress="return onlyNumbers();"
                            CausesValidation="true" ValidationGroup="MFSubmit" TabIndex="17"></asp:TextBox>
                        <span id="Span13" class="spnRequiredField">*</span>
                        <asp:RangeValidator ID="RangeValidator1" Display="Dynamic" ValidationGroup="MFSubmit"
                            runat="server" ErrorMessage="<br />Please enter a numeric value" ControlToValidate="txtNAV"
                            MaximumValue="2147483647" MinimumValue="0" Type="Double" CssClass="cvPCG"></asp:RangeValidator>
                    </td>
                    <td align="right" visible="false">
                        <asp:Label ID="lblPortfolio" runat="server" Text="Portfolio: " CssClass="FieldName"></asp:Label>
                    </td>
                    <td visible="false">
                        <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" TabIndex="18">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="trOrderType" runat="server" visible="false">
                    <td align="right">
                        <asp:Label ID="lblOrderType" runat="server" Text="Order Type:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButton ID="rbtnImmediate" Class="cmbField" runat="server" AutoPostBack="true"
                            GroupName="OrderType" Checked="True" Text="Immediate" OnCheckedChanged="rbtnImmediate_CheckedChanged" />
                        <asp:RadioButton ID="rbtnFuture" Class="cmbField" runat="server" AutoPostBack="true"
                            GroupName="OrderType" Text="Future" OnCheckedChanged="rbtnImmediate_CheckedChanged" />
                    </td>
                    <td align="right">
                        <asp:Label ID="lblPurDate" runat="server" Text="As on Date:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblNavAsOnDate" runat="server" CssClass="txtField" Enabled="false"></asp:Label>
                    </td>
                </tr>
                <tr id="trfutureDate" runat="Server" visible="false">
                    <td align="right">
                        <asp:Label ID="lblFutureDate" runat="server" Text="Select Future Date:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="txtFutureDate" CssClass="txtField" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                            TabIndex="19">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <span id="Span8" class="spnRequiredField">*</span>
                        <asp:CompareValidator ID="CVFutureDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                            Type="Date" ControlToValidate="txtFutureDate" CssClass="cvPCG" Operator="DataTypeCheck"
                            ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtFutureDate"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select Future Date" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvFutureDate1" runat="server" ControlToValidate="txtFutureDate"
                            CssClass="cvPCG" ErrorMessage="<br />Future date should  be greater than or equal to Today"
                            Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblFutureTrigger" runat="server" Text="Future Trigger:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFutureTrigger" runat="server" CssClass="txtField" TextMode="MultiLine"
                            TabIndex="18"></asp:TextBox>
                    </td>
                </tr>
                <tr class="spaceUnder" id="trDivtype" runat="server">
                    <td>
                    </td>
                    <%-- <td align="right" style="vertical-align: top;">
                    <asp:Label ID="lblDivType" runat="server" Text="Dividend Type:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDivType" runat="server" CssClass="cmbField" Style="width: 250px;">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Dividend Reinvestment" Value="DVR"></asp:ListItem>
                        <asp:ListItem Text="Dividend Payout" Value="DVP"></asp:ListItem>
                    </asp:DropDownList>
                    <span id="Span29" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Select an Dividend Type" Display="Dynamic" ControlToValidate="ddlDivType"
                        InitialValue="0" ValidationGroup="btnSubmit">
                    </asp:RequiredFieldValidator>
                </td>--%>
                    <td colspan="2">
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table width="120%">
            <tr>
                <td colspan="6">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        Payment Section
                    </div>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnl_SIP_PaymentSection" runat="server" class="Landscape" Width="100%"
            Height="80%" ScrollBars="None">
            <table id="tb_SIP_PaymentSection" width="100%">
                <tr id="trFrequency" runat="server">
                    <td align="right" style="width: 18%">
                        <asp:Label ID="lblFrequencySIP" runat="server" Text="Frequency:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFrequencySIP" runat="server" CssClass="cmbField" TabIndex="20"
                            OnSelectedIndexChanged="ddlFrequencySIP_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="trSIPStartDate" runat="server">
                    <td align="right">
                        <asp:Label ID="lblStartDateSIP" runat="server" Text="Start Date:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td style="width: 22%">
                        <asp:DropDownList ID="ddlStartDate" CssClass="cmbField" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlStartDate_SelectedIndexChanged" ValidationGroup="btnSubmit"
                            TabIndex="21" Visible="false">
                            <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                        </asp:DropDownList>
                        <telerik:RadDatePicker ID="txtstartDateSIP" CssClass="txtField" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                            TabIndex="22" Visible="true">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <span id="Span22" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ControlToValidate="ddlStartDate"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select an StartDate" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                        <%--<asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<br/>Please enter a valid date."
                            Type="Date" ControlToValidate="txtstartDateSIP" CssClass="cvPCG" Operator="DataTypeCheck"
                            ValueToCompare="" Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>--%>
                    </td>
                    <td align="right" style="width: 22.5%">
                        <asp:Label ID="lblEndDateSIP" runat="server" Text="End Date:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <%--<asp:DropDownList ID="ddlStartDate" CssClass="cmbField" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlStartDate_SelectedIndexChanged" ValidationGroup="btnSubmit">
                            <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                        </asp:DropDownList>--%>
                        <telerik:RadDatePicker ID="txtendDateSIP" CssClass="txtField" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                            Enabled="false" TabIndex="23">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Telerik" EnableEmbeddedSkins="false" TabIndex="22">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <span id="Span23" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ControlToValidate="txtendDateSIP"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select an EndDate" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="dateCompareValidator" runat="server" ControlToValidate="txtendDateSIP"
                            ControlToCompare="txtstartDateSIP" Operator="GreaterThanEqual" Type="Date" ValidationGroup="MFSubmit"
                            Display="Dynamic" ErrorMessage="To date Should be Greater Than or Equal to From Date"
                            CssClass="rfvPCG">
                        </asp:CompareValidator>
                    </td>
                </tr>
                <tr id="trSystematicDateChk1" runat="server" visible="false">
                    <td align="right">
                        <asp:Label ID="lblSystematicDate" runat="server" Text="Date of Systematic Trx:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSystematicdates" runat="server" CssClass="txtField" TabIndex="24"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSystematicdates">
                        </cc1:CalendarExtender>
                        <span id="Span24" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ControlToValidate="txtSystematicdates"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select Date of Systematic Trx" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                        <%-- <asp:RangeValidator ID="RangeValidator4" Display="Dynamic" ValidationGroup="MFSubmit"
                            runat="server" ErrorMessage="<br />Date of Systematic Trx between 1 to 30" ControlToValidate="txtSystematicdates"
                            MaximumValue="30" MinimumValue="1" Type="Integer" CssClass="cvPCG"></asp:RangeValidator>--%>
                    </td>
                </tr>
                <tr id="trSystematicDate" runat="server">
                    <td align="right">
                        <asp:Label ID="lblPeriod" runat="server" Text="Total Installments:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTotalInstallments" CssClass="cmbField" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlTotalInstallments_SelectedIndexChanged" TabIndex="25">
                            <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtPeriod" runat="server" CssClass="txtField" AutoPostBack="true"
                            OnTextChanged="txtPeriod_OnTextChanged" Visible="false" TabIndex="26"></asp:TextBox>
                        <span id="Span21" class="spnRequiredField">*</span>
                        <asp:DropDownList ID="ddlPeriodSelection" runat="server" AutoPostBack="true" CssClass="cmbField"
                            CausesValidation="true" ValidationGroup="MFSubmit" OnSelectedIndexChanged="ddlPeriodSelection_SelectedIndexChanged"
                            Visible="false" TabIndex="27">
                            <asp:ListItem Text="Days" Value="DA"></asp:ListItem>
                            <asp:ListItem Text="Months" Value="MN"></asp:ListItem>
                            <asp:ListItem Text="Years" Value="YR"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lblUnits" runat="server" Text="&nbsp;&nbsp;(Units)" Visible="false"
                            CssClass="FieldName"></asp:Label>
                        <asp:RequiredFieldValidator ID="rfvPeriod" ControlToValidate="ddlTotalInstallments"
                            ErrorMessage="<br />Please Enter a Period" Display="Dynamic" runat="server" CssClass="rfvPCG"
                            ValidationGroup="MFSubmit">
                        </asp:RequiredFieldValidator>&nbsp;&nbsp;
                        <asp:CompareValidator ID="CompareValidator_txtPeriod" runat="server" ControlToValidate="ddlTotalInstallments"
                            ErrorMessage="<br />Please Enter a numeric Value" Operator="DataTypeCheck" Type="Integer"
                            ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic" ValidationGroup="MFSubmit">
                        </asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlTotalInstallments"
                            ErrorMessage="<br />Please update the  value" Operator="GreaterThan" Type="Integer"
                            ValueToCompare="0" CssClass="cvPCG" Display="Dynamic" ValidationGroup="MFSubmit">
                        </asp:CompareValidator>
                    </td>
                </tr>
                <%--<tr id="trSection2" runat="server" visible="false">
                <td colspan="5">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        Order Section Details
                    </div>
                </td>
            </tr>--%>
                <tr id="trGetAmount" runat="server" visible="false">
                    <td class="leftField" style="width: 20%">
                        <asp:Label ID="lblAvailableAmount" runat="server" Text="Available Amount:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField" style="width: 20%">
                        <asp:Label ID="lblGetAvailableAmount" runat="server" Text="" CssClass="txtField"></asp:Label>
                    </td>
                    <td class="leftField" style="width: 20%">
                        <asp:Label ID="lblAvailableUnits" runat="server" Text="Available Units:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField" style="width: 20%">
                        <asp:Label ID="lblGetAvailableUnits" runat="server" Text="" CssClass="txtField"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnl_SEL_PaymentSection" runat="server" class="Landscape" Width="100%"
            Height="80%" ScrollBars="None">
            <table id="tb_SEL_PaymentSection" width="100%">
                <tr id="trRedeemed" runat="server">
                    <td align="right" style="width: 18%">
                        <asp:Label ID="lblReedeemed" runat="server" Text="Redeem/Switch:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td style="width: 25%">
                        <asp:RadioButton ID="rbtAmount" Class="cmbFielde" runat="server" GroupName="AmountUnit"
                            Checked="True" Text="Amount" />
                        <asp:RadioButton ID="rbtUnit" Class="cmbFielde" runat="server" GroupName="AmountUnit"
                            Text="Units" />
                    </td>
                    <td align="right">
                        <asp:Label ID="lblAmountUnits" runat="server" Text="Amount/Unit:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNewAmount" runat="server" CssClass="txtField" TabIndex="28"></asp:TextBox><span
                            id="Span2" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtNewAmount"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select Amount/Unit" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator9" ControlToValidate="txtNewAmount" runat="server"
                            Display="Dynamic" ErrorMessage="<br />Please enter a numeric value" Type="Double"
                            Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                        <asp:RangeValidator ID="RangeValidator3" Display="Dynamic" ValidationGroup="MFSubmit"
                            runat="server" ErrorMessage="<br />Please enter a valid amount" ControlToValidate="txtNewAmount"
                            MaximumValue="2147483647" MinimumValue="1" Type="Double" CssClass="cvPCG"></asp:RangeValidator>
                    </td>
                </tr>
                <tr id="trScheme" runat="server" visible="false">
                    <td align="right">
                        <asp:Label ID="lblSchemeSwitch" runat="server" Text="To Scheme:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td style="width: 22%;">
                        <asp:DropDownList ID="ddlSchemeSwitch" runat="server" CssClass="cmbLongField" TabIndex="29">
                        </asp:DropDownList>
                        <span id="Span3" runat="server" class="spnRequiredField">*</span>
                        <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="ddlSchemeSwitch"
                            CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a scheme"
                            Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label3" runat="server" Text="Folio Number:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:HiddenField ID="hdnSwitchFolioNo" runat="server" OnValueChanged="hidFolioNumber_ValueChanged" />
                        <asp:TextBox ID="txtSwitchFolioNo" runat="server" CssClass="txtField" AutoPostBack="true"
                            TabIndex="30">
                        </asp:TextBox>
                        <span id="Span27"></span>
                        <asp:ImageButton ID="ImageButton1" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                            AlternateText="Add" runat="server" ToolTip="Click here to Add folio" OnClick="btnSwitchOpenPopup_Click"
                            Height="15px" Width="15px"></asp:ImageButton>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtSwitchFolioNo"
                            WatermarkText="Type the folio name">
                        </cc1:TextBoxWatermarkExtender>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="txtSwitchFolioNo"
                            ServicePath="~/CustomerPortfolio/AutoComplete.asmx" ServiceMethod="GetCustomerFolioAccount"
                            MinimumPrefixLength="3" EnableCaching="false" CompletionSetCount="1" CompletionInterval="1"
                            CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                            CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                            UseContextKey="true" OnClientItemSelected="GetSwitchFolioAccount" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnl_BUY_ABY_SIP_PaymentSection" runat="server" class="Landscape" Width="100%"
            Height="80%" ScrollBars="None">
            <table id="tb_BUY_ABY_SIP_PaymentSection" width="100%">
                <%--<tr id="trSection1" runat="server">
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Order Section Details
            </div>
        </td>
    </tr>--%>
                <tr id="trAmount" runat="server">
                    <td align="right" style="width: 18%;">
                        <asp:Label ID="lblAmount" runat="server" Text="Amount:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td style="width: 22%;">
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField" CausesValidation="true"
                            ValidationGroup="MFSubmit" TabIndex="31"></asp:TextBox><span id="Span5" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtAmount"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select amount" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator2" Display="Dynamic" ValidationGroup="MFSubmit"
                            runat="server" ErrorMessage="<br />Please enter a numeric value" ControlToValidate="txtAmount"
                            MaximumValue="2147483647" MinimumValue="1" Type="Double" CssClass="cvPCG"></asp:RangeValidator>
                    </td>
                    <td align="right" style="width: 22.5%;">
                        <asp:Label ID="lblMode" runat="server" Text="Mode Of Payment:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged"
                            AutoPostBack="true" TabIndex="32">
                            <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                            <asp:ListItem Text="Cheque" Value="CQ"></asp:ListItem>
                            <asp:ListItem Text="Draft" Value="DF"></asp:ListItem>
                            <asp:ListItem Text="ECS" Value="ES"></asp:ListItem>
                        </asp:DropDownList>
                        <span id="Span10" class="spnRequiredField">*</span>
                        <asp:CompareValidator ID="CompareValidator13" runat="server" ControlToValidate="ddlPaymentMode"
                            CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select  Mode Of Payment"
                            Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
                    </td>
                </tr>
                <tr id="trPINo" runat="server" visible="false">
                    <td align="right">
                        <asp:Label ID="lblPaymentNumber" runat="server" Text="Payment Instrument Number: "
                            CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPaymentNumber" runat="server" MaxLength="6" CssClass="txtField"
                            TabIndex="33"></asp:TextBox>
                        <span id="Span12" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtPaymentNumber"
                            ErrorMessage="<br />Please Enter a Payment Instrument No." Display="Dynamic"
                            runat="server" CssClass="rfvPCG" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblPIDate" runat="server" Text="Payment Instrument Date:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="txtPaymentInstDate" CssClass="txtField" runat="server"
                            Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                            ShowAnimation-Type="Fade" AutoPostBack="true" MinDate="1900-01-01" TabIndex="34"
                            OnSelectedDateChanged="txtPaymentInstDate_OnSelectedDateChanged">
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
                            ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                        <%--<asp:CompareValidator ID="CVPaymentdate2" runat="server" ErrorMessage="<br/>Payment date cannot be greater than order date"
                 ControlToValidate="txtPaymentInstDate" ValidationGroup="MFSubmit" CssClass="cvPCG" Operator="LessThanEqual" Display="Dynamic"
               Type="Date"></asp:CompareValidator>--%>
                        <%--<asp:CompareValidator ID="cvdate" runat="server" ErrorMessage="<br />Payment Instrument Date should be less than or equal to Order Date"
                Type="Date" ControlToValidate="txtPaymentInstDate" ControlToCompare="txtOrderDate"
                Operator="LessThanEqual" CssClass="cvPCG" Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>--%>
                    </td>
                </tr>
                <tr id="trBankName" runat="server">
                    <td align="right">
                        <asp:Label ID="lblBankName" runat="server" Text="Bank Name:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBankName" runat="server" CssClass="cmbField" AutoPostBack="true"
                            AppendDataBoundItems="true" OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged"
                            TabIndex="35">
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
                            Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ControlToValidate="ddlBankName"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select an Bank" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                        <%--      
            <asp:CompareValidator ID="CompareValidator11" runat="server" ControlToValidate="ddlBankName"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a Bank"
                Operator="NotEqual" ValidationGroup="MFSubmit" ValueToCompare="Select"></asp:CompareValidator>--%>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblBranchName" runat="server" Text="Bank Branch:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="cmbField" AutoPostBack="false"
                            AppendDataBoundItems="true" TabIndex="36" Visible="false">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtBranchName" runat="server" CssClass="txtField" TabIndex="37"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table id="Table1" runat="server" visible="false">
            <tr id="trFrequencySTP" runat="server" visible="false">
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblFrequencySTP" runat="server" Text="Frequency:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:DropDownList ID="ddlFrequencySTP" runat="server" CssClass="cmbField" TabIndex="38">
                    </asp:DropDownList>
                </td>
                <td class="leftField" colspan="2" style="width: 40%">
                </td>
            </tr>
            <tr id="trSTPStart" runat="server" visible="false">
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblstartDateSTP" runat="server" Text="Start Date:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <telerik:RadDatePicker ID="txtstartDateSTP" CssClass="txtField" runat="server" Culture="English (United States)"
                        Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                        TabIndex="39">
                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                            Skin="Telerik" EnableEmbeddedSkins="false">
                        </Calendar>
                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                    <span id="Span25" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ControlToValidate="txtstartDateSTP"
                        CssClass="rfvPCG" ErrorMessage="<br />Please select StartDate" Display="Dynamic"
                        runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                </td>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblendDateSTP" runat="server" Text="End Date:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <telerik:RadDatePicker ID="txtendDateSTP" CssClass="txtField" runat="server" Culture="English (United States)"
                        Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                        TabIndex="40">
                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                            Skin="Telerik" EnableEmbeddedSkins="false">
                        </Calendar>
                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                    <span id="Span26" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ControlToValidate="txtendDateSTP"
                        CssClass="rfvPCG" ErrorMessage="<br />Please select End Date" Display="Dynamic"
                        runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator15" runat="server" ErrorMessage="<br/>To date should be greater than from date."
                        Type="Date" ControlToValidate="txtendDateSTP" CssClass="cvPCG" Operator="GreaterThan"
                        ControlToCompare="txtstartDateSTP" Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>
                </td>
            </tr>
        </table>
        <table id="Table2" visible="false" runat="server">
            <tr id="trSection3" runat="server">
                <td colspan="5">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        Order Section Details
                    </div>
                </td>
            </tr>
            <tr id="trAddress1" runat="server">
                <td colspan="4">
                    <asp:Label ID="Label23" CssClass="HeaderTextSmall" runat="server" Text="Current Address"></asp:Label>
                </td>
            </tr>
            <tr id="trOldLine1" runat="server">
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblLine1" CssClass="FieldName" runat="server" Text="Line1(House No./Building):"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:Label ID="lblGetLine1" CssClass="txtField" runat="server" Text=""></asp:Label>
                </td>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblLine2" CssClass="FieldName" runat="server" Text="Line2(Street):"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:Label ID="lblGetLine2" CssClass="txtField" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr id="trOldLine3" runat="server">
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblLine3" CssClass="FieldName" runat="server" Text="Line3(Area):"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:Label ID="lblGetline3" CssClass="txtField" runat="server" Text=""></asp:Label>
                </td>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblLivingSince" CssClass="FieldName" runat="server" Text="Living Since:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:Label ID="lblGetLivingSince" CssClass="txtField" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr id="trOldCity" runat="server">
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblCity" CssClass="FieldName" runat="server" Text="City:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:Label ID="lblgetCity" CssClass="txtField" runat="server" Text=""></asp:Label>
                </td>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblstate" CssClass="FieldName" runat="server" Text="State:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:Label ID="lblGetstate" CssClass="FieldName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr id="trOldPin" runat="server">
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblPin" CssClass="FieldName" runat="server" Text="Pin Code:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:Label ID="lblGetPin" CssClass="txtField" runat="server" Text=""></asp:Label>
                </td>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblCountry" CssClass="FieldName" runat="server" Text="Country:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:Label ID="lblGetCountry" CssClass="txtField" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr id="trAddress6" runat="server">
                <td colspan="4">
                    <asp:Label ID="Label18" CssClass="HeaderTextSmall" runat="server" Text="New Address"></asp:Label>
                </td>
            </tr>
            <tr id="trNewLine1" runat="server">
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblAdrLine1" CssClass="FieldName" runat="server" Text="Line1(House No./Building):"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:TextBox ID="txtCorrAdrLine1" runat="server" CssClass="txtField" TabIndex="41"></asp:TextBox>
                    <span id="Span15" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtCorrAdrLine1"
                        CssClass="rfvPCG" ErrorMessage="<br />Please Enter Details" Display="Dynamic"
                        runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                </td>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblAdrLine2" CssClass="FieldName" runat="server" Text="Line2(Street):"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:TextBox ID="txtCorrAdrLine2" runat="server" CssClass="txtField" TabIndex="42"></asp:TextBox>
                    <span id="Span16" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtCorrAdrLine2"
                        CssClass="rfvPCG" ErrorMessage="<br />Please Enter Details" Display="Dynamic"
                        runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="trNewLine3" runat="server">
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblAdrLine3" CssClass="FieldName" runat="server" Text="Line3(Area):"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:TextBox ID="txtCorrAdrLine3" runat="server" CssClass="txtField" TabIndex="43"></asp:TextBox>
                    <span id="Span17" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtCorrAdrLine3"
                        CssClass="rfvPCG" ErrorMessage="<br />Please Enter Details" Display="Dynamic"
                        runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                </td>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblResidenceLivingDate" CssClass="FieldName" runat="server" Text="Living Since:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <telerik:RadDatePicker ID="txtLivingSince" CssClass="txtField" runat="server" Culture="English (United States)"
                        Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                        TabIndex="44">
                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                            Skin="Telerik" EnableEmbeddedSkins="false">
                        </Calendar>
                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ControlToValidate="txtLivingSince"
                        CssClass="rfvPCG" ErrorMessage="<br />Please Enter  a valid date" Display="Dynamic"
                        runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="txtLivingSince_CompareValidator" runat="server" ErrorMessage="<br/>Please enter a valid date."
                        Type="Date" ControlToValidate="txtLivingSince" CssClass="cvPCG" Operator="DataTypeCheck"
                        ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trNewCity" runat="server">
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblAdrCity" CssClass="FieldName" runat="server" Text="City:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:TextBox ID="txtCorrAdrCity" runat="server" CssClass="txtField" TabIndex="45"></asp:TextBox>
                    <span id="Span18" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtCorrAdrCity"
                        CssClass="rfvPCG" ErrorMessage="<br />Please Enter City" Display="Dynamic" runat="server"
                        InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                </td>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblAdrState" CssClass="FieldName" runat="server" Text="State:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:DropDownList ID="ddlCorrAdrState" runat="server" CssClass="cmbField" TabIndex="46">
                    </asp:DropDownList>
                    <span id="Span20" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="ddlCorrAdrState"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="<br />Please enter State" runat="server"
                        InitialValue="Select" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                    <%--   <asp:CompareValidator ID="CompareValidator6" ControlToValidate="ddlCorrAdrState"
                runat="server" Display="Dynamic" ErrorMessage="<br />Please enter State"
                Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>--%>
                </td>
            </tr>
            <tr id="trNewPin" runat="server">
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblAdrPinCode" CssClass="FieldName" runat="server" Text="Pin Code:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:TextBox ID="txtCorrAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"
                        TabIndex="47"></asp:TextBox>
                    <span id="Span19" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="txtCorrAdrPinCode"
                        CssClass="rfvPCG" ErrorMessage="<br />Please Enter PinCode" Display="Dynamic"
                        runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="txtCorrAdrPinCode_comparevalidator" ControlToValidate="txtCorrAdrPinCode"
                        runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value"
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                </td>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblAdrCountry" CssClass="FieldName" runat="server" Text="Country:"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:DropDownList ID="ddlCorrAdrCountry" runat="server" CssClass="cmbField" TabIndex="48">
                        <asp:ListItem Text="India" Value="India" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <%-- <tr id="trSystematicDateChk1" runat="server" visible="false">
        <td width="25%" class="leftField">
            <asp:Label ID="lblSystematicDate" runat="server" Text="Date of Systematic Trx:" CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtSystematicdates" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSystematicdates"
                Format="dd">
            </cc1:CalendarExtender>
            <span id="Span24" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ControlToValidate="txtSystematicdates"
                CssClass="rfvPCG" ErrorMessage="<br />Please select Date of Systematic Trx" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="RangeValidator4" Display="Dynamic" ValidationGroup="MFSubmit"
                runat="server" ErrorMessage="<br />Date of Systematic Trx between 1 to 30" ControlToValidate="txtSystematicdates"
                MaximumValue="30" MinimumValue="1" Type="Integer" CssClass="cvPCG"></asp:RangeValidator>
             
        </td>--%>
            <%-- </tr>--%>
        </table>
        <table>
            <tr id="trRegistrationDate" runat="server" visible="false">
                <td class="leftField" width="25%">
                    <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date in R&T system: "
                        CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRegistrationDate" runat="server" CssClass="txtField" TabIndex="49"></asp:TextBox>
                    <cc1:CalendarExtender ID="RegistrationDate_CalendarExtender" runat="server" TargetControlID="txtRegistrationDate"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="RegistrationDate_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtRegistrationDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <asp:CompareValidator ID="CompareValidator16" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtRegistrationDate" Operator="DataTypeCheck"
                        CssClass="cvPCG" ValidationGroup="MFSubmit" Display="Dynamic"></asp:CompareValidator>
                </td>
                <td>
                </td>
            </tr>
            <tr id="trCeaseDate" runat="server" visible="false">
                <td class="leftField" width="25%">
                    <asp:Label ID="lblCeaseDate" runat="server" Text="Stopped Date: " CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCeaseDate" runat="server" CssClass="txtField" TabIndex="50"></asp:TextBox>
                    <cc1:CalendarExtender ID="CeaseDate_CalendarExtender" runat="server" TargetControlID="txtCeaseDate"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="CeaseDate_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtCeaseDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <asp:CompareValidator ID="CompareValidator17" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtCeaseDate" Operator="DataTypeCheck" SipChequeDate_CalendarExtender="cvPCG"
                        Display="Dynamic" ValidationGroup="MFSubmit" CssClass="cvPCG"></asp:CompareValidator>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="lnkBtnEdit" />
        <asp:PostBackTrigger ControlID="btnImgAddCustomer" />
    </Triggers>
</asp:UpdatePanel>
<asp:Panel ID="Panel1" runat="server" class="Landscape" Width="100%" Height="600px"
    ScrollBars="None">
    <table runat="server">
        <tr id="trRemarks" runat="server">
            <td>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            </td>
            <td class="leftField">
                <asp:Label ID="lblRemarks" runat="server" Text="Remarks:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtRemarks" Width="300px" TextMode="MultiLine" MaxLength="300" Height="65px"
                    onkeydown="return (event.keyCode!=13);" runat="server" CssClass="txtField" TabIndex="51"></asp:TextBox>
            </td>
        </tr>
        <tr id="trBtnSubmit" runat="server">
            <td align="left" colspan="3">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" ValidationGroup="MFSubmit"
                    OnClick="btnSubmit_Click" TabIndex="52" />
                <asp:Button ID="btnAddMore" runat="server" Text="Save & Add More" CssClass="PCGMediumButton"
                    ValidationGroup="MFSubmit" OnClick="btnAddMore_Click" TabIndex="53" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="PCGButton" ValidationGroup="MFSubmit"
                    OnClick="btnUpdate_Click" TabIndex="54" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="LabelMainNote" runat="server" Text="Note: To print transaction slip with Payment Details, please save the order first."
                    Font-Size="Small" CssClass="cmbFielde" Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Panel>
<table>
</table>
<asp:Panel ID="pnlOrderSteps" runat="server" Width="100%" Height="80%" Visible="false">
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="rgvOrderSteps" runat="server" Skin="Telerik" CssClass="RadGrid"
                    Width="80%" GridLines="None" AllowPaging="True" PageSize="20" AllowSorting="false"
                    AutoGenerateColumns="False" OnItemCreated="rgvOrderSteps_ItemCreated" ShowStatusBar="true"
                    AllowAutomaticUpdates="false" HorizontalAlign="NotSet" DataKeyNames="CO_OrderId,WOS_OrderStepCode"
                    OnItemDataBound="rgvOrderSteps_ItemDataBound" OnItemCommand="rgvOrderSteps_ItemCommand"
                    OnNeedDataSource="rgvOrderSteps_NeedDataSource">
                    <MasterTableView CommandItemDisplay="none" EditMode="PopUp" EnableViewState="false">
                        <Columns>
                            <telerik:GridBoundColumn DataField="WOS_OrderStep" HeaderText="Stages" UniqueName="WOS_OrderStep"
                                ReadOnly="True">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Stages" UniqueName="WOS_OrderStepCode" DataField="WOS_OrderStepCode"
                                Visible="false" ReadOnly="True">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrderStepCode" runat="server" Text='<%#Eval("WOS_OrderStepCode")%>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="XS_StatusCode" HeaderText="Status" UniqueName="DropDownColumnStatus">
                                <EditItemTemplate>
                                    <telerik:RadComboBox ID="ddlCustomerOrderStatus" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerOrderStatus_OnSelectedIndexChanged"
                                        SelectedValue='<%#Bind("XS_StatusCode") %>' runat="server">
                                    </telerik:RadComboBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblOrderStatus" runat="server" Text='<%#Eval("XS_Status")%>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="XSR_StatusReasonCode" HeaderText="Pending Reason"
                                UniqueName="DropDownColumnStatusReason">
                                <EditItemTemplate>
                                    <telerik:RadComboBox ID="ddlCustomerOrderStatusReason" runat="server">
                                    </telerik:RadComboBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblOrderStatusReason" runat="server" Text='<%#Eval("XSR_StatusReason")%>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="lblCMFOS_Date" DataField="CMFOS_Date" HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblCMFOS_Date" runat="server" DataFormatString="{0:d}" Text='<%# DataBinder.Eval(Container.DataItem, "CMFOS_Date", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridEditCommandColumn UpdateText="Update" UniqueName="EditCommandColumn"
                                CancelText="Cancel">
                                <HeaderStyle></HeaderStyle>
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn DataField="COS_IsEditable" DataType="System.Boolean" UniqueName="COS_IsEditable"
                                Display="false" ReadOnly="True">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn Visible="false" UniqueName="lblStatus" DataField="XS_StatusCode">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatusCode" runat="server" Text='<%#Eval("XS_StatusCode")%>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged1" />
<asp:HiddenField ID="hdnCustomerId" runat="server" />
<asp:HiddenField ID="hdnType" runat="server" />
<asp:HiddenField ID="hdnSchemeCode" runat="server" OnValueChanged="txtSchemeCode_ValueChanged" />
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
<asp:HiddenField ID="hdnAccountIdSwitch" runat="server" />
