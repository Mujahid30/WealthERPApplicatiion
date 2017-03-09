<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BMLeftpane.ascx.cs"
    Inherits="WealthERP.UserManagement.BMLeftpane" %>
<asp:TreeView ID="BMLeftTree" runat="server" ShowLines="True" 
    onselectednodechanged="BMLeftTree_SelectedNodeChanged" >
    <Nodes>
        <asp:TreeNode Text="Switch Roles" Value="Switch Roles"></asp:TreeNode>
        <asp:TreeNode Text="Home" Value="Dashboard">
        </asp:TreeNode>
         <asp:TreeNode Text="Staff" Value="Staff">
        </asp:TreeNode>
        <asp:TreeNode Text="Customer" Value="Customer">
        </asp:TreeNode>
        <asp:TreeNode Text="MF" Value="MF">
            <asp:TreeNode Text="MIS" Value="MFMIS"></asp:TreeNode>
        </asp:TreeNode>
        <asp:TreeNode Text="Equity" Value="Equity">
            <asp:TreeNode Text="MIS" Value="EQMIS"></asp:TreeNode>
        </asp:TreeNode>
    </Nodes>
    <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
        NodeSpacing="0px" VerticalPadding="0px" />
</asp:TreeView>
