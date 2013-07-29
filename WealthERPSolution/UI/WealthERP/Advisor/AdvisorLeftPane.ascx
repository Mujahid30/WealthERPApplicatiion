 <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisorLeftPane.ascx.cs"
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
                loadsearchcontrol('AdviserCustomer', 'AdviserCustomer', searchstring);
            }
            else {
                var searchstring = document.getElementById('ctrl_AdvisorLeftPane_txtFindAdviserCustomer').value;
                loadsearchcontrol('AdviserCustomer', 'AdviserCustomer', searchstring);
            }
        }
        else if (searchtype == "RMCustomer") {
            var searchstring = document.getElementById('ctrl_AdvisorLeftPane_txtFindRMCustomer').value;
            loadsearchcontrol('AdviserCustomer', 'AdviserCustomer', searchstring);
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
                                            <telerik:RadPanelItem runat="server" Text="Set Theme" Value="Set Theme">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Setup Advisor Staff SMTP" Value="Setup Advisor Staff SMTP">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Environment Settings" Value="Environment_Settings">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Setup IP pool" Value="Setup IP pool">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Setup customer category" Value="Setup_customer_category">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Repository Category" Value="RepositoryCategory">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Setup Associate Category" Value="Setup Associate Category">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Alert Configuration" Value="Alert Configuration">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Content" Value="Content" PostBack="false">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Repository" Value="Repository">
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
                                            <telerik:RadPanelItem runat="server" Text="Zone/Cluster" Value="Zone_Cluster">
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
                                            <telerik:RadPanelItem runat="server" Text="LOB" Value="LOB">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Add LOB" Value="Add LOB">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                             <telerik:RadPanelItem runat="server" Text="Associates" Value="Associatess">
                                             <Items>
                                             <telerik:RadPanelItem runat="server" Text="View Associates List" Value="ViewAssociatess">
                                            </telerik:RadPanelItem>
                                            </Items>
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Customer" Value="Customer">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Customer List" Value="CustomerList">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Add Customer" Value="Add Customer">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Add FP Prospect" Value="Add FP Prospect">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Manage Portfolio" Value="Manage Portfolio">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Add Portfolio" Value="Add Portfolio">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Manage Group Account" Value="Manage Group Account">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Add Group Account" Value="Add Group Account">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Reassign RM/Branch" Value="Customer Association">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="FP Offline Form" Value="FP Offline Form">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Customer Reports" Value="Customer_Report">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Order" Value="Order_Management" PostBack="false">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text=" MFOrder Entry" Value="OrderEntry">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Order List" Value="Order_List">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Operations" Value="Operations">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Upload" Value="Upload">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Start Upload" Value="Start_Upload">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Uploads History" Value="Uploads History">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="View Exceptions" Value="Uploads_Exception">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                                </telerik:RadPanelItem>
                                                <telerik:RadPanelItem runat="server" Text="Commission" Value="Commissions">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="View Receivable structure" Value="View_Receivable_structure">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Receivable Strucrure setup" Value="Receivable_Strucrure_setup">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="View Scheme Structure Association" Value="View_Scheme_Structure_Association">
                                                    </telerik:RadPanelItem>
                                                     <telerik:RadPanelItem runat="server" Text="Receivable Scheme Structure Association" Value="Receivable_Scheme_Structure_Association">
                                                    </telerik:RadPanelItem>
                                                     <telerik:RadPanelItem runat="server" Text="View Payable Structure" Value="View_Payable_Structure">
                                                    </telerik:RadPanelItem>
                                                     <telerik:RadPanelItem runat="server" Text="Payable Structure Setup" Value="Payable_Structure_Setup">
                                                    </telerik:RadPanelItem>
                                                </Items>                                              
                                            </telerik:RadPanelItem>                                            
                                            <telerik:RadPanelItem runat="server" Text="Reconciliation" Value="Reconciliation">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="MF Systematic  Recon" Value="MF Systematic Daily Recon">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Accounts Compare" Value="Customer_Accounts_Compare">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="MFOrder Recon" Value="OrderMIS">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="MF NP & Tranx Compare" Value="MFNP_Tranx_Compare">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="User Management" Value="User Management">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Staff User Management" Value="Staff User Management">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Customer User Management" Value="Customer User Management">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Associate User Management" Value="Associate_User_Mnagement">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Login History" Value="Adviser_Login_Track"
                                                        Vissble="true">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                             <telerik:RadPanelItem runat="server" Text="Associates" Value="Associates">
                                             <Items>
                                            <telerik:RadPanelItem runat="server" Text="Request Associate" Value="AddAssociates">
                                            </telerik:RadPanelItem>
                                             <telerik:RadPanelItem runat="server" Text="Associates Status" Value="ViewAssociates">
                                            </telerik:RadPanelItem>
                                             <telerik:RadPanelItem runat="server" Text="Add Agent/Associate Code" Value="AddAgentCode">
                                            </telerik:RadPanelItem>
                                             <telerik:RadPanelItem runat="server" Text="View Agent/Associate Code" Value="ViewAgentCode">
                                            </telerik:RadPanelItem>
                                            </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Valuation" Value="Valuation">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Add Transactions" Value="Add EQ Transactions">
                                            </telerik:RadPanelItem>
                                            <%--<telerik:RadPanelItem runat="server" Text="MF Reversal Txn Exception" Value="MF Reversal Txn Exception Handling">
                                            </telerik:RadPanelItem>--%>
                                            <telerik:RadPanelItem runat="server" Text="Loan Schemes" Value="Loan Schemes">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Add Schemes" Value="Add Schemes">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Loan Partner Commission" Value="Loan Partner Commission">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Content" Value="Content" PostBack="false">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Repository" Value="Repository">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Business MIS" Value="Business MIS">
                                        <Items>
                                            <%------------------New Tree View--------------%>
                                            <telerik:RadPanelItem runat="server" Text="MF DashBoard" Value="MFDashBoard" Vissble="true">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF TurnOver MIS" Value="MFTurnOverMIS"
                                                Vissble="true">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF Commission MIS" Value="MF Commission MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Customer SignUp" Value="CustomerSignUp"
                                                Vissble="true">
                                            </telerik:RadPanelItem>
                                            <%------------------New Tree View--------------%>
                                            <%-- <telerik:RadPanelItem runat="server" Text="Loan MIS" Value="Loan MIS" Vissble="false">
                                            </telerik:RadPanelItem>--%>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <%------------------New Tree View--------------%>
                                    <telerik:RadPanelItem runat="server" Text="AUM & Holdings" Value="AUM_Holdings">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Customer AUM" Value="Customer_AUM">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Customer Holdings" Value="Customer_Holdings">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Branch AUM" Value="Business_MIS_Dashboard">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Queries" Value="Queries">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Accounts" Value="MF Folios">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Transactions" Value="Transactions">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF systematic MIS" Value="MF systematic MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Customer Networth MIS" Value="Prospect List">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Goal MIS" Value="Goal_MIS" Vissble="true">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Returns & Analytics" Value="Returns_Analytics">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Asset Allocation MIS" Value="Asset_Allocation_MIS"
                                                Vissble="true">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Equity Allocation" Value="Equity MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Returns" Value="Performance_Allocation"
                                                Vissble="true">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF SIP Projection" Value="MF_SIP_Projection">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <%------------------New Tree View--------------%>
                                    <telerik:RadPanelItem runat="server" Text="Message" Value="Message" PostBack="false">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Compose" Value="Compose">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Inbox" Value="Inbox">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Outbox" Value="Outbox">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Customized SMS" Value="Customized SMS">
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
                                    <telerik:RadPanelItem runat="server" Text="Profile" Value="Profile" Visible="false">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Customer" Value="Customer">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Customer List" Value="CustomerList">
                                            </telerik:RadPanelItem>
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
                                            <telerik:RadPanelItem runat="server" Text="Customer Reports" Value="Customer_Report">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Order" Value="Order_Management" PostBack="false">
                                        <Items>
                                            <%--<telerik:RadPanelItem runat="server" Text="Order Query/MIS"
                                                        Value="OrderMIS">
                                                    </telerik:RadPanelItem>--%>
                                            <telerik:RadPanelItem runat="server" Text="Order Entry" Value="OrderEntry">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Order List" Value="Order_List">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Order Recon" Value="OrderRecon" Visible="false">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Business MIS" Value="Business MIS">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="MF Dashboard MIS" Value="MFDashboard"
                                                Vissble="true">
                                            </telerik:RadPanelItem>
                                            <%------------------New Tree View--------------%>
                                            <telerik:RadPanelItem runat="server" Text="MF TurnOver MIS" Value="MFTurnOverMIS"
                                                Vissble="true">
                                            </telerik:RadPanelItem>
                                            <%------------------New Tree View--------------%>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <%------------------New Tree View--------------%>
                                    <telerik:RadPanelItem runat="server" Text="AUM & Holdings" Value="AUM_Holdings">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Customer Holdings" Value="Customer_Holdings">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Queries" Value="Queries">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Accounts" Value="MF Folios">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Transactions" Value="Transactions">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF systematic MIS" Value="MF systematic MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Customer Networth MIS" Value="Prospect List">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Goal MIS" Value="Goal_MIS" Vissble="true">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Returns & Analytics" Value="Returns_Analytics">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Asset Allocation MIS" Value="Asset_Allocation_MIS"
                                                Vissble="true">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Equity Allocation" Value="Equity MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Returns" Value="Performance_Allocation"
                                                Vissble="true">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF SIP Projection" Value="MF_SIP_Projection">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <%------------------New Tree View--------------%>
                                    <telerik:RadPanelItem runat="server" Text="Message" Value="Message">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Compose" Value="Compose">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Inbox" Value="Inbox">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Outbox" Value="Outbox">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Customized SMS" Value="Customized SMS">
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
                                    <telerik:RadPanelItem runat="server" Text="Associates" Value="Associates">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Add Associates" Value="AddAssociates">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="View Associates" Value="ViewAssociates">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Customer" Value="Customer">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Customer List" Value="CustomerList">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Customer Reports" Value="Customer_Report">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="ISA Status" Value="Status_ISA">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Generate ISA" Value="Generate_ISA">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="ISA to Folio Mapping" Value="ISA_Mapp">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Order" Value="Order_Management" PostBack="false">
                                        <Items>
                                            <%--<telerik:RadPanelItem runat="server" Text="Order Query/MIS"  Value="OrderMIS">
                                                    </telerik:RadPanelItem>--%>
                                            <telerik:RadPanelItem runat="server" Text="MFOrder" Value="OrderEntry">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Order List" Value="Order_List">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Order Recon" Value="OrderRecon" Visible="false">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Business MIS" Value="Business MIS">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="MF DashBoard" Value="MFDashboard" Vissble="true">
                                            </telerik:RadPanelItem>
                                            <%------------------New Tree View--------------%>
                                            <telerik:RadPanelItem runat="server" Text="MF TurnOver MIS" Value="MFTurnOverMIS"
                                                Vissble="true">
                                            </telerik:RadPanelItem>
                                            <%------------------New Tree View--------------%>
                                            <%-- <telerik:RadPanelItem runat="server" Text="Customer Networth MIS" Value="Prospect List">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF MIS" Value="MF MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF Systematic MIS" Value="MF systematic MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Equity MIS" Value="Equity MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Goal MIS" Value="Goal_MIS" Vissble="true">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Asset Allocation MIS"  Value="Asset_Allocation_MIS" Vissble="true">
                                            </telerik:RadPanelItem>--%>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <%------------------New Tree View--------------%>
                                    <telerik:RadPanelItem runat="server" Text="AUM & Holdings" Value="AUM_Holdings">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Customer Holdings" Value="Customer_Holdings">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Queries" Value="Queries">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Accounts" Value="MF Folios">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Transactions" Value="Transactions">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF systematic MIS" Value="MF systematic MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Customer Networth MIS" Value="Prospect List">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Goal MIS" Value="Goal_MIS" Vissble="true">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Returns & Analytics" Value="Returns_Analytics">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Asset Allocation MIS" Value="Asset_Allocation_MIS"
                                                Vissble="true">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Equity Alloction" Value="Equity MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Returns" Value="Performance_Allocation"
                                                Vissble="true">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF SIP Projection" Value="MF_SIP_Projection">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <%------------------New Tree View--------------%>
                                    <telerik:RadPanelItem runat="server" Text="Message" Value="Message">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Compose" Value="Compose">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Inbox" Value="Inbox">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Outbox" Value="Outbox">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Customized SMS" Value="Customized SMS">
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
                                    <telerik:RadPanelItem runat="server" Text="Associates" Value="Associates">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Add Associates" Value="AddAssociates">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Associates Status" Value="ViewAssociates">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Add Agent/Associate Code" Value="AddAgentCode">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="View Agent/Associate Code" Value="ViewAgentCode">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Customer" Value="Customer">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Customer List" Value="CustomerList">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Add Customer" Value="Add Customer">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Add FP Prospect" Value="Add FP Prospect">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Manage Portfolio" Value="Manage Portfolio">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Add Portfolio" Value="Add Portfolio">
                                                    </telerik:RadPanelItem>
                                                </Items>
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
                                            <%--  <telerik:RadPanelItem runat="server" Text="Customized SMS" Value="Customized SMS">
                                            </telerik:RadPanelItem>--%>
                                            <%-- <telerik:RadPanelItem runat="server" Text="MF Folios" Value="MF Folios">
                                            </telerik:RadPanelItem>--%>
                                            <telerik:RadPanelItem runat="server" Text="ISA Status" Value="Status_ISA">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="ISA Folio Mapping" Value="ISA_Folio_Mapp">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="FP Offline Form" Value="FP Offline Form">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Customer Reports" Value="Customer_Report">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Order" Value="Order_Management" PostBack="false">
                                        <Items>
                                            <%--<telerik:RadPanelItem runat="server" Text="Order Query/MIS"
                                                        Value="OrderMIS">
                                                    </telerik:RadPanelItem>--%>
                                            <telerik:RadPanelItem runat="server" Text="Order Entry" Value="OrderEntry">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Order List" Value="Order_List">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Order Recon" Value="OrderRecon" Visible="false">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Operations" Value="Operations">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Upload" Value="Upload">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Start Upload" Value="Start_Upload">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Uploads History" Value="Uploads History">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="View Exceptions" Value="Uploads_Exception">
                                                    </telerik:RadPanelItem>
                                                   <%--  <telerik:RadPanelItem runat="server" Text="MF NP & Tranx Compare" Value="MFNP_Tranx_Compare">
                                                    </telerik:RadPanelItem>--%>
                                                </Items>
                                            </telerik:RadPanelItem>
                                             <telerik:RadPanelItem runat="server" Text="Commission" Value="Commissions">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="View Receivable structure" Value="View_Receivable_structure">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Receivable Strucrure setup" Value="Receivable_Strucrure_setup">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="View Scheme Structure Association" Value="View_Scheme_Structure_Association">
                                                    </telerik:RadPanelItem>
                                                     <telerik:RadPanelItem runat="server" Text="Receivable Scheme Structure Association" Value="Receivable_Scheme_Structure_Association">
                                                    </telerik:RadPanelItem>
                                                     <telerik:RadPanelItem runat="server" Text="View Payable Structure" Value="View_Payable_Structure">
                                                    </telerik:RadPanelItem>
                                                     <telerik:RadPanelItem runat="server" Text="Payable Structure Setup" Value="Payable_Structure_Setup">
                                                    </telerik:RadPanelItem>
                                                </Items>                                              
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Reconciliation" Value="Reconciliation">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="MF Systematic  Recon" Value="MF Systematic Daily Recon">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Accounts Compare" Value="Customer_Accounts_Compare">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="MFOrder Recon" Value="OrderMIS">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="MF NP & Tranx Compare" Value="MFNP_Tranx_Compare">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="User Management" Value="User Management">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Staff User Management" Value="Staff User Management">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Customer User Management" Value="Customer User Management">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Login History" Value="Adviser_Login_Track"
                                                        Vissble="true">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                             <%--<telerik:RadPanelItem runat="server" Text="Associates" Value="Associates">
                                             <Items>
                                            <telerik:RadPanelItem runat="server" Text="Request Associate" Value="ViewAssociates">
                                            </telerik:RadPanelItem>
                                             <telerik:RadPanelItem runat="server" Text="View Associates List" Value="ViewAssociates">
                                            </telerik:RadPanelItem>
                                             <telerik:RadPanelItem runat="server" Text="Add Agent/Associate Code" Value="AddAgentCode">
                                            </telerik:RadPanelItem>
                                             <telerik:RadPanelItem runat="server" Text="View Agent/Associate Code" Value="ViewAgentCode">
                                            </telerik:RadPanelItem>
                                            </Items>
                                            </telerik:RadPanelItem>--%>
                                            <telerik:RadPanelItem runat="server" Text="Valuation" Value="Valuation">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Add Transactions" Value="Add EQ Transactions">
                                            </telerik:RadPanelItem>
                                            <%--<telerik:RadPanelItem runat="server" Text="MF Reversal Txn Exception" Value="MF Reversal Txn Exception Handling">
                                            </telerik:RadPanelItem>--%>
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
                                    <telerik:RadPanelItem runat="server" Text="Business MIS" Value="Business MIS">
                                        <Items>
                                            <%------------------New Tree View--------------%>
                                            <telerik:RadPanelItem runat="server" Text="MF DashBoard" Value="MFDashBoard" Vissble="true">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF TurnOver MIS" Value="MFTurnOverMIS"
                                                Vissble="true">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF Commission MIS" Value="MF Commission MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Customer SignUp" Value="CustomerSignUp"
                                                Vissble="true">
                                            </telerik:RadPanelItem>
                                            <%------------------New Tree View--------------%>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <%------------------New Tree View--------------%>
                                    <telerik:RadPanelItem runat="server" Text="AUM & Holdings" Value="AUM_Holdings">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Customer AUM" Value="Customer_AUM">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Customer Holdings" Value="Customer_Holdings">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Branch AUM" Value="Business_MIS_Dashboard">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Queries" Value="Queries">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Accounts" Value="MF Folios">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Transactions" Value="Transactions">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF systematic MIS" Value="MF systematic MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Customer Networth MIS" Value="Prospect List">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Goal MIS" Value="Goal_MIS" Vissble="true">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Returns & Analytics" Value="Returns_Analytics"
                                        Visible="false">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Asset Allocation MIS" Value="Asset_Allocation_MIS"
                                                Vissble="true">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Equity Allocation" Value="Equity MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Returns" Value="Performance_Allocation"
                                                Vissble="true">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF SIP Projection" Value="MF_SIP_Projection">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <%------------------New Tree View--------------%>
                                    <telerik:RadPanelItem runat="server" Text="Message" Value="Message" PostBack="false">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Compose" Value="Compose">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Inbox" Value="Inbox">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Outbox" Value="Outbox">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Customized SMS" Value="Customized SMS">
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
                            <telerik:RadPanelItem runat="server" Text=" Advisory Setup" Value="Research">
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
                    <telerik:RadPanelBar ID="RadPanelBar6" runat="server" EnableEmbeddedSkins="false"
                        ExpandAnimation-Type="InCubic" Skin="Telerik" Width="100%" OnItemClick="RadPanelBar6_ItemClick"
                        AllowCollapseAllItems="True" ExpandMode="SingleExpandedItem">
                        <Items>
                           <telerik:RadPanelItem runat="server" Text="Associates" Value="Agent">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Dashboard" Value="RM Home">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Profile" Value="Profile" Visible="true">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Customer" Value="Customer">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Customer List" Value="CustomerList">
                                            </telerik:RadPanelItem>                                                                                    
                                            <telerik:RadPanelItem runat="server" Text="Alert Notifications" Value="Alert Notifications">
                                            </telerik:RadPanelItem>                                          
                                            <telerik:RadPanelItem runat="server" Text="FP Offline Form" Value="FP Offline Form">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Reports" Value="Customer_Report">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Order" Value="Order_Management" PostBack="false">
                                        <Items>                                           
                                            <telerik:RadPanelItem runat="server" Text="Order Entry" Value="OrderEntry">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Order List" Value="Order_List">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Order Recon" Value="OrderRecon" Visible="false">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Business MIS" Value="Business MIS">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="MF Dashboard MIS" Value="MFDashboard"
                                                Vissble="true">
                                            </telerik:RadPanelItem>
                                            <%------------------New Tree View--------------%>
                                            <telerik:RadPanelItem runat="server" Text="MF TurnOver MIS" Value="MFTurnOverMIS"
                                                Vissble="true">
                                            </telerik:RadPanelItem>
                                             <telerik:RadPanelItem runat="server" Text="MF Commission MIS" Value="MF Commission MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Customer SignUp" Value="CustomerSignUp"
                                                Vissble="true">
                                            </telerik:RadPanelItem>
                                            <%------------------New Tree View--------------%>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <%------------------New Tree View--------------%>
                                    <telerik:RadPanelItem runat="server" Text="AUM & Holdings" Value="AUM_Holdings">
                                        <Items>
                                          <telerik:RadPanelItem runat="server" Text="Customer AUM" Value="Customer_AUM">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Customer Holdings" Value="Customer_Holdings">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Queries" Value="Queries">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Accounts" Value="MF Folios">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Transactions" Value="Transactions">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF SIP MIS" Value="MF systematic MIS">
                                            </telerik:RadPanelItem>                                           
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Returns & Analytics" Value="Returns_Analytics">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Asset Allocation MIS" Value="Asset_Allocation_MIS"
                                                Vissble="true">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Equity Allocation" Value="Equity MIS">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Returns" Value="Performance_Allocation"
                                                Vissble="true">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="MF SIP Projection" Value="MF_SIP_Projection">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <%------------------New Tree View--------------%>
                                    <telerik:RadPanelItem runat="server" Text="Message" Value="Message">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Compose" Value="Compose">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Inbox" Value="Inbox">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Outbox" Value="Outbox">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Customized SMS" Value="Customized SMS">
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
