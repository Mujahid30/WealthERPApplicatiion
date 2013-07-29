<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewSchemeStructureAssociation.ascx.cs"
    Inherits="WealthERP.CommisionManagement.ViewSchemeStructureAssociation" %>
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
                            View Scheme Structure
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<br />
<div style="width:100%;overflow:scroll;">
    <telerik:RadGrid ID="RadGrid1" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="80%" AllowFilteringByColumn="true"
        AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" EnableHeaderContextMenu="true"
        EnableHeaderContextFilterMenu="true">
        <%--<exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                    filename="Company/Sector" excel-format="ExcelML">--%>
        <%-- </exportsettings>--%>
        <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
            CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
            GroupLoadMode="Client" ShowGroupFooter="true">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="SchemePlanName" DataField="PASP_SchemePlanName"
                    UniqueName="PASP_SchemePlanName" SortExpression="PASP_SchemePlanName" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="ValidityStart" DataField="ACSTSM_ValidityStart"
                    UniqueName="ACSTSM_ValidityStart" SortExpression="ACSTSM_ValidityStart" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="ValidityEnd" DataField="ACSTSM_ValidityEnd"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="ACSTSM_ValidityEnd" SortExpression="ACSTSM_ValidityEnd"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="CommissionStructureName"
                    DataField="ACSM_CommissionStructureName" HeaderStyle-HorizontalAlign="Right"
                    UniqueName="ACSM_CommissionStructureName" SortExpression="ACSM_CommissionStructureName"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <HeaderStyle Width="150px" />
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
