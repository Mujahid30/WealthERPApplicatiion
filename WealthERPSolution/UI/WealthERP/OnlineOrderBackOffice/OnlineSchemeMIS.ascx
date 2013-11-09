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
                            Scheme MIS
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
        <td align="right" >
            <asp:Label ID="lblproduct" CssClass="FieldName" runat="server" Text="Product" valign="top" ></asp:Label>
              </td><td>
            <asp:DropDownList ID="ddlProduct" runat="server" CssClass="cmbField" OnSelectedIndexChanged="Onselectedindex_select" AutoPostBack ="true" >
                <Items>
                  <asp:ListItem Text="Select" Value="Select" Selected="false" />
                  <asp:ListItem Text="Mutual Funds"  Value="MF"/>
                  <asp:ListItem Text="Bonds" value="BO" Enabled="false" />
                  
                </Items>
                                          
            </asp:DropDownList>
        </td>
        <td align="Right" id="llbtosee" runat="server">
            <asp:Label ID="lblTosee" CssClass="FieldName" runat="server" Text="Do You Wish To See" ></asp:Label>
              </td>
              <td id="tdtosee" runat="server">
            <asp:DropDownList ID="ddlTosee" runat="server" CssClass="cmbField" >
                <Items>
                  <asp:ListItem Text="Both" Value="2" />
                  <asp:ListItem Text="Online Scheme" Value="1" />
                  <asp:ListItem Text="Offline Scheme" Value="0"/>
                 <%-- <asp:ListItem Text="Both" Value="Both"/>--%>
                  
                </Items>
            </asp:DropDownList>
        </td>
        <td align="left">
            <asp:Button ID="btngo" runat="server" Text="Go" CssClass="PCGButton" 
                onclick="btngo_Click" />
        </td>
    </tr>
</table>
<asp:Panel ID="pnlSchemeMIS" runat="server" ScrollBars="Horizontal" Width="100%" visible="false">
<table width="100%">
    <tr>
    <%-- OnItemCreated="gvonlineschememis_ItemCreated"
                    OnItemDataBound="gvonlineschememis_ItemDataBound"
                    OnNeedDataSource="gvonlineschememis_OnNeedDataSource" OnPreRender="gvonlineschememis_PreRender"--%>
        <td>
            <div id="SchemeMIS" runat="server" style="width: 100%; padding-left: 5px;
            " visible="false">
                 <telerik:RadGrid ID="gvonlineschememis" runat="server" AllowAutomaticDeletes="false"
                    EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                    ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true"
                    GridLines="none" AllowAutomaticInserts="false"  Skin="Telerik" EnableHeaderContextMenu="true"
                    OnNeedDataSource="gvonlineschememis_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="PASP_SchemePlanCode" Width="100%" AllowMultiColumnSorting="True"
                        AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="Action" DataField="Action"
                                HeaderStyle-Width="140px">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlAction" 
                                        CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" AutoPostBack="true" 
                                        OnSelectedIndexChanged="ddlAction_OnSelectedIndexChanged"
                                        Width="120px" AppendDataBoundItems="true">
                                        <Items>
                                            <asp:ListItem Text="Select" Value="Select" Selected="true" />
                                            <asp:ListItem Text="View" Value="View" />
                                            <asp:ListItem Text="Edit" Value="Edit" />
                                            
                                        </Items>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="PA_AMCName" UniqueName="PA_AMCName"
                                HeaderText="AMC" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                HeaderStyle-Width="120px" SortExpression="PA_AMCNamee" FilterControlWidth="50px"
                                CurrentFilterFunction="Contains">
                                <ItemStyle Width="150px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn Visible="false" DataField="PA_AMCCode" UniqueName="PA_AMCCode"
                                HeaderText="PA_AMCCode" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                HeaderStyle-Width="120px" SortExpression="PA_AMCNamee" FilterControlWidth="50px"
                                CurrentFilterFunction="Contains">
                                <ItemStyle Width="150px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="PASC_AMC_ExternalType" UniqueName="PASC_AMC_ExternalType"
                                HeaderText="External Type" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                HeaderStyle-Width="120px" SortExpression="PA_AMCNamee" FilterControlWidth="50px"
                                CurrentFilterFunction="Contains">
                                <ItemStyle Width="150px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="PASP_SchemePlanCode" UniqueName="PASP_SchemePlanCode"
                                HeaderText="PASP_SchemePlanCode" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                HeaderStyle-Width="67px" SortExpression="PASP_SchemePlanCode" FilterControlWidth="50px"
                                CurrentFilterFunction="Contains" Visible="false">
                                <ItemStyle Width="67px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PASP_SchemePlanName" UniqueName="PASP_SchemePlanName" HeaderText="Scheme"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="290px"
                                SortExpression="PASP_SchemePlanName" FilterControlWidth="250px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="290px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="PASP_IsOnline" UniqueName="PASP_IsOnline" HeaderText="IsOnline Available"
                                SortExpression="PASP_IsOnline" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                                FilterControlWidth="80px" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemStyle Width="80px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PAIC_AssetInstrumentCategoryName" UniqueName="PAIC_AssetInstrumentCategoryName" HeaderText="Category"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="140px"
                                SortExpression="PAIC_AssetInstrumentCategoryName" FilterControlWidth="100px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PAISC_AssetInstrumentSubCategoryName" UniqueName="PAISC_AssetInstrumentSubCategoryName" HeaderText="Sub Category"
                                AutoPostBackOnFilter="true" SortExpression="PAISC_AssetInstrumentSubCategoryName" ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="145px">
                                <ItemStyle Width="145px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PAISSC_AssetInstrumentSubSubCategoryName" UniqueName="PAISSC_AssetInstrumentSubSubCategoryName" HeaderText="Sub Sub Category"
                                SortExpression="PAISSC_AssetInstrumentSubSubCategoryName" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                                AllowFiltering="true" HeaderStyle-Width="150px" FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn Visible="true" DataField="PASP_Status" UniqueName="PASP_Status"
                                HeaderText="PASP_Status" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                HeaderStyle-Width="120px" SortExpression="PASP_Status" FilterControlWidth="50px"
                                CurrentFilterFunction="Contains">
                                <ItemStyle Width="150px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
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
