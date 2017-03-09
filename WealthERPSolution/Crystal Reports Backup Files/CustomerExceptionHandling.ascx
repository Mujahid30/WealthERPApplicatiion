<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerExceptionHandling.ascx.cs"
    Inherits="WealthERP.Advisor.CustomerExceptionHandling" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<body class="BODY">
    <asp:ScriptManager ID="scptMgr" runat="server" EnablePartialRendering="true">
        <Services>
            <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
        </Services>
    </asp:ScriptManager>
    <script type="text/javascript" language="javascript">
        function GetCustomerId(source, eventArgs) {
            document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value();
            return false;
        };
    </script>
    <table>
        <tr>
            <td colspan="6">
                <div class="divPageHeading">
                    <table cellspacing="0" cellpadding="3" width="100%">
                        <tr>
                            <td align="left">
                                Customer Accounts Compare
                            </td>
                            <td align="right">
                                <asp:ImageButton  ID="btnExportFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                    OnClientClick="setFormat('excel')" Height="20px" Width="20px"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td align="right" width="10%">
                <asp:Label ID="lblExceptionType" runat="server" CssClass="FieldName" Text="Compare"></asp:Label>
            </td>
            <td align="left" style="width: 10%;">
                <asp:DropDownList ID="ddlExpType" runat="server" Style="vertical-align: middle" AutoPostBack="true"
                    CssClass="cmbField" OnSelectedIndexChanged="ddlExpType_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="right" width="10%">
                <asp:Label ID="lblExceptionList" runat="server" CssClass="FieldName" Text="Fields to compare"></asp:Label>
            </td>
            <td align="left" style="width: 10%;">
                <asp:DropDownList ID="ddlExpList" runat="server" Style="vertical-align: middle" AutoPostBack="true"
                    CssClass="cmbField" OnSelectedIndexChanged="ddlExpList_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="right" width="10%">
                <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Display:"></asp:Label>
            </td>
            <td align="left" style="width: 10%;">
                <asp:DropDownList ID="ddlDisplay" runat="server" Style="vertical-align: middle" AutoPostBack="true"
                    CssClass="cmbField" OnSelectedIndexChanged="ddlDisplay_SelectedIndexChanged">
                    <asp:ListItem Text="All" Value="ALL">
                    </asp:ListItem>
                    <asp:ListItem Text="MisMatch Only" Value="MISMATCH">
                    </asp:ListItem>
                </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td align="right" width="10%">
                <asp:Label ID="lblGrpOrInd" runat="server" CssClass="FieldName" Text="Select:"></asp:Label>
            </td>
            <td id="tdSelectCusto" runat="server" align="left" width="10%">
                <asp:DropDownList ID="ddlSelectCustomer" runat="server" CssClass="cmbField" Style="vertical-align: middle"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlSelectCustomer_SelectedIndexChanged">
                    <asp:ListItem Value="All Customer" Text="All Customer"></asp:ListItem>
                    <asp:ListItem Value="Pick Customer" Text="Pick Customer"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="right" width="10%">
                <asp:Label ID="lblSelectTypeOfCustomer" runat="server" CssClass="FieldName" Text="Customer Type: "></asp:Label>
            </td>
            <td align="left" width="10%">
                <asp:DropDownList ID="ddlSelectCutomer" Style="vertical-align: middle" runat="server"
                    CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectCutomer_SelectedIndexChanged">
                    <asp:ListItem Value="Select" Text="Select" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="Group Head" Text="Group Head"></asp:ListItem>
                    <asp:ListItem Value="Individual" Text="Individual"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="right" width="10%">
                <asp:Label ID="lblselectCustomer" runat="server" CssClass="FieldName" Text="Search Customer: "></asp:Label>
            </td>
            <td align="left" width="10%">
                <asp:TextBox ID="txtIndividualCustomer" runat="server" CssClass="txtField" AutoComplete="Off"
                    AutoPostBack="True">  </asp:TextBox>
                <cc1:TextBoxWatermarkExtender ID="txtIndividualCustomer_water" TargetControlID="txtIndividualCustomer"
                    WatermarkText="Enter few chars of Customer" runat="server" EnableViewState="false">
                </cc1:TextBoxWatermarkExtender>
                <ajaxToolkit:AutoCompleteExtender ID="txtIndividualCustomer_autoCompleteExtender"
                    runat="server" TargetControlID="txtIndividualCustomer" ServiceMethod="GetCustomerName"
                    ServicePath="~/CustomerPortfolio/AutoComplete.asmx" MinimumPrefixLength="1" EnableCaching="False"
                    CompletionSetCount="5" CompletionInterval="100" CompletionListCssClass="AutoCompleteExtender_CompletionList"
                    CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                    UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                    Enabled="True" />
               <asp:RequiredFieldValidator ID="rquiredFieldValidatorIndivudialCustomer" Display="Dynamic"
                    ControlToValidate="txtIndividualCustomer" CssClass="rfvPCG" ErrorMessage="<br />Please select the customer"
                    runat="server" ValidationGroup="btnGo">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="trBranchRM" runat="server">
            <td align="right" width="10%">
                <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch:"></asp:Label>
            </td>
            <td align="left" style="width: 10%;">
                <asp:DropDownList ID="ddlBranch" runat="server" Style="vertical-align: middle" AutoPostBack="true"
                    CssClass="cmbField" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="right" style="width: 10%;">
                <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM:"></asp:Label>
            </td>
            <td align="left" style="width: 10%;">
                <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" Style="vertical-align: middle">
                </asp:DropDownList>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:Button ID="btnGo" runat="server" Text="Go" ValidationGroup="btnGo" CssClass="PCGButton"
                    OnClick="btnGo_Click" />
            </td>
        </tr>
    </table>
    <div>
        <telerik:RadGrid ID="gvExceptionReport" runat="server" CssClass="RadGrid" GridLines="Both"
            Width="100%" AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="false"
            ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
            AllowAutomaticUpdates="false" Skin="Telerik" OnItemDataBound="gvExceptionReport_ItemDataBound"
            OnItemCommand="gvExceptionReport_ItemCommand" OnNeedDataSource="gvExceptionReport_NeedDataSource"
            AllowFilteringByColumn="true">
            <ExportSettings HideStructureColumns="false" ExportOnlyData="true">
            </ExportSettings>
            <MasterTableView EditMode="PopUp" CommandItemDisplay="None" CommandItemSettings-ShowRefreshButton="false"
                CommandItemSettings-AddNewRecordText="Select MF Investment" DataKeyNames="CustomerId">
                <Columns>
                    <telerik:GridEditCommandColumn EditText="Update" HeaderStyle-Width="70px" UniqueName="editColumn"
                        UpdateText="Update">
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn UniqueName="CustomerId" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderText="Customer Id" DataField="CustomerId"
                        SortExpression="CustomerId" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn UniqueName="CustomerName" ItemStyle-HorizontalAlign="left"
                        HeaderStyle-Width="100px" HeaderText="Customer" DataField="CustomerName"
                        SortExpression="CustomerName" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    
                     <telerik:GridBoundColumn UniqueName="Folioname" ItemStyle-HorizontalAlign="left"
                        HeaderStyle-Width="100px" HeaderText="Folio Name" DataField="Folioname"
                        SortExpression="Folioname" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                   <telerik:GridBoundColumn UniqueName="FolioNumber" HeaderText="Folio No." DataField="FolioNumber"
                        HeaderStyle-Width="100px" SortExpression="FolioNumber" AllowFiltering="true"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="Exception" HeaderText="Exception" DataField="Exception" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="100px" SortExpression="Exception" AllowFiltering="true" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                   
                    <telerik:GridBoundColumn UniqueName="ProfileFolio" HeaderText="Profile Folio" DataField="ProfileFolio"
                        HeaderStyle-Width="100px" SortExpression="ProfileFolio" AllowFiltering="true"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                     
                    <telerik:GridBoundColumn UniqueName="Exceptionlist" HeaderText="Exception List" DataField="Exceptionlist"
                        HeaderStyle-Width="100px" SortExpression="Exceptionlist" AllowFiltering="true"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        <HeaderStyle></HeaderStyle>
                    </telerik:GridBoundColumn>
                </Columns>
                <EditFormSettings EditFormType="Template" FormTableStyle-Width="600px">
                    <FormTemplate>
                        <table id="Table2" cellspacing="2" cellpadding="1" border="0" rules="none" style="border-collapse: collapse;
                            background: white;">
                            <tr class="EditFormHeader">
                                <td colspan="2" style="font-size: small">
                                    <asp:Label ID="EditFormHeader" runat="server" CssClass="HeaderTextSmall" Text="Update to Profile"></asp:Label>
                                </td>
                                <td>
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
                                                <asp:Label ID="lblMemberAddMode" Text="Profile Data" CssClass="FieldName" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtField" ID="txtProfileDataForEditForm" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="tr1">
                                            <td align="right">
                                                <asp:Label ID="Label2" Text="FolioData" CssClass="FieldName" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox Width="300px" CssClass="txtField" ID="txtFolioDataForEditForm" Text='<%# Bind("Exception") %>'
                                                    runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trFn" runat="server" visible="false">
                                            <td id="tdlblSchemeName" runat="server" align="right">
                                                <asp:Label ID="Label133" Text="Folio Number" CssClass="FieldName" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtField" ID="txtFolioNumberForEditForm" Text='<%# Bind("FolioNumber") %>'
                                                    runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trcustomer" runat="server" visible="false">
                                            <td id="td1" runat="server" align="right">
                                                <asp:Label ID="lblcustomer" Text="Customer Id" CssClass="FieldName" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="txtField" ID="txtCustomerIdForEditForm" Text='<%# Bind("CustomerId") %>'
                                                    runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td colspan="2"><asp:Label ID="Label3" Text="Following will be updated in the Profile" CssClass="FieldName" runat="server">
                                                </asp:Label>
                                                
                                        </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="2">
                                                <asp:Button ID="Button1" Text="Ok" runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                </asp:Button>&nbsp;
                                                <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                                                    CommandName="Cancel"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </FormTemplate>
                </EditFormSettings>
            </MasterTableView>
            <ClientSettings>
            </ClientSettings>
        </telerik:RadGrid>
    </div>
    </body>
<asp:HiddenField ID="hdnCustomerId" runat="server" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnadviserId" runat="server" />
<asp:HiddenField ID="hdnAll" runat="server" />
<asp:HiddenField ID="hdnbranchId" runat="server" />
<asp:HiddenField ID="hdnbranchheadId" runat="server" />
<asp:HiddenField ID="hdnrmId" runat="server" />
<asp:HiddenField ID="hdnExptype" runat="server" />
<asp:HiddenField ID="hdnExplist" runat="server" />
<asp:HiddenField ID="hdnIndividualOrGroup" runat="server" />
<asp:HiddenField ID="hdnMismatch" runat="server" />