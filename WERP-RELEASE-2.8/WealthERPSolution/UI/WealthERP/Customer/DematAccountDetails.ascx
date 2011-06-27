<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DematAccountDetails.ascx.cs"
    Inherits="WealthERP.Customer.DematAccountDetails" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<br />
<table width="100%">
    <tr>
        <td>
            <asp:Label ID="lblDematDetails" runat="server" Text="Demat Details" CssClass="HeaderTextBig"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <hr />
        </td>
    </tr>
    <tr><td>
        <asp:Label ID="lblError" runat="server" Visible="False" CssClass="Error"></asp:Label>
        </td></tr>
</table>
<%--<table width="100%">
    <tr>
        <td>
            <asp:Label ID="lblCurrentPage" runat="server" class="Field" Text=""></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblTotalRows" runat="server" class="Field" Text=""></asp:Label>
        </td>
    </tr>
</table>--%>
<table style="width: 75%;">
    <tr>
        <td>
            <asp:GridView ID="gvDematDetails" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                DataKeyNames="CEDA_DematAccountId" CssClass="GridViewStyle" OnPageIndexChanging="gvDematDetails_PageIndexChanging"
                OnSorting="gvDematDetails_Sorting">
                <RowStyle CssClass="RowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChanged"
                                CssClass="GridViewCmbField">
                                <asp:ListItem Text="Select"></asp:ListItem>
                                <asp:ListItem Text="View"></asp:ListItem>
                                <asp:ListItem Text="Edit"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="DP Name" DataField="CEDA_DPName" />
                    <asp:BoundField HeaderText="DP Client Id" DataField="CEDA_DPClientId" />
                    <asp:BoundField HeaderText="DP Id" DataField="CEDA_DPId" />
                    <asp:BoundField HeaderText="Beneficiary Acct No" DataField="CEDA_BeneficiaryAccountNum" />
                    <asp:BoundField HeaderText="Mode of holding" DataField="XMOH_ModeOfHolding" />
                    <asp:BoundField HeaderText="Account Opening Date" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" DataField="CEDA_AccountOpeningDate" />
                </Columns>
                <FooterStyle CssClass="FooterStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
            </asp:GridView>
        </td>
    </tr>
    <tr id="dataPager" runat="server">
        <td align="center" colspan="6">
            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnRecordCount" runat="server" />
