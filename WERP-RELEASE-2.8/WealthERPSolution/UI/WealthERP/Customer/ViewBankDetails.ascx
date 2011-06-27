<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewBankDetails.ascx.cs"
    Inherits="WealthERP.Customer.ViewBankDetails" %>

<%--<table class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label runat="server" Text="Bank Details" CssClass="HeaderTextSmall" Font-Bold="True"></asp:Label>
        </td>
    </tr>
</table>
<br />--%>
<table style="width: 100%;" class="TableBackground">
    <tr>
        <td>
            &nbsp;
            <asp:Label ID="lblBankDetails" runat="server" CssClass="HeaderTextBig" Text="Bank Details"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
            <asp:Label ID="lblMsg" runat="server" CssClass="Error" Text="No Records Found"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvCustomerBankAccounts" runat="server" AutoGenerateColumns="False"  AllowSorting="True" 
                CellPadding="4" CssClass="GridViewStyle" DataKeyNames="CustBankAccId" 
                OnSelectedIndexChanged="gvCustomerBankAccounts_SelectedIndexChanged" ShowFooter="True">
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="Bank Name" HeaderText="Bank Name"  />
                    <asp:BoundField DataField="Branch Name" HeaderText="Branch Name"  />
                    <asp:BoundField DataField="Account Type" HeaderText="Account Type"  />
                    <asp:BoundField DataField="Mode Of Operation" HeaderText="Mode of Operation"  />
                    <asp:BoundField DataField="Account Number" HeaderText="Account Number"  />
                </Columns>
                
            </asp:GridView>
        </td>
    </tr>
</table>
