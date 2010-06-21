<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMCustomerUserDetails.ascx.cs"
    Inherits="WealthERP.Advisor.RMCustomerUserDeatils" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<%--
<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="RM Customer User Details"></asp:Label>
            <hr />
        </td>
    </tr>
</table>--%>

<table style="width: 100%;" class="TableBackground">
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="Customer User Details"></asp:Label>
            <hr />
        </td>
    </tr>
   <tr>
        <td align="center">
            <asp:Label ID="lblStatusMsg" class="Error" runat="server" 
                EnableViewState="False"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False" CellPadding="4"
                DataKeyNames="UserId" CssClass="GridViewStyle" ShowFooter="true" 
                AllowSorting="True" OnSorting="gvCustomers_Sort" 
                onrowcommand="gvCustomers_RowCommand">
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkId" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="Customer Name" HeaderText="Customer Name"  />--%>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblCustName" runat="server" Text="Name"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtCustNameSearch" runat="server" Text = '<%# hdnNameFilter.Value %>' CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_RMCustomerUserDetails_btnNameSearch');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--<asp:Label ID="lblNameHeader" runat="server" Text='<%# Eval("CustomerName").ToString() %>'></asp:Label>--%>
                            <asp:LinkButton ID="lnkCustomerNames" runat="server" CausesValidation="false" CommandName="ViewDetails"
                                Text='<%# Eval("CustomerName").ToString() %>' CommandArgument='<%# Eval("UserId") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Login Id" HeaderText="Login Id" />
                    <asp:BoundField DataField="Email Id" HeaderText="Email Id" />
                    <asp:ButtonField CommandName="resetPassword" Text="Reset Password" />
                </Columns>
                <AlternatingRowStyle CssClass="AltRowStyle" />
            </asp:GridView>
        </td>
    </tr>
    <tr align="center">
        <td>
                        <Pager:Pager ID="mypager" runat="server" />
        </td>
    </tr>
   <%-- <tr>
        <td align="center">
            <table id="tblPager" runat="server" align="center">
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </td>
    </tr> --%>
    <tr>
        <td>
            <asp:Label ID="lblMsg" class="Error" runat="server" Text="No Records Found...!"></asp:Label>
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
<asp:Button ID="btnNameSearch" runat="server" Text="" OnClick="btnNameSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" Value="C_FirstName ASC" />
<asp:HiddenField ID="hdnNameFilter" runat="server" />
