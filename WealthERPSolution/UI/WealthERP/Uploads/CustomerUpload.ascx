<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerUpload.ascx.cs"
    Inherits="WealthERP.Uploads.CustomerUpload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
<link href="/YUI/build/container/assets/container.css" rel="stylesheet" type="text/css" />
<link href="/YUI/build/menu/assets/skins/sam/menu.css" rel="stylesheet" type="text/css" />

<script src="/YUI/build/utilities/utilities.js" type="text/javascript"></script>

<script src="/YUI/build/container/container-min.js" type="text/javascript"></script>

<%--<asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>--%>
<%--This are the linkrels for the Jquery files and CSS files abt the screen tips and info's--%>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<link href="../App_Themes/Maroon/Images/bubbletip.css" rel="stylesheet" type="text/css" />
<link href="../App_Themes/Maroon/Images/bubbletip-IE.css" rel="stylesheet" type="text/css" />
<%--End--%>
<%--This scripts includes the JQuery coding about the Screen Tips and screen info   --%>
<%--This scripts includes the JQuery coding about the Screen Tips and screen info   --%>

<script language="javascript" type="text/javascript">

    function setRadioButtonForFileFormat() {
        var a = document.getElementById('<%= hdnUploadType.ClientID %>').value;
        var b = document.getElementById('<%= hdnListCompany.ClientID %>').value;
        if (a == 'P' && b == 'WP') {
            var rdButton = document.getElementById('<%= File5.ClientID %>');
            rdButton.checked = true;
        }
        else if (a == 'PMFF' && b == 'WPT') {
            var rdButton = document.getElementById('<%--= File9.ClientID --%>');
            rdButton.checked = true;
        }
        else if (a == 'MFT' && b == 'WPT') {
            var rdButton = document.getElementById('<%= File4.ClientID %>');
            rdButton.checked = true;
        }
        else if (a == 'EQT' && b == 'WP') {
            var rdButton = document.getElementById('<%= File2.ClientID %>');
            rdButton.checked = true;
        }
        else if (a == 'MFSS' && b == 'WPT') {
            var rdButton = document.getElementById('<%= File6.ClientID %>');
            rdButton.checked = true;
        }
        else if (a == 'EQTA' && b == 'WP') {
            var rdButton = document.getElementById('<%= File1.ClientID %>');
            rdButton.checked = true;
        }
        else if (a == 'ODIN' && b == 'NSE') {
            var rdButton = document.getElementById('<%= File7.ClientID %>');
            rdButton.checked = true;
        }
        else if (a == 'ODIN' && b == 'BSE') {
            var rdButton = document.getElementById('<%= File8.ClientID %>');
            rdButton.checked = true;
        }

    }

</script>

<script language="javascript" type="text/javascript">

    function DownloadScript() {
        btn = document.getElementById('<%= btnExportExcel.ClientID %>');
        btn.click();

    }
    function checkDate(sender, args) {

        var selectedDate = new Date();
        selectedDate = sender._selectedDate;

        var todayDate = new Date();
        var msg = "";

        if (selectedDate > todayDate) {
            sender._selectedDate = todayDate;
            sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            alert("Warning! - Date Cannot be in the future");
            return false;
            lnkPaintFileDwnload.Visible = false;
        }

    }
</script>

<script type="text/javascript">
    $(document).ready(function() {
        $('.ScreenTip1').bubbletip($('#div1'), { deltaDirection: 'right' });
        $('.ScreenTip2').bubbletip($('#div2'), { deltaDirection: 'right' });
        $(".flip").click(function() { $(".panel").slideToggle(); });
        $('.ScreenTip3').bubbletip($('#div3'), { deltaDirection: 'right' });
    });
</script>

<%--End--%>
<!--This script is used for Progress bar -->

