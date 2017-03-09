<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddSchemeMapping.ascx.cs"
    Inherits="WealthERP.SuperAdmin.AddSchemeMapping" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<body class="BODY">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            //            function OnClientItemOpening() {
            //                alert('hi');
            //                var item = args.get_item(); item.get_items().getItem(1).get_element().style.display = "none";
            //                item.visible = false;
            //            }
            function HeaderMenuShowing(sender, eventArgs) {

                if (eventArgs.get_gridColumn().get_uniqueName() == "PASC_AMC_ExternalCode" ||
                eventArgs.get_gridColumn().get_uniqueName() == "PASP_SchemePlanName" ||
                 eventArgs.get_gridColumn().get_uniqueName() == "PASC_AMC_ExternalType"
                ) {
                    //hide the Clear sorting option from the header context menu of the column with name ContactName

                    eventArgs.get_menu().get_items().getItem(0).get_element().style.display = "none";
                    eventArgs.get_menu().get_items().getItem(1).get_element().style.display = "none";
                    eventArgs.get_menu().get_items().getItem(2).get_element().style.display = "none";
                    eventArgs.get_menu().get_items().getItem(3).get_element().style.display = "none";
                    eventArgs.get_menu().get_items().getItem(4).get_element().style.display = "none";
                    eventArgs.get_menu().get_items().getItem(5).get_element().style.display = "none";
                    eventArgs.get_menu().get_items().getItem(6).get_element().style.display = "none";
                    eventArgs.get_menu().get_items().getItem(9).get_element().style.display = "none";
                }
            }
            function change() // no ';' here
            {
                var elem = document.getElementById("btnUpdate");
                if (elem.value == "Close Curtain") elem.value = "Update";
                else elem.value = "Add scheme";
            }
        </script>

        <%--   <script type="text/javascript">
            function ShowColumnHeaderMenu(ev, columnName) {
                var grid = $find("<%=gvSchemeDetails.ClientID %>");              
                var columns = grid.get_masterTableView().get_columns();
                for (var i = 0; i < columns.length; i++) {
                    if (columns[i].get_uniqueName() == columnName) {
                        columns[i].showHeaderMenu(ev, 75, 20);
                    }
                }
            }           
        </script>--%>
    </telerik:RadCodeBlock>
    <div class="divPageHeading">
        <table cellspacing="1" cellpadding="3" width="100%">
            <tr>
                <td align="left">
                    <asp:Label ID="lblOrderList" runat="server" CssClass="HeaderTextBig" Text="Add Scheme/Data Translation Mapping"></asp:Label>
                </td>
                <td align="right">
                    <asp:ImageButton Visible="false" ID="btnExportFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                        OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <%--  <div style="border: none">
        <label class="FieldName">
            Scheme Name</label>
        <asp:TextBox ID="txtSchemeName" Width="500px" runat="server" CssClass="txtField"
            AutoComplete="Off" AutoPostBack="True">
        </asp:TextBox><span id="spnCustomer" class="spnRequiredField">*</span>
        <cc1:TextBoxWatermarkExtender ID="txtScheme_water" TargetControlID="txtSchemeName"
            WatermarkText="Enter few chars of Scheme" runat="server" EnableViewState="false">
        </cc1:TextBoxWatermarkExtender>
        <ajaxToolkit:AutoCompleteExtender ID="txtCustomerName_autoCompleteExtender" runat="server"
            TargetControlID="txtSchemeName" ServiceMethod="GetSchemeList" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
            MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
            CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
            CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
            UseContextKey="True" OnClientItemSelected="GetSchemePlanCode" DelimiterCharacters=""
            Enabled="True" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtSchemeName"
            ErrorMessage="<br />Please Enter Scheme Name" Display="Dynamic" runat="server"
            CssClass="rfvPCG" ValidationGroup="Submit"></asp:RequiredFieldValidator>
    </div>--%>
    <div>
        <tr>
            <td>
                <table width="100%">
                    <tr id="trMappingType" runat="server">
                        <td align="right" width="10%">
                            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Select:"></asp:Label>
                        </td>
                        <td align="left" width="10%">
                            <asp:DropDownList ID="ddlMappingType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlMappingType_SelectedIndexChanged"
                                AutoPostBack="true" Width="200px">
                                <asp:ListItem Text="Select">Select</asp:ListItem>
                                <asp:ListItem Text="Scheme Mapping" Value="0">Scheme Mapping</asp:ListItem>
                                <asp:ListItem Text="Data Translation Mapping" Value="1">Data Translation Mapping</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td id="tdmapped" runat="server" visible="false" align="right" width="10%">
                            <asp:Label ID="lblMType" runat="server" Text="Type:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td id="tduMaped" runat="server" visible="false" width="10%">
                            <asp:DropDownList ID="ddlMAapped" runat="server" CssClass="cmbField">
                                <%--<asp:ListItem Text="Select">Select</asp:ListItem>--%>
                                <asp:ListItem Text="Mapped" Value="1">Mapped</asp:ListItem>
                                <asp:ListItem Text="UnMapped" Value="2">UnMapped</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" Text="Go" CssClass="PCGButton"
                                ValidationGroup="btnGo" />
                        </td>
                    </tr>
                    <tr id="trExternalsource" runat="server" visible="false">
                        <td align="right">
                            <asp:Label ID="lblExternalType" runat="server" Text="External Source:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlExternalSource" runat="server" CssClass="cmbField" AutoPostBack="false"
                                Width="200px">
                                <asp:ListItem Text="Select" Value="Select">Select</asp:ListItem>
                                <asp:ListItem Text="CAMS" Value="CAMS"></asp:ListItem>
                                <%-- <asp:ListItem Text="Deutsche" Value="Deutsche">
                                </asp:ListItem>--%>
                                <asp:ListItem Text="Templeton" Value="Templeton">
                                </asp:ListItem>
                                <asp:ListItem Text="KARVY" Value="KARVY">
                                </asp:ListItem>
                                <asp:ListItem Text="Sundaram" Value="Sundaram">
                                </asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="trShemeMapping" runat="server" visible="false">
                        <td align="right" width="10%">
                            <asp:Label ID="lblAMC" runat="server" Text="AMC:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td align="left" width="10%">
                            <asp:DropDownList ID="ddlAMC" runat="server" CssClass="cmbField" AutoPostBack="false">
                            </asp:DropDownList>
                        </td>
                        <td align="right" width="10%">
                            <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td align="left" width="10%">
                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="false">
                            </asp:DropDownList>
                        </td>
                        <td align="right" width="10%">
                            <asp:Label ID="lblType" runat="server" Text="Type:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td align="left" width="10%">
                            <asp:DropDownList ID="ddlSchemeMappingType" runat="server" CssClass="cmbField" AutoPostBack="false">
                                <asp:ListItem Text="All" Value="0">All</asp:ListItem>
                                <asp:ListItem Text="Mapping" Value="1">Mapping</asp:ListItem>
                                <asp:ListItem Text="UnMapping" Value="2">UnMapping</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label3" runat="server" Text="External Source:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlSchemeexternalType" runat="server" CssClass="cmbField" AutoPostBack="false">
                                <asp:ListItem Text="All" Value="0">All</asp:ListItem>
                                <asp:ListItem Text="CAMS" Value="CAMS"></asp:ListItem>
                                <asp:ListItem Text="Deutsche" Value="Deutsche">
                                </asp:ListItem>
                                <asp:ListItem Text="Templeton" Value="Templeton">
                                </asp:ListItem>
                                <asp:ListItem Text="KARVY" Value="KARVY">
                                </asp:ListItem>
                                <asp:ListItem Text="Sundaram" Value="Sundaram">
                                </asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </div>
    <br />
    <asp:Panel ID="pnlgvScheme" Visible="false" runat="server" class="Landscape" ScrollBars="Horizontal">
        <telerik:RadGrid ID="gvSchemeDetails" runat="server" GridLines="None" OnPreRender="gvSchemeDetails_PreRender"
            AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="false"
            ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
            AllowAutomaticUpdates="false" Skin="Telerik" OnItemDataBound="gvSchemeDetails_ItemDataBound"
            OnNeedDataSource="gvSchemeDetails_NeedDataSource" EnableEmbeddedSkins="false"
            OnItemCommand="gvSchemeDetails_ItemCommand" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true"
            AllowFilteringByColumn="true">
            <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="ExistMFInvestlist">
            </ExportSettings>
            <MasterTableView DataKeyNames="PASP_SchemePlanCode,PASC_AMC_ExternalType,PASC_AMC_ExternalCode,PASP_SchemePlanName,PASC_IsOnline"
                EditMode="PopUp" CommandItemDisplay="None" CommandItemSettings-ShowRefreshButton="false"
                Width="102%" CommandItemSettings-AddNewRecordText="Scheme Mapping">
                <Columns>
                    <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                        UpdateText="Edit" HeaderStyle-Width="80px">
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn Visible="true" UniqueName="Type" HeaderStyle-Width="80px"
                        FilterControlWidth="50px" HeaderText="Type" DataField="Type" SortExpression="Type"
                        AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="PASP_SchemePlanName" HeaderStyle-Width="300px"
                        HeaderText="Scheme Plan Name" DataField="PASP_SchemePlanName" SortExpression="PASP_SchemePlanName"
                        CurrentFilterFunction="Contains" AllowFiltering="true" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Visible="true" UniqueName="PASP_SchemePlanCode" HeaderStyle-Width="80px"
                        HeaderText="Scheme Code" DataField="PASP_SchemePlanCode" SortExpression="PASP_SchemePlanCode"
                        AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Visible="true" UniqueName="PA_AMCName" HeaderStyle-Width="200px"
                        HeaderText="AMC" DataField="PA_AMCName" SortExpression="PA_AMCName" AllowFiltering="true"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Visible="true" UniqueName="PASC_IsOnline" HeaderStyle-Width="130px"
                        HeaderText="Is Online" DataField="PASC_IsOnline" SortExpression="PASC_IsOnline"
                        AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Visible="true" UniqueName="PAIC_AssetInstrumentCategoryName"
                        HeaderStyle-Width="120px" HeaderText="Category" DataField="PAIC_AssetInstrumentCategoryName"
                        SortExpression="PAIC_AssetInstrumentCategoryName" AllowFiltering="true" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="External Code"
                        HeaderStyle-Width="99px" DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode"
                        AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalType" HeaderText="RTA" HeaderStyle-Width="99px"
                        DataField="PASC_AMC_ExternalType" SortExpression="PASC_AMC_ExternalType" AllowFiltering="true"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <%--  <telerik:GridTemplateColumn DataField="PASC_AMC_ExternalType" UniqueName="ShipCity"
                        InitializeTemplatesFirst="false" HeaderStyle-Width="100" HeaderText="Ship city">
                        <HeaderTemplate>
                            <table>
                                <tr>
                                <td>
                                <asp:Label ID="lblCity" runat="server" Text="ExternalType"></asp:Label>
                                </td>
                                   <td>
                                        <img src="../Images/rollback.png" style="margin-top: 5px; margin-left: 5px; cursor: pointer"
                                            onclick='ShowColumnHeaderMenu(event,"PASC_AMC_ExternalType")' alt="Show context menu" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCity" runat="server" Text='<%#Eval("PASC_AMC_ExternalType") %>'></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>--%>
                    <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete this Scheme?"
                        ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                        Text="Delete" HeaderStyle-Width="100px">
                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                    </telerik:GridButtonColumn>
                </Columns>
                <%--     <EditFormSettings FormTableStyle-Height="100%" EditFormType="Template" PopUpSettings-Height="505px"
                PopUpSettings-Width="810px" FormMainTableStyle-Width="3500px">--%>
                <EditFormSettings EditFormType="Template" FormTableStyle-Width="800px" PopUpSettings-Width="530px"
                    PopUpSettings-Height="200px" CaptionFormatString="Scheme Mapping">
                    <FormTemplate>
                        <table id="Table2" cellspacing="2" cellpadding="1" border="0" rules="none" style="border-collapse: collapse;
                            background: white;">
                            <tr class="EditFormHeader">
                                <td colspan="2" style="font-size: small">
                                    <asp:Label ID="EditFormHeader" runat="server" CssClass="HeaderTextSmall" Text="Scheme Mapping"
                                        Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="Table3" cellspacing="1" cellpadding="1" border="0" class="module">
                                        <tr>
                                            <td colspan="5">
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trSchemeDDL" visible="false">
                                            <td align="right">
                                                <asp:Label ID="lblMemberAddMode" Text="Scheme Plan Code:" CssClass="FieldName" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtField" ID="txtSchemePlanCodeForEditForm" Text='<%# Bind("PASP_SchemePlanCode") %>'
                                                    runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="tr1">
                                            <td align="right">
                                                <asp:Label ID="Label2" Text="Scheme Plan Name:" CssClass="FieldName" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label CssClass="FieldName" ID="txtSchemePlanNameForEditForm" Text='<%# Bind("PASP_SchemePlanName") %>'
                                                    runat="server"></asp:Label>
                                                <%--<asp:TextBox Width="300px" CssClass="txtField" ID="txtSchemePlanNameForEditForm"
                                                        Text='<%# Bind("PASP_SchemePlanName") %>' runat="server" ReadOnly="false"></asp:TextBox>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="tdlblSchemeName" runat="server" align="right">
                                                <asp:Label ID="Label133" Text="External Code:" CssClass="FieldName" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtField" ID="txtExternalCodeForEditForm" Text='<%# Bind("PASC_AMC_ExternalCode") %>'
                                                    runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="td1" runat="server" align="right">
                                                <asp:Label ID="Label1" Text="RTA:" CssClass="FieldName" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <%--                                                <asp:TextBox ID="txtExternalTypeForEditForm" Text='<%# Bind("PASC_AMC_ExternalType") %>' runat="server"></asp:TextBox>
