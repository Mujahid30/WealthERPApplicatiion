<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserLoanMIS.ascx.cs"
    Inherits="WealthERP.Advisor.AdviserLoanMIS" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
<table style="width: 100%;">
    <tr>
        <td class="HeaderTextBig">
            <asp:Label ID="lblLoanMIS" runat="server" CssClass="HeaderTextBig" Text="Loan MIS"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr id="trMessage" runat="server" visible="false">
        <td>
            <asp:Label ID="lblMessage" runat="server" CssClass="Error" Text="No Records Found..."></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="gvLoanMIS" runat="server" DataKeyNames="LiabilitiesId" AllowSorting="True"
                AutoGenerateColumns="False" CellPadding="4" EnableViewState="false" CssClass="GridViewStyle"
                ShowFooter="True">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" 
                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="LoanType" HeaderText="Loan Type" 
                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="LoanAmount" HeaderText="Loan Amount" 
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="LenderName" HeaderText="Leander Name" 
                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="Stage" HeaderText="Stage"  ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="Commission" HeaderText="Commission" 
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
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
