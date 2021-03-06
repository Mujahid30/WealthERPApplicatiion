﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioInsuranceEntry.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioInsuranceEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--Javascript Calendar Controls - Required Files--%>
<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>

<script type="text/javascript">
    function SinglePaymentEPSelection() {
        var DropdownList = document.getElementById('<%=ddlEPPremiumFrequencyCode.ClientID %>');
        var SelectedValue = DropdownList.value;
        if (SelectedValue == "SP") {
            document.getElementById('<%=ddlEPPrPayDate.ClientID%>').disabled = 'true';
            document.getElementById('<%=txtLastPremiumDate.ClientID%>').disabled = 'true';
            document.getElementById('<%=txtEPPremiumDuration.ClientID%>').disabled = 'true';
            document.getElementById('<%=txtEPGracePeriod.ClientID%>').disabled = 'true';
        }
        else {
            document.getElementById('<%=ddlEPPrPayDate.ClientID%>').disabled = false;
            document.getElementById('<%=txtLastPremiumDate.ClientID%>').disabled = false;
            document.getElementById('<%=txtEPPremiumDuration.ClientID%>').disabled = false;
            document.getElementById('<%=txtEPGracePeriod.ClientID%>').disabled = false;
        }
    }

    function SinglePaymentMPSelection() {
        var DropdownList = document.getElementById('<%=ddlMPPremiumFrequencyCode.ClientID %>');
        var SelectedValue = DropdownList.value;
        if (SelectedValue == "SP") {
            document.getElementById('<%=ddlMPPrPayDate.ClientID%>').disabled = 'true';
            document.getElementById('<%=txtMPLastPremiumDate.ClientID%>').disabled = 'true';
            document.getElementById('<%=txtMPPremiumDuration.ClientID%>').disabled = 'true';
            document.getElementById('<%=txtMPGracePeriod.ClientID%>').disabled = 'true';
        }
        else {
            document.getElementById('<%=ddlMPPrPayDate.ClientID%>').disabled = false;
            document.getElementById('<%=txtMPLastPremiumDate.ClientID%>').disabled = false;
            document.getElementById('<%=txtMPPremiumDuration.ClientID%>').disabled = false;
            document.getElementById('<%=txtMPGracePeriod.ClientID%>').disabled = false;
        }
    }

    function SinglePaymentTPSelection() {
        var DropdownList = document.getElementById('<%=ddlTPPremiumFrequencyCode.ClientID %>');
        var SelectedValue = DropdownList.value;
        if (SelectedValue == "SP") {
            document.getElementById('<%=ddlTPPrPayDate.ClientID%>').disabled = 'true';
            document.getElementById('<%=txtTPLastPremiumDate.ClientID%>').disabled = 'true';
            document.getElementById('<%=txtTPPremiumDuration.ClientID%>').disabled = 'true';
            document.getElementById('<%=txtTPGracePeriod.ClientID%>').disabled = 'true';
        }
        else {
            document.getElementById('<%=ddlTPPrPayDate.ClientID%>').disabled = false;
            document.getElementById('<%=txtTPLastPremiumDate.ClientID%>').disabled = false;
            document.getElementById('<%=txtTPPremiumDuration.ClientID%>').disabled = false;
            document.getElementById('<%=txtTPGracePeriod.ClientID%>').disabled = false;
        }
    }
    function SinglePaymentWPSelection() {
        var DropdownList = document.getElementById('<%=ddlWLPPremiumFrequencyCode.ClientID %>');
        var SelectedValue = DropdownList.value;
        if (SelectedValue == "SP") {
            document.getElementById('<%=ddlWLPPrPayDate.ClientID%>').disabled = 'true';
            document.getElementById('<%=txtWLPLastPremiumDate.ClientID%>').disabled = 'true';
            document.getElementById('<%=txtWLPPremiumDuration.ClientID%>').disabled = 'true';
            document.getElementById('<%=txtWLPGracePeriod.ClientID%>').disabled = 'true';
        }
        else {
            document.getElementById('<%=ddlWLPPrPayDate.ClientID%>').disabled = false;
            document.getElementById('<%=txtWLPLastPremiumDate.ClientID%>').disabled = false;
            document.getElementById('<%=txtWLPPremiumDuration.ClientID%>').disabled = false;
            document.getElementById('<%=txtWLPGracePeriod.ClientID%>').disabled = false;
        }
    }
    function SinglePaymentUPSelection() {
        var DropdownList = document.getElementById('<%=ddlULIPPremiumFrequencyCode.ClientID %>');
        var SelectedValue = DropdownList.value;
        if (SelectedValue == "SP") {
            document.getElementById('<%=ddlULIPPrPayDate.ClientID%>').disabled = 'true';
            document.getElementById('<%=txtULIPLastPremiumDate.ClientID%>').disabled = 'true';
            document.getElementById('<%=txtUlipPremiuimPeriod.ClientID%>').disabled = 'true';
            document.getElementById('<%=txtULIPGracePeriod.ClientID%>').disabled = 'true';
        }
        else {
            document.getElementById('<%=ddlULIPPrPayDate.ClientID%>').disabled = false;
            document.getElementById('<%=txtULIPLastPremiumDate.ClientID%>').disabled = false;
            document.getElementById('<%=txtUlipPremiuimPeriod.ClientID%>').disabled = false;
            document.getElementById('<%=txtULIPGracePeriod.ClientID%>').disabled = false;
        }
    }

    function SinglePaymentOTSelection() {
        var DropdownList = document.getElementById('<%=ddlOTPremiumFrequencyCode.ClientID %>');
        var SelectedValue = DropdownList.value;
        if (SelectedValue == "SP") {
            document.getElementById('<%=ddlOTPrPayDate.ClientID%>').disabled = 'true';
            document.getElementById('<%=txtOTLastPremiumDate.ClientID%>').disabled = 'true';
            document.getElementById('<%=txtOTPremiumDuration.ClientID%>').disabled = 'true';
            document.getElementById('<%=txtOTGracePeriod.ClientID%>').disabled = 'true';
        }
        else {
            document.getElementById('<%=ddlOTPrPayDate.ClientID%>').disabled = false;
            document.getElementById('<%=txtOTLastPremiumDate.ClientID%>').disabled = false;
            document.getElementById('<%=txtOTPremiumDuration.ClientID%>').disabled = false;
            document.getElementById('<%=txtOTGracePeriod.ClientID%>').disabled = false;
        }
    }
</script>

<script language="javascript" type="text/javascript">
    function CheckMaturityDate(sender, args) {

        var commencementDateString = document.getElementById('ctrl_PortfolioInsuranceEntry_txtPolicyCommencementDate').value;
        var commDate = changeDate(commencementDateString);
        var maturityDateString = document.getElementById('ctrl_PortfolioInsuranceEntry_txtPolicyMaturity').value;
        var maturityDate = changeDate(maturityDateString);
        var todayDate = new Date();

        if (Date.parse(maturityDate) < Date.parse(commDate)) {
            //sender._selectedDate = todayDate;
            //sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            sender._textbox.set_Value('dd/mm/yyyy');
            alert("Warning! - Maturity date cannot be less than the commencement date");
        }
    }

    function CheckLastPremiumDate(sender, args) {
        debugger;
        var lastPremiumDate = '';

        var firstPremiumDate = document.getElementById('ctrl_PortfolioInsuranceEntry_txtFirstPremiumDate');
        if (firstPremiumDate != null) {
            // Endowment Policy Selected
            lastPremiumDate = document.getElementById('ctrl_PortfolioInsuranceEntry_txtLastPremiumDate');
        }
        else {
            firstPremiumDate = document.getElementById('ctrl_PortfolioInsuranceEntry_txtWLPFirstPremiumDate');
            if (firstPremiumDate != null) {
                // WLP Selected
                lastPremiumDate = document.getElementById('ctrl_PortfolioInsuranceEntry_txtWLPLastPremiumDate');
            }
            else {
                firstPremiumDate = document.getElementById('ctrl_PortfolioInsuranceEntry_txtMPFirstPremiumDate');
                if (firstPremiumDate != null) {
                    // MP Selected
                    lastPremiumDate = document.getElementById('ctrl_PortfolioInsuranceEntry_txtMPLastPremiumDate');
                }
                else {
                    firstPremiumDate = document.getElementById('ctrl_PortfolioInsuranceEntry_txtULIPFirstPremiumDate');
                    if (firstPremiumDate != null) {
                        // ULIP Selected
                        lastPremiumDate = document.getElementById('ctrl_PortfolioInsuranceEntry_txtULIPLastPremiumDate');
                    }
                    else {
                        firstPremiumDate = document.getElementById('ctrl_PortfolioInsuranceEntry_txtTPFirstPremiumDate');
                        if (firstPremiumDate != null) {
                            // TP Selected
                            lastPremiumDate = document.getElementById('ctrl_PortfolioInsuranceEntry_txtTPLastPremiumDate');
                        }
                    }
                }
            }
        }

        var dtFirstPremiumDate = changeDate(firstPremiumDate.value);
        var dtLastPremiumDate = changeDate(lastPremiumDate.value);
        var todayDate = new Date();

        if (Date.parse(dtFirstPremiumDate) > Date.parse(dtLastPremiumDate)) {
            //sender._selectedDate = todayDate;
            //sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            sender._textbox.set_Value('dd/mm/yyyy');
            alert("Warning! - First Premium date cannot be greater than the last premium date");
        }
    }
    function changeDate(date) {
        var newDate = date.split('/');
        date = newDate[1] + "/" + newDate[0] + "/" + newDate[2];
        return date;
    }

    function isFutureDate(sender, args) {
        var purchaseDate = sender._element;
        var dateToCheck = purchaseDate.value;
        var currentDate = '<%= DateTime.Now.ToString("yyyyMMdd") %>'
        var yyyymmdddateToCheck = dateToCheck.substr(6, 4) + dateToCheck.substr(3, 2) + dateToCheck.substr(0, 2)
        if (currentDate >= yyyymmdddateToCheck) {
            return true;
        }
        else {
            alert('Sorry, Purchase date cannot be greater than current date');
            sender._element.value = 'dd/mm/yyyy';
            return false;
        }
    }
