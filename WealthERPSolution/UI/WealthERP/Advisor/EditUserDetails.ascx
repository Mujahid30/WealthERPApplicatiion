<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditUserDetails.ascx.cs"
    Inherits="WealthERP.Advisor.EditUserDetails" %>

<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Edit User Details"></asp:Label>
            <hr />
        </td>
    </tr>
</table>

<table style="width: 100%;" class="TableBackground">
    <%--<tr>
        <td colspan="2">
            &nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblEditAccount" runat="server" Text="Edit User Accounts" 
                CssClass="HeaderTextBig"></asp:Label>
        </td>
    </tr>--%>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField" style="margin-left: 80px">
            <asp:Label ID="lblName" runat="server" Text="Name :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblUser" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="margin-left: 80px">
            <asp:Label ID="lblLoginId" runat="server" Text="Login Id :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblId" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="margin-left: 80px">
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:LinkButton ID="lnkChgLoginId" CssClass="LinkButtons" runat="server" OnClick="lnkChgLoginId_Click">Change LoginId</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td >
            &nbsp;
        </td>
        <td >
            <asp:LinkButton ID="lnlChgPassword" CssClass="LinkButtons" runat="server" OnClick="lnlChgPassword_Click">Change Password</asp:LinkButton>
        </td>
    </tr>
</table>
