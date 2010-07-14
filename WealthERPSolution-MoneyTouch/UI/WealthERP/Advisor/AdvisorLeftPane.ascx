 <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisorLeftPane.ascx.cs"
    Inherits="WealthERP.Advisor.AdvisorLeftPane" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<script id="pagescript" type="text/javascript" language="javascript">
    function callSearchControl(searchtype) {
        if (searchtype == "RM") {
            var searchstring = document.getElementById('ctrl_AdvisorLeftPane_txtFindRM').value;
            loadsearchcontrol('ViewRM', 'RM', searchstring);
        }
        else if (searchtype == "Branch") {
            var searchstring = document.getElementById('ctrl_AdvisorLeftPane_txtFindBranch').value;
            loadsearchcontrol('ViewBranches', 'Branch', searchstring);
        }
        else if (searchtype == "AdviserCustomer") {
            var searchstring = document.getElementById('ctrl_AdvisorLeftPane_txtFindAdviserCustomer').value;
            loadsearchcontrol('AdviserCustomer', 'Customer', searchstring);
        }
        else if (searchtype == "Customer") {
            var searchstring = document.getElementById('ctrl_AdvisorLeftPane_txtFindCustomer').value;
            loadsearchcontrol('RMCustomer', 'Customer', searchstring);
        }
    }
</script>

<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="upnlAdviserleftpane" runat="server">
    <ContentTemplate>
        <table style="valign: top">
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
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <Nodes>
                            <%--<asp:TreeNode Text="Customers" Value="Customers"></asp:TreeNode>--%>
                            <%--</asp:TreeNode>--%><asp:TreeNode Text="Switch Roles" Value="Switch Roles"></asp:TreeNode>
                            <asp:TreeNode Text="Admin Home" Value="Advisor Home"></asp:TreeNode>
                            <asp:TreeNode Text="Profile" Value="Profile">
                                <asp:TreeNode Text="Edit Profile" Value="Edit Profile"></asp:TreeNode>
                                <%--    <asp:TreeNode Text="Edit User Details" Value="Edit User Details"></asp:TreeNode>--%>
                            </asp:TreeNode>
                            <asp:TreeNode Text="Preferences" Value="Preferences">
                                <asp:TreeNode Text="Setup Associate Category" Value="SetupAssociateCategory"></asp:TreeNode>
                                <asp:TreeNode Text=" Setup Advisor Staff SMTP" Value="AdviserStaffSMTP"></asp:TreeNode>
                                <%--<asp:TreeNode Text="Set Theme" Value="Set Theme" />--%>
                            </asp:TreeNode>
                            <asp:TreeNode Text="LOB" Value="LOB">
                                <%--<asp:TreeNode Text="Add LOB" Value="Add LOB"></asp:TreeNode>--%>
                            </asp:TreeNode>
                            <asp:TreeNode Text="Staff" Value="Staff">
                                <asp:TreeNode Text="Add Staff" Value="Add Staff"></asp:TreeNode>
                                <%--<asp:TreeNode Text="Add Branch Association" Value="Add Branch Association"></asp:TreeNode>--%>
                            </asp:TreeNode>
                            <asp:TreeNode Text="Branch / Association" Value="Branch">
                                <asp:TreeNode Text="Add Branch/Associates" Value="Add Branch"></asp:TreeNode>
                                <asp:TreeNode Text="View Branch Association" Value="View Branch Association"></asp:TreeNode>
                                <%--<asp:TreeNode Text="Add Branch Association" Value="Add Branch Association"></asp:TreeNode>--%>
                            </asp:TreeNode>
                            <asp:TreeNode Text="Uploads" Value="Uploads">
                                <asp:TreeNode Text="Upload History" Value="Process Log"></asp:TreeNode>
                                <asp:TreeNode Text="View Trans Rejects" Value="Rejected Records"></asp:TreeNode>
                                <asp:TreeNode Text="View Rejected Folio" Value="Reject Folios"></asp:TreeNode>
                            </asp:TreeNode>
                            <asp:TreeNode Text="Customer" Value="Customer">
                                <asp:TreeNode Text="Customer Accounts" Value="Customer Accounts"></asp:TreeNode>
                                <%--<asp:TreeNode Text="Add Branch Association" Value="Add Branch Association"></asp:TreeNode>--%>
                            </asp:TreeNode>
                            <asp:TreeNode Text="User Management" Value="User Management">
                                <%--<asp:TreeNode Text="Admin User Details" Value="Admin User Details">
                        </asp:TreeNode>--%>
                                <asp:TreeNode Text="RM Details" Value="RM Details"></asp:TreeNode>
                                <asp:TreeNode Text="Customer Details" Value="Customer Details"></asp:TreeNode>
                            </asp:TreeNode>
                           
                            <asp:TreeNode Text="Daily Process" Value="Daily Process">
                                <asp:TreeNode Text="Valuation" Value="Valuation"></asp:TreeNode>
                            </asp:TreeNode>
                            <asp:TreeNode Text="SMS" Value="SMS">
                            <asp:TreeNode Text="Alerts SMS" Value="CustomerSMSAlerts"></asp:TreeNode>
                            <asp:TreeNode Text="Customized SMS" Value="SendSMS"></asp:TreeNode>
                            </asp:TreeNode>
                            <asp:TreeNode Text="MF" Value="MF">
                                <asp:TreeNode Text="MIS" Value="MMIS"></asp:TreeNode>
                                <asp:TreeNode Text="View Transactions" Value="MView Transactions"></asp:TreeNode>
                                <asp:TreeNode Text="Add Transactions" Value="MAddTransactions"></asp:TreeNode>
                                <asp:TreeNode Text="MF Reports" Value="MFReports"></asp:TreeNode>
                                <asp:TreeNode Text="Systematic Recon" Value="CustomerMFSystematicTransactionReport"></asp:TreeNode>
                                <asp:TreeNode Text="Reversal Trxn Exception Handling" Value="MFReversal"></asp:TreeNode>
                            </asp:TreeNode>
                            <asp:TreeNode Text="Equity" Value="Equity">
                                <asp:TreeNode Text="MIS" Value="EMIS"></asp:TreeNode>
                                <asp:TreeNode Text="View Transactions" Value="EView Transactions"></asp:TreeNode>
                                <asp:TreeNode Text="Add Transactions" Value="EAdd Transactions"></asp:TreeNode>
                                <asp:TreeNode Text="Equity Reports" Value="EquityReports"></asp:TreeNode>
                            </asp:TreeNode>
