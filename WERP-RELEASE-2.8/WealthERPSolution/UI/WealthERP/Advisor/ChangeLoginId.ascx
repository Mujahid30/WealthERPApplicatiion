<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangeLoginId.ascx.cs"
    Inherits="WealthERP.ChangeLoginId" %>
<table style="width: 94%;" align="center" class="TableBackground">
    <tr>
        <td colspan="2" align="left">
            <asp:Label ID="Label1" runat="server" Text="Change LoginId" CssClass="HeaderTextBig"></asp:Label>
        </td>
    </tr>
   
    <tr>
        <td class="leftField" align="left">
            <asp:Label ID="Label2" runat="server" Text="Current Login Id :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" align="left">
            <asp:TextBox ID="txCurrentLoginId" runat="server" Width="200px" CssClass="txtField"></asp:TextBox>
            
             <asp:RequiredFieldValidator
                ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG" ErrorMessage="Current Login Id cannot be empty." ControlToValidate="txCurrentLoginId"></asp:RequiredFieldValidator>
            
        </td>
    </tr>
    <tr>
        <td class="leftField" align="left">
            <asp:Label ID="Label3" runat="server" Text="New Login Id :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" align="left">
            <asp:TextBox ID="txtNewLoginId" runat="server" Width="200px" CssClass="txtField"></asp:TextBox>
            
             <asp:CompareValidator ID="cvSameLoginId" CssClass="rfvPCG" runat="server" ErrorMessage="New Login Id cannot be same as Current Login Id."
                ControlToCompare="txCurrentLoginId" ControlToValidate="txtNewLoginId" Display="Dynamic" Operator="NotEqual"></asp:CompareValidator>
                
            <asp:RequiredFieldValidator ID="rfvTxtNewLoginId" runat="server" CssClass="rfvPCG" ErrorMessage="New Login Id cannot be empty."
                ControlToValidate="txtNewLoginId" Display="Dynamic"></asp:RequiredFieldValidator>
            
        </td>
       
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="Label4" runat="server" Text="Confirm New Login Id :" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:TextBox ID="txtConfirmLoginId" runat="server" Width="200px" CssClass="txtField"></asp:TextBox>
            
             <asp:CompareValidator ID="cvLoginId" CssClass="rfvPCG" runat="server" ErrorMessage="New Login Id and Confirm New Login Id do not much."
                ControlToCompare="txtNewLoginId"  ControlToValidate="txtConfirmLoginId"></asp:CompareValidator>
            
        </td>
       
    </tr>
     <tr>
        <td colspan="2">
        <asp:Label ID="lblStatusMessage" runat="server" Text="" CssClass="Error"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="left" colspan="3" class="SubmitCell">
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save " Width="60px"
                CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_ChangeLoginId_btnSave');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_ChangeLoginId_btnSave');" />
        </td>
    </tr>
</table>
