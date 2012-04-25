<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMDashBoard.ascx.cs"
    Inherits="WealthERP.Advisor.RMDashBoard" %>
<table style="width: 100%;" class="TableBackground">
    <tr>
        <td colspan="2">
            <%--<asp:Label ID="lblNewMessages" runat="server" CssClass="HeaderTextSmall" Visible="false" />--%>
            <asp:LinkButton ID="lnkBtnNewMessages" CssClass="LinkButtonsWithoutUnderLine" Visible="false"
            runat="server" onclick="lnkBtnNewMessages_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td style="width: 50%" valign="top">
            <table style="width: 100%">
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblMyAum" runat="server" CssClass="HeaderTextSmall" Text="My AUM"></asp:Label>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="rightField" style="width: 20%">
                        <asp:Label ID="lblEquityLabel" runat="server" CssClass="FieldName" Text="Equity:"></asp:Label>
                    </td>
                    <td class="leftField" style="width: 30%">
                        <asp:Label ID="lblEquityValue" runat="server" CssClass="Field" Text="0"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td class="rightField">
                        <asp:Label ID="lblEquityMFLabel" runat="server" CssClass="FieldName" Text="MF-Equity:"></asp:Label>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblEquityMFValue" runat="server" CssClass="Field" Text="0"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td class="rightField">
                        <asp:Label ID="lblMFDebtLabel" runat="server" CssClass="FieldName" Text="MF-Debt:"></asp:Label>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblMFDebtValue" runat="server" CssClass="Field" Text="0"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td class="rightField">
                        <asp:Label ID="lblMFHybridLabel" runat="server" CssClass="FieldName" Text="MF-Hybrid:"></asp:Label>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblMFHybridValue" runat="server" CssClass="Field" Text="0"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td class="rightField">
                        <asp:Label ID="lblMFCommodityLabel" runat="server" CssClass="FieldName" Text="MF-Commodity:"></asp:Label>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblMFCommodityValue" runat="server" CssClass="Field" Text="0"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td class="rightField">
                        <asp:Label ID="lblMFOthers" runat="server" CssClass="FieldName" Text="MF-Others:"></asp:Label>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblMFOthersValue" runat="server" CssClass="Field" Text="0"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td class="rightField">
                        <asp:Label ID="lblInsurancelabel" runat="server" CssClass="FieldName" Text="Insurance:"></asp:Label>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblInsuranceValue" runat="server" CssClass="Field" Text="0"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td class="rightField">
                        <asp:Label ID="lblTotalLabel" runat="server" CssClass="FieldName" Text="Total:"></asp:Label>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblTotalValue" runat="server" CssClass="Field" Text="Total"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
        <td valign="top">
            <table style="width: 100%">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblMyAlerts" runat="server" CssClass="HeaderTextSmall" Text="Customer Alerts"></asp:Label>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblAlertsMessage" runat="server" CssClass="HeaderTextSmall" Text="No Alerts..."></asp:Label>
                        <asp:GridView ID="gvCustomerAlerts" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CellPadding="4" CssClass="GridViewStyle" ShowFooter="true" Width="100%" >
                            <FooterStyle CssClass="FooterStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="HeaderStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                            <Columns>
                                <asp:BoundField DataField="Customer" HeaderText="Customer" />
                                <asp:BoundField DataField="Details" HeaderText="Details" />
                                <asp:BoundField DataField="EventMessage" HeaderText="EventMessage" />
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblDisclaimer" runat="server" CssClass="FieldName" Text="Note: Only Five most recent Alerts will be visible on the dashboard.Please click here to view all Notifications"
                            Visible="true"></asp:Label>
                        <asp:LinkButton ID="lnkAlertNotifications" Text=" -->More" runat="server" OnClick="lnkAlertNotifications_Click"
                            CssClass="LinkButtons"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <table style="width: 100%">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblClientList" runat="server" CssClass="HeaderTextSmall" Text="Top 5 Customers by AUM"></asp:Label>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvrRMClinetList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            ShowFooter="true" CellPadding="4" DataKeyNames="CustomerId" EnableViewState="false"
                            EmptyDataText="There are no records" Width="90%" CssClass="GridViewStyle" PageSize="5">
                            <RowStyle CssClass="RowStyle" />
                            <FooterStyle CssClass="FooterStyle" />
                            <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            <HeaderStyle CssClass="HeaderStyle" />
                            <EditRowStyle CssClass="EditRowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                            <Columns>
                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkCustomerName" runat="server" CssClass="CmbField" OnClick="lnkCustomerNameClientListGrid_Click"
                                            Text='<%# Eval("Customer_Name") %>'>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EQCurrentVal" HeaderText="Equity Cur Value" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n2}" HtmlEncode="false" />
                                <asp:BoundField DataField="MFCurrentVal" HeaderText="MF Cur Value" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n2}" HtmlEncode="false" />
                                <asp:BoundField DataField="Total" HeaderText="Total" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n2}" HtmlEncode="false" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </td>
        <td valign="top">
            <table style="width: 80%" id="tblLoanCount" runat="server">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" CssClass="HeaderTextSmall" Text="Loan Details"></asp:Label>
                        <hr />
                    </td>
                </tr>
                <%-- <tr>
                    <td class="leftField">
                        <asp:Label ID="Label4" runat="server" CssClass="HeaderText" Text="Number of Loan Proposals : "></asp:Label>
                    </td>
                </tr>--%>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Pending with Us : "></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:Label ID="lblPendingUs" runat="server" CssClass="Field" Text=" "></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td td class="leftField">
                        <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Pending with Bank : "></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:Label ID="lblPendingBank" runat="server" CssClass="Field" Text=" "></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
