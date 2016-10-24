<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioSystematicView.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioSystematicView" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table style="width: 100%;">
    <tr>
        <td colspan="3">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblView" runat="server" Text="View Systematic Schemes"></asp:Label>
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton ID="imgBtnrgHoldings" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Visible="false" Height="20px" Width="20px">
                            </asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
        <%--<td class="HeaderCell" colspan="2">
            <asp:Label ID="lblView" runat="server" CssClass="HeaderTextBig" Text="View Systematic Schemes"></asp:Label>
            <hr />
        </td>--%>
    </tr>
    <tr>
        <td>
            <table width="10%" cellpadding="0">
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblPortfolio" runat="server" Text="Portfolio:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlportfolio" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlportfolio_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
        <td>
        </td>
    </tr>
</table>
<br />
    <telerik:RadGrid ID="gvrSystematicSchemes" Visible="false" runat="server" CssClass="RadGrid" GridLines="None"
        Width="100%" AllowPaging="True" PageSize="15" AllowSorting="True" AutoGenerateColumns="false"
        ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
        ShowFooter="true" AllowAutomaticUpdates="false" Skin="Telerik" OnNeedDataSource="gvMFFolio_NeedDataSource"
        EnableEmbeddedSkins="false" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true"
        AllowFilteringByColumn="true">
        <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="ExistMFInvestlist">
        </ExportSettings>
        <MasterTableView DataKeyNames="SystematicSetupId">
            <Columns>
                <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action"
                    HeaderStyle-Width="143px">
                    <ItemTemplate>
                        <telerik:RadComboBox ID="ddlMenu" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged"
                            CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" Skin="Telerik"
                            AllowCustomText="true" Width="120px" AutoPostBack="true">
                            <Items>
                                <telerik:RadComboBoxItem ImageUrl="~/Images/Select.png" Text="Select" Value="Select"
                                    Selected="true"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Text="View" Value="View" ImageUrl="~/Images/DetailedView.png"
                                    runat="server"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem ImageUrl="~/Images/RecordEdit.png" Text="Edit" Value="Edit"
                                    runat="server"></telerik:RadComboBoxItem>
                            </Items>
                        </telerik:RadComboBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="Scheme Name" HeaderStyle-Width="250px" DataField="Scheme Name"
                    UniqueName="Scheme Name" SortExpression="Scheme Name" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterText="Grand Total :">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Folio" DataField="Folio" UniqueName="Folio"
                    HeaderStyle-Width="100px" SortExpression="Folio" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Systematic Transaction Type" HeaderStyle-Width="200px"
                    DataField="Systematic Transaction Type" UniqueName="Systematic Transaction Type"
                    SortExpression="Systematic Transaction Type" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Start Date (dd/mm/yyyy)" DataField="Start Date"
                    UniqueName="Start Date" SortExpression="Start Date" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="End Date (dd/mm/yyyy)" DataField="End Date"
                    UniqueName="End Date" SortExpression="End Date" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
             
                <telerik:GridBoundColumn HeaderText="HistoricalCreated" DataField="HistoricalCreated"
                    UniqueName="HistoricalCreated" SortExpression="HistoricalCreated" HeaderStyle-Width="90px"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn HeaderText="AutoTransaction" DataField="AutoTransaction"
                    UniqueName="AutoTransaction" SortExpression="AutoTransaction" HeaderStyle-Width="90px"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
               
                <telerik:GridBoundColumn HeaderText="Amount (Rs)" DataField="Amount" UniqueName="Amount"
                    DataFormatString="{0:N0}" Aggregate="Sum" SortExpression="Amount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Frequency" DataField="Frequency" UniqueName="Frequency"
                    SortExpression="Frequency" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="SubBrokerCode" DataField="SubBrokerCode" UniqueName="SubBrokerCode"
                    SortExpression="SubBrokerCode" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings ReorderColumnsOnClient="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
            <Scrolling AllowScroll="false" />
            <Resizing AllowColumnResize="true" />
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
    </telerik:RadGrid>
    <asp:HiddenField ID="hdnIsMainPortfolio" runat="server"/>
<asp:HiddenField ID="hdnIsCustomerLogin" runat="server" />

