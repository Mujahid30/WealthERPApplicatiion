<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HoldingIssueAllotment.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.HoldingIssueAllotment" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<script language="javascript" type="text/javascript">
    function checkAllBoxes() {


        var totalChkBoxes = parseInt('<%= gvCustomerDetails.Items.Count %>');
        var gvControl = document.getElementById('<%= gvCustomerDetails.ClientID %>');


        var gvChkBoxControl = "cbRecons";


        var mainChkBox = document.getElementById("chkBxWerpAll");


        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {

            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
</script>

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            Initial Order Report AMC/RTA Wise
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
<table>
    <tr>
        <td align="right">
            <asp:Label ID="Label1" CssClass="FieldName" runat="server" Text="Order Type:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlOrderType" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlOrderType_OnSelectedIndexChanged">
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                <asp:ListItem Text="Client Intial Orders" Value="1" />
                <asp:ListItem Text="FATCA" Value="2" />
                <asp:ListItem Text="BSE" Value="3" />
                <asp:ListItem Text="CAMS Client DBF" Value="CA" />
                <asp:ListItem Text="Karvy Client DBF" Value="KA"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please select a Type" ControlToValidate="ddlOrderType" Display="Dynamic"
                InitialValue="0" ValidationGroup="Issueallotment">
            </asp:RequiredFieldValidator>
        </td>
        <td align="right" id="tdlblAMC" runat="server" visible="false">
            <asp:Label ID="lblAMC" CssClass="FieldName" runat="server" Text="AMC:"></asp:Label>
        </td>
        <td runat="server" visible="false" id="tdddlAMC">
            <asp:DropDownList ID="ddlAMC" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please select a AMC" ControlToValidate="ddlAMC" Display="Dynamic"
                InitialValue="Select" ValidationGroup="Issueallotment">
            </asp:RequiredFieldValidator>
        </td>
        <td align="right" id="tdlblType" runat="server" visible="false">
            <asp:Label ID="lblType" CssClass="FieldName" runat="server" Text="Type:"></asp:Label>
        </td>
        <td id="tdddlType" runat="server">
            <asp:DropDownList ID="ddlType" AutoPostBack="true" runat="server" CssClass="cmbField">
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                <asp:ListItem Text="AMC Wise" Value="AMC" />
                <asp:ListItem Text="RTA Wise" Value="RNT" />
                <asp:ListItem Text="Extract BSE Client File " Value="EBSE" />
                <asp:ListItem Text="Exchange MIS" Value="EMIS" />
                <asp:ListItem Text="Summary" Value="FCS" />
                <asp:ListItem Text="Detail" Value="FCD" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvType" runat="server" CssClass="rfvPCG" ErrorMessage="Please select a Type"
                ControlToValidate="ddlType" Display="Dynamic" InitialValue="0" ValidationGroup="Issueallotment">
            </asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblNfostartdate" runat="server" Text="From Date" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtFromDate" CssClass="txtField" runat="server" Culture="English (United States)"
                AutoPostBack="false" Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade"
                MinDate="1900-01-01" TabIndex="5">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
        </td>
        <td align="right">
            <asp:Label ID="lblNfoEnddate" runat="server" Text="To Date" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtToDate" CssClass="txtField" runat="server" Culture="English (United States)"
                AutoPostBack="false" Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade"
                MinDate="1900-01-01" TabIndex="5">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
        </td>
        <td>
            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="PCGButton" OnClick="Go_OnClick"
                ValidationGroup="Issueallotment" />
        </td>
        <td width="10%">
            <asp:Button ID="btnDownload" runat="server" Text="Download File" ValidationGroup="IssueExtract"
                CssClass="PCGLongButton" OnClick="btnDownload_Click" Visible="false" />
        </td>
    </tr>
</table>
<asp:Panel ID="pnlOnlneIssueExtract" runat="server" Width="100%" ScrollBars="Horizontal"
    Visible="false">
    <tr>
        <td>
            <telerik:RadGrid ID="gvOnlineIssueExtract" runat="server" AutoGenerateColumns="true"
                AllowPaging="true" AllowSorting="true" Skin="Telerik" EnableHeaderContextMenu="true"
                GridLines="Both" EnableEmbeddedSkins="false" ShowFooter="true" PagerStyle-AlwaysVisible="true"
                EnableViewState="true" ShowStatusBar="true" AllowFilteringByColumn="true" PageSize="10">
                <ExportSettings HideStructureColumns="true">
                </ExportSettings>
                <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="true"
                    HeaderStyle-Width="120px">
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
<asp:Panel ID="pnlOrderReport" runat="server" ScrollBars="Horizontal" Width="100%"
    Visible="false">
    <table width="100%" cellspacing="0" cellpadding="1">
        <tr>
            <td>
                <telerik:RadGrid ID="gvOrderReport" runat="server" fAllowAutomaticDeletes="false"
                    EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                    ShowStatusBar="false" AllowPaging="true" PageSize="10" ShowFooter="false" AllowSorting="true"
                    GridLines="none" AllowAutomaticInserts="false" Skin="Telerik" EnableHeaderContextMenu="true"
                    OnNeedDataSource="gvOrderReport_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                        FileName="Initial Order Report AMC/RTA Wise" Excel-Format="ExcelML">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="" Width="99%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                        PageSize="20">
                        <Columns>
                            <telerik:GridBoundColumn DataField="PASC_AMC_ExternalType" UniqueName="PASC_AMC_ExternalType"
                                HeaderText="RTA" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                HeaderStyle-Width="90px" SortExpression="PASC_AMC_ExternalType" FilterControlWidth="70px"
                                CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PA_AMCName" UniqueName="PA_AMCName" HeaderText="AMC Name"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                                SortExpression="PA_AMCName" FilterControlWidth="230px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PASP_SchemePlanName" UniqueName="PASP_SchemePlanName"
                                HeaderText="SchemePlan Name" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="PASP_SchemePlanName"
                                FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFOD_Amount" UniqueName="CMFOD_Amount" HeaderText="Amount"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                                SortExpression="CMFOD_Amount" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_OrderId" UniqueName="CO_OrderId" HeaderText="Order No"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="100px"
                                SortExpression="CO_OrderId" FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_OrderDate" UniqueName="CO_OrderDate" HeaderText="Order Date"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="67px"
                                SortExpression="CO_OrderDate" FilterControlWidth="50px" CurrentFilterFunction="Contains"
                                Visible="true">
                                <ItemStyle Width="67px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_FirstName" UniqueName="C_FirstName" HeaderText="Customer Name"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="80px"
                                SortExpression="C_FirstName" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="70px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="true" DataField="C_PANNum" UniqueName="C_PANNum"
                                HeaderText="PAN No" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                HeaderStyle-Width="100px" SortExpression="C_PANNum" FilterControlWidth="80px"
                                CurrentFilterFunction="Contains">
                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_CustCode" UniqueName="C_CustCode" HeaderText="Client Code"
                                SortExpression="C_CustCode" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                                FilterControlWidth="80px" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemStyle Width="80px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WMTT_TransactionClassificationCode" UniqueName="WMTT_TransactionClassificationCode"
                                HeaderText="Type" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                HeaderStyle-Width="100px" SortExpression="WMTT_TransactionClassificationCode"
                                FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
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
<table visible="false" id="trddlType" runat="server">
</table>
<asp:Panel ID="AdviserIssueList" runat="server" ScrollBars="Horizontal" Width="100%"
    Visible="false">
    <table width="100%" cellspacing="0" cellpadding="1">
        <tr>
            <td>
                <telerik:RadGrid ID="gvAdviserIssueList" runat="server" fAllowAutomaticDeletes="false"
                    EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                    ShowStatusBar="false" ShowFooter="false" AllowPaging="true" PageSize="10" AllowSorting="true"
                    GridLines="none" AllowAutomaticInserts="false" Skin="Telerik" EnableHeaderContextMenu="true"
                    OnNeedDataSource="gvAdviserIssueList_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                        FileName="MF Order Recon" Excel-Format="ExcelML">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="" Width="99%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                        PageSize="20">
                        <Columns>
                            <telerik:GridBoundColumn DataField="AIA_AllotmentDate" UniqueName="AIA_AllotmentDate"
                                HeaderText="AllotmentDate" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="AIA_AllotmentDate"
                                FilterControlWidth="70px" CurrentFilterFunction="Contains" DataFormatString="{0:d}">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_OrderId" UniqueName="CO_OrderId" HeaderText="OrderId"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                                SortExpression="CO_OrderId" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIM_IssueName" UniqueName="AIM_IssueName" HeaderText="Issue Name"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="100px"
                                SortExpression="AIM_IssueName" FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIA_Quantity" UniqueName="AIA_Quantity" HeaderText="Quantity"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="67px"
                                SortExpression="AIA_Quantity" FilterControlWidth="50px" CurrentFilterFunction="Contains"
                                Visible="true">
                                <ItemStyle Width="67px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIA_Price" UniqueName="AIA_Price" HeaderText="Price"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="80px"
                                SortExpression="AIA_Price" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="70px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="true" DataField="CO_ApplicationNo" UniqueName="CO_ApplicationNo"
                                HeaderText="ApplicationNo" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="CO_ApplicationNo"
                                FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CEDA_DPId" UniqueName="CEDA_DPId" HeaderText="DPId"
                                SortExpression="CEDA_DPId" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                                FilterControlWidth="80px" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemStyle Width="80px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_CustomerId" UniqueName="C_CustomerId" HeaderText="CustomerId"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="100px"
                                SortExpression="C_CustomerId" FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn DataField="C_CustomerId" UniqueName="C_CustomerId" HeaderText="Customer Name"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="100px"
                                SortExpression="C_CustomerId" FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>--%>
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
<asp:Panel ID="pnlFATCA" runat="server" ScrollBars="Horizontal" Width="100%" Visible="false">
    <table width="100%" cellspacing="0" cellpadding="1">
        <tr>
            <td>
                <telerik:RadGrid ID="rgFATCA" runat="server" fAllowAutomaticDeletes="false" EnableEmbeddedSkins="false"
                    AllowFilteringByColumn="true" AutoGenerateColumns="False" ShowStatusBar="false"
                    ShowFooter="false" AllowPaging="true" PageSize="10" AllowSorting="true" GridLines="none"
                    AllowAutomaticInserts="false" Skin="Telerik" EnableHeaderContextMenu="true" OnNeedDataSource="rgFATCA_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                        FileName="FATCA" Excel-Format="ExcelML">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="" Width="99%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                        PageSize="20">
                        <Columns>
                            <telerik:GridBoundColumn DataField="PA_AMCName" UniqueName="PA_AMCName" HeaderText="AMC"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                                SortExpression="PA_AMCName" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_InvestorFirstName" UniqueName="AMFE_InvestorFirstName"
                                HeaderText="Customer Name" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="AMFE_InvestorFirstName"
                                FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_PanNo" UniqueName="AMFE_PanNo" HeaderText="PAN"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                                SortExpression="AMFE_PanNo" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="LOG_NAME" UniqueName="LOG_NAME" HeaderText="LOG_NAME"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                                SortExpression="LOG_NAME" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_OrderId" UniqueName="CO_OrderId" HeaderText="Order No"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="100px"
                                SortExpression="CO_OrderId" FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="120px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_OrderDate" UniqueName="CO_OrderDate" HeaderText="Order Date"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="100px"
                                SortExpression="CO_OrderDate" FilterControlWidth="80px" CurrentFilterFunction="Contains">
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
    </table>
</asp:Panel>
<asp:Panel ID="pnlCustomerDetails" runat="server" ScrollBars="Horizontal" Width="100%"
    Visible="false">
    <table width="100%" cellspacing="0" cellpadding="1">
        <tr>
            <td>
                <telerik:RadGrid ID="gvCustomerDetails" runat="server" AutoGenerateColumns="true"
                    AllowPaging="true" AllowSorting="true" Skin="Telerik" EnableHeaderContextMenu="true"
                    GridLines="Both" EnableEmbeddedSkins="false" OnNeedDataSource="gvCustomerDetails_OnNeedDataSource"
                    OnItemDataBound="gvCustomerDetails_ItemDataBound" ShowFooter="true" PagerStyle-AlwaysVisible="true"
                    EnableViewState="true" ShowStatusBar="true" AllowFilteringByColumn="true" PageSize="10">
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="Customer Id" Width="99%" AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridBoundColumn DataField="Customer Id" UniqueName="Customer Id" HeaderText="Customer Id"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                                SortExpression="Customer Id" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Customer Code" UniqueName="Customer Code" HeaderText="Customer Code"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                                SortExpression="Customer Code" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="First Name" UniqueName="First Name" HeaderText="First Name"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                                SortExpression="First Name" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Middle Name" UniqueName="Middle Name" HeaderText="Middle Name"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                                SortExpression="Middle Name" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Last Name" UniqueName="Last Name" HeaderText="Last Name"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="100px"
                                SortExpression="Last Name" FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="120px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PAN Number" UniqueName="PAN Number" HeaderText="PAN Number"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="100px"
                                SortExpression="PAN Number" FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Email Id" UniqueName="Email Id" HeaderText="Email Id"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                                SortExpression="Email Id" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Mobile1" UniqueName="Mobile1" HeaderText="Mobile1"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                                SortExpression="Mobile1" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Mobile2" UniqueName="Mobile2" HeaderText="Mobile2"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                                SortExpression="Mobile2" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="Is DematAccepted" UniqueName="Is DematAccepted"
                                HeaderText="Is DematAccepted" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="Is DematAccepted"
                                FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn DataField="Is Real Investor" UniqueName="Is Real Investor"
                                HeaderText="Is Real Investor" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                ReadOnly="true" AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="Is Real Investor"
                                FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="120px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridCheckBoxColumn>
                        </Columns>
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
    </table>
</asp:Panel>
<tr>
    <td>
        <asp:Button ID="btnUpdate" Visible="false" runat="server" Text="Update" CssClass="PCGButton" OnClick="btnUpdate_OnClick" />
    </td>
</tr>
