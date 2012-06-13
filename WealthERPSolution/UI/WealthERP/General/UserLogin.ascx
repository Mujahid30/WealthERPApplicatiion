<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.ascx.cs" Inherits=" WealthERP.General.UserLogin" %>
<%--<link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />--%>
<meta http-equiv="cache-control" content="no-cache" />
<meta http-equiv="expires" content="0" />
<meta http-equiv="pragma" content="no-cache" />
<asp:ScriptManager runat="server">
</asp:ScriptManager>
<table width="100%" style="height: 347px" class="TableBackground">
    <tr id="trWealthERP" runat="server">
        <td align="center" style="font-weight: bold; text-decoration: underline">
            <asp:Label ID="lblCompanyName" runat="server" ForeColor="#5D7B9D"></asp:Label>
        </td>
    </tr>
    <tr id="trAdvisorLogo" runat="server">
        <td align="center">
            <asp:Image ID="imgAdvisorLogo" runat="server" Height="70" alt="Advisor Logo" ImageUrl="../Images/Standard%20Chartered%20Bank_800px-Standard_Chartered_Bank_logo.jpg" />
        </td>
    </tr>
    <tr>
        <td valign="top">
            <table width="100%">
                <tr>
                    <td width="60%" align="justify" runat="server" id="dynamicLoginContent">
                        <ul class="Field">
                            <asp:Label ID="lblUserLoginContent" runat="server"></asp:Label>
                        </ul>
                    </td>
                    <td width="60%" class="Field" runat="server" id="MT_LoginContent">
                        <ul class="Field">
                            <p>
                                Welcome to the world of Moneytouch 360 &deg, your on-demand, multi-asset, multi-partner
                                integrated technology platform. Alongwith our <a href="http://www.moneytouch.in/"
                                    target="_blank">Moneytouch.in</a> website you are assured of tremendous benefits
                                like:
                            </p>
                            <ul class="Field" style="list-style-image: url(../Images/securedownload.jpg);">
                                <li>Access to Financial planning tools, calculators </li>
                                <li>Consolidated statements for your clients </li>
                                <li>Research reports across asset classes </li>
                                <li>Multiple portfolios can be tracked in one place </li>
                                <li>Alerts can be personalised to each customer </li>
                                <li>Enable decision making with comparative tools </li>
                                <li>Generating additional revenues by increasing product penetration </li>
                            </ul>
                            <p>
                                So, login and use the platform to the maximum benefit for yourself, your clients
                                and give a tough fight to your competitors
                            </p>
                        </ul>
                    </td>
                    <td valign="top">
                        <table align="center" border="0">
                            <tr>
                                <td align="right" width="50%">
                                    <span class="FieldName">UserName:</span>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLoginId" CssClass="Field" Width="160" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="50%">
                                    <span class="FieldName">Password:</span>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPassword" TextMode="password" CssClass="Field" Width="160" MaxLength="20"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="btnSignIn" runat="server" Text="Login" OnClick="btnSignIn_Click"
                                        CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_Userlogin_btnSignIn', 'S');"
                                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_Userlogin_btnSignIn', 'S');" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Label ID="lblIllegal" Text="" runat="server" CssClass="Error" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <span class="FieldName">
                                        <asp:HyperLink ID="hlRegistration" runat="server" NavigateUrl="~/Register.aspx"></asp:HyperLink>
                                        <%--    <asp:LinkButton ID="NewUser" runat="server" OnClick="NewUser_Click">Register Here</asp:LinkButton>--%>
                                    </span>
                                </td>
                            </tr>
                            <tr runat="server" visible="false">
                                <td colspan="2" align="right">
                                    <span class="FieldName">Forgot Password?
                                        <asp:LinkButton ID="lnkForgotPassword" runat="server" OnClick="ForgotPassword_Click">Click Here</asp:LinkButton>
                                    </span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
