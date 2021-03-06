﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineIssueUpload.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineIssueUpload" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.2.6.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
<link href="/YUI/build/container/assets/container.css" rel="stylesheet" type="text/css" />
<link href="/YUI/build/menu/assets/skins/sam/menu.css" rel="stylesheet" type="text/css" />

<script src="/YUI/build/utilities/utilities.js" type="text/javascript"></script>

<script src="/YUI/build/container/container-min.js" type="text/javascript"></script>

<link href="../App_Themes/Maroon/Images/bubbletip.css" rel="stylesheet" type="text/css" />
<link href="../App_Themes/Maroon/Images/bubbletip-IE.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">

    function ValidatefileUpload() {
        var UploadDoc = document.getElementById('<%= FileUpload.ClientID  %>');
        var myfile = UploadDoc.value;
        var format = new Array();
        var Extension = myfile.substring(myfile.lastIndexOf('.') + 1);
        if (Extension == "csv" || Extension == "txt" || Extension == "TXT") {
            return true;
        }
        else {
            if (UploadDoc.value == '')
                alert('Please browse document to upload.');
            else
                alert('Invalid File Format File.');

            return false;
        }
    }

</script>

<script language="javascript" type="text/javascript">

    function DownloadScript() {
        btn = document.getElementById('<%= btnExportExcel.ClientID %>');
        btn.click();

    }
</script>

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

        else if (a == 'MFSS' && b == 'WPT') {
            var rdButton = document.getElementById('<%= File6.ClientID %>');
            rdButton.checked = true;
        }


    }

</script>

<style type="text/css">
    .table
    {
        border: 1px solid orange;
    }
    .leftLabel
    {
        width: 15%;
        text-align: right;
    }
    .rightData
    {
        width: 18%;
        text-align: left;
    }
    .rightDataTwoColumn
    {
        width: 25%;
        text-align: left;
    }
    .rightDataFourColumn
    {
        width: 50%;
        text-align: left;
    }
    .rightDataThreeColumn
    {
        width: 41%;
        text-align: left;
    }
    .tdSectionHeading
    {
        padding-bottom: 6px;
        padding-top: 6px;
        width: 100%;
    }
    .divSectionHeading table td span
    {
        padding-bottom: 5px !important;
    }
    .fltlft
    {
        float: left;
        padding-left: 3px;
        width: 20%;
    }
    .divCollapseImage
    {
        float: left;
        padding-left: 5px;
        width: 2%;
        float: right;
        text-align: right;
        cursor: pointer;
        cursor: hand;
    }
    .imgCollapse
    {
        background: Url(../Images/Section-Expand.png);
        cursor: pointer;
        cursor: hand;
    }
    .imgExpand
    {
        background: Url(../Images/Section-Collapse.png) no-repeat left top;
        cursor: pointer;
        cursor: hand;
    }
    .fltlftStep
    {
        float: left;
    }
    .StepOneContentTable, .StepTwoContentTable, .StageRequestTable, .StepThreeContentTable, .StepFourContentTable
    {
        width: 100%;
    }
    .SectionBody
    {
        width: 100%;
    }
    .collapse
    {
        text-align: right;
    }
    .divStepStatus
    {
        float: left;
        padding-left: 2px;
        padding-right: 5px;
    }
    .PCGLongButton
    {
        height: 26px;
    }
</style>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            Issue Upload
                        </td>
                    </tr>
                    
                    <tr>
                        <td  align="right">
                            <asp:LinkButton ID="lnkbtnpup" runat="server" Font-Size="X-Small" CausesValidation="False"
                                OnClientClick="return setRadioButtonForFileFormat();" OnClick="lnkbtnpup_Click1">click here to download standard file formats</asp:LinkButton>
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
                                TargetControlID="lnkbtnpup" DynamicServicePath="" BackgroundCssClass="modalBackground"
                                Enabled="True" OkControlID="btnOk" PopupDragHandleControlID="Panel1" CancelControlID="btnCancel"
                                Drag="true" OnOkScript="DownloadScript();">
                            </cc1:ModalPopupExtender>
                        </td>
                    </tr>
                </table>
                
            </div>
        </td>
    </tr>
    <tr id="tblMessagee" runat="server" visible="false">
        <td colspan="6">
            <table class="tblMessage">
                <tr>
                    <td align="center">
                        <div id="divMessage" align="center">
                        </div>
                        <div style="clear: both">
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table width="100%" id="TblResult" runat="server" visible="false" class="divPageHeading">
    <tr id="tr2" runat="server">
        <td class="leftLabel">
            <asp:Label ID="lblTotal" runat="server" CssClass="FieldName" Text="Total Uploaded orders:"></asp:Label>
        </td>
        <td class="rightData">
            <asp:Label ID="lblTotalVale" runat="server" CssClass="readOnlyField"></asp:Label>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblReject" runat="server" CssClass="FieldName" Text="Rejected Orders:"></asp:Label>
        </td>
        <td class="rightData">
            <asp:Label ID="lblRejectCountVale" runat="server" CssClass="readOnlyField"></asp:Label>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblAccpetedCount" runat="server" CssClass="FieldName" Text="Accepted Orders:"></asp:Label>
        </td>
        <td class="rightData">
            <asp:Label ID="lblAccpetedCountVale" runat="server" CssClass="readOnlyField"></asp:Label>
        </td>
    </tr>
