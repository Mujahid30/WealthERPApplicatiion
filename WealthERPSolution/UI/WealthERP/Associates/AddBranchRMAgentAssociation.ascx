<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddBranchRMAgentAssociation.ascx.cs"
    Inherits="WealthERP.Associates.AddBranchRMAgentAssociation" %>
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Add Associates/Agent Code
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<%--<table width="100%">
    <tr>
        <td colspan="6">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Step-3
            </div>
        </td>
    </tr>
</table>--%>
<table width="60%">
    <tr>
        <td align="leftField">
            &nbsp;
        </td>
        <%--        <td class="rightField">
            <asp:RadioButton ID="rbtBM" Class="cmbField" runat="server" GroupName="BMRMAgent"   AutoPostBack="true"
                Checked="True" Text="BM" oncheckedchanged="rbtBM_CheckedChanged"  /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:RadioButton ID="rbtRM" Class="cmbField" runat="server" GroupName="BMRMAgent"  AutoPostBack="true"
                Text="RM" oncheckedchanged="rbtRM_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:RadioButton ID="rbtnAgent" Class="cmbField" runat="server" GroupName="BMRMAgent"  AutoPostBack="true"
                Text="Agent" oncheckedchanged="rbtnAgent_CheckedChanged" />
        </td>--%>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Generate code for:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlUserType" runat="server" CssClass="cmbField" 
                onselectedindexchanged="ddlUserType_SelectedIndexChanged">
            <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
            <asp:ListItem Text="BM" Value="BM" ></asp:ListItem>
            <asp:ListItem Text="RM" Value="RM" ></asp:ListItem>
            <asp:ListItem Text="Associates" Value="Associates" ></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="leftField">
            <asp:Label ID="lblSelectType" runat="server" CssClass="FieldName" Text="Select:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlSelectType" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
        <tr>
        <td class="leftField">
            <asp:Label ID="lblNoOfCode" CssClass="FieldName" runat="server" Text="No. of code to be generated:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtNoOfCode" runat="server" CssClass="txtField" Text="1"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblAgentCode" CssClass="FieldName" runat="server" Text="Agent Code:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtAgentCode" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            &nbsp;
        </td>
        <td class="rightField">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnSubmit_Click" />
        </td>
    </tr>
</table>
