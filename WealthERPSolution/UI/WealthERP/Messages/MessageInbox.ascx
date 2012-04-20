<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageInbox.ascx.cs"
    Inherits="WealthERP.Messages.MessageInbox" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Inbox"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table class="TableBackground" width="100%">
    <tr id="trNoRecords" runat="server">
        <td align="center">
            <div id="divNoRecords" runat="server" class="failure-msg">
                <asp:Label ID="lblNoRecords" Text="No Records found" runat="server"></asp:Label>
            </div>
        </td>
    </tr>
    <tr id="trContent" runat="server">
        <td>
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="RadGrid1">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                            <telerik:AjaxUpdatedControl ControlID="pnlMessage" LoadingPanelID="RadAjaxLoadingPanel1" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2007" />
            <%-- IsSticky="true"--%>
            <div id="dv" class="dvInbox">
                <telerik:RadGrid ID="RadGrid1" runat="server" Width="860px" Height="230px" PageSize="5"
                    AllowPaging="True" ShowGroupPanel="true" GridLines="None" AutoGenerateColumns="False"
                    Style="border: 0; outline: none;" Skin="Telerik" EnableEmbeddedSkins="false"
                    OnItemCommand="RadGrid1_ItemCommand" OnNeedDataSource="RadGrid1_NeedDataSource"
                    OnItemDataBound="RadGrid1_ItemDataBound">
                    <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                    <MasterTableView DataKeyNames="MR_MessageRecipientId" ShowFooter="true">
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
                            <telerik:GridTemplateColumn UniqueName="TemplateColumn1" Groupable="False">
                                <HeaderStyle Width="30px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <img src='<%# Boolean.Parse(DataBinder.Eval(Container.DataItem, "IsReadByUser").ToString())? "../Images/Telerik/mailOpenIcon.png":"../Images/Telerik/mailNewIcon.png" %>'
                                        alt="MailIcon" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="From" HeaderText="From" DataField="Sender" ItemStyle-Wrap="false"
                                ItemStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Subject" HeaderText="Subject" DataField="Subject"
                                ItemStyle-Wrap="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Received" SortExpression="Received" HeaderText="Received"
                                DataField="SentOn" DataFormatString="{0:R}" ItemStyle-Wrap="false">
                                <HeaderStyle Width="125px"></HeaderStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ReadByUser" DataField="IsReadByUser" Visible="false" />
                            <telerik:GridButtonColumn ButtonType="LinkButton" Text="Read" CommandName="Read"
                                ItemStyle-Wrap="false" ItemStyle-Width="50px">
                            </telerik:GridButtonColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <br />
                <hr style="width: 860px; float: left;" />
                <asp:Panel ID="pnlMessage" runat="server" ScrollBars="Auto" Height="200px" Width="850px"
                    BackColor="White" Style="padding: 20px 0 0 10px; float:left;">
                    <br />
                    <table id="tblMessageHeaders" runat="server" visible="false">
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblSubject" runat="server" Text="Subject:" CssClass="FieldName">
                                </asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:Label ID="lblSubjectContent" runat="server" Text="" CssClass="Field">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblSent" runat="server" Text="Received:" CssClass="FieldName">
                                </asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:Label ID="lblSentContent" runat="server" Text="" CssClass="Field">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblSender" runat="server" Text="Sender:" CssClass="FieldName">
                                </asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:Label ID="lblSenderContent" runat="server" Text="" CssClass="Field">
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
