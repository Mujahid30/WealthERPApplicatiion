<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineClientAccess.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineClientAccess" %>
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
                            Online Client Access
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="imgexportButton" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                Visible="true" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                OnClick="btnExportData_OnClick" OnClientClick="setFormat('excel')" Height="22px"
                                Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
    </tr>
</table>
<asp:Panel ID="KYClist" runat="server" ScrollBars="Horizontal" Width="100%">
    <table width="100%" cellspacing="0" cellpadding="1">
        <tr>
            <td>
                <telerik:RadGrid ID="gvKYCStatusList" runat="server" fAllowAutomaticDeletes="false"
                    EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                    ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true"
                    GridLines="none" AllowAutomaticInserts="false" Skin="Telerik" EnableHeaderContextMenu="true"
                    OnNeedDataSource="gvKYCStatusList_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="CustomerId" Width="99%" AllowMultiColumnSorting="True"
                        AutoGenerateColumns="false" PageSize="20">
                        <Columns>
                            <telerik:GridBoundColumn DataField="CustomerId" UniqueName="CustomerId" HeaderText="System Id"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                                SortExpression="C_CustomerId" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Name" UniqueName="Name" HeaderText="Name" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="ClientName"
                                FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PAN" UniqueName="PAN" HeaderText="PAN" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="PAN"
                                FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="KYCStatus" UniqueName="KYCStatus" HeaderText="KYC"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="67px"
                                SortExpression="KYCStatus" FilterControlWidth="50px" CurrentFilterFunction="Contains"
                                Visible="true">
                                <ItemStyle Width="67px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Holding" UniqueName="Holding" HeaderText="Holding"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="80px"
                                SortExpression="Holding" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="70px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="true" DataField="Privilege" UniqueName="Privilege"
                                HeaderText="Access Privilege" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                HeaderStyle-Width="100px" SortExpression="Privilege" FilterControlWidth="80px"
                                CurrentFilterFunction="Contains">
                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SecondHolder" UniqueName="SecondHolder" HeaderText="2nd Holder"
                                SortExpression="2nd Holder" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                                FilterControlWidth="80px" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemStyle Width="80px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SecondHolderPAN" UniqueName="SecondHolderPAN"
                                HeaderText="2nd PAN" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                HeaderStyle-Width="100px" SortExpression="SecondHolderPAN" FilterControlWidth="80px"
                                CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SecondHolderKYC" UniqueName="SecondHolderKYC"
                                HeaderText="2nd KYC" AutoPostBackOnFilter="true" SortExpression="SecondHolderKYC"
                                ShowFilterIcon="false" AllowFiltering="true" HeaderStyle-Width="100px" FilterControlWidth="80px">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ThirdHolder" UniqueName="ThirdHolder" HeaderText="3rd Holder"
                                SortExpression="ThirdHolder" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                                AllowFiltering="true" HeaderStyle-Width="100px" FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="true" DataField="ThirdHolderPAN" UniqueName="ThirdHolderPAN"
                                HeaderText="3rd PAN" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                HeaderStyle-Width="100px" SortExpression="ThirdHolderPAN" FilterControlWidth="70px"
                                CurrentFilterFunction="Contains">
                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="true" DataField="SThirdHolderKYC" UniqueName="SThirdHolderKYC"
                                HeaderText="3rd KYC" ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true"
                                HeaderStyle-Width="100px" SortExpression="SThirdHolderKYC" FilterControlWidth="70px"
                                CurrentFilterFunction="Contains">
                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
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
