<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioPersonal.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioPersonal" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<table class="TableBackground" width="100%">
    <tr>
        <td>
            <asp:Label ID="lblHeader" class="HeaderTextBig" runat="server" Text="Personal Portfolio"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <%--<td>
            <asp:Label ID="lblMsg" class="Error" runat="server" Text="No Records Found...!"></asp:Label>
        </td>--%>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:GridView ID="gvrPersonal" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" DataKeyNames="PersonalId" EnableViewState="true" AllowPaging="True"
                CssClass="GridViewStyle" OnPageIndexChanging="gvrPersonal_PageIndexChanging"
                OnSorting="gvrPersonal_Sorting" ShowFooter="True">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlMenu" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged" EnableViewState="true">
                                <asp:ListItem>Select </asp:ListItem>
                                <asp:ListItem Text="Edit" Value="Edit">Edit</asp:ListItem>
                                <asp:ListItem Text="View" Value="View">View</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Name"  />
                    <asp:BoundField DataField="Category" HeaderText="Category" 
                        ItemStyle-HorizontalAlign="Justify">
                        <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Quantity" HeaderText="Unit" ItemStyle-HorizontalAlign="Justify">
                        <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Current Value" HeaderText="Current Value (Rs)" 
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Purchase Value" HeaderText="Purchase Value (Rs)" 
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Purchase Date" HeaderText="Purchase Date (dd/mm/yyyy)"
                         ItemStyle-HorizontalAlign="Justify">
                        <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server" visible="false">
    <tr>
    <td align="center">
     <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
    </div>
    </td>
    </tr>
 </table>
<table class="TableBackground" width="100%">
    <tr id="trPager" runat="server">
        <td align="center" colspan="3">
            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnSort" runat="server" Value="Name ASC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
