<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageOutbox.ascx.cs"
    Inherits="WealthERP.Messages.MessageOutbox" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Outbox"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table class="TableBackground" width="100%">
    <tr id="trNoRecords" runat="server">
        <td align="center">
            <div id="SuccessMessage" runat="server" class="failure-msg" align="center">
                <asp:Label ID="lblNoRecords" Text="No Records found" runat="server"></asp:Label>
            </div>
        </td>
    </tr>
    <tr id="trContent" runat="server">
        <td>
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="RadGridOutbox">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadGridOutbox" LoadingPanelID="RadAjaxLoadingPanel1" />
                            <telerik:AjaxUpdatedControl ControlID="pnlOutboxMessage" LoadingPanelID="RadAjaxLoadingPanel1" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                    <telerik:AjaxSetting AjaxControlID="btnDelete">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadGridOutbox" LoadingPanelID="RadAjaxLoadingPanel1" />
                            <telerik:AjaxUpdatedControl ControlID="pnlOutboxMessage" LoadingPanelID="RadAjaxLoadingPanel1" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2007" />
            <div id="dv" class="dvInbox">
                <telerik:RadGrid ID="RadGridOutbox" runat="server" Width="860px" Height="250px" PageSize="6"
                    AllowPaging="True" ShowGroupPanel="true" GridLines="None" AutoGenerateColumns="False"
                    Style="border: 0; outline: none;" Skin="Telerik" EnableEmbeddedSkins="false"
                    OnItemCommand="RadGridOutbox_ItemCommand" EnableViewState="true" OnNeedDataSource="RadGridOutbox_NeedDataSource"
                    OnItemDataBound="RadGridOutbox_ItemDataBound">
                    <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                    <MasterTableView DataKeyNames="M_MessageId" ShowFooter="true">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="TemplateColumn1" Groupable="False">
                                <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="hdrCheckBox" runat="server" OnCheckedChanged="hdrCheckBox_CheckedChanged"
                                        AutoPostBack="true" />
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkbxRow" runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
                                        ForeColor="White" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="To" HeaderText="To" DataField="Recipients">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Subject" HeaderText="Subject" DataField="Subject">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Sent" SortExpression="Sent" HeaderText="Sent"
                                DataField="SentOn" DataFormatString="{0:MM/dd/yy hh:mm:ss}">
                                <HeaderStyle Width="125px"></HeaderStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn ButtonType="LinkButton" Text="Read" CommandName="Read">
                            </telerik:GridButtonColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <hr style="width: 860px; float: left;" />
                <br />
                <asp:Panel ID="pnlOutboxMessage" runat="server" ScrollBars="Auto" Height="200px"
                    Width="850px" BackColor="White" Style="padding: 20px 0 0 10px;">
                    <br />
                    <table id="tblMessageHeaders" runat="server" visible="false" width="100%">
                        <tr>
                            <td class="rightField" style="width: 5%">
                                <asp:Label ID="lblSubject" runat="server" Text="Subject:" CssClass="FieldName">
                                </asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:Label ID="lblSubjectContent" runat="server" Text="" CssClass="Field">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="rightField">
                                <asp:Label ID="lblSent" runat="server" Text="Sent:" CssClass="FieldName">
                                </asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:Label ID="lblSentContent" runat="server" Text="" CssClass="Field">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="rightField">
                                <asp:Label ID="lblRecipients" runat="server" Text="To:" CssClass="FieldName">
                                </asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:Label ID="lblRecipientsContent" runat="server" Text="" CssClass="Field">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Label ID="lblMessageContent" runat="server" Text="" CssClass="Field">
                    </asp:Label>
                </asp:Panel>
            </div>
        </td>
    </tr>
</table>
