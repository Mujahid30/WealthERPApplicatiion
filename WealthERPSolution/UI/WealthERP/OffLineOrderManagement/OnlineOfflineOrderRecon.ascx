<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineOfflineOrderRecon.ascx.cs"
    Inherits="WealthERP.OffLineOrderManagement.OnlineOfflineOrderRecon" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script type="text/javascript">
    function setCustomPosition(sender, args) {
        sender.moveTo(sender.get_left(), sender.get_top());
    }
    function GeAgentId(source, eventArgs) {
        isItemSelected = true;
        document.getElementById("<%= txtAgentId.ClientID %>").value = eventArgs.get_value();
        txtOrderSubbrokerCode
        return false;
    }
</script>

<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var gvControl = document.getElementById('<%= gvOrderRecon.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chkId";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkIdAll");

        //get an array of input types in the gridview
        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0 && (!inputTypes[i].disabled))
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
</script>

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table width="100%">
                    <tr>
                        <td align="left">
                            Non MF Recon
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="ibtExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="ibtExport_OnClick"
                                Height="25px" Width="25px" Visible="false"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td align="right" style="padding-left: 10px;">
            <asp:Label ID="Label2" runat="server" Text="Product:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlProduct" runat="server" CssClass="cmbField" AutoPostBack="true"
                Width="180px" OnSelectedIndexChanged="ddlProduct_OnSelectedIndexChanged">
                <asp:ListItem Value="Select">Select</asp:ListItem>
                <asp:ListItem Value="Bonds">Bonds</asp:ListItem>
                <asp:ListItem Value="IP">IPO</asp:ListItem>
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Product Type"
                CssClass="rfvPCG" ControlToValidate="ddlProduct" ValidationGroup="btnGo" Display="Dynamic"
                InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
        <td align="right" id="tdcategory" runat="server" visible="false" style="padding-left: 10px;">
            <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
        </td>
        <td id="tdCategorydropdown" runat="server" visible="false">
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" Width="200px"
                AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Category"
                CssClass="rfvPCG" ControlToValidate="ddlCategory" ValidationGroup="btnGo" Display="Dynamic"
                InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:Label ID="lblIssue" runat="server" Text="Issue:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlIssueName" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td>
            <asp:Label ID="lblOrderStatus" runat="server" Text="status:" CssClass="FieldName"
                Visible="false"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="cmbField" Visible="false">
                <asp:ListItem Text="IsOnline" Value="1"></asp:ListItem>
                <asp:ListItem Text="IsOffline" Value="2"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trdate" runat="server">
        <td align="right">
            <asp:Label ID="lblFromDate" runat="server" CssClass="FieldName" Text="From:"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtOrderFrom" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <div id="dvTransactionDate" runat="server" class="dvInLine">
                <span id="Span3" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="rfvtxtTransactionDate" ControlToValidate="txtOrderFrom"
                    ErrorMessage="<br />Please select a From Date" CssClass="cvPCG" Display="Dynamic"
                    runat="server" InitialValue="" ValidationGroup="btnViewOrder">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                    Type="Date" ControlToValidate="txtOrderFrom" CssClass="cvPCG" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
            </div>
        </td>
        <td align="right">
            <asp:Label ID="lblTo" runat="server" CssClass="FieldName" Text="To:"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtOrderTo" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <div id="Div1" runat="server" class="dvInLine">
                <span id="Span4" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtOrderTo"
                    ErrorMessage="<br />Please select a To Date" CssClass="cvPCG" Display="Dynamic"
                    runat="server" InitialValue="" ValidationGroup="btnViewOrder">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                    Type="Date" ControlToValidate="txtOrderTo" CssClass="cvPCG" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
            </div>
            <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtOrderTo"
                ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                ControlToCompare="txtOrderFrom" CssClass="cvPCG" ValidationGroup="btnViewOrder"
                Display="Dynamic">
            </asp:CompareValidator>
        </td>
    </tr>
    <tr id="trSatus" runat="server">
        <td align="right">
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Type:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlType_OnSelectedIndexChanged"
                Width="240px">
                <asp:ListItem Text="All" Value="1">
                </asp:ListItem>
                <asp:ListItem Text="Orders Exist & Allotment Exist" Value="2">
                </asp:ListItem>
                <asp:ListItem Text="Orders Exist & Allotment Not Exist" Value="3">
                </asp:ListItem>
                <asp:ListItem Text="Orders Not Exist & Allotment Exist" Value="4">
                </asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="PCGButton" ValidationGroup="btnGo"
                OnClick="btnGo_Click" />
        </td>
    </tr>
    <tr id="trBtnSubmit" runat="server">
    </tr>
