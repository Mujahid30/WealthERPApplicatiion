<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="true" CodeBehind="AdvisorMISCommission.ascx.cs"
    Inherits="WealthERP.Advisor.AdvisorMISCommission" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager runat="server">
</telerik:RadStyleSheetManager>
<telerik:RadScriptManager runat="server">
</telerik:RadScriptManager>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<table width="100%">
    <tr>
        <td colspan="6">
            <asp:Label ID="Label7" runat="server" CssClass="HeaderTextSmall" Text="MF MIS Commission"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <table>
                <tr id="tr1" runat="server">
                    <td id="Td3" runat="server">
                        <asp:Label ID="Label1" runat="server" Text="MIS Type:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td runat="server">
                        <telerik:RadComboBox ID="ddlMISType" runat="server" CssClass="cmbField">
                            <Items>
                                <telerik:RadComboBoxItem Text="Folio Wise" Value="Folio Wise" />
                                <telerik:RadComboBoxItem Text="AMC Wise" Value="AMC Wise" Selected="true" />
                                <telerik:RadComboBoxItem Text="Transaction Type Wise" Value="Transaction_Wise" />
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
                <tr>
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
                        <telerik:RadComboBox ID="ddlPeriod" runat="server" CssClass="cmbField">
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
            <telerik:RadGrid ID="gvCommissionMIS" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" OnNeedDataSource="gvCommissionMIS_OnNeedDataSource" EnableEmbeddedSkins="false"
                Width="80%" AllowFilteringByColumn="true" AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true">
                <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="Top">
                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                        ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="true" />
                    <Columns>
                        <telerik:GridBoundColumn UniqueName="MISType" AllowFiltering="false"
                            HeaderText="">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="BrokerageAmt"  DataField="Tot" AllowFiltering="false"
                            HeaderText="Brokerage Amount">
                            <ItemStyle Width="20%" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="TrailCommission" DataField="trail" AllowFiltering="false"
                            HeaderText="Trail Commission">
                            <ItemStyle Width="20%" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
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

