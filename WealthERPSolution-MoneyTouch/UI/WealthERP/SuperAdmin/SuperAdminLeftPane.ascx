<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SuperAdminLeftPane.ascx.cs" Inherits="WealthERP.SuperAdmin.SuperAdminLeftPane" %>
<asp:ScriptManager ID="smCustomerLeftpanel" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="upnlSuperAdminLeftpanel" runat="server">
    <ContentTemplate>
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblExpressLinks" CssClass="HeaderTextSmall" Text="Express Links" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <asp:TreeView ID="SuperAdminTreeView" runat="server" Font-Size="X-Small" ImageSet="Arrows"
            ShowLines="True" OnSelectedNodeChanged="SuperAdminTreeView_SelectedNodeChanged">
            <ParentNodeStyle Font-Bold="False" />
            <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
            <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                VerticalPadding="0px" />
            <Nodes>
                <%--<asp:TreeNode Text="Home" Value="SuperAdminHome"></asp:TreeNode>--%>
                <asp:TreeNode Text="IFF" Value="IFF">
                    <asp:TreeNode Text="Add" Value="IFFAdd">                        
                    </asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="MessageBroadcast" Value="MessageBroadcast">
                </asp:TreeNode>
               <%-- <asp:TreeNode Text="MIS" Value="MFMIS">
                    <asp:TreeNode Text="MF" Value="MF"></asp:TreeNode>
                    <asp:TreeNode Text="Equity" Value="Equity"></asp:TreeNode>
                    <asp:TreeNode Text="Loan" Value="Loan"></asp:TreeNode>
                    </asp:TreeNode>--%>
                   <%-- <asp:TreeNode Text="User Management" Value="UserManagement">
                    </asp:TreeNode>--%>
                <asp:TreeNode Text="Loan Scheme" Value="LoanScheme">
                    <asp:TreeNode Text="Add" Value="AddLoanScheme">
                    </asp:TreeNode>
                </asp:TreeNode>
            </Nodes>
            <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                NodeSpacing="0px" VerticalPadding="0px" />
        </asp:TreeView>
    </ContentTemplate>
</asp:UpdatePanel>
