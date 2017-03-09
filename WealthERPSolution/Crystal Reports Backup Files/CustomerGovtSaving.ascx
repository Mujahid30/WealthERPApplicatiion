<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerGovtSaving.ascx.cs"
    Inherits="WealthERP.CustomerAssetDetails.CustomerGovtSaving" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>
<table style="width: 100%;">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Govt Savings Details
                        </td>
                         <td align="right" id="tdExport" runat="server" style="padding-bottom: 2px;">
                            <asp:ImageButton ID="btnImgGS" ImageUrl="~/Images/Export_Excel.png" runat="server"
                                AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('excel')"
                                Height="25px" Width="25px" onclick="btnImgGS_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="True"
                OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
</table>
<div style="width: 100%; padding-left: 10px; padding-top: 5px;">
    <telerik:RadGrid ID="gvGovtSaving" runat="server" CssClass="RadGrid" GridLines="Both"
        Width="98%" AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="false" AllowAutomaticInserts="false"
        ShowStatusBar="true" AllowAutomaticDeletes="True" Skin="Telerik" OnItemDataBound="gvGovtSaving_ItemDataBound"
        ExportSettings-FileName="Govt Savings" OnNeedDataSource="gvGovtSaving_NeedDataSource" OnItemCommand="gvGovtSaving_ItemCommand">
        <exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                                        filename="AssetAllocation MIS" excel-format="ExcelML">
        </exportsettings>
        <MasterTableView DataKeyNames="CGSNP_GovtSavingNPId,CGSA_AccountId,XMOH_ModeOfHoldingCode,XDI_DebtIssuerCode,XIB_InterestBasisCode,
                         XF_CompoundInterestFrequencyCode,PAIC_AssetInstrumentCategoryCode,XF_InterestPayableFrequencyCode"
            EditMode="EditForms" CommandItemDisplay="Top" CommandItemSettings-ShowRefreshButton="false"
            CommandItemSettings-AddNewRecordText="Add Govt. Savings">
            <Columns>
                <telerik:GridEditCommandColumn Visible="true" HeaderStyle-Width="50px" EditText="View/Edit"
                    UniqueName="editColumn" CancelText="Cancel" UpdateText="Update">
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn UniqueName="PAIC_AssetInstrumentCategoryName" HeaderStyle-Width="100px"
                    HeaderText="Category" DataField="PAIC_AssetInstrumentCategoryName" SortExpression="PAIC_AssetInstrumentCategoryName"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CGSNP_Name" HeaderStyle-Width="100px" HeaderText="Particulars"
                    DataField="CGSNP_Name" SortExpression="CGSNP_Name" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CGSNP_PurchaseDate" HeaderStyle-Width="100px"
                    HeaderText="Deposit Date (dd/mm/yyyy)" DataField="CGSNP_PurchaseDate" SortExpression="CGSNP_PurchaseDate"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:d}">
                    <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CGSNP_MaturityDate" HeaderStyle-Width="100px"
                    HeaderText="Maturity Date (dd/mm/yyyy)" DataField="CGSNP_MaturityDate" SortExpression="CGSNP_MaturityDate"
                    AllowFiltering="true" ShowFilterIcon="False" AutoPostBackOnFilter="true" DataFormatString="{0:d}">
                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CGSNP_DepositAmount" HeaderStyle-Width="100px"
                    HeaderText="Deposit Amount (Rs)" DataField="CGSNP_DepositAmount" SortExpression="CGSNP_DepositAmount"
                    AllowFiltering="true" DataFormatString="{0:n0}" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CGSNP_InterestRate" HeaderStyle-Width="150px"
                    HeaderText="Rate Of Interest (%)" DataField="CGSNP_InterestRate" SortExpression="CGSNP_InterestRate"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:n2}">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CGSNP_CurrentValue" HeaderStyle-Width="150px"
                    HeaderText="Current Value (Rs)" DataField="CGSNP_CurrentValue" SortExpression="CGSNP_CurrentValue"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:n0}">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CGSNP_MaturityValue" HeaderStyle-Width="150px"
                    HeaderText="Maturity Value (Rs)" DataField="CGSNP_MaturityValue" SortExpression="CGSNP_MaturityValue"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:n0}">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete?"
                    ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                    Text="Delete">
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings FormTableStyle-Height="10px" EditFormType="Template" FormTableStyle-Width="1000px">
                <FormTemplate>
                <%--<asp:ImageButton ID="btnImgAddAccount" ImageUrl="../Images/user_add.png"
                  AlternateText="Add" runat="server" ToolTip="Click here to Add Account" OnClick="btnImgAddAccount_Click"
                Height="15px" Width="15px"></asp:ImageButton>--%>
                <%--<asp:Button ID="btnShow" runat="server" CssClass="PCGButton" OnClick="btnShow_Click" Text="ShowDiv"  CausesValidation="False" Enabled="True" />--%>
                    <div id="divAccout" runat="server">
                        <table width="100%">
                            <tr>
                                <td id="tdAccount" runat="server" style="width: 50%">
                                    <table id="tblAccount" runat="server" width="100%">
                                        <tr id="trAssetGroup" runat="server">
                                            <td>
                                                <asp:Label ID="lblAssetGroup" runat="server" CssClass="FieldName" Text="Asset Group"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAssetGroupName" runat="server" CssClass="FieldName" Text="Asset Group"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trCategory" runat="server">
                                            <td class="leftField">
                                                <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Asset Category:"></asp:Label>
                                            </td>
                                            <td class="rightField">
                                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField">
                                                </asp:DropDownList>
                                                <span id="Span1" class="spnRequiredField">*</span>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />Please select a category"
                                                    ControlToValidate="ddlCategory" Operator="NotEqual" ValueToCompare="Select" Display="Dynamic"
                                                    CssClass="cvPCG" SetFocusOnError="true"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr id="trAccountNum" runat="server">
                                            <td class="leftField">
                                                <asp:Label ID="lblAccountNum" runat="server" CssClass="FieldName" Text="Account Number:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="txtField"></asp:TextBox>
                                                <span id="Span3" class="spnRequiredField">*</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAccountNumber"
                                                    ErrorMessage="<br />Please enter a Number" Display="Dynamic" runat="server" CssClass="rfvPCG"
                                                    SetFocusOnError="true" ValidationGroup="Submit">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr id="trAccountSource" runat="server">
                                            <td class="leftField">
                                                <asp:Label ID="lblAccountSource" runat="server" CssClass="FieldName" Text="Account Source:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAccountSource" runat="server" CssClass="txtField"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trJoingHolding" runat="server">
                                            <td class="leftField">
                                                <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Joint Holding:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="rbtnYes" runat="server" CssClass="cmbField" GroupName="rbtnJointHolding"
                                                    Text="Yes" OnCheckedChanged="rbtnYes_CheckedChanged" AutoPostBack="true" />
                                                <asp:RadioButton ID="rbtnNo" runat="server" CssClass="cmbField" GroupName="rbtnJointHolding"
                                                    Text="No" AutoPostBack="true" OnCheckedChanged="rbtnYes_CheckedChanged" Checked="true" />
                                            </td>
                                        </tr>
                                        <tr id="trModeOfHolding" runat="server">
                                            <td class="leftField">
                                                <asp:Label ID="lblModeOfHolding" runat="server" CssClass="FieldName" Text="Mode Of Holding:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlModeOfHolding" runat="server" CssClass="cmbField">
                                                </asp:DropDownList>
                                                <span id="Span6" class="spnRequiredField">*</span>
                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<br />Please select a Mode of Holding"
                                                    ControlToValidate="ddlModeOfHolding" Operator="NotEqual" ValueToCompare="Select"
                                                    ValidationGroup="Submit" Display="Dynamic" CssClass="cvPCG" SetFocusOnError="true"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr id="trError" runat="server" visible="false">
                                            <td colspan="2" class="SubmitCell">
                                                <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnAcctInsert" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                    runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                    ValidationGroup="Submit"></asp:Button>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAcctCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                    CssClass="PCGButton" CommandName="Cancel"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td id="tdNominee" runat="server" style="width: 50%">
                                    <table id="tblNominee" runat="server" width="100%">
                                        <tr id="trNominieeGrid" runat="server">
                                            <td colspan="2">
                                                <asp:Panel ID="pnlNominiees" runat="server">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <div>
                                                                    <telerik:RadGrid ID="gvNominees" runat="server" GridLines="None" AutoGenerateColumns="False"
                                                                        Width="100%" PageSize="4" AllowSorting="false" AllowPaging="True" ShowStatusBar="True"
                                                                        ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="false"
                                                                        AllowAutomaticInserts="false" ExportSettings-FileName="Count" Visible="True">
                                                                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                                                            FileName="Goal MIS" Excel-Format="ExcelML">
                                                                        </ExportSettings>
                                                                        <MasterTableView Width="100%" DataKeyNames="MemberCustomerId, AssociationId" AllowMultiColumnSorting="True"
                                                                            AutoGenerateColumns="false" CommandItemDisplay="None">
                                                                            <Columns>
                                                                                <telerik:GridTemplateColumn HeaderText="Select">
                                                                                    <HeaderTemplate>
                                                                                        <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="cbRecons" runat="server" Checked="false" />
                                                                                    </ItemTemplate>
                                                                                </telerik:GridTemplateColumn>
                                                                                <telerik:GridBoundColumn DataField="Name" HeaderText="Name" UniqueName="Name" SortExpression="Name">
                                                                                    <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Relationship" HeaderText="Relationship" AllowFiltering="false"
                                                                                    HeaderStyle-HorizontalAlign="Left" UniqueName="Relationship">
                                                                                    <ItemStyle HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                                                </telerik:GridBoundColumn>
                                                                            </Columns>
                                                                        </MasterTableView>
                                                                        <ClientSettings>
                                                                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                                                        </ClientSettings>
                                                                    </telerik:RadGrid></div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr id="tdJointHolders" runat="server">
                                            <td colspan="2">
                                                <asp:Panel ID="pnlJointHolders" runat="server">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadGrid ID="gvJointHoldersList" runat="server" GridLines="None" AutoGenerateColumns="False"
                                                                    Width="100%" PageSize="4" AllowSorting="false" AllowPaging="True" ShowStatusBar="True"
                                                                    ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="false"
                                                                    AllowAutomaticInserts="false" ExportSettings-FileName="Count">
                                                                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                                                        FileName="Goal MIS" Excel-Format="ExcelML">
                                                                    </ExportSettings>
                                                                    <MasterTableView Width="100%" DataKeyNames="MemberCustomerId, AssociationId" AllowMultiColumnSorting="True"
                                                                        AutoGenerateColumns="false" CommandItemDisplay="None">
                                                                        <Columns>
                                                                            <telerik:GridTemplateColumn HeaderText="Select">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="cbRecons" runat="server" Checked="false" />
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridBoundColumn DataField="AssociateName" HeaderText="Name" UniqueName="Name"
                                                                                SortExpression="Name">
                                                                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="XR_Relationship" HeaderText="Relationship" AllowFiltering="false"
                                                                                HeaderStyle-HorizontalAlign="Left" UniqueName="Relationship">
                                                                                <ItemStyle HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                                            </telerik:GridBoundColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                    <ClientSettings>
                                                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                                                    </ClientSettings>
                                                                </telerik:RadGrid>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                   <%-- <asp:ImageButton ID="btnImgGovtSaving" ImageUrl="../Images/user_add.png"
                  AlternateText="Add" runat="server" ToolTip="Click here to Add Govt. Savings" OnClientClick="return openpopupAddCustomer()"
                Height="15px" Width="15px"></asp:ImageButton>--%>
                    <div id="divGovtSavings" runat="server" >
                        <table>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Asset Category:"></asp:Label>
                                </td>
                                <td class="rightField" colspan="3">
                                    <asp:Label ID="lblInstrumentCategory" runat="server" CssClass="Field" Text='<%# Bind("PAIC_AssetInstrumentCategoryName") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                                        Account Details
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField" valign="top">
                                    <asp:Label runat="server" CssClass="FieldName" Text="Account/Certificate No:" ID="lblAccountId"></asp:Label>
                                </td>
                                <td class="rightField" colspan="3">
                                    <asp:TextBox ID="txtAccountId" runat="server" CssClass="txtField" Enabled="false"
                                        Text='<%# Bind("CGSA_AccountNum") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAccountId" ControlToValidate="txtAccountId" ValidationGroup="Submit"
                                        ErrorMessage="<br/>Please enter value." Display="Dynamic" runat="server" CssClass="rfvPCG" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label runat="server" CssClass="FieldName" Text="Account Opening Date:" ID="lblAccOpeningDate"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtAccOpenDate" runat="server" CssClass="txtField" Text='<%# Bind("CGSA_AccountOpeningDate") %>'></asp:TextBox>
                                    <span id="Span13" class="spnRequiredField">*</span>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtAccOpenDate"
                                        Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                    <cc1:TextBoxWatermarkExtender ID="txtAccOpenDate_TextBoxWatermarkExtender" runat="server"
                                        TargetControlID="txtAccOpenDate" WatermarkText="dd/mm/yyyy">
                                    </cc1:TextBoxWatermarkExtender>
                                    <asp:RequiredFieldValidator ID="rfvAccOpenDate" ControlToValidate="txtAccOpenDate"
                                        ValidationGroup="Submit" ErrorMessage="<br />Please select a Date" Display="Dynamic"
                                        runat="server" CssClass="rfvPCG">
                                    </asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cvAccOpenDate" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                                        Type="Date" ControlToValidate="txtAccOpenDate" Operator="DataTypeCheck" CssClass="cvPCG"
                                        ValidationGroup="Submit" Display="Dynamic"></asp:CompareValidator>
                                    <asp:CompareValidator ID="cvAccopenDateCheckCurrent" runat="server" ErrorMessage="<br />Account Opening Date should not be more the current date"
                                        Type="Date" ControlToValidate="txtAccOpenDate" ValueToCompare='<%# DateTime.Today.ToString("dd/MM/yyyy") %>'
                                        Operator="LessThanEqual" CssClass="cvPCG" Display="Dynamic" ValidationGroup="Submit"></asp:CompareValidator>
                                </td>
                                <td class="leftField">
                                    <asp:Label runat="server" CssClass="FieldName" Text="Account with:" ID="lblAccountwith"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtAccountWith" runat="server" CssClass="txtField" Text='<%# Bind("CGSA_AccountSource") %>'></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label runat="server" CssClass="FieldName" Text="Mode of Holding:" ID="lblModeOfHold"></asp:Label>
                                </td>
                                <td class="rightField" colspan="3">
                                    <asp:DropDownList ID="ddlgovtModeofHoldings" runat="server" CssClass="cmbField" Enabled="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="lblName" runat="server" CssClass="FieldName" Text="Asset Particulars:"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtAssetParticulars" runat="server" CssClass="txtField" Text='<%# Bind("CGSNP_Name") %>'></asp:TextBox>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Asset Issuer:"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:DropDownList ID="ddlDebtIssuerCode" runat="server" CssClass="cmbField">
                                    </asp:DropDownList>
                                    <span id="Span4" class="spnRequiredField">*</span>
                                    <br />
                                    <asp:CompareValidator ID="cvDebtIssuerCode" runat="server" ControlToValidate="ddlDebtIssuerCode"
                                        ErrorMessage="Please select a Asset Issuer" Operator="NotEqual" ValueToCompare="0"
                                        ValidationGroup="Submit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                                        Deposit Details
                                    </div>
                                </td>
                            </tr>
                            <tr runat="server" id="tdDepositAndMaturityDate">
                                <td class="leftField">
                                    <asp:Label ID="lblDepositDate" runat="server" CssClass="FieldName" Text="Deposit Date:"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtDepositDate" runat="server" CssClass="txtField" Text='<%# Bind("CGSNP_PurchaseDate") %>'></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDepositDate_CalendarExtender" runat="server" TargetControlID="txtDepositDate"
                                        Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                    <cc1:TextBoxWatermarkExtender ID="txtDepositDate_TextBoxWatermarkExtender" runat="server"
                                        TargetControlID="txtDepositDate" WatermarkText="dd/mm/yyyy">
                                    </cc1:TextBoxWatermarkExtender>
                                    <span runat="server" id="spanValidationDepositDate"><span id="Span5" class="spnRequiredField">
                                        *</span>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvDepositDate" ControlToValidate="txtDepositDate"
                                            ValidationGroup="Submit" ErrorMessage="Please select a Deposit Date" Display="Dynamic"
                                            runat="server" CssClass="rfvPCG">
                        
                        
                                        </asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cvDepositDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyy"
                                            ValidationGroup="Submit" Type="Date" ControlToValidate="txtDepositDate" Operator="DataTypeCheck"
                                            CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                                        <asp:CompareValidator ID="cmpareValidatorOnDates" runat="server" ValidationGroup="Submit"
                                            ErrorMessage="Deposit date cannot be less than Account Opening date" ControlToCompare="txtDepositDate"
                                            CssClass="cvPCG" ControlToValidate="txtAccOpenDate" Operator="LessThanEqual"
                                            Type="Date"></asp:CompareValidator>
                                    </span>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="lblMaturityDate" runat="server" CssClass="FieldName" Text="Maturity Date:"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtMaturityDate" runat="server" CssClass="txtField" Text='<%# Bind("CGSNP_MaturityDate") %>'></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtMaturityDate_CalendarExtender" runat="server" TargetControlID="txtMaturityDate"
                                        Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                    <cc1:TextBoxWatermarkExtender ID="txtMaturityDate_TextBoxWatermarkExtender" runat="server"
                                        TargetControlID="txtMaturityDate" WatermarkText="dd/mm/yyyy">
                                    </cc1:TextBoxWatermarkExtender>
                                    <asp:CompareValidator ID="cvMaturityDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyy"
                                        ValidationGroup="Submit" Type="Date" ControlToValidate="txtMaturityDate" Operator="DataTypeCheck"
                                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                                    <span runat="server" id="spanValidationMaturityDate"><span id="Span7" class="spnRequiredField">
                                        *</span>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvMaturityDate" ControlToValidate="txtMaturityDate"
                                            ValidationGroup="Submit" ErrorMessage="Please select a Maturity Date" Display="Dynamic"
                                            runat="server" CssClass="rfvPCG">
                                        </asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Maturity Date should be greater than Deposit Date"
                                            ValidationGroup="Submit" Type="Date" ControlToValidate="txtMaturityDate" ControlToCompare="txtDepositDate"
                                            Operator="GreaterThan" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="lblDepositAmount" runat="server" CssClass="FieldName" Text="Deposit Amount:"></asp:Label>
                                </td>
                                <td class="rightField" colspan="3">
                                    <asp:TextBox ID="txtDepositAmount" runat="server" CssClass="txtField" onblur="ShiftValuet()"
                                        Text='<%# Bind("CGSNP_DepositAmount") %>'></asp:TextBox>
                                    <asp:CompareValidator ID="cvDepositAmount" runat="server" ErrorMessage="<br/>Please enter a numeric value"
                                        Type="Double" ControlToValidate="txtDepositAmount" Operator="DataTypeCheck" CssClass="cvPCG"
                                        ValidationGroup="Submit" Display="Dynamic"></asp:CompareValidator>
                                    <span id="spanValidationDepositAmount" runat="server"><span id="Span8" class="spnRequiredField">
                                        *</span>
                                        <asp:RequiredFieldValidator ID="rfvDepositAmount" ControlToValidate="txtDepositAmount"
                                            ValidationGroup="Submit" ErrorMessage="<br/>Please enter the Deposit Amount"
                                            Display="Dynamic" runat="server" CssClass="rfvPCG">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                </td>
                            </tr>
                            <tr id="trSubsequent" runat="server" visible="false">
                                <td class="leftField">
                                    <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Subsequent Deposit Amount:"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtSubsqntDepositAmount" runat="server" CssClass="txtField" Text='<%# Bind("CGSNP_SubsqntDepositAmount") %>'></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="<br/>Please enter a numeric value"
                                        Type="Double" ControlToValidate="txtSubsqntDepositAmount" Operator="DataTypeCheck"
                                        CssClass="cvPCG" ValidationGroup="Submit" Display="Dynamic"></asp:CompareValidator>
                                    <span id="Span9" class="spnRequiredField">*</span>
                                    <asp:RequiredFieldValidator ID="rvSubsqntDepositAmount" ControlToValidate="txtSubsqntDepositAmount"
                                        ValidationGroup="Submit" ErrorMessage="<br/>Please enter the Amount" Display="Dynamic"
                                        runat="server" CssClass="rfvPCG">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Date:"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtSubsqntDepositDate" runat="server" CssClass="txtField" Text='<%# Bind("CGSNP_SubsqntDepositDate") %>'></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSubsqntDepositDate"
                                        Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSubsqntDepositDate"
                                        WatermarkText="dd/mm/yyyy">
                                    </cc1:TextBoxWatermarkExtender>
                                    <span runat="server" id="span9"><span id="Span10" class="spnRequiredField">*</span>
                                        <br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtSubsqntDepositDate"
                                            ValidationGroup="Submit" ErrorMessage="Please select a Date" Display="Dynamic"
                                            runat="server" CssClass="rfvPCG">
                                        </asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="The date format should be dd/mm/yyyy"
                                            ValidationGroup="Submit" Type="Date" ControlToValidate="txtSubsqntDepositDate"
                                            Operator="DataTypeCheck" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                                    </span>
                                </td>
                            </tr>
                            <tr id="trSubsequentFrequency" runat="server" visible="false">
                                <td class="leftField">
                                    <asp:Label ID="lblFrequencyDeposit" runat="server" CssClass="FieldName" Text="Frequency of deposit:"></asp:Label>
                                </td>
                                <td class="rightField" colspan="3">
                                    <asp:DropDownList ID="ddlDepositFrequency" runat="server" CssClass="cmbField">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                                        Interest Details
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="lblInterestAccumulated" runat="server" CssClass="FieldName" Text="Is Interest Accumulated?"></asp:Label>
                                </td>
                                <td class="rightField" colspan="3">
                                    <asp:RadioButton ID="rbtnAccumulated" runat="server" CssClass="txtField" Text="Yes"
                                        ValidationGroup="InterestAccumulated" GroupName="rbtnIsInterestAcc" AutoPostBack="True" />
                                    <asp:RadioButton ID="rbtnPaidout" runat="server" CssClass="txtField" Text="No" ValidationGroup="InterestAccumulated"
                                        GroupName="rbtnIsInterestAcc" AutoPostBack="True" Checked="true" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="lblInterestRate" runat="server" CssClass="FieldName" Text="Interest Rate Applicable %:"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtInterstRate" runat="server" CssClass="txtField" Text='<%# Bind("CGSNP_InterestRate") %>'></asp:TextBox>
                                    <asp:RangeValidator ID="rvInterestRate" runat="server" ControlToValidate="txtInterstRate"
                                        MinimumValue="0" MaximumValue="100" Type="Double" ErrorMessage="Enter a value between 0-100"
                                        CssClass="rfvPCG" Display="Dynamic" ValidationGroup="Submit" />
                                    <span id="spanValidationInterestRate" runat="server"><span id="Span11" class="spnRequiredField">
                                        *</span>
                                        <asp:RequiredFieldValidator ID="rfvInterstRate" ControlToValidate="txtInterstRate"
                                            ValidationGroup="Submit" ErrorMessage="<br/>Please enter the Interest Rate" Display="Dynamic"
                                            runat="server" CssClass="rfvPCG">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="lblInterestBasis" runat="server" CssClass="FieldName" Text="Interest Calc Basis:"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:DropDownList ID="ddlInterestBasis" runat="server" CssClass="cmbField" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlInterestBasis_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trSimpleInterest" runat="server" visible="false">
                                <td class="leftField">
                                    <asp:Label ID="lblSimpleInterestFC" runat="server" CssClass="FieldName" Text="Interest Credit Frequency:"></asp:Label>
                                </td>
                                <td class="rightField" colspan="3">
                                    <asp:DropDownList ID="ddlSimpleInterestFC" runat="server" CssClass="cmbField">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trCompoundInterest" runat="server" visible="false">
                                <td class="leftField">
                                    <asp:Label ID="lblCompoundInterestFC" runat="server" CssClass="FieldName" Text="Interest Calc Frequency:"></asp:Label>
                                </td>
                                <td class="rightField" colspan="3">
                                    <asp:DropDownList ID="ddlCompoundInterestFC" runat="server" CssClass="cmbField">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="lblInterestAmtCredited" runat="server" CssClass="FieldName" Text="Interest Amount Credited:"></asp:Label>
                                </td>
                                <td class="rightField" colspan="3">
                                    <asp:TextBox ID="txtInterestAmtCredited" runat="server" CssClass="txtField" Text='<%# Bind("CGSNP_InterestAmtPaidOut") %>'></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                                        Valuation
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="lblCurrentValue" runat="server" CssClass="FieldName" Text="Current Value:"></asp:Label>
                                </td>
                                <td class="rightField">
                                    <asp:TextBox ID="txtCurrentValue" runat="server" CssClass="txtField" Text='<%# Bind("CGSNP_CurrentValue") %>'></asp:TextBox>
                                    <br />
                                    <asp:CompareValidator ID="cvCurrentValue" runat="server" ErrorMessage="Please enter a numeric value"
                                        ValidationGroup="Submit" Type="Double" ControlToValidate="txtCurrentValue" Operator="DataTypeCheck"
                                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                                </td>
                                <td class="leftField">
                                    <asp:Label ID="lblMaturityValue" runat="server" CssClass="FieldName" Text="Maturity Value:"></asp:Label>
                                </td>
                                <td class="rightField" runat="server" id="tdMaturityValue">
                                    <asp:TextBox ID="txtMaturityValue" runat="server" CssClass="txtField" Text='<%# Bind("CGSNP_MaturityValue") %>'></asp:TextBox>
                                    <br />
                                    <asp:CompareValidator ID="cvMaturityValue" runat="server" ErrorMessage="Please enter a numeric value"
                                        Type="Double" ControlToValidate="txtMaturityValue" Operator="DataTypeCheck" CssClass="cvPCG"
                                        ValidationGroup="Submit" Display="Dynamic"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="leftField">
                                    <asp:Label ID="lblRemarks" runat="server" CssClass="FieldName" Text="Remarks:"></asp:Label>
                                </td>
                                <td colspan="4" rowspan="2" class="rightField">
                                    <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="server" CssClass="txtField"
                                        Text='<%# Bind("CGSNP_Remark") %>'></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnGovtSubmit" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                        runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                        ValidationGroup="Submit"></asp:Button>
                                </td>
                                <td>
                                    <asp:Button ID="btnGovtCancel" Text="Cancel" runat="server" CausesValidation="False"
                                        CssClass="PCGButton" CommandName="Cancel"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </div>
                </FormTemplate>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div>

<asp:HiddenField ID="hdnCondition" runat="server"/>