<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewEquityPortfolios.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.ViewEquityPortfolios" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<script type="text/javascript" language="javascript">
    (function() { 
        var x = window.x = {};

        onRequestStart = function(sender, args) {
            if (args.get_eventTarget().indexOf("Button") >= 0) {
                args.set_enableAjax(false);
            }
        }
    })();

</script>

<%--    <script type="text/javascript">
        function GridCreated(sender, args) {
            var scrollArea = sender.GridDataDiv;
            alert(scrollArea);
            var dataHeight = sender.get_masterTableView().get_element().clientHeight;
            alert(dataHeight);
            if (dataHeight < 350) {
                scrollArea.style.height = dataHeight + 17 + "px";
            }
        }
</script>--%>
<style type="text/css">
    .ImageButtons
    {
        box-shadow: 5px 5px 5px #888888;
    }
    .SetPlace
    {
        padding-left: 5px;
    }
    .font
    {
        font-family: Calibri;
        font-size: 12px;
    }
    .Newfont
    {
        font-family: Calibri;
        font-size: 12px;
    }
    .cmbField option
    {
        margin: 3px;
        padding: 4px 6px;
        text-shadow: none;
        background: #f2f2f2;
        border-radius: 3px;
        cursor: pointer;
    }
    .Height
    {
        height: inherit;
    }
</style>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Equity Net Position
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="60%">
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="leftField">                                      
            <asp:Label ID="lblNPType" Text="Select Type:" CssClass="FieldName" runat="server" />
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlDateRange" runat="server" OnSelectedIndexChanged="ddlDateRange_OnCheckedChanged"
                AutoPostBack="true" CssClass="cmbField">
                <asp:ListItem Text="As On" Value="ASON"></asp:ListItem>
               <%-- <asp:ListItem Text=""  Value="DateRange"></asp:ListItem>--%>
            </asp:DropDownList>
        </td>
        <td class="leftField">
            <asp:Label ID="LblType" Text="Currency:" CssClass="FieldName" runat="server" />
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddl_type" runat="server" Width="150px" CssClass="cmbField">
                <asp:ListItem Text="INR" Value="INR">INR</asp:ListItem>
                <asp:ListItem Text="Dollar" Value="$">USD</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblDate" runat="server" CssClass="FieldName">As On:</asp:Label>
        </td>
        <td class="rightField">
            <telerik:RadDatePicker ID="txtPickDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput1" EmptyMessage="dd/mm/yyyy" runat="server" DisplayDateFormat="d/M/yyyy"
                    DateFormat="d/M/yyyy">
                </DateInput>
                 
            </telerik:RadDatePicker>
         
                     <span id ="spn" class="spnRequiredField">*</span> 
                <asp:RequiredFieldValidator ID ="RequiredFieldValidator1"  runat="server" CssClass="cvPCG" Display="Dynamic" ValidationGroup="vgBtnGo"  ControlToValidate="txtPickDate"  ErrorMessage="<br />Please select  Date">
                </asp:RequiredFieldValidator>
              
        </td>
        <td class="leftField" id="tdlblDate" runat="server" visible="false">
            <asp:Label ID="lblToDate" runat="server" CssClass="FieldName">To:</asp:Label>
        </td>
        <td class="rightField" id="tdToDate" visible="false" runat="server">
            <telerik:RadDatePicker ID="radToDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput2" EmptyMessage="dd/mm/yyyy" runat="server" DisplayDateFormat="d/M/yyyy"
                    DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
              <span id ="Span1" class="spnRequiredField">*</span> 
                <asp:RequiredFieldValidator ID ="RequiredFieldValidator2"  runat="server" CssClass="cvPCG" Display="Dynamic" ValidationGroup="vgBtnGo"  ControlToValidate="radToDate"  ErrorMessage="<br />Please select  Date">
                </asp:RequiredFieldValidator>
        </td>
        <td class="rightField">
            <asp:Button ID="btnGo" runat="server" Text="GO" CssClass="PCGButton" OnClick="btnGo_Click"
                ValidationGroup="vgBtnGo" />
        </td>
    </tr>
</table>
<br />
<br />
<table width="100%">
    <tr align="center">
        <td>
            <div align="center">
                <asp:Label ID="lblErrorMsg" runat="server" CssClass="failure-msg" Visible="false">
                </asp:Label>
            </div>
        </td>
    </tr>
