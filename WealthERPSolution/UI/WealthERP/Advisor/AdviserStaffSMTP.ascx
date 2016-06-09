<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserStaffSMTP.ascx.cs"
    Inherits="WealthERP.Advisor.AdviserStaffSMTP" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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

<script type="text/javascript" src="http://code.jquery.com/jquery-latest.js"></script>

<script type="text/javascript">
    function HideMessage() {
        $(document).ready(function() {
            setTimeout(function() {
                $('.success-msg').hide();
            }, 8000);
        });
    }
</script>

<style type="text/css">
    .tdLabel
    {
        text-align: right !important;
        width: 30%;
    }
    .tdData
    {
        width: 40%;
    }
    .tdEmpty
    {
        width: 30%;
    }
</style>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<telerik:RadInputManager ID="RadInputManager1" runat="server" Skin="Telerik">
    <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" Validation-IsRequired="true">
        <TargetControls>
            <%--<telerik:TargetInput ControlID="txtEmail" />
      <telerik:TargetInput ControlID="txtSMTPHost" />--%>
            <telerik:TargetInput ControlID="txtSMTPPort" />
            <telerik:TargetInput ControlID="txtPassword" />
        </TargetControls>
        <Validation IsRequired="True" ValidationGroup="btnSave"></Validation>
    </telerik:TextBoxSetting>
    <telerik:TextBoxSetting BehaviorID="TextBoxBehavior2" Validation-IsRequired="false">
        <TargetControls>
            <telerik:TargetInput ControlID="txtSenderEmailAlias" />
        </TargetControls>
    </telerik:TextBoxSetting>
    <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehavior1" Validation-IsRequired="true"
        ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" ErrorMessage="Invalid Email">
        <TargetControls>
            <telerik:TargetInput ControlID="txtEmail" />
        </TargetControls>
        <Validation IsRequired="True" ValidationGroup="btnSave"></Validation>
    </telerik:RegExpTextBoxSetting>
    <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehavior2" Validation-IsRequired="true"
        ValidationExpression="[a-zA-Z0-9\\.]+" ErrorMessage="Invalid HostName">
        <TargetControls>
            <telerik:TargetInput ControlID="txtSMTPHost" />
        </TargetControls>
        <Validation IsRequired="True" ValidationGroup="btnSave"></Validation>
    </telerik:RegExpTextBoxSetting>
    <telerik:NumericTextBoxSetting Type="Number" MinValue="0" DecimalDigits="0">
        <TargetControls>
            <telerik:TargetInput ControlID="txtSMTPPort" />
        </TargetControls>
    </telerik:NumericTextBoxSetting>
    <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehaviorEmail" Validation-IsRequired="true"
        ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" ErrorMessage="Invalid Email">
        <TargetControls>
            <telerik:TargetInput ControlID="txtLoginWidGetEmail" />
        </TargetControls>
        <Validation IsRequired="True" ValidationGroup="btnSend"></Validation>
    </telerik:RegExpTextBoxSetting>
    <%--    <telerik:RegExpTextBoxSetting BehaviorID="rgeBrowserTitle" Validation-IsRequired="true" 
        ValidationExpression="[a-zA-Z0-9\\.]+" ErrorMessage="Invalid Browser Titile">
        <TargetControls>
            <telerik:TargetInput ControlID="txtBrowserTitleBarName" />
        </TargetControls>
        <Validation IsRequired="True" ValidationGroup="PreferenceSubmit"></Validation>
    </telerik:RegExpTextBoxSetting>--%>
    <telerik:RegExpTextBoxSetting BehaviorID="retBrowserURL" Validation-IsRequired="true"
        ValidationExpression="(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?" ErrorMessage="Invalid URL">
        <TargetControls>
            <telerik:TargetInput ControlID="txtWebSiteDomainName" />
            <telerik:TargetInput ControlID="txtLogOutPageUrl" />
        </TargetControls>
        <Validation IsRequired="True" ValidationGroup="PreferenceSubmit"></Validation>
    </telerik:RegExpTextBoxSetting>
    <%-- <telerik:RegExpTextBoxSetting BehaviorID="retPagesize" Validation-IsRequired="true"
        ValidationExpression="^\d" ErrorMessage="Row per page should numeric" >
        <TargetControls>
            <telerik:TargetInput ControlID="txtGridPageSize" />
          
        </TargetControls>
        <Validation IsRequired="True" ValidationGroup="BtnSubmitpage"></Validation>
    </telerik:RegExpTextBoxSetting>--%>
