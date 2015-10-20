<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineOrderExtract.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineOrderExtract" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<asp:ScriptManager ID="scriptmanager" runat="server">
</asp:ScriptManager>
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
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            RTA Extract
                        </td>
                        <td align="right">
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
            <%--<div id="divValidation" class="e-msg" align="center" runat="server">
                <asp:ValidationSummary ID="vsValidations" runat="server" ValidationGroup="PreviewData" HeaderText="All fields are required" />
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
                    <asp:Label ID="lblStep1" runat="server" Text="Extract"></asp:Label>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
        <td colspan="2">
            <asp:Button ID="btnAutoOrder" runat="server" CssClass="PCGLongButton" Text="Auto SIP Order"
                OnClick="btnAutoOrder_Click" />
            &nbsp;&nbsp;&nbsp;
            <tr>
                <td class="leftLabel">
                    <asp:Label runat="server" class="FieldName" Text="Extract Type:" ID="Label1"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList CssClass="cmbField" ID="DropDownList1" runat="server" AutoPostBack="false">
                        <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                        <asp:ListItem Value="OTH">Normal Orders</asp:ListItem>
                        <asp:ListItem Value="SIP">SIP Orders</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG"
                        Display="Dynamic" ErrorMessage="Please select an Extract Type" ValidationGroup="ExtractData1"
                        ControlToValidate="DropDownList1" InitialValue="0">Please select an Extract Type</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Button ID="btnExtractData" runat="server" CssClass="PCGLongButton" Text="Extract Data"
                        OnClick="btnExtract_Click" ValidationGroup="ExtractData1" />
                </td>
        </td>
    </tr>
