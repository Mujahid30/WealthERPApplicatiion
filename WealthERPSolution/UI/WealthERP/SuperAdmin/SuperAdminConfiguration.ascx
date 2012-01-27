<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SuperAdminConfiguration.ascx.cs"
    Inherits="WealthERP.SuperAdmin.SuperAdminConfiguration" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    .RadUpload input.ruFakeInput
    {
        display: none;
    }
    .RadUpload input.ruBrowse
    {
        width: 115px;
    }
    .RadUpload span.ruFileWrap input.ruButtonHover
    {
        background-position: 100% -46px;
    }
    .RadUpload input.ruButton
    {
        background-position: 0 -46px;
    }
    .invalid
    {
        color: Red;
    }
    .binary-image
    {
        margin-bottom: 5px;
    }
    .info-panel
    {
        font-family: Verdana,Tahoma;
        font-weight: bold;
        font-size: x-small;        
    }
      
</style>
<style id="Style1" type="text/css" runat="server">
        .radupload { float: left; margin-bottom:20px; }
        .bigModule { clear: both; }
        .smallModule { margin-bottom:20px; }
        #controlContainer { vertical-align: top; padding: 20px 10px; }
        .ruProgressArea { position: absolute; top: 0; left: 10px; }
        input.RadUploadSubmit { margin-top: 20px; }
    </style>
<script language="javascript" type="text/javascript" src="~/Scripts/JScript.js"></script>

<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<script type="text/javascript">
    function myOnClientFileSelected(radUpload, eventArgs) {
        document.getElementById("filestatus").style.display = "block";
        
    }
</script>

<asp:Label ID="headertitle" runat="server" CssClass="HeaderTextBig" Text="Application Configuration"></asp:Label>
<hr />
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                Settings Saved Successfully
            </div>
        </td>
    </tr>
</table>
<telerik:RadInputManager ID="RadInputManager1" runat="server" Skin="Telerik" EnableEmbeddedSkins="False">
    <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehavior1" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
        ErrorMessage="Invalid Email">
        <TargetControls>
            <telerik:TargetInput ControlID="txtEmailId" />
        </TargetControls>
    </telerik:RegExpTextBoxSetting>
    <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehavior2" ValidationExpression="\d+"
        ErrorMessage="Please Type Only Numbers">
        <TargetControls>
            <telerik:TargetInput ControlID="txtTelephoneNo" />
        </TargetControls>
        <Validation IsRequired="True"></Validation>
    </telerik:RegExpTextBoxSetting>
