<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisorRegistration.ascx.cs"
    Inherits="WealthERP.Advisor.AdvisorRegistration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .style1
    {
        height: 26px;
    }
</style>
<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Advisor Registration"></asp:Label>
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
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblOrganizationName" runat="server" Text="Organisation Name :" CssClass="FieldName"></asp:Label><%-- Of the Organization / Individual--%>
        </td>
        <td colspan="3" class="rightField">
            <asp:TextBox ID="txtOrganizationName" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtOrganizationName"
                ErrorMessage="Please enter the Organisation Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblContactPersonName" runat="server" Text="Contact Person Name :"
                CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:ScriptManager ID="scriptmanager1" runat="server">
            </asp:ScriptManager>
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtFirstName_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtFirstName" WatermarkText="FirstName">
            </cc1:TextBoxWatermarkExtender>
            <asp:TextBox ID="txtMiddleName" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtMiddleName_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtMiddleName" WatermarkText="MiddleName">
            </cc1:TextBoxWatermarkExtender>
            <asp:TextBox ID="txtLastName" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtFirstName"
                ErrorMessage="<br />Please enter the First Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <cc1:TextBoxWatermarkExtender ID="txtLastName_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtLastName" WatermarkText="LastName">
            </cc1:TextBoxWatermarkExtender>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="Label4" runat="server" Text="Office Address" CssClass="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblAddressLine1" runat="server" Text="Line1 (House No/Building) :"
                CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtAddressLine1" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span4" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvAddressLine1" ControlToValidate="txtAddressLine1"
                ErrorMessage="Please enter the Address Line 1" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label6" runat="server" Text="Line2 (Street) :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtAddressLine2" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label runat="server" Text="Line3 (Area) :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtAddressLine3" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label8" runat="server" Text="City :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtCity" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPincode" runat="server" Text="PinCode :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtPinCode" runat="server" CssClass="txtField" MaxLength="6" ValidationGroup="group1"></asp:TextBox>
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtPinCode"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label10" runat="server" Text="State :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:DropDownList ID="ddlState" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label11" runat="server" Text="Country :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="cmbField">
                <asp:ListItem>India</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="Label12" runat="server" Text="Contact Details" CssClass="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr style="height: 10px;">
        <td>
        </td>
        <td>
            <asp:Label ID="lblIsd" runat="server" Text="ISD" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblStd" runat="server" Text="STD" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblPhoneNumber" runat="server" Text="Phone Number" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPhone1" runat="server" Text="Telephone Number1 :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" width="15px">
            <asp:TextBox ID="txtISD1" runat="server" Width="55px" MaxLength="4" CssClass="txtField">91</asp:TextBox>
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtISD1"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField" width="15px">
            <asp:TextBox ID="txtSTD1" runat="server" Width="55px" CssClass="txtField" MaxLength="4"></asp:TextBox>
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtSTD1"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtPhoneNumber1" runat="server" CssClass="txtField" Width="119px"
                MaxLength="8"></asp:TextBox>
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtPhoneNumber1"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPhone2" runat="server" Text="Telephone Number2 :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtISD2" runat="server" Width="55px" MaxLength="4" CssClass="txtField">91</asp:TextBox>
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="txtISD2"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtSTD2" runat="server" Width="55px" CssClass="txtField" MaxLength="4"></asp:TextBox>
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtSTD2"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtPhoneNumber2" runat="server" Width="119px" MaxLength="8" CssClass="txtField"></asp:TextBox>
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ControlToValidate="txtPhoneNumber2"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblFax" runat="server" Text="Fax :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtFaxISD" runat="server" Width="55px" MaxLength="4" CssClass="txtField">91</asp:TextBox>
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ControlToValidate="txtFaxISD"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
        <td>
            <asp:TextBox ID="txtFaxSTD" runat="server" Width="55px" CssClass="txtField" MaxLength="4"></asp:TextBox><br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ControlToValidate="txtFaxSTD"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
        <td>
            <asp:TextBox ID="txtFaxNumber" runat="server" CssClass="txtField" Width="119px" MaxLength="8"></asp:TextBox><br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ControlToValidate="txtFaxNumber"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label15" runat="server" Text="Mobile Number :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ControlToValidate="txtMobileNumber"
                Display="Dynamic" runat="server" CssClass="rfvPCG" ErrorMessage="Not acceptable format"
                ValidationGroup="btnSubmit" ValidationExpression="^\d{10,10}$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblEmail" runat="server" Text="Email Id :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span3" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEmail"
                ErrorMessage="Please enter an Email ID" Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                ErrorMessage="Please enter a valid Email ID" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            <asp:Label ID="lblEmailError" runat="server" Text="Email id is already existing" CssClass="rfvPCG"></asp:Label>
        </td>
    </tr>
     <tr>
        <td class="leftField">
            <asp:Label ID="Label1" runat="server" Text="Website :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="TextBox1" runat="server" CssClass="txtField"></asp:TextBox>
            </td>
            </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label18" runat="server" Text="Business Type :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style1" colspan="3">
            <asp:DropDownList ID="ddlBusinessType" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label2" runat="server" Text="Associate Model Type :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3" valign="middle">
            <asp:RadioButton ID="RadioButton1" runat="server" CssClass="txtField" Font-Names="Arial"
                Font-Size="X-Small" Text="Yes" GroupName="grpassociation" />
            <asp:RadioButton ID="RadioButton2" runat="server" CssClass="txtField" Font-Names="Arial"
                Font-Size="X-Small" Text="No" GroupName="grpassociation" />
        </td>
        
        </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label19" runat="server" Text="Multibranch Network :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3" valign="middle">
            <asp:RadioButton ID="rbtnYes" runat="server" CssClass="txtField" Font-Names="Arial"
                Font-Size="X-Small" Text="Yes" GroupName="grpMultibranch" />
            <asp:RadioButton ID="rbtnNo" runat="server" CssClass="txtField" Font-Names="Arial"
                Font-Size="X-Small" Text="No" GroupName="grpMultibranch" />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label27" runat="server" CssClass="FieldName" Text="Roles:"></asp:Label>
        </td>
        <td class="rightField" colspan="3" valign="middle">
            <asp:CheckBox ID="chkRoleBM" runat="server" CssClass="txtField" Text="Branch Manager" />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="Label25" runat="server" CssClass="HeaderTextSmall" Text="Choose LOB"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:UpdatePanel runat="server" ID="up1">
                <ContentTemplate>
                    <table style="width: 105%; height: 93px;">
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkMf" runat="server" Text="Mutual Fund" AutoPostBack="True" OnCheckedChanged="chkMFEQ_CheckedChanged"
                                    CssClass="txtField" />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkIntermediary" runat="server" Text="Intermediary" CssClass="txtField" />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkCash" runat="server" Text="Cash Segment" CssClass="txtField" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkEquity" runat="server" Text="Equity" AutoPostBack="True" OnCheckedChanged="chkMFEQ_CheckedChanged"
                                    CssClass="txtField" />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkBroker" runat="server" Text="Broker" CssClass="txtField" />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkDerivative" runat="server" Text="Derivative Segment" CssClass="txtField" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:CheckBox ID="chkSubbroker" runat="server" Text="Sub Broker" CssClass="txtField" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:CheckBox ID="chkRemissary" runat="server" Text="Remissary" CssClass="txtField" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="Label26" runat="server" Text="Upload Your Logo :" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:FileUpload ID="AdvlogoPath" Height="22px" runat="server" />
                            </td>
                            <td>
                                <img alt="" id="logo" runat="server" src="" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4" align="left">
            <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" Text="Next" CssClass="PCGButton"
                onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AdvisorRegistration_btnNext');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AdvisorRegistration_btnNext');" />
        </td>
    </tr>
</table>
