<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerMutualFundPortfolioNPView.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.CustomerMutualFundPortfolioNPView" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<script type="text/javascript" language="javascript">
    function HideGrid() 
    {

    }
  
</script>



<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td style="width: 100%;">
                    <div class="divPageHeading">
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td align="left" style="width: 33%">
                                    Mutual Fund Net Position
                                </td>
                                <td style="width: 34%">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                        <ProgressTemplate>
                                            <asp:Image ID="imgProgress" ImageUrl="~/Images/ajax-loader.gif" AlternateText="Processing"
                                                runat="server" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                                <td align="right" style="width: 33%">
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <table style="width: 100%" class="TableBackground">
            <tr>
                <td class="leftField" style="width: 10%">
                    <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
                </td>
                <td class="rightField" style="width: 15%">
                    <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="leftField" style="width: 10%">
                    <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Type:"></asp:Label>
                </td>
                 <td class="rightField" style="width: 15%">
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField" onChange="HideGrid()">
                     <asp:ListItem Text="Holdings" Value="0"  Selected="True"></asp:ListItem>
                        <asp:ListItem Text="All" Value="1" ></asp:ListItem>
                         <asp:ListItem Text="Realized" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="rightField" style="width: 15%">
                    <asp:Button ID="btnGo" runat="server" Text="GO" CssClass="PCGButton" OnClick="btnGo_Click" />
                </td>
                <td class="leftField" style="width: 10%" visible="false">
                    <asp:Label ID="lblDisplayType" runat="server" Visible="false" CssClass="FieldName"
                        Text="Display type:"></asp:Label>
                </td>
                <td class="rightField" style="width: 15%">
                    <asp:DropDownList ID="ddlDisplayType" Visible="false" runat="server" CssClass="cmbField">
                        <asp:ListItem Text="Returns" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Tax" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="rightField" style="width: 10%">
                    <asp:Label ID="lblPickDate" Text="" runat="server" CssClass="FieldName" Visible="false"> </asp:Label>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <table style="width: 100%" class="TableBackground">
            <tr id="trNoRecords" runat="server">
                <td align="center">
                    <div id="divNoRecords" runat="server" class="failure-msg">
                        <asp:Label ID="lblNoRecords" Text="No Records found" runat="server"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr id="trContent" runat="server">
                <td style="width: 100%">
                <div id="div1" runat="server" style="height:288px; overflow:scroll;" >
                                    <table id="tblHoldings" runat="server" width="100%">
                                        <tr>
                                            <td>
                                                <div id="dvHoldings" runat="server" style="width: 640px;">
                                                    <asp:Label ID="lblMessageHoldings" Visible="false" Text="No Record Exists" runat="server"
                                                        CssClass="Field"></asp:Label>
                                                    <asp:LinkButton ID="lnlGoBackHoldings" runat="server" OnClick="lnlGoBackHoldings_Click"
                                                        Visible="false" CssClass="FieldName">Go Back</asp:LinkButton>
                                                    <asp:ImageButton Visible="false" ID="imgBtnrgHoldings" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportrgHoldingsFilteredData_OnClick"
                                                        Height="25px" Width="25px"></asp:ImageButton>
                                                    <telerik:RadGrid ID="rgHoldings" runat="server" Width="1500px" PageSize="10" AllowAutomaticDeletes="false"
                                                        ShowFooter="true" AllowAutomaticInserts="false" AllowAutomaticUpdates="false"
                                                        HorizontalAlign="NotSet" AllowPaging="true" GridLines="None" AutoGenerateColumns="false"
                                                        Style="border: 0; outline: none;" Skin="Telerik" EnableEmbeddedSkins="false"
                                                        OnExcelMLExportStylesCreated="rgHoldings_OnExcelMLExportStylesCreated" OnExcelMLExportRowCreated="rgHoldings_OnExcelMLExportRowCreated"
                                                        ShowStatusBar="false" OnItemCommand="rgHoldings_ItemCommand" AllowSorting="true"
                                                        OnItemDataBound="rgHoldings_ItemDataBound" EnableViewState="true" OnNeedDataSource="rgHoldings_OnNeedDataSource"
                                                        AllowFilteringByColumn="true">
                                                        <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                        <MasterTableView DataKeyNames="" ShowFooter="true" AutoGenerateColumns="false" CommandItemDisplay="None">
                                                            <Columns>
                                                                <telerik:GridButtonColumn HeaderStyle-Width="50px" Visible="false" ButtonType="LinkButton"
                                                                    Text="Select" CommandName="Select">
                                                                </telerik:GridButtonColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" SortExpression="Category" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="Category"
                                                                    HeaderText="Category" DataField="Category" FooterText="Grand Total:" FooterStyle-HorizontalAlign="Right"
                                                                    AllowFiltering="true">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" SortExpression="SubCategory" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="SubCategory"
                                                                    HeaderText="SubCategory" DataField="SubCategory" AllowFiltering="true">
                                                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="350px" SortExpression="ScripName" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="ScripName"
                                                                    HeaderText="Scheme" DataField="ScripName">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="103px" SortExpression="FolioNum" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="FolioNum"
                                                                    HeaderText="Folio" DataField="FolioNum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" UniqueName="InvStartDate"
                                                                    AllowFiltering="false" HeaderText="Scheme Invst. Date" DataField="InvStartDate"
                                                                    DataFormatString="{0:d}" HtmlEncode="False" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                                    SortExpression="InvStartDate">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                    <FilterTemplate>
                                                                        <telerik:RadDatePicker ID="resolveDateFilter" runat="server">
                                                                        </telerik:RadDatePicker>
                                                                    </FilterTemplate>
                                                                </telerik:GridDateTimeColumn>
                                                                <telerik:GridDateTimeColumn HeaderStyle-Width="93px" DataField="InvStartDate" SortExpression="InvStartDate"
                                                                    AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" AllowFiltering="false"
                                                                    HeaderText="Holding Start Date" UniqueName="InvStartDate" DataFormatString="{0:d}">
                                                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                                                    <FilterTemplate>
                                                                        <telerik:RadDatePicker ID="InvestmentStartDateFilter" runat="server">
                                                                        </telerik:RadDatePicker>
                                                                    </FilterTemplate>
                                                                </telerik:GridDateTimeColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" UniqueName="PurUnits" HeaderText="Purchased Units"
                                                                    DataField="PurUnits" AllowFiltering="false" DataFormatString="{0:N3}" FooterStyle-HorizontalAlign="Right"
                                                                    Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="77px" UniqueName="DVRUnits" HeaderText="DVR Units"
                                                                    DataField="DVRUnits" AllowFiltering="false" FooterStyle-HorizontalAlign="Right"
                                                                    Aggregate="Sum" DataFormatString="{0:N3}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="120px" UniqueName="TotalUnits" HeaderText="Total Units"
                                                                    Aggregate="Sum" DataField="TotalUnits" AllowFiltering="false" DataFormatString="{0:N3}"
                                                                    FooterStyle-HorizontalAlign="Right">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="77px" UniqueName="DVR" Aggregate="Sum"
                                                                    HeaderText="DVR Amount" DataField="DVR" AllowFiltering="false" FooterStyle-HorizontalAlign="Right"
                                                                    DataFormatString="{0:N0}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="120px" UniqueName="InvestedCost" HeaderText="Invested Cost"
                                                                    Aggregate="Sum" DataField="InvestedCost" AllowFiltering="false" DataFormatString="{0:N0}"
                                                                    FooterStyle-HorizontalAlign="Right">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="86px" UniqueName="CurrentNAV" HeaderText="CurrentNAV"
                                                                    DataField="CurrentNAV" AllowFiltering="false" DataFormatString="{0:N4}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="86px" UniqueName="CurrentNAVDate " HeaderText="NAV Date"
                                                                    DataField="CurrentNAVDate" AllowFiltering="false" DataFormatString="{0:d}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="88px" UniqueName="CurrentValue" HeaderText="Market Value"
                                                                    Aggregate="Sum" DataField="CurrentValue" AllowFiltering="false" DataFormatString="{0:N0}"
                                                                    DataType="System.Double" FooterStyle-HorizontalAlign="Right" FooterText="">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="40px" UniqueName="DVP" HeaderText="DVP"
                                                                    Aggregate="Sum" DataField="DVP" AllowFiltering="false" DataFormatString="{0:N0}"
                                                                    FooterStyle-HorizontalAlign="Right">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="77px" UniqueName="TotalPL" HeaderText="Total P/L"
                                                                    DataField="PL" AllowFiltering="false" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right"
                                                                    Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="79px" UniqueName="ABS" HeaderText="Absolute Return (%)"
                                                                    DataField="ABS" AllowFiltering="false" DataFormatString="{0:N2}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="54px" UniqueName="XIRR" HeaderText="XIRR (%)"
                                                                    DataField="XIRR" AllowFiltering="false" DataFormatString="{0:N2}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn SortExpression="AvgPrice" HeaderStyle-Width="77px" UniqueName="AvgPrice"
                                                                    HeaderText="Weighted NAV" DataField="AvgPrice" AllowFiltering="false" DataFormatString="{0:N4}"
                                                                    FooterStyle-HorizontalAlign="Right">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="CurrentNAVDate" HeaderText="Valuation As On"
                                                                    DataField="CurrentNAVDate" AllowFiltering="false" FooterStyle-HorizontalAlign="Right">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <HeaderStyle Width="150px" />
                                                        <ClientSettings>
                                                            <Resizing AllowColumnResize="true" />
                                                            <Selecting AllowRowSelect="true" />
                                                        </ClientSettings>
                                                    </telerik:RadGrid>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                          <div id ="div2" runat="server" style="height:288px; overflow:scroll;">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <div id="dvAll" runat="server" style="width: 640px">
                                                    <asp:Label ID="lblMessageAll" Visible="false" Text="No Record Exists" runat="server"
                                                        CssClass="Field"></asp:Label>
                                                    <asp:LinkButton ID="lnkGoBackAll" runat="server" OnClick="lnkGoBackAll_Click" Visible="false"
                                                        CssClass="FieldName">Go Back</asp:LinkButton>
                                                    <asp:ImageButton Visible="false" ID="imgBtnrgAll" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportrgAllFilteredData_OnClick"
                                                        Height="25px" Width="25px"></asp:ImageButton>
                                                    <telerik:RadGrid ID="rgAll" runat="server" Width="1500px" PageSize="10" AllowPaging="True"
                                                        OnExcelMLExportStylesCreated="rgAll_OnExcelMLExportStylesCreated" OnExcelMLExportRowCreated="rgAll_OnExcelMLExportRowCreated"
                                                        GridLines="None" AutoGenerateColumns="true" Style="border: 0; outline: none;"
                                                        Skin="Telerik" EnableEmbeddedSkins="false" 
                                                         AllowSorting="true" EnableViewState="true"
                                                        OnNeedDataSource="rgAll_OnNeedDataSource" AllowFilteringByColumn="true">
                                                        <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                        <MasterTableView DataKeyNames="" ShowFooter="true"
                                                            Width="100%" AutoGenerateColumns="false" CommandItemDisplay="None">
                                                            <Columns>
                                                                <telerik:GridButtonColumn HeaderStyle-Width="50px" Visible="false" ButtonType="LinkButton"
                                                                    Text="Select" CommandName="Select">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridButtonColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" SortExpression="Category" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="Category"
                                                                    HeaderText="Category" DataField="Category" AllowFiltering="true" FooterText="Grand Total:"
                                                                    FooterStyle-HorizontalAlign="Right">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" UniqueName="SubCategory" SortExpression="SubCategory"
                                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                    HeaderText="SubCategory" DataField="SubCategory" AllowFiltering="true">
                                                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="350px" SortExpression="ScripName" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="ScripName" HeaderText="Scheme"
                                                                    DataField="ScripName">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" SortExpression="FolioNum" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="FolioNum"
                                                                    HeaderText="Folio" DataField="FolioNum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="InvStartDate"
                                                                    SortExpression="InvStartDate" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                                    AllowFiltering="false" HeaderText="Scheme Invst. Date" UniqueName="InvStartDate"
                                                                    DataFormatString="{0:d}" HtmlEncode="False">
                                                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                                                    <FilterTemplate>
                                                                        <telerik:RadDatePicker ID="resolveDateFilter" runat="server">
                                                                        </telerik:RadDatePicker>
                                                                    </FilterTemplate>
                                                                </telerik:GridDateTimeColumn>
                                                                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="HoldStartDate"
                                                                    SortExpression="HoldStartDate" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                                    AllowFiltering="false" HeaderText="Holding Start Date" UniqueName="HoldStartDate"
                                                                    DataFormatString="{0:d}">
                                                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                                                    <FilterTemplate>
                                                                        <telerik:RadDatePicker ID="InvestmentStartDateFilter" runat="server">
                                                                        </telerik:RadDatePicker>
                                                                    </FilterTemplate>
                                                                </telerik:GridDateTimeColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" UniqueName="PurUnits" HeaderText="Purchased Units"
                                                                    DataField="PurUnits" AllowFiltering="false" DataFormatString="{0:N3}" FooterStyle-HorizontalAlign="Right"
                                                                    Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" UniqueName="DVRUnits" HeaderText="DVR Units"
                                                                    DataField="DVRUnits" AllowFiltering="false" FooterStyle-HorizontalAlign="Right"
                                                                    Aggregate="Sum" DataFormatString="{0:N3}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="120px" UniqueName="TotalUnits" HeaderText="Total Units"
                                                                    DataField="TotalUnits" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N3}"
                                                                    Aggregate="Sum" AllowFiltering="false">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="120px" UniqueName="InvestedCost" HeaderText="Invested Cost"
                                                                    DataField="InvestedCost" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}"
                                                                    Aggregate="Sum" AllowFiltering="false">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" UniqueName="CurrentNAV" HeaderText="NAV"
                                                                    DataField="CurrentNAV" FooterStyle-HorizontalAlign="Right" AllowFiltering="false" DataFormatString="{0:N4}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="86px" UniqueName="CurrentNAVDate " HeaderText="NAV Date"
                                                                    DataField="CurrentNAVDate" AllowFiltering="false" DataFormatString="{0:d}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="CurrentValue" HeaderText="Current Value" DataField="CurrentValue"
                                                                    FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" AllowFiltering="false"
                                                                    Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" UniqueName="SoldUnits" HeaderText="Units Sold"
                                                                    DataField="SoldUnits" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"
                                                                    Aggregate="Sum" AllowFiltering="false">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" UniqueName="RedeemedAmt" HeaderText="Redeemed Amount"
                                                                    DataField="RedeemedAmt" FooterStyle-HorizontalAlign="Right" AllowFiltering="false"
                                                                    Aggregate="Sum" DataFormatString="{0:N0}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="46px" UniqueName="DVP" HeaderText="DVP"
                                                                    DataField="DVP" AllowFiltering="false" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right"
                                                                    Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="65px" UniqueName="TotalPL" HeaderText="Total P/L"
                                                                    DataField="TotalPL" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right"
                                                                    Aggregate="Sum" AllowFiltering="false">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="59px" UniqueName="ABS" HeaderText="Absolute Return (%)"
                                                                    DataField="ABS" AllowFiltering="false" DataFormatString="{0:N2}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="59px" UniqueName="DVR" HeaderText="DVR"
                                                                    DataField="DVR" DataFormatString="{0:N0}" AllowFiltering="false" FooterStyle-HorizontalAlign="Right"
                                                                    Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="63px" UniqueName="XIRR" HeaderText="XIRR (%)"
                                                                    DataField="XIRR" AllowFiltering="false" DataFormatString="{0:N2}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="87px" UniqueName="TotalDiv" HeaderText="Total Dividends"
                                                                    DataField="TotalDiv" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}"
                                                                    Aggregate="Sum" AllowFiltering="false">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="CurrentNAVDate" HeaderText="Valuation As On"
                                                                    DataField="CurrentNAVDate" AllowFiltering="false" FooterStyle-HorizontalAlign="Right">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <HeaderStyle Width="150px" />
                                                        <ClientSettings>
                                                            <Resizing AllowColumnResize="true" />
                                                            <Selecting AllowRowSelect="true" />
                                                        </ClientSettings>
                                                    </telerik:RadGrid>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    </div>
                                    <div id ="div4" runat="server" style="height:288px; overflow:scroll;">
                                    <table id="tblRealized" runat="server" width="99%">
                                        <tr>
                                            <td>
                                                <div id="dvRealized" runat="server" style="width: 640px">
                                                    <asp:Label ID="lblMessageRealized" Visible="false" Text="No Record Exists" runat="server"
                                                        CssClass="Field"></asp:Label>
                                                    <asp:LinkButton ID="lnkGoBackRealized" runat="server" OnClick="lnkGoBackRealized_Click"
                                                        Visible="false" CssClass="FieldName">Go Back</asp:LinkButton>
                                                    <asp:ImageButton Visible="false" ID="imgBtnrgRealized" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportrgRealizedFilteredData_OnClick"
                                                        Height="25px" Width="25px"></asp:ImageButton>
                                                    <telerik:RadGrid ID="rgRealized" runat="server" Width="1500px" PageSize="10" AllowPaging="True"
                                                        AllowSorting="true" GridLines="None" AutoGenerateColumns="true" Style="border: 0;
                                                        outline: none;" OnExcelMLExportStylesCreated="rgRealized_OnExcelMLExportStylesCreated"
                                                        OnExcelMLExportRowCreated="rgRealized_OnExcelMLExportRowCreated" Skin="Telerik"
                                                        EnableEmbeddedSkins="false" OnItemCommand="rgRealized_ItemCommand" OnItemDataBound="rgRealized_ItemDataBound"
                                                        EnableViewState="true" OnNeedDataSource="rgRealized_OnNeedDataSource" AllowFilteringByColumn="true">
                                                        <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                        <MasterTableView DataKeyNames="" ShowFooter="true"
                                                            Width="100%" AutoGenerateColumns="false" CommandItemDisplay="None">
                                                            <Columns>
                                                                <telerik:GridButtonColumn HeaderStyle-Width="50px" Visible="false" ButtonType="LinkButton" Text="Select"
                                                                    CommandName="Select">
                                                                </telerik:GridButtonColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" SortExpression="Category" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="Category"
                                                                    HeaderText="Category" DataField="Category" AllowFiltering="true" FooterText="Grand Total:"
                                                                    FooterStyle-HorizontalAlign="Right">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" SortExpression="SubCategory" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="SubCategory"
                                                                    HeaderText="SubCategory" DataField="SubCategory" AllowFiltering="true">
                                                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                                </telerik:GridBoundColumn>
                                                              
                                                                <telerik:GridBoundColumn HeaderStyle-Width="350px" SortExpression="ScripName" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="ScripName" HeaderText="Scheme"
                                                                    DataField="ScripName">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" SortExpression="FolioNum" AutoPostBackOnFilter="true"
                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="FolioNum"
                                                                    HeaderText="Folio" DataField="FolioNum">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridBoundColumn>
                                                               
                                                                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="InvStartDate"
                                                                    SortExpression="InvStartDate" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                                    AllowFiltering="false" HeaderText="Scheme Invst. Date" UniqueName="InvStartDate"
                                                                    DataFormatString="{0:d}">
                                                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                                                    <FilterTemplate>
                                                                        <telerik:RadDatePicker ID="resolveDateFilter" runat="server">
                                                                        </telerik:RadDatePicker>
                                                                    </FilterTemplate>
                                                                </telerik:GridDateTimeColumn>
                                                                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="HoldStartDate"
                                                                    SortExpression="HoldStartDate" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                                    AllowFiltering="false" HeaderText="Holding Start Date" UniqueName="HoldStartDate"
                                                                    DataFormatString="{0:d}">
                                                                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                                                                    <FilterTemplate>
                                                                        <telerik:RadDatePicker ID="InvestmentStartDateFilter" runat="server">
                                                                        </telerik:RadDatePicker>
                                                                    </FilterTemplate>
                                                                </telerik:GridDateTimeColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="120px" UniqueName="InvestedCost" HeaderText="Invested Cost"
                                                                    DataField="InvestedCost" AllowFiltering="false" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right"
                                                                    Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" UniqueName="SoldUnits" HeaderText="Units Sold"
                                                                    DataField="SoldUnits" AllowFiltering="false" DataFormatString="{0:N3}" FooterStyle-HorizontalAlign="Right"
                                                                    Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" UniqueName="RedeemedAmt" HeaderText="Redeemed Amount"
                                                                    DataField="RedeemedAmt" AllowFiltering="false" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right"
                                                                    Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="50px" UniqueName="DVP" HeaderText="DVP"
                                                                    DataField="DVP" AllowFiltering="false" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right"
                                                                    Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="90px" UniqueName="TotalDiv" HeaderText="Total Dividends"
                                                                    DataField="TotalDiv" AllowFiltering="false" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right"
                                                                    Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="81px" UniqueName="TotalPL" HeaderText="Total P/L"
                                                                    DataField="TotalPL" AllowFiltering="false" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right"
                                                                    Aggregate="Sum">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="63px" UniqueName="ABS" HeaderText="Absolute Return (%)"
                                                                    DataField="ABS" AllowFiltering="false" DataFormatString="{0:N2}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="63px" UniqueName="XIRR" HeaderText="XIRR (%)"
                                                                    DataField="XIRR" AllowFiltering="false" DataFormatString="{0:N2}">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn UniqueName="CurrentNAVDate" HeaderText="Valuation As On"
                                                                    DataField="CurrentNAVDate" AllowFiltering="false" FooterStyle-HorizontalAlign="Right">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <HeaderStyle Width="150px" />
                                                        <ClientSettings>
                                                            <Resizing AllowColumnResize="true" />
                                                            <Selecting AllowRowSelect="true" />
                                                        </ClientSettings>
                                                    </telerik:RadGrid>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                              </div>
                </td>
            </tr>
          
        </table>
        <table>
            <tr id="trNote" runat="server" visible="false">
                <td>
                    <div id="Div3" class="Note">
                        <p>
                            <span style="font-weight: bold">Note:</span><br />
                            Schemes with negative balances are highlighted in red to correct the unit balance
                            please check if all the transactions and corporate actions are uploaded
                        </p>
                    </div>
                </td>
            </tr>
        </table>
        <table id="ErrorMessage" align="center" runat="server">
            <tr>
                <td>
                    <div class="failure-msg" align="center">
                        No Records found.....
                    </div>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdnReturnsRealizedCategory" runat="server" />
        <asp:HiddenField ID="hdnReturnsHoldingsCategory" runat="server" />
        <asp:HiddenField ID="hdnReturnsAllCategory" runat="server" />
        <asp:HiddenField ID="hdnTaxHoldingsCategory" runat="server" />
        <asp:HiddenField ID="hdnTaxRealizedCategory" runat="server" />
    </ContentTemplate>
    <Triggers>
        <%--<asp:PostBackTrigger ControlID="imgBtnrgTaxRealized" />--%>
       <%-- <asp:PostBackTrigger ControlID="imgBtnrgTaxHoldings" />
        <asp:PostBackTrigger ControlID="imgBtnrgRealized" />
        <asp:PostBackTrigger ControlID="imgBtnrgAll" />
        <asp:PostBackTrigger ControlID="imgBtnrgHoldings" />--%>
        <%--<asp:PostBackTrigger ControlID="imgBtnrgHoldings" />--%>
    </Triggers>
</asp:UpdatePanel>
