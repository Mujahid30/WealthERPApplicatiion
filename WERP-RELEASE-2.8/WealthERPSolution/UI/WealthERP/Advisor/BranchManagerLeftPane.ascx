<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BranchManagerLeftPane.ascx.cs"
    Inherits="WealthERP.Advisor.BranchManagerLeftPane" %>
<asp:TreeView ID="TreeView1" runat="server" Height="120px" ShowLines="True" Width="110px"
    Font-Size="X-Small" NodeIndent="10">
    <ParentNodeStyle Font-Bold="False" />
    <HoverNodeStyle BackColor="#CCCCCC" BorderColor="#888888" BorderStyle="Solid" Font-Underline="True" />
    <SelectedNodeStyle BackColor="White" BorderColor="#888888" BorderStyle="Solid" BorderWidth="1px"
        Font-Underline="False" HorizontalPadding="3px" VerticalPadding="1px" />
    <Nodes>
        <asp:TreeNode Text="Home" Value="Home">
            <asp:TreeNode Text="View Profile" Value="View Profile"></asp:TreeNode>
            <asp:TreeNode Text="View RMs" Value="View RMs"></asp:TreeNode>
            <asp:TreeNode Text="View Branch Details" Value="View Branch Details"></asp:TreeNode>
        </asp:TreeNode>
    </Nodes>
    <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
        NodeSpacing="1px" VerticalPadding="2px" />
</asp:TreeView>