</table>
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" ClientEvents-OnRequestStart="onRequestStart"
    CssClass="SetPlace" Visible="false">
    <asp:ImageButton ID="ImageButton1" ImageUrl="../Images/ExcelExport_Ico.jpg" Height="30px"
        Width="30px" OnClick="Button2_Click" runat="server" ToolTip="Export To Excel"
        CssClass="ImageButtons" />
    <asp:ImageButton ID="ImageButton2" ImageUrl="../Images/CsvExport_Ico.jpg" Height="30px"
        Width="30px" OnClick="Button2_Click" runat="server" ToolTip="Export To Csv" CssClass="ImageButtons" />
    <br />
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" EnableTheming="True" Skin="Telerik"
        EnableEmbeddedSkins="False" Width="100%" MultiPageID="EQPortfolioTabPages" SelectedIndex="0"
        EnableViewState="true">
        <Tabs>
            <telerik:RadTab runat="server" Text="UnRealized" Value="UnRealized" Visible="false" TabIndex="0">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Realized Delivery" Visible="false" Value="Realized Delivery"
                TabIndex="1">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="All" Value="All" Visible="false" TabIndex="2">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Visible="false" Text="Speculative" Value="Speculative" TabIndex="3">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <br />
    <telerik:RadMultiPage ID="EQPortfolioTabPages" runat="server" EnableViewState="true"
        SelectedIndex="0">
        <%--  ----------------Unrealized Holding-------------------------%>
        <telerik:RadPageView ID="EQPortfolioUnRealizedTabPage" runat="server">
            <asp:Panel ID="pnlEQPortfolioAll" runat="server" Width="100%">
                <table>
                    <tr>
                        <td>
                            <div id="dvEquityPortfolio" runat="server" style="width: 98%; position: absolute">
                                <asp:Label ID="lblMessage" Visible="false" Text="No Record Exists" runat="server"
                                    CssClass="Field"></asp:Label>
                                <telerik:RadGrid ID="gvEquityPortfolio" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                    Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AllowAutomaticInserts="false"
                                    ExportSettings-FileName="Equity Net Position Details" ItemStyle-CssClass="Newfont"
                                    HeaderStyle-CssClass="Newfont" OnNeedDataSource="gvEquityPortfolio_OnNeedDataSource"
                                    OnItemCommand="gvEquityPortfolio_ItemCommand" OnItemDataBound="gvEquityPortfolio_DataBound"
                                    OnDataBound="gvEquityPortfolioRealizedDelivery_OnDataBound">
                                    <ExportSettings HideStructureColumns="true">
                                    </ExportSettings>
                                    <MasterTableView DataKeyNames="ScripCode" AllowMultiColumnSorting="True" Width="100%"
                                        AutoGenerateColumns="false" CommandItemDisplay="None">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderStyle-Width="50px" AllowFiltering="false" UniqueName="action"
                                                DataField="action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Select" HeaderText="Select"
                                                        ShowHeader="True" Text="Select" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="250px" FooterText="Grand Total" HeaderText="Scrip Name" DataField="PEM_CompanyName"
                                                UniqueName="PEM_CompanyName" SortExpression="PEM_CompanyName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-CssClass="Newfont">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="80px" FooterStyle-HorizontalAlign="Right" HeaderText="No of shares"
                                                DataField="NoOfShares" UniqueName="NoOfShares" SortExpression="NoOfShares" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:n0}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            
                                           <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="80px" FooterStyle-HorizontalAlign="Right" HeaderText="Avg. Price  "
                                                DataField="AvgPrice" UniqueName="AvgPriceDup" SortExpression="AvgPrice" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains" Visible ="false"
                                                DataFormatString="{0:N4}">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            
                                            <telerik:GridTemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="80px" FooterStyle-HorizontalAlign="Right" HeaderText="Avg. Price"
                                                DataField="AvgPrice" UniqueName="AvgPrice" SortExpression="AvgPrice" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains" ItemStyle-HorizontalAlign="Right" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkButton" runat="server" Text='<%# Eval("AvgPrice") %>' CommandName="AvgPriceCalculation">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="100px" FooterStyle-HorizontalAlign="Right" HeaderText="Amount Invested "
                                                DataField="AmountInvested" UniqueName="AmountInvested" SortExpression="AmountInvested"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}" Aggregate="Sum" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="100px" FooterStyle-HorizontalAlign="Right" HeaderText="Closing Price  "
                                                DataField="CurrentpPrice" UniqueName="CurrentpPrice" SortExpression="CurrentpPrice"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N4}">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="90px" FooterStyle-HorizontalAlign="Right" HeaderText="Closing Date"
                                                DataField="CurrentPriceDate" UniqueName="CurrentPriceDate" SortExpression="CurrentPriceDate"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:d}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            
                                              <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="100px" FooterStyle-HorizontalAlign="Right" HeaderText="ForEx Closing Price  "
                                                DataField="ForExCurrentpPrice" UniqueName="ForExCurrentpPrice" SortExpression="ForExCurrentpPrice"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N4}">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="90px" FooterStyle-HorizontalAlign="Right" HeaderText="ForEx Closing Date"
                                                DataField="ForExCurrentPriceDate" UniqueName="ForExCurrentPriceDate" SortExpression="ForExCurrentPriceDate"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:d}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="Current Value  "
                                                DataField="CurrentValue" UniqueName="CurrentValue" SortExpression="CurrentValue"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                Aggregate="Sum" DataFormatString="{0:N2}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <%-- <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="Dividend"
                                                DataField="Dividend" UniqueName="Dividend" SortExpression="Dividend" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                Aggregate="Sum" DataFormatString="{0:N2}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>--%>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="P/L"
                                                DataField="UnRealizedPL" UniqueName="UnRealizedPL" SortExpression="UnRealizedPL"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                Aggregate="Sum" DataFormatString="{0:N2}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" ForeColor="Green"
                                                    CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="80px" FooterStyle-HorizontalAlign="Right" HeaderText="P/L %"
                                                DataField="PnlPercent" UniqueName="PnlPercent" SortExpression="PnlPercent" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" ForeColor="Green"
                                                    CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="70px" HeaderText="Holding XIRR (%)"
                                                DataField="XIRRValue" UniqueName="XIRRValue" SortExpression="XIRRValue" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" ForeColor="Green"
                                                    CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <%--<telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="70px" HeaderText="Total XIRR (%)"
                                                DataField="TotalXIRRValue" UniqueName="TotalXIRRValue" SortExpression="TotalXIRRValue"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" ForeColor="Green"
                                                    CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>--%>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="250px" HeaderText="Sector" DataField="PGSC_SectorCategoryName"
                                                UniqueName="PGSC_SectorCategoryName" SortExpression="PGSC_SectorCategoryName"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-CssClass="Newfont">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="250px" HeaderText="Sub Sector" DataField="PGSSC_SectorSubCategoryName"
                                                UniqueName="PGSSC_SectorSubCategoryName" SortExpression="PGSSC_SectorSubCategoryName"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-CssClass="Newfont">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="250px" HeaderText="Sub Sub Sector" DataField="PGSSSC_SectorSubSubCategoryName"
                                                UniqueName="PGSSSC_SectorSubSubCategoryName" SortExpression="PGSSSC_SectorSubSubCategoryName"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-CssClass="Newfont">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                        <Resizing AllowColumnResize="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </telerik:RadPageView>
        <%----------------Realized Holding-------------------------%>
        <telerik:RadPageView ID="EQPortfolioRealizedDeliveryTabPage" runat="server">
            <asp:Panel ID="pnlEQPortfolioRealizedDelivery" runat="server" Width="100%">
                <table>
                    <tr>
                        <td>
                            <div id="dvEquityPortfolioRealizedDelivery" runat="server" style="width: 98%; position: absolute">
                                <asp:Label ID="lblMessageSpeculative" Visible="false" Text="No Record Exists" runat="server"
                                    CssClass="Field"></asp:Label>
                                <telerik:RadGrid ID="gvEquityPortfolioRealizedDelivery" runat="server" GridLines="None"
                                    AutoGenerateColumns="False" PageSize="10" AllowSorting="true" AllowPaging="True"
                                    ShowStatusBar="True" ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false"
                                    ItemStyle-CssClass="Newfont" HeaderStyle-CssClass="Newfont" AllowFilteringByColumn="true"
                                    AllowAutomaticInserts="false" ExportSettings-FileName="Equity Net Position Details"
                                    OnNeedDataSource="gvEquityPortfolioRealizedDelivery_OnNeedDataSource" OnItemCommand="gvEquityPortfolioRealizedDelivery_ItemCommand"
                                    OnItemDataBound="gvEquityPortfolioRealizedDelivery_OnItemDataBound" OnDataBound="gvEquityPortfolioRealizedDelivery_OnDataBound">
                                    <ExportSettings HideStructureColumns="true">
                                    </ExportSettings>
                                    <MasterTableView DataKeyNames="ScripCode" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                        CommandItemDisplay="None">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderStyle-Width="50px" AllowFiltering="false" UniqueName="action"
                                                DataField="action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Select" HeaderText="Select"
                                                        ShowHeader="True" Text="Select" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="250px" FooterText="Grand Total" HeaderText="Scrip Name" DataField="PEM_CompanyName"
                                                UniqueName="PEM_CompanyName" SortExpression="PEM_CompanyName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-CssClass="Newfont">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="80px" FooterStyle-HorizontalAlign="Right" HeaderText="No of shares"
                                                DataField="NoOfShares" UniqueName="NoOfShares" SortExpression="NoOfShares" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:n0}" Aggregate="Sum" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="Cost Of Sale  "
                                                DataField="CostOfSale" UniqueName="CostOfSale" SortExpression="CostOfSale" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}" Aggregate="Sum" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="Sales Proceed "
                                                DataField="SaleProceed" UniqueName="SaleProceed" SortExpression="SaleProceed"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}" Aggregate="Sum" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="P/L"
                                                DataField="RealizedPL" UniqueName="RealizedPL" SortExpression="RealizedPL" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                Aggregate="Sum" DataFormatString="{0:N2}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" ForeColor="Green"
                                                    CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="80px" FooterStyle-HorizontalAlign="Right" HeaderText="P/L %"
                                                DataField="PnlPercent" UniqueName="PnlPercent" SortExpression="PnlPercent" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" ForeColor="Green"
                                                    CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="250px" HeaderText="Sector" DataField="PGSC_SectorCategoryName"
                                                UniqueName="PGSC_SectorCategoryName" SortExpression="PGSC_SectorCategoryName"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-CssClass="Newfont">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" CssClass="font" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="250px" HeaderText="Sub Sector" DataField="PGSSC_SectorSubCategoryName"
                                                UniqueName="PGSSC_SectorSubCategoryName" SortExpression="PGSSC_SectorSubCategoryName"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-CssClass="Newfont">
                                                <ItemStyle Width="" HorizontalAlign="left" VerticalAlign="Top" CssClass="font" Wrap="true" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="250px" HeaderText="Sub Sub Sector" DataField="PGSSSC_SectorSubSubCategoryName"
                                                UniqueName="PGSSSC_SectorSubSubCategoryName" SortExpression="PGSSSC_SectorSubSubCategoryName"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-CssClass="Newfont">
                                                <ItemStyle Width="" HorizontalAlign="left" VerticalAlign="Top" CssClass="font" Wrap="true" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                        <Resizing AllowColumnResize="true" />
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </telerik:RadPageView>
        <%----------------All-------------------------%>
        <telerik:RadPageView ID="radPageAllView" runat="server">
            <asp:Panel ID="pnlAllView" runat="server" Width="100%">
                <table>
                    <tr>
                        <td>
                            <div id="Div1" runat="server" style="width: 98%; position: absolute">
                                <asp:Label ID="Label3" Visible="false" Text="No Record Exists" runat="server" CssClass="Field"></asp:Label>
                                <telerik:RadGrid ID="gvrEQAll" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                    Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AllowAutomaticInserts="false"
                                    ExportSettings-FileName="Equity Net Position Details" ItemStyle-CssClass="Newfont"
                                    HeaderStyle-CssClass="Newfont" OnNeedDataSource="gvrEQAll_OnNeedDataSource" OnItemDataBound="gvrEQAll_DataBound"
                                    OnItemCommand="gvEquityPortfolio_ItemCommand" OnDataBound="gvEquityPortfolioRealizedDelivery_OnDataBound">
                                    <ExportSettings HideStructureColumns="true">
                                    </ExportSettings>
                                    <MasterTableView DataKeyNames="ScripCode" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                        CommandItemDisplay="None">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderStyle-Width="50px" AllowFiltering="false" UniqueName="action"
                                                DataField="action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Select" HeaderText="Select"
                                                        ShowHeader="True" Text="Select" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="250px" FooterText="Grand Total" HeaderText="Scrip Name" DataField="PEM_CompanyName"
                                                UniqueName="PEM_CompanyName" SortExpression="PEM_CompanyName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-CssClass="Newfont">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="80px" FooterStyle-HorizontalAlign="Right" HeaderText="Shares Bought"
                                                DataField="SharesBought" UniqueName="SharesBought" SortExpression="SharesBought"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:n0}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="80px" FooterStyle-HorizontalAlign="Right" HeaderText="Shares Sold"
                                                DataField="SharesSold" UniqueName="SharesSold" SortExpression="SharesSold" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:n0}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="80px" FooterStyle-HorizontalAlign="Right" HeaderText="Balance Shares"
                                                DataField="NoOfShares" UniqueName="NoOfShares" SortExpression="NoOfShares" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:n0}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="80px" FooterStyle-HorizontalAlign="Right" HeaderText="Avg. Price  "
                                                DataField="AvgPrice" UniqueName="AvgPrice" SortExpression="AvgPrice" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N4}">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="100px" FooterStyle-HorizontalAlign="Right" HeaderText="Closing Price  "
                                                DataField="CurrentpPrice" UniqueName="CurrentpPrice" SortExpression="CurrentpPrice"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N4}">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="95px" FooterStyle-HorizontalAlign="Right" HeaderText="Closing Date"
                                                DataField="CurrentPriceDate" UniqueName="CurrentPriceDate" SortExpression="CurrentPriceDate"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:d}">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            
                                                <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="100px" FooterStyle-HorizontalAlign="Right" HeaderText="ForEx Closing Price  "
                                                DataField="ForExCurrentpPrice" UniqueName="ForExCurrentpPrice" SortExpression="ForExCurrentpPrice"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N4}">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="90px" FooterStyle-HorizontalAlign="Right" HeaderText="ForEx Closing Date"
                                                DataField="ForExCurrentPriceDate" UniqueName="ForExCurrentPriceDate" SortExpression="ForExCurrentPriceDate"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:d}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="Cost Of Sale"
                                                DataField="CostOfSale" UniqueName="CostOfSale" SortExpression="CostOfSale" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}" Aggregate="Sum" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="100px" FooterStyle-HorizontalAlign="Right" HeaderText="Amount Invested"
                                                DataField="AmountInvested" UniqueName="AmountInvested" SortExpression="AmountInvested"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}" Aggregate="Sum" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="Sales Proceed"
                                                DataField="SaleProceed" UniqueName="SaleProceed" SortExpression="SaleProceed"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}" Aggregate="Sum" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="Current Value"
                                                DataField="CurrentValue" UniqueName="CurrentValue" SortExpression="CurrentValue"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                Aggregate="Sum" DataFormatString="{0:N2}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="UnRealized P/L"
                                                DataField="UnRealizedPL" UniqueName="UnRealizedPL" SortExpression="UnRealizedPL"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                Aggregate="Sum" DataFormatString="{0:N2}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" ForeColor="Green"
                                                    CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="Realized P/L"
                                                DataField="RealizedPL" UniqueName="RealizedPL" SortExpression="RealizedPL" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                Aggregate="Sum" DataFormatString="{0:N2}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" ForeColor="Green"
                                                    CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="P/L"
                                                DataField="PL" UniqueName="PL" SortExpression="PL" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                Aggregate="Sum" DataFormatString="{0:N2}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" ForeColor="Green"
                                                    CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="80px" FooterStyle-HorizontalAlign="Right" HeaderText="P/L %"
                                                DataField="PnlPercent" UniqueName="PnlPercent" SortExpression="PnlPercent" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" ForeColor="Green"
                                                    CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="Dividend"
                                                DataField="Dividend" UniqueName="Dividend" SortExpression="Dividend" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                Aggregate="Sum" DataFormatString="{0:N2}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="Total P/L(with Div)"
                                                DataField="TotalPL" UniqueName="TotalPL" SortExpression="TotalPL" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                Aggregate="Sum" DataFormatString="{0:N2}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" ForeColor="Green"
                                                    CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="80px" FooterStyle-HorizontalAlign="Right" HeaderText="Total P/L %"
                                                DataField="TotalPnlPercent" UniqueName="TotalPnlPercent" SortExpression="TotalPnlPercent"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" ForeColor="Green"
                                                    CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="80px" FooterStyle-HorizontalAlign="Right" HeaderText="MOI"
                                                DataField="Multiple" UniqueName="Multiple" SortExpression="Multiple" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" HeaderTooltip="Multiple On Investment"
                                                DataFormatString="{0:N2}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" ForeColor="Green"
                                                    CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="60px" HeaderText="Total XIRR (%)"
                                                DataField="TotalXIRRValue" UniqueName="TotalXIRRValue" SortExpression="TotalXIRRValue"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" ForeColor="Green"
                                                    CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="250px" HeaderText="Sector" DataField="PGSC_SectorCategoryName"
                                                UniqueName="PGSC_SectorCategoryName" SortExpression="PGSC_SectorCategoryName"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-CssClass="Newfont">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" CssClass="font" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="250px" HeaderText="Sub Sector" DataField="PGSSC_SectorSubCategoryName"
                                                UniqueName="PGSSC_SectorSubCategoryName" SortExpression="PGSSC_SectorSubCategoryName"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-CssClass="Newfont">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" CssClass="font" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="250px" HeaderText="Sub Sub Sector" DataField="PGSSSC_SectorSubSubCategoryName"
                                                UniqueName="PGSSSC_SectorSubSubCategoryName" SortExpression="PGSSSC_SectorSubSubCategoryName"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-CssClass="Newfont">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" CssClass="font" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                        <Resizing AllowColumnResize="true" />
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </telerik:RadPageView>
        <%----------------Speculative-------------------------%>
        <telerik:RadPageView ID="EQPortfolioRealizedSpeculative" runat="server">
            <asp:Panel ID="pnlEquityPortfolioRealizedSpeculative" runat="server" Width="100%">
                <table>
                    <tr>
                        <td>
                            <div id="dvgvEquityPortfolioRealizedSpeculative" runat="server" style="width: 98%;
                                position: absolute">
                                <asp:Label ID="Label1" Visible="false" Text="No Record Exists" runat="server" CssClass="Field"></asp:Label>
                                <telerik:RadGrid ID="gvEquityPortfolioRealizedSpeculative" runat="server" GridLines="None"
                                    Width="100%" AutoGenerateColumns="False" PageSize="10" AllowSorting="true" AllowPaging="True"
                                    ShowStatusBar="True" ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false"
                                    ItemStyle-CssClass="Newfont" HeaderStyle-CssClass="Newfont" AllowFilteringByColumn="true"
                                    AllowAutomaticInserts="false" ExportSettings-FileName="Equity Net Position Details"
                                    OnNeedDataSource="gvEquityPortfolioRealizedSpeculative_OnNeedDataSource" OnItemCommand="gvEquityPortfolioRealizedSpeculative_ItemCommand"
                                    OnItemDataBound="gvEquityPortfolioRealizedSpeculative_OnItemDataBound" OnDataBound="gvEquityPortfolioRealizedDelivery_OnDataBound">
                                    <ExportSettings HideStructureColumns="true">
                                    </ExportSettings>
                                    <MasterTableView DataKeyNames="ScripCode" Width="100%" AllowMultiColumnSorting="True"
                                        AutoGenerateColumns="false" CommandItemDisplay="None">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderStyle-Width="50px" AllowFiltering="false" UniqueName="action"
                                                DataField="action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Select" HeaderText="Select"
                                                        ShowHeader="True" Text="Select" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="250px" FooterText="Grand Total" HeaderText="Scrip Name" DataField="PEM_CompanyName"
                                                UniqueName="PEM_CompanyName" SortExpression="PEM_CompanyName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-CssClass="Newfont">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="100px" FooterStyle-HorizontalAlign="Right" HeaderText="No of shares"
                                                DataField="NoOfShares" UniqueName="NoOfShares" SortExpression="NoOfShares" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:n0}" Aggregate="Sum" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="Cost Of Sale  "
                                                DataField="CostOfSale" UniqueName="CostOfSale" SortExpression="CostOfSale" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}" Aggregate="Sum" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="Sales Proceed "
                                                DataField="SaleProceed" UniqueName="SaleProceed" SortExpression="SaleProceed"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}" Aggregate="Sum" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="P/L"
                                                DataField="RealizedPL" UniqueName="RealizedPL" SortExpression="RealizedPL" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                Aggregate="Sum" DataFormatString="{0:N2}" FooterStyle-CssClass="font">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" ForeColor="Green"
                                                    CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="100px" FooterStyle-HorizontalAlign="Right" HeaderText="P/L %"
                                                DataField="PnlPercent" UniqueName="PnlPercent" SortExpression="PnlPercent" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                DataFormatString="{0:N2}">
                                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" ForeColor="Green"
                                                    CssClass="font" />
                                                <FooterStyle ForeColor="White" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="250px" HeaderText="Sector" DataField="PGSC_SectorCategoryName"
                                                UniqueName="PGSC_SectorCategoryName" SortExpression="PGSC_SectorCategoryName"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-CssClass="Newfont">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" CssClass="font" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="250px" HeaderText="Sub Sector" DataField="PGSSC_SectorSubCategoryName"
                                                UniqueName="PGSSC_SectorSubCategoryName" SortExpression="PGSSC_SectorSubCategoryName"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-CssClass="Newfont">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" CssClass="font" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                HeaderStyle-Width="250px" HeaderText="Sub Sub Sector" DataField="PGSSSC_SectorSubSubCategoryName"
                                                UniqueName="PGSSSC_SectorSubSubCategoryName" SortExpression="PGSSSC_SectorSubSubCategoryName"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                FooterStyle-CssClass="Newfont">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" CssClass="font" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                        <Resizing AllowColumnResize="true" />
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</telerik:RadAjaxPanel>
<%---------------------------------------------------------Net Position In a Range-------------------------------------------------------------------------------------------------------%>
<telerik:RadAjaxPanel ID="RadAjaxPanel4" runat="server" ClientEvents-OnRequestStart="onRequestStart"
    CssClass="SetPlace" Visible="false">
    <asp:ImageButton ID="ImageButton5" ImageUrl="../Images/ExcelExport_Ico.jpg" Height="30px"
        Width="30px" OnClick="Button3_Click" runat="server" ToolTip="Export To Excel"
        CssClass="ImageButtons" />
    <asp:ImageButton ID="ImageButton6" ImageUrl="../Images/CsvExport_Ico.jpg" Height="30px"
        Width="30px" OnClick="Button3_Click" runat="server" ToolTip="Export To Csv" CssClass="ImageButtons" />
    <br />
    <asp:Panel ID="pnlUnRealizedHolding" runat="server" Width="100%">
        <table>
            <tr>
                <td>
                    <div id="Div2" runat="server">
                        <asp:Label ID="Label4" Visible="false" Text="No Record Exists" runat="server" CssClass="Field"></asp:Label>
                        <telerik:RadGrid ID="gvUnRealizedHolding" runat="server" GridLines="None" AutoGenerateColumns="False"
                            Style="width: 95%; position: absolute" PageSize="10" AllowSorting="true" AllowPaging="True"
                            ShowStatusBar="True" ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false"
                            AllowFilteringByColumn="true" AllowAutomaticInserts="false" ExportSettings-FileName="Equity Net Position Details"
                            ItemStyle-CssClass="Newfont" HeaderStyle-CssClass="Newfont" OnNeedDataSource="gvUnRealizedHolding_OnNeedDataSource"
                            OnItemDataBound="gvUnRealizedHolding_OnItemDataBound" OnDataBound="gvEquityPortfolioRealizedDelivery_OnDataBound">
                            <ExportSettings HideStructureColumns="true">
                            </ExportSettings>
                            <MasterTableView DataKeyNames="PEM_ScripCode" Width="100%" AllowMultiColumnSorting="True"
                                AutoGenerateColumns="false" CommandItemDisplay="None">
                                <Columns>
                                    <%-- <telerik:GridTemplateColumn HeaderStyle-Width="50px" AllowFiltering="false" UniqueName="action" 
                                            DataField="action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Select" HeaderText="Select" ShowHeader="True"
                                                        Text="Select" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>--%>
                                    <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                        HeaderStyle-Width="250px" FooterText="Grand Total" HeaderText="Scrip Name" DataField="PEM_CompanyName"
                                        UniqueName="PEM_CompanyName" SortExpression="PEM_CompanyName" AutoPostBackOnFilter="true"
                                        AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                        <FooterStyle CssClass="Newfont" ForeColor="White" />
                                    </telerik:GridBoundColumn>
                                    <%--       <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"  HeaderStyle-Width="80px" FooterStyle-HorizontalAlign="Right" HeaderText="Cost Of Inv."
                                            DataField="CostOfInvestment" UniqueName="CostOfInvestment" SortExpression="CostOfInvestment" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:n2}" >
                                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font"/>
                                              <FooterStyle  ForeColor="White"/>
                                        </telerik:GridBoundColumn>--%>
                                    <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                        HeaderStyle-Width="80px" FooterStyle-HorizontalAlign="Right" HeaderText="Opening Shares"
                                        DataField="OpeningQuantiy" UniqueName="OpeningQuantiy" SortExpression="OpeningQuantiy"
                                        AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        DataFormatString="{0:n0}" Aggregate="Sum" FooterStyle-CssClass="font">
                                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                        <FooterStyle ForeColor="White" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                        HeaderStyle-Width="83px" FooterStyle-HorizontalAlign="Right" HeaderText="Mkt Rate(Opening)"
                                        DataField="OpeningMarketRate" UniqueName="OpeningMarketRate" SortExpression="OpeningMarketRate"
                                        AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        DataFormatString="{0:n4}">
                                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                        <FooterStyle ForeColor="White" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                        HeaderStyle-Width="80px" FooterStyle-HorizontalAlign="Right" HeaderText="Opening Date"
                                        DataField="CloseDate" UniqueName="CloseDate" SortExpression="CloseDate" AutoPostBackOnFilter="true"
                                        AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        DataFormatString="{0:d}">
                                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                        <FooterStyle ForeColor="White" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                        HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="Opening Value"
                                        DataField="OpeningValue" UniqueName="OpeningValue" SortExpression="OpeningValue"
                                        AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        Aggregate="Sum" DataFormatString="{0:n2}" FooterStyle-CssClass="font">
                                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                        <FooterStyle ForeColor="White" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                        HeaderStyle-Width="80px" FooterStyle-HorizontalAlign="Right" HeaderText="Purchase Quantity"
                                        DataField="PurchaseQuantity" UniqueName="PurchaseQuantity" SortExpression="PurchaseQuantity"
                                        AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        DataFormatString="{0:n0}" Aggregate="Sum" FooterStyle-CssClass="font">
                                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                        <FooterStyle ForeColor="White" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                        HeaderStyle-Width="80px" FooterStyle-HorizontalAlign="Right" HeaderText="Avg. Pur. Rate"
                                        DataField="PurchaseRate" UniqueName="PurchaseRate" SortExpression="PurchaseRate"
                                        AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        DataFormatString="{0:n4}">
                                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                        <FooterStyle ForeColor="White" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                        HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="Purchase Value"
                                        DataField="PurchaseValue" UniqueName="PurchaseValue" SortExpression="PurchaseValue"
                                        AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        DataFormatString="{0:n2}" Aggregate="Sum" FooterStyle-CssClass="font">
                                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                        <FooterStyle ForeColor="White" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                        HeaderStyle-Width="80px" FooterStyle-HorizontalAlign="Right" HeaderText="Sale Quantity"
                                        DataField="SaleQuantity" UniqueName="SaleQuantity" SortExpression="SaleQuantity"
                                        AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        DataFormatString="{0:n0}" Aggregate="Sum" FooterStyle-CssClass="font">
                                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                        <FooterStyle ForeColor="White" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                        HeaderStyle-Width="80px" FooterStyle-HorizontalAlign="Right" HeaderText="Avg. Sale Rate"
                                        DataField="SaleRate" UniqueName="SaleRate" SortExpression="SaleRate" AutoPostBackOnFilter="true"
                                        AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        DataFormatString="{0:n4}">
                                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                        <FooterStyle ForeColor="White" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                        HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="Sale Value"
                                        DataField="SaleValue" UniqueName="SaleValue" SortExpression="SaleValue" AutoPostBackOnFilter="true"
                                        AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        DataFormatString="{0:n2}" Aggregate="Sum" FooterStyle-CssClass="font">
                                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                        <FooterStyle ForeColor="White" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                        FooterStyle-HorizontalAlign="Right" HeaderText="Closing Quantity " DataField="ClosingQuantity"
                                        UniqueName="ClosingQuantity" SortExpression="ClosingQuantity" HeaderStyle-Width="80px"
                                        AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-CssClass="font">
                                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                        <FooterStyle ForeColor="White" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                        FooterStyle-HorizontalAlign="Right" HeaderText="Closing Rate" DataField="MarketRate"
                                        UniqueName="MarketRate" SortExpression="MarketRate" HeaderStyle-Width="80px"
                                        AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        DataFormatString="{0:N4}">
                                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                        <FooterStyle ForeColor="White" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                        FooterStyle-HorizontalAlign="Right" HeaderText="Closing Date" DataField="RateAsOnDate"
                                        UniqueName="RateAsOnDate" SortExpression="RateAsOnDate" HeaderStyle-Width="80px"
                                        AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        DataFormatString="{0:d}">
                                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                        <FooterStyle ForeColor="White" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                        HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="Closing Value  "
                                        DataField="ClosingValue" UniqueName="ClosingValue" SortExpression="ClosingValue"
                                        AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        DataFormatString="{0:N2}" Aggregate="Sum" FooterStyle-CssClass="font">
                                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                        <FooterStyle ForeColor="White" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                        HeaderStyle-Width="80px" FooterStyle-HorizontalAlign="Right" HeaderText="Wtg. Per Share"
                                        DataField="WeightagePerShare" UniqueName="WeightagePerShare" SortExpression="WeightagePerShare"
                                        AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        DataFormatString="{0:N2}" Aggregate="Sum" FooterStyle-CssClass="font">
                                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                        <FooterStyle ForeColor="White" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                        HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="Div. Received"
                                        DataField="DividendRecieved" UniqueName="DividendRecieved" SortExpression="DividendRecieved"
                                        AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        DataFormatString="{0:N2}" Aggregate="Sum" FooterStyle-CssClass="font">
                                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                        <FooterStyle ForeColor="White" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                        HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="UnRealized P/L "
                                        DataField="UnrealizedProfit" UniqueName="UnrealizedProfit" SortExpression="UnrealizedProfit"
                                        AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        Aggregate="Sum" DataFormatString="{0:N2}" FooterStyle-CssClass="font">
                                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                        <FooterStyle ForeColor="White" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                        HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText="Realized P/L"
                                        DataField="RealisedProfitOrLoss" UniqueName="RealisedProfitOrLoss" SortExpression="RealisedProfitOrLoss"
                                        AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        Aggregate="Sum" DataFormatString="{0:N2}" FooterStyle-CssClass="font">
                                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                        <FooterStyle ForeColor="White" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                        HeaderStyle-Width="110px" FooterStyle-HorizontalAlign="Right" HeaderText=" Total P/L "
                                        DataField="TotalProfitOrLoss" UniqueName="TotalProfitOrLoss" SortExpression="TotalProfitOrLoss"
                                        AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        DataFormatString="{0:N2}" FooterStyle-CssClass="font" Aggregate="Sum">
                                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                        <FooterStyle ForeColor="White" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                        FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="60px" HeaderText="Total XIRR (%)"
                                        DataField="XIRRValue" UniqueName="XIRRValue" SortExpression="XIRRValue" AutoPostBackOnFilter="true"
                                        AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                        DataFormatString="{0:N2}">
                                        <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                                        <FooterStyle ForeColor="White" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings>
                                <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                <Resizing AllowColumnResize="true" />
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
</telerik:RadAjaxPanel>
<%---------------------------------------------------Avg Price Calculation-------------------------------------------------------------------------------------------------------------%>
<telerik:RadWindow ID="radEqAvgPriceDetails" runat="server" VisibleOnPageLoad="false"
    Height="10%" Width="1200px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false"
    Behaviors="close,Move" Title="Equity NP Details" AutoSize="true">
    <ContentTemplate>
        <div class="divPageHeading">
            <table>
                <tr>
                    <td align="left">
                        <asp:Label ID="lbl" runat="server" CssClass="HeaderTextBig" Text="Avg. Price Calculation "></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
       <%-- <telerik:RadAjaxPanel ID="radAjaxPanel" runat="server" >--%>
            <asp:ImageButton ID="imageButton" ImageUrl="../Images/CsvExport_Ico.jpg" Height="35px"
                Width="30px" OnClick="ButtonAvg_Click" runat="server" ToolTip="Export To Excel" />
            <br />
            <telerik:RadGrid ID="gvAvgPriceCalcDetails" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="800px" AllowFilteringByColumn="true"
                FooterStyle-CssClass="Newfont" AllowAutomaticInserts="false" ExportSettings-FileName="Equity Portfolio Details"
                ItemStyle-CssClass="Newfont" OnNeedDataSource="gvAvgPriceCalcDetails_OnNeedDataSource"
                OnPageIndexChanged="gvAvgPriceCalcDetails_PageIndexChanged" OnItemDataBound="gvEqTranxDetails_DataBound">
                <ExportSettings HideStructureColumns="true">
                </ExportSettings>
                <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                    CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="250px"  HeaderText="Scrip Name" DataField="PEM_CompanyName"
                            UniqueName="PEM_CompanyName" SortExpression="PEM_CompanyName" AutoPostBackOnFilter="true"
                            AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"  >
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" CssClass="font" />
                            <FooterStyle ForeColor="White" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Trade Date" DataField="CET_TradeDate"
                            UniqueName="CET_TradeDate" SortExpression="CET_TradeDate" AutoPostBackOnFilter="true"
                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Shares Bought"
                            DataField="CET_QuantityBefore" UniqueName="CET_QuantityBefore" SortExpression="CET_QuantityBefore"
                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                            Aggregate="Sum" DataFormatString="{0:N2}">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Net Price" DataField="CET_Rate"
                            UniqueName="CET_Rate" SortExpression="CET_Rate" AutoPostBackOnFilter="true" AllowFiltering="false"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N4}">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Balance Share"
                            DataField="CET_Quantity" UniqueName="CET_Quantity" SortExpression="CET_Quantity"
                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                            Aggregate="Sum" DataFormatString="{0:N0}">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Invested Amount"
                            DataField="InvestedAmount" UniqueName="InvestedAmount" SortExpression="InvestedAmount"
                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                            Aggregate="Sum" DataFormatString="{0:N2}">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    <Resizing AllowColumnResize="true" />
                </ClientSettings>
            </telerik:RadGrid>
      <%--  </telerik:RadAjaxPanel>--%>
    </ContentTemplate>