</telerik:RadInputManager>
<table width="100%">
    <tr>
        <td align="right">
            <asp:Label ID="lblHostLogo" runat="server" Text="Position of Host Logo: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <telerik:RadComboBox ID="ddlPickHostLogoPosition" runat="server" ShowToggleImage="True"
                EmptyMessage="-Select-" Skin="Telerik" EnableEmbeddedSkins="false" Width="205px">
                <ExpandAnimation Type="InExpo"></ExpandAnimation>
                <Items>
                    <telerik:RadComboBoxItem Text="Top Left Corner" Value="TopLeftCorner" ImageUrl="../Images/TopLeft.gif" />
                    <telerik:RadComboBoxItem Text="Top Right Corner" Value="TopRightCorner" ImageUrl="../Images/TopRight.gif"
                        Selected="true" />
                    <telerik:RadComboBoxItem Text="Top Center" Value="TopCenter" ImageUrl="../Images/TopCenter.gif" />
                    <telerik:RadComboBoxItem Text="No Logo" Value="NoLogo" ImageUrl="../Images/nologo.png" />
                </Items>
            </telerik:RadComboBox>
        </td>
        <td align="right">
            <asp:Label ID="lblUploadLogo" runat="server" Text="Browse Logo: " CssClass="FieldName"></asp:Label>
        </td>
        <td id="controlContainer" colspan="2">
            <span class="invalid"></span>            
                <div class="info-panel" id="filestatus" style="display:none">
                File Uploaded Successfully...
            </div>
            
            <telerik:RadUpload ID="RadUpload1" runat="server" TargetFolder="/Images/" OnClientFileSelected="myOnClientFileSelected"
                OverwriteExistingFiles="false" MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg,gif,png,bmp"  InitialFileInputsCount="1" ControlObjectsVisibility="None" />
            <asp:Label ID="lblFileUploaded" runat="server" Text="" CssClass="FieldName" Visible="false"></asp:Label>
        </td>        
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblAdvisorLogoPosition" runat="server" Text="Advisor Logo Position: "
                CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <telerik:RadComboBox ID="ddlPickAdvisorLogoPosition" runat="server" ExpandAnimation-Type="Linear"
                ShowToggleImage="True" EmptyMessage="-Select-" Skin="Telerik" EnableEmbeddedSkins="false"
                Width="205px">
                <ExpandAnimation Type="InExpo"></ExpandAnimation>
                <Items>
                    <telerik:RadComboBoxItem Text="Top Left Corner" Value="TopLeftCorner" ImageUrl="../Images/TopLeft.gif"
                        Selected="true" />
                    <telerik:RadComboBoxItem Text="Top Right Corner" Value="TopRightCorner" ImageUrl="../Images/TopRight.gif" />
                    <telerik:RadComboBoxItem Text="Top Center" Value="TopCenter" ImageUrl="../Images/TopCenter.gif" />
                </Items>
            </telerik:RadComboBox>
        </td>
        <td colspan="2">
            <asp:CompareValidator ID="Compare1" runat="server" Display="Dynamic" ControlToValidate="ddlPickHostLogoPosition"
                ControlToCompare="ddlPickAdvisorLogoPosition" EnableClientScript="True" CssClass="rfvPCG"
                Operator="NotEqual" ErrorMessage="Please select different Adviser Logo Position" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblApplyTheme" runat="server" Text="Apply Theme: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <telerik:RadComboBox ID="ddlApplyTheme" runat="server" ExpandAnimation-Type="Linear"
                ShowToggleImage="True" EmptyMessage="-Select-" Skin="Telerik" EnableEmbeddedSkins="false"
                Width="205px">
                <ExpandAnimation Type="InExpo"></ExpandAnimation>
                <Items>
                    <telerik:RadComboBoxItem Text="Maroon" Value="Maroon" ImageUrl="../Images/red-sample.gif" />
                    <telerik:RadComboBoxItem Text="Purple" Value="Purple" ImageUrl="../Images/purple-sample.gif" />
                    <telerik:RadComboBoxItem Text="Blue" Value="Blue" ImageUrl="../Images/blue-sample.gif" />
                </Items>
            </telerik:RadComboBox>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <asp:Label ID="lblContactInfoLabel" runat="server" Text="Contact Info to be dispalyed on Error Page "
                CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person: " CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="2">
            <asp:TextBox ID="txtContactPerson" runat="server" Text="" Width="205px"></asp:TextBox>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblTelephoneNo" runat="server" Text="Telephone No: " CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="2">
            <asp:TextBox ID="txtTelephoneNo" runat="server" Text="" MaxLength="12" Width="205px"></asp:TextBox>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblEmail" runat="server" Text="Email Id: " CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="2">
            <asp:TextBox ID="txtEmailId" runat="server" Text="" Width="205px"></asp:TextBox>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblApplicationSettings" runat="server" Text="Application Settings"
                CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblApplicationName" runat="server" Text="Application Name: " CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="2">
            <asp:TextBox ID="txtApplicationName" runat="server" Text="" Width="205px"></asp:TextBox>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblLoginPageContent" runat="server" Text="Login Page Content: " CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtLoginPageContent" runat="server" Text="" Width="500px" TextMode="MultiLine"
                Height="90px"></asp:TextBox>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
</table>
<asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnSubmit_Click" />
<asp:HiddenField ID="hdnUplFileName" runat="server" />
