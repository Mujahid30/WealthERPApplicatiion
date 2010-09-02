<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditAdvisorProfile.ascx.cs"
    Inherits="WealthERP.Advisor.EditAdvisorProfile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Edit Profile "></asp:Label>
            <hr />
        </td>
    </tr>
</table>

<table style="width: 100%;" class="TableBackground">
    <tr>
        <td colspan="6" class="tdRequiredText">
            <label id="lbl" class="lblRequiredText">
                Note: Fields marked with ' * ' are compulsory</label>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 30%">
            <asp:Label ID="lblOrganizationName" runat="server" Text="Name Of the Organization / Individual :"
                CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
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
            <asp:Label ID="lblFirst" runat="server" CssClass="FieldName" Text="First Name :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtField"></asp:TextBox>
              <span id="Span2" class="spnRequiredField">*<br />
            </span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtFirstName"
                ErrorMessage="Please enter the First Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
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
        <td colspan="4">
            <asp:Label ID="Label4" runat="server" CssClass="HeaderTextSmall" Text="Office Address"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblLine1" runat="server" CssClass="FieldName" Text="Line1 (House No/Building) :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtAddressLine1" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span4" class="spnRequiredField">*<br />
            </span>
            <asp:RequiredFieldValidator ID="rfvAddressLine1" ControlToValidate="txtAddressLine1"
                ErrorMessage="Please enter the Address Line 1" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Line2 (Street) :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtAddressLine2" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Line3 (Area) :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtAddressLine3" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="City :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtCity" runat="server" CssClass="txtField">
            </asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPincode" runat="server" CssClass="FieldName" Text="Pincode :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtPinCode" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
            <%--<asp:CompareValidator ID="CompareValidator12" runat="server" ErrorMessage="<br/>Enter a numeric value"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Type="Integer" ControlToValidate="txtPinCode"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>--%>
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtPinCode"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label10" runat="server" CssClass="FieldName" Text="State :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:DropDownList ID="ddlState" runat="server" CssClass="txtField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="Country :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="txtField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="Label12" runat="server" CssClass="HeaderTextSmall" Text="Contact Details"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Label ID="lblISD" runat="server" CssClass="FieldName" Text="ISD"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblSTD" runat="server" CssClass="FieldName" Text="STD"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblPhoneNUmber" runat="server" CssClass="FieldName" Text="Phone Number"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPhone1" runat="server" CssClass="FieldName" Text="Telephone Number1 :"
                BorderStyle="None"></asp:Label>
        </td>
        <td class="rightField" width="15px">
            <asp:TextBox ID="txtISD1" runat="server" Width="55px" MaxLength="4" CssClass="txtField"></asp:TextBox>
            <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br/>Enter a numeric value"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Type="Integer" ControlToValidate="txtISD1"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>--%>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtISD1"
                runat="server" CssClass="rfvPCG" ValidationGroup="btnSubmit" Display="Dynamic"
                ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField" width="15px">
            <asp:TextBox ID="txtSTD1" runat="server" Width="55px" MaxLength="4" CssClass="txtField"></asp:TextBox>
            <%-- <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="<br/>Enter a numeric value"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Type="Integer" ControlToValidate="txtSTD1"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>--%>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtSTD1"
                CssClass="rfvPCG" ValidationGroup="btnSubmit" Display="Dynamic" runat="server"
                ErrorMessage="<br/>Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtPhoneNumber1" runat="server" MaxLength="8" CssClass="txtField"></asp:TextBox>
            <%--<asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<br/>Enter a numeric value"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Type="Integer" ControlToValidate="txtPhoneNumber1"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>--%>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtPhoneNumber1"
                ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                ErrorMessage="<br/>Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPhone2" runat="server" CssClass="FieldName" Text="Telephone Number2 :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtISD2" runat="server" Width="55px" MaxLength="4" CssClass="txtField"></asp:TextBox>
            <%-- <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="<br/>Not acceptable format"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Type="Integer" ControlToValidate="txtISD2"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>--%>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtISD2"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Display="Dynamic" runat="server"
                ErrorMessage="<br/>Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtSTD2" runat="server" Width="55px" MaxLength="4" CssClass="txtField"></asp:TextBox>
            <%--        <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="<br/>Not acceptable format"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Type="Integer" ControlToValidate="txtSTD2"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>--%>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ControlToValidate="txtSTD2"
                ErrorMessage="<br/>Not acceptable format" CssClass="rfvPCG" ValidationGroup="btnSubmit"
                Display="Dynamic" runat="server" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtPhoneNumber2" runat="server" CssClass="txtField" MaxLength="8"></asp:TextBox>
            <%-- <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="<br/>Not acceptable format"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Type="Integer" ControlToValidate="txtPhoneNumber2"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>--%>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="txtPhoneNumber2"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Display="Dynamic" runat="server"
                ErrorMessage="<br/>Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblFax" runat="server" CssClass="FieldName" Text="Fax :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtFaxISD" runat="server" Width="55px" MaxLength="4" CssClass="txtField"></asp:TextBox>
            <%-- <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="<br/>Not acceptable format"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Type="Integer" ControlToValidate="txtFaxISD"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>--%>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ControlToValidate="txtFaxISD"
                CssClass="rfvPCG" ValidationGroup="btnSubmit" ErrorMessage="<br/>Not acceptable format"
                Display="Dynamic" runat="server" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtFaxSTD" runat="server" Width="55px" MaxLength="4" CssClass="txtField"></asp:TextBox>
            <%-- <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="<br/>Not acceptable format"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Type="Integer" ControlToValidate="txtFaxSTD"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>--%>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ControlToValidate="txtFaxSTD"
                CssClass="rfvPCG" ValidationGroup="btnSubmit" Display="Dynamic" runat="server"
                ValidationExpression="^\d*$" ErrorMessage="<br/>Not acceptable format"></asp:RegularExpressionValidator>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtFax" runat="server" MaxLength="8" CssClass="txtField"></asp:TextBox>
            <%--   <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br/>Not acceptable format"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Type="Integer" ControlToValidate="txtFax"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>--%>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ControlToValidate="txtFax"
                CssClass="rfvPCG" ValidationGroup="btnSubmit" ErrorMessage="<br/>Not acceptable format"
                Display="Dynamic" runat="server" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblMobileNumber" runat="server" CssClass="FieldName" Text="Mobile Number :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ControlToValidate="txtMobileNumber"
                Display="Dynamic" runat="server" CssClass="rfvPCG" ErrorMessage="Not acceptable format"
                ValidationGroup="btnSubmit" ValidationExpression="^\d{10,10}$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblEmail" runat="server" CssClass="FieldName" Text="Email Id :"></asp:Label>
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
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label23" runat="server" CssClass="FieldName" Text="Website :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtwebsite" runat="server" CssClass="txtField"></asp:TextBox>  
            </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label18" runat="server" CssClass="FieldName" Text="Business Type :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:DropDownList ID="ddlBusinessType" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Associate Model Type :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:RadioButton ID="rbtnAssModelTypeYes" runat="server" CssClass="txtField" Text="Yes" GroupName="grpAssModel" />
            <asp:RadioButton ID="rbtnAssModelTypeNo" runat="server" CssClass="txtField" Text="No" GroupName="grpAssModel" />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label19" runat="server" CssClass="FieldName" Text="Multibranch Network :"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:RadioButton ID="rbtnYes" runat="server" CssClass="txtField" Text="Yes" GroupName="grpMultiBranch" />
            <asp:RadioButton ID="rbtnNo" runat="server" CssClass="txtField" Text="No" GroupName="grpMultiBranch" />
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
    <tr id="AdviserLogoHdr" runat="server">
        <td colspan="4">
            <asp:Label ID="Label14" runat="server" CssClass="HeaderTextSmall" Text="Logo"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr id="AdviserLogoRow" runat="server">
        <td>
            <asp:LinkButton ID="lnklogoChange" runat="server" CssClass="LinkButtons" OnClick="DisplayLogoControl">Click to change Logo</asp:LinkButton>
        </td></tr>
        <tr>
        <td class="leftField">
            <asp:Label ID="lblLogoChange" runat="server" CssClass="FieldName" Text="Change Adviser's Logo:"
                Visible="false"></asp:Label>
            <asp:FileUpload ID="logoChange" runat="server" Height="22px" Visible="false" />
        </td>
     
    </tr>
    <tr>
        <td class="SubmitCell" colspan="4">
            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_EditAdvisorProfile_btnSubmit', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_EditAdvisorProfile_btnSubmit', 'S');"
                Text="Submit" OnClick="btnSubmit_Click" />
        </td>
    </tr>
</table>
