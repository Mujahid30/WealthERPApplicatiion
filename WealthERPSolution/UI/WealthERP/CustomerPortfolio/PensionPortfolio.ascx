<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PensionPortfolio.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioViewDashboard" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<table style="width: 100%">
</table>
<table style="width: 100%;">
    <tr>
        <td colspan="2">
            <asp:Label ID="lblHeader" Text="Pension & Gratuities Portfolio" runat="server" CssClass="HeaderTextBig"></asp:Label>
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
       <%-- <td class="rightField" colspan="2">
            <asp:Label ID="lblMsg" class="Error" runat="server" Text="No Records Found....!"></asp:Label>
        </td>--%>
    </tr>
    <tr id="trTotalRows" runat="server">
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="gvrPensionAndGratuities" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" DataKeyNames="PortfolioId" EnableViewState="true" AllowPaging="True"
                CssClass="GridViewStyle" OnRowDataBound="gvrPensionAndGratuities_RowDataBound"
                OnPageIndexChanging="gvrPensionAndGratuities_PageIndexChanging" OnSorting="gvrPensionAndGratuities_Sorting"
                OnDataBound="gvrPensionAndGratuities_DataBound" ShowFooter="True">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlMenu" AutoPostBack="true" runat="server" CssClass="GridViewCmbField" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged">
                                <asp:ListItem>Select </asp:ListItem>
                                <asp:ListItem Text="View" Value="View">View</asp:ListItem>
                                <asp:ListItem Text="Edit" Value="Edit">Edit</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Organization Name" HeaderText="Organization Name" 
                        ItemStyle-HorizontalAlign="Justify">
                        <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Category" HeaderText="Category" 
                        ItemStyle-HorizontalAlign="Justify">
                        <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Deposit Amount" HeaderText="Deposit Amount (Rs)" 
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Current Value" HeaderText="Current Value (Rs)" 
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
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
<table style="width: 100%" id="tblPager" runat="server" visible="false">
    <tr>
        <td>
            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnSort" runat="server" Value="InstrumentCategory ASC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnPortfolioID" runat="server" />
