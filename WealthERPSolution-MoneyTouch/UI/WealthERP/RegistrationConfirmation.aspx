<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationConfirmation.aspx.cs"
    Inherits="WealthERP.RegistrationConfirmation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="Scripts/JScript.js" type="text/javascript"></script>
    <style>
        body
        {
            background-color: #EBEFF9;
        }
        .centerAlign
        {
            border: 1px solid #CCCCCC;
            height: 20em; 
            left: 50%;
            margin-left: -20em;
            margin-top: -10em;
            position: absolute;
            background-color: white;
            width: 700px;
            text-align: center;
            top: 50%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="centerAlign">
        <table style="width: 100%;" cellpadding="10">
            <tr>
                <td colspan="4">
                    <%--<img src="Images/werplogo.jpg">--%>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblMsg" runat="server" CssClass="FieldName" Text="Thanks for Registering."></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblRegistrationEmail" runat="server" Text="A confirmation mail has been sent to your specified mail id."
                        CssClass="FieldName"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="Label2" runat="server" Text=" Someone from our team will connect with you in the next 24 hours on the same."
                        CssClass="FieldName"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
           <%-- <tr>
                <td colspan="4">
                    <h3>
                        <asp:LinkButton ID="lnkUserLogin" runat="server" CssClass="LinkButtons" OnClick="lnkUserLogin_Click">Login</asp:LinkButton>
                    </h3>
                </td>
            </tr>--%>
        </table>
    </div>
    </form>
</body>
</html>
