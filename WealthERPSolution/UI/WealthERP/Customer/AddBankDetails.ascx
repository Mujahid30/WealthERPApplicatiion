<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddBankDetails.ascx.cs"
    Inherits="WealthERP.Customer.AddBankDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table class="TableBackground" width="100%">
    <tr>
        <td colspan="4">
            <asp:Label ID="lblHeader" runat="server" Text="Add Bank Details" CssClass="HeaderTextBig"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="4" class="tdRequiredText">
            <label id="lbl" class="lblRequiredText">
                Note: Fields marked with ' * ' are mandatory</label>
        </td>
    </tr>
    <%--<tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="Add Bank Details"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>--%>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblAccountType" runat="server" CssClass="FieldName" Text="Account Type:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlAccountType"
                ValidationGroup="btnSubmit" ErrorMessage="<br />Please select a Account Type" Operator="NotEqual"
                ValueToCompare="Select" CssClass="cvPCG"></asp:CompareValidator>
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblAccountNumber" runat="server" CssClass="FieldName" Text="Account Number:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="spAccountNumber" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvAccountNumber" ControlToValidate="txtAccountNumber"
                ValidationGroup="btnSubmit" ErrorMessage="<br />Please enter a Account Number" Display="Dynamic"
                runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblModeOfOperation" runat="server" Text="Mode of Operation:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlModeOfOperation" CssClass="cmbField" runat="server">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlModeOfOperation"
                ValidationGroup="btnSubmit" ErrorMessage="<br />Please select a mode of holding" Operator="NotEqual"
                ValueToCompare="Select" CssClass="cvPCG"></asp:CompareValidator>
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblBankName" runat="server" Text="Bank Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtBankName" runat="server" CssClass="txtField" style="width:250px;"></asp:TextBox>
            <span id="spBankName" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvBankName" ControlToValidate="txtBankName" ErrorMessage="<br />Please enter a Bank Name"
                Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblBranchName" runat="server" Text="Branch Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtBranchName" runat="server" CssClass="txtField" style="width:250px;"></asp:TextBox>
            <span id="spBranchName" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvBranchName" ControlToValidate="txtBranchName"
                ValidationGroup="btnSubmit" ErrorMessage="<br />Please enter a Branch Name" Display="Dynamic"
                runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <%--<tr>
        <td class="leftField">
            <asp:Label ID="lblBalance" runat="server" Text="Balance:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtBalance" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>--%>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="Label26" runat="server" Text="Branch Details" CssClass="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblAdrLine1" runat="server" Text="Line1(House No/Building):" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtBankAdrLine1" runat="server" CssClass="txtField" style="width:250px;"></asp:TextBox>
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label20" runat="server" Text="Line2(Street):" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtBankAdrLine2" runat="server" CssClass="txtField" style="width:250px;"></asp:TextBox>
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label21" runat="server" Text="Line3(Area):" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtBankAdrLine3" runat="server" CssClass="txtField" style="width:250px;"></asp:TextBox>
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCity" runat="server" CssClass="FieldName" Text="City:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtBankAdrCity" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="leftField">
            <asp:Label ID="Label23" runat="server" CssClass="FieldName" Text="State:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlBankAdrState" runat="server" CssClass="txtField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPinCode" runat="server" Text="Pin Code:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtBankAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
            <asp:CompareValidator ID="cvBankPinCode" runat="server" ErrorMessage="<br />Enter a numeric value"
                CssClass="rfvPCG" Type="Integer" ControlToValidate="txtBankAdrPinCode" ValidationGroup="btnSubmit" Operator="DataTypeCheck"
                Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField">
            <asp:Label ID="Label25" runat="server" Text="Country:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlBankAdrCountry" runat="server" CssClass="cmbField">
                <asp:ListItem>India</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblMicr" runat="server" Text="MICR:" CssClass="FieldName"></asp:Label>
           
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtMicr" runat="server" CssClass="txtField" MaxLength="9"></asp:TextBox>
            <asp:CompareValidator ID="cvMicr" runat="server" ErrorMessage="<br />Enter a numeric value"
                CssClass="rfvPCG" Type="Integer" ValidationGroup="btnSubmit" ControlToValidate="txtMicr" Operator="DataTypeCheck"
                Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField">
            <asp:Label ID="lblIfsc" runat="server" Text="IFSC:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtIfsc" runat="server" CssClass="txtField" MaxLength="11"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>

</table>
<table class="TableBackground" width="60%">
    <tr>
     
        <td align="center">
            <asp:Button ID="btnNo" runat="server" Text="Submit" CssClass="PCGMediumButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AddBankDetails_btnNo','M');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AddBankDetails_btnNo','M');"
                ValidationGroup="btnSubmit" OnClick="btnNo_Click" />
        
            <asp:Button ID="btnYes" runat="server" Text="Submit and Addmore" CssClass="PCGLongButton"  onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AddBankDetails_btnYes','L');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AddBankDetails_btnYes','L');"
                ValidationGroup="btnSubmit" OnClick="btnYes_Click" />
        </td>
       
        <td>
            &nbsp;
        </td>
    </tr>
</table>