</script>

<script type="text/javascript">
    function CalculateMaturityDate() {
        var Months = document.getElementById('<%=txtPolicyTerms.ClientID %>').value;
        var dateStr = document.getElementById('<%=txtPolicyCommencementDate.ClientID %>').value;

        dArr = dateStr.split("/");
        var newDate = dArr[1] + "/" + dArr[0] + "/" + dArr[2];

        var InitialDate = new Date(newDate);
        var MaturityDate = new Date(InitialDate.setMonth(parseFloat(Months) + InitialDate.getMonth(), InitialDate.getDate()));

        var MaturityDateStr = (MaturityDate.getMonth() + 1 + "/" + MaturityDate.getDate() + "/" + MaturityDate.getFullYear());

        dArr = MaturityDateStr.split("/");
        var newMaturityDate = dArr[1] + "/" + dArr[0] + "/" + dArr[2];

        document.getElementById("<%= txtPolicyMaturity.ClientID %>").value = newMaturityDate;
    }
</script>


<telerik:RadWindow ID="radwindowPopup" runat="server" VisibleOnPageLoad="false"
    Width="500px" Modal="true" BackColor="#DADADA" Behaviors="Move,resize,close" VisibleStatusbar="false"
    Title="Insert New Scheme">
    <ContentTemplate>
        <div style="padding: 20px">
            <table width="100%">
                <tr>
                    <td class="leftField" style="width: 10%">
                        <asp:Label ID="lblIssuer" runat="server" Text="Insurance Issuer: " CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField" style="width: 25%">
                        <asp:Label ID="lblIssuerCode" runat="server" Text="" CssClass="FieldName"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="leftField" style="width: 10%">
                        <asp:Label ID="lblAsset" runat="server" Text="Asset Particulars: " CssClass="FieldName"></asp:Label>
                    </td>
                    <td class="rightField" style="width: 25%">
                        <asp:TextBox ID="txtAsset" runat="server" CssClass="txtField"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtAsset" ErrorMessage="Enter the Scheme Name"
                            ValidationGroup="vgOK" Display="Dynamic" runat="server" CssClass="rfvPCG">
                        </asp:RequiredFieldValidator>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="leftField" style="width: 10%">
                        <asp:Button ID="btnOk" runat="server" Text="OK" CssClass="PCGButton" OnClick="btnOk_OnClick"
                            ValidationGroup="vgOK" />
                    </td>
                    <td class="rightField" style="width: 25%">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="PCGButton" OnClick="btnCancel_OnClick" />
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
</telerik:RadWindow>
<table width="100%">
    <tr>
        <td colspan="6">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Life Insurance Account Entry Details
                        </td>
                        <td align="right">
                            <asp:LinkButton runat="server" ID="LinkButton1" CssClass="LinkButtons" Text="Back"
                                OnClick="lnkBtnBack_Click"></asp:LinkButton>
                                <td align="right"> 
                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="LinkButtons" Text="Edit" OnClick="lnkEdit_Click">
                                </asp:LinkButton>
                                </td>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <%--  <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>--%>
    <tr>
        <td colspan="3">
            <div class="panel">
                <p>
                    Note: Fields marked with a ' * ' are compulsory
                </p>
            </div>
        </td>
    </tr>
    <%--<tr>
        <td colspan="6" class="tdRequiredText">
            <label id="lbl" class="lblRequiredText">
                Note: Fields marked with ' * ' are compulsory</label>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>--%>
    <tr>
        <td colspan="2" class="leftField">
            <asp:Label ID="lblInstrumentCategory" runat="server" Text="Instrument Category:"
                CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblInsCategory" runat="server" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="leftField">
            <asp:Label ID="lblPolicyNumber" runat="server" CssClass="FieldName" Text="Policy Number:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtPolicyNumber" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="6">
        </td>
    </tr>
    <tr id="trInstallmentHeader" runat="server" visible="true">
        <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Account Details
            </div>
        </td>
    </tr>
    <%-- <tr>
        <td colspan="6">
            <asp:Label ID="Label5" runat="server" Text="Account Details" CssClass="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>--%>
  <%--  <tr id="trEditButton" runat="server" visible="false">
        <td colspan="6">
            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="LinkButtons" Text="Edit" OnClick="lnkEdit_Click">
            </asp:LinkButton>
        </td>
    </tr>--%>
    <tr id="trEditSpace" runat="server" visible="false">
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2" class="leftField">
            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Policy Issuer:"></asp:Label>
        </td>
        <td colspan="4">
            <asp:DropDownList ID="ddlInsuranceIssuerCode" runat="server" CssClass="cmbField"
                Width="42%" AutoPostBack="True" OnSelectedIndexChanged="ddlInsuranceIssuerCode_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cvInsuranceIssuerCode" runat="server" ControlToValidate="ddlInsuranceIssuerCode"
                ErrorMessage="Please select an Insurance Issuer" Operator="NotEqual" ValueToCompare="Select an Insurance Issuer"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="vgSubmit"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="leftField">
            <asp:Label ID="Label20" runat="server" CssClass="FieldName" Text="Scheme Name:"></asp:Label>
        </td>
        <td colspan="4">
            <%--<asp:TextBox ID="txtName" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtName" ErrorMessage="Please enter the Scheme Name"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
            <asp:DropDownList ID="ddlAssetPerticular" runat="server" CssClass="cmbField" AutoPostBack="true"
                Width="42%" OnSelectedIndexChanged="ddlUlipPlans_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Button ID="btnAddScheme" runat="server" CssClass="PCGMediumButton" Text="Add Scheme"
                CausesValidation="false"  OnClick="btnAddScheme_OnClick" />
        </td>
    </tr>
    <tr>
        <td colspan="6">
        </td>
    </tr>
    <tr id="tr1" runat="server" visible="true">
        <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Policy Details
            </div>
        </td>
    </tr>
    <%--  <tr>
        <td colspan="6">
            <asp:Label ID="Label23" runat="server" CssClass="HeaderTextSmall" Text="Policy Details"></asp:Label>
            <hr />
        </td>
    </tr>--%>
    <tr>
        <td colspan="2" class="leftField">
            <asp:Label ID="Label21" runat="server" CssClass="FieldName" Text="Policy Commencement Date:"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtPolicyCommencementDate" runat="server" CssClass="txtField" AutoPostBack="true"
                OnTextChanged="txtPolicyCommencementDate_TextChanged"></asp:TextBox>
            <cc1:CalendarExtender ID="txtPolicyCommencementDate_CalendarExtender" runat="server"
                TargetControlID="txtPolicyCommencementDate" Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckMaturityDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtPolicyCommencementDate_TextBoxWatermarkExtender"
                runat="server" TargetControlID="txtPolicyCommencementDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <span id="Span3" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvPolicyCommencementDate" ControlToValidate="txtPolicyCommencementDate"
                ErrorMessage="Please select a Policy Commencement Date" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="vgSubmit">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="The date format should be dd/mm/yyyyyy"
                Type="Date" ControlToValidate="txtPolicyCommencementDate" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trpolicytenure" runat="server">
        <td colspan="2" class="leftField">
            <asp:Label ID="lblPolicyTerms" runat="server" CssClass="FieldName" Text="Policy Term:"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtPolicyTerms" runat="server" CssClass="txtField" AutoPostBack="true"
                ValidationGroup="vgSubmit" OnTextChanged="txtPolicyTerms_TextChanged"></asp:TextBox>
            <span id="Span12" class="spnRequiredField">*</span>
            <asp:DropDownList ID="ddlPeriodSelection" runat="server" AutoPostBack="true" CssClass="cmbField"
                ValidationGroup="vgSubmit" OnSelectedIndexChanged="ddlPeriodSelection_SelectedIndexChanged">
                <asp:ListItem Text="Days" Value="DA"></asp:ListItem>
                <asp:ListItem Text="Months" Value="MN"></asp:ListItem>
                <asp:ListItem Text="Years" Value="YR"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvPolicyTerms" ControlToValidate="txtPolicyTerms"
                ErrorMessage="Please select a Policy Terms" Display="Dynamic" runat="server"
                ValidationGroup="vgSubmit" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvPolicyTerms" runat="server" ErrorMessage="Please enter intiger value"
                Type="Integer" ControlToValidate="txtPolicyTerms" Operator="DataTypeCheck" CssClass="cvPCG"
                ValidationGroup="vgSubmit" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" colspan="2">
            <asp:Label ID="Label22" runat="server" CssClass="FieldName" Text="Policy Maturity Date:"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtPolicyMaturity" runat="server" CssClass="txtField" OnTextChanged="txtPolicyMaturity_TextChanged"
                Enabled="false" AutoPostBack="true"></asp:TextBox>
            <cc1:CalendarExtender ID="txtPolicyMaturity_CalendarExtender" runat="server" TargetControlID="txtPolicyMaturity"
                Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckMaturityDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtPolicyMaturity_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtPolicyMaturity" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <span id="Span4" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvPolicyMaturity" ControlToValidate="txtPolicyMaturity"
                ErrorMessage="Please select a Policy Maturity Date" Display="Dynamic" runat="server"
                ValidationGroup="vgSubmit" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="The date format should be dd/mm/yyyyyy"
                Type="Date" ControlToValidate="txtPolicyMaturity" Operator="DataTypeCheck" CssClass="cvPCG"
                ValidationGroup="vgSubmit" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" colspan="2">
            <asp:Label ID="Label34" runat="server" CssClass="FieldName" Text="Sum Assured:"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtSumAssured" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span5" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvSumAssured" ControlToValidate="txtSumAssured"
                ErrorMessage="Please enter the Sum Assured" Display="Dynamic" runat="server"
                ValidationGroup="vgSubmit" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtSumAssured" Operator="DataTypeCheck" CssClass="cvPCG"
                ValidationGroup="vgSubmit" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" colspan="2">
            <asp:Label ID="Label35" runat="server" CssClass="FieldName" Text="Application Number:"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtApplicationNumber" runat="server" CssClass="txtField"></asp:TextBox>
            <asp:RegularExpressionValidator ID="AppValidator1" ControlToValidate="txtApplicationNumber"
                ValidationExpression="^[0-9a-zA-Z ]+$" runat="server" ErrorMessage="Please enter an Alphanumeric value" />
            <%--<asp:CompareValidator ID="CompareValidator10" runat="server" ErrorMessage="Please enter an integer value"
                Type="Integer" ControlToValidate="txtApplicationNumber" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>--%>
        </td>
    </tr>
    <tr>
        <td class="leftField" colspan="2"">
            <asp:Label ID="Label36" runat="server" CssClass="FieldName" Text="Application Date:"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtApplDate" runat="server" CssClass="txtField"></asp:TextBox>
            <asp:CompareValidator ID="cvDepositDate1" runat="server" ErrorMessage="<br/>The application date should not be greater than current date."
                Type="Date" ControlToValidate="txtApplDate" CssClass="cvPCG" Operator="LessThanEqual"
                ValidationGroup="vgSubmit" ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
            <cc1:CalendarExtender ID="txtApplDate_CalendarExtender" runat="server" TargetControlID="txtApplDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtApplDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtApplDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <%--<span id="Span7" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvApplDate" ControlToValidate="txtApplDate" ErrorMessage="Please select an Application Date"
                        Display="Dynamic" runat="server" CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator11" runat="server" ErrorMessage="The date format should be dd/mm/yyyyyy"
                Type="Date" ControlToValidate="txtApplDate" Operator="DataTypeCheck" CssClass="cvPCG"
                ValidationGroup="vgSubmit" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" colspan="2"">
            <asp:Label ID="lblPolicyPurchaseDate" runat="server" CssClass="FieldName" Text="Policy Purchase Date:"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtPolicyPurchaseDate" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<asp:CompareValidator ID="compvPolicyPurchaseDate" runat="server" ErrorMessage="<br/>The purchase date should not be greater than current date."
                Type="Date" ControlToValidate="txtPolicyPurchaseDate" CssClass="cvPCG" Operator="LessThanEqual"
                ValidationGroup="vgSubmit" Display="Dynamic"></asp:CompareValidator>--%>
            <cc1:CalendarExtender ID="cePolicyPurchaseDate" runat="server" TargetControlID="txtPolicyPurchaseDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtPolicyPurchaseDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <%--<span id="Span7" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvApplDate" ControlToValidate="txtPolicyPurchaseDate" ErrorMessage="Please select purchase Date"
                        Display="Dynamic" runat="server" CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="cvPolicyPurchaseDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyyyy"
                Type="Date" ControlToValidate="txtPolicyPurchaseDate" Operator="DataTypeCheck"
                CssClass="cvPCG" ValidationGroup="vgSubmit" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <td>
    </tr>
    <%--Start Editing From Here--%>
    <tr id="tr2" runat="server" visible="true">
        <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Premium Details
            </div>
        </td>
    </tr>
    <%-- <tr>
        <td colspan="6">
            <asp:Label ID="Label24" runat="server" CssClass="HeaderTextSmall" Text="Premium Details"></asp:Label>
            <hr />
        </td>
    </tr>--%>
    <tr id="trEPPremiumAmount" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblEPPremiumAmount" runat="server" CssClass="FieldName" Text="Premium Installment Amount:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtEPPremiumAmount" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span8" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvEPPremiumAmount" ControlToValidate="txtEPPremiumAmount"
                        ErrorMessage="Please enter a Premium Amount" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator12" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtEPPremiumAmount" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblEPPremiumCycle" runat="server" CssClass="FieldName" Text="Premium Cycle:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlEPPremiumFrequencyCode" runat="server" CssClass="cmbField"
                onchange="SinglePaymentEPSelection();">
            </asp:DropDownList>
            <span id="Span9" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlEPPremiumFrequencyCode"
                ErrorMessage="Please select a Premium Cycle" Operator="NotEqual" ValueToCompare="Select a Frequency Code"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trEPPremiumFirstLast" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblFirstPremiumDate" runat="server" CssClass="FieldName" Text="First Premium Date:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtFirstPremiumDate" runat="server" CssClass="txtField" OnTextChanged="txtLastPremiumDate_TextChanged"
                AutoPostBack="true"></asp:TextBox>
            <cc1:CalendarExtender ID="CEFirstPremiumDate" runat="server" TargetControlID="txtFirstPremiumDate"
                Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckLastPremiumDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtFirstPremiumDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <span id="Span49" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvFirstPremiumDate" ControlToValidate="txtFirstPremiumDate"
                ErrorMessage="Please enter the First Premium Date" Display="Dynamic" runat="server"
                ValidationGroup="vgSubmit" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvFirstPremiumDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyyyy"
                Type="Date" ControlToValidate="txtFirstPremiumDate" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblLastPremiumDate" runat="server" CssClass="FieldName" Text="Last Premium Date:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtLastPremiumDate" runat="server" CssClass="txtField" OnTextChanged="txtLastPremiumDate_TextChanged"
                AutoPostBack="true"></asp:TextBox>
            <cc1:CalendarExtender ID="CELastPremiumDate" runat="server" TargetControlID="txtLastPremiumDate"
                Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckLastPremiumDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="tbweLastPremiumDate" runat="server" TargetControlID="txtLastPremiumDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <span id="Span50" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvLastPremiumDate" ControlToValidate="txtLastPremiumDate"
                ErrorMessage="Please enter the Last Premium Date" Display="Dynamic" runat="server"
                ValidationGroup="vgSubmit" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvLastPremiumDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyyyy"
                Type="Date" ControlToValidate="txtLastPremiumDate" Operator="DataTypeCheck" CssClass="cvPCG"
                ValidationGroup="vgSubmit" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trEPPremiumPeriod" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblEPPremiumPeriod" runat="server" CssClass="FieldName" Text="Premium Period (Days):"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtEPPremiumDuration" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span10" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvEPPremiumDuration" ControlToValidate="txtEPPremiumDuration"
                        ErrorMessage="Please enter the Premium Period" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator13" runat="server" ErrorMessage="Please enter a integer value"
                Type="Double" ControlToValidate="txtEPPremiumDuration" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblEPPremiumPayDate" runat="server" CssClass="FieldName" Text="Premium Payment Date:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlEPPrPayDate" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span11" class="spnRequiredField">*</span> <span class="Apple-style-span"
                style="border-collapse: separate; color: rgb(0, 0, 0); font-family: 'Times New Roman';
                font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal;
                line-height: normal; orphans: 2; text-align: -webkit-auto; text-indent: 0px;
                text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-border-horizontal-spacing: 0px;
                -webkit-border-vertical-spacing: 0px; -webkit-text-decorations-in-effect: none;
                -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; font-size: medium;">
                <asp:CompareValidator ID="CompareValidator14" runat="server" ErrorMessage="Please Select Premium Date"
                    Type="String" ValueToCompare="Select Premium Date" ControlToValidate="ddlEPPrPayDate"
                    Operator="NotEqual" CssClass="cvPCG" Display="Dynamic" ValidationGroup="vgSubmit"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trEPGracePeriod" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblEPGracePeriod" runat="server" CssClass="FieldName" Text="Grace Period:"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtEPGracePeriod" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span12" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvEPGracePeriod" ControlToValidate="txtEPGracePeriod"
                        ErrorMessage="Please enter the Grace Period" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator15" runat="server" ErrorMessage="Please enter an integer value"
                Type="Integer" ControlToValidate="txtEPGracePeriod" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trOTPremiumAmount" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblOTpremiumAmount" runat="server" CssClass="FieldName" Text="Premium Installment Amount:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtOTpremiumAmount" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span8" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvEPPremiumAmount" ControlToValidate="txtEPPremiumAmount"
                        ErrorMessage="Please enter a Premium Amount" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator48" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtOTpremiumAmount" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblOTPremiumCycle" runat="server" CssClass="FieldName" Text="Premium Cycle:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlOTPremiumFrequencyCode" runat="server" CssClass="cmbField"
                onchange="SinglePaymentOTSelection();">
            </asp:DropDownList>
            <span id="Span6" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator49" runat="server" ControlToValidate="ddlOTPremiumFrequencyCode"
                ErrorMessage="Please select a Premium Cycle" Operator="NotEqual" ValueToCompare="Select a Frequency Code"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trOTPremiumFirstLast" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblOTFirstPremiumDate" runat="server" CssClass="FieldName" Text="First Premium Date:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtOTFirstPremiumDate" runat="server" CssClass="txtField" OnTextChanged="txtOTLastPremiumDate_TextChanged"
                AutoPostBack="true"></asp:TextBox>
            <cc1:CalendarExtender ID="OTFirstPremiumDate" runat="server" TargetControlID="txtOTFirstPremiumDate"
                Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckLastPremiumDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="OTTextBoxWatermarkExtender2" runat="server" TargetControlID="txtOTFirstPremiumDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <span id="Span7" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtOTFirstPremiumDate"
                ErrorMessage="Please enter the First Premium Date" Display="Dynamic" runat="server"
                ValidationGroup="vgSubmit" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator50" runat="server" ErrorMessage="The date format should be dd/mm/yyyyyy"
                Type="Date" ControlToValidate="txtOTFirstPremiumDate" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblOTLastPremiumDate" runat="server" CssClass="FieldName" Text="Last Premium Date:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtOTLastPremiumDate" runat="server" CssClass="txtField" OnTextChanged="txtOTLastPremiumDate_TextChanged"
                AutoPostBack="true"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtOTLastPremiumDate"
                Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckLastPremiumDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="tbweOTLastPremiumDate" runat="server" TargetControlID="txtOTLastPremiumDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <span id="Span8" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtOTLastPremiumDate"
                ErrorMessage="Please enter the Last Premium Date" Display="Dynamic" runat="server"
                ValidationGroup="vgSubmit" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator51" runat="server" ErrorMessage="The date format should be dd/mm/yyyyyy"
                Type="Date" ControlToValidate="txtOTLastPremiumDate" Operator="DataTypeCheck"
                CssClass="cvPCG" ValidationGroup="vgSubmit" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trOTPremiumPeriod" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblOTPremiumPeriod" runat="server" CssClass="FieldName" Text="Premium Period (Days):"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtOTPremiumDuration" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span10" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvEPPremiumDuration" ControlToValidate="txtEPPremiumDuration"
                        ErrorMessage="Please enter the Premium Period" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator52" runat="server" ErrorMessage="Please enter a integer value"
                Type="Double" ControlToValidate="txtOTPremiumDuration" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblOTPremiumPayDate" runat="server" CssClass="FieldName" Text="Premium Payment Date:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlOTPrPayDate" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span10" class="spnRequiredField">*</span> <span class="Apple-style-span"
                style="border-collapse: separate; color: rgb(0, 0, 0); font-family: 'Times New Roman';
                font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal;
                line-height: normal; orphans: 2; text-align: -webkit-auto; text-indent: 0px;
                text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-border-horizontal-spacing: 0px;
                -webkit-border-vertical-spacing: 0px; -webkit-text-decorations-in-effect: none;
                -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; font-size: medium;">
                <asp:CompareValidator ID="cmpddlOTPrPayDate" runat="server" ErrorMessage="Please Select Premium Date"
                    ValidationGroup="vgSubmit" Type="String" ValueToCompare="Select Premium Date"
                    ControlToValidate="ddlOTPrPayDate" Operator="NotEqual" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                <%--<asp:RequiredFieldValidator id="Requiredfieldvalidator3" Runat="server" CssClass="cvPCG" ControlToValidate="ddlOTPrPayDate" ErrorMessage="Please Select Premium Date" ValidationExpression=???????></asp:RequiredFieldValidator> --%>
        </td>
    </tr>
    <tr id="trOTGracePeriod" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblOTGracePeriod" runat="server" CssClass="FieldName" Text="Grace Period:"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtOTGracePeriod" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span12" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvEPGracePeriod" ControlToValidate="txtOTGracePeriod"
                        ErrorMessage="Please enter the Grace Period" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator54" runat="server" ErrorMessage="Please enter an integer value"
                Type="Integer" ControlToValidate="txtOTGracePeriod" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trWLPPremiumAmount" runat="server">
        <td colspan="2" class="leftField">
            <asp:Label ID="lblWLPPremiumAmount" runat="server" CssClass="FieldName" Text="Premium Installment Amount:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtWLPPremiumAmount" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span13" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvWLPPremiumAmount" ControlToValidate="txtWLPPremiumAmount"
                        ErrorMessage="Please enter the Premium Amount" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator16" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtWLPPremiumAmount" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td colspan="2" class="leftField">
            <asp:Label ID="lblWLPPremiumCycle" runat="server" CssClass="FieldName" Text="Premium Cycle:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlWLPPremiumFrequencyCode" runat="server" CssClass="cmbField"
                onchange="SinglePaymentWPSelection();">
            </asp:DropDownList>
            <span id="Span14" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlWLPPremiumFrequencyCode"
                ValidationGroup="vgSubmit" ErrorMessage="Please select a Premium Cycle" Operator="NotEqual"
                ValueToCompare="Select a Frequency Code" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trWLPPremiumFirstLast" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblWLPFirstPremiumDate" runat="server" CssClass="FieldName" Text="First Premium Date:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtWLPFirstPremiumDate" runat="server" CssClass="txtField" OnTextChanged="txtWLPLastPremiumDate_TextChanged"
                AutoPostBack="true"></asp:TextBox>
            <cc1:CalendarExtender ID="ceWLPFirstPremiumDate" runat="server" TargetControlID="txtWLPFirstPremiumDate"
                Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckLastPremiumDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="tbweWLPFirstPremiumDate" runat="server" TargetControlID="txtWLPFirstPremiumDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <span id="Span51" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvWLPFirstPremiumDate" ControlToValidate="txtWLPFirstPremiumDate"
                ErrorMessage="Please enter the First Premium Date" Display="Dynamic" runat="server"
                ValidationGroup="vgSubmit" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvWLPFirstPremiumDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyyyy"
                Type="Date" ControlToValidate="txtWLPFirstPremiumDate" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblWLPLastPremiumDate" runat="server" CssClass="FieldName" Text="Last Premium Date:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtWLPLastPremiumDate" runat="server" CssClass="txtField" OnTextChanged="txtWLPLastPremiumDate_TextChanged"
                AutoPostBack="true"></asp:TextBox>
            <cc1:CalendarExtender ID="ceWLPLastPremiumDate" runat="server" TargetControlID="txtWLPLastPremiumDate"
                Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckLastPremiumDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="tbweWLPLastPremiumDate" runat="server" TargetControlID="txtWLPLastPremiumDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <span id="Span52" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvWLPLastPremiumDate" ControlToValidate="txtWLPLastPremiumDate"
                ErrorMessage="Please enter the Last Premium Date" Display="Dynamic" runat="server"
                ValidationGroup="vgSubmit" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvWLPLastPremiumDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyyyy"
                Type="Date" ControlToValidate="txtWLPLastPremiumDate" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trWLPPremiumPeriod" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblWLPPremiumPeriod" runat="server" CssClass="FieldName" Text="Premium Period (Days):"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtWLPPremiumDuration" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span15" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvWLPPremiumDuration" ControlToValidate="txtWLPPremiumDuration"
                        ErrorMessage="Please enter the Premium Period" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator17" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtWLPPremiumAmount" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblWLPPremiumPayDate" runat="server" CssClass="FieldName" Text="Premium Payment Date:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlWLPPrPayDate" runat="server" CssClass="txtField">
            </asp:DropDownList>
            <span id="Span16" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator18" runat="server" ErrorMessage="Please Select Premium Date"
                ValidationGroup="vgSubmit" Type="String" ValueToCompare="Select Premium Date"
                ControlToValidate="ddlWLPPrPayDate" Operator="NotEqual" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trWLPGracePeriod" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblWLPGracePeriod" runat="server" CssClass="FieldName" Text="Grace Period:"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtWLPGracePeriod" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span17" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvWLPGracePeriod" ControlToValidate="txtWLPGracePeriod"
                        ErrorMessage="Please enter the Grace Period" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator19" runat="server" ErrorMessage="Please enter an integer value"
                Type="Integer" ControlToValidate="txtWLPGracePeriod" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trMPPremiumAmount" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblMPPremiumAmount" runat="server" CssClass="FieldName" Text="Premium Installment Amount:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtMPPremiumAmount" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span18" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtMPPremiumAmount"
                        ErrorMessage="Please enter the Premium amount" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator20" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtMPPremiumAmount" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblMPPremiumCycle" runat="server" CssClass="FieldName" Text="Premium Cycle:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlMPPremiumFrequencyCode" runat="server" CssClass="cmbField"
                onchange="SinglePaymentMPSelection();">
            </asp:DropDownList>
            <span id="Span19" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlMPPremiumFrequencyCode"
                ValidationGroup="vgSubmit" ErrorMessage="Please select a Premium Cycle" Operator="NotEqual"
                ValueToCompare="Select a Frequency Code" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trMPPremiumFirstLast" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblMPFirstPremiumDate" runat="server" CssClass="FieldName" Text="First Premium Date:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtMPFirstPremiumDate" runat="server" CssClass="txtField" OnTextChanged="txtMPLastPremiumDate_TextChanged"
                AutoPostBack="true"></asp:TextBox>
            <cc1:CalendarExtender ID="ceMPFirstPremiumDate" runat="server" TargetControlID="txtMPFirstPremiumDate"
                Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckLastPremiumDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="tbweMPFirstPremiumDate" runat="server" TargetControlID="txtMPFirstPremiumDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <span id="Span53" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvMPFirstPremiumDate" ControlToValidate="txtMPFirstPremiumDate"
                ErrorMessage="Please enter the First Premium Date" Display="Dynamic" runat="server"
                ValidationGroup="vgSubmit" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvMPFirstPremiumDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyyyy"
                Type="Date" ControlToValidate="txtMPFirstPremiumDate" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblMPLastPremiumDate" runat="server" CssClass="FieldName" Text="Last Premium Date:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtMPLastPremiumDate" runat="server" CssClass="txtField" OnTextChanged="txtMPLastPremiumDate_TextChanged"
                AutoPostBack="true"></asp:TextBox>
            <cc1:CalendarExtender ID="ceMPLastPremiumDate" runat="server" TargetControlID="txtMPLastPremiumDate"
                Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckLastPremiumDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="tbweMPLastPremiumDate" runat="server" TargetControlID="txtMPLastPremiumDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <span id="Span54" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvMPLastPremiumDate" ControlToValidate="txtMPLastPremiumDate"
                ErrorMessage="Please enter the Last Premium Date" Display="Dynamic" runat="server"
                ValidationGroup="vgSubmit" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvMPLastPremiumDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyyyy"
                Type="Date" ControlToValidate="txtMPLastPremiumDate" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trMPPeriod" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblMPPremiumPeriod" runat="server" CssClass="FieldName" Text="Premium Period (Days):"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtMPPremiumDuration" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span20" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvMPPremiumDuration" ControlToValidate="txtMPPremiumDuration"
                        ErrorMessage="Please enter the Premium Period" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator21" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtMPPremiumDuration" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblMPPremiumPayDate" runat="server" CssClass="FieldName" Text="Premium Payment Date:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlMPPrPayDate" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span21" class="spnRequiredField">*</span>
            <%--  <asp:RequiredFieldValidator ID="rfvMPPrPayDate" ControlToValidate="ddlMPPrPayDate" 
                ErrorMessage="Please Enter Premium Payment Date" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator22" runat="server" ErrorMessage="Please Select Premium Date"
                ValidationGroup="vgSubmit" Type="String" ControlToValidate="ddlMPPrPayDate" ValueToCompare="Select Premium Date"
                Operator="NotEqual" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trMPGracePeriod" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblMPGracePeriod" runat="server" CssClass="FieldName" Text="Grace Period:"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtMPGracePeriod" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span22" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvMPGracePeriod" ControlToValidate="txtMPGracePeriod"
                        ErrorMessage="Please enter the Grace Period" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator23" runat="server" ErrorMessage="Please enter an integer value"
                Type="Integer" ControlToValidate="txtMPGracePeriod" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr runat="server" id="trUlipPremiumAmount">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblUlipPremiumInstAmt" runat="server" CssClass="FieldName" Text="Premium Installment Amount:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtUlipPremiumInstAmt" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span25" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvULIPGracePeriod" ControlToValidate="txtULIPGracePeriod"
                        ErrorMessage="Please enter the Grace Period" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator53" runat="server" ErrorMessage="Please enter an integer value"
                Type="Integer" ControlToValidate="txtUlipPremiumInstAmt" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblULIPPremiumCycle" runat="server" CssClass="FieldName" Text="Premium Cycle:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlULIPPremiumFrequencyCode" runat="server" CssClass="cmbField"
                onchange="SinglePaymentUPSelection();">
            </asp:DropDownList>
            <span id="Span23" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlULIPPremiumFrequencyCode"
                ValidationGroup="vgSubmit" ErrorMessage="Please select a Premium Cycle" Operator="NotEqual"
                ValueToCompare="Select a Frequency Code" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trULIPPremiumFirstLast" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblULIPFirstPremiumDate" runat="server" CssClass="FieldName" Text="First Premium Date:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtULIPFirstPremiumDate" runat="server" AutoPostBack="true" CssClass="txtField"
                OnTextChanged="txtULIPFirstPremiumDate_TextChanged"></asp:TextBox>
            <cc1:CalendarExtender ID="ceULIPFirstPremiumDate" runat="server" TargetControlID="txtULIPFirstPremiumDate"
                Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckLastPremiumDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="tbweULIPFirstPremiumDate" runat="server" TargetControlID="txtULIPFirstPremiumDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <span id="Span55" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvULIPFirstPremiumDate" ControlToValidate="txtULIPFirstPremiumDate"
                ErrorMessage="Please enter the First Premium Date" Display="Dynamic" runat="server"
                ValidationGroup="vgSubmit" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvULIPFirstPremiumDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyyyy"
                Type="Date" ControlToValidate="txtULIPFirstPremiumDate" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblULIPLastPremiumDate" runat="server" CssClass="FieldName" Text="Last Premium Date:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtULIPLastPremiumDate" runat="server" AutoPostBack="true" CssClass="txtField"
                OnTextChanged="txtULIPFirstPremiumDate_TextChanged"></asp:TextBox>
            <cc1:CalendarExtender ID="ceULIPLastPremiumDate" runat="server" TargetControlID="txtULIPLastPremiumDate"
                Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckLastPremiumDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="tbweULIPLastPremiumDate" runat="server" TargetControlID="txtULIPLastPremiumDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <span id="Span56" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvULIPLastPremiumDate" ControlToValidate="txtULIPLastPremiumDate"
                ErrorMessage="Please enter the Last Premium Date" Display="Dynamic" runat="server"
                ValidationGroup="vgSubmit" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvULIPLastPremiumDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyyyy"
                Type="Date" ControlToValidate="txtULIPLastPremiumDate" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trULIPPremiumCycle" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblUlipPremiuimPeriod" runat="server" CssClass="FieldName" Text="Premium Period (Days):"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtUlipPremiuimPeriod" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span20" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvMPPremiumDuration" ControlToValidate="txtMPPremiumDuration"
                        ErrorMessage="Please enter the Premium Period" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator59" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtUlipPremiuimPeriod" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblULIPPremiumPayDate" runat="server" CssClass="FieldName" Text="Premium Payment Date:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlULIPPrPayDate" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span24" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator24" runat="server" ErrorMessage="Please Select Premium Date"
                ValidationGroup="vgSubmit" Type="String" ControlToValidate="ddlULIPPrPayDate"
                Operator="NotEqual" ValueToCompare="Select Premium Date" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trULIPGracePeriod" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblULIPGracePeriod" runat="server" CssClass="FieldName" Text="Grace Period:"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtULIPGracePeriod" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span25" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvULIPGracePeriod" ControlToValidate="txtULIPGracePeriod"
                        ErrorMessage="Please enter the Grace Period" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator25" runat="server" ErrorMessage="Please enter an integer value"
                Type="Integer" ControlToValidate="txtULIPGracePeriod" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trTPPremiumAmount" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblTPPremiumAmount" runat="server" CssClass="FieldName" Text="Premium Installment Amount:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtTPPremiumAmount" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span26" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvTPPremiumAmount" ControlToValidate="txtTPPremiumAmount"
                        ErrorMessage="Please enter the Premium Amount" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator26" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtTPPremiumAmount" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblTPPremiumCycle" runat="server" CssClass="FieldName" Text="Premium Cycle:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlTPPremiumFrequencyCode" runat="server" CssClass="cmbField"
                onchange="SinglePaymentTPSelection();">
            </asp:DropDownList>
            <span id="Span27" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="ddlTPPremiumFrequencyCode"
                ValidationGroup="vgSubmit" ErrorMessage="Please select a Premium Cycle" Operator="NotEqual"
                ValueToCompare="Select a Frequency Code" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trTPPremiumFirstLast" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblTPFirstPremiumDate" runat="server" CssClass="FieldName" Text="First Premium Date:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtTPFirstPremiumDate" runat="server" CssClass="txtField" OnTextChanged="txtTPLastPremiumDate_TextChanged"
                AutoPostBack="true"></asp:TextBox>
            <cc1:CalendarExtender ID="ceTPFirstPremiumDate" runat="server" TargetControlID="txtTPFirstPremiumDate"
                Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckLastPremiumDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="tbweTPFirstPremiumDate" runat="server" TargetControlID="txtTPFirstPremiumDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <span id="Span57" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvTPFirstPremiumDate" ControlToValidate="txtTPFirstPremiumDate"
                ErrorMessage="Please enter the First Premium Date" Display="Dynamic" runat="server"
                ValidationGroup="vgSubmit" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvTPFirstPremiumDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyyyy"
                Type="Date" ControlToValidate="txtTPFirstPremiumDate" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblTPLastPremiumDate" runat="server" CssClass="FieldName" Text="Last Premium Date:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtTPLastPremiumDate" runat="server" CssClass="txtField" OnTextChanged="txtTPLastPremiumDate_TextChanged"
                AutoPostBack="true"></asp:TextBox>
            <cc1:CalendarExtender ID="ceTPLastPremiumDate" runat="server" TargetControlID="txtTPLastPremiumDate"
                Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckLastPremiumDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="tbweTPLastPremiumDate" runat="server" TargetControlID="txtTPLastPremiumDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <span id="Span58" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvTPLastPremiumDate" ControlToValidate="txtTPLastPremiumDate"
                ErrorMessage="Please enter the Last Premium Date" Display="Dynamic" runat="server"
                ValidationGroup="vgSubmit" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvTPLastPremiumDate" runat="server" ErrorMessage="The date format should be dd/mm/yyyyyy"
                Type="Date" ControlToValidate="txtTPLastPremiumDate" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trTPPremiumPeriod" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblTPPremiumPeriod" runat="server" CssClass="FieldName" Text="Premium Period (Days):"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtTPPremiumDuration" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span28" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvTPPremiumDuration" ControlToValidate="txtTPPremiumDuration"
                        ErrorMessage="Please select the Premium Period" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator27" runat="server" ErrorMessage="Please enter an integer value"
                Type="Double" ControlToValidate="txtTPPremiumDuration" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblTPPremiumPayDate" runat="server" CssClass="FieldName" Text="Premium Payment Date:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlTPPrPayDate" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span29" class="spnRequiredField">*</span>
            <%-- <asp:RequiredFieldValidator ID="rfvTPPrPayDate" ControlToValidate="ddlTPPrPayDate"
                ErrorMessage="Please Enter Premium Payment Date" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator28" runat="server" ErrorMessage="Please Select Premium Date"
                ValidationGroup="vgSubmit" Type="String" ValueToCompare="Select Premium Date"
                ControlToValidate="ddlTPPrPayDate" Operator="NotEqual" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
            <%-- <asp:CompareValidator ID="cvRange5" runat="server" ErrorMessage="Premium Payment Date has to be less than or equal to 31"
                Type="Integer" ControlToValidate="ddlTPPrPayDate" Operator="LessThanEqual" ValueToCompare="31"
                CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>--%>
        </td>
    </tr>
    <tr id="trTPGracePeriod" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblTPGracePeriod" runat="server" CssClass="FieldName" Text="Grace Period:"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtTPGracePeriod" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span30" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvTPGracePeriod" ControlToValidate="txtTPGracePeriod"
                        ErrorMessage="Please enter the Grace Period" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator29" runat="server" ErrorMessage="Please enter an integer value"
                Type="Integer" ControlToValidate="txtTPGracePeriod" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trTPPremiumAccumn" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblTPPremiumAccum" runat="server" CssClass="FieldName" Text="Premium Accumulated:"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtTPPremiumAccum" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span31" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvTPPremiumAccum" ControlToValidate="txtTPPremiumAccum"
                        ErrorMessage="Please enter the Premium Accumulated" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator30" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtTPPremiumAccum" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trMPHeader" runat="server" visible="true">
        <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Moneyback Schedule
            </div>
        </td>
    </tr>
    <%--  <tr id="trMPHeader" runat="server">
        <td colspan="6">
            <asp:Label ID="Label29" runat="server" CssClass="HeaderTextSmall" Text="Moneyback Schedule"></asp:Label>
            <hr />
        </td>
    </tr>--%>
    <tr id="trMPPolicyTerm" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="Label30" runat="server" CssClass="FieldName" Text="Policy Term:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtMPPolicyTerm" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span32" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvMPPolicyTerm" ControlToValidate="txtMPPolicyTerm"
                ErrorMessage="Please enter the Policy Term" Display="Dynamic" runat="server"
                ValidationGroup="vgSubmit" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator31" runat="server" ErrorMessage="Please enter an integer value"
                Type="Double" ControlToValidate="txtMPPolicyTerm" Operator="DataTypeCheck" CssClass="cvPCG"
                ValidationGroup="vgSubmit" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="Label31" runat="server" CssClass="FieldName" Text="No. of Moneyback episodes:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtMoneyBackEpisode" runat="server" CssClass="txtField" AutoPostBack="True"
                OnTextChanged="txtMoneyBackEpisode_TextChanged"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator32" runat="server" ErrorMessage="Please enter an integer value"
                Type="Integer" ControlToValidate="txtMoneyBackEpisode" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trMPDetails" runat="server" visible="false">
        <td colspan="2">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Dates Of payment"></asp:Label>
        </td>
        <td colspan="2">
            <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Percentage of SA repaid"></asp:Label>
        </td>
        <td colspan="2">
        </td>
    </tr>
    <asp:Panel ID="pnlMoneyBackEpisode" runat="server" Visible="false">
        <tr>
            <td colspan="6">
            </td>
        </tr>
        <tr>
            <td colspan="6" align="left">
                <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
    </asp:Panel>
    <tr id="trMPSpace" runat="server">
        <td colspan="6">
        </td>
    </tr>
    <tr id="trULIPHeader" runat="server" visible="true">
        <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                ULIP Investment Allocation Schedule
            </div>
        </td>
    </tr>
    <%-- <tr id="trULIPHeader" runat="server">
        <td align="left" colspan="6">
            <asp:Label ID="Label32" runat="server" CssClass="HeaderTextSmall" Text="ULIP Investment Allocation Schedule"></asp:Label>
            <hr />
        </td>
    </tr>--%>
    <asp:Panel ID="pnlGridView" runat="server">
        <tr id="trULIPAllocation" runat="server">
            <td align="left" colspan="6">
                <telerik:RadGrid ID="rgULIPSubPlanSchedule" runat="server" Skin="Telerik" CssClass="RadGrid"
                    GridLines="None" AllowPaging="True" PageSize="20" AllowSorting="False" AutoGenerateColumns="False"
                    ShowStatusBar="true" AllowAutomaticDeletes="false" AllowAutomaticInserts="false"
                    AllowAutomaticUpdates="false" OnItemCommand="rgULIPSubPlanSchedule_ItemCommand"
                    OnItemCreated="rgULIPSubPlanSchedule_ItemCreated" OnNeedDataSource="rgULIPSubPlanSchedule_NeedDataSource"
                    OnUpdateCommand="rgULIPSubPlanSchedule_UpdateCommand" HorizontalAlign="NotSet"
                    DataKeyNames="ISF_SchemeFundId" Width="100%">
                    <MasterTableView CommandItemDisplay="Top" EditMode="PopUp" CssClass="TableBackground"
                        TableLayout="Fixed" Width="100%">
                        <CommandItemSettings ShowRefreshButton="false" />
                        <Columns>
                            <telerik:GridEditCommandColumn HeaderText="Edit" UniqueName="EditCommandColumn">
                                <HeaderStyle Width="10%" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn UniqueName="IF_FundName" HeaderText="Sub Plan Name" DataField="IF_FundName">
                                <HeaderStyle Width="20%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CINPUD_InvestedCost" HeaderText="Funds Allocated"
                                DataField="CINPUD_InvestedCost">
                                <HeaderStyle Width="10%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CINPUD_Unit" HeaderText="Unit" DataField="CINPUD_Unit">
                                <HeaderStyle Width="10%" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn UniqueName="CIUP_PurchaseDate" HeaderText="Purchase Date" DataField="CIUP_PurchaseDate">
                                <HeaderStyle Width="10%" />
                            </telerik:GridBoundColumn>--%>
                            <%--<telerik:GridDateTimeColumn UniqueName="CINPUD_PurchaseDate" SortExpression="CINPUD_PurchaseDate"
                                HeaderText="Purchase Date" DataField="CINPUD_PurchaseDate" DataFormatString="{0:d}" />--%>
                            <telerik:GridBoundColumn UniqueName="CINPUD_CurrentValue" HeaderText="Current Value"
                                DataField="CINPUD_CurrentValue">
                                <HeaderStyle Width="10%" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn UniqueName="CIUP_AbsoluteReturn" HeaderText="Absolute Return" DataField="CIUP_AbsoluteReturn">
                                <HeaderStyle Width="10%" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn HeaderText="Absolute Return" UniqueName="CINPUD_AbsoluteReturn"
                                DataField="CINPUD_AbsoluteReturn">
                                <HeaderStyle Width="10%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CINPUD_AllocationPer" HeaderText="Allocation Percentage"
                                DataField="CINPUD_AllocationPer">
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn UniqueName="Equity" HeaderText="Equity" DataField="Equity"
                                Display="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Debt" HeaderText="Debt" DataField="Debt" Display="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Cash" HeaderText="Cash" DataField="Cash" Display="false">
                            </telerik:GridBoundColumn>--%>
                            <%--<telerik:GridBoundColumn UniqueName="WUSP_ULIPSubPlanCode" HeaderText="SubPlanCode" DataField="WUSP_ULIPSubPlanCode" Display="false">
                            </telerik:GridBoundColumn>--%>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </asp:Panel>
    <asp:Panel ID="pnlUlip" runat="server" Visible="false">
        <tr>
            <td align="left" colspan="6">
                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
    </asp:Panel>
    <tr id="trULIPError" runat="server">
        <td align="center" colspan="6">
            <asp:Label ID="lblError" runat="server" CssClass="FieldName" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr id="tr3" runat="server" visible="true">
        <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Nominee Details
            </div>
        </td>
    </tr>
    <%-- <tr>
        <td colspan="6">
            <asp:Label ID="lblNomineeDetails" runat="server" CssClass="HeaderTextSmall" Text="Nominee Details"></asp:Label>
            <hr />
        </td>
    </tr>--%>
    <tr id="trNominees" runat="server">
        <td colspan="4">
            <asp:GridView ID="gvNominee" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ShowFooter="true" DataKeyNames="MemberCustomerId, AssociationId" AllowSorting="True"
                OnRowDataBound="gvNominee_RowDataBound" CssClass="GridViewStyle">
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <RowStyle CssClass="RowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkId0" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Name" DataField="Name" />
                    <asp:BoundField HeaderText="Relationship" DataField="Relationship" />
                    <asp:BoundField HeaderText="Id" DataField="MemberCustomerId" />
                </Columns>
                <FooterStyle CssClass="FooterStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
            </asp:GridView>
        </td>
    </tr>
    <tr id="trNoNominee" runat="server" visible="false">
        <td class="Message" colspan="2">
            <asp:Label ID="lblNoNominee" runat="server" Text="You have no Associations" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr id="trValuationHeader" runat="server" visible="false">
        <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Valuation
            </div>
        </td>
    </tr>
    <%-- <tr id="trValuationHeader" runat="server">
        <td align="left" colspan="6">
            <asp:Label ID="Label27" runat="server" CssClass="HeaderTextSmall" Text="Valuation"></asp:Label>
            <hr />
        </td>
    </tr>--%>
    <tr id="trEPBonus" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="Label12" runat="server" CssClass="FieldName" Text="Premium Accumulated:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtEPPremiumAccumulated" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span34" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvEPPremiumAccumulated" ControlToValidate="txtEPPremiumAccumulated"
                        ErrorMessage="Please enter the Premium Accumulated" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator33" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtEPPremiumAccumulated" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblBonus" runat="server" CssClass="FieldName" Text="Bonus Accumulated:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtEPBonusAccumulated" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span35" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvEPBonusAccumulated" ControlToValidate="txtEPBonusAccumulated"
                        ErrorMessage="Please enter the Bonus Accumulated" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator34" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtEPBonusAccumulated" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trEPSurrenderValue" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblEPSurrenderValue" runat="server" CssClass="FieldName" Text="Surrender Value:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtEPSurrenderValue" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span36" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvEPSurrenderValue" ControlToValidate="txtEPSurrenderValue"
                        ErrorMessage="Please enter the Surrender Value" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator35" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtEPSurrenderValue" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblEPMaturityValue" runat="server" CssClass="FieldName" Text="Maturity Value:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtEPMaturityValue" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span37" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvEPMaturityValue" ControlToValidate="txtEPMaturityValue"
                        ErrorMessage="Please enter the Maturity Value" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator36" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtEPMaturityValue" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trOTBonus" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblOTBonus" runat="server" CssClass="FieldName" Text="Premium Accumulated:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtOTPremiumAccumulated" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span34" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvEPPremiumAccumulated" ControlToValidate="txtOTPremiumAccumulated"
                        ErrorMessage="Please enter the Premium Accumulated" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator55" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtOTPremiumAccumulated" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblOTPBonus" runat="server" CssClass="FieldName" Text="Bonus Accumulated:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtOTBonusAccumulated" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span35" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvEPBonusAccumulated" ControlToValidate="txtOTBonusAccumulated"
                        ErrorMessage="Please enter the Bonus Accumulated" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator56" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtOTBonusAccumulated" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trOTSurrenderValue" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblOTSurrenderValue" runat="server" CssClass="FieldName" Text="Surrender Value:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtOTSurrenderValue" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span36" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvEPSurrenderValue" ControlToValidate="txtOTSurrenderValue"
                        ErrorMessage="Please enter the Surrender Value" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator57" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtOTSurrenderValue" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblOTMaturityValue" runat="server" CssClass="FieldName" Text="Maturity Value:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtOTMaturityValue" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span37" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvEPMaturityValue" ControlToValidate="txtOTMaturityValue"
                        ErrorMessage="Please enter the Maturity Value" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator58" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtOTMaturityValue" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trMPBonus" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="Label13" runat="server" CssClass="FieldName" Text="Premium Accumulated:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtMPPremiumAccumulated" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span38" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvMPPremiumAccumulated" ControlToValidate="txtMPPremiumAccumulated"
                        ErrorMessage="Please enter the Premium Accumulated" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator37" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtMPPremiumAccumulated" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="Label14" runat="server" CssClass="FieldName" Text="Bonus Accumulated:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtMPBonusAccumulated" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span39" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvMPBonusAccumulated" ControlToValidate="txtMPBonusAccumulated"
                        ErrorMessage="Please enter the Bonus Accumulated" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator38" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtMPBonusAccumulated" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trMPSurrenderValue" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblMPSurrenderValue" runat="server" CssClass="FieldName" Text="Surrender Value:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtMPSurrenderValue" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span40" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvMPSurrenderValue" ControlToValidate="txtMPSurrenderValue"
                        ErrorMessage="Please enter the Surrender Value" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator39" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtMPSurrenderValue" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblMPMaturityValue" runat="server" CssClass="FieldName" Text="Maturity Value:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtMPMaturityValue" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span41" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvMPMaturityValue" ControlToValidate="txtMPMaturityValue"
                        ErrorMessage="Please enter the Maturity Value" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator40" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtMPMaturityValue" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trULIPSurrenderValue" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblULIPSurrenderValue" runat="server" CssClass="FieldName" Text="Surrender Value:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtULIPSurrenderValue" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span42" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvULIPSurrenderValue" ControlToValidate="txtULIPSurrenderValue"
                        ErrorMessage="Please enter the Surrender Value" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator41" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtULIPSurrenderValue" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblULIPMaturityValue" runat="server" CssClass="FieldName" Text="Bonus Accumulated:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtULIPMaturityValue" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span43" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvULIPMaturityValue" ControlToValidate="txtULIPMaturityValue"
                        ErrorMessage="Please enter the Bonus Accumulated" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator42" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtULIPMaturityValue" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trULIPCharges" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblMortalityCharges" runat="server" CssClass="FieldName" Text="Mortality  Charges:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtMortalityCharges" runat="server" CssClass="txtField"></asp:TextBox>
            <asp:CompareValidator ID="cvMortalityCharges" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtMortalityCharges" Operator="DataTypeCheck"
                CssClass="cvPCG" ValidationGroup="vgSubmit" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="lblULIPCharges" runat="server" CssClass="FieldName" Text="Other Charges:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtULIPCharges" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span44" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvULIPCharges" ControlToValidate="txtULIPCharges"
                        ErrorMessage="Please enter the Charges" Display="Dynamic" runat="server" CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator43" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtULIPCharges" Operator="DataTypeCheck" CssClass="cvPCG"
                ValidationGroup="vgSubmit" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trUlipNAV" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblUlipNAV" runat="server" CssClass="FieldName" Text="NAV:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtUlipNAV" runat="server" CssClass="txtField"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtUlipNAV" Operator="DataTypeCheck" CssClass="cvPCG"
                ValidationGroup="vgSubmit" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trWPBonus" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="Label37" runat="server" CssClass="FieldName" Text="Premium Accumulated:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtWPPremiumAccumulated" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span45" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvWPPremiumAccumulated" ControlToValidate="txtWPPremiumAccumulated"
                        ErrorMessage="Please enter the Premium Accumulated" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator44" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtWPPremiumAccumulated" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="Label38" runat="server" CssClass="FieldName" Text="Bonus Accumulated:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtWPBonusAccumulated" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span46" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvWPBonusAccumulated" ControlToValidate="txtWPBonusAccumulated"
                        ErrorMessage="Please enter the Bonus Accumulated" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator45" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtWPBonusAccumulated" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trWPSurrenderValue" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="Label39" runat="server" CssClass="FieldName" Text="Surrender Value:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtWPSurrenderValue" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span47" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvWPSurrenderValue" ControlToValidate="txtWPSurrenderValue"
                        ErrorMessage="Please enter the Surrender Value" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator46" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtWPSurrenderValue" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" colspan="2">
            <asp:Label ID="Label40" runat="server" CssClass="FieldName" Text="Maturity Value:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtWPMaturityValue" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span48" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="rfvWPMaturityValue" ControlToValidate="txtWPMaturityValue"
                        ErrorMessage="Please enter the Maturity Value" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>--%>
            <asp:CompareValidator ID="CompareValidator47" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtWPMaturityValue" Operator="DataTypeCheck"
                ValidationGroup="vgSubmit" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trEPRemarks" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblEPRemarks" runat="server" CssClass="FieldName" Text="Remarks:"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtEPRemarks" runat="server" CssClass="txtField" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr id="trOTRemarks" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblOTRemarks" runat="server" CssClass="FieldName" Text="Remarks:"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtOTRemarks" runat="server" CssClass="txtField" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr id="trWLPRemarks" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblWLPRemarks" runat="server" CssClass="FieldName" Text="Remarks:"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtWLPRemarks" runat="server" CssClass="txtField" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr id="trTPRemarks" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblTPRemarks" runat="server" CssClass="FieldName" Text="Remarks:"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtTPRemarks" runat="server" CssClass="txtField" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr id="trMPRemarks" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblMPRemarks" runat="server" CssClass="FieldName" Text="Remarks:"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtMPRemarks" runat="server" CssClass="txtField" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr id="trULIPRemarks" runat="server">
        <td class="leftField" colspan="2">
            <asp:Label ID="lblULIPRemarks" runat="server" CssClass="FieldName" Text="Remarks:"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txtULIPRemarks" runat="server" CssClass="txtField" TextMode="MultiLine"></asp:TextBox>
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
    
    <table width="120%" >
            <tr visible="false" runat="server">
                <td colspan="6" >
                    <div class="divSectionHeading"  style="vertical-align: text-bottom">
                        Payment Section
                    </div>
                </td>
            </tr>
        </table>
     <asp:Panel ID="pnl_BUY_ABY_SIP_PaymentSection" runat="server" Visible="false"  class="Landscape" Width="100%"
            Height="80%" ScrollBars="None">
            <table id="tb_BUY_ABY_SIP_PaymentSection" width="100%">
               
                <tr id="trAmount" runat="server">
                    <td align="right" style="width: 18%;">
                        <asp:Label ID="lblAmount" runat="server" Text="Amount:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td style="width: 22%;">
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField" CausesValidation="true"
                            ValidationGroup="vgSubmit" ></asp:TextBox>
                            <span id="Span1" class="spnRequiredField">*</span>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtAmount"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select amount" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator2" Display="Dynamic" ValidationGroup="vgSubmit"
                            runat="server" ErrorMessage="<br />Please enter a numeric value" ControlToValidate="txtAmount"
                            MaximumValue="2147483647" MinimumValue="1" Type="Double" CssClass="cvPCG"></asp:RangeValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtAmount"
                            runat="server" ValidationGroup="vgSubmit" ErrorMessage="Please enter a valid amount/Unit" CssClass="cvPCG"
                            ValidationExpression="^(-)?\d+(\.\d\d\d\d)?$">
                        </asp:RegularExpressionValidator>
                    </td>
                    <td align="right" style="width: 22.5%;">
                        <asp:Label ID="lblMode" runat="server" Text="Mode Of Payment:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged"
                            AutoPostBack="true" >
                            <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                            <asp:ListItem Text="Cheque" Value="CQ"></asp:ListItem>
                            <asp:ListItem Text="Draft" Value="DF"></asp:ListItem>
                            <asp:ListItem Text="ECS" Value="ES" Enabled="false"></asp:ListItem>
                        </asp:DropDownList>
                        <span id="Span13" class="spnRequiredField">*</span>
                        <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="ddlPaymentMode"
                            CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select  Mode Of Payment"
                            Operator="NotEqual" ValidationGroup="vgSubmit" ValueToCompare="Select"></asp:CompareValidator>
                    </td>
                </tr>
                <tr id="trPINo" runat="server" visible="false">
                    <td align="right">
                        <asp:Label ID="lblPaymentNumber" runat="server" Text="Payment Instrument Number: "
                            CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPaymentNumber" runat="server" MaxLength="6" CssClass="txtField"
                           ></asp:TextBox>
                        <span id="Span15" class="spnRequiredField">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtPaymentNumber"
                            ErrorMessage="<br />Please Enter a Payment Instrument No." Display="Dynamic"
                            runat="server" CssClass="rfvPCG" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblPIDate" runat="server" Text="Payment Instrument Date:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="txtPaymentInstDate" CssClass="txtField" runat="server"
                            Culture="English (United States)" Skin="Telerik" EnableEmbeddedSkins="false"
                            ShowAnimation-Type="Fade" AutoPostBack="true" MinDate="1900-01-01" 
                            OnSelectedDateChanged="txtPaymentInstDate_OnSelectedDateChanged">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <span id="Span17" class="spnRequiredField">*</span>
                        <asp:CompareValidator ID="CompareValidator60" runat="server" ErrorMessage="<br/>Please enter a valid date."
                            Type="Date" ControlToValidate="txtPaymentInstDate" CssClass="cvPCG" Operator="DataTypeCheck"
                            ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
                     
                    </td>
                </tr>
                <tr id="trBankName" runat="server">
                    <td align="right">
                        <asp:Label ID="lblBankName" runat="server" Text="Bank Name:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBankName" runat="server" CssClass="cmbField" AutoPostBack="true"
                            AppendDataBoundItems="false" OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged"
                           >
                        </asp:DropDownList>
                        <span id="Span18" class="spnRequiredField">*</span>
                        <asp:ImageButton ID="imgAddBank" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                            AlternateText="Add" runat="server" ToolTip="Click here to Add Bank" OnClientClick="return openpopupAddBank()"
                            Height="15px" Width="15px" Visible="false"></asp:ImageButton>
                       
                        <asp:ImageButton ID="imgBtnRefereshBank" ImageUrl="~/Images/refresh.png" AlternateText="Refresh"
                            runat="server" ToolTip="Click here to refresh Bank List" OnClick="imgBtnRefereshBank_OnClick"
                            OnClientClick="return closepopupAddBank()" Height="15px" Width="25px" 
                            Visible="false"></asp:ImageButton>
                        <asp:CompareValidator ID="CompareValidator61" runat="server" ControlToValidate="ddlBankName"
                            CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a Bank"
                            Operator="NotEqual" ValidationGroup="vgSubmit" ValueToCompare="Select"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ControlToValidate="ddlBankName"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select an Bank" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="vgSubmit" ></asp:RequiredFieldValidator>
                        
                    </td>
                    <td align="right">
                        <asp:Label ID="lblBranchName" runat="server" Text="Bank Branch:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="cmbField" AutoPostBack="false"
                            AppendDataBoundItems="true" TabIndex="36" Visible="false">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtBranchName" runat="server" CssClass="txtField" ></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    <table id="Table1" runat="server" visible="false">
            <tr id="trFrequencySTP" runat="server" visible="false">
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblFrequencySTP" runat="server" Text="Frequency:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <asp:DropDownList ID="ddlFrequencySTP" runat="server" CssClass="cmbField" TabIndex="38">
                    </asp:DropDownList>
                </td>
                <td class="leftField" colspan="2" style="width: 40%">
                </td>
            </tr>
            <tr id="trSTPStart" runat="server" visible="false">
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblstartDateSTP" runat="server" Text="Start Date:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <telerik:RadDatePicker ID="txtstartDateSTP" CssClass="txtField" runat="server" Culture="English (United States)"
                        Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                        TabIndex="39">
                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                            Skin="Telerik" EnableEmbeddedSkins="false">
                        </Calendar>
                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                    <span id="Span25" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ControlToValidate="txtstartDateSTP"
                        CssClass="rfvPCG" ErrorMessage="<br />Please select StartDate" Display="Dynamic"
                        runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                </td>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblendDateSTP" runat="server" Text="End Date:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" style="width: 20%">
                    <telerik:RadDatePicker ID="txtendDateSTP" CssClass="txtField" runat="server" Culture="English (United States)"
                        Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                        TabIndex="40">
                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                            Skin="Telerik" EnableEmbeddedSkins="false">
                        </Calendar>
                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                    <span id="Span26" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ControlToValidate="txtendDateSTP"
                        CssClass="rfvPCG" ErrorMessage="<br />Please select End Date" Display="Dynamic"
                        runat="server" InitialValue="" ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator62" runat="server" ErrorMessage="<br/>To date should be greater than from date."
                        Type="Date" ControlToValidate="txtendDateSTP" CssClass="cvPCG" Operator="GreaterThan"
                        ControlToCompare="txtstartDateSTP" Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>
                </td>
            </tr>
        </table>
    
    
    
    <tr id="trSubmitButton" runat="server">
        <td colspan="6" class="SubmitCell">
            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" Text="Submit" OnClick="btnSubmit_Click"
                ValidationGroup="vgSubmit" CausesValidation="true" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_PortfolioInsuranceEntry_btnSubmit', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_PortfolioInsuranceEntry_btnSubmit', 'S');" />
        </td>
    </tr>
    <tr id="trDeleteButton" runat="server">
        <td colspan="6" class="SubmitCell">
            <asp:Button ID="btnDelete" runat="server" CssClass="PCGButton" Text="Delete" OnClick="btnDelete_Click"
                CausesValidation="false" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_PortfolioInsuranceEntry_btnDelete', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_PortfolioInsuranceEntry_btnDelete', 'S');" />
        </td>
    </tr>
</table>
