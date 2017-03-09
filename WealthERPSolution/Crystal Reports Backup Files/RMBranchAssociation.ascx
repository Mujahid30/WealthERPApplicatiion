<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMBranchAssociation.ascx.cs"
    Inherits="WealthERP.Advisor.RMBranchAssociation" %>

<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
    <table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Staff Branch Association"></asp:Label>
            <hr />
        </td>
    </tr>
</table>

        <table style="width: 100%;" class="TableBackground">
           
            <tr>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="Label2" runat="server" Text="RM Name:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" CssClass="cmbField"
                        OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trAssociatedBranch" runat="server">
                <td colspan="2">
                    <asp:Label ID="lblBranchList0" runat="server" CssClass="HeaderTextSmall" Text="Associated Branches"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblError1" runat="server" CssClass="FieldName" Text="Associated Branch List is empty"></asp:Label>
                    <asp:GridView ID="gvRMBranch" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" DataKeyNames="BranchId" ShowFooter="True">
                        <FooterStyle CssClass="FooterStyle" />
                        <PagerStyle CssClass="PagerStyle " />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <Columns>
                            <asp:BoundField DataField="Branch Code" HeaderText="Branch Code"  />
                            <asp:BoundField DataField="Branch Name" HeaderText="Branch Name"  />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
           
            <tr id="trBranch" runat="server">
                <td colspan="2">
                    <asp:Label ID="lblBranchList" runat="server" CssClass="HeaderTextSmall" Text="Branch List"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblError" runat="server" CssClass="FieldName" Text="Branch List"></asp:Label>
                    <asp:GridView ID="gvBranchList" runat="server" AutoGenerateColumns="False" DataKeyNames="BranchId"
                        AllowSorting="True" CssClass="GridViewStyle">
                        <FooterStyle CssClass="FooterStyle" />
                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkId" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Branch Code" HeaderText="Branch Code"  />
                            <asp:BoundField DataField="Branch Name" HeaderText="Branch Name"  />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" class="SubmitCell">
                    <asp:Button ID="btnAssociate" runat="server" OnClick="btnAssociate_Click" Text="Associate"
                        CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_RMBranchAssociation_btnAssociate', 'S');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_RMBranchAssociation_btnAssociate', 'S');" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
