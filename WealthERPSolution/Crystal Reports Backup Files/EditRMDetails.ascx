<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditRMDetails.ascx.cs"
    Inherits="WealthERP.Advisor.EditRMDetails" %>
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

        var bool = window.confirm('Are you sure you want to delete this record?');
        if (bool) {
            document.getElementById("ctrl_EditRMDetails_hdnMsgValue").value = 1;
            document.getElementById("ctrl_EditRMDetails_hiddenDelete").click();
            return false;
        }
        else {
            document.getElementById("ctrl_EditRMDetails_hdnMsgValue").value = 0;
            document.getElementById("ctrl_EditRMDetails_hiddenDelete").click();
            return true;
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

    //    function CheckItem(sender, args) {
    //        var chk4ops = document.getElementById("<%= chkOps.ClientID %>");
    //        if (chk4ops.checked == false) {

    //            var chkControlId = '<%=ChklistRMBM.ClientID%>';
    //            var options = document.getElementById(chkControlId).getElementsByTagName('input');
    //            var ischecked = false;
    //            args.IsValid = false;
    //            for (i = 0; i < options.length; i++) {
    //                var opt = options[i];
    //                if (opt.type == "checkbox") {
    //                    var check = opt.checked;
    //                    if (!check && i == 0) {
    //                        var count = document.getElementById("ctrl_EditRMDetails_hndRmCustomerCount").value;
    //                        if (count > 0) {
    //                            alert("Please deassociate Customer First");
    //                            return false;
    //                        }
    //                    }
    //                    if (check) {
    //                        ischecked = true;
    //                        args.IsValid = true;
    //                    }
    //                }
    //            }
    //        }
    //        else {
    //        }
    //    }
    function CheckItem(sender, args) {
        var hdnIsOpsEnabled = document.getElementById('ctrl_EditRMDetails_hdnIsOpsEnabled').value;

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

    function CheckRMBMRole() {
        alert("pra..");
        var chkControlId = '<%=ChklistRMBM.ClientID%>';
        var options = document.getElementById(chkControlId).getElementsByTagName('input');
        var ischecked = false;
        for (i = 0; i < options.length; i++) {
            var opt = options[i];
            if (opt.type == "checkbox") {
                if (!opt.checked) {
                    var count = document.getElementById("ctrl_EditRMDetails_hndRmCustomerCount").value;
                    if (count > 0) {
                        alert("Please deassociate Customer First");
                        return false;
                    }

                }
            }
        }
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
                    document.getElementById("<%= chkOps.ClientID %>").checked = false;
                    document.getElementById("<%= availableBranch.ClientID %>").disabled = false;
                    document.getElementById("<%= associatedBranch.ClientID %>").disabled = false;
                    document.getElementById("<%= chkExternalStaff.ClientID %>").disabled = false;
                    break;

                }
                else {

                    ischecked = false;
                    document.getElementById("<%= chkOps.ClientID %>").disabled = false;
                    document.getElementById("<%= chkOps.ClientID %>").checked = true;
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
            var hdn = document.getElementById("<%=hdnIsSubscripted.ClientID%>").value;
//            if (hdn == 'True') {
//                document.getElementById("<%= trCKMK.ClientID %>").style.visibility = 'visible';
//            }


            var chkControlId = '<%=ChklistRMBM.ClientID%>';

            var options = document.getElementById(chkControlId).getElementsByTagName('input');

            for (a = 0; a <= options.length; a++) {

                options[a].disabled = true;
            }
        }
        else {


//            document.getElementById("<%= trCKMK.ClientID %>").style.visibility = 'collapse';


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
    function keyPress(sender, args) {
        if (args.keyCode == 13) {
            return false;
        }
    }
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table class="TableBackground" width="100%">
    <tr>
        <td colspan="4">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblHeader" runat="server"></asp:Label>
                        </td>
                        <td colspan="4" align="right">
                            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                                OnClick="lnkBtnBack_Click"></asp:LinkButton>&nbsp;&nbsp;
                            <asp:LinkButton ID="lnkEdit" Text="Edit" runat="server" CssClass="LinkButtons" OnClick="lnkEdit_Click"
                                CausesValidation="false"></asp:LinkButton>&nbsp;&nbsp;
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
    <%--<tr>
        <td colspan="4">
            <asp:Label ID="Label9" runat="server" CssClass="HeaderTextBig" Text="Edit Staff Details"></asp:Label>
            <hr />
        </td>
    </tr>--%>
</table>
<table class="TableBackground" width='100%'>
    <tr>
        <td colspan="4">
            <div class="panel">
                <p>
                    Note: Fields marked with a ' * ' are compulsory
                </p>
            </div>
        </td>
    </tr>
    <%--<tr>
        <td colspan="4" class="tdRequiredText">
            <label id="lbl" class="lblRequiredText">
                Note: Fields marked with ' * ' are compulsory</label>
        </td>
    </tr>--%>
    <%--  <tr>
        <td>
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>--%>
    <tr id="tr3" runat="server" visible="true">
        <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Staff Details
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblName" runat="server" CssClass="FieldName" Text="Staff Name:"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtField" onkeypress="return keyPress(this, event)"></asp:TextBox>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvName" ValidationGroup="btnUpdate" ControlToValidate="txtFirstName"
                ErrorMessage="<br />Please Enter the Name" Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <cc1:TextBoxWatermarkExtender ID="txtFirstName_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtFirstName" WatermarkText="First Name">
            </cc1:TextBoxWatermarkExtender>
            &nbsp;
            <asp:TextBox ID="txtMiddleName" runat="server" CssClass="txtField" onkeypress="return keyPress(this, event)"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtMiddleName_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtMiddleName" WatermarkText="Middle Name">
            </cc1:TextBoxWatermarkExtender>
            &nbsp;
            <asp:TextBox ID="txtLastName" runat="server" CssClass="txtField" onkeypress="return keyPress(this, event)"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtLastName_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtLastName" WatermarkText="Last Name">
            </cc1:TextBoxWatermarkExtender>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblStaffCode" runat="server" CssClass="FieldName" Text="StaffCode :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtStaffCode" runat="server" CssClass="txtField"   ReadOnly="false"  ></asp:TextBox>
        </td>
      
    </tr>
    <tr id="trAddStaffCode"  runat="server">
       <td class="leftField" width="25%">
       <asp:Label ID="lb1AgentCode" runat="server" CssClass="FieldName" Text="Agent Code"></asp:Label>
        </td> 
         <td class="rightField" width="25%">
            <asp:TextBox ID="txtStafAgentCode" runat="server" CssClass="txtField"   ReadOnly="false"  ></asp:TextBox>
        </td>
    <td   class="rightField"   colspan="2">
     <asp:Button ID="BtnAgentCode" runat="server"  Text="AgentCode" CssClass="PCGMediumButton" onClick="BtnStaffCode_Click" />
    </td>
        
    </tr>
      
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCTC" runat="server" CssClass="FieldName" Text="CTC Per Month :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtCTC" runat="server" CssClass="txtField" onkeypress="return keyPress(this, event)"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator15" ControlToValidate="txtCTC"
                ValidationGroup="btnUpdate" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label10" runat="server" CssClass="FieldName" Text="Staff Role:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:CheckBoxList ID="ChklistRMBM" runat="server" CausesValidation="True" RepeatDirection="Horizontal"
                CssClass="cmbField" RepeatLayout="Flow" onclick="DisableControls()" OnDataBound="ChklistRMBM_DataBound">
                <asp:ListItem Value="1001">RM</asp:ListItem>
                <asp:ListItem Value="1002">BM</asp:ListItem>
                <asp:ListItem Value="1005">Research</asp:ListItem>
            </asp:CheckBoxList>
            <asp:CheckBox ID="chkOps" runat="server" Text="Ops" CssClass="cmbField" onclick="DisableControls()" />
            &nbsp;<span id="Span4" class="spnRequiredField">*</span>
            <asp:CustomValidator ID="CheckRMBM" runat="server" CssClass="rfvPCG" ControlToValidate="txtEmail"
                ValidationGroup="btnUpdate" ErrorMessage="select at least one role" ClientValidationFunction="CheckItem"
                ValidateEmptyText="true" Display="Dynamic"></asp:CustomValidator>
            &nbsp;
            <asp:CheckBox ID="chkExternalStaff" OnCheckedChanged="chkExternalStaff_CheckedChanged"
                runat="server" AutoPostBack="true" Text="IsExternalStaff" CssClass="cmbField" />
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr id="trCKMK" runat="server" visible="false">
        <td class="leftField">
            <asp:Label ID="OpsCKMK" runat="server" CssClass="FieldName" Text="Ops Role:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:CheckBoxList ID="CheckListCKMK" runat="server" CausesValidation="True" RepeatDirection="Horizontal"
                CssClass="cmbField" RepeatLayout="Flow">
                <asp:ListItem Value="2000">Checker</asp:ListItem>
                <asp:ListItem Value="2001">Maker</asp:ListItem>
            </asp:CheckBoxList>
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr id="tr1" runat="server" visible="true">
        <td colspan="4" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Contact Details
            </div>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td colspan="3">
            <asp:Label ID="lblISD" runat="server" Text="ISD" CssClass="FieldName"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblSTD" runat="server" Text="STD" CssClass="FieldName"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblPhoneNumber" runat="server" CssClass="FieldName" Text="PhoneNumber"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPhoneDirectNumber" runat="server" CssClass="FieldName" Text="Telephone Number Direct :"></asp:Label>
        </td>
        <td class="rightField" colspan="4">
            <asp:TextBox ID="txtPhDirectISD" runat="server" CssClass="txtField" Width="55px"
                onkeypress="return keyPress(this, event)" MaxLength="3"></asp:TextBox>
            <asp:TextBox ID="txtPhDirectSTD" runat="server" CssClass="txtField" Width="55px"
                onkeypress="return keyPress(this, event)" MaxLength="3"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtPhDirectSTD"
                ValidationGroup="btnUpdate" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtPhDirectPhoneNumber" runat="server" CssClass="txtField" Width="150px"
                onkeypress="return keyPress(this, event)" MaxLength="8"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtPhDirectPhoneNumber"
                ValidationGroup="btnUpdate" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="Telephone Number Extention :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtPhExtISD" runat="server" CssClass="txtField" Width="55px" MaxLength="3" onkeypress="return keyPress(this, event)"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ControlToValidate="txtPhExtISD"
                ValidationGroup="btnUpdate" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtExtSTD" runat="server" CssClass="txtField" Width="55px" MaxLength="3" onkeypress="return keyPress(this, event)"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtExtSTD"
                ValidationGroup="btnUpdate" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtPhExtPhoneNumber" runat="server" CssClass="txtField" Width="150px"
                onkeypress="return keyPress(this, event)" MaxLength="8"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtPhExtPhoneNumber"
                ValidationGroup="btnUpdate" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Telephone Number Residence :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtPhResiISD" runat="server" CssClass="txtField" Width="55px" MaxLength="3" onkeypress="return keyPress(this, event)"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ControlToValidate="txtPhResiISD"
                ValidationGroup="btnUpdate" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtResiSTD" runat="server" CssClass="txtField" Width="55px" MaxLength="3" onkeypress="return keyPress(this, event)"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtResiSTD"
                ValidationGroup="btnUpdate" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtPhResiPhoneNumber" runat="server" CssClass="txtField" Width="150px"
                onkeypress="return keyPress(this, event)" MaxLength="8"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="txtPhResiPhoneNumber"
                ValidationGroup="btnUpdate" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Fax :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtFaxISD" runat="server" CssClass="txtField" Width="55px" MaxLength="3" onkeypress="return keyPress(this, event)"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ControlToValidate="txtFaxISD"
                ValidationGroup="btnUpdate" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtFaxSTD" runat="server" CssClass="txtField" Width="55px" MaxLength="3" onkeypress="return keyPress(this, event)"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ControlToValidate="txtFaxSTD"
                ValidationGroup="btnUpdate" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtFaxNumber" runat="server" CssClass="txtField" Width="150px" MaxLength="8"
                onkeypress="return keyPress(this, event)"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ControlToValidate="txtFaxNumber"
                ValidationGroup="btnUpdate" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="Mobile Number :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="txtField" MaxLength="10"
                onkeypress="return keyPress(this, event)"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" ControlToValidate="txtMobileNumber"
                Display="Dynamic" runat="server" CssClass="rfvPCG" ErrorMessage="Not acceptable format"
                ValidationGroup="btnUpdate" ValidationExpression="^\d{10,10}$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblEmail" runat="server" CssClass="FieldName" Text="Email Id :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField" onkeypress="return keyPress(this, event)"></asp:TextBox>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEmail"
                ValidationGroup="btnUpdate" ErrorMessage="Please enter an Email ID" Display="Dynamic"
                runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ValidationGroup="btnUpdate" ID="RegularExpressionValidator1"
                ControlToValidate="txtEmail" ErrorMessage="Please enter a valid Email ID" Display="Dynamic"
                runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr id="tr2" runat="server" visible="true">
        <td colspan="4" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Branch Association
            </div>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblError" runat="server" CssClass="FieldName" Text="Branch List: "></asp:Label>
        </td>
        <td colspan="3">
            <table border="1">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Available Branches" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Associated Branches" CssClass="FieldName"></asp:Label>
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
        <td>
            &nbsp;
        </td>
        <td colspan="3" align="left">
            <table>
                <tr>
                    <td class="SubmitCell" colspan="4">
                        <asp:Button ID="btnSave" runat="server" ValidationGroup="btnUpdate" CssClass="PCGButton"
                            onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_EditRMDetails_btnSave');"
                            onmouseout="javascript:ChangeButtonCss('out', 'ctrl_EditRMDetails_btnSave');"
                            Text="Save" OnClick="btnSave_Click" OnClientClick="GetSelectedBranches()" />
                        &nbsp;
                    </td>
                    <td colspan="4">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_ViewRMDetails_btnDelete');"
                            onmouseout="javascript:ChangeButtonCss('out', 'ctrl_ViewRMDetails_btnDelete');"
                            OnClick="btnDelete_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td colspan="2">
            <asp:HiddenField ID="hdnMsgValue" runat="server" />
            <br />
            <asp:HiddenField ID="hdnExistingBranches" runat="server" />
            <asp:HiddenField ID="hdnSelectedBranches" runat="server" />
            <asp:HiddenField ID="hndRmCustomerCount" runat="server" />
            <asp:HiddenField ID="hndBMBranchHead" runat="server" />
            <asp:HiddenField ID="hdnIsSubscripted" runat="server" />
            <br />
            <br />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="hiddenDelete" runat="server" OnClick="hiddenDelete_Click" Text=""
                BorderStyle="None" BackColor="Transparent" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnIsOpsEnabled" runat="server" />
