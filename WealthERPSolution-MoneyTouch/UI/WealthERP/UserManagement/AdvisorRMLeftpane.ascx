<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisorRMLeftpane.ascx.cs"
    Inherits="WealthERP.UserManagement.AdvisorRMLeftpane" %>
    <%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
    
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<table width="100%">
<tr><td>
<telerik:RadPanelBar ID="RadPanelBar1" Runat="server" 
    onitemclick="RadPanelBar1_ItemClick" ExpandAnimation-Type="InCubic" ExpandMode="MultipleExpandedItems" Width="130%" EnableEmbeddedSkins="false" Skin="Touchbase" >
    <Items>
        <telerik:RadPanelItem runat="server" Text="Switch Role" Value="SwitchRole" Expanded="true" PostBack="false">
            <Items>
                <telerik:RadPanelItem runat="server" Text="Advisor" Value="Advisor">
                </telerik:RadPanelItem>
                <telerik:RadPanelItem runat="server" Text="RM" Value="RM">
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelItem>
    </Items>
</telerik:RadPanelBar>
</td></tr>
</table>

