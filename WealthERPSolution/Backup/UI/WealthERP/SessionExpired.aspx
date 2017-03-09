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
        <%-- <asp:Label ID="lblSessionExpired" runat="server" CssClass="FieldName">
        Your Session has expired. Please <a href="Default.aspx"
            id="aRelogin">click here</a> to re-login.
        </asp:Label>--%>
       <%-- Your Session has expired. Please--%>
       
    </div>
    <table width="905" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td id="header">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td id="bgnd_sides">
                <table width="875" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="1">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td height="5">
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table width="95%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="center">
                                </tr>
                                <tr>
                                    <td align="center">
                                        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" style="font-family: arial;">
                                            <tr>
                                                <td colspan="1" align="left" class="boldEleven" style="padding-right: 5px; padding-left: 5px;
                                                    padding-bottom: 5px; padding-top: 5px;">
                                                   
                                                     <div class="failure-msg" id="divSuccessMsg" runat="server" align="center">
                                                     <font color="red" size="4" style="font-family: arial;font-weight:bold"> Your Session has Expired</font>
                                                     </div>
                                                    <br>
                                                    <br>
                                                    <a class="HeaderTextBig">This error has occured for
                                                        one of the following reasons :</a><br><br>
                                                     <a class="FieldName">1) You have used Back/Forward/Refresh
                                                        button of your Browser.<br>
                                                        
                                                        2) You have kept the browser window idle for a long time.<br>
                                                        3) If the problem persists, please try again after clearing the Temporary Files from your web browser.<br>
                                                      </a>
                                                        <br>
                                                        <asp:LinkButton ID="lnkUserSessionExpried" runat="server" Text="click here to re-login." 
                                                        CssClass="LinkButtons" OnClick="lblSignOut_Click"></asp:LinkButton>
        
                                                        </font>
                                                    <br>
                                                    <br>
                                                    <br>
                                                    <br>
                                                    <br>
                                                    <br>
                                                    <br>
                                                    <br>
                                                    <br>
                                                    <br>
                                                    <br>
                                                    <br>
                                                    <br>
                                                    <br>
                                                </td>
                                            </tr>
                                        </table>
                                </tr>
                                <tr>
                                    <td align="center">
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="botorgngbar">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td height="5">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
      
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
