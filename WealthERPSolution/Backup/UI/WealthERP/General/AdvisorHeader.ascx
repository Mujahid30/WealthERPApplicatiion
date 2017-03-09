<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisorHeader.ascx.cs" Inherits="WealthERP.General.AdvisorHeader" %>


<table width="100%">
    <tr>
    <td colspan="3" valign="top">
    <table width="100%" style="height: 30px">
    <tr style="height:auto">
    <td class="style1" >
    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/logo.jpg" />
    &nbsp;</td>
        
    <td class="style2">
        
    </td>
    <td valign="bottom">
        <asp:LinkButton ID="LinkButtonSignIn" runat="server" 
            OnClientClick="loadcontrol('Userlogin');" 
            onclick="LinkButtonSignIn_Click" >Sign Out</asp:LinkButton>
&nbsp;
        <asp:LinkButton ID="LinkButtonContactUs" runat="server">Contact Us</asp:LinkButton>
&nbsp;
        <asp:LinkButton ID="LinkButtonHelp" runat="server">Help</asp:LinkButton>
    </td>

    </tr>
    </table>
    </td>
    </tr>
    <tr>
    <td colspan="3" bgcolor=Gainsboro height="5px">
    
    </td>
    </tr>
    <tr style="height:20px; border:border 2 #0000FF">
    <td colspan="3">
        <table width="100%">
            <tr>
                <td style="width:17%">
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        </td>
    </tr>
    </table>