--%>
                                                <asp:DropDownList CssClass="cmbField" runat="server" ID="ddlExternalType" AutoPostBack="true"
                                                    Enabled="true">
                                                    <asp:ListItem Text="Select" Value="Select">
                                                    </asp:ListItem>
                                                    <asp:ListItem Text="CAMS" Value="CAMS">
                                                    </asp:ListItem>
                                                    <%-- <asp:ListItem Text="Deutsche" Value="Deutsche" Enabled="false">
                                                        </asp:ListItem>--%>
                                                    <asp:ListItem Text="Templeton" Value="Templeton">
                                                    </asp:ListItem>
                                                    <asp:ListItem Text="KARVY" Value="KARVY">
                                                    </asp:ListItem>
                                                    <asp:ListItem Text="Sundaram" Value="Sundaram">
                                                    </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblisonline" Text="Is Online:" CssClass="FieldName" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList CssClass="cmbField" runat="server" ID="ddlONline" AutoPostBack="true">
                                                    <asp:ListItem Text="Is Online" Value="1">
                                                    </asp:ListItem>
                                                    <asp:ListItem Text="Is Offline" Value="0">
                                                    </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="btnUpdate" runat="server" Text="Add/Update" CssClass="PCGButton"
                                        CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                    </asp:Button>&nbsp;
                                    <%--<asp:Button Visible="false" ID="btnInsert" Text="Insert" runat="server" CssClass="PCGButton"
                                        CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                    </asp:Button>&nbsp;--%>
                                    <asp:Button ID="btnAddScheme" Text="Scheme Mapping" runat="server" CssClass="PCGButton"
                                        OnClick="btnAddScheme_Click" Visible="false"></asp:Button>
                                    <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                                        CommandName="Cancel"></asp:Button>
                                    <%--    <asp:Button ID="Button3" Text='<%# (Container is GridEditFormInsertItem) ? "Insert":""%>'
                                        runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Insert" %>'>
                                    </asp:Button>&nbsp;--%>
                                </td>
                            </tr>
                        </table>
                    </FormTemplate>
                </EditFormSettings>
            </MasterTableView>
            <ClientSettings ReorderColumnsOnClient="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
                <Scrolling AllowScroll="false" />
                <ClientEvents OnHeaderMenuShowing="HeaderMenuShowing" />
                <Resizing AllowColumnResize="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
        </telerik:RadGrid>
    </asp:Panel>
    <br />