</table>
<table width="100%">
    <tr id="tblMessagee" runat="server" visible="true">
        <td align="center">
            <div id="divMessage" align="center">
            </div>
        </td>
    </tr>
</table>
<asp:Panel ID="pnlOrderRecon" runat="server" Width="100%" Visible="false" ScrollBars="Horizontal">
    <table width="80%">
        <tr>
            <td>
                <telerik:RadGrid ID="gvOrderRecon" runat="server" AllowSorting="false" enableloadondemand="True"
                    PageSize="10" AutoGenerateColumns="False" EnableEmbeddedSkins="False" GridLines="None"
                    ShowFooter="True" PagerStyle-AlwaysVisible="true" AllowPaging="true" ShowStatusBar="True"
                    Skin="Telerik" AllowFilteringByColumn="true" OnItemDataBound="gvOrderRecon_ItemDataBound"
                    OnNeedDataSource="gvOrderRecon_OnNeedDataSource" OnItemCommand="gvOrderRecon_OnItemCommand"
                    OnItemCreated="gvOrderRecon_ItemCreated" OnPreRender="gvOrderRecon_PreRender">
                    <MasterTableView AllowMultiColumnSorting="false" AllowSorting="false" DataKeyNames="COAD_Id,CFIOD_Quantity,AAC_AgentCode,C_PANNum,CO_OrderId,AIM_IssueId,CFIOD_DetailsId,AAC_AdviserAgentId,COAD_Quantity,COAD_SubBrokerCode,COAD_PAN,CO_ApplicationNumberAlloted,AllotmentDate,COAD_Id,AllotmentOROrderDate"
                        EditMode="PopUp" AutoGenerateColumns="false" Width="100%" CommandItemSettings-ShowRefreshButton="false">
                        <Columns>
                            <telerik:GridEditCommandColumn EditText="Allotment Edit" UniqueName="editColumn"
                                CancelText="Cancel" UpdateText="Update" HeaderStyle-Width="80px" EditImageUrl="../Images/logo6.jpg">
                            </telerik:GridEditCommandColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="IsValid"
                                HeaderStyle-Width="70px">
                                <HeaderTemplate>
                                    <input id="chkIdAll" name="chkIdAll" type="checkbox" onclick="checkAllBoxes()" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkId" Enabled='<%# Bind("IsValid") %>' runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="Type" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Type" UniqueName="Type"
                                SortExpression="Type" AllowFiltering="true" Visible="true">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IsValidPanSubbrokerCode" HeaderStyle-Width="20px"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                HeaderText="IsValidPanSubbrokerCode" UniqueName="IsValidPanSubbrokerCode" SortExpression="IsValidPanSubbrokerCode"
                                AllowFiltering="true">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MissmatchType" AllowFiltering="true" HeaderText="MissmatchType"
                                HeaderStyle-Width="270px" UniqueName="MissmatchType" SortExpression="MissmatchType"
                                AutoPostBackOnFilter="false" ShowFilterIcon="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                <FilterTemplate>
                                    <telerik:RadComboBox ID="RadComboBoxRR" Width="180px" CssClass="cmbField" AllowFiltering="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxRR_SelectedIndexChanged"
                                        IsFilteringEnabled="true" AppendDataBoundItems="true" AutoPostBackOnFilter="false"
                                        OnPreRender="rcbContinents1_PreRender" EnableViewState="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("MissmatchType").CurrentFilterValue %>'
                                        runat="server">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="All" Value="" Selected="false"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">

                                        <script type="text/javascript">
                                            function InvesterNameIndexChanged(sender, args) {
                                                var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                                tableView.filter("MissmatchType", args.get_item().get_value(), "EqualTo");
                                            } 
                                        </script>

                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COAD_Quantity" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Alloted Quantity"
                                UniqueName="COAD_Quantity" SortExpression="COAD_Quantity" AllowFiltering="true"
                                Visible="true">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="issueName" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Issue Name" UniqueName="issueName"
                                SortExpression="issueName" AllowFiltering="true">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COAD_SubBrokerCode" HeaderStyle-Width="20px"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                HeaderText="Alloted SubBrokerCode" UniqueName="COAD_SubBrokerCode" SortExpression="COAD_SubBrokerCode"
                                AllowFiltering="true">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AllotmentDate" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Allotment Date"
                                DataType="System.String" UniqueName="AllotmentDate" SortExpression="AllotmentDate"
                                AllowFiltering="true" Visible="true">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AllotmentOrderDate" HeaderStyle-Width="20px"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                HeaderText="Allotment Order Date" UniqueName="AllotmentOrderDate" SortExpression="AllotmentOrderDate"
                                AllowFiltering="true" Visible="true">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COAD_PAN" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Alloted PAN" UniqueName="COAD_PAN"
                                SortExpression="COAD_PAN" AllowFiltering="true">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ApplicationNumberAlloted" HeaderStyle-Width="20px"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                HeaderText="Alloted Application Number" UniqueName="CO_ApplicationNumberAlloted"
                                SortExpression="CO_ApplicationNumberAlloted" AllowFiltering="true" Visible="true">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CFIOD_Quantity" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Order Quantity"
                                UniqueName="CFIOD_Quantity" SortExpression="CFIOD_Quantity" AllowFiltering="true"
                                Visible="true" HeaderStyle-ForeColor="Black">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AAC_AgentCode" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Order SubBrokerCode"
                                UniqueName="AAC_AgentCode" SortExpression="AAC_AgentCode" AllowFiltering="true"
                                HeaderStyle-ForeColor="Black">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ApplicationNumber" HeaderStyle-Width="20px"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                HeaderText="Application Number" UniqueName="CO_ApplicationNumber" SortExpression="CO_ApplicationNumber"
                                AllowFiltering="true" Visible="true" HeaderStyle-ForeColor="Black">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="OrderDate" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Order Date" UniqueName="OrderDate"
                                SortExpression="OrderDate" AllowFiltering="true" Visible="true" HeaderStyle-ForeColor="Black">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_PANNum" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Order PAN" UniqueName="C_PANNum"
                                SortExpression="C_PANNum" AllowFiltering="true" HeaderStyle-ForeColor="Black">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="InvestorName" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="InvestorName"
                                UniqueName="InvestorName" SortExpression="InvestorName" AllowFiltering="true"
                                HeaderStyle-ForeColor="Black">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WOS_OrderStep" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Order Status"
                                UniqueName="WOS_OrderStep" SortExpression="WOS_OrderStep" AllowFiltering="true"
                                HeaderStyle-ForeColor="Black">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" UniqueName="OrderEdit">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkOrderEntry" runat="server" Text="Order Edit" OnClick="OnClick_lnkOrderEntry"></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" UniqueName="AddOrder">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkMatch" runat="server" Text="Add Order" OnClick="lnkMatch_SelectedIndexChanged"></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <%-- <telerik:GridEditCommandColumn EditText="Allotment Edit" UniqueName="editColumn1" 
                                CancelText="Cancel" UpdateText="Update" HeaderStyle-Width="80px" EditImageUrl="../Images/logo6.jpg">
                            </telerik:GridEditCommandColumn>--%>
                        </Columns>
                        <EditFormSettings EditFormType="Template" PopUpSettings-Height="200px" PopUpSettings-Width="350px"
                            CaptionFormatString="Allotment Update">
                            <FormTemplate>
                                <table width="100%">
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblAllotedQty" runat="server" CssClass="FieldName" Text="Alloted Qty."></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAllotedQty" runat="server" CssClass="txtField" Text='<%# Bind("COAD_Quantity") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblAllotedSubBrokerCode" runat="server" CssClass="FieldName" Text="Alloted SubBrokerCode."></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAllotedSubBrokerCode" runat="server" CssClass="txtField" Text='<%# Bind("COAD_SubBrokerCode") %>'></asp:TextBox>
                                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtAllotedSubBrokerCode"
                                                ServiceMethod="GetAgentCodeAssociateDetails" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                                                MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                                                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                                                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True">
                                            </ajaxToolkit:AutoCompleteExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Application No.">  </asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtApplicationNo" runat="server" CssClass="txtField" Text='<%# Bind("CO_ApplicationNumberAlloted") %>'> </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblPan" runat="server" CssClass="FieldName" Text="Alloted PAN"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPAN" runat="server" CssClass="txtField"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Button ID="btnOK" Text="Update" runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
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
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
        <tr runat="server" id="trReprocess">
            <td>
                <asp:Button ID="btnReprocess" Text="Reprocess" runat="server" CausesValidation="False"
                    CssClass="PCGButton" OnClick="btnReprocess_Click"></asp:Button>
                <asp:Button ID="btnBulkOrder" Text="BulkOrderGeneration" runat="server" CausesValidation="False"
                    CssClass="PCGButton" OnClick="BulkOrderGeneration_Click"></asp:Button>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Panel>
