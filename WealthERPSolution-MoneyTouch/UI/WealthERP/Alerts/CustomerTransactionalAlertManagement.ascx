<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerTransactionalAlertManagement.ascx.cs"
    Inherits="WealthERP.Alerts.CustomerTransactionalAlertManagement" %>
    <%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<table style="width: 100%;" cssclass="TableBackground">
    <tr>
        <td align="center" class="rightField">
            <asp:Label ID="lblHeader" runat="server" Text="Confirmation Setup" CssClass="HeaderTextBig"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td class="rightField">
            <asp:Label ID="lblCustomerName" runat="server" CssClass="HeaderTextSmall"></asp:Label>
        </td>
    </tr>
</table>
<br />
<table style="width: 100%;" cssclass="TableBackground">
    <tr>
        <td colspan="2">
            <asp:Button ID="btnAddNewTranxEvent" runat="server" Text="Add New" OnClick="btnAddNewTranxEvent_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <asp:Label ID="lblMessage" runat="server" CssClass="HeaderTextSmall">You have not Subscribed for any Events. Please click on &#39;Add New&#39; to Subscribe.</asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <asp:GridView ID="gvTransactionalAlerts" runat="server" AutoGenerateColumns="False"
                CellPadding="4" CssClass="GridViewStyle" DataKeyNames="EventSetupID" AllowSorting="True"
                Font-Size="Small" BackImageUrl="~/CSS/Images/PCGGridViewHeaderGlass2.jpg" ShowFooter="True"
                EnableViewState="true">
                <%--OnRowCommand="gvTransactionalAlerts_RowCommand"--%>
                <FooterStyle CssClass="FooterStyle" HorizontalAlign="Center" />
                <RowStyle CssClass="RowStyle" HorizontalAlign="Center" />
                <SelectedRowStyle CssClass="SelectedRowStyle" HorizontalAlign="Center" />
                <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle Font-Bold="True" ForeColor="White" Font-Size="Small" HorizontalAlign="Center"
                    CssClass="HeaderStyle" />
                <AlternatingRowStyle BackColor="White" Font-Size="Small" HorizontalAlign="Center" />
                <Columns>
                    <%--Check Boxes--%>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkBx" runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="btnDeleteSelected" CssClass="FieldName" runat="server" Text="Delete"
                                OnClick="btnDeleteSelected_Click" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <%--Details--%>
                    <asp:BoundField DataField="Details" HeaderText="Details" />
                    <%--Scheme--%>
                    <%--<asp:BoundField DataField="Scheme" HeaderText="Scheme" SortExpression="Scheme" />--%>
                    <%--Message--%>
                    <asp:BoundField DataField="Message" HeaderText="Message" />
                </Columns>
            </asp:GridView>
            <br />
        </td>
    </tr>
</table>
<table style="width: 100%" id="tblPager" runat="server" visible="false">
    <tr>
        <td>
            <pager:pager id="mypager" runat="server"></pager:pager>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td colspan="2">
        </td>
    </tr>
    <tr id="trAddNewEvent" runat="server">
        <td class="rightField" colspan="2">
            <label id="lblAddEvent" class="HeaderTextSmall">
                Add New Event</label>
            <hr />
        </td>
    </tr>
    <tr id="trAddNewTranxAlt" runat="server" visible="false">
        <td class="rightField" width="20%">
            <asp:Label ID="lblSchemeEntry" runat="server" CssClass="FieldName" Text="Select Scheme:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="2">
        </td>
    </tr>
    <tr id="trTranxSave" runat="server" visible="false">
        <td class="leftField" width="10%">
            <asp:Button ID="btnSaveTranxAlert" runat="server" CssClass="ButtonField" Text="Subscribe"
                OnClick="btnSaveTranxAlert_Click" />
        </td>
        <td align="center">
            &nbsp;
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnSort" runat="server" Value="SchemeName ASC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />