<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditCustomerBankAccount.ascx.cs"
    Inherits="WealthERP.Customer.EditCustomerBankAccount" %>

<style type="text/css">
    .style1
    {
        width: 162px;
    }
    .style8
    {
        width: 170px;
    }
    .style3
    {
        width: 143px;
    }
    .style9
    {
        width: 162px;
        height: 16px;
    }
    .style10
    {
        height: 16px;
    }
    .style13
    {
        width: 176px;
    }
    .style12
    {
        width: 69px;
    }
    .style14
    {
        width: 162px;
        height: 22px;
    }
    .style15
    {
        width: 170px;
        height: 22px;
    }
    .style16
    {
        width: 143px;
        height: 22px;
    }
    .style17
    {
        height: 22px;
    }
</style>
<table cssclass="TableBackground" style="width:100%;">
      <tr>
            <td colspan="4" class="HeaderCell">
                <asp:Label ID="Label34" runat="server" Text="Edit Bank Details" CssClass="HeaderTextBig"></asp:Label>
                <hr />
            </td>
        </tr>
</table>
<table cssclass="TableBackground" width="100%">

 
    <tr>
        <td class="style14" align="right" valign="top">
            <asp:Label ID="lblAccountType" runat="server" CssClass="FieldName" Text="Account Type: "></asp:Label>
        </td>
        <td class="style15" align="left" style="width:30%;">
            <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Savings" Value="SV" ></asp:ListItem>
                <asp:ListItem Text="C.C." Value="CC" ></asp:ListItem>
                <asp:ListItem Text="Current" Value="CR" ></asp:ListItem>
                <asp:ListItem Text="F.C.N.R." Value="FR" ></asp:ListItem>
                <asp:ListItem Text="NRE" Value="NE" ></asp:ListItem>
                <asp:ListItem Text="NRO" Value="NO" ></asp:ListItem>
                <asp:ListItem Text="O.D." Value="OD" ></asp:ListItem>
                <asp:ListItem Text="Other" Value="OT" ></asp:ListItem>
                <asp:ListItem Text="To be categorized" Value="TBC" ></asp:ListItem>
            </asp:DropDownList>
             <span id="Span1" class="spnRequiredField">*</span>
                     <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlAccountType" Display="Dynamic"
                ValidationGroup="btnSubmit" ErrorMessage="<br />Please select a Account Type" Operator="NotEqual"
                ValueToCompare="Select" CssClass="cvPCG"></asp:CompareValidator>
        </td>
        <td class="style16" align="left">

        </td>
        <td class="style17">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1" align="right" valign="top">
            <asp:Label ID="Label28" runat="server" CssClass="FieldName" Text="Account Number: "></asp:Label>
        </td>
        <td class="style8" align="left" style="width:30%;">
            <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span2" class="spnRequiredField">*</span>
             <asp:RequiredFieldValidator ID="rfvAccountNumber" ControlToValidate="txtAccountNumber"
                ValidationGroup="btnSubmit" ErrorMessage="<br />Please Enter a Account Number" Display="Dynamic"
                runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
        
        <td class="style3" align="left">
            
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1" align="right" valign="top">
            <asp:Label ID="Label29" runat="server" Text="Mode of Operation: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="style8" align="left" style="width:40%;">
            <asp:DropDownList ID="ddlModeOfOperation" CssClass="cmbField" runat="server">
                <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Anyone or Survivor" Value="AS" ></asp:ListItem>
               <asp:ListItem Text="As Per Board Resolution" Value="BR" ></asp:ListItem>
                <asp:ListItem Text="Either or Survivor" Value="ES" ></asp:ListItem>
                <asp:ListItem Text="Former or Survivor" Value="FS" ></asp:ListItem>
               <asp:ListItem Text="Jointly" Value="JO" ></asp:ListItem>
                <asp:ListItem Text="Other" Value="OT" ></asp:ListItem>
                <asp:ListItem Text="Severaly" Value="SE" ></asp:ListItem>
                <asp:ListItem Text="Singly" Value="SI" ></asp:ListItem>
                <asp:ListItem Text="Self Only" Value="SO" ></asp:ListItem>
                <asp:ListItem Text="To be categorized" Value="TBC" ></asp:ListItem>
            </asp:DropDownList><span id="Span3" class="spnRequiredField">&nbsp;*</span>
              <asp:CompareValidator ID="CVddlModeOfOperation" runat="server" ControlToValidate="ddlModeOfOperation"
                ValidationGroup="btnSubmit" Display="Dynamic" ErrorMessage="<br />Please select a mode of holding" Operator="NotEqual"
                ValueToCompare="Select" CssClass="cvPCG"></asp:CompareValidator>
        </td>
        <td class="style3">
        
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1" align="right" valign="top"> 
            <asp:Label ID="Label30" runat="server" Text="Bank Name: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="style8" align="left" valign="top" style="width:40%;">
            <asp:TextBox ID="txtBankName" runat="server" CssClass="txtField" style="width:250px;"></asp:TextBox>
            <span id="Span4" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvBankName" ControlToValidate="txtBankName" ErrorMessage="<br />Please enter a Bank Name"
                Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
        </td>
        <td class="style3">
           
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1" align="right">
            <asp:Label ID="Label31" runat="server" Text="Branch Name: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="style8" align="left" valign="top" style="width:30%;">
            <asp:TextBox ID="txtBranchName" runat="server" CssClass="txtField" style="width:250px;"></asp:TextBox>
            <span id="Span5" class="spnRequiredField">*</span>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtBranchName" Display="Dynamic" ErrorMessage="<br />Please enter a Bank Name"
                 runat="server" CssClass="rfvPCG" ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
        </td>
        <td class="style3">
         
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
  <tr>
        <td class="style8">
            &nbsp;
        </td>
        <td class="style3">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    </table>
    
 <table cssclass="TableBackground" style="width:100%;">
      <tr>
            <td colspan="4" class="HeaderCell">
                <asp:Label ID="Label1" runat="server" Text="Branch Details" CssClass="HeaderTextSmall"></asp:Label>
                <hr />
            </td>
        </tr>
        