<script type="text/javascript">


    function pageLoad() {
        InitDialogs();
        Loading(false);
    }

    function UpdateImg(ctrl, imgsrc) {
        var img = document.getElementById(ctrl);
        img.src = imgsrc;
    }

    // sets up all of the YUI dialog boxes
    function InitDialogs() {
        DialogBox_Loading = new YAHOO.widget.Panel("waitBox",
	{ fixedcenter: true, modal: true, visible: false,
	    width: "230px", close: false, draggable: true
	});
        DialogBox_Loading.setHeader("Processing, please wait...");
        DialogBox_Loading.setBody('<div style="text-align:center;"><img src="/Images/Wait.gif" id="Image1" /></div>');
        DialogBox_Loading.render(document.body);
    }
    function Loading(b) {
        if (b == true && Page_IsValid == true) {
            DialogBox_Loading.show();
        }
        else {
            DialogBox_Loading.hide();
        }
    }
    function DownloadScript() {
        btn = document.getElementById('<%= btnExportExcel.ClientID %>');
        btn.click();
    }
</script>

<style type="text/css">
    success-msg
    {
        z-index: -1; !important;}</style>
<%--<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <img src="../Images/helpImage.png" height="25px" width="25px" style="float: right;"
                class="flip" />
            <asp:Label ID="Label2" runat="server" CssClass="HeaderTextBig" Text="Uploads"></asp:Label>
            <hr />
        </td>
    </tr>
</table>--%>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Uploads
                        </td>
                        <td align="right">
                            <img src="../Images/helpImage.png" height="15px" width="20px" style="float: right;"
                                class="flip" />
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td colspan="3">
            <div class="panel">
                <p>
                    Using this feature you can upload your client profiles and other financial data
                    (MF Transactions, Equity Transactions etc.) provided by your institution or you
                    can use our standard templates to do so.
                </p>
            </div>
        </td>
    </tr>
    <tr>
        <td align="center">
            <div id="msgUploadComplete" runat="server" class="success-msg" align="center" visible="false">
                Uploading successfully Completed
            </div>
            <asp:LinkButton ID="lnkClick" runat="server" Text="Click here to start new upload"
                Font-Size="Small" Font-Underline="false" class="textfield" OnClick="lnkClick_Click"
                Visible="false"></asp:LinkButton>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="uploadError" runat="server" class="failure-msg" align="center" visible="false">
                <asp:Label ID="Message_lbl" runat="server" Text="Label" Font-Bold="False"></asp:Label>
            </div>
        </td>
    </tr>
