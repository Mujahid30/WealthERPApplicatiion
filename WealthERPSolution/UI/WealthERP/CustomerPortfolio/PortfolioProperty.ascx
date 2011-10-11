<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioProperty.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioProperty" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<script language="javascript" type="text/javascript">
    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this record?');

        if (bool) {
            document.getElementById("ctrl_PortfolioProperty_hdnMsgValue").value = 1;
            document.getElementById("ctrl_PortfolioProperty_hiddenassociation").click();
            return false;
        }
        else {
            document.getElementById("ctrl_PortfolioProperty_hdnMsgValue").value = 0;
            document.getElementById("ctrl_PortfolioProperty_hiddenassociation").click();
            return true;
        }
    }
</script>

<table width="100%">
    <tr>
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                Record has been deleted Successfully.
            </div>
        </td>
    </tr>
</table>

<table style="width: 100%;">
    <tr>
        <td colspan="4" class="HeaderCell">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Property Portfolio"></asp:Label>
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
    <tr>
       <%-- <td colspan="3">
            <asp:Label ID="lblMsg" runat="server" CssClass="Error" Text="No Records Found...!"></asp:Label>
        </td>--%>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:GridView ID="gvrProperty" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" DataKeyNames="PropertyId" EnableViewState="true" AllowPaging="True"
                OnSorting="gvrProperty_Sorting" CssClass="GridViewStyle" OnDataBound="gvrProperty_DataBound"
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
                            <asp:DropDownList ID="ddlMenu" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged">
                                <asp:ListItem>Select </asp:ListItem>
                                <asp:ListItem Text="View" Value="View">View</asp:ListItem>
                                <asp:ListItem Text="Edit" Value="Edit">Edit</asp:ListItem>
                                <asp:ListItem Text="Delete" Value="Delete">Delete</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Sub Category" HeaderText="Asset Category" ItemStyle-HorizontalAlign="Justify">
                        <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Particulars" HeaderText="Particulars" ItemStyle-HorizontalAlign="Justify">
                        <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="City" HeaderText="City" ItemStyle-HorizontalAlign="Justify">
                        <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Area" HeaderText="Area" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Measurement Unit" HeaderText="Measurement Unit" ItemStyle-HorizontalAlign="Justify">
                        <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Purchase Date" HeaderText="Purchase Date(dd/mm/yyyy)"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Purchase Cost" HeaderText="Purchase Cost (Rs)" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Current Value" HeaderText="Current Value (Rs)" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server" visible="false">
    <tr>
    <td align="center">
     <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
    </div>
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
<asp:HiddenField ID="hdnSort" runat="server" Value="SubCategory ASC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />

<asp:HiddenField ID="hdnMsgValue" runat="server" />
<asp:HiddenField ID="hdndeleteId" runat="server" />
<asp:Button ID="hiddenassociation" runat="server" OnClick="hiddenassociation_Click"
    BorderStyle="None" BackColor="Transparent" />