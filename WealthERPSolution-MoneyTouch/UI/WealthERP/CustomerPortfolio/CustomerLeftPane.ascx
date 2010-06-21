<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerLeftPane.ascx.cs" Inherits="WealthERP.CustomerPortfolio.CustomerLeftPane" %>
<asp:TreeView ID="TreeView1" runat="server" ImageSet="Simple" 
    onselectednodechanged="TreeView1_SelectedNodeChanged" BorderStyle="Double">
    <ParentNodeStyle Font-Bold="False" />
    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" 
        HorizontalPadding="0px" VerticalPadding="0px" />
    <Nodes>
        <asp:TreeNode NavigateUrl="javascript:loadcontrol('EquityManualSingleTransaction','none');" Text="Home" Value="Home">
            <asp:TreeNode NavigateUrl="javascript:void(0);" Text="Equity" Value="Equity">
                <asp:TreeNode NavigateUrl="javascript:loadcontrol('EquityManualSingleTransaction','none');" Text="Manual Single" Value="Manual Single"></asp:TreeNode>
            </asp:TreeNode>
            <asp:TreeNode NavigateUrl="javascript:void(0);" Text="MF" Value="MF"></asp:TreeNode>
        </asp:TreeNode>
    </Nodes>
    <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" 
        HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />  
</asp:TreeView>
