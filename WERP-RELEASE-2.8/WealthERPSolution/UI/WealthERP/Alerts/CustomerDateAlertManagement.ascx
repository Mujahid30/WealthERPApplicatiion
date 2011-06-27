<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerDateAlertManagement.ascx.cs"
    Inherits="WealthERP.Alerts.CustomerAlertManagement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<table style="width: 100%;" cssclass="TableBackground">
    <tr>
        <td class="rightField">
            <asp:Label ID="lblHeader" runat="server" Text="Reminder Setup Screen" CssClass="HeaderTextBig"></asp:Label>
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
<asp:ScriptManager ID="scrptMgr" runat="server" />
<table style="width: 100%;" cssclass="TableBackground">
    <tr>
        <td colspan="2">
            &nbsp;<asp:Button ID="btnAddNewDateEvent" runat="server" Text="Add New" OnClick="btnAddNewDateEvent_Click" />
        </td>
    </tr>
    <tr>
        <td align="center" colspan="2">
            <asp:Label ID="lblMessage" runat="server" CssClass="HeaderTextSmall">You have not Subscribed for any Event. Click &#39;Add New&#39; to Subscribe.</asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <%-- <asp:UpdatePanel ID="upPnlDateGrid" runat="server">
                    <ContentTemplate>--%>
            <asp:GridView ID="gvDateAlerts" runat="server" AutoGenerateColumns="False" CellPadding="4"
                CssClass="GridViewStyle" DataKeyNames="EventSetupID" AllowSorting="True" Font-Size="Small"
                HorizontalAlign="Center" BackImageUrl="~/CSS/Images/PCGGridViewHeaderGlass2.jpg"
                ShowFooter="True" EnableViewState="true">
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
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
                    <%--Alert--%>
                    <asp:TemplateField HeaderText="Details" HeaderStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAlert" runat="server" Text='<%# Bind("Details") %>' Enabled="false"
                                Width="75px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAlert" runat="server" Text='<%# Bind("Details") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--Scheme--%>
                    <asp:TemplateField HeaderText="Event Date" HeaderStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtScheme" runat="server" Text='<%# Bind("EventDate") %>' Enabled="false"
                                Width="75px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblScheme" runat="server" Text='<%# Bind("EventDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--Next Occurence--%>
                    <asp:TemplateField HeaderText="Next Reminder Date" HeaderStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNxtOccur" runat="server" Text='<%# Bind("NextOccur") %>' Enabled="false"
                                Width="75px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblNxtOccur" runat="server" Text='<%# Bind("NextOccur") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--Message--%>
                    <asp:TemplateField HeaderText="Message" HeaderStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMessage" runat="server" Text='<%# Bind("Message") %>' Enabled="false"
                                Width="75px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblMessage" runat="server" Text='<%# Bind("Message") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--Reminder--%>
                    <%--<asp:TemplateField HeaderText="Reminder 1" HeaderStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtReminder1" runat="server" Text='<%# Bind("Reminder") %>' Enabled="false" Width="25px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblReminder1" runat="server" Text='<%# Bind("Reminder") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <%--Frequency--%>
                    <%--<asp:TemplateField HeaderText="Frequency" HeaderStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlFrequency" runat="server" Enabled="false">
                            </asp:DropDownList>
                            <asp:HiddenField ID="hdnFrequency" runat="server" Value='<%# Bind("Frequency") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFrequency" runat="server" Text='<%# Bind("Frequency") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <%--Edit Field--%>
                    <%--<asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                        <EditItemTemplate>
                            <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                Text="Update"></asp:LinkButton>
                            <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="Cancel"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                Text="Edit"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
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
    </table>
    <table style="width: 100%" id="tblPager" runat="server" visible="false">
    <tr>
        <td>
            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>
    </tr>
</table>
<table width="100%">
    <tr id="trAddNewEvent" runat="server">
        <td class="rightField" colspan="2">
            <label id="lblAddEvent" class="HeaderTextSmall">
                Add New Event</label>
            <hr />
        </td>
    </tr>
    <tr id="trDOB" runat="server">
        <td class="leftField" width="25%">
            <label id="lblScheme0" class="FieldName">
                Date of Birth:</label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtDOB" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr id="trSchemeDetails" runat="server">
        <td class="leftField" width="25%">
            <label id="lblScheme" class="FieldName">
                Scheme:</label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trReminderFive" runat="server">
        <td class="leftField" width="25%">
            <label id="lblReminderFreq" class="FieldName">
                Reminder Frequency:</label>
        </td>
        <td class="rightField">
            <asp:CheckBox ID="chkReminderFive" runat="server" CssClass="cmbField" Text="5 Days Before" />
        </td>
    </tr>
    <tr id="trReminderTen" runat="server">
        <td class="leftField" width="25%">
            &nbsp;
        </td>
        <td class="rightField">
            <asp:CheckBox ID="chkReminderTen" runat="server" CssClass="cmbField" Text="10 Days Before" />
        </td>
    </tr>
    <tr id="trReminderFifteen" runat="server">
        <td class="leftField" width="25%">
            &nbsp;
        </td>
        <td class="rightField">
            <asp:CheckBox ID="chkReminderFifteen" runat="server" CssClass="cmbField" Text="15 Days Before" />
        </td>
    </tr>
    <tr>
        <td class="leftField" width="25%">
            &nbsp;
        </td>
        <td class="rightField">
            &nbsp;
        </td>
    </tr>
    <tr id="trSubscribeButton" runat="server">
        <td class="leftField" width="25%">
            &nbsp;
        </td>
        <td class="rightField">
            <asp:Button ID="btnSaveReminderAlert" runat="server" CssClass="ButtonField" Text="Subscribe"
                OnClick="btnSaveReminderAlert_Click" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnSort" runat="server" Value="SchemeName ASC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />