<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioCashSavingsView.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioCashSavingsView" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<script language="javascript" type="text/javascript">
    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this record?');

        if (bool) {
            document.getElementById("ctrl_PortfolioCashSavingsView_hdnMsgValue").value = 1;
            document.getElementById("ctrl_PortfolioCashSavingsView_hiddenassociation").click();
            return false;
        }
        else {
            document.getElementById("ctrl_PortfolioCashSavingsView_hdnMsgValue").value = 0;
            document.getElementById("ctrl_PortfolioCashSavingsView_hiddenassociation").click();
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

<table style="width: 100%;" class="TableBackground">
    <tr>
        <td class="HeaderCell" colspan="2">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="Cash and Savings Portfolio"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td >
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
       
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <%--<td colspan="2">
            <asp:Label ID="lblMsg" class="Error" runat="server" Text="No Records Found!"></asp:Label>
        </td>--%>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="gvCustomerCashSavings" runat="server" AutoGenerateColumns="False"
                CellPadding="4" CssClass="GridViewStyle" DataKeyNames="CashSavingsPortfolioId"
                AllowSorting="True" ShowFooter="True" OnRowEditing="gvCustomerCashSavings_RowEditing"
                OnPageIndexChanging="gvCustomerCashSavings_PageIndexChanging" OnSorting="gvCustomerCashSavings_Sorting">
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle CssClass="EditRowStyle" HorizontalAlign="Left" VerticalAlign="Top" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" CssClass="GridViewCmbField"
                                OnSelectedIndexChanged="ddlAction_OnSelectedIndexChange">
                                <asp:ListItem>Select </asp:ListItem>
                                <asp:ListItem Text="Edit" Value="Edit">Edit</asp:ListItem>
                                <asp:ListItem Text="View" Value="View">View</asp:ListItem>
                                <asp:ListItem Text="Delete" Value="Delete">Delete</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Instrument Category" HeaderText="Asset Category" ItemStyle-Wrap="false"
                        HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="AccountWithBank" HeaderText="Account With Bank" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="Deposit Amount" HeaderText="Deposit Amount - Balance/Loaned Amount(Rs)"
                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="InterestAmount" HeaderText="Interest Amount(Rs)" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-Wrap="false" />
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

<div id="DivPager" runat="server" style="display: none">
    <table style="width: 100%">
        <tr align="center">
            <td>
                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
</div>
<%--<table width="100%">
    <tr>
        <td class="SubmitCell">
            <asp:Button ID="btnSubmit" Text="Submit" CssClass="PCGButton" runat="server" OnClick="OnClick_Submit"
                onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_PriceList_btnSubmit');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_PriceList_btnSubmit');" />
        </td>
    </tr>
</table>--%>
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" Value="CCSNP_Name ASC" />

<asp:HiddenField ID="hdnMsgValue" runat="server" />
<asp:HiddenField ID="hdndeleteId" runat="server" />
<asp:Button ID="hiddenassociation" runat="server" OnClick="hiddenassociation_Click"
    BorderStyle="None" BackColor="Transparent" />