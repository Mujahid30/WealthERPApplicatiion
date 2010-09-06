<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerEQAlerts.ascx.cs"
    Inherits="WealthERP.Alerts.CustomerEQAlerts" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<div>
    <table style="width: 100%;" cssclass="TableBackground">
        <tr>
            <td colspan="2" class="rightField">
                <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Equity Alerts">
                </asp:Label>
                <hr />
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlEQAlertsGrid" runat="server">
        <table style="width: 100%;" id="tblEQAlertGrid" runat="server" cssclass="TableBackground">
            <tr>
                <td>
                    <asp:Label ID="lblMessage" Text="There are no Equity Net Position Records" class="Error"
                        runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvEQAlerts" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        CssClass="GridViewStyle" DataKeyNames="ScripId,AccountId" AllowSorting="True"
                        Font-Size="Small" HorizontalAlign="Center"
                        ShowFooter="True" OnRowDataBound="gvEQAlerts_RowDataBound" EnableViewState="true"
                        PageSize="30">
                        <%--OnRowDataBound="gvAlertDashboard_RowDataBound"--%>
                        <FooterStyle CssClass="FooterStyle" />
                        <RowStyle CssClass="RowStyle" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" CssClass="SelectedRowStyle"
                            HorizontalAlign="Center" />
                        <PagerStyle ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkId" runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btnDeleteSetup" runat="server" Text="Delete" CssClass="PCGButton"
                                        OnClick="btnDeleteSetup_Click" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <%--Scrip--%>
                            <asp:BoundField DataField="Scrip" HeaderText="Scrip" />
                            <%--Absolute Stop Loss Column--%>
                            <asp:TemplateField HeaderText="Absolute Stop Loss">
                                <ItemTemplate>
                                    <asp:Button ID="btnAbsoluteStopLoss" runat="server" Text="Setup" OnClick="btnAbsoluteStopLoss_Click" />
                                    <asp:Label ID="lblAbsoluteStopLoss" runat="server" Text='<%# Eval("AbsoluteStopLoss").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--Absolute Profit Booking Column--%>
                            <asp:TemplateField HeaderText="Absolute Profit Booking">
                                <ItemTemplate>
                                    <asp:Button ID="btnAbsoluteProfitBooking" runat="server" Text="Setup" OnClick="btnAbsoluteProfitBooking_Click" />
                                    <asp:Label ID="lblAbsoluteProfitBooking" runat="server" Text='<%# Eval("AbsoluteProfitBooking").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <table id="tblOccurrenceEdit" width="100%" runat="server">
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblOccurrenceEdit" CssClass="HeaderTextSmall" Text="Occurrence Alert Setup"
                        runat="server"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblCondition" CssClass="FieldName" Text="Preset Condition" runat="server"></asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:Label ID="lblPreset" CssClass="FieldName" Text="Preset" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="leftField" width="15%">
                    &nbsp;
                </td>
                <td>
                </td>
                <td class="rightField">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="leftField" width="15%">
                    <asp:DropDownList ID="ddlOccurrenceCondition" CssClass="cmbField" runat="server">
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:Label ID="lblBy" CssClass="Field" Text="By" runat="server"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtOccurrencePreset" CssClass="txtField" runat="server"></asp:TextBox>
                    <asp:Label ID="lblPercent" CssClass="Field" Text="%" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="leftField" width="15%">
                    &nbsp;
                </td>
                <td align="center" width="15%">
                    <asp:Button ID="btnSubmitOccurrence" runat="server" Text="Submit" CssClass="PCGButton"
                        onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerEQAlerts_btnSubmitOccurrence', 'S');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerEQAlerts_btnSubmitOccurrence', 'S');"
                        OnClick="btnSubmitOccurrence_Click" />
                </td>
                <td class="rightField">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
