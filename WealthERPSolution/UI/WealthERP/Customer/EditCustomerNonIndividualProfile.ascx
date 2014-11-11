<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditCustomerNonIndividualProfile.ascx.cs"
    Inherits="WealthERP.Customer.EditCustomerNonIndividualProfile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<script src="../Scripts/tabber.js" type="text/javascript"></script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<script type="text/javascript">
    function ButtonClick() {
        return true;
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

<table width="100%">
    <tr>
        <td colspan="3">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Edit Profile
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td class="rightField" style="width: 75%;">
            <table style="width: 100%;">
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
                        <asp:CompareValidator ID="ddlAdviserBranchList_CompareValidator" runat="server" ValidationGroup="btnSubmit"
                            ControlToValidate="ddlAdviserBranchList" ErrorMessage="Please select a Branch"
                            Operator="NotEqual" ValueToCompare="Select a Branch" CssClass="cvPCG">
                        </asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblCustomerSubType" runat="server" CssClass="FieldName" Text="Customer Sub Type:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCustomerSubType" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                        <span id="Span2" class="spnRequiredField">*</span>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlCustomerSubType"
                            ValidationGroup="btnSubmit" ErrorMessage="Please select a Customer Sub-Type"
                            Operator="NotEqual" ValueToCompare="Select a Sub-Type" CssClass="cvPCG"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Date of Profiling:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtDateofProfiling" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr id="trSalutation" runat="server">
                    <td class="leftField">
                        <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Salutation:"></asp:Label>
                    </td>
                    <td class="rightField"">
                        <asp:DropDownList ID="ddlSalutation" runat="server" CssClass="cmbField">
                            <asp:ListItem>Mr.</asp:ListItem>
                            <asp:ListItem>Mrs.</asp:ListItem>
                            <asp:ListItem>Ms.</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                            <asp:ListItem>M.S.</asp:ListItem>
                            <asp:ListItem>Dr.</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Name of Company:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtCompanyName" runat="server" CssClass="txtField"></asp:TextBox>
                        <span id="spAccountNumber" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="rfvComapanyName" ControlToValidate="txtCompanyName"
                            ValidationGroup="btnSubmit" ErrorMessage="Please enter Company Name" Display="Dynamic"
                            runat="server" CssClass="rfvPCG">
                        </asp:RequiredFieldValidator>
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
                    <td class="leftField">
                        <asp:Label ID="Label10" runat="server" CssClass="FieldName" Text="Date Of Registration:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtDateofRegistration" runat="server" CssClass="txtField"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtDateofRegistration_CalendarExtender" runat="server"
                            TargetControlID="txtDateofRegistration" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="txtDateofRegistration_TextBoxWatermarkExtender"
                            WatermarkText="dd/mm/yyyy" TargetControlID="txtDateofRegistration" runat="server">
                        </cc1:TextBoxWatermarkExtender>
                    </td>
                    <td align="left">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="Date Of Commencement:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtDateofCommencement" runat="server" CssClass="txtField"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateofCommencement"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="dd/mm/yyyy"
                            TargetControlID="txtDateofCommencement" runat="server">
                        </cc1:TextBoxWatermarkExtender>
                    </td>
                    <td align="left">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label12" runat="server" CssClass="FieldName" Text="Reg. No. with ROC-Registrar:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtRocRegistration" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label13" runat="server" CssClass="FieldName" Text="Place Of Registration:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtRegistrationPlace" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label14" runat="server" CssClass="FieldName" Text="Company Website:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtCompanyWebsite" runat="server" CssClass="txtField"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="PAN Number:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtPanNumber" runat="server" CssClass="txtField"></asp:TextBox>
                        <asp:CheckBox ID="chkdummypan" runat="server" CssClass="txtField" Text="Dummy PAN"
                            AutoPostBack="true" />
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label15" runat="server" CssClass="FieldName" Text="Contact Person Name:"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                        &nbsp;
                        <asp:TextBox ID="txtMiddleName" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                        &nbsp;
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <telerik:RadTabStrip ID="RadTabStripCustomerProfile" runat="server" EnableTheming="True"
                Skin="Telerik" EnableEmbeddedSkins="False" MultiPageID="CustomerProfileDetails"
                SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab runat="server" Text="Correspondence Address" Value="CorrespondenceAddress"
                        TabIndex="0">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Permanent Address" Value="PermanentAddress"
                        TabIndex="1">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Contact Details" Value="ContactDetails" TabIndex="2">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Additional Information" Value="AdditionalInformation"
                        TabIndex="3">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="CustomerProfileDetails" EnableViewState="true" runat="server">
                <telerik:RadPageView ID="rpvCorrespondenceAddress" Selected="true" runat="server">
                    <asp:Panel ID="pnlCorrespondenceAddress" runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="4">
                                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                                        Correspondence Address
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="Label19" runat="server" Text="Line1(House No./Building):" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtCorrAdrLine1" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                                    <span id="spBranchName" class="spnRequiredField">*</span>
                                    <asp:RequiredFieldValidator ID="reqtxtCorrAdrLine1" runat="server" ControlToValidate="txtCorrAdrLine1"
                                        ValidationGroup="btnEdit" Enabled="false" InitialValue="" Display="Dynamic" ErrorMessage="Please fill address line1"
                                        CssClass="rfvPCG"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="Label20" runat="server" Text="Line2(Street):" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtCorrAdrLine2" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="Label21" runat="server" Text="Line3(Area):" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtCorrAdrLine3" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="Label22" runat="server" Text="City:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:DropDownList ID="ddlCorrAdrCity" runat="server" CssClass="cmbField">
                                    </asp:DropDownList>
                                    <span id="Span10" class="spnRequiredField">*</span>
                                    <asp:RequiredFieldValidator ID="reqddlCorrAdrCity" runat="server" CssClass="cvPCG"
                                        ControlToValidate="ddlCorrAdrCity" ErrorMessage="Select city" Display="Dynamic"
                                        ValidationGroup="btnEdit" InitialValue="0"></asp:RequiredFieldValidator>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="Label23" runat="server" Text="State:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:DropDownList ID="ddlCorrAdrState" runat="server" CssClass="txtField">
                                        <asp:ListItem>Karnataka</asp:ListItem>
                                        <asp:ListItem>Andhra Pradesh</asp:ListItem>
                                        <asp:ListItem>Tamil Nadu</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="Label24" runat="server" Text="Pin Code:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtCorrAdrPinCode" MaxLength="6" runat="server" OnKeypress="javascript:return isNumberKey(event);"
                                        CssClass="txtField"></asp:TextBox>
                                    <asp:CompareValidator ID="txtCorrAdrPinCode_comparevalidator" ControlToValidate="txtCorrAdrPinCode"
                                        runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value"
                                        Type="Integer" Enabled="false" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="Label1" runat="server" Text="Country:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:DropDownList ID="ddlCorrAdrCountry" runat="server" CssClass="cmbField">
                                        <asp:ListItem>India</asp:ListItem>
                                        <asp:ListItem>USA</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <td>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            </td>
                            <tr>
                            </tr>
                        </table>
                    </asp:Panel>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvPermanentAddress" runat="server">
                    <asp:Panel ID="pnlPermanentAddress" runat="server">
                        <table style="width: 100%; height: 196px;">
                            <tr>
                                <td colspan="4">
                                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                                        Permanent Address
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="Label27" runat="server" Text="Line1(House No./Building):" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtPermAdrLine1" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="Label28" runat="server" Text="Line2(Street):" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtPermAdrLine2" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="Label29" runat="server" Text="Line3(Area):" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtPermAdrLine3" runat="server" CssClass="txtField" Style="width: 30%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="Label30" runat="server" Text="City:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:DropDownList ID="ddlPermAdrCity" runat="server" CssClass="cmbField">
                                    </asp:DropDownList>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="Label31" runat="server" Text="State:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:DropDownList ID="ddlPermAdrState" runat="server" CssClass="cmbField">
                                        <asp:ListItem>Karnataka</asp:ListItem>
                                        <asp:ListItem>Andhra Pradesh</asp:ListItem>
                                        <asp:ListItem>Tamil Nadu</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="Label32" runat="server" Text="Pin Code:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtPermAdrPinCode" MaxLength="6" runat="server" OnKeypress="javascript:return isNumberKey(event);"
                                        CssClass="txtField"></asp:TextBox>
                                    <asp:CompareValidator ID="txtPermAdrPinCode_CompareValidator" ControlToValidate="txtPermAdrPinCode"
                                        runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value"
                                        Type="Integer" Enabled="false" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="Label25" runat="server" Text="Country:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:DropDownList ID="ddlPermAdrCountry" runat="server" CssClass="cmbField">
                                        <asp:ListItem>India</asp:ListItem>
                                        <asp:ListItem>USA</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvContactDetails" runat="server">
                    <asp:Panel ID="pnlContactDetails" runat="server">
                        <table style="width: 100%; height: 170px;">
                            <tr>
                                <td colspan="4">
                                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                                        Contact Details
                                    </div>
                                </td>
                            </tr>
                            <tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="Label34" runat="server" Text="Telephone No.1:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtPhoneNo1Isd" runat="server" Width="40px" CssClass="txtField"></asp:TextBox>
                                    <asp:TextBox ID="txtPhoneNo1Std" runat="server" Width="40px" CssClass="txtField"></asp:TextBox>
                                    <asp:TextBox ID="txtPhoneNo1" runat="server" Width="100px" CssClass="txtField"></asp:TextBox>
                                    <asp:CompareValidator ID="txtPhoneNo1Isd_CompareValidator" ControlToValidate="txtPhoneNo1Isd"
                                        runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value for ISD code."
                                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                                    <asp:CompareValidator ID="txtPhoneNo1Std_CompareValidator" ControlToValidate="txtPhoneNo1Std"
                                        runat="server" Enabled="false" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for STD code."
                                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                                    <asp:CompareValidator ID="txtPhoneNo1_CompareValidator" ControlToValidate="txtPhoneNo1"
                                        runat="server" Enabled="false" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for Phone number."
                                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="Label35" runat="server" Text="Telephone No.2:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtPhoneNo2Isd" runat="server" Width="40px" CssClass="txtField"></asp:TextBox>
                                    <asp:TextBox ID="txtPhoneNo2Std" runat="server" Width="40px" CssClass="txtField"></asp:TextBox>
                                    <asp:TextBox ID="txtPhoneNo2" runat="server" Width="100px" CssClass="txtField"></asp:TextBox>
                                    <asp:CompareValidator ID="txtPhoneNo2Isd_CompareValidator" ControlToValidate="txtPhoneNo2Isd"
                                        runat="server" Enabled="false" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value for ISD code."
                                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                                    <asp:CompareValidator ID="txtPhoneNo2Std_CompareValidator" ControlToValidate="txtPhoneNo2Std"
                                        runat="server" Enabled="false" Display="Dynamic" ErrorMessage=" <br />Please enter a numeric value for STD code."
                                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                                    <asp:CompareValidator ID="txtPhoneNo2_CompareValidator" ControlToValidate="txtPhoneNo2"
                                        runat="server" Enabled="false" Display="Dynamic" ErrorMessage=" <br />Please enter a numeric value for Phone number."
                                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="Label36" runat="server" Text="Fax:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtFaxIsd" runat="server" Width="40px" CssClass="txtField"></asp:TextBox>
                                    <asp:TextBox ID="txtFaxStd" runat="server" Width="40px" CssClass="txtField"></asp:TextBox>
                                    <asp:TextBox ID="txtFax" runat="server" Width="100px" CssClass="txtField"></asp:TextBox>
                                    <asp:CompareValidator ID="txtFaxIsd_CompareValidator" ControlToValidate="txtFaxIsd"
                                        runat="server" Enabled="false" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value for ISD code."
                                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                                    <asp:CompareValidator ID="txtFaxStd_CompareValidator" ControlToValidate="txtFaxStd"
                                        runat="server" Enabled="false" Display="Dynamic" ErrorMessage=" <br />Please enter a numeric value for STD code."
                                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                                    <asp:CompareValidator ID="txtFax_CompareValidator" ControlToValidate="txtFax" runat="server"
                                        Display="Dynamic" Enabled="false" ErrorMessage=" <br />Please enter a numeric value for Fax number."
                                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="Label37" runat="server" Text="Email Id:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                                        ErrorMessage="Please enter a valid Email ID" Display="Dynamic" Enabled="false"
                                        runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        CssClass="revPCG"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="lblAltEmail" runat="server" Text="Alternate Email:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtAltEmail" runat="server" CssClass="txtField"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Enabled="false"
                                        ControlToValidate="txtAltEmail" ErrorMessage="Please enter a valid Email ID"
                                        Display="Dynamic" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        CssClass="revPCG"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="Label38" CssClass="FieldName" runat="server" Text="Mobile1:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMobile1" runat="server" OnKeypress="javascript:return isNumberKey(event);"
                                        CssClass="txtField" MaxLength="10"></asp:TextBox>
                                    <asp:RegularExpressionValidator ValidationGroup="btnEdit" ControlToValidate="txtMobile1"
                                        Display="Dynamic" Enabled="false" ErrorMessage="Telephone Number must be 7-11 digit"
                                        ValidationExpression="^((\+)?(\d{2}[-]))?(\d{10}){1}?$"></asp:RegularExpressionValidator>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="Label43" CssClass="FieldName" runat="server" Text="Mobile2:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMobile2" runat="server" OnKeypress="javascript:return isNumberKey(event);"
                                        CssClass="txtField" MaxLength="10"></asp:TextBox>
                                    <asp:RegularExpressionValidator ValidationGroup="btnEdit" ControlToValidate="txtMobile2"
                                        Display="Dynamic" Enabled="false" ErrorMessage="Telephone Number must be 7-11 digit"
                                        ValidationExpression="^((\+)?(\d{2}[-]))?(\d{10}){1}?$"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvAdditionalInformation" runat="server">
                    <asp:Panel ID="pnlAdditionalInformation" runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="4">
                                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                                        Additional Information
                                    </div>
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
                                    <asp:Label ID="lblAnnualIncome" CssClass="FieldName" runat="server" Text="AnnualIncome:"></asp:Label>
                                </td>
                                <td class="rightField" width="25%">
                                    <asp:TextBox ID="txtAnnualIncome" runat="server" CssClass="txtField"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator2" ControlToValidate="txtAnnualIncome"
                                        runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value"
                                        Type="Integer" Enabled="false" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
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
                                <td class="leftField">
                                    <asp:Label ID="lblMinno1" CssClass="FieldName" runat="server" Text="MinNo1:"></asp:Label>
                                </td>
                                <td class="rightField" width="25%">
                                    <asp:TextBox ID="txtMinNo1" runat="server" CssClass="txtField"></asp:TextBox>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="lblMinno2" CssClass="FieldName" runat="server" Text="MinNo2:"></asp:Label>
                                </td>
                                <td class="rightField" width="25%">
                                    <asp:TextBox ID="txtMinNo2" runat="server" CssClass="txtField"></asp:TextBox>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="lblMinno3" CssClass="FieldName" runat="server" Text="MinNo3:"></asp:Label>
                                </td>
                                <td class="rightField" width="25%">
                                    <asp:TextBox ID="txtMinNo3" runat="server" CssClass="txtField"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="lblESCNo" CssClass="FieldName" runat="server" Text="ESCNo:"></asp:Label>
                                </td>
                                <td class="rightField" width="25%">
                                    <asp:TextBox ID="txtESCNo" runat="server" CssClass="txtField"></asp:TextBox>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="lblUINNo" CssClass="FieldName" runat="server" Text="UINNo:"></asp:Label>
                                </td>
                                <td class="rightField" width="25%">
                                    <asp:TextBox ID="txtUINNo" runat="server" CssClass="txtField"></asp:TextBox>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="lblPOA" CssClass="FieldName" runat="server" Text="POA:"></asp:Label>
                                </td>
                                <td class="rightField" width="25%">
                                    <asp:TextBox ID="txtPOA" runat="server" MaxLength="3" CssClass="txtField"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="lblSubBroker" CssClass="FieldName" runat="server" Text="SubBroker:"></asp:Label>
                                </td>
                                <td class="rightField" width="25%">
                                    <asp:TextBox ID="txtSubBroker" runat="server" CssClass="txtField"></asp:TextBox>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Date of Birth:"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:CompareValidator ID="cvDepositDate1" runat="server" ErrorMessage="<br/>Please enter a valid date."
                                        Type="Date" ControlToValidate="txtDate" CssClass="cvPCG" Operator="DataTypeCheck"
                                        ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                                    <telerik:RadDatePicker ID="txtDate" CssClass="txtTo" runat="server" Culture="English (United States)"
                                        Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" onChange="Compare();"
                                        MinDate="1900-01-01">
                                        <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                            ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                    <span id="Span13" class="spnRequiredField">*</span>
                                    <asp:Label ID="lblBirthMsg" runat="server"></asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvtxtDate" ControlToValidate="txtDate" ValidationGroup="btnEdit"
                                        ErrorMessage="<br />Please enter DOB" Display="Dynamic" runat="server" CssClass="rfvPCG"
                                        InitialValue="">
                                    </asp:RequiredFieldValidator>
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
                                    <asp:Label ID="lblGuardianName" CssClass="FieldName" runat="server" Text="GuardianName:"></asp:Label>
                                </td>
                                <td class="rightField" width="25%">
                                    <asp:TextBox ID="txtGuardianName" runat="server" CssClass="txtField"></asp:TextBox>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="lblGuardianRelation" CssClass="FieldName" runat="server" Text="GuardianRelation:"></asp:Label>
                                </td>
                                <td class="rightField" width="25%">
                                    <asp:TextBox ID="txtGuardianRelation" runat="server" CssClass="txtField"></asp:TextBox>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="lblContactGuardianPANNum" CssClass="FieldName" runat="server" Text="GuardianPANNum:"></asp:Label>
                                </td>
                                <td class="rightField" width="25%">
                                    <asp:TextBox ID="txtContactGuardianPANNum" runat="server" CssClass="txtField"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField" width="25%">
                                    <asp:Label ID="Label6" runat="server" Text="Alert Preferences:" CssClass="FieldName"></asp:Label>
                                </td>
                                <td class="rightField" width="25%">
                                    <asp:CheckBox ID="chkmailn" runat="server" CssClass="txtField" Text="Via Mail" AutoPostBack="false"
                                        Enabled="true" />
                                    &nbsp;
                                    <asp:CheckBox ID="chksmsn" runat="server" CssClass="txtField" Text="Via SMS" Checked="true"
                                        AutoPostBack="false" Enabled="true" />
                                </td>
                                <td class="leftField" width="25%">
                                    <asp:Label ID="lblGuardianMinNo" CssClass="FieldName" runat="server" Text="GuardianMinNo:"></asp:Label>
                                </td>
                                <td class="rightField" width="25%">
                                    <asp:TextBox ID="txtGuardianMinNo" runat="server" CssClass="txtField"></asp:TextBox>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="lblGuardianDOB" runat="server" CssClass="FieldName" Text="Guardian Date Of Birth:"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="<br/>Please enter a valid date."
                                        Type="Date" Enabled="false" ControlToValidate="txtGuardianDOB" CssClass="cvPCG"
                                        Operator="DataTypeCheck" ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                                    <telerik:RadDatePicker ID="txtGuardianDOB" CssClass="txtTo" runat="server" Culture="English (United States)"
                                        Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                                        <Calendar ID="Calendar3" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                            ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        <DateInput ID="DateInput3" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="lblOtherBankName" CssClass="FieldName" runat="server" Text="Other BankName:"></asp:Label>
                                </td>
                                <td class="rightField" width="25%">
                                    <asp:TextBox ID="txtOtherBankName" runat="server" CssClass="txtField"></asp:TextBox>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="lblTaxStatus" CssClass="FieldName" runat="server" Text="TaxStatus:"></asp:Label>
                                </td>
                                <td class="rightField" width="25%">
                                    <asp:TextBox ID="txtTaxStatus" runat="server" CssClass="txtField"></asp:TextBox>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="lblCategory" CssClass="FieldName" runat="server" Text="Category:"></asp:Label>
                                </td>
                                <td class="rightField" width="25%">
                                    <asp:TextBox ID="txtCategory" runat="server" CssClass="txtField"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="lblAdr1City" CssClass="FieldName" runat="server" Text="Other City:"></asp:Label>
                                </td>
                                <td class="rightField" width="25%">
                                    <asp:TextBox ID="txtAdr1City" runat="server" CssClass="txtField"></asp:TextBox>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="lblAdr1State" CssClass="FieldName" runat="server" Text="Other State:"></asp:Label>
                                </td>
                                <td class="rightField" width="25%">
                                    <asp:TextBox ID="txtAdr1State" runat="server" CssClass="txtField"></asp:TextBox>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="lblOtherCountry" CssClass="FieldName" runat="server" Text="Other Country:"></asp:Label>
                                </td>
                                <td class="rightField" width="25%">
                                    <asp:TextBox ID="txtOtherCountry" runat="server" CssClass="txtField"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
            <table>
                <tr>
                    <td colspan="4" class="SubmitCell">
                        <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Update" CssClass="PCGButton"
                            ValidationGroup="btnSubmit" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_EditCustomerNonIndividualProfile_btnEdit', 'S');"
                            onmouseout="javascript:ChangeButtonCss('out', 'ctrl_EditCustomerNonIndividualProfile_btnEdit', 'S');" />
                    </td>
                </tr>
            </table>
            