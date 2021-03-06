﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserNewSignupMIS.ascx.cs"
    Inherits="WealthERP.BusinessMIS.AdviserNewSignupMIS" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<script type="text/javascript">

    function stopRKey(evt) {
        var evt = (evt) ? evt : ((event) ? event : null);
        var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
        if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
    }

    document.onkeypress = stopRKey;

</script>

<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
<body class="BODY">
    <table width="100%">
        <tr>
            <td>
                <div class="divPageHeading">
                    <table cellspacing="0" cellpadding="3" width="100%">
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblOrderList" runat="server" CssClass="HeaderTextBig" Text="Customer SignUp Details"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:ImageButton Visible="false" ID="btnExportFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                    OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                                <asp:ImageButton Visible="false" ID="btnExportFolio" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('excel')"
                                    Height="20px" Width="25px" OnClick="btnExportFolio_Click"></asp:ImageButton>
                                <asp:ImageButton Visible="false" ID="btnExportSIP" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('excel')"
                                    Height="20px" Width="25px" OnClick="btnExportSIP_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblType" runat="server" CssClass="FieldName">Type:</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField">
                    <asp:ListItem Text="CustomerWise" Value="Customer"></asp:ListItem>
                    <asp:ListItem Text="FolioWise" Value="folio" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="SIPWise" Value="SIP"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="right">
                <asp:Label ID="lblTypes" runat="server" Text="Select Type:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlTypes" runat="server" CssClass="cmbField">
                    <asp:ListItem Text="Online" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Offline" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <%-- </table>
    <table>--%>
        <tr>
            <td id="tdlblFromDate">
                <asp:Label class="FieldName" ID="lblFromTran" Text="From :" runat="server" />
            </td>
            <td id="tdTxtFromDate">
                <telerik:RadDatePicker ID="rdpFromDate" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput ID="DateInput1" EmptyMessage="dd/mm/yyyy" runat="server" DisplayDateFormat="d/M/yyyy"
                        DateFormat="d/M/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
                <span id="Span1" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="rfvtxtTransactionDate" ControlToValidate="rdpFromDate"
                    ErrorMessage="<br />Please select a From Date" CssClass="cvPCG" Display="Dynamic"
                    runat="server" InitialValue="">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                    Type="Date" ControlToValidate="rdpFromDate" CssClass="cvPCG" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
            </td>
            <td id="tdlblToDate" runat="server" align="right">
                <asp:Label ID="lblToTran" Text="To:" CssClass="FieldName" runat="server" />
            </td>
            <td id="tdTxtToDate">
                <telerik:RadDatePicker ID="rdpToDate" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput ID="DateInput2" runat="server" EmptyMessage="dd/mm/yyyy" DisplayDateFormat="d/M/yyyy"
                        DateFormat="d/M/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
                <span id="Span2" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdpToDate"
                    ErrorMessage="<br />Please select a To Date" CssClass="cvPCG" Display="Dynamic"
                    runat="server" InitialValue="">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                    Type="Date" ControlToValidate="rdpToDate" CssClass="cvPCG" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
                <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="rdpToDate"
                    ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                    ControlToCompare="rdpFromDate" CssClass="cvPCG" ValidationGroup="btnSubmit" Display="Dynamic">
                </asp:CompareValidator>
            </td>
            <td id="tdBtnViewRejetcs" runat="server">
                <asp:Button ID="btnViewMIS" runat="server" OnClick="btnViewMIS_Click" CssClass="PCGButton"
                    Text="Go" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%">
        <tr>
            <td colspan="1">
                <asp:Panel ID="PnlCustomerWise" runat="server" Width="90%" ScrollBars="Horizontal"
                    Visible="false">
                    <table>
                        <tr id="trOnlineProductWiseCustomers">
                            <td>
                                <telerik:RadGrid ID="gvProductWiseCustomersCount" runat="server" CssClass="RadGrid"
                                    GridLines="None" Width="120%" AllowPaging="True" PageSize="10" AllowSorting="True"
                                    AutoGenerateColumns="false" ShowStatusBar="true" AllowAutomaticDeletes="True"
                                    AllowAutomaticInserts="false" AllowAutomaticUpdates="false" Skin="Telerik" OnNeedDataSource="gvProductWiseCustomersCount_NeedDataSource"
                                    OnItemDataBound="gvProductWiseCustomersCount_OnItemDataBound"
                                    EnableEmbeddedSkins="false" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true"
                                    AllowFilteringByColumn="true">
                                    <MasterTableView ShowFooter="true" Width="50%">
                                        <Columns>
                                             <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Agent Code" DataField="AAC_AgentCode"
                                                HeaderStyle-HorizontalAlign="Left" UniqueName="AAC_AgentCode" SortExpression="AAC_AgentCode"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Product" DataField="PAG_AssetGroupCode"
                                                HeaderStyle-HorizontalAlign="Left" UniqueName="PAG_AssetGroupCode" SortExpression="PAG_AssetGroupCode"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Category" DataField="PAISC_AssetInstrumentSubCategoryName"
                                                HeaderStyle-HorizontalAlign="Left" UniqueName="PAISC_AssetInstrumentSubCategoryName"
                                                SortExpression="PAISC_AssetInstrumentSubCategoryName" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="TotalCustomer" DataField="CustCount"
                                                HeaderStyle-HorizontalAlign="Left" UniqueName="CustCount" SortExpression="CustCount"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings ReorderColumnsOnClient="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
                                        <Scrolling AllowScroll="false" />
                                        <Resizing AllowColumnResize="true" />
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td colspan="4">
                <asp:Panel ID="pnlFolio" runat="server" Width="50%" Visible="false" ScrollBars="Horizontal">
                    <table>
                        <tr>
                            <td>
                                <%--   <div runat="server" id="divFolio">--%>
                                <telerik:RadGrid ID="gvFolio" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                    AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" EnableHeaderContextMenu="true"
                                    EnableHeaderContextFilterMenu="true" OnNeedDataSource="gvFolio_OnNeedDataSource">
                                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                        FileName="gvFolio" Excel-Format="ExcelML">
                                    </ExportSettings>
                                    <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                        CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                                        GroupLoadMode="Client" ShowGroupFooter="true">
                                        <Columns>
                                         <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="C_CustCode" DataField="C_CustCode"
                                                HeaderStyle-HorizontalAlign="Left" UniqueName="C_CustCode" SortExpression="C_CustCode"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="TotalFolio" DataField="TotalFolio"
                                                HeaderStyle-HorizontalAlign="Left" UniqueName="TotalFolio" SortExpression="TotalFolio"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            
                                          
                                        </Columns>
                                    </MasterTableView>
                                    <HeaderStyle Width="150px" />
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                        <Resizing AllowColumnResize="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                                <%-- </div>--%>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td colspan="4">
                <asp:Panel ID="pnlSIP" runat="server" Width="50%" Visible="false" ScrollBars="Horizontal">
                    <table>
                        <tr>
                            <td>
                                <%--  <div runat="server" id="divSIP" style="margin: 2px; width: 640px;">--%>
                                <telerik:RadGrid ID="gvSIP" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                    AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" EnableHeaderContextMenu="true"
                                    EnableHeaderContextFilterMenu="true" OnNeedDataSource="gvSIP_OnNeedDataSource">
                                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                        FileName="gvFolio" Excel-Format="ExcelML">
                                    </ExportSettings>
                                    <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                        CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                                        GroupLoadMode="Client" ShowGroupFooter="true">
                                        <Columns>
                                         <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="C_CustCode" DataField="C_CustCode"
                                                HeaderStyle-HorizontalAlign="Left" UniqueName="C_CustCode" SortExpression="C_CustCode"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="TotalSIP" DataField="TotalSIP"
                                                HeaderStyle-HorizontalAlign="Left" UniqueName="TotalSIP" SortExpression="TotalSIP"
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
                                <%-- </div>--%>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <br />
    <%--    <div style="padding:6px">
        <asp:Label ID="LabelMainNote" runat="server" "Font-Size="Small" CssClass="cmbField"  Text="Note:&nbsp1. For folio count the folio created date is considered  <br />
         &nbsp&nbsp &nbsp &nbsp &nbsp  2. For Customer Count the customer profiling date is considered<br />
         &nbsp&nbsp &nbsp &nbsp &nbsp  3. For SIP count the SIP start date is considered">
         </asp:Label>
    </div>--%>
    <%--<br />
    <div>
        <asp:Label ID="lblNote" runat="server" CssClass="HeaderTextSmall" Text="Note: "></asp:Label>
    </div>--%>
</body>
