<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RejectedSystematicTransactionInputRejects.ascx.cs"
    Inherits="WealthERP.Uploads.RejectedSystematicTransactionInputRejects" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
<div class="divPageHeading">
    <table cellspacing="1" cellpadding="3" width="100%">
        <tr>
            <td align="left">
                <asp:Label ID="lblOrderList" runat="server" CssClass="HeaderTextBig" Text="Systemaic Input Reject"></asp:Label>
            </td>
            <td align="right">
                <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="View ProcessLog"
                    OnClick="lnkBtnBack_Click"></asp:LinkButton>
            </td>
            <td>
                <asp:ImageButton ID="btnExportFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                    OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
            </td>
        </tr>
    </table>
</div>
<br />
<div>
    <telerik:RadGrid ID="gvSIPInputRejectDetails" runat="server" CssClass="RadGrid" GridLines="None"
        Width="100%" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false"
        ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
        AllowAutomaticUpdates="false" Skin="Telerik" EnableEmbeddedSkins="false" EnableHeaderContextMenu="true"
        EnableHeaderContextFilterMenu="true" AllowFilteringByColumn="true" OnNeedDataSource="gvSIPInputRejectDetails_OnNeedDataSource">
        <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="SIPInputRejectDetails">
        </ExportSettings>
        <MasterTableView EditMode="PopUp" CommandItemDisplay="None">
            <Columns>
                <telerik:GridBoundColumn UniqueName="CMFCXSSI_PRODUCT" HeaderText="PRODUCT" DataField="CMFCXSSI_PRODUCT"
                    SortExpression="CMFCXSSI_PRODUCT" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFCXSSI_SCHEME" HeaderText="SCHEME" DataField="CMFCXSSI_SCHEME"
                    SortExpression="CMFCXSSI_SCHEME" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFCXSSI_FOLIONO" HeaderText="FOLIONO" DataField="CMFCXSSI_FOLIONO"
                    SortExpression="CMFCXSSI_FOLIONO" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFCXSSI_INVNAME" HeaderText="INVNAME" DataField="CMFCXSSI_INVNAME"
                    SortExpression="CMFCXSSI_INVNAME" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFCXSSI_AUTOTRXN" HeaderText="AUTOTRXN" DataField="CMFCXSSI_AUTOTRXN"
                    SortExpression="CMFCXSSI_AUTOTRXN" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFCXSSI_AUTOTRXNNum" HeaderText="AUTOTRXNNum"
                    DataField="CMFCXSSI_AUTOTRXNNum" SortExpression="CMFCXSSI_AUTOTRXNNum" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFCXSSI_AUTOAMOUN" HeaderText="AUTOAMOUN" DataField="CMFCXSSI_AUTOAMOUN"
                    SortExpression="CMFCXSSI_AUTOAMOUN" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFCXSSI_FROMDATE" HeaderText="FROMDATE" DataField="CMFCXSSI_FROMDATE"
                    SortExpression="CMFCXSSI_FROMDATE" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFCXSSI_TODATE" HeaderText="TODATE" DataField="CMFCXSSI_TODATE"
                    SortExpression="CMFCXSSI_TODATE" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFCXSSI_CEASEDATE" HeaderText="CEASEDATE" DataField="CMFCXSSI_CEASEDATE"
                    SortExpression="CMFCXSSI_CEASEDATE" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFCXSSI_PERIODICIT" HeaderText="PERIODICIT"
                    DataField="CMFCXSSI_PERIODICIT" SortExpression="CMFCXSSI_PERIODICIT" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFCXSSI_PERIODDAY" HeaderText="PERIODDAY" DataField="CMFCXSSI_PERIODDAY"
                    SortExpression="CMFCXSSI_PERIODDAY" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFCXSSI_INVIIN" HeaderText="INVIIN" DataField="CMFCXSSI_INVIIN"
                    SortExpression="CMFCXSSI_INVIIN" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFCXSSI_PAYMENTMO" HeaderText="PAYMENTMO" DataField="CMFCXSSI_PAYMENTMO"
                    SortExpression="CMFCXSSI_PAYMENTMO" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFCXSSI_TARGETSCH" HeaderText="TARGETSCH" DataField="CMFCXSSI_TARGETSCH"
                    SortExpression="CMFCXSSI_TARGETSCH" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFCXSSI_REGDATE" HeaderText="REGDATE" DataField="CMFCXSSI_REGDATE"
                    SortExpression="CMFCXSSI_REGDATE" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFCXSSI_SUBBROKER" HeaderText="SUBBROKER" DataField="CMFCXSSI_SUBBROKER"
                    SortExpression="CMFCXSSI_SUBBROKER" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="WUPL_ProcessId" HeaderText="ProcessId" DataField="WUPL_ProcessId"
                    SortExpression="WUPL_ProcessId" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings ReorderColumnsOnClient="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
            <Scrolling AllowScroll="false" />
            <ClientEvents OnHeaderMenuShowing="HeaderMenuShowing" />
            <Resizing AllowColumnResize="true" />
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
<div>
</div>
