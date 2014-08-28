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
    .wide-button 
    {
        width:auto;
        background-repeat:repeat-x;
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
                                Schemes Mapped To The Structures
                                <asp:HiddenField ID="hdnStructId" runat="server" />
                                <asp:HiddenField ID="hdnProductId" runat="server" />
                                <asp:HiddenField ID="hdnStructValidFrom" runat="server" />
                                <asp:HiddenField ID="hdnStructValidTill" runat="server" />
                                <asp:HiddenField ID="hdnIssuerId" runat="server" />
                                <asp:HiddenField ID="hdnCategoryId" runat="server" />
                                <asp:HiddenField ID="hdnSubcategoryIds" runat="server" />
                            </td>
                            <td align=right>
                                <asp:ImageButton ID="ibtExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="ibtExportSummary_OnClick"
                                    Height="25px" Width="25px" />
                            </td>
                        </tr>
                    </table>
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
                <asp:LinkButton ID="lbtStructureName" runat="server" CssClass="txtField" AutoPostBack="True" OnClick="lbtStructureName_Click" Visible="false">StructureName</asp:LinkButton>
                <asp:DropDownList ID="ddlStructs" runat="server" Visible="false" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlStructs_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td class="leftLabel">
                <asp:Label ID="lblProductName" runat="server" Text="Product: " CssClass="FieldName"></asp:Label></td>
            <td class="rightData">
                <asp:TextBox ID="txtProductName" runat="server" CssClass="txtField" Enabled="false" AutoPostBack="true" /></td>
            <td class="leftLabel">
                <asp:Label ID="lblSubcats" runat="server" Text="Sub-Category: " CssClass="FieldName"></asp:Label></td>     
            <td rowspan="4">
                <telerik:RadListBox ID="rlbAssetSubCategory" runat="server" CssClass="cmbField" 
                    Width="200px" Height="90px" AutoPostBack="true">
                    <ButtonSettings TransferButtons="All"></ButtonSettings>
                </telerik:RadListBox></td>
       </tr>
       <tr>
            <td class="leftLabel">
                <asp:Label ID="lblCategory" runat="server" Text="Category: " CssClass="FieldName"></asp:Label></td>
            <td class="rightData">
                <asp:TextBox ID="txtCategory" runat="server" CssClass="txtField" Enabled="false" AutoPostBack="true" /></td>
            <td class="leftLabel">
                <asp:Label ID="lblIssuerName" runat="server" Text="Issuer: " CssClass="FieldName"></asp:Label></td>
            <td class="rightData">
                <asp:TextBox ID="txtIssuerName" runat="server" CssClass="txtField" Enabled="false"  AutoPostBack="true"/></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>     
       </tr>
       <tr>
            <td class="leftLabel">
                <asp:Label ID="lblStructValidFrom" runat="server" Text="Validity From: " CssClass="FieldName"></asp:Label></td>
            <td class="rightData">
                <asp:TextBox ID="txtValidFrom" runat="server" Enabled="false" CssClass="txtField" AutoPostBack="true"></asp:TextBox></td>
            <td class="leftLabel">
                <asp:Label ID="lblStrucValidTo" runat="server" Text="Valid To: " CssClass="FieldName"></asp:Label></td>           
            <td class="rightData">
                <asp:TextBox ID="txtValidTo" runat="server" Enabled="false" CssClass="txtField" AutoPostBack="true"></asp:TextBox></td>
            <td>&nbsp;</td>
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

    <table id="tblCommissionStructureRule" runat="server" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pnlGrid" runat="server" CssClass="Landscape" Width="100%" ScrollBars="None" Visible="false">
                    <table width="75%">
                        <tr> 
                            <td>
                                <telerik:RadGrid ID="gvMappedSchemes" AllowSorting="false" runat="server" AllowAutomaticInserts="false" 
                                    AllowPaging="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" enableloadondemand="true"
                                    EnableEmbeddedSkins="false" GridLines="none" ShowFooter="true" PagerStyle-AlwaysVisible="true"
                                    EnableViewState="true" ShowStatusBar="true" Skin="Telerik" OnPageSizeChanged="gvMappedSchemes_PageSizeChanged"
                                    OnNeedDataSource="gvMappedSchemes_NeedDataSource" OnItemCreated="gvMappedSchemes_OnItemCreated"
                                    OnPageIndexChanged="gvMappedSchemes_PageIndexChanged" OnUpdateCommand="gvMappedSchemes_UpdateCommand"
                                    OnDeleteCommand="gvMappedSchemes_DeleteCommand">
                                    <HeaderContextMenu EnableEmbeddedSkins="False"></HeaderContextMenu>

                                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="MappedSchemes"
                                    IgnorePaging="true" ></ExportSettings>
                                    <PagerStyle AlwaysVisible="True" />
                                    <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" 
                                        AutoGenerateColumns="false" Width="100%" DataKeyNames="ACSTSM_SetupId">
                                        <CommandItemSettings ExportToExcelText="Export to excel" />
                                        <Columns>
                                            <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" HeaderStyle-Width="50px">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px" Wrap="false" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridBoundColumn DataField="Name" HeaderStyle-Width="400px"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true" 
                                                HeaderText="Scheme Name" UniqueName="structSchemeName" ReadOnly="true">
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="400px" Wrap="false" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridDateTimeColumn DataField="ValidFrom" ReadOnly="true"
                                                DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px"
                                                CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                HeaderText="Valid From" SortExpression="ValidFrom" UniqueName="schemeValidFrom">
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                                            </telerik:GridDateTimeColumn>
                                            <telerik:GridDateTimeColumn DataField="ValidTill"
                                                DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px"
                                                CurrentFilterFunction="EqualTo" ShowFilterIcon="false" AutoPostBackOnFilter="true" 
                                                HeaderText="Valid Till" UniqueName="schemeValidTill">
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                                            </telerik:GridDateTimeColumn>
                                             <telerik:GridButtonColumn ButtonType="LinkButton" Text="Delete" 
                                             ConfirmText="Do you want to delete the mapping?" CommandName="Delete"
                                             UniqueName="DeleteCommandColumn" HeaderStyle-Width="50px">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px" Wrap="false" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <PagerStyle AlwaysVisible="True" />
                                    </MasterTableView>                                        
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                        <Resizing AllowColumnResize="true" />                                          
                                    </ClientSettings>
                                    <%--<FilterMenu EnableEmbeddedSkins="False"></FilterMenu>--%>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlAddSchemesButton" runat="server" Visible="false">
        <table width="33%">
            <tr>
                <td class="leftLabel"><asp:Label ID="lblAddNewSchemes" runat="server" CssClass="FieldName" Text="Add Schemes"></asp:Label></td>
                <td class="rightData">
                    <asp:Button ID="btnAddNewSchemes" runat="server" Text="New Schemes" OnClick="btnAddNewSchemes_Click" CssClass="PCGButton wide-button" /></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlAddSchemes" runat="server" Visible="false">
        <table width="100%">
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblPeriodStart" runat="server" CssClass="FieldName" Text="Available Between: "></asp:Label></td>
                <td class="rightData">
                    <telerik:RadDatePicker ID="rdpPeriodStart" runat="server"></telerik:RadDatePicker></td>
                <td class="leftLabel">
                    <telerik:RadDatePicker ID="rdpPeriodEnd" runat="server"></telerik:RadDatePicker></td>
                <td class="rightData"><asp:Button ID="btn_GetAvailableSchemes" runat="server" Text="Schemes" 
                    CssClass="PCGButton" onclick="btn_GetAvailableSchemes_Click" /></td>
                <td colspan="2">
                    <asp:RequiredFieldValidator ID="rfvPeriodStart" runat="server" CssClass="rfvPCG" ErrorMessage="Please enter valid date(s)" ControlToValidate="rdpPeriodStart" ValidationGroup="availSchemesPeriod" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="rfvPeriodEnd" runat="server" CssClass="rfvPCG" ErrorMessage="Please enter valid date(s)" ControlToValidate="rdpPeriodEnd" ValidationGroup="availSchemesPeriod" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvPeriodEnd" runat="server" CssClass="rfvPCG"
                        ControlToCompare="rdpPeriodStart" ErrorMessage="Please enter valid date(s)" 
                        ControlToValidate="rdpPeriodEnd" Display="Dynamic" Operator="GreaterThan" 
                        SetFocusOnError="True" Type="Date" ValidationGroup="availSchemesPeriod"></asp:CompareValidator>
                    <asp:Label ID="lblMapError" runat="server" CssClass="rfvPCG" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td>&nbsp;</td>
                <td class="rightData">
                    <asp:Label ID="lblAvailableSchemes" runat="server" Text="Available Schemes" CssClass="FieldName"></asp:Label></td>
                <td class="rightData">
                    <asp:Label ID="lblMappedSchemes" runat="server" Text="Mapped Schemes" CssClass="FieldName"></asp:Label></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td rowspan="2" class="rightData">
                    <telerik:RadListBox SelectionMode="Multiple" EnableDragAndDrop="true"
                        AllowTransferOnDoubleClick="true" AllowTransferDuplicates="false"
                        EnableViewState="true" EnableMarkMatches="true" runat="server" ID="rlbAvailSchemes"
                        Height="200px" Width="250px" AllowTransfer="true"
                        TransferToID="rlbMappedSchemes" CssClass="cmbField">
                        <ButtonSettings TransferButtons="All" />
                    </telerik:RadListBox></td>            
                <td rowspan="2" class="leftLabel">
                    <telerik:RadListBox runat="server" AutoPostBackOnTransfer="true" SelectionMode="Multiple"
                        ID="rlbMappedSchemes" Height="200px" Width="220px" CssClass="cmbField">
                    </telerik:RadListBox>
                </td>
                <td>&nbsp;</td>
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
        <table>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lblMappedFrom" runat="server" CssClass="FieldName" Text="Mapping Period: "></asp:Label></td>
                <td class="rightData">
                    <telerik:RadDatePicker ID="rdpMappedFrom" runat="server"></telerik:RadDatePicker>
                <td class="leftLabel">
                    <telerik:RadDatePicker ID="rdpMappedTill" runat="server"></telerik:RadDatePicker>
                <td class="rightData">
                    <asp:Button ID="btnMapSchemes" CssClass="PCGButton" runat="server" Text="Map" onclick="btnMapSchemes_Click" /></td>
                <td colspan="2">
                    <asp:RequiredFieldValidator ID="rfvMappingTo" runat="server" ErrorMessage="Please enter valid date(s)" Display="Dynamic" CssClass="rfvPCG" ValidationGroup="mappingPeriod" ControlToValidate="rdpMappedTill"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="rfvMappingFrom" runat="server" ErrorMessage="Please enter valid date(s)" Display="Dynamic" CssClass="rfvPCG" ValidationGroup="mappingPeriod" ControlToValidate="rdpMappedFrom"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cmvMappingPeriod" runat="server" ErrorMessage="Please enter valid date(s)" CssClass="rfvPCG" Display="Dynamic" ControlToCompare="rdpMappedFrom" ControlToValidate="rdpMappedTill" Operator="GreaterThan"></asp:CompareValidator></td>
            </tr>
        </table>
    </asp:Panel>    
</ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="ibtExportSummary" />
    </Triggers>
</asp:UpdatePanel>