<%--<asp:Panel ID="pnlMatch" runat="server" ScrollBars="Both">
    <telerik:RadWindowManager runat="server" ID="RadWindowManager2">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" Modal="true" Behaviors="Close, Move" Width="450px"
                Height="200px" runat="server" Title="Match" Left="60%" Top="100" OnClientShow="setCustomPosition"
                VisibleOnPageLoad="false" Visible="false" >
                <ContentTemplate>
                    <div id="div2" runat="server"  >
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="rgMatch" runat="server" AllowSorting="false" enableloadondemand="True"
                                        PageSize="10" AutoGenerateColumns="False" EnableEmbeddedSkins="False" GridLines="None"
                                        ShowFooter="True" PagerStyle-AlwaysVisible="true" AllowPaging="true" ShowStatusBar="True"
                                        Skin="Telerik" AllowFilteringByColumn="true" OnNeedDataSource="rgMatch_OnNeedDataSource"
                                        Visible="true" >
                                        <MasterTableView AllowMultiColumnSorting="false" AllowSorting="false" DataKeyNames="COAD_Id"
                                            EditMode="PopUp" AutoGenerateColumns="false" Width="100%" CommandItemSettings-ShowRefreshButton="false">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="COAD_SubBrokerCode" HeaderStyle-Width="20px"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Alloted SubBrokerCode" UniqueName="COAD_SubBrokerCode" SortExpression="COAD_SubBrokerCode"
                                                    AllowFiltering="true">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="COAD_PAN" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Alloted PAN" UniqueName="COAD_PAN"
                                                    SortExpression="COAD_PAN" AllowFiltering="true">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="COAD_Quantity" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Order Quantity"
                                                    UniqueName="COAD_Quantity" SortExpression="COAD_Quantity" AllowFiltering="true"
                                                    Visible="true"  >
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                 
                                                
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="Button1" runat="server" CssClass="PCGButton" Text="Update" OnClick="btnSubmit_Update" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Panel>--%>
<asp:Panel ID="pnlOrderDetails" runat="server">
    <telerik:RadWindowManager runat="server" ID="RadWindowManager1">
        <Windows>
            <telerik:RadWindow ID="radOrderDetails" Modal="true" Behaviors="Close, Move" Width="350px"
                Height="200px" runat="server" Title="Order Details" Left="50%" Top="100" OnClientShow="setCustomPosition"
                VisibleOnPageLoad="false" Visible="false">
                <ContentTemplate>
                    <div id="divorderQty" runat="server" visible="false">
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOrderQty" runat="server" CssClass="FieldName" Text="Order Qty."></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOrderQty" runat="server" CssClass="txtField"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblOrderSubbrokerCode" runat="server" CssClass="FieldName" Text="Order SubbrokerCode"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOrderSubbrokerCode" runat="server" CssClass="txtField"></asp:TextBox>
                                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2_txtOrderSubbrokerCode"
                                        runat="server" TargetControlID="txtOrderSubbrokerCode" ServiceMethod="GetAgentCodeAssociateDetails"
                                        ServicePath="~/CustomerPortfolio/AutoComplete.asmx" MinimumPrefixLength="1" EnableCaching="False"
                                        CompletionSetCount="5" CompletionInterval="100" CompletionListCssClass="AutoCompleteExtender_CompletionList"
                                        CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                                        UseContextKey="True" DelimiterCharacters="" Enabled="True" OnClientItemSelected="GeAgentId">
                                    </ajaxToolkit:AutoCompleteExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblPAN" runat="server" CssClass="FieldName" Text="Order PAN"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPAN" runat="server" CssClass="txtField" Enabled="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" Text="Update" OnClick="btnSubmit_Update" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Panel>
<asp:HiddenField ID="txtAgentId" runat="server" />
<asp:HiddenField ID="hdnissueId" runat="server" />
<asp:HiddenField ID="hdnorderId" runat="server" />
<asp:HiddenField ID="hdnFIorderId" runat="server" />
