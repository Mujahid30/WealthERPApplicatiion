<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerIndividualLeftPane.ascx.cs"
    Inherits="WealthERP.Customer.CustomerLeftPane" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ScriptManager ID="smCustomerLeftpanel" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="upnlCustomerLeftpanel" runat="server">
    <ContentTemplate>
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblExpressLinks" CssClass="HeaderTextSmall" Text="Express Links" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        
<telerik:RadPanelBar ID="RadPanelBar1" runat="server" ExpandAnimation-Type="InCubic"
    ExpandMode="SingleExpandedItem" Width="210px" EnableEmbeddedSkins="false" 
    Skin="Touchbase" onitemclick="RadPanelBar1_ItemClick">
    <Items>
        <telerik:RadPanelItem Text="Home" Value="Customer Dashboard">
        </telerik:RadPanelItem>
        <telerik:RadPanelItem Text="Portfolio Dashboard" Value="Portfolio Dashboard">
            <Items>
                <telerik:RadPanelItem Text="Equity" Value="Equity">
                    <Items>
                        <telerik:RadPanelItem Text="View Equity Transaction" Value="View Equity Transaction">
                        </telerik:RadPanelItem>
                        <telerik:RadPanelItem Text="Add Equity Transaction" Value="Add Equity Transaction">
                        </telerik:RadPanelItem>
                        <telerik:RadPanelItem Text="Add Equity Account" Value="Add Equity Account">
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="MF" Value="MF">
                    <Items>
                        <telerik:RadPanelItem Text="View MF Transaction" Value="View MF Transaction">
                        </telerik:RadPanelItem>
                        <telerik:RadPanelItem Text="Add MF Transaction" Value="Add MF Transaction">
                        </telerik:RadPanelItem>
                        <telerik:RadPanelItem Text="Add MF Folio" Value="Add MF Folio">
                        </telerik:RadPanelItem>
                        <telerik:RadPanelItem Text="View Systematic Schemes" Value="View Systematic Schemes">
                        </telerik:RadPanelItem>
                        <telerik:RadPanelItem Text="Register Systematic Schemes" Value="Register Systematic Schemes">
                        </telerik:RadPanelItem>
                        <telerik:RadPanelItem Text="View MF Folio" Value="View MF Folio">
                        </telerik:RadPanelItem>
                        <telerik:RadPanelItem Text="MF Reports" Value="MFReports">
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="Fixed Income" Value="Fixed Income">
                    <Items>
                        <telerik:RadPanelItem Text="Add Fixed Income" Value="Add Fixed Income">
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="Govt Savings" Value="Govt Savings">
                    <Items>
                        <telerik:RadPanelItem Text="Add Govt Savings" Value="Add Govt Savings">
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="Property" Value="Property">
                    <Items>
                        <telerik:RadPanelItem Text="Add Property" Value="Add Property">
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="Pension And Gratuities" Value="Pension And Gratuities">
                    <Items>
                        <telerik:RadPanelItem Text="Add Pension and Gratuities" Value="Add Pension and Gratuities">
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="Personal Assets" Value="Personal Assets">
                    <Items>
                        <telerik:RadPanelItem Text="Add Personal Assets" Value="Add Personal Assets">
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="Gold Assets" Value="Gold Assets">
                    <Items>
                        <telerik:RadPanelItem Text="Add Gold Assets" Value="Add Gold Assets">
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="Collectibles" Value="Collectibles">
                    <Items>
                        <telerik:RadPanelItem Text="Add Collectibles" Value="Add Collectibles">
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="Cash and Savings" Value="Cash And Savings">
                    <Items>
                        <telerik:RadPanelItem Text="Add Cash and Savings" Value="Add Cash and Savings">
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelItem>
        <telerik:RadPanelItem Text="Alert" Value="Alerts">
            <Items>
                <telerik:RadPanelItem Text="View Notifications" Value="View Notifications">
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="MF Alerts" Value="MF Alerts">
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="FI Alerts" Value="FI Alerts">
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="Insurance Alerts" Value="Insurance Alerts">
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="Equity Alerts" Value="Equity Alerts">
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelItem>
        <telerik:RadPanelItem Text="Profile Dashboard" Value="Profile Dashboard">
            <Items>
                <telerik:RadPanelItem Text="View Profile" Value="View Profile">
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="Edit Profile" Value="Edit Profile">
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="Bank Details" Value="Bank Details">
                    <Items>
                        <telerik:RadPanelItem Text="Add Bank Details" Value="Add Bank Details">
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="Proof" Value="Proof">
                    <Items>
                        <telerik:RadPanelItem Text="Add Proof" Value="Add Proof">
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="Income Details" Value="Income Details">
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="Expense Details" Value="Expense Details">
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelItem>
        <telerik:RadPanelItem Text="Insurance" Value="Insurance">
            <Items>
                <telerik:RadPanelItem Text="Life Insurance" Value="Life Insurance">
                    <Items>
                        <telerik:RadPanelItem Text="Add Life Insurance" Value="Add Life Insurance">
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Text="General Insurance" Value="General Insurance">
                    <Items>
                        <telerik:RadPanelItem Text="Add General Insurance" Value="Add General Insurance">
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelItem>
        <telerik:RadPanelItem Text="Liabilities Dashboard" Value="Liabilities Dashboard">
            <Items>
                <telerik:RadPanelItem Text="Add Liability" Value="Add Liability">
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelItem>
        <telerik:RadPanelItem Text="Financial Planning" Value="FinancialPlanning">
            <Items>
                <telerik:RadPanelItem Text="Reports" Value="Reports">
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelItem>
    </Items>
</telerik:RadPanelBar>
