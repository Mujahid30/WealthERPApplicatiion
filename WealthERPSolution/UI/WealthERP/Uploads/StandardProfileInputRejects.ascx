<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StandardProfileInputRejects.ascx.cs"
    Inherits="WealthERP.Uploads.StandardProfileInputRejects" %>
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
                            Standard Input Profile Rejects
                        </td>
                        <td align="right" style="width: 48%">
                            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="View ProcessLog"
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
<div runat="server" id="divGvRejectedRecords" visible="false" style="width: 99%;
    overflow: scroll;">
    <telerik:RadGrid ID="gvRejectedRecords" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
        AllowAutomaticInserts="false" ExportSettings-FileName="UPLOAD HISTORY DETAILS"
        OnNeedDataSource="gvRejectedRecords_NeedDataSource">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView DataKeyNames="CAMSTransInputId" Width="100%" AllowMultiColumnSorting="True"
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
                <telerik:GridBoundColumn DataField="PAN_NO" HeaderText="PAN NO" UniqueName="PAN_NO"
                    SortExpression="PAN_NO" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CUST_ADDRESS" HeaderText="CUST ADDRESS" UniqueName="CUST_ADDRESS"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CITY" HeaderText="CITY" UniqueName="CITY" SortExpression="CITY"
                    AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PIN_CODE" HeaderText="PIN CODE" UniqueName="PIN_CODE"
                    SortExpression="PIN_CODE" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PIN_CODE2" HeaderText="PIN CODE2" UniqueName="PIN_CODE2"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RES_ISD_CODE" HeaderText="RES ISD CODE" UniqueName="RES_ISD_CODE"
                    SortExpression="RES_ISD_CODE" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RES_STD_CODE" HeaderText="RES STD CODE" UniqueName="RES_STD_CODE"
                    SortExpression="RES_STD_CODE" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RES_PHONE_NUM" HeaderText="RES PHONE NUM" UniqueName="RES_PHONE_NUM"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OFC_ISD_C0DE" HeaderText="OFC ISD C0DE" UniqueName="OFC_ISD_C0DE"
                    SortExpression="OFC_ISD_C0DE" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OFC_STD_CODE" HeaderText="OFC STD CODE" UniqueName="OFC_STD_CODE"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OFC_PHONE_NUM" HeaderText="OFC PHONE NUM" UniqueName="OFC_PHONE_NUM"
                    SortExpression="OFC_PHONE_NUM" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MOBILE1" HeaderText="MOBILE1" UniqueName="MOBILE1"
                    SortExpression="MOBILE1" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MOBILE2" HeaderText="MOBILE2" UniqueName="MOBILE2"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ISD_FAX" HeaderText="ISD FAX" UniqueName="ISD_FAX"
                    SortExpression="ISD_FAX" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="STD_FAX" HeaderText="STD FAX" UniqueName="STD_FAX"
                    SortExpression="STD_FAX" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FAX" HeaderText="FAX" UniqueName="FAX" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OFC_FAX" HeaderText="OFC FAX" UniqueName="OFC_FAX"
                    SortExpression="OFC_FAX" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OFC_FAX_ISD" HeaderText="OFC FAX ISD" UniqueName="OFC_FAX_ISD"
                    SortExpression="OFC_FAX_ISD" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OFC_STD_FAX" HeaderText="OFC STD FAX" UniqueName="OFC_STD_FAX"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OFC_PIN_CODE" HeaderText="OFC PIN CODE" UniqueName="OFC_PIN_CODE"
                    SortExpression="OFC_PIN_CODE" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BRANCH_PIN_CODE" HeaderText="BRANCH PIN CODE"
                    UniqueName="BRANCH_PIN_CODE" SortExpression="BRANCH_PIN_CODE" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MICR" HeaderText="MICR" UniqueName="MICR" SortExpression="MICR"
                    AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="COMMENCEMENT_DATE" HeaderText="COMMENCEMENT DATE"
                    UniqueName="COMMENCEMENT_DATE" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="REGISTRATION_DATE" HeaderText="REGISTRATION DATE"
                    UniqueName="REGISTRATION_DATE" SortExpression="REGISTRATION_DATE" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RBI_APPROVAL_DATE" HeaderText="RBI APPROVAL DATE"
                    UniqueName="RBI_APPROVAL_DATE" SortExpression="RBI_APPROVAL_DATE" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MARRIAGE_DATE" HeaderText="MARRIAGE DATE" UniqueName="MARRIAGE_DATE"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DATE_OF_BIRTH" HeaderText="DOB" UniqueName="DATE_OF_BIRTH"
                    SortExpression="DATE_OF_BIRTH" AutoPostBackOnFilter="true" ShowFilterIcon="false">
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
