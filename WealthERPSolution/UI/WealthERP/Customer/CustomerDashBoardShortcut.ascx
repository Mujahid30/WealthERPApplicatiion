<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerDashBoardShortcut.ascx.cs" Inherits="WealthERP.Customer.CustomerDashBoardShortcut" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<table width="100%">
<tr>
     <td colspan="3">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                <tr>
                    <td align="left">Customer Quick Links</td>
                    
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
        <td style="width: 15%;">
        </td>
        <td style="width: 70%;">
            <table width="100%">
                <tr>
                    <td style="width: 30%;" align="center">
                        <div class="divDashBoardMouseInLinks" onmouseover="this.className='divDashBoardMouseOutLinks'" onmouseout="this.className='divDashBoardMouseInLinks'">
                            <%--<asp:ImageButton ID="imgClientsClick" ImageUrl="~/Images/Dashboard-Clients.png" runat="server"
                                ToolTip="Navigate to Customer Grid" OnClick="imgClientsClick_OnClick" Width="70px" />--%>
                            <br />
                            <asp:LinkButton ID="lnkbtnCustomerDashBoard" runat="server" 
                                Font-Underline="false"  CssClass="FieldName" 
                                ToolTip="Navigate to Customer DashBoard" Text="DashBoard" 
                                onclick="lnkbtnCustomerDashBoard_Click"></asp:LinkButton>
                        </div>
                    </td>
                    <td style="width: 30%;" align="center">
                        <div class="divDashBoardMouseInLinks" onmouseover="this.className='divDashBoardMouseOutLinks'" onmouseout="this.className='divDashBoardMouseInLinks'">
                            <%--<asp:ImageButton ID="imgUploads" ImageUrl="~/Images/Upload-icon.png" runat="server"
                                ToolTip="Navigate to Uploads" OnClick="imgUploads_OnClick" Width="70px" />--%>
                            <br />
                            <asp:LinkButton ID="lnkbtnFPDashBoard" runat="server"  Font-Underline="false"  
                                CssClass="FieldName" ToolTip="Navigate to FP Dashboard"
                                 Text="FP DashBoard" onclick="lnkbtnFPDashBoard_Click" ></asp:LinkButton>
                        </div>
                    </td>
                    <td style="width: 30%;" align="center">
                         <div class="divDashBoardMouseInLinks" onmouseover="this.className='divDashBoardMouseOutLinks'" onmouseout="this.className='divDashBoardMouseInLinks'">
                            <%--<asp:ImageButton ID="imgOrderentry" ImageUrl="~/Images/Dashboard-MF-Order.png" runat="server"
                                ToolTip="Navigate to Order Entry" OnClick="imgOrderentry_OnClick" Width="70px" />--%>
                            <br />
                            <asp:LinkButton ID="lnkbtnProductOrder" runat="server"  Font-Underline="false"  CssClass="FieldName" 
                                ToolTip="Navigate to Product Order" Text="Product Order" 
                                 onclick="lnkbtnProductOrder_Click" ></asp:LinkButton>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 15%;">
        </td>
    </tr>
    
    <tr>
    <td colspan="3">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;
    <br />
    </td>
    </tr>
    
    <tr>
        <td style="width: 15%;">
        </td>
        <td style="width: 70%;">
            <table width="100%">
                <tr>
                    <td style="width: 30%;" align="center">
                            <div class="divDashBoardMouseInLinks" onmouseover="this.className='divDashBoardMouseOutLinks'" onmouseout="this.className='divDashBoardMouseInLinks'">
                            <%--<asp:ImageButton ID="imgBusinessMIS" ImageUrl="~/Images/Dashboard-BusinessMIS.png"
                                runat="server" ToolTip="Navigate to Business MIS" OnClick="imgBusinessMIS_OnClick"
                                Width="70px" />--%>
                            <br />
                            <asp:LinkButton ID="lnkbtnMF" runat="server"  Font-Underline="false"  CssClass="FieldName" 
                                ToolTip="Navigate to MF" Text="MF" onclick="lnkbtnMF_Click" ></asp:LinkButton>
                        </div>
                    </td>
                    <td style="width: 30%;" align="center">
                           <div class="divDashBoardMouseInLinks" onmouseover="this.className='divDashBoardMouseOutLinks'" onmouseout="this.className='divDashBoardMouseInLinks'">
                            <%--<asp:ImageButton ID="imgInbox" runat="server" ToolTip="Navigate to Inbox" OnClick="imgInbox_OnClick"
                                Width="70px" />--%>
                            <br />
                            <asp:LinkButton ID="lnkbtnEquity" runat="server"  Font-Underline="false"  CssClass="FieldName" 
                                ToolTip="Navigate to Equity" Text="Equity" onclick="lnkbtnEquity_Click"></asp:LinkButton>
                        </div>
                    </td>
                    <td style="width: 30%;" align="center">
                            <div class="divDashBoardMouseInLinks" onmouseover="this.className='divDashBoardMouseOutLinks'" onmouseout="this.className='divDashBoardMouseInLinks'">
                            <%--<asp:ImageButton ID="imgbtnFPClients" ImageUrl="~/Images/DashBoard-ProspectUser.png"
                                runat="server" ToolTip="Navigate to Add FP Customers" OnClick="imgbtnFPClients_OnClick"
                                Width="70px" />--%>
                            <br />
                            <asp:LinkButton ID="lnkbtnAlerts" runat="server"  Font-Underline="false"  CssClass="FieldName" 
                                ToolTip="Navigate to Add Alerts" Text="Notifications" 
                                    onclick="lnkbtnAlerts_Click" ></asp:LinkButton>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 15%;">
        </td>
    </tr>
</table>