</telerik:RadWindow>
<%---------------------------------------------------Sell Pair-------------------------------------------------------------------------------------------------------------%>
<telerik:RadWindow ID="radEqSellPair" runat="server" VisibleOnPageLoad="false" Width="1200px"
    Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="close,Move"
    Title="Equity NP Sell Pair" AutoSize="true">
    <ContentTemplate>
        <telerik:RadAjaxPanel ID="RadAjaxPanel3" runat="server" ClientEvents-OnRequestStart="onRequestStart">
            <asp:ImageButton ID="ImageButton3" ImageUrl="../Images/CsvExport_Ico.jpg" Height="35px"
                Width="30px" OnClick="ButtonSellPair_Click" runat="server" ToolTip="Export To Excel" />
            <br />
            <telerik:RadGrid ID="gvEqSellPairDetails" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="true" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="800px" AllowFilteringByColumn="true"
                FooterStyle-CssClass="Newfont" AllowAutomaticInserts="false" ExportSettings-FileName="Equity Portfolio Details"
                ItemStyle-CssClass="Newfont" HeaderStyle-CssClass="Newfont" OnNeedDataSource="gvEqSellPairDetails_OnNeedDataSource"
                OnPageIndexChanged="gvEqSellPairDetails_PageIndexChanged" OnItemDataBound="gvEqSellPairDetails_DataBound">
                <ExportSettings HideStructureColumns="true">
                </ExportSettings>
                <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                    CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" HeaderText="Scrip Name" DataField="ScripName"
                            UniqueName="ScripName" SortExpression="ScripName" AutoPostBackOnFilter="true"
                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="TradeDate" DataField="TradeDate"
                            UniqueName="TradeDate" SortExpression="TradeDate" AutoPostBackOnFilter="true"
                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Buy/Sell" DataField="CET_BuySell" UniqueName="CET_BuySell"
                            SortExpression="CET_BuySell" AutoPostBackOnFilter="true" AllowFiltering="false"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Quantity"
                            DataField="Quantity" UniqueName="Quantity" SortExpression="Quantity" AutoPostBackOnFilter="true"
                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                            Aggregate="Sum" DataFormatString="{0:N0}">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Net Price" DataField="CET_Rate" UniqueName="CET_Rate"
                            SortExpression="CET_Rate" AutoPostBackOnFilter="true" AllowFiltering="false"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N4}">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Total Amount" DataField="Amount" UniqueName="Amount"
                            SortExpression="Amount" AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false"
                            CurrentFilterFunction="Contains" DataFormatString="{0:N2}">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Realized PL"
                            DataField="ProfitOrLoss" UniqueName="ProfitOrLoss" SortExpression="ProfitOrLoss"
                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                            Aggregate="Sum" DataFormatString="{0:N2}">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    <Resizing AllowColumnResize="true" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadAjaxPanel>
    </ContentTemplate>
