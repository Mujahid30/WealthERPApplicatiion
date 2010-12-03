<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMLeftPane.ascx.cs"
    Inherits="WealthERP.Advisor.RMLeftPane" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

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

            var searchstring = document.getElementById('ctrl_RMLeftPane_txtFindCustomer').value;
            loadsearchcontrol('RMCustomer', 'Customer', searchstring);
        }
    }
</script>

<asp:ScriptManager ID="scptManager" runat="server">
</asp:ScriptManager>
<table style="width: 100%;">
    <tr>
        <td>
            <telerik:RadPanelBar ID="RadPanelBar1" runat="server" ExpandAnimation-Type="InCubic"
                ExpandMode="SingleExpandedItem" Width="210px" EnableEmbeddedSkins="false" Skin="Touchbase"
                OnItemClick="RadPanelBar1_ItemClick">
                <Items>
                    <telerik:RadPanelItem runat="server" Text="Switch Roles" Value="Switch Roles">
                    </telerik:RadPanelItem>
                    <telerik:RadPanelItem runat="server" Text="Dashboard" Value="Dashboard">
                    </telerik:RadPanelItem>
                    <telerik:RadPanelItem runat="server" Text="Profile" Value="Profile">
                    </telerik:RadPanelItem>
                    <telerik:RadPanelItem runat="server" Text="Customers" Value="Customers">
                        <Items>
                            <telerik:RadPanelItem runat="server" Text="Add Customer" Value="Add Customer">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Manage Group Accounts" Value="Customer Group Accounts">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Add" Value="Add Group Account">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Manage Portfolio" Value="Customer Portfolio">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Add" Value="Add Portfolio">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Multi Asset Reports" Value="MultiAssetReports">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Alert Configuration" Value="Customer Alerts">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Notifications" Value="Notifications">
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelItem>
                    <telerik:RadPanelItem Text="MF" Value="MF">
                        <Items>
                            <telerik:RadPanelItem Text="MIS" Value="MMIS">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Text="View Transactions" Value="MView Transactions">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Text="Add Transactions" Value="MAdd Transactions">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Text="MF Reports" Value="MFReports">
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelItem>
                    <telerik:RadPanelItem Text="Equity" Value="Equity">
                        <Items>
                            <telerik:RadPanelItem Text="MIS" Value="EMIS">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Text="View Transactions" Value="EView Transactions">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Text="Add Transactions" Value="EAdd Transactions">
                            </telerik:RadPanelItem>
                        </Items>
                        <%--<asp:TreeNode Text="Equity Reports" Value="EquityReports"  ></asp:TreeNode>--%>
                    </telerik:RadPanelItem>
                    <telerik:RadPanelItem Text="FP" Value="FP">
                        <Items>
                            <telerik:RadPanelItem Text="Prospect List" Value="ProspectList">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Text="Add Prospect" Value="AddProspect">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem Text="Offline Form" Value="DownloadForm" NavigateUrl="~/FP/FinancialPlanning.htm"
                                Target="_blank">
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
