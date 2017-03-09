<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FamilyDetails.ascx.cs"
    Inherits="WealthERP.Customer.FamilyDetails" %>
    <table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblView" runat="server" CssClass="HeaderTextBig" Text="Add New Customer"></asp:Label>
            <hr />
        </td>
    </tr>
</table>


<table class="TableBackground">
    <%--<tr>
        <td class="HeaderCell" colspan="2">
            <asp:Label ID="Label1" runat="server" Text="Customer Family Entry Form" CssClass="HeaderTextBig"></asp:Label>
        </td>
    </tr>--%>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label2" runat="server" Text="Relationship:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlRelationship" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="SubmitCell" colspan="2">
            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" CssClass="PCGButton"
                onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_FamilyDetails_btnAdd');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_FamilyDetails_btnAdd');"
                Text="Add Details" />
        </td>
    </tr>
</table>
