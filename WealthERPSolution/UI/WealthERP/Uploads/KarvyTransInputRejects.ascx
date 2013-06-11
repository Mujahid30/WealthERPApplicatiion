<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KarvyTransInputRejects.ascx.cs"
    Inherits="WealthERP.Uploads.KarvyTransInputRejects" %>
 
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left"  style="width: 50%">
                            Karvy Transaction Input Rejects
                        </td>
                         <td align="right"  style="width: 48%">
                            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="View ProcessLog"
                                OnClick="lnkBtnBack_Click"></asp:LinkButton>
                        </td>
                        <td align="right"  style="width: 2%">
                            <asp:ImageButton ID="imgBtnrgHoldings" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="22px" Width="25px"></asp:ImageButton>
                        </td>
                       
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<br />
<div runat="server" id="divGvRejectedRecords" visible="false" style="width:99%;overflow:scroll;">
    <telerik:RadGrid ID="gvRejectedRecords" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
        AllowAutomaticInserts="false" ExportSettings-FileName="UPLOAD HISTORY DETAILS"
        OnNeedDataSource="gvRejectedRecords_NeedDataSource">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView DataKeyNames="KarvyTransInputId" Width="100%" AllowMultiColumnSorting="True"
            AutoGenerateColumns="false" CommandItemDisplay="None">
            <Columns>
                <telerik:GridBoundColumn DataField="ProcessId" HeaderText="PROCESS ID" UniqueName="ProcessId"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="INV_NAME" HeaderText="INVESTOR NAME" UniqueName="INV_NAME"
                    SortExpression="INV_NAME" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FOLIO_NUMBER" HeaderText="FOLIO NUMBER" UniqueName="FOLIO_NUMBER"
                    SortExpression="FOLIO_NUMBER" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TRANSACTION_NUMBER" HeaderText="TRANSACTION NUMBER"
                    UniqueName="TRANSACTION_NUMBER" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PRODUCT_CODE" HeaderText="PRODUCT CODE" UniqueName="PRODUCT_CODE"
                    SortExpression="PRODUCT_CODE" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SCHEME_NAME" HeaderText="SCHEME NAME" UniqueName="SCHEME_NAME"
                    SortExpression="SCHEME_NAME" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TRANSACTION_DATE" HeaderText="TRANSACTION DATE" UniqueName="TRANSACTION_DATE"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PRICE" HeaderText="PRICE" UniqueName="PRICE"
                    SortExpression="PRICE" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="UNITS" HeaderText="UNITS" UniqueName="UNITS"
                    SortExpression="UNITS" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AMOUNT" HeaderText="AMOUNT" UniqueName="AMOUNT"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="STT" HeaderText="STT" UniqueName="STT" SortExpression="STT"
                    AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
<br />
<table>
    <tr id ="trNote" runat="server">
        <td colspan="2">
            <div id="Div2" class="Note">
                <p>
                    <span style="font-weight: bold">Note:</span><br />
                    1.Please remove these records from the file and reupload <br />
                      2. Clean the file before uploading 
                </p>
            </div>
        </td>
    </tr>
</table>
