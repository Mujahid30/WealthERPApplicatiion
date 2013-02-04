<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="true" CodeBehind="AdvisorMISCommission.ascx.cs"
    Inherits="WealthERP.Advisor.AdvisorMISCommission" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager runat="server">
</telerik:RadStyleSheetManager>
<telerik:RadScriptManager runat="server">
</telerik:RadScriptManager>

<script type="text/javascript" language="javascript">

    function keyPress(sender, args) {
        if (args.keyCode == 13) {
            return false;
        }
    }
</script>

<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            MF MIS Commission
                        </td>
                        <td align="right" id="trCommissionMIS" runat="server">
                            <asp:ImageButton ID="btnCommissionMIS" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnCommissionMIS_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                         <td align="right" id="tdZoneClusterCommissionMIS" runat="server">
                            <asp:ImageButton ID="imgZoneClusterCommissionMIS" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnZoneCLusterMISCommission_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                         <td align="right" id="tdCategoryWise" runat="server">
                            <asp:ImageButton ID="imgMISCommission" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnCategoryWise_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <%--<tr>
        <td colspan="6">
            <asp:Label ID="Label7" runat="server" CssClass="HeaderTextSmall" Text="MF MIS Commission"></asp:Label>
            <hr />
        </td>
    </tr>--%>
    <tr>
        <td colspan="6">
            <table>
                <tr id="tr1" runat="server">
                    <td id="Td3" runat="server">
                        <asp:Label ID="Label1" runat="server" Text="MIS Type:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td runat="server">
                        <telerik:RadComboBox ID="ddlMISType" runat="server" CssClass="cmbField" EnableEmbeddedSkins="false"
                            Skin="Telerik" AllowCustomText="true" Width="120px">
                            <Items>
                            <telerik:RadComboBoxItem Text="Organization Level" Value="Zone_Cluster_Wise" />
                                <telerik:RadComboBoxItem Text="AMC/Folio/Type Wise" Value="AMC_Folio_Type_AllMIS" />                               
                                <telerik:RadComboBoxItem Text="Category Wise" Value="Category Wise" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td class="style1">
                        <asp:RadioButton ID="rbtnPickDate" Class="cmbField" Checked="True" runat="server"
                            AutoPostBack="true" Text="Pick a Date" GroupName="Date" OnCheckedChanged="RadioButtonClick" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rbtnPickPeriod" Class="cmbField" runat="server" Text="Pick a Period"
                            AutoPostBack="true" GroupName="Date" OnCheckedChanged="RadioButtonClick" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <table>
                <tr onkeypress="return keyPress(this, event)">
                    <td>
                        <asp:Label ID="lblFromDate" Text="From:" runat="server" CssClass="FieldName">
                        </asp:Label>
                        <telerik:RadDatePicker ID="txtFromDate" CssClass="txtField" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                    </td>
                    <td>
                        <asp:Label ID="lblToDate" runat="server" CssClass="FieldName" Text="To:"></asp:Label>
                        <telerik:RadDatePicker ID="txtToDate" CssClass="txtField" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                    </td>
                    <td>
                        <asp:CompareValidator ID="CompareValidator1" ControlToCompare="txtFromDate" ControlToValidate="txtToDate"
                            ErrorMessage="To date should be greater than from date" Display="Dynamic" runat="server"
                            CssClass="rfvPCG" Operator="GreaterThanEqual" ValidationGroup="btnView">
                        </asp:CompareValidator>
                    </td>
                </tr>
                <tr id="PickADateValidation" runat="server">
                    <td>
                        <asp:RequiredFieldValidator CssClass="rfvPCG" ValidationGroup="btnView" Display="Dynamic"
                            runat="server" ID="txtFromDate_RequiredFieldValidator" ControlToValidate="txtFromDate"
                            ErrorMessage="Please Select a from date">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator CssClass="rfvPCG" ValidationGroup="btnView" Display="Dynamic"
                            runat="server" ID="txtToDate_RequiredFieldValidator" ControlToValidate="txtToDate"
                            ErrorMessage="Please Select a to date">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:Label ID="lblPeriod" runat="server" CssClass="FieldName">Period: </asp:Label>
                    </td>
                    <td valign="top">
                        <telerik:RadComboBox ID="ddlPeriod" runat="server" CssClass="cmbField" EnableEmbeddedSkins="false"
                            Skin="Telerik" AllowCustomText="true" Width="120px">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr id="PickAPeriodValidation" runat="server">
                    <td>
                        <asp:RequiredFieldValidator ID="ddlPeriod_RequiredFieldValidator" runat="server"
                            CssClass="rfvPCG" ErrorMessage="Please select a Period" Text="Please select a Period"
                            Display="Dynamic" ValidationGroup="btnView" ControlToValidate="ddlPeriod" InitialValue="0">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnView" runat="server" CssClass="PCGButton" Text="Go" OnClick="btnView_Click"
                ValidationGroup="btnView" />
        </td>
    </tr>
    <tr>
        <td>
            <%-- <asp:ImageButton ID="btnCommissionMIS" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnCommissionMIS_OnClick"
                OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>--%>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadGrid ID="gvCommissionMIS" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" OnNeedDataSource="gvCommissionMIS_OnNeedDataSource" EnableEmbeddedSkins="false"
                Width="80%" AllowFilteringByColumn="true" AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true">
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                    FileName="MF Commission MIS Category Wise" Excel-Format="ExcelML">
                </ExportSettings>
                <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="none">
                    <%--<CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                        ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="true" />--%>
                    <Columns>
                        <telerik:GridBoundColumn UniqueName="CustomerName" DataField="CustomerName" AllowFiltering="true"
                            HeaderText="Customer Name" SortExpression="CustomerName" AutoPostBackOnFilter="true"
                            ShowFilterIcon="false" FooterText="Grand Total:" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="MISType" AllowFiltering="true" AutoPostBackOnFilter="true"
                            ShowFilterIcon="false" HeaderText="" FooterStyle-HorizontalAlign="Right" AllowSorting="true">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="RM_Name" DataField="RM_Name" AllowFiltering="true"
                            HeaderText="RM Name" SortExpression="RM_Name" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="AB_BranchName" DataField="AB_BranchName" AllowFiltering="true"
                            HeaderText="Branch Name" SortExpression="AB_BranchName" AutoPostBackOnFilter="true"
                            ShowFilterIcon="false">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="BrokerageAmt" DataField="Brokerage" AllowFiltering="false"
                            HeaderText="Brokerage Amount" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Width="20%" HorizontalAlign="Right" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="TrailCommission" DataField="trailFee" AllowFiltering="false"
                            HeaderText="Trail Commission" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Width="20%" HorizontalAlign="Right" Wrap="false" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </td>
    </tr>
