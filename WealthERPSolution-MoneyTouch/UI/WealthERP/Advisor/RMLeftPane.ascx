<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMLeftPane.ascx.cs"
    Inherits="WealthERP.Advisor.RMLeftPane" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
            <asp:TreeView ID="TreeView1" runat="server" Font-Size="X-Small" ImageSet="Arrows"
                ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                <ParentNodeStyle Font-Bold="False" />
                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                    VerticalPadding="0px" />
                <Nodes>
                    <asp:TreeNode Text="Switch Roles" Value="Switch Roles"></asp:TreeNode>
                    <asp:TreeNode Text="Dashboard" Value="Dashboard"></asp:TreeNode>
                    <asp:TreeNode Text="Profile" Value="Profile"></asp:TreeNode>
                    <asp:TreeNode Text="Customer" Value="Customers">
                        <asp:TreeNode Text="Add Customer" Value="Add Customer"></asp:TreeNode>
                        <asp:TreeNode Text="Manage Group Accounts" Value="Customer Group Accounts">
                            <asp:TreeNode Text="Add" Value="Add Group Account"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="Manage Portfolio" Value="Customer Portfolio">
                            <asp:TreeNode Text="Add" Value="Add Portfolio"></asp:TreeNode>
                        </asp:TreeNode>
                                              
                        <asp:TreeNode Text="Multi Asset Reports" Value="MultiAssetReports"></asp:TreeNode>
                        <asp:TreeNode Text="Alert Configuration" Value="Customer Alerts"></asp:TreeNode>
                        <asp:TreeNode Text="Notifications" Value="Notifications"></asp:TreeNode>
                        
                    </asp:TreeNode>
                    <%--<asp:TreeNode Text="Loan" Value="Loan">
                        <asp:TreeNode Text="Loan Proposal" Value="Loan Proposal"></asp:TreeNode>
                        <asp:TreeNode Text="Add Loan Proposal" Value="Add Loan Proposal"></asp:TreeNode>
                        <asp:TreeNode Text="LoanMIS" Value="LoanMIS"></asp:TreeNode>
                    </asp:TreeNode>--%>
                    <asp:TreeNode Text="MF" Value="MF">
                        <asp:TreeNode Text="MIS" Value="MMIS"></asp:TreeNode>
                        <asp:TreeNode Text="View Transactions" Value="MView Transactions"></asp:TreeNode>
                        <asp:TreeNode Text="Add Transactions" Value="MAdd Transactions"></asp:TreeNode>
                        <asp:TreeNode Text="MF Reports" Value="MFReports"></asp:TreeNode>
                      
                    </asp:TreeNode>
                    <asp:TreeNode Text="Equity" Value="Equity">
                        <asp:TreeNode Text="MIS" Value="EMIS"></asp:TreeNode>
                        <asp:TreeNode Text="View Transactions" Value="EView Transactions"></asp:TreeNode>
                        <asp:TreeNode Text="Add Transactions" Value="EAdd Transactions"></asp:TreeNode>
                         <%--<asp:TreeNode Text="Equity Reports" Value="EquityReports"  ></asp:TreeNode>--%>
                    </asp:TreeNode>
                </Nodes>
                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                    NodeSpacing="0px" VerticalPadding="0px" />
            </asp:TreeView>
        </td>
    </tr>
    <tr>
        <td class="style1">
            &nbsp;
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
