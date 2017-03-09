<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewStaff.ascx.cs" Inherits="WealthERP.Advisor.ViewStaff" %>
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Sales Hierarchy
                        </td>
                        <td align="right" id="tdExport" runat="server" style="padding-bottom: 2px;">
                            <asp:ImageButton ID="imgViewStaff" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" Height="20px"
                                Width="25px" Visible="true" OnClick="imgViewStaff_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr id="trBMBranchs" runat="server">
        <td colspan="2" style="float: left">
            <asp:Label ID="lblChooseBr" runat="server" Font-Bold="true" Font-Size="Small" CssClass="FieldName"
                Text="Branch: "></asp:Label>
            &nbsp;&nbsp;
            <asp:DropDownList ID="ddlBMBranch" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlBMBranch_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td colspan="4">
            <asp:Panel ID="pnlViewStaff" runat="server" Width="98%" ScrollBars="Horizontal" Visible="true">
                <table>
                    <tr>
                        <td>
                            <div runat="server" id="divViewStaff" style="margin: 2px; width: 640px;">
                                <telerik:RadGrid ID="rgvViewStaff" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                    AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" EnableHeaderContextMenu="true"
                                    EnableHeaderContextFilterMenu="true" OnNeedDataSource="gvAssociates_rgvViewStaff" OnItemDataBound="rgvViewStaff_ItemDataBound">
                                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                        FileName="ViewStaffList" Excel-Format="ExcelML">
                                    </ExportSettings>
                                    <MasterTableView Width="100%" DataKeyNames="AR_RMId" AllowMultiColumnSorting="True"
                                        AutoGenerateColumns="false" CommandItemDisplay="None" GroupsDefaultExpanded="false"
                                        ExpandCollapseColumn-Groupable="true" GroupLoadMode="Client" ShowGroupFooter="true">
                                        <Columns>
                                            <telerik:GridTemplateColumn ItemStyle-Width="80Px" AllowFiltering="false" >
                                                <ItemTemplate>
                                                   <%-- <telerik:RadComboBox ID="ddlMenu" UniqueName="ddlMenu"   OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged" 
                                                        CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" Skin="Telerik"
                                                        AllowCustomText="true" Width="120px" AutoPostBack="true" >
                                                        <Items>
                                                            <telerik:RadComboBoxItem ImageUrl="~/Images/Select.png" Text="Select" Value="0" Selected="true"></telerik:RadComboBoxItem>
                                                            <telerik:RadComboBoxItem Text="View" Value="View" ImageUrl="~/Images/DetailedView.png" runat="server"></telerik:RadComboBoxItem>
                                                            <telerik:RadComboBoxItem ImageUrl="~/Images/RecordEdit.png" Text="Edit" Value="Edit" runat="server"></telerik:RadComboBoxItem>
                                                        </Items>
                                                    </telerik:RadComboBox>--%>
                                                     <asp:DropDownList ID="ddlMenu"  OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged" runat="server" EnableEmbeddedSkins="false" AutoPostBack="true"
                                                     Width="120px" AppendDataBoundItems="true" CssClass="cmbField" >
                                                     <items>
                                                     <asp:ListItem ImageUrl="~/Images/Select.png" Text="Select" Value="0" Selected="true"></asp:ListItem>
                                                     <asp:ListItem Text="View" Value="View" ImageUrl="~/Images/DetailedView.png" runat="server"></asp:ListItem>
                                                     <asp:ListItem ImageUrl="~/Images/RecordEdit.png" Text="Edit" Value="Edit" runat="server" ></asp:ListItem>
                                                     </items>
                                                     </asp:DropDownList>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="RMName" SortExpression="RMName" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Name" UniqueName="RMName">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="AR_StaffCode" SortExpression="AR_StaffCode" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Employee Code" UniqueName="AR_StaffCode">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SubBrokerCode" SortExpression="SubBrokerCode"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                AllowFiltering="true" HeaderText="SubBroker Code" UniqueName="SubBrokerCode">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="AR_Email" SortExpression="AR_Email" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Email" UniqueName="AR_Email">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="AR_Mobile" SortExpression="AR_Mobile" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                                HeaderText="Mobile" UniqueName="AR_Mobile">
                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="Titles" SortExpression="Titles" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Title" UniqueName="Titles">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="120px"  DataField="BranchList" SortExpression="BranchList" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Branch" UniqueName="Branch">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="130px" Visible="true" DataField="ReportingManagerName" SortExpression="ReportingManagerName"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                AllowFiltering="true" HeaderText="Reporting To" UniqueName="ReportingManagerName">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="130px" Visible="true" DataField="ReportingManagerTitle" SortExpression="ReportingManagerName"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                AllowFiltering="true" HeaderText="Reporting Title" UniqueName="ReportingManagerTitle">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="AH_Teamname" SortExpression="AH_Teamname" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Team" UniqueName="AH_Teamname">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="AH_ChannelName" SortExpression="AH_ChannelName" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Channel" UniqueName="AH_ChannelName">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="ClusterManager" SortExpression="ClusterManager" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Cluster Manager" UniqueName="ClusterManager">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="AreaManager" SortExpression="AreaManager" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Area Manager" UniqueName="AreaManager">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="ZonalManagerName" SortExpression="ZonalManagerName"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                AllowFiltering="true" HeaderText="Zonal Manager" UniqueName="ZonalManagerName">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="DeputyHead" SortExpression="DeputyHead"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                AllowFiltering="true" HeaderText="Deputy Head" UniqueName="DeputyHead">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="ar_rmid" SortExpression="ar_rmid" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Id" UniqueName="ar_rmid">
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
