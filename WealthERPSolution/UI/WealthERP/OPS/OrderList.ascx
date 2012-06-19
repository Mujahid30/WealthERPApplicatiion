<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderList.ascx.cs" Inherits="WealthERP.OPS.OrderList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
<table width="100%">
    <tr>
        <td style="width: 30%">
            <asp:Label ID="lblOrderList" runat="server" CssClass="HeaderTextBig" Text="Order List"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadGrid1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="gvOrderList" LoadingPanelID="RadAjaxLoadingPanel1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>

<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />

<telerik:RadGrid ID="gvOrderList" runat="server" GridLines="None" AutoGenerateColumns="False"
    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
    Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AllowAutomaticInserts="false"
    OnNeedDataSource="gvOrderList_OnNeedDataSource" OnItemCommand="gvOrderList_ItemCommand">
    <ExportSettings HideStructureColumns="true" ExportOnlyData="true">
    </ExportSettings>
    <MasterTableView DataKeyNames="CO_OrderId" Width="100%" AllowMultiColumnSorting="True"
        AutoGenerateColumns="false" CommandItemDisplay="Top">
        <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
            ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
        <Columns>
           <telerik:gridbuttonColumn ButtonType="LinkButton" CommandName="Redirect" UniqueName="CO_OrderId" HeaderText="Order No." 
             DataTextField="CO_OrderId" FooterStyle-HorizontalAlign="Right">
                <ItemStyle HorizontalAlign="Right" />
            </telerik:gridbuttonColumn>
            
            <telerik:GridBoundColumn DataField="CO_OrderDate" AllowFiltering="false" HeaderText="Order Date"
                UniqueName="CO_OrderDate">
                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="PAS_AssetCategory" AllowFiltering="false" HeaderText="Category"
                UniqueName="ActiveLevel">
                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="WOSR_SourceCode" AllowFiltering="false" HeaderText="SourceCode"
                UniqueName="WOSR_SourceCode">
                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="CO_ChequeNumber" AllowFiltering="false" HeaderText="Cheque Number"
                UniqueName="CO_ChequeNumber">
                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="CO_ApplicationNumber" AllowFiltering="false" HeaderText="ApplicationNumber"
                UniqueName="CO_ApplicationNumber">
                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="CO_ApplicationReceivedDate" DataFormatString="{0:dd/MM/yyyy}"
                AllowFiltering="false" HeaderText="Application Received Date" UniqueName="CO_ApplicationReceivedDate">
                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="CO_PaymentDate" DataFormatString="{0:dd/MM/yyyy}"
                AllowFiltering="false" HeaderText="Payment Date" UniqueName="CO_PaymentDate">
                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
            </telerik:GridBoundColumn>
        </Columns>
    </MasterTableView>
    <ClientSettings>
        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
    </ClientSettings>
</telerik:RadGrid>