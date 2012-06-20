<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SuperAdminLeftPane.ascx.cs"
    Inherits="WealthERP.SuperAdmin.SuperAdminLeftPane" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<telerik:RadPanelBar ID="RadPanelBar1" runat="server" ExpandAnimation-Type="InCubic"
    ExpandMode="SingleExpandedItem" Width="100%" Skin="Telerik" EnableEmbeddedSkins="false"
    OnItemClick="RadPanelBar1_ItemClick">
    <Items>
         <telerik:RadPanelItem Text="IFA" Value="IFF">
            <Items>
                <telerik:RadPanelItem Text="Add" Value="IFFAdd">
                </telerik:RadPanelItem>
            </Items>
             <Items>
                <telerik:RadPanelItem Text="User Management" Value="IFFUserManagement">
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelItem>
        
         <telerik:RadPanelItem Text="Ops" Value="OPS">
            <Items>
                <telerik:RadPanelItem Text="Valuation Monitor" Value="Valuation_Monitor">
                <Items>
                    <telerik:RadPanelItem Text="Manual Valuation" Value="Manual_Valuation">
                    </telerik:RadPanelItem>
                </Items>
                </telerik:RadPanelItem>
                 <telerik:RadPanelItem Text="Gold Price Maintenance" Value="Gold_Price_Monito">
                </telerik:RadPanelItem>
                 <telerik:RadPanelItem Text="Sync" Value="GoalFunding_Sync">
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="Uploads" Value="Uploads">
                <Items>
                 <telerik:RadPanelItem Text="Uploads History" Value="Uploads_History">
                </telerik:RadPanelItem>
                 <telerik:RadPanelItem Text="View Profile Exceptions" Value="View_Profile_Exceptions">
                </telerik:RadPanelItem>
                 <telerik:RadPanelItem Text="View Transaction Exceptions" Value="View_Transactions_Exceptions">
                </telerik:RadPanelItem>
                 <telerik:RadPanelItem Text="View MF Folio Exceptions" Value="View_MFFolio_Exceptions">
                </telerik:RadPanelItem>
                 <telerik:RadPanelItem Text="View EQ Trade A/c Exceptions" Value="View_EQ_TRADE_Account">
                </telerik:RadPanelItem>
                 <telerik:RadPanelItem Text="View EQ Transaction Exceptions" Value="View_EQ_Transaction">
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="View Systematic Transaction Exceptions" Value="View_Systematic_Transaction">
                </telerik:RadPanelItem>
                </Items>
                 </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelItem>
         
         <telerik:RadPanelItem Text="Issue Tracker">
            <Items>
                <telerik:RadPanelItem Text="View Issue Tracker" Value="IssueTracker">
                </telerik:RadPanelItem>
            </Items>
            <Items>
                <telerik:RadPanelItem Text="Add New Issue" Value="AddNewIssue">
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelItem>
         
         <telerik:RadPanelItem Text="Messages">
            <Items>
            
            
                <telerik:RadPanelItem Text="Compose" Value="MsgCompose">
                </telerik:RadPanelItem>                
                
                <%--<telerik:RadPanelItem Text="Inbox" Value="MsgInbox">
                </telerik:RadPanelItem>--%>
                                
                <telerik:RadPanelItem Text="Outbox" Value="MsgOutbox">
                </telerik:RadPanelItem>
                
            </Items>
            
        </telerik:RadPanelItem>
        
        
         <telerik:RadPanelItem Text="MessageBroadcast" Value="MessageBroadcast">
         </telerik:RadPanelItem>
        
         <telerik:RadPanelItem Text="Configuration" Value="Configuration">
         </telerik:RadPanelItem>
         
         <telerik:RadPanelItem Text="Loan Scheme" Value="LoanScheme">
            <Items>
                <telerik:RadPanelItem Text="Add" Value="AddLoanScheme">
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelItem>
        
        
        
       
        
       
        
       
    </Items>
</telerik:RadPanelBar>
