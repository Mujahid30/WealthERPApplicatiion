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
<table>
    <tr>
        <td class="leftLabel">
            <asp:Label ID="lblStatusStage2" runat="server" Text="Pick Product:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlProductType" runat="server" CssClass="cmbField" AutoPostBack="true">
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Select Product Type" Display="Dynamic" ControlToValidate="ddlProductType"
                InitialValue="Select" ValidationGroup="btnGo">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnGo" CssClass="PCGButton" Text="Go" runat="server" ValidationGroup="btnGo"
                OnClick="btnGo_Click" />
        </td>
    </tr>
</table>
<div style="width: 100%; overflow: scroll;">
    <telerik:RadGrid ID="RadGridSchemeRule" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true"
        OnNeedDataSource="RadGridStructureRule_NeedDataSource" AllowAutomaticInserts="false"
        ExportSettings-ExportOnlyData="true" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true">
        <%--<exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                    filename="Company/Sector" excel-format="ExcelML">--%>
        <%-- </exportsettings>--%>
        <ExportSettings Excel-Format="ExcelML">
        </ExportSettings>
        <MasterTableView DataKeyNames="ACSM_CommissionStructureId" Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
            CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
            GroupLoadMode="Client" ShowGroupFooter="true" HeaderStyle-Wrap="false">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="180px" HeaderText="Scheme Plan" DataField="PASP_SchemePlanName"
                    UniqueName="PASP_SchemePlanName" SortExpression="PASP_SchemePlanName" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="150px" HeaderText="Structure Name"
                    DataField="ACSM_CommissionStructureName" UniqueName="ACSM_CommissionStructureName"
                    SortExpression="ACSM_CommissionStructureName" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtStruct" runat="server" OnClick="lbtStruct_Click" AutoPostBack="true"
                            CssClass="" Width="300px" EnableEmbeddedSkins="false" Text='<%#Eval("ACSM_CommissionStructureName") %>'>                            
                        </asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Validity Start Date"
                    DataField="ACSTSM_ValidityStart" UniqueName="ACSTSM_ValidityStart" DataFormatString="{0:dd/MM/yyyy}"
                    SortExpression="ACSTSM_ValidityStart" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Validity End Date"
                    DataField="ACSTSM_ValidityEnd" UniqueName="ACSTSM_ValidityEnd" DataFormatString="{0:dd/MM/yyyy}"
                    SortExpression="ACSTSM_ValidityEnd" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Frequency" DataField="XF_Frequency"
                    UniqueName="XF_Frequency" SortExpression="XF_Frequency" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Applicable Level"
                    DataField="WCAL_ApplicableLevelCode" UniqueName="WCAL_ApplicableLevelCode" SortExpression="WCAL_ApplicableLevelCode"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
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
                <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Min. Invest Age In Day"
                    DataField="ACSR_MinInvestmentAge" UniqueName="ACSR_MinInvestmentAge" SortExpression="ACSR_MinInvestmentAge"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Max. Invest Age In Day"
                    DataField="ACSR_MaxInvestmentAge" UniqueName="ACSR_MaxInvestmentAge " SortExpression="ACSR_MaxInvestmentAge "
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Transaction Type"
                    DataField="ACSR_TransactionType" UniqueName="ACSR_TransactionType" SortExpression="ACSR_TransactionType"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="SIP Trans Frequency"
                    DataField="ACSR_SIPFrequency" UniqueName="ACSR_SIPFrequency" SortExpression="ACSR_SIPFrequency"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Min. No Of Applications"
                    DataField="ACSR_MinNumberOfApplications" UniqueName="ACSR_MinNumberOfApplications"
                    SortExpression="ACSR_MinNumberOfApplications" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Receivable Brokerage Value" DataField="RecievableValue"
                    UniqueName="RecievableValue" SortExpression="RecievableValue" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Receivable Brokerage Unit" DataField="RecievableUnit"
                    UniqueName="RecievableUnit" SortExpression="RecievableUnit" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Payable Brokerage Value" DataField="PaybleValue"
                    UniqueName="PaybleValue" SortExpression="PaybleValue" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Payable Brokerage Unit" DataField="PaybleUnit"
                    UniqueName="PaybleUnit" SortExpression="PaybleUnit" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
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
    <%--</div>

