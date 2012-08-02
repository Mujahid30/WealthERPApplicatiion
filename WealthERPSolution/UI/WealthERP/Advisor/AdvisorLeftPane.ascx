﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisorLeftPane.ascx.cs"
    Inherits="WealthERP.Advisor.AdvisorLeftPane" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<script id="pagescript" type="text/javascript" language="javascript">
    function callSearchControl(searchtype) {
        var sessionValue = document.getElementById('ctrl_AdvisorLeftPane_hdfSession').value;


        if (searchtype == "RM") {
            var searchstring = document.getElementById('ctrl_AdvisorLeftPane_txtFindRM').value;
            loadsearchcontrol('ViewRM', 'RM', searchstring);
        }
        else if (searchtype == "Branch") {
            var searchstring = document.getElementById('ctrl_AdvisorLeftPane_txtFindBranch').value;
            loadsearchcontrol('ViewBranches', 'Branch', searchstring);
        }
        else if (searchtype == "AdviserCustomer") {
            if (sessionValue == "RM") {
                var searchstring = document.getElementById('ctrl_AdvisorLeftPane_txtFindAdviserCustomer').value;
                loadsearchcontrol('RMCustomer', 'RMCustomer', searchstring);
            }
            else {
                var searchstring = document.getElementById('ctrl_AdvisorLeftPane_txtFindAdviserCustomer').value;
                loadsearchcontrol('AdviserCustomer', 'AdviserCustomer', searchstring);
            }
        }
        else if (searchtype == "RMCustomer") {
            var searchstring = document.getElementById('ctrl_AdvisorLeftPane_txtFindRMCustomer').value;
            loadsearchcontrol('RMCustomer', 'RMCustomer', searchstring);
        }
    }
</script>

