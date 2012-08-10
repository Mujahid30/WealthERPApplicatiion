<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IFADashBoard.ascx.cs" Inherits="WealthERP.SuperAdmin.IFADashBoard" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>



<table width="100%">
<tr>
     <td colspan="3">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                <tr>
                    <td align="left"> Quick Links</td>
                    
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
                            <asp:ImageButton ID="imgIFAAddClick" ImageUrl="~/Images/business_user_add.png" runat="server"
                                ToolTip="Navigate to IFA Add" OnClick="imgIFAAddClick_OnClick" Width="70px" />
                            <br />
                            
                            <asp:LinkButton ID="lnkbtnIFAAddLink" runat="server" Font-Underline="false"  CssClass="FieldName" OnClick="lnkbtnIFAAddLink_OnClick"
                                ToolTip="Navigate to IFA Grid" Text="IFA Add"></asp:LinkButton>
                        </div>
                    </td>
                    <td style="width: 30%;" align="center">
                        <div class="divDashBoardMouseInLinks" onmouseover="this.className='divDashBoardMouseOutLinks'" onmouseout="this.className='divDashBoardMouseInLinks'">
                            <asp:ImageButton ID="imgIFAList" ImageUrl="~/Images/Dashboard-Clients.png" runat="server"
                                ToolTip="Navigate to IFA List" OnClick="imgIFAList_OnClick" Width="70px" />
                            <br />
                            <asp:LinkButton ID="lnkbtnIFAList" runat="server"  Font-Underline="false"  CssClass="FieldName" ToolTip="Navigate to IFA List"
                                OnClick="lnkbtnIFAList_OnClick" Text="IFA List"></asp:LinkButton>
                        </div>
                    </td>
                    <td style="width: 30%;" align="center">
                         <div class="divDashBoardMouseInLinks" onmouseover="this.className='divDashBoardMouseOutLinks'" onmouseout="this.className='divDashBoardMouseInLinks'">
                            <asp:ImageButton ID="imgUserManagement" ImageUrl="~/Images/project-management-icon.png" runat="server"
                                ToolTip="Navigate to User Management" OnClick="imgUserManagement_OnClick" Width="70px" />
                            <br />
                            <asp:LinkButton ID="lnkbtnUserManagement" runat="server"  Font-Underline="false"  CssClass="FieldName" OnClick="lnkbtnUserManagement_OnClick"
                                ToolTip="Navigate to User Management" Text="User Management"></asp:LinkButton>
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
                            <asp:ImageButton ID="imgManualValuation" ImageUrl="~/Images/valuation.jpg"
                                runat="server" ToolTip="Navigate to Manual Valuation" OnClick="imgManualValuation_OnClick"
                                Width="70px" />
                            <br />
                            <asp:LinkButton ID="lnkbtnManualValuation" runat="server"  Font-Underline="false"  CssClass="FieldName" OnClick="lnkbtnManualValuation_OnClick"
                                ToolTip="Navigate to Manual Valuation" Text="Manual Valuation"></asp:LinkButton>
                        </div>
                    </td>
                    <td style="width: 30%;" align="center">
                           <div class="divDashBoardMouseInLinks" onmouseover="this.className='divDashBoardMouseOutLinks'" onmouseout="this.className='divDashBoardMouseInLinks'">
                            <asp:ImageButton ID="imgValuationMonitor" runat="server" ImageUrl="~/Images/monitoring_camera.jpg" ToolTip="Navigate to Valuation Monitor" OnClick="imgValuationMonitor_OnClick"
                                Width="70px" />
                            <br />
                            <asp:LinkButton ID="lnkbtnValuationMonitor" runat="server"  Font-Underline="false"  CssClass="FieldName" OnClick="lnkbtnValuationMonitor_OnClick"
                                ToolTip="Navigate to Valuation Monitor" Text="Valuation Monitor"></asp:LinkButton>
                        </div>
                    </td>
                    <td style="width: 30%;" align="center">
                            <div class="divDashBoardMouseInLinks" onmouseover="this.className='divDashBoardMouseOutLinks'" onmouseout="this.className='divDashBoardMouseInLinks'">
                            <asp:ImageButton ID="imgbtnMsgBroadcast" ImageUrl="~/Images/vpicebroadcasting.jpg"
                                runat="server" ToolTip="Navigate to Messages Broadcast" OnClick="imgbtnMsgBroadcast_OnClick"
                                Width="70px" />
                            <br />
                            <asp:LinkButton ID="lnkbtnMsgBroadcast" runat="server"  Font-Underline="false"  CssClass="FieldName" OnClick="lnkbtnMsgBroadcast_OnClick"
                                ToolTip="Navigate to Messages Broadcast" Text="Messages Broadcast"></asp:LinkButton>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 15%;">
        </td>
    </tr>
</table>