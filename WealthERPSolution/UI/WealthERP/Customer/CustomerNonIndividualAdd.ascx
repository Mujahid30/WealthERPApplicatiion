<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerNonIndividualAdd.ascx.cs"
    Inherits="WealthERP.Customer.BasicNonIndividualProfile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script src="../Scripts/tabber.js" type="text/javascript"></script>

<script type="text/javascript" language="javascript">
    function checkDate(sender, args) {

        var selectedDate = new Date();
        selectedDate = sender._selectedDate;

        var todayDate = new Date();
        var msg = "";

        if (selectedDate > todayDate) {
            sender._selectedDate = todayDate;
            sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            alert("Warning! - Date Cannot be in the future");
        }
    }
</script>

<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
<table class="TableBackground" width="100%">
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 25%">
            <asp:Label ID="lblProfilingDate" runat="server" CssClass="FieldName" Text="Date of Profiling:"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:TextBox ID="txtDateofProfiling" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
            <cc1:CalendarExtender ID="txtDateofProfiling_CalendarExtender" runat="server" TargetControlID="txtDateofProfiling">
            </cc1:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Salutation:"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:DropDownList ID="ddlSalutation" runat="server" CssClass="cmbField">
                <asp:ListItem>Mr.</asp:ListItem>
                <asp:ListItem>Mrs.</asp:ListItem>
                <asp:ListItem>Ms.</asp:ListItem>
                <%--<asp:ListItem>Other</asp:ListItem>--%>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCompanyName" runat="server" CssClass="FieldName" Text="Name of Company:"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="spCompanyName" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="rfvCompanyName" ControlToValidate="txtCompanyName"
                ErrorMessage="Please enter the Company Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="Customer Code:"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblRegistrationDate" runat="server" CssClass="FieldName" Text="Date Of Registration:"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:TextBox ID="txtDateofRegistration" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="txtDateofRegistration_CalendarExtender" runat="server"
                TargetControlID="txtDateofRegistration" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtDateofRegistration_TextBoxWatermarkExtender"
                runat="server" TargetControlID="txtDateofRegistration" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="Date of Commencement:"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:TextBox ID="txtDateofCommencement" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="txtDateofCommencement_CalendarExtender" runat="server"
                TargetControlID="txtDateofCommencement" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtDateofCommencement_TextBoxWatermarkExtender"
                runat="server" TargetControlID="txtDateofCommencement" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblRegistrationNum" runat="server" CssClass="FieldName" Text="Registration No with ROC-Registrar:"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:TextBox ID="txtRocRegistration" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label13" runat="server" CssClass="FieldName" Text="Place Of Registration:"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:TextBox ID="txtRegistrationPlace" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label14" runat="server" CssClass="FieldName" Text="Company Website:"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:TextBox ID="txtCompanyWebsite" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label15" runat="server" CssClass="FieldName" Text="First Name:"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtFirstName_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtFirstName" WatermarkText="FirstName">
            </cc1:TextBoxWatermarkExtender>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <label id="lbl1" class="FieldName">
                Middle Name:</label>
        </td>
        <td class="rightField" colspan="2">
            <asp:TextBox ID="txtMiddleName" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtMiddleName_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtMiddleName" WatermarkText="MiddleName">
            </cc1:TextBoxWatermarkExtender>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <label id="Label2" class="FieldName">
                Last Name:</label>
        </td>
        <td class="rightField" colspan="2">
            <asp:TextBox ID="txtLastName" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtLastName_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtLastName" WatermarkText="LastName">
            </cc1:TextBoxWatermarkExtender>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label16" runat="server" CssClass="FieldName" Text="Designation:"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:TextBox ID="txtDesignation" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPANNum" runat="server" CssClass="FieldName" Text="PAN Number:"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:TextBox ID="txtPanNumber" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
            <span id="spPanNumber" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="rfvPanNumber" ControlToValidate="txtPanNumber" ErrorMessage="Please enter a PAN Number"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label9" runat="server" CssClass="FieldName" Text="RM Name:"></asp:Label>
        </td>
        <td class="rightField" colspan="2">
            <asp:TextBox ID="txtRmName" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
