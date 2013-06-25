<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadFolioTrxnReconcilation.ascx.cs"
    Inherits="WealthERP.SuperAdmin.UploadFolioTrxnReconcilation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>

<script type="text/javascript" language="javascript">
    function keyPress(sender, args) {
        if (args.keyCode == 13) {
            return false;
        }
    }

</script>

<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Upload Folio Tranx Reconcilation
                        </td>
                        <td align="right" id="tdFolioTranxReconExport" runat="server" style="padding-bottom: 2px;">
                            <asp:ImageButton ID="btnFolio" ImageUrl="~/Images/Export_Excel.png" runat="server"
                                AlternateText="Excel" ToolTip="Export To Excel" Visible="false" OnClientClick="setFormat('excel')"
                                Height="25px" Width="25px" OnClick="btnFolio_Click"></asp:ImageButton>
                            <asp:ImageButton ID="imgBtnTransaction" ImageUrl="~/Images/Export_Excel.png" runat="server"
                                AlternateText="Excel" ToolTip="Export To Excel" Visible="false" OnClientClick="setFormat('excel')"
                                Height="25px" Width="25px" OnClick="imgBtnTransaction_Click"></asp:ImageButton>
                            <asp:ImageButton ID="imgbtnCustomer" ImageUrl="~/Images/Export_Excel.png" runat="server"
                                AlternateText="Excel" ToolTip="Export To Excel" Visible="false" OnClientClick="setFormat('excel')"
                                Height="25px" Width="25px" OnClick="imgbtnCustomer_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="60%">
    <tr>
        <td align="right" style="width: 20%" valign="top">
            <asp:Label ID="lblAdviser" CssClass="FieldName" runat="server" Text="Select Adviser: "></asp:Label>
        </td>
        <td align="left" style="width: 20%;">
            <asp:DropDownList ID="ddlAdviser" runat="server" CssClass="cmbField" AutoPostBack="true">
            </asp:DropDownList>
            <asp:CompareValidator ID="cvAdviserId" runat="server" ControlToValidate="ddlAdviser"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Adviser"
                Operator="NotEqual" ValidationGroup="btnGo" ValueToCompare="Select">
            </asp:CompareValidator>
        </td>
        <td align="right" style="width: 20%">
            <asp:Label ID="lblAsonDate" runat="server" CssClass="FieldName" Text="Select Type: "></asp:Label>
        </td>
        <td align="left" style="width: 20%;">
            <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField" AutoPostBack="true">
                <asp:ListItem Value="Folio" Text="Folio"></asp:ListItem>
                <asp:ListItem Value="Tranx" Text="Transaction"></asp:ListItem>
                <asp:ListItem Value="Customer" Text="Customer"></asp:ListItem>
                <asp:ListItem Value="Systematic" Text="Systematic"></asp:ListItem>
                <asp:ListItem Value="Trail" Text="Trail"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right" style="width: 20%" valign="top">
            <asp:Label ID="lblFromDate" CssClass="FieldName" runat="server" Text="FromDate: "></asp:Label>
        </td>
        <td align="left" style="width: 20%;" valign="top">
            <telerik:RadDatePicker ID="txtFromDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFromDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnGo"> </asp:RequiredFieldValidator>
        </td>
        <td align="right" style="width: 20%" valign="top">
            <asp:Label ID="lblToDate" runat="server" CssClass="FieldName" Text="ToDate: "></asp:Label>
        </td>
        <td align="left" style="width: 20%;" valign="top">
            <telerik:RadDatePicker ID="txtToDate" CssClass="txtTo" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a To Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnGo"> </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="To Date should not be less than From Date"
                Type="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnGo"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td align="right" style="width: 20%">
            &nbsp;
        </td>
        <td align="left" style="width: 20%" valign="top">
            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="PCGButton" ValidationGroup="btnGo"
                onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_RMMultipleTransactionView_btnGo', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_RMMultipleTransactionView_btnGo', 'S');"
                OnClick="btnGo_Click" />
        </td>
        <td align="left" style="width: 40%">
            &nbsp;
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td colspan="4">
            <asp:Panel ID="pnlFolioRecon" runat="server" Width="98%" Visible="true" ScrollBars="Horizontal">
                <table>
                    <tr>
                        <td>
                            <div runat="server" id="divFolioRecon" style="margin: 2px; width: 640px;">
                                <telerik:RadGrid ID="gvFolioRecon" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                    AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" EnableHeaderContextMenu="true"
                                    EnableHeaderContextFilterMenu="true" OnNeedDataSource="gvFolioRecon_OnNeedDataSource">
                                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                        FileName="FolioRecon" Excel-Format="ExcelML">
                                    </ExportSettings>
                                    <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                        CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                                        GroupLoadMode="Client" ShowGroupFooter="true">
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Customer" DataField="Customer"
                                                UniqueName="Customer" SortExpression="Customer" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterText="Grand Total:"
                                                FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="ProcesssId" DataField="ADUL_ProcessId"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="ADUL_ProcessId" SortExpression="ADUL_ProcessId"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="true" CurrentFilterFunction="Contains"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="R&T" DataField="WUXFT_XMLFileName"
                                                UniqueName="WUXFT_XMLFileName" SortExpression="WUXFT_XMLFileName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="AMC" DataField="PA_AMCName"
                                                UniqueName="PA_AMCName" SortExpression="PA_AMCName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="AccountId" DataField="CMFA_AccountId"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="CMFA_AccountId" SortExpression="CMFA_AccountId"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Folio No." DataField="CMFA_FolioNum"
                                                UniqueName="CMFA_FolioNum" SortExpression="CMFA_FolioNum" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Account Opening Date"
                                                DataField="CMFA_AccountOpeningDate" UniqueName="CMFA_AccountOpeningDate" SortExpression="CMFA_AccountOpeningDate"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" DataFormatString="{0:d}"
                                                DataType="System.DateTime" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="CreatedOn" DataField="CMFA_CreatedOn"
                                                UniqueName="CMFA_CreatedOn" SortExpression="CMFA_CreatedOn" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" DataFormatString="{0:d}" DataType="System.DateTime"
                                                CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="ModifiedOn" DataField="CMFA_ModifiedOn"
                                                UniqueName="CMFA_ModifiedOn" SortExpression="CMFA_ModifiedOn" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" DataFormatString="{0:d}" DataType="System.DateTime"
                                                CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <HeaderStyle Width="150px" />
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                        <Resizing AllowColumnResize="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td colspan="4">
            <asp:Panel ID="pnlTransaction" runat="server" Width="98%" Visible="true" ScrollBars="Horizontal">
                <table>
                    <tr>
                        <td>
                            <div runat="server" id="divTransaction" style="margin: 2px; width: 640px;">
                                <telerik:RadGrid ID="gvTransaction" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                    AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" EnableHeaderContextMenu="true"
                                    EnableHeaderContextFilterMenu="true" OnNeedDataSource="gvTransaction_OnNeedDataSource">
                                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                        FileName="Transaction" Excel-Format="ExcelML">
                                    </ExportSettings>
                                    <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                        CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                                        GroupLoadMode="Client" ShowGroupFooter="true">
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Customer" DataField="Customer"
                                                UniqueName="Customer" SortExpression="Customer" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterText="Grand Total:"
                                                FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="ProcessId" DataField="ADUL_ProcessId"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="ADUL_ProcessId" SortExpression="ADUL_ProcessId"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="true" CurrentFilterFunction="Contains"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="R&T" DataField="XES_SourceName"
                                                UniqueName="XES_SourceName" SortExpression="XES_SourceName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Folio No." DataField="CMFA_FolioNum"
                                                UniqueName="CMFA_FolioNum" SortExpression="CMFA_FolioNum" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Scheme" DataField="PASP_SchemePlanName"
                                                UniqueName="PASP_SchemePlanName" SortExpression="PASP_SchemePlanName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Tranx Date" DataField="CMFT_TransactionDate"
                                                UniqueName="CMFT_TransactionDate" SortExpression="CMFT_TransactionDate" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:d}" DataType="System.DateTime">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Type" DataField="Type"
                                                UniqueName="Type" SortExpression="Type" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Tranx No." DataField="CMFT_TransactionNumber"
                                                UniqueName="CMFT_TransactionNumber" SortExpression="CMFT_TransactionNumber" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Units" DataField="CMFT_Units"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="CMFT_Units" SortExpression="CMFT_Units"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N3}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Price" DataField="CMFT_Price"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="CMFT_Price" SortExpression="CMFT_Price"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N4}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Amount" DataField="CMFT_Amount"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="CMFT_Amount" SortExpression="CMFT_Amount"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="CreatedOn" DataField="CMFT_CreatedOn"
                                                UniqueName="CMFT_CreatedOn" SortExpression="CMFT_CreatedOn" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" DataFormatString="{0:d}" DataType="System.DateTime"
                                                CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="ModifiedOn" DataField="CMFT_ModifiedOn"
                                                UniqueName="CMFT_ModifiedOn" SortExpression="CMFT_ModifiedOn" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" DataFormatString="{0:d}" DataType="System.DateTime"
                                                CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <HeaderStyle Width="150px" />
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                        <Resizing AllowColumnResize="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td colspan="4">
            <asp:Panel ID="pnlCustomer" runat="server" Width="98%" Visible="true" ScrollBars="Horizontal">
                <table>
                    <tr>
                        <td>
                            <div runat="server" id="divCustomer" style="margin: 2px; width: 640px;">
                                <telerik:RadGrid ID="gvCustomer" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                    AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" EnableHeaderContextMenu="true"
                                    EnableHeaderContextFilterMenu="true" OnNeedDataSource="gvCustomer_OnNeedDataSource">
                                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                        FileName="Customer" Excel-Format="ExcelML">
                                    </ExportSettings>
                                    <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                        CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                                        GroupLoadMode="Client" ShowGroupFooter="true">
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="CustomerId" DataField="C_CustomerId"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="C_CustomerId" SortExpression="C_CustomerId"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="true" CurrentFilterFunction="Contains"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Customer" DataField="Customer"
                                                UniqueName="Customer" SortExpression="Customer" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterText="Grand Total:"
                                                FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="PAN No." DataField="C_PANNum"
                                                UniqueName="C_PANNum" SortExpression="C_PANNum" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterText="Grand Total:"
                                                FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="ProcessId" DataField="ADUL_ProcessId"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="ADUL_ProcessId" SortExpression="ADUL_ProcessId"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="true" CurrentFilterFunction="Contains"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="R&T" DataField="WUXFT_XMLFileName"
                                                UniqueName="WUXFT_XMLFileName" SortExpression="WUXFT_XMLFileName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Customer Type" DataField="XCT_CustomerTypeCode"
                                                UniqueName="XCT_CustomerTypeCode" SortExpression="XCT_CustomerTypeCode" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="ProfilingDate" DataField="C_ProfilingDate"
                                                UniqueName="C_ProfilingDate" SortExpression="C_ProfilingDate" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" DataFormatString="{0:d}" DataType="System.DateTime"
                                                CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="CreatedOn" DataField="C_CreatedOn"
                                                UniqueName="C_CreatedOn" SortExpression="C_CreatedOn" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" DataFormatString="{0:d}" DataType="System.DateTime"
                                                CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="ModifiedOn" DataField="C_Modifiedon"
                                                UniqueName="C_Modifiedon" SortExpression="C_Modifiedon" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" DataFormatString="{0:d}" DataType="System.DateTime"
                                                CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <HeaderStyle Width="150px" />
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                        <Resizing AllowColumnResize="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td colspan="4">
            <asp:Panel ID="pnlsystematic" runat="server" Width="90%" Visible="true" ScrollBars="Horizontal">
                <table>
                    <tr>
                        <td>
                            <div runat="server" id="divsystematic" style="margin: 2px; width: 640px;">
                                <telerik:RadGrid ID="gvSystematic" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                    AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" EnableHeaderContextMenu="true"
                                    EnableHeaderContextFilterMenu="true" OnNeedDataSource="gvSystematic_OnNeedDataSource">
                                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                        FileName="Customer" Excel-Format="ExcelML">
                                    </ExportSettings>
                                    <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                        CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                                        GroupLoadMode="Client" ShowGroupFooter="true">
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Customer" DataField="Customer"
                                                UniqueName="Customer" SortExpression="Customer" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterText="Grand Total:"
                                                FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="ProcessId" DataField="Wupl_processid"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="Wupl_processid" SortExpression="Wupl_processid"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-HorizontalAlign="Right" FilterControlWidth="65px">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="R&T" DataField="WUXFT_XMLFileName"
                                                UniqueName="WUXFT_XMLFileName" SortExpression="WUXFT_XMLFileName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Folio No." DataField="CMFA_FolioNum"
                                                UniqueName="CMFA_FolioNum" SortExpression="CMFA_FolioNum" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Scheme" DataField="PASP_SchemePlanName"
                                                UniqueName="PASP_SchemePlanName" SortExpression="PASP_SchemePlanName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FilterControlWidth="100px">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="SystematicDate" DataField="CMFSS_SystematicDate"
                                                UniqueName="CMFSS_SystematicDate" SortExpression="CMFSS_SystematicDate" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Type" DataField="XSTT_SystematicTypeCode"
                                                UniqueName="XSTT_SystematicTypeCode" SortExpression="XSTT_SystematicTypeCode"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Amount" DataField="CMFSS_Amount"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="CMFSS_Amount" SortExpression="CMFSS_Amount"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <HeaderStyle Width="150px" />
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                        <Resizing AllowColumnResize="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td colspan="4">
            <asp:Panel ID="pnlTrail" runat="server" Width="98%" Visible="true" ScrollBars="Horizontal">
                <table>
                    <tr>
                        <td>
                            <div runat="server" id="divTrail" style="margin: 2px; width: 640px;">
                                <telerik:RadGrid ID="gvTrail" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                    AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" EnableHeaderContextMenu="true"
                                    EnableHeaderContextFilterMenu="true" OnNeedDataSource="gvTrail_OnNeedDataSource">
                                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                        FileName="Customer" Excel-Format="ExcelML">
                                    </ExportSettings>
                                    <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                        CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                                        GroupLoadMode="Client" ShowGroupFooter="true">
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Customer" DataField="Customer"
                                                UniqueName="Customer" SortExpression="Customer" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterText="Grand Total:"
                                                FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="ProcessId" DataField="ADUL_ProcessId"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="ADUL_ProcessId" SortExpression="ADUL_ProcessId"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-HorizontalAlign="Right" FilterControlWidth="65px">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="R&T" DataField="WUXFT_XMLFileName"
                                                UniqueName="WUXFT_XMLFileName" SortExpression="WUXFT_XMLFileName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Folio No." DataField="CMFA_FolioNum"
                                                UniqueName="CMFA_FolioNum" SortExpression="CMFA_FolioNum" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Scheme" DataField="PASP_SchemePlanName"
                                                UniqueName="PASP_SchemePlanName" SortExpression="PASP_SchemePlanName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FilterControlWidth="100px">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Tranx Date" DataField="CMFTTCS_TransactionDate"
                                                UniqueName="CMFTTCS_TransactionDate" SortExpression="CMFTTCS_TransactionDate"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:d}" DataType="System.DateTime">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Type" DataField="Type"
                                                UniqueName="Type" SortExpression="Type" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <%-- <telerik:GridBoundColumn visible ="false" HeaderStyle-Width="150px" HeaderText="Tranx No." DataField="CMFTTCS_TransactionNo"
                                                UniqueName="CMFTTCS_TransactionNo" SortExpression="CMFTTCS_TransactionNo" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>--%>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Units" DataField="CMFTTCS_Units"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="CMFTTCS_Units" SortExpression="CMFTTCS_Units"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N3}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Amount" DataField="CMFTTCS_Amount"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="CMFTTCS_Amount" SortExpression="CMFTTCS_Amount"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <HeaderStyle Width="150px" />
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                        <Resizing AllowColumnResize="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