</table>
<table class="TableBackground" style="width: 100%;">
    <tr>
        <td colspan="4" align="right" runat="server">
            <asp:Label ID="lblLastUploadDateText" runat="server" Text="Last Upload Date:" CssClass="UploadDateLbl"
                Visible="false"></asp:Label>
            <asp:Label ID="lblLastUploadDate" runat="server" Text="" Font-Bold="true" CssClass="UploadDateLbl"
                Visible="false"></asp:Label>
        </td>
    </tr>
    <tr id="trError" visible="false" runat="server">
        <td colspan="4">
            <asp:Label ID="lblError" runat="server" Text="" CssClass="Error"></asp:Label>
        </td>
    </tr>
    <tr id="trMessage" visible="false" runat="server">
        <td colspan="4" class="Message">
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="SuccessMsg"></asp:Label>
        </td>
    </tr>
    <tr>
        <div class="divSectionHeading" style="vertical-align: text-bottom">
            Source Selection
        </div>
    </tr>
    <tr id="trAdviserSelection" runat="server">
        <td class="leftField">
            <asp:Label ID="lblAdviser" CssClass="FieldName" runat="server" Text="Please Select Adviser:"></asp:Label>
        </td>
        <td id="tdDdlAdviser" runat="server" align="left">
            <asp:DropDownList ID="ddlAdviser" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAdviser_OnSelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <%-- <asp:CompareValidator ID="cvDDLAdviser" runat="server" ErrorMessage="<br />Please Select Adviser"
            ValidationGroup="btn_Upload" ControlToValidate="ddlAdviser" Operator="NotEqual"
            CssClass="rfvPCG" ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>--%>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label4" CssClass="FieldName" runat="server" Text="Upload Type:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlUploadType" runat="server" OnSelectedIndexChanged="ddlUploadType_SelectedIndexChanged"
                AutoPostBack="true" CssClass="cmbLongField">
                <asp:ListItem Value="0">Select an Extract Type</asp:ListItem>
                <%-- <asp:ListItem Value="MFF">MF Folio Only</asp:ListItem>--%>
                <asp:ListItem Value="PMFF" Enabled="false">Profile & MF Folio</asp:ListItem>
                <asp:ListItem Value="MFT">MF Transaction</asp:ListItem>
                <%--    <asp:ListItem Value="MFF">MF Folio Only</asp:ListItem> --%>
                <asp:ListItem Value="EQTA" Enabled="false">Equity Trade Account Only</asp:ListItem>
                <%-- <asp:ListItem Value="EQDA">Equity Demat Account Only</asp:ListItem>--%>
                <asp:ListItem Value="EQT" Enabled="false">Equity Transaction</asp:ListItem>
                <asp:ListItem Value="MFSS" Enabled="false">Systematic</asp:ListItem>
                <asp:ListItem Value="TRAIL" Enabled="false">Trail Commission</asp:ListItem>
                <asp:ListItem Value="P">Profile Incremental</asp:ListItem>
                <asp:ListItem Value="CM">Client Modification</asp:ListItem>
                <asp:ListItem Value="Link">Fixed Income</asp:ListItem>
                <asp:ListItem Value="MFR">MF Holding Recon</asp:ListItem>
            </asp:DropDownList>
            <img src="../Images/help.png" class="ScreenTip1" style="height: 15px; width: 15px;" />
            <div id="div1" style="display: none;">
                <p class="tip">
                    Choose what data you intend to upload from the dropdown<br />
                    (eg: Client Profiles or MF Transactions etc)
                </p>
            </div>
            <asp:RequiredFieldValidator ID="ddlUploadType_RequiredFieldValidator" ControlToValidate="ddlUploadType"
                ValidationGroup="btn_Upload" ErrorMessage="Please select an Extract type" InitialValue="0"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trType" runat="server" Visible="false">
        <td class="leftField">
            <asp:Label ID="Label5" CssClass="FieldName" runat="server" Text="Offline/Online Type:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlType" runat="server"  
                AutoPostBack="true" CssClass="cmbLongField" >
                <asp:ListItem Value="1">Online</asp:ListItem>                
                <asp:ListItem Value="0">Offline</asp:ListItem>
                <%-- <asp:ListItem Value="MFF">MF Folio Only</asp:ListItem>--%>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlType" 
                ValidationGroup="btn_Upload" ErrorMessage="Please select type" InitialValue="" 
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trRM" runat="server">
        <td class="leftField">
            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Pick a RM:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" AutoPostBack="true">
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlRM"
                ErrorMessage="Please select a Branch" Operator="NotEqual" ValueToCompare="Select a RM"
                Display="Dynamic" CssClass="cvPCG">
            </asp:CompareValidator>
        </td>
    </tr>
    <tr id="trListBranch" runat="server" visible="false">
        <td class="leftField">
            <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Pick a Branch:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlListBranch" runat="server" CssClass="cmbField" AutoPostBack="true">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="ddlListBranch_CompareValidator" runat="server" ControlToValidate="ddlListBranch"
                ErrorMessage="Please select a Branch" Operator="NotEqual" ValueToCompare="Select a Branch"
                Display="Dynamic" CssClass="cvPCG">
            </asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="External Source:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlListCompany" runat="server" CssClass="cmbLongField" OnSelectedIndexChanged="ddlListCompany_SelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
            <img src="../Images/help.png" class="ScreenTip2" style="height: 15px; width: 15px;" />
            <div id="div2" style="display: none;">
                <p class="tip">
                    Choose the source of the data your Uploading, it could be from your Financial Institution
                    <br />
                    (eg: CAMS, Karvy etc) or the Standard Templates provided by the software
                </p>
            </div>
            <asp:CompareValidator ID="ddlListCompany_CompareValidator" runat="server" ControlToValidate="ddlListCompany"
                ErrorMessage="Please select an External Source" Operator="NotEqual" ValueToCompare="Select Source Type"
                Display="Dynamic" CssClass="cvPCG" ValidationGroup="btn_Upload">
            </asp:CompareValidator>
            <asp:RequiredFieldValidator ID="ddlListCompany_RequiredFieldValidator" ControlToValidate="ddlListCompany"
                ValidationGroup="btn_Upload" ErrorMessage="Please select an External Source"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:LinkButton ID="lnkbtnpup" runat="server" Font-Size="X-Small" CausesValidation="False"
                OnClientClick="return setRadioButtonForFileFormat();" OnClick="lnkbtnpup_Click1">click here to download standard file formats</asp:LinkButton>
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
                TargetControlID="lnkbtnpup" DynamicServicePath="" BackgroundCssClass="modalBackground"
                Enabled="True" OkControlID="btnOk" PopupDragHandleControlID="Panel1" CancelControlID="btnCancel"
                Drag="true" OnOkScript="DownloadScript();">
            </cc1:ModalPopupExtender>
            <%-- <asp:LinkButton ID="lnkPaintFileDwnload" runat="server" Font-Size="X-Small" CausesValidation="False"
                    OnClick="lnkPaintFileDwnload_Click" Visible="false">click here to download SIP file upload steps</asp:LinkButton>--%>
        </td>
        <td align="right">
            <asp:LinkButton ID="lnkPaintFileDwnload" runat="server" Font-Size="X-Small" CausesValidation="False"
                OnClick="lnkPaintFileDwnload_Click" Visible="false">click here to download SIP file upload steps</asp:LinkButton>
        </td>
        <%-- </td> --%>
    </tr>
    <tr id="datevisible" runat="server" visible="false">
        <td class="leftField">
            <asp:Label ID="lbldate" runat="server" CssClass="FieldName" Text="Select Date:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtUploadDate" runat="server" CssClass="txtField"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="txtUploadDate_CalendarExtender" runat="server"
                Format="dd/MM/yyyy" TargetControlID="txtUploadDate" OnClientDateSelectionChanged="checkDate">
            </ajaxToolkit:CalendarExtender>
            <ajaxToolkit:TextBoxWatermarkExtender ID="txtUploadDate_TextBoxWatermarkExtender"
                runat="server" TargetControlID="txtUploadDate" WatermarkText="dd/mm/yyyy">
            </ajaxToolkit:TextBoxWatermarkExtender>
            <span id="Span4" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtUploadDate"
                CssClass="rfvPCG" ValidationGroup="btn_Upload" ErrorMessage="<br />Please select a Transaction Date"
                Display="Dynamic" runat="server" InitialValue="">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="upload" runat="server" visible="false">
        <td class="leftField">
            <asp:Label ID="lbluploadtype" runat="server" CssClass="FieldName" Text="Upload Type:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlAction_OnSelectedIndexChanged">
                <asp:ListItem Text="NSE" />
                <asp:ListItem Text="BSE" />
            </asp:DropDownList>
        </td>
    </tr>
    <%--<tr id="trFileTypeRow" runat="server">
        <td class="leftField">
            <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="File Extension:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlListExtensionType" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>--%>
    <%-- <tr id="SkiprowsVisible" runat="server" visible="False">
        <td class="leftField">
            <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Do you wish to skip rows?:"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:RadioButton ID="rbSkipRowsYes" runat="server" Text="Yes" CssClass="FieldName"
                GroupName="rbSkipRows" OnCheckedChanged="rbSkipRowsYes_CheckedChanged" AutoPostBack="true" />
            <asp:RadioButton ID="rbSkipRowsNo" runat="server" Text="No" CssClass="FieldName"
                GroupName="rbSkipRows" OnCheckedChanged="rbSkipRowsNo_CheckedChanged" AutoPostBack="true" />
            <img src="../Images/help.png" class="ScreenTip3" style="height: 15px; width: 15px;" />
            <div id="div3" style="display: none;">
                <p class="tip">
                    Specify if any rows need to be omitted in your files.</p>
            </div>
        </td>
    </tr>
    <tr id="trNoOfRows" runat="server" visible="false">
        <td class="leftField">
            <asp:Label ID="lblNoOfRows" runat="server" CssClass="FieldName" Text="Specify number of rows:"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtNoOfRows" runat="server" CssClass="txtField"></asp:TextBox>
            <asp:RequiredFieldValidator ID="txtNoOfRows_RequiredFieldValidator" ControlToValidate="txtNoOfRows"
                ValidationGroup="btn_Upload" ErrorMessage="Please enter number of rows" Display="Dynamic"
                runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtNoOfRows"
                ValidationGroup="btn_Upload" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
        </td>
    </tr>--%>
    <tr id="trMfRecon" runat="server" visible="false">
        <td class="leftField">
            <asp:Label ID="lblMfRecon" runat="server" CssClass="FieldName" Text="Remarks:"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:TextBox ID="txtMfRecon" runat="server" CssClass="txtField"></asp:TextBox>
            <asp:RequiredFieldValidator ID="txtNoOfRows_RequiredFieldValidator" ControlToValidate="txtMfRecon"
                ValidationGroup="btn_Upload" ErrorMessage="Please enter remark" Display="Dynamic"
                runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Browse:"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:FileUpload ID="FileUpload" runat="server" Height="22px" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="FileUpload"
                runat="Server" ValidationGroup="btn_Upload" ErrorMessage="Only .dbf, .xls and.xlsx File allowed"
                Display="Dynamic" ValidationExpression="^.*\.((x|X)(l|L)(s|S)|(x|X)(l|L)(s|S)(x|X)|(d|D)(b|B)(f|F)|(t|T)(x|X)(t|T))$"
                CssClass="rfvPCG" />
            <br />
            <asp:RequiredFieldValidator ID="FileUpload_RequiredFieldValidator" ControlToValidate="FileUpload"
                ValidationGroup="btn_Upload" ErrorMessage="Please select a file for upload" Display="Dynamic"
                runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td colspan="3">
            <%--<asp:label ID="FileType_lbl" runat="server" Font-Bold="false" CssClass="MsgInfo" text="Label"></asp:label>--%>
            <asp:Label ID="lblFileType" runat="server" Font-Bold="false" CssClass="MsgInfo" Text="Label"></asp:Label>
            <%--<asp:label ID="FileType_lbl" runat="server" Font-Bold="false" CssClass="MsgInfo" text="Label"></asp:label>--%>
            <%-- <asp:Label ID="lblErrorFileType" runat="server"  CssClass="rfvPCG" Text="Please Upload Correct Format of file.."></asp:Label>
            --%>
        </td>
    </tr>
    <tr>
        <td class="SubmitCell" colspan="4">
            <asp:Button ID="btn_Upload" runat="server" Text="Upload" OnClick="btn_Upload_Click"
                ValidationGroup="btn_Upload" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerUpload_btn_Upload','S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerUpload_btn_Upload','S');"
                OnClientClick="Page_ClientValidate();Loading(true);" />
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="Panel1" runat="server" CssClass="ModelPup" Visible="false" Width="450px">
                <asp:RadioButton ID="File1" Text="Equity Trade Account" Checked="false" GroupName="colors"
                    runat="server" />
                <br />
                <asp:RadioButton ID="File2" Text="Equity Transaction" Checked="false" GroupName="colors"
                    runat="server" />
                <br />
                <asp:RadioButton ID="File3" Text="MF Folio" Checked="false" GroupName="colors" runat="server" />
                <br />
                <asp:RadioButton ID="File4" Text="MF Transaction" Checked="false" GroupName="colors"
                    runat="server" />
                <br />
                <asp:RadioButton ID="File5" Text="Profile Only" Checked="false" GroupName="colors"
                    runat="server" />
                <br />
                <asp:RadioButton ID="File6" Text="Systematic" Checked="false" GroupName="colors"
                    runat="server" />
                <br />
                <asp:RadioButton ID="File7" Text="ODIN (NSE)" Checked="false" GroupName="colors"
                    runat="server" />
                <br />
                <asp:RadioButton ID="File8" Text="ODIN (BSE)" Checked="false" GroupName="colors"
                    runat="server" />
                <br />
                <%--<asp:RadioButton ID="File9" Text="Profile & MF Folio" Checked="false" GroupName="colors"
                    runat="server" />
                <br />--%>
                <asp:RadioButton ID="File10" Text="FI *" Checked="false" GroupName="colors" runat="server" />
                <br />
                <asp:RadioButton ID="File11" Text="LI *" Checked="false" GroupName="colors" runat="server" />
                <br />
                <asp:RadioButton ID="File12" Text="GI *" Checked="false" GroupName="colors" runat="server" />
                <br />
                <asp:RadioButton ID="AllFiles" Text="All Standard Upload Files" Checked="false" GroupName="colors"
                    runat="server" />
                <br />
                <asp:Button ID="btnOk" runat="server" Text="Download" CausesValidation="false" CssClass="PCGButton" />
                &nbsp;
                <asp:Button ID="btnCancel" CausesValidation="false" runat="server" Text="Cancel"
                    CssClass="PCGButton" />
                <br />
                Note:* Request Custcare to Upload these files from back end
            </asp:Panel>
            <asp:Button class="ExportButton" ID="btnExportExcel" runat="server" Style="display: none"
                OnClick="btnExportExcel_Click" CausesValidation="false" Height="31px" Width="35px" />
        </td>
    </tr>
