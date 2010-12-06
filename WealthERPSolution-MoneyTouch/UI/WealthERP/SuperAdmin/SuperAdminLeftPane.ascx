<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SuperAdminLeftPane.ascx.cs"
    Inherits="WealthERP.SuperAdmin.SuperAdminLeftPane" %>
    <%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>


<telerik:radpanelbar id="RadPanelBar1" runat="server" expandanimation-type="InCubic"
    expandmode="SingleExpandedItem" width="210px" enableembeddedskins="false" 
    skin="Touchbase" onitemclick="RadPanelBar1_ItemClick"
    >
    <Items>
    <telerik:RadPanelItem Text="IFF" Value="IFF">
    <Items>
                    <telerik:RadPanelItem Text="Add" Value="IFFAdd">                        
                    </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="MessageBroadcast" Value="MessageBroadcast">
                </telerik:RadPanelItem>
               
                <telerik:RadPanelItem Text="Loan Scheme" Value="LoanScheme">
                <Items>
                    <telerik:RadPanelItem Text="Add" Value="AddLoanScheme">
                    </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
    </Items>
    </telerik:radpanelbar>