<%--<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>--%>
<asp:UpdatePanel ID="upnlAdviserleftpane" runat="server">
    <ContentTemplate>
        <table style="vertical-align: top; width: 100%;">
            <tr>
                <td valign="top">
                    <telerik:RadPanelBar ID="RadPanelBar1" runat="server" EnableEmbeddedSkins="false"
                        ExpandAnimation-Type="InCubic" Skin="Telerik" Width="100%" OnItemClick="RadPanelBar1_ItemClick"
                        AllowCollapseAllItems="True" ExpandMode="SingleExpandedItem">
                        <Items>
                            <telerik:RadPanelItem runat="server" Text="Admin" Value="Admin">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Admin Home" Value="Admin Home">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Settings" Value="Settings">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Setup Associate Category" Value="Setup Associate Category">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Setup Advisor Staff SMTP" Value="Setup Advisor Staff SMTP">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Environment Settings" Value="Environment_Settings">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Setup IP pool" Value="Setup IP pool">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Repository Category" Value="RepositoryCategory">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Set Theme" Value="Set Theme">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Organization" Value="Organization">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Profile" Value="Profile">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Edit Profile" Value="Edit Profile">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="LOB" Value="LOB">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Add LOB" Value="Add LOB">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Staff" Value="Staff">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Add Staff" Value="Add Staff">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Branch/Association" Value="Branch/Association">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Add Branch" Value="Add Branch">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="View Branch Association" Value="View Branch Association">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Customer" Value="Customer">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Add Customer" Value="Add Customer">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Manage Group Account" Value="Manage Group Account">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Add Group Account" Value="Add Group Account">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Reassign RM/Branch" Value="Customer Association">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Alert Configuration" Value="Alert Configuration">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Customized SMS" Value="Customized SMS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF Folios" Value="MF Folios">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>                                    
                                    
                                    <telerik:RadPanelItem runat="server" Text="Order" Value="Order_Management" PostBack="false">
                                                <Items>
                                                  <%--<telerik:RadPanelItem runat="server" Text="Order Query/MIS"
                                                        Value="OrderMIS">
                                                    </telerik:RadPanelItem>--%>
                                                    <telerik:RadPanelItem runat="server" Text=" MFOrder Entry" Value="OrderEntry">
                                                        <Items>
                                                           <telerik:RadPanelItem runat="server" Text="MFOrder Recon" Value="OrderMIS">
                                                           </telerik:RadPanelItem>
                                                        </Items>
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="LI Order" 
                                                        Value="LI_Order">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Order List" 
                                                        Value="Order_List">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Order Recon" Visible="false"
                                                        Value="OrderRecon" >
                                                    </telerik:RadPanelItem>
                                                </Items>
                                     </telerik:RadPanelItem>                                        
                                    
                                    <telerik:RadPanelItem runat="server" Text="Business MIS" Value="Business MIS">
                                        <Items>
                                        <telerik:RadPanelItem runat="server" Text="Business MIS Dashboard" Value="Business_MIS_Dashboard">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Multi Product MIS" Value="Multi_Product_MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF MIS" Value="MF MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF Systematic MIS" Value="MF systematic MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF Commission MIS" Value="MF Commission MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF" Value="MF">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="View Transactions" Value="View MF Transactions">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Add Transactions" Value="Add MF Transactions"
                                                        Visible="false">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Equity MIS" Value="Equity MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Equity" Value="Equity">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="View Transactions" Value="View EQ Transactions">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Add Transactions" Value="Add EQ Transactions">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Loan MIS" Value="Loan MIS" Vissble="false">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    
                                    
                                    <telerik:RadPanelItem runat="server" Text="Operations" Value="Operations">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Upload" Value="Upload">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Uploads History" Value="Uploads History">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="View Profile Exceptions" Value="View_Profile_Exceptions">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="View MF Transaction Exceptions" Value="View Transaction Exceptions">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="View MF Folio Exceptions" Value="View MF Folio Exceptions">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="View Equity Trade Account Exceptions"
                                                        Value="View_Equity_Trade_Account_Exceptions">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="View Equity Transaction Exceptions" Value="View_Equity_Transaction_Exceptions">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="View Systematic Transaction Exceptions"
                                                        Value="View_Systematic_Transaction_Exceptions">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Trail Commission Exception"
                                                        Value="Trail_Commission_Exception">
                                                    </telerik:RadPanelItem>
                                                   
                                                </Items>
                                            </telerik:RadPanelItem>
                                          
                                            <telerik:RadPanelItem runat="server" Text="User Management" Value="User Management">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Staff User Management" Value="Staff User Management">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Customer User Management" Value="Customer User Management">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Daily Process" Value="Daily Process">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Valuation" Value="Valuation">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF Systematic Daily Recon" Value="MF Systematic Daily Recon">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF Reversal Txn Exception" Value="MF Reversal Txn Exception Handling">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Loan Schemes" Value="Loan Schemes">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Add Schemes" Value="Add Schemes">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Loan Partner Commission" Value="Loan Partner Commission">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Customer Reports" Value="Customer Report">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Multi Asset" Value="Multi Asset Report">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF" Value="MF Report">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Equity" Value="Equity Report">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Message" Value="Message" PostBack="false">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Compose" Value="Compose">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Inbox" Value="Inbox">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Outbox" Value="Outbox">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Content" Value="Content" PostBack="false">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Repository" Value="Repository">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                        </Items>
                        <ExpandAnimation Type="InCubic" />
                    </telerik:RadPanelBar>
                    <telerik:RadPanelBar ID="RadPanelBar2" runat="server" EnableEmbeddedSkins="false"
                        ExpandAnimation-Type="InCubic" Skin="Telerik" OnItemClick="RadPanelBar2_ItemClick"
                        Width="100%" AllowCollapseAllItems="True" ExpandMode="SingleExpandedItem">
                        <Items>
                            <telerik:RadPanelItem runat="server" Text="RM" Value="RM">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="RM Home" Value="RM Home">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Profile" Value="Profile">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Customer" Value="Customer">
                                        <Items>
                                            <%--<telerik:RadPanelItem runat="server" Text="Add Customer" Value="Add Customer">
                                            </telerik:RadPanelItem>--%>
                                            <telerik:RadPanelItem runat="server" Text="Add FP Prospect" Value="Add FP Prospect">
                                            </telerik:RadPanelItem>
                                            <%-- <telerik:RadPanelItem runat="server" Text="Manage Group Account" Value="Manage Group Account">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Add Group Account" Value="Add Group Account">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>--%>
                                            <telerik:RadPanelItem runat="server" Text="Manage Portfolio" Value="Manage Portfolio">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Add Portfolio" Value="Add Portfolio">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <%--<telerik:RadPanelItem runat="server" Text="Alert Configuration" Value="Alert Configuration">
                                            </telerik:RadPanelItem>--%>
                                            <telerik:RadPanelItem runat="server" Text="Alert Notifications" Value="Alert Notifications">
                                            </telerik:RadPanelItem>
                                            <%--<telerik:RadPanelItem runat="server" Text="Customized SMS" Value="Customized SMS">
                                            </telerik:RadPanelItem>--%>
                                            <telerik:RadPanelItem runat="server" Text="Loan" Value="Loan">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Loan Proposal" Value="Loan Proposal">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Add Loan Proposal" Value="Add Loan proposal">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="FP Offline Form" Value="FP Offline Form">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Business MIS" Value="Business MIS">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Customer/Prospect MIS" Value="Prospect List">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF MIS" Value="MF MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF Systematic MIS" Value="MF systematic MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF Transactions" Value="MF">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="View MF Transactions" Value="View MF Transactions">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Add MF Transactions" Value="Add MF Transactions"
                                                        Visible="false">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Equity MIS" Value="Equity MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Equity Transactions" Value="Equity">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="View Equity Transactions" Value="View EQ Transactions">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Add Equity Transactions" Value="Add EQ Transactions">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Loan MIS" Value="Loan MIS" Vissble="false">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Customer Reports" Value="Customer Report">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Multi Asset Report" Value="Multi Asset Report">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF Report" Value="MF Report">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Equity Report" Value="Equity Report">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Message" Value="Message">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Compose" Value="Compose">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Inbox" Value="Inbox">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Outbox" Value="Outbox">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                        </Items>
                        <ExpandAnimation Type="InCubic" />
                    </telerik:RadPanelBar>
                    <telerik:RadPanelBar ID="RadPanelBar3" runat="server" EnableEmbeddedSkins="false"
                        ExpandAnimation-Type="InCubic" Skin="Telerik" OnItemClick="RadPanelBar3_ItemClick"
                        Width="100%" AllowCollapseAllItems="True" ExpandMode="SingleExpandedItem">
                        <Items>
                            <telerik:RadPanelItem runat="server" Text="BM" Value="BM">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="BM Home" Value="BM Home">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Staff" Value="Staff">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Customer" Value="Customer">
                                    </telerik:RadPanelItem>
                                    
                                    <telerik:RadPanelItem runat="server" Text="Order" Value="Order_Management" PostBack="false">
                                                <Items>
                                                    <%--<telerik:RadPanelItem runat="server" Text="Order Query/MIS"  Value="OrderMIS">
                                                    </telerik:RadPanelItem>--%>
                                                    <telerik:RadPanelItem runat="server" Text="MFOrder" Value="OrderEntry">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="LI Order" 
                                                        Value="LI_Order">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Order List" 
                                                        Value="Order_List">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Order Recon"
                                                        Value="OrderRecon" Visible="false">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                     </telerik:RadPanelItem>   
                                    <telerik:RadPanelItem runat="server" Text="Business MIS" Value="Business MIS">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="MF MIS" Value="MF MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF Systematic MIS" Value="MF systematic MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Equity MIS" Value="Equity MIS">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Customer Reports" Value="Customer Report">
                                        <Items>
                                            <%--<telerik:RadPanelItem runat="server" Text="Multi Asset" Value="Multi Asset Report">
                                            </telerik:RadPanelItem>--%>
                                            <telerik:RadPanelItem runat="server" Text="MF" Value="MF Report">
                                            </telerik:RadPanelItem>
                                            <%-- <telerik:RadPanelItem runat="server" Text="Equity" Value="Equity Report">
                                            </telerik:RadPanelItem>--%>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    
                                    <telerik:RadPanelItem runat="server" Text="Message" Value="Message">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Compose" Value="Compose">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Inbox" Value="Inbox">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Outbox" Value="Outbox">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                        </Items>
                        <ExpandAnimation Type="InCubic" />
                    </telerik:RadPanelBar>
                    <telerik:RadPanelBar ID="RadPanelBar4" runat="server" EnableEmbeddedSkins="false"
                        ExpandAnimation-Type="InCubic" Skin="Telerik" Width="100%" AllowCollapseAllItems="True"
                        ExpandMode="SingleExpandedItem" OnItemClick="RadPanelBar4_ItemClick">
                        <Items>
                            <telerik:RadPanelItem runat="server" Text="Ops" Value="Ops">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Customer" Value="Customer">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Add Customer" Value="Add Customer">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Manage Group Account" Value="Manage Group Account">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Add Group Account" Value="Add Group Account">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Reassign RM/Branch" Value="Customer Association">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Alert Configuration" Value="Alert Configuration">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Customized SMS" Value="Customized SMS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF Folios" Value="MF Folios">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Order" Value="Order_Management" PostBack="false">
                                                <Items>
                                                    <%--<telerik:RadPanelItem runat="server" Text="Order Query/MIS"
                                                        Value="OrderMIS">
                                                    </telerik:RadPanelItem>--%>
                                                    <telerik:RadPanelItem runat="server" Text="Order Entry" Value="OrderEntry">
                                                          <Items>
                                                           <telerik:RadPanelItem runat="server" Text="MFOrder Recon" Value="OrderMIS">
                                                           </telerik:RadPanelItem>
                                                        </Items>
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="LI Order" 
                                                        Value="LI_Order">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Order List" 
                                                        Value="Order_List">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Order Recon"
                                                        Value="OrderRecon" Visible="false">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                     </telerik:RadPanelItem>  
                                    <telerik:RadPanelItem runat="server" Text="Business MIS" Value="Business MIS">
                                        <Items>
                                      
                                            <telerik:RadPanelItem runat="server" Text="MF MIS" Value="MF MIS">
                                            </telerik:RadPanelItem>
                                             <telerik:RadPanelItem runat="server" Text="Multi Product MIS" Value="Multi_Product_MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF Systematic MIS" Value="MF systematic MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF Commission MIS" Value="MF Commission MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF" Value="MF">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="View Transactions" Value="View MF Transactions">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Add Transactions" Value="Add MF Transactions"
                                                        Visible="false">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Equity MIS" Value="Equity MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Equity" Value="Equity">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="View Transactions" Value="View EQ Transactions">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Add Transactions" Value="Add EQ Transactions">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Loan MIS" Value="Loan MIS" Vissble="false">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Operations" Value="Operations">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Upload" Value="Upload">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Uploads History" Value="Uploads History">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="View Profile Exceptions" Value="View_Profile_Exceptions">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="View MF Transaction Exceptions" Value="View Transaction Exceptions">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="View MF Folio Exceptions" Value="View MF Folio Exceptions">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="View Equity Trade Account Exceptions"
                                                        Value="View_Equity_Trade_Account_Exceptions">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="View Equity Transaction Exceptions" Value="View_Equity_Transaction_Exceptions">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="View Systematic Transaction Exceptions"
                                                        Value="View_Systematic_Transaction_Exceptions">
                                                    </telerik:RadPanelItem>
                                                     <telerik:RadPanelItem runat="server" Text="Trail Commission Exception"
                                                        Value="Trail_Commission_Exception">
                                                        </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" ImageUrl="~/Images/new.gif" Text="Order Management"
                                                Value="OrderManagement">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" ImageUrl="~/Images/new.gif" Text="Order Query/MIS"
                                                        Value="OrderMIS">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" ImageUrl="~/Images/new.gif" Text="Order Entry"
                                                        Value="OrderEntry">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" ImageUrl="~/Images/new.gif" Text="Order Recon"
                                                        Value="OrderRecon">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="User Management" Value="User Management">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Staff User Management" Value="Staff User Management">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Customer User Management" Value="Customer User Management">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Daily Process" Value="Daily Process">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Valuation" Value="Valuation">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF Systematic Daily Recon" Value="MF Systematic Daily Recon">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF Reversal Txn Exception" Value="MF Reversal Txn Exception Handling">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Loan Schemes" Value="Loan Schemes">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Add Schemes" Value="Add Schemes">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Loan Partner Commission" Value="Loan Partner Commission">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Customer Reports" Value="Customer Report">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Multi Asset" Value="Multi Asset Report">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF" Value="MF Report">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Equity" Value="Equity Report">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Message" Value="Message">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Compose" Value="Compose">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Inbox" Value="Inbox">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Outbox" Value="Outbox">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                        </Items>
                        <ExpandAnimation Type="InCubic" />
                    </telerik:RadPanelBar>
                    <telerik:RadPanelBar ID="RadPanelBar5" Style="vertical-align: middle;" runat="server"
                        EnableEmbeddedSkins="false" ExpandAnimation-Type="InCubic" Skin="Telerik" Width="100%"
                        AllowCollapseAllItems="True" ExpandMode="SingleExpandedItem" OnItemClick="RadPanelBar5_ItemClick">
                        <Items>
                            <telerik:RadPanelItem runat="server" Text="Research"
                                Value="Research">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Research Dashboard" Value="Research_Dashboard">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Configure" Value="Configure">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Risk/Goal Classes" Value="RiskGoal_Classes">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Risk Score" Value="Risk_Score">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Goal Score" Value="Goal_Score" Visible="false">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Asset Mapping" Value="Asset_Mapping">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Assumptions" Value="Assumptions">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Model Portfolio" Value="Reference_Data">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Portfolio" Value="Asset_Allocation">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF Investment" Value="MF_Investment">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Message" Value="Message">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Inbox" Value="Inbox">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                        </Items>
                        <ExpandAnimation Type="InCubic" />
                    </telerik:RadPanelBar>
                    <%--<telerik:RadPanelBar ID="RadPnlBrRepository" Style="vertical-align: middle;" runat="server"
                        EnableEmbeddedSkins="false" ExpandAnimation-Type="InCubic" Skin="Telerik" Width="100%"
                        AllowCollapseAllItems="True" ExpandMode="SingleExpandedItem" OnItemClick="RadPnlBrRepository_ItemClick">
                        <Items>
                            <telerik:RadPanelItem runat="server" ImageUrl="~/Images/new.gif" Text="Content" Value="Message">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Compose" Value="Compose">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Inbox" Value="Inbox">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Outbox" Value="Outbox">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                        </Items>
                        <ExpandAnimation Type="InCubic" />
                    </telerik:RadPanelBar>--%>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <div style="vertical-align: middle;">
                        <asp:TextBox runat="server" ID="txtFindRM" Style="width: 110px;" onkeypress="return JSdoPostback(event,'ctrl_AdvisorLeftPane_btnSearchRM');" />
                        <cc1:TextBoxWatermarkExtender ID="txtFindRM_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtFindRM" WatermarkText="Find Staff">
                        </cc1:TextBoxWatermarkExtender>
                        <asp:Button ID="btnSearchRM" runat="server" CssClass="SearchButton" OnClientClick="javascript:callSearchControl('RM');return false;" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="display: inline">
                        <asp:TextBox runat="server" ID="txtFindBranch" Style="width: 110px" onkeypress="return JSdoPostback(event,'ctrl_AdvisorLeftPane_btnSearchBranch');" />
                        <cc1:TextBoxWatermarkExtender ID="txtFindBranch_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtFindBranch" WatermarkText="Find Branch">
                        </cc1:TextBoxWatermarkExtender>
                        <asp:Button ID="btnSearchBranch" runat="server" CssClass="SearchButton" OnClientClick="javascript:callSearchControl('Branch');return false;" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="display: inline">
                        <asp:TextBox runat="server" ID="txtFindAdviserCustomer" Style="width: 110px" onkeypress="return JSdoPostback(event,'ctrl_AdvisorLeftPane_btnSearchAdviserCustomer');" />
                        <cc1:TextBoxWatermarkExtender ID="txtFindAdviserCustomer_TextBoxWatermarkExtender"
                            runat="server" TargetControlID="txtFindAdviserCustomer" WatermarkText="Find Customer">
                        </cc1:TextBoxWatermarkExtender>
                        <asp:Button ID="btnSearchAdviserCustomer" runat="server" CssClass="SearchButton"
                            OnClientClick="javascript:callSearchControl('AdviserCustomer');return false;" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="display: inline">
                        <asp:TextBox runat="server" ID="txtFindRMCustomer" Style="width: 110px" onkeypress="return JSdoPostback(event,'ctrl_AdvisorLeftPane_btnSearchRMCustomer');" />
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtFindRMCustomer"
                            WatermarkText="Find Customer">
                        </cc1:TextBoxWatermarkExtender>
                        <asp:Button ID="btnSearchRMCustomer" runat="server" CssClass="SearchButton" OnClientClick="javascript:callSearchControl('RMCustomer');return false;" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    <asp:HiddenField ID="hdfSession" runat="server" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