</table>
<table runat="server" id="tblCommissionMIS" width="95%">
    <tr>
        <td>
            <asp:Panel ID="pnlCommissionMIS" ScrollBars="Horizontal" Width="100%" runat="server">
                <div runat="server" id="divCommissionMIS" style="margin: 2px; width: 640px;">
                    <telerik:RadGrid ID="gvMISCommission" runat="server" GridLines="None" AutoGenerateColumns="False"
                        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                        Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true" OnNeedDataSource="gvMISCommission_OnNeedDataSource"
                        EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" AllowAutomaticInserts="false"
                        ExportSettings-ExportOnlyData="true">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                            FileName="Commission MIS Details" Excel-Format="ExcelML">
                        </ExportSettings>
                        <MasterTableView GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                            GroupLoadMode="Client" EditMode="EditForms" ShowGroupFooter="true" Width="100%"
                            AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                            <GroupByExpressions>
                                <telerik:GridGroupByExpression>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="AMCName" />
                                    </GroupByFields>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldName="AMCName" FieldAlias="AMC" />
                                    </SelectFields>
                                </telerik:GridGroupByExpression>
                            </GroupByExpressions>
                            <GroupByExpressions>
                                <telerik:GridGroupByExpression>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="SchemeName" />
                                    </GroupByFields>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldName="SchemeName" FieldAlias="Scheme" />
                                    </SelectFields>
                                </telerik:GridGroupByExpression>
                            </GroupByExpressions>
                            <GroupByExpressions>
                                <telerik:GridGroupByExpression>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="TransactionType" />
                                    </GroupByFields>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldName="TransactionType" FieldAlias="Transaction" />
                                    </SelectFields>
                                </telerik:GridGroupByExpression>
                            </GroupByExpressions>
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="AMC" HeaderTooltip="AMC" DataField="AMCName"
                                    UniqueName="AMCName" SortExpression="AMCName" AllowFiltering="true" AutoPostBackOnFilter="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Scheme" HeaderTooltip="Scheme" DataField="SchemeName"
                                    UniqueName="SchemeName" SortExpression="SchemeName" AutoPostBackOnFilter="true"
                                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Transaction Classification" HeaderTooltip="Transaction Classification Name"
                                    DataField="TransactionType" UniqueName="TransactionType" SortExpression="TransactionType"
                                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                   <telerik:GridBoundColumn HeaderText="Group" HeaderTooltip="Group Name" DataField="Parent"
                                    UniqueName="Parent" SortExpression="Parent" AutoPostBackOnFilter="true"
                                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Customer" HeaderTooltip="Customer Name" DataField="CustomerName"
                                    UniqueName="CustomerName" SortExpression="CustomerName" AutoPostBackOnFilter="true"
                                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Folio" HeaderTooltip="Folio Number" DataField="FolioNum"
                                    UniqueName="FolioNum" SortExpression="FolioNum" AutoPostBackOnFilter="true" AllowFiltering="true"  Aggregate="Count" FooterText="Row Count : "
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="RM" HeaderTooltip="RM Name" DataField="RM_Name"
                                    UniqueName="RM_Name" SortExpression="RM_Name" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Brokerage Amount"
                                    HeaderText="Brokerage Amount" DataField="Brokerage" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="Brokerage" SortExpression="Brokerage" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Trail Commission"
                                    HeaderText="Trail Commission" DataField="trailFee" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="trailFee" SortExpression="trailFee" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <HeaderStyle Width="150px" />
                        <ClientSettings ReorderColumnsOnClient="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </asp:Panel>
        </td>
    </tr>
