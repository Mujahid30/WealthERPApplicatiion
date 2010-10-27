<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerMFAlert.ascx.cs"
    Inherits="WealthERP.Alerts.CustomerMFAlert" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<div>
    <table style="width: 100%;" cssclass="TableBackground">
        <tr>
            <td colspan="2" class="rightField">
                <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Mutual Fund Alerts">
                </asp:Label>
                <hr />
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
                <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlMFAlertsGrid" runat="server">
        <table style="width: 100%;" id="tblMFAlertGrid" runat="server" cssclass="TableBackground">
            <tr>
                <td>
                    <asp:Label ID="lblMessage" Text="There are no MF Net Position Records" class="Error"
                        runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDisclaimer" runat="server" CssClass="FieldName" Text="Note: To edit alert setup, please delete alert subscription and re-subscribe"
                        Visible="true" ForeColor="Red"></asp:Label>
                    <br /><br />
                    <asp:GridView ID="gvMFAlerts" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        CssClass="GridViewStyle" DataKeyNames="SchemeId,AccountId" AllowSorting="True"
                        Font-Size="Small" HorizontalAlign="Center" ShowFooter="True" OnRowDataBound="gvMFAlerts_RowDataBound"
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
                            <%--Scheme Name--%>
                            <asp:BoundField DataField="Scheme" HeaderText="Scheme Name" ItemStyle-HorizontalAlign="Left" />
                            <%--SIP Reminder Column--%>
                            <asp:TemplateField HeaderText="SIP Reminder">
                                <ItemTemplate>
                                    <asp:Button ID="btnSIPReminder" runat="server" Text="Setup" OnClick="btnSIPReminder_Click" />
                                    <asp:Label ID="lblSIPReminder" runat="server" Text='<%# Eval("SIPReminder").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--SIP Confirmation Column--%>
                            <asp:TemplateField HeaderText="SIP Confirmation">
                                <ItemTemplate>
                                    <asp:Button ID="btnSIPConfirmation" runat="server" Text="Setup" OnClick="btnSIPConfirmation_Click" />
                                    <asp:Label ID="lblSIPConfirmation" runat="server" Text='<%# Eval("SIPConfirmation").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--MF Absolute Stop Loss Column--%>
                            <asp:TemplateField HeaderText="Absolute Stop Loss">
                                <ItemTemplate>
                                    <asp:Button ID="btnMFAbsoluteStopLoss" runat="server" Text="Setup" OnClick="btnMFAbsoluteStopLoss_Click" />
                                    <asp:Label ID="lblMFAbsoluteStopLoss" runat="server" Text='<%# Eval("MFAbsoluteStopLoss").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--SIP Absolute Profit Booking Column--%>
                            <asp:TemplateField HeaderText="Absolute Profit Booking">
                                <ItemTemplate>
                                    <asp:Button ID="btnMFAbsoluteProfitBooking" runat="server" Text="Setup" OnClick="btnMFAbsoluteProfitBooking_Click" />
                                    <asp:Label ID="lblMFAbsoluteProfitBooking" runat="server" Text='<%# Eval("MFAbsoluteProfitBooking").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--SWP Reminder Column--%>
                            <asp:TemplateField HeaderText="SWP Reminder">
                                <ItemTemplate>
                                    <asp:Button ID="btnSWPReminder" runat="server" Text="Setup" OnClick="btnSWPReminder_Click" />
                                    <asp:Label ID="lblSWPReminder" runat="server" Text='<%# Eval("SWPReminder").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--SWP Confirmation Column--%>
                            <asp:TemplateField HeaderText="SWP Confirmation">
                                <ItemTemplate>
                                    <asp:Button ID="btnSWPConfirmation" runat="server" Text="Setup" OnClick="btnSWPConfirmation_Click" />
                                    <asp:Label ID="lblSWPConfirmation" runat="server" Text='<%# Eval("SWPConfirmation").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--ELSS Maturity Column--%>
                            <asp:TemplateField HeaderText="ELSS Maturity">
                                <ItemTemplate>
                                    <asp:Button ID="btnELSSMaturity" runat="server" Text="Setup" OnClick="btnELSSMaturity_Click" />
                                    <asp:Label ID="lblELSSMaturity" runat="server" Text='<%# Eval("ELSSMaturity").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--Dividend Transaction Column--%>
                            <asp:TemplateField HeaderText="Dividend Transactions Occurrence">
                                <ItemTemplate>
                                    <asp:Button ID="btnDivTranxOccur" runat="server" Text="Setup" OnClick="btnDivTranxOccur_Click" />
                                    <asp:Label ID="lblDivTranxOccur" runat="server" Text='<%# Eval("DivTranxOccur").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td class="SubmitCell">
                    <%--<asp:Button ID="btnSubmit" runat="server" Text="Apply Alert" CssClass="PCGLongButton"
                        onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AlertDashboard_btnSubmit', 'S');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AlertDashboard_btnSubmit', 'S');"
                        OnClick="btnSubmit_Click" />--%>
                </td>
            </tr>
        </table>
        <table style="width: 100%" id="tblPager" runat="server" visible="false">
            <tr>
                <td>
                    <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
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
                        onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerMFAlert_btnSubmitReminder', 'S');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerMFAlert_btnSubmitReminder', 'S');"
                        OnClick="btnSubmitReminder_Click" />
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
                        onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerMFAlert_btnSubmitOccurrence', 'S');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerMFAlert_btnSubmitOccurrence', 'S');"
                        OnClick="btnSubmitOccurrence_Click" />
                </td>
                <td class="rightField">
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdnSort" runat="server" Value="SchemeName ASC" />
        <asp:HiddenField ID="hdnRecordCount" runat="server" />
    </asp:Panel>
</div>
