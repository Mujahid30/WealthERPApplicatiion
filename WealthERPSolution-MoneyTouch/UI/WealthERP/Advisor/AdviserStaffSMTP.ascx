<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserStaffSMTP.ascx.cs"
    Inherits="WealthERP.Advisor.AdviserStaffSMTP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script>
    function ToggleCredentialDisplay() {
        checkbox = document.getElementById("ctrl_AdviserStaffSMTP_chkAthenticationRequired");

        if (checkbox.checked == true) {
            document.getElementById("trPassword").style.display = "";
            document.getElementById("trEmail").style.display = "";

        }
        else {
            document.getElementById("ctrl_AdviserStaffSMTP_txtPassword").value = "";
            document.getElementById("ctrl_AdviserStaffSMTP_txtEmail").value = "";
            document.getElementById("trPassword").style.display = "none";
            document.getElementById("trEmail").style.display = "none";
        }

    }
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Advisor Staff SMTP"></asp:Label>
            <hr />
        </td>
    </tr>
</table>

<table class="TableBackground" width="100%">
    <tr>
        <td style="width: 20%">
            &nbsp;&nbsp;
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label1" runat="server" Text="SMTP Host:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtSMTPHost" runat="server" CssClass="txtField"></asp:TextBox>
              <span id="Span2" class="spnRequiredField">*<br />
              </span>
              
          <asp:RequiredFieldValidator ID="reqtxtSMTPHost" ValidationGroup="btnSave" ControlToValidate="txtSMTPHost"
                ErrorMessage="Please enter the SMTP Host" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label2" runat="server" Text="SMTP Port:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtSMTPPort" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="txtSMTPPort_E" runat="server" Enabled="True" FilterType="Numbers"
                TargetControlID="txtSMTPPort">
            </cc1:FilteredTextBoxExtender>
              <span id="Span1" class="spnRequiredField">*<br />
            </span>
            <asp:RequiredFieldValidator ID="reqtxtSMTPPort" ValidationGroup="btnSave" ControlToValidate="txtSMTPHost"
                ErrorMessage="Please enter the SMTP Port" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trEmail">
        <td class="leftField">
            <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span4" class="spnRequiredField">*<br />
            </span>
            
            <asp:RequiredFieldValidator ID="reqtxtEmail" ValidationGroup="btnSave" ControlToValidate="txtSMTPHost"
                ErrorMessage="Please enter the Email address" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                ErrorMessage="Please enter a valid Email ID" Display="Dynamic" runat="server" ValidationGroup="btnSave"
                CssClass="rfvPCG" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
            </asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr id="trPassword">
        <td class="leftField">
            <asp:Label ID="lblPassword" runat="server" Text="Password:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtPassword" runat="server" CssClass="txtField" TextMode="Password"></asp:TextBox>
              <span id="Span3" class="spnRequiredField">*<br />
              <asp:RequiredFieldValidator ID="reqtxtPassword" ValidationGroup="btnSave" ControlToValidate="txtSMTPHost"
                ErrorMessage="Please enter the password" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            </span>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label5" runat="server" Text="SMTP Authentication Required:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:CheckBox ID="chkAthenticationRequired" Checked="true" runat="server" onchange="ToggleCredentialDisplay(this)" />
        </td>
    </tr>
    <tr>
        <td class="leftField">
        </td>
        <td>
            &nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
        </td>
        <td>
            <asp:Button ID="btnSave" runat="server" ValidationGroup="btnSave" Text="Save" CssClass="PCGButton" OnClick="btnSave_Click" />
            &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:Button ID="btnTest" runat="server" Text="Test Credentials" ValidationGroup="btnSave" CssClass="PCGMediumButton"
             OnClick="btnTest_Click" />
        </td>
    </tr>
    <tr id="trInsertMessage" runat="server">
        <td class="leftField">
        </td>
        <td>
            <asp:Label ID="lblInsertMessage" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
</table>
