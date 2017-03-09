<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddRM.ascx.cs" Inherits="WealthERP.Advisor.AddRM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script type="text/javascript"> 
    $(document).ready(function() {
        $(".flip").click(function() { $(".panel").slideToggle(); });
    });
</script>
<script language="javascript" type="text/javascript">
    function showmessage() {

        var bool = window.confirm('Do u want to add Agent code?');
        if (bool) {
            document.getElementById("ctrl_AddRM_hdnMsgValue").value = 1;
            document.getElementById("ctrl_AddRM_hiddenDelete").click();
            return false;
        }
        else {
            document.getElementById("ctrl_AddRM_hdnMsgValue").value = 0;
            document.getElementById("ctrl_AddRM_hiddenDelete").click();
            return true;
        }
    }
   
</script>
<script type="text/javascript">
    var content_Prefix = "ctrl_AddRM_";
    function CalculateYear(txtMonth, txtYearName) {


        txtYearName = content_Prefix + txtYearName;
        txtYear = document.getElementById(txtYearName);
        if (!isNaN(txtMonth.value) && txtMonth.value != 0) {

            txtYear.value = roundNumber(parseFloat(txtMonth.value) * 12, 2);
        }
        else {
            txtYear.value = 0;
            txtMonth.value = 0;
        }
    }
    function confirmbtn(){
var confirmed = window.confirm("change a record, would you like to continue ?");
 return(confirmed);
 }
function ShowYesNo() 
{    
    var answer = window.showModalDialog("myModalDialog.htm", '', "dialogWidth:300px; dialogHeight:200px; center:yes");
if(answer="yes")
var hdnIsOpsEnabled = document.getElementById('ctrl_AddRM_hdnIsOpsEnabled').value;
       document.write('Clicked yes');
    } else
     {
       document.write('Clicked no');
    }
}

    function roundNumber(num, dec) {
        var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
        return result;
    }
    function CheckItem(sender, args) {

        var hdnIsOpsEnabled = document.getElementById('ctrl_AddRM_hdnIsOpsEnabled').value;
         
        
        if (hdnIsOpsEnabled == "1") {
            var chk4ops = document.getElementById("<%= chkOps.ClientID %>");

            if (chk4ops.checked == false) {

                var chkControlId = '<%=ChklistRMBM.ClientID%>';
                var options = document.getElementById(chkControlId).getElementsByTagName('input');
                var ischecked = false;
                args.IsValid = false;

                for (i = 0; i < options.length; i++) {
                    var opt = options[i];
                    if (opt.type == "checkbox") {
                        if (opt.checked) {
                            ischecked = true;
                            args.IsValid = true;
                        }
                    }
                }
            }
            else {

            }
        }
        else {
            var ischecked = false;
            args.IsValid = false;
            var chkControlId = '<%=ChklistRMBM.ClientID%>';
            var options = document.getElementById(chkControlId).getElementsByTagName('input');
            for (i = 0; i < options.length; i++) {
                var opt = options[i];
                if (opt.type == "checkbox") {
                    if (opt.checked) {
                        ischecked = true;
                        args.IsValid = true;
                    }
                }
            }
        }
        
    }

    function addbranches() {

        ///var isSuccess = false;
        var Source = document.getElementById("<%= availableBranch.ClientID %>");
        var Target = document.getElementById("<%= associatedBranch.ClientID %>");

        if ((Source != null) && (Target != null) && Source.selectedIndex >= 0) {
            var newOption = new Option();
            newOption.text = Source.options[Source.selectedIndex].text;
            newOption.value = Source.options[Source.selectedIndex].value;
            Target.options[Target.length] = newOption;
            Source.remove(Source.selectedIndex);

        }
        return false;
    }



    function deletebranches() {
        var Source = document.getElementById("<%= associatedBranch.ClientID %>");
        var Target = document.getElementById("<%= availableBranch.ClientID %>");

        if ((Source != null) && (Target != null) && Source.selectedIndex >= 0) {

            var newOption = new Option();
            newOption.text = Source.options[Source.selectedIndex].text;
            newOption.value = Source.options[Source.selectedIndex].value;
            Target.options[Target.length] = newOption;
            Source.remove(Source.selectedIndex);
        }
        return false;
    }
    function GetSelectedBranches() {
        var Target = document.getElementById("<%= associatedBranch.ClientID %>");
        var selectedBranches = document.getElementById("<%= hdnSelectedBranches.ClientID %>");
        for (var i = 0; i < Target.length; i++) {
            selectedBranches.value += Target.options[i].value + ",";
        }
        //alert(selectedBranches)
    }
   
    function DisableControls() {

        var chkControlId = '<%=ChklistRMBM.ClientID%>';

        var options = document.getElementById(chkControlId).getElementsByTagName('input');
        var ischecked = false;
        for (i = 0; i < options.length; i++) {
            var opt = options[i];
            if (opt.type == "checkbox") {
                if (opt.checked == true) {

                    ischecked = true;
                    document.getElementById("<%= chkOps.ClientID %>").disabled = true;
                    document.getElementById("<%= availableBranch.ClientID %>").disabled = false;
                    document.getElementById("<%= associatedBranch.ClientID %>").disabled = false;
                    document.getElementById("<%= chkExternalStaff.ClientID %>").disabled = false;
                    break;

                }
                else {

                    ischecked = false;
                    document.getElementById("<%= chkOps.ClientID %>").disabled = false;
                    document.getElementById("<%= availableBranch.ClientID %>").disabled = true;
                    document.getElementById("<%= associatedBranch.ClientID %>").disabled = true;
                    document.getElementById("<%= chkExternalStaff.ClientID %>").disabled = true;


                }
            }
        }
        var chk4ops = document.getElementById("<%= chkOps.ClientID %>");

        if (chk4ops.checked == true) {

            document.getElementById("<%= availableBranch.ClientID %>").disabled = true;
            document.getElementById("<%= associatedBranch.ClientID %>").disabled = true;
            document.getElementById("<%= chkExternalStaff.ClientID %>").disabled = true;
            document.getElementById('addBranch').disabled = true;
            document.getElementById('deleteBranch').disabled = true;
            var chkckmk = '<%=ChklistRMBM.ClientID%>';
//            var hdn = document.getElementById("<%=hdnIsSubscripted.ClientID%>").value;
//            if (hdn == "True") {
//                document.getElementById("<%= trCKMK.ClientID %>").style.visibility = 'visible';
//            }


            var chkControlId = '<%=ChklistRMBM.ClientID%>';

            var options = document.getElementById(chkControlId).getElementsByTagName('input');

            for (a = 0; a <= options.length; a++) {

                options[a].disabled = true;
            }
        }
        else {
            
            
//                document.getElementById("<%= trCKMK.ClientID %>").style.visibility = 'collapse';
                
           
            document.getElementById("<%= availableBranch.ClientID %>").disabled = false;
            document.getElementById("<%= associatedBranch.ClientID %>").disabled = false;
            document.getElementById("<%= chkExternalStaff.ClientID %>").disabled = false;
            document.getElementById('addBranch').disabled = false;
            document.getElementById('deleteBranch').disabled = false;

            var chkControlId = '<%=ChklistRMBM.ClientID%>';

            var options = document.getElementById(chkControlId).getElementsByTagName('input');

            for (a = 0; a <= options.length; a++) {

                options[a].disabled = false;
            }
        }
    }
    function CheckSubscription() {
//        document.getElementById("<%= trCKMK.ClientID %>").style.visibility = 'collapse';
    }
