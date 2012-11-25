<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserRMMFSystematicMIS.ascx.cs"
    Inherits="WealthERP.Advisor.AdviserRMMFSystematicMIS" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="demo" Namespace="DanLudwig.Controls.Web" Assembly="DanLudwig.Controls.AspAjax.ListBox" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scptMgr" runat="server" EnablePartialRendering="true">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    };
</script>

<table style="width: 100%;">
    <tr>
        <td class="HeaderTextBig">
            <asp:Label ID="lblMFSystematicMIS" runat="server" CssClass="HeaderTextBig" Text="MF Systematic MIS"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td style="width: 100%;">
            <table style="width: 100%;">
                <tr id="trBranchRM" runat="server">
                    <td align="right" width="10%">
                        <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch:"></asp:Label>
                    </td>
                    <td align="left" style="width: 10%;">
                        <asp:DropDownList ID="ddlBranch" runat="server" Style="vertical-align: middle" AutoPostBack="true"
                            CssClass="cmbField" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="width: 10%;">
                        <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM:"></asp:Label>
                    </td>
                    <td align="left" style="width: 10%;">
                        <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" Style="vertical-align: middle">
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
                <tr>
                    <td align="right" width="10%">
                        <asp:Label ID="lblGrpOrInd" runat="server" CssClass="FieldName" Text="MIS for :"></asp:Label>
                    </td>
                    <td id="tdSelectCusto" runat="server" align="left" width="10%">
                        <asp:DropDownList ID="ddlSelectCustomer" runat="server" CssClass="cmbField" Style="vertical-align: middle"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlSelectCustomer_SelectedIndexChanged">
                            <asp:ListItem Value="All Customer" Text="All Customer"></asp:ListItem>
                            <asp:ListItem Value="Pick Customer" Text="Pick Customer"></asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:RadioButton runat="server" ID="rdoAllCustomer" Text="All Customers" AutoPostBack="true"
              Class="cmbField" GroupName="SelectCustomer" Checked="True" 
              oncheckedchanged="rdoAllCustomer_CheckedChanged" />
              <br />
              <asp:RadioButton runat="server" ID="rdoPickCustomer" Text="Pick Customer" AutoPostBack="true"
              Class="cmbField" GroupName="SelectCustomer" 
              oncheckedchanged="rdoPickCustomer_CheckedChanged" />--%>
                    </td>
                    <td align="right" width="10%">
                        <asp:Label ID="lblSelectTypeOfCustomer" runat="server" CssClass="FieldName" Text="Customer Type: "></asp:Label>
                    </td>
                    <td align="left" width="10%">
                        <asp:DropDownList ID="ddlSelectCutomer" Style="vertical-align: middle" runat="server"
                            CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectCutomer_SelectedIndexChanged">
                            <asp:ListItem Value="Select" Text="Select" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="Group Head" Text="Group Head"></asp:ListItem>
                            <asp:ListItem Value="Individual" Text="Individual"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        <asp:Label ID="lblselectCustomer" runat="server" CssClass="FieldName" Text="Search Customer: "></asp:Label>
                    </td>
                    <td align="left" width="10%">
                        <asp:TextBox ID="txtIndividualCustomer" runat="server" CssClass="txtField" AutoComplete="Off"
                            AutoPostBack="True">  </asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtIndividualCustomer_water" TargetControlID="txtIndividualCustomer"
                            WatermarkText="Enter few chars of Customer" runat="server" EnableViewState="false">
                        </cc1:TextBoxWatermarkExtender>
                        <ajaxToolkit:AutoCompleteExtender ID="txtIndividualCustomer_autoCompleteExtender"
                            runat="server" TargetControlID="txtIndividualCustomer" ServiceMethod="GetCustomerName"
                            ServicePath="~/CustomerPortfolio/AutoComplete.asmx" MinimumPrefixLength="1" EnableCaching="False"
                            CompletionSetCount="5" CompletionInterval="100" CompletionListCssClass="AutoCompleteExtender_CompletionList"
                            CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                            UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                            Enabled="True" />
                        <asp:RequiredFieldValidator ID="rquiredFieldValidatorIndivudialCustomer" Display="Dynamic"
                            ControlToValidate="txtIndividualCustomer" CssClass="rfvPCG" ErrorMessage="<br />Please select the customer"
                            runat="server" ValidationGroup="btnGo">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="10%">
                        <asp:Label ID="lblSystematicType" runat="server" Text="Systematic Type:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td align="left" width="10%">
                        <asp:DropDownList ID="ddlSystematicType" runat="server" CssClass="cmbField" AutoPostBack="false">
                        </asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        <asp:Label ID="lblAMC" runat="server" Text="AMC:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td align="left" width="10%">
                        <asp:DropDownList ID="ddlAMC" runat="server" CssClass="cmbField" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlAMC_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td align="left" width="10%">
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="false">
                        </asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        <asp:Label ID="lblScheme" runat="server" Text="Scheme:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td align="left" width="10%">
                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="10%">
                        <asp:Label ID="lblDate" runat="server" Text="Date filter on: " CssClass="FieldName"></asp:Label>
                    </td>
                    <td align="left" width="10%">
                        <asp:DropDownList ID="ddlDateFilter" Style="vertical-align: middle" runat="server"
                            CssClass="cmbField">
                            <asp:ListItem Text="Start Date" Value="StartDate" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="End Date" Value="EndDate"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        <asp:Label ID="lblFromDate" Text="From:" runat="server" CssClass="FieldName">
                        </asp:Label>
                    </td>
                    <td align="left" width="10%">
                        <%--<asp:TextBox ID="txtFrom" runat="server" CssClass="txtField"></asp:TextBox>
         <span id="SpanFromDate" class="spnRequiredField">*</span>
         <ajaxToolkit:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy" Enabled="True" PopupPosition="TopRight">
         </ajaxToolkit:CalendarExtender>
         <ajaxToolkit:TextBoxWatermarkExtender ID="txtFrom_TextBoxWatermarkExtender" runat="server" TargetControlID="txtFrom" WatermarkText="dd/mm/yyyy" Enabled="True">
         </ajaxToolkit:TextBoxWatermarkExtender>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" ControlToValidate="txtFrom" CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" runat="server" ValidationGroup="btnGo">
          </asp:RequiredFieldValidator>--%>
                        <telerik:RadDatePicker ID="txtFrom" CssClass="txtField" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                            <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                    </td>
                    <td align="right" width="10%">
                        <asp:Label ID="lblToDate" runat="server" CssClass="FieldName" Text="To:"></asp:Label>
                    </td>
                    <td align="left" width="10%" valign="middle" colspan="2">
                        <%--<asp:TextBox ID="txtTo" runat="server" CssClass="txtField"></asp:TextBox>
          <span id="SpanToDate" class="spnRequiredField">*</span>
              <ajaxToolkit:CalendarExtender ID="txtTo_CalendarExtender" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy" Enabled="True" PopupPosition="TopRight">
               </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:TextBoxWatermarkExtender ID="txtTo_TextBoxWatermarkExtender" runat="server" TargetControlID="txtTo" WatermarkText="dd/mm/yyyy" Enabled="True">
                </ajaxToolkit:TextBoxWatermarkExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtTo" CssClass="rfvPCG" ErrorMessage="<br />Please select a To Date" Display="Dynamic"
                runat="server" ValidationGroup="btnGo">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="To Date should not be less than From Date" Type="Date" ControlToValidate="txtTo" ControlToCompare="txtFrom" Operator="GreaterThanEqual"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnGo">
               </asp:CompareValidator>--%>
                        <telerik:RadDatePicker ID="txtTo" CssClass="txtTo" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                            <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:Button ID="btnGo" runat="server" Text="Go" ValidationGroup="btnGo" CssClass="PCGButton"
                            OnClick="btnGo_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<div style="width: 99%">
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" EnableTheming="True" Skin="Telerik"
        EnableEmbeddedSkins="False" MultiPageID="SystameticMISMultiPage" SelectedIndex="1">
        <Tabs>
            <telerik:RadTab runat="server" Text="Systematic Setup" Value="Systematic Setup View"
                TabIndex="0" Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Systematic Payment Projection" Value="Calender Summary View"
                TabIndex="1">
            </telerik:RadTab>
            <%--<telerik:RadTab runat="server" Text="Calendar Detail View" 
            Value="Calendar Detail View" TabIndex="1" Selected="True">
        </telerik:RadTab>--%>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="SystameticMISMultiPage" EnableViewState="true" runat="server"
        SelectedIndex="0" Width="100%">
        <telerik:RadPageView ID="RadPageView1" runat="server" Style="margin-top: 20px">
            <asp:Panel ID="pnlSystameticSetupView" runat="server" Width="100%">
                <asp:ImageButton ID="btnExportSystematicMIS" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportSystematicMIS_OnClick"
                    Height="25px" Width="25px"></asp:ImageButton>
                <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" EnableHistory="True"
                    HorizontalAlign="NotSet" LoadingPanelID="PorspectListLoading">
                    <telerik:RadGrid ID="gvSystematicMIS" AllowSorting="true" runat="server" AllowAutomaticInserts="false"
                        AllowFilteringByColumn="true" AllowPaging="True" AutoGenerateColumns="False"
                        EnableEmbeddedSkins="false" GridLines="None" PageSize="15" ShowFooter="true"
                        ShowStatusBar="True" Skin="Telerik">
                        <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                            FileName="SystematicMIS Details" Excel-Format="ExcelML">
                        </ExportSettings>
                        <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridBoundColumn DataField="CustomerName" HeaderText="Customer" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="true" UniqueName="CustomerName" FooterText="Grand Total:"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SystematicTransactionType" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="true" HeaderText="Type" UniqueName="SystematicTransactionType">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <%-- <telerik:GridBoundColumn DataField="AMCname" HeaderText="AMC" 
                                       UniqueName="AMCname">
                                       <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                   </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn DataField="SchemePlaneName" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                    HeaderText="Scheme" UniqueName="SchemePlaneName">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="160px" Wrap="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FolioNumber" HeaderText="Folio" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="true" UniqueName="FolioNumber">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn SortExpression="StartDate" DataField="StartDate" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="true" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Start Date"
                                    UniqueName="StartDate">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                                     <FilterTemplate>
                                <telerik:RadDatePicker ID="StartDateFilter" AutoPostBack="true" runat="server">
                                </telerik:RadDatePicker>
                            </FilterTemplate>
                                </telerik:GridDateTimeColumn>
                                <telerik:GridDateTimeColumn DataField="EndDate" DataFormatString="{0:dd/MM/yyyy}"
                                    ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="End Date" UniqueName="EndDate"
                                    SortExpression="EndDate">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                                     <FilterTemplate>
                                <telerik:RadDatePicker ID="EndDateFilter" AutoPostBack="true" runat="server">
                                </telerik:RadDatePicker>
                            </FilterTemplate>
                                </telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn DataField="Frequency" HeaderText="Frequency" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="true" UniqueName="Frequency">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn DataField="NextSystematicDate" HeaderText="Next Date"
                                    ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:dd/MM/yyyy}"
                                    UniqueName="NextSystematicDate" SortExpression="NextSystematicDate">
                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn Aggregate="Sum" AllowFiltering="false" DataField="Amount"
                                    DataType="System.Decimal" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                    HeaderText="Amount" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"
                                    UniqueName="Amount">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn DataField="CeaseDate" DataFormatString="{0:d}" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="true" HeaderText="CEASE DATE" UniqueName="CeaseDate" SortExpression="CeaseDate">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                                    <FilterTemplate>
                                        <telerik:RadDatePicker ID="CeaseDateFilter" AutoPostBack="true" runat="server">
                                        </telerik:RadDatePicker>
                                    </FilterTemplate>
                                </telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn DataField="REMARKS" HeaderText="REMARKS" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="true" UniqueName="REMARKS">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Scrolling AllowScroll="false" UseStaticHeaders="True" SaveScrollPosition="true"
                                FrozenColumnsCount="1"></Scrolling>
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
            </asp:Panel>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView2" runat="server">
            <asp:Panel ID="pnlCalenderSummaryView" runat="server" Width="100%">
                <table id="tblNote" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblNote" runat="server" Text="Note: The view displays the expected monthly order flow for the individual schemes displayed on the systematic set up tab."
                                Font-Size="Small" CssClass="cmbField"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:ImageButton ID="btnExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportSummary_OnClick"
                    Height="25px" Width="25px"></asp:ImageButton>
                <telerik:RadGrid ID="reptCalenderSummaryView" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="false"
                    AllowAutomaticInserts="false" OnItemDataBound="reptCalenderSummaryView_ItemDataBound"
                    OnPreRender="reptCalenderSummaryView_PreRender">
                    <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                        FileName="CalenderSummary Details" Excel-Format="ExcelML">
                    </ExportSettings>
                    <MasterTableView Width="100%" ExpandCollapseColumn-ButtonType="ImageButton">
                        <Columns>
                            <telerik:GridBoundColumn DataField="Year" HeaderText="Year" UniqueName="Year" FooterText="Grand Total: ">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FinalMonth" HeaderText="Month" UniqueName="FinalMonth">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridTemplateColumn HeaderText="SIP Amount" UniqueName="SIPAmount"  ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSIPAmount" Text='<%# Eval("SIPAmount")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label runat="server" ID="lblSIPAmountFooter" Text=""></asp:Label>
                    </FooterTemplate>
                </telerik:GridTemplateColumn>--%>
                            <telerik:GridBoundColumn Aggregate="sum" DataField="SIPAmount" DataType="System.Decimal"
                                HeaderText="SIP Amount" UniqueName="SIPAmount" FooterText="" FooterStyle-HorizontalAlign="Right"
                                DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Aggregate="Sum" DataField="NoOfSIP" HeaderText="No. of SIPs"
                                DataFormatString="{0:N0}" UniqueName="SIPAmount" DataType="System.Int16" FooterText=""
                                FooterStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridTemplateColumn HeaderText="No. of Fresh SIPs" UniqueName="NoOfFreshSIP" ItemStyle-HorizontalAlign="Right" >
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblNoOfFreshSIP" Text='<%# Eval("NoOfFreshSIP")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label runat="server" ID="lblNoOfFreshSIPFooter" Text=""></asp:Label>
                    </FooterTemplate>
                </telerik:GridTemplateColumn>--%>
                            <telerik:GridBoundColumn Aggregate="sum" DataField="NoOfFreshSIP" DataType="System.Int16"
                                HeaderText="No. of Fresh SIPs" UniqueName="SIPAmount" FooterText="" FooterStyle-HorizontalAlign="Right"
                                DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridTemplateColumn HeaderText="SWP Amount" UniqueName="NoOfFreshSIP" ItemStyle-HorizontalAlign="Right" >
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSWPAmount" Text='<%# Eval("SWPAmount")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label runat="server" ID="lblSWPAmountFooter" Text=""></asp:Label>
                    </FooterTemplate>
                </telerik:GridTemplateColumn>--%>
                            <telerik:GridBoundColumn Aggregate="sum" DataField="SWPAmount" DataType="System.Decimal"
                                HeaderText="SWP Amount" UniqueName="SIPAmount" FooterText="" FooterStyle-HorizontalAlign="Right"
                                DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <%-- <telerik:GridTemplateColumn HeaderText="No. of SWPs" UniqueName="NoOfSWP" ItemStyle-HorizontalAlign="Right" >
                    <ItemTemplate >
                        <asp:Label runat="server" ID="lblNoOfSWP" Text='<%# Eval("NoOfSWP")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label runat="server" ID="lblNoOfSWPFooter" Text=""></asp:Label>
                    </FooterTemplate>
                </telerik:GridTemplateColumn>--%>
                            <telerik:GridBoundColumn Aggregate="sum" DataField="NoOfSWP" DataType="System.Int16"
                                HeaderText="No. of SWPs" UniqueName="NoOfSWP" FooterText="" FooterStyle-HorizontalAlign="Right"
                                DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridTemplateColumn HeaderText="No. of fresh SWPs" UniqueName="NoOfFreshSWP" ItemStyle-HorizontalAlign="Right" >
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblNoOfFreshSWP" Text='<%# Eval("NoOfFreshSWP")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label runat="server" ID="lblNoOfFreshSWPFooter" Text=""></asp:Label>
                    </FooterTemplate>
                </telerik:GridTemplateColumn>--%>
                            <telerik:GridBoundColumn Aggregate="sum" DataField="NoOfFreshSWP" DataType="System.Decimal"
                                HeaderText="No. of fresh SWPs" UniqueName="NoOfFreshSWP" FooterText="" FooterStyle-HorizontalAlign="Right"
                                DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="True"></Resizing>
                    </ClientSettings>
                </telerik:RadGrid>
            </asp:Panel>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</div>
<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
    visible="false">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
            </div>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnCustomerId" runat="server" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnadviserId" runat="server" />
<asp:HiddenField ID="hdnAll" runat="server" />
<asp:HiddenField ID="hdnbranchId" runat="server" />
<asp:HiddenField ID="hdnbranchheadId" runat="server" />
<asp:HiddenField ID="hdnrmId" runat="server" />
<asp:HiddenField ID="hdnstartdate" runat="server" />
<asp:HiddenField ID="hdnendDate" runat="server" />
<asp:HiddenField ID="hdnamcCode" runat="server" />
<asp:HiddenField ID="hdnCategory" runat="server" />
<asp:HiddenField ID="hdnschemeCade" runat="server" />
<asp:HiddenField ID="hdnSystematicType" runat="server" />
<asp:HiddenField ID="hdnTodate" runat="server" />
<asp:HiddenField ID="hdnFromDate" runat="server" />
<asp:HiddenField ID="hdnIndividualOrGroup" runat="server" />
