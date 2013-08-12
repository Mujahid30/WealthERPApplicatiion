<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesDashBoard.ascx.cs" Inherits="WealthERP.Advisor.SalesDashBoard" %>
<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function() {
        $(".panel").show();

        $(".flip").click(function() { $(".panel").slideToggle(); });
    });
</script>


<%--<script type="text/javascript">
//    $(function() {
//    $(".divDashBoardQuickLinks").mouseover(function() {
//            $(this).animate({ "borderLeftWidth": "5px",
//                "borderRightWidth": "5px",
//                "borderTopWidth": "5px",
//                "borderBottomWidth": "5px",

//                "marginLeft": "-5px",
//                "marginTop": "-5px",
//                "marginRight": "-5px",
//                "marginBottom": "-5px"
//            }, 1000);
//        }).mouseout(function() {
//            $(this).animate({ "borderLeftWidth": "1px",
//                "borderRightWidth": "1px",
//                "borderTopWidth": "1px",
//                "borderBottomWidth": "1px",

//                "marginLeft": "1px",
//                "marginTop": "1px",
//                "marginRight": "1px",
//                "marginBottom": "1px"
//            }, 1000);
//        });
//    });

  
    $('#divDashBoardQuickLinks').click(function() {
        $('#imgBaby').animate({
                opacity: 0.25,
                left: '+=50',
                height: 'toggle'
            }, 5000, function() {
                // Animation complete.
            });
        });
    

  


//    $(function() {
//    jQuery("#confirm_selection, #abc").mouseenter(function() {
//            jQuery(this).css("outlineStyle", "solid").animate({
//                'outlineWidth': '5px'
//            }, 500);
//        }).mouseout(function() {
//            jQuery(this).animate({
//                'outlineWidth': '0px'
//            }, 500).css("outlineStyle", "solid");
//        });
//    });

</script>

--%>
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
                            <asp:ImageButton ID="imgClientsClick" ImageUrl="~/Images/Dashboard-Clients.png" runat="server"
                                ToolTip="Navigate to Customer Grid" OnClick="imgClientsClick_OnClick" Width="70px" />
                            <br />
                            <asp:LinkButton ID="lnkbtnClientLink" runat="server" Font-Underline="false"  CssClass="FieldName" OnClick="lnkbtnClientLink_OnClick"
                                ToolTip="Navigate to Customer Grid" Text="Clients"></asp:LinkButton>
                        </div>
                    </td>
                    <td style="width: 30%;" align="center">
                         <div class="divDashBoardMouseInLinks" onmouseover="this.className='divDashBoardMouseOutLinks'" onmouseout="this.className='divDashBoardMouseInLinks'">
                            <asp:ImageButton ID="imgOrderentry" ImageUrl="~/Images/Dashboard-MF-Order.png" runat="server"
                                ToolTip="Navigate to Order Entry" OnClick="imgOrderentry_OnClick" Width="70px" />
                            <br />
                            <asp:LinkButton ID="lnkbtnOrderEntry" runat="server"  Font-Underline="false"  CssClass="FieldName" OnClick="lnkbtnOrderEntry_OnClick"
                                ToolTip="Navigate to Order Entry" Text="Order Entry"></asp:LinkButton>
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
    
</table>
<br />

<asp:HiddenField ID="hdfFlavourId" runat="server" />