</body>
<div>
    <asp:Panel ID="pnlCamsKarvy" Visible="false" runat="server" class="Landscape" Width="90%"
        ScrollBars="Horizontal">
        <telerik:RadGrid ID="gvCamsKarvy" runat="server" CssClass="RadGrid" GridLines="None"
            Width="90%" AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="false"
            ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
            AllowAutomaticUpdates="false" Skin="Telerik" OnItemDataBound="gvCamsKarvy_ItemDataBound"
            OnNeedDataSource="gvCamsKarvy_NeedDataSource" EnableEmbeddedSkins="false" OnItemCommand="gvCamsKarvy_ItemCommand"
            EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" AllowFilteringByColumn="true">
            <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="ExistMFInvestlist">
            </ExportSettings>
            <MasterTableView DataKeyNames="ClassificationCode,WKDTM_TransactionHead,Transaction_Type,Description,WKDTM_TransactionTypeFlag" EditMode="PopUp" CommandItemDisplay="Top"
                CommandItemSettings-ShowRefreshButton="false" Width="90%" CommandItemSettings-AddNewRecordText="New Karvy Data For Mapping">
                <%-- CommandItemSettings-AddNewRecordText="Add New Scheme For Mapping"--%>
                <Columns>
                    <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                        HeaderStyle-Width="80px" UpdateText="Edit">
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn UniqueName="WKDTM_TransactionHead" HeaderStyle-Width="150px"
                        HeaderText="Source Head" DataField="WKDTM_TransactionHead" SortExpression="WKDTM_TransactionHead"
                        AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="Transaction_Type" HeaderStyle-Width="150px"
                        HeaderText="source code" DataField="Transaction_Type" SortExpression="Transaction_Type"
                        AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Visible="true" UniqueName="Description" HeaderStyle-Width="150px"
                        HeaderText="Source Description" DataField="Description" SortExpression="Description"
                        AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Visible="true" UniqueName="WKDTM_TransactionTypeFlag" HeaderStyle-Width="150px"
                        HeaderText="Source Flag" DataField="WKDTM_TransactionTypeFlag" SortExpression="WKDTM_TransactionTypeFlag"
                        AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Visible="true" UniqueName="ClassificationCode" HeaderStyle-Width="150px"
                        HeaderText="System Translation Code" DataField="ClassificationCode" SortExpression="ClassificationCode"
                        AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Visible="true" UniqueName="WMTT_TransactionClassificationName"
                        HeaderStyle-Width="150px" HeaderText="System Translation Description" DataField="WMTT_TransactionClassificationName"
                        SortExpression="WMTT_TransactionClassificationName" AllowFiltering="true" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Visible="false" UniqueName="Type" HeaderStyle-Width="150px"
                        HeaderText="Type" DataField="Type" SortExpression="Type" AllowFiltering="true"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                </Columns>
                <EditFormSettings EditFormType="Template" FormTableStyle-Width="600px" CaptionFormatString="Data Translator Mapping">
                    <FormTemplate>
                        <table id="Table2" cellspacing="2" cellpadding="1" border="0" rules="none" style="border-collapse: collapse;
                            background: white;">
                            <tr class="EditFormHeader">
                                <td colspan="2" style="font-size: small">
                                    <asp:Label ID="EditFormHeader" runat="server" CssClass="HeaderTextSmall" Text="Data Translator Mapping"
                                        Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="Table3" cellspacing="1" cellpadding="1" border="0" class="module">
                                        <tr>
                                            <td colspan="5">
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trSchemeDDL" visible="false">
                                            <td align="right">
                                                <asp:Label ID="lblMemberAddMode" Text="Source Head:" CssClass="FieldName" runat="server">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label5" Text="Source Head:" CssClass="FieldName" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtField" ID="txtHead" Text='<%# Bind("WKDTM_TransactionHead") %>'
                                                    runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="tr1">
                                            <td align="right">
                                                <asp:Label ID="Label2" Text="Source Type:" CssClass="FieldName" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox Width="150px" CssClass="txtField" ID="txttransactiontype" Text='<%# Bind("Transaction_Type") %>'
                                                    runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="tdlblSchemeName" runat="server" align="right">
                                                <asp:Label ID="Label133" Text="Source Description:" CssClass="FieldName" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtField" ID="txtDescription" MaxLength="250" 
                                                    runat="server" Width="300px" ></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="td2" runat="server" align="right">
                                                <asp:Label ID="Label4" Text="Source Flag:" CssClass="FieldName" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtField" ID="txtsourceflag" Text='<%# Bind("WKDTM_TransactionTypeFlag") %>'
                                                    runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="td1" runat="server" align="right">
                                                <asp:Label ID="Label1" Text="System Translation Description:" CssClass="FieldName"
                                                    runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlclassificationCode" CssClass="cmbField" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                        runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                    </asp:Button>&nbsp;
                                    <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                                        CommandName="Cancel"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </FormTemplate>
                </EditFormSettings>
            </MasterTableView>
            <ClientSettings ReorderColumnsOnClient="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
                <Scrolling AllowScroll="false" />
                <ClientEvents OnHeaderMenuShowing="HeaderMenuShowing" />
                <Resizing AllowColumnResize="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
        </telerik:RadGrid>
    </asp:Panel>
