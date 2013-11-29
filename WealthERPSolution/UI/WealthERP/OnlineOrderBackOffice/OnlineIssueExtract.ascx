<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineIssueExtract.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineNCDExtract" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
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
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            Online NCD Extract
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
            <div id="msgRecordStatus" class="success-msg" align="center" runat="server"></div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr id="trStepOneHeading" runat="server">
        <td class="tdSectionHeading" colspan="6">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                <div class="divSectionHeadingNumber fltlftStep">1</div>
                <div class="fltlft">
                    <asp:Label ID="lblStep1" runat="server" Text="Product"></asp:Label>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:Label ID="lblproduct" runat="server" CssClass="FieldName" Text="Select Product:"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlProduct" runat="server" CssClass="cmbField">
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                <asp:ListItem Text="NCD" Value="1" />
                <asp:ListItem Text="IPO" Value="2" />
            </asp:DropDownList>            
        </td>
        <td class="rightData">
            <asp:RequiredFieldValidator ID="rfvProduct" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please select a product" ControlToValidate="ddlProduct" 
                Display="Dynamic" InitialValue="0" ValidationGroup="NcdExtract">Please select a product</asp:RequiredFieldValidator></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td class="tdSectionHeading" colspan="6">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                <div class="divSectionHeadingNumber fltlftStep">2</div>
                <div class="fltlft">
                    <asp:Label ID="Label1" runat="server" Text="NCD Extract"></asp:Label>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td class="rightData">
            <asp:Button ID="btnNcdExtract" runat="server" CssClass="PCGLongButton" Text="NCD Extract"
                OnClick="btnNcdExtract_Click" ValidationGroup="NcdExtract" /></td>
        <td class="rightData">
            <asp:Label ID="lblCurrentDate" CssClass="FieldName" runat="server" Text="(For current date only)"></asp:Label></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
</table>
<table width="100%">
    <tr id="tr2" runat="server">
        <td class="tdSectionHeading" colspan="6">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                <div class="divSectionHeadingNumber fltlftStep">3</div>
                <div class="fltlft">
                    <asp:Label ID="lblDownloads" runat="server" Text="Downloads"></asp:Label>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:Label ID="lblDWNData" runat="server" CssClass="FieldName" Text="Download Data:"></asp:Label></td>
        <td class="rightData">
            <asp:DropDownList ID="ddlDWNData" runat="server" CssClass="cmbField">
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                <asp:ListItem Text="BSE" Value="1" />
                <asp:ListItem Text="NSE" Value="2" />
            </asp:DropDownList><br />
            <asp:RequiredFieldValidator ID="rfvDownload" runat="server" 
                ErrorMessage="Please select a download" ControlToValidate="ddlDWNData" 
                CssClass="rfvPCG" Display="Dynamic" InitialValue="0" 
                ValidationGroup="NcdExtract">Please select a download</asp:RequiredFieldValidator></td>
        <td class="leftLabel">
            <asp:Label ID="lblDownloadDate" Text="Select Date:" CssClass="FieldName" runat="server"></asp:Label></td>
        <td class="rightData">
            <telerik:RadDatePicker ID="rdpDownloadDate" CssClass="txtField" runat="server"
                AutoPostBack="false" Skin="Telerik" EnableEmbeddedSkins="false">
                <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                        Skin="Telerik" EnableEmbeddedSkins="false"></Calendar>
                <DateInput runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy"></DateInput>
            </telerik:RadDatePicker><br />
            <asp:RequiredFieldValidator ID="rfvDOwnloadDate" runat="server" 
                ErrorMessage="Please select a valid date" Display="Dynamic" ControlToValidate="rdpDownloadDate" Text="Please select a valid date" CssClass="rfvPCG" ValidationGroup="NcdExtract">Please select a valid date</asp:RequiredFieldValidator></td>
        <td class="rightData">
            <asp:Button ID="btnGo" runat="server" Text="Preview" ValidationGroup="NcdExtract" CssClass="PCGButton" OnClick="btnGo_Click" /></td>
        <td>&nbsp;</td>
    </tr>
