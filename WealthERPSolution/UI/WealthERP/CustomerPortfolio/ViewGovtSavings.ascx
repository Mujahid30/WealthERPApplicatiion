<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewGovtSavings.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.ViewGovtSavings" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<table style="width: 100%">
 <tr>
        <td class="HeaderCell" colspan="4">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Government Savings"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td >
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
       
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
       <%-- <td colspan="2">
            <asp:Label ID="lblMsg" class="Error" runat="server" Text="No Records Found!"></asp:Label>
        </td>--%>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:GridView ID="gvrGovtSavings" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" DataKeyNames="GovtSavingId" AllowPaging="True" CssClass="GridViewStyle"
                OnRowDataBound="gvrGovtSavings_RowDataBound" OnSorting="gvrGovtSavings_Sorting"
                OnDataBound="gvrGovtSavings_DataBound" ShowFooter="True">
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
                            <asp:DropDownList ID="ddlMenu" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged"
                                CssClass="GridViewCmbField">
                                <asp:ListItem>Select </asp:ListItem>
                                <asp:ListItem Text="View" Value="View">View</asp:ListItem>
                                <asp:ListItem Text="Edit" Value="Edit">Edit</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Category" HeaderText="Category" 
                        ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Particulars" HeaderText="Particulars" 
                        ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Deposit Date" HeaderText="Deposit Date (dd/mm/yyyy)" 
                        ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Maturity Date" HeaderText="Maturity Date (dd/mm/yyyy)" 
                        ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Deposit Amount" HeaderText="Deposit Amount (Rs)" 
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Rate Of Interest" HeaderText="Rate Of Interest (%)" 
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Current Value" HeaderText="Current Value (Rs)" 
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Maturity Value" HeaderText="Maturity Value (Rs)" 
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
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
<asp:HiddenField ID="hdnSort" runat="server" Value="CGSNP_CreatedOn DESC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
