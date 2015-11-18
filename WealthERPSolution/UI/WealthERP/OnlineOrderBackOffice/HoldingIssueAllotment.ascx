<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HoldingIssueAllotment.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.HoldingIssueAllotment" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
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
            <asp:DropDownList ID="ddlOrderType" runat="server" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlOrderType_OnSelectedIndexChanged">
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                <asp:ListItem Text="Client Intial Orders" Value="False" />
                <asp:ListItem Text="FATCA" Value="True" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please select a Type" ControlToValidate="ddlOrderType" Display="Dynamic"
                InitialValue="0" ValidationGroup="Issueallotment">
            </asp:RequiredFieldValidator>
        </td>
         <td align="right" id="tdlblAMC" runat="server"  visible="false">
            <asp:Label ID="lblAMC" CssClass="FieldName" runat="server" Text="AMC:"></asp:Label>
        </td>
        <td runat="server" visible="false" id="tdddlAMC">
            <asp:DropDownList ID="ddlAMC" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please select a Type" ControlToValidate="ddlAMC" Display="Dynamic"
                InitialValue="0" ValidationGroup="Issueallotment">
            </asp:RequiredFieldValidator>
        </td>
        <td align="right" id="tdlblType" runat="server" visible="false">
            <asp:Label ID="lblType" CssClass="FieldName" runat="server" Text="Type:"></asp:Label>
        </td>
        <td id="tdddlType" runat="server" visible="false"> 
            <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField">
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                <asp:ListItem Text="AMC Wise" Value="AMC" />
                <asp:ListItem Text="RTA Wise" Value="RNT" />
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
            <%-- <span id="Span7" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="appRecidRequiredFieldValidator" ControlToValidate="txtNFOStartDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select NFO Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnsubmit"></asp:RequiredFieldValidator>--%>
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
            <%--<span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNFOStartDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select NFO End Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnsubmit"></asp:RequiredFieldValidator>--%>
        </td>
        <%-- <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtToDate"
            ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
            ControlToCompare="txtFromDate" CssClass="cvPCG" ValidationGroup="btnsubmit" Display="Dynamic">
        </asp:CompareValidator>--%>
        <td>
            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="PCGButton" OnClick="Go_OnClick"
                ValidationGroup="Issueallotment" />
        </td>
    </tr>
</table>
<asp:Panel ID="pnlOrderReport" runat="server" ScrollBars="Horizontal" Width="100%"
    Visible="false">
    <table width="100%" cellspacing="0" cellpadding="1">
        <tr>
            <td>
                <telerik:RadGrid ID="gvOrderReport" runat="server" fAllowAutomaticDeletes="false"
                    EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                    ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true"
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
                                SortExpression="PA_AMCName" FilterControlWidth="70px" CurrentFilterFunction="Contains">
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
                    ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true"
                    GridLines="none" AllowAutomaticInserts="false" Skin="Telerik" EnableHeaderContextMenu="true"
                    OnNeedDataSource="gvAdviserIssueList_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                        FileName="MF Order Recon" Excel-Format="ExcelML">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="AIA_Id" Width="99%" AllowMultiColumnSorting="True"
                        AutoGenerateColumns="false" PageSize="20">
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
<asp:Panel ID="pnlFATCA" runat="server" ScrollBars="Horizontal" Width="100%"
    Visible="false">
    <table width="100%" cellspacing="0" cellpadding="1">
        <tr>
            <td>
                <telerik:RadGrid ID="rgFATCA" runat="server" fAllowAutomaticDeletes="false"
                    EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                    ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true"
                    GridLines="none" AllowAutomaticInserts="false" Skin="Telerik" EnableHeaderContextMenu="true"
                    OnNeedDataSource="rgFATCA_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                        FileName="FATCA" Excel-Format="ExcelML">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="" Width="99%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                        PageSize="20">
                        <Columns>
                            <telerik:GridBoundColumn DataField="PA_AMCName" UniqueName="PA_AMCName"
                                HeaderText="AMC" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                HeaderStyle-Width="90px" SortExpression="PA_AMCName" FilterControlWidth="70px"
                                CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_InvestorFirstName" UniqueName="AMFE_InvestorFirstName" HeaderText="Customer Name"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                                SortExpression="AMFE_InvestorFirstName" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AMFE_PanNo" UniqueName="AMFE_PanNo"
                                HeaderText="PAN" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="AMFE_PanNo"
                                FilterControlWidth="70px" CurrentFilterFunction="Contains">
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
