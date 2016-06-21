<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineSchemeMIS.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineSchemeMIS" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            View Schemes
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="imgexportButton" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                Visible="false" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                OnClick="btnExportData_OnClick" OnClientClick="setFormat('excel')" Height="22px"
                                Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="right" style="width: 8%;">
            <asp:Label ID="lblproduct" CssClass="FieldName" runat="server" Text="Product:" valign="top"></asp:Label>
        </td>
        <td style="width: 10%;">
            <asp:DropDownList ID="ddlProduct" runat="server" CssClass="cmbField" AutoPostBack="false"
                Enabled="false">
                <Items>
                    <asp:ListItem Text="Select" Value="Select" Selected="false" />
                    <asp:ListItem Text="Mutual Funds" Value="MF" Selected="True" />
                    <asp:ListItem Text="Bonds" Value="BO" Enabled="false" />
                </Items>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvType" runat="server" CssClass="rfvPCG" ErrorMessage="</br>Please Select a Product"
                ControlToValidate="ddlProduct" Display="Dynamic" InitialValue="Select" ValidationGroup="OnlineMis">
            </asp:RequiredFieldValidator>
        </td>
        <td align="Right" id="llbtosee" runat="server" style="width: 15%;">
            <asp:Label ID="lblTosee" CssClass="FieldName" runat="server" Text="Do You Wish To See:"></asp:Label>
        </td>
        <td id="tdtosee" runat="server" style="width: 10%;">
            <asp:DropDownList ID="ddlTosee" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlTosee_OnSelectedIndexChanged"
                AutoPostBack="true">
                <Items>
                    <asp:ListItem Text="Both" Value="2" />
                    <asp:ListItem Text="Online Scheme" Value="1" Selected="True" />
                    <asp:ListItem Text="Offline Scheme" Value="0" />
                    <%-- <asp:ListItem Text="Both" Value="Both"/>--%>
                </Items>
            </asp:DropDownList>
        </td>
        <td style="width: 20%;" id="tdMode" runat="server">
            <label class="FieldName" style="width: 4%; margin-left: 25px">
                Mode:</label>
            <asp:DropDownList ID="ddlMode" CssClass="cmbField" runat="server">
                <asp:ListItem Text="Select" Value="2"></asp:ListItem>
                <asp:ListItem Selected="True" Text="Online" Value="1"></asp:ListItem>
                <asp:ListItem Text="Demat" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td style="width: 5%;" align="right">
            <asp:Label ID="lblstatus" CssClass="FieldName" runat="server" Text="Status:"> </asp:Label>
        </td>
        <td style="width: 10%;">
            <asp:DropDownList ID="ddlststus" runat="server" CssClass="cmbField" AutoPostBack="true">
                <Items>
                    <%--    <asp:ListItem Text="Both" Value="All" />--%>
                    <asp:ListItem Text="Active" Value="Active" Selected="True" />
                    <asp:ListItem Text="Liquidated" Value="Liquidated" />
                    <asp:ListItem Text="Merged" Value="Merged" />
                    <asp:ListItem Text="NFO" Value="NFO" />
                    <asp:ListItem Text="Close NFO" Value="CloseNFO" />
                </Items>
            </asp:DropDownList>
        </td>
        <td align="left" style="margin-left: 15px">
            <asp:Button ID="btngo" runat="server" Text="Go" CssClass="PCGMediumButton" OnClick="btngo_Click"
                ValidationGroup="OnlineMis" />
        </td>
    </tr>
