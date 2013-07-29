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
    .divViewEdit
    {
        padding-right: 15px;
        width: 2%;
        float: right;
        text-align: right;
        cursor: hand;
    }
    .divLeftSchemeList 
    {
        display:block;
        width:85px;
        float:left;
    }
    .divRightSchemeList 
    {
        display:block;
        width:116px;
        float:right;
    }
    .divMidButton 
    {
        display:block;
        width:20%;
        margin-left:auto;
        margin-right:auto;
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
                                <asp:HiddenField ID="hdnStructId" runat="server" />
                                <asp:HiddenField ID="hdnProductId" runat="server" />
                                <asp:HiddenField ID="hdnStructValidFrom" runat="server" />
                                <asp:HiddenField ID="hdnStructValidTill" runat="server" />
                                <asp:HiddenField ID="hdnIssuerId" runat="server" />
                                <asp:HiddenField ID="hdnCategoryId" runat="server" />
                                <asp:HiddenField ID="hdnSubcategoryIds" runat="server" />
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
    <%--<table width="100%">
        <tr id="trStepOneHeading" runat="server">
            <td class="tdSectionHeading">
                <div class="divSectionHeading" style="vertical-align: text-bottom">                        
                    <div class="fltlft">                           
                        <asp:Label ID="lblStructDetails" runat="server" Text="Structure Details"></asp:Label>
                    </div>                       
                </div>
            </td>
        </tr>
    </table>--%>
    <table width="100%">
       <tr>
            <td class="leftLabel">
                <asp:Label ID="lblStructName" runat="server" Text="Structure:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightData">
                <asp:TextBox ID="txtStructureName" runat="server" CssClass="txtField" 
                    AutoPostBack="True" />                
            </td>
            <td class="leftLabel">
                <asp:Label ID="lblProductName" runat="server" Text="Product: " CssClass="FieldName"></asp:Label></td>
            <td class="rightData">
                <asp:TextBox ID="txtProductName" runat="server" CssClass="txtField" AutoPostBack="True" /></td>
            <td class="leftLabel">
                <asp:Label ID="lblSubcats" runat="server" Text="Sub-Category" CssClass="FieldName"></asp:Label></td>     
            <td rowspan="2">
                <telerik:RadListBox ID="rlbAssetSubCategory" runat="server" CssClass="cmbField" 
                    Width="200px" Height="25px" AutoPostBack="True">
                    <ButtonSettings TransferButtons="All"></ButtonSettings>
                </telerik:RadListBox></td>
       </tr>
       <tr>
            <td class="leftLabel">
                <asp:Label ID="lblCategory" runat="server" Text="Category: " CssClass="FieldName"></asp:Label></td>
            <td class="rightData">
                <asp:TextBox ID="txtCategory" runat="server" CssClass="txtField" /></td>
            <td class="leftLabel">
                <asp:Label ID="lblIssuerName" runat="server" Text="Issuer: " CssClass="FieldName"></asp:Label></td>
            <td class="rightData">
                <asp:TextBox ID="txtIssuerName" runat="server" CssClass="txtField" /></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>     
       </tr>
       <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>            
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>     
       </tr>
    </table>
         
   <%-- <table width="100%">
        <tr id="tr1" runat="server">
            <td class="tdSectionHeading">
                <div class="divSectionHeading" style="vertical-align: text-bottom">                        
                    <div class="fltlft">                           
                        <asp:Label ID="lblSchemes" runat="server" Text="Associated Schemes"></asp:Label>
                    </div>                       
                </div>
            </td>
        </tr>        
    </table>--%>
    <table id="tblCommissionStructureRule" runat="server" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pnlGrid" runat="server" CssClass="Landscape" Width="100%" ScrollBars="Horizontal">
                    <table width="70%">
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
                                            <telerik:GridBoundColumn DataField="Name" HeaderStyle-Width="400px" 
                                                HeaderText="Name" UniqueName="structSchemeName" ReadOnly="true">
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="400px" 
                                                    Wrap="false" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridDateTimeColumn DataField="ValidFrom" ReadOnly="true"
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
    <table width="100%">
        <tr id="tr2" runat="server">
            <td class="tdSectionHeading" colspan="6">
                <div class="divSectionHeading" style="vertical-align: text-bottom">                        
                    <div class="fltlft">                           
                        <asp:Label ID="lblAddNewSchemes" runat="server" Text="Add New Schemes"></asp:Label>
                    </div>
                    <div class="divViewEdit">
                        <asp:LinkButton ID="lnkAddNewSchemes" Text="Add" runat="server" CssClass="LinkButtons"
                            OnClick="lnkAddNewSchemes_Click">
                        </asp:LinkButton>
                    </div>                     
                </div>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td class="leftLabel">
                <asp:Label ID="lblPeriodStart" runat="server" CssClass="FieldName" Text="Available Between"></asp:Label>
            </td>
            <td class="rightData">
                <telerik:RadDatePicker ID="rclPeriodStart" runat="server"></telerik:RadDatePicker></td>
            <td class="leftLabel">
                <telerik:RadDatePicker ID="rclPeriodEnd" runat="server"></telerik:RadDatePicker></td>
            <td class="rightData"><asp:Button ID="btn_GetAvailableSchemes" runat="server" Text="Schemes" 
                CssClass="PCGButton" onclick="btn_GetAvailableSchemes_Click" /></td>
            <td class="rightData"></td>                
            <td class="rightData"></td>
        </tr>
        <tr>
            <td class="leftLabel">
                <%--<asp:Label ID="Label3" CssClass="FieldName" runat="server" Text="Fetch Schemes"></asp:Label>--%></td></td>
            <td class="rightData">
                <%--<asp:Button ID="btn_GetAvailableSchemes" runat="server" Text="Go" CssClass="PCGButton" 
                    onclick="btn_GetAvailableSchemes_Click" />--%></td>
            <td class="leftLabel"></td>
            <td class="rightData"></td>
            <td class="leftLabel"></td>
            <td class="rightData"></td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td class="leftLabel"></td>
            <td class="rightData">
                <asp:Label ID="lblAvailableSchemes" runat="server" Text="Available Schemes" CssClass="FieldName"></asp:Label></td>
            <td class="rightData">
                <asp:Label ID="lblMappedSchemes" runat="server" Text="Mapped Schemes" CssClass="FieldName"></asp:Label></td>
            <td class="rightData"></td>
            <td class="leftLabel"></td>
            <td class="rightData"></td>
        </tr>
        <tr>
            <td class="leftLabel">
                </td>
            <td rowspan="2" class="rightData">
                <telerik:RadListBox SelectionMode="Multiple" EnableDragAndDrop="true"
                    AllowTransferOnDoubleClick="true" AllowTransferDuplicates="false"
                    EnableViewState="true" EnableMarkMatches="true" runat="server" ID="rlbAvailSchemes"
                    Height="200px" Width="250px" AllowTransfer="true"
                    TransferToID="rlbMappedSchemes" CssClass="cmbField" 
                    ontransferred="rlbAvailSchemes_Transferred">
                    <ButtonSettings TransferButtons="All" />
                </telerik:RadListBox></td>            
            <td rowspan="2" class="leftLabel">
                <telerik:RadListBox runat="server" AutoPostBackOnTransfer="true" SelectionMode="Multiple"
                    ID="rlbMappedSchemes" Height="200px" Width="220px" CssClass="cmbField" 
                    ontransferred="rlbMappedSchemes_Transferred">
                </telerik:RadListBox>
            </td>
            <td class="rightData"></td>
            <td class="leftLabel"></td>
            <td class="rightData"></td>
        </tr>

        <tr>
            <td class="leftLabel"></td>
            <td class="rightData"></td>
            <td class="leftLabel"></td>
            <td class="rightData"></td>
            <%--<td class="leftLabel"></td>
            <td class="rightData"></td>--%>
        </tr>
        <tr>
            <td class="leftLabel">
                <asp:Label ID="lblMappedFrom" runat="server" CssClass="FieldName" Text="Mapped From"></asp:Label>
            </td>
            <td class="rightData">
                <telerik:RadDatePicker ID="rdpMappedFrom" runat="server"></telerik:RadDatePicker>
            </td>
            <td class="rightData">
                <telerik:RadDatePicker ID="rdpMappedTill" runat="server"></telerik:RadDatePicker>
                <%--<asp:Label ID="lblMappedTill" runat="server" CssClass="FieldName" Text="Mapped Till"></asp:Label>--%></td>
            <td class="rightData">
                <asp:Button ID="btnMapSchemes" CssClass="PCGButton" runat="server" Text="Map" 
                    onclick="btnMapSchemes_Click" />
                <%--<telerik:RadDatePicker ID="rdpMappedTill" runat="server"></telerik:RadDatePicker>--%></td>
            <td class="leftLabel"></td>                
            <td class="rightData"></td>
        </tr>
        <%--<tr>
            <td class="leftLabel">
                <asp:Label ID="lblMapSchemes" CssClass="FieldName" runat="server" Text="Map Schemes"></asp:Label></td>
            <td class="rightData">
                <%--<asp:Button ID="btnMapSchemes" CssClass="PCGButton" runat="server" Text="Go" 
                    onclick="btnMapSchemes_Click" /></td>
            <td class="leftLabel"></td>
            <td class="rightData"></td>
            <td class="leftLabel"></td>
            <td class="rightData"></td>
        </tr>--%>
    </table>
</ContentTemplate>
    <%--<Triggers-->
        <asp:PostBackTrigger ControlID="ibtExportSummary" />
    </Triggers>--%>
</asp:UpdatePanel>