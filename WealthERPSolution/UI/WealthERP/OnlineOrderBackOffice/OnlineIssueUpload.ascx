<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineIssueUpload.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineIssueUpload" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script src="../Scripts/JScript.js" type="text/javascript"></script>
<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.2.6.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>


<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

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
<%--<asp:UpdatePanel ID="upIssueExtract" runat="server">
    <ContentTemplate>--%>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            Online Issue Upload
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table id="tblMessage" width="100%" runat="server" visible="false">
    <tr id="trSumbitSuccess">
        <td align="center">
          <div id="msgRecordStatus" class="success-msg" align="center" runat="server">
            
            </div>
            <%--    <asp:LinkButton ID="lnkClick" runat="server" Text="Click here to start new upload"
                Font-Size="Small" Font-Underline="false" class="textfield" OnClick="lnkClick_Click" Visible="false"></asp:LinkButton>--%>
                 <%--<div id="divValidationError" runat="server" class="failure-msg" align="center" visible="true">
                        <asp:ValidationSummary ID="vsSummary" runat="server" Visible="true" ValidationGroup="btnSubmit"
                            ShowSummary="true" DisplayMode="BulletList" />
                    </div>--%>
        </td>
    </tr>
</table>
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
    <td class="leftLabel"> <asp:Label ID="lblType" runat="server" CssClass="FieldName" Text="Select Type:"></asp:Label></td>
    <td class="rightData"> 
    <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField">
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
    </tr>
    <tr>
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
        <td class="leftLabel">
            <asp:Label ID="lblSource" runat="server" CssClass="FieldName" Text="Source Data:"></asp:Label>
        </td>
        <td class="rightData">
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
            <asp:DropDownList ID="ddlFileType" runat="server" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_ddlFileType">
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
            <asp:DropDownList ID="ddlIssueName" runat="server" CssClass="cmbField">
                <asp:ListItem Selected="True" Value="Select">--SELECT--</asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlIssueName"
                CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please select a IssueName"
                InitialValue="Select" ValidationGroup="OnlineIssueUpload">
            </asp:RequiredFieldValidator>
        </td>
        <td class="leftLabel">
       <asp:Label ID="lblAllotementType" runat="server" Text="Allotment Type :" CssClass="FieldName" Visible="false"></asp:Label>
        </td>
        <td class="rightData">
         <asp:DropDownList ID="ddlAlltmntTyp"  runat="server" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlAlltmntTyp_OnSelectedIndexChanged" Visible="false">
          <asp:ListItem  Selected="True" Value="Select">--SELECT--</asp:ListItem>
          <asp:ListItem  Value="WSR" Text="Advance" Enabled="false"></asp:ListItem>
          <asp:ListItem Value="R1" Text="Basic"></asp:ListItem>
         </asp:DropDownList>
          <br />
            <asp:RequiredFieldValidator ID="RFVddlAlltmnt" runat="server" ControlToValidate="ddlAlltmntTyp"
                CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please select Allotment Type"
                InitialValue="Select" ValidationGroup="OnlineIssueUpload" Enabled="false">
            </asp:RequiredFieldValidator>
        </td >
        <td class="leftLabel">
        <asp:Label ID="lblRgsttype" runat="server" Text="Register Type:" CssClass="FieldName" Visible="false"></asp:Label>
        </td>
       <td>
       <asp:DropDownList ID="ddlRgsttype" runat="server" AutoPostBack="true" CssClass="cmbField" Visible="false">
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
        <MasterTableView Width="90%" AllowMultiColumnSorting="True"  AutoGenerateColumns="true"
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
<asp:HiddenField ID="hdnsavePath" runat="server"  />
<%--    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="FileUpload" />
        <asp:PostBackTrigger ControlID="btnFileUpload" />        
    </Triggers>
</asp:UpdatePanel>--%>
