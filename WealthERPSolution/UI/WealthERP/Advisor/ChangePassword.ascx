<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.ascx.cs"
    Inherits="WealthERP.Advisor.ChangePassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<style type="text/css">
    .style1
    {
        height: 26px;
    }
</style>
<table style="width: 100%;" align="center" class="TableBackground">
    <tr>
        <td colspan="3" align="left">
            <asp:Label ID="Label4" runat="server" CssClass="HeaderTextBig" Text="Change Password"></asp:Label>
        </td>
    </tr>
    <tr>
        <td  align="left">
            
        </td>
        <td  align="left">
            
        </td>
        <td align="left">
            
        </td>
    </tr>
    <tr>
        <td class="leftField" >
            <asp:Label ID="Label1" runat="server" Text="Current Password :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" align="left">
            <asp:TextBox ID="txtCurrentPassword" runat="server" TextMode="Password" CssClass="txtField"></asp:TextBox>
        </td>
        <td align="left">
            
        </td>
    </tr>
    <tr>
        <td class="leftField" >
            <asp:Label ID="Label2" runat="server" Text="New Password :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" align="left">
            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="txtField"></asp:TextBox>
        </td>
        <td align="left">
            
        </td>
    </tr>
    <tr>
        <td class="leftField" >
            <asp:Label ID="Label3" runat="server" Text="Confirm Password" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" >
            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="txtField"></asp:TextBox>
        </td>
        <td>
            
        </td>
    </tr>
    <tr>
        <td  align="left">
            
        </td>
        <td  align="left">
            
        </td>
        <td>
            
        </td>
    </tr>
    <tr>
        <td class="SubmitCell" align="left" colspan="3" >
            
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AddBranch_btnSignIn');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AddBranch_btnSignIn');" />
            
        </td>
    </tr>
</table>
