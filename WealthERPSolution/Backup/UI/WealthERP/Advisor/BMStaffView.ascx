<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BMStaffView.ascx.cs" Inherits="WealthERP.Advisor.BMStaffView" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<table class="TableBackground" width="100%">
    <tr>
        <td>
            <asp:Label ID="lblMessage" runat="server" Text="No Records Found..." CssClass="Error"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="Label1" runat="server" Text="BM Staff" CssClass="HeaderTextBig"></asp:Label>
        </td>
    </tr>
    <tr align="center">
        <td colspan="2" class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <div id="print" runat="server">
                <asp:GridView ID="gvBMStaffList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    DataKeyNames="UserId"  OnSorting="gvRMList_Sorting"
                    CssClass="GridViewStyle"
                    ShowFooter="True">
                    <RowStyle CssClass="RowStyle" />
                    <FooterStyle CssClass="FooterStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlMenu" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged" EnableViewState="True">
                                    <asp:ListItem>Select </asp:ListItem>
                                    <asp:ListItem Text="Edit profile" Value="Edit profile">Edit profile </asp:ListItem>
                                    <asp:ListItem Text="View profile" Value="View profile">View profile  </asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="StaffName" HeaderText="Staff Name" SortExpression="StaffName" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" />
                        <asp:BoundField DataField="BranchName" HeaderText="Branch Name" />
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