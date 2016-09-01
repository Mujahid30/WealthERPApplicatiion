<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewAgentCode.ascx.cs"
    Inherits="WealthERP.Associates.ViewAgentCode" %>
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            View SubBroker Code
                        </td>
                        <td align="right" style="padding-bottom: 2px;">
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
<table width="100%%">
    <tr>
        <td colspan="4">
            <asp:Panel ID="pnlAgentCodeView" runat="server" ScrollBars="Horizontal"  Width="98%"
                Visible="true">
                <table>
                    <tr>
                        <td>
                            <div runat="server" id="divAgentCodeView" style="margin: 2px; width: 640px;">
                                <telerik:RadGrid ID="gvAgentCodeView" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                    Skin="Telerik" EnableEmbeddedSkins="false" Width="55%" AllowFilteringByColumn="true"
                                    AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" OnNeedDataSource="gvAgentCodeView_OnNeedDataSource"
                                    EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true">
                                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                        FileName="ViewAssociates" Excel-Format="ExcelML">
                                    </ExportSettings>
                                    <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" DataKeyNames="AAC_AgentCode,AAC_AdviserAgentId,Id"
                                        CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                                        GroupLoadMode="Client" ShowGroupFooter="true">
                                        <Columns>
                                       <%--  <telerik:GridTemplateColumn HeaderText="Id" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Width="80px" AllowFiltering="true" DataField="AAC_AdviserAgentId"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LnkRQ" runat="server" CssClass="CmbField" OnClick="LnkRQ_Click"
                                                        Text='<%#Eval("AAC_AdviserAgentId") %>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>--%>
                                         <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="SubBroker Code" DataField="AAC_AgentCode"
                                                UniqueName="AAC_AgentCode" SortExpression="AAC_AgentCode" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Name" DataField="NAME"
                                                UniqueName="NAME" SortExpression="NAME" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            
                                           
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Type" DataField="AAC_UserType"
                                                UniqueName="AAC_UserType" SortExpression="AAC_UserType" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            
                                             <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Parent Id" DataField="ClusterManager"
                                                UniqueName="ParentId" SortExpression="ParentId" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                              <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Parent name" DataField="ClusterManager"
                                                UniqueName="Parentname" SortExpression="Parentname" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Parent PAN" DataField="ClusterManager"
                                                UniqueName="Parentpan" SortExpression="Parentpan" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            
                                            <telerik:GridBoundColumn DataField="Titles" SortExpression="Titles" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Title" UniqueName="Titles">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ChannelName" SortExpression="ChannelName" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Channel Manager" UniqueName="ChannelName">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="ClusterManager" SortExpression="ClusterManager" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" Visible="false" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Cluster Manager" UniqueName="ClusterManager">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AreaManager" SortExpression="AreaManager" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" Visible="false" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Area Manager" UniqueName="AreaManager">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ZonalManagerName" SortExpression="ZonalManagerName" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" Visible="false" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Zone Manager" UniqueName="ZonalManagerName">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="DeputyHead" SortExpression="DeputyHead"
                                                AutoPostBackOnFilter="true" Visible="false" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                AllowFiltering="true" HeaderText="Deputy Head" UniqueName="DeputyHead">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
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