</telerik:RadWindow>
<%---------------------------------------------------Tranx Details-------------------------------------------------------------------------------------------------------------%>
<telerik:RadWindow ID="radEqTranxDetails" runat="server" VisibleOnPageLoad="false"
    Height="10%" Width="1200px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false"
    Behaviors="close,Move" Title="Equity NP Details" AutoSize="true">
    <ContentTemplate>
        <div class="divPageHeading">
            <table>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label2" runat="server" CssClass="HeaderTextBig" Text="Equity Transaction"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" ClientEvents-OnRequestStart="onRequestStart">
            <asp:ImageButton ID="ImageButton4" ImageUrl="../Images/CsvExport_Ico.jpg" Height="35px"
                Width="30px" OnClick="ButtonTranx_Click" runat="server" ToolTip="Export To Excel" />
            <br />
            <telerik:RadGrid ID="gvEqTranxDetails" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="1100px" AllowFilteringByColumn="true"
                FooterStyle-CssClass="Newfont" AllowAutomaticInserts="false" ExportSettings-FileName="Equity Portfolio Details"
                ItemStyle-CssClass="Newfont" OnNeedDataSource="gvEqTranxDetails_OnNeedDataSource"
                OnPageIndexChanged="gvEqTranxDetails_PageIndexChanged" OnItemDataBound="gvEqTranxDetails_DataBound">
                <ExportSettings HideStructureColumns="true">
                </ExportSettings>
                <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                    CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="250px" HeaderText="Trade Account" DataField="CETA_TradeAccountNum"
                            UniqueName="CETA_TradeAccountNum" SortExpression="CETA_TradeAccountNum" AutoPostBackOnFilter="true"
                            AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" CssClass="font" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                            HeaderStyle-Width="250px" HeaderText="Scrip Name" DataField="PEM_CompanyName"
                            UniqueName="PEM_CompanyName" SortExpression="PEM_CompanyName" AutoPostBackOnFilter="true"
                            AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" CssClass="font" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="No. Of Shares"
                            DataField="CET_Quantity" UniqueName="CET_Quantity" SortExpression="CET_Quantity"
                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                            DataFormatString="{0:N0}">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="Price" DataField="CET_Rate"
                            UniqueName="CET_Rate" SortExpression="CET_Rate" AutoPostBackOnFilter="true" AllowFiltering="false"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N4}">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn FooterStyle-HorizontalAlign="Right" HeaderText="TradeTotal"
                            DataField="TradeTotal" UniqueName="TradeTotal" SortExpression="TradeTotal" AutoPostBackOnFilter="true"
                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                            DataFormatString="{0:N2}">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="125px" HeaderText="Trade Date" DataField="CET_TradeDate"
                            UniqueName="CET_TradeDate" SortExpression="CET_TradeDate" AutoPostBackOnFilter="true"
                            DataFormatString="{0:d}" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" CssClass="font" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Transaction Type" DataField="WETT_TransactionTypeName"
                            UniqueName="WETT_TransactionTypeName" SortExpression="WETT_TransactionTypeName"
                            AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Transaction Status" DataField="WTS_TransactionStatus"
                            UniqueName="WTS_TransactionStatus" SortExpression="WTS_TransactionStatus" AutoPostBackOnFilter="true"
                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" CssClass="font" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    <Resizing AllowColumnResize="true" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadAjaxPanel>
    </ContentTemplate>
</telerik:RadWindow>
<asp:HiddenField ID="hdnScipNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRealizedScipFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRealizedSpecScipFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnUnRealizedScipFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSelectedTab" runat="server" />
<asp:HiddenField ID="hdnNoOfRecords" runat="server" />
