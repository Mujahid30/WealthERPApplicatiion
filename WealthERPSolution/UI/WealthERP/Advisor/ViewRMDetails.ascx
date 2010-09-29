<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewRMDetails.ascx.cs"
    Inherits="WealthERP.Advisor.ViewRMDetails" %>
<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Staff Details"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table class="TableBackground">
    <%--<tr>
        <td colspan="2" class="HeaderCell">
            <asp:Label ID="Label9" runat="server" CssClass="HeaderTextBig" Text="View RM Profile"></asp:Label>
        </td>
    </tr>--%>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="leftField" colspan="2">
            <asp:LinkButton ID="lnkEdit" runat="server" class="LinkButtons" Text="Edit" OnClick="lnkEdit_Click">Edit</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 50%">
            <asp:Label ID="lblName" runat="server" CssClass="FieldName" Text="Name:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblRMName" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 50%">
            <asp:Label ID="lblCTC" runat="server" CssClass="FieldName" Text="CTC:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblCTCValue" runat="server" Text="" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 50%">
            <asp:Label ID="lblStaffType" runat="server" CssClass="FieldName" Text="Staff Type:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblStaffTypeValue" runat="server" Text="" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="Label2" runat="server" CssClass="HeaderTextSmall" Text="Contact Details"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPhoneDirectNumber" runat="server" CssClass="FieldName" Text="Telephone Number Direct:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblPhDirect" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="Telephone Number Extention:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblPhExt" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Telephone Number Residence:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblPhResi" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Fax:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblFax" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="Mobile Number:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblMobile" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblEmail" runat="server" CssClass="FieldName" Text="Email Id:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblMail" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            &nbsp;
        </td>
        <td class="rightField">
            &nbsp;
        </td>
    </tr>
    <tr id="trBranchAssoication" runat="server">
        <td colspan="2">
            <asp:Label ID="lblBranchAssociation" runat="server" CssClass="HeaderTextSmall" Text="Branch Association"></asp:Label>
            <hr />
        </td>
        <td class="rightField">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:GridView ID="gvRMBranch" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="GridViewStyle" DataKeyNames="BranchId" ShowFooter="True" 
                OnRowDataBound="gvRMBranch_RowDataBound">
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle CssClass="PagerStyle " />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <RowStyle CssClass="RowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkBx" runat="server" Enabled="false"/>
                        </ItemTemplate>
                        <FooterTemplate>
                            <%--<asp:Button ID="btnDeleteSelected" CssClass="FieldName" runat="server" Text="Delete"
                                OnClick="btnDeleteSelected_Click" />--%>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Is Main Branch?">
                        <ItemTemplate>
                            <asp:RadioButton ID="rbtnMainBranch" runat="server" GroupName="grpMainBranch" AutoPostBack="true"
                                Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:BoundField DataField="Branch Code" HeaderText="Branch Code" SortExpression="Branch Code" />
                    <asp:BoundField DataField="Branch Name" HeaderText="Branch Name" SortExpression="Branch Name" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            &nbsp;
        </td>
        <td class="rightField">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            &nbsp;
        </td>
        <td class="rightField">
            &nbsp;
        </td>
    </tr>
    <tr>
        <%-- <td class="SubmitCell" colspan="2">
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_ViewRMDetails_btnDelete');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_ViewRMDetails_btnDelete');"
                OnClick="btnDelete_Click" />
        </td>--%>
    </tr>
</table>
<table class="TableBackground" width="100%">
    <tr>
        <td>
        </td>
    </tr>
</table>
