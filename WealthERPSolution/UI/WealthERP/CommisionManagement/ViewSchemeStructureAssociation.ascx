<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewSchemeStructureAssociation.ascx.cs"
    Inherits="WealthERP.CommisionManagement.ViewSchemeStructureAssociation" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<style type="text/css">
    .nowrap
    {
        overflow: hidden;
        white-space: nowrap;
    }
</style>

<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<asp:UpdatePanel ID="upCMGrid" runat="server">
    <ContentTemplate>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            View Scheme Structure Association
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="ibtExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png" Visible="false"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="ibtExportSummary_OnClick"
                                Height="25px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<br />
<div>
    <telerik:RadGrid ID="rgvSchemeToStruct" runat="server" GridLines="None" AutoGenerateColumns="False"
        AllowSorting="true" AllowPaging="true" ShowStatusBar="True" ShowFooter="true" OnItemDataBound="rgvSchemeToStruct_ItemDataBound"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true" OnNeedDataSource="rgvSchemeToStruct_NeedDataSource"
        AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" EnableHeaderContextMenu="true"
        EnableHeaderContextFilterMenu="true">
        <ExportSettings excel-format="ExcelML"></ExportSettings>
        <MasterTableView Width="100%" AllowMultiColumnSorting="true" AutoGenerateColumns="false"
            CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
            GroupLoadMode="Client" ShowGroupFooter="true" DataKeyNames="StructureId">
            <Columns>                
                <telerik:GridBoundColumn HeaderStyle-Width="120px" HeaderText="Scheme Plan" DataField="PASP_SchemePlanName"
                    UniqueName="PASP_SchemePlanName" SortExpression="PASP_SchemePlanName" FilterControlWidth="60%"
                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true" >
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="120px" HeaderText="Commission Structure"
                    HeaderStyle-HorizontalAlign="left" DataField="ACSM_CommissionStructureName"
                    UniqueName="ACSM_CommissionStructureName" SortExpression="ACSM_CommissionStructureName"
                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true" FilterControlWidth="60%"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtStruct" runat="server" OnClick="lbtStruct_Click"
                            AutoPostBack="true" CssClass="" Width="300px" EnableEmbeddedSkins="false">                            
                        </asp:LinkButton>
                    </ItemTemplate>                
                </telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn  HeaderStyle-Width="40px" HeaderText="Validity Start" DataField="ACSTSM_ValidityStart"
                 HeaderStyle-HorizontalAlign="Center"   UniqueName="ACSTSM_ValidityStart" SortExpression="ACSTSM_ValidityStart"
                    CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:dd/MM/yyyy}" >
                    <ItemStyle HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                </telerik:GridDateTimeColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="40px" HeaderText="Validity End" DataField="ACSTSM_ValidityEnd"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="ACSTSM_ValidityEnd" SortExpression="ACSTSM_ValidityEnd"
                    CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                    FooterStyle-HorizontalAlign="Right" DataFormatString="{0:dd/MM/yyyy}">
                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                </telerik:GridDateTimeColumn>
            </Columns>
            <PagerStyle AlwaysVisible="True" />
        </MasterTableView>
        <HeaderStyle Width="150px" />
        <ClientSettings>
            <Selecting AllowRowSelect="true" EnableDragToSelectRows="true" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
        <FilterMenu EnableEmbeddedSkins="false"></FilterMenu>
    </telerik:RadGrid>
    </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="ibtExportSummary" />
    </Triggers>
</asp:UpdatePanel>


