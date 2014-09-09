<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HierarchySetup.ascx.cs"
    Inherits="WealthERP.Advisor.HierarchySetup" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Hierarchy Setup
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<br />
<table w>
    <tr>
        <td>
            <%--EditMode="PopUp" CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="false"  OnItemDataBound="gvHirarchy_ItemDataBound" OnItemCommand="gvHirarchy_ItemCommand"--%>
            <telerik:RadGrid ID="gvHirarchy" runat="server" CssClass="RadGrid" GridLines="None"
                Width="700px" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false"
                ShowStatusBar="true" AllowAutomaticDeletes="false" AllowAutomaticInserts="false"
                OnNeedDataSource="gvHirarchy_NeedDataSource" AllowAutomaticUpdates="false" Skin="Telerik"
                EnableEmbeddedSkins="false" 
                EnableHeaderContextMenu="false" EnableHeaderContextFilterMenu="false" AllowFilteringByColumn="true">
                <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="ExistMFInvestlist">
                </ExportSettings>
                <MasterTableView DataKeyNames="AH_Id,AH_HierarchyName,AH_TitleId,AH_Teamname,AH_TeamId,AH_ReportsToId,AH_ReportsTo,AH_ChannelName,AH_ChannelId,AH_Sequence"
                    CommandItemSettings-AddNewRecordText="Add Hierarchy Setup">
                    <Columns>
                       <%-- <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                            UpdateText="Update">
                        </telerik:GridEditCommandColumn>--%>
                        <telerik:GridBoundColumn Visible="false" UniqueName="AH_Id" HeaderText="Type" DataField="AH_Id"
                            SortExpression="AH_Id" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="AH_HierarchyName" HeaderText="Role/Title" DataField="AH_HierarchyName"
                            SortExpression="AH_HierarchyName" AllowFiltering="true" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="AH_Teamname" HeaderText="Team" DataField="AH_Teamname"
                            SortExpression="AH_Teamname" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="AH_TeamId" Visible="false" HeaderText="Teamid"
                            DataField="AH_TeamId" SortExpression="AH_TeamId" AllowFiltering="true" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Visible="false" UniqueName="AH_ReportsToId" HeaderText="ReportsToId"
                            DataField="AH_ReportsToId" SortExpression="Name" AllowFiltering="true" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" UniqueName="AH_ReportsTo" HeaderText="ReportingTo"
                            DataField="AH_ReportsTo" SortExpression="AH_ReportsTo" AllowFiltering="true"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="AH_ChannelName" HeaderText="Channel" DataField="AH_ChannelName"
                            SortExpression="AH_ChannelName" AllowFiltering="true" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="AH_Sequence" HeaderText="Seq in Channel" DataField="AH_Sequence"
                            SortExpression="AH_Sequence" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <HeaderStyle></HeaderStyle>
                        </telerik:GridBoundColumn>
                       <%-- <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete this Hierarchy?"
                            ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                            Text="Delete">
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>--%>
                    </Columns>
                    <HeaderStyle Width="100px" />
                </MasterTableView>
                <ClientSettings ReorderColumnsOnClient="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
                    <Scrolling AllowScroll="false" />
                    <Resizing AllowColumnResize="true" />
                    <Selecting AllowRowSelect="true" />
                </ClientSettings>
            </telerik:RadGrid>
        </td>
    </tr>
</table>
