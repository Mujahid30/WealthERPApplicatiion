<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FamilyDetailsChild.ascx.cs"
    Inherits="WealthERP.Customer.FamilyDetailsChild" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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

<table style="width: 100%; height: 186px;" class="TableBackground">
    <tr>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblCustomerType" runat="server" CssClass="FieldName" Text="Customer Type:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:RadioButton ID="rbtnIndividual" runat="server" CssClass="txtField" Text="Individual"
                GroupName="grpCustomerType" AutoPostBack="true" Checked ="true" OnCheckedChanged="rbtnIndividual_CheckedChanged" />
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
            
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlCustomerSubType" ValidationGroup="btnSubmit"
                ErrorMessage="Please select a Customer Sub-Type" Operator="NotEqual" ValueToCompare="Select a Sub-Type"
                CssClass="cvPCG"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblName" runat="server" CssClass="FieldName" Text="Name(First/Middle/Last):"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtField"></asp:TextBox>
            &nbsp;<cc1:TextBoxWatermarkExtender ID="txtFirstName_TextBoxWatermarkExtender" runat="server"
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
           
            <span id="spFirstName" class="spnRequiredField">*<br />
            </span>
            
            <asp:RequiredFieldValidator ID="rfvFirstName" ControlToValidate="txtFirstName"  ErrorMessage="Please enter the First Name"
                Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblDob" runat="server" CssClass="FieldName" Text="Date of Birth:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtDob" runat="server" CssClass="txtField"></asp:TextBox>
            <br />
            <cc1:CalendarExtender ID="txtDob_CalendarExtender" runat="server" TargetControlID="txtDob"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <asp:CompareValidator ID="txtDob_CompareValidator" runat="server" Operator="LessThanEqual"
                ErrorMessage="Please Select a valid date" Type="Date" ControlToValidate="txtDob"
                CssClass="cvPCG"></asp:CompareValidator>
            <cc1:TextBoxWatermarkExtender ID="txtDob_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtDob" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Gender:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:RadioButton ID="rbtnMale" runat="server" CssClass="txtField" GroupName="rbtnGender"
                Text="Male" />
            <asp:RadioButton ID="rbtnFemale" runat="server" CssClass="txtField" GroupName="rbtnGender"
                Text="Female" />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Email:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField"></asp:TextBox>
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                ErrorMessage="Please enter a valid Email ID" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
            <table class="TableBackground" style="width: 100%; height: 28px;">
                <tr>
                    <td class="SubmitCell">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="btnSubmit"
                            CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_FamilyDetailsChild_btnSubmit', 'S');"
                            onmouseout="javascript:ChangeButtonCss('out', 'ctrl_FamilyDetailsChild_btnSubmit', 'S');" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
