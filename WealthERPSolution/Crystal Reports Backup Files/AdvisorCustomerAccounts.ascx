<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisorCustomerAccounts.ascx.cs"
    Inherits="WealthERP.Advisor.AdvisorCustomerAccounts" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>


<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Customer Accounts"></asp:Label>
            <hr />
        </td>
    </tr>
</table>


<table style="width: 100%;">
    <tr>
        <td colspan="3">
            <asp:Label ID="lblMsg" runat="server" CssClass="Error" Text="No Records Found"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:GridView ID="gvrMFAccounts" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" DataKeyNames="MFAccountId, CustomerId, PortfolioId" EnableViewState="false"
                AllowPaging="false" OnSorting="gvrMFAccounts_Sorting" CssClass="GridViewStyle"
                OnSelectedIndexChanged="gvrMFAccounts_SelectedIndexChanged"
                ShowFooter="True" PageSize="15">
                <%--OnDataBound="gvrMFAccounts_DataBound"--%>
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <%--<asp:BoundField DataField="Customer Name" HeaderText="Customer Name" SortExpression="CustomerName"
                        ItemStyle-HorizontalAlign="Justify">
                        <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                    </asp:BoundField>--%>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" SortExpression="CustomerName">
                        <HeaderTemplate>
                            <asp:Label ID="lblCustName" runat="server" Text="Customer Name / Company Name"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtCustNameSearch" runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_AdvisorCustomerAccounts_btnNameSearch');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCustNameHeader" runat="server" Text='<%# Eval("Customer Name").ToString() %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <%--<asp:BoundField DataField="AMC Name" HeaderText="AMC Name" SortExpression="AMC Name"
                        ItemStyle-HorizontalAlign="Justify">
                        <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                    </asp:BoundField>--%>
                    <asp:TemplateField ItemStyle-Wrap="false" SortExpression="AMC Name">
                            <HeaderTemplate>
                                <asp:Label ID="lblAMCName" runat="server" Text="AMC Name"></asp:Label>
                                <asp:DropDownList ID="ddlAMCName" AutoPostBack="true" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlAMCName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAMCNameHeader" runat="server" Text='<%# Eval("AMC Name").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField ItemStyle-Wrap="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblFolio" runat="server" Text="Folio Number"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFolioNum" runat="server" Text='<%# Eval("Folio Number").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                   <%--<asp:BoundField DataField="Folio Number" HeaderText="Folio Number" ItemStyle-HorizontalAlign="Justify">
                        <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                    </asp:BoundField>--%>
                    <asp:ButtonField CommandName="Select" HeaderText="Select" ShowHeader="True" Text="Select"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:ButtonField>
                </Columns>
            </asp:GridView>
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
<asp:Button ID="btnNameSearch" runat="server" Text="" OnClick="btnNameSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:HiddenField ID="hdnSort" runat="server" Value="CustomerName ASC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAMCFilter" runat="server" Visible="false" />