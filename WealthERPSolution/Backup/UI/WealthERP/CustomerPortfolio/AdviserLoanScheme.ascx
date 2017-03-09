<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserLoanScheme.ascx.cs"
    Inherits="WealthERP.Loans.AdviserLoanScheme" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .smallTxt
    {
        width: 70px;
    }
    fieldset
    {
        border: 1px solid #942627;
    }
    legend
    {
        font-weight: bolder;
        color: #942627;
    }
    .txtField
    {
        width: 70px;
    }
</style>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table style="font-size: 11px" class="TableBackground">
    <tr>
        <td>
            <asp:Label ID="Label20" runat="server" CssClass="HeaderTextBig" Text="Loan Schemes"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="center">
            <div class="success-msg" id="UpdationIncomplete" runat="server" visible="false" align="center">
                Please Check all the grid items have been Updated!
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lnkViewAll" CssClass="LinkButtons" runat="server" OnClick="lnkViewAll_Click"
                CausesValidation="False">Back</asp:LinkButton>
            &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:LinkButton ID="lnkEdit" CssClass="LinkButtons" runat="server" CausesValidation="False"
                OnClick="lnkEdit_Click">Edit</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <table border="0">
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label2" Text="Scheme Name:" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox CssClass="txtField" ID="txtSchemeName" runat="server" Width="190px"></asp:TextBox>                       
                        <span id="span3" class="spnRequiredField">*</span>                         
                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvSchemeName" runat="server"
                            ControlToValidate="txtSchemeName" ErrorMessage="<br/>Enter Scheme Name" ValidationGroup="main" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        
                    </td>
                    <td class="leftField">
                        <asp:Label ID="lblSchemeId" runat="server" Text="Scheme ID : " CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSchemeIdVal" runat="server" Text="" CssClass="FieldName"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label3" Text="Loan Type:" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList CssClass="cmbField" ID="ddlLoanType" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlLoanType_SelectedIndexChanged">
                        </asp:DropDownList>
                        <span id="Span2" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvLoanType" runat="server"
                            ControlToValidate="ddlLoanType" ErrorMessage="<br>Select Loan Type." InitialValue="0"
                            ValidationGroup="main"  Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label4" Text="Loan Partner:" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlLoanPartner" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                        <span id="Span1" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvLoanPartner" runat="server"
                            ControlToValidate="ddlLoanPartner" ErrorMessage="<br>Select Loan Partner" InitialValue="0"
                            ValidationGroup="main"  Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label6" Text="Borrower Type:" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlBorrowerType" CssClass="cmbField" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlBorrowerType_SelectedIndexChanged">
                        </asp:DropDownList>
                        <span id="Span3" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvBorrowerType" runat="server"
                            ControlToValidate="ddlBorrowerType" ErrorMessage="<br>Select Borrower Type" InitialValue="0"
                            ValidationGroup="main"  Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="Label5" runat="server" CssClass="HeaderTextSmall" Text="Eligibilty Criteria"></asp:Label>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label7" Text="Min Loan amount:" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass="txtField" ID="txtMinLoanAmount" runat="server"></asp:TextBox>
                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator10" runat="server"
                            ControlToValidate="txtMinLoanAmount" Type="Double" ErrorMessage="Invalid value"
                            Operator="GreaterThanEqual" ValueToCompare="0" ValidationGroup="main" Display="Dynamic"></asp:CompareValidator>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label8" Text="Max Loan Amount:" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox CssClass="txtField" ID="txtMaxLoanAmount" runat="server"></asp:TextBox>
                        <%--<asp:Label ID="lblMaxLoanAmount" runat="server" Visible="false"></asp:Label>--%>
                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator22" runat="server"
                            ControlToValidate="txtMaxLoanAmount" ControlToCompare="txtMinLoanAmount" ErrorMessage="Max Loan Amount should be greater than Min Loan Amount"
                            Operator="GreaterThanEqual" Type="Double" ValidationGroup="main" Display="Dynamic"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label9" Text="Min loan period(in months):" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox CssClass="txtField" ID="txtMinLoanPeriod" runat="server"></asp:TextBox>
                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator11" runat="server"
                            ControlToValidate="txtMinLoanPeriod" Type="Integer" ErrorMessage="Invalid value"
                            Operator="GreaterThanEqual" ValueToCompare="0" ValidationGroup="main"></asp:CompareValidator>
                        <asp:Label ID="lblMinLoanPeriod" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label10" Text="Max loan period(in months):" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox CssClass="txtField" ID="txtMaxLoanPeriod" runat="server"></asp:TextBox>
                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator4" runat="server"
                            ControlToValidate="txtMaxLoanPeriod" ControlToCompare="txtMinLoanPeriod" ErrorMessage="Max Loan Period should be greater than Min Loan Period "
                            Operator="GreaterThanEqual" Type="Double" ValidationGroup="main"></asp:CompareValidator>
                        <asp:Label ID="lblMaxLoanPeriod" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label11" Text="Prime Lending Rate%(PLR):" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox CssClass="txtField" ID="txtPrimeLendingRate" runat="server"></asp:TextBox>
                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator3" runat="server"
                            ControlToValidate="txtPrimeLendingRate" Type="Double" ErrorMessage="Invalid value"
                            Operator="GreaterThanEqual" ValueToCompare="0" ValidationGroup="main"></asp:CompareValidator>
                        <asp:Label ID="lblPrimeLendingRate" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label12" Text="Floating Rate of interest:" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:RadioButtonList ID="rdoFloatingRate" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="true" OnSelectedIndexChanged="rdoFloatingRate_SelectedIndexChanged">
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:Label ID="lblFloatingRate" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label13" Text="Margin to be Maintained:(%)" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox CssClass="txtField" ID="txtMargin" Width="40px" runat="server"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidator1" SetFocusOnError="true" Type="Double" ErrorMessage="Margin should be 0 - 100%"
                            MinimumValue="0" MaximumValue="100" ControlToValidate="txtMargin" runat="server"></asp:RangeValidator>
                        <%--<asp:CompareValidator SetFocusOnError="true" ID="CompareValidator7" runat="server"
                            ControlToValidate="txtMargin" Type="Double" ErrorMessage="Margin should be less than 100%"
                            Operator="LessThan" ValueToCompare="100.00"></asp:CompareValidator>--%>
                        <asp:Label ID="lblMargin" runat="server" Visible="false" ValidationGroup="main"></asp:Label>
                    </td>
                </tr>
                <tr id="tblIndividual1" runat="server" visible="false">
                    <td class="leftField">
                        <asp:Label ID="Label14" Text="Min age:(Years)" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox CssClass="txtField" ID="txtMinAge" MaxLength="2" Width="30px" runat="server"></asp:TextBox>
                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator8" runat="server"
                            ControlToValidate="txtMinAge" Type="Integer" ErrorMessage="Invalid value" Operator="GreaterThanEqual"
                            ValueToCompare="0" ValidationGroup="main"></asp:CompareValidator>
                        <%-- <asp:Label ID="lblMinAge" runat="server" Visible="false"></asp:Label>--%>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label15" Text="Max age:(Years)" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox CssClass="txtField" ID="txtMaxAge" MaxLength="3" Width="30px" runat="server"></asp:TextBox>
                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator1" runat="server"
                            ControlToCompare="txtMinAge" ControlToValidate="txtMaxAge" Type="Integer" ErrorMessage="Max age should be greater than Min age"
                            Operator="GreaterThan" ValidationGroup="main"></asp:CompareValidator>
                        <%--<asp:Label ID="lblMaxAge" runat="server" Visible="false"></asp:Label>--%>
                    </td>
                </tr>
                <tr id="tblIndividual2" runat="server" visible="false">
                    <td class="leftField">
                        <asp:Label ID="Label19" Text="Min Salary Amount:" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox CssClass="txtField" ID="txtMinSalary" runat="server"></asp:TextBox>
                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator9" runat="server"
                            ControlToValidate="txtMinSalary" Type="Double" ErrorMessage="Invalid value" Operator="GreaterThanEqual"
                            ValidationGroup="main" ValueToCompare="0"></asp:CompareValidator>
                        <%--<asp:Label ID="lblMinSalary" runat="server" Visible="false"></asp:Label>--%>
                    </td>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr id="tblNonIndividual1" runat="server" visible="false">
                    <td class="leftField">
                        <asp:Label ID="Label16" Text="Minimum Profit Amount:" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox CssClass="txtField" ID="txtMinimumProfitAmount" runat="server" MaxLength="19"></asp:TextBox>
                        <%-- <asp:Label ID="lblMinimumProfitAmount" runat="server" Visible="false"></asp:Label>--%>
                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator5" runat="server"
                            ControlToValidate="txtMinimumProfitAmount" Type="Double" ErrorMessage="Invalid value"
                            Operator="GreaterThanEqual" ValueToCompare="0" ValidationGroup="main"></asp:CompareValidator>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label17" Text="Minimum Profit Period(Years):" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox CssClass="txtField" ID="txtMinimumProfitPeriod" runat="server" MaxLength="3"
                            Style="width: 50px"></asp:TextBox>
                        <%-- <asp:Label ID="lblMinimumProfitPeriod" runat="server" Visible="false"></asp:Label>--%>
                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator6" runat="server"
                            ControlToValidate="txtMinimumProfitPeriod" Type="Integer" ErrorMessage="Invalid value"
                            Operator="GreaterThanEqual" ValueToCompare="0" ValidationGroup="main"></asp:CompareValidator>
                    </td>
                </tr>
                <tr valign="top">
                    <td class="leftField" valign="top">
                        <asp:Label ID="Label18" Text="Remark:" runat="server" CssClass="FieldName" Style="vertical-align: text-top"></asp:Label>
                    </td>
                    <td colspan="3" class="rightField">
                        <asp:TextBox CssClass="txtField" ID="txtRemarks" MaxLength="150" runat="server" TextMode="MultiLine"
                            Rows="3" Style="width: 300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="Label1" runat="server" CssClass="HeaderTextSmall" Text="Interest Rate %"></asp:Label>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvAdviserInterestRates" runat="server" AutoGenerateColumns="False"
                            CellPadding="4" DataKeyNames="ALSIR_LoanSchemeInterestRateId" AllowSorting="True"
                            CssClass="GridViewStyle" EmptyDataText="No Interest rates available." AutoGenerateEditButton="True"
                            OnRowEditing="gvAdviserInterestRates_RowEditing" OnRowUpdating="gvAdviserInterestRates_RowUpdating"
                            OnRowCancelingEdit="gvAdviserInterestRates_RowCancelingEdit">
                            <FooterStyle CssClass="FooterStyle" />
                            <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            <HeaderStyle CssClass="HeaderStyle" />
                            <EditRowStyle CssClass="EditRowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Category
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("ALSIR_InterestCategory") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox CssClass="txtField" MaxLength="30" ID="txtCategory" runat="server" Text='<%# Eval("ALSIR_InterestCategory") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox class="smallTxt" ID="txtCategory" runat="server" MaxLength="30" Text='<%# Eval("ALSIR_InterestCategory") %>'></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Min Finance
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMinFinance" runat="server" Text='<%# Eval("ALSIR_MinimumFinance","{0:f2}") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox CssClass="txtField" ID="txtMinFinance" MaxLength="19" runat="server"
                                            Text='<%# Eval("ALSIR_MinimumFinance","{0:f2}") %>'></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtMinFinance_E" runat="server" Enabled="True" TargetControlID="txtMinFinance"
                                            FilterType="Custom, Numbers" ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox class="smallTxt" ID="txtMinFinance" MaxLength="19" runat="server" Text='<%# Eval("ALSIR_MinimumFinance","{0:f2}") %>'></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtMinFinance_E" runat="server" Enabled="True" TargetControlID="txtMinFinance"
                                            FilterType="Custom, Numbers" ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="ALSIR_MaximumFinance" HeaderText="Max Finance %" />--%>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Max Finance
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaxFinance" runat="server" Text='<%# Eval("ALSIR_MaximumFinance","{0:f2}") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox CssClass="txtField" ID="txtMaxFinance" MaxLength="19" runat="server"
                                            Text='<%# Eval("ALSIR_MaximumFinance","{0:f2}") %>'></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtMaxFinance_E" runat="server" Enabled="True" TargetControlID="txtMaxFinance"
                                            FilterType="Custom, Numbers" ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator25" runat="server"
                                            ControlToValidate="txtMaxFinance" ControlToCompare="txtMinFinance" ErrorMessage="Max Finance should be greater than Min Finance"
                                            Operator="GreaterThanEqual" Type="Double" ValidationGroup="main" Display="Dynamic"></asp:CompareValidator>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox class="smallTxt" ID="txtMaxFinance" MaxLength="19" runat="server" Text='<%# Eval("ALSIR_MaximumFinance","{0:f2}") %>'></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtMaxFinance_E" runat="server" Enabled="True" TargetControlID="txtMaxFinance"
                                            FilterType="Custom, Numbers" ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator2" runat="server"
                                            ControlToValidate="txtMaxFinance" ControlToCompare="txtMinFinance" ErrorMessage="Max Finance should be greater than Min Finance"
                                            Operator="GreaterThanEqual" Type="Double" ValidationGroup="main" Display="Dynamic"></asp:CompareValidator>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="ALSIR_MinimumPeriod" HeaderText="Min. Period (months)" />--%>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Min Period(in months)</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMinimumPeriod" runat="server" Text='<%# Eval("ALSIR_MinimumPeriod") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox CssClass="txtField" ID="txtMinimumPeriod" MaxLength="4" runat="server"
                                            Text='<%# Eval("ALSIR_MinimumPeriod") %>'></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtMinimumPeriod_E" runat="server" Enabled="True"
                                            TargetControlID="txtMinimumPeriod" FilterType="Numbers">
                                        </cc1:FilteredTextBoxExtender>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox class="smallTxt" ID="txtMinimumPeriod" MaxLength="4" runat="server"
                                            Text='<%# Eval("ALSIR_MinimumPeriod") %>'></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtMinimumPeriod_E" runat="server" Enabled="True"
                                            TargetControlID="txtMinimumPeriod" FilterType="Numbers">
                                        </cc1:FilteredTextBoxExtender>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Max Period(in months)</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaximumPeriod" runat="server" Text='<%# Eval("ALSIR_MaximumPeriod") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox CssClass="txtField" ID="txtMaximumPeriod" MaxLength="4" runat="server"
                                            Text='<%# Eval("ALSIR_MaximumPeriod") %>'></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtMaximumPeriod_E" runat="server" Enabled="True"
                                            TargetControlID="txtMaximumPeriod" FilterType="Numbers">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator26" runat="server"
                                            ControlToValidate="txtMaximumPeriod" ControlToCompare="txtMinimumPeriod" ErrorMessage="Max Period should be greater than Min Period"
                                            Operator="GreaterThanEqual" Type="Double" ValidationGroup="main" Display="Dynamic"></asp:CompareValidator>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox class="smallTxt" ID="txtMaximumPeriod" MaxLength="4" runat="server"
                                            Text='<%# Eval("ALSIR_MaximumPeriod") %>'></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtMaximumPeriod_E" runat="server" Enabled="True"
                                            TargetControlID="txtMaximumPeriod" FilterType="Numbers">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:CompareValidator SetFocusOnError="true" ID="CompareValidator28" runat="server"
                                            ControlToValidate="txtMaximumPeriod" ControlToCompare="txtMinimumPeriod" ErrorMessage="Max Period should be greater than Min Period"
                                            Operator="GreaterThanEqual" Type="Double" ValidationGroup="main" Display="Dynamic"></asp:CompareValidator>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Diff. Interest Rate</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDifferentialInterestRate" runat="server" Text='<%# Eval("ALSIR_DifferentialInterestRate","{0:f2}") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox CssClass="txtField" ID="txtDifferentialInterestRate" runat="server"
                                            MaxLength="19" Text='<%# Eval("ALSIR_DifferentialInterestRate") %>'></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtDifferentialInterestRate_E" runat="server" Enabled="True"
                                            TargetControlID="txtDifferentialInterestRate" FilterType="Custom, Numbers" ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox class="smallTxt" ID="txtDifferentialInterestRate" MaxLength="19" runat="server"
                                            Text='<%# Eval("ALSIR_DifferentialInterestRate","{0:f2}") %>'></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtDifferentialInterestRate_E" runat="server" Enabled="True"
                                            TargetControlID="txtDifferentialInterestRate" FilterType="Custom, Numbers" ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Max Finance %</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaximumFinancePer" MaxLength="19" runat="server" Text='<%# Eval("ALSIR_MaximumFinancePer","{0:f2}") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox CssClass="txtField" ID="txtMaximumFinancePer" MaxLength="19" runat="server"
                                            Text='<%# Eval("ALSIR_MaximumFinancePer","{0:f2}") %>'></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtMaximumFinancePer_E" runat="server" Enabled="True"
                                            TargetControlID="txtMaximumFinancePer" FilterType="Custom, Numbers" ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox class="smallTxt" ID="txtMaximumFinancePer" MaxLength="19" runat="server"
                                            Text='<%# Eval("ALSIR_MaximumFinancePer","{0:f2}") %>'></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtMaximumFinancePer_E" runat="server" Enabled="True"
                                            TargetControlID="txtMaximumFinancePer" FilterType="Custom, Numbers" ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Processing Chg.%</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblProcessingCharges" runat="server" Text='<%# Eval("ALSIR_ProcessingCharges","{0:f2}") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtProcessingCharges" runat="server" CssClass="txtField" MaxLength="10"
                                            Text='<%# Eval("ALSIR_ProcessingCharges","{0:f2}") %>'></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtProcessingCharges_E" runat="server" Enabled="True"
                                            TargetControlID="txtProcessingCharges" FilterType="Custom, Numbers" ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox class="smallTxt" ID="txtProcessingCharges" runat="server" MaxLength="10"
                                            Text='<%# Eval("ALSIR_ProcessingCharges","{0:f2}") %>'></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtProcessingCharges_E" runat="server" Enabled="True"
                                            TargetControlID="txtProcessingCharges" FilterType="Custom, Numbers" ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Preclosing Chg.%</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPreClosingCharges" runat="server" Text='<%# Eval("ALSIR_PreClosingCharges","{0:f2}") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox CssClass="txtField" ID="txtPreClosingCharges" MaxLength="10" runat="server"
                                            Text='<%# Eval("ALSIR_PreClosingCharges","{0:f2}") %>'></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtPreClosingCharges_E" runat="server" Enabled="True"
                                            TargetControlID="txtPreClosingCharges" FilterType="Custom, Numbers" ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox class="smallTxt" ID="txtPreClosingCharges" MaxLength="10" runat="server"
                                            Text='<%# Eval("ALSIR_PreClosingCharges","{0:f2}") %>'></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtPreClosingCharges_E" runat="server" Enabled="True"
                                            TargetControlID="txtPreClosingCharges" FilterType="Custom, Numbers" ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Panel ID="pnlDocumentsHeader" runat="server">
                            <table style="width: 450px; cursor: pointer;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblApplicationEntry" Text="Documents to be submitted" runat="server"
                                            CssClass="HeaderTextSmall">
                                        </asp:Label>
                                        <asp:Image ID="imgApplicationEntry" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <hr />
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlDocuments">
                            <asp:Table ID="tblDocuments" runat="server" Style="border: solid 1px #CCC" Width="550px"
                                rules="rows">
                            </asp:Table>
                        </asp:Panel>
                        <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" TargetControlID="pnlDocuments"
                            ExpandControlID="pnlDocumentsHeader" CollapseControlID="pnlDocumentsHeader" ImageControlID="imgApplicationEntry"
                            ExpandedImage="~/Images/arrow_double_up_7.gif" CollapsedImage="~/Images/arrow_double_down_7.gif"
                            Collapsed="false" SuppressPostBack="true">
                        </cc1:CollapsiblePanelExtender>
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGLongButton" OnClick="btnSubmit_Click"
                Text="Create Scheme" ValidationGroup="main" />
        </td>
    </tr>
</table>
