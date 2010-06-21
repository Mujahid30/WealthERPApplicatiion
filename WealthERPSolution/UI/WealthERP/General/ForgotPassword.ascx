<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.ascx.cs"
    Inherits="WealthERP.General.ForgotPassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<table style="width: 100%;" align="center" class="TableBackground">
    <tr>
        <td colspan="3" align="left">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Forgot Password"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr id="trLogin" runat="server">
        <td width="25%" class="leftField">
            <asp:Label ID="lblLoginId" runat="server" Text="Login Id:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtLoginId" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td>
        </td>
    </tr>
    <tr id="trEmail" runat="server">
        <td width="25%" class="leftField">
            <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEmail"
                ErrorMessage="Please enter an Email ID" Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                ErrorMessage="Please enter a valid Email ID" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td width="50%" align="center" colspan="2">
            <asp:Label ID="lblMailSent" runat="server" Text="Your Password was sent to your Email Address. "
                CssClass="FieldName"></asp:Label>
            <asp:LinkButton ID="lnkSignIn" runat="server" CssClass="FieldName" 
                onclick="lnkSignIn_Click" Visible="False">Sign In</asp:LinkButton>
            <asp:Label ID="lblError" runat="server" Text="Please check your Email Address" CssClass="Error"></asp:Label>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_ForgotPassword_btnSignIn');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_ForgotPassword_btnSignIn');" />
        </td>
    </tr>
</table>
