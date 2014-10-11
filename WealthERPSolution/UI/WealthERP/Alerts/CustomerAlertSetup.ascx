<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerAlertSetup.ascx.cs"
    Inherits="WealthERP.Alerts.CustomerAlertSetup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

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
    function GetCustomerId(source, eventArgs) {
        isItemSelected = true;
        //         document.getElementById("lblgetPan").innerHTML = "";
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();

        return false;
    }
</script>

<div class="divPageHeading">
    <table width="100%">
        <tr>
            <td align="left">
                Customer Alert Setup
            </td>
            <td>
            </td>
        </tr>
    </table>
</div>
<br />
<table>
    <tr>
        <td>
        </td>
        <td align="right">
            <asp:Label ID="lblType" runat="server" Text="Search Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem Text="Select" Value="Select" />
                <asp:ListItem Text="Name" Value="Name" />
                <asp:ListItem Text="PAN" Value="Panno" />
                <asp:ListItem Text="Client Code" Value="Clientcode" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlType" runat="server" ErrorMessage="</br> Select Search Type"
                CssClass="rfvPCG" ControlToValidate="ddlType" ValidationGroup="btnGo" Display="Dynamic"
                InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
        <td align="left" id="tdtxtPansearch" runat="server" visible="false">
            <asp:TextBox ID="txtPansearch" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" TabIndex="2" Width="250px">
            </asp:TextBox><span id="Span1" class="spnRequiredField"></span>
            <cc1:textboxwatermarkextender id="TextBoxWatermarkExtender1" targetcontrolid="txtPansearch"
                watermarktext="Enter few characters of Pan" runat="server" enableviewstate="false">
            </cc1:textboxwatermarkextender>
            <ajaxToolkit:autocompleteextender id="txtPansearch_autoCompleteExtender" runat="server"
                targetcontrolid="txtPansearch" servicemethod="GetAdviserCustomerPan" servicepath="~/CustomerPortfolio/AutoComplete.asmx"
                minimumprefixlength="1" enablecaching="False" completionsetcount="1" completioninterval="0"
                completionlistcssclass="AutoCompleteExtender_CompletionList" completionlistitemcssclass="AutoCompleteExtender_CompletionListItem"
                completionlisthighlighteditemcssclass="AutoCompleteExtender_HighlightedItem"
                usecontextkey="True" onclientitemselected="GetCustomerId" delimitercharacters=""
                enabled="True" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPansearch"
                ErrorMessage="<br />Please Enter Pan number" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="btnGo"></asp:RequiredFieldValidator>
        </td>
        <td align="left" id="tdtxtClientCode" runat="server" visible="false">
            <asp:TextBox ID="txtClientCode" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" Width="250px"></asp:TextBox>
            <cc1:textboxwatermarkextender id="TextBoxWatermarkExtender2" targetcontrolid="txtClientCode"
                watermarktext="Enter few characters of Client Code" runat="server" enableviewstate="false">
            </cc1:textboxwatermarkextender>
            <ajaxToolkit:autocompleteextender id="txtClientCode_autoCompleteExtender" runat="server"
                targetcontrolid="txtClientCode" servicemethod="GetCustCode" servicepath="~/CustomerPortfolio/AutoComplete.asmx"
                minimumprefixlength="1" enablecaching="False" completionsetcount="5" completioninterval="100"
                completionlistcssclass="AutoCompleteExtender_CompletionList" completionlistitemcssclass="AutoCompleteExtender_CompletionListItem"
                completionlisthighlighteditemcssclass="AutoCompleteExtender_HighlightedItem"
                usecontextkey="True" onclientitemselected="GetCustomerId" delimitercharacters=""
                enabled="True" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtClientCode"
                ErrorMessage="<br />Please Enter Client Code" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="btnGo"></asp:RequiredFieldValidator>
        </td>
        <td align="left" id="tdtxtCustomerName" runat="server" visible="false">
            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" Width="250px">  </asp:TextBox>
            <cc1:textboxwatermarkextender id="txtCustomerName_water" targetcontrolid="txtCustomerName"
                watermarktext="Enter Three Characters of Customer" runat="server" enableviewstate="false">
            </cc1:textboxwatermarkextender>
            <ajaxToolkit:autocompleteextender id="txtCustomerName_autoCompleteExtender" runat="server"
                targetcontrolid="txtCustomerName" servicemethod="GetCustomerName" servicepath="~/CustomerPortfolio/AutoComplete.asmx"
                minimumprefixlength="3" enablecaching="False" completionsetcount="5" completioninterval="100"
                completionlistcssclass="AutoCompleteExtender_CompletionList" completionlistitemcssclass="AutoCompleteExtender_CompletionListItem"
                completionlisthighlighteditemcssclass="AutoCompleteExtender_HighlightedItem"
                usecontextkey="True" onclientitemselected="GetCustomerId" delimitercharacters=""
                enabled="True" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCustomerName"
                ErrorMessage="<br />Please Enter Customer Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="btnGo"></asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:Button ID="btngo" runat="server" CssClass="PCGButton" OnClick="click_Go" Text="Go"
                ValidationGroup="btnGo" />
        </td>
    </tr>
