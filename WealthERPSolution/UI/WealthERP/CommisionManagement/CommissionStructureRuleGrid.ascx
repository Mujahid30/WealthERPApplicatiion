<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommissionStructureRuleGrid.ascx.cs"
    Inherits="WealthERP.CommisionManagement.CommissionStructureRuleGrid" %>
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
<asp:UpdatePanel ID="upCMGrid" runat="server" ChildrenAsTriggers="true">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td>
                    <div class="divPageHeading">
                        <table width="100%">
                            <tr>
                                <td align="left">
                                    View Brokerage Structure
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="ibtExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="ibtExportSummary_OnClick"
                                        Height="25px" Width="25px"></asp:ImageButton>
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
                    <asp:Label ID="lblProduct" runat="server" Text="Product:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddProduct" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddProduct_SelectedIndexChanged">
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvProduct" runat="server" CssClass="rfvPCG" ErrorMessage="Please select a product"
                        Display="Dynamic" ControlToValidate="ddProduct" InitialValue="Select" ValidationGroup="btnGo">
                    </asp:RequiredFieldValidator>
                </td>
              
                <td colspan="2">
                </td>
            </tr>
            <tr id="trCategory" runat="server">
             
                <td class="leftLabel">
                    <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddCategory_OnSelectedIndexChanged">
                    </asp:DropDownList>
                    <span id="Span2" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ErrorMessage="Please Select Category"
                        CssClass="rfvPCG" ControlToValidate="ddCategory" ValidationGroup="btnGo" Display="Dynamic"
                        Visible="true" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
                 <td class="leftLabel">
                    <asp:Label ID="lblIssuer" runat="server" Text="Issuer :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddIssuer" runat="server" CssClass="cmbField" AutoPostBack="false"
                        Width="300px">
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvIssuer" runat="server" CssClass="rfvPCG" ErrorMessage="Please select an issuer"
                        Display="Dynamic" ControlToValidate="ddIssuer" InitialValue="Select" ValidationGroup="btnGo"></asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel" id="tdlblSubCategory" runat="server" visible="false">
                    <asp:Label ID="lblSubCategory" runat="server" Text="Sub Category" CssClass="FieldName"
                        Visible="true"></asp:Label>
                </td>
                <td class="rightData" id="tdddSubCategory" runat="server" visible="false">
                    <asp:DropDownList ID="ddSubCategory" runat="server" CssClass="cmbField" AutoPostBack="false"
                        Enabled="False" Visible="true">
                        <asp:ListItem>All</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="leftLabel" id="tdlblStatus" runat="server" visible="false">
                    <asp:Label ID="lblStatus" runat="server" Text="Status" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData" id="tdddStatus" runat="server" visible="false">
                    <asp:DropDownList ID="ddStatus" runat="server" CssClass="cmbField" AutoPostBack="false">
                        <asp:ListItem Value="All">All</asp:ListItem>
                        <asp:ListItem Value="Active" Selected="True">Active</asp:ListItem>
                        <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="5">
                    <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" CssClass="PCGButton"
                        ValidationGroup="btnGo" />
                </td>
            </tr>
        </table>
        
        <asp:Panel ID="pnlGrid" runat="server" CssClass="Landscape" Width="100%" ScrollBars="Both">
            <table width="100%" runat="server">
                <tr>
                    <td>
                        <telerik:RadGrid ID="gvCommMgmt" runat="server" AllowSorting="True" enableloadondemand="True"
                            PageSize="10" AllowPaging="True" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                            GridLines="None" ShowFooter="True" PagerStyle-AlwaysVisible="true" ShowStatusBar="True"
                            Skin="Telerik" AllowFilteringByColumn="true" OnPageIndexChanged="gvCommMgmt_PageIndexChanged"
                            OnNeedDataSource="gvCommMgmt_OnNeedDataSource" OnItemDataBound="gvCommMgmt_ItemDataBound">
                            <HeaderContextMenu EnableEmbeddedSkins="False">
                            </HeaderContextMenu>
                            <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="CommissionStructureRule">
                            </ExportSettings>
                            <PagerStyle AlwaysVisible="True" />
                            <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="StructureId,Name"
                                AutoGenerateColumns="false" AllowFilteringByColumn="true" Width="100%">
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="false" DataField="Action" HeaderStyle-Width="100px"
                                        UniqueName="Action">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddAction" runat="server" OnSelectedIndexChanged="ddAction_OnSelectedIndexChanged"
                                                AutoPostBack="true" CssClass="cmbField" EnableEmbeddedSkins="false" Width="80px">
                                                <Items>
                                                    <asp:ListItem Selected="true" Text="Select" Value="Select" />
                                                    <asp:ListItem Text="View Details" Value="ViewSTDetails" />
                                                    <asp:ListItem Text="Copy & Create Structure" Value="CopyStructure" />
                                                    <asp:ListItem Text="View Mapped Schemes" Value="ManageSchemeMapping" Enabled="false" />
                                                </Items>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="Name" HeaderStyle-Width="160px" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Name" UniqueName="cmRuleName"
                                        SortExpression="Name">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="160px" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Product" HeaderStyle-Width="100px" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Product" UniqueName="cmProdType"
                                        SortExpression="Product">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Issuer" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Issuer" UniqueName="cmIssuer"
                                        SortExpression="Issuer" Visible="false">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="200px" Wrap="true" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Category" HeaderStyle-Width="100px" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Category" UniqueName="cmCategory"
                                        SortExpression="Category" Visible="false">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="SubCategory" HeaderStyle-Width="150px" HeaderText="SubCategory"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                        UniqueName="cmSubCategory">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="150px" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDateTimeColumn DataField="ValidFrom" DataFormatString="{0:dd/MM/yyyy}"
                                        HeaderStyle-Width="100px" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                        AutoPostBackOnFilter="true" HeaderText="Valid From" SortExpression="ValidFrom"
                                        UniqueName="cmValidFrom">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridDateTimeColumn DataField="ValidTill" DataFormatString="{0:dd/MM/yyyy}"
                                        HeaderStyle-Width="100px" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                        AutoPostBackOnFilter="true" HeaderText="Valid Till" UniqueName="cmValidTill"
                                        SortExpression="ValidTill">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                                    </telerik:GridDateTimeColumn>
                                    <telerik:GridBoundColumn DataField="ACSM_Note" HeaderStyle-Width="140px" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Note" UniqueName="ACSM_Note"
                                        SortExpression="ACSM_Note">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="140px" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CreatedOn" HeaderStyle-Width="150px" HeaderText="Created On"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                        UniqueName="cmCreatedOn" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="150px" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CreatedBy" HeaderStyle-Width="150px" HeaderText="Created By"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                        UniqueName="cmCreatedBy" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="150px" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ModifiedOn" HeaderStyle-Width="150px" HeaderText="Modified On"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                        UniqueName="cmModifiedOn" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="150px" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ModifiedBy" HeaderStyle-Width="150px" HeaderText="ModifiedBy"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                        UniqueName="cmModifiedBy" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="150px" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <PagerStyle AlwaysVisible="True" />
                            </MasterTableView>
                            <ClientSettings>
                                <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                <Resizing AllowColumnResize="true" />
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <FilterMenu EnableEmbeddedSkins="False">
                            </FilterMenu>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </asp:Panel>
       
        <telerik:RadWindow ID="radCopyStructurePopUp" runat="server" VisibleOnPageLoad="false"
                Height="150px" Width="500px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false"
                Behaviors="Resize, Close, Move" Title="Copy & Create Structure" Left="10" Top="20" OnClientShow="setCustomPosition">
                <ContentTemplate>
                <table>
                <tr>
                <td colspan="2">
                  <asp:Label ID="lblStructureName" CssClass="FieldName" runat="server"></asp:Label>
                </td>
                </tr>
                <tr>
                <td>
                  <asp:Label ID="lblValidityStart" CssClass="FieldName" runat="server" Text="Valid From:"></asp:Label>
                </td>
                <td class="rightData">
                 <asp:TextBox ID="txtValidityFrom" runat="server" CssClass="txtField"></asp:TextBox>
                        <span id="Span1" class="spnRequiredField">*</span>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtValidityFrom"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtValidityFrom"
                            WatermarkText="dd/mm/yyyy">
                        </cc1:TextBoxWatermarkExtender>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br/>Please enter a valid date."
                            Type="Date" ControlToValidate="txtValidityFrom" CssClass="cvPCG" Operator="DataTypeCheck"
                            ValidationGroup="btnCopyStructure" ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtValidityFrom"
                            ErrorMessage="<br />Please enter a validity from Date" Display="Dynamic" CssClass="rfvPCG"
                            runat="server" InitialValue="" ValidationGroup="btnCopyStructure">
                        </asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel">
                  <asp:Label ID="lblValidityTo" CssClass="FieldName" runat="server" Text="Valid To:"></asp:Label>
                </td>
                <td class="rightData">
                 <asp:TextBox ID="txtValidityTo" runat="server" CssClass="txtField"></asp:TextBox>
                        <span id="Span3" class="spnRequiredField">*</span>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtValidityTo"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtValidityTo"
                            WatermarkText="dd/mm/yyyy">
                        </cc1:TextBoxWatermarkExtender>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="<br/>Please enter a valid date."
                            Type="Date" ControlToValidate="txtValidityTo" CssClass="cvPCG" Operator="DataTypeCheck"
                            ValidationGroup="btnCopyStructure" ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtValidityTo"
                            ErrorMessage="<br />Please enter a validity from Date" Display="Dynamic" CssClass="rfvPCG"
                            runat="server" InitialValue="" ValidationGroup="btnCopyStructure">
                        </asp:RequiredFieldValidator>
                </td>
                </tr>
                <tr>
                <td >
                <asp:Button ID="btnCopyStructure" runat="server" Text="Copy and Create" CssClass="PCGMediumButton" ValidationGroup="btnCopyStructure" OnClick="btnCopyStructure_OnClick"/>
                </td>
                </tr>
                </table>
                 </ContentTemplate>
            </telerik:RadWindow>
            <asp:HiddenField ID="hdnStructure" runat="server" />
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="ibtExportSummary" />
         
    </Triggers>
</asp:UpdatePanel>
  <script type="text/javascript">
    function setCustomPosition(sender, args) {
        sender.moveTo(sender.get_left(), sender.get_top());
    }
</script>
