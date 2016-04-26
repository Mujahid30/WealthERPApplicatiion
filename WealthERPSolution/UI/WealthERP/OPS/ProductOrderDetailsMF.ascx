<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductOrderDetailsMF.ascx.cs"
    Inherits="WealthERP.OPS.ProductOrderDetailsMF" EnableViewState="true" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Life Insurance
                        </td>
                        <td align="right" id="tdExport" runat="server" style="padding-bottom: 2px;">
                            <asp:ImageButton ID="btnExportFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                Visible="false" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                OnClick="btnExportFilteredData_OnClick" OnClientClick="setFormat('excel')" Height="25px"
                                Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td style="width: 3%">
            <asp:Label ID="lblInsuranceType" runat="server" CssClass="FieldName" Text="Insurance Type:"></asp:Label>
            <asp:DropDownList ID="ddlInsurance" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Life Insurance" Value="LI"></asp:ListItem>
                <asp:ListItem Text="General Insurance" Value="GI"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="rightField" style="width: 15%">
            <asp:Button ID="btnSubmit" runat="server" Text="GO" CssClass="PCGButton" OnClick="btnSubmit_Click"
                ValidationGroup="Submit" />
        </td>
    </tr>
</table>
<asp:Panel ID="pnlInsuranceBook" runat="server" class="Landscape" Width="99%" ScrollBars="Horizontal"
    runat="server" Visible="false">
    <table>
        <tr>
            <td>
                <telerik:RadGrid ID="gvrInsurance" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AllowAutomaticInserts="false"
                    OnNeedDataSource="gvrInsurance_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="LI Details">
                    </ExportSettings>
                    <MasterTableView AllowFilteringByColumn="false" Width="100%" AllowMultiColumnSorting="True"
                        AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridBoundColumn DataField="CO_OrderId" AllowFiltering="false" HeaderText="Order No."
                                UniqueName="PolicyNo" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ApplicationNumber" AllowFiltering="false"
                                HeaderText="Application Number." UniqueName="PolicyNo" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Name" AllowFiltering="false" HeaderText="Name"
                                UniqueName="ActiveLevel" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XII_InsuranceIssuerName" AllowFiltering="false"
                                HeaderText="Policy Issuer" UniqueName="ActiveLevel" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Particulars" AllowFiltering="false" HeaderText="Scheme"
                                UniqueName="ActiveLevel">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PAIC_AssetInstrumentCategoryName" AllowFiltering="false"
                                HeaderText="Insurance Type" UniqueName="ActiveLevel">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CIOD_SumAssured" AllowFiltering="false" HeaderText="Sum Assured"
                                UniqueName="CINP_SumAssured" Aggregate="Sum" DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataFormatString="{0:N0}" DataField="XF_Frequency" AllowFiltering="false"
                                HeaderText="Premium Frequency" UniqueName="Premium Frequency">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CIOD_PolicyMaturityDate" AllowFiltering="false"
                                HeaderText="Maturity Date" DataType="System.DateTime" DataFormatString="{0:d}"
                                UniqueName="ActiveLevel">
                                <ItemStyle Width="" HorizontalAlign="center" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AAC_AgentCode" SortExpression="AAC_AgentCode"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="SubBroker Code" UniqueName="AAC_AgentCode">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                         
                            <telerik:GridBoundColumn DataField="AssociatesName" SortExpression="AssociatesName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Associates" UniqueName="AssociatesName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DeputyHead" SortExpression="DeputyHead" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                HeaderStyle-Width="160px" HeaderText="Deputy Manager" UniqueName="DeputyHead">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ZonalManagerName" SortExpression="ZonalManagerName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Zonal Manager" UniqueName="ZonalManagerName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AreaManager" SortExpression="AreaManager" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                HeaderStyle-Width="160px" HeaderText="Area Manager" UniqueName="AreaManager">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ClusterManager" SortExpression="ClusterManager"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Cluster Manager"
                                UniqueName="ClusterManager">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ChannelName" SortExpression="ChannelName" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                HeaderStyle-Width="160px" HeaderText="Channel" UniqueName="ChannelName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Titles" SortExpression="Titles" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                HeaderStyle-Width="160px" HeaderText="Titles" UniqueName="Titles">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ReportingManagerName" SortExpression="ReportingManagerName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Reporting Manager"
                                UniqueName="ReportingManagerName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_CreatedBy" AllowFiltering="false" HeaderText="Created By"
                                UniqueName="PolicyNo" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_CreatedOn" AllowFiltering="false" HeaderText="Created On"
                                DataType="System.DateTime" DataFormatString="{0:d}" UniqueName="ActiveLevel">
                                <ItemStyle Width="" HorizontalAlign="center" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ModifiedBy" AllowFiltering="false" HeaderText="Modified By"
                                UniqueName="PolicyNo" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ModifiedOn" AllowFiltering="false" HeaderText="Modified On"
                                DataType="System.DateTime" DataFormatString="{0:d}" UniqueName="ActiveLevel">
                                <ItemStyle Width="" HorizontalAlign="center" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="AgentCode" runat="server" />
