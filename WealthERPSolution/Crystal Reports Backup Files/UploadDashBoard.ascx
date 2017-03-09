<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadDashBoard.ascx.cs"
    Inherits="WealthERP.Uploads.UploadDashBoard" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scptMgr" runat="server" EnablePartialRendering="true">
</asp:ScriptManager>


<table style="width: 100%!Important;">
    <tr>
        <td colspan="3">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            View Exceptions
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="3">
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                MF Exceptions
            </div>
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
        </td>
        <td style="width: 60%;" align="center">
            <asp:Repeater ID="rptMFTree" runat="server" OnItemCommand="rptMFTree_ItemCommand"
                OnItemDataBound="rptMFTree_ItemDataBound">
                <ItemTemplate>
                    <table>
                        <tr>
                       
                            <td id="td1MF" runat="server" align="center" style="width: 25%;">
                            <div class="divRectangleLinks" onmouseout="this.className='divRectangleLinks'" onmouseover="this.className='divRectangleMouseOverLinks'">
                                <asp:LinkButton ID="lnkMFTreeNode1" Font-Underline="false" CssClass="LinkTextBig"  Text='<%# Eval("TreeNodeText1").ToString() %>'
                                    runat="server" CommandName="Tree_MF_Row1" CommandArgument='<%# Eval("TreeNode1").ToString() %>'>  
                                </asp:LinkButton>
                                </div>
                               
                                
                            </td>
                            <td style="width: 10%;">
                            
                            </td>
                            <td id="td2MF" style="width: 25%;" runat="server" align="center">
                            <div id="div2MF" runat="server" class="divRectangleLinks" onmouseout="this.className='divRectangleLinks'" onmouseover="this.className='divRectangleMouseOverLinks'">
                                <asp:LinkButton ID="lnkMFTreeNode2"  Font-Underline="false" CssClass="LinkTextBig" Text='<%# Eval("TreeNodeText2").ToString() %>'
                                    runat="server" CommandName="Tree_MF_Row2" CommandArgument='<%# Eval("TreeNode2").ToString() %>'>  
                                </asp:LinkButton>
                                </div>
                            </td>
                           <%-- <td id="td3MF" runat="server" align="center">
                                <asp:LinkButton ID="lnkMFTreeNode3" CssClass="HeaderTextBig" Text='<%# Eval("TreeNodeText3").ToString() %>'
                                    runat="server" CommandName="Tree_MF_Row3" CommandArgument='<%# Eval("TreeNode3").ToString() %>'>  
                                </asp:LinkButton>
                            </td>
                            <td id="td4MF" runat="server" align="center">
                                <asp:LinkButton ID="lnkMFTreeNode4" CssClass="HeaderTextBig" Text='<%# Eval("TreeNodeText4").ToString() %>'
                                    runat="server" CommandName="Tree_MF_Row4" CommandArgument='<%# Eval("TreeNode4").ToString() %>'>  
                                </asp:LinkButton>
                            </td>--%>
                        </tr>
                    </table>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
        </td>
        <td style="width: 20%;">
        </td>
    </tr>
    <tr>
    <td colspan="3"></td>
    </tr>
    <%--<tr>
        <td colspan="3">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                EQ Exceptions
            </div>
        </td>
    </tr>--%>
    <%--<tr>
        <td style="width: 20%;">
        </td>
        <td style="width: 60%;" align="center">
            <asp:Repeater ID="rptTreenodeEQ" runat="server" OnItemCommand="rptTreenodeEQ_ItemCommand"
                OnItemDataBound="rptTreenodeEQ_ItemDataBound">
                <ItemTemplate>
                    <table>
                        <tr>
                            <td id="td1EQ" runat="server" align="center" style="width: 25%;">
                              <div class="divRectangleLinks"  onmouseout="this.className='divRectangleLinks'" onmouseover="this.className='divRectangleMouseOverLinks'">
                                <asp:LinkButton ID="lnkEQTreeNode1" CssClass="LinkTextBig" Text='<%# Eval("TreeNodeText1").ToString() %>'
                                    runat="server" Font-Underline="false" CommandName="Tree_EQ_Row1" CommandArgument='<%# Eval("TreeNode1").ToString() %>'>  
                                </asp:LinkButton>
                                </div>
                            </td>
                            <td style="width: 10%;"></td>
                            <td id="td2EQ" runat="server" align="center" style="width: 25%;">
                              <div class="divRectangleLinks"  onmouseout="this.className='divRectangleLinks'" onmouseover="this.className='divRectangleMouseOverLinks'">
                                <asp:LinkButton ID="lnkEQTreeNode2" CssClass="LinkTextBig" Font-Underline="false" Text='<%# Eval("TreeNodeText2").ToString() %>'
                                    runat="server" CommandName="Tree_EQ_Row2" CommandArgument='<%# Eval("TreeNode2").ToString() %>'>  
                                </asp:LinkButton>
                                </div>
                            </td>
                                <td id="td3EQ" runat="server" align="center">
                                <asp:LinkButton ID="lnkEQTreeNode3" CssClass="HeaderTextBig" Text='<%# Eval("TreeNodeText3").ToString() %>'
                                    runat="server" CommandName="Tree_EQ_Row3" CommandArgument='<%# Eval("TreeNode3").ToString() %>'>  
                                </asp:LinkButton>
                            </td>
                            <td id="td4EQ" runat="server" align="center">
                                <asp:LinkButton ID="lnkEQTreeNode4" CssClass="HeaderTextBig" Text='<%# Eval("TreeNodeText4").ToString() %>'
                                    runat="server" CommandName="Tree_EQ_Row4" CommandArgument='<%# Eval("TreeNode4").ToString() %>'>  
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
        </td>
        <td style="width: 20%;">
        </td>
    </tr>
     <tr>
    --%>    <td colspan="3">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                FI Exceptions
            </div>
        </td>
    </tr>
    <tr>
        <td style="width: 18%;">
        </td>
        <td style="width: 50%;" align="center">
            <asp:Repeater ID="rptTreenodeFI" runat="server" OnItemCommand="rptTreenodeFI_ItemCommand"
                OnItemDataBound="rptTreenodeFI_ItemDataBound">
                <ItemTemplate>
                    <table>
                        <tr>
                            <td id="td1FI" runat="server" align="center" style="width:16.5%;">
                              <div class="divRectangleLinks" onmouseout="this.className='divRectangleLinks'" onmouseover="this.className='divRectangleMouseOverLinks'">
                                 <asp:LinkButton ID="lnkFITreeNode1" CssClass="LinkTextBig" Text='<%# Eval("TreeNodeText1").ToString() %>'
                                    runat="server" Font-Underline="false" CommandName="Tree_FI_Row1" CommandArgument='<%# Eval("TreeNode1").ToString() %>'>  
                                </asp:LinkButton>
                                </div>
                            </td>
                            <td style="width: 23.5%;"></td>
                          <%-- <td id="td2EQ" runat="server" align="center" visible="false">
                              <div class="divRectangleLinks"  onmouseout="this.className='divRectangleLinks'" onmouseover="this.className='divRectangleMouseOverLinks'">
                                <asp:LinkButton ID="lnkEQTreeNode2" CssClass="LinkTextBig" Font-Underline="false" Text='<%# Eval("TreeNodeText2").ToString() %>'
                                    runat="server" CommandName="Tree_EQ_Row2" CommandArgument='<%# Eval("TreeNode2").ToString() %>'>  
                                </asp:LinkButton>
                                </div>
                            </td>--%>
                          <%--  <td id="td3EQ" runat="server" align="center">
                                <asp:LinkButton ID="lnkEQTreeNode3" CssClass="HeaderTextBig" Text='<%# Eval("TreeNodeText3").ToString() %>'
                                    runat="server" CommandName="Tree_EQ_Row3" CommandArgument='<%# Eval("TreeNode3").ToString() %>'>  
                                </asp:LinkButton>
                            </td>
                            <td id="td4EQ" runat="server" align="center">
                                <asp:LinkButton ID="lnkEQTreeNode4" CssClass="HeaderTextBig" Text='<%# Eval("TreeNodeText4").ToString() %>'
                                    runat="server" CommandName="Tree_EQ_Row4" CommandArgument='<%# Eval("TreeNode4").ToString() %>'>  
                                </asp:LinkButton>
                            </td>--%>
                        </tr>
                    </table>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
        </td>
        <td style="width: 20%;">
        </td>
    </tr>
</table>
