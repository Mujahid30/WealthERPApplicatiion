<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FamilyDetailsSpouse.ascx.cs"
    Inherits="WealthERP.Customer.FamilyDetailsSpouse" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<script src="../Scripts/tabber.js" type="text/javascript"></script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblView" runat="server" CssClass="HeaderTextBig" Text="New Customer"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table class="TableBackground" style="width: 100%">
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
            <asp:Label ID="lblCustomerSubType" runat="server" CssClass="FieldName" Text="Customer Sub Type:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlCustomerSubType" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlCustomerSubType"
                ErrorMessage="Please select a Customer Sub-Type" Operator="NotEqual" ValueToCompare="Select a Sub-Type"
                CssClass="cvPCG"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblName" runat="server" CssClass="FieldName" Text="Name(First/Middle/Last):"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtFirstName_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtFirstName" WatermarkText="FirstName">
            </cc1:TextBoxWatermarkExtender>
            <asp:TextBox ID="txtMiddleName" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtMiddleName_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtMiddleName" WatermarkText="MiddleName">
            </cc1:TextBoxWatermarkExtender>
            <asp:TextBox ID="txtLastName" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtLastName_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtLastName" WatermarkText="LastName">
            </cc1:TextBoxWatermarkExtender>
            <span id="spLastName" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvLastName" ControlToValidate="txtFirstName" ErrorMessage="Please enter the Last Name"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblDob" runat="server" CssClass="FieldName" Text="Date of Birth:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtDob" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="txtDob_CalendarExtender" runat="server" TargetControlID="txtDob"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <asp:CompareValidator ID="txtDob_CompareValidator" runat="server" Operator="LessThanEqual"
                ErrorMessage="Please select a valid date" Type="Date" ControlToValidate="txtDob"
                CssClass="cvPCG"></asp:CompareValidator>
            <cc1:TextBoxWatermarkExtender ID="txtDob_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtDob" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPanNum" runat="server" CssClass="FieldName" Text="PAN Number:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtPanNum" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
            <span id="spPanNum" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvPanNum" ControlToValidate="txtPanNum" ErrorMessage="Please enter a Pan Num"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
