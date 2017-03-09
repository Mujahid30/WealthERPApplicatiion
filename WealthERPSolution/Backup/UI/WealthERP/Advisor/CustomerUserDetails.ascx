<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerUserDetails.ascx.cs" Inherits="WealthERP.Advisor.CustomerUserDetails" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>


<div>
    <table class="TableBackground">
        <tr>
            <td colspan="2" class="rightField">
                <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="Customer List"></asp:Label>
                <br />
                <asp:Label ID="lblMessage" runat="server" CssClass="FieldName" Text="You Dont have any Customers..."></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
                <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="rightField" colspan="2">
                <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    CssClass="GridViewStyle" DataKeyNames="CustomerId" ForeColor="#333333" Width="640px"
                    OnSelectedIndexChanged="gvCustomers_SelectedIndexChanged" Height="64px" AllowSorting="True"
                    HorizontalAlign="Center" AllowPaging="True" OnPageIndexChanging="gvCustomers_PageIndexChanging"
                    BackImageUrl="~/CSS/Images/HeaderGlassBlack.jpg" OnSorting="gvCustomers_Sort">
                    <FooterStyle BackColor="#507CD1" CssClass="FieldName" ForeColor="White" />
                    <RowStyle Font-Size="Small" CssClass="RowStyle" />
                    <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="EditRowStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" ForeColor="#333333" />
                    <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="PagerStyle" />
                    <HeaderStyle CssClass="HeaderStyle" ForeColor="White" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                        <asp:BoundField DataField="S.No" HeaderText="S.No" />
                        <asp:BoundField DataField="Customer Name" HeaderText="Customer Name" SortExpression="C_FirstName" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Phone Number" HeaderText="Phone Number" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChange">
                                    <asp:ListItem Text="Select" />
                                    <asp:ListItem Text="Dashboard" />
                                    <asp:ListItem Text="Profile" />
                                    <asp:ListItem Text="Portfolio" />
                                    <asp:ListItem Text="Alerts" />
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
            </td>
        </tr>
        <br />
    </table>
</div>
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" />

