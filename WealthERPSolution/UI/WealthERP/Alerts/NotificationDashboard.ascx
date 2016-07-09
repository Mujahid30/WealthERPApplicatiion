<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NotificationDashboard.ascx.cs"
    Inherits="WealthERP.Alerts.NotificationDashboard" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<asp:Panel ID="pnlHeader" runat="server" Visible="true">
    <table style="width: 100%;" cssclass="TableBackground">
        <tr>
            <td align="center">
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
            <td align="center">
                <asp:Label ID="lblCustomerName" runat="server" CssClass="HeaderTextSmall"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr id="pnlNoEntries" runat="server" visible="false">
            <td align="center">
                <label id="lblNoEntries" class="FieldName">
                    You have no Notifications!</label>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblCurrentPage" Visible="false" class="Field" runat="server"></asp:Label>
                <asp:Label ID="lblTotalRows" Visible="false" class="Field" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <%-- <asp:UpdatePanel ID="upPnlDateGrid" runat="server">
                    <ContentTemplate>--%>
                <asp:GridView ID="gvNotification" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Visible="false" CssClass="GridViewStyle" DataKeyNames="EventQueueID" AllowSorting="false"
                    Font-Size="Small" HorizontalAlign="Center" ShowFooter="True" EnableViewState="true">
                    <%-- OnRowEditing="gvDateAlerts_RowEditing" OnRowCommand="gvDateAlerts_RowCommand" OnRowUpdating="gvDateAlerts_RowUpdating" OnRowCancelingEdit="gvDateAlerts_RowCancelingEdit"
                    OnRowDataBound="gvDateAlerts_RowDataBound" --%>
                    <FooterStyle Font-Bold="True" ForeColor="White" />
                    <RowStyle Font-Size="Small" CssClass="RowStyle" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" CssClass="SelectedRowStyle" />
                    <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="PagerStyle" />
                    <HeaderStyle Font-Bold="True" ForeColor="White" Font-Size="Small" HorizontalAlign="Center" />
                    <AlternatingRowStyle BackColor="White" Font-Size="Small" />
                    <Columns>
                        <%--Check Boxes--%>
                        <%--<asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkBx" runat="server" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="btnDeleteSelected" CssClass="FieldName" OnClick="btnDeleteSelected_Click"
                                    runat="server" Text="Delete" />
                            </FooterTemplate>
                        </asp:TemplateField>--%>
                        <asp:BoundField DataField="Category" HeaderText="Category" />
                        <asp:BoundField DataField="Alert" HeaderText="Sub Category" />
                        <asp:BoundField DataField="Scheme" HeaderText="Detail" />
                        <asp:BoundField DataField="Message" HeaderText="Message" />
                        <asp:BoundField DataField="NotifiedDate" HeaderText="Notified Date" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </asp:GridView>
                <telerik:RadGrid ID="gvCustomerAlerts" runat="server" AllowAutomaticDeletes="false" Visible="false"
                    PageSize="20" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                    ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true"
                    OnNeedDataSource="gvCustomerAlerts_OnNeedDataSource" GridLines="none" AllowAutomaticInserts="false"
                    Skin="Telerik" EnableHeaderContextMenu="true" Width="95%" >
                    <MasterTableView Width="100%" DataKeyNames="ID" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                        CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                        GroupLoadMode="Client" ShowGroupFooter="true">
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="Message" DataField="DashBoardCustomerNotification" UniqueName="DashBoardCustomerNotification" SortExpression="DashBoardCustomerNotification"
                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Notified Date" DataField="CTNDE_CreatedOn" UniqueName="CTNDE_CreatedOn" SortExpression="CTNDE_CreatedOn"
                                AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                    </ClientSettings>
                </telerik:RadGrid>
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
                    <Pager:Pager ID="mypager" runat="server" Visible="false"></Pager:Pager>
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
<asp:HiddenField ID="hdnSort" runat="server" Value="NotifiedDate Desc" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
