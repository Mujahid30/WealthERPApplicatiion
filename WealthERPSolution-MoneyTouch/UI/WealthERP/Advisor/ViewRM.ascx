<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewRM.ascx.cs" Inherits="WealthERP.Advisor.ViewRM"
    Debug="false" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="View Staff"></asp:Label>
            <hr />
        </td>
    </tr>
</table>


<table class="TableBackground" width="100%">
    <tr id="trMessage" runat="server" visible="false">
        <td>
            <asp:Label ID="lblMessage" runat="server" Text="No Records Found..." CssClass="Error"></asp:Label>
        </td>
    </tr>
    <%--<tr>
        <td class="HeaderCell">
            <asp:Label ID="Label1" runat="server" Text="RM List" CssClass="HeaderTextBig"></asp:Label>
        </td>
    </tr>--%>
    <tr align="center">
        <td colspan="2" class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <div id="print" runat="server">
                <asp:GridView ID="gvRMList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
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
                                <asp:DropDownList ID="ddlMenu" AutoPostBack="true" runat="server" CssClass="GridViewCmbField" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged" EnableViewState="True">
                                    <asp:ListItem>Select </asp:ListItem>
                                    <%--<asp:ListItem Text="Edit profile" Value="Edit profile">Edit profile </asp:ListItem>--%>
                                    <asp:ListItem Text="View profile" Value="View profile">View profile  </asp:ListItem>
                                    <asp:ListItem Text="RM Dashboard" Value="RM Dashboard">RM Dashboard</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RMName" HeaderText="RM Name" SortExpression="RMName" />
                        <%--<asp:BoundField DataField="RM Main Branch" HeaderText="RM Main Branch" />--%>
                        <asp:BoundField DataField="StaffType" HeaderText="Staff Type" />
                        <asp:BoundField DataField="StaffRole" HeaderText="Staff Role" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />                        
                        <asp:BoundField DataField="Mobile Number" HeaderText="Mobile Number" />                        
                        <asp:BoundField DataField="WealthERP Id" HeaderText="Staff Id"  />
                        
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