</table>
 <table cssclass="TableBackground"> 
 <tr>
        <td class="style8">
            &nbsp;
        </td>
        <td class="style3">
            &nbsp;
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1" align="right">
            <asp:Label ID="Label19" runat="server" Text="Line1(House No/Building):" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style8" align="left">
            <asp:TextBox ID="txtBankAdrLine1" runat="server" CssClass="txtField" style="width:250px;"></asp:TextBox>
        </td>
        <td class="style3">
            &nbsp;</td>
        <td class="style3">
            &nbsp;</td>
        <td class="style3">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1" align="right">
            <asp:Label ID="Label20" runat="server" Text="Line2(Street):" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style8" align="left">
            <asp:TextBox ID="txtBankAdrLine2" runat="server" CssClass="txtField" style="width:250px;"></asp:TextBox>
        </td>
        <td class="style3" align="left">
            &nbsp;</td>
        <td class="style3" align="left">
            &nbsp;</td>
        <td class="style3" align="left">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1" align="right">
            <asp:Label ID="Label21" runat="server" Text="Line3(Area):" CssClass="FieldName"></asp:Label>
        </td>
        <td class="style8" align="left">
            <asp:TextBox ID="txtBankAdrLine3" runat="server" CssClass="txtField" style="width:250px;"></asp:TextBox>
        </td>
        <td class="style3">
            &nbsp;</td>
        <td class="style3">
            &nbsp;</td>
        <td class="style3">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1" align="right">
            <asp:Label ID="Label22" runat="server" CssClass="FieldName" Text="City:"></asp:Label>
        </td>
        <td class="style8" align="left">
            <asp:TextBox ID="txtBankAdrCity" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="style3" align="right">
            &nbsp;</td>
        <td class="style3" align="right">
            &nbsp;</td>
        <td class="style3" align="right">
            <asp:Label ID="Label23" runat="server" CssClass="FieldName" Text="State:"></asp:Label>
        </td>
        <td align="left" class="style8">
            <asp:DropDownList ID="ddlBankAdrState" runat="server" CssClass="txtField">
               
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="style1" align="right" valign="top">
            <asp:Label ID="Label24" runat="server" Text="Pin Code: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="style8" align="left">
            <asp:TextBox ID="txtBankAdrPinCode" runat="server" MaxLength="6" CssClass="txtField"></asp:TextBox>
             <asp:CompareValidator ID="cvBankPinCode" runat="server" ErrorMessage="<br />Enter a numeric value"
                CssClass="rfvPCG" Type="Integer" ControlToValidate="txtBankAdrPinCode" ValidationGroup="btnSubmit" Operator="DataTypeCheck"
                Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="style3" align="right" valign="top">
            &nbsp;</td>
        <td class="style3" align="right" valign="top">
            &nbsp;</td>
        <td class="style3" align="right" valign="top">
            <asp:Label ID="Label25" runat="server" Text="Country: " CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" class="style8" valign="top">
            <asp:DropDownList ID="ddlBankAdrCountry" runat="server" CssClass="cmbField">
                <asp:ListItem>India</asp:ListItem>
                <asp:ListItem>USA</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="style1" align="right" valign="top">
            <asp:Label ID="Label32" runat="server" Text="MICR: " CssClass="FieldName"></asp:Label>
            
        </td>
        <!--MICR Validation-->
        <td class="style8" align="left" valign="top">
            <asp:TextBox ID="txtMicr" runat="server" CssClass="txtField" MaxLength="9"></asp:TextBox>
            <asp:CompareValidator ID="cvMicr" runat="server" ErrorMessage="<br />Enter a numeric value"
                CssClass="rfvPCG" Type="Integer" ValidationGroup="btnSubmit" ControlToValidate="txtMicr" Operator="DataTypeCheck"
                Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="style3" align="right" valign="top">
            &nbsp;</td>
        <td class="style3" align="right" valign="top">
            &nbsp;</td>
        <td class="style3" align="right" valign="top">
            <asp:Label ID="Label33" runat="server" Text="IFSC: " CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" valign="top">
            <asp:TextBox ID="txtIfsc" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style9">
        </td>
        <td class="style10" colspan="4" align="left">
        </td>
        <table style="width: 549px">
            <tr>
                <td class="style13" align="left">
                </td>
                <td class="style12" align="left">
                    &nbsp;
                </td>
                <td align="left">
                    <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Save" CssClass="PCGButton" ValidationGroup="btnSubmit"
                        onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_EditCustomerBankAccount_btnEdit', 'S');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_EditCustomerBankAccount_btnEdit', 'S');" />
                </td>
            </tr>
        </table>
        <td class="style11">
        </td>
    </tr>
    </table> 

