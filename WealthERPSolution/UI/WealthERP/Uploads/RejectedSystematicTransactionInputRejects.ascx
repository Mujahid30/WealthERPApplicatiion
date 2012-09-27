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
            <td align="right" style="width: 10px">
                <asp:ImageButton ID="btnExportFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                    OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
            </td>
        </tr>
    </table>
</div>
<br />
<asp:Panel Width="95%" ScrollBars="Horizontal" runat="server">
    <table>
        <tr>
            <td>
                <telerik:RadGrid ID="gvSIPInputRejectDetails" runat="server" CssClass="RadGrid" GridLines="None"
                    Width="1050px" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="false"
                    ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
                    AllowAutomaticUpdates="false" Skin="Telerik" EnableEmbeddedSkins="false" EnableHeaderContextMenu="true"
                    EnableHeaderContextFilterMenu="true" AllowFilteringByColumn="true" OnNeedDataSource="gvSIPInputRejectDetails_OnNeedDataSource">
                    <%--  <telerik:RadGrid ID="gvSIPInputRejectDetails" ShowFooter="True" GridLines="None"  CssClass="RadGrid"
        AllowSorting="True" Width="1050px" runat="server" AutoGenerateColumns="false">--%>
                    <PagerStyle Mode="NumericPages" />
                    <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="SIPInputRejectDetails">
                    </ExportSettings>
                    <MasterTableView Width="100%" EditMode="PopUp" CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="CMFCXSSI_PRODUCT" HeaderText="PRODUCT" DataField="CMFCXSSI_PRODUCT"
                                SortExpression="CMFCXSSI_PRODUCT" AllowFiltering="true" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <ItemStyle HorizontalAlign="left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="95px" UniqueName="WRR_RejectReasonDescription"
                                HeaderText="Reject Reason" DataField="WRR_RejectReasonDescription" SortExpression="WRR_RejectReasonDescription"
                                AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <ItemStyle HorizontalAlign="left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="250px" UniqueName="CMFCXSSI_SCHEME" HeaderText="SCHEME"
                                DataField="CMFCXSSI_SCHEME" SortExpression="CMFCXSSI_SCHEME" AllowFiltering="true"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <ItemStyle HorizontalAlign="left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CMFCXSSI_FOLIONO" HeaderText="FOLIONO" DataField="CMFCXSSI_FOLIONO"
                                SortExpression="CMFCXSSI_FOLIONO" AllowFiltering="false" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <ItemStyle HorizontalAlign="right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" UniqueName="CMFCXSSI_INVNAME"
                                HeaderText="INVNAME" DataField="CMFCXSSI_INVNAME" SortExpression="CMFCXSSI_INVNAME"
                                AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <ItemStyle HorizontalAlign="left" />
                            </telerik:GridBoundColumn>
                            <%--  <telerik:GridBoundColumn UniqueName="CMFCXSSI_AUTOTRXN" HeaderText="AUTOTRXN" DataField="CMFCXSSI_AUTOTRXN"
                    SortExpression="CMFCXSSI_AUTOTRXN" AllowFiltering="false" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFCXSSI_AUTOTRXNNum" HeaderText="AUTOTRXNNum"
                    DataField="CMFCXSSI_AUTOTRXNNum" SortExpression="CMFCXSSI_AUTOTRXNNum" AllowFiltering="false"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn UniqueName="CMFCXSSI_AUTOAMOUN" HeaderText="AUTOAMOUN" DataField="CMFCXSSI_AUTOAMOUN"
                                SortExpression="CMFCXSSI_AUTOAMOUN" AllowFiltering="false" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CMFCXSSI_FROMDATE" HeaderText="FROMDATE" DataField="CMFCXSSI_FROMDATE"
                                SortExpression="CMFCXSSI_FROMDATE" AllowFiltering="false" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CMFCXSSI_TODATE" HeaderText="TODATE" DataField="CMFCXSSI_TODATE"
                                SortExpression="CMFCXSSI_TODATE" AllowFiltering="false" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CMFCXSSI_CEASEDATE" HeaderText="CEASEDATE" DataField="CMFCXSSI_CEASEDATE"
                                SortExpression="CMFCXSSI_CEASEDATE" AllowFiltering="false" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CMFCXSSI_PERIODICIT" HeaderText="PERIODICIT"
                                DataField="CMFCXSSI_PERIODICIT" SortExpression="CMFCXSSI_PERIODICIT" AllowFiltering="false"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <ItemStyle HorizontalAlign="left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CMFCXSSI_PERIODDAY" HeaderText="PERIODDAY" DataField="CMFCXSSI_PERIODDAY"
                                SortExpression="CMFCXSSI_PERIODDAY" AllowFiltering="false" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <ItemStyle HorizontalAlign="right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CMFCXSSI_INVIIN" HeaderText="INVIIN" DataField="CMFCXSSI_INVIIN"
                                SortExpression="CMFCXSSI_INVIIN" AllowFiltering="false" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <ItemStyle HorizontalAlign="right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CMFCXSSI_PAYMENTMO" HeaderText="PAYMENTMO" DataField="CMFCXSSI_PAYMENTMO"
                                SortExpression="CMFCXSSI_PAYMENTMO" AllowFiltering="false" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <ItemStyle HorizontalAlign="left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CMFCXSSI_TARGETSCH" HeaderText="TARGETSCH" DataField="CMFCXSSI_TARGETSCH"
                                SortExpression="CMFCXSSI_TARGETSCH" AllowFiltering="false" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <ItemStyle HorizontalAlign="left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CMFCXSSI_REGDATE" HeaderText="REGDATE" DataField="CMFCXSSI_REGDATE"
                                SortExpression="CMFCXSSI_REGDATE" AllowFiltering="false" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CMFCXSSI_SUBBROKER" HeaderText="SUBBROKER" DataField="CMFCXSSI_SUBBROKER"
                                SortExpression="CMFCXSSI_SUBBROKER" AllowFiltering="false" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <ItemStyle HorizontalAlign="left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WUPL_ProcessId" HeaderText="ProcessId" DataField="WUPL_ProcessId"
                                SortExpression="WUPL_ProcessId" AllowFiltering="true" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <ItemStyle HorizontalAlign="right" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle Width="100px" />
                    <ClientSettings>
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
