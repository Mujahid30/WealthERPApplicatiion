<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LiabilityView.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.LiabilityView" %>

<table width="100%">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Liabilities"></asp:Label>
            <hr />
        </td>
    </tr>
<tr>
<td>
<asp:Label ID="lblMsg" Text="No Records Found..!" runat="server" CssClass="Error"></asp:Label>
</td>
</tr>
    <tr>
        <td>
            <asp:GridView ID="gvLiabilities" EnableViewState="true" AllowPaging="True" CssClass="GridViewStyle"
                runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="LiabilityId" 
                onpageindexchanging="gvLiabilities_PageIndexChanging">
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
                            <asp:DropDownList ID="ddlAction" AutoPostBack="true" runat="server" 
                                EnableViewState="True" CssClass="GridViewCmbField" 
                                onselectedindexchanged="ddlAction_SelectedIndexChanged" >
                                <asp:ListItem>Select </asp:ListItem>
                                <asp:ListItem Text="View" Value="View">View</asp:ListItem>
                                <asp:ListItem Text="Edit" Value="Edit">Edit</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Loan Type" HeaderText="Loan Type"  ItemStyle-HorizontalAlign="Left"   HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Lender" HeaderText="Lender"  ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="Amount" HeaderText="Amount(Rs)"  ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Rate of Interest" HeaderText="Rate of Interest (%)" 
                        ItemStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PaymentType" HeaderText="Payment Type" 
                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="LumpsusmInstallment" HeaderText="Lumpsum/Installment" 
                        ItemStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="LoanOutstanding" HeaderText="OutStanding Amount" 
                        ItemStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Frequency" HeaderText="Frequency" 
                        ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="NextInstallmentDate" HeaderText="Next Installment Date" 
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                  <%--  <asp:BoundField DataField="Tenure(in Months)" HeaderText="Tenure(in Months)" Visible="false"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" Visible="false" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>--%>
                   <%-- <asp:BoundField DataField="Scheme" HeaderText="Scheme" Visible="false" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>--%>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