</table>
<asp:Panel ID="pnlOnlneNCDExtract" runat="server" Width="100%" ScrollBars="Horizontal">
    <tr>
        <td>
            <telerik:RadGrid ID="gvOnlneNCDExtract" runat="server" fAllowAutomaticDeletes="false"
                EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true"
                GridLines="none" AllowAutomaticInserts="false" Skin="Telerik" EnableHeaderContextMenu="true"
                OnNeedDataSource="gvOnlneNCDExtract_OnNeedDataSource">
                <ExportSettings HideStructureColumns="true"></ExportSettings>
                <MasterTableView DataKeyNames="AIOE_ExtractionDate" Width="100%" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false" PageSize="20">
                    <Columns>
                        <telerik:GridBoundColumn DataField="XS_StatusCode" UniqueName="XS_StatusCode" HeaderText="StatusCode"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                            SortExpression="XS_StatusCode" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="C_CustCode" UniqueName="C_CustCode" HeaderText="CustCode"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                            SortExpression="C_CustCode" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CO_OrderId" UniqueName="CO_OrderId" HeaderText="OrderId"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="100px"
                            SortExpression="CO_OrderId" FilterControlWidth="80px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AIOE_ExtractionDate" UniqueName="AIOE_ExtractionDate"
                            HeaderText="ExtractionDate" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="67px" SortExpression="AIOE_ExtractionDate"
                            FilterControlWidth="50px" CurrentFilterFunction="Contains" Visible="true">
                            <ItemStyle Width="67px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AIM_IssueName" UniqueName="AIM_IssueName" HeaderText="IssueName"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="80px"
                            SortExpression="AIM_IssueName" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="70px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="true" DataField="CO_ApplicationNumber" UniqueName="CO_ApplicationNumber"
                            HeaderText="ApplicationNumber" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="CO_ApplicationNumber"
                            FilterControlWidth="80px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AIOE_SubBroker" UniqueName="AIOE_SubBroker" HeaderText="SubBroker"
                            SortExpression="AIOE_SubBroker" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                            FilterControlWidth="80px" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            <ItemStyle Width="80px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AIOE_PersonStatus" UniqueName="AIOE_PersonStatus"
                            HeaderText="PersonStatus" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="AIOE_PersonStatus"
                            FilterControlWidth="80px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AIOE_FirstApplicantName" UniqueName="AIOE_FirstApplicantName"
                            HeaderText="FirstApplicantName" AutoPostBackOnFilter="true" SortExpression="AIOE_FirstApplicantName"
                            ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="100px" FilterControlWidth="80px">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AIOE_FirstApplicantAge" UniqueName="AIOE_FirstApplicantAge"
                            HeaderText="FirstApplicantAge" SortExpression="AIOE_FirstApplicantAge" AutoPostBackOnFilter="true"
                            ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="100px" FilterControlWidth="80px"
                            CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="true" DataField="AIOE_FirstApplicantDOB" UniqueName="AIOE_FirstApplicantDOB"
                            HeaderText="FirstApplicantDOB" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="AIOE_FirstApplicantDOB"
                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="true" DataField="C_Adr1Line1_FirstApplicant" UniqueName="C_Adr1Line1_FirstApplicant"
                            HeaderText="1 Address FirstApplicant" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="C_Adr1Line1_FirstApplicant"
                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="C_Adr1Line2_FirstApplicant" UniqueName="C_Adr1Line2_FirstApplicant"
                            HeaderText="2 Address FirstApplicant" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="C_Adr1Line2_FirstApplicant"
                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="C_Adr1Line3_FirstApplicant" UniqueName="C_Adr1Line3_FirstApplicant"
                            HeaderText="3RD Address FirstApplicant" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="C_Adr1Line3_FirstApplicant"
                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="C_Adr1PinCode_FirstApplicant" UniqueName="C_Adr1PinCode_FirstApplicant"
                            HeaderText="PinCode" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                            HeaderStyle-Width="100px" SortExpression="C_Adr1PinCode_FirstApplicant" FilterControlWidth="80px"
                            CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="C_PANNum_FirstApplicant" UniqueName="C_PANNum_FirstApplicant"
                            HeaderText="PAN Num" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                            HeaderStyle-Width="67px" SortExpression="C_PANNum_FirstApplicant" FilterControlWidth="50px"
                            CurrentFilterFunction="Contains" Visible="true">
                            <ItemStyle Width="67px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AIOE_ClientCategory" UniqueName="AIOE_ClientCategory"
                            HeaderText="ClientCategory" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="80px" SortExpression="AIOE_ClientCategory"
                            FilterControlWidth="50px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="70px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="true" DataField="AIOE_ClientSubCategory" UniqueName="AIOE_ClientSubCategory"
                            HeaderText="Client SubCategory" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="AIOE_ClientSubCategory"
                            FilterControlWidth="80px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AIOE_SecondApplicantName" UniqueName="AIOE_SecondApplicantName"
                            HeaderText="Second Applicant Name" SortExpression="AIOE_SecondApplicantName"
                            AutoPostBackOnFilter="true" HeaderStyle-Width="100px" FilterControlWidth="80px"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            <ItemStyle Width="80px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AIOE_SecondApplicantAge" UniqueName="AIOE_SecondApplicantAge"
                            HeaderText="Second Applicant Age" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="AIOE_SecondApplicantAge"
                            FilterControlWidth="80px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AIOE_ThirdApplicantName" UniqueName="AIOE_ThirdApplicantName"
                            HeaderText="Third Applicant Name" AutoPostBackOnFilter="true" SortExpression="AIOE_ThirdApplicantName"
                            ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="100px" FilterControlWidth="80px">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AIOE_ThirdApplicantAge" UniqueName="AIOE_ThirdApplicantAge"
                            HeaderText="Third Applicant Age" SortExpression="AIOE_ThirdApplicantAge" AutoPostBackOnFilter="true"
                            ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="100px" FilterControlWidth="80px"
                            CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="true" DataField="CEDA_DPId" UniqueName="CEDA_DPId"
                            HeaderText="Depository ID" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="CEDA_DPId" FilterControlWidth="70px"
                            CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="true" DataField="CEDA_DPName" UniqueName="CEDA_DPName"
                            HeaderText="Depository Name" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="CEDA_DPName"
                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AIOE_DepositoryParticipantName" UniqueName="AIOE_DepositoryParticipantName"
                            HeaderText="Depository Participant Name" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="AIOE_DepositoryParticipantName"
                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CEDA_BeneficiaryAccountNum" UniqueName="CEDA_BeneficiaryAccountNum"
                            HeaderText="Beneficiary Account Num" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="CEDA_BeneficiaryAccountNum"
                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AID_SeriesId01" UniqueName="AID_SeriesId01" HeaderText="SeriesId 01"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="100px"
                            SortExpression="AID_SeriesId01" FilterControlWidth="80px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="COID_QuantitySeries01" UniqueName="COID_QuantitySeries01"
                            HeaderText="Quantity Series 01" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="67px" SortExpression="COID_QuantitySeries01"
                            FilterControlWidth="50px" CurrentFilterFunction="Contains" Visible="true">
                            <ItemStyle Width="67px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="COID_AmountPayableSeries01" UniqueName="COID_AmountPayableSeries01"
                            HeaderText="Amount Payable Series 01" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="80px" SortExpression="COID_AmountPayableSeries01"
                            FilterControlWidth="50px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="70px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="true" DataField="AID_SeriesId02" UniqueName="AID_SeriesId02"
                            HeaderText="SeriesId 02" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                            HeaderStyle-Width="100px" SortExpression="AID_SeriesId02" FilterControlWidth="80px"
                            CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="COID_QuantitySeries02" UniqueName="COID_QuantitySeries02"
                            HeaderText="Quantity Series 02" SortExpression="COID_QuantitySeries02" AutoPostBackOnFilter="true"
                            HeaderStyle-Width="100px" FilterControlWidth="80px" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false">
                            <ItemStyle Width="80px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="COID_AmountPayableSeries02" UniqueName="COID_AmountPayableSeries02"
                            HeaderText="Amount Payable Series 02" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="COID_AmountPayableSeries02"
                            FilterControlWidth="80px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AID_SeriesId03" UniqueName="AID_SeriesId03" HeaderText="SeriesId 03"
                            AutoPostBackOnFilter="true" SortExpression="AID_SeriesId03" ShowFilterIcon="false"
                            AllowFiltering="true" HeaderStyle-Width="100px" FilterControlWidth="80px">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="COID_QuantitySeries03" UniqueName="COID_QuantitySeries03"
                            HeaderText="Quantity Series 03" SortExpression="COID_QuantitySeries03" AutoPostBackOnFilter="true"
                            ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="100px" FilterControlWidth="80px"
                            CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="true" DataField="COID_AmountPayableSeries03" UniqueName="COID_AmountPayableSeries03"
                            HeaderText="Amount Payable Series 03" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="COID_AmountPayableSeries03"
                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="true" DataField="AID_SeriesId04" UniqueName="AID_SeriesId04"
                            HeaderText="SeriesId 04" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                            HeaderStyle-Width="100px" SortExpression="AID_SeriesId04" FilterControlWidth="70px"
                            CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="COID_AmountPayableSeries04" UniqueName="COID_AmountPayableSeries04"
                            HeaderText="Amount Payable Series 04" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="COID_AmountPayableSeries04"
                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AID_SeriesId05" UniqueName="AID_SeriesId05" HeaderText="SeriesId 05"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                            SortExpression="AID_SeriesId05" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="COID_QuantitySeries05" UniqueName="COID_QuantitySeries05"
                            HeaderText="Quantity Series 05" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="COID_QuantitySeries05"
                            FilterControlWidth="80px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="COID_AmountPayableSeries05" UniqueName="COID_AmountPayableSeries05"
                            HeaderText="Amount PayableSeries 05" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="67px" SortExpression="COID_AmountPayableSeries05"
                            FilterControlWidth="50px" CurrentFilterFunction="Contains" Visible="true">
                            <ItemStyle Width="67px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AID_SeriesId06" UniqueName="AID_SeriesId06" HeaderText="SeriesId 06"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="80px"
                            SortExpression="AID_SeriesId06" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="70px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="true" DataField="COID_QuantitySeries06" UniqueName="COID_QuantitySeries06"
                            HeaderText="Quantity Series 06" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="COID_QuantitySeries06"
                            FilterControlWidth="80px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="COID_AmountPayableSeries06" UniqueName="COID_AmountPayableSeries06"
                            HeaderText="Amount Payable Series 06" SortExpression="COID_AmountPayableSeries06"
                            AutoPostBackOnFilter="true" HeaderStyle-Width="100px" FilterControlWidth="80px"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            <ItemStyle Width="80px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AID_SeriesId07" UniqueName="AID_SeriesId07" HeaderText="SeriesId 07"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="100px"
                            SortExpression="AID_SeriesId07" FilterControlWidth="80px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="COID_QuantitySeries07" UniqueName="COID_QuantitySeries07"
                            HeaderText="Quantity Series 07" AutoPostBackOnFilter="true" SortExpression="COID_QuantitySeries07"
                            ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="100px" FilterControlWidth="80px">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="COID_AmountPayableSeries07" UniqueName="COID_AmountPayableSeries07"
                            HeaderText="Amount Payable Series 07" SortExpression="COID_AmountPayableSeries07"
                            AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="100px"
                            FilterControlWidth="80px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="true" DataField="AID_SeriesId08" UniqueName="AID_SeriesId08"
                            HeaderText="SeriesId 08" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                            HeaderStyle-Width="100px" SortExpression="AID_SeriesId08" FilterControlWidth="70px"
                            CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="true" DataField="COID_QuantitySeries08" UniqueName="COID_QuantitySeries08"
                            HeaderText="Quantity Series 08" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="COID_QuantitySeries08"
                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="COID_AmountPayableSeries08" UniqueName="COID_AmountPayableSeries08"
                            HeaderText="Amount Payable Series 08" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="COID_AmountPayableSeries08"
                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AID_SeriesId09" UniqueName="AID_SeriesId09" HeaderText="SeriesId 09"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                            SortExpression="AID_SeriesId09" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="COID_QuantitySeries09" UniqueName="COID_QuantitySeries09"
                            HeaderText="Quantity Series 09" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="COID_QuantitySeries09"
                            FilterControlWidth="80px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="COID_AmountPayableSeries09" UniqueName="COID_AmountPayableSeries09"
                            HeaderText="Amount PayableSeries 09" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="67px" SortExpression="COID_AmountPayableSeries09"
                            FilterControlWidth="50px" CurrentFilterFunction="Contains" Visible="true">
                            <ItemStyle Width="67px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AID_SeriesId10" UniqueName="AID_SeriesId10" HeaderText="SeriesId 10"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="80px"
                            SortExpression="AID_SeriesId10" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="70px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="true" DataField="COID_QuantitySeries10" UniqueName="COID_QuantitySeries10"
                            HeaderText="Quantity Series 10" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="COID_QuantitySeries10"
                            FilterControlWidth="80px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="COID_AmountPayableSeries10" UniqueName="COID_AmountPayableSeries10"
                            HeaderText="Amount Payable Series 10" SortExpression="COID_AmountPayableSeries10"
                            AutoPostBackOnFilter="true" HeaderStyle-Width="100px" FilterControlWidth="80px"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            <ItemStyle Width="80px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AID_SeriesId11" UniqueName="AID_SeriesId11" HeaderText="SeriesId 11"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="100px"
                            SortExpression="AID_SeriesId11" FilterControlWidth="80px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="COID_QuantitySeries11" UniqueName="COID_QuantitySeries11"
                            HeaderText="Quantity Series 11" AutoPostBackOnFilter="true" SortExpression="COID_QuantitySeries11"
                            ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="100px" FilterControlWidth="80px">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="COID_AmountPayableSeries11" UniqueName="COID_AmountPayableSeries11"
                            HeaderText="Amount Payable Series 11" SortExpression="COID_AmountPayableSeries11"
                            AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="100px"
                            FilterControlWidth="80px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="true" DataField="AIOE_TotalQuantity" UniqueName="AIOE_TotalQuantity"
                            HeaderText="Total Quantity" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="AIOE_TotalQuantity"
                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="true" DataField="AIOE_TotalAmountPayable" UniqueName="AIOE_TotalAmountPayable"
                            HeaderText="Total Amount Payable" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="AIOE_TotalAmountPayable"
                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AIOE_IsChequeOrDemandDraft" UniqueName="AIOE_IsChequeOrDemandDraft"
                            HeaderText="Is Cheque Or DemandDraft" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="AIOE_IsChequeOrDemandDraft"
                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AIM_InitialChequeNo" UniqueName="AIM_InitialChequeNo"
                            HeaderText="Initial ChequeNo" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="AIM_InitialChequeNo"
                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AIM_ChequeDate" UniqueName="AIM_ChequeDate" HeaderText="Cheque Date"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="100px"
                            SortExpression="AIM_ChequeDate" FilterControlWidth="80px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AIOE_ChequeBankName" UniqueName="AIOE_ChequeBankName"
                            HeaderText="Cheque BankName" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="67px" SortExpression="AIOE_ChequeBankName"
                            FilterControlWidth="50px" CurrentFilterFunction="Contains" Visible="true">
                            <ItemStyle Width="67px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AIOE_ChequeBranch" UniqueName="AIOE_ChequeBranch"
                            HeaderText="Cheque Branch" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="80px" SortExpression="AIOE_ChequeBranch"
                            FilterControlWidth="50px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="70px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="true" DataField="AIOE_NCDCode" UniqueName="AIOE_NCDCode"
                            HeaderText="NCDCode" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                            HeaderStyle-Width="100px" SortExpression="AIOE_NCDCode" FilterControlWidth="80px"
                            CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AIOE_ExchangeBidId" UniqueName="AIOE_ExchangeBidId"
                            HeaderText="Exchange Bid Id" SortExpression="AIOE_ExchangeBidId" AutoPostBackOnFilter="true"
                            HeaderStyle-Width="100px" FilterControlWidth="80px" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false">
                            <ItemStyle Width="80px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AIOE_ActionCode" UniqueName="AIOE_ActionCode"
                            HeaderText="ActionCode" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                            HeaderStyle-Width="100px" SortExpression="AIOE_ActionCode" FilterControlWidth="80px"
                            CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AIOE_Location" UniqueName="AIOE_Location" HeaderText="Location"
                            AutoPostBackOnFilter="true" SortExpression="AIOE_Location" ShowFilterIcon="false"
                            AllowFiltering="true" HeaderStyle-Width="100px" FilterControlWidth="80px">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AIOE_AccountNumber" UniqueName="AIOE_AccountNumber"
                            HeaderText="Account Number" SortExpression="AIOE_AccountNumber" AutoPostBackOnFilter="true"
                            ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="100px" FilterControlWidth="80px"
                            CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="true" DataField="PFSID_DefaultInterestRate" UniqueName="PFSID_DefaultInterestRate"
                            HeaderText="Default InterestRate" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="PFSID_DefaultInterestRate"
                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="true" DataField="AIOE_IsChequeReceived" UniqueName="AIOE_IsChequeReceived"
                            HeaderText="IsCheque Received" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="AIOE_IsChequeReceived"
                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AID_BuyBackFacility01" UniqueName="AID_BuyBackFacility01"
                            HeaderText="BuyBackFacility 01" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="AID_BuyBackFacility01"
                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AID_BuyBackFacility02" UniqueName="AID_BuyBackFacility02"
                            HeaderText="BuyBackFacility 02" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="AID_BuyBackFacility02"
                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AID_BuyBackFacility03" UniqueName="AID_BuyBackFacility03"
                            HeaderText="BuyBackFacility 03" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="AID_BuyBackFacility03"
                            FilterControlWidth="80px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AID_BuyBackFacility04" UniqueName="AID_BuyBackFacility04"
                            HeaderText="BuyBackFacility 04" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="67px" SortExpression="AID_BuyBackFacility04"
                            FilterControlWidth="50px" CurrentFilterFunction="Contains" Visible="true">
                            <ItemStyle Width="67px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AID_BuyBackFacility05" UniqueName="AID_BuyBackFacility05"
                            HeaderText="BuyBackFacility 05" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="80px" SortExpression="AID_BuyBackFacility05"
                            FilterControlWidth="50px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="70px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="true" DataField="AID_BuyBackFacility06" UniqueName="AID_BuyBackFacility06"
                            HeaderText="BuyBackFacility 06" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="AID_BuyBackFacility06"
                            FilterControlWidth="80px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AID_BuyBackFacility07" UniqueName="AID_BuyBackFacility07"
                            HeaderText="BuyBackFacility 07" SortExpression="AID_BuyBackFacility07" AutoPostBackOnFilter="true"
                            HeaderStyle-Width="100px" FilterControlWidth="80px" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false">
                            <ItemStyle Width="80px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AID_BuyBackFacility08" UniqueName="AID_BuyBackFacility08"
                            HeaderText="BuyBackFacility 08" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="AID_BuyBackFacility08"
                            FilterControlWidth="80px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AID_BuyBackFacility09" UniqueName="AID_BuyBackFacility09"
                            HeaderText="BuyBackFacility 09" AutoPostBackOnFilter="true" SortExpression="AID_BuyBackFacility09"
                            ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="100px" FilterControlWidth="80px">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AID_BuyBackFacility10" UniqueName="ThirAID_BuyBackFacility10dHolder"
                            HeaderText="BuyBackFacility 10" SortExpression="AID_BuyBackFacility10" AutoPostBackOnFilter="true"
                            ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="100px" FilterControlWidth="80px"
                            CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="true" DataField="AID_BuyBackFacility11" UniqueName="AID_BuyBackFacility11"
                            HeaderText="BuyBackFacility 11" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="AID_BuyBackFacility11"
                            FilterControlWidth="70px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
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
</asp:Panel>

