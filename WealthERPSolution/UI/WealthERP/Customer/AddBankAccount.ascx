<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddBankAccount.ascx.cs"
    Inherits="WealthERP.Customer.AddBankAccount" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<script language="javascript" type="text/javascript">

    function ClosePopUp() {

        window.close();
        return true;
    }
</script>

<table style: width="100%;">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            Add Bank Account
                        </td>
                        <td align="right">
                            <asp:LinkButton runat="server" ID="lnkBtnEdit" CssClass="LinkButtons" Text="Edit"
                                Visible="false" OnClick="lnkBtnEdit_Click"></asp:LinkButton>
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                                Visible="false" OnClick="lnkBtnBack_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<div>
    <table width="100%" style="background-color: White;" border="0">
        <tr>
            <td colspan="4">
                <div class="divSectionHeading" style="vertical-align: text-bottom">
                    Bank Details
                </div>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
            </td>
            <td class="rightField">
                <asp:DropDownList ID="ddlPortfolioId" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
                </asp:DropDownList>
                <span id="Span4" class="spnRequiredField">*</span>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlPortfolioId"
                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please select Portfolio" Operator="NotEqual"
                    ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
            </td>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblAccountType" runat="server" CssClass="FieldName" Text="Account Type:"></asp:Label>
            </td>
            <td class="rightField">
                <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="cmbLongField" Width="300px">
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
                <asp:RegularExpressionValidator ID="reqtxtAccountNumber" ControlToValidate="txtAccountNumber"
                    ErrorMessage=" </br>Enter Only Number" runat="server" Display="Dynamic" CssClass="cvPCG"
                    ValidationExpression="^\d+(;\d+)*$" ValidationGroup="btnSubmit">     
                </asp:RegularExpressionValidator>
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
                <asp:Label ID="lblBankName" runat="server" Text="Bank Name:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField">
                <asp:DropDownList ID="ddlBankName" CssClass="cmbLongField" Width="300px" runat="server">
                </asp:DropDownList>
                <span id="Span3" class="spnRequiredField">*</span>
                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlBankName"
                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please select a Bank Name" Operator="NotEqual"
                    ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
            </td>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <%-- <tr>
            <td class="leftField">
                <asp:Label ID="lblBankCity" runat="server" Text="Bank City:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtBankCity" runat="server" CssClass="txtField" Style="width: 225px;"
                    Text='<%# Bind("CB_BankCity") %>'></asp:TextBox>
                <span id="Span5" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtBankCity"
                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please Enter Bank City " Display="Dynamic"
                    runat="server" CssClass="rfvPCG">
                </asp:RequiredFieldValidator>
            </td>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>--%>
        <%--<tr>
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
--%>
        <tr id="trJoingHolding" runat="server">
            <td class="leftField">
                <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Joint Holding:"></asp:Label>
            </td>
            <td>
                <asp:RadioButton ID="rbtnYes" runat="server" CssClass="cmbFielde" GroupName="rbtnJointHolding"
                    Text="Yes" AutoPostBack="true" OnCheckedChanged="rbtnYes_CheckedChanged" />
                <%--  <asp:RadioButton ID="rbtnNo" runat="server" CssClass="cmbField" GroupName="rbtnJointHolding"
                    Text="No" AutoPostBack="true"
                    oncheckedchanged="rbtnNo_CheckedChanged" />--%>
                <%--<asp:RadioButton ID="rbtnN" runat="server" CssClass="cmbField" GroupName="rbtnJointHolding"
                    Text="No" AutoPostBack="true"
                    OnCheckedChanged="rbtnYes_CheckedChanged" />--%>
                <asp:RadioButton ID="RadioButton1" runat="server" CssClass="cmbFielde" GroupName="rbtnJointHolding"
                    Text="No" AutoPostBack="true" Checked="true" OnCheckedChanged="rbtnYes_CheckedChanged" />
                <%--OnCheckedChanged="rbtnYes_CheckedChanged"--%>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblModeOfOperation" runat="server" Text="Mode of Operation:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField">
                <asp:DropDownList ID="ddlModeofOperation" runat="server" CssClass="cmbField" Enabled="false">
                    <asp:ListItem Text="Select" Value="0" Selected="trues">
                    </asp:ListItem>
                </asp:DropDownList>
                <span id="Span2" class="spnRequiredField">*</span>
                <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlModeofOperation"
                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please select a ModeofHolding"
                    Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
            </td>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Add Nominee:"></asp:Label>
            </td>
            <td>
                <asp:RadioButton ID="rbtnomyes" runat="server" CssClass="cmbFielde" GroupName="rbtnNominee"
                    Text="Yes" AutoPostBack="true" OnCheckedChanged="rbtnNominee_CheckedChanged " />
                <asp:RadioButton ID="rbtnomNo" runat="server" CssClass="cmbFielde" GroupName="rbtnNominee"
                    Text="No" AutoPostBack="true" OnCheckedChanged="rbtnNominee_CheckedChanged" Checked="true" />
                <%--OnCheckedChanged="rbtnYes_CheckedChanged"--%>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lbl_Ismain" runat="server" CssClass="FieldName" Text="Is Main Bank:"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="chk_Ismain" runat="server" />
            </td>
        </tr>
        <tr id="trjointholder" runat="server" visible="false">
            <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
                <div class="divSectionHeading" style="vertical-align: text-bottom">
                    Joint Holder
                </div>
            </td>
        </tr>
        <tr id="trgvjointHolder" runat="server" visible="false">
            <td>
                <telerik:RadGrid ID="gvJointHolders" runat="server" GridLines="None" Width="100%"
                    AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false" ShowStatusBar="true"
                    AllowAutomaticDeletes="True" AllowAutomaticInserts="false" AllowAutomaticUpdates="false"
                    Skin="Telerik" EnableEmbeddedSkins="true" EnableHeaderContextFilterMenu="false"
                    AllowFilteringByColumn="false" OnItemDataBound="gvJointHolders_ItemDataBound">
                    <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="MemberCustomerId,AssociationId" Width="100%">
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Select" HeaderStyle-Width="30px">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkId" runat="server" />
                                    <asp:HiddenField ID="hdnchkBx" runat="server" Value='<%# Eval("AssociationId").ToString()%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="Name" HeaderStyle-Width="80px" HeaderText="Name"
                                DataField="Name" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Relationship" HeaderStyle-Width="90px" HeaderText="Relationship"
                                DataField="Relationship" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
        <tr id="trNoJointHolders" runat="server" visible="false">
            <td class="Message" colspan="2">
                <asp:Label ID="lblNoJointHolders" runat="server" Text="You have no Joint Holder"
                    CssClass="FieldName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr id="trNomineeCaption" runat="server" visible="false">
            <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
                <div class="divSectionHeading" style="vertical-align: text-bottom">
                    Nominees
                </div>
            </td>
        </tr>
        <tr id="trgvNominees" runat="server" visible="false">
            <td>
                <telerik:RadGrid ID="gvNominees" runat="server" GridLines="None" Width="100%" AllowPaging="true"
                    AllowSorting="false" AutoGenerateColumns="false" ShowStatusBar="true" AllowAutomaticDeletes="True"
                    AllowAutomaticInserts="false" AllowAutomaticUpdates="false" Skin="Telerik" EnableEmbeddedSkins="true"
                    EnableHeaderContextFilterMenu="false" AllowFilteringByColumn="false" OnItemDataBound="gvNominees_ItemDataBound">
                    <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="MemberCustomerId,AssociationId" Width="100%">
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Select" HeaderStyle-Width="30px">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkId0" runat="server" />
                                    <asp:HiddenField ID="hdnchkBxnom" runat="server" Value='<%# Eval("AssociationId").ToString()%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="Name" HeaderStyle-Width="80px" HeaderText="Name"
                                DataField="Name" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Relationship" HeaderStyle-Width="90px" HeaderText="Relationship"
                                DataField="Relationship" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
        <tr id="trNoNominee" runat="server" visible="false">
            <td class="Message" colspan="2">
                <asp:Label ID="lblNoNominee" runat="server" Text="You have no Associations" CssClass="FieldName"></asp:Label>
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
            <td class="leftField">
                <asp:Label ID="lblAdrLine1" runat="server" Text="Line1(House No/Building):" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtBankAdrLine1" runat="server" CssClass="txtField" Style="width: 250px;"
                    Text='<%# Bind("CB_BranchAdrLine1") %>'></asp:TextBox>
            </td>
            <td class="leftField">
                <asp:Label ID="lblBankCity" runat="server" Text="Bank City:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtBankCity" runat="server" CssClass="txtField" Style="width: 225px;"
                    Text='<%# Bind("CB_BankCity") %>'></asp:TextBox>
                <span id="Span5" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtBankCity"
                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please Enter Bank City " Display="Dynamic"
                    runat="server" CssClass="rfvPCG">
                </asp:RequiredFieldValidator>
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
            <%--<td colspan="2">--%>
            <td class="leftField">
                <asp:Label ID="lblNEFTCode" runat="server" Text="NEFT Code:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtNEFTCode" runat="server" CssClass="txtField" MaxLength="11" Text='<%# Bind("CB_NEFT") %>'></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator5" runat="server" CssClass="rfvPCG" Type="String"
                    ControlToValidate="txtNEFTCode" ValidationGroup="btnSubmit" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
            </td>
            <%--</td>--%>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="Label21" runat="server" Text="Line3(Area):" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtBankAdrLine3" runat="server" CssClass="txtField" Style="width: 250px;"
                    Text='<%# Bind("CB_BranchAdrLine3") %>'></asp:TextBox>
            </td>
            <%--            <td colspan="2">
                &nbsp;
            </td>--%>
            <td class="leftField">
                <asp:Label ID="lblRTGSCode" runat="server" Text="RTGS Code:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtRTGSCode" runat="server" CssClass="txtField" MaxLength="11" Text='<%# Bind("CB_RTGS") %>'></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator6" runat="server" CssClass="rfvPCG" Type="String"
                    ControlToValidate="txtRTGSCode" ValidationGroup="btnSubmit" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
            </td>
        </tr>
        <%--<tr>
            <td class="leftField">
                <asp:Label ID="lblCity" runat="server" CssClass="FieldName" Text="City:"></asp:Label>
            </td>
            <td class="rightField">
                <%-- <asp:TextBox ID="txtBankAdrCity" runat="server" CssClass="txtField" Text='<%# Bind("CB_BranchAdrCity") %>'></asp:TextBox>
                <asp:DropDownList ID="ddlBankAdrCity" runat="server" CssClass="txtField" Width="150px">
                </asp:DropDownList>
            </td>
            <td class="leftField">
                <asp:Label ID="Label23" runat="server" CssClass="FieldName" Text="State:"></asp:Label>
            </td>
            <td class="rightField">
                <asp:DropDownList ID="ddlBankAdrState" runat="server" CssClass="txtField" Width="150px">
                </asp:DropDownList>
            </td>
        </tr>--%>
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
                <asp:Label ID="lblBankBranchCode" runat="server" Text="Bank Branch Code:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtBankBranchCode" runat="server" CssClass="txtField" MaxLength="6"
                    Text='<%# Bind("CB_BankBranchCode") %>'></asp:TextBox>
            </td>
            <%-- <td class="leftField">
                <asp:Label ID="Label25" runat="server" Text="Country:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightField">
                <asp:DropDownList ID="ddlBankAdrCountry" runat="server" CssClass="cmbField">
                </asp:DropDownList>
            </td>--%>
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
                <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerAccountAdd_btnSubmit', 'S');"
                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerAccountAdd_btnSubmit', 'S');"
                    Text="Submit" Visible="true" OnClick="btnSubmit_Click" ValidationGroup="btnSubmit" />
                <asp:Button ID="btnUpdate" runat="server" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerAccountAdd_btnSubmit', 'S');"
                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerAccountAdd_btnSubmit', 'S');"
                    Text="Update" Visible="false" OnClick="btnUpdate_Click" ValidationGroup="btnSubmit" />
            </td>
        </tr>
        <%--<td>
            <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                CommandName="Cancel"></asp:Button>
        </td>--%>
        </tr>
    </table>
</div>