</telerik:RadInputManager>
<table width="100%" class="TableBackground" style="padding-bottom: 6px;">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Email/SMS Account
                        </td>
                    </tr>
                </table>
            </div>
            <br />
        </td>
    </tr>
   
</table>
<%--<div class="exampleWrapper">--%>
<%--<telerik:RadTabStrip ID="RadTabStrip2" runat="server" Skin="Telerik" MultiPageID="SetupEmailSMS"
    SelectedIndex="0">--%>
    <telerik:RadTabStrip ID="RadTabStrip2" EnableTheming="True" Skin="Telerik" EnableEmbeddedSkins="False"
    runat="server" MultiPageID="SetupEmailSMS" SelectedIndex="0">
    <Tabs>
        <telerik:RadTab runat="server" Text="Emails" Value="Email" TabIndex="0" Selected="True">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="SMS" Value="sms" TabIndex="1">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Preference Setting" Value="Preferance" TabIndex="3" Visible="false"
            PageViewID="RadPageView3">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Display" Value="Display" TabIndex="2" PageViewID="RadPageView4"  Visible="false">
        </telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="SetupEmailSMS" EnableViewState="false" runat="server" SelectedIndex="0"
    Width="100%">
    <telerik:RadPageView ID="RadPageView1" runat="server" Style="margin-top: 20px">
        <asp:Panel ID="pnlSystameticSetupView" runat="server" Width="100%">
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
                        <%--<asp:RequiredFieldValidator ID="reqtxtSMTPHost" ValidationGroup="btnSave" ControlToValidate="txtSMTPHost"
                ErrorMessage="Please enter the SMTP Host" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
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
                        <%--<asp:RequiredFieldValidator ID="reqtxtSMTPPort" ValidationGroup="btnSave" ControlToValidate="txtSMTPHost"
                ErrorMessage="Please enter the SMTP Port" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
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
                        <%--<asp:RequiredFieldValidator ID="reqtxtEmail" ValidationGroup="btnSave" ControlToValidate="txtEmail"
                ErrorMessage="Please enter the Email address" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                ErrorMessage="Please enter a valid Email ID" Display="Dynamic" runat="server" ValidationGroup="btnSave"
                CssClass="rfvPCG" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
            </asp:RegularExpressionValidator>--%>
                    </td>
                </tr>
                <tr id="trPassword">
                    <td class="leftField">
                        <asp:Label ID="lblPassword" runat="server" Text="Password:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="txtField"  MaxLength="15" TextMode="Password"></asp:TextBox>
                        <span id="Span3" class="spnRequiredField">*<br />
                            <%--<asp:RequiredFieldValidator ID="reqtxtPassword" ValidationGroup="btnSave" ControlToValidate="txtPassword"
                ErrorMessage="Please enter the password" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
                        </span>
                        <%--<asp:Label ID="lblNote" Text="Maximum 15 Characters"
                CssClass="rfvPCG" runat="server"></asp:Label>--%>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label3" runat="server" Text="Sender Email Alias:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtSenderEmailAlias" runat="server" MaxLength="60" CssClass="txtField"></asp:TextBox>
                        <%-- <asp:RequiredFieldValidator ID="reqtxtSMTPHost" ValidationGroup="btnSave" ControlToValidate="txtSMTPHost"
                ErrorMessage="Please enter the SMTP Host" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
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
                        <asp:Button ID="btnSave" runat="server" ValidationGroup="btnSave" Text="Save" CssClass="PCGButton"
                            OnClick="btnSave_Click" />
                        &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:Button ID="btnTest" runat="server" Text="Test Credentials" ValidationGroup="btnSave"
                            CssClass="PCGMediumButton" OnClick="btnTest_Click" />
                    </td>
                </tr>
                <tr id="trInsertMessage" runat="server">
                    <td class="leftField">
                    </td>
                    <td>
                        <asp:Label ID="lblInsertMessage" runat="server" Text="" CssClass="FieldName"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="passwordNote" class="Note">
                            <p>
                                <span style="font-weight: bold">Note:</span><br />
                                1. Please note that these settings used in all email Communication, includes :<br />
                                - Creating new Staff/customer , User Management , Reports (Send via Email) , Message
                                (via Email) , etc<br />
                                2. If these Settings are not filled, or not Validate then by default , it will use
                                email id< admin@wealtherp.com >.<br />
                                3. Use of Alias : it will appear in the mail as Sender's Name.<br />
                                4. Please Validate your settings by clicking on “Test Credentials “ .
                                <br />
                                5. SMTP relates to your Email host Setting, Please ask your provider for SMTP Settings
                                .<br />
                                <br />
                                <span style="font-weight: bold">SMTP Examples:</span><br />
                                <b>Example # 1: For Gmail users</b><br />
                                SMTP Host : smtp.gmail.com ;SMTP Port : 25 ;<br />
                                Email : abc12@gmail.com Password : < valid Password of Email id you entered> ;
                                <br />
                                Sender Email Alias : ABC Company ;SMTP Authentication Required : Select The Check
                                Box
                                <br />
                                Click on test credentials :<br />
                                1) SMTP Credentials Are Valid , Save it.<br />
                                2) Not able to send mail using the SMTP credentials check your Email id & Password.<br />
                                Email Rcvd as: Sender's Name: ABC Company abcd12@gmail.com
                                <br />
                                <br />
                                <b>Example # 2: For Yahoo users</b><br />
                                SMTP Host : smtp.mail.yahoo.com ; SMTP Port : 25 ;<br />
                                Email : abc12@yahoo.com Password : < valid Password of Email id you entered> ;<br />
                                Sender Email Alias : ABC Company ;SMTP Authentication Required : Select The Check
                                Box<br />
                                Click on test credentials :<br />
                                1) SMTP Credentials Are Valid , Save it.<br />
                                2) Not able to send mail using the SMTP credentials check your Email id & Password.<br />
                                Email Rcvd as: Sender's Name: ABC Company abcd12@yahoo.com
                                <br />
                                <br />
                            </p>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView2" runat="server">
        <asp:Panel ID="pnlCalenderSummaryView" runat="server" Width="100%">
            <table width="47%" class="TableBackground">
                <tr>
                    <td colspan="2">
                        &nbsp;&nbsp;<br />
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblSMSProvider" runat="server" Text="SMS Provider:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlSMSProvider" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                        <span id="spnAMC" runat="server" class="spnRequiredField">*</span>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlSMSProvider"
                            CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Provider"
                            Operator="NotEqual" ValidationGroup="btnSubmit" ValueToCompare="Select"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblUserName" runat="server" Text="User Name:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="txtField"></asp:TextBox>
                        <span id="Span5" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtUserName"
                            CssClass="rfvPCG" ErrorMessage="<br />Please enter UserName" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="btnSubmit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblPwd" runat="server" Text="Password:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtPwd" runat="server" MaxLength="15" TextMode="Password" CssClass="txtField"></asp:TextBox>
                        <span id="Span6" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPwd"
                            CssClass="rfvPCG" ErrorMessage="<br />Please enter password" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="btnSubmit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <tr>
                    <td class="leftField">
                        <asp:Label ID="Label6" runat="server" Text="SenderId:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtSenderID" runat="server" MaxLength="15"  CssClass="txtField"></asp:TextBox>
                        <span id="Span13" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtSenderID"
                            CssClass="rfvPCG" ErrorMessage="<br />Please enter Sender ID" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="btnSubmit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblsmsCredit" runat="server" Text="SMS Credit:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtsmsCredit" runat="server" CssClass="txtField"></asp:TextBox>
                        <span id="Span7" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtsmsCredit"
                            CssClass="rfvPCG" ErrorMessage="<br />Please enter SMS credits" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="btnSubmit"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator6" ControlToValidate="txtsmsCredit" runat="server"
                            ValidationGroup="btnSubmit" Display="Dynamic" ErrorMessage="<br />Please enter a numeric value"
                            Type="Double" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                        <asp:CompareValidator ID="cvAppRcvDate" runat="server" ControlToValidate="txtsmsCredit"
                            Display="Dynamic" CssClass="cvPCG" ValidationGroup="btnSubmit" ErrorMessage="<br />SMS credit value should be greater than 0"
                            Operator="GreaterThan" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                    </td>
                    <td class="rightField">
                        <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="PCGButton" ValidationGroup="btnSubmit"
                            OnClick="btnSubmit_Click" />
                    </td>
                </tr>
                <tr id="trBtnSaveMsg" runat="server">
                    <td class="leftField">
                    </td>
                    <td>
                        <asp:Label ID="lblbtnSaveMsg" runat="server" Text="" CssClass="FieldName"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="Div1" class="Note">
                            <p>
                                <span style="font-weight: bold">Note:</span><br />
                                1. You can pick up your 3rd Party SMS Provider .<br />
                                2. If your Provider is not in the list, please contact customer Care.
                            </p>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView4" runat="server">
        <asp:Panel ID="Panel1" runat="server" Width="100%">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <script type="text/javascript">
                        Sys.Application.add_load(HideMessage);
                    </script>

                    <table width="100%">
                        <tr runat="server" id="trMsg" visible="false">
                            <td align="center">
                                <div class="success-msg" id="divMsgSuccess" runat="server" align="center">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="divSectionHeading" id="div4">
                                    Grid PageSize Setup
                                </div>
                            </td>
                        </tr>
                        &nbsp;
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td class="leftField">
                                            <asp:Label ID="Label4" runat="server" Text="Default Rows Per Page :" CssClass="FieldName"></asp:Label>
                                        </td>
                                        <td class="RightField">
                                            <asp:TextBox ID="txtGridPageSize" runat="server" CssClass="txtField" Width="260px"
                                                ValidationGroup="check"></asp:TextBox>
                                            <span id="Span12" class="spnRequiredField">*</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtGridPageSize"
                                                CssClass="rfvPCG" ErrorMessage="<br />Please enter Row per page " Display="Dynamic"
                                                runat="server" InitialValue="" ValidationGroup="BtnSubmitpage"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="emailValidator" runat="server" Display="Dynamic"
                                                ErrorMessage="Please, enter valid number." CssClass="rfvPCG" ValidationExpression="^[0-9]+$"
                                                ControlToValidate="txtGridPageSize">
                                            </asp:RegularExpressionValidator>
                                            <asp:RangeValidator runat="server" ID="RangeValidator1" ControlToValidate="txtGridPageSize"
                                                Type="Integer" MinimumValue="10" MaximumValue="100" CssClass="rfvPCG" ErrorMessage="Page Range between 10-100 Acceptable"
                                                ValidationGroup="BtnSubmitpage">
                                            </asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="BtnSubmitpage" runat="server" Text="Submit" ValidationGroup="BtnSubmitpage"
                                                CssClass="PCGButton" OnClick="btnSubmitPageSize_Click" />
                                        </td>
                                        <td class="tdEmpty">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div id="Div3" class="Note">
                                                <p>
                                                    <span style="font-weight: bold">Note:</span><br />
                                                     Following Grid associated with display setting.<br />
                                                     1.Accounts <br />
                                                     2.MF SIP MIS <br />
                                                     3.MF SIP Projections <br />
                                                     4.Customer List <br />
                                                     5.View Equity Transactions <br/>
                                                </p>
                                            </div>
                                        </td>
                                        <td class="tdEmpty">
                                        </td>
                                    </tr>
                                </table>
                    
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView3" runat="server">
        <asp:Panel ID="pnlPreferenceSetup" runat="server" Width="100%">
            <asp:UpdatePanel ID="upAdviserPreferenceSetup" runat="server">
                <ContentTemplate>

                    <script type="text/javascript">
                        Sys.Application.add_load(HideMessage);
                    </script>

                    <table width="100%">
                        <tr runat="server" id="trSuccessMsg" visible="false">
                            <td align="center">
                                <div class="success-msg" id="divSuccessMsg" runat="server" align="center">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="divSectionHeading" id="divTest">
                                    Preference Setup
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td class="leftField">
                                            <asp:Label ID="lblIsLoginWidGetEnable" runat="server" Text="Enable login widget:"
                                                CssClass="FieldName"></asp:Label>
                                        </td>
                                        <td class="RightField,tdData">
                                            <asp:RadioButton ID="rbLoginWidGetYes" GroupName="LoginEnable" runat="server" Style="vertical-align: middle"
                                                CssClass="FieldName" Text="Yes" OnCheckedChanged="rbLoginWidGetYes_CheckedChanged"
                                                AutoPostBack="true" />
                                            <asp:RadioButton ID="rbLoginWidGetNo" GroupName="LoginEnable" runat="server" Style="vertical-align: middle"
                                                CssClass="FieldName" Text="No" OnCheckedChanged="rbLoginWidGetNo_CheckedChanged"
                                                AutoPostBack="true" />
                                        </td>
                                        <td class="tdEmpty">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftField">
                                            <asp:Label ID="lblWebSiteDomainName" runat="server" Text="Application login page URL:"
                                                CssClass="FieldName"></asp:Label>
                                        </td>
                                        <td class="RightField,tdData">
                                            <asp:TextBox ID="txtWebSiteDomainName" runat="server" CssClass="txtField" Width="260px"></asp:TextBox>
                                            <span id="Span9" class="spnRequiredField" />*<br />
                                        </td>
                                        <td class="tdEmpty">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftField">
                                            <asp:Label ID="lblLogOutPageURL" runat="server" Text="Application logout page URL:"
                                                CssClass="FieldName"></asp:Label>
                                        </td>
                                        <td class="RightField,tdData">
                                            <asp:TextBox ID="txtLogOutPageUrl" runat="server" CssClass="txtField" Width="260px"></asp:TextBox>
                                            <span id="Span10" class="spnRequiredField" />*<br />
                                        </td>
                                        <td class="tdEmpty">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftField">
                                            <asp:Label ID="lblBrowserTitleBarName" runat="server" Text="Application title:" CssClass="FieldName"></asp:Label>
                                        </td>
                                        <td class="RightField,tdData">
                                            <asp:TextBox ID="txtBrowserTitleBarName" runat="server" MaxLength="60" CssClass="txtField"
                                                Width="260px"></asp:TextBox>
                                            <span id="Span11" class="spnRequiredField" />*<br />
                                        </td>
                                        <td class="tdEmpty">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSubmitPreference" runat="server" ValidationGroup="PreferenceSubmit"
                                                Text="Submit" CssClass="PCGButton" OnClick="btnSubmitPreference_Click" />
                                        </td>
                                        <td class="tdEmpty">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div class="divSectionHeading">
                                                Login Widget Code Genaration
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftField">
                                            <asp:Label ID="lblLoginWidGet" runat="server" Text="Email Id to send:" CssClass="FieldName"></asp:Label>
                                        </td>
                                        <td class="RightField,tdData">
                                            <asp:TextBox ID="txtLoginWidGetEmail" runat="server" CssClass="txtField" Width="260px"></asp:TextBox>
                                            <span id="Span8" class="spnRequiredField" />*<br />
                                        </td>
                                        <td class="tdEmpty">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftField">
                                            &nbsp;&nbsp;
                                        </td>
                                        <td class="RightField">
                                            <asp:Button ID="btnSendLoginWidGet" runat="server" ValidationGroup="btnSend" Text="Send Login WidGet"
                                                CssClass="PCGMediumButton" OnClick="btnSendLoginWidGet_Click" />
                                        </td>
                                        <td class="tdEmpty">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div id="Div2" class="Note">
                                                <p>
                                                    <span style="font-weight: bold">Note:</span><br />
                                                    1. Use this to direct access from your website.<br />
                                                    2. Share the Instructions, with your website Provider.<br />
                                                </p>
                                            </div>
                                        </td>
                                        <td class="tdEmpty">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </telerik:RadPageView>
</telerik:RadMultiPage>
<%--<telerik:RadFormDecorator runat="server" ID="RadFormDecorator2" DecoratedControls="Textarea">
    </telerik:RadFormDecorator>--%>
<%--</div>--%>