</table>
<div id="divInputErrorList" runat="server" visible="false">
    <table class="TableBackground" style="width: 100%;">
        <tr>
            <td>
                <asp:Label ID="lblInputError" runat="server" Text="Please rectify the following errors in the source file and Upload again"
                    CssClass="Error"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvInputError" runat="server" CssClass="GridViewStyle" ShowFooter="true"
                    AllowPaging="false">
                    <FooterStyle CssClass="FieldName" />
                    <RowStyle CssClass="RowStyle" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</div>
<div id="divresult" runat="server" visible="false">
    <table class="TableBackground" style="width: 100%;">
        <%--<tr>
            <td colspan="4">
                <asp:Label ID="lblProcessMonitoring" Text="Process Progress Monitoring" CssClass="HeaderTextSmall"
                    runat="server">
                </asp:Label>
                <hr />
            </td>
        </tr>--%>
        <%--<tr>
            <td class="leftField">
                <asp:Label ID="lblProcessID" Text="Process ID:" CssClass="FieldName" runat="server">
                </asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtProcessID" CssClass="txtField" runat="server" Enabled="false">
                </asp:TextBox>
            </td>
        </tr>--%>
        <%--<tr>
            <td class="leftField">
                <asp:Label ID="lblValidationProgress" Text="Validation Progress:" CssClass="FieldName"
                    runat="server">
                </asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtValidationProgress" CssClass="txtField" runat="server" Enabled="false">
                </asp:TextBox>
            </td>
        </tr>--%>
        <%--<tr>
            <td class="leftField">
                <asp:Label ID="lblXMLProgress" Text="Conversion to XML Progress:" CssClass="FieldName"
                    runat="server">
                </asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtXMLProgress" CssClass="txtField" runat="server" Enabled="false">
                </asp:TextBox>
            </td>
        </tr>--%>
        <%--<tr>
            <td class="leftField">
                <asp:Label ID="lblInputInsertionProgress" Text="Input Insertion Progress:" CssClass="FieldName"
                    runat="server">
                </asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtInputInsertionProgress" CssClass="txtField" runat="server" Enabled="false">
                </asp:TextBox>
            </td>
        </tr>--%>
        <%--<tr>
            <td class="leftField">
                <asp:Label ID="lblFirstStagingInsertionProgress" Text="First Staging Insertion Progress:"
                    CssClass="FieldName" runat="server">
                </asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtFirstStagingInsertionProgress" CssClass="txtField" runat="server"
                    Enabled="false">
                </asp:TextBox>
            </td>
        </tr>--%>
        <%--<tr>
            <td class="leftField">
                <asp:Label ID="lblSecondStagingInsertionProgress" Text="Second Staging Insertion Progress:"
                    CssClass="FieldName" runat="server">
                </asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtSecondStagingInsertionProgress" CssClass="txtField" runat="server"
                    Enabled="false">
                </asp:TextBox>
            </td>
        </tr>--%>
        <%--<tr>
            <td class="leftField">
                <asp:Label ID="lblWERPInsertionProgress" Text="WERP Insertion Progress:" CssClass="FieldName"
                    runat="server">
                </asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtWERPInsertionProgress" CssClass="txtField" runat="server" Enabled="false">
                </asp:TextBox>
            </td>
        </tr>--%>
        <%--<tr>
            <td class="leftField">
                <asp:Label ID="lblXtrnlInsertionProgress" Text="External Insertion Progress:" CssClass="FieldName"
                    runat="server">
                </asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtXtrnlInsertionProgress" CssClass="txtField" runat="server" Enabled="false">
                </asp:TextBox>
            </td>
        </tr>--%>
        <%--<tr>
            <td colspan="4">
                &nbsp;
            </td>
        </tr>--%>
        <%--<tr>
            <td colspan="4" class="SubmitCell">
                <asp:Button ID="btnRollback" runat="server" Text="Rollback" CssClass="PCGButton"
                    onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerUpload_btnRollback','S');"
                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerUpload_btnRollback','S');"
                    OnClick="btnRollback_Click" />
            </td>
        </tr>--%>
        <tr>
            <td colspan="4">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblSummary" Text="Summary of Upload" CssClass="HeaderTextSmall" runat="server">
                </asp:Label>
                <hr />
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblUploadStartTime" Text="Start Time:" CssClass="FieldName" runat="server">
                </asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtUploadStartTime" CssClass="txtField" runat="server" Enabled="false">
                </asp:TextBox>
            </td>
            <td class="leftField">
                <asp:Label ID="lblUploadEndTime" Text="End Time:" CssClass="FieldName" runat="server">
                </asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtUploadEndTime" CssClass="txtField" runat="server" Enabled="false">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblExternalTotalRecords" Text="Total Records in External File:" CssClass="FieldName"
                    runat="server">
                </asp:Label>
            </td>
            <td colspan="3" class="rightField">
                <asp:TextBox ID="txtExternalTotalRecords" CssClass="txtField" runat="server" Enabled="false">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblUploadedRecords" Text="Total Records Uploaded:" CssClass="FieldName"
                    runat="server">
                </asp:Label>
            </td>
            <td colspan="3" class="rightField">
                <asp:TextBox ID="txtUploadedRecords" CssClass="txtField" runat="server" Enabled="false">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblRejectedRecords" Text="Total Records Rejected:" CssClass="FieldName"
                    runat="server">
                </asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtRejectedRecords" CssClass="txtField" runat="server" Enabled="false">
                </asp:TextBox>
            </td>
            <td colspan="2" class="rightField">
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label runat="server" ID="lblTotalInputRecordsRejected" Visible="false" Text="Total Input Records Rejected:"
                    CssClass="FieldName">
                </asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtInputRejectedRecords" CssClass="txtField" runat="server" Visible="false"
                    Enabled="false">
                </asp:TextBox>
            </td>
            <td colspan="2" class="rightField">
                <asp:Button ID="btn_ViewRjects" runat="server" Text="View Rejects" CssClass="PCGMediumButton"
                    onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerUpload_btn_ViewRjects','M');"
                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerUpload_btn_ViewRjects','M');"
                    OnClick="btn_ViewRjects_Click" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <%-- <td><asp:Label ID="lblUploadProcessId" runat="server"></asp:Label></td>
         <td><asp:Label ID="lblpackagePath" runat="server"></asp:Label></td>
          <td><asp:Label ID="lblfileName" runat="server"></asp:Label></td>
           <td><asp:Label ID="lblconfigPath" runat="server"></asp:Label></td>--%>
        </tr>
    </table>
</div>
<asp:HiddenField runat="server" ID="hdnUploadType" />
<asp:HiddenField runat="server" ID="hdnListCompany" />

<script type="text/javascript" language="javascript">
    function ChangeVisibilty(ctrl, action) {

        if (action == "U") {
            alert(document.getElementById(ctrl));
            document.getElementById(ctrl).visible = true;
        }
        else {
            alert(document.getElementById(ctrl));
            document.getElementById(ctrl).visible = false;
        }
    }
</script>

<asp:HiddenField ID="hfRmId" runat="server" />
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>
