<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerNonIndividualLeftPane.ascx.cs"
    Inherits="WealthERP.Customer.CustomerNonIndividualLeftPane" %>

<%--<link href="../CSS/ControlsStyleSheet.css" rel="stylesheet" type="text/css" />--%>
<div>
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
        <asp:TreeNode Text="Profile" Value="Profile">
            <%-- <asp:TreeNode NavigateUrl="javascript:loadcontrol('ViewNonIndividualProfile','none');"
                Text="View Profile" Value="View Profile"></asp:TreeNode>--%>
        </asp:TreeNode>
        <asp:TreeNode Text="Bank" Value="Bank">
            <%--<asp:TreeNode NavigateUrl="javascript:loadcontrol('ViewBankDetails','none');" Text="View Bank Details"
                Value="View Bank Details"></asp:TreeNode>--%>
        </asp:TreeNode>
        <asp:TreeNode Text="Financial Planning" Value="FinancialPlanning">
            <asp:TreeNode Text="Reports" Value="Reports"></asp:TreeNode>
        </asp:TreeNode>
    </Nodes>
    <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
        NodeSpacing="0px" VerticalPadding="0px" />
</asp:TreeView>
