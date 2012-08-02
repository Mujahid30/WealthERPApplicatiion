﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TrailCommisionTransactionRejects.ascx.cs"
    Inherits="WealthERP.Uploads.TrailCommisionTransactionRejects" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<div class="divPageHeading">
    <table cellspacing="0" cellpadding="3" width="100%">
        <tr>
            <td class="HeaderCell">
                <asp:Label ID="lblOrderList" runat="server" CssClass="HeaderTextBig" Text="Trail Commission Rejects"></asp:Label>
            </td>
        </tr>
    </table>
</div>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgReprocessComplete" runat="server" class="success-msg" align="center"
                visible="false">
                Reprocess successfully Completed
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgDelete" runat="server" class="success-msg" align="center" visible="false">
                Records has been deleted successfully
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgReprocessincomplete" runat="server" class="failure-msg" align="center"
                visible="false">
                Reprocess Failed!
            </div>
        </td>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
    <table>
        <tr>
            <td>
                <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="View ProcessLog"
                    OnClick="lnkBtnBack_Click"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ImageButton ID="imgBtnrgHoldings" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                    OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
                <telerik:RadGrid ID="GVTrailTransactionRejects" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AllowAutomaticInserts="false"
                    OnNeedDataSource="GVTrailTransactionRejects_OnNeedDataSource">
                    <ExportSettings ExportOnlyData="true" FileName="Trail Commission Reject Details">
                    </ExportSettings>
                    <MasterTableView AllowFilteringByColumn="false" DataKeyNames="CMFTCCS_Id,A_AdviserId,Adul_ProcessId"
                        Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false">
                                <HeaderTemplate>
                                    <asp:CheckBox runat="server" ID="ChkALL" AutoPostBack="True" OnCheckedChanged="ToggleSelectedState" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="ChkOne" AutoPostBack="True" OnCheckedChanged="ToggleRowSelection" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="WRR_RejectReasonDescription" AllowFiltering="false"
                                HeaderText="Reject Reason" UniqueName="RejectReason">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Adul_ProcessId" AllowFiltering="false" HeaderText="Process Id"
                                UniqueName="ProcessId">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="ADUL_FileName" HeaderText="File Name"
                                UniqueName="ADUL_FileName" SortExpression="ADUL_FileName" AutoPostBackOnFilter="true"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFA_FolioNum" HeaderText="Folio Number"
                                UniqueName="CMFA_FolioNum" SortExpression="CMFA_FolioNum" AutoPostBackOnFilter="true"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PASP_SchemePlanName" HeaderText="Scheme Plan Name"
                                UniqueName="PASP_SchemePlanName" SortExpression="PASP_SchemePlanName" AutoPostBackOnFilter="true"
                                ShowFilterIcon="false" AllowFiltering="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_FROMDate" HeaderText="From Date"
                                UniqueName="FromDate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_TODate" HeaderText="To Date"
                                UniqueName="ToDate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_PRDate" HeaderText="Purchase Date"
                                UniqueName="purchasedate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_TransactionType"
                                HeaderText="Transaction Type" UniqueName="transactiontype" SortExpression="ADUL_FileName"
                                AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_Units" HeaderText="Units"
                                UniqueName="units">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_Amount" HeaderText="Amount"
                                UniqueName="amount">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="CMFTTCS_SubBroker" HeaderText="Sub Broker"
                                UniqueName="subbroker">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <ClientEvents OnRowClick="onRowClick" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
<table width="100%">
    <tr id="trReprocess" runat="server">
        <td class="SubmitCell">
            <asp:Button ID="btnReprocess" runat="server" Text="Reprocess" CssClass="PCGLongButton"
                OnClick="btnReprocess_Click" OnClientClick="Loading(true);" />
            <asp:Button ID="btnDelete" runat="server" CssClass="PCGLongButton" Text="Delete Records"
                OnClick="btnDelete_Click" />
        </td>
    </tr>
    <tr id="trMessage" runat="server" visible="false">
        <td class="Message">
            <label id="lblEmptyMsg" class="FieldName">
                There are no records to be displayed!</label>
        </td>
    </tr>
    <tr id="trErrorMessage" runat="server" visible="false">
        <td class="Message">
            <asp:Label ID="lblError" CssClass="Message" runat="server">
            </asp:Label>
        </td>
    </tr>
</table>
