<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMCustomerNonIndividualLeftPane.ascx.cs"
    Inherits="WealthERP.Advisor.RMCustomerNonIndividualLeftPane" %>
<div>
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
            <td colspan="2">
                <asp:Label ID="lblExpressLinks" CssClass="HeaderTextSmall" Text="Express Links" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</div>
<asp:TreeView ID="TreeView1" runat="server" Font-Size="X-Small" ImageSet="Arrows"
    ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
    <ParentNodeStyle Font-Bold="False" />
    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
        VerticalPadding="0px" />
    <Nodes>
        <asp:TreeNode Text="Home" Value="Home">
            <asp:TreeNode Text="RM Home" Value="RM Home"></asp:TreeNode>
            <asp:TreeNode Text="Customer Dashboard" Value="Customer Dashboard"></asp:TreeNode>
        </asp:TreeNode>
        <asp:TreeNode Text="Profile Dashboard" Value="Profile Dashboard">
            <asp:TreeNode Text="View Profile" Value="View Profile"></asp:TreeNode>
            <asp:TreeNode Text="Edit Profile" Value="Edit Profile"></asp:TreeNode>
            <asp:TreeNode Text="Bank Details" Value="Bank Details">
                <asp:TreeNode Text="Add Bank Details" Value="Add Bank Details"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Proof" Value="Proofs">
                <asp:TreeNode Text="Add Proof" Value="Add Proof"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Group Account" Value="Group Account">
                <asp:TreeNode Text="Add Group Member" Value="Add Group Member"></asp:TreeNode>
                <asp:TreeNode Text="Associate Member" Value="Associate Member"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode Text="Portfolio Details" Value="Portfolio Details"></asp:TreeNode>
        </asp:TreeNode>
        <asp:TreeNode Text="Financial Planning" Value="FinancialPlanning">
            <asp:TreeNode Text="Customer Prospect" Value="CustomerProspect" Selected="true">
            </asp:TreeNode>
            <asp:TreeNode Text="RiskProfile & AssetAllocation " Value="RiskProfileAssetAllocation">
            </asp:TreeNode>
            <asp:TreeNode Text="Goal Profiling" Value="GoalProfiling"></asp:TreeNode>
            <asp:TreeNode Text="Reports" Value="Reports"></asp:TreeNode>
        </asp:TreeNode>
    </Nodes>
    <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
        NodeSpacing="0px" VerticalPadding="0px" />
</asp:TreeView>
