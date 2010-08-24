<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerUpload.ascx.cs"
    Inherits="WealthERP.Uploads.CustomerUpload" %>
<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
<link href="/YUI/build/container/assets/container.css" rel="stylesheet" type="text/css" />
<link href="/YUI/build/menu/assets/skins/sam/menu.css" rel="stylesheet" type="text/css" />

<script src="/YUI/build/utilities/utilities.js" type="text/javascript"></script>

<script src="/YUI/build/container/container-min.js" type="text/javascript"></script>

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
</script>

<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblView" runat="server" CssClass="HeaderTextBig" Text="Uploads"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
    </tr>
    <tr>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgUploadComplete" runat="server" class="success-msg" align="center" visible="false">
                Uploading successfully Completed
            </div>
        </td>
    </tr>
</table>
<table class="TableBackground" style="width: 100%;">
    <tr>
        <td colspan="4" align="right" runat="server">
            <asp:Label ID="lblLastUploadDateText" runat="server" Text="Last Upload Date:" CssClass="Error"
                Visible="false"></asp:Label>
            <asp:Label ID="lblLastUploadDate" runat="server" Text="" CssClass="Error" Visible="false"></asp:Label>
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
        <td colspan="4">
            <asp:Label ID="lblSelection" runat="server" Text="Source Selection" CssClass="HeaderTextSmall">
            </asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label4" CssClass="FieldName" runat="server" Text="Extract Type:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlUploadType" runat="server" OnSelectedIndexChanged="ddlUploadType_SelectedIndexChanged"
                AutoPostBack="true" CssClass="cmbField">
                <asp:ListItem Value="0">Select an Extract Type</asp:ListItem>
                <asp:ListItem Value="P">Profile Only</asp:ListItem>
                <asp:ListItem Value="MFF">MF Folio Only</asp:ListItem>
                <asp:ListItem Value="PMFF">Profile & MF Folio</asp:ListItem>
                <asp:ListItem Value="MFT">MF Transaction</asp:ListItem>
                <asp:ListItem Value="EQTA">Equity Trade Account Only</asp:ListItem>
                <asp:ListItem Value="EQDA">Equity Demat Account Only</asp:ListItem>
                <asp:ListItem Value="EQT">Equity Transaction</asp:ListItem>
                <asp:ListItem Value="MFSS">Systematic Setup</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="ddlUploadType_RequiredFieldValidator" ControlToValidate="ddlUploadType"
                ValidationGroup="btn_Upload" ErrorMessage="Please select an Extract type" InitialValue="0"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
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
            <asp:DropDownList ID="ddlListCompany" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlListCompany_SelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
            <asp:CompareValidator ID="ddlListCompany_CompareValidator" runat="server" ControlToValidate="ddlListCompany"
                ErrorMessage="Please select an External Source" Operator="NotEqual" ValueToCompare="Select Source Type"
                Display="Dynamic" CssClass="cvPCG" ValidationGroup="btn_Upload">
            </asp:CompareValidator>
            <asp:RequiredFieldValidator ID="ddlListCompany_RequiredFieldValidator" ControlToValidate="ddlListCompany"
                ValidationGroup="btn_Upload" ErrorMessage="Please select an External Source"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
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
    <tr>
        <td class="leftField">
            <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Do you wish to skip rows?:"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:RadioButton ID="rbSkipRowsYes" runat="server" Text="Yes" CssClass="FieldName"
                GroupName="rbSkipRows" OnCheckedChanged="rbSkipRowsYes_CheckedChanged" AutoPostBack="true" />
            <asp:RadioButton ID="rbSkipRowsNo" runat="server" Text="No" CssClass="FieldName"
                GroupName="rbSkipRows" OnCheckedChanged="rbSkipRowsNo_CheckedChanged" AutoPostBack="true" />
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
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Browse:"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:FileUpload ID="FileUpload" runat="server" Height="22px" />
            <asp:RequiredFieldValidator ID="FileUpload_RequiredFieldValidator" ControlToValidate="FileUpload"
                ValidationGroup="btn_Upload" ErrorMessage="Please select a file for upload" Display="Dynamic"
                runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td colspan="4">
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
        <td colspan="4">
            &nbsp;
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
                    AllowPaging="true" OnPageIndexChanging="gvInputError_PageIndexChanging">
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
        <tr>
            <td colspan="4">
                <asp:Label ID="lblProcessMonitoring" Text="Process Progress Monitoring" CssClass="HeaderTextSmall"
                    runat="server">
                </asp:Label>
                <hr />
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblProcessID" Text="Process ID:" CssClass="FieldName" runat="server">
                </asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtProcessID" CssClass="txtField" runat="server" Enabled="false">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblValidationProgress" Text="Validation Progress:" CssClass="FieldName"
                    runat="server">
                </asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtValidationProgress" CssClass="txtField" runat="server" Enabled="false">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblXMLProgress" Text="Conversion to XML Progress:" CssClass="FieldName"
                    runat="server">
                </asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtXMLProgress" CssClass="txtField" runat="server" Enabled="false">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblInputInsertionProgress" Text="Input Insertion Progress:" CssClass="FieldName"
                    runat="server">
                </asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtInputInsertionProgress" CssClass="txtField" runat="server" Enabled="false">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
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
        </tr>
        <tr>
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
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblWERPInsertionProgress" Text="WERP Insertion Progress:" CssClass="FieldName"
                    runat="server">
                </asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtWERPInsertionProgress" CssClass="txtField" runat="server" Enabled="false">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblXtrnlInsertionProgress" Text="External Insertion Progress:" CssClass="FieldName"
                    runat="server">
                </asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtXtrnlInsertionProgress" CssClass="txtField" runat="server" Enabled="false">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" class="SubmitCell">
                <asp:Button ID="btnRollback" runat="server" Text="Rollback" CssClass="PCGButton"
                    onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerUpload_btnRollback','S');"
                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerUpload_btnRollback','S');"
                    OnClick="btnRollback_Click" />
            </td>
        </tr>
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
    </table>
</div>

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

<%--    </ContentTemplate>
</asp:UpdatePanel>--%>