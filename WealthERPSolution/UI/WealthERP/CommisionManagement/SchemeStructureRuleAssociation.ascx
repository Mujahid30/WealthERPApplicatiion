<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SchemeStructureRuleAssociation.ascx.cs"
    Inherits="WealthERP.CommisionManagement.SchemeStructureRuleAssociation" %>
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
                        <td align="left">
                            View Scheme Rules
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="ibtExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                Visible="false" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                OnClick="ibtExport_OnClick" Height="25px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<br />
<div style="width: 100%; overflow: scroll;">
    <telerik:RadGrid ID="RadGridSchemeRule" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true"
        OnNeedDataSource="RadGridStructureRule_NeedDataSource"
        AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" EnableHeaderContextMenu="true"
        EnableHeaderContextFilterMenu="true">
        <%--<exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                    filename="Company/Sector" excel-format="ExcelML">--%>
        <%-- </exportsettings>--%>
        <ExportSettings Excel-Format="ExcelML">
        </ExportSettings>
        <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
            CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
            GroupLoadMode="Client" ShowGroupFooter="true" HeaderStyle-Wrap="false">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="180px" HeaderText="Scheme Plan" DataField="PASP_SchemePlanName"
                    UniqueName="PASP_SchemePlanName" SortExpression="PASP_SchemePlanName" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Structure Name" DataField="ACSM_CommissionStructureName"
                    UniqueName="ACSM_CommissionStructureName" SortExpression="ACSM_CommissionStructureName" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Validity Start Date" DataField="ACSTSM_ValidityStart"
                   UniqueName="ACSTSM_ValidityStart" DataFormatString="{0:dd/MM/yyyy}" SortExpression="ACSTSM_ValidityStart" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Validity End Date" DataField="ACSTSM_ValidityEnd"
                    UniqueName="ACSTSM_ValidityEnd" DataFormatString="{0:dd/MM/yyyy}" SortExpression="ACSTSM_ValidityEnd" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Frequency" DataField="XF_Frequency"
                    UniqueName="XF_Frequency" SortExpression="XF_Frequency" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Applicable Level" DataField="WCAL_ApplicableLevelCode"
                    UniqueName="WCAL_ApplicableLevelCode" SortExpression="WCAL_ApplicableLevelCode" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Is ServiceTax Reduced" DataField="ACSR_IsServiceTaxReduced"
                    UniqueName="ACSR_IsServiceTaxReduced" SortExpression="ACSR_IsServiceTaxReduced" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Is TDS Reduced" DataField="ACSR_IsTDSReduced"
                    UniqueName="ACSR_IsTDSReduced" SortExpression="ACSR_IsTDSReduced" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Is OtherTax Reduced" DataField="ACSM_IsOtherTaxReduced"
                    UniqueName="ACSM_IsOtherTaxReduced" SortExpression="ACSM_IsOtherTaxReduced" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Min. Invest Amount" DataField="ACSR_MinInvestmentAmount"
                    UniqueName="ACSR_MinInvestmentAmount" SortExpression="ACSR_MinInvestmentAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Max. Invest Amount" DataField="ACSR_MaxInvestmentAmount"
                    UniqueName="ACSR_MaxInvestmentAmount" SortExpression="ACSR_MaxInvestmentAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
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
                    <ItemStyle Width="" HorizontalAlign ="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Tenure Unit" DataField="ACSR_TenureUnit"
                    UniqueName="ACSR_TenureUnit " SortExpression="ACSR_TenureUnit" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Min. Invest Age In Day" DataField="ACSR_MinInvestmentAge"
                    UniqueName="ACSR_MinInvestmentAge" SortExpression="ACSR_MinInvestmentAge" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Max. Invest Age In Day" DataField="ACSR_MaxInvestmentAge"
                    UniqueName="ACSR_MaxInvestmentAge " SortExpression="ACSR_MaxInvestmentAge " AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Transaction Type" DataField="ACSR_TransactionType"
                    UniqueName="ACSR_TransactionType" SortExpression="ACSR_TransactionType" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="SIP Trans Frequency" DataField="ACSR_SIPFrequency"
                    UniqueName="ACSR_SIPFrequency" SortExpression="ACSR_SIPFrequency" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Min. No Of Applications" DataField="ACSR_MinNumberOfApplications"
                    UniqueName="ACSR_MinNumberOfApplications" SortExpression="ACSR_MinNumberOfApplications" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Brokerage Value" DataField="ACSR_BrokerageValue"
                    UniqueName="ACSR_BrokerageValue" SortExpression="ACSR_BrokerageValue" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Brokerage Unit" DataField="WCU_Unit"
                    UniqueName="WCU_Unit" SortExpression="WCU_Unit" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Calculated On" DataField="WCCO_CalculatedOn"
                    UniqueName="WCCO_CalculatedOn" SortExpression="WCCO_CalculatedOn" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                    
            </telerik:GridBoundColumn>
                
               
                
                
                
            </Columns>
        </MasterTableView>
        <HeaderStyle Width="120px" />
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
