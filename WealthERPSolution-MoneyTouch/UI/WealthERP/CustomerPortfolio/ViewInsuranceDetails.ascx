<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewInsuranceDetails.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.ViewInsuranceDetails" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<table class="TableBackground">
    <tr>
        <td colspan="2">
            <asp:Label ID="lblInsuranceHeader" class="HeaderTextBig" runat="server" Text="Insurance Portfolio"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trNoRecords" runat="server">
        <td class="rightField">
            <asp:Label ID="lblMsg" class="Error" runat="server" Text="No Records Found..!"></asp:Label>
        </td>
    </tr>
    <tr id="trPager" runat="server">
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:GridView ID="gvrLifeInsurance" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" DataKeyNames="InsuranceId" EnableViewState="true" CssClass="GridViewStyle"
                OnSorting="gvrLifeInsurance_Sorting" OnDataBound="gvrLifeInsurance_DataBound" ShowFooter="True">
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
                    <asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="Particulars" HeaderText="Particulars" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="Sum Assured" HeaderText="Sum Assured (Rs)" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Premium Amount" HeaderText="Premium Amount (Rs)" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Next Premium Date" HeaderText="Next Premium Date (dd/mm/yyyy)"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Maturity Value" HeaderText="Maturity Value (Rs)" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Maturity Date" HeaderText="Maturity Date (dd/mm/yyyy)"
                        ItemStyle-HorizontalAlign="Center">
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
