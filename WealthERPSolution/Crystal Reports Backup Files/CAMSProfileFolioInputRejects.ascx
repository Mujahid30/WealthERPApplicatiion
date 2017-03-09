<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CAMSProfileFolioInputRejects.ascx.cs"
    Inherits="WealthERP.Uploads.CAMSProfileFolioInputRejects" %>
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
                        <td align="left" style="width: 50%">
                            CAMS Profile Input Rejects
                        </td>
                     
                        <td align="right" style="width: 48%">
                            <asp:LinkButton runat="server" ID="LinkButton1" CssClass="LinkButtons" Text="View ProcessLog"
                                OnClick="lnkBtnBack_Click"></asp:LinkButton>
                        </td>
                           <td align="right" style="width: 2%">
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
<div runat="server" id="divGvRejectedRecords" style="width:99%;overflow:scroll;" visible="false">
    <telerik:RadGrid ID="gvRejectedRecords" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
        AllowAutomaticInserts="false" ExportSettings-FileName="CAMS input reject details"
        OnNeedDataSource="gvRejectedRecords_NeedDataSource">
        <exportsettings hidestructurecolumns="true">
        </exportsettings>
        <mastertableview datakeynames="CAMSProfileInputId" width="100%" allowmulticolumnsorting="True"
            autogeneratecolumns="false" commanditemdisplay="None">
            <Columns>
                <telerik:GridBoundColumn DataField="ProcessId" HeaderText="PROCESS ID" UniqueName="ProcessId"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn DataField="FOLIO_NUMBER" HeaderText="FOLIO NUMBER" UniqueName="FOLIO_NUMBER"
                    SortExpression="FOLIO_NUMBER" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
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
                <telerik:GridBoundColumn DataField="REPORT_DATE" HeaderText="REPORT DATE" UniqueName="REPORT_DATE"
                    SortExpression="REPORT_DATE" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PHONE_OFF" HeaderText="PHONE OFF" UniqueName="PHONE_OFF"
                    SortExpression="PHONE_OFF" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PHONE_RES" HeaderText="PHONE RES" UniqueName="PHONE_RES"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RUPEE_BAL" HeaderText="RUPEE BAL" UniqueName="RUPEE_BAL" SortExpression="RUPEE_BAL"
                    AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                
                
                <telerik:GridBoundColumn DataField="CMGCXPI_DOB" HeaderText="DOB" UniqueName="CMGCXPI_DOB"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CMGCXPI_Mobile" HeaderText="Mobile" UniqueName="CMGCXPI_Mobile"
                    SortExpression="CMGCXPI_Mobile" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CMGCXPI_BankAddress1" HeaderText="BankAddress1" UniqueName="CMGCXPI_BankAddress1"
                    SortExpression="CMGCXPI_BankAddress1" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CMGCXPI_BankAddress2" HeaderText="BankAddress2" UniqueName="CMGCXPI_BankAddress2"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CMGCXPI_BankAddress3" HeaderText="BankAddress3" UniqueName="CMGCXPI_BankAddress3" SortExpression="CMGCXPI_BankAddress3"
                    AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn><telerik:GridBoundColumn DataField="CMGCXPI_BankCity" HeaderText="BankCity" UniqueName="CMGCXPI_BankCity"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CMGCXPI_BankPinCode" HeaderText="BankPinCode" UniqueName="CMGCXPI_BankPinCode"
                    SortExpression="CMGCXPI_BankPinCode" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CMGCXPI_ACCNT_NO" HeaderText="ACCNT NO" UniqueName="CMGCXPI_ACCNT_NO"
                    SortExpression="CMGCXPI_ACCNT_NO" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CMGCXPI_BANK_NAME" HeaderText="BANK NAME" UniqueName="CMGCXPI_BANK_NAME"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CMGCXPI_AC_TYPE" HeaderText="AC TYPE" UniqueName="CMGCXPI_AC_TYPE" SortExpression="CMGCXPI_AC_TYPE"
                    AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn><telerik:GridBoundColumn DataField="CMGCXPI_BRANCH" HeaderText="BRANCH" UniqueName="CMGCXPI_BRANCH"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CMGCXPI_PRODUCT" HeaderText="PRODUCT" UniqueName="CMGCXPI_PRODUCT"
                    SortExpression="CMGCXPI_PRODUCT" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CMGCXPI_SCH_NAME" HeaderText="SCH NAME" UniqueName="CMGCXPI_SCH_NAME"
                    SortExpression="CMGCXPI_SCH_NAME" AutoPostBackOnFilter="true" ShowFilterIcon="false">
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
