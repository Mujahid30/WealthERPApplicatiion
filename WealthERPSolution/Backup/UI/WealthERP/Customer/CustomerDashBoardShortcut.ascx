<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerDashBoardShortcut.ascx.cs" Inherits="WealthERP.Customer.CustomerDashBoardShortcut" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<table width="100%">
<tr>
     <td colspan="3">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                <tr>
                    <td align="left">Shortcuts</td>
                    
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
                            <asp:ImageButton ID="imgCusDashBoard" ImageUrl="~/Images/Dashboard-DAshboard.png" runat="server"
                                ToolTip="Navigate to Customer DashBoard"  Width="70px" 
                                onclick="imgCusDashBoard_Click" />
                            <br />
                            <asp:LinkButton ID="lnkbtnCustomerDashBoard" runat="server" 
                                Font-Underline="false"  CssClass="FieldName" 
                                ToolTip="Navigate to Customer DashBoard" Text="DashBoard" 
                                onclick="lnkbtnCustomerDashBoard_Click"></asp:LinkButton>
                        </div>
                    </td>
                    <td style="width: 30%;" align="center">
                        <div class="divDashBoardMouseInLinks" onmouseover="this.className='divDashBoardMouseOutLinks'" onmouseout="this.className='divDashBoardMouseInLinks'">
                            <asp:ImageButton ID="imgFP" ImageUrl="~/Images/Dashboard-FP.png" runat="server"
                                ToolTip="Navigate to Financial Planning"  Width="70px" 
                                onclick="imgFP_Click" />
                            <br />
                            <asp:LinkButton ID="lnkbtnFPDashBoard" runat="server"  Font-Underline="false"  
                                CssClass="FieldName" ToolTip="Navigate to Financial Planning"
                                 Text="FP" onclick="lnkbtnFPDashBoard_Click" ></asp:LinkButton>
                        </div>
                    </td>
                   <%-- <td style="width: 30%;" align="center" visible="false">
                         <div visible="false" class="divDashBoardMouseInLinks" onmouseover="this.className='divDashBoardMouseOutLinks'" onmouseout="this.className='divDashBoardMouseInLinks'">
                            <asp:ImageButton ID="imgOrderentry" ImageUrl="~/Images/Dashboard-MF-Order.png" runat="server"
                                ToolTip="Navigate to Order Management"  Width="70px" 
                                 onclick="imgOrderentry_Click" />
                            <br />
                            <asp:LinkButton ID="lnkbtnProductOrder" runat="server"  Font-Underline="false"  CssClass="FieldName" 
                                ToolTip="Navigate to Order Management" Text="Product Order" 
                                 onclick="lnkbtnProductOrder_Click" ></asp:LinkButton>
                        </div>
                    </td>--%>
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
                            <asp:ImageButton ID="imgbtnMF" ImageUrl="~/Images/Dashboard-MutualFund.png" runat="server" ToolTip="Navigate to MF" 
                                Width="70px" onclick="imgbtnMF_Click" />
                            <br />
                            <asp:LinkButton ID="lnkbtnMF" runat="server"  Font-Underline="false"  CssClass="FieldName" 
                                ToolTip="Navigate to MF" Text="MF" onclick="lnkbtnMF_Click" ></asp:LinkButton>
                        </div>
                    </td>
                    <td style="width: 30%;" align="center">
                           <div class="divDashBoardMouseInLinks" onmouseover="this.className='divDashBoardMouseOutLinks'" onmouseout="this.className='divDashBoardMouseInLinks'">
                            <asp:ImageButton ID="imgbtnEquity" runat="server" 
                                   ImageUrl="~/Images/Dashboard-Equity.png" ToolTip="Navigate to Equity" 
                                Width="70px" onclick="imgbtnEquity_Click" />
                            <br />
                            <asp:LinkButton ID="lnkbtnEquity" runat="server"  Font-Underline="false"  CssClass="FieldName" 
                                ToolTip="Navigate to Equity" Text="Equity" onclick="lnkbtnEquity_Click"></asp:LinkButton>
                        </div>
                    </td>
                    <%--<td style="width: 30%;" align="center" visible="false">
                            <div visible="false" class="divDashBoardMouseInLinks" onmouseover="this.className='divDashBoardMouseOutLinks'" onmouseout="this.className='divDashBoardMouseInLinks'">
                            <asp:ImageButton ID="imgbtnNotification" ImageUrl="~/Images/DashBoard-Notification.png"
                                runat="server" ToolTip="Navigate to Notification" 
                                Width="70px" onclick="imgbtnNotification_Click" />
                            <br />
                            <asp:LinkButton ID="lnkbtnAlerts" runat="server"  Font-Underline="false"  CssClass="FieldName" 
                                ToolTip="Navigate to Add Alerts" Text="Notifications" 
                                    onclick="lnkbtnAlerts_Click" ></asp:LinkButton>
                        </div>
                    </td>--%>
                </tr>
            </table>
        </td>
        <td style="width: 15%;">
        </td>
    </tr>
</table>