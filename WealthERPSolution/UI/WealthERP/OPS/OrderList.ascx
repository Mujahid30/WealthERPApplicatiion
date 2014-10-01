<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderList.ascx.cs" Inherits="WealthERP.OPS.OrderList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value();
        //alert(document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value());
    };

</script>

<script type="text/javascript">
    function keyPress(sender, args) {
        if (args.keyCode == 13) {
            return false;
        }
    }
</script>

<script language="javascript" type="text/javascript">

    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this Order?');
        if (bool) {
            document.getElementById("ctrl_OrderList_hdnMsgValue").value = 1;
            document.getElementById("ctrl_OrderList_hiddenassociation").click();
            return false;
        }
        else {
            document.getElementById("ctrl_OrderList_hdnMsgValue").value = 0;
            document.getElementById("ctrl_OrderList_hiddenassociation").click();
            return true;
        }
    }
</script>

<table width="100%">
    <tr>
        <td colspan="3">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            MF Order Book
                        </td>
                        <td align="right" style="padding-bottom: 2px;">
                            <asp:ImageButton ID="btnExportFilteredDupData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredDupData_OnClick"
                                OnClientClick="setFormat('CSV')" Height="25px" Width="25px" Visible="false">
                            </asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="80%" cellspacing="0" cellpadding="3" onkeypress="return keyPress(this, event)">
    <tr>
        <td class="leftField">
            <asp:Label ID="lblOrderType" runat="server" CssClass="FieldName" Text="Order Type:"></asp:Label>
        </td>
        <td class="rightField">
            <%--<asp:RadioButton runat="server" ID="rdoPickCustomer" Text="Pick Customer" AutoPostBack="true"
              Class="cmbField" GroupName="SelectCustomer" oncheckedchanged="rdoPickCustomer_CheckedChanged"/>--%>
            <asp:DropDownList ID="ddlOrderType" Style="vertical-align: middle" runat="server"
                CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged">
                <asp:ListItem Value="All" Text="All" Selected="True" Enabled="false"></asp:ListItem>
                <asp:ListItem Value="MF" Text="Mutual Fund"></asp:ListItem>
                <asp:ListItem Value="IN" Text="Life Insurance" Enabled="false"></asp:ListItem>
                <asp:ListItem Value="FI" Text="FixedIncome"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblFromDate" Text="From Date:" runat="server" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <telerik:RadDatePicker ID="txtFromDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                    <%-- <ClientEvents OnLoad="onLoadRadTimePicker1"> </ClientEvents>--%>
                </DateInput>
            </telerik:RadDatePicker>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="rvFromdate" ControlToValidate="txtFromDate" CssClass="rfvPCG"
                ErrorMessage="<br />Please select a  Date" Display="Dynamic" runat="server" InitialValue=""
                ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblToDate" runat="server" CssClass="FieldName" Text="To Date:"></asp:Label>
        </td>
        <td align="left">
            <telerik:RadDatePicker ID="txtToDate" CssClass="txtTo" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                    <%--  <ClientEvents OnLoad="onLoadRadTimePicker2"> </ClientEvents>--%>
                </DateInput>
            </telerik:RadDatePicker>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="rvtoDate" ControlToValidate="txtToDate" CssClass="rfvPCG"
                ErrorMessage="<br />Please select a Date" Display="Dynamic" runat="server" InitialValue=""
                ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:CompareValidator ID="dateCompareValidator" runat="server" ControlToValidate="txtToDate"
                ControlToCompare="txtFromDate" Operator="GreaterThanEqual" Type="Date" ValidationGroup="MFSubmit"
                Display="Dynamic" ErrorMessage="To date Should be Greater Than or Equal to From Date"
                CssClass="rfvPCG">
            </asp:CompareValidator>
        </td>
    </tr>
    <%-- <tr>
        <td align="right">
            <asp:Label ID="lblFrom" runat="server" Text=" Order From Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtFrom" runat="server" CssClass="txtField">
            </asp:TextBox><span id="spnFromDate" class="spnRequiredField">*</span>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFrom"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtFrom"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="rvFromdate" ControlToValidate="txtFrom" CssClass="rfvPCG"
                ErrorMessage="<br />Please select a  Date" Display="Dynamic" runat="server" InitialValue=""
                ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="rfvPCG" ControlToValidate="txtFrom"
                Display="Dynamic" ErrorMessage="Invalid Date" ValidationGroup="MFSubmit" Operator="DataTypeCheck"
                Type="Date">
            </asp:CompareValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblTo" runat="server" Text="Order ToDate: " CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtTo" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="spnToDate" class="spnRequiredField">*</span>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTo"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtTo"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="rvtoDate" ControlToValidate="txtTo" CssClass="rfvPCG"
                ErrorMessage="<br />Please select a Date" Display="Dynamic" runat="server" InitialValue=""
                ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator2" runat="server" CssClass="rfvPCG" ControlToValidate="txtTo"
                Display="Dynamic" ErrorMessage="Invalid Date" ValidationGroup="MFSubmit" Operator="DataTypeCheck"
                Type="Date">
            </asp:CompareValidator>
            <asp:CompareValidator ID="cvtodate" runat="server" ErrorMessage="<br/>To Date should not less than From Date"
                Type="Date" ControlToValidate="txtTo" ControlToCompare="txtFrom" Operator="GreaterThanEqual"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>
        </td>
    </tr>--%>
    <tr id="trRMbranch" runat="server" visible="false">
        <td align="right">
            <asp:Label ID="lblBranch" runat="server" Text="Select The Branch: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td>
        </td>
        <td align="right">
            <asp:Label ID="lblRM" runat="server" Text="Select the RM: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblOrderStatus" runat="server" Text="Order Status:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="cmbField" AutoPostBack="false">
            </asp:DropDownList>
        </td>
        <td>
        </td>
        <td class="leftField">
            <asp:Label ID="lblselectCustomer" runat="server" CssClass="FieldName" Text="Search Customer:"></asp:Label>
        </td>
        <td align="left" onkeypress="return keyPress(this, event)">
            <asp:TextBox ID="txtIndividualCustomer" runat="server" CssClass="txtField" AutoComplete="Off"
                Enabled="true" AutoPostBack="True">  </asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtIndividualCustomer_water" TargetControlID="txtIndividualCustomer"
                WatermarkText="Enter few chars of Customer" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtIndividualCustomer_autoCompleteExtender"
                runat="server" TargetControlID="txtIndividualCustomer" ServiceMethod="GetCustomerName"
                ServicePath="~/CustomerPortfolio/AutoComplete.asmx" MinimumPrefixLength="1" EnableCaching="False"
                CompletionSetCount="5" CompletionInterval="100" CompletionListCssClass="AutoCompleteExtender_CompletionList"
                CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" DelimiterCharacters="" OnClientItemSelected="GetCustomerId"
                Enabled="True" />
            <asp:RequiredFieldValidator ID="rquiredFieldValidatorIndivudialCustomer" Display="Dynamic"
                ControlToValidate="txtIndividualCustomer" CssClass="rfvPCG" ErrorMessage="<br />Please select the customer"
                runat="server" ValidationGroup="CustomerValidation">
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr id="trZCCS" runat="server">
        <td align="right">
            <asp:Label ID="lblBrokerCode" runat="server" Text="Sub Broker Code:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:DropDownList ID="ddlBrokerCode" runat="server" CssClass="cmbField">
                <%--  <asp:ListItem Text="SubBroker Code" Value="0" Selected="true"></asp:ListItem>--%>
            </asp:DropDownList>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <%--<td align="right">
            <asp:Label ID="lblZonal" runat="server" Text="Zonal Manager: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlZonalHead" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlZonal_SelectedIndexChanged">
                <asp:ListItem Text="All" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Label ID="lblCluster" runat="server" Text="Area Manager: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlClusterHead" runat="server" CssClass="cmbField">
                <asp:ListItem Text="All" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Label ID="lblChannel" runat="server" Text="Channel Manager: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlChannelManager" runat="server" CssClass="cmbField">
                <asp:ListItem Text="All" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </td>--%>
    <%-- </tr>--%>
    <tr>
        <td colspan="2" align="left">
        </td>
        <td colspan="2">
        </td>
        <td colspan="2" align="left">
        </td>
    </tr>
    <tr>
        <%-- <td colspan="2">
           <asp:DropDownList ID="DropDownList1" Style="vertical-align: middle" runat="server"
                CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged">
               <asp:ListItem Value="MF" Text="Mutual Fund"></asp:ListItem>
                <asp:ListItem Value="SetTheme" Text="SetTheme"  ></asp:ListItem>
               
                 
            </asp:DropDownList>
        </td>--%>
        <td colspan="2" align="left">
            <asp:Button ID="btnGo" runat="server" Text="GO" CssClass="PCGButton" ValidationGroup="MFSubmit"
                OnClick="btnGo_Click" />
        </td>
        <td colspan="2" align="left">
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
<asp:Panel ID="pnlOrderList" runat="server" class="Landscape" Width="100%" Height="80%"
    ScrollBars="Horizontal" Visible="false">
    <table width="100%">
        <tr id="trExportFilteredDupData" runat="server">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="gvOrderList" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AllowAutomaticInserts="false"
                    OnNeedDataSource="gvOrderList_OnNeedDataSource" OnItemDataBound="gvOrderList_ItemDataBound">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="OrderMIS">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="CO_OrderId,C_CustomerId,PAG_AssetGroupCode,CO_OrderDate"
                        Width="102%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                            ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                        <Columns>
                            <telerik:GridTemplateColumn ItemStyle-Width="120px" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlMenu1" runat="server" CssClass="cmbField" Width="100px" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="View" Value="View"></asp:ListItem>
                                        <asp:ListItem Text="Edit" Value="Edit"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <%-- <telerik:RadComboBox ID="ddlMenu" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged"
                                        CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" Skin="Telerik"
                                        AllowCustomText="true" Width="100px" AutoPostBack="true">
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="~/Images/Select.png" Text="Select" Value="0" Selected="true" >
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem Text="View" Value="View" ImageUrl="~/Images/DetailedView.png"
                                                runat="server"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="~/Images/RecordEdit.png" Text="Edit" Value="Edit"
                                                runat="server"></telerik:RadComboBoxItem>
                                            <%--<telerik:RadComboBoxItem ImageUrl="~/Images/DeleteRecord.png" Text="Delete" Value="Delete"
                                            runat="server"></telerik:RadComboBoxItem>--%>
                            <%--  </Items>
                                    </telerik:RadComboBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn> --%>
                            <%-- <telerik:GridButtonColumn ButtonType="LinkButton" CommandName="Redirect" UniqueName="CO_OrderId"
                                HeaderText="Order No." DataTextField="CO_OrderId" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridButtonColumn>--%>
                            <telerik:GridBoundColumn DataField="CO_OrderId" AllowFiltering="true" HeaderText="Order No."
                                UniqueName="CO_OrderId" SortExpression="CO_OrderId" ShowFilterIcon="false" CurrentFilterFunction="EqualTo"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="85px" FilterControlWidth="50px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFOD_OrderNumber" AllowFiltering="true" HeaderText="Order No."
                                UniqueName="CMFOD_OrderNumber" SortExpression="CMFOD_OrderNumber" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="85px"
                                FilterControlWidth="50px" Visible="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_OrderDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="true" HeaderText="Order Date" UniqueName="CO_OrderDate" SortExpression="CO_OrderDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFA_FolioNum" AllowFiltering="true" HeaderText="Folio"
                                UniqueName="CMFA_FolioNum" SortExpression="CMFA_FolioNum" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"
                                FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Name" AllowFiltering="true" HeaderText="Customer"
                                UniqueName="Name" SortExpression="Name" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SubBrokerCode" AllowFiltering="true" HeaderText="SubBroker Code"
                                UniqueName="SubBrokerCode" SortExpression="SubBrokerCode" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_PANNum" AllowFiltering="true" HeaderText="PAN"
                                UniqueName="C_PANNum" SortExpression="C_PANNum" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PAG_AssetGroupName" AllowFiltering="true" HeaderText="Asset Name"
                                UniqueName="PAG_AssetGroupName" SortExpression="PAG_AssetGroupName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PASP_SchemePlanName" HeaderText="SchemePlanName"
                                AllowFiltering="true" SortExpression="PASP_SchemePlanName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="PASP_SchemePlanName"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="250px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PAIC_AssetInstrumentCategoryName" HeaderText="Category"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="PAIC_AssetInstrumentCategoryName"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="PAIC_AssetInstrumentCategoryName" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PAISC_AssetInstrumentSubCategoryName" HeaderText="Sub Category"
                                AllowFiltering="false" HeaderStyle-Wrap="false" SortExpression="PAISC_AssetInstrumentSubCategoryName"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="100px" UniqueName="PAISC_AssetInstrumentSubCategoryName" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFOD_Amount" AllowFiltering="true" HeaderText="Amount"
                                DataFormatString="{0:N0}" UniqueName="CMFOD_Amount" SortExpression="CMFOD_Amount"
                                ShowFilterIcon="false" HeaderStyle-Width="80px" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ChequeNumber" HeaderText="Cheque No.\Draft No."
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CO_ChequeNumber"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="100px" UniqueName="CO_ChequeNumber" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_PaymentDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="true" HeaderText="Payment Date" UniqueName="CO_PaymentDate" SortExpression="CO_PaymentDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFOD_BankName" HeaderText="Bank" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="CMFOD_BankName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                                UniqueName="CMFOD_BankName" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFOD_BranchName" HeaderText="Branch" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="CMFOD_BranchName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                                UniqueName="CMFOD_BranchName" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFOD_ARNNo" HeaderText="ARN No." AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="CMFOD_ARNNo" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                                UniqueName="CMFOD_ARNNo" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Address" HeaderText="Address" AllowFiltering="true"
                                Visible="false" HeaderStyle-Wrap="false" SortExpression="Address" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                                UniqueName="Address" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFOD_City" HeaderText="City" AllowFiltering="true"
                                Visible="false" HeaderStyle-Wrap="false" SortExpression="CMFOD_City" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                                UniqueName="CMFOD_City" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFOD_PinCode" HeaderText="Pin Code" AllowFiltering="true"
                                Visible="false" HeaderStyle-Wrap="false" SortExpression="CMFOD_PinCode" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                                UniqueName="CMFOD_PinCode" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XF_FrequencyCode" HeaderText="Frequency" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="XF_FrequencyCode" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                                UniqueName="XF_FrequencyCode" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFOD_ApprovalDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="true" HeaderText="Approval Date" UniqueName="CMFOD_ApprovalDate"
                                SortExpression="CMFOD_ApprovalDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WMTT_TransactionType" AllowFiltering="true" HeaderText="Transaction Type"
                                UniqueName="WMTT_TransactionType" SortExpression="WMTT_TransactionType" ShowFilterIcon="false"
                                HeaderStyle-Width="80px" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XS_Status" AllowFiltering="true" HeaderText="Order Status"
                                HeaderStyle-Width="80px" UniqueName="XS_Status" SortExpression="XS_Status" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ApplicationNumber" AllowFiltering="true" HeaderText="Application No."
                                UniqueName="CO_ApplicationNumber" SortExpression="CO_ApplicationNumber" ShowFilterIcon="false"
                                HeaderStyle-Width="80px" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DeputyManagerName" AllowFiltering="true" HeaderText="Deputy Manager"
                                UniqueName="DeputyManagerName" SortExpression="DeputyManagerName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ZonalManagerName" AllowFiltering="true" HeaderText="Zonal Manager"
                                UniqueName="ZonalManagerName" SortExpression="ZonalManagerName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AreaManager" AllowFiltering="true" HeaderText="Area Manager"
                                UniqueName="AreaManager" SortExpression="AreaManager" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="true" DataField="ClusterManager" AllowFiltering="true"
                                HeaderText="Cluster Manager" UniqueName="ClusterManager" SortExpression="ClusterManager"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ChannelName" AllowFiltering="true" HeaderText="Channel"
                                UniqueName="ChannelName" SortExpression="ChannelName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AssociatesName" AllowFiltering="true" HeaderText="SubBroker Name"
                                Visible="true" UniqueName="AssociatesName" SortExpression="AssociatesName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="120px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Titles" AllowFiltering="true" HeaderText="Title"
                                Visible="true" UniqueName="Titles" SortExpression="Titles" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="120px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ReportingManagerName" AllowFiltering="true" HeaderText="Reporting Manager"
                                Visible="true" UniqueName="ReportingManagerName" SortExpression="ReportingManagerName"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="120px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="UserType" AllowFiltering="true" HeaderText="Type"
                                Visible="true" UniqueName="UserType" SortExpression="UserType" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="120px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn DataField="IS_SchemeName" AllowFiltering="false" HeaderText="Scheme Name"
                                UniqueName="IS_SchemeName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XII_InsuranceIssuerName" AllowFiltering="false"
                                HeaderText="Issuer Name" UniqueName="XII_InsuranceIssuerName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                            <%--<telerik:GridBoundColumn DataField="CO_ApplicationReceivedDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="false" HeaderText="Application Received Date" UniqueName="CO_ApplicationReceivedDate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CIOD_PolicyMaturityDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="false" HeaderText="Maturity Date" UniqueName="CIOD_PolicyMaturityDate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_PaymentDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="false" HeaderText="Payment Date" UniqueName="CO_PaymentDate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WOSR_SourceName" AllowFiltering="false" HeaderText="Source Type"
                                UniqueName="WOSR_SourceName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                            <%--<telerik:GridBoundColumn DataField="XPM_PaymentMode" AllowFiltering="false" HeaderText="Payment Mode"
                                UniqueName="XPM_PaymentMode">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ChequeNumber" AllowFiltering="false" HeaderText="Cheque Number"
                                UniqueName="CO_ChequeNumber">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CB_BankName" AllowFiltering="false" HeaderText="Bank Name"
                                UniqueName="CB_BankName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CIOD_SumAssured" AllowFiltering="false" HeaderText="Sum Assured"
                                UniqueName="CIOD_SumAssured">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XF_Frequency" AllowFiltering="false" HeaderText="Frequency"
                                UniqueName="XF_Frequency">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="hdnBranchId" runat="server" />
