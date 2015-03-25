<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.ascx.cs"
    Inherits="WealthERP.General.ForgotPassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
 <link id="lnkBrowserIcon" rel="Shortcut Icon" type="image/x-icon" runat="server" />

    <script src="/Scripts/jquery.js" type="text/javascript"></script>

    <script src="/Scripts/jquery.colorbox-min.js" type="text/javascript"></script>

    <link href="/CSS/colorbox.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="Scripts/JScript.js"></script>

<table style="width: 100%;" align="center" class="TableBackground" border="0">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                    <td colspan="3"></td>
                        <td align="left">
                            Forgot Password
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="lnkBackbutton" runat="server" Text="Login" CssClass="LinkButtons" CausesValidation="false" OnClientClick="javascript:loadcontrol('Userlogin','none'); return false;"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <%--<tr>
        <td colspan="3" align="left">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Forgot Password"></asp:Label>
        </td>
    </tr>--%>
    <tr>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
        visible="false">
        <tr>
            <td align="center">
                <div class="success-msg" id="SuccessMsg" runat="server" visible="false" align="center">
                </div>
                <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
                </div>
            </td>
        </tr>
    </table>
    <table>
        <tr id="trLogin" runat="server">
            <td width="25%" class="leftField">
                <asp:Label ID="lblLoginId" runat="server" Text="Login Id:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtLoginId" runat="server" CssClass="txtField"></asp:TextBox><font
                    color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtLoginId"
                    ErrorMessage=" Please enter Login ID" Display="Dynamic" runat="server" CssClass="rfvPCG">
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr id="trEmail" runat="server">
            <td width="25%" class="leftField">
                <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField"></asp:TextBox><font
                    color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEmail"
                    ErrorMessage="Please enter an Email" Display="Dynamic" runat="server" CssClass="rfvPCG">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                    ErrorMessage=" Plese enter a valid Email" Display="Dynamic" runat="server" CssClass="rfvPCG"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
            <td>
            </td>
        </tr>
        <%--<tr>
            <td width="50%" align="center" colspan="2">
                <asp:Label ID="lblMailSent" runat="server" Text="Your Password was sent to your Email Address. "
                    CssClass="FieldName"></asp:Label>
                <asp:LinkButton ID="lnkSignIn" runat="server" CssClass="FieldName" OnClick="lnkSignIn_Click"
                    Visible="False">Sign In</asp:LinkButton>
                <asp:Label ID="lblError" runat="server" Text="Please check your Email Address" CssClass="Error"></asp:Label>
            </td>
            <td>
            </td>
        </tr>--%>
        <tr id="trPan" runat="server">
            <td width="25%" class="leftField">
                <asp:Label ID="lblPan" runat="server" Text="PAN.No:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtPan" runat="server" CssClass="txtField"></asp:TextBox><font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ControlToValidate="TxtPan"
                    ErrorMessage="Please enter your PAN" Display="Dynamic" runat="server" CssClass="rfvPCG">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server"
                    ControlToValidate="TxtPan" Display="Dynamic" ForeColor="Red" ErrorMessage="Please enter a valid PAN"
                    CssClass="rfvPCG" ValidationExpression="[A-Z]{5}\d{4}[A-Z]{1}"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Reset"
                    CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_ForgotPassword_btnSignIn');"
                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_ForgotPassword_btnSignIn');" />
            </td>
        </tr>
    </table>
</table>
