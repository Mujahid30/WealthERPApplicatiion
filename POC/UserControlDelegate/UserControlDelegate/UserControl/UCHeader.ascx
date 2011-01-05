<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCHeader.ascx.cs" Inherits="UserControlDelegate.UserControl.UCHeader" %>
<style type="text/css">
    .style1
    {
        font-family: Tahoma;
        font-weight: bold;
        text-align: center;
        font-size: xx-large;
    }
    .style2
    {
        width: 589px;
    }
</style>
<p class="style1" align="center">
    <table style="width:100%;">
        <tr>
            <td>
                &nbsp;</td>
            <td class="style2" 
                style="text-align: center; font-weight: 700; font-size: xx-large">
                Demo Site Header</td>
            <td valign =top; align=center >
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" 
                    Font-Bold="True" Font-Names="Georgia" Font-Size="X-Small">Login</asp:LinkButton>
            </td>
        </tr>
    </table>
</p>