</table>
<table width="100%">
    <tr id="trStepTwoHeading" runat="server">
        <td class="tdSectionHeading" colspan="6">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                <div class="divSectionHeadingNumber fltlftStep">
                    2</div>
                <div class="fltlft">
                    <asp:Label ID="lblBulkDownload" runat="server" Text="Bulk File Download"></asp:Label>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
       
       
        <td class="leftLabel">
            <asp:Label runat="server" class="FieldName" Text="Extract Type:" ID="Label2"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList CssClass="cmbField" ID="DropDownList2" runat="server" AutoPostBack="false">
                <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                <asp:ListItem Value="OTH">Normal Orders</asp:ListItem>
                <asp:ListItem Value="SIP">SIP Orders</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="rfvPCG"
                Display="Dynamic" ErrorMessage="Please select an Extract Type" ValidationGroup="CreateFiles,BulkDownload"
                ControlToValidate="DropDownList2" InitialValue="0">Please select an Extract Type</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblOverwrite" class="FieldName" runat="server" Text="Over Write: "></asp:Label>
        </td>
        <td>
            <asp:CheckBox ID="chkOverwrite" runat="server" />
            <asp:Label ID="lblYes" class="FieldName" runat="server" Text="Yes"></asp:Label>
        </td>
        <td>
            <asp:Button ID="btnCreateFiles" runat="server" CssClass="PCGLongButton" Text="Create Files" ValidationGroup="CreateFiles"
                OnClick="btnCreateFiles_Click" />
            <asp:Label ID="lblCurrentDate" class="FieldName" runat="server" Text="(For current date only)"></asp:Label>
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td class="leftLabel">
            <asp:Label class="FieldName" ID="lblBulkDownloadDate" Text="Download Date: " runat="server" />
        </td>
        <td class="rightData">
            <telerik:RadDatePicker ID="rdpBulkDownloadDate" runat="server">
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator ID="rfvBulkDownDate" runat="server" CssClass="rfvPCG"
                ControlToValidate="rdpBulkDownloadDate" Display="Dynamic" ErrorMessage="Please select a download date"
                ValidationGroup="BulkDownload">Please select a download date</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:Button ID="btnBulkDownload" runat="server" CssClass="PCGLongButton" Text="Download"
                OnClick="btnBulkDownload_Click" ValidationGroup="BulkDownload" />
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
<table width="100%">
    <tr id="tr1" runat="server">
        <td class="tdSectionHeading" colspan="6">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                <div class="divSectionHeadingNumber fltlftStep">
                    3</div>
                <div class="fltlft">
                    <asp:Label ID="lblStep2" runat="server" Text="Individual Download"></asp:Label>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:Label runat="server" class="FieldName" Text="Product: " ID="lblProduct"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList CssClass="cmbField" ID="ddlProduct" runat="server" AutoPostBack="false">
                <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                <asp:ListItem Value="MF">Mutual Fund</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvProduct" runat="server" ErrorMessage="Please select a product"
                CssClass="rfvPCG" Display="Dynamic" InitialValue="0" ValidationGroup="PreviewData"
                ControlToValidate="ddlProduct">Please select a product</asp:RequiredFieldValidator>
        </td>
        <td class="leftLabel">
            <asp:Label runat="server" class="FieldName" Text="Product AMC: " ID="lblProductAmc"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList CssClass="cmbField" ID="ddlProductAmc" runat="server" AutoPostBack="false">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvProductAmc" runat="server" ErrorMessage="Please select a product"
                CssClass="rfvPCG" Display="Dynamic" InitialValue="0" ValidationGroup="PreviewData"
                ControlToValidate="ddlProductAmc">Please select a product</asp:RequiredFieldValidator>
        </td>
        <td colspan="2">
            <%--<asp:Button ID="btnAutoOrder" runat="server" CssClass="PCGLongButton" Text="Auto SIP Order"
                OnClick="btnAutoOrder_Click" />
            <asp:Button ID="btnExtractData" runat="server" CssClass="PCGLongButton" Text="Extract Data"
                OnClick="btnExtract_Click" CausesValidation="false" />--%>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:Label runat="server" class="FieldName" Text="Extract Type:" ID="lblExtractType"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList CssClass="cmbField" ID="ddlExtractType" runat="server" AutoPostBack="false">
                <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                <asp:ListItem Value="ALL">All</asp:ListItem>
                <asp:ListItem Value="OTH">Normal Orders</asp:ListItem>
                <asp:ListItem Value="SIP">SIP Orders</asp:ListItem>
                <asp:ListItem Value="NFO">NFO Orders</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvExtractType" runat="server" CssClass="rfvPCG"
                Display="Dynamic" ErrorMessage="Please select an Extract Type" ValidationGroup="PreviewData"
                ControlToValidate="ddlExtractType" InitialValue="0">Please select an Extract Type</asp:RequiredFieldValidator>
        </td>
        <td class="leftLabel">
            <asp:Label runat="server" class="FieldName" Text="R&T:" ID="lblRnT"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList CssClass="cmbField" ID="ddlRnT" runat="server" AutoPostBack="false">
                <asp:ListItem Value="CA">CAMS</asp:ListItem>
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvRnT" runat="server" CssClass="rfvPCG" Display="Dynamic"
                ErrorMessage="Please select an R&amp;T" ValidationGroup="PreviewData" ControlToValidate="ddlRnT"
                InitialValue="0">Please select an R&amp;T</asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:Label class="FieldName" ID="lblExtractDate" Text="Extract Date: " runat="server" />
        </td>
        <td class="rightData">
            <telerik:RadDatePicker ID="rdpExtractDate" runat="server">
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator ID="rfvExtractDate" runat="server" CssClass="rfvPCG"
                Display="Dynamic" ErrorMessage="Please select an extract date" ValidationGroup="PreviewData"
                ControlToValidate="rdpExtractDate">Please select an extract date</asp:RequiredFieldValidator>
        </td>
        <td class="leftLabel">
            <asp:Label runat="server" class="FieldName" Text="File Format:" ID="lblFileFormat"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList CssClass="cmbField" ID="ddlFileFormat" runat="server" AutoPostBack="false">
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                <asp:ListItem Value="dbf">DBF File</asp:ListItem>
                <asp:ListItem Value="txt">Text File</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvFileFormat" runat="server" CssClass="rfvPCG" Display="Dynamic"
                ErrorMessage="Please select a file format" ValidationGroup="ExtractData" ControlToValidate="ddlFileFormat"
                InitialValue="0">Please select a file format</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:Button ID="btnPreview" runat="server" CssClass="PCGLongButton" Text="1-Preview Data"
                OnClick="btnPreview_Click" ValidationGroup="PreviewData" />
            <asp:Button ID="btnGenerate" runat="server" CssClass="PCGLongButton" Text="2-Download File"
                OnClick="btnGenerateFile_Click" ValidationGroup="PreviewData" />
        </td>
        <td>
        </td>
    </tr>
