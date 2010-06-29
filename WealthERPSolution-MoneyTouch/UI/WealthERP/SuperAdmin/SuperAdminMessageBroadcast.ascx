<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SuperAdminMessageBroadcast.ascx.cs"
    Inherits="WealthERP.SuperAdmin.SuperAdminMessageBroadcast" %>

<script type="text/javascript" src="../Scripts/jquery.js"></script>
<script type="text/javascript" src="../Scripts/jquery.js"></script>


<table width="100%">
    <tr>
        <td align="center">
            <div class="success-msg" id="SuccessMessage" runat="server" visible="false" align="center">
                Message sent to all Advisors Successfully...
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Broadcast Message"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:TextBox ID="MessageBox" runat="server" Width="600px" Height="100px" TextMode="MultiLine"
                MaxLength="255"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
<div align="center">
    <asp:Button ID="SendMessage" runat="server" Text="Send Info" CssClass="PCGButton"
        OnClick="SendMessage_Click" />
</div>
<br />
<table width="100%">
    <tr>
        <td align="center">
            <div class="warning-msg" id="MessageBroadcast" runat="server" visible="false" align="center">
                <br />
                <asp:Label ID="lblSuperAdmnMessage" runat="server"></asp:Label>
            </div>
        </td>
    </tr>
</table>



