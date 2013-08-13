<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddStaff.ascx.cs" Inherits="WealthERP.Advisor.AddStaff" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<style type="text/css">
    .table
    {
        border: 1px solid orange;
    }
    .leftLabel
    {
        text-align: right;
    }
    .rightData
    {
        text-align: left;
    }
    .divViewEdit
    {
        padding-right: 15px;
        width: 2%;
        float: right;
        text-align: right;
        cursor: hand;
    }
</style>

<script language="JavaScript" type="text/jscript">
    function ValidatereportingManager(source, arguments) {
        arguments.IsValid = false;

        var ddlReportingManager = document.getElementById("ddlReportingMgr").value;
        var minHierarchyRoleId = document.getElementById("hidMinHierarchyTitleId").value;
        alert(ddlReportingManager);
        alert(minHierarchyRoleId);
        if (minHierarchyRoleId == ddlReportingManager) {
            arguments.IsValid = true;
            
        } else if (minHierarchyRoleId != ddlReportingManager && ddlReportingManager == 0) {
        arguments.IsValid = false;

    }

    return;
    }
</script>


<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td colspan="6">
                    <div class="panel">
                        <p>
                            Note: Fields marked with a ' * ' are compulsory
                        </p>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div class="divPageHeading">
                        <table cellspacing="0" cellpadding="3" width="100%">
                            <tr>
                                <td align="left" style="width: 50%;">
                                    <asp:Label ID="lblTitle" runat="server" Text="Add Staff"></asp:Label>
                                </td>
                                <td align="right" style="width: 50%;">
                                    <asp:LinkButton ID="lnkEditStaff" Text="Edit" runat="server" CssClass="LinkButtons"
                                        ToolTip="Edit Staff Details" OnClick="lnkEditStaff_Click">
                                    </asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <asp:LinkButton ID="lnkAddNewStaff" Text="AddNew/Clear " runat="server" CssClass="LinkButtons"
                                        ToolTip="Add new New Staff" OnClick="lnkAddNewStaff_Click">
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr id="trSuccessMsg" runat="server" visible="false">
                <td colspan="6" align="center">
                    <div class="success-msg" id="divMsgSuccess" align="center" runat="server">
                    </div>
                </td>
            </tr>
            <tr id="tr3" runat="server" visible="true">
                <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        Staff Details
                    </div>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblName" runat="server" CssClass="FieldName" Text="First Name:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span1" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtFirstName" ErrorMessage="<br />Please Enter the Name"
                        Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="btnSubmit">
                    </asp:RequiredFieldValidator>
                    <%--<cc1:TextBoxWatermarkExtender ID="txtFirstName_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtFirstName" WatermarkText="Firstname">
            </cc1:TextBoxWatermarkExtender>--%>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblMiddleName" runat="server" CssClass="FieldName" Text="Middle Name:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMiddleName" runat="server" CssClass="txtField"></asp:TextBox>
                    &nbsp;&nbsp;
                    <%-- <cc1:TextBoxWatermarkExtender ID="txtMiddleName_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtMiddleName" WatermarkText="MiddleName">
            </cc1:TextBoxWatermarkExtender>--%>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblLastName" runat="server" CssClass="FieldName" Text="Last Name:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="txtField"></asp:TextBox>
                    <%-- <cc1:TextBoxWatermarkExtender ID="txtLastName_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtLastName" WatermarkText="LastName">
            </cc1:TextBoxWatermarkExtender>--%>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lb1StaffCode" runat="server" CssClass="FieldName" Text="Staff Code:"></asp:Label>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtStaffcode" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr id="trTeamTitle" runat="server">
                <td class="leftLabel">
                    <asp:Label ID="lblTeamList" runat="server" Text="Team:" CssClass="FieldName"></asp:Label>
                </td>
                <td class=" rightData">
                    <asp:DropDownList ID="ddlTeamList" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlTeamList_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <span id="Span4" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Select Team"
                        CssClass="rfvPCG" ControlToValidate="ddlTeamList" ValidationGroup="btnSubmit"
                        Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblTitleList" runat="server" Text="Title:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlTitleList" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlTitleList_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <span id="Span5" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Title"
                        CssClass="rfvPCG" ControlToValidate="ddlTitleList" ValidationGroup="btnSubmit"
                        Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
                <td colspan="2">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblChannel" runat="server" Text="Channel:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlChannel" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblBranch" runat="server" Text="Branch:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                    <span id="Span6" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Branch"
                        CssClass="rfvPCG" ControlToValidate="ddlBranch" ValidationGroup="btnSubmit" InitialValue="0"
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </td>
                <td colspan="2">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblReportingRole" runat="server" Text="Reporting to Role:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlRportingRole" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlRportingRole_SelectedIndexChanged">
                    </asp:DropDownList>
                    <span id="Span7" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Select Reporting Role"
                        CssClass="rfvPCG" ControlToValidate="ddlRportingRole" ValidationGroup="btnSubmit"
                        Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="Label2" runat="server" Text="Reporting Manager:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlReportingMgr" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                    <span id="Span8" class="spnRequiredField">*</span>
                   <%-- <asp:CustomValidator ControlToValidate="ddlReportingMgr" ID="cus" runat="server"
                        ErrorMessage="Please Select Reporting Manager" ClientValidationFunction="ValidatereportingManager" Display="Dynamic"
                        ValidationGroup="btnSubmit">
                    </asp:CustomValidator>--%>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Select Reporting Manager"
                        CssClass="rfvPCG" ControlToValidate="ddlReportingMgr" ValidationGroup="btnSubmit"
                        Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>--%>
                </td>
                <td colspan="2">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr id="tr1" runat="server" visible="true">
                <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        Contact Details
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="2">
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
                <td class="style2" colspan="5">
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
                <td colspan="5">
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
                <td colspan="5">
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
                <td colspan="5">
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
                <td class="rightField" colspan="5">
                    <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
                    <span id="Span3" class="spnRequiredField">*</span>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" ControlToValidate="txtMobileNumber"
                        Display="Dynamic" runat="server" CssClass="rfvPCG" ErrorMessage="Not acceptable format"
                        ValidationGroup="btnSubmit" ValidationExpression="^\d{10,10}$">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtMobileNumber"
                        ErrorMessage="Please enter a Contact Number" Display="Dynamic" runat="server"
                        ValidationGroup="btnSubmit" CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblEmail" runat="server" CssClass="FieldName" Text="Email Id :"></asp:Label>
                </td>
                <td class="rightField" colspan="5">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span2" class="spnRequiredField">*&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                    <asp:CheckBox ID="chkMailSend" Checked="false" runat="server" Text="Send Login info?"
                        CssClass="cmbField" />
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEmail"
                        ValidationGroup="btnSubmit" ErrorMessage="Please enter an Email ID" Display="Dynamic"
                        runat="server" CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                        ErrorMessage="Please enter a valid Email ID" Display="Dynamic" runat="server"
                        ValidationGroup="btnSubmit" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        CssClass="revPCG"></asp:RegularExpressionValidator>
                    <%--  <asp:Label ID="lblEmailDuplicate" runat="server" CssClass="Error" Text="Email Id already exists"></asp:Label>--%>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                </td>
                <td colspan="5" class="SubmitCell">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" ValidationGroup="btnSubmit"
                        onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AddRM_btnNext', 'S');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AddRM_btnNext', 'S');" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="PCGButton" ValidationGroup="btnSubmit"
                        onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AddRM_btnNext', 'S');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AddRM_btnNext', 'S');" OnClick="btnUpdate_Click" />
                </td>
            </tr>
        </table>
        <div>
            <asp:HiddenField ID="hidRMid" runat="server" />
            <asp:HiddenField ID="hidMinHierarchyTitleId" runat="server" />
        </div>
    </ContentTemplate>
    <Triggers>
        <%-- <asp:PostBackTrigger ControlID="imgexportButton" />--%>
    </Triggers>
</asp:UpdatePanel>
