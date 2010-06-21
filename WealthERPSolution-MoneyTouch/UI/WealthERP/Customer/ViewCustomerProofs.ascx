<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewCustomerProofs.ascx.cs"
    Inherits="WealthERP.Customer.ViewCustomerProofs" %>
    <%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>


<script language="javascript" type="text/javascript">
    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this record?');
        if (bool) {
            document.getElementById("ctrl_ViewCustomerProofs_hdnMsgValue").value = 1;
            document.getElementById("ctrl_ViewCustomerProofs_hiddenDelete").click();
            return false;
        }
        else {
            document.getElementById("ctrl_ViewCustomerProofs_hdnMsgValue").value = 0;
            document.getElementById("ctrl_ViewCustomerProofs_hiddenDelete").click();
            return true;
        }
    }
   
</script>

<table class="TableBackground" width="100%">
 <tr>
        <td colspan="2">
            <asp:Label ID="Label1" runat="server" Text="Proof" CssClass="HeaderTextBig"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="No Records Found..!" CssClass="Error"></asp:Label>
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
            <asp:GridView ID="gvCustomerProofs" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ProofCode"
                CssClass="GridViewStyle" AllowSorting="True" ShowFooter="true">
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkBx" runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="btnDelete" runat="server" CssClass="ButtonField" OnClick="btnDelete_Click"
                                Text="Delete" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Proof Name" HeaderText="Proof Name" />
                    <asp:BoundField DataField="Proof Category" HeaderText="Proof Category" />
                </Columns>
                <AlternatingRowStyle CssClass="AltRowStyle" />
            </asp:GridView>
        </td>
    </tr>
     <tr>
        <td>
            <asp:Button ID="hiddenDelete" runat="server" OnClick="hiddenDelete_Click" Text=""
                BorderStyle="None" BackColor="Transparent" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:HiddenField ID="hdnMsgValue" runat="server" />
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
