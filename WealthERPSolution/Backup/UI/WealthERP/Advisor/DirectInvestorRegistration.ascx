  <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DirectInvestorRegistration.ascx.cs"
    Inherits="WealthERP.DirectInvestorRegistration" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
  <style type="text/css">
      .style1
      {
          width: 204px;
      }
  </style>
<table class="TableBackground">
<asp:ScriptManager ID="scriptmanager1" runat="server">
            </asp:ScriptManager>
    <tr>
        <td colspan="4" >
            <asp:Label ID="Label2" runat="server" Text="Direct Investor Registration" CssClass="HeaderTextSmall"></asp:Label>
            <hr /></td>
    </tr>
    <tr>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblCustomerType" runat="server" CssClass="FieldName" Text="Customer Type:"></asp:Label>
        </td>
        <td colspan="3">
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
        <td colspan="3">
            <asp:DropDownList ID="ddlCustomerSubType" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <br />
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlCustomerSubType"
                ErrorMessage="Please select a Customer Sub-Type" Operator="NotEqual" ValueToCompare="Select a Sub-Type"
                CssClass="cvPCG"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trCompanyName" runat="server">
        <td class="leftField">
            <asp:Label ID="lblCompanyName" runat="server" CssClass="FieldName" 
                Text="Company Name :"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtCompanyName_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtCompanyName" WatermarkText="CompanyName">
            </cc1:TextBoxWatermarkExtender>
        </td>
    </tr>
    <tr id="trIndividualName" runat="server">
        <td class="leftField">
            <asp:Label ID="lblName" runat="server" CssClass="FieldName" Text="Name (First/Middle/Last):"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtFirstName_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtFirstName" WatermarkText="FirstName">
            </cc1:TextBoxWatermarkExtender>
            <asp:TextBox ID="txtMiddleName" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtMiddleName_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtMiddleName" WatermarkText="MiddleName">
            </cc1:TextBoxWatermarkExtender>
            <asp:TextBox ID="txtLastName" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtLastName_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtLastName" WatermarkText="LastName">
            </cc1:TextBoxWatermarkExtender>
            <span id="Span3" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtFirstName"
                ErrorMessage="<br />Please enter the First Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPanNum" runat="server" CssClass="FieldName" Text="PAN Number:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtPanNumber" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
            <span id="Span2" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="rfvPanNumber" ControlToValidate="txtPanNumber" ErrorMessage="Please enter a PAN Number"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
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
            &nbsp;</td>
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
            <asp:Label ID="Label1" runat="server" Text="Line3 (Area) :" CssClass="FieldName"></asp:Label>
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
            <asp:CompareValidator ID="CompareValidator12" runat="server" ErrorMessage="<br/>Not acceptable format"
                ValidationGroup="btnSubmit" CssClass="rfvPCG" Type="Integer" ControlToValidate="txtPinCode"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
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
                                <asp:Label ID="Label35" runat="server" CssClass="HeaderTextSmall" Text="Contact Details"></asp:Label>
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                &nbsp
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="lblResPhone" CssClass="FieldName" runat="server" Text="Telephone No.(Res):"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtResPhoneNoIsd" runat="server" Width="40px" CssClass="txtField"
                                    MaxLength="2">91</asp:TextBox>
                                <asp:TextBox ID="txtResPhoneNoStd" runat="server" Width="40px" CssClass="txtField"
                                    MaxLength="3"></asp:TextBox>
                                <asp:TextBox ID="txtResPhoneNo" runat="server" Width="80px" CssClass="txtField" MaxLength="8"></asp:TextBox>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label41" CssClass="FieldName" runat="server" Text="Fax(Res):"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:TextBox ID="txtResFaxIsd" runat="server" Width="40px" CssClass="txtField" MaxLength="2">91</asp:TextBox>
                                <asp:TextBox ID="txtResFaxStd" runat="server" Width="40px" CssClass="txtField" MaxLength="3"></asp:TextBox>
                                <asp:TextBox ID="txtResFax" runat="server" Width="80px" CssClass="txtField" MaxLength="8"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="lblOfcPhone" CssClass="FieldName" runat="server" Text="Telephone No.(Ofc):"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtOfcPhoneNoIsd" runat="server" Width="40px" CssClass="txtField"
                                    MaxLength="2">91</asp:TextBox>
                                <asp:TextBox ID="txtOfcPhoneNoStd" runat="server" Width="40px" CssClass="txtField"
                                    MaxLength="3"></asp:TextBox>
                                <asp:TextBox ID="txtOfcPhoneNo" runat="server" Width="80px" CssClass="txtField" MaxLength="8"></asp:TextBox>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label42" CssClass="FieldName" runat="server" Text="Fax(Ofc):"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:TextBox ID="txtOfcFaxIsd" runat="server" Width="40px" CssClass="txtField" MaxLength="2">91</asp:TextBox>
                                <asp:TextBox ID="txtOfcFaxStd" runat="server" Width="40px" CssClass="txtField" MaxLength="3"></asp:TextBox>
                                <asp:TextBox ID="txtOfcFax" runat="server" Width="80px" CssClass="txtField" MaxLength="8"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="lblMobile1" CssClass="FieldName" runat="server" Text="Mobile1:"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtMobile1" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label43" CssClass="FieldName" runat="server" Text="Mobile2:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:TextBox ID="txtMobile2" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" >
                                <asp:Label ID="lblEmail" CssClass="FieldName" runat="server" Text="Email:"></asp:Label>
                                <br />
                                <asp:Label ID="Label3" CssClass="FieldName" runat="server" Text=" "></asp:Label>
                                <br />
                                <asp:Label ID="Label5" CssClass="FieldName" runat="server" Text=" "></asp:Label>
                            </td>
                            <td >
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField"></asp:TextBox>
                                 <span id="Span4" class="spnRequiredField">*</span>
                                <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEmail"
                ErrorMessage="Please enter an Email ID" Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
                               <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                ErrorMessage="Please enter a valid Email ID" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label40" CssClass="FieldName" runat="server" Text="Alternate Email:"></asp:Label>
                                <br />
                                <asp:Label ID="Label7" CssClass="FieldName" runat="server" Text=" "></asp:Label>
                                <br />
                                <asp:Label ID="Label9" CssClass="FieldName" runat="server" Text=" "></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:TextBox ID="txtAltEmail" runat="server" CssClass="txtField"></asp:TextBox>
                                <br />
                                <asp:Label ID="Label12" CssClass="FieldName" runat="server" Text=" "></asp:Label>
                                <br />
                                <asp:Label ID="Label13" CssClass="FieldName" runat="server" Text=" "></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                &nbsp;</td>
                            <td class="style1">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" 
                                    onclick="btnSubmit_Click" />
                            </td>
                            <td class="leftField" width="25%">
                                &nbsp;</td>
                            <td class="rightField" width="25%">
                                &nbsp;</td>
                        </tr>
</table>
