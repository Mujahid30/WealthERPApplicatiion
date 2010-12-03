<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisorLeftPane.ascx.cs"
    Inherits="WealthERP.Advisor.AdvisorLeftPane" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

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
       
                    <telerik:RadPanelBar ID="RadPanelBar1" Runat="server" ExpandAnimation-Type="InCubic" ExpandMode="SingleExpandedItem" Width="210px" 
    EnableEmbeddedSkins="false" Skin="Touchbase"
                        onitemclick="RadPanelBar1_ItemClick">
                        <Items>
                            <telerik:RadPanelItem runat="server" Text="Switch Roles" Value="Switch Roles">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Admin Home" Value="Advisor Home">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Profile" Value="Profile">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Edit Profile" Value="Edit Profile">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Preferences" Value="Preferences">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Setup Associate Category" 
                                        Value="Setup Associate Category">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Setup Advisor Staff SMTP" 
                                        Value="Setup Advisor Staff SMTP">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Set Theme" Value="Set Theme">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="LOB" Value="LOB">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Add LOB" Value="Add LOB">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Staff" Value="Staff">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Add Staff" Value="Add Staff">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Branch/Assocation" Value="Branch">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Add Branch" Value="Add Branch">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="View Branch Assocation" 
                                        Value="View Branch Assocation">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Uploads" Value="Uploads">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Upload History" 
                                        Value="Upload History">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="View Trans Rejects" 
                                        Value="View Trans Rejects">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="View Rejected Folio" 
                                        Value="View Rejected Folio">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Customer" Value="Customer">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Customer Accounts" 
                                        Value="Customer Accounts">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Association" Value="Association">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="User Management" 
                                Value="User Management">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="RM Details" Value="RM Details">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Customer Details" 
                                        Value="Customer Details">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Daily Process" Value="Daily Process">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Valuation" Value="Valuation">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="SMS" Value="SMS">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Alerts SMS" Value="Alerts SMS">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Customized SMS" 
                                        Value="Customized SMS">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="MF" Value="MF">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="MIS" Value="MMIS">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Commission MIS" 
                                        Value="Commission MIS">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="View Transactions" 
                                        Value="MView Transactions">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Add Transactions" 
                                        Value="MAdd Transactions">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="MF Reports" Value="MF Reports">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Systematic Recon" 
                                        Value="Systematic Recon">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Reversal Trxn Handling" 
                                        Value="Reversal Trxn Handling">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Equity" Value="Equity">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="MIS" Value="EMIS">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="View Transactions" 
                                        Value="EView Transactions">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Add Transactions" 
                                        Value="EAdd Transactions">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Equity Reports" 
                                        Value="Equity Reports">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Loan" Value="Loan">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Text="Schemes" Value="Schemes">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Add Schemes" Value="Add Schemes">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Loan Partner Commission" 
                                        Value="Loan Partner Commission">
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem runat="server" Text="Loan MIS" Value="Loan MIS">
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelBar>
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
                    <div style="vertical-align:middle;">
                        <asp:TextBox runat="server" ID="txtFindRM" Style="width: 110px;" onkeypress="return JSdoPostback(event,'ctrl_AdvisorLeftPane_btnSearchRM');" />
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
<p>
    &nbsp;</p>

