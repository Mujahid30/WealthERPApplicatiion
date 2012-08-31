﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LiabilitiesMaintenanceForm.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.LiabilitiesMaintenanceForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript">
    function checkDate(sender, args) {

        var selectedDate = new Date();
        selectedDate = sender._selectedDate;

        var todayDate = new Date();
        var msg = "";

        if (selectedDate > todayDate) {
            sender._selectedDate = todayDate;
            sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            alert("Warning! - Date Cannot be in the future");
        }
    }
</script>

<script type="text/javascript" language="javascript">
    function ShiftValuet() {
        if (document.getElementById('<%=txtLoanAmount.ClientID %>').value > 0)
            document.getElementById('<%=txtLoanOutstandingAmount.ClientID %>').value = document.getElementById('<%=txtLoanAmount.ClientID %>').value;
    }
</script>

<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="upnlLiabilities" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td colspan="4">
                    <div class="divPageHeading">
                        <table cellspacing="0" cellpadding="3" width="100%">
                            <tr>
                                <td align="left">
                                    Liabilities Maintenance Form
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="tdRequiredText">
                    <label id="lbl" class="lblRequiredText">
                        Note: Fields marked with a ' * ' are compulsory</label>
                </td>
            </tr>
            <tr id="trEdit" runat="server">
                <td colspan="4">
                    <asp:LinkButton ID="lnkEdit" Text="Edit" runat="server" CssClass="LinkButtons" OnClick="lnkEdit_Click"
                        CausesValidation="false"></asp:LinkButton>
                </td>
            </tr>
            <%--  <tr id="trBack" runat="server">
                <td colspan="4">
                    <asp:LinkButton ID="lnkBack" Text="Back" runat="server" CssClass="LinkButtons" OnClick="lnkBtnBack_Click"
                        CausesValidation="false"></asp:LinkButton>
                </td>
            </tr>--%>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblBasicDetails" runat="server" Text="Basic Details" CssClass="HeaderTextSmall"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblLoanType" runat="server" Text="Loan Type :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlLoanType" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlLoanType_SelectedIndexChanged">
                    </asp:DropDownList>
                    <span id="Span1" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />Please select a loan type"
                        ValidationGroup="vgBtnSubmitTemp" ControlToValidate="ddlLoanType" Operator="NotEqual"
                        ValueToCompare="Select Loan Type" Display="Dynamic" CssClass="rfvPCG"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="Asset" runat="server" Text="Asset Particulars :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <%-- <asp:DropDownList ID="ddlGuarantor" runat="server" CssClass="cmbField">
                    </asp:DropDownList>--%>
                    <asp:TextBox ID="txtasset" MaxLength="10" runat="server" CssClass="txtField"></asp:TextBox>
                    <%--<span id="Span7" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<br />Please select a Guarantor"
                        ValidationGroup="btnSubmit" ControlToValidate="ddlGuarantor" Operator="NotEqual"
                        ValueToCompare="Select the Guarantor" Display="Dynamic" CssClass="rfvPCG"></asp:CompareValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblLoanAmount" runat="server" Text="Loan Amount :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtLoanAmount" runat="server" CssClass="txtField" MaxLength="18"
                        OnTextChanged="txtLoanAmount_TextChanged" onblur="ShiftValuet()" AutoPostBack="true"></asp:TextBox>
                    <span id="Span4" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtLoanAmount"
                        ErrorMessage="<br />Please enter a loan amount" Display="Dynamic" CssClass="rfvPCG"
                        runat="server" InitialValue="" ValidationGroup="vgBtnSubmitTemp">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtLoanAmount"
                        Display="Dynamic" CssClass="rfvPCG" runat="server" ErrorMessage="Not acceptable format"
                        ValidationExpression="^\d*(\.(\d{0,5}))?$"></asp:RegularExpressionValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblLender" runat="server" Text="Lender :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlLender" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlLender_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtOtherLender" Text="" CssClass="txtField" runat="server" Visible="false"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtOtherLender"
                        WatermarkText="Enter Lender Name">
                    </cc1:TextBoxWatermarkExtender>
                    <span id="Span2" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="<br />Please select a lender"
                        ValidationGroup="vgBtnSubmitTemp" ControlToValidate="ddlLender" Operator="NotEqual"
                        ValueToCompare="Select Lender" Display="Dynamic" CssClass="rfvPCG"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblInterestRate" runat="server" Text="Interest Rate % (p.a):" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtInterestRate" runat="server" CssClass="txtField" MaxLength="6"
                        OnTextChanged="txtInterestRate_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <span id="Span6" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtInterestRate"
                        ErrorMessage="<br />Please enter the interest rate" Display="Dynamic" CssClass="rfvPCG"
                        runat="server" InitialValue="" ValidationGroup="vgBtnSubmitTemp">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtInterestRate"
                        Display="Dynamic" CssClass="rfvPCG" runat="server" ErrorMessage="Not acceptable format"
                        ValidationExpression="^\d*(\.(\d{0,2}))?$"></asp:RegularExpressionValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="Label1" runat="server" Text="Loan Start Date:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtLoanStartDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span3" class="spnRequiredField">*</span>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtLoanStartDate"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtLoanStartDate"
                        WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <asp:CompareValidator ID="CVReceivedDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                        Type="Date" ControlToValidate="txtLoanStartDate" CssClass="cvPCG" Operator="DataTypeCheck"
                        ValidationGroup="vgBtnSubmitTemp" ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtLoanStartDate"
                        ErrorMessage="<br />Please enter a loan Start Date" Display="Dynamic" CssClass="rfvPCG"
                        runat="server" InitialValue="" ValidationGroup="vgBtnSubmitTemp">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblCompoundFrequency" runat="server" Text="Compound Frequency :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlCompoundFrequency" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlCompoundFrequency_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <span id="Span8" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<br />Please select an Action"
                        ValidationGroup="vgBtnSubmitTemp" ControlToValidate="ddlCompoundFrequency" Operator="NotEqual"
                        CssClass="rfvPCG" ValueToCompare="Select the Frequency" Display="Dynamic"></asp:CompareValidator>
                    <br />
                    <asp:RequiredFieldValidator ID="reqddlAdviser" runat="server" CssClass="rfvPCG" ErrorMessage="Please select an Action"
                        Text="Please select a Field" Display="Dynamic" ValidationGroup="vgBtnSubmitTemp"
                        ControlToValidate="ddlCompoundFrequency" InitialValue="0">
                    </asp:RequiredFieldValidator>
                </td>
                <%--<asp:TextBox ID="txtNoCoBorrowers" runat="server" CssClass="txtField" AutoPostBack="true"></asp:TextBox>
                    <span id="Span8" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtNoCoBorrowers"
                        ErrorMessage="<br />Please enter the number of co-borrowers" Display="Dynamic"
                        CssClass="rfvPCG" runat="server" InitialValue="" ValidationGroup="btnSubmit">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ControlToValidate="txtNoCoBorrowers"
                        ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                        Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>--%>
                </td>
                <td class="leftField">
                    <%-- <asp:Button ID="btnCoborrowers" runat="server" Text="Go" CssClass="PCGButton" OnClick="btnCoborrowers_Click"
                        CausesValidation="false" />--%>
                    <asp:Label ID="lblTenture" runat="server" Text="Tenure :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtTenture" runat="server" CssClass="txtField" OnTextChanged="txtTenture_TextChanged"
                        AutoPostBack="true"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtTenture"
                        WatermarkText="Years">
                    </cc1:TextBoxWatermarkExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please select a year" Display="Dynamic" ValidationGroup="vgBtnSubmitTemp"
                        ControlToValidate="txtTenture">
                    </asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtTenureMonths" runat="server" CssClass="txtField" OnTextChanged="txtTenureMonths_TextChanged"
                        AutoPostBack="true"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtTenureMonths"
                        WatermarkText="Months">
                    </cc1:TextBoxWatermarkExtender>
                    <span id="Span7" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please select a Month" Display="Dynamic" ValidationGroup="vgBtnSubmitTemp"
                        ControlToValidate="txtTenureMonths">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ControlToValidate="txtTenureMonths"
                        ValidationGroup="vgBtnSubmitTemp" Display="Dynamic" runat="server" CssClass="rfvPCG"
                        Operator="DataTypeCheck" ErrorMessage="Tenure in Years Not in acceptable format"
                        ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="txtTenture"
                        ValidationGroup="vgBtnSubmitTemp" Display="Dynamic" runat="server" CssClass="rfvPCG"
                        Operator="DataTypeCheck" ErrorMessage="Tenure in Months Not in acceptable format"
                        ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <%-- <tr id="trAssets" runat="server">
        <td class="leftField">
            <asp:Label ID="lblAddAsset" runat="server" Text="Do you want to add asset? :" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:RadioButton ID="rbtnYes" Text="Yes" runat="server" CssClass="cmbField" GroupName="grpAsset"
                OnCheckedChanged="rbtnYes_CheckedChanged" />
            <asp:RadioButton ID="rbtnNo" Text="No" runat="server" CssClass="cmbField" GroupName="grpAsset"
                OnCheckedChanged="rbtnNo_CheckedChanged" />
            <span id="Span3" class="spnRequiredField">*</span>
        </td>
        <td>
            &nbsp;</td>
        <td>
        </td>
    </tr>--%>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblPaymentOption" runat="server" Text="Payment Option :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlPaymentOption" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlPaymentOption_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <span id="Span10" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="<br />Please select an Action"
                        ValidationGroup="vgBtnSubmitTemp" ControlToValidate="ddlPaymentOption" Operator="NotEqual"
                        CssClass="rfvPCG" ValueToCompare="Select the Payment Option" Display="Dynamic"></asp:CompareValidator>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please select an Action" Text="Please select a Field" Display="Dynamic"
                        ValidationGroup="vgBtnSubmitTemp" ControlToValidate="ddlPaymentOption" InitialValue="0">
                    </asp:RequiredFieldValidator>
                    <%--<asp:TextBox ID="txtNoCoBorrowers" runat="server" CssClass="txtField" AutoPostBack="true"></asp:TextBox>
                    <span id="Span8" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtNoCoBorrowers"
                        ErrorMessage="<br />Please enter the number of co-borrowers" Display="Dynamic"
                        CssClass="rfvPCG" runat="server" InitialValue="" ValidationGroup="btnSubmit">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ControlToValidate="txtNoCoBorrowers"
                        ValidationGroup="btnSubmit" Display="Dynamic" runat="server" CssClass="rfvPCG"
                        Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>--%>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblAmountPrepaid" runat="server" Text="Loan Outstanding Amount :"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtLoanOutstandingAmount" runat="server" CssClass="txtField"></asp:TextBox>
                    <%--     <span id="Span14" class="spnRequiredField">*</span>--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtLoanOutstandingAmount"
                        ErrorMessage="<br />Please enter the Loan Outstanding Amount" Display="Dynamic"
                        CssClass="rfvPCG" runat="server" InitialValue="" ValidationGroup="vgBtnSubmitTemp">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtLoanOutstandingAmount"
                        Display="Dynamic" CssClass="rfvPCG" runat="server" ErrorMessage="Not acceptable format"
                        ValidationExpression="^\d*(\.(\d{0,5}))?$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
              <td class="leftField">
                    <asp:Label ID="lblGuarantor" runat="server" Text="Guarantor :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <%-- <asp:DropDownList ID="ddlGuarantor" runat="server" CssClass="cmbField">
                    </asp:DropDownList>--%>
                    <asp:TextBox ID="txtGuarantor" Text="" runat="server" CssClass="txtField"></asp:TextBox>
                    <%--<span id="Span7" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<br />Please select a Guarantor"
                        ValidationGroup="btnSubmit" ControlToValidate="ddlGuarantor" Operator="NotEqual"
                        ValueToCompare="Select the Guarantor" Display="Dynamic" CssClass="rfvPCG"></asp:CompareValidator>--%>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <%--  <asp:GridView ID="gvCoBorrower" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        CssClass="GridViewStyle" AllowSorting="true" OnRowDataBound="gvCoBorrower_RowDataBound"
                        DataKeyNames="CLA_LiabilitiesAssociationId">
                        <FooterStyle CssClass="FieldName" />
                        <RowStyle CssClass="RowStyle" />
                        <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Borrowers">
                                <ItemTemplate>
                                    <asp:Label ID="lblMainCustomer" runat="server" CssClass="FieldName"></asp:Label>
                                    <asp:DropDownList ID="ddlCoBorrowers" runat="server" CssClass="cmbField">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Asset Ownership %">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAssetOwnership" runat="server" CssClass="txtField"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Loan Obligation %">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtLoanObligation" CssClass="txtField" runat="server"></asp:TextBox>
                                    <span id="Span8" class="spnRequiredField">*</span>
                                    <asp:RequiredFieldValidator ID="rfv1" ControlToValidate="txtLoanObligation" ErrorMessage="<br />Please enter the loan obligation %"
                                        Display="Dynamic" CssClass="rfvPCG" runat="server" InitialValue="" ValidationGroup="btnSubmit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Margin %">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMargin" CssClass="txtField" runat="server"></asp:TextBox>
                                    <span id="Span8" class="spnRequiredField">*</span>
                                    <asp:RequiredFieldValidator ID="rfv2" ControlToValidate="txtMargin" ErrorMessage="<br />Please enter Margin %"
                                        Display="Dynamic" CssClass="rfvPCG" runat="server" InitialValue="" ValidationGroup="btnSubmit">
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>--%>
                </td>
            </tr>
            <%--<tr id="trExistingAssets" runat="server">
                <td class="leftField">
                    <asp:Label ID="Label2" Text="Pick an Asset:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlExistingAssets" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlExistingAssets_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnAddAsset0" runat="server" CssClass="PCGMediumButton" OnClick="btnAddAsset_Click"
                        Text="Add Asset" />
                </td>
                <td>
                </td>
            </tr>
            <tr id="trLAPTitle" runat="server">
                <td colspan="4">
                    <asp:Label ID="lblPickAssetHeader" runat="server" Text="Pick an existing asset :"
                        CssClass="HeaderTextSmall"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr id="trLAPError" runat="server">
                <td>
                    <asp:Label ID="lblErrorMsg" runat="server" Text="No Records Found..!" CssClass="Error"></asp:Label>
                </td>
            </tr>
            <tr id="trLAP" runat="server">
                <td colspan="4">
                    <asp:CheckBoxList ID="chkLstAsset" runat="server" CssClass="cmbField" RepeatDirection="Horizontal"
                        RepeatColumns="10" RepeatLayout="Table">
                    </asp:CheckBoxList>
                </td>
            </tr>--%>
            <tr id="trInstallmentHeader" runat="server" visible="false">
                <td colspan="4">
                    <asp:Label ID="lblEMIHeader" runat="server" Text="Installment Details" CssClass="HeaderTextSmall"></asp:Label><hr />
                </td>
            </tr>
            <tr id="trInstallment1" runat="server" visible="false">
                <td class="leftField">
                    <asp:Label ID="lblRePaymentType" runat="server" Text="Installment Type :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlRepaymentType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlRepaymentType_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <span id="Span11" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="<br />Please select a Installment type"
                        ValidationGroup="vgBtnSubmitTemp" ControlToValidate="ddlRepaymentType" Operator="NotEqual"
                        ValueToCompare="Select the Repayment Type" Display="Dynamic" CssClass="rfvPCG"></asp:CompareValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblEMIFrequency" runat="server" Text="Installment Frequency :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlEMIFrequency" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlEMIFrequency_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <span id="Span12" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />Please select the Installment Frequency"
                        ValidationGroup="vgBtnSubmitTemp" ControlToValidate="ddlEMIFrequency" Operator="NotEqual"
                        ValueToCompare="Select the Frequency" Display="Dynamic" CssClass="rfvPCG"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trInstallment2" runat="server" visible="false">
                <td class="leftField">
                    <asp:Label ID="lblInstallmentStartDt" runat="server" Text="Installment Start Date :"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtInstallmentStartDt" runat="server" CssClass="txtField" OnTextChanged="txtInstallmentStartDt_TextChanged"
                        AutoPostBack="true"></asp:TextBox><span id="Span15" class="spnRequiredField">*</span>
                    <cc1:CalendarExtender ID="txtInstallmentStartDt_CalendarExtender" runat="server"
                        TargetControlID="txtInstallmentStartDt" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtInstallmentStartDt_TextBoxWatermarkExtender"
                        runat="server" TargetControlID="txtInstallmentStartDt" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <%--<asp:CompareValidator ID="CompareValidator5" runat="server" Operator="LessThanEqual"
                        ErrorMessage="Please Select a valid date" Type="Date" ControlToValidate="txtInstallmentStartDt"
                        CssClass="cvPCG" ValidationGroup="btnSubmit"></asp:CompareValidator>--%>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblInstallmentEndDt" runat="server" Text="Installment End Date :"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtInstallmentEndDt" runat="server" CssClass="txtField" OnTextChanged="txtInstallmentEndDt_TextChanged"
                        AutoPostBack="true"></asp:TextBox><span id="Span16" class="spnRequiredField">*</span>
                    <cc1:CalendarExtender ID="txtInstallmentEndDt_CalendarExtender" runat="server" TargetControlID="txtInstallmentEndDt"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtInstallmentEndDt_TextBoxWatermarkExtender" runat="server"
                        TargetControlID="txtInstallmentEndDt" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtInstallmentEndDt"
                        ErrorMessage="End Date should be greater than start Date" Type="Date" Operator="GreaterThanEqual"
                        ControlToCompare="txtInstallmentStartDt" ValidationGroup="vgBtnSubmitTemp" Display="Dynamic"
                        CssClass="rfvPCG">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </asp:CompareValidator>
                </td>
            </tr>
            <tr id="trInstallment3" runat="server" visible="false">
                <td class="leftField">
                    <asp:Label ID="lblNoOfInstallments" runat="server" Text="Number of Instalments :"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtNoOfInstallments" runat="server" CssClass="txtField" MaxLength="8"></asp:TextBox><span
                        id="Span13" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtNoOfInstallments"
                        ErrorMessage="<br />Please enter the number of instalments" Display="Dynamic"
                        CssClass="rfvPCG" runat="server" InitialValue="" ValidationGroup="vgBtnSubmitTemp">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtNoOfInstallments"
                        ValidationGroup="vgBtnSubmitTemp" Display="Dynamic" runat="server" CssClass="rfvPCG"
                        Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                </td>
                <td class="leftField">
                    <asp:Label ID="lblEMIAmount" runat="server" Text="Installment Amount :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtEMIAmount" runat="server" CssClass="txtField"></asp:TextBox><span
                        id="Span9" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtEMIAmount"
                        ErrorMessage="<br />Please enter EMI amount" Display="Dynamic" CssClass="rfvPCG"
                        runat="server" InitialValue="" ValidationGroup="vgBtnSubmitTemp">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtEMIAmount"
                        Display="Dynamic" CssClass="rfvPCG" runat="server" ErrorMessage="Not acceptable format"
                        ValidationExpression="^\d*(\.(\d{0,5}))?$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr id="trLumpsum" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblLumpsusmRepaymentAmount" runat="server" Text="Lumpsum Repayment Amount :"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtLumpsumRepaymentAmount" runat="server" CssClass="txtField"></asp:TextBox>
                    <%--  <span id="Span5" class="spnRequiredField">*</span>--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtLumpsumRepaymentAmount"
                        ErrorMessage="<br />Please enter Lumpsum Repayment Amount" Display="Dynamic"
                        CssClass="rfvPCG" runat="server" InitialValue="" ValidationGroup="vgBtnSubmitTemp">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtLumpsumRepaymentAmount"
                        Display="Dynamic" CssClass="rfvPCG" runat="server" ErrorMessage="Not acceptable format"
                        ValidationExpression="^\d*(\.(\d{0,5}))?$"></asp:RegularExpressionValidator>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                </td>
                <td class="rightField">
                </td>
                <td class="leftField">
                </td>
                <td class="rightField">
                </td>
            </tr>
            <%--            <tr id="trLoanExceptionsTitle" runat="server">
                <td class="leftField">
                    <asp:Button ID="btnAddAlterations" runat="server" Text="Add Alterations" CssClass="PCGMediumButton"
                        OnClick="btnAddAlterations_Click" />
                </td>
                <td class="rightField">
                </td>
                <td class="leftField">
                </td>
                <td class="rightField">
                </td>
            </tr>
            <tr id="trLoanExceptions" runat="server">
                <td colspan="4">
                    <asp:Label ID="lblLoanExceptionHeader" runat="server" Text="Loan Exceptions" CssClass="HeaderTextSmall"></asp:Label>
                    <hr />
                    <asp:GridView ID="gvAddAlterations" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ShowHeader="true" ShowFooter="True" CssClass="GridViewStyle" AllowSorting="True"
                        OnRowDataBound="gvAddAlterations_RowDataBound">
                        <FooterStyle CssClass="FooterStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <EditRowStyle HorizontalAlign="Left" CssClass="EditRowStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="SI.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblSerialNo" runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btnAddException" CssClass="PCGButton" runat="server" Text="Add Alterations"
                                        OnClick="btnAddException_Click" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Exception Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblExceptionType" runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlExceptionType" runat="server" CssClass="cmbField">
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtDate" runat="server" CssClass="txtField"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" TargetControlID="txtDate">
                                    </cc1:CalendarExtender>
                                    <cc1:TextBoxWatermarkExtender ID="txtDate_TextBoxWatermarkExtender" runat="server"
                                        TargetControlID="txtDate" WatermarkText="dd/mm/yyyy">
                                    </cc1:TextBoxWatermarkExtender>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reference Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblRefNo" runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtRefNo" runat="server" CssClass="txtField"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemark" runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="txtField"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr id="trAddAlterations" runat="server">
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>--%>
            <tr id="trSubmit" runat="server">
                <td class="SubmitCell" colspan="2">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" ValidationGroup="vgBtnSubmitTemp"
                        onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_LiabilitiesMaintenanceForm_btnSubmit');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_LiabilitiesMaintenanceForm_btnSubmit');"
                        Text="Submit" OnClick="btnSubmit_Click" />
                </td>
            </tr>
            <tr id="trUpdate" runat="server">
                <td class="SubmitCell" colspan="2">
                    <asp:Button ID="btnUpdate" runat="server" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_LiabilitiesMaintenanceForm_btnUpdate');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_LiabilitiesMaintenanceForm_btnUpdate');"
                        Text="Update" ValidationGroup="vgBtnSubmitTemp" OnClick="btnUpdate_Click" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
