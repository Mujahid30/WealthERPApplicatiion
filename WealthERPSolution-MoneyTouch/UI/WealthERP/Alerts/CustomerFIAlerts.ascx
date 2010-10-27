<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerFIAlerts.ascx.cs"
    Inherits="WealthERP.Alerts.CustomerFIAlerts" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<div>
    <table style="width: 100%;" cssclass="TableBackground">
        <tr>
            <td colspan="2" class="rightField">
                <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Fixed Income Alerts">
                </asp:Label>
                <hr />
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlFIAlertsGrid" runat="server">
        <table style="width: 100%;" id="tblFIAlertGrid" runat="server" cssclass="TableBackground">
            <tr>
                <td>
                    <asp:Label ID="lblMessage" Text="There are no Fixed Income Records" class="Error"
                        runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDisclaimer" runat="server" CssClass="FieldName" Text="Note: To edit alert setup, please delete alert subscription and re-subscribe"
                        Visible="true" ForeColor="Red"></asp:Label>
                    <br />
                    <br />
                    <asp:GridView ID="gvFIAlerts" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        CssClass="GridViewStyle" DataKeyNames="FINPId,AccountId" AllowSorting="True"
                        Font-Size="Small" HorizontalAlign="Center" ShowFooter="True" OnRowDataBound="gvFIAlerts_RowDataBound"
                        EnableViewState="true" PageSize="30">
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
                            <%--Fixed Income Investment--%>
                            <asp:BoundField DataField="FIInvestment" HeaderText="Fixed Income Investment" ItemStyle-HorizontalAlign="Left" />
                            <%--Reccurring Deposit Reminder Column--%>
                            <asp:TemplateField HeaderText="Reccurring Deposit Reminder">
                                <ItemTemplate>
                                    <asp:Button ID="btnReccurringDepositReminder" runat="server" Text="Setup" OnClick="btnReccurringDepositReminder_Click" />
                                    <asp:Label ID="lblReccurringDepositReminder" runat="server" Text='<%# Eval("ReccurringDepositReminder").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--Fixed Deposit Maturity Column--%>
                            <asp:TemplateField HeaderText="Fixed Deposit Maturity Reminder">
                                <ItemTemplate>
                                    <asp:Button ID="btnFDMaturityReminder" runat="server" Text="Setup" OnClick="btnFDMaturityReminder_Click" />
                                    <asp:Label ID="lblFDMaturityReminder" runat="server" Text='<%# Eval("FDMaturityReminder").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <table id="tblReminderEdit" width="100%" runat="server">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblReminderEdit" CssClass="HeaderTextSmall" Text="Reminder Alert Setup"
                        runat="server"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="leftField" width="15%">
                    <asp:Label ID="lblReminderDays" CssClass="FieldName" Text="Reminder:" runat="server"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtReminderDays" CssClass="txtField" runat="server"></asp:TextBox>
                    <asp:Label ID="lblDaysBefore" CssClass="Field" Text=" days before" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="leftField" width="15%">
                    &nbsp;
                </td>
                <td class="rightField">
                    <asp:Button ID="btnSubmitReminder" runat="server" Text="Submit" CssClass="PCGButton"
                        onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AlertDashboard_btnSubmit', 'S');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AlertDashboard_btnSubmit', 'S');"
                        OnClick="btnSubmitReminder_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
