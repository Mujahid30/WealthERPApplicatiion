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
                            MF Commission MIS
                        </td>
                        <td align="right">
                            <asp:ImageButton Visible="false" ID="btnCommissionMIS" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnCommissionMIS_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                            <asp:ImageButton Visible="false" ID="imgZoneClusterCommissionMIS" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnZoneCLusterMISCommission_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                            <asp:ImageButton Visible="false" ID="imgMISCommission" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
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
            <table width="50%">
                <tr id="tr1" runat="server">
                    <td id="Td3" runat="server" align="right">
                        <asp:Label ID="Label1" runat="server" Text="MIS Type:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td runat="server">
                        <telerik:RadComboBox ID="ddlMISType" AutoPostBack="true" OnSelectedIndexChanged="ddlMISType_SelectedIndexChanged"
                            runat="server" CssClass="cmbFielde" EnableEmbeddedSkins="false" Skin="Telerik"
                            AllowCustomText="true" Width="150px">
                            <Items>
                                <telerik:RadComboBoxItem Text="Organization Level" Value="Zone_Cluster_Wise" />
                                <telerik:RadComboBoxItem Text="AMC/Folio/Type Wise" Value="AMC_Folio_Type_AllMIS" />
                                <telerik:RadComboBoxItem Text="Category Wise" Value="Category Wise" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td class="style1">
                        <asp:RadioButton ID="rbtnPickDate" Class="cmbFielde" Checked="True" runat="server"
                            AutoPostBack="true" Text="Pick a Date" GroupName="Date" OnCheckedChanged="RadioButtonClick"
                            Visible="false" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbtnPickPeriod" Class="cmbFielde" runat="server" Text="Pick a Period"
                            AutoPostBack="true" GroupName="Date" OnCheckedChanged="RadioButtonClick" Visible="false" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblFromDate" Text="From:" runat="server" CssClass="FieldName">
                        </asp:Label>
                    </td>
                    <td>
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
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblPeriod" runat="server" Text="Period:" CssClass="FieldName"> </asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPeriod" runat="server" Width="120px" CssClass="cmbFielde">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <%--<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlPeriod"
                            CssClass="rfvPCG" ErrorMessage="Please select a Period" Operator="NotEqual" ValueToCompare="Select a Period"
                            ValidationGroup="btnView"> </asp:CompareValidator>
                    </td>--%>
                </tr>
                <tr id="PickAPeriodValidation" runat="server">
                    <td>
                        <%-- <asp:RequiredFieldValidator ID="ddlPeriod_RequiredFieldValidator" runat="server"
                            CssClass="rfvPCG" ErrorMessage="Please select a Period" Text="Please select a Period"
                            Display="Dynamic" ValidationGroup="btnView" ControlToValidate="ddlPeriod" InitialValue="0">
                        </asp:RequiredFieldValidator>--%>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlPeriod"
                            CssClass="rfvPCG" ErrorMessage="Please select a Period" Operator="NotEqual" ValueToCompare="Select a Period"
                            ValidationGroup="btnView"> </asp:CompareValidator>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <%--<tr id="trAMCScheme" runat="server">
        <td align="right" width="10%">
            <asp:Label ID="lblAMC" runat="server" Text="AMC:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" width="10%">
            <asp:DropDownList ID="ddlAMC" runat="server" CssClass="cmbField" AutoPostBack="false">
                <%--<asp:ListItem Text="All" Value="0">All</asp:ListItem>--%>
    <%--       </asp:DropDownList>
        </td>
        <td align="right" width="10%">
            <asp:Label ID="lblScheme" runat="server" Text="Scheme:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" width="10%">
            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField" AutoPostBack="false">
                <%--<asp:ListItem Text="All" Value="0">All</asp:ListItem>--%>
    <%--      </asp:DropDownList>
        </td>
    </tr>--%>
    <tr id="trAMCSelection" visible="false" runat="server">
        <td align="left">
            <table width="70%">
                <tr id="trSelectMutualFund" runat="server" align="left">
                    <td align="right">
                        <asp:Label ID="lblSelectMutualFund" runat="server" CssClass="FieldName" Text="Select AMC Code:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSelectMutualFund" runat="server" AutoPostBack="true" CssClass="cmbField"
                            OnSelectedIndexChanged="ddlSelectMutualFund_OnSelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="cvddlSelectMutualFund" runat="server" ControlToValidate="ddlSelectMutualFund"
                            CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select AMC Code" Operator="NotEqual"
                            ValidationGroup="vgbtnSubmit" ValueToCompare="Select AMC Code"></asp:CompareValidator>
                    </td>
                    <td id="tdscheme" runat="server" visible="true">
                        <td align="right">
                            <asp:Label ID="lblSelectSchemeNAV" runat="server" CssClass="FieldName" Text="Select Scheme Name:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSelectSchemeNAV" runat="server" CssClass="cmbFielde">
                                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlSelectSchemeNAV"
                                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select Scheme" Operator="NotEqual"
                                ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
                        </td>
                    </td>
                </tr>
                <tr id="trNavCategory" runat="server" visible="false">
                    <td align="left" class="leftField">
                        <asp:Label ID="lblNAVCategory" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlNAVCategory" runat="server" AutoPostBack="true" CssClass="cmbField"
                            OnSelectedIndexChanged="ddlNAVCategory_OnSelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <%--<tr id="trNavSubCategory" runat="server">
                <td align="right">
                    <asp:Label ID="lblNAVSubCategory" runat="server" CssClass="FieldName" 
                        Text="Sub Category:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlNAVSubCategory" runat="server" AutoPostBack="true" 
                        CssClass="cmbField" 
                        OnSelectedIndexChanged="ddlNAVSubCategory_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>--%>
                <tr id="trSelectSchemeNAV" runat="server">
                </tr>
                <%-- <tr id="trbtnSubmit" runat="server">
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" OnClick="OnClick_Submit"
                                        Text="Submit" ValidationGroup="vgbtnSubmit" />
                                </td>
                            </tr>--%>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <table>
                <tr onkeypress="return keyPress(this, event)">
                </tr>
            </table>
            <asp:Button ID="btnView" runat="server" CssClass="PCGButton" Text="Go" OnClick="btnView_Click"
                ValidationGroup="btnView" />
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadGrid ID="gvCommissionMIS" Visible="false" runat="server" GridLines="None"
                AutoGenerateColumns="False" PageSize="10" AllowSorting="true" AllowPaging="True"
                ShowStatusBar="True" ShowFooter="true" EnableViewState="false" Skin="Telerik"
                OnNeedDataSource="gvCommissionMIS_OnNeedDataSource" EnableEmbeddedSkins="false"
                OnItemCommand="gvCommissionMIS_OnItemCommand" Width="80%" AllowFilteringByColumn="true"
                AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true">
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                    FileName="MF Commission MIS Category Wise" Excel-Format="ExcelML">
                </ExportSettings>
                <MasterTableView DataKeyNames="CategoryCode" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                    CommandItemDisplay="none">
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
                        <telerik:GridBoundColumn UniqueName="subCategoryName" AllowFiltering="true" AutoPostBackOnFilter="true"
                            DataField="subCategoryName" ShowFilterIcon="false" HeaderText="SubCategory" FooterStyle-HorizontalAlign="Right"
                            AllowSorting="true">
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
                        <%--<telerik:GridTemplateColumn HeaderText="Brokerage Amount" AllowFiltering="false" UniqueName="BrokerageAmt" DataField="Brokerage"
                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkBrokerage" runat="server" CssClass="cmbField" Text='<%# Eval("BrokerageAmt") %>'
                                    OnClick="lnkBrokerage_Click">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>--%>
                        <telerik:GridTemplateColumn AllowFiltering="false" DataField="Brokerage" AutoPostBackOnFilter="true"
                            HeaderText="Brokerage Amount" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkprAmcB" runat="server" CommandName="SelectAmt" Text='<%# Eval("Brokerage")%>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <%-- <telerik:GridBoundColumn UniqueName="BrokerageAmt" DataField="Brokerage" AllowFiltering="false"
                            HeaderText="Brokerage Amount" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Width="20%" HorizontalAlign="Right" Wrap="false" />
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridTemplateColumn AllowFiltering="false" DataField="trailFee" AutoPostBackOnFilter="true"
                            HeaderText="Trail Commission" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkprAmcT" runat="server" CommandName="SelectTrail" Text='<%# Eval("trailFee") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <%--    <telerik:GridBoundColumn UniqueName="TrailCommission" DataField="trailFee" AllowFiltering="false"
                            HeaderText="Trail Commission" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Width="20%" HorizontalAlign="Right" Wrap="false" />
                        </telerik:GridBoundColumn>--%>
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
            <asp:Panel Visible="false" ID="pnlCommissionMIS" ScrollBars="Horizontal" Width="100%"
                runat="server">
                <div runat="server" id="divCommissionMIS" style="margin: 2px; width: 640px;">
                    <telerik:RadGrid ID="gvMISCommission" runat="server" GridLines="None" AutoGenerateColumns="False"
                        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                        Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                        OnNeedDataSource="gvMISCommission_OnNeedDataSource" EnableHeaderContextMenu="true"
                        OnItemCommand="gvMISCommission_OnItemCommand" EnableHeaderContextFilterMenu="true"
                        AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                            FileName="Commission MIS Details" Excel-Format="ExcelML">
                        </ExportSettings>
                        <MasterTableView DataKeyNames="AccountId,FolioNum,schemeCode" GroupsDefaultExpanded="false"
                            ExpandCollapseColumn-Groupable="true" GroupLoadMode="Client" EditMode="EditForms"
                            ShowGroupFooter="true" Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                            CommandItemDisplay="None">
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="AMC" HeaderTooltip="AMC" DataField="AMCName"
                                    UniqueName="AMCName" SortExpression="AMCName" FooterText="Grand Total:" AllowFiltering="true"
                                    AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Scheme" HeaderTooltip="Scheme" DataField="SchemeName"
                                    HeaderStyle-Width="350px" UniqueName="SchemeName" SortExpression="SchemeName"
                                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="RNT" HeaderTooltip="RNT" DataField="RNT" HeaderStyle-Width="120px"
                                    UniqueName="RNT" SortExpression="RNT" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Transaction Classification" HeaderTooltip="Transaction Classification Name"
                                    DataField="TransactionType" UniqueName="TransactionType" SortExpression="TransactionType"
                                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Group" HeaderTooltip="Group Name" DataField="Parent"
                                    UniqueName="Parent" SortExpression="Parent" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Customer" HeaderTooltip="Customer Name" DataField="CustomerName"
                                    UniqueName="CustomerName" SortExpression="CustomerName" AutoPostBackOnFilter="true"
                                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Folio" HeaderTooltip="Folio Number" DataField="FolioNum"
                                    UniqueName="FolioNum" SortExpression="FolioNum" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    Aggregate="Count" FooterText="Row Count : " ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="RM" HeaderTooltip="RM Name" DataField="RM_Name"
                                    UniqueName="RM_Name" SortExpression="RM_Name" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn AllowFiltering="false" DataField="Brokerage" AutoPostBackOnFilter="true"
                                    HeaderText="Brokerage Amount" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Select1" Text='<%# Eval("Brokerage").ToString() %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <%-- <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Brokerage Amount"
                                    HeaderText="Brokerage Amount" DataField="Brokerage" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="Brokerage" SortExpression="Brokerage" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridTemplateColumn AllowFiltering="false" DataField="trailFee" AutoPostBackOnFilter="true"
                                    HeaderText="Trail Commission" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkprAmc1" runat="server" CommandName="Select2" Text='<%# Eval("trailFee").ToString() %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <%--<telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Trail Commission"
                                    HeaderText="Trail Commission" DataField="trailFee" HeaderStyle-HorizontalAlign="Right"
                                    UniqueName="trailFee" SortExpression="trailFee" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                            </Columns>
                        </MasterTableView>
                        <HeaderStyle Width="150px" />
                        <ClientSettings ReorderColumnsOnClient="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
                <%-- <div runat="server" id="divCommissionMIS" style="margin: 2px; width: 640px;">
                    <telerik:RadGrid Visible="false" ID="gvMISCommission" runat="server" CssClass="RadGrid" GridLines="None"
                        Width="100%" AllowPaging="True" PageSize="15" AllowSorting="True" AutoGenerateColumns="false"
                        ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
                        AllowAutomaticUpdates="false" Skin="Telerik" EnableEmbeddedSkins="false" EnableHeaderContextMenu="true"
                        EnableHeaderContextFilterMenu="true" AllowFilteringByColumn="true" OnNeedDataSource="gvMISCommission_OnNeedDataSource">
                        <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="ExistMFInvestlist">
                        </ExportSettings>
                        <MasterTableView CommandItemDisplay="None">
                            <Columns>
                              <telerik:GridBoundColumn UniqueName="AMCName" HeaderText="AMC" DataField="AMCName" HeaderStyle-Width="108px"
                            SortExpression="AMCName" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                          <telerik:GridBoundColumn UniqueName="SchemeName" HeaderText="Scheme" DataField="SchemeName" HeaderStyle-Width="108px"
                            SortExpression="SchemeName" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TransactionType" HeaderText="Transaction Classification" DataField="TransactionType" HeaderStyle-Width="108px"
                            SortExpression="TransactionType" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                          <telerik:GridBoundColumn UniqueName="Parent" HeaderText="Group Name" DataField="Parent" HeaderStyle-Width="108px"
                            SortExpression="Parent" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CustomerName" HeaderText="Customer" DataField="CustomerName" HeaderStyle-Width="108px"
                            SortExpression="CustomerName" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                          <telerik:GridBoundColumn UniqueName="FolioNum" HeaderText="Folio" DataField="FolioNum" HeaderStyle-Width="108px"
                            SortExpression="FolioNum" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="RM_Name" HeaderText="RM Name" DataField="RM_Name" HeaderStyle-Width="108px"
                            SortExpression="RM_Name" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                          <telerik:GridBoundColumn UniqueName="Brokerage" HeaderText="Brokerage" DataField="Brokerage" HeaderStyle-Width="108px"
                            SortExpression="Brokerage" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="trailFee" HeaderText="trailFee" DataField="trailFee" HeaderStyle-Width="108px"
                            SortExpression="trailFee" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings ReorderColumnsOnClient="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
                            <Scrolling AllowScroll="false" />
                            <Resizing AllowColumnResize="true" />
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>--%>
            </asp:Panel>
        </td>
    </tr>
