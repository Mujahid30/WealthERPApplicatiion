<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BMRMLeftpane.ascx.cs"
    Inherits="WealthERP.UserManagement.BMRMLeftpane" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
<telerik:RadPanelBar ID="RadPanelBar1" runat="server" ExpandAnimation-Type="InCubic"
    ExpandMode="SingleExpandedItem" Width="210px" EnableEmbeddedSkins="false" 
    Skin="Touchbase" onitemclick="RadPanelBar1_ItemClick">
    <Items>
        <telerik:RadPanelItem Text="Roles" Value="Roles" PostBack="false">
            <Items>
                <telerik:RadPanelItem Text="Branch Manager" Value="Branch Manager">
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="RM" Value="RM">
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelItem>
    </Items>
</telerik:RadPanelBar>