</table>
<br />
<table width="100%">
    <tr id="trStepOneHeading" runat="server">
        <td class="tdSectionHeading" colspan="6">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                <div class="divSectionHeadingNumber fltlftStep">
                    1</div>
                <div class="fltlft">
                    <asp:Label ID="lblStep1" runat="server" Text="Product"></asp:Label>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:Label ID="lblType" runat="server" CssClass="FieldName" Text="Select Type:"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="2">--SELECT--</asp:ListItem>
                <asp:ListItem Text="Offline" Value="0" />
                <asp:ListItem Text="Online" Value="1" />
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlType"
                CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please select a Type" InitialValue="2"
                ValidationGroup="FileType">
            </asp:RequiredFieldValidator>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblProduct" runat="server" CssClass="FieldName" Text="Select Product:"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlProduct" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                <asp:ListItem Text="NCD/Bond" Value="FI" />
                <asp:ListItem Text="IPO" Value="IP" />
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="rfvProduct" runat="server" ControlToValidate="ddlProduct"
                CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please select a product" InitialValue="0"
                ValidationGroup="FileType">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftLabel" id="tdlblSubCategory" runat="server" visible="false">
            <asp:Label ID="lblSubCategory" runat="server" Text="Sub Category:" CssClass="FieldName"
                Visible="true"></asp:Label>
        </td>
        <td class="rightData" id="tdddSubCategory" runat="server" visible="false">
            <asp:DropDownList ID="ddSubCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                Visible="true" OnSelectedIndexChanged="ddSubCategory_OnSelectedIndexChanged">
                <asp:ListItem>All</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="leftLabel" id="tdlblSource" runat="server">
            <asp:Label ID="lblSource" runat="server" CssClass="FieldName" Text="Source Data:"></asp:Label>
        </td>
        <td class="rightData" id="tdddlSource" runat="server">
            <asp:DropDownList ID="ddlSource" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSource_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                <asp:ListItem Text="BSE" Value="BSE" />
                <asp:ListItem Text="NSE" Value="NSE" />
                <asp:ListItem Text="Internal Ops" Value="IOPS" />
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="rfvSource" runat="server" ErrorMessage="Please select Source Data"
                ControlToValidate="ddlSource" CssClass="rfvPCG" Display="Dynamic" InitialValue="0"
                ValidationGroup="FileType"></asp:RequiredFieldValidator>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblFileType" runat="server" CssClass="FieldName" Text="File Type:"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlFileType" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="OnSelectedIndexChanged_ddlFileType">
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="rfgFileType" runat="server" ErrorMessage="Please select a File Type"
                ControlToValidate="ddlFileType" CssClass="rfvPCG" Display="Dynamic" InitialValue="0"
                ValidationGroup="OnlineIssueUpload"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Select Issue:"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlIssueName" runat="server" AutoPostBack="true" CssClass="cmbField">
                <asp:ListItem Selected="True" Value="Select">--SELECT--</asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlIssueName"
                CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please select a IssueName"
                InitialValue="Select" ValidationGroup="OnlineIssueUpload">
            </asp:RequiredFieldValidator>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblAllotementType" runat="server" Text="Allotment Type :" CssClass="FieldName"
                Visible="false"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlAlltmntTyp" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAlltmntTyp_OnSelectedIndexChanged" Visible="false">
                <asp:ListItem Selected="True" Value="Select">--SELECT--</asp:ListItem>
                <asp:ListItem Value="WSR" Text="Advance"></asp:ListItem>
                <asp:ListItem Value="R1" Text="Basic"></asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="RFVddlAlltmnt" runat="server" ControlToValidate="ddlAlltmntTyp"
                CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please select Allotment Type"
                InitialValue="Select" ValidationGroup="OnlineIssueUpload" Enabled="false">
            </asp:RequiredFieldValidator>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblRgsttype" runat="server" Text="Register Type:" CssClass="FieldName"
                Visible="false"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlRgsttype" runat="server" AutoPostBack="true" CssClass="cmbField"
                Visible="false">
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="RFVRgsttype" runat="server" ControlToValidate="ddlRgsttype"
                CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please select Type of Register"
                InitialValue="Select" ValidationGroup="OnlineIssueUpload" Enabled="false">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trStep2" runat="server">
        <td class="tdSectionHeading" colspan="6">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                <div class="divSectionHeadingNumber fltlftStep">
                    2</div>
                <div class="fltlft">
                    <asp:Label ID="Label1" runat="server" Text="Product"></asp:Label>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:Label ID="lblUploadFile" runat="server" CssClass="FieldName" Text="Upload File: "></asp:Label>
        </td>
        <td class="rightData">
            <asp:FileUpload ID="FileUpload" runat="server" CssClass="FieldName" />
        </td>
        <td class="rightData">
            <asp:Button ID="btnFileUpload" CssClass="PCGLongButton" Text="Upload File" runat="server"
                OnClick="btnFileUpload_Click" ValidationGroup="OnlineIssueUpload" OnClientClick="if(ValidatefileUpload() == false) return false;ValidatefileUpload();" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Label ID="lblmsg" runat="server" CssClass="rfvPCG"></asp:Label>
        </td>
    </tr>
