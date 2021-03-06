﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerType.ascx.cs"
    Inherits="WealthERP.Customer.CustomerType" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--Javascript Calendar Controls - Required Files--%>

<script type="text/javascript" src="../Scripts/Calender/calendar.js"></script>

<script type="text/javascript" src="../Scripts/Calender/lang/calendar-en.js"></script>

<script type="text/javascript" src="../Scripts/Calender/calendar-setup.js"></script>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<link href="../Scripts/Calender/skins/aqua/theme.css" rel="stylesheet" type="text/css" />
<%--Javascript Calendar Controls - Required Files--%>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<script type="text/javascript" language="javascript">
    function ShowHideSalulation(value) {
        alert(value);
        if (value == 'rbtnNonIndividual') {
            document.getElementById("<%= trSalutation.ClientID %>").style.display = 'none';
        }
        else {
            document.getElementById("<%= trSalutation.ClientID %>").style.display = 'block';
        }
    }
    function ShowSubmitAndSave() {
        var check = document.getElementById("<%= chkRealInvestor.ClientID %>").checked;
        if (check) {
            document.getElementById("<%=btnCustomerProfile.ClientID %>").style.visibility = 'visible';
        }
        else {
            document.getElementById("<%=btnCustomerProfile.ClientID %>").style.visibility = 'hidden';
        }
    }
</script>

<%--<asp:UpdatePanel ID="upPnl" runat="server">
    <ContentTemplate>--%>
<style type="text/css">
    .style1
    {
        height: 49px;
    }
</style>
<table width="100%" class="TableBackground">
    <tr>
        <td colspan="3">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Add Customer
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<%--<table width="100%" class="TableBackground">
    <tr id="trSumbitSuccess" runat="server" visible="true">
        <td align="center" colspan="2">
            <div class="success-msg" id="divMsgSuccess" runat="server" align="center">
                Customer Added Sucessfully
            </div>
        </td>
    </tr>
