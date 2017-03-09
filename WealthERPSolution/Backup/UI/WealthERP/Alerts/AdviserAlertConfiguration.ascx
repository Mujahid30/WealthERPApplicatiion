<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserAlertConfiguration.ascx.cs"
    Inherits="WealthERP.Alerts.AdviserAlertConfiguration" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script runat="server">  
   
</script>

<script type="text/javascript">
    function RowContextMenu(sender, eventArgs) {
        var menu = $find("<%=radMenu.ClientID %>");
        var evt = eventArgs.get_domEvent();

        if (evt.target.tagName == "INPUT" || evt.target.tagName == "A") {
            return;
        }

        var index = eventArgs.get_itemIndexHierarchical();
        document.getElementById("radGridClickedRowIndex").value = index;

        sender.get_masterTableView().selectItem(sender.get_masterTableView().get_dataItems()[index].get_element(), true);

        menu.show(evt);

        evt.cancelBubble = true;
        evt.returnValue = false;

        if (evt.stopPropagation) {
            evt.stopPropagation();
            evt.preventDefault();
        }
    }
</script>

<div class="divPageHeading">
    <table width="100%">
        <tr>
            <td align="left">
                Alert Rule
            </td>
            <td>
            </td>
        </tr>
    </table>
</div>
<br />
<table width="68%">
    <tr>
        <td>
            <telerik:RadGrid ID="gvAdviserAlert" runat="server" AllowSorting="True" enableloadondemand="True"
                PageSize="5" AutoGenerateColumns="False" EnableEmbeddedSkins="False" GridLines="None"
                ShowFooter="True" PagerStyle-AlwaysVisible="true" AllowPaging="true" ShowStatusBar="True"
                Skin="Telerik" AllowFilteringByColumn="true" OnItemCommand="gvAdviserAlert_OnItemCommand"
                OnNeedDataSource="gvAdviserAlert_OnNeedDataSource" OnItemDataBound="gvAdviserAlert_OnItemDataBound">
                <MasterTableView DataKeyNames="AAECR_RuleId" AllowFilteringByColumn="true" Width="100%"
                    AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="Top"
                    EditMode="PopUp">
                    <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                        AddNewRecordText="Add SIP Alert" ShowExportToCsvButton="false" ShowAddNewRecordButton="true"
                        ShowRefreshButton="false" />
                    <Columns>
                      <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" Visible="true"></telerik:GridEditCommandColumn>
                        <%--<telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                            UpdateText="Update" HeaderStyle-Width="80px">
                        </telerik:GridEditCommandColumn>--%>
                        <telerik:GridBoundColumn DataField="AAECR_RuleId" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="RuleId" UniqueName="AAECR_RuleId"
                            SortExpression="AAECR_RuleId" AllowFiltering="true" Visible="false">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AEL_EventDescription" HeaderStyle-Width="20px"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            HeaderText="Event Description" UniqueName="AEL_EventDescription" SortExpression="AEL_EventDescription"
                            AllowFiltering="true" Visible="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AAECR_DefaultReminderDay" HeaderStyle-Width="20px"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            HeaderText="Default Reminder Day" UniqueName="AAECR_DefaultReminderDay" SortExpression="AAECR_DefaultReminderDay"
                            AllowFiltering="true" Visible="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AAECR_RuleName" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Rule Name" UniqueName="AAECR_RuleName"
                            SortExpression="AAECR_RuleName" AllowFiltering="true" Visible="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AAECR_DefaultCondition" HeaderStyle-Width="20px"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            HeaderText="DefaultCondition" UniqueName="AAECR_DefaultCondition" SortExpression="AAECR_DefaultCondition"
                            AllowFiltering="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AAECR_DefaultConditionValue" HeaderStyle-Width="20px"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            HeaderText="DefaultConditionValue" UniqueName="AAECR_DefaultConditionValue" SortExpression="AAECR_DefaultConditionValue"
                            AllowFiltering="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AAECR_IsOverride" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Is Override" UniqueName="AAECR_IsOverride"
                            SortExpression="AAECR_IsOverride" AllowFiltering="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AAECR_IsActive" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Is Active" UniqueName="AAECR_IsActive"
                            SortExpression="AAECR_IsActive" AllowFiltering="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                    </Columns>
                    <EditFormSettings EditFormType="Template" PopUpSettings-Height="220px" PopUpSettings-Width="350px"
                        CaptionFormatString="Add SIP Configuration">
                        <FormTemplate>
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblSIPDescription" runat="server" Text="SIP Description:" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSIPDiscription" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlSIPDiscription_OnSelectedIndexChanged"
                                            AutoPostBack="false">
                                        </asp:DropDownList>
                                        <span id="Span3" class="spnRequiredField">*</span>
                                        <br />
                                        <asp:RequiredFieldValidator ID="ReqddlLevel" runat="server" CssClass="rfvPCG" ErrorMessage="Please Select SIP Description"
                                            Display="Dynamic" ControlToValidate="ddlSIPDiscription" ValidationGroup="btnOK"
                                            InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblAlert" runat="server" Text="Alert Before:" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAlert" runat="server" CssClass="cmbField">
                                        </asp:DropDownList>
                                        <span id="Span2" class="spnRequiredField">*</span>
                                        <br />
                                        <asp:RequiredFieldValidator ID="ReqddlAlert" runat="server" CssClass="rfvPCG" ErrorMessage="Please Select Alert"
                                            Display="Dynamic" ControlToValidate="ddlAlert" ValidationGroup="btnOK" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblRuleType" runat="server" Text="Rule Type" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRuleType" runat="server" MaxLength="100"></asp:TextBox>
                                        <span id="Span1" class="spnRequiredField">*</span>
                                        <br />
                                        <asp:RequiredFieldValidator ID="ReqtxtRuleType" runat="server" CssClass="rfvPCG"
                                            ErrorMessage="Enter SIP Rule" Display="Dynamic" ControlToValidate="txtRuleType"
                                            ValidationGroup="btnOK" InitialValue="">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkActive" runat="server" Text="IsActive" />
                                    </td>
                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkOverrite" runat="server" Text="IS Override" />
                                        </td>
                                    </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="btnOK" Text="Submit" runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                            CausesValidation="True" ValidationGroup="btnOK" />
                                    </td>
                                    <td class="rightData">
                                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                            CssClass="PCGButton" CommandName="Cancel"></asp:Button>
                                    </td>
                                    <td class="rightData">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </FormTemplate>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings>
                    <ClientEvents OnRowContextMenu="RowContextMenu" />
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="false"></Scrolling>
                    
                </ClientSettings>
            </telerik:RadGrid>
        </td>
    </tr>
</table>
<input type="hidden" id="radGridClickedRowIndex" name="radGridClickedRowIndex" visible="false"/>
<telerik:RadContextMenu ID="radMenu" runat="server" OnItemClick="radMenu_ItemClick"
    EnableRoundedCorners="true" EnableShadows="true" Skin="WebBlue" Visible="false">
    <Items>
        <telerik:RadMenuItem Text="Add">
        </telerik:RadMenuItem>
        <telerik:RadMenuItem Text="Edit">
        </telerik:RadMenuItem>
    </Items>
</telerik:RadContextMenu>
