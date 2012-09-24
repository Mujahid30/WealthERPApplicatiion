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
            function GetSchemePlanCode(source, eventArgs) {
                document.getElementById("<%= txtSchemePlanCode.ClientID %>").value = eventArgs.get_value();
                return false;
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
                    <asp:Label ID="lblOrderList" runat="server" CssClass="HeaderTextBig" Text="Add Scheme Mapping"></asp:Label>
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
    <div style="border: none">
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
    </div>
    <br />
    <div>
        <telerik:RadGrid ID="gvSchemeDetails" runat="server" CssClass="RadGrid" GridLines="None"
            Width="100%" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false"
            ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
            AllowAutomaticUpdates="false" Skin="Telerik" OnItemDataBound="gvSchemeDetails_ItemDataBound"
            EnableEmbeddedSkins="false" OnItemCommand="gvSchemeDetails_ItemCommand" EnableHeaderContextMenu="true"
            EnableHeaderContextFilterMenu="true" AllowFilteringByColumn="true">
            <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="ExistMFInvestlist">
            </ExportSettings>
            <MasterTableView DataKeyNames="PASP_SchemePlanCode,PASC_AMC_ExternalType,PASC_AMC_ExternalCode,PASP_SchemePlanName" EditMode="PopUp"
                CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="false" CommandItemSettings-AddNewRecordText="Add New Scheme For Mapping">
                <Columns>
                    <telerik:GridEditCommandColumn EditText="Update" UniqueName="editColumn" CancelText="Cancel"
                        UpdateText="Update">
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn UniqueName="PASP_SchemePlanName" HeaderStyle-Width="500px" HeaderText="Scheme Plan Name"
                        DataField="PASP_SchemePlanName" SortExpression="PASP_SchemePlanName" AllowFiltering="true"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                   <telerik:GridBoundColumn Visible="false" UniqueName="PASP_SchemePlanCode" HeaderStyle-Width="500px" HeaderText="SchemePlanCode"
                        DataField="PASP_SchemePlanCode" SortExpression="PASP_SchemePlanCode" AllowFiltering="true"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    
                    <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="External Code"
                        DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalType" HeaderText="External Type"
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
                        Text="Delete">
                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                    </telerik:GridButtonColumn>
                </Columns>
                <EditFormSettings EditFormType="Template" FormTableStyle-Width="600px">
                    <FormTemplate>
                        <table id="Table2" cellspacing="2" cellpadding="1"  border="0" rules="none"
                            style="border-collapse: collapse; background: white;">
                            <tr class="EditFormHeader">
                                <td colspan="2" style="font-size: small">
                                    <asp:Label ID="EditFormHeader" runat="server" CssClass="HeaderTextSmall" Text="MF Investment Funding"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="Table3"  cellspacing="1" cellpadding="1" border="0" class="module">
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
                                                <asp:TextBox Width="300px" CssClass="txtField" ID="txtSchemePlanNameForEditForm" Text='<%# Bind("PASP_SchemePlanName") %>'
                                                    runat="server"></asp:TextBox>
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
                                                <asp:Label ID="Label1" Text="External Type:" CssClass="FieldName" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <%--                                                <asp:TextBox ID="txtExternalTypeForEditForm" Text='<%# Bind("PASC_AMC_ExternalType") %>' runat="server"></asp:TextBox>
--%>
                                                <asp:DropDownList CssClass="cmbField" runat="server" ID="ddlExternalType">
                                                    <asp:ListItem Text="CAMS" Value="CAMS">
                                                    </asp:ListItem>
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
    </div>
    <br />
    <div>
        <asp:Label ID="lblNote" runat="server" CssClass="HeaderTextSmall" Text="Note: Please reprocess the upload details in order to completed the upload process"></asp:Label>
    </div>
</body>
<asp:HiddenField ID="txtSchemePlanCode" runat="server" OnValueChanged="txtSchemePlanCode_ValueChanged1" />
<asp:HiddenField ID="hdnSchemePlanCode" runat="server" />
