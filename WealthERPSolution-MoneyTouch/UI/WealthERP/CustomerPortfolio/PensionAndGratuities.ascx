<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PensionAndGratuities.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PensionAndGratuities" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
        <table style="width: 100%;">
            <tr>
                <td colspan="6">
                    <asp:Label ID="lblHeader" Text="Pension & Gratuities Portfolio Details" runat="server" CssClass="HeaderTextBig"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="6" class="tdRequiredText">
                    <label id="lbl" class="lblRequiredText">
                        Note: Fields marked with ' * ' are compulsory</label>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
                </td>
                <td colspan="5" class="rightField">
                    <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" runat="server"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table style="width: 100%;">
          
            <tr>
                <td colspan="6">
                    <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                        OnClick="lnkBtnBack_Click"></asp:LinkButton>
                </td>
            </tr>
             <tr id="trEditButton" runat="server" visible="false">
                <td colspan="6">
                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" OnClick="lnkEdit_Click" CssClass="LinkButtons">
                    </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblInstrumentCat" runat="server" Text="Asset Category:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" colspan="2">
                    <asp:Label ID="lblInstrumentCategory" runat="server" CssClass="Field"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:Label ID="Label34" runat="server" Text="Account Details" CssClass="HeaderTextSmall"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="leftField">
                    <asp:Label ID="Label35" runat="server" Text="Account Identifier:" CssClass="FieldName"></asp:Label>
                </td>
                <td colspan="4" class="rightField">
                    <asp:Label ID="lblAccountNum" runat="server" CssClass="Field"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="leftField">
                    <asp:Label ID="Label1" runat="server" Text="Account Opening Date:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:Label ID="lblOpeningDate" runat="server" CssClass="Field" Text=""></asp:Label>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label2" runat="server" Text="Account With:" CssClass="FieldName"></asp:Label>
                </td>
                <td colspan="2" class="rightField">
                    <asp:Label ID="lblAccWith" runat="server" CssClass="Field" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="leftField">
                    <asp:Label ID="Label36" runat="server" Text="Mode Of Holding:" CssClass="FieldName"></asp:Label>
                </td>
                <td colspan="4" class="rightField">
                    <asp:Label ID="lblModeOfHolding" runat="server" CssClass="Field" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    &nbsp;
                </td>
            </tr>
           
            <tr id="trEditSpace" runat="server" visible="false">
                <td colspan="6">
                    &nbsp;
                </td>
            </tr>
            <tr id="trGraOrganisation" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblGraOrganisationName" Text="Organisation Name:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td colspan="4" class="rightField">
                    <asp:TextBox ID="txtGraOrganisationName" Text="" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span1" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtGraOrganisationName"
                        ErrorMessage="Please enter the Organisation Name" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="trGraAmount" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblGraAmount" Text="Gratuity Amount(Rs):" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtGraAmount" Text="" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span2" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtGraAmount"
                        ErrorMessage="Please enter the Gratuity Amount" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtGraAmount" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trGraRemarks" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblGraRemarks" Text="Remarks:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtGraRemarks" Text="" runat="server" TextMode="MultiLine" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr id="trEPFOrganisation" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblEPFOrganisationName" Text="Organisation Name:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtEPFOrganisationName" Text="" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span3" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEPFOrganisationName"
                        ErrorMessage="Please enter the OrganisationName" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="trEPFAccum" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblEPFAccum" Text="EPF Accumulated Amount as on Date(Rs):" runat="server"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEPFAccum" Text="" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span4" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtEPFAccum"
                        ErrorMessage="Please enter the EPF Accumulated Amount" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtEPFAccum" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                </td>
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblEPFAccumFiscal" Text="Fiscal Year:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <%--Defaulted To Last Fiscal Year || Not Editable--%>
                    <asp:DropDownList ID="ddlEPFAccumFiscalYear" runat="server" CssClass="cmbField" Enabled="false">
                    </asp:DropDownList>
                    <span id="Span27" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator24" runat="server" ControlToValidate="ddlEPFAccumFiscalYear"
                        ErrorMessage="Please select a Fiscal Year" Operator="NotEqual" ValueToCompare="Select a Fiscal Year"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trEPFEmployee" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblEPFEmployeeContribution" Text="Employee Contribution(Rs):" runat="server"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEPFEmployeeContribution" Text="" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span6" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtEPFEmployeeContribution"
                        ErrorMessage="Please enter the Employee Contribution" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtEPFEmployeeContribution" Operator="DataTypeCheck"
                        CssClass="cvPCG"></asp:CompareValidator>
                </td>
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblEPFEmployerContribution" Text="Employer Contribution(Rs):" runat="server"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEPFEmployerContribution" Text="" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span5" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtEPFEmployerContribution"
                        ErrorMessage="Please enter the Employer Contribution" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator10" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtEPFEmployerContribution" Operator="DataTypeCheck"
                        CssClass="cvPCG"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trEPFSpace1" runat="server">
                <td colspan="6">
                    &nbsp;
                </td>
            </tr>
            <tr id="trEPFInterestDetails" runat="server">
                <td colspan="6">
                    <asp:Label ID="lblEPFInterestDetails" Text="Interest Details" runat="server" CssClass="HeaderTextSmall"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr id="trEPFInterestRate" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblEPFInterestRate" Text="Interest Rate(%):" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtEPFInterestRate" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span7" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtEPFInterestRate"
                        ErrorMessage="Please enter the Interest Rate" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator11" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtEPFInterestRate" Operator="DataTypeCheck"
                        CssClass="cvPCG"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trEPFInterestBasis" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblEPFInterestBasis" Text="Interest Basis:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlEPFInterestBasis" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlEPFInterestBasis_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <span id="Span8" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="cvInsuranceIssuerCode" runat="server" ControlToValidate="ddlEPFInterestBasis"
                        ErrorMessage="Please select an Interest Basis" Operator="NotEqual" ValueToCompare="Select an Interest Basis"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblEPFInterestCalFreq" Text="Interest Calculation Frequency:" runat="server"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlEPFInterestCalFreq" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlEPFInterestCalFreq_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <span id="Span9" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlEPFInterestCalFreq"
                        ErrorMessage="Please select a frequency" Operator="NotEqual" ValueToCompare="Select a Frequency"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trEPFInterestAccum" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblEPFInterestAccum" Text="Interest Accumulated(Rs):" runat="server"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtEPFInterestAccum" Text="" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span10" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtEPFInterestAccum"
                        ErrorMessage="Please enter the Interest Accumulated" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator12" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtEPFInterestAccum" Operator="DataTypeCheck"
                        CssClass="cvPCG"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trEPFCurrentVal" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblEPFCurrentValue" Text="Current Value(Rs):" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtEPFCurrentValue" Text="" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span11" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtEPFCurrentValue"
                        ErrorMessage="Please enter the Current Value" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator13" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtEPFCurrentValue" Operator="DataTypeCheck"
                        CssClass="cvPCG"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trEPFRemarks" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblEPFRemarks" Text="Remarks:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtEPFRemarks" Text="" runat="server" TextMode="MultiLine" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr id="trPPFAccum" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblPPFAccum" Text="PPF Accumulated as on Date(Rs):" runat="server"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPPFAccum" Text="" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span12" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtPPFAccum"
                        ErrorMessage="Please enter the PPF Accumulated" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator14" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtPPFAccum" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                </td>
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblPPFAccumFiscal" Text="Fiscal Year:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <%--Defaulted To Last Fiscal Year || Not Editable--%>
                    <asp:DropDownList ID="ddlPPFAccumFiscal" runat="server" CssClass="cmbField" Enabled="false">
                    </asp:DropDownList>
                    <span id="Span13" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlPPFAccumFiscal"
                        ErrorMessage="Please select a Fiscal Year" Operator="NotEqual" ValueToCompare="Select a Fiscal Year"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trPPFYearlyContribution" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblPPFYearlyContribution" Text="Yearly Contribution(Rs):" runat="server"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtPPFYearlyContribution" Text="" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span14" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtPPFYearlyContribution"
                        ErrorMessage="Please enter the Yearly Contribution" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator15" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtPPFYearlyContribution" Operator="DataTypeCheck"
                        CssClass="cvPCG"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trPPFSpace1" runat="server">
                <td colspan="6">
                    &nbsp;
                </td>
            </tr>
            <tr id="trPPFInterestDetails" runat="server">
                <td colspan="6">
                    <asp:Label ID="lblPPFInterestDetails" Text="Interest Details" runat="server" CssClass="HeaderTextSmall"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr id="trPPFInterestRate" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblPPFInterestRate" Text="Interest Rate(%):" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td colspan="4" class="rightField">
                    <asp:TextBox ID="txtPPFInterestRate" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span15" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtPPFInterestRate"
                        ErrorMessage="Please enter the Interest Rate" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator16" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtPPFInterestRate" Operator="DataTypeCheck"
                        CssClass="cvPCG"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trPPFInterestBasis" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblPPFInterestBasis" Text="Interest Basis:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlPPFInterestBasis" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlPPFInterestBasis_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <span id="Span16" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlPPFInterestBasis"
                        ErrorMessage="Please select an Interest Basis" Operator="NotEqual" ValueToCompare="Select an Interest Basis"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblPPFInterestFrequency" Text="Interest Calculation Frequency:" runat="server"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlPPFInterestFrequency" runat="server" CssClass="cmbField"
                        OnSelectedIndexChanged="ddlPPFInterestFrequency_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <span id="Span17" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlPPFInterestFrequency"
                        ErrorMessage="Please select a Frequency" Operator="NotEqual" ValueToCompare="Select a Frequency"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trPPFInterestAccumulated" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblPPFInterestAccumulated" Text="Interest Accumulated(Rs):" runat="server"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td colspan="4" class="rightField">
                    <asp:TextBox ID="txtPPFInterestAccumulated" Text="" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span18" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtPPFInterestAccumulated"
                        ErrorMessage="Please enter the Interest Accumulated" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator17" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtPPFInterestAccumulated" Operator="DataTypeCheck"
                        CssClass="cvPCG"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trPPFCurrentValue" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblPPFCurrentValue" Text="Current Value(Rs):" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td colspan="4" class="rightField">
                    <asp:TextBox ID="txtPPFCurrentValue" Text="" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span19" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtPPFCurrentValue"
                        ErrorMessage="Please enter the Current Value" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator18" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtPPFCurrentValue" Operator="DataTypeCheck"
                        CssClass="cvPCG"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trPPFRemarks" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblPPFRemarks" Text="Remarks:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td colspan="4" class="rightField">
                    <asp:TextBox ID="txtPPFRemarks" Text="" runat="server" TextMode="MultiLine" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr id="trSuperAccum" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblSuperAccum" Text="Superannuation Accumulated Amount as on Date:"
                        runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtSuperAccum" Text="" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span20" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="txtSuperAccum"
                        ErrorMessage="Please enter the Superannuation Accumulated Amount" Display="Dynamic"
                        runat="server" CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator19" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtSuperAccum" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                </td>
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblSuperAccumFiscal" Text="Fiscal Year:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <%--Defaulted To Last Fiscal Year || Not Editable--%>
                    <asp:DropDownList ID="ddlSuperAccumFiscal" runat="server" CssClass="cmbField" Enabled="false">
                    </asp:DropDownList>
                    <span id="Span28" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator25" runat="server" ControlToValidate="ddlSuperAccumFiscal"
                        ErrorMessage="Please select a Fiscal Year" Operator="NotEqual" ValueToCompare="Select a Fiscal Year"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trSuperYearlyContribution" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblSuperYearlyContribution" Text="Yearly Contribution(Rs):" runat="server"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td colspan="4" class="rightField">
                    <asp:TextBox ID="txtSuperYearlyContribution" Text="" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span21" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="txtSuperYearlyContribution"
                        ErrorMessage="Please enter the Yearly Contribution" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator20" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtSuperYearlyContribution" Operator="DataTypeCheck"
                        CssClass="cvPCG"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trSuperSpace1" runat="server">
                <td colspan="6">
                    &nbsp;
                </td>
            </tr>
            <tr id="trSuperInterestDetails" runat="server">
                <td colspan="6">
                    <asp:Label ID="lblSuperInterestDetails" Text="Interest Details" runat="server" CssClass="HeaderTextSmall"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr id="trSuperInterestRate" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblSuperInterestRate" Text="Interest Rate(%):" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td colspan="4" class="rightField">
                    <asp:TextBox ID="txtSuperInterestRate" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span22" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ControlToValidate="txtSuperInterestRate"
                        ErrorMessage="Please enter the Interest Rate" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator21" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtSuperInterestRate" Operator="DataTypeCheck"
                        CssClass="cvPCG"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trSuperInterestBasis" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblSuperInterestBasis" Text="Interest Basis:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlSuperInterestBasis" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlSuperInterestBasis_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <span id="Span23" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="ddlSuperInterestBasis"
                        ErrorMessage="Please select an Interest Basis" Operator="NotEqual" ValueToCompare="Select an Interest Basis"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblSuperInterestCalcFreq" Text="Interest Calculation Frequency:" runat="server"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlSuperInterestCalcFreq" runat="server" CssClass="cmbField"
                        OnSelectedIndexChanged="ddlSuperInterestCalcFreq_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <span id="Span24" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="ddlSuperInterestCalcFreq"
                        ErrorMessage="Please select a Frequency" Operator="NotEqual" ValueToCompare="Select a Frequency"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trSuperInterestAccum" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblSuperInterestAccum" Text="Interest Accumulated(Rs):" runat="server"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td colspan="4" class="rightField">
                    <asp:TextBox ID="txtSuperInterestAccum" Text="" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span25" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ControlToValidate="txtSuperInterestAccum"
                        ErrorMessage="Please enter the Interest Accumulated" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator22" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtSuperInterestAccum" Operator="DataTypeCheck"
                        CssClass="cvPCG"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trSuperCurrentValue" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblSuperCurrentValue" Text="Current Value(Rs):" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td colspan="4" class="rightField">
                    <asp:TextBox ID="txtSuperCurrentValue" Text="" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span26" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ControlToValidate="txtSuperCurrentValue"
                        ErrorMessage="Please enter the Current Value" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator23" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtSuperCurrentValue" Operator="DataTypeCheck"
                        CssClass="cvPCG"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trSuperRemarks" runat="server">
                <td colspan="2" class="leftField">
                    <asp:Label ID="lblSuperRemarks" Text="Remarks:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td colspan="4" class="rightField">
                    <asp:TextBox ID="txtSuperRemarks" Text="" runat="server" TextMode="MultiLine" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                </td>
            </tr>
            <tr>
                <td colspan="6">
                </td>
            </tr>
            <tr id="trButton" runat="server" visible="false">
                <td colspan="6" class="SubmitCell">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" OnClick="Button1_Click"
                        onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_PensionAndGratuities_btnSubmit');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_PensionAndGratuities_btnSubmit');" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
