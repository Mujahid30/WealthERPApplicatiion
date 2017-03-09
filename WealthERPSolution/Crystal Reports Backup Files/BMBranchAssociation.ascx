<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BMBranchAssociation.ascx.cs"
    Inherits="WealthERP.Advisor.BMBranchAssociation" %>
<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Branch Association"></asp:Label>
            <hr />
        </td>
    </tr>
</table>

<asp:GridView ID="gvBranchListBM" runat="server" AutoGenerateColumns="False" DataKeyNames="BranchId"
    AllowSorting="True" CssClass="GridViewStyle">
    <FooterStyle CssClass="FooterStyle" />
    <PagerStyle HorizontalAlign="Center" />
    <SelectedRowStyle CssClass="SelectedRowStyle" />
    <HeaderStyle CssClass="HeaderStyle" />
    <EditRowStyle CssClass="EditRowStyle" />
    <AlternatingRowStyle CssClass="AltRowStyle" />
    <RowStyle CssClass="RowStyle" />
    <Columns>
        <asp:TemplateField HeaderText="Select">
            <ItemTemplate>
                <asp:RadioButton ID="rbtnBM" runat="server" AutoPostBack="true" value='<%# Eval("BranchId") %>'
                    OnCheckedChanged="RadioButton1_CheckedChanged" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Branch Name" HeaderText="Branch Name" SortExpression="Branch Name" />
        <asp:BoundField DataField="Branch Address" HeaderText="Branch Address" SortExpression="Branch Address" />
        <asp:BoundField DataField="Branch Phone" HeaderText="Branch Phone" SortExpression="Branch Phone" />
    </Columns>
</asp:GridView>
<p>
    &nbsp;</p>
<p>
    <asp:Button ID="btnAssociateBM" runat="server" OnClick="btnAssociateBM_Click" Text="Associate"
        CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_BMBranchAssociation_btnAssociateBM');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_BMBranchAssociation_btnAssociateBM');" />
</p>
<asp:Label ID="lblIllegal" runat="server" Text="All Branches Have Managers.. "></asp:Label>
<asp:LinkButton ID="lnkAddBranch" runat="server" OnClick="lnkAddBranch_Click" CssClass="LinkButtons">Add Branch</asp:LinkButton>
