<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GenerateLoginPassword.ascx.cs"
    Inherits="WealthERP.Advisor.AdvisorCustomerGeneratePassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table style="width: 100%;" class="TableBackground">
    <tr>
        <td colspan="2">
            <asp:Label ID="lblGenerateLoginPass" runat="server" Text="Generate Login and Password"
                CssClass="HeaderTextBig"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
         <td>
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <asp:Button ID="btnGenerate" runat="server" Text="Generate Login and Password" 
                onclick="btnGenerate_Click" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="leftField" width="50%">
            <asp:Label ID="lblLoginIdLabel" runat="server" Text="LoginId:"
                CssClass="HeaderTextSmall"></asp:Label>
        </td>
        <td> 
        <asp:Label ID="lblLoginId" runat="server" 
                CssClass="Field" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField" width="50%">
            <asp:Label ID="lblPasswordLabel" runat="server" Text="Password:"
                CssClass="HeaderTextSmall"></asp:Label>
        </td>
        <td>
        <asp:Label ID="lblPassword" runat="server" 
                CssClass="Field" Text=""></asp:Label>
        </td>
    </tr>
    
</table>
