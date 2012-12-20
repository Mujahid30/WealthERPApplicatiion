<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewBankDetails.ascx.cs"
    Inherits="WealthERP.Customer.ViewBankDetails" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<table style: width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            Bank Details
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="imgBtnrgHoldings" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
                        </td>
                        <td style="width: 10px" align="right">
                            <img src="../Images/helpImage.png" height="20px" width="25px" style="float: right;"
                                class="flip" />
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr id="trMessage" runat="server" visible="false">
        <td>
            <asp:Label ID="lblMessage" runat="server" Text="No Records Found..." CssClass="Error"></asp:Label>
        </td>
    </tr>
</table>
<br />
<div style="width: 100%; padding-left:4px; padding-right: 10px;">
    <telerik:RadGrid ID="gvBankDetails" runat="server" GridLines="None" 
        Width="99%" AllowPaging="true" PageSize="10" AllowSorting="True" AutoGenerateColumns="false"
        ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
        AllowAutomaticUpdates="false" Skin="Telerik" OnItemDataBound="gvBankDetails_ItemDataBound"
        EnableEmbeddedSkins="true" OnItemCommand="gvBankDetails_ItemCommand" EnableHeaderContextMenu="true"
        EnableHeaderContextFilterMenu="true" AllowFilteringByColumn="true" OnNeedDataSource="gvBankDetails_NeedDataSource" EditItemStyle-Width="600px">
        <%-- ,ModeOfHoldingCode,BankAccountTypeCode,CB_BranchAdrState" --%>
        <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView DataKeyNames="CB_CustBankAccId,XMOH_ModeOfHoldingCode,XBAT_BankAccountTypeCode,CB_BranchAdrState,WERPBM_BankCode"
            EditMode="PopUp" CommandItemDisplay="Top">
            <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false" AddNewRecordText="Add New Bank Details"
                ShowRefreshButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="true" ShowExportToPdfButton="false" />
            <%--,ModeOfHoldingCode,BankAccountTypeCode,CB_BranchAdrState--%>
            <Columns>
                <telerik:GridEditCommandColumn Visible="true" HeaderStyle-Width="50px" EditText="View/Edit"
                    UniqueName="editColumn" CancelText="Cancel" UpdateText="Update">
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn  Visible="false" UniqueName="CB_CustBankAccId" HeaderStyle-Width="80px" HeaderText="CB_CustBankAccId"
                    DataField="CB_CustBankAccId" SortExpression="CB_CustBankAccId" AllowFiltering="false"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="WERPBMBankName" HeaderStyle-Width="80px" HeaderText="Bank" 
                 DataField="WERPBMBankName" SortExpression="WERPBMBankName" AllowFiltering="false" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CB_BranchName" HeaderStyle-Width="80px" HeaderText="Branch"
                    DataField="CB_BranchName" SortExpression="CB_BranchName" AllowFiltering="false"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="BankAccountType" HeaderStyle-Width="80px"
                    HeaderText="Account Type" DataField="BankAccountType" SortExpression="BankAccountType"
                    AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="XMOH_ModeOfHolding" HeaderStyle-Width="80px"
                    HeaderText="Mode Of Operation" DataField="XMOH_ModeOfHolding" SortExpression="XMOH_ModeOfHolding"
                    AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CB_AccountNum" HeaderStyle-Width="80px" HeaderText="Account No."
                    DataField="CB_AccountNum" SortExpression="CB_AccountNum" AllowFiltering="false"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                </telerik:GridBoundColumn>
                <telerik:GridButtonColumn Visible="true" UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete this Record?"
                    ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete" HeaderStyle-Width="80px"
                    Text="Delete">
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
                <telerik:GridBoundColumn Visible="false" UniqueName="CB_IFSC" HeaderStyle-Width="150px"
                    HeaderText="IFSC" DataField="CB_IFSC" SortExpression="CB_IFSC" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" UniqueName="CB_MICR" HeaderStyle-Width="150px"
                    HeaderText="MICR" DataField="CB_MICR" SortExpression="CB_MICR" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" UniqueName="CB_BranchAdrCountry" HeaderStyle-Width="150px"
                    HeaderText="Branch Country" DataField="CB_BranchAdrCountry" SortExpression="CB_BranchAdrCountry"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" UniqueName="CB_BranchAdrState" HeaderStyle-Width="150px"
                    HeaderText="Branch State" DataField="CB_BranchAdrState" SortExpression="CB_BranchAdrState"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" UniqueName="CB_BranchAdrCity" HeaderStyle-Width="150px"
                    HeaderText="Branch City" DataField="CB_BranchAdrCity" SortExpression="CB_BranchAdrCity"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" UniqueName="CB_BranchAdrPinCode" HeaderStyle-Width="150px"
                    HeaderText="PinCode" DataField="CB_BranchAdrPinCode" SortExpression="CB_BranchAdrPinCode"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" UniqueName="CB_BranchAdrLine3" HeaderStyle-Width="150px"
                    HeaderText="Branch Line3" DataField="CB_BranchAdrLine3" SortExpression="CB_BranchAdrLine3"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" UniqueName="CB_BranchAdrLine2" HeaderStyle-Width="150px"
                    HeaderText="Branch Line2" DataField="CB_BranchAdrLine2" SortExpression="CB_BranchAdrLine2"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" UniqueName="CB_BranchAdrLine1" HeaderStyle-Width="150px"
                    HeaderText="Branch Line1" DataField="CB_BranchAdrLine1" SortExpression="CB_BranchAdrLine1"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
            </Columns>
            <EditFormSettings FormTableStyle-Height="20px" EditFormType="Template" PopUpSettings-Height="260px" PopUpSettings-Width="600px" FormMainTableStyle-Width=1000px >
                     <FormTemplate>
                                    <table width="100%" style="background-color: White" border="0" >
                                        <tr>
                                            <td colspan="4">
                                                <div class="divSectionHeading" style="vertical-align: text-bottom">
                                                    Bank Details
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblAccountType" runat="server" CssClass="FieldName" Text="Account Type:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                                <span id="Span1" class="spnRequiredField">*</span>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlAccountType"
                                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please select a Account Type"
                                    Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblAccountNumber" runat="server" CssClass="FieldName" Text="Account Number:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="txtField" Text='<%# Bind("CB_AccountNum") %>'></asp:TextBox>
                                <span id="spAccountNumber" class="spnRequiredField">*</span>
                                <asp:RequiredFieldValidator ID="rfvAccountNumber" ControlToValidate="txtAccountNumber"
                                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please enter a Account Number"
                                    Display="Dynamic" runat="server" CssClass="rfvPCG">
                                </asp:RequiredFieldValidator>
                            </td>
                           
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblModeOfOperation" runat="server" Text="Mode of Operation:" CssClass="FieldName"></asp:Label>
                            </td>
                             <td class="rightField">
                                <asp:DropDownList ID="ddlModeofOperation" runat="server"  CssClass="cmbField">
                                </asp:DropDownList>
                                <span id="Span2" class="spnRequiredField">*</span>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlModeofOperation"
                                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please select a ModeofHolding"
                                    Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblBankName" runat="server" Text="Bank Name:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                             <asp:DropDownList ID="ddlBankName" CssClass="cmbField" runat="server">
                                </asp:DropDownList>
                                <span id="Span3" class="spnRequiredField">*</span>
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlBankName"
                                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please select a Bank Name"
                                    Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                                <%--<asp:TextBox ID="txtBankName" runat="server" CssClass="txtField" Style="width: 250px;"
                                    Text='<%# Bind("CB_BankName") %>'></asp:TextBox>
                                <span id="spBankName" class="spnRequiredField">*</span>
                                <asp:RequiredFieldValidator ID="rfvBankName" ControlToValidate="txtBankName" ErrorMessage="<br />Please enter a Bank Name"
                                    Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="btnSubmit">--%>
                               <%-- </asp:RequiredFieldValidator>--%>
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblBranchName" runat="server" Text="Branch Name:" CssClass="FieldName"></asp:Label>
                                     </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtBranchName" runat="server" CssClass="txtField" Style="width: 225px;"
                                    Text='<%# Bind("CB_BranchName") %>'></asp:TextBox>
                                <span id="spBranchName" class="spnRequiredField">*</span>
                                <asp:RequiredFieldValidator ID="rfvBranchName" ControlToValidate="txtBranchName"
                                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please enter a Branch Name" Display="Dynamic"
                                    runat="server" CssClass="rfvPCG">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div class="divSectionHeading" style="vertical-align: text-bottom">
                                    Branch Details
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblAdrLine1" runat="server" Text="Line1(House No/Building):" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtBankAdrLine1" runat="server" CssClass="txtField" Style="width: 250px;"
                                    Text='<%# Bind("CB_BranchAdrLine1") %>'></asp:TextBox>
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="Label20" runat="server" Text="Line2(Street):" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtBankAdrLine2" runat="server" CssClass="txtField" Style="width: 250px;"
                                    Text='<%# Bind("CB_BranchAdrLine2") %>'></asp:TextBox>
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="Label21" runat="server" Text="Line3(Area):" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtBankAdrLine3" runat="server" CssClass="txtField" Style="width: 250px;"
                                    Text='<%# Bind("CB_BranchAdrLine3") %>'></asp:TextBox>
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblCity" runat="server" CssClass="FieldName" Text="City:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtBankAdrCity" runat="server" CssClass="txtField" Text='<%# Bind("CB_BranchAdrCity") %>'></asp:TextBox>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="Label23" runat="server" CssClass="FieldName" Text="State:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:DropDownList ID="ddlBankAdrState" runat="server" CssClass="txtField" Width="150px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblPinCode" runat="server" Text="Pin Code:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtBankAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"
                                    Text='<%# Bind("CB_BranchAdrPinCode") %>'></asp:TextBox>
                                <asp:CompareValidator ID="cvBankPinCode" runat="server" ErrorMessage="<br />Enter a numeric value"
                                    CssClass="rfvPCG" Type="Integer" ControlToValidate="txtBankAdrPinCode" ValidationGroup="btnSubmit"
                                    Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="Label25" runat="server" Text="Country:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:DropDownList ID="ddlBankAdrCountry" runat="server" CssClass="cmbField">
                                    <asp:ListItem>India</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblMicr" runat="server" Text="MICR:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtMicr" runat="server" CssClass="txtField" MaxLength="9" Text='<%# Bind("CB_MICR") %>'></asp:TextBox>
                                <asp:CompareValidator ID="cvMicr" runat="server" ErrorMessage="<br />Enter a numeric value"
                                    CssClass="rfvPCG" Type="Integer" ValidationGroup="btnSubmit" ControlToValidate="txtMicr"
                                    Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblIfsc" runat="server" Text="IFSC:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtIfsc" runat="server" CssClass="txtField" MaxLength="11" Text='<%# Bind("CB_IFSC") %>'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                    runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                    ValidationGroup="btnSubmit"></asp:Button>
                            </td>
                            <td visible="false">
                                <asp:Button Visible="false" ID="btnYes" runat="server" Text="Submit and Addmore"
                                    CssClass="PCGLongButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AddBankDetails_btnYes','L');"
                                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AddBankDetails_btnYes','L');"
                                    ValidationGroup="btnSubmit" />
                            </td>
                            <td>
                                <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                                    CommandName="Cancel"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </FormTemplate>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
