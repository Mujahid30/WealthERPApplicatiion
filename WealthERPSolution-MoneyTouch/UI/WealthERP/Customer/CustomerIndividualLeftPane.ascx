<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerIndividualLeftPane.ascx.cs"
    Inherits="WealthERP.Customer.CustomerLeftPane" %>
<asp:ScriptManager ID="smCustomerLeftpanel" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="upnlCustomerLeftpanel" runat="server">
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
        <asp:TreeView ID="TreeView1" runat="server" Font-Size="X-Small" ImageSet="Arrows"
            ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
            <ParentNodeStyle Font-Bold="False" />
            <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
            <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                VerticalPadding="0px" />
            <Nodes>
                <asp:TreeNode Text="Home" Value="Customer Dashboard"></asp:TreeNode>
                <asp:TreeNode Text="Portfolio Dashboard" Value="Portfolio Dashboard">
                    <asp:TreeNode Text="Equity" Value="Equity">
                        <asp:TreeNode Text="View Equity Transaction" Value="View Equity Transaction"></asp:TreeNode>
                        <asp:TreeNode Text="Add Equity Transaction" Value="Add Equity Transaction"></asp:TreeNode>
                        <asp:TreeNode Text="Add Equity Account" Value="Add Equity Account"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="MF" Value="MF">
                        <asp:TreeNode Text="View MF Transaction" Value="View MF Transaction"></asp:TreeNode>
                        <asp:TreeNode Text="Add MF Transaction" Value="Add MF Transaction"></asp:TreeNode>
                        <asp:TreeNode Text="Add MF Folio" Value="Add MF Folio"></asp:TreeNode>
                        <asp:TreeNode Text="View Systematic Schemes" Value="View Systematic Schemes"></asp:TreeNode>
                        <asp:TreeNode Text="Register Systematic Schemes" Value="Register Systematic Schemes">
                        </asp:TreeNode>
                        <asp:TreeNode Text="View MF Folio" Value="View MF Folio"></asp:TreeNode>
                        <asp:TreeNode Text="MF Reports" Value="MFReports"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="Fixed Income" Value="Fixed Income">
                        <asp:TreeNode Text="Add Fixed Income" Value="Add Fixed Income"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="Govt Savings" Value="Govt Savings">
                        <asp:TreeNode Text="Add Govt Savings" Value="Add Govt Savings"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="Property" Value="Property">
                        <asp:TreeNode Text="Add Property" Value="Add Property"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="Pension And Gratuities" Value="Pension And Gratuities">
                        <asp:TreeNode Text="Add Pension and Gratuities" Value="Add Pension and Gratuities">
                        </asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="Personal Assets" Value="Personal Assets">
                        <asp:TreeNode Text="Add Personal Assets" Value="Add Personal Assets"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="Gold Assets" Value="Gold Assets">
                        <asp:TreeNode Text="Add Gold Assets" Value="Add Gold Assets"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="Collectibles" Value="Collectibles">
                        <asp:TreeNode Text="Add Collectibles" Value="Add Collectibles"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="Cash and Savings" Value="Cash And Savings">
                        <asp:TreeNode Text="Add Cash and Savings" Value="Add Cash and Savings"></asp:TreeNode>
                    </asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="Alert" Value="Alerts">
                    <asp:TreeNode Text="View Notifications" Value="View Notifications"></asp:TreeNode>
                    <asp:TreeNode Text="MF Alerts" Value="MF Alerts"></asp:TreeNode>
                    <asp:TreeNode Text="FI Alerts" Value="FI Alerts"></asp:TreeNode>
                    <asp:TreeNode Text="Insurance Alerts" Value="Insurance Alerts"></asp:TreeNode>
                    <asp:TreeNode Text="Equity Alerts" Value="Equity Alerts"></asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="Profile Dashboard" Value="Profile Dashboard">
                    <asp:TreeNode Text="View Profile" Value="View Profile"></asp:TreeNode>
                    <asp:TreeNode Text="Edit Profile" Value="Edit Profile"></asp:TreeNode>
                    <asp:TreeNode Text="Bank Details" Value="Bank Details">
                        <asp:TreeNode Text="Add Bank Details" Value="Add Bank Details"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="Proof" Value="Proof">
                        <asp:TreeNode Text="Add Proof" Value="Add Proof"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="Income Details" Value="Income Details"></asp:TreeNode>
                    <asp:TreeNode Text="Expense Details" Value="Expense Details"></asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="Insurance" Value="Insurance">
                    <asp:TreeNode Text="Life Insurance" Value="Life Insurance">
                        <asp:TreeNode Text="Add Life Insurance" Value="Add Life Insurance"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="General Insurance" Value="General Insurance">
                        <asp:TreeNode Text="Add General Insurance" Value="Add General Insurance"></asp:TreeNode>
                    </asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="Liabilities Dashboard" Value="Liabilities Dashboard">
                    <asp:TreeNode Text="Add Liability" Value="Add Liability"></asp:TreeNode>
                </asp:TreeNode>
            </Nodes>
            <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                NodeSpacing="0px" VerticalPadding="0px" />
        </asp:TreeView>
    </ContentTemplate>
</asp:UpdatePanel>
