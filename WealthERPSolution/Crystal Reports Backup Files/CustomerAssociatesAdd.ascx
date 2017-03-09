<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerAssociatesAdd.ascx.cs"
    Inherits="WealthERP.Customer.CustomerAssociatesAdd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<table width="100%">
    <tr>
        <td colspan="2">
            <asp:Label ID="lblCustomerAssociation" runat="server" Text="Customer Association"
                CssClass="HeaderTextBig"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="2">
        </td>
    </tr>
    <tr id="trRelationShip" runat="server">
        <td class="leftField" width="35%">
            <asp:Label ID="lblRelationship" runat="server" Text="Relationship:" CssClass="HeaderTextSmall"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlRelationship" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <%--   <tr>
        <td class="leftField" width="35%">
            <asp:Label ID="lblCustomerSearch" runat="server" Text="Find Associate:" CssClass="HeaderTextSmall"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtFindAssociate" CssClass="txtField" runat="server"></asp:TextBox>
            <asp:Button ID="btnFindAssociate" runat="server" Text="Find Associate" CssClass="Button"
                Width="150px" onclick="btnFindAssociate_Click" />
        </td>
    </tr>--%>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblMessage" class="Field" runat="server" CssClass="FieldName">No Records Found</asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False" DataKeyNames="CustomerId,AssociationId"
                AllowSorting="True" CssClass="GridViewStyle" Height="64px" HorizontalAlign="Center"
                AllowPaging="True" OnPageIndexChanging="gvCustomers_PageIndexChanging" BackImageUrl="~/CSS/Images/HeaderGlassBlack.jpg"
                OnSorting="gvCustomers_Sort" PageSize="15">
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <RowStyle CssClass="RowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkId" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Customer Name" HeaderText="Customer Name" SortExpression="C_FirstName" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <asp:Button ID="btnAssociate" runat="server" OnClick="btnAssociate_Click" Text="Associate"
                CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_RMBranchAssociation_btnAssociate');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_RMBranchAssociation_btnAssociate');" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" />
