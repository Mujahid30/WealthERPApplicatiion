<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AlertsLeftPane.ascx.cs"
    Inherits="WealthERP.Alerts.AlertsLeftPane" %>
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
        <asp:TreeNode Text="Home" Value="Home"></asp:TreeNode>
        <asp:TreeNode Text="Customer Dashboard" Value="Customer Dashboard"></asp:TreeNode>
        <asp:TreeNode Text="Profile Dashboard" Value="Profile Dashboard"></asp:TreeNode>
        <asp:TreeNode Text="Portfolio Dashboard" Value="Portfolio Dashboard"></asp:TreeNode>
        <asp:TreeNode Text="Alert Dashboard" Value="Alert Dashboard">
            <asp:TreeNode Text="View Notifications" Value="View Notifications"></asp:TreeNode>
            <asp:TreeNode Text="MF Alerts" Value="MF Alerts"></asp:TreeNode>
            <asp:TreeNode Text="FI Alerts" Value="FI Alerts"></asp:TreeNode>
            <asp:TreeNode Text="Insurance Alerts" Value="Insurance Alerts"></asp:TreeNode>
            <asp:TreeNode Text="Equity Alerts" Value="Equity Alerts"></asp:TreeNode>
        </asp:TreeNode>
        <asp:TreeNode Text="Liabilities Dashboard" Value="Liabilities Dashboard">
            <asp:TreeNode Text="Add Liability" Value="Add Liability"></asp:TreeNode>
        </asp:TreeNode>
    </Nodes>
    <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
        NodeSpacing="0px" VerticalPadding="0px" />
</asp:TreeView>
