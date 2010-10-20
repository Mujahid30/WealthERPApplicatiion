<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMCustomerIndividualLeftPane.ascx.cs"
    Inherits="WealthERP.Advisor.RMIndividualCustomerLeftPane" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:ScriptManager ID="smCustomerLeftpanel" runat="server">
</asp:ScriptManager>

<script id="pagescript" type="text/javascript" language="javascript">
    function callSearch(searchtype) {
        if (searchtype == "RM") {
            var searchstring = document.getElementById('ctrl_RMLeftPane_txtFindRM').value;
            loadsearchcontrol('ViewRM', 'RM', searchstring);
        }
        else if (searchtype == "Branch") {
            var searchstring = document.getElementById('ctrl_RMLeftPane_txtFindBranch').value;
            loadsearchcontrol('ViewBranches', 'Branch', searchstring);
        }
        else if (searchtype == "Customer") {

        var searchstring = document.getElementById('<%= txtFindCustomer.ClientID %>').value;
            loadsearchcontrol('RMCustomer', 'Customer', searchstring);
        }
    }
</script>

<asp:UpdatePanel ID="upnlCustomerLeftpanel" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblClientInfo" CssClass="HeaderTextSmall" Text="Customer Contact Info"
                        runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblName" Text="Name:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblNameValue" Text="" runat="server" CssClass="Field"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEmailId" Text="EmailID:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblEmailIdValue" Text="" runat="server" CssClass="Field"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td colspan="2">
                    <label id="lblExpressLinks" class="HeaderTextSmall">
                        Express Links</label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TreeView ID="TreeView1" runat="server" Font-Size="X-Small" ImageSet="Arrows"
                        ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <Nodes>
                            <asp:TreeNode Text="Home" Value="RM Home"></asp:TreeNode>
                            <asp:TreeNode Text="Customer Dashboard" Value="Customer Dashboard"></asp:TreeNode>
                            <asp:TreeNode Text="Portfolio Dashboard" Value="Portfolio Dashboard"></asp:TreeNode>
                            <asp:TreeNode Text="Alerts" Value="Alerts"></asp:TreeNode>
                            <asp:TreeNode Text="Profile Dashboard" Value="Profile Dashboard">
                                <asp:TreeNode Text="View Profile" Value="View Profile"></asp:TreeNode>
                                <asp:TreeNode Text="Edit Profile" Value="Edit Profile"></asp:TreeNode>
                                <asp:TreeNode Text="Bank Details" Value="Bank Details">
                                    <asp:TreeNode Text="Add Bank Details" Value="Add Bank Details"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Text="Demat Account Details" Value="Demat Account Details">
                                    <asp:TreeNode Text="Add Demat Account" Value="Add Demat Account"></asp:TreeNode>
                                </asp:TreeNode>
                                <%--<asp:TreeNode Text="Group Accounts" Value="Group Accounts">
                <asp:TreeNode Text="Add New Customer" Value="Add Group Member"></asp:TreeNode>
                <asp:TreeNode Text="Associate Existing Customer" Value="Associate Member"></asp:TreeNode>
            </asp:TreeNode>--%>
                                <asp:TreeNode Text="Proof" Value="Proof">
                                    <asp:TreeNode Text="Add Proof" Value="Add Proof"></asp:TreeNode>
                                </asp:TreeNode>
                                <%--<asp:TreeNode Text="Portfolio Details" Value="Portfolio Details"></asp:TreeNode>--%>
                                <asp:TreeNode Text="Income Details" Value="Income Details"></asp:TreeNode>
                                <asp:TreeNode Text="Expense Details" Value="Expense Details"></asp:TreeNode>
                                <asp:TreeNode Text="Advisor Notes" Value="Advisor Notes"></asp:TreeNode>
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
                </td>
            </tr>
            <tr>
                <td>
                    <div style="display: inline">
                        <asp:TextBox runat="server" ID="txtFindCustomer" Style="width: 110px" onkeypress="JSdoPostback(event,'ctrl_RMLeftPane_btnSearchCustomer')" />
                        <cc1:textboxwatermarkextender id="txtFindCustomer_TextBoxWatermarkExtender" runat="server"
                            targetcontrolid="txtFindCustomer" watermarktext="Find Customer">
                </cc1:textboxwatermarkextender>
                        <asp:Button ID="btnSearchCustomer" runat="server" CssClass="SearchButton" OnClientClick="javascript:callSearch('Customer');return false;" />
                    </div>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
