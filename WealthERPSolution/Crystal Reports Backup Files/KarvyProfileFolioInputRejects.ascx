<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KarvyProfileFolioInputRejects.ascx.cs"
    Inherits="WealthERP.Uploads.KarvyProfileFolioInputRejects" %>
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
                        <td align="left"   style="width: 50%">
                            Karvy Profile Input Rejects
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
        AllowAutomaticInserts="false" ExportSettings-FileName="KARVY input reject details"
        OnNeedDataSource="gvRejectedRecords_NeedDataSource">
        <exportsettings hidestructurecolumns="true">
        </exportsettings>
        <mastertableview datakeynames="KarvyProfileInputId" width="100%" allowmulticolumnsorting="True"
            autogeneratecolumns="false" commanditemdisplay="None">
            <Columns>
                <telerik:GridBoundColumn DataField="ProcessId" HeaderText="PROCESS ID" UniqueName="ProcessId"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="INV_NAME" HeaderText="INVESTOR NAME" UniqueName="INV_NAME"
                    SortExpression="INV_NAME" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PAN_NO" HeaderText="PAN NO" UniqueName="PAN_NO"
                    SortExpression="PAN_NO" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BROKER_CODE" HeaderText="BROKER CODE"
                    UniqueName="BROKER_CODE" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CUST_ADDRESS" HeaderText="CUST ADDRESS" UniqueName="CUST_ADDRESS"
                    SortExpression="CUST_ADDRESS" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CITY" HeaderText="CITY" UniqueName="CITY"
                    SortExpression="CITY" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PIN_CODE" HeaderText="PIN CODE" UniqueName="PIN_CODE"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PHONE_OFF" HeaderText="PHONE OFF" UniqueName="PHONE_OFF"
                    SortExpression="PHONE_OFF" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn DataField="PHONE_OFF1" HeaderText="PHONE OFF" UniqueName="PHONE_OFF1"
                    SortExpression="PHONE_OFF1" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn DataField="PHONE_OFF2" HeaderText="PHONE OFF" UniqueName="PHONE_OFF2"
                    SortExpression="PHONE_OFF2" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PHONE_RES" HeaderText="PHONE RES" UniqueName="PHONE_RES"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PHONE_RES1" HeaderText="PHONE RES" UniqueName="PHONE_RES1"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PHONE_RES2" HeaderText="PHONE RES" UniqueName="PHONE_RES2"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FAX_RES" HeaderText="FAX RES" UniqueName="FAX_RES" SortExpression="FAX_RES"
                    AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="FAX_OFF" HeaderText="FAX OFF" UniqueName="FAX_OFF"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BANK_PHONE" HeaderText="BANK PHONE" UniqueName="BANK_PHONE" SortExpression="BANK_PHONE"
                    AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="MOBILE_NO" HeaderText="MOBILE NO" UniqueName="MOBILE_NO"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DATE_OF_BIRTH" HeaderText="DOB" UniqueName="DATE_OF_BIRTH" SortExpression="DATE_OF_BIRTH"
                    AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="REPORT_DATE" HeaderText="REPORT DATE" UniqueName="REPORT_DATE" SortExpression="REPORT_DATE"
                    AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
        </mastertableview>
        <clientsettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
        </clientsettings>
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
