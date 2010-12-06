<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BMLeftpane.ascx.cs"
    Inherits="WealthERP.UserManagement.BMLeftpane" %>
     <%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<table width="100%">
<tr><td>
<telerik:RadPanelBar ID="RadPanelBar1" Runat="server" 
    onitemclick="RadPanelBar1_ItemClick" ExpandAnimation-Type="InCubic" ExpandMode="MultipleExpandedItems" Width="130%" EnableEmbeddedSkins="false" Skin="Touchbase" >
            <Items>
                <telerik:RadPanelItem runat="server" Text="Switch Roles" Value="SwitchRole">
                </telerik:RadPanelItem>
                <telerik:RadPanelItem runat="server" Text="Home" Value="Dashboard">
                </telerik:RadPanelItem>
                <telerik:RadPanelItem runat="server" Text="Staff" Value="Staff">
                </telerik:RadPanelItem>
                <telerik:RadPanelItem runat="server" Text="Customer" Value="Customer">
                </telerik:RadPanelItem>
                <telerik:RadPanelItem runat="server" Text="MF" Value="MF">
                <Items>
                <telerik:RadPanelItem runat="server" Text="MIS" Value="MFMIS">
                </telerik:RadPanelItem>
                </Items>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem runat="server" Text="Equity" Value="Equity">
                <Items>
                <telerik:RadPanelItem runat="server" Text="MIS" Value="EQMIS">
                </telerik:RadPanelItem>
                </Items>
                </telerik:RadPanelItem>
            </Items>
</telerik:RadPanelBar>
</td></tr>
</table>