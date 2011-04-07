<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerIndividualAdd.ascx.cs"
    Inherits="WealthERP.Customer.BasicIndividualProfile" EnableViewState="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--Javascript Calendar Controls - Required Files--%>

<script src="../Scripts/tabber.js" type="text/javascript"></script>

<script type="text/javascript" src="../Scripts/Calender/calendar.js"></script>

<script type="text/javascript" src="../Scripts/Calender/lang/calendar-en.js"></script>

<script type="text/javascript" src="../Scripts/Calender/calendar-setup.js"></script>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

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
            document.getElementById('<%= btnSubmit.ClientID %>').focus(); // to make the focus out of the textbox
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

<%--Javascript Calendar Controls - Required Files--%>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<%--<asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>--%>
<table class="TableBackground" width="100%">
    <tr>
        <td colspan="4" class="tdRequiredText">
            <label id="lbl" class="lblRequiredText">
                Note: Fields marked with ' * ' are compulsory</label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblProfilingDate" runat="server" CssClass="FieldName" Text="Date of Profiling:"></asp:Label>
        </td>
        <td class="rightField" width="75%">
            <asp:TextBox ID="txtProfilingDate" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
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
            </asp:DropDownList>
           <%-- <span id="Span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cmpddlSalutation" runat="server" ControlToValidate="ddlSalutation"
                ErrorMessage="Please select a Salutation for customer" Operator="NotEqual" ValueToCompare="Select a Salutation"
                CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>--%>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblName" runat="server" CssClass="FieldName" Text="Name(First/Middle/Last):"></asp:Label>
        </td>
        <td class="rightField" width="75%">
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span1" class="spnRequiredField">*</span>
            <cc1:TextBoxWatermarkExtender ID="txtFirstName_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtFirstName" WatermarkText="FirstName">
            </cc1:TextBoxWatermarkExtender>
            <asp:TextBox ID="txtMiddleName" runat="server" CssClass="txtField"></asp:TextBox>
            &nbsp;&nbsp;
            <cc1:TextBoxWatermarkExtender ID="txtMiddleName_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtMiddleName" WatermarkText="MiddleName">
            </cc1:TextBoxWatermarkExtender>
            <asp:TextBox ID="txtLastName" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtLastName_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtLastName" WatermarkText="LastName">
            </cc1:TextBoxWatermarkExtender>
            <br />
            <asp:RequiredFieldValidator ID="rfvLastName" ControlToValidate="txtFirstName" ErrorMessage="Please enter the First Name"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trGuardianName" runat="server">
        <td class="leftField">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Guardian Name(First/Middle/Last):"></asp:Label>
        </td>
        <td class="rightField" width="75%">
            <asp:TextBox ID="txtGuardianFirstName" runat="server" CssClass="txtField"></asp:TextBox>
            &nbsp;&nbsp;
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtGuardianFirstName"
                WatermarkText="FirstName">
            </cc1:TextBoxWatermarkExtender>
            <asp:TextBox ID="txtGuardianMiddleName" runat="server" CssClass="txtField"></asp:TextBox>
            &nbsp;&nbsp;
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtGuardianMiddleName"
                WatermarkText="MiddleName">
            </cc1:TextBoxWatermarkExtender>
            <asp:TextBox ID="txtGuardianLastName" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtGuardianLastName"
                WatermarkText="LastName">
            </cc1:TextBoxWatermarkExtender>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCustCode" runat="server" CssClass="FieldName" Text="Customer Code:"></asp:Label>
        </td>
        <td class="rightField" width="75%">
            <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblGender" runat="server" CssClass="FieldName" Text="Gender:"></asp:Label>
        </td>
        <td class="rightField" width="75%">
            <asp:RadioButton ID="rbtnMale" runat="server" CssClass="txtField" Text="Male" GroupName="rbtnGender" />
            <asp:RadioButton ID="rbtnFemale" runat="server" CssClass="txtField" Text="Female"
                GroupName="rbtnGender" />
        </td>
    </tr>
    
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPanNum" runat="server" CssClass="FieldName" Text="PAN Number:"></asp:Label>
        </td>
        <td class="rightField" width="75%">
            <asp:TextBox ID="txtPanNumber" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
             &nbsp;
             &nbsp;
            <asp:Checkbox ID="chkdummypan" runat="server" CssClass="txtField" Text="Dummy PAN"
                AutoPostBack="true" />
            <br />
            <asp:RequiredFieldValidator ID="rfvPanNumber" ControlToValidate="txtPanNumber" ErrorMessage="Please enter a PAN Number"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblRMName" runat="server" CssClass="FieldName" Text="RM Name:"></asp:Label>
        </td>
        <td class="rightField" width="75%">
            <asp:TextBox ID="txtRMName" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
            <br />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div class="tabber" id="divTab" runat="server" style="width: 100%">
                <div class="tabbertab" id="divCorrAdr">
                    <h6>
                        Correspondence Address</h6>
                    <table width="100%">
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="Label10" CssClass="HeaderTextSmall" runat="server" Text="Correspondence Address"></asp:Label>
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
                                <asp:Label ID="lblAdrLine1" CssClass="FieldName" runat="server" Text="Line1(House No./Building):"></asp:Label>
                            </td>
                            <td class="rightField" colspan="3">
                                <asp:TextBox ID="txtCorrAdrLine1" runat="server" CssClass="txtField"></asp:TextBox>
                               <%-- <span id="Span3" class="spnRequiredField">*</span>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCorrAdrLine1"
                                    ErrorMessage="Please enter Line1 of Correspondence Address" Display="Dynamic"
                                    runat="server" CssClass="rfvPCG">
                                </asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label12" CssClass="FieldName" runat="server" Text="Line2(Street):"></asp:Label>
                            </td>
                            <td class="rightField" colspan="3">
                                <asp:TextBox ID="txtCorrAdrLine2" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label13" CssClass="FieldName" runat="server" Text="Line3(Area):"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtCorrAdrLine3" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                            <td class="leftField" width="25%">
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
                                    ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="lblAdrCity" CssClass="FieldName" runat="server" Text="City:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:TextBox ID="txtCorrAdrCity" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label16" CssClass="FieldName" runat="server" Text="State:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:DropDownList ID="ddlCorrAdrState" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="lblAdrPinCode" CssClass="FieldName" runat="server" Text="Pin Code:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:TextBox ID="txtCorrAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
                                <asp:CompareValidator ID="txtCorrAdrPinCode_comparevalidator" ControlToValidate="txtCorrAdrPinCode"
                                    runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value"
                                    Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label17" CssClass="FieldName" runat="server" Text="Country:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:DropDownList ID="ddlCorrAdrCountry" runat="server" CssClass="cmbField">
                                    <asp:ListItem>India</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                <asp:Button ID="btnCorrNext" runat="server" Text="Next" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerIndividualAdd_btnCorrNext');"
                                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerIndividualAdd_btnCorrNext');"
                                    OnClick="btnCorrNext_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tabbertab" id="divPermAdr">
                    <h6>
                        Permanent Address</h6>
                    <table width="100%">
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="Label18" CssClass="HeaderTextSmall" runat="server" Text="Permanent Address"></asp:Label>
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                &nbsp
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:CheckBox ID="chkCorrPerm" runat="server" CssClass="FieldName" Text="Same as Correspondence Address" />
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label19" CssClass="FieldName" runat="server" Text="Line1(House No./Building):"></asp:Label>
                            </td>
                            <td class="rightField" colspan="3">
                                <asp:TextBox ID="txtPermAdrLine1" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label20" CssClass="FieldName" runat="server" Text="Line2(Street):"></asp:Label>
                            </td>
                            <td class="rightField" colspan="3">
                                <asp:TextBox ID="txtPermAdrLine2" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label21" CssClass="FieldName" runat="server" Text="Line3(Area):"></asp:Label>
                            </td>
                            <td class="rightField" colspan="3">
                                <asp:TextBox ID="txtPermAdrLine3" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label22" CssClass="FieldName" runat="server" Text="City:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:TextBox ID="txtPermAdrCity" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label23" CssClass="FieldName" runat="server" Text="State:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:DropDownList ID="ddlPermAdrState" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label24" CssClass="FieldName" runat="server" Text="Pin Code:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:TextBox ID="txtPermAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
                                <asp:CompareValidator ID="txtPermAdrPinCode_CompareValidator" ControlToValidate="txtPermAdrPinCode"
                                    runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value"
                                    Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label25" CssClass="FieldName" runat="server" Text="Country:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:DropDownList ID="ddlPermAdrCountry" runat="server" CssClass="cmbField">
                                    <asp:ListItem>India</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                <asp:Button ID="btnPermNext" runat="server" Text="Next" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerIndividualAdd_btnPermNext');"
                                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerIndividualAdd_btnPermNext');"
                                    OnClick="btnPermNext_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tabbertab" id="divOfcAdr">
                    <h6>
                        Office Address</h6>
                    <table width="100%">
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="Label26" CssClass="HeaderTextSmall" runat="server" Text="Office Address"></asp:Label>
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
                                <asp:Label ID="Label34" CssClass="FieldName" runat="server" Text="Company Name:"></asp:Label>
                            </td>
                            <td class="rightField" colspan="3">
                                <asp:TextBox ID="txtOfcCompanyName" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label27" CssClass="FieldName" runat="server" Text="Line1(House No./Building):"></asp:Label>
                            </td>
                            <td class="rightField" colspan="3">
                                <asp:TextBox ID="txtOfcAdrLine1" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label28" CssClass="FieldName" runat="server" Text="Line2(Street):"></asp:Label>
                            </td>
                            <td class="rightField" colspan="3">
                                <asp:TextBox ID="txtOfcAdrLine2" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label29" CssClass="FieldName" runat="server" Text="Line3(Area):"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtOfcAdrLine3" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                            <td class="leftField" width="25%">
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
                                    ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label30" CssClass="FieldName" runat="server" Text="City:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:TextBox ID="txtOfcAdrCity" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label31" CssClass="FieldName" runat="server" Text="State:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:DropDownList ID="ddlOfcAdrState" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label32" CssClass="FieldName" runat="server" Text="Pin Code:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:TextBox ID="txtOfcAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
                                <asp:CompareValidator ID="txtOfcAdrPinCode_CompareValidator" ControlToValidate="txtOfcAdrPinCode"
                                    runat="server" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value"
                                    Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label33" CssClass="FieldName" runat="server" Text="Country:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:DropDownList ID="ddlOfcAdrCountry" runat="server" CssClass="cmbField">
                                    <asp:ListItem>India</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                <asp:Button ID="btnOfcNext" runat="server" Text="Next" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerIndividualAdd_btnOfcNext');"
                                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerIndividualAdd_btnOfcNext');"
                                    OnClick="btnOfcNext_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tabbertab" id="divContactDetails">
                    <h6>
                        Contact Details</h6>
                    <table width="100%">
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
                            <td class="rightField" width="25%">
                                <asp:TextBox ID="txtResPhoneNoIsd" runat="server" Width="40px" CssClass="txtField"
                                    MaxLength="2">91</asp:TextBox>
                                <asp:TextBox ID="txtResPhoneNoStd" runat="server" Width="40px" CssClass="txtField"
                                    MaxLength="3"></asp:TextBox>
                                <asp:TextBox ID="txtResPhoneNo" runat="server" Width="80px" CssClass="txtField" MaxLength="8"></asp:TextBox>
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
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label41" CssClass="FieldName" runat="server" Text="Fax(Res):"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:TextBox ID="txtResFaxIsd" runat="server" Width="40px" CssClass="txtField" MaxLength="2">91</asp:TextBox>
                                <asp:TextBox ID="txtResFaxStd" runat="server" Width="40px" CssClass="txtField" MaxLength="3"></asp:TextBox>
                                <asp:TextBox ID="txtResFax" runat="server" Width="80px" CssClass="txtField" MaxLength="8"></asp:TextBox>
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
                            <td class="leftField" width="25%">
                                <asp:Label ID="lblOfcPhone" CssClass="FieldName" runat="server" Text="Telephone No.(Off):"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:TextBox ID="txtOfcPhoneNoIsd" runat="server" Width="40px" CssClass="txtField"
                                    MaxLength="2">91</asp:TextBox>
                                <asp:TextBox ID="txtOfcPhoneNoStd" runat="server" Width="40px" CssClass="txtField"
                                    MaxLength="3"></asp:TextBox>
                                <asp:TextBox ID="txtOfcPhoneNo" runat="server" Width="80px" CssClass="txtField" MaxLength="8"></asp:TextBox>
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
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label42" CssClass="FieldName" runat="server" Text="Fax(Off):"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:TextBox ID="txtOfcFaxIsd" runat="server" Width="40px" CssClass="txtField" MaxLength="2">91</asp:TextBox>
                                <asp:TextBox ID="txtOfcFaxStd" runat="server" Width="40px" CssClass="txtField" MaxLength="3"></asp:TextBox>
                                <asp:TextBox ID="txtOfcFax" runat="server" Width="80px" CssClass="txtField" MaxLength="8"></asp:TextBox>
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
                            <td class="leftField" width="25%">
                                <asp:Label ID="lblMobile1" CssClass="FieldName" runat="server" Text="Mobile1:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:TextBox ID="txtMobile1" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
                                <asp:CompareValidator ID="txtMobile1_CompareValidator" ControlToValidate="txtMobile1"
                                    runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for Mobile Number."
                                    Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label43" CssClass="FieldName" runat="server" Text="Mobile2:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:TextBox ID="txtMobile2" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
                                <asp:CompareValidator ID="txtMobile2_CompareValidator" ControlToValidate="txtMobile2"
                                    runat="server" Display="Dynamic" ErrorMessage="<br /> Please enter a numeric value for Mobile Number."
                                    Type="Integer" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="lblEmail" CssClass="FieldName" runat="server" Text="Email:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                                    ErrorMessage="<br />Please enter a valid Email ID" Display="Dynamic" runat="server"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="revPCG"></asp:RegularExpressionValidator>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label40" CssClass="FieldName" runat="server" Text="Alternate Email:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:TextBox ID="txtAltEmail" runat="server" CssClass="txtField"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="txtAltEmail_RegularExpressionValidator" ControlToValidate="txtAltEmail"
                                    ErrorMessage="<br />Please enter a valid Email ID" Display="Dynamic" runat="server"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="revPCG"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                <asp:Button ID="btnContactNext" runat="server" Text="Next" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerIndividualAdd_btnContactNext');"
                                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerIndividualAdd_btnContactNext');"
                                    OnClick="btnContactNext_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tabbertab" id="divAddInfo">
                    <h6>
                        Additional Information</h6>
                    <table width="100%">
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="Label44" CssClass="HeaderTextSmall" runat="server" Text="Additional Information"></asp:Label>
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
                                <asp:Label ID="Label45" CssClass="FieldName" runat="server" Text="Occupation:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:DropDownList ID="ddlOccupation" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label46" CssClass="FieldName" runat="server" Text="Qualification:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:DropDownList ID="ddlQualification" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label47" CssClass="FieldName" runat="server" Text="Marital Status:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="cmbField" OnChange="OnMaritalStatusChange(this)">
                                </asp:DropDownList>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label48" CssClass="FieldName" runat="server" Text="Nationality:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:DropDownList ID="ddlNationality" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label2" CssClass="FieldName" runat="server" Text="Marriage Date:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:TextBox ID="txtMarriageDate" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
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
                            <td class="leftField" width="25%">
                                <asp:Label ID="lblRBIRefNum" CssClass="FieldName" runat="server" Text="RBI Reference No:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:TextBox ID="txtRBIRefNo" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="lblRBIApprovalDate" CssClass="FieldName" runat="server" Text="RBI Approval Date:"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:TextBox ID="txtRBIApprovalDate" runat="server" CssClass="txtField"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtRBIApprovalDate_CalendarExtender" runat="server" TargetControlID="txtRBIApprovalDate"
                                    Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
                                </cc1:CalendarExtender>
                                <cc1:TextBoxWatermarkExtender ID="txtRBIApprovalDate_TextBoxWatermarkExtender" runat="server"
                                    TargetControlID="txtRBIApprovalDate" WatermarkText="dd/mm/yyyy">
                                </cc1:TextBoxWatermarkExtender>
                            </td>
                        </tr>
                        <tr>
        
        <td class="leftField" width="25%">
                                <asp:Label ID="Label9" runat="server" Text="Alert Preferences:" CssClass="FieldName"></asp:Label>
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
            <asp:Label ID="lblDob" runat="server" CssClass="FieldName" Text="Date of Birth:"></asp:Label>
        </td>
        <td class="rightField" >
            <div class="dvInLine">
                <asp:TextBox ID="txtDob" runat="server" CssClass="txtField"></asp:TextBox>
                <%--<img alt="Calendar" src="../CSS/Images/calendar3.jpg" id="imgCalendar" />--%>
                <cc1:CalendarExtender ID="txtDob_CalendarExtender" runat="server" TargetControlID="txtDob"
                    Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
                </cc1:CalendarExtender>
                <cc1:TextBoxWatermarkExtender ID="txtDob_TextBoxWatermarkExtender" runat="server"
                    TargetControlID="txtDob" WatermarkText="dd/mm/yyyy">
                </cc1:TextBoxWatermarkExtender>
            </div>
        </td>
    </tr>
                    </table>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td class="SubmitCell" colspan="2">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerIndividualAdd_btnSubmit');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerIndividualAdd_btnSubmit', 'S');"
                OnClick="btnSubmit_Click" />
            &nbsp &nbsp
            <asp:Button ID="btnAddBankDetails" runat="server" CssClass="PCGMediumButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerIndividualAdd_btnAddBankDetails');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerIndividualAdd_btnAddBankDetails', 'M');"
                Text="Add Bank Details" OnClick="btnAddBankDetails_Click" />
        </td>
    </tr>
</table>
<%--  </ContentTemplate>
</asp:UpdatePanel>--%>
