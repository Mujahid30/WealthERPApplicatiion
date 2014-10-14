<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewBranches.ascx.cs"
    Inherits="WealthERP.Advisor.ViewBranches" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div id="divBranchDetails" runat="server" style="width: 1100px; overflow: scroll">
    <table width="100%" class="TableBackground">
        <tr>
            <td>
                <div class="divPageHeading">
                    <table cellspacing="0" cellpadding="3" width="100%">
                        <tr>
                            <td align="left">
                                Branch/Association
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <div id="msgRecordStatus" runat="server" class="success-msg" align="center">
                    Record Deleted Successfully...!
                </div>
            </td>
        </tr>
    </table>
    <table class="TableBackground" style="width: 100%">
        <tr>
            <td>
                <asp:Label ID="lblMessage" runat="server" Text="No Records Found..." CssClass="Error"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
                <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvBranchList" runat="server" OnSorting="gvBranchList_Sorting" AllowSorting="True"
                    AutoGenerateColumns="False" DataKeyNames="BranchId" OnRowCommand="gvBranchlist_RowCommand"
                    OnRowEditing="gvBranchList_RowEditing" Font-Size="Small" CssClass="GridViewStyle"
                    ShowFooter="true">
                    <FooterStyle CssClass="FooterStyle" />
                    <PagerStyle CssClass="PagerStyle " />
                    <%-- <EmptyDataTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Text="Edit" Value="Edit">Edit Details</asp:ListItem>
                    <asp:ListItem Text="View" Value="View">View Details</asp:ListItem>
                    </asp:DropDownList>
                </EmptyDataTemplate>--%>
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlMenu" AutoPostBack="true" CssClass="GridViewCmbField" runat="server"
                                    OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged">
                                    <asp:ListItem>Select </asp:ListItem>
                                    <asp:ListItem Text="Edit" Value="Edit">Edit </asp:ListItem>
                                    <asp:ListItem Text="View" Value="View">View</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="BranchName" HeaderText="Name" />
                        <asp:BoundField DataField="BranchCode" HeaderText="Code" />
                        <asp:BoundField DataField="BranchHead" HeaderText="Head" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Phone" HeaderText="Phone" />
                        <asp:BoundField DataField="BranchType" HeaderText="Type" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table id="tblPager" runat="server" style="width: 100%" visible="true">
        <tr>
            <td>
                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnCount" runat="server" />
    <asp:HiddenField ID="hdnCurrentPage" runat="server" />
    <asp:HiddenField ID="hdnSort" runat="server" Value="AB_BranchName ASC" />
