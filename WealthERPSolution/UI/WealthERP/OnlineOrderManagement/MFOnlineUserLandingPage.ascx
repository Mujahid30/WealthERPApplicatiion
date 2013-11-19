<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFOnlineUserLandingPage.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.MFOnlineUserLandingPage" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<asp:ScriptManager ID="scriptmanager" runat="server">
</asp:ScriptManager>
<style type="text/css">
    .table
    {
        border: 1px solid orange;
    }
    .leftLabel
    {
        width: 15%;
        text-align: right;
    }
    .rightData
    {
        width: 18%;
        text-align: left;
    }
    .rightDataTwoColumn
    {
        width: 25%;
        text-align: left;
    }
    .rightDataFourColumn
    {
        width: 50%;
        text-align: left;
    }
    .rightDataThreeColumn
    {
        width: 41%;
        text-align: left;
    }
    .tdSectionHeading
    {
        padding-bottom: 6px;
        padding-top: 6px;
        width: 100%;
    }
    .divSectionHeading table td span
    {
        padding-bottom: 5px !important;
    }
    .fltlft
    {
        float: left;
        padding-left: 3px;
        width: 20%;
    }
    .divCollapseImage
    {
        float: left;
        padding-left: 5px;
        width: 2%;
        float: right;
        text-align: right;
        cursor: pointer;
        cursor: hand;
    }
    .imgCollapse
    {
        background: Url(../Images/Section-Expand.png);
        cursor: pointer;
        cursor: hand;
    }
    .imgExpand
    {
        background: Url(../Images/Section-Collapse.png) no-repeat left top;
        cursor: pointer;
        cursor: hand;
    }
    .fltlftStep
    {
        float: left;
    }
    .StepOneContentTable, .StepTwoContentTable, .StageRequestTable, .StepThreeContentTable, .StepFourContentTable
    {
        width: 100%;
    }
    .SectionBody
    {
        width: 100%;
    }
    .collapse
    {
        text-align: right;
    }
    .divStepStatus
    {
        float: left;
        padding-left: 2px;
        padding-right: 5px;
    }
    .PCGLongButton
    {
        height: 26px;
    }
</style>
<%--<div class="divOnlinePageHeading" style="float: right; width: 100%">
    <div style="float: right; padding-right: 100px;">
        <table cellspacing="0" cellpadding="3" width="100%">
            <tr>
                <td align="right" style="width: 10px">
                    <asp:ImageButton Visible="false" ID="btnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                        OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                </td>
            </tr>
        </table>
    </div>
</div>--%>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            MF Online User Landing Page
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table id="tblMessage" width="100%" runat="server" visible="false">
    <tr id="trSumbitSuccess">
        <td align="center">
            <div id="msgRecordStatus" class="success-msg" align="center" runat="server"></div>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td class="leftLabel">
            <asp:Label runat="server" class="FieldName" Text="AMC:" ID="lblAmc"></asp:Label></td>
        <td class="rightData">    
            <asp:DropDownList CssClass="cmbField" ID="ddlAmc" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAmc_OnSelectedIndexChanged" ValidationGroup="SchemeLanding">
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvAmc" runat="server" 
                ErrorMessage="Please select an AMC" ControlToValidate="ddlAmc" InitialValue="0" 
                ValidationGroup="SchemeLanding" CssClass="rfvPCG">*</asp:RequiredFieldValidator></td>
        <td class="leftLabel">
            <asp:Label runat="server" class="FieldName" Text="Category:" ID="lblCategory"></asp:Label></td>
        <td class="rightData">
            <asp:DropDownList CssClass="cmbField" ID="ddlCategory" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged" ValidationGroup="SchemeLanding">
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvCategory" runat="server" 
                ErrorMessage="Please select a category" ControlToValidate="ddlCategory" 
                CssClass="rfvPCG" ValidationGroup="SchemeLanding" InitialValue="0">*</asp:RequiredFieldValidator></td>
        <td class="leftLabel">
            <asp:Label runat="server" class="FieldName" Text="Scheme:" ID="lblScheme"></asp:Label></td>
        <td class="rightData">
            <asp:DropDownList CssClass="cmbField" ID="ddlScheme" runat="server" AutoPostBack="false" ValidationGroup="SchemeLanding">
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvScheme" runat="server" 
                ErrorMessage="Please select a scheme" CssClass="rfvPCG" 
                Text="Please select a scheme" InitialValue="0" ValidationGroup="SchemeLanding" 
                ControlToValidate="ddlScheme">*</asp:RequiredFieldValidator></td>
        <td class="leftLabel">
            <asp:Button ID="btnSchemeLanding" runat="server" CssClass="PCGButton" Text="Go" ValidationGroup="SchemeLanding"
                OnClick="btnSchemeLanding_Click" /></td>
        <td>&nbsp;</td>
    </tr>
</table>

