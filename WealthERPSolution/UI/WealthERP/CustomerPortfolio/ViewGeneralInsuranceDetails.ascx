<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewGeneralInsuranceDetails.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.ViewGeneralInsuranceDetails" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<table id="Table1" class="TableBackground" width="100%" runat="server">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblInsuranceHeader" class="HeaderTextBig" runat="server" Text="General Insurance Portfolio"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table id="ErrorMessage" align="center" runat="server">
    <tr>
        <td>
            <div class="failure-msg" align="center">
                No Records found.....
            </div>
        </td>
    </tr>
</table>

<table class="TableBackground" width="100%">
    <tr id="trPager" runat="server">
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:GridView ID="gvGeneralInsurance" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" DataKeyNames="InsuranceId" EnableViewState="true" CssClass="GridViewStyle" AllowPaging="true"
                ShowFooter="True" OnPageIndexChanging="gvGeneralInsurance_PageIndexChanging" PageSize="20">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" Wrap="false" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlMenu" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged">
                                <asp:ListItem>Select </asp:ListItem>
                                <asp:ListItem Text="Edit" Value="Edit">Edit</asp:ListItem>
                                <asp:ListItem Text="View" Value="View">View</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="InsCompany" HeaderText="Ins Company" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="SubCategory" HeaderText="Sub Category" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="InsuredAmount" HeaderText="Insured Amount" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PremiumAmount" HeaderText="Premium Amount" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="CommencementDate" HeaderText="Commencement Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MaturityDate" HeaderText="Maturity Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
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
<asp:HiddenField ID="hdnSort" runat="server" Value="Particulars ASC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
