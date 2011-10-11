<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewGoldPortfolio.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.ViewGoldPortfolio" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>


<script language="javascript" type="text/javascript">
    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this record?');

        if (bool) {
            document.getElementById("ctrl_ViewGoldPortfolio_hdnMsgValue").value = 1;
            document.getElementById("ctrl_ViewGoldPortfolio_hiddenassociation").click();
            return false;
        }
        else {
            document.getElementById("ctrl_ViewGoldPortfolio_hdnMsgValue").value = 0;
            document.getElementById("ctrl_ViewGoldPortfolio_hiddenassociation").click();
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

<table class="TableBackground" style="width: 100%">
    <tr>
        <td colspan="4" class="HeaderCell">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="Gold"></asp:Label>
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
        <td>
            <%--<asp:Label ID="lblMessage" runat="server" CssClass="Error" Text="No Records found..!"></asp:Label>--%>
            
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
            <asp:GridView ID="gvGoldPortfolio" runat="server" AutoGenerateColumns="False" CellPadding="4"
                CssClass="GridViewStyle" DataKeyNames="GoldNPId" OnSelectedIndexChanged="gvGoldPortfolio_SelectedIndexChanged"
                AllowSorting="True" AllowPaging="True" OnPageIndexChanging="gvGoldPortfolio_PageIndexChanging"
                OnSorting="gvGoldPortfolio_Sorting" ShowFooter="True">
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" CssClass="GridViewCmbField" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChange">
                                <asp:ListItem Text="Select" />
                                <asp:ListItem Text="View" Value="View">View</asp:ListItem>
                                <asp:ListItem Text="Edit" Value="Edit">Edit</asp:ListItem>
                                <asp:ListItem Text="Delete" Value="Delete">Delete</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Instrument Category" HeaderText="Instrument Category" />
                    <asp:BoundField DataField="Purchase Date" HeaderText="Purchase Date (dd/mm/yyyy)"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Purchase Value" HeaderText="Purchase Value (Rs)" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="Current Value" HeaderText="Current Value (Rs)" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
<div id="DivPager" runat="server" style="display: none">
    <table style="width: 100%">
        <tr>
            <td>
                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
</div>
<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server" visible="false">
    <tr>
    <td align="center">
     <div class="failure-msg" id="ErrorMessage" visible="false" runat="server"  align="center">
    </div>
    </td>
    </tr>
 </table>
<tr>
<asp:Button ID="btnUpdateNP" runat="server" Text="Update Current Value" OnClick="btn_Update" CssClass="PCGLongButton" AutoPostBack="true" />
</tr>

<asp:HiddenField ID="hdnSort" runat="server" Value="InstrumentCategory ASC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />



<asp:HiddenField ID="hdnMsgValue" runat="server" />
<asp:HiddenField ID="hdndeleteId" runat="server" />
<asp:Button ID="hiddenassociation" runat="server" OnClick="hiddenassociation_Click"
    BorderStyle="None" BackColor="Transparent" />