<div style="margin-top:10px">
    <asp:Panel ID="pnlMFSchemeLanding" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal" Visible="false">
        <table>
            <tr>
                <td>
                    <telerik:RadGrid ID="gvMFSchemeLanding" runat="server" GridLines="None" AutoGenerateColumns="False"
                        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                        Skin="Telerik" EnableEmbeddedSkins="false" ClientSettings-AllowColumnsReorder="true" Width="104%"
                        AllowAutomaticInserts="false" OnNeedDataSource="gvMFSchemeLanding_OnNeedDataSource" OnItemDataBound="gvMFSchemeLanding_OnItemDataBound">
                        <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                            CommandItemDisplay="None" AllowFilteringByColumn="true" DataKeyNames="PASP_SchemePlanCode">
                            <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                                ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                            <Columns>
                            <telerik:GridBoundColumn DataField="PASP_SchemePlanCode" AllowFiltering="true" HeaderText="Scheme Code"
                                    UniqueName="PASP_SchemePlanCode" SortExpression="PASP_SchemePlanCode" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="70px"
                                    FilterControlWidth="30px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PASP_SchemePlanName" AllowFiltering="true" HeaderText="Scheme Name"
                                    UniqueName="PASP_SchemePlanName" SortExpression="PASP_SchemePlanName" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="180px"
                                    FilterControlWidth="120px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PA_AMCName" AllowFiltering="true" HeaderText="Amc"
                                    UniqueName="PA_AMCName" SortExpression="PA_AMCName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" HeaderStyle-Width="180px" FilterControlWidth="120px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PAIC_AssetInstrumentCategoryName" AllowFiltering="true"
                                    HeaderText="Category" UniqueName="PAIC_AssetInstrumentCategoryName" SortExpression="PAIC_AssetInstrumentCategoryName"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    HeaderStyle-Width="95px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PASPD_CutOffTime" AllowFiltering="true" HeaderText="Cut-OffTime"
                                    UniqueName="PASPD_CutOffTime" SortExpression="PASPD_CutOffTime" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="90px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Rating" HeaderText="Rating" AllowFiltering="true"
                                    HeaderStyle-Wrap="false" SortExpression="Rating" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="Rating" FooterStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="50px">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PASPD_IsSIPAvailable" HeaderText="IsSIPAvailable" AllowFiltering="true"
                                    HeaderStyle-Wrap="false" SortExpression="PASPD_IsSIPAvailable" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="PASPD_IsSIPAvailable" FooterStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="50px">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FundManagerRating" AllowFiltering="false" HeaderText="Fund Manager Rating"
                                    UniqueName="FundManagerRating" SortExpression="FundManagerRating" ShowFilterIcon="false"
                                    HeaderStyle-Width="80px" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SchemeBenchmark" HeaderText="Scheme & Benchmark Performance"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="SchemeBenchmark"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="SchemeBenchmark" FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="90px">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Nav" HeaderText="Last NAV" AllowFiltering="true"
                                    HeaderStyle-Wrap="false" SortExpression="Nav" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="Nav" FooterStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="80px">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NavDate" HeaderText="NAV Date" AllowFiltering="true"
                                    HeaderStyle-Wrap="false" SortExpression="NavDate" DataFormatString="{0:d}" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="NavDate"
                                    FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PASPD_InitialPurchaseAmount" HeaderText="Initial Amount(Regular)"
                                    AllowFiltering="false" SortExpression="PASPD_InitialPurchaseAmount" ShowFilterIcon="false"
                                    DataFormatString="{0:N2}" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"
                                    UniqueName="PASPD_InitialPurchaseAmount" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PASPSD_MinAmount" AllowFiltering="true" HeaderText="Initial Amount(SIP)"
                                    DataFormatString="{0:N2}" UniqueName="PASPSD_MinAmount" SortExpression="PASPSD_MinAmount"
                                    ShowFilterIcon="false" HeaderStyle-Width="80px" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PASPD_AdditionalPruchaseAmount" AllowFiltering="true"
                                    HeaderText="Additional Purchase(Regular)" DataFormatString="{0:N0}" UniqueName="PASPD_AdditionalPruchaseAmount"
                                    SortExpression="PASPD_AdditionalPruchaseAmount" ShowFilterIcon="false" HeaderStyle-Width="80px"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PASPSD_MultipleAmount" AllowFiltering="true"
                                    HeaderText="Additional Purchase(SIP)" UniqueName="PASPSD_MultipleAmount" SortExpression="PASPSD_MultipleAmount"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    HeaderStyle-Width="75px" FilterControlWidth="50px" DataFormatString="{0:N2}">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="Recomndation" HeaderText="Recomndation"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="Recomndation"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="Recomndation" FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn ItemStyle-Width="60px" AllowFiltering="false" HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSIPSchemeFlag" runat="server" CssClass="cmbField" Text='<%# Eval("PASPD_IsSIPAvailable") %>'></asp:Label>
                                        <asp:ImageButton OnClick="imgBuy_OnClick" ID="imgBuy" runat="server" CommandName="Buy" ImageUrl="~/Images/Buy-Button.png" ToolTip="BUY" />
                                        &nbsp;
                                        <asp:ImageButton OnClick="imgSip_OnClick" ID="imgSip" runat="server" CommandName="SIP" ImageUrl="~/Images/SIP-Button.png" ToolTip="SIP" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
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
</div>
<asp:HiddenField ID="hdnScheme" runat="server" />
<asp:HiddenField ID="hdnCategory" runat="server" />
