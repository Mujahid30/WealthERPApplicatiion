<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.ascx.cs" Inherits=" WealthERP.General.UserLogin" %>
<%--<link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />--%>
<meta http-equiv="cache-control" content="no-cache" />
<meta http-equiv="expires" content="0" />
<meta http-equiv="pragma" content="no-cache" />
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:ScriptManager runat="server">
</asp:ScriptManager>
<style type="text/css">
    .button
    {
        width: 100px;
        background: #3399cc;
        display: block;
        margin: 0 auto;
        margin-top: 1%;
        padding: 10px;
        text-align: center;
        text-decoration: none;
        color: #fff;
        cursor: pointer;
        transition: background .3s;
        -webkit-transition: background .3s;
    }
    .button:hover
    {
        background: #2288bb;
    }
    #login
    {
        width: 400px;
        margin: 0 auto;
        margin-top: 8px;
        margin-bottom: 2%;
        transition: opacity 1s;
        -webkit-transition: opacity 1s;
    }
    #login h1
    {
        background: #3399cc;
        padding: 20px 0;
        font-size: 140%;
        font-weight: 300;
        text-align: center;
        color: #fff;
    }
    .textbox
    {
        -webkit-transition: all 0.30s ease-in-out;
        -moz-transition: all 0.30s ease-in-out;
        -ms-transition: all 0.30s ease-in-out;
        -o-transition: all 0.30s ease-in-out;
        outline: none;
        padding: 3px 0px 3px 3px;
        margin: 5px 1px 3px 0px;
        border: 1px solid #DDDDDD;
    }
    .textbox:focus
    {
        box-shadow: 0 0 5px rgba(81, 203, 238, 1);
        padding: 3px 0px 3px 3px;
        margin: 5px 1px 3px 0px;
        border: 1px solid rgba(81, 203, 238, 1);
    }
   
  
    .LABEL
    {
        color: #3399cc;
        margin-bottom: 1px;
        font-family: Times New Roman;
    }
    .table
    {
        width: 35%;
        height: 45%;
        float: inherit;
        border-color: black;
        border-width: thin;
        border-style: solid;
        -webkit-transition: all 0.30s ease-in-out;
        -moz-transition: all 0.30s ease-in-out;
        -ms-transition: all 0.30s ease-in-out;
        -o-transition: all 0.30s ease-in-out;
    }
</style>
<table width="100%" style="height: 347px" class="TableBackground">
    <tr id="trWealthERP" runat="server">
        <td align="center" style="font-weight: bold; text-decoration: underline">
        </td>
    </tr>
    <tr id="trAdvisorLogo" runat="server">
        <td align="center">
            <asp:Image ID="imgAdvisorLogo" runat="server" Height="70" alt="Advisor Logo" ImageUrl="../Images/Standard%20Chartered%20Bank_800px-Standard_Chartered_Bank_logo.jpg" />
        </td>
    </tr>
    <tr>
        <td valign="top">
            <table width="100%" style="padding-top: 50px;">
                <tr>
                    <td width="100%" align="center" runat="server" id="dynamicLoginContent">
                        <table border="0" class="table">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblCompanyName" runat="server" ForeColor="#5D7B9D" Font-Bold="true"
                                        Font-Size="X-Large"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Image ID="imglogin" runat="server" Height="70" ImageUrl="~/Images/loginLogog.png" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="font-weight: bold; padding-left: 60px;" width="60%">
                                    <%--<span style="color:#3399cc;margin-bottom:0px;">--%>
                                    <asp:Label ID="lblUserName" runat="server" CssClass="LABEL">UserName:</asp:Label>
                                    <%--   <h4>
                                            UserName:</h4>
                                    </span>--%>
                                    <asp:TextBox ID="txtLoginId" runat="server" CssClass="textbox" Height="20px" Width="82%"></asp:TextBox>
                                    <%--<cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" TargetControlID="txtLoginId"
                                        WatermarkText="UserName" runat="server" EnableViewState="false" WatermarkCssClass="input">
                                    </cc1:TextBoxWatermarkExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="font-weight: bold; padding-left: 60px;" width="60%">
                                    <%-- <span style="color: #3399cc;">
                                        <h4></h4> </span>--%>
                                    <asp:Label ID="lblPAssword" runat="server" CssClass="LABEL">Password:</asp:Label>
                                    <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="textbox" Width="82%"
                                        MaxLength="20" runat="server" Height="20px"></asp:TextBox>
                                    <%-- <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" TargetControlID="txtPassword"
                                        WatermarkText="Password" runat="server" EnableViewState="false" WatermarkCssClass="input">
                                    </cc1:TextBoxWatermarkExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-left:60px; height:30px;" >
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
                            </table>
                            <table>
                            <tr>
                                <td colspan="2">
                                    <span class="FieldName">
                                        <asp:HyperLink ID="hlRegistration" runat="server" NavigateUrl="~/Register.aspx"></asp:HyperLink>
                                        <%--    <asp:LinkButton ID="NewUser" runat="server" OnClick="NewUser_Click">Register Here</asp:LinkButton>--%>
                                    </span>
                                </td>
                            </tr>
                            <tr id="Tr1" runat="server" visible="false">
                                <td colspan="2" align="right">
                                    <span class="FieldName">Forgot Password?
                                        <asp:LinkButton ID="lnkForgotPassword" runat="server" OnClick="ForgotPassword_Click">Click Here</asp:LinkButton>
                                    </span>
                                </td>
                            </tr>
                            <%--<tr id="trGodaddy" runat="server">
                                <td align="right" colspan="2">
                                    <span id="siteseal">

                                        <script type="text/javascript" src="https://seal.godaddy.com/getSeal?sealID=OB3Z92r1Re28pR7Rrwh09U9YkpYQ15o7Xc72vLCP3q87CHp3JBZidCm">
                                        </script>

                                    </span>
                                </td>
                            </tr>--%>
                        </table>
                        <asp:Label ID="lblUserLoginContent" runat="server"></asp:Label>
                    </td>
                    <td width="60%" class="Field" runat="server" id="MT_LoginContent">
                    </td>
                    <td valign="top">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