<%--<table class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label runat="server" Text="Bank Details" CssClass="HeaderTextSmall" Font-Bold="True"></asp:Label>
        </td>
    </tr>
</table>
<br />--%>
<%--<table style="width: 100%;" class="TableBackground">
    <tr>
        <td>
            &nbsp;
            <asp:Label ID="lblBankDetails" runat="server" CssClass="HeaderTextBig" Text="Bank Details"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
            <asp:Label ID="lblMsg" runat="server" CssClass="Error" Text="No Records Found"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvCustomerBankAccounts" runat="server" AutoGenerateColumns="False"  AllowSorting="True" 
                CellPadding="4" CssClass="GridViewStyle" DataKeyNames="CustBankAccId" 
                OnSelectedIndexChanged="gvCustomerBankAccounts_SelectedIndexChanged" ShowFooter="True">
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="Bank Name" HeaderText="Bank Name"  />
                    <asp:BoundField DataField="Branch Name" HeaderText="Branch Name"  />
                    <asp:BoundField DataField="Account Type" HeaderText="Account Type"  />
                    <asp:BoundField DataField="Mode Of Operation" HeaderText="Mode of Operation"  />
                    <asp:BoundField DataField="Account Number" HeaderText="Account Number"  />
                </Columns>
                
            </asp:GridView>
        </td>
    </tr>
</table>
--%>