<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFNPAndTransactionCompare.ascx.cs"
    Inherits="WealthERP.Advisor.MFNPAndTransactionCompare" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    };

    function keyPress(sender, args) {
        if (args.keyCode == 13) {
            return false;
        }
    }

</script>

<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            MF NP & Tranx Compare
                        </td>
                        <td align="right" id="tdMFNPTranxCompareExport" runat="server" style="padding-bottom: 2px;">
                            <asp:ImageButton ID="btnMFNPTranxCompare" ImageUrl="~/Images/Export_Excel.png" runat="server"
                                AlternateText="Excel" ToolTip="Export To Excel" Visible="false" OnClientClick="setFormat('excel')"
                                Height="25px" Width="25px" OnClick="btnMFNPTranxCompare_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table class="TableBackground" width="100%">
    <tr>
        <td id="tdAdviser" runat="server" align="right"  valign="top">
            <asp:Label ID="lblAdviser" CssClass="FieldName" runat="server" Text="Select Adviser:"></asp:Label>
            </td><td align="left">
            <asp:DropDownList ID="ddlAdviser" runat="server" CssClass="cmbField" 
                AutoPostBack="true" onselectedindexchanged="ddlAdviser_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:CompareValidator ID="cvAdviserId" runat="server" ControlToValidate="ddlAdviser"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Adviser"
                Operator="NotEqual" ValidationGroup="btnGo" ValueToCompare="Select">
            </asp:CompareValidator>
        </td>
        <td colspan="4" class="leftField" >
        </td>
    </tr>
    <tr id="trBranchRM" runat="server">
        <td align="right" >
            <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch:"></asp:Label>
            </td><td>
            <asp:DropDownList ID="ddlBranch" runat="server" Style="vertical-align: middle" AutoPostBack="true"
                CssClass="cmbField" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM:"></asp:Label>
            </td><td>
            <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" AutoPostBack="true"
                Style="vertical-align: middle">
            </asp:DropDownList>
        </td>
        <td align="right" >
            <asp:Label ID="lblAsonDate" runat="server" CssClass="FieldName" Text="As on Date: "></asp:Label>
            </td><td>
            <%--<asp:Label ID="lblPickDate" Text="" runat="server" CssClass="FieldName"> </asp:Label>--%>
            <telerik:RadDatePicker ID="txtAsOnDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
        </td>
    </tr>
    <tr id="TrCustomerType" runat="server">
        <td align="right" >
            <asp:Label ID="lblGrpOrInd" runat="server" CssClass="FieldName" Text="Search for :"></asp:Label>
            </td><td>
            <asp:DropDownList ID="ddlSelectCustomer" runat="server" CssClass="cmbField" Style="vertical-align: middle"
                AutoPostBack="true" OnSelectedIndexChanged="ddlSelectCustomer_SelectedIndexChanged">
                <asp:ListItem Value="All Customer" Text="All Customer"></asp:ListItem>
                <asp:ListItem Value="Pick Customer" Text="Pick Customer"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right" >
            <asp:Label ID="lblSelectTypeOfCustomer" runat="server" CssClass="FieldName" Text="Customer Type: "></asp:Label>
            </td><td>
            <asp:DropDownList ID="ddlCustomerType" Style="vertical-align: middle" runat="server"
                CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerType_SelectedIndexChanged">
                <asp:ListItem Value="Select" Text="Select" Selected="True"></asp:ListItem>
                <asp:ListItem Value="0" Text="Group Head"></asp:ListItem>
                <asp:ListItem Value="1" Text="Individual"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td id="trCustomerSearch" runat="server" align="right"  onkeypress="return keyPress(this, event)">
            <asp:Label ID="lblselectCustomer" runat="server" CssClass="FieldName" Text="Search Customer: "></asp:Label>
            </td><td>
            <asp:TextBox ID="txtIndividualCustomer" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True">  </asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" TargetControlID="txtIndividualCustomer"
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
    </tr>
    <tr>
        <td align="left" >
            <asp:Button ID="btnGo" runat="Server" Text="Go" CssClass="PCGButton" OnClick="btnGo_Click" ValidationGroup="btnGo"/>
        </td>
        <td class="leftField"colspan="4">
            &nbsp;
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td colspan="4">
            <asp:Panel ID="pnlMfNPTranxCompare" runat="server" Width="98%" Visible="true">
                <table>
                    <tr>
                        <td>
                            <div runat="server" id="divMfNPTranxCompare" style="margin: 2px; width: 640px;">
                                <telerik:RadGrid ID="gvMfNPTranxCompare" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                    AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" OnNeedDataSource="gvMfNPTranxCompare_OnNeedDataSource"
                                    EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true">
                                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                        FileName="MfNPTranxCompare" Excel-Format="ExcelML">
                                    </ExportSettings>
                                    <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                        CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                                        GroupLoadMode="Client" ShowGroupFooter="true">
                                        <Columns>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="CustomerId" DataField="CustomerId"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="CustomerId" SortExpression="CustomerId"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-HorizontalAlign="Right" Visible="false">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Customer" DataField="Name"
                                                UniqueName="Name" SortExpression="Name" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterText="Grand Total:"
                                                FooterStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Folio" DataField="Folio"
                                                UniqueName="Folio" SortExpression="Folio" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Scheme" DataField="Scheme"
                                                UniqueName="Scheme" SortExpression="Scheme" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="NP Holding Units (A)" DataField="MfNpHoldings"
                                                HeaderStyle-HorizontalAlign="Right" UniqueName="MfNpHoldings" SortExpression="MfNpHoldings"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N3}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Tranx Holding Units (B)"
                                                DataField="TranxHoldings" HeaderStyle-HorizontalAlign="Right" UniqueName="TranxHoldings"
                                                SortExpression="TranxHoldings" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N3}"
                                                Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Holding Difference  (B-A)"
                                                DataField="HoldingDiff" HeaderStyle-HorizontalAlign="Right" UniqueName="HoldingDiff"
                                                SortExpression="HoldingDiff" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N3}"
                                                Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="NP Realized Units (C)"
                                                DataField="MfNpRealized" HeaderStyle-HorizontalAlign="Right" UniqueName="MfNpRealized"
                                                SortExpression="MfNpRealized" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N3}"
                                                Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Tranx Realized Units (D)"
                                                DataField="TranxRealized" HeaderStyle-HorizontalAlign="Right" UniqueName="TranxRealized"
                                                SortExpression="TranxRealized" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N3}"
                                                Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Realized Difference (D-C)"
                                                DataField="RealizedDiff" HeaderStyle-HorizontalAlign="Right" UniqueName="RealizedDiff"
                                                SortExpression="RealizedDiff" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N3}"
                                                Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
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
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnCustomerId" runat="server" OnValueChanged="hdnCustomerId_ValueChanged" />
<asp:HiddenField ID="hdnIndividualOrGroup" runat="server" />
<asp:HiddenField ID="hdnGoalCode" runat="server" />
<asp:HiddenField ID="HiddenField1" runat="server" />
<asp:HiddenField ID="hdnadviserId" runat="server" />
<asp:HiddenField ID="hdnAll" runat="server" />
<asp:HiddenField ID="hdnbranchId" runat="server" />
<asp:HiddenField ID="hdnbranchheadId" runat="server" />
<asp:HiddenField ID="hdnrmId" runat="server" />
<asp:HiddenField ID="hdnDate" runat="server" />