</table>
<table runat="server" id="tblZoneClusterWiseMIS" width="100%">
    <tr>
        <td>
            <asp:Panel ID="pnlZoneClusterWiseMIS" Visible="false" ScrollBars="Horizontal" Height="440px"
                runat="server">
                <div runat="server" id="divZoneClusterWiseMIS" style="margin: 2px; width: 440px;">
                    <telerik:RadGrid Visible="false" ID="gvZoneClusterWiseCommissionMIS" runat="server"
                        GridLines="None" AutoGenerateColumns="False" AllowSorting="true" ShowStatusBar="True"
                        ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                        OnNeedDataSource="gvZoneClusterWiseCommissionMIS_OnNeedDataSource" EnableHeaderContextMenu="true"
                        EnableHeaderContextFilterMenu="true" AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true"
                        OnItemDataBound="gvZoneClusterWiseCommissionMIS_OnItemDataBound">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                            FileName="Zone/Cluster Commission MIS Details" Excel-Format="ExcelML">
                        </ExportSettings>
                        <MasterTableView GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                            GroupLoadMode="Client" EditMode="EditForms" ShowGroupFooter="true" Width="100%"
                            GroupHeaderItemStyle-ForeColor="Black"
                            AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                            <GroupByExpressions>
                                <telerik:GridGroupByExpression>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="ZoneName" />
                                    </GroupByFields>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldName="ZoneName" FieldAlias="Zone"/>
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
                                    UniqueName="ZoneName" SortExpression="ZoneName" FooterText="Grand Total:" AllowFiltering="true"
                                    AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Cluster" HeaderTooltip="Cluster" DataField="ClusterName"
                                    UniqueName="ClusterName" SortExpression="ClusterName" AutoPostBackOnFilter="true"
                                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Branch" HeaderTooltip="Branch Name" DataField="BranchName"
                                    Aggregate="Count" FooterText="Row Count : " UniqueName="BranchName" SortExpression="BranchName"
                                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderTooltip="Brokerage Amount" HeaderText="Brokerage Amount"
                                    DataField="Brokerage" HeaderStyle-HorizontalAlign="Right" UniqueName="Brokerage"
                                    SortExpression="Brokerage" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderTooltip="Trail Commission" HeaderText="Trail Commission"
                                    DataField="trailFee" HeaderStyle-HorizontalAlign="Right" UniqueName="trailFee"
                                    SortExpression="trailFee" AutoPostBackOnFilter="true" AllowFiltering="false"
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
<table width="100%">
    <tr>
        <td>
            <asp:Label ID="LabelMainNote" runat="server" Font-Size="Small" CssClass="cmbFielde"
                Text="Note: 1.You can group/ungroup or hide/unhide fields by a right click on the grid label and then making the selection."></asp:Label>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hidDateType" Value="" runat="server" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnScheme" runat="server" />
<asp:HiddenField ID="hdnAMC" runat="server" />
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

