<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisorRM.ascx.cs" Inherits="WealthERP.UserManagement.AdvisorRM" %>
<asp:TreeView ID="TreeView1" runat="server">
    <Nodes>
        <asp:TreeNode Text="Roles" Value="Roles">
            <asp:TreeNode Text="Advisor" Value="Advisor"></asp:TreeNode>
            <asp:TreeNode Text="RM" Value="RM"></asp:TreeNode>
        </asp:TreeNode>
    </Nodes>
</asp:TreeView>