<%--                            <asp:TreeNode Text="Loan" Value="Loan">
                                <asp:TreeNode Text="Schemes" Value="Schemes">                                    
                                </asp:TreeNode>
                                <asp:TreeNode Text="Loan Partner Commission" Value="AdviserLoanCommsnStrucWithLoanPartner">
                                </asp:TreeNode>
                                <asp:TreeNode Text="Loan MIS" Value="LoanMis"></asp:TreeNode>
                            </asp:TreeNode>--%>
                        </Nodes>
                        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <div style="display: inline">
                        <asp:TextBox runat="server" ID="txtFindRM" Style="width: 110px" onkeypress="return JSdoPostback(event,'ctrl_AdvisorLeftPane_btnSearchRM');" />
                        <cc1:TextBoxWatermarkExtender ID="txtFindRM_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtFindRM" WatermarkText="Find RM">
                        </cc1:TextBoxWatermarkExtender>
                        <asp:Button ID="btnSearchRM" runat="server" CssClass="SearchButton" OnClientClick="javascript:callSearchControl('RM');return false;" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="display: inline">
                        <asp:TextBox runat="server" ID="txtFindBranch" Style="width: 110px" onkeypress="return JSdoPostback(event,'ctrl_AdvisorLeftPane_btnSearchBranch');" />
                        <cc1:TextBoxWatermarkExtender ID="txtFindBranch_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtFindBranch" WatermarkText="Find Branch">
                        </cc1:TextBoxWatermarkExtender>
                        <asp:Button ID="btnSearchBranch" runat="server" CssClass="SearchButton" OnClientClick="javascript:callSearchControl('Branch');return false;" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="display: inline">
                        <asp:TextBox runat="server" ID="txtFindAdviserCustomer" Style="width: 110px" onkeypress="return JSdoPostback(event,'ctrl_AdvisorLeftPane_btnSearchCustomer');" />
                        <cc1:TextBoxWatermarkExtender ID="txtFindAdviserCustomer_TextBoxWatermarkExtender"
                            runat="server" TargetControlID="txtFindAdviserCustomer" WatermarkText="Find Customer">
                        </cc1:TextBoxWatermarkExtender>
                        <asp:Button ID="btnSearchCustomer" runat="server" CssClass="SearchButton" OnClientClick="javascript:callSearchControl('AdviserCustomer');return false;" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
