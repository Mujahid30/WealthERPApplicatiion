<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransactionDashBoard.ascx.cs" Inherits="WealthERP.BusinessMIS.TransactionDashBoard" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scptMgr" runat="server" EnablePartialRendering="true">
</asp:ScriptManager>

<table width="100%">
    <tr>
        <td colspan="3">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Transactions
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    </table>
        <table width="100%">
    <tr>
    </tr>
    <tr>
    <td style="width: 15%;"></td>
        <td style="width: 70%;">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Repeater ID="rptTransationTree" runat="server" OnItemCommand="rptTransationTree_ItemCommand" OnItemDataBound="rptTransationTree_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td id="td1" runat="server" align="center">
                                        <div class="divDashBoardMouseInLinks" onmouseover="this.className='divDashBoardMouseOutLinks'"
                                            onmouseout="this.className='divDashBoardMouseInLinks'">
                                            <asp:ImageButton ID="imgbtnTransation1" ImageUrl='<%# Eval("Path1").ToString() %>' runat="server"
                                                Width="70px" CommandName="Tree_Navi_Row1" CommandArgument='<%# Eval("TreeNode1").ToString() %>' />
                                            <br />
                                            <asp:LinkButton ID="lnkTransationTreeNode1" CssClass="FieldName" Text='<%# Eval("TreeNodeText1").ToString() %>'
                                                runat="server" Font-Underline="false" CommandName="Tree_Navi_Row1" CommandArgument='<%# Eval("TreeNode1").ToString() %>'>  
                                            </asp:LinkButton>
                                        </div>
                                    </td>
                                    <td id="td2" runat="server" align="center">
                                        <div class="divDashBoardMouseInLinks" onmouseover="this.className='divDashBoardMouseOutLinks'"
                                            onmouseout="this.className='divDashBoardMouseInLinks'">
                                            <asp:ImageButton ID="imgbtnTransation2"  ImageUrl='<%# Eval("Path2").ToString() %>' runat="server"
                                                Width="70px" CommandName="Tree_Navi_Row2" CommandArgument='<%# Eval("TreeNode2").ToString() %>' />
                                            <br />
                                            <asp:LinkButton ID="lnkTransationTreeNode2" Font-Underline="false" CssClass="FieldName" Text='<%# Eval("TreeNodeText2").ToString() %>'
                                                runat="server" CommandName="Tree_Navi_Row2" CommandArgument='<%# Eval("TreeNode2").ToString() %>'>  
                                            </asp:LinkButton>
                                        </div>
                                    </td>
                                    <td id="td3" runat="server" align="center">
                                        <div class="divDashBoardMouseInLinks" onmouseover="this.className='divDashBoardMouseOutLinks'"
                                            onmouseout="this.className='divDashBoardMouseInLinks'">
                                            <asp:ImageButton ID="imgbtnTransation3" ImageUrl='<%# Eval("Path3").ToString() %>' runat="server"
                                                Width="70px" CommandName="Tree_Navi_Row3" CommandArgument='<%# Eval("TreeNode3").ToString() %>' />
                                            <br />
                                            <asp:LinkButton ID="lnkTransationTreeNode3" CssClass="FieldName" Font-Underline="false" Text='<%# Eval("TreeNodeText3").ToString() %>'
                                                runat="server" CommandName="Tree_Navi_Row3" CommandArgument='<%# Eval("TreeNode3").ToString() %>'>  
                                            </asp:LinkButton>
                                        </div>
                                    </td>
                                    
                                    <td id="td4" runat="server" align="center">
                                        <div class="divDashBoardMouseInLinks" onmouseover="this.className='divDashBoardMouseOutLinks'"
                                            onmouseout="this.className='divDashBoardMouseInLinks'">
                                            <asp:ImageButton ID="imgbtnTransation4" ImageUrl='<%# Eval("Path4").ToString() %>' runat="server"
                                                Width="70px" CommandName="Tree_Navi_Row4" CommandArgument='<%# Eval("TreeNode4").ToString() %>' />
                                            <br />
                                            <asp:LinkButton ID="lnkTransationTreeNode4" CssClass="FieldName" Text='<%# Eval("TreeNodeText4").ToString() %>'
                                                runat="server" Font-Underline="false" CommandName="Tree_Navi_Row4" CommandArgument='<%# Eval("TreeNode4").ToString() %>'>  
                                            </asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                               
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 15%;"></td>
    </tr>
</table>



