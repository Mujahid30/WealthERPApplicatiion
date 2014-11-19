<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PayableStructureView.ascx.cs"
    Inherits="WealthERP.CommisionManagement.PayableStructureView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Payable Structure View With Associate Details
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td class="leftLabel">
            <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="false">
            </asp:DropDownList>
            <span id="Span6" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Select Category"
                CssClass="rfvPCG" ControlToValidate="ddlCategory" ValidationGroup="btnGo" Display="Dynamic"
                InitialValue="0"></asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="PCGButton" ValidationGroup="btnGo"
                OnClick="btnGo_Click" />
        </td>
    </tr>
</table>
<table id="tblCommissionStructureRuleView" runat="server" width="99%" visible="false">
    <tr>
        <td>
            <asp:Panel ID="Panel2" runat="server" class="Landscape" Width="90%" ScrollBars="Horizontal">
                <telerik:RadGrid ID="gvStructureView" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true"
                    AllowAutomaticInserts="false" ExportSettings-FileName="Issue List" OnNeedDataSource="RadGridStructureRule_NeedDataSource">
                    <%-- <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                            FileName="MF Order Recon" Excel-Format="ExcelML">
                        </ExportSettings>--%>
                    <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                        CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="AA_ContactPersonName" HeaderText="Associate Name"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                DataField="AA_ContactPersonName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AAC_AgentCode" HeaderText="SubBroker Code"
                                DataField="AAC_AgentCode">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="ACSR_MinInvestmentAmount" HeaderText="Category Name"
                                DataField="AC_CategoryName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="ACSM_CommissionStructureName" HeaderText="Commission Structure Name"
                                DataField="ACSM_CommissionStructureName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="RuleType" HeaderText="Rules" DataField="RuleType">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="ACSM_ValidityStartDate" HeaderText="Validity Start Date"
                                DataFormatString="{0:d}" DataField="ACSM_ValidityStartDate">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="ACSM_ValidityEndDate" HeaderText="Validity End Date"
                                DataFormatString="{0:d}" DataField="ACSM_ValidityEndDate">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </asp:Panel>
        </td>
    </tr>
</table>
