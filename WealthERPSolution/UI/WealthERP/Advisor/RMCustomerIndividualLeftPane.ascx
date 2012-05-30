<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMCustomerIndividualLeftPane.ascx.cs"
    Inherits="WealthERP.Advisor.RMIndividualCustomerLeftPane" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="smCustomerLeftpanel" runat="server">
</asp:ScriptManager>

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
</script>

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
            <tr>
                <td colspan="2">
                    <hr />
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
                            <telerik:RadPanelItem runat="server" Text="Home" Value="Home">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Value="Group Dashboard" Text="Group Dashboard"
                                Visible="false">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Customer Dashboard" Value="Customer Dashboard">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Value="Profile Dashboard" Text="Profile Dashboard">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="View Profile" Value="View Profile">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Value="Edit Profile" Text="Edit Profile">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Bank Details" Value="Bank Details">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Add Bank Details" Value="Add Bank Details">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Demat Account Details" Value="Demat Account Details">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Add Demat Account" Value="Add Demat Account">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Proof" Value="Proof">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Vault Proof" Value="VaultProof">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server"  Value="Customer_Order" Text="Product Order">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Order Management" 
                                    Value="Order_Approval_List" ImagePosition="Right">
                                    </telerik:RadPanelItem>
                                                                     
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Financial Planning" Value="Financial Planning">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Finance Profile" Value="Finance Profile">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" ImageUrl="~/Images/new.gif" ImagePosition="Right"
                                        Text="Assumptions and Preferences" Value="Preferences">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Advisor Notes" Value="Advisor Notes">
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
                            <telerik:RadPanelItem runat="server" Text="Portfolio Dashboard" Value="Portfolio Dashboard">
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
                                    <telerik:RadPanelItem runat="server" Text="MF" Value="MF">
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
                                    <telerik:RadPanelItem runat="server" Text="Cash and Savings" Value="Cash and Savings">
                                        <Items>
                                            <telerik:RadPanelItem runat="server" Text="Add Cash and Savings" Value="Add Cash and Savings">
                                            </telerik:RadPanelItem>
                                        </Items>
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
                            <telerik:RadPanelItem runat="server" Text="Liabilities Dashboard" Value="Liabilities">
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
                            <telerik:RadPanelItem runat="server" Text="Message" Value="Message">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Inbox" Value="Inbox">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                        </Items>
                        <ExpandAnimation Type="InCubic" />
                    </telerik:RadPanelBar>
                </td>
            </tr>
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
    </ContentTemplate>
</asp:UpdatePanel>
<asp:HiddenField ID="hdnUserRole" runat="server" Visible="true" EnableViewState="true" />