</table>
<table width="68%" id="tblCustomerAlertSetup" runat="server" visible="false">
    <tr>
        <td>
            <telerik:RadGrid ID="gvCustomerAlertSetup" runat="server" AllowSorting="True" enableloadondemand="True"
                PageSize="5" AutoGenerateColumns="False" EnableEmbeddedSkins="False" GridLines="None"
                ShowFooter="True" PagerStyle-AlwaysVisible="true" AllowPaging="true" ShowStatusBar="True"
                Skin="Telerik" AllowFilteringByColumn="true" OnItemCommand="gvCustomerAlertSetup_OnItemCommand"
                OnNeedDataSource="gvCustomerAlertSetup_OnNeedDataSource" OnItemDataBound="gvCustomerAlertSetup_OnItemDataBound">
                <MasterTableView DataKeyNames="AES_EventSetupID" AllowFilteringByColumn="true" Width="100%"
                    AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="Top"
                    EditMode="PopUp">
                    <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                        AddNewRecordText="Add New Customer Alert Setup" ShowExportToCsvButton="false"
                        ShowAddNewRecordButton="true" ShowRefreshButton="false" />
                    <Columns>
                        <%-- <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" Visible="false">
                        </telerik:GridEditCommandColumn>--%>
                        <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                            UpdateText="Update" HeaderStyle-Width="80px" Visible="false">
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="AES_EventSetupID" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Event SetupId"
                            UniqueName="AES_EventSetupID" SortExpression="AES_EventSetupID" AllowFiltering="true"
                            Visible="false">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CMFSS_SystematicSetupId" HeaderStyle-Width="20px"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            HeaderText="Request Id" UniqueName="CMFSS_SystematicSetupId" SortExpression="CMFSS_SystematicSetupId"
                            AllowFiltering="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PASP_SchemePlanName" HeaderStyle-Width="80px"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            HeaderText="Scheme Plan Name" UniqueName="PASP_SchemePlanName" SortExpression="PASP_SchemePlanName"
                            AllowFiltering="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CMFA_FolioNum" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Folio No." UniqueName="CMFA_FolioNum"
                            SortExpression="CMFA_FolioNum" AllowFiltering="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CMFSS_Amount" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Amount" UniqueName="CMFSS_Amount"
                            SortExpression="CMFSS_Amount" AllowFiltering="true" Visible="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CMFSS_SystematicDate" HeaderStyle-Width="20px"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            HeaderText="SIP Date" UniqueName="CMFSS_SystematicDate" SortExpression="CMFSS_SystematicDate"
                            AllowFiltering="true" Visible="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AES_EventSubscriptionDate" HeaderStyle-Width="20px"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                            HeaderText="Event Subscription Date" UniqueName="AES_EventSubscriptionDate" SortExpression="AES_EventSubscriptionDate"
                            AllowFiltering="true" Visible="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AAECR_DefaultReminderDay" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Rule Id" UniqueName="AAECR_DefaultReminderDay"
                            SortExpression="AAECR_DefaultReminderDay" AllowFiltering="true" Visible="true">
                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete?"
                            ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                            Text="Delete">
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" Width="70px" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings EditFormType="Template" PopUpSettings-Height="220px" PopUpSettings-Width="500px"
                        CaptionFormatString="Add Customer SIP Configuration">
                        <FormTemplate>
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblSIPDescription" runat="server" Text="SIP:" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSIPDiscription" runat="server" CssClass="cmbField" AutoPostBack="false"
                                            Width="300px">
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
                                        <asp:Label ID="lblAlert" runat="server" Text="Alert Rule:" CssClass="FieldName"></asp:Label>
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
                                        <asp:Label ID="lblMsg" runat="server" CssClass="FieldName" Text="Event Message:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMsg" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblEventSubscription" runat="server" CssClass="FieldName" Text="Subsccription Date:"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtEventSubscription" CssClass="txtField" runat="server"
                                            Culture="English (United States)" AutoPostBack="false" Skin="Telerik" EnableEmbeddedSkins="false"
                                            ShowAnimation-Type="Fade" MinDate="1900-01-01" TabIndex="5">
                                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                Skin="Telerik" EnableEmbeddedSkins="false">
                                            </Calendar>
                                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                            <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                        <span id="Span7" class="spnRequiredField">*</span>
                                        <asp:RequiredFieldValidator ID="ReqtxtEventSubscription" ControlToValidate="txtEventSubscription"
                                            CssClass="rfvPCG" ErrorMessage="<br />Please select Subscription Date" Display="Dynamic"
                                            runat="server" InitialValue="" ValidationGroup="btnsubmit"></asp:RequiredFieldValidator>
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
    EnableRoundedCorners="true" EnableShadows="true" Skin="WebBlue" visible="false">
    <Items>
        <telerik:RadMenuItem Text="Add">
        </telerik:RadMenuItem>
        <telerik:RadMenuItem Text="Delete">
        </telerik:RadMenuItem>
    </Items>
</telerik:RadContextMenu>
<asp:HiddenField ID="txtCustomerId" runat="server" />
