<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMCustomerIndividualLeftPane.ascx.cs"
    Inherits="WealthERP.Advisor.RMIndividualCustomerLeftPane" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="smCustomerLeftpanel" runat="server">
</asp:ScriptManager>

<script id="pagescript" type="text/javascript" language="javascript">
    function callSearch(searchtype) {
        if (searchtype == "RM") {
            var searchstring = document.getElementById('ctrl_RMLeftPane_txtFindRM').value;
            loadsearchcontrol('ViewRM', 'RM', searchstring);
        }
        else if (searchtype == "Branch") {
            var searchstring = document.getElementById('ctrl_RMLeftPane_txtFindBranch').value;
            loadsearchcontrol('ViewBranches', 'Branch', searchstring);
        }
        else if (searchtype == "Customer") {

            var searchstring = document.getElementById('<%= txtFindCustomer.ClientID %>').value;
            var userRole = document.getElementById('<%=hdnUserRole.ClientID %>').value;
            if (userRole == "RM") {
                loadsearchcontrol('RMCustomer', 'Customer', searchstring);
            }
            else if (userRole == "Adviser") {
                loadsearchcontrol('AdviserCustomer', 'Customer', searchstring);
            }
            else if (userRole == "BM") {
                loadsearchcontrol('BMCustomer', 'Customer', searchstring);
            }
        }
    }
</script>

<asp:UpdatePanel ID="upnlCustomerLeftpanel" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblClientInfo" CssClass="HeaderTextSmall" Text="Customer Contact Info"
                        runat="server"></asp:Label>
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
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <label id="lblExpressLinks" class="HeaderTextSmall">
                        Express Links</label>
                </td>
            </tr>
            <tr>
                <td>
                    
                    <telerik:RadPanelBar ID="RadPanelBar1" runat="server" expandanimation-type="InCubic"
                expandmode="SingleExpandedItem" width="210px" enableembeddedskins="false" 
                skin="Touchbase" onitemclick="RadPanelBar1_ItemClick">
                        <Items>
                            <telerik:RadPanelItem runat="server" Text="Home" Value="Rm Home">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Text="Customer Dashboard" Value="Customer Dashboard">
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
                                    <telerik:RadPanelItem Text="View Equity Account" Value="View Equity Account">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Text="MF" Value="MF">
                                <Items>
                                    <telerik:RadPanelItem Text="View MF Transaction" Value="View MF Transaction">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem Text="Add MF Transaction" Value="Add MF Transaction">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem Text="View MF Folio" Value="View MF Folio">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem Text="Add MF Folio" Value="Add MF Folio">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem Text="View Systematic Schemes" Value="View Systematic Schemes">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem Text="Register Systematic Schemes" Value="Register Systematic Schemes">
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
                                    <%--NavigateUrl="javascript:OnTreeClick(this);"--%>
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
                            <telerik:RadPanelItem Text="Alerts Dashboard" Value="Alerts">
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
                                    <telerik:RadPanelItem Text="Demat Account Details" Value="Demat Account Details">
                                        <Items>
                                            <telerik:RadPanelItem Text="Add Demat Account" Value="Add Demat Account">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem Text="Proof" Value="Proof">
                                        <Items>
                                            <telerik:RadPanelItem Text="Add Proof" Value="Add Proof">
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelItem>
                                    <%--<telerik:RadPanelItemText="Portfolio Details" Value="Portfolio Details"></telerik:RadPanelItem>--%>
                                    <telerik:RadPanelItem Text="Income Details" Value="Income Details">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem Text="Expense Details" Value="Expense Details">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem Text="Advisor Notes" Value="Advisor Notes">
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
                            <telerik:RadPanelItem Text="Financial Planning" Value="Financial Planning">
                                <Items>
                                    <telerik:RadPanelItem Text="Finance Profile" Value="FinanceProfile" Selected="true">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem Text="RiskProfile & AssetAllocation " Value="RiskProfileAssetAllocation">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem Text="Goal Profiling" Value="GoalProfiling">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem Text="Reports" Value="Reports">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelBar>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="display: inline">
                        <asp:TextBox runat="server" ID="txtFindCustomer" Style="width: 110px" onkeypress="JSdoPostback(event,'ctrl_RMLeftPane_btnSearchCustomer')" />
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
