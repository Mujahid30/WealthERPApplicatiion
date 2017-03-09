<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FinancialPlanningLeftPane.ascx.cs"
    Inherits="WealthERP.Advisor.FinancialPlanningLeftPane" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="upnlAdviserleftpane" runat="server">
    <ContentTemplate>
        <table style="vertical-align: top">
            <tr>
                <td>
                    <label id="lblExpressLinks" class="HeaderTextSmall">
                        Express Links</label>
                </td>
            </tr>
            <tr>
                <td valign="top" height="400">
                    <asp:TreeView ID="TreeView1" runat="server" Font-Size="X-Small" ImageSet="Arrows"
                        ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                        <ParentNodeStyle Font-Bold="False" ForeColor="#000000"/>
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <Nodes>
                        <asp:TreeNode Text="RiskProfile & AssetAllocation " Value="RiskProfileAssetAllocation"></asp:TreeNode>
                        <asp:TreeNode Text="Goal Profiling" Value="GoalProfiling"></asp:TreeNode>
                        <asp:TreeNode Text="Reports" Value="Reports"></asp:TreeNode>
                        </Nodes>
                        <NodeStyle  Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                    NodeSpacing="0px" VerticalPadding="0px"/>
                    </asp:TreeView>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