</table>
<div class="tabber">
    <div class="tabbertab">
        <h6>
            Office Address</h6>
        <table style="width: 100%; height: 197px;">
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
                <td class="rightField">
                    <asp:TextBox ID="txtOfcCompanyName" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label27" CssClass="FieldName" runat="server" Text="Line1(House No/Building):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtOfcAdrLine1" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label28" CssClass="FieldName" runat="server" Text="Line2(Street):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtOfcAdrLine2" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label29" CssClass="FieldName" runat="server" Text="Line3(Area):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtOfcAdrLine3" runat="server" CssClass="txtField"></asp:TextBox>
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
                    <asp:CompareValidator ID="cvOfcAdrPinCode" runat="server" ErrorMessage="</br>Enter a numeric value"
                        CssClass="rfvPCG" Type="Integer" ControlToValidate="txtOfcAdrPinCode" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label33" CssClass="FieldName" runat="server" Text="Country:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlOfcAdrCountry" runat="server" CssClass="cmbField">
                        <asp:ListItem>India</asp:ListItem>
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div class="tabbertab">
        <h6>
            Contact Details</h6>
        <table style="width: 100%; height: 155px;">
            <tr>
                <td colspan="4">
                    <asp:Label ID="Label35" runat="server" Text="Contact Details" CssClass="HeaderTextSmall"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label36" CssClass="FieldName" runat="server" Text="Telephone No.(Res):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtResPhoneNoIsd" runat="server" Width="30px" CssClass="txtField"
                        MaxLength="5">91</asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="</br>Enter a numeric value"
                        CssClass="cvPCG" Type="Integer" ControlToValidate="txtResPhoneNoIsd" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                    <asp:TextBox ID="txtResPhoneNoStd" runat="server" Width="30px" CssClass="txtField"
                        MaxLength="5"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="</br>Enter a numeric value"
                        CssClass="cvPCG" Type="Integer" ControlToValidate="txtResPhoneNoStd" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                    <asp:TextBox ID="txtResPhoneNo" runat="server" Width="90px" CssClass="txtField" MaxLength="9"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="</br>Enter a numeric value"
                        CssClass="cvPCG" Type="Integer" ControlToValidate="txtResPhoneNo" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label41" CssClass="FieldName" runat="server" Text="Fax(Res):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtResFaxIsd" runat="server" Width="30px" CssClass="txtField" MaxLength="5">91</asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="</br>Enter a numeric value"
                        CssClass="cvPCG" Type="Integer" ControlToValidate="txtResFaxIsd" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                    <asp:TextBox ID="txtResFaxStd" runat="server" Width="30px" CssClass="txtField" MaxLength="5"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="</br>Enter a numeric value"
                        CssClass="cvPCG" Type="Integer" ControlToValidate="txtResFaxStd" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                    <asp:TextBox ID="txtResFax" runat="server" Width="90px" CssClass="txtField" MaxLength="9"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="</br>Enter a numeric value"
                        CssClass="cvPCG" Type="Integer" ControlToValidate="txtResFax" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label37" CssClass="FieldName" runat="server" Text="Telephone No.(Off):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtOfcPhoneNoIsd" runat="server" Width="30px" CssClass="txtField"
                        MaxLength="5">91</asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator10" runat="server" ErrorMessage="</br>Enter a numeric value"
                        CssClass="cvPCG" Type="Integer" ControlToValidate="txtOfcPhoneNoIsd" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                    <asp:TextBox ID="txtOfcPhoneNoStd" runat="server" Width="30px" CssClass="txtField"
                        MaxLength="5"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator11" runat="server" ErrorMessage="</br>Enter a numeric value"
                        CssClass="cvPCG" Type="Integer" ControlToValidate="txtOfcPhoneNoStd" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                    <asp:TextBox ID="txtOfcPhoneNo" runat="server" Width="90px" CssClass="txtField" MaxLength="9"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator12" runat="server" ErrorMessage="</br>Enter a numeric value"
                        CssClass="cvPCG" Type="Integer" ControlToValidate="txtOfcPhoneNo" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label42" CssClass="FieldName" runat="server" Text="Fax(Off):"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtOfcFaxIsd" runat="server" Width="30px" CssClass="txtField" MaxLength="5">91</asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator13" runat="server" ErrorMessage="</br>Enter a numeric value"
                        CssClass="cvPCG" Type="Integer" ControlToValidate="txtOfcFaxIsd" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                    <asp:TextBox ID="txtOfcFaxStd" runat="server" Width="30px" CssClass="txtField" MaxLength="5"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator14" runat="server" ErrorMessage="</br>Enter a numeric value"
                        CssClass="cvPCG" Type="Integer" ControlToValidate="txtOfcFaxStd" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                    <asp:TextBox ID="txtOfcFax" runat="server" Width="90px" CssClass="txtField" MaxLength="9"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator15" runat="server" ErrorMessage="</br>Enter a numeric value"
                        CssClass="cvPCG" Type="Integer" ControlToValidate="txtOfcFax" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label38" CssClass="FieldName" runat="server" Text="Mobile1:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtMobile1" runat="server" CssClass="txtField" MaxLength="14"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="</br>Enter a numeric value"
                        CssClass="cvPCG" Type="Integer" ControlToValidate="txtMobile1" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label43" CssClass="FieldName" runat="server" Text="Mobile2:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtMobile2" runat="server" CssClass="txtField" MaxLength="14"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="</br>Enter a numeric value"
                        CssClass="cvPCG" Type="Integer" ControlToValidate="txtMobile2" Operator="DataTypeCheck"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblEmail" CssClass="FieldName" runat="server" Text="Email:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                        ErrorMessage="</br>Please enter a valid Email ID" Display="Dynamic" runat="server"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="revPCG"></asp:RegularExpressionValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label40" CssClass="FieldName" runat="server" Text="Alternate Email:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtAltEmail" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
</div>
<table class="TableBackground" style="width: 100%">
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="SubmitCell">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_FamilyDetailsSpouse_btnSubmit');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_FamilyDetailsSpouse_btnSubmit');"
                OnClick="btnSubmit_Click" />
            <asp:Button ID="btnAddDetails" runat="server" OnClick="btnAddDetails_Click" Text="Add Bank Details"
                CssClass="PCGLongButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_FamilyDetailsSpouse_btnAddDetails', 'L');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_FamilyDetailsSpouse_btnAddDetails', 'L');" />
        </td>
    </tr>
</table>