<div style="width: 100%; overflow: scroll;">--%>
    <telerik:RadGrid ID="RadGridIssueStructureRule" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true"
        OnNeedDataSource="RadGridIssueStructureRule_NeedDataSource" AllowAutomaticInserts="false"
        ExportSettings-ExportOnlyData="true" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true">
        <%--<exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                    filename="Company/Sector" excel-format="ExcelML">--%>
        <%-- </exportsettings>--%>
        <ExportSettings Excel-Format="ExcelML">
        </ExportSettings>
        <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
            CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
            GroupLoadMode="Client" ShowGroupFooter="true" HeaderStyle-Wrap="false" DataKeyNames="ACSM_CommissionStructureId">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="180px" HeaderText="Issue Name" DataField="AIM_Issuename"
                    UniqueName="AIM_Issuename" SortExpression="AIM_Issuename" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="150px" HeaderText="Structure Name"
                    DataField="ACSM_CommissionStructureName" UniqueName="ACSM_CommissionStructureName"
                    SortExpression="ACSM_CommissionStructureName" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtStructs" runat="server" OnClick="lbtStructs_Click" AutoPostBack="true"
                            CssClass="" Width="300px" EnableEmbeddedSkins="false" Text='<%#Eval("ACSM_CommissionStructureName") %>'>                            
                        </asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Rule Name"
                    DataField="ACSR_CommissionStructureRuleName" UniqueName="ACSR_CommissionStructureRuleName" 
                    SortExpression="ACSR_CommissionStructureRuleName" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Validity Start Date"
                    DataField="ACSTSM_ValidityStart" UniqueName="ACSTSM_ValidityStart" DataFormatString="{0:dd/MM/yyyy}"
                    SortExpression="ACSTSM_ValidityStart" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Validity End Date"
                    DataField="ACSTSM_ValidityEnd" UniqueName="ACSTSM_ValidityEnd" DataFormatString="{0:dd/MM/yyyy}"
                    SortExpression="ACSTSM_ValidityEnd" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Receivable Brokerage Value" DataField="RecievableValue"
                    UniqueName="RecievableValue" SortExpression="RecievableValue" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Receivable Brokerage Unit" DataField="RecievableUnit"
                    UniqueName="RecievableUnit" SortExpression="RecievableUnit" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Payable Brokerage Value" DataField="PaybleValue"
                    UniqueName="PaybleValue" SortExpression="PaybleValue" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="EqualTo">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Payable Brokerage Unit" DataField="PaybleUnit"
                    UniqueName="PaybleUnit" SortExpression="PaybleUnit" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Broker Name" DataField="AIM_BrokerName"
                    UniqueName="AIM_BrokerName" SortExpression="PaybleUnit" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Commission Type" DataField="WCT_CommissionType"
                    UniqueName="WCT_CommissionType" SortExpression="PaybleUnit" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
               
                    <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Broker" DataField="WCMV_Name"
                    UniqueName="WCMV_Name" SortExpression="PaybleUnit" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Broker Commission Type" DataField="XB_BrokerShortName"
                    UniqueName="XB_BrokerShortName" SortExpression="PaybleUnit" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
               
               
                
                 <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Service TaxValue" DataField="ACSR_ServiceTaxValue"
                    UniqueName="ACSR_ServiceTaxValue" SortExpression="ACSR_ServiceTaxValue" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="TDS Value" DataField="ACSR_ReducedValue"
                    UniqueName="ACSR_ReducedValue" SortExpression="ACSR_ReducedValue" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
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
