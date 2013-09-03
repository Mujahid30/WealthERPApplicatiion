<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommissionReconMIS.ascx.cs"
    Inherits="WealthERP.BusinessMIS.CommissionReceivableRecon" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Commission Receivable Recon
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="80%" class="TableBackground" cellspacing="0" cellpadding="2">
    <tr id="trSelectMutualFund" runat="server">
        <td align="left" class="leftField">
            <asp:Label ID="lblSelectMutualFund" runat="server" CssClass="FieldName" Text="Issuer:"></asp:Label>
        </td>
        <td align="right">
            <asp:DropDownList ID="ddlIssuer" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlIssuer_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:CompareValidator ID="cvddlSelectMutualFund" runat="server" ControlToValidate="ddlIssuer"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select AMC Code" Operator="NotEqual"
                ValidationGroup="vgbtnSubmit" ValueToCompare="Select AMC Code"></asp:CompareValidator>
        </td>
        <td align="left" class="leftField">
            <asp:Label ID="lblNAVCategory" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
        </td>
        <td align="right">
            <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="left" class="leftField">
            <asp:Label ID="lblSelectSchemeNAV" runat="server" CssClass="FieldName" Text="Scheme:"></asp:Label>
        </td>
        <td align="right">
            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlScheme"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select Scheme" Operator="NotEqual"
                ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trSelectType" runat="server">
        <td align="left" class="leftField">
            <asp:Label ID="lblSelectType" runat="server" CssClass="FieldName" Text="Select Type:"></asp:Label>
        </td>
        <td align="right">
            <asp:DropDownList ID="ddlUserType" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Associates" Value="Associates"></asp:ListItem>
                <asp:ListItem Text="Branch" Value="BM"></asp:ListItem>
                <asp:ListItem Text="Employee" Value="RM"></asp:ListItem>
            </asp:DropDownList>
            <asp:CompareValidator ID="CVTrxType" runat="server" ControlToValidate="ddlUserType"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an user type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td align="left" class="leftField">
            <asp:Label ID="lblSelectCode" runat="server" CssClass="FieldName" Text="Select SubBrokerCode:"></asp:Label>
        </td>
        <td align="right">
            <asp:DropDownList ID="ddlSelectType" runat="server" CssClass="cmbField" AutoPostBack="true">
            </asp:DropDownList>
        </td>
        <td align="left" class="leftField">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Select Commission Type:"></asp:Label>
        </td>
        <td align="right">
            <asp:DropDownList ID="ddlCommType" runat="server" CssClass="cmbField" AutoPostBack="true">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="Upfront" Value="UF"></asp:ListItem>
                <asp:ListItem Text="Trail" Value="TC"></asp:ListItem>
                <asp:ListItem Text="Incentive" Value="IN"></asp:ListItem>
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlCommType"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an COmmission type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td id="tdFromDate" runat="server" align="left">
            <%--            <td align="Left">--%>
            <asp:Label ID="Label10" Text="From Date:" runat="server" CssClass="FieldName"></asp:Label>
        </td>
        <td align="right">
            <telerik:RadDatePicker ID="txtFrom" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFrom"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="vgbtnSubmit"> </asp:RequiredFieldValidator>
        </td>
        <%--        </td>
--        <td id="tdToDate" runat="server">--%>
        <td align="right">
            <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="To Date:"></asp:Label>
        </td>
        <td align="left">
            <telerik:RadDatePicker ID="txtTo" CssClass="txtTo" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTo"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a to Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="vgbtnSubmit"> </asp:RequiredFieldValidator>
        </td>
        <%--        </td>
--%>
        <td align="left" style="padding-right: 50px">
            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" OnClick="GdBind_Click"
                Text="GO" ValidationGroup="vgbtnSubmit" />
        </td>
        <td>
        </td>
    </tr>
</table>
<div style="width: 100%; overflow: scroll;">
    <telerik:RadGrid ID="gvCommissionReceiveRecon" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="150%" AllowFilteringByColumn="true"
        AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" EnableHeaderContextMenu="true"
        EnableHeaderContextFilterMenu="true" OnNeedDataSource="gvCommissionReceiveRecon_OnNeedDataSource">
        <%--<exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                    filename="Company/Sector" excel-format="ExcelML">--%>
        <%-- </exportsettings>--%>
        <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
            CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
            GroupLoadMode="Client" ShowGroupFooter="true">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="SchemePlan" DataField="schemeplanname"
                    UniqueName="schemeplanname" SortExpression="schemeplanname" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Transaction Date"
                    DataField="transactiondate" UniqueName="transactiondate" SortExpression="transactiondate"
                    AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Amount" DataField="amount"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="amount" SortExpression="amount"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Amount" DataField="AMOUNT"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="AMOUNT" SortExpression="AMOUNT"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Transaction Type"
                    DataField="transactiontype" HeaderStyle-HorizontalAlign="Right" UniqueName="transactiontype"
                    SortExpression="transactiontype" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Age" DataField="Age"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="Age" SortExpression="Age" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Transaction Type"
                    DataField="TRANSACTIONTYPE" HeaderStyle-HorizontalAlign="Right" UniqueName="TRANSACTIONTYPE"
                    SortExpression="TRANSACTIONTYPE" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Transaction Date"
                    DataField="TransactionDate" HeaderStyle-HorizontalAlign="Right" UniqueName="TransactionDate"
                    SortExpression="UPFRONT" AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="300px" HeaderText="Scheme" DataField="SCHEMEPLANNAME"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="SCHEMEPLANNAME" SortExpression="SCHEMEPLANNAME"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Folio Number" DataField="FOLIONUM"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="FOLIONUM" SortExpression="FOLIONUM"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Sub-Brokercode" DataField="subbroker"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="subbroker" SortExpression="subbroker"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Brokerage Value(%)"
                    DataField="BROKERAGEVALUE" HeaderStyle-HorizontalAlign="Right" UniqueName="BROKERAGEVALUE"
                    SortExpression="BROKERAGEVALUE" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="ExpectedAmt" DataField="UPFRONT"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="UPFRONT" SortExpression="UPFRONT"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Average Aum" DataField="AverageAum"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="AverageAum" SortExpression="AverageAum"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Total Days" DataField="TotalDays"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="TotalDays" SortExpression="TotalDays"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Aum Per Day" DataField="AumPerDay"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="AumPerDay" SortExpression="AumPerDay"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Trail Fee" DataField="TrailFee"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="TrailFee" SortExpression="TrailFee"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Trail Per Day" DataField="TrailPerDay"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="TrailPerDay" SortExpression="TrailPerDay"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Trail For Period"
                    DataField="TrailForPeriod" HeaderStyle-HorizontalAlign="Right" UniqueName="TrailForPeriod"
                    SortExpression="TrailForPeriod" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
    <asp:HiddenField ID="hdnschemeId" runat="server" Visible="false" />
    <asp:HiddenField ID="hdnCategory" runat="server" Visible="false" />
    <asp:HiddenField ID="hdnFromDate" runat="server" />
    <asp:HiddenField ID="hdnToDate" runat="server" />
    <asp:HiddenField ID="hdnSBbrokercode" runat="server" />
</div>
