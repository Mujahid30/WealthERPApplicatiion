<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineIssueExtract.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineIssueExtract" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<script language="javascript" type="text/javascript">
    function show() {
        if (document.getElementById('ctrl_OnlineIssueExtract_msgRecordStatus').style.display == 'none') {
            document.getElementById('ctrl_OnlineIssueExtract_msgRecordStatus').style.display = 'block';
        }
        return false;
    }
    function hide() {
        if (document.getElementById('ctrl_OnlineIssueExtract_msgRecordStatus').style.display == 'block') {
            document.getElementById('ctrl_OnlineIssueExtract_msgRecordStatus').style.display = 'none';
        }
        return false;
    }
</script>

<style type="text/css">
    .table
    {
        border: 1px solid orange;
    }
    .leftLabel
    {
        width: 18%;
        text-align: right;
    }
    .rightData
    {
        width: 15%;
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
<asp:UpdatePanel ID="upIssueExtract" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td>
                    <div class="divPageHeading">
                        <table cellspacing="0" cellpadding="2" width="100%">
                            <tr>
                                <td align="left">
                                    Online Issue Extract
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgexportButton" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                        Visible="false" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                        OnClick="btnExportData_OnClick" OnClientClick="setFormat('excel')" Height="22px"
                                        Width="25px"></asp:ImageButton>
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
                    <%-- <asp:LinkButton ID="lnkClick" runat="server" Text="Click here to start new Extract"
                Font-Size="Small" Font-Underline="false" class="textfield" OnClick="lnkClick_Click" Visible="false"></asp:LinkButton>--%>
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
                <td class="leftLabel" style="width: 15.5%">
                    <asp:Label ID="lblproduct" runat="server" CssClass="FieldName" Text="Select Product:"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlProduct" runat="server" CssClass="cmbField" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                        <asp:ListItem Text="NCD/Bond" Value="FI" />
                        <asp:ListItem Text="IPO" Value="IP" />
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvProduct" runat="server" CssClass="rfvPCG" ErrorMessage="Please select a product"
                        ControlToValidate="ddlProduct" Display="Dynamic" InitialValue="0" ValidationGroup="IssueExtract">Please select a product</asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="rfvProductFileType" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please select a product" ControlToValidate="ddlProduct" Display="Dynamic"
                        InitialValue="0" ValidationGroup="onlineIssueExtract">Please select a product</asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel" style="width:15%">
                    <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Select Issue:"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlIssueName" runat="server" Width="450px" CssClass="cmbLongField"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlIssueName_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="Select">--SELECT--</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlIssueName"
                        CssClass="rfvPCG" Display="Dynamic" ErrorMessage="Please select a IssueName"
                        InitialValue="Select" ValidationGroup="onlineIssueExtract">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="rightData" colspan="3">
                    <asp:Button ID="btnIssueExtract" runat="server" CssClass="PCGLongButton" Text="Extract"
                        OnClick="btnIssueExtract_Click" ValidationGroup="onlineIssueExtract" />
                </td>
            </tr>
        </table>
        <%--  ValidationGroup="IssueExtract" --%>
        <%--ValidationGroup="FileType"--%>
        <table width="100%">
            <tr>
                <td class="tdSectionHeading" colspan="6">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        <div class="divSectionHeadingNumber fltlftStep">
                            2</div>
                        <div class="fltlft">
                            <asp:Label ID="Label1" runat="server" Text="NCD/IPO Extract"></asp:Label>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="leftLabel" style="width:10%">
                    <asp:Label ID="lblDWNData" runat="server" CssClass="FieldName" Text="Download Data:"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlExternalSource" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlExternalSource_SelectedIndexChanged">
                        <%--<asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                        <asp:ListItem Text="BSE" Value="BSE" />
                        <asp:ListItem Text="NSE" Value="NSE" />
                        <asp:ListItem Text="Internal Ops" Value="IOPS" />--%>
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvDownload" runat="server" ErrorMessage="Please select a download"
                        ControlToValidate="ddlExternalSource" CssClass="rfvPCG" Display="Dynamic" InitialValue="0"
                        ValidationGroup="IssueExtract">Please select a download</asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel" style="width:9%">
                    <asp:Label ID="lblFileType" runat="server" CssClass="FieldName" Text="File Type:"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlFileType" runat="server" CssClass="cmbLongField" OnSelectedIndexChanged="OnSelectedIndexChanged_ddlFileType" AutoPostBack="true">
                        <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvFileType" runat="server" ErrorMessage="Please select a file type"
                        ControlToValidate="ddlFileType" CssClass="rfvPCG" Display="Dynamic" InitialValue="0"
                        ValidationGroup="IssueExtract">Please select a file type</asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel" style="width:12%">
                    <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Date:"></asp:Label>
                </td>
                <td class="rightData">
                    <telerik:RadDatePicker ID="rdpDownloadDate" CssClass="txtField" runat="server" AutoPostBack="false"
                        Skin="Telerik" EnableEmbeddedSkins="false">
                        <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                            ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                        </Calendar>
                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvDOwnloadDate" runat="server" ErrorMessage="Please select a valid date"
                        Display="Dynamic" ControlToValidate="rdpDownloadDate" Text="Please select a valid date"
                        CssClass="rfvPCG" ValidationGroup="IssueExtract">Please select a valid date</asp:RequiredFieldValidator>
                </td>
                <%-- <td class="leftLabel" colspan="2">&nbsp;</td>--%>
            </tr>
            <tr>
                <%-- <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>--%>
                <%-- <td class="rightData">
                    <asp:Button ID="btnIssueExtract" runat="server" CssClass="PCGLongButton" Text="NCD Extract"
                        OnClick="btnIssueExtract_Click" ValidationGroup="IssueExtract" />
                </td>--%>
                <%--<td class="rightData">
                    <asp:Label ID="lblCurrentDate" CssClass="FieldName" runat="server" Text="(For current date only)"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>--%>
            </tr>
        </table>
        <table width="100%">
            <tr id="tr2" runat="server">
                <td class="tdSectionHeading" colspan="6">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        <div class="divSectionHeadingNumber fltlftStep">
                            3</div>
                        <div class="fltlft">
                            <asp:Label ID="lblDownloads" runat="server" Text="Downloads"></asp:Label>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <%-- <td class="leftLabel">
                    <telerik:RadDatePicker ID="rdpDownloadDate" CssClass="txtField" runat="server" AutoPostBack="false"
                        Skin="Telerik" EnableEmbeddedSkins="false">
                        <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                            ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                        </Calendar>
                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvDOwnloadDate" runat="server" ErrorMessage="Please select a valid date"
                        Display="Dynamic" ControlToValidate="rdpDownloadDate" Text="Please select a valid date"
                        CssClass="rfvPCG" ValidationGroup="IssueExtract">Please select a valid date</asp:RequiredFieldValidator>
                </td>--%>
                <td width="15%" align="right">
                    <asp:Button ID="btnPreview" runat="server" Text="Preview Data" ValidationGroup="IssueExtract"
                        CssClass="PCGLongButton" OnClick="btnPreview_Click" />
                </td>
                <td  width="10%">
                    <asp:Button ID="btnDownload" runat="server" Text="Download File" ValidationGroup="IssueExtract"
                        CssClass="PCGLongButton" OnClick="btnDownload_Click" />
                </td>
                <td align="left">
                   <asp:Label ID="lblMsg" runat="server" CssClass="rfvPCG"></asp:Label>
                </td>
                
            </tr>
        </table>
        <asp:Panel ID="pnlOnlneIssueExtract" runat="server" Width="100%" ScrollBars="Horizontal"
            Visible="false"  >
            <tr>
                <td>
                    <telerik:RadGrid ID="gvOnlineIssueExtract" runat="server" AutoGenerateColumns="true"
                        AllowPaging="true" AllowSorting="true" Skin="Telerik" EnableHeaderContextMenu="true"
                        OnNeedDataSource="gvOnlineIssueExtract_OnNeedDataSource" GridLines="Both" EnableEmbeddedSkins="false"
                        ShowFooter="true" PagerStyle-AlwaysVisible="true" EnableViewState="true" ShowStatusBar="true"
                        AllowFilteringByColumn="true"  PageSize="5">
                        <ExportSettings HideStructureColumns="true">
                        </ExportSettings>
                        <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="true"
                            HeaderStyle-Width="120px" >
                        </MasterTableView>
                        <ClientSettings>
                            <Resizing AllowColumnResize="true" />
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        </ClientSettings>
                        <FilterMenu EnableEmbeddedSkins="false">
                        </FilterMenu>
                    </telerik:RadGrid>
                </td>
            </tr>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnDownload" />
    </Triggers>
</asp:UpdatePanel>