//    function XYZ() {
//        var flag = 0;
//        var chkControlId = '<%=CheckListCKMK.ClientID%>';
//           if document.getElementById("<%= trCKMK.ClientID %>").Visible=true;
//           {
//            var options = document.getElementById(chkControlId).getElementsByTagName('input');
//          
//            for (a = 0; a <= options.length; a++) {
//               
//                if (options[a].checked) {
//                    flag++;
//                    alert(flag);
//            }
//        
//        if (flag != 1) {
//            alert("You must check one and only one checkbox!");
//            return false;
//        }
//        return true;
//        alert('hi');
//    }
//        }
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<%--<asp:UpdatePanel ID="updatePnl" runat="server">
    <ContentTemplate>--%>
<table width="100%" class="TableBackground">
    <%--<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Add Staff"></asp:Label>
            <hr />
        </td>
    </tr>--%>
    <tr>
        <td colspan="4">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblTitle" runat="server" Text="Add Staff"></asp:Label>
                        </td>
                        <td colspan="4" align="right">
                            <%-- <asp:LinkButton ID="LnkBack" Text="Back" runat="server" CssClass="LinkButtons" OnClick="lnkBack_Click"
                                        CausesValidation="false"></asp:LinkButton>&nbsp;&nbsp;
                                    <asp:LinkButton ID="lnkEdit" Text="Edit" runat="server" CssClass="LinkButtons" OnClick="lnkEdit_Click"
                                        CausesValidation="false"></asp:LinkButton>&nbsp;&nbsp;--%>
                        </td>
                        <td>
                            <img src="../Images/helpImage.png" height="15px" width="20px" style="float: right;"
                                class="flip" />
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table class="TableBackground" width="100%">
    <tr>
        <td colspan="3">
            <div class="panel">
                <p>
                    Note: Fields marked with a ' * ' are compulsory
                </p>
            </div>
        </td>
    </tr>
    <%-- <tr>
        <td colspan="4" class="tdRequiredText">
            <label id="lbl" class="lblRequiredText">
                Note: Fields marked with ' * ' are mandatory</label>
        </td>
    </tr>--%>
    <tr id="tr3" runat="server" visible="true">
        <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Staff Details
            </div>
        </td>
    </tr>
    <%--<tr>
    <td class="leftField" width="25%">
            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Code :"></asp:Label>
        </td>
        <td class="rightField" width="25%">
            <asp:TextBox ID="txtStaffCode" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span5" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ControlToValidate="txtStaffCode" ErrorMessage="Please enter the Branch Code"
                CssClass="rfvPCG" Display="Dynamic" ID="rfvBranchCode" ValidationGroup="btnSubmit"
                runat="server"></asp:RequiredFieldValidator>
        </td>
    </tr>--%>
    <tr>
    <td class="leftField">
            <asp:Label ID="lb1StaffCode" runat="server" CssClass="FieldName" Text="Staff Code:"></asp:Label>
        </td>
        <td>
         <asp:TextBox ID="txtStaffcode" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblName" runat="server" CssClass="FieldName" Text="Staff Name:"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span1" class="spnRequiredField">*</span>
            <cc1:TextBoxWatermarkExtender ID="txtFirstName_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtFirstName" WatermarkText="Firstname">
            </cc1:TextBoxWatermarkExtender>
            <asp:TextBox ID="txtMiddleName" runat="server" CssClass="txtField"></asp:TextBox>
            &nbsp;&nbsp;
            <cc1:TextBoxWatermarkExtender ID="txtMiddleName_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtMiddleName" WatermarkText="MiddleName">
            </cc1:TextBoxWatermarkExtender>
            <asp:TextBox ID="txtLastName" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtLastName_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtLastName" WatermarkText="LastName">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtFirstName" ErrorMessage="<br />Please Enter the Name"
                Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    
    <tr>
        <td class="leftField">
            <asp:Label ID="Label10" runat="server" CssClass="FieldName" Text="Staff Role:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:CheckBoxList ID="ChklistRMBM" runat="server" CausesValidation="True" RepeatDirection="Horizontal"
                CssClass="cmbField" RepeatLayout="Flow" onclick="DisableControls()">
                <asp:ListItem Value="1001">RM</asp:ListItem>
                <asp:ListItem Value="1002">BM</asp:ListItem>
                <asp:ListItem Value="1005">Research</asp:ListItem>
            </asp:CheckBoxList>
            <asp:Label ID="lblOr" runat="server" Text="&nbsp;/&nbsp;" CssClass="FieldName" Visible="false"></asp:Label>
            <asp:CheckBox ID="chkOps" runat="server" Text="Ops" CssClass="cmbField" value="1004"  Visible="false"
                onclick="DisableControls()" />&nbsp; <span id="Span4" class="spnRequiredField">*&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
            <asp:CheckBox ID="chkExternalStaff" OnCheckedChanged="chkExternalStaff_CheckedChanged"
                runat="server" AutoPostBack="true"  Text="IsExternalStaff" CssClass="cmbField" />
            <asp:CustomValidator ID="CheckRMBM" runat="server" CssClass="rfvPCG" ControlToValidate="txtEmail"
                ValidationGroup="btnSubmit" ErrorMessage="<br />Select at least one role" ClientValidationFunction="CheckItem"
                ValidateEmptyText="true"></asp:CustomValidator>
        </td>
        <%--  <td class="style1">
        
          <asp:CheckBox ID="chkExternalStaff" OnCheckedChanged="chkExternalStaff_CheckedChanged" runat="server" AutoPostBack="true" Text="IsExternalStaff" CssClass="cmbField" />
           
        </td>--%>
    </tr>
    <tr id="trCKMK" runat="server" visible="false">
        <td class="leftField">
            <asp:Label ID="OpsCKMK" runat="server" CssClass="FieldName" Text="Ops Role:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:CheckBoxList ID="CheckListCKMK" runat="server" CausesValidation="True" RepeatDirection="Horizontal"
                CssClass="cmbField" RepeatLayout="Flow" onclick="XYZ()">
                <asp:ListItem Value="2000">Checker</asp:ListItem>
                <asp:ListItem Value="2001">Maker</asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
    
    <tr>
        <td class="leftField">
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="CTC:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtCTCMonthly" runat="server" CssClass="txtField" onblur="CalculateYear(this,'txtCTCYearly')"></asp:TextBox>
            <asp:TextBox ID="txtCTCYearly" runat="server" CssClass="txtField"></asp:TextBox>
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator15" ControlToValidate="txtCTCMonthly"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Enter a numeric value" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField" colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td class="style2">
            <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text=" per month"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text=" per year"></asp:Label>
        </td>
        <td class="style1">
            &nbsp;
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr id="tr1" runat="server" visible="true">
        <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Contact Details
            </div>
        </td>
    </tr>
    <%--<tr>
        <td colspan="4">
            <asp:Label ID="Label2" runat="server" CssClass="HeaderTextSmall" Text="Contact Details"></asp:Label>
            <hr />
        </td>
    </tr>--%>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Label ID="lblISD" runat="server" Text="ISD" CssClass="FieldName"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblSTD" runat="server" Text="STD" CssClass="FieldName"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblPhoneNumber" runat="server" CssClass="FieldName" Text="PhoneNumber"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPhoneDirectNumber" runat="server" CssClass="FieldName" Text="Telephone Number Direct:"></asp:Label>
        </td>
        <td class="style2">
            <asp:TextBox ID="txtPhDirectISD" runat="server" CssClass="txtField" Width="55px"
                MaxLength="4">91</asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtPhDirectISD"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtPhDirectSTD" runat="server" CssClass="txtField" Width="55px"
                MaxLength="4"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtPhDirectSTD"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtPhDirectPhoneNumber" runat="server" CssClass="txtField" Width="150px"
                MaxLength="8"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtPhDirectPhoneNumber"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblExtPh" runat="server" CssClass="FieldName" Text="Telephone Number Extention :"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtPhExtISD" runat="server" CssClass="txtField" Width="55px" MaxLength="4">91</asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtPhExtISD"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtExtSTD" runat="server" CssClass="txtField" Width="55px" MaxLength="4"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtExtSTD"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtPhExtPhoneNumber" runat="server" CssClass="txtField" Width="150px"
                MaxLength="8"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ControlToValidate="txtPhExtPhoneNumber"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblResPh" runat="server" CssClass="FieldName" Text="Telephone Number Residence :"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtPhResiISD" runat="server" CssClass="txtField" Width="55px" MaxLength="4">91</asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="txtPhResiISD"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtResiSTD" runat="server" CssClass="txtField" Width="55px" MaxLength="4"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ControlToValidate="txtResiSTD"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtPhResiPhoneNumber" runat="server" CssClass="txtField" Width="150px"
                MaxLength="8"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ControlToValidate="txtPhResiPhoneNumber"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
        <td>
        </td>
        <td class="rightField">
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblFax" runat="server" CssClass="FieldName" Text="Fax :"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtFaxISD" runat="server" CssClass="txtField" Width="55px" MaxLength="4">91</asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ControlToValidate="txtFaxISD"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtFaxSTD" runat="server" CssClass="txtField" Width="55px" MaxLength="4"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ControlToValidate="txtFaxSTD"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtFaxNumber" runat="server" CssClass="txtField" Width="150px" MaxLength="8"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator13" ControlToValidate="txtFaxNumber"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="Mobile Number :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
            <span id="Span3" class="spnRequiredField">*</span>
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" ControlToValidate="txtMobileNumber"
                Display="Dynamic" runat="server" CssClass="rfvPCG" ErrorMessage="Not acceptable format"
                ValidationGroup="btnSubmit" ValidationExpression="^\d{10,10}$">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtMobileNumber"
                ErrorMessage="Please enter a Contact Number" Display="Dynamic" runat="server"
                ValidationGroup="btnSubmit" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblEmail" runat="server" CssClass="FieldName" Text="Email Id :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span2" class="spnRequiredField">*&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
            <asp:CheckBox ID="chkMailSend" Checked="false" runat="server" Text="Send Login info?"
                CssClass="cmbField" />
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEmail"
                ValidationGroup="btnSubmit" ErrorMessage="Please enter an Email ID" Display="Dynamic"
                runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                ErrorMessage="Please enter a valid Email ID" Display="Dynamic" runat="server"
                ValidationGroup="btnSubmit" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                CssClass="revPCG"></asp:RegularExpressionValidator>
            <asp:Label ID="lblEmailDuplicate" runat="server" CssClass="Error" Text="Email Id already exists"></asp:Label>
        </td>
    </tr>
    <tr id="tr2" runat="server" visible="true">
        <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Branch Association
            </div>
        </td>
    </tr>
    <%--<tr>
        <td colspan="4">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextSmall" Text="Branch Association"></asp:Label>
            <hr />
        </td>
    </tr>--%>
    <tr>
        <td align="right">
            <asp:Label ID="lblError" runat="server" CssClass="FieldName" Text="Branch List: "></asp:Label>
        </td>
        <%--</tr>
    <tr>--%>
        <td colspan="3">
            <table border="1">
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Available Branches" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Associated Branches" CssClass="FieldName"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ListBox ID="availableBranch" runat="server" Height="150px" Width="130px"></asp:ListBox>
                    </td>
                    <td>
                        <table border="1">
                            <tr>
                                <td>
                                    <input type="button" id="addBranch" value=">>" onclick="addbranches();return false;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="button" id="deleteBranch" value="<<" onclick="deletebranches();return false;" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:ListBox ID="associatedBranch" runat="server" Height="150px" Width="128px"></asp:ListBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:GridView ID="gvBranchList" runat="server" AutoGenerateColumns="False" DataKeyNames="BranchId"
                AllowSorting="True" CssClass="GridViewStyle">
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <RowStyle CssClass="RowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkId" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Is Main Branch?">
                        <ItemTemplate>
                            <asp:RadioButton ID="rbtnMainBranch" runat="server" GroupName="grpMainBranch" AutoPostBack="true"
                                OnCheckedChanged="rbtnMainBranch_CheckedChanged" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:BoundField DataField="Branch Name" HeaderText="Branch Name" />
                    <asp:BoundField DataField="Branch Address" HeaderText="Branch Address" />
                    <asp:BoundField DataField="Branch Phone" HeaderText="Branch Phone" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <%--    <tr>
        <td colspan="4">            
            <asp:CheckBox ID="chkMailSend" Checked="false" runat="server" Text="Send Login info?"  CssClass="cmbField"/>
        </td>
    </tr>--%>
    <%--  <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>--%>
    <tr>
        <td colspan="4" class="SubmitCell">
            <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" Text="Submit" CssClass="PCGButton"
                OnClientClick="GetSelectedBranches()" ValidationGroup="btnSubmit" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AddRM_btnNext', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AddRM_btnNext', 'S');" />
        </td>
    </tr>
      <tr id="trAddStaffCode"  runat="server" width="25%">
       <td class="leftField">
       <asp:Label ID="lb1BranchCode" runat="server" CssClass="FieldName" Text="Add Branch code"></asp:Label>
        </td> 
    <td  class="rightField" width="25%">
     <asp:Button ID="BtnStaffCode" runat="server"  Text="Agent Code" 
            CssClass="PCGMediumButton" onClick="BtnStaffCode_Click"  />
    </td>
    <td class="leftField" width="25%">
       <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="View Branch"></asp:Label>
        </td> 
        <td  class="rightField" width="25%">
     <asp:Button ID="BtnviewStaffCode" runat="server"  Text="View Branch" 
            CssClass="PCGMediumButton" onClick="BtnStaffCode1_Click" />
    </td>
    </tr>
    <%--  <tr id="trAddStaffCode"  runat="server">
       <td class="leftField">
       <asp:Label ID="lb1StaffCode" runat="server" CssClass="FieldName" Text="Add Staff code"></asp:Label>
        </td> 
    <td colspan="3" class="rightField">
     <asp:Button ID="BtnStaffCode" runat="server"  Text="StaffCode" CssClass="PCGButton" onClick="BtnStaffCode_Click" />
    </td>
        
    </tr>--%>
    <tr>
    
        <td style="height: 10px;" colspan="4">
            &nbsp;
        </td>
    </tr>
     <tr>
        <td colspan="2">
            <asp:HiddenField ID="hdnMsgValue" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="hiddenDelete" runat="server" OnClick="hiddenDelete_Click" Text=""
                BorderStyle="None" BackColor="Transparent" />
        </td>
    </tr>
     <%--<button style="width: 200px;" runat="server" ID="BtnStaffCode1" onclick="radconfirm('Client radconfirm: Are you sure?', confirmCallBackFn, 330, 180, null,'Client RadConfirm', imgUrl); return false;">
                    radconfirm from client</button>--%>
</table>
<asp:HiddenField ID="hdnExistingBranches" runat="server" />
<asp:HiddenField ID="hdnSelectedBranches" runat="server" />
<asp:HiddenField ID="hdnIsSubscripted" runat="server" />
<asp:HiddenField ID="hdnIsOpsEnabled" runat="server"/>
<%--
<asp:HiddenField ID="HiddenField1" runat="server" OnValueChanged='ShowYesNo();'/>--%>
<%--</ContentTemplate>--%><%--</asp:UpdatePanel>--%>