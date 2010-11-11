<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddRM.ascx.cs" Inherits="WealthERP.Advisor.AddRM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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

    function roundNumber(num, dec) {
        var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
        return result;
    }
    function CheckItem(sender, args) {
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

    

</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<%--<asp:UpdatePanel ID="updatePnl" runat="server">
    <ContentTemplate>--%>
    <table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Add Staff"></asp:Label>
            <hr />
        </td>
    </tr>
</table>

<table class="TableBackground" width="100%" >

    <tr>
        <td colspan="4" class="tdRequiredText">
            <label id="lbl" class="lblRequiredText">
                Note: Fields marked with ' * ' are compulsory</label>
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
            
              
            <asp:CheckBoxList ID="ChklistRMBM" runat="server" CausesValidation="True" 
                RepeatDirection="Horizontal" CssClass="cmbField" RepeatLayout="Flow">
                <asp:ListItem Value="1001">RM</asp:ListItem>
                <asp:ListItem Value="1002">BM</asp:ListItem>
            </asp:CheckBoxList>&nbsp;<span id="Span4" class="spnRequiredField">*</span>
           <asp:CustomValidator ID="CheckRMBM" runat="server" CssClass="rfvPCG" ControlToValidate="txtEmail" ValidationGroup="btnSubmit" ErrorMessage="select at least one role" ClientValidationFunction="CheckItem" ValidateEmptyText="true"></asp:CustomValidator>
           
            
              
        </td>
        <td class="style1">
        
          <asp:CheckBox ID="chkExternalStaff" OnCheckedChanged="chkExternalStaff_CheckedChanged" runat="server" AutoPostBack="true" Text="IsExternalStaff" CssClass="cmbField" />
           
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
            &nbsp;</td>
        <td colspan="2">
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
            &nbsp;
        </td>
        <td >
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
                ErrorMessage="Please enter a Contact Number" Display="Dynamic" runat="server" ValidationGroup="btnSubmit"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblEmail" runat="server" CssClass="FieldName" Text="Email Id :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span2" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEmail" ValidationGroup="btnSubmit"
                ErrorMessage="Please enter an Email ID" Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                ErrorMessage="Please enter a valid Email ID" Display="Dynamic" runat="server"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="revPCG"></asp:RegularExpressionValidator>
            <asp:Label ID="lblEmailDuplicate" runat="server" CssClass="Error" Text="Email Id already exists"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextSmall" Text="Branch Association"></asp:Label>
            <hr />
        </td>
    </tr>
     <tr>
        <td colspan="4">
            <asp:Label ID="lblError" runat="server" CssClass="FieldName" Text="Branch List"></asp:Label>
         </td>
     </tr>
    <tr>
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
                    <asp:BoundField DataField="Branch Name" HeaderText="Branch Name"  />
                    <asp:BoundField DataField="Branch Address" HeaderText="Branch Address"  />
                    <asp:BoundField DataField="Branch Phone" HeaderText="Branch Phone"  />
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
     <tr>
        <td colspan="4">            
            <asp:CheckBox ID="chkMailSend" Checked="false" runat="server" Text="Send Login info?"  CssClass="cmbField"/>
        </td>
    </tr>
     <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4" class="SubmitCell">
            <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" Text="Submit" CssClass="PCGButton" OnClientClick="GetSelectedBranches()"
                ValidationGroup="btnSubmit" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AddRM_btnNext', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AddRM_btnNext', 'S');" />
        </td>
    </tr>
    <tr>
        <td style="height: 10px;" colspan="4">
            &nbsp;
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnExistingBranches" runat="server" />
<asp:HiddenField ID="hdnSelectedBranches" runat="server" />
<%--</ContentTemplate>--%><%--</asp:UpdatePanel>--%>