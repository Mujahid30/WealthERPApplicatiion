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
<%--<table id="tblCommissionStructureRuleView" runat="server" width="99%" visible="false">
    <tr>
        <td>--%>
            <asp:Panel ID="Panel2" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal" Visible="false">
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
                                AutoPostBackOnFilter="true" UniqueName="AC_CategoryName" HeaderText="Category Name"
                                DataField="AC_CategoryName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="ACSM_CommissionStructureName" HeaderText="Commission Structure Name"
                                DataField="ACSM_CommissionStructureName">
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="RuleType" HeaderText="Rules" DataField="RuleType">
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="ACSM_ValidityStartDate" HeaderText="Validity Start Date"
                                DataFormatString="{0:d}" DataField="ACSM_ValidityStartDate">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="ACSM_ValidityEndDate" HeaderText="Validity End Date"
                                DataFormatString="{0:d}" DataField="ACSM_ValidityEndDate">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Is ServiceTax Reduced"
                                DataField="ACSR_IsServiceTaxReduced" UniqueName="ACSR_IsServiceTaxReduced" SortExpression="ACSR_IsServiceTaxReduced"
                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Is TDS Reduced" DataField="ACSR_IsTDSReduced"
                                UniqueName="ACSR_IsTDSReduced" SortExpression="ACSR_IsTDSReduced" AutoPostBackOnFilter="true"
                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Is OtherTax Reduced"
                                DataField="ACSM_IsOtherTaxReduced" UniqueName="ACSM_IsOtherTaxReduced" SortExpression="ACSM_IsOtherTaxReduced"
                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Min. Invest Amount"
                                DataField="ACSR_MinInvestmentAmount" UniqueName="ACSR_MinInvestmentAmount" SortExpression="ACSR_MinInvestmentAmount"
                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Max. Invest Amount"
                                DataField="ACSR_MaxInvestmentAmount" UniqueName="ACSR_MaxInvestmentAmount" SortExpression="ACSR_MaxInvestmentAmount"
                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Min. Tenure" DataField="ACSR_MinTenure"
                                UniqueName="ACSR_MinTenure" SortExpression="ACSR_MinTenure" AutoPostBackOnFilter="true"
                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Max. Tenure" DataField="ACSR_MaxTenure"
                                UniqueName="ACSR_MaxTenure" SortExpression="ACSR_MaxTenure" AutoPostBackOnFilter="true"
                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Tenure Unit" DataField="ACSR_TenureUnit"
                                UniqueName="ACSR_TenureUnit " SortExpression="ACSR_TenureUnit" AutoPostBackOnFilter="true"
                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Min. Invest Age In Day"
                                DataField="ACSR_MinInvestmentAge" UniqueName="ACSR_MinInvestmentAge" SortExpression="ACSR_MinInvestmentAge"
                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Max. Invest Age In Day"
                                DataField="ACSR_MaxInvestmentAge" UniqueName="ACSR_MaxInvestmentAge " SortExpression="ACSR_MaxInvestmentAge "
                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Transaction Type"
                                DataField="ACSR_TransactionType" UniqueName="ACSR_TransactionType" SortExpression="ACSR_TransactionType"
                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="SIP Trans Frequency"
                                DataField="ACSR_SIPFrequency" UniqueName="ACSR_SIPFrequency" SortExpression="ACSR_SIPFrequency"
                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Min. No Of Applications"
                                DataField="ACSR_MinNumberOfApplications" UniqueName="ACSR_MinNumberOfApplications"
                                SortExpression="ACSR_MinNumberOfApplications" AutoPostBackOnFilter="true" AllowFiltering="true"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Brokerage Value" DataField="ACSR_BrokerageValue"
                                UniqueName="ACSR_BrokerageValue" SortExpression="ACSR_BrokerageValue" AutoPostBackOnFilter="true"
                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                           
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </asp:Panel>
      <%--  </td>
    </tr>
</table>--%>