</div>
<div>
    <asp:Panel ID="pnlCams" Visible="false" runat="server" class="Landscape" Width="75%"
        ScrollBars="Horizontal">
        <telerik:RadGrid ID="gvCams" runat="server" CssClass="RadGrid" GridLines="None" Width="75%"
            AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="false"
            ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
            AllowAutomaticUpdates="false" Skin="Telerik" OnItemDataBound="gvCams_ItemDataBound"
            OnNeedDataSource="gvCams_NeedDataSource" EnableEmbeddedSkins="false" OnItemCommand="gvCams_ItemCommand"
            EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" AllowFilteringByColumn="true">
            <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="ExistMFInvestlist">
            </ExportSettings>
            <MasterTableView DataKeyNames="ClassificationCode,Transaction_Type,Description" EditMode="PopUp"
                CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="false" Width="75%"
                CommandItemSettings-AddNewRecordText="New Cams Data For Mapping">
                <Columns>
                    <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                        HeaderStyle-Width="80px" UpdateText="Edit">
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn UniqueName="Transaction_Type" HeaderStyle-Width="150px"
                        HeaderText="Source Code" DataField="Transaction_Type" SortExpression="Transaction_Type"
                        AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Visible="true" UniqueName="Description" HeaderStyle-Width="150px"
                        HeaderText="Source Description" DataField="Description" SortExpression="Description"
                        AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Visible="true" UniqueName="ClassificationCode" HeaderStyle-Width="150px"
                        HeaderText="System Translation Code" DataField="ClassificationCode" SortExpression="ClassificationCode"
                        AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Visible="true" UniqueName="WMTT_TransactionClassificationName"
                        HeaderStyle-Width="150px" HeaderText="System Translation Description" DataField="WMTT_TransactionClassificationName"
                        SortExpression="WMTT_TransactionClassificationName" AllowFiltering="true" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Visible="false" UniqueName="Type" HeaderStyle-Width="150px"
                        HeaderText="Type" DataField="Type" SortExpression="Type" AllowFiltering="true"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                </Columns>
                <EditFormSettings EditFormType="Template" FormTableStyle-Width="600px" CaptionFormatString="Data Translator Mapping">
                    <FormTemplate>
                        <table id="Table2" cellspacing="2" cellpadding="1" border="0" rules="none" style="border-collapse: collapse;
                            background: white;">
                            <tr class="EditFormHeader">
                                <td colspan="2" style="font-size: small">
                                    <asp:Label ID="EditFormHeader" runat="server" CssClass="HeaderTextSmall" Text="Data Translator Mapping"
                                        Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="Table3" cellspacing="1" cellpadding="1" border="0" class="module">
                                        <tr>
                                            <td colspan="5">
                                            </td>
                                        </tr>
                                        <%-- <tr runat="server" id="trSchemeDDL" visible="false">
                                            <td align="right">
                                                <asp:Label ID="lblMemberAddMode" Text="Scheme Plan Code:" CssClass="FieldName" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtField" ID="txtSchemePlanCodeForEditForm" Text='<%# Bind("PASP_SchemePlanCode") %>'
                                                    runat="server"></asp:TextBox>
                                            </td>
                                        </tr>--%>
                                        <tr runat="server" id="tr1">
                                            <td align="right">
                                                <asp:Label ID="Label2" Text="Source Type:" CssClass="FieldName" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox Width="150px" CssClass="txtField" ID="txttransactiontype" Text='<%# Bind("Transaction_Type") %>'
                                                    runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="tdlblSchemeName" runat="server" align="right">
                                                <asp:Label ID="Label133" Text="Source Description:" CssClass="FieldName" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtField" MaxLength="250" ID="txtDescription" 
                                                    runat="server" Width="300px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="td1" runat="server" align="right">
                                                <asp:Label ID="Label1" Text="Destination Descriotion:" CssClass="FieldName" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlclassificationCode" CssClass="cmbField" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                        runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                    </asp:Button>&nbsp;
                                    <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                                        CommandName="Cancel"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </FormTemplate>
                </EditFormSettings>
            </MasterTableView>
            <ClientSettings ReorderColumnsOnClient="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
                <Scrolling AllowScroll="false" />
                <ClientEvents OnHeaderMenuShowing="HeaderMenuShowing" />
                <Resizing AllowColumnResize="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
        </telerik:RadGrid>
    </asp:Panel>