</div>
<table id="tblClusterZone" runat="server" width="100%" class="TableBackground">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Branch Details
                        </td>
                        <td align="right">
                            <asp:ImageButton Visible="false" ID="btnExportFilteredDataForZoneClusterdetails"
                                ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png" runat="server" AlternateText="Excel"
                                ToolTip="Export To Excel" OnClick="btnExportFilteredDataForZoneClusterdetails_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td style="padding-top: 8px;">
            <asp:Panel ID="pnlZoneCluster" ScrollBars="Horizontal" runat="server">
                <div runat="server" id="divZoneCluster" style="margin: 2px; width: 640px;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <telerik:RadGrid ID="gvZoneClusterdetails" runat="server" CssClass="RadGrid" GridLines="None"
                                enableloadondemand="True" Width="120%" AllowSorting="True" PagerStyle-AlwaysVisible="true"
                                AllowPaging="true" AutoGenerateColumns="false" ShowStatusBar="true" PageSize="5"
                                AllowAutomaticInserts="false" AllowAutomaticUpdates="false" Skin="Telerik" OnNeedDataSource="gvZoneClusterdetails_NeedDataSource"
                                EnableEmbeddedSkins="false" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true"
                                AllowFilteringByColumn="true" ShowFooter="false">
                                <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="ZoneClusterdetailslist">
                                </ExportSettings>
                                <MasterTableView ShowGroupFooter="true" EditMode="EditForms" GroupLoadMode="Client"
                                    CommandItemSettings-ShowRefreshButton="false" DataKeyNames="AB_BranchId">
                                    <%--<GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="ZoneName" />
                                            </GroupByFields>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldName="ZoneName" FieldAlias="Zone" />
                                            </SelectFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>--%>
                                    <Columns>
                                        <telerik:GridBoundColumn UniqueName="AB_BranchId" HeaderStyle-Width="120px" HeaderText="Zone"
                                            DataField="AB_BranchId" SortExpression="AB_BranchId" AllowFiltering="true" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true" Visible="false">
                                            <HeaderStyle></HeaderStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn ItemStyle-Width="100Px" AllowFiltering="false">
                                            <ItemTemplate>
                                                <%--       <telerik:RadComboBox ID="ddlMenuzcd" UniqueName="ddlMenuzcd" OnSelectedIndexChanged="ddlMenuzcd_SelectedIndexChanged"
                                                        runat="server" EnableEmbeddedSkins="true" Skin ="Telerik"
                                                        AllowCustomText="true" Width="100px" AutoPostBack="true">
                                                        <Items>
                                                            <telerik:RadComboBoxItem  Text="Select" Value="0" Selected="true"></telerik:RadComboBoxItem>
                                                            <telerik:RadComboBoxItem Text="View" Value="View"  runat="server"></telerik:RadComboBoxItem>
                                                            <telerik:RadComboBoxItem  Text="Edit" Value="Edit" runat="server"></telerik:RadComboBoxItem>
                                                        </Items>
                                                  </telerik:RadComboBox>--%>
                                                <asp:DropDownList ID="ddlMenuzcd" OnSelectedIndexChanged="ddlMenuzcd_SelectedIndexChanged"
                                                    runat="server" EnableEmbeddedSkins="false" AutoPostBack="true" Width="100px"
                                                    AppendDataBoundItems="true" CssClass="cmbField">
                                                    <Items>
                                                        <asp:ListItem Text="Select" Value="0" Selected="true"></asp:ListItem>
                                                        <asp:ListItem Text="View" Value="View" ImageUrl="~/Images/DetailedView.png" runat="server"></asp:ListItem>
                                                        <asp:ListItem Text="Edit" Value="Edit" runat="server"></asp:ListItem>
                                                    </Items>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn UniqueName="ZoneName" HeaderStyle-Width="120px" HeaderText="Zone"
                                            DataField="ZoneName" SortExpression="ZoneName" AllowFiltering="true" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true" Aggregate="Count" FooterText="Row Count : ">
                                            <HeaderStyle></HeaderStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="ClusterName" HeaderStyle-Width="120px" HeaderText="Cluster"
                                            DataField="ClusterName" SortExpression="ClusterName" AllowFiltering="true" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true">
                                            <HeaderStyle></HeaderStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="true" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                                            HeaderText="Branch" UniqueName="AB_BranchName" DataField="AB_BranchName">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBranchName" OnClick="lnkBranchName_OnClick" runat="server"
                                                    Text='<%#Eval("AB_BranchName")%>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn UniqueName="AB_BranchCode" HeaderStyle-Width="120px" HeaderText="Code"
                                            DataField="AB_BranchCode" SortExpression="AB_BranchCode" AllowFiltering="true"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                            <HeaderStyle></HeaderStyle>
                                        </telerik:GridBoundColumn>
                                        <%--<asp:BoundField DataField="AAC_AgentCode" HeaderText="AgentCode" />--%>
                                        <telerik:GridBoundColumn UniqueName="AAC_AgentCode" HeaderStyle-Width="120px" HeaderText="SubBroker Code"
                                            DataField="AAC_AgentCode" SortExpression="AAC_AgentCode" AllowFiltering="true"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                            <HeaderStyle></HeaderStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="AR_FirstName" HeaderStyle-Width="120px" HeaderText="Head"
                                            DataField="AR_FirstName" SortExpression="AR_FirstName" AllowFiltering="true"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                            <HeaderStyle></HeaderStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="AB_Email" HeaderStyle-Width="180px" HeaderText="Email"
                                            DataField="AB_Email" SortExpression="AB_Email" AllowFiltering="true" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true">
                                            <HeaderStyle></HeaderStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="AB_Phone1" HeaderStyle-Width="100px" HeaderText="Phone"
                                            DataField="AB_Phone1" SortExpression="AB_Phone1" AllowFiltering="true" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true">
                                            <HeaderStyle></HeaderStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="XABRT_BranchType" HeaderStyle-Width="100px"
                                            HeaderText="Type" DataField="XABRT_BranchType" SortExpression="XABRT_BranchType"
                                            AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                            <HeaderStyle></HeaderStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="AB_AddressLine1" HeaderStyle-Width="180px" HeaderText="AddressLine1"
                                            DataField="AB_AddressLine1" SortExpression="AB_AddressLine1" AllowFiltering="true"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                            <HeaderStyle></HeaderStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="AB_AddressLine2" HeaderStyle-Width="180px" HeaderText="AddressLine2"
                                            DataField="AB_AddressLine2" SortExpression="AB_AddressLine2" AllowFiltering="true"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                            <HeaderStyle></HeaderStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="AB_AddressLine3" HeaderStyle-Width="180px" HeaderText="AddressLine3"
                                            DataField="AB_AddressLine3" SortExpression="AB_AddressLine3" AllowFiltering="true"
                                            ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                            <HeaderStyle></HeaderStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="AB_City" HeaderStyle-Width="100px" HeaderText="City"
                                            DataField="AB_City" SortExpression="AB_City" AllowFiltering="true" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true">
                                            <HeaderStyle></HeaderStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="AB_PinCode" HeaderStyle-Width="100px" HeaderText="PinCode"
                                            DataField="AB_PinCode" SortExpression="AB_PinCode" AllowFiltering="true" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true">
                                            <HeaderStyle></HeaderStyle>
                                        </telerik:GridBoundColumn>
                                        <%--        <telerik:GridBoundColumn UniqueName="AB_State" HeaderStyle-Width="100px" HeaderText="State"
                    DataField="AB_State" SortExpression="AB_State" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true"  >
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>--%>
                                        <telerik:GridBoundColumn UniqueName="AB_Country" HeaderStyle-Width="100px" HeaderText="Country"
                                            DataField="AB_Country" SortExpression="AB_Country" AllowFiltering="true" ShowFilterIcon="false"
                                            AutoPostBackOnFilter="true">
                                            <HeaderStyle></HeaderStyle>
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <HeaderStyle Width="110px" />
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                    <Resizing AllowColumnResize="true" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>
        </td>
    </tr>
</table>
