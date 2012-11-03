﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioSystematicView.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioSystematicView" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<table width="100%" class="TableBackground">
</table>
<table style="width: 100%;">
    <tr>
        <td colspan="3">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblView" runat="server" Text="View Systematic Schemes"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
        <%--<td class="HeaderCell" colspan="2">
            <asp:Label ID="lblView" runat="server" CssClass="HeaderTextBig" Text="View Systematic Schemes"></asp:Label>
            <hr />
        </td>--%>
    </tr>
    <tr>
        <td>
            <table width="10%" cellpadding="0">
                <tr>
                    <td class="leftField">
                        <asp:Label ID="lblPortfolio" runat="server" Text="Portfolio:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlportfolio" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlportfolio_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblMsg" runat="server" CssClass="Error" Text="No Records Found"></asp:Label>
        </td>
    </tr>
    <tr id="trPager" runat="server">
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
</table>
<table>
</table>
<asp:Panel ID="Panel1" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <asp:GridView ID="gvrSystematicSchemes" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CellPadding="4" DataKeyNames="SystematicSetupId" EnableViewState="true" AllowPaging="True"
                    OnSorting="gvrSystematicSetup_Sorting" CssClass="GridViewStyle" OnDataBound="gvrSystematicSetup_DataBound"
                    ShowFooter="True">
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
                        <asp:BoundField DataField="Scheme Name" HeaderText="Scheme Name" SortExpression="SchemeName"
                            ItemStyle-HorizontalAlign="Justify">
                            <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Folio" HeaderText="Folio" ItemStyle-HorizontalAlign="Justify">
                            <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Systematic Transaction Type" HeaderText="Systematic Transaction Type"
                            SortExpression="SystematicType" ItemStyle-HorizontalAlign="Justify">
                            <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Start Date" HeaderText="Start Date (dd/mm/yyyy)" SortExpression="StartDate"
                            ItemStyle-HorizontalAlign="Justify">
                            <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="End Date" SortExpression="EndDate" HeaderText="End Date (dd/mm/yyyy)"
                            ItemStyle-HorizontalAlign="Justify">
                            <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Systematic Date" HeaderText="Systematic Date" ItemStyle-HorizontalAlign="Justify">
                            <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Amount" HeaderText="Amount (Rs)" ItemStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Frequency" HeaderText="Frequency" ItemStyle-HorizontalAlign="Justify">
                            <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Panel>
<table style="width: 100%" id="tblPager" runat="server" visible="false">
    <tr>
        <td>
            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnSort" runat="server" Value="SchemeName ASC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
