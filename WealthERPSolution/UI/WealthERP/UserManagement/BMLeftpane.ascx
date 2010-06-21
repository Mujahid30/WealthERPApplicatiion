<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BMLeftpane.ascx.cs"
    Inherits="WealthERP.UserManagement.BMLeftpane" %>
<asp:TreeView ID="TreeView1" runat="server" ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
    <Nodes>
        <asp:TreeNode Text="Switch Roles" Value="Switch Roles"></asp:TreeNode>
        <asp:TreeNode Text="BM" Value="BM">
            <asp:TreeNode Text="View Branch Details" Value="View Branch Details"></asp:TreeNode>
            <asp:TreeNode Text="Staff" Value="Staff"></asp:TreeNode>
        </asp:TreeNode>
        <asp:TreeNode Text="Customer" Value="Customer">
        </asp:TreeNode>
        <asp:TreeNode Text="MIS" Value="MIS">
            <asp:TreeNode Text="MF" Value="MFMIS"></asp:TreeNode>
            <asp:TreeNode Text="Equity" Value="EQMIS"></asp:TreeNode>
        </asp:TreeNode>
    </Nodes>
    <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
        NodeSpacing="0px" VerticalPadding="0px" />
</asp:TreeView>
