<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SessionExpired.ascx.cs"
    Inherits="WealthERP.General.SessionExpired" %>
<div>
    <asp:ScriptManager ID="scpt" runat="server"></asp:ScriptManager>
    Your Session has expired. Please <a href="javascript:loadcontrol('Userlogin', 'none');"
        id="aRelogin">click here</a> to re-login.
</div>