</table>--%>
<table class="TableBackground" style="width: 100%">
    <tr id="trBtnSaveMsg" runat="server" visible="false">
        <td class="leftField">
        </td>
        <td>
            <asp:Label ID="lblbtnSaveMsg" runat="server" Text="Customer Added Successfully" CssClass="FieldName"
                Font-Size="Medium"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
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
    <tr id="trBranchlist" runat="server" visible="false">
        <td class="leftField">
            <asp:Label ID="lblBranchName" runat="server" CssClass="FieldName" Text="Branch Name:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlAdviserBranchList" AutoPostBack="true" runat="server" CssClass="cmbField"
                OnSelectedIndexChanged="ddlAdviserBranchList_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <br />
            <asp:CompareValidator ID="ddlAdviserBranchList_CompareValidator2" runat="server"
                ControlToValidate="ddlAdviserBranchList" ErrorMessage="Please select a Branch"
                Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic">
            </asp:CompareValidator>
        </td>
    </tr>
    <tr id="trRMlist" runat="server" visible="false">
        <td class="leftField">
            <asp:Label ID="lblRMName" runat="server" CssClass="FieldName" Text="Select RM:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlAdviseRMList" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span8" class="spnRequiredField">*</span>
            <br />
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlAdviseRMList"
                ErrorMessage=" " Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG"
                Display="Dynamic">
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
            <span id="Span1" class="spnRequiredField">*</span> &nbsp;
            <asp:CheckBox ID="chkRealInvestor" runat="server" CssClass="txtField" Text="Investor"
                AutoPostBack="false" Checked="true" onclick="javascript:ShowSubmitAndSave();" />
            <br />
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlCustomerSubType"
                ErrorMessage="Please select a Customer Sub-Type" Operator="NotEqual" ValueToCompare="Select"
                CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblClientCode" runat="server" CssClass="FieldName" Text="Client Code:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtClientCode" runat="server"   CssClass="txtField" MaxLength="15"></asp:TextBox>
            
            <%-- <span id="Span5" class="spnRequiredField">*</span> &nbsp;
            <br />--%>
            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtClientCode"
                ErrorMessage="Please enter a client code" Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPanNum" runat="server" CssClass="FieldName" Text="PAN Number:"></asp:Label>
        </td>
        <td class="rightField" width="75%">
            <asp:TextBox ID="txtPanNumber" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
            <span id="Span6" class="spnRequiredField">*</span> &nbsp;
            <asp:CheckBox ID="chkdummypan" runat="server" Visible="false" CssClass="txtField" Text="Dummy PAN"
                AutoPostBack="true" />
            <br />
            <asp:RequiredFieldValidator ID="rfvPanNumber" ControlToValidate="txtPanNumber" ErrorMessage="Please enter a PAN Number"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revPan" runat="server" Display="Dynamic" CssClass="rfvPCG"
                ErrorMessage="Please check PAN Format" ControlToValidate="txtPanNumber" ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}">
            </asp:RegularExpressionValidator>
            <asp:Label ID="lblPanDuplicate" runat="server" CssClass="Error" Text="PAN Number already exists"></asp:Label>
        </td>
    </tr>
    <%--    <tr>
        <td class="leftField">
            <asp:Label ID="lblAssetInterest" runat="server" CssClass="FieldName" Text="Asset Interest:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlAssetInterest" CssClass="cmbField" runat="server">
                <asp:ListItem>Select an Asset Interest</asp:ListItem>
                <asp:ListItem>MF</asp:ListItem>
                <asp:ListItem>Equity</asp:ListItem>
                <asp:ListItem>Both</asp:ListItem>
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlAssetInterest"
                ErrorMessage="Please select an Asset Interest" Operator="NotEqual" ValueToCompare="Select an Asset Interest"
                CssClass="cvPCG"></asp:CompareValidator>
        </td>
    </tr>--%>
    <tr id="trSalutation" runat="server">
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
                <asp:ListItem>Dr.</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trIndividualName" runat="server">
        <td class="leftField">
            <asp:Label ID="lblName" runat="server" CssClass="FieldName" Text="Name:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtFirstName" runat="server" MaxLength="75" Style="width: 30%" CssClass="txtField"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtFirstName_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtFirstName" WatermarkText="FirstName">
            </cc1:TextBoxWatermarkExtender>
            <span id="Span7" class="spnRequiredField">*</span>
            <asp:TextBox ID="txtMiddleName" runat="server" MaxLength="25" Style="width: 30%"
                CssClass="txtField"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtMiddleName_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtMiddleName" WatermarkText="MiddleName">
            </cc1:TextBoxWatermarkExtender>
            <asp:TextBox ID="txtLastName" runat="server" MaxLength="75" Style="width: 30%" CssClass="txtField"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtLastName_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="txtLastName" WatermarkText="LastName">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtFirstName"
                ErrorMessage="<br />Please enter the First Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trNonIndividualName" runat="server">
        <td class="leftField">
            <asp:Label ID="lblCompanyName" runat="server" CssClass="FieldName" Text="Company Name:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtCompanyName" CssClass="txtField" Style="width: 30%" runat="server"></asp:TextBox>
            <span id="Span4" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCompanyName"
                ErrorMessage="Please enter the Company Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblEmail" runat="server" CssClass="FieldName" Text="Email Id:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtEmail" runat="server" Style="width: 30%" CssClass="txtField"></asp:TextBox>
            <!--<span id="Span5" class="spnRequiredField">*</span>
            <br />-->
            <%--         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtEmail"
                ErrorMessage="Please enter an Email Id" Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                ErrorMessage="Please enter a valid Email ID" Display="Dynamic" runat="server"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Mobile No:"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
            <span id="Span3" class="spnRequiredField">*</span>
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" ControlToValidate="txtMobileNumber"
                Display="Dynamic" runat="server" CssClass="rfvPCG" ErrorMessage="Not acceptable format"
                ValidationExpression="^\d{10,10}$">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtMobileNumber"
                ErrorMessage="Please enter a Contact Number" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
</table>
<table class="TableBackground" style="width: 100%">
    <tr>
        <td>
            &nbsp;
        </td>
        <td class="SubmitCell">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerType_btnSubmit', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerType_btnSubmit', 'S');" /><%----%>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCustomerProfile" runat="server" Text="Save and Add Details" OnClick="btnCustomerProfile_Click"
                CssClass="PCGLongButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerType_btnCustomerProfile', 'L');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerType_btnCustomerProfile', 'L');" />
        </td>
    </tr>
</table>
<%--</ContentTemplate> </asp:UpdatePanel>--%>