</table>
<div class="tabber">
    <div class="tabbertab" id="divCorrAdr">
        <h6>
            Correspondence Address</h6>
        <table style="width: 100%;">
            <tr>
                <td colspan="4" class="HeaderCell">
                    <asp:Label ID="Label18" runat="server" Text="Correspondence Address" CssClass="HeaderTextSmall"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblAdrLine1" runat="server" Text="Line1(House No/Building):" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtCorrAdrLine1" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="spCorrAdrLine1" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvCorrAdrLine1" ControlToValidate="txtCorrAdrLine1"
                        ErrorMessage="<br />Please enter Line1 of Correspondence Address" Display="Dynamic"
                        runat="server" CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label20" runat="server" Text="Line2(Street):" CssClass="FieldName"></asp:Label>
                </td>
                <td class="style2" colspan="3">
                    <asp:TextBox ID="txtCorrAdrLine2" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label21" runat="server" Text="Line3(Area):" CssClass="FieldName"></asp:Label>
                </td>
                <td class="style2" colspan="3">
                    <asp:TextBox ID="txtCorrAdrLine3" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblAdrCity" runat="server" Text="City:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="style9">
                    <asp:TextBox ID="txtCorrAdrCity" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label23" runat="server" Text="State:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="style11">
                    <asp:DropDownList ID="ddlCorrAdrState" runat="server" CssClass="txtField">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblAdrPinCode" runat="server" Text="Pin Code:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="txtCorrAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label1" runat="server" Text="Country:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCorrAdrCountry" runat="server" CssClass="cmbField">
                        <asp:ListItem>India</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="SubmitCell" colspan="4">
                    <asp:Button ID="btnCorrNext" runat="server" Text="Next" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerNonIndividualAdd_btnCorrNext', 'S');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerNonIndividualAdd_btnCorrNext', 'S');"
                        CssClass="PCGButton" OnClick="btnCorrNext_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="tabbertab" id="divPermAdr">
        <h6>
            Permanent Address</h6>
        <table style="width: 100%">
            <tr>
                <td colspan="4" class="HeaderCell">
                    <asp:Label ID="Label26" runat="server" Text="Permanent Address" CssClass="HeaderTextSmall"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:CheckBox ID="chkCorrPerm" runat="server" CssClass="FieldName" Text="Same as Correspondence Address" />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label27" runat="server" Text="Line1(House No/Building):" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtPermAdrLine1" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label28" runat="server" Text="Line2(Street):" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtPermAdrLine2" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label29" runat="server" Text="Line3(Area):" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtPermAdrLine3" runat="server" CssClass="txtField"></asp:TextBox>
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
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label32" runat="server" Text="Pin Code:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPermAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label25" runat="server" Text="Country:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlPermAdrCountry" runat="server" CssClass="cmbField">
                        <asp:ListItem>India</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="SubmitCell" colspan="4">
                    <asp:Button ID="btnPermNext" runat="server" Text="Next" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerNonIndividualAdd_btnPermNext', 'S');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerNonIndividualAdd_btnPermNext', 'S');"
                        CssClass="PCGButton" OnClick="btnPermNext_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="tabbertab" id="divContactDetails">
        <h6>
            Contact Details</h6>
        <table style="width: 100%;">
            <tr>
                <td colspan="4" class="HeaderCell">
                    <asp:Label ID="Label33" runat="server" Text="Contact Details" CssClass="HeaderTextSmall"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblPhone1" runat="server" Text="Telephone 1:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtPhoneNo1Isd" runat="server" Width="40px" CssClass="txtField"
                        MaxLength="3">91</asp:TextBox>
                    <asp:TextBox ID="txtPhoneNo1Std" runat="server" Width="40px" CssClass="txtField"
                        MaxLength="4"></asp:TextBox>
                    <asp:TextBox ID="txtPhoneNo1" runat="server" Width="100px" CssClass="txtField" MaxLength="8"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label35" runat="server" Text="Telephone 2:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtPhoneNo2Isd" runat="server" Width="40px" CssClass="txtField"
                        MaxLength="2">91</asp:TextBox>
                    <asp:TextBox ID="txtPhoneNo2Std" runat="server" Width="40px" CssClass="txtField"
                        MaxLength="4"></asp:TextBox>
                    <asp:TextBox ID="txtPhoneNo2" runat="server" Width="100px" CssClass="txtField" MaxLength="8"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label36" runat="server" Text="Fax:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtFaxIsd" runat="server" Width="40px" CssClass="txtField" MaxLength="2">91</asp:TextBox>
                    <asp:TextBox ID="txtFaxStd" runat="server" Width="40px" CssClass="txtField" MaxLength="4"></asp:TextBox>
                    <asp:TextBox ID="txtFax" runat="server" Width="100px" CssClass="txtField" MaxLength="8"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label37" runat="server" Text="Email Id:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField"></asp:TextBox>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                        ErrorMessage="Please enter a valid Email ID" Display="Dynamic" runat="server"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="revPCG"></asp:RegularExpressionValidator>
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
        <td class="SubmitCell" colspan="3">
            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerNonIndividualAdd_btnSubmit', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerNonIndividualAdd_btnSubmit', 'S');"
                CssClass="PCGButton" />
            &nbsp;
            <asp:Button ID="btnAddBankDetails" runat="server" Text="Add Bank Details" CssClass="PCGMediumButton"
                onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerNonIndividualAdd_btnAddBankDetails', 'M');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerNonIndividualAdd_btnAddBankDetails', 'M');"
                OnClick="btnAddBankDetails_Click" />
        </td>
    </tr>
</table>