</table>
<table width="100%">
    <tr id="tr1" runat="server">
        <td class="tdSectionHeading">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                <div class="divSectionHeadingNumber fltlftStep">
                    3</div>
                <div class="fltlft">
                    <asp:Label ID="lblValidateData" runat="server" Text="Validate & Upload"></asp:Label>
                </div>
            </div>
        </td>
    </tr>
</table>
<asp:Panel ID="pnlOnlneIssueUpload" runat="server" Width="100%" ScrollBars="Horizontal"
    Visible="false">
    <telerik:RadGrid ID="gvOnlineIssueUpload" runat="server" AutoGenerateColumns="true"
        AllowPaging="true" AllowSorting="true" Skin="Telerik" EnableHeaderContextMenu="true"
        OnNeedDataSource="gvOnlineIssueUpload_OnNeedDataSource" OnItemDataBound="gvOnlineIssueUpload_ItemDataBound"
        GridLines="Both" EnableEmbeddedSkins="false" ShowFooter="true" PagerStyle-AlwaysVisible="true"
        EnableViewState="true" ShowStatusBar="true" AllowFilteringByColumn="true">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView Width="90%" AllowMultiColumnSorting="True" DataKeyNames="SN" AutoGenerateColumns="true"
            HeaderStyle-Width="120px" PageSize="20">
        </MasterTableView>
        <ClientSettings>
            <Resizing AllowColumnResize="true" />
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
        </ClientSettings>
        <FilterMenu EnableEmbeddedSkins="false">
        </FilterMenu>
    </telerik:RadGrid>
    <telerik:RadGrid ID="gvAllotmentUploadData" runat="server" AutoGenerateColumns="true"
        AllowPaging="true" AllowSorting="true" Skin="Telerik" EnableHeaderContextMenu="true"
        GridLines="Both" EnableEmbeddedSkins="false" ShowFooter="true" PagerStyle-AlwaysVisible="true"
        EnableViewState="true" ShowStatusBar="true" AllowFilteringByColumn="true" Visible="false"
        OnNeedDataSource="gvAllotmentUploadData_OnNeedDataSource" OnItemDataBound="gvAllotmentUploadData_ItemDataBound">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView Width="90%" AllowMultiColumnSorting="True" AutoGenerateColumns="true"
            HeaderStyle-Width="120px" PageSize="20">
        </MasterTableView>
        <ClientSettings>
            <Resizing AllowColumnResize="true" />
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
        </ClientSettings>
        <FilterMenu EnableEmbeddedSkins="false">
        </FilterMenu>
    </telerik:RadGrid>
</asp:Panel>
<table width="100%">
    <tr>
        <td class="leftLabel">
            <asp:Label ID="lblUploadData" runat="server" Text="Upload Data: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:Button ID="btnUploadData" runat="server" Text="Upload Data:" CssClass="PCGLongButton"
                OnClick="btnUploadData_Click" />
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:Panel ID="Panel1" runat="server" CssClass="ModelPup" Visible="false" Width="450px">
                <asp:RadioButton ID="File3" Text="IPO-Offline" Checked="false" GroupName="colors"
                    runat="server" />
                <br />
                <asp:RadioButton ID="File4" Text="NCD-Offline" Checked="false" GroupName="colors"
                    runat="server" />
                <br />
                <asp:RadioButton ID="File5" Text="IPO-Online" Checked="false" GroupName="colors"
                    runat="server" />
                <br />
                <asp:RadioButton ID="File6" Text="NCD-Online" Checked="false" GroupName="colors"
                    runat="server" />
                <br />
                <asp:RadioButton ID="File15" Text="54EC-Offline" Checked="false" GroupName="colors"
                    runat="server" />
                <br />
                <asp:RadioButton ID="File16" Text="CFD-Offline" Checked="false" GroupName="colors"
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
<asp:HiddenField ID="hdnsavePath" runat="server" />
<asp:HiddenField ID="hdnddlSubCategory" runat="server" />
<asp:HiddenField runat="server" ID="hdnUploadType" />
<asp:HiddenField runat="server" ID="hdnListCompany" />
