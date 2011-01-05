<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SMS_POC._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="vertical-align:top">
       Phone Number <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
        <br />
        Message
        <asp:TextBox ID="txtMessage" runat="server" Height="59px" TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:Button ID="btnSend" runat="server" Text="Send" onclick="Button1_Click" />
        <br />
        <asp:Label ID="lblMessage" Text="" Visible="false" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
