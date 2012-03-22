<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageRepositoryCategory.ascx.cs"
    Inherits="WealthERP.Admin.ManageRepositoryCategory" %>
<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Manage Repository Category"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<div id="dv" style="padding: 20px 0 0 10px;">
    <table class="TableBackground" width="800px">
        <tr id="trContent" runat="server" class="trHeader">
            <td>
                <asp:Label ID="lblExistingCat" runat="server" Text="Existing Category Name" CssClass="HeaderTextSmall"
                    ForeColor="White"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCustomCategory" runat="server" Text="Custom Category Name" CssClass="HeaderTextSmall"
                    ForeColor="White"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblRoleAccess" runat="server" Text="Role Access" CssClass="HeaderTextSmall"
                    ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCategory1" runat="server" CssClass="FieldName"></asp:Label>
                <asp:HiddenField ID="hdnCat1" runat="server" />
            </td>
            <td>
                <asp:TextBox CssClass="txtField" ID="txtCategory1" runat="server" MaxLength="30"></asp:TextBox>
            </td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" CausesValidation="True" RepeatDirection="Horizontal"
                    CssClass="cmbField" RepeatLayout="Flow">
                    <asp:ListItem Value="1000" Selected="True" Enabled="false">Advisor</asp:ListItem>
                    <asp:ListItem Value="1002">BM</asp:ListItem>
                    <asp:ListItem Value="1001">RM</asp:ListItem>
                    <asp:ListItem Value="1005">Research</asp:ListItem>
                    <asp:ListItem Value="1004">Ops</asp:ListItem>
                    <asp:ListItem Value="1003">Customer</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCategory2" runat="server" CssClass="FieldName"></asp:Label>
                <asp:HiddenField ID="hdnCat2" runat="server" />
            </td>
            <td>
                <asp:TextBox CssClass="txtField" ID="txtCategory2" runat="server" MaxLength="30"></asp:TextBox>
            </td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList2" runat="server" CausesValidation="True" RepeatDirection="Horizontal"
                    CssClass="cmbField" RepeatLayout="Flow">
                    <asp:ListItem Value="1000" Selected="True" Enabled="false">Advisor</asp:ListItem>
                    <asp:ListItem Value="1002">BM</asp:ListItem>
                    <asp:ListItem Value="1001">RM</asp:ListItem>
                    <asp:ListItem Value="1005">Research</asp:ListItem>
                    <asp:ListItem Value="1004">Ops</asp:ListItem>
                    <asp:ListItem Value="1003">Customer</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCategory3" runat="server" CssClass="FieldName"></asp:Label>
                <asp:HiddenField ID="hdnCat3" runat="server" />
            </td>
            <td>
                <asp:TextBox CssClass="txtField" ID="txtCategory3" runat="server" MaxLength="30"></asp:TextBox>
            </td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList3" runat="server" CausesValidation="True" RepeatDirection="Horizontal"
                    CssClass="cmbField" RepeatLayout="Flow">
                    <asp:ListItem Value="1000" Selected="True" Enabled="false">Advisor</asp:ListItem>
                    <asp:ListItem Value="1002">BM</asp:ListItem>
                    <asp:ListItem Value="1001">RM</asp:ListItem>
                    <asp:ListItem Value="1005">Research</asp:ListItem>
                    <asp:ListItem Value="1004">Ops</asp:ListItem>
                    <asp:ListItem Value="1003">Customer</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCategory4" runat="server" CssClass="FieldName"></asp:Label>
                <asp:HiddenField ID="hdnCat4" runat="server" />
            </td>
            <td>
                <asp:TextBox CssClass="txtField" ID="txtCategory4" runat="server" MaxLength="30"></asp:TextBox>
            </td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList4" runat="server" CausesValidation="True" RepeatDirection="Horizontal"
                    CssClass="cmbField" RepeatLayout="Flow">
                    <asp:ListItem Value="1000" Selected="True" Enabled="false">Advisor</asp:ListItem>
                    <asp:ListItem Value="1002">BM</asp:ListItem>
                    <asp:ListItem Value="1001">RM</asp:ListItem>
                    <asp:ListItem Value="1005">Research</asp:ListItem>
                    <asp:ListItem Value="1004">Ops</asp:ListItem>
                    <asp:ListItem Value="1003">Customer</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td class="SubmitCell">
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Update"
                    CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_ManageRepositoryCategory_btnSubmit', 'S');"
                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_ManageRepositoryCategory_btnSubmit', 'S');" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</div>