</table>
<asp:Panel ID="pnlSchemeMIS" runat="server" ScrollBars="Horizontal" Height="100%"
    Width="100%" Visible="false">
    <table width="100%">
        <tr>
            <%-- OnItemCreated="gvonlineschememis_ItemCreated"
                    OnItemDataBound="gvonlineschememis_ItemDataBound"
                    OnNeedDataSource="gvonlineschememis_OnNeedDataSource" OnPreRender="gvonlineschememis_PreRender"--%>
            <td>
                <div id="SchemeMIS" runat="server" style="width: 100%; padding-left: 0px;" visible="false">
                    <telerik:RadGrid ID="gvonlineschememis" runat="server" AllowAutomaticDeletes="false"
                        PageSize="20" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                        ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true"
                        GridLines="none" AllowAutomaticInserts="false" Skin="Telerik" EnableHeaderContextMenu="true"
                        OnNeedDataSource="gvonlineschememis_OnNeedDataSource" OnItemDataBound="gvonlineschememis_OnItemDataBound"
                        Width="120%" Height="400px">
                        <ExportSettings HideStructureColumns="true">
                        </ExportSettings>
                        <MasterTableView DataKeyNames="PASP_SchemePlanCode,PASP_Status" Width="100%" AllowMultiColumnSorting="True"
                            AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="Action" DataField="Action"
                                    HeaderStyle-Width="110px">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlAction" CssClass="cmbField" runat="server" EnableEmbeddedSkins="false"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChanged"
                                            Width="110px">
                                            <Items>
                                                <asp:ListItem Text="Select" Value="Select" Selected="true" />
                                                <asp:ListItem Text="View" Value="View" />
                                                <asp:ListItem Text="Edit" Value="Edit" />
                                            </Items>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="PASP_SchemePlanCode" UniqueName="PASP_SchemePlanCode"
                                    HeaderText="System Code" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    HeaderStyle-Width="80px" SortExpression="PASP_SchemePlanCode" FilterControlWidth="80px"
                                    CurrentFilterFunction="Contains" Visible="true">
                                    <ItemStyle Width="80px" HorizontalAlign="Left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SchemCode" UniqueName="SchemCode" HeaderText="Scheme Code"
                                    ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="80px"
                                    SortExpression="SchemCode" FilterControlWidth="80px" CurrentFilterFunction="Contains"
                                    Visible="true">
                                    <ItemStyle Width="80px" HorizontalAlign="Left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="productcode" UniqueName="productcode" HeaderText="Product Code"
                                    ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="80px"
                                    SortExpression="productcode" FilterControlWidth="80px" CurrentFilterFunction="Contains"
                                    Visible="true">
                                    <ItemStyle Width="80px" HorizontalAlign="Left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AMFIcode" UniqueName="AMFIcode" HeaderText="AMFI Code"
                                    ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="80px"
                                    SortExpression="AMFIcode" FilterControlWidth="80px" CurrentFilterFunction="Contains"
                                    Visible="true">
                                    <ItemStyle Width="80px" HorizontalAlign="Left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PASP_SchemePlanName" UniqueName="PASP_SchemePlanName"
                                    HeaderText="Scheme" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    HeaderStyle-Width="240px" SortExpression="PASP_SchemePlanName" FilterControlWidth="240px"
                                    CurrentFilterFunction="Contains">
                                    <ItemStyle Width="240px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PA_AMCName" UniqueName="PA_AMCName" HeaderText="AMC"
                                    ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="200px"
                                    SortExpression="PA_AMCName" FilterControlWidth="200px" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="200px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PMFRD_FundManagerName" UniqueName="PMFRD_FundManagerName"
                                    HeaderText="Fund Manager Name" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                    AllowFiltering="true" HeaderStyle-Width="80px" SortExpression="PMFRD_FundManagerName"
                                    FilterControlWidth="200px" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="80px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="PA_AMCCode" UniqueName="PA_AMCCode"
                                    HeaderText="PA_AMCCode" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    HeaderStyle-Width="200px" SortExpression="PA_AMCCode" FilterControlWidth="200px"
                                    CurrentFilterFunction="Contains">
                                    <ItemStyle Width="200px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PASC_AMC_ExternalType" UniqueName="PASC_AMC_ExternalType"
                                    HeaderText="RTA" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    HeaderStyle-Width="70px" SortExpression="PASC_AMC_ExternalType" FilterControlWidth="50px"
                                    CurrentFilterFunction="Contains">
                                    <ItemStyle Width="70px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PASP_IsOnline" UniqueName="PASP_IsOnline" HeaderText="IsOnline"
                                    SortExpression="PASP_IsOnline" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"
                                    FilterControlWidth="60px" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                    <ItemStyle Width="80px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PAIC_AssetInstrumentCategoryName" UniqueName="PAIC_AssetInstrumentCategoryName"
                                    HeaderText="Category" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    HeaderStyle-Width="100px" SortExpression="PAIC_AssetInstrumentCategoryName" FilterControlWidth="100px"
                                    CurrentFilterFunction="Contains">
                                    <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PAISC_AssetInstrumentSubCategoryName" UniqueName="PAISC_AssetInstrumentSubCategoryName"
                                    HeaderText="Sub Category" AutoPostBackOnFilter="true" SortExpression="PAISC_AssetInstrumentSubCategoryName"
                                    ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="145px">
                                    <ItemStyle Width="145px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PAISSC_AssetInstrumentSubSubCategoryName" UniqueName="PAISSC_AssetInstrumentSubSubCategoryName"
                                    HeaderText="Sub Sub Category" SortExpression="PAISSC_AssetInstrumentSubSubCategoryName"
                                    AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="150px"
                                    FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="true" DataField="PASP_Status" UniqueName="PASP_Status"
                                    HeaderText="Status" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    HeaderStyle-Width="90px" SortExpression="PASP_Status" FilterControlWidth="50px"
                                    CurrentFilterFunction="Contains">
                                    <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="true" DataField="PAIC_CreatedBy" UniqueName="PAIC_CreatedBy"
                                    HeaderText="Created By" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    HeaderStyle-Width="90px" SortExpression="PAIC_CreatedBy" FilterControlWidth="50px"
                                    CurrentFilterFunction="Contains">
                                    <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="true" DataField="PAIC_CreatedOn" UniqueName="PAIC_CreatedOn"
                                    HeaderText="Created On" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    HeaderStyle-Width="90px" SortExpression="PAIC_CreatedOn" FilterControlWidth="50px"
                                    CurrentFilterFunction="Contains">
                                    <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="true" DataField="PAIC_ModifiedBy" UniqueName="PAIC_ModifiedBy"
                                    HeaderText="Modified By" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    HeaderStyle-Width="90px" SortExpression="PAIC_ModifiedBy" FilterControlWidth="50px"
                                    CurrentFilterFunction="Contains">
                                    <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="true" DataField="PAIC_ModifiedOn" UniqueName="PAIC_ModifiedOn"
                                    HeaderText="Modified On" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                    HeaderStyle-Width="90px" SortExpression="PAIC_ModifiedOn" FilterControlWidth="50px"
                                    CurrentFilterFunction="Contains">
                                    <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Resizing AllowColumnResize="true" />
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="hdnAssettype" runat="server" />
<asp:HiddenField ID="hdnIsonline" runat="server" />
<asp:HiddenField ID="hdnStatus" runat="server" />
