<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewBranchAssociation.ascx.cs"
    Inherits="WealthERP.Advisor.ViewBranchAssociation" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>


<script language="javascript" type="text/javascript">
    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this record?');
        if (bool) {
            document.getElementById("ctrl_ViewBranchAssociation_hdnMsgValue").value = 1;
            document.getElementById("ctrl_ViewBranchAssociation_hiddenDelete").click();
            return false;
        }
        else {
            document.getElementById("ctrl_ViewBranchAssociation_hdnMsgValue").value = 0;
            document.getElementById("ctrl_ViewBranchAssociation_hiddenDelete").click();
            return true;
        }
    }
   
</script>
<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Branch Association"></asp:Label>
            <hr />
        </td>
    </tr>
</table>


<table style="width: 100%;" class="TableBackground">
    <%--<tr>
        <td class="HeaderCell">
            <asp:Label ID="Label1" runat="server" Text="RM Branch Association" CssClass="HeaderTextBig"></asp:Label>
        </td>
    </tr>--%>
    <tr>
        <td>
            &nbsp;
            <asp:Label ID="lblMsg" runat="server" CssClass="Error" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="margin-left: 80px">
            <asp:GridView ID="gvBranchList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="GridViewStyle" DataKeyNames="BranchId,RMId" ShowFooter="true">
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle CssClass="PagerStyle " />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <RowStyle CssClass="RowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkBx" runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="btnDeleteSelected" CssClass="FieldName" OnClick="btnDeleteSelected_Click"
                                runat="server" Text="Delete" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblBranchNameHeader" runat="server" Text="Branch Name"></asp:Label>
                            <asp:DropDownList ID="ddlBranchName" AutoPostBack="true" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlBranchName_SelectedIndexChanged">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("Branch Name").ToString() %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblRMNameHeader" runat="server" Text="RM Name"></asp:Label>
                            <asp:DropDownList ID="ddlRMName" AutoPostBack="true" runat="server" CssClass="cmbField"  OnSelectedIndexChanged="ddlRMName_SelectedIndexChanged">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRMName" runat="server" Text='<%# Eval("RM Name").ToString() %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="Branch Name" HeaderText="Branch Name" SortExpression="Branch Name" />
                    <asp:BoundField DataField="RM Name" HeaderText="RM Name" SortExpression="RM Name" />--%>
                    <%--<asp:TemplateField>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlMenu" AutoPostBack="true" runat="server" 
                                onselectedindexchanged="ddlMenu_SelectedIndexChanged">
                                <asp:ListItem>Select </asp:ListItem>
                                <asp:ListItem Text="View Association" Value="View Association">View Association  </asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <table id="tblPager" width="100%" runat="server">
        <tr id="trPager" runat="server">
            <td align="center">
                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
    <tr>
        <td style="margin-left: 80px">
            <asp:HiddenField ID="hdnMsgValue" runat="server" />
            <asp:Button ID="hiddenDelete" runat="server" OnClick="hiddenDelete_Click" Text=""
                BorderStyle="None" BackColor="Transparent" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" Value="BranchName ASC"/>
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnBranchFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRMFilter" runat="server" Visible="false" />
