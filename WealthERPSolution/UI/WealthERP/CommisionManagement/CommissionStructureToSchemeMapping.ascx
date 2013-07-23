<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommissionStructureToSchemeMapping.ascx.cs" Inherits="WealthERP.CommisionManagement.CommissionStructureToSchemeMapping" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />

<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<style type="text/css">
    .table
    {
        border: 1px solid orange;
    }
    .leftLabel
    {
        width: 15%;
        text-align: right;
    }
    .rightData
    {
        width: 18%;
        text-align: left;
    }
    .rightDataTwoColumn
    {
        width: 25%;
        text-align: left;
    }
    .rightDataFourColumn
    {
        width: 50%;
        text-align: left;
    }
    .rightDataThreeColumn
    {
        width: 41%;
        text-align: left;
    }
    .tdSectionHeading
    {
        padding-bottom: 6px;
        padding-top: 6px;
        width: 100%;
    }
    .divSectionHeading table td span
    {
        padding-bottom: 5px !important;
    }
    .fltlft
    {
        float: left;
        padding-left: 3px;
        width: 20%;
    }
    .divCollapseImage
    {
        float: left;
        padding-left: 5px;
        width: 2%;
        float: right;
        text-align: right;
        cursor: pointer;
        cursor: hand;
    }
    .imgCollapse
    {
        background: Url(../Images/Section-Expand.png);
        cursor: pointer;
        cursor: hand;
    }
    .imgExpand
    {
        background: Url(../Images/Section-Collapse.png) no-repeat left top;
        cursor: pointer;
        cursor: hand;
    }
    .fltlftStep
    {
        float: left;
    }
    .StepOneContentTable, .StepTwoContentTable, .StageRequestTable, .StepThreeContentTable, .StepFourContentTable
    {
        width: 100%;
    }
    .SectionBody
    {
        width: 100%;
    }
    .collapse
    {
        text-align: right;
    }
    .divStepStatus
    {
        float: left;
        padding-left: 2px;
        padding-right: 5px;
    }
</style>

<asp:UpdatePanel ID="upCMGrid" runat="server">
    <ContentTemplate>
    <table width="100%">
        <tr>
            <td>
                <div class="divPageHeading">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                Schemes mapped to the structures
                            </td>
                            <td align=right>
                                <%--<asp:ImageButton ID="ibtExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="ibtExportSummary_OnClick"
                                    Height="25px" Width="25px"></asp:ImageButton>--%>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr id="trStepOneHeading" runat="server">
            <td class="tdSectionHeading">
                <div class="divSectionHeading" style="vertical-align: text-bottom">                        
                    <div class="fltlft">                           
                        <asp:Label ID="lblStructDetails" runat="server" Text="Structure Details"></asp:Label>
                    </div>                       
                </div>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td class="leftLabel">
                <asp:Label ID="lblStructName" runat="server" Text="Structure:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightData">
                <asp:DropDownList ID="ddlStructures" runat="server" 
                    onselectedindexchanged="ddlStructures_SelectedIndexChanged">
                </asp:DropDownList>
                
            </td>
            <td>
                <asp:Button ID="btnGo" runat="server" Text="Schemes" CssClass="PCGButton" 
                    onclick="btnGo_Click" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr id="tr2" runat="server">
            <td class="tdSectionHeading">
                <div class="divSectionHeading" style="vertical-align: text-bottom">                        
                    <div class="fltlft">                           
                        <asp:Label ID="lblAddNewSchemes" runat="server" Text="Add New Schemes"></asp:Label>
                    </div>                       
                </div>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr id="tr1" runat="server">
            <td class="tdSectionHeading">
                <div class="divSectionHeading" style="vertical-align: text-bottom">                        
                    <div class="fltlft">                           
                        <asp:Label ID="lblSchemes" runat="server" Text="Associated Schemes"></asp:Label>
                    </div>                       
                </div>
            </td>
        </tr>        
    </table>
    <table id="tblCommissionStructureRule" runat="server" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pnlGrid" runat="server" CssClass="Landscape" Width="100%" ScrollBars="Horizontal">
                    <table width="100%">
                        <tr>
                            <td>
                                <telerik:RadGrid ID="gvMappedSchemes" AllowSorting="false" runat="server" AllowAutomaticInserts="false"
                                    EnableLoadOnDemand="True" AllowPaging="True" AutoGenerateColumns="False"
                                    EnableEmbeddedSkins="false" GridLines="none" ShowFooter="true" PagerStyle-AlwaysVisible="true"
                                    EnableViewState="true" ShowStatusBar="true" Skin="Telerik" 
                                    onneeddatasource="gvMappedSchemes_NeedDataSource" 
                                    onpageindexchanged="gvMappedSchemes_PageIndexChanged">
                                    
                                    <HeaderContextMenu EnableEmbeddedSkins="False"></HeaderContextMenu>

                                    <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="CommissionStructureRule"></ExportSettings>
                                    <PagerStyle AlwaysVisible="True" />
                                    <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" 
                                        AutoGenerateColumns="false" Width="100%">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridEditCommandColumn UniqueName="EditCommandColumn"></telerik:GridEditCommandColumn>
                                            <%--<telerik:GridTemplateColumn AllowFiltering="false" DataField="Action" 
                                                HeaderStyle-Width="100px" UniqueName="Action">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" onclick="btnEdit_Click" CssClass="PCGButton" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>--%>
                                            <telerik:GridBoundColumn DataField="Name" HeaderStyle-Width="400px" 
                                                HeaderText="Name" UniqueName="structSchemeName">
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="400px" 
                                                    Wrap="false" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridDateTimeColumn DataField="ValidFrom" 
                                                DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px"
                                                HeaderText="Valid From" SortExpression="ValidFrom" UniqueName="schemeValidFrom">
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" 
                                                    Wrap="false" />
                                            </telerik:GridDateTimeColumn>
                                            <telerik:GridDateTimeColumn DataField="ValidTill"
                                                DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px" 
                                                HeaderText="Valid Till" UniqueName="schemeValidTill">
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                                            </telerik:GridDateTimeColumn>
                                        </Columns>
                                        <PagerStyle AlwaysVisible="True" />
                                        <EditFormSettings>
                                            <EditColumn UniqueName="EditCommandColumn" UpdateText="Update"></EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>                                        
                                    <ClientSettings>
                                        <ClientEvents OnGridCreated="GridCreated" />
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                        <Resizing AllowColumnResize="true" />                                          
                                    </ClientSettings>
                                    <FilterMenu EnableEmbeddedSkins="False"></FilterMenu>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </ContentTemplate>
    <Triggers>
        <%--<asp:PostBackTrigger ControlID="ibtExportSummary" />--%>
    </Triggers>
</asp:UpdatePanel>