﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditCustomerNonIndividualProfile.ascx.cs"
    Inherits="WealthERP.Customer.EditCustomerNonIndividualProfile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script src="../Scripts/tabber.js" type="text/javascript"></script>

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
<table style="width: 100%; height: 391px;">
    <tr>
        <td>
            &nbsp;
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
    <td>
    </td>
    <td>
    <asp:Checkbox ID="chkprospectn" runat="server" CssClass="txtField"  Text="Prospect" 
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
        <td class="leftField">
            <asp:Label ID="lblCustomerSubType" runat="server" CssClass="FieldName" Text="Customer Sub Type:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlCustomerSubType" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlCustomerSubType"
                ErrorMessage="Please select a Customer Sub-Type" Operator="NotEqual" ValueToCompare="Select a Sub-Type"
                CssClass="cvPCG"></asp:CompareValidator>
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
    <tr>
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
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Name of Company:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="txtField"></asp:TextBox>
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
            
           
            <asp:Checkbox ID="chkdummypan" runat="server" CssClass="txtField" Text="Dummy PAN"
                AutoPostBack="true" />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label15" runat="server" CssClass="FieldName" Text="Contact Person Name:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
            &nbsp;
            <asp:TextBox ID="txtMiddleName" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
            &nbsp;
            <asp:TextBox ID="txtLastName" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
        </td>
    </tr>
   
     
</table>
<div class="tabber">
    <div class="tabbertab">
        <h6>
            Correspondence Address</h6>
        <table style="width: 100%; height: 196px;">
            <tr>
                <td colspan="4" class="HeaderCell">
                    <asp:Label ID="Label18" runat="server" Text="Correspondence Address" CssClass="HeaderTextSmall"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label19" runat="server" Text="Line1(House No./Building):" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtCorrAdrLine1" runat="server" CssClass="txtField" style="width: 30%" ></asp:TextBox>
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
                    <asp:Label ID="Label20" runat="server" Text="Line2(Street):" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtCorrAdrLine2" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
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
                    <asp:Label ID="Label21" runat="server" Text="Line3(Area):" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtCorrAdrLine3" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
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
                    <asp:Label ID="Label22" runat="server" Text="City:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtCorrAdrCity" runat="server" CssClass="txtField"></asp:TextBox>
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
                    <asp:TextBox ID="txtCorrAdrPinCode" runat="server" CssClass="txtField"></asp:TextBox>
                    <asp:CompareValidator ID="txtCorrAdrPinCode_comparevalidator" ControlToValidate="txtCorrAdrPinCode"
                        runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value"
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
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
        </table>
    </div>
    <div class="tabbertab">
        <h6>
            Permanent Address</h6>
        <table style="width: 100%; height: 196px;">
            <tr>
                <td colspan="4" class="HeaderCell">
                    <asp:Label ID="Label26" runat="server" Text="Permanent Address " CssClass="HeaderTextSmall"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label27" runat="server" Text="Line1(House No./Building):" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPermAdrLine1" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
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
                    <asp:TextBox ID="txtPermAdrLine2" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
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
                    <asp:Label ID="Label29" runat="server" Text="Line3(Area):" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPermAdrLine3" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
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
                    <asp:Label ID="Label30" runat="server" Text="City:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPermAdrCity" runat="server" CssClass="txtField"></asp:TextBox>
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
                    <asp:TextBox ID="txtPermAdrPinCode" runat="server" CssClass="txtField"></asp:TextBox>
                    <asp:CompareValidator ID="txtPermAdrPinCode_CompareValidator" ControlToValidate="txtPermAdrPinCode"
                        runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value"
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
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
    </div>
    <div class="tabbertab">
        <h6>
            Contact Details</h6>
        <table style="width: 100%; height: 196px;">
            <tr>
                <td colspan="4" class="HeaderCell">
                    <asp:Label ID="Label33" runat="server" Text="Contact Details" CssClass="HeaderTextSmall"></asp:Label>
                </td>
            </tr>
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
                        runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for STD code."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                    <asp:CompareValidator ID="txtPhoneNo1_CompareValidator" ControlToValidate="txtPhoneNo1"
                        runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for Phone number."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
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
                    <asp:Label ID="Label35" runat="server" Text="Telephone No.2:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPhoneNo2Isd" runat="server" Width="40px" CssClass="txtField"></asp:TextBox>
                    <asp:TextBox ID="txtPhoneNo2Std" runat="server" Width="40px" CssClass="txtField"></asp:TextBox>
                    <asp:TextBox ID="txtPhoneNo2" runat="server" Width="100px" CssClass="txtField"></asp:TextBox>
                    <asp:CompareValidator ID="txtPhoneNo2Isd_CompareValidator" ControlToValidate="txtPhoneNo2Isd"
                        runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value for ISD code."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                    <asp:CompareValidator ID="txtPhoneNo2Std_CompareValidator" ControlToValidate="txtPhoneNo2Std"
                        runat="server" Display="Dynamic" ErrorMessage=" <br />Please enter a numeric value for STD code."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                    <asp:CompareValidator ID="txtPhoneNo2_CompareValidator" ControlToValidate="txtPhoneNo2"
                        runat="server" Display="Dynamic" ErrorMessage=" <br />Please enter a numeric value for Phone number."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
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
                    <asp:Label ID="Label36" runat="server" Text="Fax:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtFaxIsd" runat="server" Width="40px" CssClass="txtField"></asp:TextBox>
                    <asp:TextBox ID="txtFaxStd" runat="server" Width="40px" CssClass="txtField"></asp:TextBox>
                    <asp:TextBox ID="txtFax" runat="server" Width="100px" CssClass="txtField"></asp:TextBox>
                    <asp:CompareValidator ID="txtFaxIsd_CompareValidator" ControlToValidate="txtFaxIsd"
                        runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value for ISD code."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                    <asp:CompareValidator ID="txtFaxStd_CompareValidator" ControlToValidate="txtFaxStd"
                        runat="server" Display="Dynamic" ErrorMessage=" <br />Please enter a numeric value for STD code."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                    <asp:CompareValidator ID="txtFax_CompareValidator" ControlToValidate="txtFax" runat="server"
                        Display="Dynamic" ErrorMessage=" <br />Please enter a numeric value for Fax number."
                        Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
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
                    <asp:Label ID="Label37" runat="server" Text="Email Id:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField" style="width: 30%"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                        ErrorMessage="Please enter a valid Email ID" Display="Dynamic" runat="server"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="revPCG"></asp:RegularExpressionValidator>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
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
                        <td class="leftField" width="25%">
                                <asp:Label ID="Label6" runat="server" Text="Alert Preferances:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                 <asp:Checkbox ID="chkmailn" runat="server" CssClass="txtField" Text="Via Mail"
                AutoPostBack="false"  Enabled = "true"/>
                &nbsp;
            
                 <asp:Checkbox ID="chksmsn" runat="server" CssClass="txtField" Text="Via SMS"
                AutoPostBack="false"  Enabled = "true"/>
                            </td>
                            <td>
                            &nbsp;
                            </td>
                            <td>
                            &nbsp;
                            </td>
                        </tr>
            
           
            
            
        </table>
    </div>
</div>
<table>
    <tr>
        <td colspan="4" class="SubmitCell">
            <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Update" CssClass="PCGButton"
                onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_EditCustomerNonIndividualProfile_btnEdit', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_EditCustomerNonIndividualProfile_btnEdit', 'S');" />
        </td>
    </tr>
</table>
