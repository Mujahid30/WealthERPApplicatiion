<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssociateCustomerList.ascx.cs"
    Inherits="WealthERP.Advisor.AssociateCustomerList" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<script language="javascript" type="text/javascript">
    function GridCreated(sender, args) {
        var scrollArea = sender.GridDataDiv;
        var dataHeight = sender.get_masterTableView().get_element().clientHeight;
        if (dataHeight < 380) {
            scrollArea.style.height = dataHeight + 17 + "px";
        }
    }
</script>

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            Associate Customer List(Offline)
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="imgExportButton" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                Visible="true" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                OnClick="btnExportData_OnClick" OnClientClick="setFormat('excel')" Height="22px"
                                Width="25px">
                            </asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>

<asp:UpdatePanel ID="upCMGrid" runat="server">
    <ContentTemplate>
        <div id="divAssocCustList" runat="server">
            <telerik:RadGrid ID="gvAssocCustList" runat="server" AllowAutomaticDeletes="false" Width="100%" 
                EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true"
                GridLines="none" AllowAutomaticInserts="false" Skin="Telerik" EnableHeaderContextMenu="true"
                OnNeedDataSource="gvAssocCustList_OnNeedDataSource" OnPageIndexChanged="gvAssocCustList_PageIndexChanged">
                <ExportSettings HideStructureColumns="true"></ExportSettings>
                <MasterTableView DataKeyNames="C_CustomerId" AllowMultiColumnSorting="True" AutoGenerateColumns="false" Width="100%" >
                    <Columns>
                        <telerik:GridBoundColumn DataField="CustomerName" UniqueName="CustomerName" HeaderText="Name"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="140px"
                            SortExpression="CustomerName" FilterControlWidth="120px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="140px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AA_ContactPersonName" UniqueName="AA_ContactPersonName" HeaderText="Agent Associate"
                            AutoPostBackOnFilter="true" SortExpression="AA_ContactPersonName" ShowFilterIcon="false" AllowFiltering="true"
                            HeaderStyle-Width="90px" FilterControlWidth="90px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AAC_AgentCode" UniqueName="AAC_AgentCode" HeaderText="Agent Code"
                            AutoPostBackOnFilter="true" SortExpression="AAC_AgentCode" ShowFilterIcon="false" AllowFiltering="true"
                            HeaderStyle-Width="90px" FilterControlWidth="90px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Titles" UniqueName="Titles" HeaderText="Title"
                            AutoPostBackOnFilter="true" SortExpression="Titles" ShowFilterIcon="false" AllowFiltering="true"
                            HeaderStyle-Width="140px" FilterControlWidth="90px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn DataField="AA_StartDate" ReadOnly="true" 
                            DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px" ShowFilterIcon="false" AllowFiltering="true" 
                            AutoPostBackOnFilter="true" HeaderText="From" SortExpression="AA_StartDate" UniqueName="AA_StartDate" 
                            CurrentFilterFunction="EqualTo" >
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridDateTimeColumn DataField="AA_EndDate"
                            DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px" ShowFilterIcon="false"
                             CurrentFilterFunction="EqualTo" HeaderText="To" AllowFiltering="true" SortExpression="AA_EndDate" 
                             UniqueName="AA_EndDate" AutoPostBackOnFilter="true" >
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                        </telerik:GridDateTimeColumn>
                        
                         <telerik:GridBoundColumn DataField="[DeputyHead]" UniqueName="[DeputyHead]" HeaderText="Deputy Head"
                            AutoPostBackOnFilter="true" SortExpression="[DeputyHead]" ShowFilterIcon="false" AllowFiltering="true"
                            HeaderStyle-Width="140px" FilterControlWidth="90px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="ZonalManagerName" UniqueName="ZonalManagerName" HeaderText="Zonal Manager"
                            AutoPostBackOnFilter="true" SortExpression="ZonalManagerName" ShowFilterIcon="false" AllowFiltering="true"
                            HeaderStyle-Width="140px" FilterControlWidth="90px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="AreaManager" UniqueName="AreaManager" HeaderText="Area Manager"
                            AutoPostBackOnFilter="true" SortExpression="AreaManager" ShowFilterIcon="false" AllowFiltering="true"
                            HeaderStyle-Width="140px" FilterControlWidth="90px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ClusterManager" UniqueName="ClusterManager" HeaderText="Cluster Manager"
                            AutoPostBackOnFilter="true" SortExpression="ClusterManager" ShowFilterIcon="false" AllowFiltering="true"
                            HeaderStyle-Width="140px" FilterControlWidth="90px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ReportingManagerName" UniqueName="ReportingManagerName" HeaderText="Channel Manager"
                            AutoPostBackOnFilter="true" SortExpression="ReportingManagerName" ShowFilterIcon="false" AllowFiltering="true"
                            HeaderStyle-Width="140px" FilterControlWidth="90px" CurrentFilterFunction="Contains">
                            <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                        </telerik:GridBoundColumn>
                        
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="380px" />
                    <ClientEvents OnGridCreated="GridCreated" />
                    <Resizing AllowColumnResize="true" />
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </div>
    </ContentTemplate>
   <Triggers>
        <asp:PostBackTrigger ControlID="imgExportButton" />
    </Triggers>
</asp:UpdatePanel>