</table>

<table runat="server" id="tblZoneClusterWiseMIS" width="100%">
    <tr>
        <td>
            <asp:Panel ID="pnlZoneClusterWiseMIS" ScrollBars="Horizontal" Width="77%" runat="server">
                <div runat="server" id="divZoneClusterWiseMIS" style="margin: 2px; width: 440px;">
                    <telerik:RadGrid ID="gvZoneClusterWiseCommissionMIS" runat="server" GridLines="None" AutoGenerateColumns="False"
                        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                        Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true" 
                        OnNeedDataSource="gvZoneClusterWiseCommissionMIS_OnNeedDataSource"
                        EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" AllowAutomaticInserts="false" enab
                        ExportSettings-ExportOnlyData="true">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                            FileName="Zone/Cluster Commission MIS Details" Excel-Format="ExcelML">
                        </ExportSettings>
                        <MasterTableView GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                            GroupLoadMode="Client" EditMode="EditForms" ShowGroupFooter="true" Width="100%"
                            AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                            <GroupByExpressions>
                                <telerik:GridGroupByExpression>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="ZoneName" />
                                    </GroupByFields>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldName="ZoneName" FieldAlias="Zone" />
                                    </SelectFields>
                                </telerik:GridGroupByExpression>
                            </GroupByExpressions>
                            <GroupByExpressions>
                                <telerik:GridGroupByExpression>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="ClusterName" />
                                    </GroupByFields>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldName="ClusterName" FieldAlias="Cluster" />
                                    </SelectFields>
                                </telerik:GridGroupByExpression>
                            </GroupByExpressions>
                           <GroupByExpressions>
                                <telerik:GridGroupByExpression>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="BranchName" />
                                    </GroupByFields>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldName="BranchName" FieldAlias="Branch" />
                                    </SelectFields>
                                </telerik:GridGroupByExpression>
                            </GroupByExpressions>
                            <Columns>
                             <telerik:GridBoundColumn HeaderText="Zone" HeaderTooltip="Zone" DataField="ZoneName"
                                    UniqueName="ZoneName" SortExpression="ZoneName" AllowFiltering="true" AutoPostBackOnFilter="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Cluster" HeaderTooltip="Cluster" DataField="ClusterName"
                                    UniqueName="ClusterName" SortExpression="ClusterName" AutoPostBackOnFilter="true"
                                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                
                                  <telerik:GridBoundColumn HeaderText="Branch" HeaderTooltip="Branch Name" DataField="BranchName"
                                  Aggregate="Count" FooterText="Row Count : "
                                    UniqueName="BranchName" SortExpression="BranchName" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>                        
                                <telerik:GridBoundColumn HeaderTooltip="Brokerage Amount"
                                    HeaderText="Brokerage Amount" DataField="Brokerage" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="Brokerage" SortExpression="Brokerage" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn  HeaderTooltip="Trail Commission"
                                    HeaderText="Trail Commission" DataField="trailFee" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="trailFee" SortExpression="trailFee" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <HeaderStyle Width="150px" />
                        <ClientSettings ReorderColumnsOnClient="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </asp:Panel>
        </td>
    </tr>
</table>
<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
    visible="false">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
            </div>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hidDateType" Value="" runat="server" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnMISType" runat="server" />
<asp:HiddenField ID="hdnFromDate" runat="server" />
<asp:HiddenField ID="hdnToDate" runat="server" />

<script type="text/javascript">

    if (document.getElementById("<%= rbtnPickDate.ClientID %>").checked) {
        document.getElementById("tblRange").style.display = 'block';
        document.getElementById("tblPeriod").style.display = 'none';
    }
    else if (document.getElementById("<%= rbtnPickPeriod.ClientID %>").checked) {
        document.getElementById("tblRange").style.display = 'none';
        document.getElementById("tblPeriod").style.display = 'block';

    }
</script>

