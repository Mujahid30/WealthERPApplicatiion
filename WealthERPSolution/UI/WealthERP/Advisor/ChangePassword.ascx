<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.ascx.cs"
    Inherits="WealthERP.Advisor.ChangePassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript">
    function ValidatePassWords() {
        
    }

</script>

<asp:ScriptManager runat="server" ID="scriptManager1">
</asp:ScriptManager>

<style type="text/css">
    
    .TextCSS
    {
        color: White;
    }
    
    .barIndicatorBorder 
    {
        border:solid 1px #c0c0c0;
        width: 140px;
        color: White;
        font-weight: bold;
        font-size: small;
    }

    .barIndicator_poor 
    {
        background-color: Red;
    }

    .barIndicator_weak 
    {
        background-color: Fuchsia;
    }

    .barIndicator_good 
    {
        background-color: Green;
    }

    .barIndicator_strong 
    {
        background-color: Blue;
    }

    .barIndicator_excellent 
    {
        background-color: Lime;   
    }

    .textbox
    {
        border: solid 2px #cccccc;
        border-top: solid 2px #a0a0a0;
        
    }
</style>

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
            <br />
            <asp:RequiredFieldValidator ID="rqdcurrentPass" Visible="true" runat="server" Display="Dynamic" Font-Size="11px"
            ControlToValidate="txtCurrentPassword" ErrorMessage="Please enter a password" ValidationGroup="PassWordGroup"></asp:RequiredFieldValidator>
        </td>
        <td align="left">
            
        </td>
    </tr>
    <tr>
        <td class="leftField" >
            <asp:Label ID="Label2" runat="server" Text="New Password :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" align="left">
            <asp:TextBox ID="txtNewPassword" MaxLength="8" runat="server" ValidationGroup="PassWordRegularExpression" TextMode="Password" CssClass="txtField"></asp:TextBox>
            
            <cc1:PasswordStrength runat="server" ID="PasswordStrength1"
            TargetControlID="txtNewPassword" DisplayPosition="RightSide" MinimumSymbolCharacters="1" MinimumUpperCaseCharacters="1" PreferredPasswordLength="8"
            CalculationWeightings="50;20;20;10" RequiresUpperAndLowerCaseCharacters="true" TextStrengthDescriptions="Poor; Weak; Good; Strong; Excellent"
            StrengthIndicatorType="Text" HelpHandlePosition="BelowLeft" BarBorderCssClass="barIndicatorBorder" TextCssClass="TextCSS"
            StrengthStyles="barIndicator_poor; barIndicator_weak; barIndicator_good; barIndicator_strong; barIndicator_excellent">
            </cc1:PasswordStrength>
            
            <br />
            <asp:RegularExpressionValidator Font-Size="11px" style="display: list-item" ID="passWordRegularExrp" runat="server" ControlToValidate="txtNewPassword" 
            ErrorMessage="Wrong password.!!" Display="Dynamic" ValidationGroup="PassWordGroup"
            ValidationExpression="^.*(?=.{6,})(?=.*\d)(?=.*[a-z,A-Z]).*$"></asp:RegularExpressionValidator>
            
            
            <asp:RequiredFieldValidator ID="rqdNewPassWord" runat="server" Display="Dynamic"  Font-Size="11px"
            ControlToValidate="txtNewPassword" ErrorMessage="Please enter a password" ValidationGroup="PassWordGroup"></asp:RequiredFieldValidator>
         
           
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
            
            <br />
            <asp:RequiredFieldValidator ID="rqdConfirmPass" runat="server" Display="Dynamic"  Font-Size="11px"
            ControlToValidate="txtConfirmPassword" ErrorMessage="Please enter a password" ValidationGroup="PassWordGroup"></asp:RequiredFieldValidator>
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
            
            <asp:Button ID="btnSave" runat="server" ValidationGroup="PassWordGroup" OnClientClick="ValidatePassWords()" OnClick="btnSave_Click" Text="Save" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AddBranch_btnSignIn');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AddBranch_btnSignIn');" />
            
        </td>
    </tr>
</table>

<table>
    <tr>
        <td>
    
        </td>
    </tr>
    <tr>
        <td>
           <div id="passwordNote" style="font-size: 11px; color: Red;">
           <p>
             <span style="font-weight: bold">Note:</span><br />
             1: Password length - 6 to 8.<br />
             2: There should be 1 alpha character.<br />
             3: There should be 1 Numeric.<br />
             <br />
             <br />
             <span style="font-weight: bold">Password Examples:</span><br />
             Good Password: "ab$g2s6mn"<br />
             Bad Password : "abcdefg","123456",1223%^&6","avc1".
          </p>
           </div>
        </td>
    </tr>
</table>

<asp:HiddenField ID="hdnuname" runat="server" />