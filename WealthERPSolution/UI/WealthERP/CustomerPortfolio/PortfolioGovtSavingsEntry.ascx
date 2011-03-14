<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioGovtSavingsEntry.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioGovtSavingsEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
<%--<asp:UpdatePanel ID="up1" runat="server">
<contenttemplate>--%>
        <table style="width: 100%;" class="TableBackground">
        <tr>
                <td class="HeaderCell" colspan="4">
                    <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Govt Savings"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="tdRequiredText">
                    <label id="lbl" class="lblRequiredText">
                        Note: Fields marked with ' * ' are compulsory</label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                        OnClick="lnkBtnBack_Click" CausesValidation="false"></asp:LinkButton>
                &nbsp; &nbsp;  &nbsp;  &nbsp; 
                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="LinkButtons" CausesValidation="false" OnClick="lnkEdit_Click">Edit</asp:LinkButton>
                </td>
            </tr>
            
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Asset Category:"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:Label ID="lblInstrumentCategory" runat="server" CssClass="Field" Text="Instrument Category"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblAccountDetails" runat="server" CssClass="HeaderTextSmall" Text="Account Details"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="leftField" valign="top">
                    <asp:Label runat="server" CssClass="FieldName" Text="Account/Certificate No:" ID="lblAccountId"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtAccountId" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvAccountId" ControlToValidate="txtAccountId"
                        ErrorMessage="<br/>Please enter value." Display="Dynamic" runat="server"
                        CssClass="rfvPCG" />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label runat="server" CssClass="FieldName" Text="Account Opening Date:" ID="lblAccOpeningDate"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtAccOpenDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span13" class="spnRequiredField">*</span>
                    
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtAccOpenDate" 
                    Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtAccOpenDate_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtAccOpenDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <asp:RequiredFieldValidator ID="rfvAccOpenDate" ControlToValidate="txtAccOpenDate"
                        ErrorMessage="<br />Please select a Date" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvAccOpenDate" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtAccOpenDate" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label runat="server" CssClass="FieldName" Text="Account with:" ID="lblAccountwith"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtAccountWith" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label runat="server" CssClass="FieldName" Text="Mode of Holding:" ID="lblModeOfHold"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <%--<asp:Label ID="lblModeOfHolding" runat="server" CssClass="FieldName" Text="Mode Of Holding"></asp:Label>--%>
                    <asp:DropDownList ID="ddlModeOfHolding" runat="server" CssClass="cmbField" Enabled="false">
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblName" runat="server" CssClass="FieldName" Text="Asset Particulars:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtAssetParticulars" runat="server" CssClass="txtField"></asp:TextBox>
                    <%--<span id="Span1" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvAssetParticulars" ControlToValidate="txtAssetParticulars"
                        ErrorMessage="Please enter the Asset Particulars" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
                </td>
                <td class="leftField">
                    <asp:Label runat="server" CssClass="FieldName" Text="Asset Issuer:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlDebtIssuerCode" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                    <span id="Span2" class="spnRequiredField">*</span>
                    <br />
                    <asp:CompareValidator ID="cvDebtIssuerCode" runat="server" ControlToValidate="ddlDebtIssuerCode"
                        ErrorMessage="Please select a Asset Issuer" Operator="NotEqual" ValueToCompare="0"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblDepositDetails" runat="server" CssClass="HeaderTextSmall" Text="Deposit Details"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr runat="server" id="tdDepositAndMaturityDate">
                <td class="leftField">
                    <asp:Label ID="lblDepositDate" runat="server" CssClass="FieldName" Text="Deposit Date:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtDepositDate" runat="server" CssClass="txtField"></asp:TextBox>
                    
                    <cc1:CalendarExtender ID="txtDepositDate_CalendarExtender" runat="server" TargetControlID="txtDepositDate" 
                    Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtDepositDate_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtDepositDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    
                    <span runat="server" id="spanValidationDepositDate">
                    <span id="Span3" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvDepositDate" ControlToValidate="txtDepositDate"
                        ErrorMessage="Please select a Deposit Date" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    <asp:CompareValidator ID="cvDepositDate1" runat="server" ErrorMessage="<br/>The deposit date should not be greater than current date."
                        Type="Date" ControlToValidate="txtDepositDate" CssClass="cvPCG" Operator="LessThanEqual" 
                        ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                        
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvDepositDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtDepositDate" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                     </span>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblMaturityDate" runat="server" CssClass="FieldName" Text="Maturity Date:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtMaturityDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtMaturityDate_CalendarExtender" runat="server" TargetControlID="txtMaturityDate"
                    Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtMaturityDate_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtMaturityDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    
                    <asp:CompareValidator ID="cvMaturityDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtMaturityDate" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                    <span runat="server" id="spanValidationMaturityDate">
                    
                    <span id="Span4" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvMaturityDate" ControlToValidate="txtMaturityDate"
                        ErrorMessage="Please select a Maturity Date" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Maturity Date should be greater than Deposit Date"
                        Type="Date" ControlToValidate="txtMaturityDate" ControlToCompare="txtDepositDate" Operator="GreaterThan" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                        </span>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblDepositAmount" runat="server" CssClass="FieldName" Text="Deposit Amount:"></asp:Label>
                </td>
                <td class="rightField" colspan="3">
                    <asp:TextBox ID="txtDepositAmount" AutoPostBack="true" runat="server" CssClass="txtField" 
                        ontextchanged="txtDepositAmount_TextChanged"></asp:TextBox>
                     <asp:CompareValidator ID="cvDepositAmount" runat="server" ErrorMessage="<br/>Please enter a numeric value"
                        Type="Double" ControlToValidate="txtDepositAmount" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                    <span id="spanValidationDepositAmount" runat="server">
                    <span id="Span5" class="spnRequiredField">*</span>
                    
                    <asp:RequiredFieldValidator ID="rfvDepositAmount" ControlToValidate="txtDepositAmount"
                        ErrorMessage="<br/>Please enter the Deposit Amount" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                   </span>
                </td>
            </tr>
            <tr id= "trSubsequent" runat="server" visible="false">
                
                <td class="leftField">
                    <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Subsequent Deposit Amount:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtSubsqntDepositAmount" runat="server" CssClass="txtField"></asp:TextBox>
                     <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<br/>Please enter a numeric value"
                        Type="Double" ControlToValidate="txtSubsqntDepositAmount" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                    <span id="Span9" class="spnRequiredField">*</span>
                    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtSubsqntDepositAmount"
                        ErrorMessage="<br/>Please enter the Amount" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                </td>
            
                <td class="leftField">
                    <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Date:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtSubsqntDepositDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSubsqntDepositDate" 
                    Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                        TargetControlID="txtSubsqntDepositDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    
                    <span runat="server" id="span1">
                    <span id="Span7" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtSubsqntDepositDate"
                        ErrorMessage="Please select a Date" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="The date format should be dd/mm/yyyy"
                        Type="Date" ControlToValidate="txtSubsqntDepositDate" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                     </span>
                </td>
                
            </tr>
            <tr id= "trSubsequentFrequency" runat="server" visible="false">
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
                    <asp:Label ID="lblInterestDetails" runat="server" CssClass="HeaderTextSmall" Text="Interest Details"></asp:Label>
                    <hr />
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
                        GroupName="rbtnIsInterestAcc" AutoPostBack="True" Checked="true"/>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblInterestRate" runat="server" CssClass="FieldName" Text="Interest Rate Applicable %:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtInterstRate" runat="server" CssClass="txtField"></asp:TextBox>
                    <%--<asp:CompareValidator ID="cvInterstRate" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtInterstRate"  Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>--%>
                     <asp:RangeValidator ID="rvInterestRate" runat="server" ControlToValidate="txtInterstRate" MinimumValue="0" MaximumValue="100"
                      Type="Double" ErrorMessage="Enter a value between 0-100" CssClass="rfvPCG" Display="Dynamic" />
                        
                    <span id="spanValidationInterestRate" runat="server">
                    <span id="Span6" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvInterstRate" ControlToValidate="txtInterstRate"
                        ErrorMessage="<br/>Please enter the Interest Rate" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
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
                    <asp:TextBox ID="txtInterestAmtCredited" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="Label8" runat="server" CssClass="HeaderTextSmall" Text="Valuation"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblCurrentValue" runat="server" CssClass="FieldName" Text="Current Value:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtCurrentValue" runat="server" CssClass="txtField"></asp:TextBox>
                    <br />
                    <asp:CompareValidator ID="cvCurrentValue" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtCurrentValue"  Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblMaturityValue" runat="server" CssClass="FieldName" Text="Maturity Value:"></asp:Label>
                </td>
                <td class="rightField" runat="server" id="tdMaturityValue">
                    <asp:TextBox ID="txtMaturityValue" runat="server" CssClass="txtField"></asp:TextBox>
                    <br />
                    <asp:CompareValidator ID="cvMaturityValue" runat="server" ErrorMessage="Please enter a numeric value"
                        Type="Double" ControlToValidate="txtMaturityValue" Operator="DataTypeCheck" CssClass="cvPCG"
                        Display="Dynamic"></asp:CompareValidator>
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
                    <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="server" CssClass="txtField"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" class="SubmitCell">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnSubmit_Click"
                        onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_PortfolioGovtSavingsEntry_btnSubmit');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_PortfolioGovtSavingsEntry_btnSubmit');" />
                    <asp:Button ID="btnSaveChanges" runat="server" Text="Update" CssClass="PCGButton"
                        OnClick="btnSaveChanges_Click" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_PortfolioGovtSavingsEntry_btnSubmit');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_PortfolioGovtSavingsEntry_btnSubmit');" />
                </td>
            </tr>
        </table>
<%--    </contenttemplate>
</asp:UpdatePanel>--%>