</div>
<div>
    <asp:Panel ID="pnltempleton" Visible="false" runat="server" class="Landscape" Width="70%"
        ScrollBars="Horizontal">
        <telerik:RadGrid ID="gvTempleton" runat="server" CssClass="RadGrid" GridLines="None"
            AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="false"
            ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
            Width="70%" AllowAutomaticUpdates="false" Skin="Telerik" OnItemDataBound="gvTempleton_ItemDataBound"
            OnNeedDataSource="gvTempleton_NeedDataSource" EnableEmbeddedSkins="false" OnItemCommand="gvTempleton_ItemCommand"
            EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" AllowFilteringByColumn="true">
            <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="ExistMFInvestlist">
            </ExportSettings>
            <MasterTableView DataKeyNames="ClassificationCode,Transaction_Type" EditMode="PopUp" CommandItemDisplay="Top"
                CommandItemSettings-ShowRefreshButton="false" Width="100%" CommandItemSettings-AddNewRecordText="New Templeton Data For Mapping">
                <Columns>
                    <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                        HeaderStyle-Width="80px" UpdateText="Edit">
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn UniqueName="Transaction_Type" HeaderStyle-Width="100px"
                        HeaderText="source code" DataField="Transaction_Type" SortExpression="Transaction_Type"
                        AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Visible="true" UniqueName="ClassificationCode" HeaderStyle-Width="80px"
                        HeaderText="System Translation Code" DataField="ClassificationCode" SortExpression="ClassificationCode"
                        AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Visible="true" UniqueName="WMTT_TransactionClassificationName"
                        HeaderStyle-Width="100px" HeaderText="System Translation Description" DataField="WMTT_TransactionClassificationName"
                        SortExpression="WMTT_TransactionClassificationName" AllowFiltering="true" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn Visible="false" UniqueName="Type" HeaderStyle-Width="100px"
                        HeaderText="Type" DataField="Type" SortExpression="Type" AllowFiltering="true"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <%--   <telerik:GridBoundColumn Visible="true" UniqueName="ClassificationCode" HeaderStyle-Width="200px"
                        HeaderText="System Translation Code" DataField="ClassificationCode" SortExpression="ClassificationCode"
                        AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>  --%>
                </Columns>
                <EditFormSettings EditFormType="Template" FormTableStyle-Width="600px" CaptionFormatString="Data Translator Mapping">
                    <FormTemplate>
                        <table id="Table2" cellspacing="2" cellpadding="1" border="0" rules="none" style="border-collapse: collapse;
                            background: white;">
                            <tr class="EditFormHeader">
                                <td colspan="2" style="font-size: small">
                                    <asp:Label ID="EditFormHeader" runat="server" CssClass="HeaderTextSmall" Text="Data Translator Mapping"
                                        Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="Table3" cellspacing="1" cellpadding="1" border="0" class="module">
                                        <tr>
                                            <td colspan="5">
                                            </td>
                                        </tr>
                                        <tr runat="server" id="tr1">
                                            <td align="right">
                                                <asp:Label ID="Label2" Text="Source Type:" CssClass="FieldName" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox Width="300px" CssClass="txtField" ID="txttransactiontype" Text='<%# Bind("Transaction_Type") %>'
                                                    runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="td1" runat="server">
                                                <asp:Label ID="Label1" Text="System Translation Description:" CssClass="FieldName"
                                                    runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlclassificationCode" CssClass="cmbField" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                        runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                    </asp:Button>&nbsp;
                                    <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                                        CommandName="Cancel"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </FormTemplate>
                </EditFormSettings>
            </MasterTableView>
            <ClientSettings ReorderColumnsOnClient="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
                <Scrolling AllowScroll="false" />
                <ClientEvents OnHeaderMenuShowing="HeaderMenuShowing" />
                <Resizing AllowColumnResize="true" />
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
        </telerik:RadGrid>
    </asp:Panel>
</div>
<div>
    <asp:Label ID="lblNote" runat="server" CssClass="HeaderTextSmall" Text="Note: Please reprocess the upload details in order to complete the upload process"></asp:Label>
</div>
<br />
<div>
    <%--<asp:HiddenField ID="txtSchemePlanCode" runat="server" OnValueChanged="txtSchemePlanCode_ValueChanged1" />--%>
    <asp:HiddenField ID="hdnSchemePlanCode" runat="server" />
    <asp:HiddenField ID="hdnCategory" runat="server" />
    <asp:HiddenField ID="hdnAMC" runat="server" />
    <asp:HiddenField ID="hdnType" runat="server" />
    <asp:HiddenField ID="hdnExternalSource" runat="server" />
    <asp:HiddenField ID="hdnmtype" runat="server" />
