<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserSettingsLeftPane.ascx.cs" Inherits="WealthERP.General.UserSettingsLeftPane" %>
<link href="../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<link href="../CSS/ControlsStyleSheet.css" rel="stylesheet" type="text/css" />
<div>
    <table>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblExpressLinks" CssClass="HeaderTextSmall" Text="Express Links" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:TreeView ID="TreeView1" runat="server" Font-Size="X-Small" ImageSet="Arrows"
    ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
    <ParentNodeStyle Font-Bold="False" />
    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
        VerticalPadding="0px" />
    <Nodes>
        <asp:TreeNode Text="Home" Value="Home"></asp:TreeNode>
        <asp:TreeNode Text="Change Password" Value="Change Password"></asp:TreeNode>
         <asp:TreeNode Text="Change Login Id" Value="Change Login Id"></asp:TreeNode>

    </Nodes>
    <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
        NodeSpacing="0px" VerticalPadding="0px" />
</asp:TreeView>