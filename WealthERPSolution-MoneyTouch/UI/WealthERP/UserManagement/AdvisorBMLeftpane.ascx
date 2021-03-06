﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisorBMLeftpane.ascx.cs"
    Inherits="WealthERP.UserManagement.AdvisorBMLeftpane" %>
        <%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<telerik:RadPanelBar ID="RadPanelBar1" Runat="server" 
    ExpandAnimation-Type="InCubic" ExpandMode="MultipleExpandedItems" Width="130%" 
    EnableEmbeddedSkins="false" Skin="Touchbase" 
    onitemclick="RadPanelBar1_ItemClick">
    <Items>
        <telerik:RadPanelItem runat="server" PostBack="False" Text="Roles" 
            Value="Roles">
            <Items>
                <telerik:RadPanelItem runat="server" Text="Admin" Value="Advisor">
                </telerik:RadPanelItem>
                
                <telerik:RadPanelItem runat="server" Text="Branch Manager" 
                        Value="Branch Manager">
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelItem>
    </Items>
</telerik:RadPanelBar>