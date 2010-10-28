<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditCustomerIndividualProfile.ascx.cs"
    Inherits="WealthERP.Customer.EditCustomerIndividualProfile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" src="../Scripts/tabber.js"></script>
<script type="text/javascript" language="javascript">
    function checkDate(sender, args) {

        var selectedDate = new Date();
        selectedDate = sender._selectedDate;

        var todayDate = new Date();
        var msg = "";

        if (selectedDate > todayDate) {
            //sender._selectedDate = todayDate;
            //sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            sender._textbox.set_Value('');
            alert("Warning! - Date cannot be in the future");
            document.getElementById('<%= btnEdit.ClientID %>').focus(); // to make the focus out of the textbox
        }
    }
    function OnMaritalStatusChange(ddlMaritalStatus) {
        var selectedValue = document.getElementById(ddlMaritalStatus.id).value;
        document.getElementById('<%= txtMarriageDate.ClientID %>').value = 'dd/mm/yyyy';
        if (selectedValue == 'MA') {
            document.getElementById('<%= txtMarriageDate.ClientID %>').disabled = false;
        }
        else {
            document.getElementById('<%= txtMarriageDate.ClientID %>').disabled = true;
        }
    }
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblView" runat="server" CssClass="HeaderTextBig" Text="Edit Profile"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table style="width: 100%; height: 254px;">
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
    <td>
    </td>
    <td>
    <asp:Checkbox ID="chkprospect" runat="server" CssClass="txtField"  Text="Prospect" 
                AutoPostBack="false"  Enabled = "true" /></asp:Label>
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
            <asp:Label ID="lblBranchName" runat="server" CssClass="FieldName" Text="Branch Name:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlAdviserBranchList" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span3" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="ddlAdviserBranchList_CompareValidator" runat="server" ControlToValidate="ddlAdviserBranchList"
                ErrorMessage="Please select a Branch" Operator="NotEqual" ValueToCompare="Select a Branch"
                CssClass="cvPCG">
            </asp:CompareValidator>
        </td>
    </tr>
    
    <tr>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblRMName" runat="server" CssClass="FieldName" Text="RM Name:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM Name:"></asp:Label>
        </td>
    </tr>
    
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCustomerSubType" runat="server" CssClass="FieldName" Text="Customer Sub Type:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlCustomerSubType" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlCustomerSubType_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlCustomerSubType"
                ErrorMessage="Please select a Customer Sub-Type" Operator="NotEqual" ValueToCompare="Select a Sub-Type"
                CssClass="cvPCG"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Date of Profiling:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtProfilingDate" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="Customer Code:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        
    </tr>
    
    <tr>
        <td class="leftField">
            <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="PAN Number:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtPanNumber" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
             &nbsp;
            &nbsp;
            &nbsp;
            <asp:Checkbox ID="chkdummypan" runat="server" CssClass="txtField" Text="Dummy PAN"
                AutoPostBack="true" />
            <span id="Span4" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="rfvPanNumber" ControlToValidate="txtPanNumber" ErrorMessage="Please enter a PAN Number"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:Label ID="lblPanDuplicate" runat="server" CssClass="Error" Text="PAN Number already exists"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Salutation:"></asp:Label>
        </td>
        <td class="rightField" width="75%">
            <asp:DropDownList ID="ddlSalutation" runat="server" CssClass="cmbField">
                <asp:ListItem>Select a Salutation</asp:ListItem>
                <asp:ListItem>Mr.</asp:ListItem>
                <asp:ListItem>Mrs.</asp:ListItem>
                <asp:ListItem>Ms.</asp:ListItem>
                <asp:ListItem>M/S.</asp:ListItem>
            </asp:DropDownList>
            <span id="Span5" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cmpddlSalutation" runat="server" ControlToValidate="ddlSalutation"
                ErrorMessage="Please select a Salutation for customer" Operator="NotEqual" ValueToCompare="Select a Salutation"
                CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Name:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtField" style="width: 30%" ></asp:TextBox>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:TextBox ID="txtMiddleName" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
            <asp:TextBox ID="txtLastName" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvLastName" ControlToValidate="txtFirstName" ErrorMessage="Please enter the First Name"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trGuardianName" runat="server">
        <td class="leftField">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Guardian Name:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtGuardianFirstName" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
            <asp:TextBox ID="txtGuardianMiddleName" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
            <asp:TextBox ID="txtGuardianLastName" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
        </td>
    </tr>
    
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
</table>
<div class="tabber">
    <div class="tabbertab" style="height: 250px;">
        <h6>
            Correspondence Address</h6>
        <table style="width: 100%; height: 196px;">
            <tr>
                <td colspan="4">
                    <asp:Label ID="Label10" CssClass="HeaderTextSmall" runat="server" Text="Correspondence Address"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label11" CssClass="FieldName" runat="server" Text="Line1(House No./Building):"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtCorrAdrLine1" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label12" CssClass="FieldName" runat="server" Text="Line2(Street):"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtCorrAdrLine2" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label13" CssClass="FieldName" runat="server" Text="Line3(Area):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtCorrAdrLine3" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblResidenceLivingDate" CssClass="FieldName" runat="server" Text="Living Since:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtLivingSince" runat="server" CssClass="txtField"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtLivingSince_CalendarExtender" runat="server" TargetControlID="txtLivingSince"
                        Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtLivingSince_TextBoxWatermarkExtender" WatermarkText="dd/mm/yyyy"
                        TargetControlID="txtLivingSince" runat="server">
                    </cc1:TextBoxWatermarkExtender>
                    <asp:CompareValidator ID="txtLivingSince_CompareValidator" runat="server" ErrorMessage="<br/>Please enter a valid date."
                        Type="Date" ControlToValidate="txtLivingSince" CssClass="cvPCG" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label14" CssClass="FieldName" runat="server" Text="City:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtCorrAdrCity" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label16" CssClass="FieldName" runat="server" Text="State:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlCorrAdrState" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label15" CssClass="FieldName" runat="server" Text="Pin Code:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtCorrAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
                    <asp:CompareValidator ID="txtCorrAdrPinCode_comparevalidator" ControlToValidate="txtCorrAdrPinCode"
                        runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value"
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label17" CssClass="FieldName" runat="server" Text="Country:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtCorrAdrCountry" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="tabbertab" style="height: 250px;">
        <h6>
            Permanent Address</h6>
        <table style="width: 100%; height: 196px;">
            <tr>
                <td colspan="4">
                    <asp:Label ID="Label18" CssClass="HeaderTextSmall" runat="server" Text="Permanent Address "></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:CheckBox ID="chkCorrPerm" runat="server" CssClass="FieldName" Text="Same as Correspondance Address" />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label19" CssClass="FieldName" runat="server" Text="Line1(House No./Building):"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtPermAdrLine1" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label20" CssClass="FieldName" runat="server" Text="Line2(Street):"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtPermAdrLine2" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label21" CssClass="FieldName" runat="server" Text="Line3(Area):"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtPermAdrLine3" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label22" CssClass="FieldName" runat="server" Text="City:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPermAdrCity" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label23" CssClass="FieldName" runat="server" Text="State:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlPermAdrState" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label24" CssClass="FieldName" runat="server" Text="Pin Code:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPermAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
                    <asp:CompareValidator ID="txtPermAdrPinCode_CompareValidator" ControlToValidate="txtPermAdrPinCode"
                        runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value"
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label25" CssClass="FieldName" runat="server" Text="Country:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPermAdrCountry" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="tabbertab" style="height: 250px;">
        <h6>
            Office Address</h6>
        <table style="width: 100%; height: 213px;">
            <tr>
                <td colspan="4">
                    <asp:Label ID="Label26" CssClass="HeaderTextSmall" runat="server" Text="Office Address"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label34" CssClass="FieldName" runat="server" Text="Company Name:"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtOfcCompanyName" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label27" CssClass="FieldName" runat="server" Text="Line1(House No./Building):"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtOfcAdrLine1" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label28" CssClass="FieldName" runat="server" Text="Line2(Street):"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtOfcAdrLine2" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label29" CssClass="FieldName" runat="server" Text="Line3(Area):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtOfcAdrLine3" runat="server" CssClass="txtField" style="width: 30%" ></asp:TextBox>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblJobStartDate" CssClass="FieldName" runat="server" Text="Job Start Date:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtJobStartDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtJobStartDate_CalendarExtender" runat="server" TargetControlID="txtJobStartDate"
                        Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtJobStartDate_TextBoxWatermarkExtender" WatermarkText="dd/mm/yyyy"
                        TargetControlID="txtJobStartDate" runat="server">
                    </cc1:TextBoxWatermarkExtender>
                    <asp:CompareValidator ID="cvJobStartDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                        Type="Date" ControlToValidate="txtJobStartDate" CssClass="cvPCG" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label30" CssClass="FieldName" runat="server" Text="City:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtOfcAdrCity" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label31" CssClass="FieldName" runat="server" Text="State:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlOfcAdrState" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label32" CssClass="FieldName" runat="server" Text="Pin Code:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtOfcAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
                    <asp:CompareValidator ID="txtOfcAdrPinCode_CompareValidator" ControlToValidate="txtOfcAdrPinCode"
                        runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value"
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label33" CssClass="FieldName" runat="server" Text="Country:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtOfcAdrCountry" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="tabbertab" style="height: 250px;">
        <h6>
            Contact Details</h6>
        <table style="width: 100%; height: 170px;">
            <tr>
                <td colspan="4">
                    <asp:Label ID="Label35" CssClass="HeaderTextSmall" runat="server" Text="Contact Details"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label36" CssClass="FieldName" runat="server" Text="Telephone No.(Res):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtResPhoneNoIsd" runat="server" Width="30px" CssClass="txtField"
                        MaxLength="3">91</asp:TextBox>
                    <asp:TextBox ID="txtResPhoneNoStd" runat="server" Width="30px" CssClass="txtField"
                        MaxLength="3"></asp:TextBox>
                    <asp:TextBox ID="txtResPhoneNo" runat="server" Width="90px" CssClass="txtField" MaxLength="8"></asp:TextBox>
                    <asp:CompareValidator ID="txtResPhoneNoIsd_CompareValidator" ControlToValidate="txtResPhoneNoIsd"
                        runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value for ISD code."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                    <asp:CompareValidator ID="txtResPhoneNoStd_CompareValidator" ControlToValidate="txtResPhoneNoStd"
                        runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for STD code."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                    <asp:CompareValidator ID="txtResPhoneNo_CompareValidator" ControlToValidate="txtResPhoneNo"
                        runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for Phone number."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label41" CssClass="FieldName" runat="server" Text="Fax(Res):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtResFaxIsd" runat="server" Width="30px" CssClass="txtField" MaxLength="3">91</asp:TextBox>
                    <asp:TextBox ID="txtResFaxStd" runat="server" Width="30px" CssClass="txtField" MaxLength="3"></asp:TextBox>
                    <asp:TextBox ID="txtResFax" runat="server" Width="90px" CssClass="txtField" MaxLength="8"></asp:TextBox>
                    <asp:CompareValidator ID="txtResFaxIsd_CompareValidator" ControlToValidate="txtResFaxIsd"
                        runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value for ISD code."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                    <asp:CompareValidator ID="txtResFaxStd_CompareValidator" ControlToValidate="txtResFaxStd"
                        runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for STD code."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                    <asp:CompareValidator ID="txtResFax_CompareValidator" ControlToValidate="txtResFax"
                        runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for Fax number."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label37" CssClass="FieldName" runat="server" Text="Telephone No.(Off):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtOfcPhoneNoIsd" runat="server" Width="30px" CssClass="txtField"
                        MaxLength="3">91</asp:TextBox>
                    <asp:TextBox ID="txtOfcPhoneNoStd" runat="server" Width="30px" CssClass="txtField"
                        MaxLength="3"></asp:TextBox>
                    <asp:TextBox ID="txtOfcPhoneNo" runat="server" Width="90px" CssClass="txtField" MaxLength="8"></asp:TextBox>
                    <asp:CompareValidator ID="txtOfcPhoneNoIsd_CompareValidator" ControlToValidate="txtOfcPhoneNoIsd"
                        runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value for ISD code."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                    <asp:CompareValidator ID="txtOfcPhoneNoStd_CompareValidator" ControlToValidate="txtOfcPhoneNoStd"
                        runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for STD code."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                    <asp:CompareValidator ID="txtOfcPhoneNo_CompareValidator" ControlToValidate="txtOfcPhoneNo"
                        runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for Phone number."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label42" CssClass="FieldName" runat="server" Text="Fax(Off):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtOfcFaxIsd" runat="server" Width="30px" CssClass="txtField" MaxLength="3">91</asp:TextBox>
                    <asp:TextBox ID="txtOfcFaxStd" runat="server" Width="30px" CssClass="txtField" MaxLength="3"></asp:TextBox>
                    <asp:TextBox ID="txtOfcFax" runat="server" Width="90px" CssClass="txtField" MaxLength="8"></asp:TextBox>
                    <asp:CompareValidator ID="txtOfcFaxIsd_CompareValidator" ControlToValidate="txtOfcFaxIsd"
                        runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value for ISD Code."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                    <asp:CompareValidator ID="txtOfcFaxStd_CompareValidator" ControlToValidate="txtOfcFaxStd"
                        runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for STD Code."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                    <asp:CompareValidator ID="txtOfcFax_CompareValidator" ControlToValidate="txtOfcFax"
                        runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for Fax Number."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label38" CssClass="FieldName" runat="server" Text="Mobile1:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtMobile1" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
                    <asp:CompareValidator ID="txtMobile1_CompareValidator" ControlToValidate="txtMobile1"
                        runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for Mobile Number."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label43" CssClass="FieldName" runat="server" Text="Mobile2:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtMobile2" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
                    <asp:CompareValidator ID="txtMobile2_CompareValidator" ControlToValidate="txtMobile2"
                        runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for Mobile Number."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label39" CssClass="FieldName" runat="server" Text="Email:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField" style="width: 30%" ></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                        ErrorMessage="<br />Please enter a valid Email ID" Display="Dynamic" runat="server"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="revPCG"></asp:RegularExpressionValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label40" CssClass="FieldName" runat="server" Text="Alternate Email:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtAltEmail" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="txtAltEmail_RegularExpressionValidator" ControlToValidate="txtAltEmail"
                        ErrorMessage="<br />Please enter a valid Email ID" Display="Dynamic" runat="server"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="revPCG"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
    </div>
    <div class="tabbertab" style="height: 250px;">
        <h6>
            Additional Information</h6>
        <table style="width: 100%; height: 163px;">
            <tr>
                <td colspan="4">
                    <asp:Label ID="Label44" CssClass="HeaderTextSmall" runat="server" Text="Additional Information"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label45" CssClass="FieldName" runat="server" Text="Occupation:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlOccupation" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label46" CssClass="FieldName" runat="server" Text="Qualification:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlQualification" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label47" CssClass="FieldName" runat="server" Text="Marital Status:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="cmbField" OnChange="OnMaritalStatusChange(this)">
                    </asp:DropDownList>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label48" CssClass="FieldName" runat="server" Text="Nationality:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlNationality" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="leftField" width="25%">
                    <asp:Label ID="Label6" CssClass="FieldName" runat="server" Text="Marriage Date:"></asp:Label>
                </td>
                <td class="rightField" width="25%">
                    <asp:TextBox ID="txtMarriageDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtMarriageDate_CalendarExtender" runat="server" TargetControlID="txtMarriageDate"
                        Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtMarriageDate"
                        WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <asp:CompareValidator ID="txtMarriageDate_CompareValidator" runat="server" ErrorMessage="<br/>Please enter a valid date."
                        Type="Date" ControlToValidate="txtMarriageDate" CssClass="cvPCG" Operator="DataTypeCheck"
                        ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                </td>
                <td class="leftField" width="25%">
                    <asp:Label ID="lblMotherMaidenName" CssClass="FieldName" runat="server" Text="Mother's Maiden Name:"></asp:Label>
                </td>
                <td class="rightField" width="25%">
                    <asp:TextBox ID="txtMotherMaidenName" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblRBIRefNo" CssClass="FieldName" runat="server" Text="RBI Reference Number:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtRBIRefNo" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblRBIRefDate" CssClass="FieldName" runat="server" Text="RBI Reference Date:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtRBIRefDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtRBIRefDate_CalendarExtender" runat="server" TargetControlID="txtRBIRefDate">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtRBIRefDate_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtRBIRefDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                </td>
            </tr>
            <tr>
                        <td class="leftField" width="25%">
                                <asp:Label ID="Label9" runat="server" Text="Alert Preferances:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                 <asp:Checkbox ID="chkmail" runat="server" CssClass="txtField" Text="Via Mail"
                AutoPostBack="false"  Enabled = "true"/>
                &nbsp;
            &nbsp;
                 <asp:Checkbox ID="chksms" runat="server" CssClass="txtField" Text="Via SMS"
                AutoPostBack="false"  Enabled = "true"/>
                            </td>
                           
        <td class="leftField">
            <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Date of Birth:"></asp:Label>
           
        </td>
        <td class="rightField">
         
            <asp:TextBox ID="txtDob" runat="server" CssClass="txtField"></asp:TextBox>
            <asp:CompareValidator ID="cvDepositDate1" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtDob" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDob"
                Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="dd/mm/yyyy"
                TargetControlID="txtDob" runat="server">
            </cc1:TextBoxWatermarkExtender>
        </td>
    
                        </tr>
        </table>
    </div>
</div>
<table style="width: 100%;">
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="3" class="SubmitCell">
            <asp:Button ID="btnEdit" runat="server" Text="Update" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_EditCustomerIndividualProfile_btnEdit');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_EditCustomerIndividualProfile_btnEdit');"
                OnClick="btnEdit_Click" />
        </td>
    </tr>
</table>
