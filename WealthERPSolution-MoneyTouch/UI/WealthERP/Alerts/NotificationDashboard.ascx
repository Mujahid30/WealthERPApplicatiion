<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NotificationDashboard.ascx.cs"
    Inherits="WealthERP.Alerts.NotificationDashboard" %>
    <%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<asp:Panel ID="pnlHeader" runat="server" Visible="true">
    <table style="width: 100%;" cssclass="TableBackground">
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lblNotificationHeader" runat="server" CssClass="HeaderTextBig" Text="Notifications">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlCustomerSelection" runat="server" Visible="true">
    <table style="width: 100%;" cssclass="TableBackground">
        <tr>
            <td align="center">
                <asp:Label ID="lblChooseCust" runat="server" CssClass="HeaderTextSmall" Text="Choose a Customer">
                </asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlRMCustList" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlRMCustList_SelectedIndexChanged"
                    AutoPostBack="true">
                    <%--onchange="javascript:ddlCustDrop_Change(this); return false;"--%>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlNotifications" runat="server" Visible="false">
    <table style="width: 100%;" cssclass="TableBackground">
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lblCustomerName" runat="server" CssClass="HeaderTextSmall"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr id="pnlNoEntries" runat="server" visible="false">
            <td colspan="2" align="center">
                <label id="lblNoEntries" class="FieldName">
                    You have no Notifications!</label>
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
                <%-- <asp:UpdatePanel ID="upPnlDateGrid" runat="server">
                    <ContentTemplate>--%>
                <asp:GridView ID="gvNotification" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    CssClass="GridViewStyle" DataKeyNames="EventQueueID" AllowSorting="True" Font-Size="Small"
                    HorizontalAlign="Center" BackImageUrl="~/CSS/Images/PCGGridViewHeaderGlass2.jpg"
                    ShowFooter="True" EnableViewState="true">
                    <%-- OnRowEditing="gvDateAlerts_RowEditing" OnRowCommand="gvDateAlerts_RowCommand" OnRowUpdating="gvDateAlerts_RowUpdating" OnRowCancelingEdit="gvDateAlerts_RowCancelingEdit"
                    OnRowDataBound="gvDateAlerts_RowDataBound" --%>
                    <FooterStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle Font-Size="Small" CssClass="RowStyle" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" CssClass="SelectedRowStyle" />
                    <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="PagerStyle" />
                    <HeaderStyle Font-Bold="True" ForeColor="White" Font-Size="Small" HorizontalAlign="Center" />
                    <AlternatingRowStyle BackColor="White" Font-Size="Small" HorizontalAlign="Center" />
                    <Columns>
                        <%--Check Boxes--%>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkBx" runat="server" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="btnDeleteSelected" CssClass="FieldName" OnClick="btnDeleteSelected_Click"
                                    runat="server" Text="Delete" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
                        <asp:BoundField DataField="Alert" HeaderText="Sub Category" SortExpression="SubCategory" />
                        <asp:BoundField DataField="Scheme" HeaderText="Detail" />
                        <asp:BoundField DataField="Message" HeaderText="Message" />
                        <asp:BoundField DataField="NotifiedDate" HeaderText="Notified Date" SortExpression="NotifiedDate" />
                    </Columns>
                </asp:GridView>
                <br />
                <%--<div align="center">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upPnlDateGrid"
                                DisplayAfter="100" DynamicLayout="true">
                                <ProgressTemplate>
                                    <img border="0" src="../Images/ajax_loader.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>--%>
                <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </td>
        </tr>
        <table style="width: 100%" id="tblPager" runat="server" visible="false">
            <tr>
                <td>
                    <pager:pager id="mypager" runat="server"></pager:pager>
                </td>
            </tr>
        </table>
        <tr>
            <td>
            </td>
        </tr>
        <tr id="trSelectCustomer" runat="server">
            <td colspan="2" align="center">
                <asp:Button ID="btnSelectCustomer" runat="server" Text="Select Customer" OnClick="btnSelectCustomer_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="hdnSort" runat="server" Value="NotificationDate ASC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />