<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SessionExpired.ascx.cs"
    Inherits="WealthERP.General.SessionExpired" %>
<div>
    <asp:ScriptManager ID="scpt" runat="server"></asp:ScriptManager>
    Your Session has expired. Please
     <asp:LinkButton ID="lnkUserSessionExpried" runat="server" Text="click here " Style="text-decoration: none"                                                    
                                                    CssClass="LinkButtons" onclick="lblSignOut_Click"></asp:LinkButton>
                                                    to re-login.
    <%-- <a href="javascript:loadcontrol('Userlogin', 'none');"
        id="aRelogin">click here</a> to re-login.--%>
</div>
