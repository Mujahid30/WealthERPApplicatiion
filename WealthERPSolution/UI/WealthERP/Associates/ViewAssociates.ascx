<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewAssociates.ascx.cs"
    Inherits="WealthERP.Associates.ViewAssociates" %>
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            View Associates
                        </td>
                        <td align="right" id="tdExport" runat="server" style="padding-bottom: 2px;">
                          <asp:ImageButton ID="imgExportAssociates" runat="server" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                 AlternateText="Excel" ToolTip="Export To Excel" Height="20px"
                                Width="25px" Visible="false"  OnClick="imgExportAssociates_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td>
            <br />
            <asp:Label ID="lblbranch" runat="server" Font-Bold="true" Font-Size="Small" CssClass="FieldName"
                Text="Branch: "></asp:Label>
            <asp:DropDownList ID="ddlBMBranch" runat="server" 
                CssClass="cmbField" Style="vertical-align: middle" AutoPostBack="true" 
                onselectedindexchanged="ddlBMBranch_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
        <tr id="trErrorMessage" align="center" style="width: 100%" runat="server">
        <td align="center" style="width: 100%">
            <div class="failure-msg" style="text-align: center" align="center">
                No  Records found!!!!
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Panel ID="pnlAssociatesView" runat="server" ScrollBars="Horizontal" Width="98%"
                Visible="true">
                <table>
                    <tr>
                        <td>
                            <div runat="server" id="divAssociatesView" style="margin: 2px; width: 640px;">
                                <telerik:RadGrid ID="gvViewAssociates" runat="server" GridLines="None" AutoGenerateColumns="False"
                                    PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                    Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                    AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" OnNeedDataSource="gvAssociates_NeedDataSource"
                                    EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true">
                                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                        FileName="ViewAssociates" Excel-Format="ExcelML">
                                    </ExportSettings>
                                    <MasterTableView Width="100%" DataKeyNames="AA_AdviserAssociateId,WWFSM_StepCode,AA_StepStatus" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                        CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                                        GroupLoadMode="Client" ShowGroupFooter="true">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Request Id" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Width="80px" AllowFiltering="true" DataField="AA_AdviserAssociateId"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LnkRQ" runat="server" CssClass="CmbField" OnClick="LnkRQ_Click"
                                                        Text='<%#Eval("AA_AdviserAssociateId") %>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="AA_RequestDate" AllowFiltering="false" HeaderStyle-Width="130px"
                                                HeaderText="Request Date/Time">
                                                <ItemStyle Width="" Wrap="false" VerticalAlign="Top" HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Name" DataField="AA_ContactPersonName"
                                                UniqueName="AA_ContactPersonName" SortExpression="AA_ContactPersonName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Associate Code" DataField="AAC_AgentCode"
                                                UniqueName="AAC_AgentCode" SortExpression="AAC_AgentCode" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="XS_Status" AllowFiltering="false" AutoPostBackOnFilter="true"
                                                ShowFilterIcon="False" HeaderStyle-Width="90px" HeaderText="Status">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Step Code" DataField="WWFSM_StepCode"
                                                UniqueName="WWFSM_StepCode" SortExpression="WWFSM_StepCode" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains" Visible="false">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="StepStatus" DataField="AA_StepStatus"
                                                UniqueName="AA_StepStatus" SortExpression="AA_StepStatus" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains" Visible="false">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Current Stage" DataField="WWFSM_StepName"
                                                UniqueName="WWFSM_StepName" SortExpression="WWFSM_StepName" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Role" DataField="UR_RoleName"
                                                UniqueName="UR_RoleName" SortExpression="UR_RoleName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains" Visible="false">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Branch" DataField="AB_BranchName"
                                                UniqueName="AB_BranchName" SortExpression="AB_BranchName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="RM" DataField="RM"
                                                UniqueName="RM" SortExpression="RM" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Email" DataField="AA_Email"
                                                HeaderStyle-HorizontalAlign="Left" UniqueName="AA_Email" SortExpression="AA_Email"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Phono No." DataField="AA_Mobile"
                                                HeaderStyle-HorizontalAlign="Left" UniqueName="AA_Mobile" SortExpression="AA_Mobile"
                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
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