<asp:HiddenField ID="hdnRMId" runat="server" />
<asp:HiddenField ID="hdnFromdate" runat="server" />
<asp:HiddenField ID="hdnTodate" runat="server" />
<asp:HiddenField ID="hdnAgentId" runat="server" />
<asp:HiddenField ID="hdnAgentCode" runat="server" />
<asp:HiddenField ID="hdnSubBrokerCode" runat="server" />
<asp:HiddenField ID="hdnSubBrokerName" runat="server" />
<asp:HiddenField ID="hdnMsgValue" runat="server" />
<asp:HiddenField ID="hdnOrderStatus" runat="server" />
<asp:Button ID="hiddenassociation" runat="server" OnClick="hiddenassociation_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:HiddenField ID="hdnCustomerId" runat="server" OnValueChanged="hdnCustomerId_ValueChanged" />
<asp:HiddenField ID="hdnIndividualOrGroup" runat="server" />
<asp:HiddenField ID="hdnOrderType" runat="server" />
<%--
-------------   Fixed Income ----------------------%>
<asp:Panel ID="pnlFiOrderList" runat="server" class="Landscape" Width="100%" Height="100%"
    ScrollBars="Horizontal" Visible="false">
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="gvFIOrderList" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AllowAutomaticInserts="false"
                    OnNeedDataSource="gvFIOrderList_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="OrderMIS">
                    </ExportSettings>
                    <%--  OnItemDataBound="gvOrderList_ItemDataBound"--%>
                    <MasterTableView DataKeyNames="CO_OrderId,C_CustomerId,PAG_AssetGroupCode,CO_OrderDate"
                        Width="102%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                        <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                            ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                        <Columns>
                            <telerik:GridTemplateColumn ItemStyle-Width="120px" AllowFiltering="false">
                                <ItemTemplate>
                                    <telerik:RadComboBox ID="ddlMenu" OnSelectedIndexChanged="ddlFIMenu_SelectedIndexChanged"
                                        CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" Skin="Telerik"
                                        AllowCustomText="true" Width="80px" AutoPostBack="true">
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="~/Images/Select.png" Text="Select" Value="0" Selected="true">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem Text="View" Value="View" ImageUrl="~/Images/DetailedView.png"
                                                runat="server"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="~/Images/RecordEdit.png" Text="Edit" Value="Edit"
                                                runat="server"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="CFIOD_OrderNO" AllowFiltering="true" HeaderText="Order No."
                                UniqueName="CFIOD_OrderNO" SortExpression="CFIOD_OrderNO" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="85px"
                                FilterControlWidth="50px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_OrderDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="true" HeaderText="Order Date" UniqueName="CO_OrderDate" SortExpression="CO_OrderDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="80px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CFIOD_TransactionType" HeaderText="Fresh/Renewal"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CFIOD_TransactionType"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CFIOD_TransactionType" FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PFIIM_IssuerName" HeaderText="Company Name" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="PFIIM_IssuerName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="PFIIM_IssuerName"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PFISM_SchemeName" HeaderText="Scheme Name" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="PFISM_SchemeName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="PFISM_SchemeName"
                                FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ApplicationNumber" HeaderText="Applicati- No/Cheque No"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CO_ApplicationNumber"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="100px" UniqueName="CO_ApplicationNumber" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Name" AllowFiltering="true" HeaderText="Name of the applicant"
                                UniqueName="Name" SortExpression="Name" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CFIOD_AmountPayable" AllowFiltering="true" HeaderText="Deposit Amount"
                                DataFormatString="{0:N0}" UniqueName="CFIOD_AmountPayable" SortExpression="CFIOD_AmountPayable"
                                ShowFilterIcon="false" HeaderStyle-Width="80px" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XPM_PaymentModeCode" AllowFiltering="true" HeaderText="Mode of payment"
                                UniqueName="XPM_PaymentModeCode" SortExpression="XPM_PaymentModeCode" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Tenure" AllowFiltering="true" HeaderText="Tenure(in m-th)"
                                UniqueName="Tenure" SortExpression="Tenure" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Cumulative/ N- Cumulative" AllowFiltering="true"
                                HeaderText="CFIOD_SchemeOption" UniqueName="Cumulative/ N- Cumulative" SortExpression="Tenure"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_PANNum" AllowFiltering="true" HeaderText="Applicant PAN No"
                                UniqueName="C_PANNum" SortExpression="C_PANNum" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CFIOD_ExisitingDepositreceiptno" AllowFiltering="true"
                                HeaderText="Folio No" UniqueName="CFIOD_ExisitingDepositreceiptno" SortExpression="CFIOD_ExisitingDepositreceiptno"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CB_MICR" AllowFiltering="true" HeaderText="MICR"
                                UniqueName="CB_MICR" SortExpression="CB_MICR" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ChequeNumber" AllowFiltering="true" HeaderText="Cheque No/DD No"
                                UniqueName="CO_ChequeNumber" SortExpression="CO_ChequeNumber" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WERPBM_BankName" AllowFiltering="true" HeaderText="Submitted Bank"
                                UniqueName="WERPBM_BankName" SortExpression="WERPBM_BankName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CB_BranchName" AllowFiltering="true" HeaderText="Submitted Bank Branch"
                                UniqueName="CB_BranchName" SortExpression="CB_BranchName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ZonalManagerName" AllowFiltering="true" HeaderText="Zonal Manager"
                                UniqueName="ZonalManagerName" SortExpression="ZonalManagerName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AreaManager" AllowFiltering="true" HeaderText="Area Manager"
                                UniqueName="AreaManager" SortExpression="AreaManager" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="CircleManager" AllowFiltering="true"
                                HeaderText="Channel Manager" UniqueName="CircleManager" SortExpression="CircleManager"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ChannelName" AllowFiltering="true" HeaderText="Channel"
                                UniqueName="ChannelName" SortExpression="ChannelName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PAG_AssetGroupName" AllowFiltering="true" HeaderText="Asset Name"
                                UniqueName="PAG_AssetGroupName" SortExpression="PAG_AssetGroupName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SubBrokerCode" AllowFiltering="true" HeaderText="SubBroker Code"
                                UniqueName="SubBrokerCode" SortExpression="SubBrokerCode" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AssociatesName" AllowFiltering="true" HeaderText="SubBroker Name"
                                Visible="true" UniqueName="AssociatesName" SortExpression="AssociatesName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="120px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PAIC_AssetInstrumentCategoryName" HeaderText="Category"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="PAIC_AssetInstrumentCategoryName"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="PAIC_AssetInstrumentCategoryName" FooterStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XS_Status" HeaderText="Status" AllowFiltering="true"
                                HeaderStyle-Wrap="false" SortExpression="XS_Statust" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="100px" UniqueName="XS_Status"
                                FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%-- <telerik:GridBoundColumn DataField="CFIOD_MaturityAmount" HeaderText="MaturityAmount"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="CFIOD_MaturityAmount"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="100px" UniqueName="CFIOD_MaturityAmount" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PFISD_defaultInterestRate" HeaderText="InterestRate(%)"
                                AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="PFISD_defaultInterestRate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="100px" UniqueName="PFISD_defaultInterestRate" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