</table>
<%--<table>
    <tr>
        <td id="td10" runat="server">
            <asp:Label runat="server" class="FieldName" Text="File format:" ID="Label6"></asp:Label>
        </td>
        <%--<td id="td11" runat="server">
            <asp:DropDownList CssClass="cmbField" ID="ddlFileFormat" runat="server" AutoPostBack="false"></asp:DropDownList>
        </td>
        <td id="td7" runat="server">
            <asp:Label runat="server" class="FieldName" Text="Location to store the file:" ID="Label4"></asp:Label>
        </td>
        <td id="td8" runat="server">
            <td>
                <asp:Label ID="lbllocation" runat="server" CssClass="FieldName"></asp:Label>
            </td>
        </td>
        <tr>
            <td id="td9" runat="server">
                <asp:Button ID="btnGenerate" runat="server" CssClass="PCGButton" Text="Generate file" OnClick="btnGenerateFile_Click" />
            </td>
        </tr>
    </tr>
</table>--%>
<asp:Panel ID="pnlExtractMIS" runat="server" class="Landscape" Width="100%" Visible="false"
    ScrollBars="Horizontal">
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="gvExtractMIS" runat="server" GridLines="None" AutoGenerateColumns="false"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" Width="102%"
                    AllowAutomaticInserts="false" OnItemDataBound="gvExtractMIS_ItemDataBound" OnNeedDataSource="gvExtractMIS_OnNeedDataSource"
                    ExportSettings-FileName="MF Order Extract">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="MF Order Extract"
                        Excel-Format="ExcelML">
                    </ExportSettings>
                    <MasterTableView Width="102%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                        CommandItemDisplay="None" DataKeyNames="CO_OrderId">
                        <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                            ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="CO_OrderId" AllowFiltering="true" HeaderText="OrderId"
                                UniqueName="" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_AMCCode" AllowFiltering="true" HeaderText="AMC Code"
                                UniqueName="AMFE_AMCCode" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="75px" FilterControlWidth="50px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_BrokerCode" HeaderText="Broker Code" AllowFiltering="true"
                                Visible="false" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                                UniqueName="AMFE_BrokerCode" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_SubBrokerCode" HeaderText="Sub Broker Code"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                                UniqueName="AMFE_SubBrokerCode" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_UserCode" AllowFiltering="true" HeaderText="User Code"
                                UniqueName="AMFE_UserCode" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_UserTrxnNo" AllowFiltering="false" HeaderText="User Trxn No"
                                UniqueName="AMFE_UserTrxnNo" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_ApplicationNumber" AllowFiltering="false"
                                HeaderText="Application Number" UniqueName="AMFE_ApplicationNumber" SortExpression=""
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_FolioNumber" HeaderText="Folio Number" AllowFiltering="true"
                                SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="AMFE_FolioNumber" FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="300px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_CheckDigitNumber" HeaderText="Check Digit Number"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_CheckDigitNumber"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_TrxnType" HeaderText="Trxn Type" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_TrxnType" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_SchemeCode" HeaderText="Scheme Code" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_SchemeCode" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_InvestorFirstName" HeaderText="Investor First Name"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_InvestorFirstName"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_JointName1" HeaderText="Joint Name 1" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_JointName1" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_JointName2" HeaderText="Joint Name 2" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_JointName2" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_AddressLine1" HeaderText="Address Line 1"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_AddressLine1"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_AddressLine2" HeaderText="Address Line 2"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_AddressLine2"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_AddressLine3" HeaderText="Address Line 3"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_AddressLine3"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_City" HeaderText="City" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_City" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Pincode" HeaderText="Pincode" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_Pincode" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_PhoneOffice" HeaderText="Phone Office" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_PhoneOffice" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_TransactionDate" HeaderText="Transaction Date"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_TransactionDate"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_TransactionTime" HeaderText="Transaction Time"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_TransactionTime"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Units" HeaderText="Units" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_Units" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Amount" HeaderText="Amount" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_Amount" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_CloseAC" HeaderText="Close AC" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_CloseAC" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_DateofBirth" HeaderText="Date of Birth"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_DateofBirth"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_GuardianName" HeaderText="Guardian Name"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_GuardianName"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_PanNo" HeaderText="PAN NO" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_PanNo" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_PhoneResidence" HeaderText="Phone Residence"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_PhoneResidence"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_FaxOffice" HeaderText="Fax Office" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_FaxOffice" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_FaxResidence" HeaderText="Fax Residence"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_FaxResidence"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_EmailID" HeaderText="Email ID" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_EmailID" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_AccountNumber" HeaderText="Account Number"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_AccountNumber"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_AccountType" HeaderText="Account Type" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_AccountType" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_BankName" HeaderText="Bank Name" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_BankName" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_BranchName" HeaderText="Branch Name" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_BranchName" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_BankCity" HeaderText="Bank City" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_BankCity" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_ReinvestOption" HeaderText="Reinvest Option"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_ReinvestOption"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_HoldingNature" HeaderText="Holding Nature"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_HoldingNature"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_OccupationCode" HeaderText="Occupation Code"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_OccupationCode"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_TaxStatusCode" HeaderText="Tax Status Code"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_TaxStatusCode"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Remarks" HeaderText="Remarks" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_Remarks" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_State" HeaderText="State" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_State" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_SubTrxnType" HeaderText="Sub Trxn Type"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_SubTrxnType"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_DivPayoutMechanism" HeaderText="Div Payout Mechanism"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_DivPayoutMechanism"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_ECSNumber" HeaderText="ECS Number" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_ECSNumber" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_BankCode" HeaderText="Bank Code" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_BankCode" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_AltFolioNumber" HeaderText="Alt Folio Number"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_AltFolioNumber"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_AltBrokerCode" HeaderText="Alt Broker Code"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_AltBrokerCode"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_LocationCode" HeaderText="Location Code"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_LocationCode"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_RedPayoutMechanism" HeaderText="Red Payout Mechanism"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_RedPayoutMechanism"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Pricing" HeaderText="Pricing" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_Pricing" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_JointHolder1Pan" HeaderText="Joint Holder 1 PAN"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_JointHolder1Pan"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_JointHolder2Pan" HeaderText="Joint Holder 2 PAN"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_JointHolder2Pan"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_NomineeName" HeaderText="Nominee Name" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_NomineeName" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_NomineeRelation" HeaderText="Nominee Relation"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_NomineeRelation"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_GuardianPAN" HeaderText="Guardian PAN" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_GuardianPAN" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_InstrmNo" HeaderText="Instrm No" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_InstrmNo" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_UinNo" HeaderText="UIN Number" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_UinNo" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_PANValid" HeaderText="PAN Valid" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_PANValid" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_GuardianPanValid" HeaderText="Guardian PAN Valid"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_GuardianPanValid"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_JH1PanValid" HeaderText="JH1 PAN Valid"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_JH1PanValid"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_JH2PanValid" HeaderText="JH2 PAN Valid"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_JH2PanValid"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_SIPRegisteredDate" HeaderText="SIP Registered Date"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_SIPRegisteredDate"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_FH_Min" HeaderText="FH_MIN" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_FH_Min" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Jh1_Min" HeaderText="JH1_MIN" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_Jh1_Min" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Jh2_Min" HeaderText="JH2_min" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_Jh2_Min" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Guard_min" HeaderText="Guard_min" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_Guard_min" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Neft_Code" HeaderText="NEFT_CODE" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_Neft_Code" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Rtgs_Code" HeaderText="RTGS_CODE" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_Rtgs_Code" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Email_Acst" HeaderText="EMAIL_ACST" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_Email_Acst" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Mobile_No" HeaderText="MOBILE_NO" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_Mobile_No" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Dp_Id" HeaderText="DP_ID" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_Dp_Id" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Poa_Type" HeaderText="POA_type" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_Poa_Type" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_TrxnMode" HeaderText="Trxn Mode" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_TrxnMode" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Trxn_Sign_Confn" HeaderText="Trxn_Sign_Confn"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_Trxn_Sign_Confn"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Addl_Address1" HeaderText="Addl_Address1"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_Addl_Address1"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Addl_Address2" HeaderText="Addl_Address2"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_Addl_Address2"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Addl_Address3" HeaderText="Addl_Address3"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_Addl_Address3"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Addl_Address1_City" HeaderText="City" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_Addl_Address1_City" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Addl_Address1_State" HeaderText="State"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_Addl_Address1_State"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Addl_Address1_Country" HeaderText="Country"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_Addl_Address1_Country"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Addl_Address1_Pincode" HeaderText="Pincode"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_Addl_Address1_Pincode"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Nom1_Applicable(%)" HeaderText="Nom1_Applicable(%)"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_Nom1_Applicable(%)"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Nom2_Name" HeaderText="Nom2_Name" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_Nom2_Name" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Nom2_Relationship" HeaderText="Nom2_Relationship"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_Nom2_Relationship"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Nom2_Applicable(%)" HeaderText="Nom2_Applicable(%)"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="Nom2_Applicable(%)"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Nom3_Name" HeaderText="Nom3_Name" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_Nom3_Name" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Nom3_Relationship" HeaderText="Nom3_Relationship"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_Nom3_Relationship"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Nom3_Applicable(%)" HeaderText="Nom3_Applicable(%)"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="Nom3_Applicable(%)"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Check_Flag" HeaderText="Check_Flag" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_Check_Flag" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Third_Party_Payment" HeaderText="Third_Party_Payment"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_Third_Party_Payment"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_KYC_Status" HeaderText="KYC_Status" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_KYC_Status" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_FIRCStatus" HeaderText="FIRC Status" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_FIRCStatus" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_SIPRegistrationNumber" HeaderText="SIP Registration Number"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_SIPRegistrationNumber"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_NoOfInstallments" HeaderText="No Of Installments"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_NoOfInstallments"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_SIPFrequency" HeaderText="SIP Frequency"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_SIPFrequency"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_StartDate" HeaderText="Start Date" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_StartDate" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_EndDate" HeaderText="End Date" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_EndDate" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_InstallmentNumber" HeaderText="Installment Number"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_InstallmentNumber"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_NomineeDateofBirth" HeaderText="Nominee Date-of-Birth"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_NomineeDateofBirth"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_NomineeMinorFlag" HeaderText="Nominee Minor Flag"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_NomineeMinorFlag"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_NomineeGaurdianName" HeaderText="Nominee Gaurdian Name"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_NomineeGaurdianName"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_FirstHolderPanExempt" HeaderText="First Holder PAN Exempt"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_FirstHolderPanExempt"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_JH1PanExempt" HeaderText="JH1 Pan Exempt"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_JH1PanExempt"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_JH2PanExempt" HeaderText="JH2 Pan Exempt"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_JH2PanExempt"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_GuardianPANExempt" HeaderText="Guardian PAN Exempt"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_GuardianPANExempt"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_FirstHolderExemptCategory" HeaderText="First Holder-Exempt Category"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_FirstHolderExemptCategory"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Jh1ExemptCategory" HeaderText="JH 1- Exempt Category"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_Jh1ExemptCategory"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Jh2ExemptCategory" HeaderText="JH 2- Exempt Category"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_Jh2ExemptCategory"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_GaurdianExemptCategory" HeaderText="Gaurdian Exempt Category"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_GaurdianExemptCategory"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Fh_KraExemptReferenceNo" HeaderText="FH_KRA Exempt Reference No"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_Fh_KraExemptReferenceNo"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Jh1_KraExemptReferenceNo" HeaderText="JH1_KRA Exempt Reference No"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_Jh1_KraExemptReferenceNo"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Jh2_KraExemptReferenceNo" HeaderText="JH2_KRA Exempt Reference No"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_Jh2_KraExemptReferenceNo"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_GaurdianExemptReferenceNo" HeaderText="Gaurdian Exempt Reference No"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_GaurdianExemptReferenceNo"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_EuinOpted" HeaderText="EUIN Opted" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_EuinOpted" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_Euin" HeaderText="EUIN " AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AMFE_Euin" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_NominationNotOpted" HeaderText="Nomination not opted"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_NominationNotOpted"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_SubbrokerARNcode" HeaderText="Sub broker ARN code"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="AMFE_SubbrokerARNcode"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
