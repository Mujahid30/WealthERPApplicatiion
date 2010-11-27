<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditRMDetails.ascx.cs"
    Inherits="WealthERP.Advisor.EditRMDetails" %>

<script language="javascript" type="text/javascript">
    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this record?');
        if (bool) {
            alert(bool);
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

    function CheckItem(sender, args) {
        var chkControlId = '<%=ChklistRMBM.ClientID%>';
        var options = document.getElementById(chkControlId).getElementsByTagName('input');
        var ischecked = false;
        args.IsValid = false;
        for (i = 0; i < options.length; i++) {
            var opt = options[i];
            if (opt.type == "checkbox") {
                var check = opt.checked;
                if (!check && i == 0) {
                    var count = document.getElementById("ctrl_EditRMDetails_hndRmCustomerCount").value;
                    if (count > 0) {
                        alert("Please deassociate Customer First");
                        return false;
                    }
                }
                if (check) {
                    ischecked = true;
                    args.IsValid = true;
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
                    var count=document.getElementById("ctrl_EditRMDetails_hndRmCustomerCount").value;
                    if (count > 0) {
                        alert("Please deassociate Customer First");
                        return false;                     
                    }
                    
                }
            }
        }
    }

</script>

<p>
</p>
<table class="TableBackground" width="100%">
    <tr>
        <td colspan="4">
            <asp:Label ID="Label9" runat="server" CssClass="HeaderTextBig" Text="Edit Staff Details"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table class="TableBackground">
    <tr>
        <td colspan="4" class="tdRequiredText">
            <label id="lbl" class="lblRequiredText">
                Note: Fields marked with ' * ' are compulsory</label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblFirst" runat="server" CssClass="FieldName" Text="First Name :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvName" ValidationGroup="btnUpdate" ControlToValidate="txtFirstName" ErrorMessage="<br />Please Enter the Name"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblMiddle" runat="server" CssClass="FieldName" Text="Middle Name :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtMiddleName" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblLast" runat="server" CssClass="FieldName" Text="Last Name :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtLastName" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCTC" runat="server" CssClass="FieldName" Text="CTC :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtCTC" runat="server" CssClass="txtField"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator15" ControlToValidate="txtCTC"
                ValidationGroup="btnUpdate" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Label ID="lblPerMonth" runat="server" CssClass="FieldName" Text="per month"></asp:Label>
        </td>
    </tr>
     <tr>
        <td class="leftField">
            <asp:Label ID="Label10" runat="server" CssClass="FieldName" Text="Staff Role:"></asp:Label>
        </td>
        <td class="rightField">
            
              
            <asp:CheckBoxList ID="ChklistRMBM" runat="server" CausesValidation="True"
                RepeatDirection="Horizontal" CssClass="cmbField" RepeatLayout="Flow" 
                ondatabound="ChklistRMBM_DataBound">
                 <asp:ListItem Value="1001">RM</asp:ListItem>
                <asp:ListItem Value="1002">BM</asp:ListItem>
               
            </asp:CheckBoxList>&nbsp;<span id="Span4" class="spnRequiredField">*</span>
           <asp:CustomValidator ID="CheckRMBM" runat="server" CssClass="rfvPCG" ControlToValidate="txtEmail" ValidationGroup="btnUpdate" ErrorMessage="select at least one role" ClientValidationFunction="CheckItem" ValidateEmptyText="true"></asp:CustomValidator>
           
            
              
        </td>
        <td class="style1">
        
          <asp:CheckBox ID="chkExternalStaff" OnCheckedChanged="chkExternalStaff_CheckedChanged" runat="server" AutoPostBack="true" Text="IsExternalStaff" CssClass="cmbField" />
           
        </td>
    </tr>
    
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="Label2" runat="server" CssClass="HeaderTextSmall" Text="Contact Details"></asp:Label>
            <hr />
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
                MaxLength="3"></asp:TextBox>
        
            <asp:TextBox ID="txtPhDirectSTD" runat="server" CssClass="txtField" Width="55px"
                MaxLength="3"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvphDirect" ControlToValidate="txtPhDirectSTD" ErrorMessage="Please enter STD Code"
                Display="Dynamic" runat="server" CssClass="rfvPCG"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtPhDirectSTD"
                ValidationGroup="btnUpdate" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        
            <asp:TextBox ID="txtPhDirectPhoneNumber" runat="server" CssClass="txtField" Width="150px"
                MaxLength="8"></asp:TextBox>
            <span id="Span3" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPhDirectPhoneNumber"
                ErrorMessage="Please enter phone number" Display="Dynamic" runat="server" CssClass="rfvPCG"></asp:RequiredFieldValidator>
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
            <asp:TextBox ID="txtPhExtISD" runat="server" CssClass="txtField" Width="55px" MaxLength="3"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ControlToValidate="txtPhExtISD"
                ValidationGroup="btnUpdate" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        
            <asp:TextBox ID="txtExtSTD" runat="server" CssClass="txtField" Width="55px" MaxLength="3"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtExtSTD"
                ValidationGroup="btnUpdate" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        
            <asp:TextBox ID="txtPhExtPhoneNumber" runat="server" CssClass="txtField" Width="150px"
                MaxLength="8"></asp:TextBox>
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
            <asp:TextBox ID="txtPhResiISD" runat="server" CssClass="txtField" Width="55px" MaxLength="3"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ControlToValidate="txtPhResiISD"
                ValidationGroup="btnUpdate" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        
            <asp:TextBox ID="txtResiSTD" runat="server" CssClass="txtField" Width="55px" MaxLength="3"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtResiSTD"
                ValidationGroup="btnUpdate" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        
            <asp:TextBox ID="txtPhResiPhoneNumber" runat="server" CssClass="txtField" Width="150px"
                MaxLength="8"></asp:TextBox>
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
            <asp:TextBox ID="txtFaxISD" runat="server" CssClass="txtField" Width="55px" MaxLength="3"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ControlToValidate="txtFaxISD"
                ValidationGroup="btnUpdate" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        
            <asp:TextBox ID="txtFaxSTD" runat="server" CssClass="txtField" Width="55px" MaxLength="3"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ControlToValidate="txtFaxSTD"
                ValidationGroup="btnUpdate" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        
            <asp:TextBox ID="txtFaxNumber" runat="server" CssClass="txtField" Width="150px" MaxLength="8"></asp:TextBox>
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
            <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
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
            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEmail" ValidationGroup="btnUpdate"
                ErrorMessage="Please enter an Email ID" Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ValidationGroup="btnUpdate" ID="RegularExpressionValidator1" ControlToValidate="txtEmail" 
                ErrorMessage="Please enter a valid Email ID" Display="Dynamic" runat="server"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
</table>
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
<table>
    <tr>
        <td class="SubmitCell" colspan="4">
            <asp:Button ID="btnSave" runat="server" ValidationGroup="btnUpdate" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_EditRMDetails_btnSave');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_EditRMDetails_btnSave');"
                Text="Save" OnClick="btnSave_Click" Style="height: 26px" OnClientClick="GetSelectedBranches()" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
        <td colspan="4">
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_ViewRMDetails_btnDelete');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_ViewRMDetails_btnDelete');"
                OnClick="btnDelete_Click" Height="22px" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:HiddenField ID="hdnMsgValue" runat="server" />
            <br />
            <asp:HiddenField ID="hdnExistingBranches" runat="server" />
            <asp:HiddenField ID="hdnSelectedBranches" runat="server" />
            <asp:HiddenField ID="hndRmCustomerCount" runat="server" />
            <asp:HiddenField ID="hndBMBranchHead" runat="server"/>
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

