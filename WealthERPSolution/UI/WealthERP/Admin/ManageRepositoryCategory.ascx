﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageRepositoryCategory.ascx.cs"
    Inherits="WealthERP.Admin.ManageRepositoryCategory" %>
    <table width="100%">
<tr>
<td colspan="3" style="width: 100%;">
<div class="divPageHeading">
    <table cellspacing="0"  width="100%">
        <tr>
        <td align="left">Repository Category</td>
        <td  align="right" style="padding-bottom:2px;">
        </td>
        </tr>
    </table>
</div>
</td>
</tr>
</table>
<%--<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Repository Category"></asp:Label>
            <hr />
        </td>
    </tr>
</table>--%>
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
                <span id="span1" class="spnRequiredField">*</span>
            </td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" CausesValidation="True" RepeatDirection="Horizontal"
                    CssClass="cmbFielde" RepeatLayout="Flow">
                    <asp:ListItem Value="1000" Selected="True" Enabled="false">Advisor</asp:ListItem>
                    <asp:ListItem Value="1002">BM</asp:ListItem>
                    <asp:ListItem Value="1001">RM</asp:ListItem>
                    <asp:ListItem Value="1005">Research</asp:ListItem>
                    <asp:ListItem Value="1004">Ops</asp:ListItem>
                    <asp:ListItem Value="1003">Customer</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr style="line-height:5px;">
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvtxtCategory1" runat="server"
                    ControlToValidate="txtCategory1" ErrorMessage="<br/>Enter Category Text" CssClass="cvPCG"
                    Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCategory2" runat="server" CssClass="FieldName"></asp:Label>
                <asp:HiddenField ID="hdnCat2" runat="server" />
            </td>
            <td>
                <asp:TextBox CssClass="txtField" ID="txtCategory2" runat="server" MaxLength="30"></asp:TextBox>
                <span id="span2" class="spnRequiredField">*</span>
            </td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList2" runat="server" CausesValidation="True" RepeatDirection="Horizontal"
                    CssClass="cmbFielde" RepeatLayout="Flow">
                    <asp:ListItem Value="1000" Selected="True" Enabled="false">Advisor</asp:ListItem>
                    <asp:ListItem Value="1002">BM</asp:ListItem>
                    <asp:ListItem Value="1001">RM</asp:ListItem>
                    <asp:ListItem Value="1005">Research</asp:ListItem>
                    <asp:ListItem Value="1004">Ops</asp:ListItem>
                    <asp:ListItem Value="1003">Customer</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr style="line-height:5px;">
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvtxtCategory2" runat="server"
                    ControlToValidate="txtCategory2" ErrorMessage="<br/>Enter Category Text" CssClass="cvPCG"
                    Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCategory3" runat="server" CssClass="FieldName"></asp:Label>
                <asp:HiddenField ID="hdnCat3" runat="server" />
            </td>
            <td>
                <asp:TextBox CssClass="txtField" ID="txtCategory3" runat="server" MaxLength="30"></asp:TextBox>
                <span id="span3" class="spnRequiredField">*</span>
            </td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList3" runat="server" CausesValidation="True" RepeatDirection="Horizontal"
                    CssClass="cmbFielde" RepeatLayout="Flow">
                    <asp:ListItem Value="1000" Selected="True" Enabled="false">Advisor</asp:ListItem>
                    <asp:ListItem Value="1002">BM</asp:ListItem>
                    <asp:ListItem Value="1001">RM</asp:ListItem>
                    <asp:ListItem Value="1005">Research</asp:ListItem>
                    <asp:ListItem Value="1004">Ops</asp:ListItem>
                    <asp:ListItem Value="1003">Customer</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr style="line-height:5px;">
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvtxtCategory3" runat="server"
                    ControlToValidate="txtCategory3" ErrorMessage="<br/>Enter Category Text" CssClass="cvPCG"
                    Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCategory4" runat="server" CssClass="FieldName"></asp:Label>
                <asp:HiddenField ID="hdnCat4" runat="server" />
            </td>
            <td>
                <asp:TextBox CssClass="txtField" ID="txtCategory4" runat="server" MaxLength="30"></asp:TextBox>
                <span id="span4" class="spnRequiredField">*</span>
            </td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList4" runat="server" CausesValidation="True" RepeatDirection="Horizontal"
                    CssClass="cmbFielde" RepeatLayout="Flow">
                    <asp:ListItem Value="1000" Selected="True" Enabled="false">Advisor</asp:ListItem>
                    <asp:ListItem Value="1002">BM</asp:ListItem>
                    <asp:ListItem Value="1001">RM</asp:ListItem>
                    <asp:ListItem Value="1005">Research</asp:ListItem>
                    <asp:ListItem Value="1004">Ops</asp:ListItem>
                    <asp:ListItem Value="1003">Customer</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr style="line-height:5px;">
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvtxtCategory4" runat="server"
                    ControlToValidate="txtCategory4" ErrorMessage="<br/>Enter Category Text" CssClass="cvPCG"
                    Display="Dynamic"></asp:RequiredFieldValidator>
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
