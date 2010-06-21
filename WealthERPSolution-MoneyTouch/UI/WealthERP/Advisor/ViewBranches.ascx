<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewBranches.ascx.cs"
    Inherits="WealthERP.Advisor.ViewBranches" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Branch/Association"></asp:Label>
            <hr />
        </td>
    </tr>
</table>

<table class="TableBackground" style="width: 100%">
    <tr>
        <td>
            <asp:Label ID="lblMessage" runat="server" Text="No Records Found..." CssClass="Error"></asp:Label>
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
            <asp:GridView ID="gvBranchList" runat="server" OnSorting="gvBranchList_Sorting" AllowSorting="True"
                AutoGenerateColumns="False" DataKeyNames="BranchId" OnRowCommand="gvBranchlist_RowCommand"
                OnRowEditing="gvBranchList_RowEditing" Font-Size="Small" CssClass="GridViewStyle" ShowFooter="true">
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle CssClass="PagerStyle " />
                <%-- <EmptyDataTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Text="Edit" Value="Edit">Edit Details</asp:ListItem>
                    <asp:ListItem Text="View" Value="View">View Details</asp:ListItem>
                    </asp:DropDownList>
                </EmptyDataTemplate>--%>
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <RowStyle CssClass="RowStyle" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlMenu" AutoPostBack="true" CssClass="GridViewCmbField" runat="server" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged">
                                <asp:ListItem>Select </asp:ListItem>
                                <asp:ListItem Text="Edit" Value="Edit">Edit </asp:ListItem>
                                <asp:ListItem Text="View" Value="View">View</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="BranchName" HeaderText="Branch/Associate Name"  />
                    <asp:BoundField DataField="BranchCode" HeaderText="Branch/Associate Code"/>
                    <asp:BoundField DataField="Email" HeaderText="Email"  />
                    <asp:BoundField DataField="Phone" HeaderText="Phone Number"  />
                    <asp:BoundField DataField="BranchHead" HeaderText="Branch Head Name"  />
                    <asp:BoundField DataField="BranchType" HeaderText="Branch Type"  />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
<table id="tblPager" runat="server" style="width: 100%" visible="true">
    <tr>
        <td>
            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" Value="AB_BranchName ASC" />
