<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMUserDetails.ascx.cs"
    Inherits="WealthERP.Advisor.RMUserDetails" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="RM User Details"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table style="width: 100%;">
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr id="trNoRecords" runat="server">
        <td>
            <asp:Label ID="lblMsg" class="Error" runat="server" Text="No Records Found...!"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label ID="lblStatusMsg" class="Error" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td rowspan="2">
            <asp:GridView ID="gvRMUsers" runat="server" CellPadding="4" CssClass="GridViewStyle"
                AllowSorting="True" OnSorting="gvRMUsers_Sorting" ShowFooter="true" AutoGenerateColumns="False"
                OnRowCommand="gvRMUsers_RowCommand" DataKeyNames="UserId">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Center" VerticalAlign="Middle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle " />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkId" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="RMName" HeaderText="RM Name" />--%>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblRMName" runat="server" Text="RM Name"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtRMNameSearch" runat="server" Text = '<%# hdnNameFilter.Value %>' CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_RMUserDetails_btnNameSearch');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--<asp:Label ID="lblRMNameHeader" runat="server" Text='<%# Eval("RMName").ToString() %>'></asp:Label>--%>
                            <asp:LinkButton ID="lnkRMNames" runat="server" CausesValidation="false" CommandName="ViewDetails"
                                Text='<%# Eval("RMName").ToString() %>' CommandArgument='<%# Eval("UserId") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="LoginId" HeaderText="Login Id" />
                    <%--<asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />--%>
                    <asp:BoundField DataField="EmailId" HeaderText="Email Id" />
                    <asp:ButtonField CommandName="resetPassword" Text="Reset Password" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center">
            <Pager:Pager ID="mypager" runat="server">
            </Pager:Pager>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblStatus" class="FieldName" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnGenerate" runat="server" OnClick="btnGenerate_Click" Text="Send Login Password"
                CssClass="PCGLongButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerProofsAdd_btnSubmit', 'L');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerProofsAdd_btnSubmit', 'L');" />
        </td>
    </tr>
</table>
<table id="tblPager" runat="server" align="center">
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
<asp:Button ID="btnNameSearch" runat="server" Text="" OnClick="btnNameSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" Value="RMName ASC" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnNameFilter" runat="server" />
