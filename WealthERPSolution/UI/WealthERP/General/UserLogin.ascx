<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.ascx.cs" Inherits=" WealthERP.General.UserLogin" %>
<%--<link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />--%>
<meta http-equiv="cache-control" content="no-cache"/>
<meta http-equiv="expires" content="0"/>
<meta http-equiv="pragma" content="no-cache"/>
<table width="100%" style="height: 347px" class="TableBackground">
    <tr id="trWealthERP" runat="server">
        <td align="center">
            <asp:Label ID="lblCompanyName" runat="server" Text="WealthERP Login" ForeColor="#5D7B9D"></asp:Label>
        </td>
    </tr>
    <tr id="trAdvisorLogo" runat="server">
        <td align="center">
            <asp:Image ID="imgAdvisorLogo" runat="server"  height="70" alt="Advisor Logo" ImageUrl="../Images/Standard%20Chartered%20Bank_800px-Standard_Chartered_Bank_logo.jpg" />            
        </td>
    </tr>
    <tr>
        <td valign="top">
            <table width="100%">
                <tr>
                    <td width="60%">
                        <ul class="Field">
                            <p>
                                WERP is an on-demand wealth management cum financial services platform for wealth
                                advisors and financial planners and distributors. The platform covers multiple asset
                                classes and allows advisors to plan, manage and analyze investments across assets
                                and have a 360 Degree view of their client’s investment portfolio.
                            </p>
                            <p>
                                WERP’s key differentiators are integrated investment management with a process driven
                                ERP approach which comes loaded with knowledge based investment tools and financial
                                planning models.
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
                            <tr>
                            <td colspan="2">
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
