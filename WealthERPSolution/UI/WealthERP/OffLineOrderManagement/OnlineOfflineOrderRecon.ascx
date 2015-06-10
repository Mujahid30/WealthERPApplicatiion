﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineOfflineOrderRecon.ascx.cs"
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

<%--<telerik:RadAjaxManager ID="AjaxManagerMain" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadWindowManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="radOrderDetails" LoadingPanelID="AjaxLoadingPanelMain" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>--%>
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
            <asp:Label ID="lblOrderStatus" runat="server" Text="status:" CssClass="FieldName" Visible="false"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="cmbField" Visible="false">
            <asp:ListItem Text="IsOnline" Value="1"></asp:ListItem>
            <asp:ListItem Text="IsOffline" Value="2"></asp:ListItem>
            
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="PCGButton" ValidationGroup="btnGo"
                OnClick="btnGo_Click" />
        </td>
    </tr>
    <tr id="trBtnSubmit" runat="server">
    </tr>
</table>
<asp:Panel ID="pnlOrderRecon" runat="server" Width="100%" Visible="false">
    <table width="80%">
        <tr>
            <td>
                <telerik:RadGrid ID="gvOrderRecon" runat="server" AllowSorting="false" enableloadondemand="True"
                    PageSize="10" AutoGenerateColumns="False" EnableEmbeddedSkins="False" GridLines="None"
                    ShowFooter="True" PagerStyle-AlwaysVisible="true" AllowPaging="true" ShowStatusBar="True"
                    Skin="Telerik" AllowFilteringByColumn="true" OnItemDataBound="gvOrderRecon_ItemDataBound"
                    OnNeedDataSource="gvOrderRecon_OnNeedDataSource" OnItemCommand="gvOrderRecon_OnItemCommand"
                    OnItemCreated="gvOrderRecon_ItemCreated" OnPreRender="gvOrderRecon_PreRender">
                    <MasterTableView AllowMultiColumnSorting="false" AllowSorting="false" DataKeyNames="COAD_Id,CFIOD_Quantity,AAC_AgentCode,C_PANNum,CO_OrderId,AIM_IssueId,CFIOD_DetailsId,AAC_AdviserAgentId"
                        EditMode="PopUp" AutoGenerateColumns="false" Width="100%" CommandItemSettings-ShowRefreshButton="false">
                        <Columns>
                            <telerik:GridEditCommandColumn EditText="Allotment Edit" UniqueName="editColumn"
                                CancelText="Cancel" UpdateText="Update" HeaderStyle-Width="80px" EditImageUrl="../Images/logo6.jpg">
                            </telerik:GridEditCommandColumn>
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
                                        <%--OnPreRender="rcbContinents_PreRender"--%>
                                        <Items>
                                            <telerik:RadComboBoxItem Text="All" Value="" Selected="false"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                    <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">

                                        <script type="text/javascript">
                                            function InvesterNameIndexChanged(sender, args) {
                                                var tableView = $find("<%#((GridItem)Container).OwnerTableView.ClientID %>");
                                                //////sender.value = args.get_item().get_value();
                                                tableView.filter("MissmatchType", args.get_item().get_value(), "EqualTo");
                                            } 
                                        </script>

                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COAD_Quantity" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Alloted Quentity" UniqueName="COAD_Quantity"
                                SortExpression="COAD_Quantity" AllowFiltering="true" Visible="true">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
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
                            <telerik:GridBoundColumn DataField="CFIOD_Quantity" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Order Quantity"
                                UniqueName="CFIOD_Quantity" SortExpression="CFIOD_Quantity" AllowFiltering="true"
                                Visible="true" HeaderStyle-ForeColor="Black">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AAC_AgentCode" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Order SubBrokerCode"
                                UniqueName="AAC_AgentCode" SortExpression="AAC_AgentCode" AllowFiltering="true" HeaderStyle-ForeColor="Black">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_PANNum" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Order PAN" UniqueName="C_PANNum"
                                SortExpression="C_PANNum" AllowFiltering="true" HeaderStyle-ForeColor="Black">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" UniqueName="Order">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkOrderEntry" runat="server" Text="Order Edit" OnClick="OnClick_lnkOrderEntry"></asp:LinkButton>
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
                                            <asp:Label ID="lblPan" runat="server" CssClass="FieldName" Text="Alloted PAN"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPAN" runat="server" CssClass="txtField" Text='<%# Bind("COAD_PAN") %>'></asp:TextBox>
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
    </table>
</asp:Panel>
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
                                    <asp:TextBox ID="txtOrderSubbrokerCode" runat="server" CssClass="txtField" ></asp:TextBox>
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
                                    <asp:TextBox ID="txtPAN" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
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