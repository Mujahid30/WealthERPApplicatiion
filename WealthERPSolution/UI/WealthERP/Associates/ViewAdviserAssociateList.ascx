<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewAdviserAssociateList.ascx.cs"
    Inherits="WealthERP.Associates.ViewAdviserAssociateList" %>
<telerik:radscriptmanager id="scptMgr" runat="server">
</telerik:radscriptmanager>
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            View Associate List
                        </td>
                        <td align="right" id="tdExport" runat="server" style="padding-bottom: 2px;">
                            <asp:ImageButton ID="imgViewAssoList" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" Height="20px"
                                OnClick="btnExportFilteredData_OnClick" Width="25px" Visible="true"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>

            <asp:Panel ID="pnlAdviserAssociateList" runat="server" ScrollBars="Both" Width="98%"
                Height="400Px" Visible="true">
                <table width="80%">
                    <tr>
                        <td>
                            <div runat="server" id="divAdviserAssociateList" style="width: 80%;">
                                <telerik:radgrid id="gvAdviserAssociateList" runat="server" allowautomaticdeletes="false"
                                    pagesize="10" enableembeddedskins="false" allowfilteringbycolumn="true" autogeneratecolumns="False"
                                    showstatusbar="false" showfooter="false" allowpaging="true" allowsorting="true"
                                    gridlines="none" allowautomaticinserts="false" skin="Telerik" enableheadercontextmenu="true"
                                    onneeddatasource="gvAdviserAssociateList_OnNeedDataSource" onitemdatabound="gvAdviserAssociateList_ItemDataBound">
                                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="ViewAssociateList"
                                        Excel-Format="ExcelML">
                                    </ExportSettings>
                                    <MasterTableView Width="80%" DataKeyNames="AA_AdviserAssociateId,WelcomeNotePath" AllowMultiColumnSorting="True"
                                        AutoGenerateColumns="false" CommandItemDisplay="None" GroupsDefaultExpanded="false"
                                        ExpandCollapseColumn-Groupable="true" GroupLoadMode="Client" ShowGroupFooter="true">
                                        <Columns>
                                            <telerik:GridTemplateColumn  ItemStyle-Width="80Px" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlMenu" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged"
                                                        CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" Skin="Telerik"
                                                        AllowCustomText="true" Width="120px" AutoPostBack="true">
                                                        <Items>
                                                            <asp:ListItem Text="Select" Value="0" Selected="true" />
                                                            <asp:ListItem Text="View" Value="View" />
                                                            <asp:ListItem Text="Edit" Value="Edit" />
                                                        </Items>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="SubBroker Code" DataField="SubBrokerCode"
                                                UniqueName="SubBrokerCode" SortExpression="SubBrokerCode" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Associate Name" DataField="AA_ContactPersonName"
                                                UniqueName="AA_ContactPersonName" SortExpression="AA_ContactPersonName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="PAN" DataField="AA_PAN"
                                                UniqueName="AA_PAN" SortExpression="AA_PAN" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <%--<telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="RM" DataField="RMName"
                                                UniqueName="RMName" SortExpression="RMName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>--%>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Mobile" DataField="AA_Mobile"
                                                UniqueName="AA_Mobile" SortExpression="AA_Mobile" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Email Id" DataField="AA_Email"
                                                UniqueName="AA_Email" SortExpression="AA_Email" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Address" DataField="Address"
                                                UniqueName="Address" SortExpression="Address" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn DataField="AA_AMFIregistrationNo" SortExpression="Titles" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="AMFI Registration No" UniqueName="AA_AMFIregistrationNo">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Branch" DataField="AB_BranchName"
                                                UniqueName="AB_BranchName" SortExpression="AB_BranchName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="WERPBM_BankName" SortExpression="Titles" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Bank Name" UniqueName="WERPBM_BankName">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                          <telerik:GridBoundColumn DataField="AA_AccountNum" SortExpression="Titles" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Bank Account No" UniqueName="AA_AccountNum">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CB_MICR" SortExpression="Titles" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="MICR No" UniqueName="CB_MICR">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ReportingManagerName" SortExpression="Titles" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Channel Manager Name" UniqueName="ReportingManagerName">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ClusterManager" SortExpression="ClusterManager"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                AllowFiltering="true" HeaderText="Cluster Manager" UniqueName="ClusterManager">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AreaManager" SortExpression="AreaManager" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Area Manager" UniqueName="AreaManager">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ZonalManagerName" SortExpression="ZonalManagerName"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                AllowFiltering="true" HeaderText="Zone Manager" UniqueName="ZonalManagerName">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="DeputyHead" SortExpression="DeputyHead"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                AllowFiltering="true" HeaderText="Deputy Head" UniqueName="DeputyHead">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                               <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="AAC_AssociateCategoryName" SortExpression="AAC_AssociateCategoryName"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                AllowFiltering="true" HeaderText="Associate Category" UniqueName="AAC_AssociateCategoryName">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="IsActive" SortExpression="IsActive"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                AllowFiltering="true" HeaderText="Is Active" UniqueName="IsActive">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="IsDummyAssociate" SortExpression="IsDummyAssociate"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                AllowFiltering="true" HeaderText="Is Dummy" UniqueName="IsDummy">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                               <telerik:GridTemplateColumn UniqueName="Welcome" ItemStyle-Width="100Px" AllowFiltering="false" HeaderText="Welcome Letter">
                                         
                                                <ItemTemplate>
                                                  <asp:LinkButton ID="lbtnWelcomeletter" OnClientClick="window.document.forms[0].target='_blank'; setTimeout(function(){window.document.forms[0].target='';}, 500);"
                                                   runat="server"  OnClick="lbtnWelcomeletter_OnClick" >WelcomeLetter</asp:LinkButton>
                                                      <%--   Visible='<%# Eval("WelcomeNotePath") != DBNull.Value %>' --%>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                    </ClientSettings>
                                </telerik:radgrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
      
