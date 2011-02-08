<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IFF.ascx.cs" Inherits="WealthERP.SuperAdmin.IFF" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<script>

</script>
<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="IFF Grid"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table width="100%">
<tr>
<td align="center">
<div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
    No Records found.....
</div>
</td>
</tr>
</table>
<table class="TableBackground" width="100%">
    <%--<tr>
        <td class="HeaderCell">
            <asp:Label ID="Label1" runat="server" Text="RM List" CssClass="HeaderTextBig"></asp:Label>
        </td>
    </tr>--%>
    <tr align="center">
        <td colspan="2" class="leftField" align="right">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <div id="print" runat="server">
                <asp:GridView ID="gvAdvisorList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    DataKeyNames="UserId" OnSorting="gvAdvisorList_Sorting" CssClass="GridViewStyle"
                    ShowFooter="True" OnRowDataBound="gvAdvisorList_RowDataBound">
                    <RowStyle CssClass="RowStyle" />
                    <FooterStyle CssClass="FooterStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Left" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlMenu" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                    OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged" EnableViewState="True">
                                    <asp:ListItem>Select </asp:ListItem>
                                    <asp:ListItem Text="View Dashboard" Value="View Dashboard">View Dashboard  </asp:ListItem>
                                    <asp:ListItem Text="Edit profile" Value="Edit profile">View/Edit profile </asp:ListItem>
                                    <%--<asp:ListItem Text="User Management" Value="User Management">User Management</asp:ListItem>--%>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="IFFName" HeaderText="IFF" SortExpression="IFFName" ItemStyle-HorizontalAlign="Left"
                            ItemStyle-Wrap="false" />
                        <%--<asp:BoundField DataField="RM Main Branch" HeaderText="RM Main Branch" />--%>
                        <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <asp:Label ID="lblCategory" runat="server" Text="Category"></asp:Label>
                                <asp:DropDownList ID="ddlCategory" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                    OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Category").ToString() %>'
                                    ItemStyle-HorizontalAlign="Left"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="IFFAddress" HeaderText="Area" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="IFFCity" HeaderText="City " ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="IFFContactPerson" HeaderText="Contact Person" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="IFFMobileNumber" HeaderText="Mobile" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="IFFEmailId" HeaderText="Email" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderText="Is Active" DataField="imgIFFIsActive" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="MF Subs" DataField="imgIFFMutualfund" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="EQ Subs" DataField="imgIFFEquity" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="LI Subs" DataField="imgIFFInsurance" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Loan Subs" DataField="imgIFFLiabilities" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="PMS Subs" DataField="imgIFFPMS" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Fixed Inc. Subs" DataField="imgIFFFixedIncome" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Postal Subs" DataField="imgIFFPostalSavings" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Commodities Subs" DataField="imgIFFComodities" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Real Est., Subs" DataField="imgIFFRealEstate" ItemStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </td>
    </tr>
</table>
<div id="DivPager" runat="server">
    <table style="width: 100%">
        <tr id="trPager" runat="server">
            <td>
                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField ID="hdnCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" Value="RMName ASC" />
<asp:HiddenField ID="hdnCategory" runat="server" />
