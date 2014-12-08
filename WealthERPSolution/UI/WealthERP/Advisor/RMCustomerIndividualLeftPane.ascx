<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMCustomerIndividualLeftPane.ascx.cs"
    Inherits="WealthERP.Advisor.RMIndividualCustomerLeftPane" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script id="pagescript" type="text/javascript" language="javascript">
    function callSearch(searchtype) {
        var searchstring = document.getElementById('<%= txtFindCustomer.ClientID %>').value;
        var userRole = document.getElementById('<%=hdnUserRole.ClientID %>').value;
        if (userRole == 'RM')
            loadsearchcontrol('RMCustomer', 'RMCustomer', searchstring);
        else if (userRole == 'Adviser')
            loadsearchcontrol('AdviserCustomer', 'AdviserCustomer', searchstring);
        else if (userRole == 'BM')
            loadsearchcontrol('BMCustomer', 'BMCustomer', searchstring);

    }
    function buttonCliclMF() {

        document.getElementById('<%= btnOnlineOrder.ClientID %>').click();
    }
    function buttonCliclNCD() {

        document.getElementById('<%= btnNCDOnline.ClientID %>').click();
    }
    function buttonCliclIPO() {

        document.getElementById('<%= btnIPOOnline.ClientID %>').click();
    }
</script>

<style type="text/css">
    .OnlineOrder
    {
        width: 30px;
        background: #68bfe1 url('/./Images/Buy-Button.png') no-repeat;
    }
