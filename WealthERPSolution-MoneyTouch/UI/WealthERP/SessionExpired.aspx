<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionExpired.aspx.cs"
    Inherits="WealthERP.SessionExpired" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body class="TableBackground">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblSessionExpired" runat="server" CssClass="FieldName">
        Your Session has expired. Please <a href="Default.aspx"
            id="aRelogin">click here</a> to re-login.
        </asp:Label>
    </div>
    </form>
</body>
</html>