</style>
<asp:ScriptManager ID="smCustomerLeftpanel" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="upnlCustomerLeftpanel" runat="server">
    <ContentTemplate>
        <table>
            <tr id="trCustDetails" runat="server">
                <td>
                    <asp:Label ID="lblCustomer" Text="Customer:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCustomerDetails" Text="" runat="server" CssClass="Field"></asp:Label>
                </td>
            </tr>
            <tr id="trCustRMDetailsDivider" runat="server">
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblClientInfo" CssClass="HeaderTextSmall" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblName" Text="Name:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblNameValue" Text="" runat="server" CssClass="Field"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEmailId" Text="EmailID:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblEmailIdValue" Text="" runat="server" CssClass="Field"></asp:Label>
                </td>
            </tr>
            <tr id="trMobileDetails" runat="server">
                <td>
                    <asp:Label ID="lblMobile" Text="Mobile:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblMobileValue" Text="" runat="server" CssClass="Field"></asp:Label>
                </td>
            </tr>
            <tr visible="false">
                <td colspan="2" align="right" visible="false">
                  
                </td>
            </tr>
        </table>
        <table style="valign: top; width: 100%;">
            <tr>
                <td>
                    <telerik:RadPanelBar ID="RadPanelBar1" runat="server" EnableEmbeddedSkins="false"
                        ExpandAnimation-Type="InCubic" Skin="Telerik" Width="100%" OnItemClick="RadPanelBar1_ItemClick"
                        AllowCollapseAllItems="True" ExpandMode="SingleExpandedItem">
                        <Items>
                            <telerik:RadPanelItem runat="server" Text="Home" Value="Home" o>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Home" Value="CusHome">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Short Cut" Value="CusQuickLinks">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Value="Group Dashboard" Text="Group Dashboard"
                                Visible="false">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Customer Dashboard" Value="Customer Dashboard">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Value="Profile Dashboard" Text="Profile">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="View Profile" Value="View Profile">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Value="Edit Profile" Text="Edit Profile">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Bank Details" Value="Bank Details">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Add Bank Account" Value="Add_Bank_Account">
                                            </telerik:RadPanelItem>
                                        </Items>
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Add Bank Transaction/Balance" Value="Add Bank Details">
                                            </telerik:RadPanelItem>
                                        </Items>
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="View Bank Transaction" Value="View Bank Transaction">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Demat Account Details" Value="Demat Account Details">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Add Demat Account" Value="Add Demat Account">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="View Demat Account" Value="View_Demate_Account">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Proof" Value="Proof">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Vault Proof" Value="VaultProof">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Advisor Notes" Value="Advisor Notes">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Value="Customer_Order" Text="Product Order">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Order Management" Value="Order_Approval_List"
                                        ImagePosition="Right">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Financial Planning" Value="Financial Planning">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Finance Profile" Value="Finance Profile">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" ImagePosition="Right" Text="Assumptions and Preferences"
                                        Value="Preferences">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Recommendation" Value="CustomerFPRecommendation">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Risk Profile and Asset Allocation" Value="Risk profile and asset allocation">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Projections" Value="Projections" Visible="false">
                                    </telerik:RadPanelItem>
                                    <%-- <telerik:RadPanelItem runat="server" Text="Advanced Goal Planning" Value="Goal_Planning">
                                     <Items>
                                            <telerik:RadPanelItem runat="server" Text="Goal Setup" Value="Goal Setup">
                                            </telerik:RadPanelItem>
                                     </Items>
                                    <Items>
                                            <telerik:RadPanelItem runat="server" Text="Goal Funding" Value="Goal Funding">
                                            </telerik:RadPanelItem>
                                     </Items>
                                    </telerik:RadPanelItem>--%>
                                    <telerik:RadPanelItem runat="server" Text="Goal" Value="Goal_Planning">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Goal List" Value="Goal_List">
                                            </telerik:RadPanelItem>
                                        </Items>
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Add Goal" Value="Goal_Setup">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Analytics" Value="Analytics">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Standard" Value="Standard">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Online Order" Value="Online_Order">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="MF Order" Value="MF_Order">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Bond Order" Value="Bond_Order">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Order MIS" Value="Online_Order_MIS">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Insurance" Value="Insurance">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Life Insurance" Value="Life Insurance">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Add Life Insurance" Value="Add Life Insurance">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="General Insurance" Value="General Insurance">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Add General Insurance" Value="Add General Insurance">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Assets" Value="Portfolio Dashboard">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Equity" Value="Equity">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="View Equity Transaction" Value="View Equity Transactions">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Add Equity Transaction" Value="Add Equity Transactions">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Add Equity Account" Value="Add Equity Account">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="View Equity Account" Value="View Equity Account">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="MF" Value="M_F">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="View MF Transaction" Value="View MF Transactions">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Add MF Transaction" Value="Add MF Transactions">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Add MF Folio" Value="Add MF Folio">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="View MF Folio" Value="View MF Folio">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="View Systematic Schemes" Value="View Systematic Schemes">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Register Systematic Schemes" Value="Register Systematic Schemes">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Fixed Income" Value="Fixed Income">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Add Fixed Income" Value="Add Fixed Income">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Govt. Savings" Value="Govt Savings">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Add Govt. Savings" Value="Add Govt Savings">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Property" Value="Property">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Add Property" Value="Add Property">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Pension and Gratuities" Value="Pension and Gratuities">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Add Pension and Gratuities" Value="Add Pension and Gratuities">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Personal Assets" Value="Personal Assets">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Add Personal Assets" Value="Add Personal Assets">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Gold Assets" Value="Gold Assets">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Add Gold Assets" Value="Add Gold Assets">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Collectibles" Value="Collectibles">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Add Collectibles" Value="Add Collectibles">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Cash and Savings" Value="Cash and Savings"
                                        Visible="false">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Add Cash and Savings" Value="Add Cash and Savings">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Liabilities" Value="Liabilities">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Add Liability" Value="Add Liability">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Report" Value="Report">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="MF Report" Value="MF Report">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="FP Report" Value="FP Report">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Equity Report" Value="Equity Report">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Multi Asset report" Value="Multi Asset report">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Alerts" Value="Alerts">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="View Notifications" Value="View Notifications">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="MF Alerts" Value="MF Alerts">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="FI Alerts" Value="FI Alerts">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Insurance Alerts" Value="Insurance Alerts">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Equity Alerts" Value="Equity Alerts">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Message" Value="Message_Customer">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Inbox" Value="Inbox_Customer">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="MF Order-Online" Value="MF_Online_Order">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="MF Online Landing Page" Value="MF_Online_Landing_Page">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Transact" Value="Transact">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="NewPurchase" Value="NewPurchase">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="AdditionalPurchase" Value="AdditionalPurchase">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Redeem" Value="Redeem">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="SIP" Value="SIP">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="NFO" Value="NFO">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Switch" Value="MF_Switch">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="SWP" Value="MF_SWP">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Books" Value="Books">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Order Book" Value="OrderBook">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Transaction Book" Value="TransactionBook">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="SIP Book" Value="SIPBook">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="SWPBook" Value="SWP_Book">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="SIP Book" Value="SIPSumBook" Visible="false">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Holdings" Value="Holdings">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="UnitHoldings" Value="UnitHoldings">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="NCD Order-Online" Value="NCD_Online_Order">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Transact" Value="NCDTransact">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="NCD Issue List" Value="NCDIssueList">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="NCD Issue Transact" Value="NCDIssueTransact"
                                                Visible="false">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="NCD Books" Value="NCDBooks">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="NCD Order Book" Value="NCDOrderBook">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="NCD Issue Holdings" Value="NCDHoldings">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="NCDHolding" Value="NCDHolding">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="IPO Order-Online" Value="IPO_Online_Order">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Transact" Value="IPOIssueTransact">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="IPO Issue List" Value="IPOIssueList">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="IPO Issue Book" Value="IPOIssueBook">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="IPO Holding" Value="IPOHolding">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Investor Online Order" Value="INVESTOR_ONLINE_ORDER">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Online Investor Page" Value="ONLINE_INVESTOR_PAGE">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="MF Order" Value="MF_Order_Investor">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="NCD Order" Value="NCD_Order_Investor">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="IPO Order" Value="IPO_Order_Investor">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                        </Items>
                        <ExpandAnimation Type="InCubic" />
                    </telerik:RadPanelBar>
                </td>
            </tr>
            <%-- <tr>
                <td>
                    <telerik:RadPanelBar ID="RPBOnlineOrder" runat="server" EnableEmbeddedSkins="false"
                        ExpandAnimation-Type="InCubic" Skin="Telerik" Width="100%" OnItemClick="RPBOnlineOrder_ItemClick"
                        AllowCollapseAllItems="True" ExpandMode="SingleExpandedItem">
                        <Items>
                            <telerik:RadPanelItem runat="server" Value="Profile Dashboard" Text="OnlineOrder">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="MFOrder" Value="MFOrder">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="MF Online Landing Page" Value="MF_Online_Landing_Page" Visible="true">
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Transact" Value="Transact">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="NewPurchase" Value="NewPurchase">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="AdditionalPurchase" Value="AdditionalPurchase">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Redeem" Value="Redeem">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="SIP" Value="SIP">
                                                    </telerik:RadPanelItem>
                                                     <telerik:RadPanelItem runat="server" Text="NFO" Value="NFO">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Books" Value="Books">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="Order Book" Value="OrderBook">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="Transaction Book" Value="TransactionBook">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="SIP Book" Value="SIPBook">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="SIP Book" Value="SIPSumBook" Visible="false">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="Holdings" Value="Holdings">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="UnitHoldings" Value="UnitHoldings">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                </Items>
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="NCD Order" Value="NCDMFOrder">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Transact" Value="NCDTransact">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="NCD Issue List" Value="NCDIssueList">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="NCD Issue Transact" Value="NCDIssueTransact"
                                                        Visible="false">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="NCD Books" Value="NCDBooks">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="NCD Order Book" Value="NCDOrderBook">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem runat="server" Text="NCD Issue Holdings" Value="NCDHoldings">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="NCDHolding" Value="NCDHolding">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                </Items>
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="IPO Order" Value="IPOOrder">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Transact" Value="IPOIssueTransact">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server" Text="IPO Issue List" Value="IPOIssueList">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="IPO Issue Book" Value="IPOIssueBook">
                                                    </telerik:RadPanelItem>
                                                    <telerik:RadPanelItem runat="server" Text="IPO Holding" Value="IPOHolding">
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                        </Items>
                        <ExpandAnimation Type="InCubic" />
                    </telerik:RadPanelBar>
                </td>
            </tr>--%>
            <tr>
                <td>
                    <div style="display: inline">
                        <asp:TextBox runat="server" ID="txtFindCustomer" Style="width: 110px" onkeypress="JSdoPostback(event,'ctrl_RMCustomerIndividualLeftPane_btnSearchCustomer')" />
                        <cc1:TextBoxWatermarkExtender ID="txtFindCustomer_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtFindCustomer" WatermarkText="Find Customer">
                        </cc1:TextBoxWatermarkExtender>
                        <asp:Button ID="btnSearchCustomer" runat="server" CssClass="SearchButton" OnClientClick="javascript:callSearch('Customer');return false;" />
                    </div>
                </td>
            </tr>
        </table>
          <table>
                        <tr>
                            <td style="visibility:hidden;">
                                <asp:Button ID="btnOnlineOrder" runat="server" OnClientClick="window.document.forms[0].target='_blank'; setTimeout(function(){window.document.forms[0].target='';}, 500);" />
                                <asp:Button ID="btnNCDOnline" runat="server" OnClientClick="window.document.forms[0].target='_blank'; setTimeout(function(){window.document.forms[0].target='';}, 500);" />
                                <asp:Button ID="btnIPOOnline" runat="server" OnClientClick="window.document.forms[0].target='_blank'; setTimeout(function(){window.document.forms[0].target='';}, 500);" />
                                <hr />
                            </td>
                        </tr>
                    </table>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:HiddenField ID="hdnUserRole" runat="server" Visible="true" EnableViewState="true" />
